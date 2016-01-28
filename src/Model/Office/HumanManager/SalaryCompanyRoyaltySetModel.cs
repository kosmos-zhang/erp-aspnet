using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public class SalaryCompanyRoyaltySetModel
    {
        public SalaryCompanyRoyaltySetModel()
        { }
        #region Model
        private string _id;
        private string _deptid;
        private string _companycd;
        private string _minimoney;
        private string _maxmoney;
        private string _modifieduserid;
        private string _modifieddate;
        private string _taxpercent;
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
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
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
        public string MiniMoney
        {
            set { _minimoney = value; }
            get { return _minimoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MaxMoney
        {
            set { _maxmoney = value; }
            get { return _maxmoney; }
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
        /// <summary>
        /// 
        /// </summary>
        public string TaxPercent
        {
            set { _taxpercent = value; }
            get { return _taxpercent; }
        }
        #endregion Model

    }
}
