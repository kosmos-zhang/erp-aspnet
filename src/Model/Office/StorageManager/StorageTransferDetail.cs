using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
   public  class StorageTransferDetail
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _transferno;
        private int? _sortno;
        private int? _productid;
        private int? _unitid;
        private decimal? _trancount;
        private decimal? _outcount;
        private decimal? _incount;
        private decimal? _tranprice;
        private decimal? _tranpricetotal;
        private decimal? _outpricetotal;
        private decimal? _inpricetotal;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private int _usedunitid;
        private decimal _usedunitcount;
        private decimal _usedprice;
        private decimal _exrate;
        private string _batchno;



        public string BatchNo
        {
            get { return _batchno; }
            set { _batchno = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int UsedUnitID
        {
            set { _usedunitid = value; }
            get { return _usedunitid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal UsedUnitCount
        {
            set { _usedunitcount = value; }
            get { return _usedunitcount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal UsedPrice
        {
            set { _usedprice = value; }
            get { return _usedprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ExRate
        {
            set { _exrate = value; }
            get { return _exrate; }
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
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TransferNo
        {
            set { _transferno = value; }
            get { return _transferno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
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
        public int? UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TranCount
        {
            set { _trancount = value; }
            get { return _trancount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OutCount
        {
            set { _outcount = value; }
            get { return _outcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? InCount
        {
            set { _incount = value; }
            get { return _incount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TranPrice
        {
            set { _tranprice = value; }
            get { return _tranprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TranPriceTotal
        {
            set { _tranpricetotal = value; }
            get { return _tranpricetotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OutPriceTotal
        {
            set { _outpricetotal = value; }
            get { return _outpricetotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? InPriceTotal
        {
            set { _inpricetotal = value; }
            get { return _inpricetotal; }
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
