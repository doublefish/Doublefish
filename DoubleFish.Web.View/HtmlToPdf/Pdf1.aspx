<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pdf1.aspx.cs" Inherits="DoubleFish.Web.View.HtmlToPdf._Pdf1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>WnvHtmlConvert</title>
	<script type="text/javascript">
		function htmlToPdf() {

			var url = document.getElementById("txtUrl").value;
			var pdf = document.getElementById("txtPdf").value;
			window.open("Pdf1.aspx?pdf=" + pdf + "&url=" + url + "");
		}
	</script>
</head>
<body>
	Url<input type="text" value="" id="txtUrl" />
	Pdf<input type="text" value="" id="txtPdf" />.pdf
	<input type="button" value="convert" onclick="htmlToPdf()" />
</body>
</html>
