<%@ Page Title="" Language="C#" MasterPageFile="~/MpIframe.Master" AutoEventWireup="true"
	CodeBehind="CustomForm.aspx.cs" Inherits="DoubleFish.Web.View.Sys._CustomForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<script type="text/javascript" language="javascript">
		var rowCount = 0;
		function addTr() {
			rowCount++;
			var html = '<tr id="tr-' + rowCount + '"><td>&nbsp;</td><td>&nbsp;</td></tr>';
			$("#divForm > table").append(html);
		}
	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="title">
		您当前位置：系统设置&gt;&gt;自定义表单
	</div>

	<div class="info" style=" border: solid 1px green;" id="divForm">
		<table cellpadding="0" cellspacing="0" border="1" style="width: 98%; margin-left: auto;
			margin-right: auto; overflow: hidden;">
		</table>
	</div>

	<div class="info">
		<input type="button" value="增加一行" onclick="addTr();" />
	</div>

	<div id="container">
		<div id="dragsource">
			<p>
				拽我!</p>
		</div>
	</div>
	<!-- End container -->

	<script>
		$(function () {
			$("#dragsource").draggable();
		})
	</script>
</asp:Content>
