using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;
using DoubleFish.DAL;

namespace DoubleFish.BLL
{
	public class MenuBLL : BaseBLL
	{
		MenuDAL MenuDAL = new MenuDAL();
		
		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public MenuInfo Save (MenuInfo data)
		{
			return MenuDAL.Save(data);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public int Delete (long id)
		{
			return MenuDAL.Delete(id);
		}

		/// <summary>
		/// 是否被引用
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool IsReferenced (long id)
		{
			return MenuDAL.IsReferenced(id);
		}

		/// <summary>
		/// 查询单条信息
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public MenuInfo Get (long id)
		{
			return MenuDAL.Get(id);
		}

		/// <summary>
		/// 查询指定父级下所有子级
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public List<MenuInfo> GetByParent (long parent)
		{
			return MenuDAL.GetByParent(parent);
		}

		/// <summary>
		/// 递归查询指定父级下所有下级
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public List<MenuInfo> RecursiveByParent (long parent)
		{
			return MenuDAL.RecursiveByParent(parent);
		}

		public List<MenuInfo> List ()
		{
			return MenuDAL.List();
		}

		/// <summary>
		/// 查询所有
		/// </summary>
		/// <returns></returns>
		public List<MenuInfo> GetAll ()
		{
			return MenuDAL.GetAll();
		}

		public List<MenuInfo> GetByPermission (long user)
		{
			return MenuDAL.GetByPermission(user);
		}
	}
}
