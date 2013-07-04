using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;

using DoubleFish.DAL;

namespace DoubleFish.BLL
{
	public class PermissionBLL : BaseBLL
	{
		PermissionDAL PermissionDAL = new PermissionDAL();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pms"></param>
		/// <param name="user"></param>
		/// <param name="role"></param>
		/// <returns></returns>
		public Permission[] Save (Permission[] pms, long user, long role)
		{
			return PermissionDAL.Save(pms, user, role);
		}

		/// <summary>
		/// 按角色、用户 查询权限
		/// </summary>
		/// <param name="role">角色</param>
		/// <param name="user">用户</param>
		/// <returns>查询结果</returns>
		public Permission[] GetByIds (long role, long user)
		{
			return PermissionDAL.GetByIds(role, user);
		}
	}
}
