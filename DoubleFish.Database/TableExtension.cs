using System;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace DoubleFish.Database
{
	public static class TableExtension
	{
		/// <summary>
		/// 批量删除
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="table">表</param>
		/// <param name="predicate">查询条件表达式</param>
		/// <returns>影响的行数</returns>
		public static int Delete<T> (this Table<T> table, Expression<Func<T, bool>> predicate) where T : class
		{
			//获取表名
			//string tableName = table.Context.Mapping.GetTable(typeof(T)).TableName;
			string tableName = table.GetTableName();

			//查询条件表达式转换成SQL的条件语句
			ConditionBuilder builder = new ConditionBuilder();
			builder.Build(predicate.Body);
			string sqlCondition = builder.Condition;

			//SQL命令
			string commandText = string.Format("DELETE FROM {0} WHERE {1}", tableName, sqlCondition);

			//获取SQL参数数组 
			var args = builder.Arguments;

			//执行
			return table.Context.ExecuteCommand(commandText, args);
		}

		/// <summary>
		/// 批量删除
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="table">表</param>
		/// <param name="predicate">查询条件表达式</param>
		/// <returns>影响的行数</returns>
		//public static int Delete<T> (this Table<T> table, Expression<Func<T, bool>> predicate) where T : class
		//{
		//    //查询条件表达式转换成SQL的条件语句
		//    DbCommand command = table.Context.GetCommand(table.Where(predicate));

		//    DbParameter[] parameters = new DbParameter[command.Parameters.Count];
		//    command.Parameters.CopyTo(parameters, 0);

		//    string sqlBlock = string.Join(", ", parameters.Select(c => string.Format("[{0}]=@{0}", c.ParameterName)).ToArray());

		//    //SQL命令
		//    command.CommandText = string.Format("DELETE FROM {0} WHERE {1}", table.GetTableName(), sqlBlock);

		//    //执行 
		//    return ExecuteNonQuery(command);
		//}

		/// <summary>
		/// 批量更新
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="table">表</param>
		/// <param name="predicate">查询条件表达式</param>
		/// <param name="updater">更新表达式</param>
		/// <returns>影响的行数</returns>
		public static int Update<T> (this Table<T> table, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updater) where T : class
		{
			DbCommand command = table.Context.GetCommand(table.Where(predicate));
			string sqlCondition = command.CommandText;
			sqlCondition = sqlCondition.Substring(sqlCondition.LastIndexOf("WHERE ", StringComparison.InvariantCultureIgnoreCase) + 6);

			//获取Update的赋值语句
			MemberInitExpression expression = (MemberInitExpression)updater.Body;
			DbParameter[] parameters = expression.Bindings.Cast<MemberAssignment>().Select(c =>
			{
				var p = command.CreateParameter();
				p.ParameterName = c.Member.Name;
				//p.Value = ((ConstantExpression)c.Expression).Value;
				p.Value = Expression.Lambda(c.Expression, null).Compile().DynamicInvoke();
				return p;
			}).ToArray();

			string sqlBlock = string.Join(", ", parameters.Select(c => string.Format("[{0}]=@{0}", c.ParameterName)).ToArray());

			//SQL命令
			command.CommandText = string.Format("UPDATE {0} SET {1} FROM {0} AS t0 WHERE {2}", table.GetTableName(), sqlBlock, sqlCondition);

			//获取SQL参数数组 (包括查询参数和赋值参数)
			command.Parameters.AddRange(parameters);

			//执行 
			return ExecuteNonQuery(command);
		}

		/// <summary>
		/// Immediately updates all entities in the collection with a single update command based on a <typeparamref name="TEntity"/> created from a Lambda expression.
		/// </summary>
		/// <typeparam name="TEntity">Represents the object type for rows contained in <paramref name="table"/>.</typeparam>
		/// <param name="table">Represents a table for a particular type in the underlying database containing rows are to be updated.</param>
		/// <param name="filter">Represents a filter of items to be updated in <paramref name="table"/>.</param>
		/// <param name="evaluator">A Lambda expression returning a <typeparamref name="TEntity"/> that defines the update assignments to be performed on each item in <paramref name="entities"/>.</param>
		/// <returns>The number of rows updated in the database.</returns>
		/// <remarks>
		/// <para>Similiar to stored procedures, and opposite from similiar InsertAllOnSubmit, rows provided in <paramref name="entities"/> will be updated immediately with no need to call <see cref="DataContext.SubmitChanges()"/>.</para>
		/// <para>Additionally, to improve performance, instead of creating an update command for each item in <paramref name="entities"/>, a single update command is created.</para>
		/// </remarks>
		public static int UpdateBatch<TEntity> (this Table<TEntity> table, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> evaluator) where TEntity : class
		{
			return table.UpdateBatch(table.Where(filter), evaluator);
		}

		/// <summary>
		/// Immediately updates all entities in the collection with a single update command based on a <typeparamref name="TEntity"/> created from a Lambda expression.
		/// </summary>
		/// <typeparam name="TEntity">Represents the object type for rows contained in <paramref name="table"/>.</typeparam>
		/// <param name="table">Represents a table for a particular type in the underlying database containing rows are to be updated.</param>
		/// <param name="entities">Represents the collection of items which are to be updated in <paramref name="table"/>.</param>
		/// <param name="evaluator">A Lambda expression returning a <typeparamref name="TEntity"/> that defines the update assignments to be performed on each item in <paramref name="entities"/>.</param>
		/// <returns>The number of rows updated in the database.</returns>
		/// <remarks>
		/// <para>Similiar to stored procedures, and opposite from similiar InsertAllOnSubmit, rows provided in <paramref name="entities"/> will be updated immediately with no need to call <see cref="DataContext.SubmitChanges()"/>.</para>
		/// <para>Additionally, to improve performance, instead of creating an update command for each item in <paramref name="entities"/>, a single update command is created.</para>
		/// </remarks>
		public static int UpdateBatch<T> (this Table<T> table, IQueryable<T> entities, Expression<Func<T, T>> evaluator) where T : class
		{
			DbCommand command = table.GetUpdateBatchCommand<T>(entities, evaluator);

			//执行
			//var parameters = from p in command.Parameters.Cast<DbParameter>()
			//                 select p.Value;

			//return table.Context.ExecuteCommand(command.CommandText, parameters.ToArray());

			return ExecuteNonQuery(command);
		}

		private static DbCommand GetUpdateBatchCommand<T> (this Table<T> table, IQueryable<T> entities, Expression<Func<T, T>> evaluator) where T : class
		{
			DbCommand command = table.Context.GetCommand(entities);

			//获取Update的赋值语句
			MemberInitExpression expression = (MemberInitExpression)evaluator.Body;
			DbParameter[] parameters = expression.Bindings.Cast<MemberAssignment>().Select(c =>
			{
				var p = command.CreateParameter();
				p.ParameterName = c.Member.Name;
				//p.Value = ((ConstantExpression)c.Expression).Value;
				p.Value = Expression.Lambda(c.Expression, null).Compile().DynamicInvoke();
				return p;
			}).ToArray();

			string sqlBlock = string.Join(", ", parameters.Select(c => string.Format("[{0}]=@{0}", c.ParameterName)).ToArray());

			//获取SQL参数数组 (包括查询参数和赋值参数)
			command.Parameters.AddRange(parameters);

			// Complete the command text by concatenating bits together.
			command.CommandText = string.Format("UPDATE {0}\r\nSET {1}\r\n{2}",
															table.GetTableName(),									// Database table name
															sqlBlock,									// SET fld = {}, fld2 = {}, ...
															GetBatchJoinQuery<T>(table, entities));	// Subquery join created from entities command text

			if (command.CommandText.IndexOf("[arg0]") >= 0 || command.CommandText.IndexOf("NULL AS [EMPTY]") >= 0)
			{
				throw new NotSupportedException(string.Format("The evaluator Expression<Func<{0},{0}>> has processing that needs to be performed once the query is returned (i.e. string.Format()) and therefore can not be used during batch updating.", table.GetType()));
			}

			return command;
		}

		private static string GetBatchJoinQuery<T> (Table<T> table, IQueryable<T> entities) where T : class
		{
			MetaTable metaTable = table.Context.Mapping.GetTable(typeof(T));

			var keys = from mdm in metaTable.RowType.DataMembers
					   where mdm.IsPrimaryKey
					   select new { mdm.MappedName };

			StringBuilder joinBuilder = new StringBuilder();
			StringBuilder subSelectBuilder = new StringBuilder();

			foreach (var key in keys)
			{
				joinBuilder.AppendFormat("j0.[{0}] = j1.[{0}] AND ", key.MappedName);
				subSelectBuilder.AppendFormat("[t0].[{0}], ", key.MappedName);
			}

			DbCommand selectCommand = table.Context.GetCommand(entities);
			string select = selectCommand.CommandText;

			string join = joinBuilder.ToString();

			if (string.IsNullOrEmpty(join))
			{
				throw new MissingPrimaryKeyException(string.Format("{0} does not have a primary key defined.  Batch updating/deleting can not be used for tables without a primary key.", metaTable.TableName));
			}

			join = join.Substring(0, join.Length - 5);											// Remove last ' AND '

			int endSelect = select.IndexOf("[t");													// Get 'SELECT ' and any TOP clause if present
			string selectClause = select.Substring(0, endSelect);
			int selectTableNameStart = endSelect + 1;												// Get the table name LINQ to SQL used in query generation
			string selectTableName = select.Substring(selectTableNameStart,							// because I have to replace [t0] with it in the subSelectSB
										select.IndexOf("]", selectTableNameStart) - (selectTableNameStart));

			bool needsTopClause = selectClause.IndexOf(" TOP ") < 0 && select.IndexOf("\r\nORDER BY ") > 0;

			string subSelect = selectClause
								+ (needsTopClause ? "TOP 100 PERCENT " : "")							// If order by in original select without TOP clause, need TOP
								+ subSelectBuilder.ToString()												// Append just the primary keys.
											 .Replace("[t0]", string.Format("[{0}]", selectTableName));
			subSelect = subSelect.Substring(0, subSelect.Length - 2);									// Remove last ', '

			subSelect += select.Substring(select.IndexOf("\r\nFROM ")); // Create a sub SELECT that *only* includes the primary key fields

			string batchJoin = String.Format("FROM {0} AS j0 INNER JOIN (\r\n\r\n{1}\r\n\r\n) AS j1 ON ({2})\r\n", table.GetTableName(), subSelect, join);
			return batchJoin;
		}

		private static string GetTableName<T> (this Table<T> table) where T : class
		{
			Type entityType = typeof(T);
			MetaTable metaTable = table.Context.Mapping.GetTable(entityType);
			string tableName = metaTable.TableName;

			if (!tableName.StartsWith("["))
			{
				string[] parts = tableName.Split('.');
				tableName = string.Format("[{0}]", string.Join("].[", parts));
			}

			return tableName;
		}

		private static int ExecuteNonQuery (DbCommand command)
		{
			try
			{
				if (command.Connection.State != ConnectionState.Open)
				{
					command.Connection.Open();
				}
				return command.ExecuteNonQuery();
			}
			finally
			{
				command.Connection.Close();
				command.Dispose();
			}
		}
	}
}
