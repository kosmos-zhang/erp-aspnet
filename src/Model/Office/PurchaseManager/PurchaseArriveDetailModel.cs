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
    /// 类名：PurchaseArriveDetailModel
    /// 描述：PurchaseArrive表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/15
    /// 最后修改时间：2009/04/15
    /// </summary>
    ///
    public class PurchaseArriveDetailModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _arriveno;
        private int _sortno;
        private string _fromtype;
        private int _frombillid;
        private int _fromlineno;
        private int _productid;
        private string _productno;
        private string _productname;
        private decimal _productcount;
        private int _unitid;
        private decimal _unitprice;
        private decimal _taxprice;
        private decimal _discount;
        private decimal _taxrate;
        private decimal _totalfee;
        private decimal _totalprice;
        private decimal _totaltax;
        private int _storageid;
        private decimal _applycheckcount;
        private decimal _checkedcount;
        private decimal _passcount;
        private decimal _notpasscount;
        private decimal _incount;
        private decimal _rejectcount;
        private decimal _backcount;
        private string _remark;
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
        /// 采购到货单编号（对应到货单表中的单据编号）
        /// </summary>
        public string ArriveNo
        {
            set { _arriveno = value; }
            get { return _arriveno; }
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
        /// 源单类型（0无来源，1采购订单）
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
        /// 金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
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
        /// 仓库ID(对应仓库表ID)
        /// </summary>
        public int StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 已报质检数量(由质检模块更新)
        /// </summary>
        public decimal ApplyCheckCount
        {
            set { _applycheckcount = value; }
            get { return _applycheckcount; }
        }
        /// <summary>
        /// 实检数量(由质检模块更新)
        /// </summary>
        public decimal CheckedCount
        {
            set { _checkedcount = value; }
            get { return _checkedcount; }
        }
        /// <summary>
        /// 合格数量(由质检模块更新)
        /// </summary>
        public decimal PassCount
        {
            set { _passcount = value; }
            get { return _passcount; }
        }
        /// <summary>
        /// 不合格数量(由质检模块更新)
        /// </summary>
        public decimal NotPassCount
        {
            set { _notpasscount = value; }
            get { return _notpasscount; }
        }
        /// <summary>
        /// 已入库数量(由库存模块入库时更新)
        /// </summary>
        public decimal InCount
        {
            set { _incount = value; }
            get { return _incount; }
        }
        /// <summary>
        /// 拒收数量（由质检模块更新）
        /// </summary>
        public decimal RejectCount
        {
            set { _rejectcount = value; }
            get { return _rejectcount; }
        }
        /// <summary>
        /// 退货数量（由采购退货模块更新）
        /// </summary>
        public decimal BackCount
        {
            set { _backcount = value; }
            get { return _backcount; }
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
