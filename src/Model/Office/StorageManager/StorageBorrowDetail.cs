using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
  public  class StorageBorrowDetail
    {
      /// <summary>
      /// 借货单明细表实体
      /// </summary>
        public StorageBorrowDetail()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _borrowno;
        private int? _sortno;
        private string _productid;
        private int? _unitid;
        private decimal? _unitprice;
        private decimal? _productcount;
        private decimal? _totalprice;
        private DateTime? _returndate;
        private decimal? _returncount;
        private decimal? _realreturncount;
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
        public string BorrowNo
        {
            set { _borrowno = value; }
            get { return _borrowno; }
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
        public string ProductID
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
        public decimal? UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReturnDate
        {
            set { _returndate = value; }
            get { return _returndate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ReturnCount
        {
            set { _returncount = value; }
            get { return _returncount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? RealReturnCount
        {
            set { _realreturncount = value; }
            get { return _realreturncount; }
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
