using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageAccountModel
    { 
        #region Model
        private int _id;
        private string _companycd;
        private int _billtype;
        private int _productid;
        private int _storageid;
        private string _batchno;
        private string _billno;
        private decimal _price;
        private DateTime _happendate;
        private decimal _happencount;
        private decimal _productcount;
        private string _pageurl;
        private int _creator;
        private string reMark;
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
        /// 单据类型
        /// </summary>
        public int BillType
        {
            set { _billtype = value; }
            get { return _billtype; }
        }
        /// <summary>
        /// 物品ID
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
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
        /// 单据编号
        /// </summary>
        public string BillNo
        {
            set { _billno = value; }
            get { return _billno; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 出入库时间
        /// </summary>
        public DateTime HappenDate
        {
            set { _happendate = value; }
            get { return _happendate; }
        }
        /// <summary>
        /// 出入库数量
        /// </summary>
        public decimal HappenCount
        {
            set { _happencount = value; }
            get { return _happencount; }
        }
        /// <summary>
        /// 现有存量
        /// </summary>
        public decimal ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 业务操作人(取当前登录人的ID)
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 页面链接地址
        /// </summary>
        public string PageUrl
        {
            set { _pageurl = value; }
            get { return _pageurl; }
        }


        /// <summary>
        /// 备注
        /// </summary>
        public string ReMark
        {
            set { reMark = value; }
            get { return reMark; }
        }
        #endregion Model
    }
}
