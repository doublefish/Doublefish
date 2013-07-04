using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DoubleFish;
using DoubleFish.Http;

using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;
using DoubleFish.BLL;

namespace DoubleFish.Web.View.Sys
{
	public partial class _Permission : PageBase
	{
		public override void ProcessRequest (HttpContext context)
		{
			if (context.Request.Form.Count > 0)
			{
				context.Response.Write(Action(context));
				return;
			}
			base.ProcessRequest(context);
		}

		protected void Page_Load (object sender, EventArgs e)
		{

		}

		private object Action (HttpContext context)
		{
			var action = context.Request.Form["Action"];
			switch (action)
			{
				case "GetPms":
					return GetPms(context);
				case "PageRole":
					return PageRole(context);
				case "PageUser":
					return PageUser(context);
				case "GetMenu":
					return GetMenu(context);
				case "Save":
					return Save(context);
				default:
					return null;
			}
		}

		private string PageRole (HttpContext context)
		{

			PagedRoleArgs<RoleInfo> query = new PagedRoleArgs<RoleInfo>();
			query.PageIndex = context.Request.Form["PageIndex"].ToInt32(1);
			query.PageSize = context.Request.Form["PageSize"].ToInt32(10);

			query.NameIn = context.Request.Form["Name"].Trim();

			var server = context.GetInstanceFromItems<RoleBLL>();

			query = server.List(query);

			var json = new Json(query);

			json.ClassName = "";
			return json.ToJsonString();
		}

		private string PageUser (HttpContext context)
		{

			PagedUserArgs<UserInfo> query = new PagedUserArgs<UserInfo>();
			query.PageIndex = context.Request.Form["PageIndex"].ToInt32(1);
			query.PageSize = context.Request.Form["PageSize"].ToInt32(10);

			query.NameIn = context.Request.Form["Name"].Trim();

			var server = context.GetInstanceFromItems<UserBLL>();

			query = server.List(query);

			var json = new Json(query);

			json.ClassName = "";
			return json.ToJsonString();
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		private object Save (HttpContext context)
		{
			var user = context.Request.Form["user"].ToInt64(0);
			var role = context.Request.Form["role"].ToInt64(0);
			var menus = context.Request.Form.GetValues("Menus");
			if (menus == null) menus = context.Request.Form.GetValues("Menus[]");

			List<Permission> pms = new List<Permission>();
			if (menus != null)
			{
				for (var i = 0; i < menus.Length; i++)
				{
					var pm = new Permission();
					pm.User = user;
					pm.Role = role;
					pm.Menu = menus[i].ToInt64();
					pms.Add(pm);
				}
			}

			var server = context.GetInstanceFromItems<PermissionBLL>();
			try
			{
				pms = server.Save(pms.ToArray(), user, role).ToList();
			}
			catch (Exception ex)
			{
				context.WriteError(ex);
				return string.Empty;
			}
			var json = new Json(pms);
			json.ClassName = string.Empty;

			return json.ToJsonString();
		}

		/// <summary>
		/// 获取权限
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		private object GetPms (HttpContext context)
		{
			var role = context.Request.Form["Role"].ToInt64();
			var user = context.Request.Form["User"].ToInt64();

			var server = context.GetInstanceFromItems<PermissionBLL>();

			var list = server.GetByIds(role, user);

			var json = new Json(list);
			json.ClassName = string.Empty;
			return json.ToJsonString();
		}

		private object GetMenu (HttpContext context)
		{
			var parent = context.Request.Form["Parent"].ToInt64();

			var server = context.GetInstanceFromCache<MenuBLL>();

			var list = server.List();

			var json = new Json(list);
			json.ClassName = "";
			return json.ToJsonString();
		}
	}
}