<%@ Page Title="" Language="C#" MasterPageFile="~/MpIframe.Master" AutoEventWireup="true"
	CodeBehind="Permission.aspx.cs" Inherits="DoubleFish.Web.View.Sys._Permission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<link href="../common/lib/doublefish.pager.css" rel="stylesheet" type="text/css" />
	<script src="../common/lib/doublefish.pager.js" type="text/javascript"></script>
	<link href="../common/jquery.ztree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet"
		type="text/css" />
	<script src="../common/jquery.ztree/js/jquery.ztree.core-3.2.js" type="text/javascript"></script>
	<script src="../common/jquery.ztree/js/jquery.ztree.excheck-3.2.js" type="text/javascript"></script>
	<script src="../common/js/sys/Permission.js" type="text/javascript"></script>
	<script type="text/javascript" language="javascript">
		$(document).ready(function () {
			onPageLoad();
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="title">
		您当前位置：系统设置&gt;&gt;权限管理&gt;&gt;权限设置
	</div>
	<div class="left" style="width: 50%;">
		<div class="list">
			<div style=" text-align: center;">
				角色名称：
				<input type="text" id="txtRoleName" />
				<input type="button" value="检索" onclick="search('PageRole');" />
			</div>
			<table border="0" cellspacing="0" cellpadding="0">
				<tr style="background-color: #eff7fb">
					<th style="width: 15%;">
						选择
					</th>
					<th style="width: 20%;">
						编号
					</th>
					<th style="width: 65%;">
						角色名称
					</th>
				</tr>
				<tbody id="tb-role">
				</tbody>
			</table>
			<div id="divPagerRole" class="dfPager">
			</div>
			<div style="text-align: center;">
				用&nbsp;&nbsp;户&nbsp;&nbsp;名：
				<input type="text" id="txtUserCode" />
				<input type="button" class="inputBtm2" value="检索" onclick="search('PageUser');" />
			</div>
			<table border="0" cellspacing="0" cellpadding="0">
				<tr style="background-color: #eff7fb">
					<th style="width: 15%;">
						选择
					</th>
					<th style="width: 20%;">
						编号
					</th>
					<th style="width: 65%;">
						用户名
					</th>
				</tr>
				<tbody id="tb-user">
				</tbody>
			</table>
			<div id="divPagerUser" class="dfPager">
			</div>
		</div>
	</div>
	<div class="right" style="width: 49%; overflow: auto;">
		<input type="button" class="inputBtn" value="保存" onclick="onSave();" />
		<div class="bdcolor">
			<ul id="menu" class="ztree">
			</ul>
		</div>
	</div>
</asp:Content>
