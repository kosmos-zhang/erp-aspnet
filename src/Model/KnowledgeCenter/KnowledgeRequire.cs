using System;

namespace XBase.Model.KnowledgeCenter
{

	/// <summary>
	///求知记录表 实体
	/// </summary>
	public class KnowledgeRequire
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

		private DateTime _sendDate;
		/// <summary>
		///发送时间
		/// </summary>
		public DateTime SendDate
		{
			get{ return _sendDate;}
			set{ _sendDate=value;}
		}

		private string _sendUserID;
		/// <summary>
		///对应操作用户表UserID
		/// </summary>
		public string SendUserID
		{
			get{ return _sendUserID;}
			set{ _sendUserID=value;}
		}

		private string _content;
		/// <summary>
		///求知内容
		/// </summary>
		public string Content
		{
			get{ return _content;}
			set{ _content=value;}
		}

		private string _feeBackStatus;
		/// <summary>
		///:未处理；1：已处理
		/// </summary>
		public string FeeBackStatus
		{
			get{ return _feeBackStatus;}
			set{ _feeBackStatus=value;}
		}

		private DateTime _feeBackDate;
		/// <summary>
		///服务时间
		/// </summary>
		public DateTime FeeBackDate
		{
			get{ return _feeBackDate;}
			set{ _feeBackDate=value;}
		}

		private string _feeBackUserID;
		/// <summary>
		///对应公司后台维护人员ID
		/// </summary>
		public string FeeBackUserID
		{
			get{ return _feeBackUserID;}
			set{ _feeBackUserID=value;}
		}

	}
}