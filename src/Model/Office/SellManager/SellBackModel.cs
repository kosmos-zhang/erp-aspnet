/***********************************
 * 修改记录：
 *  添加所属项目ID字段：ProjectID    于2010-3-31 by hexw
 * ********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public  class SellBackModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _title;
        private string _backno;
        private int? _custid;
        private string _fromtype;
        private int? _frombillid;
        private string _custtel;
        private string _busitype;
        private DateTime? _backdate;
        private int? _seller;
        private int? _selldeptid;
        private int? _carrytype;
        private string _sendaddress;
        private string _receiveaddress;
        private string _stockstatus;
        private int? _paytype;
        private int? _moneytype;
        private int? _currencytype;
        private decimal? _rate;
        private string _isaddtax;
        private decimal? _totalprice;
        private decimal? _tax;
        private decimal? _totalfee;
        private decimal? _discount;
        private decimal? _discounttotal;
        private decimal? _realtotal;
        private decimal? _counttotal;
        private decimal? _notpaytotal;
        private decimal? _backtotal;
        private string _remark;
        private string _billstatus;
        private int? _creator;
        private DateTime? _createdate;
        private int? _confirmor;
        private DateTime? _confirmdate;
        private int? _closer;
        private DateTime? _closedate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private int? _projectID;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public int ID
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
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 退货单编号
        /// </summary>
        public string BackNo
        {
            set { _backno = value; }
            get { return _backno; }
        }
        /// <summary>
        /// 客户ID（关联客户信息表）
        /// </summary>
        public int? CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 来源单据类型（0无来源，1发货通知单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 来源单据ID（发货通知单）
        /// </summary>
        public int? FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string CustTel
        {
            set { _custtel = value; }
            get { return _custtel; }
        }
        /// <summary>
        /// 业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨，6分期付款）
        /// </summary>
        public string BusiType
        {
            set { _busitype = value; }
            get { return _busitype; }
        }
        /// <summary>
        /// 退货日期
        /// </summary>
        public DateTime? BackDate
        {
            set { _backdate = value; }
            get { return _backdate; }
        }
        /// <summary>
        /// 业务员(对应员工表ID)
        /// </summary>
        public int? Seller
        {
            set { _seller = value; }
            get { return _seller; }
        }
        /// <summary>
        /// 部门(对部门表ID)
        /// </summary>
        public int? SellDeptId
        {
            set { _selldeptid = value; }
            get { return _selldeptid; }
        }
        /// <summary>
        /// 运送方式ID
        /// </summary>
        public int? CarryType
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
        public string ReceiveAddress
        {
            set { _receiveaddress = value; }
            get { return _receiveaddress; }
        }
        /// <summary>
        /// 入库状态（1货物已入库2货物未入库）
        /// </summary>
        public string StockStatus
        {
            set { _stockstatus = value; }
            get { return _stockstatus; }
        }
        /// <summary>
        /// 结算方式ID
        /// </summary>
        public int? PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 支付方式ID
        /// </summary>
        public int? MoneyType
        {
            set { _moneytype = value; }
            get { return _moneytype; }
        }
        /// <summary>
        /// 币种ID
        /// </summary>
        public int? CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal? Rate
        {
            set { _rate = value; }
            get { return _rate; }
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
        /// 金额合计
        /// </summary>
        public decimal? TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 税额合计
        /// </summary>
        public decimal? Tax
        {
            set { _tax = value; }
            get { return _tax; }
        }
        /// <summary>
        /// 含税金额合计
        /// </summary>
        public decimal? TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 整单折扣（%）
        /// </summary>
        public decimal? Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal? DiscountTotal
        {
            set { _discounttotal = value; }
            get { return _discounttotal; }
        }
        /// <summary>
        /// 折后含税金额合计
        /// </summary>
        public decimal? RealTotal
        {
            set { _realtotal = value; }
            get { return _realtotal; }
        }
        /// <summary>
        /// 退货数量合计
        /// </summary>
        public decimal? CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 抵应收账款
        /// </summary>
        public decimal? NotPayTotal
        {
            set { _notpaytotal = value; }
            get { return _notpaytotal; }
        }
        /// <summary>
        /// 应退货款总额
        /// </summary>
        public decimal? BackTotal
        {
            set { _backtotal = value; }
            get { return _backtotal; }
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
        /// 单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public int? Creator
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
        /// 确认人ID
        /// </summary>
        public int? Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime? ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 结单人ID
        /// </summary>
        public int? Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单日期
        /// </summary>
        public DateTime? CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
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
        /// 所属项目ID
        /// </summary>
        public int? ProjectID
        {
            set { _projectID = value; }
            get { return _projectID; }
        }
        #endregion Model
    }
}
