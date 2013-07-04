using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;

using DoubleFish.DAL;

namespace DoubleFish.BLL
{
	public class UserRoleBLL : BaseBLL
	{
		UserRoleDAL UserRoleDAL = new UserRoleDAL();
		/// <summary>
		/// 移动用户
		/// </summary>
		/// <param name="ur">要移动的集合</param>
		/// <param name="role">角色</param>
		/// <param name="isMoveIn">移入/移出</param>
		/// <returns>移动后此角色下的所有用户</returns>
		public void MoveUser (long[] users, long role, bool isMoveIn)
		{
			UserRoleDAL.MoveUser(users, role, isMoveIn);
		}

		public PagedUserArgs<UserInfo> List (PagedUserArgs<UserInfo> query)
		{
			return UserRoleDAL.List(query);
		}
	}
}
