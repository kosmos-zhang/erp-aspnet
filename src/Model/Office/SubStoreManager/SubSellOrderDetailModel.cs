/***********************************************
 * 类作用：   SubStorageManager表数据模板      *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/05/18                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SubStoreManager
{
    /// <summary>
    /// 类名：SubSellOrderDetailModel
    /// 描述：SubSellOrderDetail表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/18
    /// 最后修改时间：2009/05/27
    /// </summary>
    ///
    public class SubSellOrderDetailModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _deptid;
        private string _orderno;
        private string _sortno;
        private string _productid;
        private string _storageid;
        private string _productcount;
        private string _unitid;
        private string _unitprice;
        private string _taxprice;
        private string _discount;
        private string _taxrate;
        private string _totalfee;
        private string _totalprice;
        private string _totaltax;
        private string _remark;
        private decimal _backcount;
        private int _usedunitid;
        private decimal _usedunitcount;
        private decimal _usedprice;
        private decimal _exrate;

        /// <summary>
        /// 
        /// </summary>
        public int UsedUnitID
        {
            set { _usedunitid = value; }
            get { return _usedunitid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal UsedUnitCount
        {
            set { _usedunitcount = value; }
            get { return _usedunitcount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal UsedPrice
        {
            set { _usedprice = value; }
            get { return _usedprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ExRate
        {
            set { _exrate = value; }
            get { return _exrate; }
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
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 分店ID（对应组织机构表ID）
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
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
        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
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
        /// 出货仓库（对应仓库表ID）
        /// </summary>
        public string StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 订购数量
        /// </summary>
        public string ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 单位ID（对应计量单位ID）
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
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 退货数量（由门店销售退货模块更新）
        /// </summary>
        public decimal BackCount
        {
            set { _backcount = value; }
            get { return _backcount; }
        }



        /// <summary>
        /// 批次
        /// </summary>
        private string _batchNo = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo
        {
            get
            {
                return _batchNo;
            }
            set
            {
                _batchNo = value;
            }
        }
        #endregion Model
    }
}
