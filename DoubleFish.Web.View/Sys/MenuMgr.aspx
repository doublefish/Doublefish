<%@ Page Title="" Language="C#" MasterPageFile="~/MpIframe.Master" AutoEventWireup="true"
	CodeBehind="MenuMgr.aspx.cs" Inherits="DoubleFish.Web.View.Sys._MenuMgr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<link type="text/css" rel="stylesheet" href="../common/jquery.ztree/css/zTreeStyle/zTreeStyle.css" />
	<script type="text/javascript" src="../common/jquery.ztree/js/jquery.ztree.core-3.2.js"></script>
	<script type="text/javascript" src="../common/js/sys/MenuMgr.js"></script>
	<style type="text/css">
		.table { width: 90%; margin: 0px auto 0px auto; padding: 10px 10px 10px 10px; border: solid 1px gray; }
		.table th { width: 25%; padding-right: 5px; text-align: right; line-height: 30px; }
		.table td { padding-left: 5px; line-height: 30px; }
		.table td input.text { width: 80%; }
		.table td textarea { width: 80%; }
	</style>
	<style type="text/css">
		div#rMenu {position:absolute; visibility:hidden ;text-align: left;padding: 1px; border: solid 1px #718BB7;}
		div#rMenu ul li {
			margin: 0 2px 0 2px;
			padding: 0 1px 0 1px;
			cursor: pointer;
			list-style: none outside none;
			line-height:22px;
			background-color: #F0F0F0;
		}
		div#rMenu ul li:hover { background-color:#D9E8FB; border: solid 1px #718BB7; }
	</style>
	
	<script type="text/javascript">
		$(document).ready(function () {
			onPageLoad();
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="title">
		您当前位置：系统设置&gt;&gt;权限管理&gt;&gt;菜单管理
	</div>
	<div class="left" style=" width: 30%; overflow: auto;">
		<ul id="menu" class="ztree">
		</ul>
	</div>
	<div class="right" style="width: 69%;">
		<table cellpadding="0" cellspacing="0" border="0" class="table">
			<tr>
				<th>
					Name:
				</th>
				<td>
					<input type="text" id="txtName" />
					<input type="hidden" id="hId" value="0" />
				</td>
			</tr>
			<tr>
				<th>
					Parent:
				</th>
				<td>
					<input type="text" id="txtParent" readonly="readonly" />
					<input type="hidden" id="hParent" value="0" />
				</td>
			</tr>
			<tr>
				<th>
					Type:
				</th>
				<td>
					<select id="selType">
						<option value="0">请选择</option>
						<option value="1">菜单</option>
						<option value="2">页面</option>
						<option value="3">元素</option>
					</select>
				</td>
			</tr>
			<tr>
				<th>
					Url:
				</th>
				<td>
					<input type="text" id="txtUrl" />
				</td>
			</tr>
			<tr>
				<th>
					Flag:
				</th>
				<td>
					<select id="selFlag">
						<option value="1">启用</option>
						<option value="0">禁用</option>
					</select>
				</td>
			</tr>
			<tr>
				<th>
					Note:
				</th>
				<td>
					<textarea cols="37" rows="3" id="txtNote">
					</textarea>
				</td>
			</tr>
			<tr>
				<td colspan="2" style="text-align: center; height: 35px;">
					<input type="button" value="保存" onclick="javascript:onSave();" />
					<input type="button" value="重置" onclick="javascript:onReset();" />
				</td>
			</tr>
		</table>
	</div>
	<div id="rMenu">
		<ul>
			<li id="m_add_child" onclick="javascript:addChildNode();">添加子级</li>
			<li id="m_add_brother" onclick="javascript:addBrotherNode();">添加平级</li>
			<li id="m_able" onclick="javascript:ableNode();">启用</li>
			<li id="m_del" onclick="javascript:delNode();">删除</li>
		</ul>
	</div>
</asp:Content>
