using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	/// 实体
	/// </summary>
	public class MyCollector
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

		private string _flag;
		/// <summary>
		///
		/// </summary>
		public string Flag
		{
			get{ return _flag;}
			set{ _flag=value;}
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

		private int _infoID;
		/// <summary>
		///
		/// </summary>
		public int InfoID
		{
			get{ return _infoID;}
			set{ _infoID=value;}
		}

		private string _owner;
		/// <summary>
		///
		/// </summary>
		public string Owner
		{
			get{ return _owner;}
			set{ _owner=value;}
		}

		private string _sourceType;
		/// <summary>
		///
		/// </summary>
		public string SourceType
		{
			get{ return _sourceType;}
			set{ _sourceType=value;}
		}

		private DateTime _createDate;
		/// <summary>
		///
		/// </summary>
		public DateTime CreateDate
		{
			get{ return _createDate;}
			set{ _createDate=value;}
		}

		private string _readStatus;
		/// <summary>
		///
		/// </summary>
		public string ReadStatus
		{
			get{ return _readStatus;}
			set{ _readStatus=value;}
		}

		private DateTime _readDate;
		/// <summary>
		///
		/// </summary>
		public DateTime ReadDate
		{
			get{ return _readDate;}
			set{ _readDate=value;}
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

	}
}