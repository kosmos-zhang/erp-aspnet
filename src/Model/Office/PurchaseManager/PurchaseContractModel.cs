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
    /// 类名：PurchaseContractModel
    /// 描述：PurchaseContract表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/03/25
    /// 最后修改时间：2009/03/25
    /// </summary>
    ///
   public class PurchaseContractModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _contractno;
        private string _title;
        private int _providerid;
        private string _fromtype;
        private int _frombillid;
        private decimal _totalprice;
        private int _currencytype;
        private decimal _rate;
        private int _paytype;
        private DateTime ? _signdate = null;
        private int _seller;
        private string _theydelegate;
        private int _ourdelegate;
        private string _status;
        private string _note;
        private int _taketype;
        private int _carrytype;
        private string _attachment;
        private string _remark;
        private string _billstatus;
        private int _creator;
        private DateTime ? _createdate = null;
        private DateTime ? _modifieddate = null;
        private string _modifieduserid;
        private int _confirmor;
        private DateTime ? _confirmdate = null;
        private int _closer;
        private DateTime ? _closedate = null;
        private int _deptid;
        private decimal _totaltax;
        private decimal _totalfee;
        private decimal _discount;
        private decimal _discounttotal;
        private decimal _realtotal;
        private string _isaddtax;
        private decimal _counttotal;
        private int _typeid;
        private string _signaddr;
        private int _moneytype;
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
        /// 合同编号
        /// </summary>
        public string ContractNo
        {
            set { _contractno = value; }
            get { return _contractno; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 供应商ID（对应供应商表ID）
        /// </summary>
        public int ProviderID
        {
            set { _providerid = value; }
            get { return _providerid; }
        }
        /// <summary>
        /// 源单类型（0无来源，1采购申请，2采购计划，3询价单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 源单ID（采购计划ID或询价单ID）
        /// </summary>
        public int FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 合同总金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 币种ID(对应货币代码表CD)
        /// </summary>
        public int CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 结算方式ID
        /// </summary>
        public int PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 签约时间
        /// </summary>
        public DateTime ? SignDate
        {
            set { _signdate = value; }
            get { return _signdate; }
        }
        /// <summary>
        /// 业务员ID(对应员工表ID)
        /// </summary>
        public int Seller
        {
            set { _seller = value; }
            get { return _seller; }
        }
        /// <summary>
        /// 对方签约人
        /// </summary>
        public string TheyDelegate
        {
            set { _theydelegate = value; }
            get { return _theydelegate; }
        }
        /// <summary>
        /// 我方签约人ID
        /// </summary>
        public int OurDelegate
        {
            set { _ourdelegate = value; }
            get { return _ourdelegate; }
        }
        /// <summary>
        /// 合同状态：1：执行中2：执行完3：终止
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 终止原因
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 交货方式ID
        /// </summary>
        public int TakeType
        {
            set { _taketype = value; }
            get { return _taketype; }
        }
        /// <summary>
        /// 运送方式ID
        /// </summary>
        public int CarryType
        {
            set { _carrytype = value; }
            get { return _carrytype; }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime ? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ? ModifiedDate
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
        /// 确认人ID
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime ? ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 结单人
        /// </summary>
        public int Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单时间
        /// </summary>
        public DateTime ? CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 部门（对应部门表ID）
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 税额合计
        /// </summary>
        public decimal TotalTax
        {
            set { _totaltax = value; }
            get { return _totaltax; }
        }
        /// <summary>
        /// 含税金额合计
        /// </summary>
        public decimal TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 整单折扣（%）
        /// </summary>
        public decimal Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal DiscountTotal
        {
            set { _discounttotal = value; }
            get { return _discounttotal; }
        }
        /// <summary>
        /// 折后含税金额
        /// </summary>
        public decimal RealTotal
        {
            set { _realtotal = value; }
            get { return _realtotal; }
        }
        /// <summary>
        /// 是否增值税（0否,1是)
        /// </summary>
        public string isAddTax
        {
            set { _isaddtax = value; }
            get { return _isaddtax; }
        }
        /// <summary>
        /// 采购数量合计
        /// </summary>
        public decimal CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 采购类别ID（分类代码表ID）
        /// </summary>
        public int TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 签约地点
        /// </summary>
        public string SignAddr
        {
            set { _signaddr = value; }
            get { return _signaddr; }
        }
        /// <summary>
        /// 支付方式ID（对应分类代码表ID）
        /// </summary>
        public int MoneyType
        {
            set { _moneytype = value; }
            get { return _moneytype; }
        }
        #endregion Model

    }
}
