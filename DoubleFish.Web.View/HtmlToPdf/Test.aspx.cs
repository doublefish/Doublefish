using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using mshtml;
using System.Net;
using System.IO;

namespace DoubleFish.Web.View.Test
{
	public partial class _Test : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			string szTestURL_ = @"http://localhost:1989/test1.aspx";

			string szHtmlContent_ = this.DownloadWeb(szTestURL_);

			this.ParserHtml(szHtmlContent_, null);

			//new DoubleFish.Html.Html().ParserHtml(szHtmlContent_, "");

			//string szAfterParser_ = this.ParserHtml(szHtmlContent_, "TD");
		}

		/// <summary>
		/// 利用mshtml进行分析
		/// </summary>
		/// <param name="html"></param>
		/// <param name="tag"></param>
		public List<string>[] ParserHtml (string html, string tag)
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
			IHTMLElementCollection elements = (IHTMLElementCollection)body.tags("TD");

			if (elements.length < 1)
				return null;

			int rowCount = ((IHTMLElementCollection)body.tags("TR")).length;

			var columnCount = elements.length / rowCount;

			List<string>[] list = new List<string>[rowCount];

			for (int i = 0; i < elements.length; i++)
			{
				IHTMLElement element = (IHTMLElement)elements.item(i, null);

				if (i % rowCount == 0)
				{
					list[i / columnCount] = new List<string>();
				}

				//list[i / columnCount][i % rowCount] = element.innerHTML;
				list[i / columnCount].Add(element.innerHTML);
			}

			return list;

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

			//return result;
		}

		/// <summary>
		/// 利用mshtml進行分析
		/// </summary>
		/// <param name="szHtmlContent"></param>
		/// <param name="szFilterTag"></param>
		//private string ParserHtml (string szHtmlContent, string szFilterTag)
		//{
		//    // 首先將網頁內容存入HTMLDocumentClass
		//    IHTMLDocument2 docHtml_ = new HTMLDocumentClass();
		//    docHtml_.write(new object[] { szHtmlContent });
		//    docHtml_.close();


		//    // 屬性body則是html中的body tag
		//    // 而body本身就是一個IHTMLElement
		//    // 所以可以用all這個屬性將所有元素取出成為一個collection
		//    IHTMLElementCollection col_1_ = (IHTMLElementCollection)docHtml_.body.all;


		//    // 可以用tags這個方法過濾出我們所需要的tag
		//    IHTMLElementCollection col_2_ = (IHTMLElementCollection)col_1_.tags(szFilterTag);


		//    string szResult_ = "";
		//    IHTMLElement elem_ = null;


		//    // length這個屬性可以得到目前的集合中的元素數量
		//    int iCollectionLength = col_2_.length;


		//    for (int i = 0; i < iCollectionLength; i++)
		//    {
		//        // 使用item這個方法可以將集合中的元素取出
		//        // 第一個參數代表的是順序,但是在msdn中標示為name
		//        // 第二個參數msdn中標示為index,但經過測試後,指的並不是順序,所以目前無法確定他的用途
		//        // 如果有知道的朋友,也請跟我說一下
		//        elem_ = (IHTMLElement)col_2_.item(i, null);


		//        if (elem_.innerHTML == null)
		//            continue;

		//        szResult_ += elem_.innerHTML;
		//    }

		//    return szResult_;
		//}

		/// <summary>
		/// 下載網頁的方式
		/// 網路上很多範例
		/// 我在這邊就不綴訴
		/// </summary>
		/// <param name="szURL"></param>
		/// <returns></returns>
		private string DownloadWeb (string szURL)
		{
			HttpWebRequest reqHttp_ = (HttpWebRequest)WebRequest.Create(szURL);
			reqHttp_.Timeout = 30000;

			HttpWebResponse respHttp_ = (HttpWebResponse)reqHttp_.GetResponse();

			StreamReader readerHtml = new StreamReader(respHttp_.GetResponseStream());

			string szResult_ = readerHtml.ReadToEnd();

			readerHtml.Close();

			return szResult_;
		}
	}
}