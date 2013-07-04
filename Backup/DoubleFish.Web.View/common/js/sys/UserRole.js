
var PageSize = 10;

function onPageLoad() {
	
}

function loadUser(json, isIn) {

	$("#tb-user" + isIn).html("");
	$("#divPager" + isIn).html("");

	if(!json)
		return;

	var totalCount = parseInt(json.ResultCount);
	var pageSize = parseInt(json.PageSize);
	var pageIndex = parseInt(json.PageIndex);

	if (!json.Results)
		return;

	var html = '';
	if (json.Results && json.Results.length > 0) {
		for (var i = 0; i < json.Results.length; i++) {

			var data = json.Results[i];

			if (i % 2 == 0)
				html += '<tr style="background-color: whitesmoke;">';
			else
				html += '<tr style="background-color: white;">';

			html += '<td><input name="user' + isIn + '" type="checkbox" value="' + data.Id + '" /></td>';
			html += '<td>' + data.Id + '</td>';
			html += '<td>' + data.Name + '</td>';
			html += '<td>' + data.FullName + '</td>';
			html += '</tr>';
		}
	}
	$("#tb-user" + isIn).html(html);

	if (isIn > 0) {
		$('#divPager1').pager({

			pageIndex: pageIndex,
			pageSize: pageSize,
			totalCount: totalCount,
			viewCount: 5,
			callBack: changePage1,
			prevText: '上一页',
			nextText: '下一页',
			homeText: '首页',
			lastText: '尾页',
			showHomeLast: false
		});
	}
	else {
		$('#divPager0').pager({

			pageIndex: pageIndex,
			pageSize: pageSize,
			totalCount: totalCount,
			viewCount: 5,
			callBack: changePage0,
			prevText: '上一页',
			nextText: '下一页',
			homeText: '首页',
			lastText: '尾页',
			showHomeLast: false
		});
	}
}

function changePage(page, isIn) {

	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = {};
	ajax.data.Action = "PageUser";
	ajax.data.Role = $("#selRole").val();
	ajax.data.Code = $("#txtCode" + isIn).val();
	ajax.data.IsIn = isIn;
	ajax.data.PageIndex = page;
	ajax.data.PageSize = PageSize;
	ajax.data.ajax = new Date().getTime();
	ajax.success = function(result) {
		var sr = eval("(" + result + ")");
		loadUser(sr, isIn);
	};
	ajax.error = function(error) {
		alert(error.responseText);
	};
	$.ajax(ajax);
}

function changePage0(page) {

	changePage(page, 0);
}

function changePage1(page) {

	changePage(page, 1);
}

function selRole(role) {

	if (!role || role <= 0) {
		$("#tb-user0").html("");
		$("#tb-user1").html("");
		$("#pageBar0").html("");
		$("#pageBar1").html("");
		return;
	}
	$("#txtCode0").val("");
	$("#txtCode1").val("");
	changePage(1, 0);
	changePage(1, 1);
}

function selByCode(isIn) {

	var role = $("#selRole").val();
	if (role <= 0) {

		alert("请选择角色！");
		$("#selRole").focus();
		return false;
	}
	changePage(1, isIn);
}

function checkAll(name) {
	var checked = $("#" + name + "").attr("checked");
	$("input[name='" + name + "']").each(function() {
		$(this).attr("checked", !!checked);
	});
}

function getChkArr(name) {
	var ids = new Array();
	$("input[name='" + name + "']").each(function() {
		if (this.checked)
			ids.push(this.value);
	});
	return ids;
}

function move(isIn) {
	var role = $("#selRole").val();
	if (!role || role < 0) {
		alert("请选择要操作的角色！");
		return false;
	}
	var name = "";
	if (isIn > 0)//移入
		name = "user0";
	else//移出
		name = "user1";

	var users = getChkArr(name);

	if (users.length == 0) {
		var msg = "";
		if (isIn > 0)
			msg = "请选择要移入的用户！";
		else
			msg = "请选择要移出的用户！";
		alert(msg);
		return false;
	}
	moveUser(isIn, users, role);
}

/*
 * 移动用户
 */
function moveUser(moveIn, users, role) {
	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = {};
	ajax.data.Action = "MoveUser";
	ajax.data.Role = role;
	ajax.data.MoveIn = moveIn;
	ajax.data.Users = users;
	ajax.success = function (result) {
		if (result == "ok") {
			changePage(1, 0);
			changePage(1, 1);
		}
		else
			alert(result);
	};
	ajax.error = function(error) {
		alert(error.responseText);
	};
	$.ajax(ajax);
}

