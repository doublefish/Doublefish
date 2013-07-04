<%@ Page Title="" Language="C#" MasterPageFile="~/MpHome.Master" AutoEventWireup="true"
	CodeBehind="Index.aspx.cs" Inherits="DoubleFish.Web.View.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

	<script type="text/javascript">

		$(document).ready(function () {
			var interval = function () { initIframe("frame_body") };
			window.setInterval(interval, 200);
		});

	</script>
	<script type="text/javascript">

		function login() {

			window.location.href = "Main.aspx";

		}

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
<asp:PlaceHolder ID="phLogin" runat="server">
	<div class="top" id="divLogin">
		登录名&nbsp;&nbsp;<input type="text" id="txtUser" />
		密码&nbsp;&nbsp;<input type="text" id="txtPassword" />
		<input type="button" value="登录" onclick="javascript:login();" />
		<input type="button" value="忘记密码" />
		<input type="button" value="注册" />
	</div>
</asp:PlaceHolder>
	<div class="header">
		<div class="wrap">
			<h1 class="logo">
				<a href="#">DoubleFish</a>
				<span id="spaUser"><asp:Literal ID="litUser" runat="server"></asp:Literal></span>
			</h1>
			<p>
				<br />
				世界没有悲剧和喜剧之分，如果你能从悲剧中走出来，那就是喜剧，如果你沉缅于喜剧之中，那它就是悲剧。</p>
			<ul class="menu">
				<li><a class="current" href="Home.aspx" target="frame_body">Home</a></li>
				<li><a href="#">Underwater Gear</a></li>
				<li><a href="#">Guides</a></li>
				<li><a href="#">Equipment</a></li>
				<li><a href="#">Videos</a></li>
				<li><a href="#">FAQ</a></li>
				<li><a href="#">Become a Partner</a></li>
				<li><a href="#">About Us</a></li>
				<li class="last"><a href="#">Contact Us</a></li>
			</ul>
		</div>
	</div>
	<div class="wrap" style="margin-top: 30px;">
		<div class="main">
			<div class="l">
				<h2>
					Lifestyle</h2>
				<h3>
					Duis aute irure dolor in reprehenderit</h3>
				<img src="common/images/img1.jpg" alt="Img" />
				<span>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip
					ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit
					esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non
					proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</span>
				<a href="#">&raquo; Read More</a>
			</div>
			<div class="r">
				<h2>
					Sed ut perspiciatis unde omnis</h2>
				<span>Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
					mollit anim id est laborum.</span>
				<h2>
					Nemo enim ipsam voluptatem</h2>
				<span>Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
					mollit anim id est laborum.</span>
				<h2>
					Sed ut perspiciatis unde omnis</h2>
				<span>Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
					mollit anim id est laborum.</span>
			</div>
			<div class="line">
			</div>
			<div class="l">
				<h2>
					Nemo enim ipsam voluptatem</h2>
				<span>Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
					mollit anim id est laborum.</span>
			</div>
			<div class="r">
				<h2>
					Nemo enim ipsam voluptatem</h2>
				<span>Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
					mollit anim id est laborum.</span>
			</div>
		</div>
		<div class="side">
			<ul>
				<li><a href="#">Esse cillum dolore</a><br />
					Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
					mollit anim id est laborum.</li>
				<li><a href="#">Esse cillum dolore</a><br />
					Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
					mollit anim id est laborum.</li>
				<li><a href="#">Esse cillum dolore</a><br />
					Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
					mollit anim id est laborum.</li>
				<li><a href="#">Esse cillum dolore</a><br />
					Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
					mollit anim id est laborum.</li>
			</ul>
		</div>
		<div class="footer">
			<ul>
				<li class="title">Item group #1</li>
				<li><a href="#">Ut enim ad minim</a></li>
				<li><a href="#">Ut enim ad minim</a></li>
				<li><a href="#">Ut enim ad minim</a></li>
			</ul>
			<ul>
				<li class="title">Item group #2</li>
				<li><a href="#">Ut enim ad minim</a></li>
				<li><a href="#">Ut enim ad minim</a></li>
				<li><a href="#">Ut enim ad minim</a></li>
			</ul>
			<div>
				Design: DoubleFish - <a href="#" title="doublefish">doublefish.com</a></div>
		</div>
	</div>
</asp:Content>
