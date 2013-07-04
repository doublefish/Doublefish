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

using EvoPdf.HtmlToPdf;

namespace DoubleFish.Web.View.Test
{
	public partial class _Pdf3 : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			var url = this.Context.Request.QueryString["url"];
			var pdf = this.Context.Request.QueryString["pdf"];
			if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(pdf))
				this.HtmlToPdf(url, pdf);
		}

		/// <summary>
		/// Convert the HTML code from the specified URL to a PDF document
		/// and send the document to the browser
		/// </summary>
		private void HtmlToPdf (string url, string pdf)
		{
			if (url == "")
			{
				return;
			}
			SautinSoft.PdfVision v = new SautinSoft.PdfVision();

			//Specify top and bottom page margins
			v.PageStyle.PageMarginTop.Mm(5f);
			v.PageStyle.PageMarginBottom.Mm(5f);

			byte[] pdfBytes = null;


			//convert URL to pdf stream
			pdfBytes = v.ConvertHtmlFileToPDFStream(url);


			//show PDF
			if (pdfBytes != null)
			{
				Response.Buffer = true;
				Response.Clear();
				Response.ContentType = "application/PDF";
				Response.AddHeader("Content-Disposition:", "attachment; filename=" + pdf + ".pdf");
				Response.BinaryWrite(pdfBytes);
				Response.Flush();
				Response.End();
			}
			else
			{
				Response.Write("Converting failed!");
			}
		}
	}
}