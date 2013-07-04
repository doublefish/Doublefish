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
	public partial class _UserRole : PageBase
	{
		public override void ProcessRequest (HttpContext context)
		{
			if (context.Request.Form.Count > 0)
			{
				context.Response.Write(this.Action(context));
				return;
			}
			base.ProcessRequest(context);
		}

		protected void Page_Load (object sender, EventArgs e)
		{
			var server = this.Context.GetInstanceFromItems<RoleBLL>();
			repRole.DataSource = server.List();
			repRole.DataBind();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		private object Action (HttpContext context)
		{
			var action = context.Request.Form["Action"];//操作类型
			switch (action)
			{
				case "MoveUser":
					return this.MoveUser(context);
				case "PageUser":
					return this.PageUser(context);
				default:
					return string.Empty;
			}
		}

		private object MoveUser (HttpContext context)
		{
			var role = context.Request.Form["Role"].ToInt64();
			var isIn = context.Request.Form["MoveIn"].ToInt32();
			var temp = context.Request.Form.GetValues("Users");
			if (temp == null) temp = context.Request.Form.GetValues("Users[]");

			var users = this.ToInt64Array(temp);

			var server = context.GetInstanceFromItems<UserRoleBLL>();

			try
			{
				server.MoveUser(users, role, isIn > 0);
			}
			catch (Exception ex)
			{
				context.WriteError(ex.Message);
				return string.Empty;
			}
			return "ok";
		}

		/// <summary>
		/// 根据角色 取用户
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		private object PageUser (HttpContext context)
		{
			var query = new PagedUserArgs<UserInfo>();
			query.PageIndex = context.Request.Form["PageIndex"].ToInt32(1);
			query.PageSize = context.Request.Form["PageSize"].ToInt32(10);

			query.NameIn = context.Request.Form["Name"];
			query.Role = context.Request.Form["Role"].ToInt64();
			query.IsInRole = context.Request.Form["IsIn"].ToInt32() > 0;

			return this.PageUser(context, query);
		}

		private object PageUser (HttpContext context, PagedUserArgs<UserInfo> query)
		{
			var server = context.GetInstanceFromItems<UserRoleBLL>();

			query = server.List(query);

			var json = new Json(query);
			json.ClassName = string.Empty;

			return json.ToJsonString();
		}


		private long[] ToInt64Array (string[] array)
		{
			if (array == null)
				return null;

			long[] result = new long[array.Length];

			for (int i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToInt64(0L);
			}
			return result;
		}
	}
}