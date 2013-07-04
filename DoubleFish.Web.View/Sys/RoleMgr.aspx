<%@ Page Title="" Language="C#" MasterPageFile="~/MpIframe.Master" AutoEventWireup="true"
	CodeBehind="RoleMgr.aspx.cs" Inherits="DoubleFish.Web.View.Sys._RoleMgr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<link href="../common/lib/doublefish.pager.css" rel="stylesheet" type="text/css" />
	<script src="../common/lib/doublefish.pager.js" type="text/javascript"></script>
	<script src="../common/js/sys/RoleMgr.js" type="text/javascript"></script>
	<script type="text/javascript">

		$(document).ready(function () { onPageLoad(); });

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="title">
		您当前位置：系统设置&gt;&gt;权限管理&gt;&gt;角色管理
	</div>
	<div class="search">
		<table border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td style="width: 7%;">
					名称：
				</td>
				<td style="width: 26%; text-align: left;">
					<input type="text" id="txt_name" />
				</td>
				<td style="width: 8%; text-align: right;">
					状态：
				</td>
				<td style="width: 27%; text-align: left;">
					<select id="sel_flag">
						<option value="0">请选择</option>
						<option value="2">正常</option>
						<option value="1">禁用</option>
					</select>
				</td>
				<td style="width: 32%; text-align: left;">
					<input type="button" value="查询" onclick="search();" />
				</td>
			</tr>
		</table>
	</div>
	<div class="list">
		<input type="button" value="添加" onclick="add();" style="float: right;" />
		<table border="1" cellspacing="0" cellpadding="0">
			<tr style="background-color: #eff7fb;">
				<td>
					<b>序号</b>
				</td>
				<td>
					<b>名称</b>
				</td>
				<td>
					<b>时间</b>
				</td>
				<td>
					<b>状态</b>
				</td>
				<td>
					<b>操作</b>
				</td>
			</tr>
			<tbody id="tbody">
			</tbody>
		</table>
		<div id="divPager" class="dfPager">
		</div>
	</div>
	<div id="divInfo" style="width: 360px; display: none;">
		<table cellpadding="0" cellspacing="0" border="0" style="width: 100%;" class="table">
			<tr>
				<th>
					名称：
				</th>
				<td>
					<input type="text" id="txtName" />
					<input type="hidden" id="hId" value="0" />
				</td>
			</tr>
			<tr>
				<th>
					备注：
				</th>
				<td>
					<textarea id="txtNote" rows="3" style="width: 95%;">
				</textarea>
				</td>
			</tr>
			<tr>
				<th>
					状态：
				</th>
				<td>
					<select id="selFlag" style="width: 50%;">
						<option value="1">正常</option>
						<option value="0">禁用</option>
					</select>
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
