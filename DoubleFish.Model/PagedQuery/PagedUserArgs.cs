using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleFish.Model.PagedQuery
{
	/// <summary>
	/// 用户信息
	/// </summary>
	/// <typeparam name="ORM">数据库实体类型</typeparam>
	[Serializable]
	public class PagedUserArgs<ORM> : PagedQueryArgs<ORM> where ORM : class, new()
	{
		/// <summary>
		/// 用户名
		/// </summary>
		public string NameIn { set; get; }
		/// <summary>
		/// 名称：全模糊
		/// </summary>
		public string FullNameIn { set; get; }
		/// <summary>
		/// 所属部门
		/// </summary>
		public long Department { set; get; }
		/// <summary>
		/// 所属区域
		/// </summary>
		public long Region { set; get; }
		/// <summary>
		/// 性别
		/// </summary>
		public int Sex { set; get; }
		/// <summary>
		/// 状态
		/// </summary>
		public int Status { set; get; }
		/// <summary>
		/// 角色
		/// </summary>
		public long Role { set; get; }
		/// <summary>
		/// 是否角色内
		/// </summary>
		public bool IsInRole { set; get; }

		public OrderBy OrderBy { set; get; }
	
	}

	public enum OrderBy
	{
		Id,
		Code,
		Time
	}
}
