using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public  class SellChannelSttlModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _sttlno;
        private string _title;
        private int? _custid;
        private string _fromtype;
        private int? _paytype;
        private int? _moneytype;
        private int? _seller;
        private int? _selldeptid;
        private int? _currencytype;
        private decimal? _rate;
        private DateTime? _sttldate;
        private decimal? _counttotal;
        private decimal? _totalfee;
        private decimal? _pushmoneypercent;
        private decimal? _pushmoney;
        private decimal? _handfeetotal;
        private decimal? _sttltotal;
        private string _remark;
        private string _billstatus;
        private int? _confirmor;
        private DateTime? _confirmdate;
        private int? _closer;
        private DateTime? _closedate;
        private int? _creator;
        private DateTime? _createdate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private int? _frombillid;
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
        /// 代销结算单编号
        /// </summary>
        public string SttlNo
        {
            set { _sttlno = value; }
            get { return _sttlno; }
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
        /// 客户ID（客户表）
        /// </summary>
        public int? CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 源单类型（1发货通知单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
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
        /// 结算日期
        /// </summary>
        public DateTime? SttlDate
        {
            set { _sttldate = value; }
            get { return _sttldate; }
        }
        /// <summary>
        /// 本次结算代销数量合计
        /// </summary>
        public decimal? CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 本次结算代销金额合计
        /// </summary>
        public decimal? TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 代销提成率
        /// </summary>
        public decimal? PushMoneyPercent
        {
            set { _pushmoneypercent = value; }
            get { return _pushmoneypercent; }
        }
        /// <summary>
        /// 代销提成额
        /// </summary>
        public decimal? PushMoney
        {
            set { _pushmoney = value; }
            get { return _pushmoney; }
        }
        /// <summary>
        /// 代销手续费
        /// </summary>
        public decimal? HandFeeTotal
        {
            set { _handfeetotal = value; }
            get { return _handfeetotal; }
        }
        /// <summary>
        /// 应结金额合计
        /// </summary>
        public decimal? SttlTotal
        {
            set { _sttltotal = value; }
            get { return _sttltotal; }
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
        /// 来源单据ID（发货通知单）
        /// </summary>
        public int? FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        #endregion Model
    }
}
