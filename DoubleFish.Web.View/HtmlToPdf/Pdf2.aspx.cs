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
	public partial class _Pdf2 : System.Web.UI.Page
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
			string urlToConvert = url;

			// Create the PDF converter. Optionally the HTML viewer width can 
			// be specified as parameter
			// The default HTML viewer width is 1024 pixels.
			PdfConverter pdfConverter = new PdfConverter();

			// set the license key - required
			pdfConverter.LicenseKey = "ORIJGQoKGQkZCxcJGQoIFwgLFwAAAAA=";

			// set the converter options - optional
			pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
			pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
			pdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;


			// set if header and footer are shown in the PDF - optional - default is false 
			pdfConverter.PdfDocumentOptions.ShowHeader = true;
			pdfConverter.PdfDocumentOptions.ShowFooter = true;
			// set if the HTML content is resized if necessary to fit the PDF page width - default is true
			pdfConverter.PdfDocumentOptions.FitWidth = true;

			// set the embedded fonts option - optional - default is false
			pdfConverter.PdfDocumentOptions.EmbedFonts = false;
			// set the live HTTP links option - optional - default is true
			pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;

			// set if the JavaScript is enabled during conversion to a PDF - default is true
			pdfConverter.JavaScriptEnabled = true;

			// set if the images in PDF are compressed with JPEG to reduce the
			// PDF document size - default is true
			pdfConverter.PdfDocumentOptions.JpegCompressionEnabled = true;

			// enable auto-generated bookmarks for a specified list of HTML selectors (e.g. H1 and H2)
			//if (cbBookmarks.Checked)
			//{
			//    pdfConverter.PdfBookmarkOptions.HtmlElementSelectors = new string[] { "H1", "H2" };
			//}

			// add HTML header
			//if (cbAddHeader.Checked)
			//    AddHeader(pdfConverter);
			// add HTML footer
			//if (cbAddFooter.Checked)
			//    AddFooter(pdfConverter);

			// Performs the conversion and get the pdf document bytes that can

			// be saved to a file or sent as a browser response
			byte[] pdfBytes = pdfConverter.GetPdfBytesFromUrl(urlToConvert);

			// send the PDF document as a response to the browser for download
			System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
			response.Clear();
			response.AddHeader("Content-Type", "application/pdf");
			//if (radioAttachment.Checked)
			//    response.AddHeader("Content-Disposition", String.Format("attachment; filename=GettingStarted.pdf; size={0}", 
			//                        pdfBytes.Length.ToString()));
			//else
			//    response.AddHeader("Content-Disposition", String.Format("inline; filename=GettingStarted.pdf; size={0}", 
			//                        pdfBytes.Length.ToString()));
			response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + pdf + ".pdf; size={0}",
									pdfBytes.Length.ToString()));
			response.BinaryWrite(pdfBytes);
			// Note: it is important to end the response, otherwise the ASP.NET
			// web page will render its content to PDF document stream
			response.End();
		}
	}
}