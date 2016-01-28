using System;

namespace XBase.Model.KnowledgeCenter
{

	/// <summary>
	///订阅发送记录表 实体
	/// </summary>
	public class SubscribeHistory
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

		private string _receiver;
		/// <summary>
		///对应操作用户表UserID
		/// </summary>
		public string Receiver
		{
			get{ return _receiver;}
			set{ _receiver=value;}
		}

		private int _knowledgeID;
		/// <summary>
		///知识库ID
		/// </summary>
		public int KnowledgeID
		{
			get{ return _knowledgeID;}
			set{ _knowledgeID=value;}
		}

		private DateTime _sendDate;
		/// <summary>
		///发送时间
		/// </summary>
		public DateTime SendDate
		{
			get{ return _sendDate;}
			set{ _sendDate=value;}
		}

		private string _keyWordID;
		/// <summary>
		///关键字ID
		/// </summary>
		public string KeyWordID
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

	}
}