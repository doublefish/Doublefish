using System;
using System.Data;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace DoubleFish.Web.View.Test
{
	public partial class _Pdf4 : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			var url = this.Context.Request.QueryString["url"];
			var pdf = this.Context.Request.QueryString["pdf"];
			if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(pdf))
				this.CreatPdf(url, pdf);//url, pdf
		}

		private System.Drawing.Bitmap bitmap;
		private string url;
		private int w = 760, h = 900;
		public void setBitmap ()
		{
			using (WebBrowser wb = new WebBrowser())
			{
				wb.Width = w;
				wb.Height = h;
				wb.ScrollBarsEnabled = false;
				wb.Navigate(url);
				//确保页面被解析完全
				while (wb.ReadyState != WebBrowserReadyState.Complete)
				{
					System.Windows.Forms.Application.DoEvents();
				}
				bitmap = new System.Drawing.Bitmap(w, h);
				wb.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, w, h));
				wb.Dispose();
			}
		}
		private void CreatPdf (string html, string pdf)
		{
			Document doc = new Document(PageSize.A4, 9, 18, 36, 36);//左右上下
			MemoryStream ms = new MemoryStream();
			try
			{
				//PdfWriter writer = PdfWriter.GetInstance(doc, ms);
				PdfWriter writer = PdfWriter.getInstance(doc, ms);
				//writer.CloseStream = false;无此方法
				doc.Open();
				url = Server.MapPath(html);
				Thread thread = new Thread(new ThreadStart(setBitmap));
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
				while (thread.IsAlive)
					Thread.Sleep(100);
				bitmap.Save(Server.MapPath("t.BMP"));

				//iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bitmap, System.Drawing.Imaging.ImageFormat.Bmp);
				iTextSharp.text.Image img = iTextSharp.text.Image.getInstance(bitmap, System.Drawing.Imaging.ImageFormat.Bmp);
				//img.ScalePercent(75);//560 630
				img.scalePercent(75);
				doc.Add(img);
			}
			catch (Exception err)
			{
				throw new Exception(err.Message);
			}
			finally
			{
				doc.Close();
				using (FileStream fs = new FileStream(Server.MapPath("out.pdf"), FileMode.Create))
				{
					ms.Position = 0;
					byte[] bit = new byte[ms.Length];
					ms.Read(bit, 0, (int)ms.Length);
					fs.Write(bit, 0, bit.Length);
				}
				ViewPdf(ms);
			}
		}
		private void ViewPdf (Stream fs)
		{
			Response.Clear();
			//中文名的话
			//Response.AppendHeader("Content-Disposition", "attachment;filename=" +
			//             HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + ";charset=GB2312");
			Response.AddHeader("Content-Disposition", "attachment;FileName=out.pdf");
			Response.AddHeader("Content-Length", fs.Length.ToString());
			Response.ContentType = "application/pdf";
			long fileLength = fs.Length;
			int size = 10240;//10K一--分块下载，10K为1块
			byte[] readData = new byte[size];
			if (size > fileLength)
				size = Convert.ToInt32(fileLength);
			long fPos = 0;
			bool isEnd = false;
			while (!isEnd)
			{
				if ((fPos + size) >= fileLength)
				{
					size = Convert.ToInt32(fileLength - fPos);
					isEnd = true;
				}
				readData = new byte[size];
				fs.Position = fPos;
				fs.Read(readData, 0, size);
				Response.BinaryWrite(readData);
				Response.OutputStream.Flush();
				fPos += size;
			}
			fs.Close();
			Response.OutputStream.Close();
			Response.End();//非常重要，没有这句的话，页面的HTML代码将会保存到文件中
			Response.Close();
		}
	}
}