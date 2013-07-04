<%@ Page Title="" Language="C#" MasterPageFile="~/MpIframe.Master" AutoEventWireup="true"
	CodeBehind="ICS.aspx.cs" Inherits="DoubleFish.Web.View.ICS._ICS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	
	<script type="text/javascript" language="javascript">
		function onCollection() {

			var data = {};
			data.Url = $("#txtUrl").val();

			var ajax = {};
			ajax.type = "POST";
			ajax.url = window.location.href;
			ajax.data = data;
			ajax.data.ajax = new Date().getTime();
			ajax.success = function (result) {
				$("#txtResult").text(result);
			};
			ajax.error = function (error) {
				$("#txtResult").text(error);
			};
			$.ajax(ajax);
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<input type="text" id="txtUrl" value="" />
	<input type="button" value="collection" />
	<br />
	<textarea id="txtResult" rows="50" cols="99"></textarea>
</asp:Content>
