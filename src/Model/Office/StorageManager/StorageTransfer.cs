using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
  public   class StorageTransfer
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _transferno;
        private string _title;
        private int? _reasontype;
        private int? _instorageid;
        private int? _outstorageid;
        private int? _applyuserid;
        private int? _applydeptid;
        private int? _outdeptid;
        private DateTime? _requireindate;
        private decimal? _transferprice;
        private decimal? _transferfeesum;
        private decimal? _outfeesum;
        private decimal? _infeesum;
        private decimal? _transfercount;
        private decimal? _outcount;
        private decimal? _incount;
        private string _busistatus;
        private int? _outuserid;
        private DateTime? _outdate;
        private int? _inuserid;
        private DateTime? _indate;
        private decimal? _transferfee;
        private string _isopenbill;
        private string _remark;
        private int? _creator;
        private DateTime? _createdate;
        private string _billstatus;
        private int? _confirmor;
        private DateTime? _confirmdate;
        private int? _closer;
        private DateTime? _closedate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _summary;

      /// <summary>
      /// 
      /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReasonType
        {
            set { _reasontype = value; }
            get { return _reasontype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? InStorageID
        {
            set { _instorageid = value; }
            get { return _instorageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OutStorageID
        {
            set { _outstorageid = value; }
            get { return _outstorageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ApplyUserID
        {
            set { _applyuserid = value; }
            get { return _applyuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ApplyDeptID
        {
            set { _applydeptid = value; }
            get { return _applydeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OutDeptID
        {
            set { _outdeptid = value; }
            get { return _outdeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? RequireInDate
        {
            set { _requireindate = value; }
            get { return _requireindate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TransferPrice
        {
            set { _transferprice = value; }
            get { return _transferprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TransferFeeSum
        {
            set { _transferfeesum = value; }
            get { return _transferfeesum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OutFeeSum
        {
            set { _outfeesum = value; }
            get { return _outfeesum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? InFeeSum
        {
            set { _infeesum = value; }
            get { return _infeesum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TransferCount
        {
            set { _transfercount = value; }
            get { return _transfercount; }
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
        public string BusiStatus
        {
            set { _busistatus = value; }
            get { return _busistatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OutUserID
        {
            set { _outuserid = value; }
            get { return _outuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutDate
        {
            set { _outdate = value; }
            get { return _outdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? InUserID
        {
            set { _inuserid = value; }
            get { return _inuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? InDate
        {
            set { _indate = value; }
            get { return _indate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TransferFee
        {
            set { _transferfee = value; }
            get { return _transferfee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string isOpenbill
        {
            set { _isopenbill = value; }
            get { return _isopenbill; }
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
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
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
