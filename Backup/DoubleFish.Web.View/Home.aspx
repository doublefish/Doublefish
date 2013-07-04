<%@ Page Title="" Language="C#" MasterPageFile="~/MpHome.Master" AutoEventWireup="true"
	CodeBehind="Home.aspx.cs" Inherits="DoubleFish.Web.View._Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<script type="text/javascript" src="../common/js/initIframe.js"></script>
	<script type="text/javascript" language="javascript">

		$(document).ready(function () {
			initIframe("frame_left");
			var interval = function () { initIframe("frame_right"); };
			window.setInterval(interval, 200);
		});

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="left">
		<iframe id="frame_left" name="frame_left" src="Left.aspx" frameborder="0" scrolling="auto"
			style="width: 100%; min-height: 500px;"></iframe>
	</div>
	<div class="right">
		<iframe id="frame_right" name="frame_right" src="/Org/UserMgr.aspx" frameborder="0" scrolling="no"
			style="width: 100%;"></iframe>
	</div>
</asp:Content>
