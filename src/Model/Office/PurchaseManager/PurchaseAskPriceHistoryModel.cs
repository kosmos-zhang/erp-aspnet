using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
    public class PurchaseAskPriceHistoryModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _askno;
        private string _askorder;
        private string _providerid;
        private string _askdate;
        private string _askuserid;
        private string _deptid;
        private string _counttotal;
        private string _totalprice;
        private string _totaltax;
        private string _totalfee;
        private string _discount;
        private string _discounttotal;
        private string _realtotal;
        private string _isaddtax;
        private string _productid;
        private string _productcount;
        private string _unitid;
        private string _discountdetail;
        private string _taxrate;
        private string _taxprice;
        private string _totalfeedetail;
        private string _totalpricedetail;
        private string _totaltaxdetail;
        private string _requiredate;
        private string _unitprice;
        private string _remark;
        private string _UsedUnitID;
        private string _UsedUnitCount;
        private string _UsedPrice;
        private string _ExRate;
        /// <summary>
        /// 实际单位
        /// </summary>
        public string UsedUnitID
        {
            set { _UsedUnitID = value; }
            get { return _UsedUnitID; }
        }
        /// <summary>
        /// 实际数量
        /// </summary>
        public string UsedUnitCount
        {
            set { _UsedUnitCount = value; }
            get { return _UsedUnitCount; }
        }
        /// <summary>
        /// 实际单价
        /// </summary>
        public string UsedPrice
        {
            set { _UsedPrice = value; }
            get { return _UsedPrice; }
        }

        /// <summary>
        /// 单位换算率
        /// </summary>
        public string ExRate
        {
            set { _ExRate = value; }
            get { return _ExRate; }
        }
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
        public string ProviderID
        {
            set { _providerid = value; }
            get { return _providerid; }
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
        public string AskUserID
        {
            set { _askuserid = value; }
            get { return _askuserid; }
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
        public string CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
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
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DiscountDetail
        {
            set { _discountdetail = value; }
            get { return _discountdetail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaxRate
        {
            set { _taxrate = value; }
            get { return _taxrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaxPrice
        {
            set { _taxprice = value; }
            get { return _taxprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TotalFeeDetail
        {
            set { _totalfeedetail = value; }
            get { return _totalfeedetail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TotalPriceDetail
        {
            set { _totalpricedetail = value; }
            get { return _totalpricedetail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TotalTaxDetail
        {
            set { _totaltaxdetail = value; }
            get { return _totaltaxdetail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequireDate
        {
            set { _requiredate = value; }
            get { return _requiredate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
