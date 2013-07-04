using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;

using DoubleFish.Database;

namespace DoubleFish.DAL
{
	public class RoleDAL: DataBase
	{
		public RoleInfo Get (long id)
		{
			var db = this.GetDatabase();

			return db.RoleInfo.Where(u => u.Id == id).FirstOrDefault();
		}

		public RoleInfo Save (RoleInfo data)
		{

			var db = this.GetDatabase();

			if (db.RoleInfo.Where(item => item.Name == data.Name && item.Id != data.Id).Count() > 0)
				throw new Exception("相同名称角色信息已存在！");

			if (data.Time == DateTime.MinValue)
				data.Time = DateTime.Now;

			if (data.Id > 0L)
			{
				var sr = db.RoleInfo.Where(item => item.Id == data.Id).FirstOrDefault();
				if (sr == null || sr.Id == 0L)
					return data;

				sr.Name = data.Name;
				sr.Time = data.Time;
				sr.Note = data.Note;
				sr.Flag = data.Flag;

				data = sr;
			}
			else
			{
				db.RoleInfo.InsertOnSubmit(data);
			}

			db.SubmitChanges();

			return data;
		}

		public int Delete (long id)
		{
			if (id < 1L)
				return -1;

			var db = this.GetDatabase();

			if (db.UserRole.Where(item => item.Role == id).Count() > 0)
				throw new Exception("该信息被引用，不允许删除！");

			if (db.Permission.Where(item => item.Role == id).Count() > 0)
				throw new Exception("该信息被引用，不允许删除！");

			return db.RoleInfo.Delete(item => item.Id == id);
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
			var db = this.GetDatabase();
			return db.RoleInfo.Where(item => item.Flag == 1).ToList();
		}

		public PagedRoleArgs<RoleInfo> List (PagedRoleArgs<RoleInfo> query)
		{
			if (query == null)
				return new PagedRoleArgs<RoleInfo>();

			var db = this.GetDatabase();

			var rs = db.RoleInfo.Where(item => item.Flag > -1);

			if (!string.IsNullOrEmpty(query.NameIn))
				rs = rs.Where(item => item.Name.Contains(query.NameIn));

			if (query.Flag > 0)
				rs = rs.Where(item => item.Flag == query.Flag - 1);

			query.ResultCount = rs.Count();

			rs = rs.OrderByDescending(item => item.Id);

			if (query.PageIndex > 0 && query.PageSize > 0)
				rs = rs.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);

			query.Results = rs.ToArray();

			return query;
		}
	}
}
