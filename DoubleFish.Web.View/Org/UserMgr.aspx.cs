using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DoubleFish.BLL;
using DoubleFish.Model;
using DoubleFish.Model.PagedQuery;

using DoubleFish;
using DoubleFish.Http;

namespace DoubleFish.Web.View.Org
{
	public partial class _UserMgr : PageBase
	{
		public override void ProcessRequest (HttpContext context)
		{
			if (context.Request.Form.Count > 0)
			{
				context.Response.Write(Submit(context));
				return;
			}
			base.ProcessRequest(context);
		}

		protected void Page_Load (object sender, EventArgs e)
		{

		}

		private object Submit (HttpContext context)
		{

			var action = context.Request.Form["Action"];

			if (string.Compare(action, "page", StringComparison.OrdinalIgnoreCase) == 0)
				return this.PageResult(context);

			return null;

		}

		private string PageResult (HttpContext context)
		{

			PagedUserArgs<UserInfo> query = new PagedUserArgs<UserInfo>();
			query.PageIndex = context.Request.Form["PageIndex"].ToInt32(1);
			query.PageSize = context.Request.Form["PageSize"].ToInt32(20);
			query.FullNameIn = context.Request.Form["Name"].Trim();
			query.FullNameIn = context.Request.Form["FullName"].Trim();
			query.Sex = context.Request.Form["Sex"].ToInt32(0);
			var server = context.GetInstanceFromCache<UserBLL>();

			//server.SavePassword(1000, "admin");

			query = server.List(query);

			var json = new Json(query);
			json.ClassName = "";
			return json.ToJsonString();
		}
	}
}