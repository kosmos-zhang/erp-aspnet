/***********************************************
 * 类作用：   PurchaseManager表数据模板        *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/15                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
    /// <summary>
    /// 类名：PurchaseArriveModel
    /// 描述：PurchaseArrive表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/15
    /// 最后修改时间：2009/04/15
    /// </summary>
    public class PurchaseOrderDetailModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _orderno;
        private string _sortno;
        private string _fromtype;
        private string _frombillid;
        private string _fromlineno;
        private string _productid;
        private string _productno;
        private string _productname;
        private string _productcount;
        private string _unitid;
        private string _unitprice;
        private string _taxprice;
        private string _discount;
        private string _taxrate;
        private string _totalfee;
        private string _totalprice;
        private string _totaltax;
        private string _requiredate = null;
        private string _applyreason;
        private string _remark;
        private string _arrivedcount;
        private string _modifieddate = null;
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
        /// 采购订单编号（对应采购订单表中的单据编号）
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        /// <summary>
        /// 序号（行号）
        /// </summary>
        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 源单类型（0无来源，1采购申请，2采购计划，3询价单，4采购合同）
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
        public string FromLineNo
        {
            set { _fromlineno = value; }
            get { return _fromlineno; }
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
        /// 采购数量
        /// </summary>
        public string ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
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
        /// 含税价
        /// </summary>
        public string TaxPrice
        {
            set { _taxprice = value; }
            get { return _taxprice; }
        }
        /// <summary>
        /// 折扣（%）
        /// </summary>
        public string Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 税率（%）
        /// </summary>
        public string TaxRate
        {
            set { _taxrate = value; }
            get { return _taxrate; }
        }
        /// <summary>
        /// 含税金额
        /// </summary>
        public string TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
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
        /// 税额
        /// </summary>
        public string TotalTax
        {
            set { _totaltax = value; }
            get { return _totaltax; }
        }
        /// <summary>
        /// 交货日期
        /// </summary>
        public string RequireDate
        {
            set { _requiredate = value; }
            get { return _requiredate; }
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
        /// 已到货数量（由到货通知单模块更新）
        /// </summary>
        public string ArrivedCount
        {
            set { _arrivedcount = value; }
            get { return _arrivedcount; }
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
