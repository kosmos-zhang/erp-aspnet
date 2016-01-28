using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.LogisticsDistributionManager
{
    public  class SubDeliveryTrans
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _transno;
        private string _title;
        private int? _applyuserid;
        private int? _outdeptid;
        private int? _indeptid;
        private int? _tansdeptid;
        private decimal? _transprice;
        private decimal? _transfeesum;
        private decimal? _transcount;
        private string _reason;
        private string _busistatus;
        private int? _outuserid;
        private DateTime? _outdate;
        private int? _inuserid;
        private DateTime? _indate;
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
        public string TransNo
        {
            set { _transno = value; }
            get { return _transno; }
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
        public int? OutDeptID
        {
            set { _outdeptid = value; }
            get { return _outdeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? InDeptID
        {
            set { _indeptid = value; }
            get { return _indeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TransDeptID
        {
            set { _tansdeptid = value; }
            get { return _tansdeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TransPrice
        {
            set { _transprice = value; }
            get { return _transprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TransFeeSum
        {
            set { _transfeesum = value; }
            get { return _transfeesum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TransCount
        {
            set { _transcount = value; }
            get { return _transcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
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
