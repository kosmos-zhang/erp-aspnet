using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.CustManager
{
    public class CustCallModel
    {
       #region Model
		private int _id;
		private string _companycd;
		private int _custid;
		private string _tel;
		private DateTime _calltime;
		private string _callcontents;
        private string _creator;
        private string _callor;
        private string _modifieduserid;
        private string _modifieddate;
        private string _title;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
		set{ _id=value;}
		get{return _id;}
		}
		/// <summary>
		/// 公司代码
		/// </summary>
		public string CompanyCD
		{
		set{ _companycd=value;}
		get{return _companycd;}
		}
		/// <summary>
		/// 客户ID
		/// </summary>
		public int CustID
		{
		set{ _custid=value;}
		get{return _custid;}
		}
		/// <summary>
		/// 来电号码
		/// </summary>
		public string Tel
		{
		set{ _tel=value;}
		get{return _tel;}
		}
		/// <summary>
		/// 来电时间
		/// </summary>
		public DateTime CallTime
		{
		set{ _calltime=value;}
		get{return _calltime;}
		}
		/// <summary>
		/// 来电内容
		/// </summary>
		public string CallContents
		{
		set{ _callcontents=value;}
		get{return _callcontents;}
		}
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 来电人（不一定存在于客户联系人表中）
        /// </summary>
        public string Callor
        {
            set { _callor = value; }
            get { return _callor; }
        }
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
		#endregion Model
    }
}
