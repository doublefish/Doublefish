

function onPageLoad() {

	loadMenu();
}

function loadMenu() {

	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = {};
	ajax.data.Action = "GetMenu";
	ajax.data.ajax = new Date().getTime();
	ajax.success = function (result) {

		var json = eval("(" + result + ")");
		initMenu(json);
	};
	ajax.error = function (error) {
		alert(error.responseText);
	}
	$.ajax(ajax);
}

var JsonMenu = [];

function initMenu(json) {

	JsonMenu = json;

	$("#divMenu").html('<ul id="menu_ul_3"></ul>');
	loadMenuByParent(3);

	$("ul:eq(1)").show();
}

function loadMenuByParent(parent) {

	var json = JsonMenu;

	if (!json)
		return;

	for (var i = 0; i < json.length; i++) {

		var menu = json[i];

		if (!menu)
			continue;

		if (menu.Parent != parent)
			continue;

		if (!menu || !menu.Id || !menu.Name)
			continue;

		var html = '';
		if (menu.Type == 1) {
			html += '<li>';
			html += '<span id="" onclick="selectMenu(' + menu.Id + ')"><a href="#">' + menu.Name + '</a></span>';
			html += '<ul class="menu_list" id="menu_ul_' + menu.Id + '" style="display: none;"></ul>';
			html += '</li>';
		}
		else {
			html += '<li><a href="' + menu.Url + '" target="frame_right">' + menu.Name + '</a></li>';
		}

		$("#menu_ul_" + menu.Parent + "").append(html);

		loadMenuByParent(menu.Id);
	}
}
//菜单变换
function selectMenu(menu) {

	if ($("#menu_ul_" + menu).css("display") == "none")
		$("#menu_ul_" + menu).show();
	else
		$("#menu_ul_" + menu).hide();
}
