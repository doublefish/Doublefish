using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DoubleFish.Web;

namespace DoubleFish.Web.View
{
	public partial class _Default : PageBase
	{
		public override void ProcessRequest (HttpContext context)
		{
			context.Request.Form[""].ToInt32();

			base.ProcessRequest(context);
		}

		protected void Page_Load (object sender, EventArgs e)
		{
			
		}
	}
}
