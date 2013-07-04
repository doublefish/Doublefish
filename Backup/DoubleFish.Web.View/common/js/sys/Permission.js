
var setting = {
	view: {
		selectedMulti: false
	},
	check: {
		enable: true
	},
	data: {
		key: {
			name: "Name"
		},
		simpleData: {
			enable: true,
			idKey: "Id",
			pIdKey: "Parent",
			rootPId: null
		}
	}
};

var PageSize = 10;

function onPageLoad() {

	changePageRole(1);
	changePageUser(1);
	getMenu();
}

function getTree() {
	var zTree = $.fn.zTree.getZTreeObj("menu");
	return zTree;
}

function getMenu() {

	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = {};
	ajax.data.Action = "GetMenu";
	ajax.data.ajax = new Date().getTime();
	ajax.success = function (result) {

		var nodes = eval("(" + result + ")");
		$.fn.zTree.init($("#menu"), setting, nodes);
		getTree().expandAll(true);
	};
	ajax.error = function (error) {
		alert(error.responseText);
	};
	$.ajax(ajax);
}

/*
 * 加载角色
 */
function loadRole(json) {

	$("#tb-role").html("");
	$("#divPagerRole").html("");

	if (!json)
		return;

	var totalCount = parseInt(json.ResultCount);
	var pageSize = parseInt(json.PageSize);
	var pageIndex = parseInt(json.PageIndex);

	var html = '';
	if (json.Results && json.Results.length > 0) {
		for (var i = 0; i < json.Results.length; i++) {

			var data = json.Results[i];

			var index = (pageIndex - 1) * pageSize + i + 1;

			if (i % 2 == 0)
				html += '<tr style="background-color: whitesmoke;">';
			else
				html += '<tr style="background-color: white;">';

			html += '<td><input name="ur" type="radio" value="role-' + data.Id + '" onclick="getPermission(' + data.Id + ', 0);" /></td>';
			html += '<td>' + index + '</td>';
			html += '<td>' + data.Name + '</td>';
			html += '</tr>';
		}
	}
	$("#tb-role").html(html);

	$('#divPagerRole').pager({

		pageIndex: pageIndex,
		pageSize: pageSize,
		totalCount: totalCount,
		viewCount: 7,
		callBack: changePageRole,
		prevText: '上一页',
		nextText: '下一页',
		homeText: '首页',
		lastText: '尾页',
		showHomeLast: false
	});
}

/*
 * 加载用户
*/
function loadUser(json) {

	$("#tb-user").html("");
	$("#divPagerUser").html("");

	if (!json)
		return;

	var totalCount = parseInt(json.ResultCount);
	var pageSize = parseInt(json.PageSize);
	var pageIndex = parseInt(json.PageIndex);

	var html = '';

	for (var i = 0; i < json.Results.length; i++) {

		var data = json.Results[i];

		var index = (pageIndex - 1) * pageSize + i + 1;

		if (i % 2 == 0)
			html += '<tr style="background-color: whitesmoke;">';
		else
			html += '<tr style="background-color: white;">';

		html += '<td><input name="ur" type="radio" value="user-' + data.Id + '" onclick="getPermission(0, ' + data.Id + ');" /></td>';
		html += '<td>' + index + '</td>';
		html += '<td>' + data.Name + '</td>';
		html += '</tr>';
	}

	$("#tb-user").html(html);

	$('#divPagerUser').pager({

		pageIndex: pageIndex,
		pageSize: pageSize,
		totalCount: totalCount,
		viewCount: 7,
		callBack: changePageUser,
		prevText: '上一页',
		nextText: '下一页',
		homeText: '首页',
		lastText: '尾页',
		showHomeLast: false
	});
}

/*
 * 列表翻页
 * action=pageRole=角色列表翻页
 * action=pageUser=用户列表翻页
*/
function changePage(page, action) {

	var data = {};
	data.Action = action;
	if (data.Action == "PageRole") {
		data.Name = $("#txtRoleName").val();
	}
	else if (data.Action == "PageUser") {
		data.Name = $("#txtUserCode").val();
	}

	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = data;
	ajax.data.PageIndex = page;
	ajax.data.PageSize = PageSize;
	ajax.data.ajax = new Date().getTime();
	ajax.success = function (result) {
		var sr = eval("(" + result + ")");
		if (data.Action == "PageRole")
			loadRole(sr);
		else if (data.Action == "PageUser")
			loadUser(sr);
	};
	ajax.error = function (error) {
		alert(error.responseText);
	};
	$.ajax(ajax);
}

/*
 * 角色列表翻页
 */
function changePageRole(page)
{
	changePage(page, "PageRole");
}

/*
 * 菜单列表翻页
 */
function changePageUser(page)
{
	changePage(page, "PageUser");
}

function search(action) {

	changePage(1, action);
}

function getPermission(role, user)
{
	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = {};
	ajax.data.Action = "GetPms";
	ajax.data.User = user;
	ajax.data.Role = role;
	ajax.data.ajax = new Date().getTime();
	ajax.success = function(result)
	{
		var sr = eval("(" + result + ")");
		loadPms(sr);
	};
	ajax.error = function(error)
	{
		alert(error.responseText);
	};
	$.ajax(ajax);
}

/*
* 将指定权限所关联的菜单选项设置为选中状态
*/
function loadPms(json) {

	var zTree = getTree();
	zTree.checkAllNodes(false);

	for (var i = 0; i < json.length; i++) {
		var node = zTree.getNodeByParam("Id", json[i].Menu, null);
		if (!node) continue;
		zTree.checkNode(node, true, false);
	}
}

/*
 * 保存
 */
function onSave() {

	var ur = $('input[type="radio"][name="ur"]:checked').val();

	if (!ur) {
	
		alert("请选择要分配权限的角色或用户！");
		return false;
	}

	var nodes = getTree().getCheckedNodes(true);

	var menus = new Array();
	for (var i = 0; i < nodes.length; i++) {

		menus.push(nodes[i].Id);
	}

	var user = 0, role = 0;

	var temp = ur.split('-');
	if (temp[0] == "role") role = temp[1];
	else if (temp[0] == "user") user = temp[1];
	
	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = {};
	ajax.data.Action = "Save";
	ajax.data.Role = role;
	ajax.data.User = user;
	ajax.data.Menus = menus;
	ajax.data.ajax = new Date().getTime();
	ajax.success = function (result) {

		try {
			var json = eval("(" + result + ")");
			if (json.length > 0) {
				alert("保存成功！");
				loadPms(json);
			}
			else
				alert(result);
		}
		catch (x) {
			alert(result);
		}
	};
	ajax.error = function(error) {
		alert(error.responseText);
	};
	$.ajax(ajax);
}
















