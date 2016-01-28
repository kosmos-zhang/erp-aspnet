using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SupplyChain
{
   public class ProductPriceChangeModel
    {
        public ProductPriceChangeModel()
        { }
        #region Model
        private int _id;
        private string _changeno;
        private string _companycd;
        private string _title;
        private string _productid;
        private string _standardsell;
        private string _selltax;
        private string _standardsellnew;
        private string _selltaxnew;
        private string _changedate;
        private string _chenger;
        private string _remark;
        private string _billstatus;
        private string _creator;
        private string _createdate;
        private string _confirmor;
        private string _confirmdate;

        private string _TaxRate;
        private string _TaxRateNew;
        private string _Discount;
        private string _DiscountNew;

        private string _modifieddate;
        private string _modifieduserid;

        public string TaxRate
        {
            set { _TaxRate = value; }
            get { return _TaxRate; }
        }
        public string TaxRateNew
        {
            set { _TaxRateNew = value; }
            get { return _TaxRateNew; }
        }
        public string Discount
        {
            set { _Discount = value; }
            get { return _Discount; }
        }
        public string DiscountNew
        {
            set { _DiscountNew = value; }
            get { return _DiscountNew; }
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
        public string ChangeNo
        {
            set { _changeno = value; }
            get { return _changeno; }
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
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
        public string StandardSell
        {
            set { _standardsell = value; }
            get { return _standardsell; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SellTax
        {
            set { _selltax = value; }
            get { return _selltax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StandardSellNew
        {
            set { _standardsellnew = value; }
            get { return _standardsellnew; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SellTaxNew
        {
            set { _selltaxnew = value; }
            get { return _selltaxnew; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChangeDate
        {
            set { _changedate = value; }
            get { return _changedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Chenger
        {
            set { _chenger = value; }
            get { return _chenger; }
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
        #endregion Model
    }
}
