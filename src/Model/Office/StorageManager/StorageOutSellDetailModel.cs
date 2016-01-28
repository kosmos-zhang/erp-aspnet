using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageOutSellDetailModel
    {
        public StorageOutSellDetailModel()
        { }
        #region Model
        private string _id;
        private string _companycd;
        private string _outno;
        private string _sortno;
        private string _storageid;
        private string _productid;
        private string _unitid;
        private string _unitprice;
        private string _productcount;
        private string _totalprice;
        private string _remark;
        private string _package;
        private string _fromtype;
        private string _frombillid;
        private string _fromlineno;
        private string _modifieddate;
        private string _modifieduserid;
        private string _defaultstorageid;

        private string _batchno;
        private string _usedunitid;
        private string _usedunitcount;
        private string _usedprice;
        private string _exrate;
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
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
        public string UsedUnitID
        {
            set { _usedunitid = value; }
            get { return _usedunitid; }
        }
        /// <summary>
        /// 数量(实际使用单位的数量)
        /// </summary>
        public string UsedUnitCount
        {
            set { _usedunitcount = value; }
            get { return _usedunitcount; }
        }
        /// <summary>
        /// 单价（实际单价）
        /// </summary>
        public string UsedPrice
        {
            set { _usedprice = value; }
            get { return _usedprice; }
        }
        /// <summary>
        /// 单位换算率
        /// </summary>
        public string ExRate
        {
            set { _exrate = value; }
            get { return _exrate; }
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
        public string OutNo
        {
            set { _outno = value; }
            get { return _outno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
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
        /// 基本单位
        /// </summary>
        public string UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 基本单价
        /// </summary>
        public string UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }
        /// <summary>
        /// 基本数量
        /// </summary>
        public string ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
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
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Package
        {
            set { _package = value; }
            get { return _package; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromLineNo
        {
            set { _fromlineno = value; }
            get { return _fromlineno; }
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
        public string DefaultStorageID
        {
            set { _defaultstorageid = value; }
            get { return _defaultstorageid; }
        }
        #endregion Model

    }
}

