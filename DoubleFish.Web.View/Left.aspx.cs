using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DoubleFish.Http;

using DoubleFish.Model;
using DoubleFish.BLL;

namespace DoubleFish.Web.View
{
	public partial class _Left : PageBase
	{
		public override void ProcessRequest (HttpContext context)
		{
			if (context.Request.Form.Count > 0)
			{
				context.Response.Write(GetMenu(context));
				return;
			}
			base.ProcessRequest(context);
		}
		protected void Page_Load (object sender, EventArgs e)
		{

		}

		private object GetMenu (HttpContext context)
		{
			var server = this.Context.GetInstanceFromCache<MenuBLL>();
			var list = server.GetByPermission(LoginUser.Id);
			var json = new Json(list);
			json.ClassName = string.Empty;
			return json.ToJsonString();
		}
	}
}