using System;
namespace XBase.Model.Office.StorageManager
{
    /// <summary>
    /// 实体类SubStorageAccountModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SubStorageAccountModel
    {
        public SubStorageAccountModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private int _deptid;
        private int _billtype;
        private int _productid;
        private string _batchno;
        private string _billno;
        private DateTime _happendate;
        private decimal _happencount;
        private decimal _productcount;
        private int _creator;
        private string _pageurl;
        private decimal _price;
        private string _remark;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 分店ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
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
        /// 单价
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
} 

