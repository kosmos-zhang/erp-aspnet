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
    ///
    public class PurchaseArriveModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _arriveno;
        private string _title;
        private string _fromtype;
        private int _providerid;
        private int _purchaser;
        private int _deptid;
        private int _paytype;
        private int _typeid;
        private int _taketype;
        private int _carrytype;
        private string _sendaddress;
        private string _receiveoveraddress;
        private int _checkuserid;
        private DateTime? _checkdate = null;
        private int _currencytype;
        private decimal _rate;
        private decimal _totalmoney;
        private decimal _totaltax;
        private decimal _totalfee;
        private decimal _discount;
        private decimal _discounttotal;
        private decimal _realtotal;
        private string _isaddtax;
        private decimal _counttotal;
        private string _remark;
        private string _billstatus;
        private int _creator;
        private DateTime? _createdate = null;
        private DateTime? _modifieddate = null;
        private string _modifieduserid;
        private int _confirmor;
        private DateTime? _confirmdate = null;
        private int _closer;
        private DateTime? _closedate = null;
        private decimal _othertotal;
        private string _attachment;
        private int _moneytype;
        private DateTime? _arrivedate = null;

        private string _CanUserID;
        private string _CanUserName;
        public string CanUserID
        {
            get { return _CanUserID; }
            set { _CanUserID = value; ;}
        }
        public string CanUserName
        {
            get { return _CanUserName; }
            set { _CanUserName = value; }
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
        /// 单据编号
        /// </summary>
        public string ArriveNo
        {
            set { _arriveno = value; }
            get { return _arriveno; }
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
        /// 源单类型（0无来源，1采购订单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public int ProviderID
        {
            set { _providerid = value; }
            get { return _providerid; }
        }
        /// <summary>
        /// 采购员（对应员工表ID）
        /// </summary>
        public int Purchaser
        {
            set { _purchaser = value; }
            get { return _purchaser; }
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
        /// 结算方式ID（对应分类代码表ID）
        /// </summary>
        public int PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
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
        /// 交货方式ID（对应分类代码表ID）
        /// </summary>
        public int TakeType
        {
            set { _taketype = value; }
            get { return _taketype; }
        }
        /// <summary>
        /// 运送方式ID（对应分类代码表ID）
        /// </summary>
        public int CarryType
        {
            set { _carrytype = value; }
            get { return _carrytype; }
        }
        /// <summary>
        /// 发货地址
        /// </summary>
        public string SendAddress
        {
            set { _sendaddress = value; }
            get { return _sendaddress; }
        }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ReceiveOverAddress
        {
            set { _receiveoveraddress = value; }
            get { return _receiveoveraddress; }
        }
        /// <summary>
        /// 点收人ID（对应员工表ID）
        /// </summary>
        public int CheckUserID
        {
            set { _checkuserid = value; }
            get { return _checkuserid; }
        }
        /// <summary>
        /// 点收日期
        /// </summary>
        public DateTime? CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 币种ID
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
        /// 金额合计
        /// </summary>
        public decimal TotalMoney
        {
            set { _totalmoney = value; }
            get { return _totalmoney; }
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
        /// 是否增值税（0否,1是）
        /// </summary>
        public string isAddTax
        {
            set { _isaddtax = value; }
            get { return _isaddtax; }
        }
        /// <summary>
        /// 订购数量合计
        /// </summary>
        public decimal CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
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
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
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
        public DateTime? ConfirmDate
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
        public DateTime? CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 其他费用合计
        /// </summary>
        public decimal OtherTotal
        {
            set { _othertotal = value; }
            get { return _othertotal; }
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
        /// 支付方式
        /// </summary>
        public int MoneyType
        {
            set { _moneytype = value; }
            get { return _moneytype; }
        }
        /// <summary>
        /// 到货时间
        /// </summary>
        public DateTime? ArriveDate
        {
            set { _arrivedate = value; }
            get { return _arrivedate; }
        }

        /// <summary>
        /// 所属项目ID
        /// </summary>
        private int? _projectID = null;

        /// <summary>
        /// 所属项目ID
        /// </summary>
        public int? ProjectID
        {
            set
            {
                _projectID = value;
            }
            get
            {
                return _projectID;
            }
        }

        #endregion Model
    }
}
