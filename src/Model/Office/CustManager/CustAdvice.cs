using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.CustManager
{
   public  class CustAdviceModel
    {
       public CustAdviceModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _adviceno;
        private string _title;
        private string  _custid;
        private string _advicer;
        private string _custlinkman;
        private string _destclerk;
        private string _advicetype;
        private string _advicedate;
        private string _accept;
        private string _state;
        private string _contents;
        private string _dosomething;
        private string _leadsay;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
        private string _creator;
        private string _createddate;
        private string _canviewuser;
        private string _canviewusername;

        /// <summary>
        /// 可查看该客户档案的人员ID
        /// </summary>
        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        /// <summary>
        /// 可查看该客户档案的人员姓名
        /// </summary>
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdviceNo
        {
            set { _adviceno = value; }
            get { return _adviceno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Advicer
        {
            set { _advicer = value; }
            get { return _advicer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CustLinkMan
        {
            set { _custlinkman = value; }
            get { return _custlinkman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DestClerk
        {
            set { _destclerk = value; }
            get { return _destclerk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdviceType
        {
            set { _advicetype = value; }
            get { return _advicetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdviceDate
        {
            set { _advicedate = value; }
            get { return _advicedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Accept
        {
            set { _accept = value; }
            get { return _accept; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DoSomething
        {
            set { _dosomething = value; }
            get { return _dosomething; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LeadSay
        {
            set { _leadsay = value; }
            get { return _leadsay; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        #endregion Model
    }
}
