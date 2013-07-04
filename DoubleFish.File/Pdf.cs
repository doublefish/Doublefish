using System;
using System.Web;
/*要引用以下命名空间*/
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DoubleFish.File
{
	public class Pdf
	{
		public void HtmlToPdf (HttpContext context, string url, string name)
		{
			//var application = context.Server.MapPath("/common/wkhtmltopdf/wkhtmltopdf.exe");

			var application = context.Server.MapPath("~/common/wkhtmltopdf/wkhtmltopdf.exe");

			var fileName = context.Server.MapPath("~/Pdf/Temp/" + name + ".pdf");

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
