using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

using DoubleFish.Database;

using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;

namespace DoubleFish.DAL
{
	public class UserDAL : DataBase
	{

		public UserInfo Get (long id)
		{
			var db = this.GetDatabase();

			return db.UserInfo.Where(u => u.Id == id).First();
		}

		public UserInfo Save (UserInfo data)
		{
			var db = this.GetDatabase();

			if (data.Id > 0L)
			{
				var sr = db.UserInfo.Where(item => item.Id == data.Id).First();
				if (sr == null || sr.Id == 0)
					return data;

				sr.Name = data.Name;
				sr.FullName = data.FullName;
				sr.Sex = data.Sex;
				sr.Birthday = data.Birthday;
				sr.Tel = data.Tel;
				sr.Mobile = data.Mobile;
				sr.Mail = data.Mail;
				sr.Region = data.Region;
				sr.Status = data.Status;

				data = sr;
			}
			else
			{
				data.RegistTime = DateTime.Now;
				db.UserInfo.InsertOnSubmit(data);
			}

			db.SubmitChanges();

			return data;
		}

		public void SavePassword (long user, string password)
		{
			var db = this.GetDatabase();

			var u = db.UserInfo.Where(item => item.Id == user).FirstOrDefault();

			if (u == null || u.Id == 0)
				throw new Exception("用户不存在！");

			var i = db.UserPassword.Update(item => item.User == u.Id && item.Flag == 2,
				item => new UserPassword
				{
					Password = password,
					Time = DateTime.Now,
					Flag = 0,
				});

			i = db.UserPassword.Delete(o => o.User == 1000 && o.Flag == 3);
			
			i = db.UserPassword.UpdateBatch(item => item.UserInfo.Id == 1000,
					item => new UserPassword
					{
						Time = DateTime.Now,
						Flag = 0
					});

			var p = new UserPassword();
			p.User = u.Id;
			p.Time = DateTime.Now;
			p.Flag = 1;
			p.Password = MD5Encrypt(password);

			db.UserPassword.InsertOnSubmit(p);

			db.SubmitChanges();
		}

		public PagedUserArgs<UserInfo> List (PagedUserArgs<UserInfo> query)
		{
			if (query == null)
				query = new PagedUserArgs<UserInfo>();

			var db = this.GetDatabase();

			IQueryable<UserInfo> rs = this.GetQueryable(query, db.UserInfo);

			query.ResultCount = rs.Count();

			rs = rs.OrderByDescending(item => item.Id);

			if (query.PageIndex > 0 && query.PageSize > 0)
				rs = rs.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);

			query.Results = rs.ToArray();

			var dataTable = new System.Data.DataTable();

			dataTable.Columns.Add("Id", Int64.MinValue.GetType());
			dataTable.Columns.Add("Name", string.Empty.GetType());
			dataTable.Columns.Add("FullName", string.Empty.GetType());

			foreach (var data in query.Results)
			{
				var dataRow = dataTable.NewRow();
				dataRow["Id"] = data.Id;
				dataRow["Name"] = data.Name;
				dataRow["FullName"] = data.FullName;
				dataTable.Rows.Add(dataRow);
			}
			var array = dataTable.ToArray<UserInfo>();
			var list = dataTable.ToList<UserInfo>();
			return query;
		}

		public IQueryable<UserInfo> GetQueryable (PagedUserArgs<UserInfo> query, IQueryable<UserInfo> rs)
		{
			if (query == null)
				return rs;

			if (!string.IsNullOrEmpty(query.NameIn))
				rs = rs.Where(item => item.Name.Contains(query.NameIn));

			if (!string.IsNullOrEmpty(query.FullNameIn))
				rs = rs.Where(item => item.FullName.Contains(query.FullNameIn));

			if (query.Sex > 0)
				rs = rs.Where(item => item.Sex == query.Sex);

			return rs;
		}
	}
}
