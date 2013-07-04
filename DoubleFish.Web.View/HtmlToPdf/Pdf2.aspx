<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pdf2.aspx.cs" Inherits="DoubleFish.Web.View.HtmlToPdf._Pdf2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>EvoHtmlToPdf</title>
	<script type="text/javascript">
		function htmlToPdf() {

			var url = document.getElementById("txtUrl").value;
			var pdf = document.getElementById("txtPdf").value;
			window.open("Pdf2.aspx?pdf=" + pdf + "&url=" + url + "");
		}
	</script>
</head>
<body>
	Url<input type="text" value="" id="txtUrl" />
	Pdf<input type="text" value="" id="txtPdf" />.pdf
	<input type="button" value="convert" onclick="htmlToPdf()" />
</body>
</html>
