using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;

using DoubleFish.Database;

namespace DoubleFish.DAL
{
	public class PermissionDAL : DataBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pms"></param>
		/// <param name="user"></param>
		/// <param name="role"></param>
		/// <returns></returns>
		public Permission[] Save (Permission[] pms, long user, long role)
		{
			if (user < 1L && role < 1L)
				throw new Exception("未指定角色或用户");

			var db = this.GetDatabase();

			var rs = db.Permission.Where(item => 1 == 1);

			if (role > 0L)
			{
				rs = rs.Where(item => item.Role == role);
			}
			else if (user > 0L)
			{
				rs = rs.Where(item => item.User == user);
			}

			db.Permission.DeleteAllOnSubmit(rs.ToArray());

			if (pms.Length > 0)
				db.Permission.InsertAllOnSubmit(pms);

			db.SubmitChanges();

			return rs.ToArray();
		}

		/// <summary>
		/// 按角色、用户 查询权限
		/// </summary>
		/// <param name="role">角色</param>
		/// <param name="user">用户</param>
		/// <returns>查询结果</returns>
		public Permission[] GetByIds (long role, long user)
		{
			var db = this.GetDatabase();
			var rs = db.Permission.Where(item => 1 == 1);
			if (role > 0L)
				rs = rs.Where(item => item.Role == role);
			if (user > 0L)
				rs = rs.Where(item => item.User == user);

			return rs.ToArray();
		}
	}
}
