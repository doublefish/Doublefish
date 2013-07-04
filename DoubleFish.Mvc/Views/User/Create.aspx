<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<DoubleFish.Model.UserInfo>" %>

<%@ Import Namespace="DoubleFish.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Create</title>
</head>
<body>
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Id) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Id) %>
                <%: Html.ValidationMessageFor(model => model.Id) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.FullName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.FullName) %>
                <%: Html.ValidationMessageFor(model => model.FullName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Sex) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Sex) %>
                <%: Html.ValidationMessageFor(model => model.Sex) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Birthday) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Birthday) %>
                <%: Html.ValidationMessageFor(model => model.Birthday) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Tel) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Tel) %>
                <%: Html.ValidationMessageFor(model => model.Tel) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Mobile) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Mobile) %>
                <%: Html.ValidationMessageFor(model => model.Mobile) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Mail) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Mail) %>
                <%: Html.ValidationMessageFor(model => model.Mail) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Region) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Region) %>
                <%: Html.ValidationMessageFor(model => model.Region) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Status) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Status) %>
                <%: Html.ValidationMessageFor(model => model.Status) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.RegistTime) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.RegistTime) %>
                <%: Html.ValidationMessageFor(model => model.RegistTime) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</body>
</html>

