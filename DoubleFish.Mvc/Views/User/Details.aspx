<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<DoubleFish.Model.UserInfo>" %>

<%@ Import Namespace="DoubleFish.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">Id</div>
        <div class="display-field"><%: Model.Id %></div>
        
        <div class="display-label">Name</div>
        <div class="display-field"><%: Model.Name %></div>
        
        <div class="display-label">FullName</div>
        <div class="display-field"><%: Model.FullName %></div>
        
        <div class="display-label">Sex</div>
        <div class="display-field"><%: Model.Sex %></div>
        
        <div class="display-label">Birthday</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.Birthday) %></div>
        
        <div class="display-label">Tel</div>
        <div class="display-field"><%: Model.Tel %></div>
        
        <div class="display-label">Mobile</div>
        <div class="display-field"><%: Model.Mobile %></div>
        
        <div class="display-label">Mail</div>
        <div class="display-field"><%: Model.Mail %></div>
        
        <div class="display-label">Orgin</div>
        <div class="display-field"><%: Model.Region %></div>
        
        <div class="display-label">Status</div>
        <div class="display-field"><%: Model.Status %></div>
        
        <div class="display-label">RegistTime</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.RegistTime) %></div>
        
    </fieldset>
    <p>

        <%: Html.ActionLink("Edit", "Edit", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</body>
</html>

