using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI;
using System.IO;
using System.Text.RegularExpressions;

namespace DoubleFish.Web
{
	public class MpBase : System.Web.UI.MasterPage
	{
		protected override void Render (HtmlTextWriter writer)
		{
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
			base.Render(htmlWriter);
			string html = stringWriter.ToString();


			#region 转换相对路径
			MatchCollection collection = Regex.Matches(html, "<(a|link|img|script|input|form).[^>]*(href|src|action)=(\\\"|'|)(.[^\\\"']*)(\\\"|'|)[^>]*>", RegexOptions.IgnoreCase);

			foreach (Match match in collection)
			{
				if (match.Groups[match.Groups.Count - 2].Value.IndexOf("~") != -1)
				{
					string url = this.Page.ResolveUrl(match.Groups[match.Groups.Count - 2].Value);
					html = html.Replace(match.Groups[match.Groups.Count - 2].Value, url);
				}
			}
			#endregion
			writer.Write(html);
		} 

	}
}
