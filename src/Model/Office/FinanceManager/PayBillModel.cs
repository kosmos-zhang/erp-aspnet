/**********************************************
 * 类作用：   PayBillModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/04/25
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类PayBillModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class PayBillModel
    {
        public PayBillModel()
        { }
        #region Model
        private int _id;
        private int _billingid;
        private string _companycd;
        private DateTime _creatdate;
        private string _payno;
        private string _custname;
        private decimal _payamount;
        private DateTime _paydate;
        private string _acceway;
        private int _executor;
        private string _bankname;
        private string _bankno;
        private string _confirmstatus;
        private int _confirmor;
        private DateTime _confirmdate;
        private string _isaccout;
        private DateTime _accountdate;
        private string _remark;
        private DateTime _modifieddate;
        private int _modifieduserid;
        private string _blendingtype;
        private int _currencytype;
        private decimal _currencyrate;
        private int _custid;
        private string _fromtbname;
        private string _filename;
        private int _projectid;
        /// <summary>
        /// 标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 业务单ID
        /// </summary>
        public int BillingID
        {
            set { _billingid = value; }
            get { return _billingid; }
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
        /// 创建日期
        /// </summary>
        public DateTime CreatDate
        {
            set { _creatdate = value; }
            get { return _creatdate; }
        }
        /// <summary>
        /// 付款单编码
        /// </summary>
        public string PayNo
        {
            set { _payno = value; }
            get { return _payno; }
        }
        /// <summary>
        /// 往来客户
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
        }
        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PayAmount
        {
            set { _payamount = value; }
            get { return _payamount; }
        }
        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime PayDate
        {
            set { _paydate = value; }
            get { return _paydate; }
        }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string AcceWay
        {
            set { _acceway = value; }
            get { return _acceway; }
        }
        /// <summary>
        /// 经办人
        /// </summary>
        public int Executor
        {
            set { _executor = value; }
            get { return _executor; }
        }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName
        {
            set { _bankname = value; }
            get { return _bankname; }
        }
        /// <summary>
        /// 银行帐号
        /// </summary>
        public string BankNo
        {
            set { _bankno = value; }
            get { return _bankno; }
        }
        /// <summary>
        /// 确认状态
        /// </summary>
        public string ConfirmStatus
        {
            set { _confirmstatus = value; }
            get { return _confirmstatus; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 是否登记凭证
        /// </summary>
        public string IsAccout
        {
            set { _isaccout = value; }
            get { return _isaccout; }
        }
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime AccountDate
        {
            set { _accountdate = value; }
            get { return _accountdate; }
        }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 最后修改日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }

        /// <summary>
        /// 勾兑类别
        /// </summary>
        public string BlendingType
        {
            set { _blendingtype= value; }
            get { return _blendingtype; }
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

        /// <summary>
        /// 往来客户主键
        /// </summary>
        public int CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 往来客户来源表
        /// </summary>
        public string FromTBName
        {
            set { _fromtbname = value; }
            get { return _fromtbname; }
        }
        /// <summary>
        /// 往来客户来源表名称字段
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }

        /// <summary>
        /// 所属项目
        /// </summary>
        public int ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }

        #endregion Model

    }
}

