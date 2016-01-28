using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
   public  class StorageCheckDetail
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _checkno;
        private int? _sortno;
        private int? _productid;
        private int? _unitid;
        private decimal? _nowcount;
        private decimal? _checkcount;
        private decimal? _diffcount;
        private string _difftype;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;

        private string _batchno;
        private int? _usedunitid;
        private decimal? _usedunitcount;
        private decimal? _usedprice;
        private decimal? _exrate;
        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo
        {
            set { _batchno = value; }
            get { return _batchno; }
        }
        /// <summary>
        /// 单位(实际使用的单位)
        /// </summary>
        public int? UsedUnitID
        {
            set { _usedunitid = value; }
            get { return _usedunitid; }
        }
        /// <summary>
        /// 数量(实际使用单位的数量)
        /// </summary>
        public decimal? UsedUnitCount
        {
            set { _usedunitcount = value; }
            get { return _usedunitcount; }
        }
        /// <summary>
        /// 单价（实际单价）
        /// </summary>
        public decimal? UsedPrice
        {
            set { _usedprice = value; }
            get { return _usedprice; }
        }
        /// <summary>
        /// 单位换算率
        /// </summary>
        public decimal? ExRate
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
        public string CheckNo
        {
            set { _checkno = value; }
            get { return _checkno; }
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
        public string DiffType
        {
            set { _difftype = value; }
            get { return _difftype; }
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
