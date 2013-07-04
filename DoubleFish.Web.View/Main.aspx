<%@ Page Title="" Language="C#" MasterPageFile="~/MpHome.Master" AutoEventWireup="true"
	CodeBehind="Main.aspx.cs" Inherits="DoubleFish.Web.View._Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

	<script type="text/javascript">

		$(document).ready(function () {
			var interval = function () { initIframe("frame_left"); initIframe("frame_right"); };
			window.setInterval(interval, 200);
		});

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="header" style="height: 100px;">
		<div class="wrap">
			<h1 class="logo">
				<a href="#">DoubleFish</a> <span id="spaUser">
					<asp:Literal ID="litUser" runat="server"></asp:Literal></span>
			</h1>
			<p>
				<br />
				世界没有悲剧和喜剧之分，如果你能从悲剧中走出来，那就是喜剧，如果你沉缅于喜剧之中，那它就是悲剧。</p>
		</div>
	</div>
	<div class="wrap">
		<div class="left">
			<iframe id="frame_left" name="frame_left" src="Left.aspx" frameborder="0" scrolling="no"
				style="width: 100%; background-color: red;"></iframe>
		</div>
		<div class="right">
			<iframe id="frame_right" name="frame_right" src="Right.aspx" frameborder="0" scrolling="no"
				style="width: 100%; background-color: Green;"></iframe>
		</div>

	</div>
</asp:Content>
