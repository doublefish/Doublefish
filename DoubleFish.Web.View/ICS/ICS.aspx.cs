using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DoubleFish.Web.View.ICS
{
	public partial class _ICS : System.Web.UI.Page
	{
		public override void ProcessRequest (HttpContext context)
		{
			if (context.Request.Form.Count > 0)
			{
				context.Response.Write("");
				return;
			}
			base.ProcessRequest(context);
		}

		protected void Page_Load (object sender, EventArgs e)
		{
			var url = this.Context.Request.QueryString["url"];
			if (string.IsNullOrEmpty(url))
				return;

			this.Context.Response.Write(this.InformationCollection(this.Context, url));
		}

		//获取页面的html源码
		public string GetHtmlSource (string url, string charset)
		{
			if (charset == "" || charset == null) charset = "utf-8";
			string text = "";
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				Stream stream = response.GetResponseStream();
				StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(charset));
				text = reader.ReadToEnd();
				stream.Close();
				response.Close();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return text;
		}

		//截取字符串
		public string SniffwebCode (string code, string wordsBegin, string wordsEnd)
		{
			string NewsTitle = "";
			Regex regex = new Regex("" + wordsBegin + @"(?<title>[\s\S]+?)" + wordsEnd + "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			for (Match match = regex.Match(code); match.Success; match = match.NextMatch())
			{
				NewsTitle = match.Groups["title"].ToString();
			}
			return NewsTitle;

		}

		//截取网址
		public ArrayList SniffwebCodeReturnList (string code, string wordsBegin, string wordsEnd)
		{
			ArrayList urlList = new ArrayList();
			//string NewsTitle = "";
			Regex regex = new Regex("" + wordsBegin + @"(?<title>[\s\S]+?)" + wordsEnd + "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			for (Match match = regex.Match(code); match.Success; match = match.NextMatch())
			{
				urlList.Add(match.Groups["title"].ToString());
			}
			return urlList;

		}

		public static string GetSource (string url, string charset)
		{
			if (charset == "" || charset == null) charset = "gb2312";
			string text = "";
			try
			{
				Stream stream = new WebClient().OpenRead(url);
				text = new StreamReader(stream, Encoding.GetEncoding(charset)).ReadToEnd();
				stream.Close();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return text;
		}

		//public string GetHtmlSource (string url, string charset)
		//{
		//	if (charset == "" || charset == null) charset = "utf-8";
		//	string text = "";
		//	try
		//	{
		//		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
		//		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		//		Stream stream = response.GetResponseStream();
		//		StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(charset));
		//		text = reader.ReadToEnd();
		//		stream.Close();
		//		response.Close();
		//	}
		//	catch (Exception ex)
		//	{
		//	}
		//	return text;
		//}

		public string Get_Http (string a_strUrl, int timeout)
		{
			string strResult;
			try
			{
				HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(a_strUrl);
				myReq.Timeout = timeout;
				HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
				Stream myStream = HttpWResp.GetResponseStream();
				StreamReader sr = new StreamReader(myStream, Encoding.Default);
				StringBuilder strBuilder = new StringBuilder();
				while (-1 != sr.Peek())
				{
					strBuilder.Append(sr.ReadLine() + "\r\n");
				}
				strResult = strBuilder.ToString();
			}
			catch (Exception exp)
			{
				strResult = "错误：" + exp.Message;
			}
			return strResult;
		}
		
		//获取页面内容后，分析页面中连接地址取到要抓取的url：
		//处理页面标题和链接
		//public string SniffwebCode (string code, string wordsBegin, string wordsEnd)
		//{
		//	string NewsTitle = "";
		//	Regex regex = new Regex("" + wordsBegin + @"(?<title>[\s\S]+?)" + wordsEnd + "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		//	for (Match match = regex.Match(code); match.Success; match = match.NextMatch())
		//	{
		//		NewsTitle = match.Groups["title"].ToString();
		//	}
		//	return NewsTitle;
		//}


		//public ArrayList SniffwebCodeReturnList (string code, string wordsBegin, string wordsEnd)
		//{
		//	ArrayList urlList = new ArrayList();
		//	//string NewsTitle = "";
		//	Regex regex = new Regex("" + wordsBegin + @"(?<title>[\s\S]+?)" + wordsEnd + "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		//	for (Match match = regex.Match(code); match.Success; match = match.NextMatch())
		//	{
		//		urlList.Add(match.Groups["title"].ToString());
		//	}
		//	return urlList;
		//}


		public string InformationCollection (HttpContext context, string url)
		{

			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			WebResponse webResponse = httpWebRequest.GetResponse();
			StreamReader streamReader = new StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.Default);
			string allCode = streamReader.ReadToEnd();
			streamReader.Close();

			string p = @".+";
			Regex regex = new Regex(p, RegexOptions.IgnoreCase);
			MatchCollection collection = regex.Matches(allCode);
			for (int i = 0; i < collection.Count; i++)
			{

			}

			return "";
		}
	}
}