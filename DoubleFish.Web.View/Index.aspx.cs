using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoubleFish.Web.View
{
	public partial class Index : System.Web.UI.Page
	{
		public override void ProcessRequest (HttpContext context)
		{
			
			base.ProcessRequest(context);
		}
		protected void Page_Load (object sender, EventArgs e)
		{
			var login = this.Context.GetLoginInfo();
			if (login != null)
			{
				this.litUser.Text = login.FullName + " 欢迎您！";
				this.phLogin.Visible = false;
			}
		}
	}
}