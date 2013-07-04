
var PageSize = 10;

function onPageLoad() {
	changePage(1);
}

function get(index) {

	return window["JsonLst"]["Results"][index];
}

function loadList(json) {

	window["JsonLst"] = json;
	$("#tbody").empty();
	$('#divPager').empty();
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

			html += '<tr>';
			html += '<td>' + index + '</td>';
			html += '<td>' + data.Name + '</td>';
			html += '<td>' + data.Time + '</td>';
			html += '<td>' + (data.Flag > 0 ? '正常' : '<span style=" color:red;">禁用') + '</td>';
			html += '<td>';
			html += '<a href="javascript://" onclick="edit(' + i + ');">编辑</a>&nbsp;&nbsp;';
			html += '<a href="javascript://" onclick="del(' + data.Id + ');">删除</a>';
			html += '</td>';
			html += '</tr>';
		} 
	}
	$('#tbody').html(html);
	$('#divPager').pager({

		pageIndex: pageIndex,
		pageSize: pageSize,
		totalCount: totalCount,
		callBack: changePage
	});
}

function changePage(page, jq) {

	var data = {};
	
	data.Name = $("#txt_name").val();
	data.Flag = $("#sel_flag").val();

	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = data;
	ajax.data.Action = "Page";
	ajax.data.PageIndex = page;
	ajax.data.PageSize = PageSize;
	ajax.data.ajax = new Date().getTime();
	ajax.success = function (result) {

		var json = eval("(" + result + ")");
		loadList(json);
	};
	ajax.error = function (error) {
		alert(error.responseText);
	};
	$.ajax(ajax);
}

function search(e) {

	changePage(1);
}

function add() {
	clear();
	$("#dialog:ui-dialog").dialog("destroy");
	$("#divInfo").dialog({
		resizable: true,
		modal: true,
		title: '新建角色信息',
		width: 360,
		buttons: {
			"保存": function () {
				onSave();
			},
			"取消": function () {
				$(this).dialog("close");
			}
		}
	});
}

function edit(i) {

	$("#dialog:ui-dialog").dialog("destroy");
	$("#divInfo").dialog({
		resizable: true,
		modal: true,
		title: '编辑角色信息',
		width: 360,
		buttons: {
			"保存": function () {
				onSave();
			},
			"取消": function () {
				$(this).dialog("close");
			}
		}
	});
	load(get(i));
}

function del(id)
{
	if (!confirm("确认删除此角色信息？"))
		return false;

	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = {};
	ajax.data.Id = id;
	ajax.data.Action = "Delete";
	ajax.data.ajax = new Date().getTime();
	ajax.success = function (result) {

		if (result == "ok") {
			changePage(1);
			return true;
		}
		else {
			alert(result);
			return false;
		}
	};
	ajax.error = function (error) {
		alert(error.responseText);
	};
	$.ajax(ajax);
}

function clear() {

	$("#hId").val(0);
	$("#txtName").val("");
	$("#txtNote").val("");
	$("#selFlag").val("");
}

function load(json) {

	if (!json)
		return false;

	$("#hId").val(json.Id);
	$("#txtName").val(json.Name);
	$("#txtNote").val(json.Note);
	$("#selFlag").val(json.Flag);
}

function onSave() {

	var data = {};
	data.Id = $("#hId").val();
	data.Name = $("#txtName").val();
	if (!data.Name || data.Name.length > 10) {
		alert("角色名称不可为空，且最大长度不可超过20个字节（1个汉字等于2个字节）");
		$("#txtName").focus();
		return false;
	}
	data.Note = $("#txtNote").val();
	data.Flag = $("#selFlag").val();

	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = data;
	ajax.data.Action = "Save";
	ajax.data.ajax = new Date().getTime();
	ajax.success = function (result) {

		if (result == "ok") {
			changePage(1);
			$("#divInfo").dialog("close")
		}
		else
			alert(result);
	};
	ajax.error = function (error) {
		alert(error.responseText);
	};
	$.ajax(ajax);
}
