using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public class PerformanceRoyaltyBaseModel
    {
        public PerformanceRoyaltyBaseModel()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private string _companycd;
        private string _basemoney;
        private string _taskflag;
        private string _modifieduserid;
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
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
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
        public string TaskFlag
        {
            set { _taskflag = value; }
            get { return _taskflag; }
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
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        #endregion Model

    }
}
