/**********************************************
 * 类作用：   BlendingDetailsModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/06/27
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类BlendingDetailsModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class BlendingDetailsModel
    {
        public BlendingDetailsModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _payorincometype;
        private int _billingid;
        private string _sourcedt;
        private int _sourceid;
        private string _billcd;
        private string _billingtype;
        private string _invoicetype;
        private decimal _totalprice;
        private decimal _yaccounts;
        private decimal _naccounts;
        private DateTime _createdate;
        private string _contactunits;
        private DateTime _executdate;
        private int _executor;
        private string _status;
        private int _currencytype;
        private decimal _currencyrate;
        /// <summary>
        /// 标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 来源表类型 1.付款单 2.收款单
        /// </summary>
        public string PayOrInComeType
        {
            set { _payorincometype = value; }
            get { return _payorincometype; }
        }
        /// <summary>
        /// 业务单表ID
        /// </summary>
        public int BillingID
        {
            set { _billingid = value; }
            get { return _billingid; }
        }
        /// <summary>
        /// 源单表
        /// </summary>
        public string SourceDt
        {
            set { _sourcedt = value; }
            get { return _sourcedt; }
        }
        /// <summary>
        /// 源单表ID
        /// </summary>
        public int SourceID
        {
            set { _sourceid = value; }
            get { return _sourceid; }
        }
        /// <summary>
        /// 源单编码
        /// </summary>
        public string BillCD
        {
            set { _billcd = value; }
            get { return _billcd; }
        }
        /// <summary>
        /// 源单类别 0：销售订单1：采购订单2：销售退货单3：代销结算单4：采购退货单
        /// </summary>
        public string BillingType
        {
            set { _billingtype = value; }
            get { return _billingtype; }
        }
        /// <summary>
        /// 发票类型  1增值税发票，2普通地税，3普通国税，4收据
        /// </summary>
        public string InvoiceType
        {
            set { _invoicetype = value; }
            get { return _invoicetype; }
        }
        /// <summary>
        /// 源单总金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 已勾兑金额
        /// </summary>
        public decimal YAccounts
        {
            set { _yaccounts = value; }
            get { return _yaccounts; }
        }
        /// <summary>
        /// 未勾兑金额
        /// </summary>
        public decimal NAccounts
        {
            set { _naccounts = value; }
            get { return _naccounts; }
        }
        /// <summary>
        /// 勾兑日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 往来客户
        /// </summary>
        public string ContactUnits
        {
            set { _contactunits = value; }
            get { return _contactunits; }
        }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime ExecutDate
        {
            set { _executdate = value; }
            get { return _executdate; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public int Executor
        {
            set { _executor = value; }
            get { return _executor; }
        }
        /// <summary>
        /// 状态  0：未勾兑完成 1：勾兑完成
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal CurrencyRate
        {
            set { _currencyrate = value; }
            get { return _currencyrate; }
        }

        #endregion Model

    }
}

