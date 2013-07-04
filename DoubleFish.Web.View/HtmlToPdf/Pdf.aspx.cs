using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;

namespace DoubleFish.Web.View.Test
{
	public partial class _Pdf : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			var url = this.Context.Request.QueryString["url"];
			var pdf = this.Context.Request.QueryString["pdf"];
			if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(pdf))
			{
				try
				{
					this.HtmlToPdf(this.Context, url, pdf);
				}
				catch (Exception ex)
				{
					this.Context.Response.Write(ex.Message);
				}
			}
		}

		public void HtmlToPdf (HttpContext context, string url, string name)
		{
			//var application = context.Server.MapPath("/common/wkhtmltopdf/wkhtmltopdf.exe");

			var application = Server.MapPath("~/common/wkhtmltopdf/wkhtmltopdf.exe");

			var fileName = Server.MapPath("~/Pdf/Temp/" + name + ".pdf");

			string cmd = string.Format("\"{0}\" \"{1}\"", url, fileName);

			System.Diagnostics.Process process = System.Diagnostics.Process.Start(application, cmd);

			//指定进程自行退行为止
			process.WaitForExit();

			//把文件读进文件流 
			FileStream fs = new FileStream(fileName, FileMode.Open);
			byte[] file = new byte[fs.Length];
			fs.Read(file, 0, file.Length);
			fs.Close();

			//Response给客户端下载 
			context.Response.Clear();
			
			context.Response.AddHeader("content-disposition", "attachment; filename=" + name + ".pdf");//强制下载 
			context.Response.ContentType = "application/octet-stream";
			context.Response.BinaryWrite(file);
		}
	}
}