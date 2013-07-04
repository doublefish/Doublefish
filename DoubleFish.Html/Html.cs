using System;
using System.Collections.Generic;

using mshtml;

namespace DoubleFish.Html
{
	public class Html
	{
		/// <summary>
		/// 利用mshtml进行分析
		/// </summary>
		/// <param name="html"></param>
		/// <param name="tag"></param>
		public string ParserHtml (string html, string tag)
		{
			// 首先html代码內容存入HTMLDocumentClass
			IHTMLDocument2 document = new HTMLDocumentClass();
			document.write(new object[] { html });
			document.close();

			// 属性body则是html中的body tag
			// 而body本身就是一个IHTMLElement
			// 所以可以用all这个属性将所有元素取出成为一个collection
			IHTMLElementCollection body = (IHTMLElementCollection)document.body.all;

			// 可以用tags这个方法过滤出我们所需要的tag
			IHTMLElementCollection elements = (IHTMLElementCollection)body.tags("TABLE");

			for (int i = 0; i < elements.length; i++)
			{
				IHTMLElement tr = (IHTMLElement)elements.item(i, null);

				

				//result += element.innerHTML;
			}

			string result = "";

			for (int i = 0; i < elements.length; i++)
			{
				// 使用item这个方法可以将集合中的元素取出
				// 第一个参数代表的是顺序，但是在msdn中表示为name
				// 第二个参数msdn中表示为index,但经过测试后,指的并不是顺序,所以目前无法确定它的用途
				// 如果有知道的朋友，也请跟我说一下
				IHTMLElement element = (IHTMLElement)elements.item(i, null);

				if (string.IsNullOrEmpty(element.innerHTML))
					continue;

				result += element.innerHTML;
			}

			return result;
		}

		public IHTMLDocument2 ConverToTable (string html)
		{

			IHTMLDocument2 doc = new HTMLDocumentClass();
			doc.write(new object[] { html });
			doc.close();
			var title = doc.title;
			var body = doc.body.innerText;

			return doc;
		}

	}
}
