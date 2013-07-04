<%@ Page Title="用户管理" Language="C#" MasterPageFile="~/MpIframe.Master" AutoEventWireup="true"
	CodeBehind="UserMgr.aspx.cs" Inherits="DoubleFish.Web.View.Org._UserMgr" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="cphHead" runat="server">
	<link href="../common/lib/doublefish.pager.css" rel="stylesheet" type="text/css" />
	<script src="../common/lib/doublefish.pager.js" type="text/javascript"></script>
	<script src="../common/js/org/User.js" type="text/javascript"></script>
	
	<script type="text/javascript" language="javascript">

		$(document).ready(function () {
			onPageLoad();
		});

	</script>
</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="cphBody" runat="server">
	<div class="title">
		您当前位置：组织机构 &gt;&gt; 用户管理
	</div>
	<div class="search">
		<table cellpadding="0" cellspacing="0" border="0">
			<tr>
				<th>
					Name
				</th>
				<td>
					<input type="text" id="sel_name" />
				</td>
				<th>
					FullName
				</th>
				<td>
					<input type="text" id="sel_fullname" />
				</td>
				<th>
					Sex
				</th>
				<td>
					<input type="radio" id="sel_sex_2" name="sel_sex" value="2" /><label for="sel_sex_2">女</label>
					<input type="radio" id="sel_sex_1" name="sel_sex" value="1" /><label for="sel_sex_1">男</label>
					<input type="radio" id="sel_sex_0" name="sel_sex" value="0" /><label for="sel_sex_0">全部</label>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<input type="button" value="search" onclick="javascript:search();" />
				</td>
			</tr>
		</table>
	</div>

	<div class="list">
		<table id="table" cellpadding="0" cellspacing="0" border="1">
			<thead>
				<tr>
					<th>
						Id
					</th>
					<th>
						Name
					</th>
					<th>
						FullName
					</th>
					<th>
						Sex
					</th>
					<th>
						Birthday
					</th>
					<th>
						Tel
					</th>
					<th>
						Mobile
					</th>
					<th>
						Mail
					</th>
					<th>
						Region
					</th>
					<th>
						Status
					</th>
					<th>
						RegistTime
					</th>
				</tr>
			</thead>
			<tbody id="tbody">
			</tbody>
		</table>
	</div>
	<div class="dfPager" id="divPager">
	</div>
</asp:Content>
