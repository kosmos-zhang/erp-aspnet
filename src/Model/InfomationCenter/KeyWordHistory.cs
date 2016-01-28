using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	/// 实体
	/// </summary>
	public class KeyWordHistory
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

		private string _keyWordName;
		/// <summary>
		///
		/// </summary>
		public string KeyWordName
		{
			get{ return _keyWordName;}
			set{ _keyWordName=value;}
		}

		private int _searchTimes;
		/// <summary>
		///
		/// </summary>
		public int SearchTimes
		{
			get{ return _searchTimes;}
			set{ _searchTimes=value;}
		}

		private DateTime _lastDate;
		/// <summary>
		///
		/// </summary>
		public DateTime LastDate
		{
			get{ return _lastDate;}
			set{ _lastDate=value;}
		}

	}
}