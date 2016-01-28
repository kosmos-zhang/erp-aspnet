/**********************************************
 * 类作用：  收款单表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/04/23
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
  public class IncomeBillModel
	{
        private int _id;
        private string _companycd;
        private string _incomeno;
        private DateTime _accedate;
        private string _custname;
        private int _billingid;
        private decimal _totalprice;
        private string _acceway;
        private string _bankname;
        private int _executor;
        private string _accountno;
        private string _confirmstatus;
        private int _confirmor;
        private DateTime _confirmdate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _summary;
        private string _blendingtype;
        private int _currencytype;
        private decimal _currencyrate;
        private int _custid;
        private string _fromtbname;
        private string _filename;
        private int _projectid;
        /// <summary>
        /// 自动生成
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
        /// 收款单编号
        /// </summary>
        public string InComeNo
        {
            set { _incomeno = value; }
            get { return _incomeno; }
        }
        /// <summary>
        /// 收款日期
        /// </summary>
        public DateTime AcceDate
        {
            set { _accedate = value; }
            get { return _accedate; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
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
        /// 收款金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 0:现金；1：银行存款
        /// </summary>
        public string AcceWay
        {
            set { _acceway = value; }
            get { return _acceway; }
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
        /// 经办人ID
        /// </summary>
        public int Executor
        {
            set { _executor = value; }
            get { return _executor; }
        }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string AccountNo
        {
            set { _accountno = value; }
            get { return _accountno; }
        }
        /// <summary>
        /// 确认状态0：未确认，1：已确认
        /// </summary>
        public string ConfirmStatus
        {
            set { _confirmstatus = value; }
            get { return _confirmstatus; }
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
        public DateTime ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiedDate
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
        /// 摘要
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
        }
        /// <summary>
        /// 勾兑类别
        /// </summary>
        public string BlendingType
        {
            set { _blendingtype = value; }
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

	}
}
