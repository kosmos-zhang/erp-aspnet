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
    /// 类名：PurchaseOrderModel
    /// 描述：PurchaseOrder表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/15
    /// 最后修改时间：2009/04/15
    /// </summary>
     public class PurchaseOrderModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _orderno;
        private string _title;
        private string _typeid;
        private string _fromtype;
        private string _currencytype;
        private string _rate;
        private string _purchasedate;
        private string _orderdate;
        private string _theydelegate;
        private string _ourdelegate;
        private string _providerid;
        private string _deptid;
        private string _paytype;
        private string _moneytype;
        private string _purchaser;
        private string _taketype;
        private string _carrytype;
        private string _providerbillid;
        private string _remark;
        private string _totalprice;
        private string _totaltax;
        private string _totalfee;
        private string _discount;
        private string _discounttotal;
        private string _othertotal;
        private string _realtotal;
        private string _isaddtax;
        private string _counttotal;
        private string _isopenbill;
        private string _confirmor;
        private string _confirmdate;
        private string _closer;
        private string _closedate;
        private string _billstatus;
        private string _creator;
        private string _createdate;
        private string _modifieddate;
        private string _modifieduserid;
        private string _attachment;
        private string _flowstatus;
        private string _ProjectID;

        private string _CanUserID;
        private string _CanUserName;

        /// <summary>
        /// 可查看人员ID
        /// </summary>
        public string CanUserID
        {
            get { return _CanUserID; }
            set { _CanUserID = value; }
        }
        /// <summary>
        /// 可查看人名称
        /// </summary>
        public string CanUserName
        {
            get { return _CanUserName; }
            set { _CanUserName = value; }
        }

      /// <summary>
        ///    项目ID
      /// </summary>
        public string ProjectID
        {
            get {return _ProjectID  ;}
            set { _ProjectID =value ;}
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
        /// 采购单编号
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        public string EFIndex
        { get; set; }
        public string EFDesc
        { get; set; }
        /// <summary>
        /// 采购单主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 采购类别ID（分类代码表ID）
        /// </summary>
        public string TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
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
        /// 币种ID
        /// </summary>
        public string CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public string Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 下单日期
        /// </summary>
        public string PurchaseDate
        {
            set { _purchasedate = value; }
            get { return _purchasedate; }
        }
        /// <summary>
        /// 签单日期
        /// </summary>
        public string OrderDate
        {
            set { _orderdate = value; }
            get { return _orderdate; }
        }
        /// <summary>
        /// 对方代表
        /// </summary>
        public string TheyDelegate
        {
            set { _theydelegate = value; }
            get { return _theydelegate; }
        }
        /// <summary>
        /// 我方代表（员工表ID）
        /// </summary>
        public string OurDelegate
        {
            set { _ourdelegate = value; }
            get { return _ourdelegate; }
        }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public string ProviderID
        {
            set { _providerid = value; }
            get { return _providerid; }
        }
        /// <summary>
        /// 部门（对应部门表ID）
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 结算方式ID
        /// </summary>
        public string PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 支付方式ID（对应分类代码表ID）
        /// </summary>
        public string MoneyType
        {
            set { _moneytype = value; }
            get { return _moneytype; }
        }
        /// <summary>
        /// 采购员ID
        /// </summary>
        public string Purchaser
        {
            set { _purchaser = value; }
            get { return _purchaser; }
        }
        /// <summary>
        /// 交货方式ID
        /// </summary>
        public string TakeType
        {
            set { _taketype = value; }
            get { return _taketype; }
        }
        /// <summary>
        /// 运送方式ID
        /// </summary>
        public string CarryType
        {
            set { _carrytype = value; }
            get { return _carrytype; }
        }
        /// <summary>
        /// 供方订单号
        /// </summary>
        public string ProviderBillID
        {
            set { _providerbillid = value; }
            get { return _providerbillid; }
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
        /// 金额合计
        /// </summary>
        public string TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 税额合计
        /// </summary>
        public string TotalTax
        {
            set { _totaltax = value; }
            get { return _totaltax; }
        }
        /// <summary>
        /// 含税金额合计
        /// </summary>
        public string TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 整单折扣（%）
        /// </summary>
        public string Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public string DiscountTotal
        {
            set { _discounttotal = value; }
            get { return _discounttotal; }
        }
        /// <summary>
        /// 其他费用支出合计
        /// </summary>
        public string OtherTotal
        {
            set { _othertotal = value; }
            get { return _othertotal; }
        }
        /// <summary>
        /// 折后含税金额
        /// </summary>
        public string RealTotal
        {
            set { _realtotal = value; }
            get { return _realtotal; }
        }
        /// <summary>
        /// 是否增值税（0否,1是
        /// </summary>
        public string isAddTax
        {
            set { _isaddtax = value; }
            get { return _isaddtax; }
        }
        /// <summary>
        /// 订购数量合计
        /// </summary>
        public string CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 是否已开票（0否，1是）（由开票管理更新）
        /// </summary>
        public string isOpenbill
        {
            set { _isopenbill = value; }
            get { return _isopenbill; }
        }
        /// <summary>
        /// 确认人ID
        /// </summary>
        public string Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        public string ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 结单人
        /// </summary>
        public string Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单时间
        /// </summary>
        public string CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
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
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
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
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        } 
        /// <summary>
        /// 审批状态
        /// </summary>
        public string FlowStatus
        {
            set { _flowstatus = value; }
            get { return _flowstatus; }
        }    
     
        #endregion Model
    }
}
