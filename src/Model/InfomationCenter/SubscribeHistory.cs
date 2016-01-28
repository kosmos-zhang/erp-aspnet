using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	/// 实体
	/// </summary>
	public class SubscribeHistory
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

		private string _receiver;
		/// <summary>
		///
		/// </summary>
		public string Receiver
		{
			get{ return _receiver;}
			set{ _receiver=value;}
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

		private DateTime _sendDate;
		/// <summary>
		///
		/// </summary>
		public DateTime SendDate
		{
			get{ return _sendDate;}
			set{ _sendDate=value;}
		}

		private string _keyWordID;
		/// <summary>
		///
		/// </summary>
		public string KeyWordID
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

	}
}