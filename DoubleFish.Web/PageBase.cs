using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;

using DoubleFish;
using DoubleFish.Http;

using DoubleFish.Model;

namespace DoubleFish.Web
{
	public class PageBase : System.Web.UI.Page
	{
		public override void ProcessRequest (HttpContext context)
		{
			//if (LoginUser == null)
			//{

			//    var path = context.Request.ApplicationPath;

			//    context.Response.Write("<script type=\"text/javascript\">alert('登录超时，请重新登录！');top.location.href='" + path + "/index.aspx';</script>");
			//    return;
			//}

			base.ProcessRequest(context);
		}

		public LoginUser _LoginUser;

		/// <summary>
		/// 当前登录用户。
		/// </summary>
		public LoginUser LoginUser
		{
			get
			{
				if (this._LoginUser == null)
					this._LoginUser = this.Context.GetLoginInfo();
				if (this._LoginUser == null)
				{
					this._LoginUser = new LoginUser();
					this._LoginUser.Id = 1000;
					this._LoginUser.Name = "admin";
					this._LoginUser.FullName = "administrator";
				}
				return this._LoginUser;
			}
		}

		public void Test ()
		{
		
		}

	}
}
