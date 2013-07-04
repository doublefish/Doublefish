using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleFish.Model.PagedQuery
{
	/// <summary>
	/// 部门信息
	/// </summary>
	/// <typeparam name="ORM">数据库实体类型</typeparam>
	[Serializable]
	public class PagedDepartmentArgs<ORM> : PagedQueryArgs<ORM> where ORM : class, new()
	{
		/// <summary>
		/// 名称：全模糊
		/// </summary>
		public string NameIn { set; get; }
		/// <summary>
		/// 机构代码
		/// </summary>
		public string OrgCodeIn { set; get; }
		/// <summary>
		/// 联系人
		/// </summary>
		public string ContactPersonIn { set; get; }
		/// <summary>
		/// 负责人
		/// </summary>
		public string ChargePersonIn { set; get; }
		/// <summary>
		/// 部门类别
		/// </summary>
		public long Category { set; get; }
		/// <summary>
		/// 部门类型
		/// </summary>
		public long Type { set; get; }
		/// <summary>
		/// 经济类型
		/// </summary>
		public long EconomicType { set; get; }
		/// <summary>
		/// 所属地域
		/// </summary>
		public long Region { set; get; }
		/// <summary>
		/// 状态
		/// </summary>
		public int Status { set; get; }
	}
}
