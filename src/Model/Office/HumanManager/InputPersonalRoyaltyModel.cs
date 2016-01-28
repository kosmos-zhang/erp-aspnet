using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public class InputPersonalRoyaltyModel
    {
        public InputPersonalRoyaltyModel()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private string _taskNo;
        private string _companycd;
        private string _fromtype;
        private string _currencytype;
        private string _rate;
        private string _aftertaxmoney;
        private string _custno;
        private string _newcustomertax;
        private string _oldcustomertax;
        private string _frombillno;
        private string _modifieduserid;
        private string _createtime;
        private string _modifieddate;
        private string _customerno;
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
            set { _taskNo = value; }
            get { return _taskNo; }
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
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AfterTaxMoney
        {
            set { _aftertaxmoney = value; }
            get { return _aftertaxmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CustNo
        {
            set { _custno = value; }
            get { return _custno; }
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
        public string FromBillNo
        {
            set { _frombillno = value; }
            get { return _frombillno; }
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
        /// <summary>
        /// 
        /// </summary>
        public string CustomerNo
        {
            set { _customerno = value; }
            get { return _customerno; }
        }
        #endregion Model

    }
}
