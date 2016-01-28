using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	/// 实体
	/// </summary>
	public class InfoType
	{
		private int _iD;
		/// <summary>
		///
		/// </summary>
		public int ID
		{
			get{ return _iD;}
			set{ _iD=value;}
		}

		private int _rootID;
		/// <summary>
		///
		/// </summary>
		public int RootID
		{
			get{ return _rootID;}
			set{ _rootID=value;}
		}

		private string _flag;
		/// <summary>
		///
		/// </summary>
		public string Flag
		{
			get{ return _flag;}
			set{ _flag=value;}
		}

		private string _typeName;
		/// <summary>
		///
		/// </summary>
		public string TypeName
		{
			get{ return _typeName;}
			set{ _typeName=value;}
		}

		private int _supperTypeID;
		/// <summary>
		///
		/// </summary>
		public int SupperTypeID
		{
			get{ return _supperTypeID;}
			set{ _supperTypeID=value;}
		}

		private DateTime _modifiedDate;
		/// <summary>
		///
		/// </summary>
		public DateTime ModifiedDate
		{
			get{ return _modifiedDate;}
			set{ _modifiedDate=value;}
		}

		private string _modifiedUserID;
		/// <summary>
		///
		/// </summary>
		public string ModifiedUserID
		{
			get{ return _modifiedUserID;}
			set{ _modifiedUserID=value;}
		}

		private string _path;
		/// <summary>
		///
		/// </summary>
		public string Path
		{
			get{ return _path;}
			set{ _path=value;}
		}

	}
}