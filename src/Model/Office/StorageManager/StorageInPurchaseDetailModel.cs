using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageInPurchaseDetailModel
    {
        public StorageInPurchaseDetailModel()
        { }
        #region Model
        private string _id;
        private string _inno;
        private string _fromtype;
        private string _frombillid;
        private string _fromlineno;
        private string _productid;
        private string _productcount;
        private string _unitid;
        private string _unitprice;
        private string _totalprice;
        private string _storageid;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
        private string _sortno;
        private string _companycd;
        private string _defaultstorageid;

        private string _usedunitid;
        private string _usedunitcount;
        private string _usedprice;
        private string _exrate;
        private string _batchno;//批次

        public string UsedUnitID
        {
            set { _usedunitid = value; }
            get { return _usedunitid; }
        }
        public string UsedUnitCount
        {
            set { _usedunitcount = value; }
            get { return _usedunitcount; }
        }
        public string UsedPrice
        {
            set { _usedprice = value; }
            get { return _usedprice; }
        }
        public string ExRate
        {
            set { _exrate = value; }
            get { return _exrate; }
        }
        public string BatchNo//批次
        {
            set { _batchno = value; }
            get { return _batchno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InNo
        {
            set { _inno = value; }
            get { return _inno; }
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
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
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
        public string StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
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

        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }


        public string DefaultStorageID
        {
            set { _defaultstorageid = value; }
            get { return _defaultstorageid; }
        }
        #endregion Model

    }
}

