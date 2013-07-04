using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleFish.Model.PagedQuery
{
	/// <summary>
	/// 分页查询
	/// </summary>
	/// <typeparam name="ORM">数据库实体类型</typeparam>
	[Serializable]
	public class PagedQueryArgs<ORM> : EventArgs where ORM : class, new()
	{
		/// <summary>
		/// 页大小
		/// </summary>
		public int PageSize { set; get; }
		/// <summary>
		/// 页索引，第一页索引从零开始。
		/// </summary>
		public int PageIndex { set; get; }
		/// <summary>
		/// 总记录条数，如果此属性预设值大于零则表示不需要从数据库获取。
		/// </summary>
		public int ResultCount { set; get; }
		/// <summary>
		/// 分页结果集。
		/// </summary>
		public ORM[] Results { set; get; }
	}

	/// <summary>
	/// 排序方向
	/// </summary>
	public enum OrderDirection : sbyte
	{
		/// <summary>
		/// 不排序
		/// </summary>
		None = 0,
		/// <summary>
		/// 升序排序
		/// </summary>
		Ascending = 1,
		/// <summary>
		/// 降序排序
		/// </summary>
		Descending = -1
	}
}
