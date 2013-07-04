using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DoubleFish.Http;

using DoubleFish.Model;
using DoubleFish.BLL;

namespace DoubleFish.Web.View.Sys
{
	public partial class _MenuMgr : PageBase
	{
		public override void ProcessRequest (HttpContext context)
		{
			if (context.Request.Form.Count > 0)
			{
				context.Response.Write(PostResult(context));
				return;
			}
			base.ProcessRequest(context);
		}

		protected void Page_Load (object sender, EventArgs e)
		{

		}

		private object PostResult (HttpContext context)
		{
			var action = context.Request.Form["action"];
			switch(action)
			{
				case "save":
					return Save(context);
				case "getByParent":
					return GetByParent(context);
				default:
					return null;
			}
		}

		private object Delete (HttpContext context)
		{
			var id = context.Request.Form["Id"].ToInt64();
			var parent = context.Request.Form["Parent"].ToInt64();
			if (id < 1L)
			{
				context.WriteError("数据出错！");
				return null;
			}

			var server = context.GetInstanceFromCache<MenuBLL>();
			try
			{
				id = server.Delete(id);
			}
			catch (Exception ex)
			{
				context.WriteError(ex.Message);
			}
			if (id < 1)
			{
				context.WriteError("删除失败！");
				return null;
			}
			return GetByParent(context, parent);
		}

		private object Save (HttpContext context)
		{
			MenuInfo data = new MenuInfo();
			data.Id = context.Request.Form["Id"].ToInt64(0);
			data.Name = context.Request.Form["Name"];
			data.Parent = context.Request.Form["Parent"].ToInt64(0);
			data.Type = context.Request.Form["Type"].ToInt32(0);
			data.Url = context.Request.Form["Url"];
			data.Flag = context.Request.Form["Flag"].ToInt32(0);
			data.Note = context.Request.Form["Note"];

			var server = context.GetInstanceFromCache<MenuBLL>();

			try
			{
				data = server.Save(data);
			}
			catch (Exception ex)
			{
				context.WriteError(ex.Message);
				return null;
			}

			return "{\"Parent\": " + data.Parent + ", \"Nodes\":" + GetByParent(context, data.Parent) + "}";
		}

		private object GetByParent (HttpContext context)
		{
			var parent = context.Request.Form["Parent"].ToInt64();
			return GetByParent(context, parent);
		}

		private object GetByParent (HttpContext context, long parent)
		{
			var server = context.GetInstanceFromCache<MenuBLL>();
			List<MenuInfo> list;
			if (parent > 0L)
				list = server.RecursiveByParent(parent);
			else
				list = server.GetAll();

			var json = new Json(list);
			json.ClassName = "";
			return json.ToJsonString();
		}
	}
}