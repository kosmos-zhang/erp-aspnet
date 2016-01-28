using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
    public class PurchaseAskPriceModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _askno;
        private string _askorder;
        private string _asktitle;
        private string _fromtype;
        private string _askdate;
        private string _endaskdate;
        private string _providerid;
        private string _deptid;
        private string _askuserid;
        private string _typeid;
        private string _currencytype;
        private string _rate;
        private string _totalprice;
        private string _totaltax;
        private string _totalfee;
        private string _discount;
        private string _discounttotal;
        private string _realtotal;
        private string _isaddtax;
        private string _counttotal;
        private string _remark;
        private string _billstatus;
        private string _creator;
        private string _createdate;
        private string _modifieddate;
        private string _modifieduserid;
        private string _confirmor;
        private string _confirmdate;
        private string _closer;
        private string _closedate;
        private string _flowstatus;
        private string _askagain;
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
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AskNo
        {
            set { _askno = value; }
            get { return _askno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AskOrder
        {
            set { _askorder = value; }
            get { return _askorder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AskTitle
        {
            set { _asktitle = value; }
            get { return _asktitle; }
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
        public string AskDate
        {
            set { _askdate = value; }
            get { return _askdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EndAskDate
        {
            set { _endaskdate = value; }
            get { return _endaskdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProviderID
        {
            set { _providerid = value; }
            get { return _providerid; }
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
        public string AskUserID
        {
            set { _askuserid = value; }
            get { return _askuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
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
        public string TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TotalTax
        {
            set { _totaltax = value; }
            get { return _totaltax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DiscountTotal
        {
            set { _discounttotal = value; }
            get { return _discounttotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RealTotal
        {
            set { _realtotal = value; }
            get { return _realtotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string isAddTax
        {
            set { _isaddtax = value; }
            get { return _isaddtax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
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
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
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
        public string Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FlowStatus
        {
            set { _flowstatus = value; }
            get { return _flowstatus; }
        }
        public string EFIndex
        { get; set; }
        public string EFDesc
        { get; set; }
        /// <summary>
        /// 再次询价标志
        /// </summary>
        public string AskAgain
        {
            set { _askagain = value; }
            get { return _askagain; }
        }
        #endregion Model
    }
}
