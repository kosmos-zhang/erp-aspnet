using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SystemManager
{
   public class FlowModel
    {
        public FlowModel()
        { }
        #region Model
        private string _companycd;
        private int _deptid;
        private string _flowid;
        private string _flowname;
        private int _billtypeflag;
        private int _billtypecode;
        private string _usedstatus;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _isMobileNotice;
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
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FlowNo
        {
            set { _flowid = value; }
            get { return _flowid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FlowName
        {
            set { _flowname = value; }
            get { return _flowname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BillTypeFlag
        {
            set { _billtypeflag = value; }
            get { return _billtypeflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BillTypeCode
        {
            set { _billtypecode = value; }
            get { return _billtypecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedDate
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
        /// 是否发送手机短信提醒
        /// </summary>
        public string IsMobileNotice
        {
            set { _isMobileNotice = value; }
            get { return _isMobileNotice; }
        }
        #endregion Model
    }
}
