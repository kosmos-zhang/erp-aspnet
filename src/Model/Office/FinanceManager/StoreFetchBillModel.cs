/**********************************************
 * 类作用：   存取款但表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/04/28
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
  public class StoreFetchBillModel
    {
        private int _id;
        private string _companycd;
        private string _sfno;
        private string _sfbillnum;
        private DateTime _sfdate;
        private int _executor;
        private int _deptid;
        private string _accountno;
        private string _dirt;
        private string _type;
        private decimal _totalprice;
        private string _confirmstatus;
        private int _confirmor;
        private DateTime _confirmdate;
        private string _isaccount;
        private int _accountor;
        private string bankname;
        private DateTime _accountdate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _summary;

        private int _currencytype;
        private decimal _currencyrate;

        /// <summary>
        /// 银行名称
        /// </summary>
        public string  BankName
        {
            set { bankname = value; }
            get { return bankname; }
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
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 存取单编码
        /// </summary>
        public string SFNo
        {
            set { _sfno = value; }
            get { return _sfno; }
        }
        /// <summary>
        /// 收付款单票号
        /// </summary>
        public string SFBillNum
        {
            set { _sfbillnum = value; }
            get { return _sfbillnum; }
        }
        /// <summary>
        /// 收付款日期
        /// </summary>
        public DateTime SFDate
        {
            set { _sfdate = value; }
            get { return _sfdate; }
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
        /// 经办人部门
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 账户
        /// </summary>
        public string AccountNo
        {
            set { _accountno = value; }
            get { return _accountno; }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public string Dirt
        {
            set { _dirt = value; }
            get { return _dirt; }
        }
        /// <summary>
        /// 存取款类别
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 收付金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
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
        /// 
        /// </summary>
        public DateTime ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 是否登记凭证
        /// </summary>
        public string IsAccount
        {
            set { _isaccount = value; }
            get { return _isaccount; }
        }
        /// <summary>
        /// 登记人
        /// </summary>
        public int Accountor
        {
            set { _accountor = value; }
            get { return _accountor; }
        }
        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime AccountDate
        {
            set { _accountdate = value; }
            get { return _accountdate; }
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
        public string  ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
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

    }
}
