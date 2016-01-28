/**********************************************
 * 类作用：   AcountBookModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/04/026
 ***********************************************/

using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类AcountBookModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class AcountBookModel
    {
        public AcountBookModel()
        { }
        #region Model
        private int _id;
        private string _accoutbookno;
        private DateTime _accountdate;
        private string _subjectscd;
        private string _subjectsdetails;
        private string _abstract;
        private string _direction;
        private int _attestbillid;
        private int _attestbilldetailsid;
        private DateTime _voucherdate;
        private int _currencytypeid;
        private decimal _exchangerate;
        private decimal _originalamount;
        private decimal _foreignbeginamount;
        private decimal _foreignthisdebit;
        private decimal _foreignthiscredit;
        private decimal _foreignendamount;
        private decimal _beginamount;
        private decimal _thisdebit;
        private decimal _thiscredit;
        private decimal _endamount;
        private string _companycd;
        private string _attestbillno;
        private string _formtbname;
        private string _filename;
        /// <summary>
        /// 标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 账簿编码
        /// </summary>
        public string AccoutBookNo
        {
            set { _accoutbookno = value; }
            get { return _accoutbookno; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime AccountDate
        {
            set { _accountdate = value; }
            get { return _accountdate; }
        }
        /// <summary>
        /// 会计科目
        /// </summary>
        public string SubjectsCD
        {
            set { _subjectscd = value; }
            get { return _subjectscd; }
        }
        /// <summary>
        /// 科目明细
        /// </summary>
        public string SubjectsDetails
        {
            set { _subjectsdetails = value; }
            get { return _subjectsdetails; }
        }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstract
        {
            set { _abstract = value; }
            get { return _abstract; }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public string Direction
        {
            set { _direction = value; }
            get { return _direction; }
        }
        /// <summary>
        /// 凭证主表ID
        /// </summary>
        public int AttestBillID
        {
            set { _attestbillid = value; }
            get { return _attestbillid; }
        }
        /// <summary>
        /// 凭证明细表ID
        /// </summary>
        public int AttestBillDetailsID
        {
            set { _attestbilldetailsid = value; }
            get { return _attestbilldetailsid; }
        }
        /// <summary>
        /// 凭证日期
        /// </summary>
        public DateTime VoucherDate
        {
            set { _voucherdate = value; }
            get { return _voucherdate; }
        }
        /// <summary>
        /// 币种ID
        /// </summary>
        public int CurrencyTypeID
        {
            set { _currencytypeid = value; }
            get { return _currencytypeid; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal ExchangeRate
        {
            set { _exchangerate = value; }
            get { return _exchangerate; }
        }
        /// <summary>
        /// 外币金额
        /// </summary>
        public decimal OriginalAmount
        {
            set { _originalamount = value; }
            get { return _originalamount; }
        }
        /// <summary>
        /// 外币期初余额
        /// </summary>
        public decimal ForeignBeginAmount
        {
            set { _foreignbeginamount = value; }
            get { return _foreignbeginamount; }
        }
        /// <summary>
        /// 外币本期借方金额
        /// </summary>
        public decimal ForeignThisDebit
        {
            set { _foreignthisdebit = value; }
            get { return _foreignthisdebit; }
        }
        /// <summary>
        /// 外币本期贷方金额
        /// </summary>
        public decimal ForeignThisCredit
        {
            set { _foreignthiscredit = value; }
            get { return _foreignthiscredit; }
        }
        /// <summary>
        /// 外币期末余额
        /// </summary>
        public decimal ForeignEndAmount
        {
            set { _foreignendamount = value; }
            get { return _foreignendamount; }
        }
        /// <summary>
        /// 综合本位币期初余额
        /// </summary>
        public decimal BeginAmount
        {
            set { _beginamount = value; }
            get { return _beginamount; }
        }
        /// <summary>
        /// 综合本位币本期借方金额
        /// </summary>
        public decimal ThisDebit
        {
            set { _thisdebit = value; }
            get { return _thisdebit; }
        }
        /// <summary>
        /// 综合本位币本期贷方金额
        /// </summary>
        public decimal ThisCredit
        {
            set { _thiscredit = value; }
            get { return _thiscredit; }
        }
        /// <summary>
        /// 综合本位币期末余额
        /// </summary>
        public decimal EndAmount
        {
            set { _endamount = value; }
            get { return _endamount; }
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
        /// 凭证号
        /// </summary>
        public string AttestBillNo
        {
            set { _attestbillno = value; }
            get { return _attestbillno; }
        }
        /// <summary>
        /// 辅助核算对应表名
        /// </summary>
        public string FormTBName
        {
            set { _formtbname = value; }
            get { return _formtbname; }
        }
        /// <summary>
        /// 辅助核算对应字段名
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        #endregion Model

    }
}

