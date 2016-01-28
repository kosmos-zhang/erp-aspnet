using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageLossDetailModel
    {
        public StorageLossDetailModel()
        { }
        #region Model
        private string _id;
        private string _companycd;
        private string _lossno;
        private string _sortno;
        private string _productid;
        private string _unitid;
        private string _productcount;
        private string _unitprice;
        private string _costprice;
        private string _remark;
        private string _batchno;
        private string _usedunitid;
        private string _usedunitcount;
        private string _usedprice;
        private string _exrate;
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
        public string ID
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
        public string LossNo
        {
            set { _lossno = value; }
            get { return _lossno; }
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
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
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
        public string ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
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
        public string CostPrice
        {
            set { _costprice = value; }
            get { return _costprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}

