<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<DoubleFish.Model.UserInfo>" %>

<%@ Import Namespace="DoubleFish.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>Edit</title>
	<script src="../../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
	<script type="text/javascript" language="javascript">
		function onSub() {

			var ajax = {};
			ajax.type = "POST";
			ajax.url = window.location.href;
			ajax.data = {};
			ajax.data.Id = $("#Id").val();
			ajax.data.Name = $("#Name").val();
			ajax.data.FullName = $("#FullName").val();
			ajax.data.Sex = $("#Sex").val();
			ajax.data.Birthday = $("#Birthday").val();
			ajax.data.Tel = $("#Tel").val();
			ajax.data.Mobile = $("#Mobile").val();
			ajax.data.Mail = $("#Mail").val();
			ajax.data.Region = $("#Region").val();
			ajax.data.Status = $("#Status").val();
			ajax.data.RegistTime = $("#RegistTime").val();
			ajax.data.ajax = new Date().getTime();
			ajax.success = function (result) {
				alert(result);
			};
			ajax.error = function (error) {
				alert(error);
			};
			$.ajax(ajax);
		}
	</script>
</head>
<body>
	<fieldset>
		<legend>用户基本信息</legend>
		<div class="editor-label">
			Id
		</div>
		<div class="editor-field">
			<input id="Id" name="Id" type="text" value="<%: Model.Id %>" />
		</div>
		<div class="editor-label">
			Name
		</div>
		<div class="editor-field">
			<input id="Name" name="Name" type="text" value="<%: Model.Name %>" />
		</div>
		<div class="editor-label">
			FullName
		</div>
		<div class="editor-field">
			<input id="FullName" name="FullName" type="text" value="<%: Model.FullName %>" />
		</div>
		<div class="editor-label">
			Sex
		</div>
		<div class="editor-field">
			<input id="Sex" name="Sex" type="text" value="<%: Model.Sex %>" />
		</div>
		<div class="editor-label">
			Birthday
		</div>
		<div class="editor-field">
			<input id="Birthday" name="Birthday" type="text" value="<%: String.Format("{0:g}", Model.Birthday) %>" />
		</div>
		<div class="editor-label">
			Tel
		</div>
		<div class="editor-field">
			<input id="Tel" name="Tel" type="text" value="<%: Model.Tel %>" />
		</div>
		<div class="editor-label">
			Mobile
		</div>
		<div class="editor-field">
			<input id="Mobile" name="Mobile" type="text" value="<%: Model.Mobile %>" />
		</div>
		<div class="editor-label">
			Mail
		</div>
		<div class="editor-field">
			<input id="Mail" name="Mail" type="text" value="<%: Model.Mail %>" />
		</div>
		<div class="editor-label">
			Orgin
		</div>
		<div class="editor-field">
			<input id="Region" name="Region" type="text" value="<%: Model.Region %>" /><%: Model.RegionInfo.Name %>
		</div>
		<div class="editor-label">
			Status
		</div>
		<div class="editor-field">
			<input id="Status" name="Status" type="text" value="<%: Model.Status %>" />
		</div>
		<div class="editor-label">
			RegistTime
		</div>
		<div class="editor-field">
			<input id="RegistTime" name="RegistTime" type="text" value="<%: String.Format("{0:g}", Model.RegistTime) %>" />
		</div>
		<p>
			<input type="submit" value="Save" />
			<input type="button" value="Ajax" onclick="javascript:onSub();" />
			<input type="button" value="Back" onclick="javascript:window.location.href = '/User/Index';" />
		</p>
	</fieldset>																																																																																																																																																																																											
	<div>
		<%: Html.ActionLink("Back To List", "Index") %>
		<a href="/User/Index">Back To List</a>
	</div>
</body>
</html>
