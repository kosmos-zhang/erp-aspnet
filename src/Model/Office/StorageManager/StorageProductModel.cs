using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    /// <summary>
    /// 实体类StorageProductModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class StorageProductModel
    {
        public StorageProductModel()
        { }
        #region Model
        private string _id;
        private string _companycd;
        private string _storageno;
        private string _storageid;
        private string _productid;
        private string _deptid;
        private string _productcount;
        private string _usecount;
        private string _ordercount;
        private string _roadcount;
        private string _OutCount;
        private string _incount;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
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
        public string StorageNo
        {
            set { _storageno = value; }
            get { return _storageno; }
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
        /// 
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
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
        public string UseCount
        {
            set { _usecount = value; }
            get { return _usecount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderCount
        {
            set { _ordercount = value; }
            get { return _ordercount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoadCount
        {
            set { _roadcount = value; }
            get { return _roadcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OutCount
        {
            set { _OutCount = value; }
            get { return _OutCount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InCount
        {
            set { _incount = value; }
            get { return _incount; }
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
        #endregion Model

    }
}
