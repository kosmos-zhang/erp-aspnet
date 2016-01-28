using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	/// 实体
	/// </summary>
	public class InfoRequire
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

		private DateTime _sendDate;
		/// <summary>
		///
		/// </summary>
		public DateTime SendDate
		{
			get{ return _sendDate;}
			set{ _sendDate=value;}
		}

		private string _sendUserID;
		/// <summary>
		///
		/// </summary>
		public string SendUserID
		{
			get{ return _sendUserID;}
			set{ _sendUserID=value;}
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

		private string _feeBackStatus;
		/// <summary>
		///
		/// </summary>
		public string FeeBackStatus
		{
			get{ return _feeBackStatus;}
			set{ _feeBackStatus=value;}
		}

		private DateTime _feeBackDate;
		/// <summary>
		///
		/// </summary>
		public DateTime FeeBackDate
		{
			get{ return _feeBackDate;}
			set{ _feeBackDate=value;}
		}

		private string _feeBackUserID;
		/// <summary>
		///
		/// </summary>
		public string FeeBackUserID
		{
			get{ return _feeBackUserID;}
			set{ _feeBackUserID=value;}
		}

	}
}