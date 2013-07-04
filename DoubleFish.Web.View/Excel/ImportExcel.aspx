<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportExcel.aspx.cs" Inherits="DoubleFish.Web.View.Excel.ImportExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
	<form action="" method="post" runat="server">
	<asp:FileUpload ID="fileUpload" runat="server" />
	<asp:Button ID="btnUpload" runat="server" Text="上传" onclick="btnUpload_Click" />
	<asp:DataGrid ID="dg1" runat="server">
	</asp:DataGrid>
	</form>
</body>
</html>
