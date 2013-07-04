using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;
using DoubleFish.DAL;

namespace DoubleFish.BLL
{
	public class RoleBLL
	{
		RoleDAL RoleDAL = new RoleDAL();

		public RoleInfo Get (long id)
		{
			return RoleDAL.Get(id);
		}

		public RoleInfo Save (RoleInfo data)
		{
			return RoleDAL.Save(data);
		}

		public int Delete (long id)
		{
			return RoleDAL.Delete(id);
		}

		/// <summary>
		/// 是否被引用
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool IsReferenced (long id)
		{

			return false;
		}

		public IList<RoleInfo> List ()
		{
			return RoleDAL.List();
		}

		public PagedRoleArgs<RoleInfo> List (PagedRoleArgs<RoleInfo> query)
		{
			return RoleDAL.List(query);
		}
	}
}
