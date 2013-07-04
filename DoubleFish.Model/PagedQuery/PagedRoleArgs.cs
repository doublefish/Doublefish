using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleFish.Model.PagedQuery
{
	/// <summary>
	/// 角色信息
	/// </summary>
	/// <typeparam name="ORM">数据库实体类型</typeparam>
	[Serializable]
	public class PagedRoleArgs<ORM> : PagedQueryArgs<ORM> where ORM : class, new()
	{
		/// <summary>
		/// 名称：全模糊
		/// </summary>
		public string NameIn { set; get; }
		/// <summary>
		/// 状态
		/// </summary>
		public int Flag { set; get; }
	}
}
