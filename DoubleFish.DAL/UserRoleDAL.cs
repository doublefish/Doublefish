using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;

using DoubleFish.Database;

namespace DoubleFish.DAL
{
	public class UserRoleDAL : DataBase
	{
		/// <summary>
		/// 移动用户
		/// </summary>
		/// <param name="ur">要移动的集合</param>
		/// <param name="role">角色</param>
		/// <param name="isMoveIn">移入/移出</param>
		/// <returns>移动后此角色下的所有用户</returns>
		public void MoveUser (long[] users, long role, bool isMoveIn)
		{
			if (users == null || users.Length == 0)
				return;

			var db = this.GetDatabase();

			if (isMoveIn)//移入
			{
				for (var i = 0; i < users.Length; i++)
				{
					var data = new UserRole();
					data.Role = role;
					data.User = users[i];

					db.UserRole.InsertOnSubmit(data);
				}
			}
			else//移出
			{
				var list = db.UserRole.Where(item => item.Role == role && users.Contains(item.User)).ToArray();
				db.UserRole.DeleteAllOnSubmit(list);
			}
			db.SubmitChanges();
		}

		public PagedUserArgs<UserInfo> List (PagedUserArgs<UserInfo> query)
		{
			if (query == null)
				return new PagedUserArgs<UserInfo>();

			var db = this.GetDatabase();

			var rs = db.UserInfo.Where(item => 1 == 1);

			if (!string.IsNullOrEmpty(query.NameIn))
				rs = rs.Where(item => item.Name.Contains(query.NameIn));

			if (query.IsInRole)
				rs = rs.Where(item => db.UserRole.Any(obj => obj.User == item.Id && obj.Role == query.Role));
			else
				rs = rs.Where(item => !db.UserRole.Any(obj => obj.User == item.Id && obj.Role == query.Role));

			query.ResultCount = rs.Count();

			rs = rs.OrderByDescending(item => item.Id);

			if (query.PageIndex > 0 && query.PageSize > 0)
				rs = rs.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);

			query.Results = rs.ToArray();

			return query;
		}
	}
}
