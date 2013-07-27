using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace DoubleFish.Database
{
	/// <summary>
	/// DataTable扩展
	/// </summary>
	public static class DataTableExtension
	{

		/// <summary>
		/// 将DataTable转换为对应的实体数组
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataTable"></param>
		/// <returns></returns>
		public static T[] ToArray<T> (this DataTable dataTable) where T : new()
		{
			if (dataTable == null || dataTable.Rows.Count == 0)
				return new T[] { };

			// 定义集合 
			T[] array = new T[dataTable.Rows.Count];

			// 获得此模型的类型 
			Type type = typeof(T);

			T t = new T();
			// 获得此模型的公共属性
			PropertyInfo[] properties = t.GetType().GetProperties();

			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				array[i] = new T();
				foreach (PropertyInfo pi in properties)
				{
					// 检查DataTable是否包含此列 
					if (!dataTable.Columns.Contains(pi.Name))
						continue;

					// 判断此属性是否有Setter 
					if (!pi.CanWrite) continue;

					object value = dataTable.Rows[i][pi.Name];
					if (value == DBNull.Value)
						continue;

					pi.SetValue(array[i], value, null);
				}
			}
			return array;
		}

		/// <summary>
		/// 将DataTable转换为对应的实体列表
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataTable"></param>
		/// <returns></returns>
		public static IList<T> ToList<T> (this DataTable dataTable) where T : new()
		{
			// 定义集合 
			IList<T> list = new List<T>();

			if (dataTable == null || dataTable.Rows.Count == 0)
				return list;

			// 获得此模型的类型 
			Type type = typeof(T);

			T t = new T();
			// 获得此模型的公共属性 
			PropertyInfo[] properties = t.GetType().GetProperties();

			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				list[i] = new T();
				foreach (PropertyInfo pi in properties)
				{
					// 检查DataTable是否包含此列 
					if (!dataTable.Columns.Contains(pi.Name))
						continue;

					// 判断此属性是否有Setter 
					if (!pi.CanWrite) continue;

					object value = dataTable.Rows[i][pi.Name];
					if (value == DBNull.Value)
						continue;

					pi.SetValue(list[i], value, null);
				}
			}
			return list;
		}

		/// <summary>
		/// 将DataTable转换为对应的实体
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dr"></param>
		/// <returns></returns>
		public static T ToModel<T> (this DataTable dataTable) where T : new()
		{
			if (dataTable == null || dataTable.Rows.Count == 0)
				return new T();

			T t = new T();

			// 获得此模型的类型 
			Type type = typeof(T);

			// 获得此模型的公共属性 
			PropertyInfo[] properties = t.GetType().GetProperties();

			foreach (PropertyInfo pi in properties)
			{
				// 检查DataTable是否包含此列 
				if (!dataTable.Columns.Contains(pi.Name))
					continue;

				// 判断此属性是否有Setter 
				if (!pi.CanWrite) continue;

				object value = dataTable.Rows[0][pi.Name];
				if (value == DBNull.Value)
					continue;

				pi.SetValue(t, value, null);
			}
			return t;
		}

		/// <summary>
		/// 将DataRow转换为对应的实体
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataRow"></param>
		/// <returns></returns>
		public static T ToModel<T> (this DataRow dataRow) where T : new()
		{
			if (dataRow == null || dataRow.Table.Columns.Count == 0)
				return new T();

			T t = new T();
			
			// 获得此模型的类型 
			Type type = typeof(T);

			// 获得此模型的公共属性 
			PropertyInfo[] properties = t.GetType().GetProperties();

			foreach (PropertyInfo pi in properties)
			{
				// 检查DataTable是否包含此列 
				if (!dataRow.Table.Columns.Contains(pi.Name))
					continue;

				// 判断此属性是否有Setter 
				if (!pi.CanWrite) continue;

				object value = dataRow[pi.Name];
				if (value == DBNull.Value)
					continue;

				pi.SetValue(t, value, null);
			}
			return t;
		}

		/// <summary>
		/// 返回DataRow中是否包含指定的列
		/// </summary>
		/// <param name="row"></param>
		/// <param name="columnName">列名</param>
		/// <returns></returns>
		public static bool Contains (this DataRow row, string columnName)
		{
			return row.Contains(columnName, row.Table.Columns);
		}

		/// <summary>
		/// 返回DataRow中是否包含指定的列
		/// </summary>
		/// <param name="row"></param>
		/// <param name="columnName"></param>
		/// <param name="columns">列名</param>
		/// <returns></returns>
		public static bool Contains (this DataRow row, string columnName, DataColumnCollection columns)
		{
			if (columns.Contains(columnName))
				return true;
			else
				return false;
		}

		/// <summary>
		/// 返回DataRow中是否包含指定的列并且是否有值
		/// </summary>
		/// <param name="row"></param>
		/// <param name="columnName">列名</param>
		/// <returns></returns>
		public static bool IsNullOrEmpty (this DataRow row, string columnName)
		{
			return row.IsNullOrEmpty(columnName, row.Table.Columns);
		}

		/// <summary>
		/// 返回DataRow中是否包含指定的列并且是否有值
		/// </summary>
		/// <param name="row"></param>
		/// <param name="columnName"></param>
		/// <param name="columns">列名</param>
		/// <returns></returns>
		public static bool IsNullOrEmpty (this DataRow row, string columnName, DataColumnCollection columns)
		{
			if (columns.Contains(columnName) && row[columnName] != DBNull.Value)
				return true;
			else
				return false;
		}
	}
}
