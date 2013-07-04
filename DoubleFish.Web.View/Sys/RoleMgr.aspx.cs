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
	public partial class _RoleMgr : PageBase
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

		}

		private object Action (HttpContext context)
		{
			var action = context.Request.Form["Action"];
			switch (action)
			{
				case "Page":
					return PageResult(context);
				case "Save":
					return Save(context);
				case "Delete":
					return Delete(context);
				default:
					return null;
			}
		}

		private string PageResult (HttpContext context)
		{

			PagedRoleArgs<RoleInfo> query = new PagedRoleArgs<RoleInfo>();
			query.PageIndex = context.Request.Form["PageIndex"].ToInt32(1);
			query.PageSize = context.Request.Form["PageSize"].ToInt32(10);

			query.NameIn = context.Request.Form["Name"].Trim();
			query.Flag = context.Request.Form["Flag"].ToInt32();

			var server = context.GetInstanceFromItems<RoleBLL>();

			query = server.List(query);

			var json = new Json(query);

			json.ClassName = "";
			return json.ToJsonString();
		}

		private object Delete (HttpContext context)
		{
			var id = context.Request.Form["Id"].ToInt64(0L);

			var server = context.GetInstanceFromItems<RoleBLL>();
			try
			{
				server.Delete(id);
			}
			catch (Exception ex)
			{
				context.WriteError(ex.Message);
				return string.Empty;
			}

			return "ok";

		}

		private object Save (HttpContext context)
		{
			var data = new RoleInfo();
			data.Id = context.Request.Form["Id"].ToInt64(0L);
			data.Name = context.Request.Form["Name"].Trim();
			data.Note = context.Request.Form["Note"];
			data.Flag = context.Request.Form["Flag"].ToInt32();

			var server = context.GetInstanceFromItems<RoleBLL>();

			try
			{
				server.Save(data);
			}
			catch (Exception ex)
			{
				context.WriteError(ex.Message);
				return string.Empty;
			}

			if (data == null || data.Id < 1)
				return "保存失败！";

			return "ok";
		}
	}
}