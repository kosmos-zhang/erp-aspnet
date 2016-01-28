using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.LogisticsDistributionManager
{
 public   class SubDeliveryBack
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _backno;
        private int? _sendid;
        private string _title;
        private int? _applyuserid;
        private int? _applydeptid;
        private int? _outdeptid;
        private decimal? _backprice;
        private decimal? _backfeesum;
        private decimal? _backcount;
        private string _backreason;
        private string _busistatus;
        private int? _outuserid;
        private DateTime? _outdate;
        private int? _inuserid;
        private DateTime?  _indate;
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
        public string BackNo
        {
            set { _backno = value; }
            get { return _backno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SendID
        {
            set { _sendid = value; }
            get { return _sendid; }
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
        public decimal? BackPrice
        {
            set { _backprice = value; }
            get { return _backprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? BackFeeSum
        {
            set { _backfeesum = value; }
            get { return _backfeesum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? BackCount
        {
            set { _backcount = value; }
            get { return _backcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BackReason
        {
            set { _backreason = value; }
            get { return _backreason; }
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
