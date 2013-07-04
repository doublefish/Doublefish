using System;
using System.Reflection;
using System.Web;
using System.Web.Caching;

namespace DoubleFish.Http
{
	/// <summary>
	/// Http相关
	/// </summary>
	public static class Http
	{
		/// <summary>
		/// 从HttpContext.Cache中获取缓存的实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="context"></param>
		/// <returns></returns>
		public static T GetInstanceFromCache<T> (this HttpContext context) where T : class
		{
			if (context == null || context.Cache == null)
				return null;

			return context.Cache.GetInstance<T>();
		}

		/// <summary>
		/// 从HttpContext.Cache中获取缓存的实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cache"></param>
		/// <returns></returns>
		public static T GetInstance<T> (this System.Web.Caching.Cache cache) where T : class
		{

			Type type = typeof(T);

			T t = cache[type.FullName] as T;

			if (t != null)
				return t;

			Assembly assembly = type.Assembly;
			t = assembly.CreateInstance(type.FullName) as T;

			CacheDependency fileDependency = new CacheDependency(type.Assembly.Location);
			cache.Insert(type.FullName, t, fileDependency);

			return t;
		}

		/// <summary>
		/// 从HttpContext.Items中获取缓存的实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="context"></param>
		/// <returns></returns>
		public static T GetInstanceFromItems<T> (this HttpContext context) where T : class
		{
			if (context == null || context.Items == null)
				return null;

			return context.Items.GetInstance<T>();
		}

		/// <summary>
		/// 从HttpContext.Items中获取缓存的实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items"></param>
		/// <returns></returns>
		public static T GetInstance<T> (this System.Collections.IDictionary items) where T : class
		{
			if (items == null)
				return null;

			T t = items[typeof(T).AssemblyQualifiedName] as T;//取缓存实例
			if (t != null)
				return t;

			Type type = typeof(T);

			t = (T)Activator.CreateInstance(type);

			items[typeof(T).AssemblyQualifiedName] = t;

			return t;
		}
	}
}
