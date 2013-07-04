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

using Winnovative.WnvHtmlConvert;

namespace DoubleFish.Web.View.Test
{
	public partial class _Pdf1 : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			var url = this.Context.Request.QueryString["url"];
			var pdf = this.Context.Request.QueryString["pdf"];
			if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(pdf))
				this.HtmlToPdf(url, pdf);
		}

		private void HtmlToPdf (string url, string downloadName)
		{
			PdfConverter pdfConverter = new PdfConverter();

			//pdfConverter.LicenseKey = "ORIJGQoKGQkZCxcJGQoIFwgLFwAAAAA=";

			pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
			pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
			pdfConverter.PdfDocumentOptions.ShowHeader = true;
			pdfConverter.PdfDocumentOptions.ShowFooter = true;
			pdfConverter.PdfDocumentOptions.LeftMargin = 5;
			pdfConverter.PdfDocumentOptions.RightMargin = 5;
			pdfConverter.PdfDocumentOptions.TopMargin = 5;
			pdfConverter.PdfDocumentOptions.BottomMargin = 5;
			pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;

			pdfConverter.PdfDocumentOptions.ShowHeader = false;
			//pdfConverter.PdfHeaderOptions.HeaderText = "Sample header: " + url;
			//pdfConverter.PdfHeaderOptions.HeaderTextColor = Color.Blue;
			//pdfConverter.PdfHeaderOptions.HeaderDescriptionText = string.Empty;
			//pdfConverter.PdfHeaderOptions.DrawHeaderLine = false;

			pdfConverter.PdfFooterOptions.FooterText = "Sample footer: " + url +
			". You can change color, font and other options";
			pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue;
			pdfConverter.PdfFooterOptions.DrawFooterLine = false;
			pdfConverter.PdfFooterOptions.PageNumberText = "Page";
			pdfConverter.PdfFooterOptions.ShowPageNumber = true;

			//pdfConverter.LicenseKey = "put your serial number here";
			byte[] downloadBytes = pdfConverter.GetPdfFromUrlBytes(url);


			System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
			response.Clear();
			response.AddHeader("Content-Type", "binary/octet-stream");
			response.AddHeader("Content-Disposition",
				"attachment; filename=" + downloadName + ".pdf; size=" + downloadBytes.Length.ToString());
			response.Flush();
			response.BinaryWrite(downloadBytes);
			response.Flush();
			response.End();
		}
	}
}