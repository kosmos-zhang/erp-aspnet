using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.LogisticsDistributionManager
{
   public  class SubProductSendPrice
    {
        #region Model
        private int _id;
        private string _companycd;
        private int? _productid;
        private int? _deptid;
        private decimal? _sendprice;
        private decimal? _sendpricetax;
        private decimal? _sendtax;
        private decimal? _discount;
        private int? _creator;
        private DateTime? _createdate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
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
        public int? ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SendPrice
        {
            set { _sendprice = value; }
            get { return _sendprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SendPriceTax
        {
            set { _sendpricetax = value; }
            get { return _sendpricetax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SendTax
        {
            set { _sendtax = value; }
            get { return _sendtax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedDate
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
