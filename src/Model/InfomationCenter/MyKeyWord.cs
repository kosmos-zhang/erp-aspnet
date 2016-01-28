using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	/// 实体
	/// </summary>
	public class MyKeyWord
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

		private string _companyCD;
		/// <summary>
		///
		/// </summary>
		public string CompanyCD
		{
			get{ return _companyCD;}
			set{ _companyCD=value;}
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

		private int _typeID;
		/// <summary>
		///
		/// </summary>
		public int TypeID
		{
			get{ return _typeID;}
			set{ _typeID=value;}
		}

		private string _keyType;
		/// <summary>
		///
		/// </summary>
		public string KeyType
		{
			get{ return _keyType;}
			set{ _keyType=value;}
		}

		private int _keyWordID;
		/// <summary>
		///
		/// </summary>
		public int KeyWordID
		{
			get{ return _keyWordID;}
			set{ _keyWordID=value;}
		}

		private string _keyWordName;
		/// <summary>
		///
		/// </summary>
		public string KeyWordName
		{
			get{ return _keyWordName;}
			set{ _keyWordName=value;}
		}

		private string _creator;
		/// <summary>
		///
		/// </summary>
		public string Creator
		{
			get{ return _creator;}
			set{ _creator=value;}
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

	}
}