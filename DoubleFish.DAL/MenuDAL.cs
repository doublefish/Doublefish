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
	public class MenuDAL : DataBase
	{
		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public MenuInfo Save (MenuInfo data)
		{
			var db = this.GetDatabase();

			if (data.Id > 0L)
			{
				var sr = db.MenuInfo.Where(item => item.Id == data.Id).First();
				if (sr == null || sr.Id == 0)
					return data;

				sr.Name = data.Name;
				sr.Parent = data.Parent;
				sr.Type = data.Type;
				sr.Url = data.Url;
				sr.Flag = data.Flag;
				sr.Note = data.Note;

				data = sr;
			}
			else
			{
				db.MenuInfo.InsertOnSubmit(data);
			}

			db.SubmitChanges();

			return data;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public int Delete (long id)
		{
			if (id <= 0L)
				return -1;

			var db = this.GetDatabase();
			return db.MenuInfo.Delete(m => m.Id == id);
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

		/// <summary>
		/// 查询单条信息
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public MenuInfo Get (long id)
		{
			var db = this.GetDatabase();

			return db.MenuInfo.Where(m => m.Id == id).First();
		}
		
		/// <summary>
		/// 查询指定指定父级下的所有子级
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public List<MenuInfo> GetByParent (long parent)
		{
			var db = this.GetDatabase();
			var rs = db.MenuInfo.Where(m => m.Parent == parent);
			return rs.ToList();
		}

		/// <summary>
		/// 递归查询指定父级下所有下级
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public List<MenuInfo> RecursiveByParent (long parent)
		{


			var db = this.GetDatabase();
			var rs = db.MenuInfo_RecursiveByParent(parent);
			return rs.ToList();
		}

		public List<MenuInfo> List ()
		{
			var db = this.GetDatabase();

			return db.MenuInfo.Where(item => item.Flag == 1).ToList();
		}

		/// <summary>
		/// 查询所有
		/// </summary>
		/// <returns></returns>
		public List<MenuInfo> GetAll ()
		{
			var db = this.GetDatabase();
			return db.MenuInfo.ToList();
		}

		public List<MenuInfo> GetByPermission (long user)
		{
			var db = this.GetDatabase();

			//var role = db.UserRole.Where(item => item.User == user).ToArray();

			var rs = from item in db.MenuInfo
					 join pms in db.Permission
					 on item.Id equals pms.Menu
					 where item.Flag == 1
					 && (db.UserRole.Where(ur => ur.User == user).Select(ur => ur.Role).Contains(pms.Role)
					 || pms.User == user)
					 select item;

			return rs.Distinct().ToList();
		}
	}
}
