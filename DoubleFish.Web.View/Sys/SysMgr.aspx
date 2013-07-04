<%@ Page Title="系统管理-首页" Language="C#" MasterPageFile="~/MpHome.Master" AutoEventWireup="true"
	CodeBehind="SysMgr.aspx.cs" Inherits="DoubleFish.Web.View.Sys._SysMgr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<script type="text/javascript" src="../common/js/initIframe.js"></script>
	<style type="text/css">
		.left { width: 20%; float:left; }
		.right { width: 80%; float:left;}
		li{ line-height: 25px; }
	</style>
	<script type="text/javascript" language="javascript">

		$(document).ready(function () {
			var interval = function () { initIframe("frame_right") };
			window.setInterval(interval, 200);
		});

	</script>
	<script type="text/javascript" language="javascript">
		$(function () {
			$("#accordion").accordion({
				collapsible: true,
				autoHeight: false,
				navigation: true
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="left">
		<div id="accordion">
			<h3>
				<a href="#">组织机构</a></h3>
			<div>
				<ul>
					<li><a href="DepartmentMgr.aspx" target="frame_right">部门管理</a></li>
					<li><a href="UserMgr.aspx" target="frame_right">用户管理</a></li>
				</ul>
			</div>
			<h3>
				<a href="#">权限设置</a></h3>
			<div>
				<ul>
					<li><a href="MenuMgr.aspx" target="frame_right">菜单管理</a></li>
					<li><a href="RoleMgr.aspx" target="frame_right">角色管理</a></li>
					<li><a href="UserRoleR.aspx" target="frame_right">用户角色</a></li>
					<li><a href="Permission.aspx" target="frame_right">权限分配</a></li>
				</ul>
			</div>
			<h3>
				<a href="#">Section 3</a></h3>
			<div>
				<p>
					Nam enim risus, molestie et, porta ac, aliquam ac, risus. Quisque lobortis. Phasellus
					pellentesque purus in massa. Aenean in pede. Phasellus ac libero ac tellus pellentesque
					semper. Sed ac felis. Sed commodo, magna quis lacinia ornare, quam ante aliquam
					nisi, eu iaculis leo purus venenatis dui.
				</p>
				<ul>
					<li>List item one</li>
					<li>List item two</li>
					<li>List item three</li>
				</ul>
			</div>
			<h3>
				<a href="#">Section 4</a></h3>
			<div>
				<p>
					Cras dictum. Pellentesque habitant morbi tristique senectus et netus et malesuada
					fames ac turpis egestas. Vestibulum ante ipsum primis in faucibus orci luctus et
					ultrices posuere cubilia Curae; Aenean lacinia mauris vel est.
				</p>
				<p>
					Suspendisse eu nisl. Nullam ut libero. Integer dignissim consequat lectus. Class
					aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.
				</p>
			</div>
		</div>
	</div>
	<div class="right">
		<iframe id="frame_right" name="frame_right" src="MenuMgr.aspx" frameborder="0" scrolling="no"
			style="width: 100%;"></iframe>
	</div>
</asp:Content>
