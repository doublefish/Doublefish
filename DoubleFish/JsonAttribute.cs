using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DoubleFish
{
	[Serializable, ComVisible(true), ClassInterface(ClassInterfaceType.None), AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
	public class JsonAttribute : Attribute
	{
		// Methods
		public JsonAttribute () { }
	}
}
