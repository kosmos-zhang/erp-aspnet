using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public class SalaryPersonalRoyaltySetModel
    {
        public SalaryPersonalRoyaltySetModel()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private string _companycd;
        private string _custid;
        private string _minimoney;
        private string _newcustomertax;
        private string _oldcustomertax;
        private string _maxmoney;
        private string _modifieduserid;
        private string _iscustomerroyaltyset;
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
        public string CustID
        {
            set { _custid = value; }
            get { return _custid; }
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
        public string NewCustomerTax
        {
            set { _newcustomertax = value; }
            get { return _newcustomertax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldCustomerTax
        {
            set { _oldcustomertax = value; }
            get { return _oldcustomertax; }
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
        public string ISCustomerRoyaltySet
        {
            set { _iscustomerroyaltyset = value; }
            get { return _iscustomerroyaltyset; }
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
