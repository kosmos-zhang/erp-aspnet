using System;

namespace XBase.Model.KnowledgeCenter
{

	/// <summary>
	///关键字记录表 实体
	/// </summary>
	public class KeyWordHistory
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

		private int _searchTimes;
		/// <summary>
		///搜索次数
		/// </summary>
		public int SearchTimes
		{
			get{ return _searchTimes;}
			set{ _searchTimes=value;}
		}


        private DateTime _LastDate;
        /// <summary>
        ///最后一次搜索时间
        /// </summary>
        public DateTime LastDate
        {
            get { return _LastDate; }
            set { _LastDate = value; }
        }
	}
}