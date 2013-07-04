using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleFish.Model
{
	public class OrmBase : MarshalByRefObject
	{
		/// <summary>
		/// 附加的额外数据
		/// </summary>
		public object Tag { set; get; }
	}
}
