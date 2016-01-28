using System;
namespace XBase.Model.Office.PurchaseManager
{
	/// <summary>
	/// 实体类PurchaseApplyDetailSource 。
	/// </summary>
	public class PurchaseApplyDetailSouceModel
	{
        public PurchaseApplyDetailSouceModel()
		{}
        #region Model
        private int _id;
        private string _companycd;
        private string _applyno;
        private int _sortno;
        private string _fromtype;
        private int _frombillid;
        private string _fromdeptid;
        private int _fromlineno;
        private int _productid;
        private string _productno;
        private string _productname;
        private int _unitid;
        private decimal _unitprice;
        private decimal _totalprice;
        private decimal _productcount;
        private DateTime _requiredate;
        private int _providerid;
        private string _remark;
        private decimal _plancount;
        private DateTime _plantakedate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _UsedUnitID;
        private string _UsedUnitCount;
        private string _UsedPrice;
        private string _ExRate;
        /// <summary>
        /// 实际单位
        /// </summary>
        public string UsedUnitID
        {
            set { _UsedUnitID = value; }
            get { return _UsedUnitID; }
        }
        /// <summary>
        /// 实际数量
        /// </summary>
        public string UsedUnitCount
        {
            set { _UsedUnitCount = value; }
            get { return _UsedUnitCount; }
        }
        /// <summary>
        /// 实际单价
        /// </summary>
        public string UsedPrice
        {
            set { _UsedPrice = value; }
            get { return _UsedPrice; }
        }

        /// <summary>
        /// 单位换算率
        /// </summary>
        public string ExRate
        {
            set { _ExRate = value; }
            get { return _ExRate; }
        }
        /// <summary>
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 采购申请编号（对应采购申请表中的单据编号）
        /// </summary>
        public string ApplyNo
        {
            set { _applyno = value; }
            get { return _applyno; }
        }
        /// <summary>
        /// 序号（行号）
        /// </summary>
        public int SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 源单类型（0无来源，1销售订单，2物料需求计划）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 源单ID
        /// </summary>
        public int FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 来源部门ID
        /// </summary>
        public string FromDeptID
        {
            set { _fromdeptid = value; }
            get { return _fromdeptid; }
        }
        /// <summary>
        /// 来源单据行号
        /// </summary>
        public int FromLineNo
        {
            set { _fromlineno = value; }
            get { return _fromlineno; }
        }
        /// <summary>
        /// 物品ID（对应物品表ID）
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 物品编号
        /// </summary>
        public string ProductNo
        {
            set { _productno = value; }
            get { return _productno; }
        }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 单位(对应计量单位表ID)
        /// </summary>
        public int UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 单价 
        /// </summary>
        public decimal UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 需求数量
        /// </summary>
        public decimal ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime RequireDate
        {
            set { _requiredate = value; }
            get { return _requiredate; }
        }
        /// <summary>
        /// 供应商ID(对应供应商表ID)
        /// </summary>
        public int ProviderID
        {
            set { _providerid = value; }
            get { return _providerid; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 申请数量
        /// </summary>
        public decimal PlanCount
        {
            set { _plancount = value; }
            get { return _plancount; }
        }
        /// <summary>
        /// 申请交货日期
        /// </summary>
        public DateTime PlanTakeDate
        {
            set { _plantakedate = value; }
            get { return _plantakedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedDate
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

