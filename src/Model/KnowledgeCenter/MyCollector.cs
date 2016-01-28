using System;

namespace XBase.Model.KnowledgeCenter
{

	/// <summary>
	///我的收件夹 实体
	/// </summary>
	public class MyCollector
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

		private string _flag;
		/// <summary>
		///收藏夹类型
		/// </summary>
		public string Flag
		{
			get{ return _flag;}
			set{ _flag=value;}
		}

		private int _knowledgeID;
		/// <summary>
		///对应知识库ID
		/// </summary>
		public int KnowledgeID
		{
			get{ return _knowledgeID;}
			set{ _knowledgeID=value;}
		}

		private string _owner;
		/// <summary>
		///对应操作用户表UserID
		/// </summary>
		public string Owner
		{
			get{ return _owner;}
			set{ _owner=value;}
		}

		private string _sourceType;
		/// <summary>
		///0：自动搜索；1：服务添加
		/// </summary>
		public string SourceType
		{
			get{ return _sourceType;}
			set{ _sourceType=value;}
		}

		private DateTime _createDate;
		/// <summary>
		///收藏时间
		/// </summary>
		public DateTime CreateDate
		{
			get{ return _createDate;}
			set{ _createDate=value;}
		}

		private string _readStatus;
		/// <summary>
		///0未读，1已读
		/// </summary>
		public string ReadStatus
		{
			get{ return _readStatus;}
			set{ _readStatus=value;}
		}

		private DateTime _readDate;
		/// <summary>
		///阅读状态和时间仅对“收件夹”有用，用于标记是否已读
		/// </summary>
		public DateTime ReadDate
		{
			get{ return _readDate;}
			set{ _readDate=value;}
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