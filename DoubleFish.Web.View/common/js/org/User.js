
var PageSize = 20;

function onPageLoad() {

	changePage(1);
}

function loadList(json) {

	window["JsonLst"] = json;
	$("#tbody").empty();
	$('#divPager').empty();
	if (!json)
		return;

	var list = json.Results;
	var totalCount = parseInt(json.ResultCount);
	var pageSize = parseInt(json.PageSize);
	var pageIndex = parseInt(json.PageIndex);

	var html = '';

	for (var i = 0; i < list.length; i++) {

		var index = (pageIndex - 1) * pageSize + i + 1;
		var sex = "";
		switch (list[i].Sex) { case 1: sex = "男"; break; case 2: sex = "女"; break; default: sex = ""; break; }
		html += '<tr>';
		html += '<td>' + index + '</td>';
		html += '<td>' + list[i].Name + '</td>';
		html += '<td>' + list[i].FullName + '</td>';
		html += '<td>' + sex + '</td>';
		html += '<td>' + list[i].Birthday + '</td>';
		html += '<td>' + list[i].Tel + '</td>';
		html += '<td>' + list[i].Mobile + '</td>';
		html += '<td>' + list[i].Mail + '</td>';
		html += '<td>' + list[i].Region + '</td>';
		html += '<td>' + list[i].Status + '</td>';
		html += '<td>' + list[i].RegistTime + '</td>';
		html += '</tr>';
	}
	$('#tbody').html(html);
	$('#divPager').pager({

		pageIndex: pageIndex,
		pageSize: pageSize,
		totalCount: totalCount,
		prevText: '上一页',
		nextText: '下一页',
		homeText: '首页',
		lastText: '尾页',
		callBack: changePage
	});
}

function changePage(page, jq) {

	var ajax = {};
	ajax.type = "POST";
	ajax.url = "UserMgr.aspx";
	ajax.data = {};
	ajax.data.Action = "page";
	ajax.data.PageIndex = page;
	ajax.data.PageSize = PageSize;
	ajax.data.Name = $("#sel_name").val();
	ajax.data.FullName = $("#sel_fullname").val();
	ajax.data.Sex = $('input[name=sel_sex]:checked').val();
	ajax.data.ajax = new Date().getTime();
	ajax.success = function (result) {

		var json = eval("(" + result + ")");
		loadList(json);
	};
	ajax.error = function (error) {
		
	};
	$.ajax(ajax);
}

function search() { changePage(1); }

