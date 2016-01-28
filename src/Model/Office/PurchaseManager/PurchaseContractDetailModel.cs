/**********************************************
 * 类作用：   PurchaseManager表数据模板
 * 建立人：   宋飞
 * 建立时间： 2009/03/25
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
    /// <summary>
    /// 类名：PurchaseContractDetailModel
    /// 描述：PurchaseContract表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/03/25
    /// 最后修改时间：2009/03/25
    /// </summary>
    ///
    public class PurchaseContractDetailModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _contractno;
        private int _sortno;
        private string _fromtype;
        private int _frombillid;
        private int _fromlineno;
        private int _fromdeptid;
        private int _productid;
        private string _productno;
        private string _productname;
        private decimal _productcount;
        private int _unitid;
        private decimal _unitprice;
        private decimal _totalprice;
        private DateTime _requiredate;
        private int _applyreason;
        private string _remark;
        private decimal _taxprice;
        private decimal _discount;
        private decimal _taxrate;
        private decimal _totalfee;
        private decimal _totaltax;
        private string _standard;
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
        /// 采购合同编号（对应采采购合同表中的单据编号）
        /// </summary>
        public string ContractNo
        {
            set { _contractno = value; }
            get { return _contractno; }
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
        /// 源单类型（1采购申请，2采购计划，3询价单）
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
        /// 来源单据行号
        /// </summary>
        public int FromLineNo
        {
            set { _fromlineno = value; }
            get { return _fromlineno; }
        }
        /// <summary>
        /// 来源部门ID
        /// </summary>
        public int FromDeptID
        {
            set { _fromdeptid = value; }
            get { return _fromdeptid; }
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
        /// 需求（计划）数量
        /// </summary>
        public decimal ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 单位ID
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
        /// 需求（计划）日期
        /// </summary>
        public DateTime RequireDate
        {
            set { _requiredate = value; }
            get { return _requiredate; }
        }
        /// <summary>
        /// 申请原因ID（对应原因表ID）
        /// </summary>
        public int ApplyReason
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
        /// 含税价
        /// </summary>
        public decimal TaxPrice
        {
            set { _taxprice = value; }
            get { return _taxprice; }
        }
        /// <summary>
        /// 折扣（%）
        /// </summary>
        public decimal Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 税率（%）
        /// </summary>
        public decimal TaxRate
        {
            set { _taxrate = value; }
            get { return _taxrate; }
        }
        /// <summary>
        /// 含税金额
        /// </summary>
        public decimal TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 税额
        /// </summary>
        public decimal TotalTax
        {
            set { _totaltax = value; }
            get { return _totaltax; }
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string standard
        {
            set { _standard = value; }
            get { return _standard; }
        }
        #endregion Model

    }
}
