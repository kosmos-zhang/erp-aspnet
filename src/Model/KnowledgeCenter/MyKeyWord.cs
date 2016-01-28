using System;

namespace XBase.Model.KnowledgeCenter
{

	/// <summary>
	///关键字订阅表 实体
	/// </summary>
	public class MyKeyWord
	{
		private int _iD;
		/// <summary>
		///ID
		/// </summary>
		public int ID
		{
			get{ return _iD;}
			set{ _iD=value;}
		}

		private string _companyCD;
		/// <summary>
		///自动记录企业代码
		/// </summary>
		public string CompanyCD
		{
			get{ return _companyCD;}
			set{ _companyCD=value;}
		}

		private int _typeID;
		/// <summary>
		///对应知识分类表ID
		/// </summary>
		public int TypeID
		{
			get{ return _typeID;}
			set{ _typeID=value;}
		}

		private string _keyType;
		/// <summary>
		///1：系统预定；2：用户自定义
		/// </summary>
		public string KeyType
		{
			get{ return _keyType;}
			set{ _keyType=value;}
		}

		private int _keyWordID;
		/// <summary>
		///KeyType为1时，对应关键字预设表ID
		/// </summary>
		public int KeyWordID
		{
			get{ return _keyWordID;}
			set{ _keyWordID=value;}
		}

		private string _keyWordName;
		/// <summary>
		///关键字名称
		/// </summary>
		public string KeyWordName
		{
			get{ return _keyWordName;}
			set{ _keyWordName=value;}
		}

		private string _creator;
		/// <summary>
		///所有者UserID
		/// </summary>
		public string Creator
		{
			get{ return _creator;}
			set{ _creator=value;}
		}

		private DateTime _createDate;
		/// <summary>
		///创建时间
		/// </summary>
		public DateTime CreateDate
		{
			get{ return _createDate;}
			set{ _createDate=value;}
		}

		private DateTime _modifiedDate;
		/// <summary>
		///修改时间
		/// </summary>
		public DateTime ModifiedDate
		{
			get{ return _modifiedDate;}
			set{ _modifiedDate=value;}
		}

		private string _modifiedUserID;
		/// <summary>
		///修改用户ID
		/// </summary>
		public string ModifiedUserID
		{
			get{ return _modifiedUserID;}
			set{ _modifiedUserID=value;}
		}

	}
}