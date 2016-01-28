using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public class InputPerformanceRoyaltyModel
    {
        public InputPerformanceRoyaltyModel()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private string _taskno;
        private string _companycd;
        private string _tasknum;
        private string _startdate;
        private string _enddate;
        private string _taskflag;
        private string _basemoney;
        private string _performancemoney;
        private string _confficent;
        private string _modifieduserid;
        private string _createtime;
        private string _modifieddate;
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskNo
        {
            set { _taskno = value; }
            get { return _taskno; }
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
        public string taskNum
        {
            set { _tasknum = value; }
            get { return _tasknum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskFlag
        {
            set { _taskflag = value; }
            get { return _taskflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BaseMoney
        {
            set { _basemoney = value; }
            get { return _basemoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PerformanceMoney
        {
            set { _performancemoney = value; }
            get { return _performancemoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Confficent
        {
            set { _confficent = value; }
            get { return _confficent; }
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
        public string CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        #endregion Model

    }
}
