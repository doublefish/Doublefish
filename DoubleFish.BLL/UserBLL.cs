using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;
using DoubleFish.DAL;

namespace DoubleFish.BLL
{
	public class UserBLL : BaseBLL
	{
		UserDAL UserDAL = new UserDAL();

		public UserInfo Get (long id)
		{
			return UserDAL.Get(id);
		}

		public UserInfo Save (UserInfo data)
		{
			return UserDAL.Save(data);
		}

		public void SavePassword (long user, string password)
		{
			UserDAL.SavePassword(user, password);
		}

		public PagedUserArgs<UserInfo> List ()
		{
			return UserDAL.List(null);
		}

		public PagedUserArgs<UserInfo> List (PagedUserArgs<UserInfo> query)
		{
			return UserDAL.List(query);
		}
	}
}
