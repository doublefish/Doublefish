<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DoubleFish.Model.UserInfo>>" %>

<%@ Import Namespace="DoubleFish.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>Index</title>
</head>
<body>
	<table>
		<tr>
			<th>
			</th>
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
		<% foreach (var item in Model) { %>
		<tr>
			<td>
				<%: Html.ActionLink("Edit", "Edit", new { id=item.Id }) %>
				|
				<%: Html.ActionLink("Details", "Details", new { id=item.Id })%>
				|
				<%: Html.ActionLink("Delete", "Delete", new { id=item.Id })%>
			</td>
			<td>
				<%: item.Id %>
			</td>
			<td>
				<%: item.Name %>
			</td>
			<td>
				<%: item.FullName %>
			</td>
			<td>
				<%: item.Sex %>
			</td>
			<td>
				<%: String.Format("{0:g}", item.Birthday) %>
			</td>
			<td>
				<%: item.Tel %>
			</td>
			<td>
				<%: item.Mobile %>
			</td>
			<td>
				<%: item.Mail %>
			</td>
			<td>
				<%: item.Region%>
			</td>
			<td>
				<%: item.Status %>
			</td>
			<td>
				<%: String.Format("{0:g}", item.RegistTime) %>
			</td>
		</tr>
		<% } %>
	</table>
	<p>
		<%: Html.ActionLink("Create New", "Create") %>
	</p>
</body>
</html>
