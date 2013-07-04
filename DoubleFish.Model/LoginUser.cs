using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleFish.Model
{
	[Serializable]
	public class LoginUser
	{
		private long _Id;
		private string _Name;
		private string _FullName;
		private string _Mobilephone;
		private long _Status;
		private string _Address;
		private int _LoginCount;
		private DateTime _LastLogin;

		public long Id
		{
			set
			{
				this._Id = value;
			}
			get
			{
				return this._Id;
			}
		}
		public string Name
		{
			set
			{
				this._Name = value;
			}
			get
			{
				return this._Name;
			}
		}
		public string FullName
		{
			set
			{
				this._FullName = value;
			}
			get
			{
				return this._FullName;
			}
		}
		public string Mobilephone
		{
			get
			{
				return this._Mobilephone;
			}
			set
			{
				this._Mobilephone = value;
			}
		}
		public long Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}
		public string Address
		{
			set
			{
				this._Address = value;
			}
			get
			{
				return this._Address;
			}
		}
		public int LoginCount
		{
			set
			{
				this._LoginCount = value;
			}
			get
			{
				return this._LoginCount;
			}
		}
		public DateTime LastLogin
		{
			set
			{
				this._LastLogin = value;
			}
			get
			{
				return this._LastLogin;
			}
		}

	}
}
