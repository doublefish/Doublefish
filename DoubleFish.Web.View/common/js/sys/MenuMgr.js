/*
 * for page /Sys/MenuMgr.aspx
 */

var setting = {
	view: {
		selectedMulti: false
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
	},
	callback: {
		onClick: onClick,
		onRightClick: onRightClick
	}
};

function onPageLoad() {
	initTree();
	getByParent(0);
	$("#txtNote").val("");
}

function initTree() {
	$.fn.zTree.init($("#menu"), setting, null);
}

function getTree() {
	var zTree = $.fn.zTree.getZTreeObj("menu");
	return zTree;
}

function loadNode(node) {

	$("#hId").val(node.Id);
	$("#txtName").val(node.Name);
	$("#hParent").val(node.Parent);
	$("#txtParent").val(node.ParentName);
	$("#selType").val(node.Type);
	$("#txtUrl").val(node.Url);
	$("#selFlag").val(node.Flag);
	$("#txtNote").val(node.Note);
}

function onClick(event, treeId, treeNode, clickFlag) {
	getByParent(treeNode);
	treeNode.ParentName = treeNode.level > 0 ? treeNode.getParentNode().Name : "";
	loadNode(treeNode);
}

function onRightClick(event, treeId, treeNode) {
	if (!treeNode)
		return;
	if (treeNode.Flag > 0)
		$("#m_able").text("禁用");
	else
		$("#m_able").text("启用");

	var zTree = $.fn.zTree.getZTreeObj("menu");

	if (!treeNode && event.target.tagName.toLowerCase() != "button" && $(event.target).parents("a").length == 0) {
		zTree.cancelSelectedNode();
		showRMenu("root", event.clientX, event.clientY);
	} else if (treeNode && !treeNode.noR) {
		zTree.selectNode(treeNode);
		showRMenu("node", event.clientX, event.clientY);
	}
}

function showRMenu(type, x, y) {
	$("#rMenu ul").show();
	if (type == "root") {
		//$("#m_add_child").hide();
		//$("#m_add_brother").hide();
		//$("#m_edit").hide();
		$("#m_del").hide();
	} else {
		$("#m_add_child").show();
		$("#m_add_brother").show();
		$("#m_edit").show();
		$("#m_del").show();
	}
	$("#rMenu").css({ "top": y + "px", "left": x + "px", "visibility": "visible" });

	$("body").bind("mousedown", onBodyMouseDown);
}
function hideRMenu() {
	if ($("#rMenu")) $("#rMenu").css({ "visibility": "hidden" });
	$("body").unbind("mousedown", onBodyMouseDown);
}

function onBodyMouseDown(event) {
	if (!(event.target.id == "rMenu" || $(event.target).parents("#rMenu").length > 0)) {
		$("#rMenu").css({ "visibility": "hidden" });
	}
}

function addChildNode() {
	hideRMenu();
	var nodes = getTree().getSelectedNodes();
	if (!nodes || nodes.length == 0)
		return;

	var node = { Id: 0, Name: "", Parent: nodes[0].Id, ParentName: nodes[0].Name };
	loadNode(node);
}

function addBrotherNode() {
	hideRMenu();
	var nodes = getTree().getSelectedNodes();
	if (!nodes || nodes.length == 0)
		return;

	var node = { Id: 0, Name: "", Parent: nodes[0].Parent, ParentName: nodes[0].level > 0 ? nodes[0].getParentNode().Name : "" };
	loadNode(node);
}

function onReset() {

	//$("#hId").val(0);
	$("#txtName").val("");
	//$("#txtParent").val(0);
	//$("#hParent").val(0);
	$("#selType").val(0);
	$("#txtUrl").val("");
	$("#selFlag").val(1);
	$("#txtNote").val("");
}

function getByParent(treeNode) {

	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = {};
	ajax.data.action = "getByParent";
	ajax.data.Parent = treeNode.Id;
	ajax.data.ajax = new Date().getTime();
	ajax.success = function (result) {

		var nodes = eval("(" + result + ")");
		
		if (treeNode.Id == 0) {
			initTree();
			getTree().addNodes(null, nodes);
			return;
		}
		var zTree = getTree();
		if (treeNode)
			zTree.removeChildNodes(treeNode);
		if (nodes.length > 0)
			zTree.addNodes(treeNode, nodes);

	};
	ajax.error = function (error) {
		alert(error.responseText);
	};
	$.ajax(ajax);
}

function ableNode() {
	hideRMenu();
	var nodes = getTree().getSelectedNodes();
	if (!nodes || nodes.length == 0)
		return;
	if (nodes[0].Flag > 0 && nodes[0].children && nodes[0].children.length > 0) {
		var msg = "要禁用的节点是父节点，如果禁用将连同子节点一禁用掉。\n\n请确认！";
		if (confirm(msg) == true) {
			//zTree.removeNode(nodes[0]);
		}
	}
}

function delNode() {
	hideRMenu();
	var zTree = getTree();
	var nodes = zTree.getSelectedNodes();
	if (!nodes || nodes.length == 0)
		return;
	var msg = "";
	if (nodes[0].children && nodes[0].children.length > 0) {
		msg = "删除的节点存在子节点，如果要删除请先将其所有子节点删除！";
	} else {
		msg = "确认删除节点：" + nodes[0].Name + "！";
	}
	if (confirm(msg)) {
		zTree.removeNode(nodes[0]);
	}
}

function onSave() {
	var ajax = {};
	ajax.type = "POST";
	ajax.url = window.location.href;
	ajax.data = {};
	ajax.data.action = "save";
	ajax.data.Id = $("#hId").val();
	ajax.data.Name = $("#txtName").val();
	ajax.data.Parent = $("#hParent").val();
	ajax.data.Type = $("#selType").val();
	ajax.data.Url = $("#txtUrl").val();
	ajax.data.Flag = $("#selFlag").val();
	ajax.data.Note = $("#txtNote").val();
	ajax.data.ajax = new Date().getTime();
	ajax.success = function (result) {

		var rs = eval("(" + result + ")");
		if (rs && rs.Nodes) {
			var zTree = getTree();
			var selectedNode = getTree().getSelectedNodes()[0];
			if (selectedNode.Id == rs.Parent) {
				zTree.removeChildNodes(selectedNode);
				zTree.addNodes(selectedNode, rs.Nodes);
			}
			else if (selectedNode.level == 0) {
				initTree();
				zTree.addNodes(null, rs.Nodes);
			}
			else if (selectedNode.Parent == rs.Parent) {
				selectedNode = selectedNode.getParentNode();
				zTree.removeChildNodes(selectedNode);
				zTree.addNodes(selectedNode, rs.Nodes);
			}
		}
	};
	ajax.error = function (error) {
		alert(error.responseText);
	};
	$.ajax(ajax);
}
