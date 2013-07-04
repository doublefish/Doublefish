
/*
$('#divPager').pager({
pageIndex: pageIndex,//当前页
pageSize: pageSize,//页码
totalCount: totalCount,//记录总数
callBack: changePage//翻页事件
});
*/

$.fn.pager = function (options) {

	var settings = {
		pageIndex: 1, //当前页
		pageSize: 10, //页码
		pageCount: 0, //总页数
		totalCount: 0, //记录总数
		viewCount: 10,
		prevText: '<<',
		nextText: '>>',
		homeText: 'First',
		lastText: 'Last',
		showHomeLast: true,
		linkTo: "#",
		callBack: function () { return false; },
		goPage: true,
		goText: 'Go'
	}

	if (options) $.extend(settings, options || {});

	var panel = $(this);
	panel.empty();

	settings.pageCount = parseInt(settings.totalCount / settings.pageSize);
	if (settings.totalCount % settings.pageSize > 0)
		settings.pageCount++;

	//总页数为0，则清空
	if (settings.pageCount < 1) return;

	if (settings.viewCount < 1) settings.viewCount = 10;

	if (settings.pageIndex > 1) {

		if (settings.showHomeLast) appendItem(1, { text: settings.homeText }); /*首页*/
		appendItem(settings.pageIndex - 1, { text: settings.prevText }); /*上一页*/
	}

	var halfView = settings.viewCount / 2;
	
	if (settings.pageIndex < halfView + 1) {

		appendItems(1, settings.viewCount);
	}
	else if (settings.pageCount - settings.pageIndex < halfView + 1) {

		appendItems(settings.pageCount - settings.viewCount + 1, settings.pageCount);
	}
	else {

		appendItems(settings.pageIndex - halfView + 1, settings.pageIndex + halfView);
	}

	if (settings.pageIndex < settings.pageCount) {

		appendItem(settings.pageIndex + 1, { text: settings.nextText }); /*下一页*/
		if (settings.showHomeLast) appendItem(settings.pageCount, { text: settings.lastText }); /*尾页*/
	}

	if (settings.goPage) {

		var btn = jQuery('<input type="text" value="' + settings.pageIndex + '" />');
		panel.append(btn);
		var lnk = jQuery('<a>' + settings.goText + '</a>')
						.bind("click", function () { settings.callBack(panel.find('input').val()); })
						.attr('href', settings.linkTo.replace(/__id__/, settings.goText));
		panel.append(lnk);
	}

	return false;

	function appendItems(start, end) {

		start = parseInt(start);
		end = parseInt(end);
		if (start < 1) start = 1;
		if (end > settings.pageCount) end = settings.pageCount;
		for (var i = start; i <= end; i++) {

			appendItem(i);
		}
	}

	function appendItem(page_id, appendopts) {

		appendopts = jQuery.extend({ text: page_id, classes: "" }, appendopts || {});

		var lnk;

		if (page_id == settings.pageIndex) {
			lnk = jQuery('<span class="current">' + (appendopts.text) + '</span>');
		}
		else {
			lnk = jQuery('<a>' + (appendopts.text) + '</a>')
						.bind('click', function () { settings.callBack(page_id); })
						.attr('href', settings.linkTo.replace(/__id__/, page_id));
		}
		if (appendopts.classes) { lnk.addClass(appendopts.classes); }

		panel.append(lnk);
	}
}