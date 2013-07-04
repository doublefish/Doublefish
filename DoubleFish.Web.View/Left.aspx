<%@ Page Title="" Language="C#" MasterPageFile="~/MpIframe.Master" AutoEventWireup="true"
	CodeBehind="Left.aspx.cs" Inherits="DoubleFish.Web.View._Left" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<link type="text/css" rel="stylesheet" href="common/css/Left.css" />
	<script type="text/javascript" src="common/js/Left.js"></script>
	<script type="text/javascript" language="javascript">
		$(document).ready(function () {
			onPageLoad();
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	
	<div class="menu" id="divMenu">
		<%--<ul id="menu_ul_0">
			<li>
				<span>
					<a href="#">组织机构</a></span>
				<ul>
					<li><a href="Org/DepartmentMgr.aspx" target="frame_right">部门管理</a></li>
					<li><a href="Org/UserMgr.aspx" target="frame_right">用户管理</a></li>
				</ul>
				<span>
					<a href="#">系统管理</a></span>
				<ul>
					<li>
						<span>
							<a href="#">权限设置</a></span>
						<ul>
							<li><a href="Sys/MenuMgr.aspx" target="frame_right">菜单管理</a></li>
							<li><a href="Sys/RoleMgr.aspx" target="frame_right">角色管理</a></li>
							<li><a href="Sys/UserRole.aspx" target="frame_right">用户角色</a></li>
							<li><a href="Sys/Permission.aspx" target="frame_right">权限管理</a></li>
						</ul>
					</li>
				</ul>
			</li>
		</ul>--%>
	</div>
</asp:Content>
