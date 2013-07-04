using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

using DoubleFish.Model;

namespace DoubleFish.DAL
{
	public class DataBase : MarshalByRefObject
	{
		class Db : OrmDataContext
		{
			public Db () : base(DataBase._ConnectionString) { }
			public Db (string conString) : base(conString) { }
		}

		private static string _ConnectionString = null;

		protected DataBase () { }

		/// <summary>
		/// 获取可读写数据库操作代理实例。
		/// </summary>
		/// <returns>可读写数据库操作代理实例。</returns>
		protected OrmDataContext GetDatabase ()
		{
			if (DataBase._ConnectionString == null && string.IsNullOrEmpty(DataBase._ConnectionString))//短路设计，后面条件只判断一次。
			{
				var cs = ConfigurationManager.ConnectionStrings["doublefish:" + Environment.MachineName];
				if (cs == null)
					cs = ConfigurationManager.ConnectionStrings["doublefish"];
				DataBase._ConnectionString = cs.ConnectionString;
			}
			return new Db(DataBase._ConnectionString);
		}

		public static string MD5Encrypt (String input)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] res = md5.ComputeHash(Encoding.UTF8.GetBytes(input), 0, input.Length);
			char[] temp = new char[res.Length];
			System.Array.Copy(res, temp, res.Length);
			return new String(temp);
		}
	}
}
