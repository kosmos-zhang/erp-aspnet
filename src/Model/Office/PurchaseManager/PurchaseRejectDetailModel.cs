/***********************************************
 * 类作用：   PurchaseManager表数据模板        *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/21                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
    /// <summary>
    /// 类名：PurchaseRejectDetailModel
    /// 描述：PurchaseRejectDetail表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/21
    /// 最后修改时间：2009/04/21
    /// </summary>
    ///
    public class PurchaseRejectDetailModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _rejectno;
        private string _fromtype;
        private int _sortno;
        private int _frombillid;
        private int _fromlineno;
        private int _productid;
        private string _productno;
        private string _productname;
        private decimal _productcount;
        private decimal _backcount;
        private int _unitid;
        private decimal _unitprice;
        private decimal _totalprice;
        private DateTime ? _requiredate;
        private int _applyreason;
        private decimal _outedtotal;
        private string _remark;
        private decimal _taxprice;
        private decimal _discount;
        private decimal _taxrate;
        private decimal _totalfee;
        private decimal _totaltax;
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
        /// 采购退货单编号（对应退货单表中的单据编号）
        /// </summary>
        public string RejectNo
        {
            set { _rejectno = value; }
            get { return _rejectno; }
        }
        /// <summary>
        /// 源单类型（0无来源，1到货通知单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
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
        /// 到货数量
        /// </summary>
        public decimal ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 退货数量
        /// </summary>
        public decimal BackCount
        {
            set { _backcount = value; }
            get { return _backcount; }
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
        /// 退货日期
        /// </summary>
        public DateTime ? RequireDate
        {
            set { _requiredate = value; }
            get { return _requiredate; }
        }
        /// <summary>
        /// 退货原因ID（对应原因表ID）
        /// </summary>
        public int ApplyReason
        {
            set { _applyreason = value; }
            get { return _applyreason; }
        }
        /// <summary>
        /// 已出库数量（由库存模块更新）
        /// </summary>
        public decimal OutedTotal
        {
            set { _outedtotal = value; }
            get { return _outedtotal; }
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
        /// 基本计量数量
        /// </summary>
        private decimal? _usedUnitCount = null;

        /// <summary>
        /// 基本计量数量
        /// </summary>
        public decimal? UsedUnitCount
        {
            get
            {
                return _usedUnitCount;
            }
            set
            {
                _usedUnitCount = value;
            }
        }
        /// <summary>
        /// 基本单位
        /// </summary>
        private int? _usedUnitID = null;

        /// <summary>
        /// 基本单位
        /// </summary>
        public int? UsedUnitID
        {
            get
            {
                return _usedUnitID;
            }
            set
            {
                _usedUnitID = value;
            }
        }

        /// <summary>
        /// 单位换算率
        /// </summary>
        private decimal? _exRate = null;

        /// <summary>
        /// 单位换算率
        /// </summary>
        public decimal? ExRate
        {
            get
            {
                return _exRate;
            }
            set
            {
                _exRate = value;
            }
        }

        /// <summary>
        /// 单价（对应于单位的单价，也就是明细中显示的实际单价）
        /// </summary>
        private decimal? _usedPrice = null;

        /// <summary>
        /// 单价（对应于单位的单价，也就是明细中显示的实际单价）
        /// </summary>
        public decimal? UsedPrice
        {
            get
            {
                return _usedPrice;
            }
            set
            {
                _usedPrice = value;
            }
        }
        #endregion Model
    }
}
