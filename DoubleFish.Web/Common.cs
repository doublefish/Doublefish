using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Web;

using DoubleFish.Model;
using DoubleFish.BLL;

namespace DoubleFish.Web
{
	public static class Common
	{
		/// <summary>
		/// 向客户端输出错误。
		/// </summary>
		/// <param name="context"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public static int WriteError (this HttpContext context, Exception error)
		{
			context.Response.StatusDescription = error.Message;
			context.Response.Write(context.Response.StatusDescription);
			return context.Response.StatusCode;
		}

		/// <summary>
		/// 向客户端输出错误。
		/// </summary>
		/// <param name="context"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public static int WriteError (this HttpContext context, string error)
		{
			context.Response.StatusDescription = error;
			context.Response.Write(context.Response.StatusDescription);
			return context.Response.StatusCode;
		}

		/// <summary>
		/// 获取当前登录用户信息
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static LoginUser GetLoginInfo (this HttpContext context)
		{
        
			if (context == null)
				context = HttpContext.Current;

			var ui = context.Items[typeof(LoginUser).AssemblyQualifiedName] as LoginUser;//取缓存用户
			if (ui != null)
				return ui;

			var session = context.Session;
			if (session == null)
				return null;

			ui = session["LoginUser"] as LoginUser;
			context.Items[typeof(LoginUser).AssemblyQualifiedName] = ui;//缓存用户
			return ui;
		}
	}
}
