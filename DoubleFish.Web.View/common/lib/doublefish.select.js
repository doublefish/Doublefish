/*
* 生成下拉菜单HTML
* dataSource      : 数据源
* keyValue        : 下拉菜单value属性在数据源中对应的键
* keyOption       : 下拉菜单显示文本在数据源中对应的键
* selectedValue   : 将下拉菜单中value属性为selectedValue的项设为选中
* selectedOption  : 将下拉菜单中显示文本为selectedOption的项设为选中
* defaultValue    : 默认首选项的value属性的值
* defaultOption   : 默认首选项的显示文本
* selectId, selectName  设定此属性则表示要生成整个下拉菜单代码
* by doublefish
*/


function getSelectHtml(dataSource, keyValue, keyOption, selectedValue, selectedOption, defaultValue, defaultOption, selectId, selectName)
{
	this.options = get_select_html(dataSource, keyValue, keyOption, selectedValue, selectedOption, defaultValue, defaultOption);
	if (!selectId)
		return this.options;

	this.html = '<select id="' + selectId + '" name="' + selectName + '">' + this.options + '</select>';

	return this.html;
}

function getOption(value, option, isSelected)
{
	return get_select_option(value, option, isSelected)
}

/*
* 以上两个方法供调用，方法名冲突时，可修改方法名
*/

function get_select_html(dataSource, keyValue, keyOption, selectedValue, selectedOption, defaultValue, defaultOption)
{
	return get_select_options(dataSource, keyValue, keyOption, selectedValue, selectedOption, defaultValue, defaultOption);
}

function get_select_options(dataSource, keyValue, keyOption, selectedValue, selectedOption, defaultValue, defaultOption)
{
	var html = "";

	if (!defaultValue)
		defaultValue = 0;
	if (!defaultOption)
		defaultOption = "请选择";

	var defaultKey = null; //以value还是option显示的文本判断默认选中项
	var defaultVal = null; //用来判断默认选中项的值

	if (selectedValue)//设置指定value为默认选中项
	{
		defaultKey = keyValue; //以value判断为默认选中项
		defaultVal = selectedValue;
		html = get_select_option(defaultValue, defaultOption, false);
	}
	else if (selectedOption)//设置指定的option显示的文本为默认选中项
	{
		defaultKey = keyOption; //以option显示的文本判断默认选中项
		defaultVal = selectedOption;
		html = get_select_option(defaultValue, defaultOption, false);
	}
	else
	{
		defaultKey = keyValue;
		defaultVal = defaultValue;
		html = get_select_option(defaultValue, defaultOption, true);
	}

	if (!dataSource || dataSource.length == 0)
		return html;

	for (var i = 0; i < dataSource.length; i++)
	{
		if (!dataSource[i][keyOption])
			continue;

		if (dataSource[i][defaultKey] == defaultVal)
			html += get_select_option(dataSource[i][keyValue], dataSource[i][keyOption], true);
		else
			html += get_select_option(dataSource[i][keyValue], dataSource[i][keyOption], "");
	}

	return html;
}

function get_select_option(value, option, isSelected)
{
	if (isSelected)
		return '<option value="' + value + '" selected="selected">' + option + '</option>';
	else
		return '<option value="' + value + '">' + option + '</option>';
}

