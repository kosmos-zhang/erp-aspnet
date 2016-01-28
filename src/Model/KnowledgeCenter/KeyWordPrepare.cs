using System;

namespace XBase.Model.KnowledgeCenter
{

	/// <summary>
	///关键字预设表 实体
	/// </summary>
	public class KeyWordPrepare
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

		private int _typeID;
		/// <summary>
		///对应知识分类表ID
		/// </summary>
		public int TypeID
		{
			get{ return _typeID;}
			set{ _typeID=value;}
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

		private string _createUserID;
		/// <summary>
		///创建人ID
		/// </summary>
		public string CreateUserID
		{
			get{ return _createUserID;}
			set{ _createUserID=value;}
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