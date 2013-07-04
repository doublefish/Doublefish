using System;
using System.Collections.Generic;

namespace DoubleFish.File
{
	public class Excel
	{
		#region [属性]

		private Dictionary<string, string> _Css;
		private string _FileName;
		private string _DocumentTitle;
		private int[] _ColumnsWidth;
		private string _Body;
		private PageSetup _PageSetup;

		/// <summary>
		/// 样式
		/// </summary>
		public Dictionary<string, string> Css
		{
			set
			{
				this._Css = value;
			}
			get
			{
				return this._Css;
			}
		}

		/// <summary>
		/// 文件名
		/// </summary>
		public string FileName
		{
			set
			{
				this._FileName = value;
			}
			get
			{
				return this._FileName;
			}
		}

		/// <summary>
		/// 标题
		/// </summary>
		public string DocumentTitle
		{
			set
			{
				this._DocumentTitle = value;
			}
			get
			{
				return this._DocumentTitle;
			}
		}

		/// <summary>
		/// 列宽
		/// </summary>
		public int[] ColumnsWidth
		{
			set
			{
				this._ColumnsWidth = value;
			}
			get
			{
				return this._ColumnsWidth;
			}
		}

		/// <summary>
		/// 内容
		/// </summary>
		public string Body
		{
			set
			{
				this._Body = value;
			}
			get
			{
				return this._Body;
			}
		}

		/// <summary>
		/// 打印页面设置
		/// </summary>
		public PageSetup PageSetup
		{
			set
			{
				this._PageSetup = value;
			}
			get
			{
				return this._PageSetup;
			}
		}

		#endregion

		public Excel ()
		{
			this._Css = new Dictionary<string, string>();
			this._PageSetup = new PageSetup();
		}

		public void Export ()
		{
			if (string.IsNullOrEmpty(this.FileName))
				this.FileName = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

			string code = @"
<html xmlns:o='urn:schemas-microsoft-com:office:office'
xmlns:x='urn:schemas-microsoft-com:office:excel'
xmlns='http://www.w3.org/TR/REC-html40'>
<head>
<meta http-equiv=Content-Type content='text/html; charset=utf-8'>
<meta name=ProgId content=Excel.Sheet>
<meta name=Generator content='Microsoft Excel 11'>
<link rel=File-List href='" + this.FileName + @".files/filelist.xml'>
<link rel=Edit-Time-Data href='" + this.FileName + @".files/editdata.mso'>
<link rel=OLE-Object-Data href='" + this.FileName + @".files/oledata.mso'>
<xml>
 <o:DocumentProperties>
  <o:LastAuthor>DoubleFish</o:LastAuthor>
  <o:LastSaved>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"</o:LastSaved>
  <o:Version>1.0</o:Version>
 </o:DocumentProperties>
</xml>
<style type='text/css'>
@page
	{margin:.98in .35in .98in .35in;
	mso-header-margin:.51in;
	mso-footer-margin:.51in;
	" + (this.PageSetup.CenterFooter ? "mso-footer-data:'第 &P 页，共 &N 页';" : string.Empty) + @"
	" + (this.PageSetup.PrintHorizontal ? "mso-page-orientation:landscape;" : string.Empty) + @"}
tr
	{mso-height-source:auto;
	mso-ruby-visibility:none;}
col
	{mso-width-source:auto;
	mso-ruby-visibility:none;}
br
	{mso-data-placement:same-cell;}
.style0
	{
	font-size:10.0pt;
}
td
	{mso-style-parent:style0;
	color:windowtext;
	font-size:10.0pt;
	font-style:normal;
	text-decoration:none;
	font-family:宋体;
	vertical-align:middle;
	border:none;
	white-space:normal;}";
			foreach (string key in this.Css.Keys)
			{
				code += "\r\n." + key + "{" + this.Css[key] + "}";
			}
			code += @"
</style>
<xml>
 <x:ExcelWorkbook>
  <x:ExcelWorksheets>
   <x:ExcelWorksheet>
    <x:Name>" + this.FileName + @"</x:Name>
    <x:WorksheetOptions>
     <x:DefaultRowHeight>240</x:DefaultRowHeight>
     <x:Print>
      <x:ValidPrinterInfo/>
      <x:PaperSizeIndex>9</x:PaperSizeIndex>
      <x:HorizontalResolution>600</x:HorizontalResolution>
      <x:VerticalResolution>600</x:VerticalResolution>
     </x:Print>
     <x:Selected/>
     <x:DoNotDisplayGridlines/>
     <x:Panes>
      <x:Pane>
       <x:Number>3</x:Number>
       <x:ActiveRow>5</x:ActiveRow>
       <x:RangeSelection>$6:$6</x:RangeSelection>
      </x:Pane>
     </x:Panes>
     <x:ProtectContents>False</x:ProtectContents>
     <x:ProtectObjects>False</x:ProtectObjects>
     <x:ProtectScenarios>False</x:ProtectScenarios>
    </x:WorksheetOptions>
   </x:ExcelWorksheet>
  </x:ExcelWorksheets>
  <x:WindowHeight>10695</x:WindowHeight>
  <x:WindowWidth>21435</x:WindowWidth>
  <x:WindowTopX>0</x:WindowTopX>
  <x:WindowTopY>105</x:WindowTopY>
  <x:ProtectStructure>False</x:ProtectStructure>
  <x:ProtectWindows>False</x:ProtectWindows>
 </x:ExcelWorkbook>
</xml>
</head>

<body>
<table border=0 cellpadding=0 cellspacing=0 style='white-space:normal;'>
";
			if (this.ColumnsWidth != null)
			{
				for (var i = 0; i < this.ColumnsWidth.Length; i++)
				{
					code += "\r\n<col style='width:" + this.ColumnsWidth[i] + "px;mso-width-source:userset;'>";
				}
			}
			code += this.Body;
			code += @"
</table>
</body>
</html>
";
			System.Web.HttpContext context = System.Web.HttpContext.Current;

			context.Response.Charset = "UTF-8";

			context.Response.ContentEncoding = System.Text.Encoding.UTF8;
			// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
			context.Response.AppendHeader("Content-Disposition", "attachment;filename=" + context.Server.UrlEncode(this.FileName) + ".xls");
			// 添加头信息，指定文件大小，让浏览器能够显示下载进度
			//context.Response.AddHeader("Content-Length", table.Length.ToString());
			// 指定返回的是一个不能被客户端读取的流，必须被下载
			context.Response.ContentType = "application/ms-excel";

			context.Response.Write(code);
			context.Response.End();

		}
	}
	/// <summary>
	/// 打印页面设置
	/// </summary>
	public class PageSetup
	{
		#region [属性]

		private bool _CenterHorizontally;
		private bool _CenterVertically;
		private bool _PrintHorizontal;
		private bool _CenterFooter;

		/// <summary>
		/// 水平居中（默认为true）
		/// </summary>
		public bool CenterHorizontally
		{
			set
			{
				this._CenterHorizontally = value;
			}
			get
			{
				return this._CenterHorizontally;
			}
		}

		/// <summary>
		/// 垂直居中（默认为false）
		/// </summary>
		public bool CenterVertically
		{
			set
			{
				this._CenterVertically = value;
			}
			get
			{
				return this._CenterVertically;
			}
		}

		/// <summary>
		/// 横版打印（默认为false）
		/// </summary>
		public bool PrintHorizontal
		{
			set
			{
				this._PrintHorizontal = value;
			}
			get
			{
				return this._PrintHorizontal;
			}
		}

		/// <summary>
		/// 中间页脚页码（默认为true）
		/// </summary>
		public bool CenterFooter
		{
			set
			{
				this._CenterFooter = value;
			}
			get
			{
				return this._CenterFooter;
			}
		}
		#endregion

		public PageSetup ()
		{
			this._CenterHorizontally = true;
			this._CenterVertically = false;
			this._PrintHorizontal = false;
			this._CenterFooter = true;
		}
	}
}
