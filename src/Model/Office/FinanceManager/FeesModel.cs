using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.FinanceManager
{
    public class FeesModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _feesno;
        private string _feesnum;
        private string _feestype;
        private string _invoicetype;
        private DateTime? _createdate;
        private string _contactunits;
        private string _contacttype;
        private string _acceway;
        private decimal _totalprice;
        private decimal _yaccounts;
        private decimal _naccounts;
        private string _executor;
        private string _deptid;
        private string _confirmstatus;
        private string _confirmor;
        private DateTime? _confirmdate;
        private string _isaccount;
        private string _accountor;
        private DateTime? _accountdate;
        private string _sourceno;
        private string _accountsstatus;
        private string _currencytype;
        private decimal _currencyrate;
        private string _note;
        private string _subjectsno;
        private string _projectid;

        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }

        /// <summary>
        /// 结算科目编码
        /// </summary>
        public string SubjectsNo
        {
            set { _subjectsno = value; }
            get { return _subjectsno; }
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
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string FeesNo
        {
            set { _feesno = value; }
            get { return _feesno; }
        }
        /// <summary>
        /// 发票号
        /// </summary>
        public string FeesNum
        {
            set { _feesnum = value; }
            get { return _feesnum; }
        }
        /// <summary>
        /// 费用票据类型（0.无来源，1.采购订单、2.采购到货通知单、3.采购退货单、4.销售订单、5.销售发货通知单、6.销售退货单、7.费用报销单）
        /// </summary>
        public string FeesType
        {
            set { _feestype = value; }
            get { return _feestype; }
        }
        /// <summary>
        /// 发票类型（1增值税发票，2普通地税，3普通国税，4收据）
        /// </summary>
        public string InvoiceType
        {
            set { _invoicetype = value; }
            get { return _invoicetype; }
        }
        /// <summary>
        /// 开票时间
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 往来单位ID
        /// </summary>
        public string ContactUnits
        {
            set { _contactunits = value; }
            get { return _contactunits; }
        }
        /// <summary>
        /// 往来单位类型（1.供应商、2.客户、3.职员）
        /// </summary>
        public string ContactType
        {
            set { _contacttype = value; }
            get { return _contacttype; }
        }
        /// <summary>
        /// 接收方式
        /// </summary>
        public string AcceWay
        {
            set { _acceway = value; }
            get { return _acceway; }
        }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal YAccounts
        {
            set { _yaccounts = value; }
            get { return _yaccounts; }
        }
        /// <summary>
        /// 未付金额
        /// </summary>
        public decimal NAccounts
        {
            set { _naccounts = value; }
            get { return _naccounts; }
        }
        /// <summary>
        /// 经办人
        /// </summary>
        public string Executor
        {
            set { _executor = value; }
            get { return _executor; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 确认状态（0，未确认 1，已确认）
        /// </summary>
        public string ConfirmStatus
        {
            set { _confirmstatus = value; }
            get { return _confirmstatus; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public string Confirmor
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
        /// 是否登记凭证（0，未登记 1，已登记）
        /// </summary>
        public string IsAccount
        {
            set { _isaccount = value; }
            get { return _isaccount; }
        }
        /// <summary>
        /// 登记人
        /// </summary>
        public string Accountor
        {
            set { _accountor = value; }
            get { return _accountor; }
        }
        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime? AccountDate
        {
            set { _accountdate = value; }
            get { return _accountdate; }
        }
        /// <summary>
        /// 来源表主键ID集（多个来源单据的ID用”,”号隔开）
        /// </summary>
        public string SourceNo
        {
            set { _sourceno = value; }
            get { return _sourceno; }
        }
        /// <summary>
        /// 结算状态（0：未结算1：已结算2：结算中）
        /// </summary>
        public string AccountsStatus
        {
            set { _accountsstatus = value; }
            get { return _accountsstatus; }
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
        public decimal CurrencyRate
        {
            set { _currencyrate = value; }
            get { return _currencyrate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }
        #endregion Model
    }
}
