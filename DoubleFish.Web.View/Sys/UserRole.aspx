<%@ Page Title="" Language="C#" MasterPageFile="~/MpIframe.Master" AutoEventWireup="true"
	CodeBehind="UserRole.aspx.cs" Inherits="DoubleFish.Web.View.Sys._UserRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<link href="../common/lib/doublefish.pager.css" rel="stylesheet" type="text/css" />
	<script src="../common/lib/doublefish.pager.js" type="text/javascript"></script>
	<script src="../common/js/sys/UserRole.js" type="text/javascript"></script>

	<script type="text/javascript">
		$(document).ready(function () {
			
		});
	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="title">
		您当前位置：系统设置&gt;&gt;权限管理&gt;&gt;用户角色
	</div>
	<div class="search" style="margin-top: 15px; height: 30px;">
		<table cellspacing="0" cellpadding="0" border="0" style=" width: 50%;">
			<tr>
				<th>
					选择角色：
				</th>
				<td>
					<select id="selRole" onchange="javascript:selRole(this.value);">
						<option value="0">请选择</option>
						<asp:Repeater ID="repRole" runat="server">
							<ItemTemplate>
								<option value='<%# Eval("Id") %>'>
									<%# Eval("Name") %></option>
							</ItemTemplate>
						</asp:Repeater>
					</select>
				</td>
			</tr>
		</table>
	</div>
	<div class="info">
		<table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
			<tr>
				<td style="width: 46%; vertical-align: top;">
					<table border="0" cellpadding="0" cellspacing="0" class="table">
						<tr>
							<th style=" height: 30px; vertical-align: top; font-size: 14px; color: #0b74a9;" colspan="3">
								角色外用户
							</th>
						</tr>
						<tr>
							<td style=" width: 20%;">
								用户名：
							</td>
							<td style=" width: 55%;">
								<input type="text" id="txtCode0" />
							</td>
							<td style=" width: 25%;">
								<input type="submit" value="检索" onclick="selByCode(0);" />
							</td>
						</tr>
					</table>
					<table border="0" cellspacing="0" cellpadding="0" class="table">
						<tr style="background-color: #eff7fb">
							<th>
								<input type="checkbox" onclick="checkAll(this.id);" id="user0" />
							</th>
							<th>
								编号
							</th>
							<th>
								用户名
							</th>
							<th>
								姓名
							</th>
						</tr>
						<tbody id="tb-user0" style=" text-align: center;">
						</tbody>
					</table>
					<div id="divPager0" class="dfPager">
					</div>
				</td>
				<td style=" width: 8%; text-align: center; vertical-align: middle;">
					<input type="button" value="<<" title="移出" onclick="move(0);" />
					<br />
					<br />
					<input type="button" value=">>" title="移入" onclick="move(1);" />
				</td>
				<td style="width: 46%; vertical-align: top;">
					<table border="0" cellpadding="0" cellspacing="0" class="table">
						<tr>
							<th style=" color: #0b74a9;" colspan="3">
								角色内用户
							</th>
						</tr>
						<tr>
							<td style=" width: 20%;">
								用户名：
							</td>
							<td style=" width: 55%;">
								<input type="text" id="txtCode1" />
							</td>
							<td style=" width: 25%;">
								<input type="button" value="检索" onclick="selByCode(1);" />
							</td>
						</tr>
					</table>
					<table border="0" cellspacing="0" cellpadding="0" class="table">
						<tr style="background-color: #eff7fb">
							<th>
								<input type="checkbox" onclick="checkAll(this.id);" id="user1" />
							</th>
							<th>
								编号
							</th>
							<th>
								用户名
							</th>
							<th>
								姓名
							</th>
						</tr>
						<tbody id="tb-user1" style="text-align: center;">
						</tbody>
					</table>
					<div id="divPager1" class="dfPager">
					</div>
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
