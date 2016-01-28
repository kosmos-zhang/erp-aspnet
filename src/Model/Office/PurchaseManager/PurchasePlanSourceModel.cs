using System;
namespace XBase.Model.Office.PurchaseManager
{
    public class PurchasePlanSourceModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _sortno;
        private string _planno;
        private string _fromtype;
        private string _frombillid;
        private string _fromsortno;
        private string _fromdeptid;
        private string _productid;
        private string _productno;
        private string _productname;
        private string _unitid;
        private string _unitprice;
        private string _totalprice;
        private string _requirecount;
        private string _requiredate;
        private string _providerid;
        private string _applyreason;
        private string _remark;
        private string _plancount;
        private string _plantakedate;
        private string _modifieddate;
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
        public string ID
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
        /// 行号
        /// </summary>
        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 采购计划编号（对应采购计划表中的单据编号）
        /// </summary>
        public string PlanNo
        {
            set { _planno = value; }
            get { return _planno; }
        }
        /// <summary>
        /// 源单类型（0无来源，1采购申请单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 源单ID
        /// </summary>
        public string FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 来源单据行号
        /// </summary>
        public string FromSortNo
        {
            set { _fromsortno = value; }
            get { return _fromsortno; }
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
        /// 物品ID（对应物品表ID）
        /// </summary>
        public string ProductID
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
        /// 单位ID
        /// </summary>
        public string UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 单价 
        /// </summary>
        public string UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public string TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 需求数量
        /// </summary>
        public string RequireCount
        {
            set { _requirecount = value; }
            get { return _requirecount; }
        }
        /// <summary>
        /// 需求日期
        /// </summary>
        public string RequireDate
        {
            set { _requiredate = value; }
            get { return _requiredate; }
        }
        /// <summary>
        /// 供应商ID(对应供应商表ID)
        /// </summary>
        public string ProviderID
        {
            set { _providerid = value; }
            get { return _providerid; }
        }
        /// <summary>
        /// 申请原因ID（对应原因表ID）
        /// </summary>
        public string ApplyReason
        {
            set { _applyreason = value; }
            get { return _applyreason; }
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
        /// 计划数量
        /// </summary>
        public string PlanCount
        {
            set { _plancount = value; }
            get { return _plancount; }
        }
        /// <summary>
        /// 计划交货日期
        /// </summary>
        public string PlanTakeDate
        {
            set { _plantakedate = value; }
            get { return _plantakedate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID(对应操作用户UserID)
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
