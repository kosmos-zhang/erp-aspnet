using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
  public   class StorageCheck
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _checkno;
        private string _title;
        private DateTime? _checkstartdate;
        private DateTime? _checkenddate;
        private int? _storageid;
        private int? _deptid;
        private int? _checktype;
        private int? _transactor;
        private decimal? _nowcount;
        private decimal? _checkcount;
        private decimal? _diffcount;
        private decimal? _nowmoney;
        private decimal? _checkmoney;
        private decimal? _diffmoney;
        private string _summary;
        private string _remark;
        private string _attachment;
        private int? _checkuserid;
        private DateTime? _checkdate;
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
        public string CheckNo
        {
            set { _checkno = value; }
            get { return _checkno; }
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
        public DateTime? CheckStartkDate
        {
            set {  _checkstartdate = value; }
            get { return _checkstartdate; }
        }


        public DateTime? CheckEndDate
        {
            set { _checkenddate = value; }
            get { return _checkenddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
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
        public int? CheckType
        {
            set { _checktype = value; }
            get { return _checktype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Transactor
        {
            set { _transactor = value; }
            get { return _transactor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? NowCount
        {
            set { _nowcount = value; }
            get { return _nowcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CheckCount
        {
            set { _checkcount = value; }
            get { return _checkcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DiffCount
        {
            set { _diffcount = value; }
            get { return _diffcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? NowMoney
        {
            set { _nowmoney = value; }
            get { return _nowmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CheckMoney
        {
            set { _checkmoney = value; }
            get { return _checkmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DiffMoney
        {
            set { _diffmoney = value; }
            get { return _diffmoney; }
        }
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
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }

        public int? CheckUserID
        {
            set { _checkuserid = value; }
            get { return _checkuserid; }
        }
        public DateTime? CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
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
