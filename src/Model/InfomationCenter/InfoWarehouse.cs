using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	/// 实体
	/// </summary>
	public class InfoWarehouse
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

		private string _infoNo;
		/// <summary>
		///
		/// </summary>
		public string InfoNo
		{
			get{ return _infoNo;}
			set{ _infoNo=value;}
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

		private int _typeID;
		/// <summary>
		///
		/// </summary>
		public int TypeID
		{
			get{ return _typeID;}
			set{ _typeID=value;}
		}

		private string _title;
		/// <summary>
		///
		/// </summary>
		public string Title
		{
			get{ return _title;}
			set{ _title=value;}
		}

		private string _keyword;
		/// <summary>
		///
		/// </summary>
		public string Keyword
		{
			get{ return _keyword;}
			set{ _keyword=value;}
		}

		private string _content;
		/// <summary>
		///
		/// </summary>
		public string Content
		{
			get{ return _content;}
			set{ _content=value;}
		}

		private string _isShow;
		/// <summary>
		///
		/// </summary>
		public string IsShow
		{
			get{ return _isShow;}
			set{ _isShow=value;}
		}

		private string _sourceFrom;
		/// <summary>
		///
		/// </summary>
		public string SourceFrom
		{
			get{ return _sourceFrom;}
			set{ _sourceFrom=value;}
		}

		private string _safeLevel;
		/// <summary>
		///
		/// </summary>
		public string SafeLevel
		{
			get{ return _safeLevel;}
			set{ _safeLevel=value;}
		}

		private string _author;
		/// <summary>
		///
		/// </summary>
		public string Author
		{
			get{ return _author;}
			set{ _author=value;}
		}

		private string _attachment;
		/// <summary>
		///
		/// </summary>
		public string Attachment
		{
			get{ return _attachment;}
			set{ _attachment=value;}
		}

		private string _createUserID;
		/// <summary>
		///
		/// </summary>
		public string CreateUserID
		{
			get{ return _createUserID;}
			set{ _createUserID=value;}
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

		private int _readTimes;
		/// <summary>
		///
		/// </summary>
		public int ReadTimes
		{
			get{ return _readTimes;}
			set{ _readTimes=value;}
		}

		private int _downloadTimes;
		/// <summary>
		///
		/// </summary>
		public int DownloadTimes
		{
			get{ return _downloadTimes;}
			set{ _downloadTimes=value;}
		}

		private string _professionType;
		/// <summary>
		///
		/// </summary>
		public string ProfessionType
		{
			get{ return _professionType;}
			set{ _professionType=value;}
		}

		private int _expireDays;
		/// <summary>
		///
		/// </summary>
		public int ExpireDays
		{
			get{ return _expireDays;}
			set{ _expireDays=value;}
		}

	}
}