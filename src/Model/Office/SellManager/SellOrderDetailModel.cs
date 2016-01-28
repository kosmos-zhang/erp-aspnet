/*************************************
 * 修改记录：
 *    1.添加字段：UsedUnitID int,UsedUnitCount Decimal(14,6),UsedPrice Decimal(14,6),ExRate Decimal(12,6).
 *      2010-4-8 add by hexw 
 * *********************************/
using System;

namespace XBase.Model.Office.SellManager
{
    /// <summary>
    /// 实体类SellOrderDetail 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class SellOrderDetailModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _orderno;
        private int? _sortno;
        private int? _productid;
        private decimal? _productcount;
        private int? _unitid;
        private decimal? _unitprice;
        private decimal? _taxprice;
        private decimal? _discount;
        private decimal? _taxrate;
        private decimal? _totalfee;
        private decimal? _totalprice;
        private decimal? _totaltax;
        private int? _sendtime;
        private int? _package;
        private string _remark;
        private decimal? _sendcount;
        private decimal? _planproductcount;
        private decimal? _usestockcount;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private int? _usedUnitID;
        private decimal? _usedUnitCount;
        private decimal? _usedPrice;
        private decimal? _exRate;
        /// <summary>
        /// ID，自动生成
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
        /// 订单编号
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        /// <summary>
        /// 序号（行号）
        /// </summary>
        public int? SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 物品ID（对应物品表ID）
        /// </summary>
        public int? ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 订购数量
        /// </summary>
        public decimal? ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 单位ID（对应计量单位ID）
        /// </summary>
        public int? UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }
        /// <summary>
        /// 含税价
        /// </summary>
        public decimal? TaxPrice
        {
            set { _taxprice = value; }
            get { return _taxprice; }
        }
        /// <summary>
        /// 折扣（%）
        /// </summary>
        public decimal? Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 税率（%）
        /// </summary>
        public decimal? TaxRate
        {
            set { _taxrate = value; }
            get { return _taxrate; }
        }
        /// <summary>
        /// 含税金额
        /// </summary>
        public decimal? TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 税额
        /// </summary>
        public decimal? TotalTax
        {
            set { _totaltax = value; }
            get { return _totaltax; }
        }
        /// <summary>
        /// 交货期限（天）
        /// </summary>
        public int? SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        /// <summary>
        /// 包装要求ID
        /// </summary>
        public int? Package
        {
            set { _package = value; }
            get { return _package; }
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
        /// 已通知数量（由销售发货通知单更新）
        /// </summary>
        public decimal? SendCount
        {
            set { _sendcount = value; }
            get { return _sendcount; }
        }
        /// <summary>
        /// 计划生产数量（由生产模块中的主生产计划更新）
        /// </summary>
        public decimal? PlanProductCount
        {
            set { _planproductcount = value; }
            get { return _planproductcount; }
        }
        /// <summary>
        /// 使用库存数量
        /// </summary>
        public decimal? UseStockCount
        {
            set { _usestockcount = value; }
            get { return _usestockcount; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime? ModifiedDate
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
        /// <summary>
        /// 单位
        /// </summary>
        public int? UsedUnitID
        {
            set { _usedUnitID = value; }
            get { return _usedUnitID; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? UsedUnitCount
        {
            set { _usedUnitCount = value; }
            get { return _usedUnitCount; }
        }
        /// <summary>
        /// 单价（对应于单位的单价，也就是明细中显示的实际单价）
        /// </summary>
        public decimal? UsedPrice
        {
            set { _usedPrice = value; }
            get { return _usedPrice; }
        }
        /// <summary>
        /// 换算率（单位与基本计量单位之间的换算比例，取自计量单位组表）
        /// </summary>
        public decimal? ExRate
        {
            set { _exRate = value; }
            get { return _exRate; }
        }
        #endregion Model
    }
}
