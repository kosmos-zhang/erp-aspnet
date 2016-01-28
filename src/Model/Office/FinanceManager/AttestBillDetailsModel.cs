/**********************************************
 * 类作用：   AttestBillDetailsModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/04/09
 * 修改于：2009/05/12
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类AttestBillDetailsModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class AttestBillDetailsModel
    {
        public AttestBillDetailsModel()
        { }
        #region Model
        private int _id;
        private int _attestbillid;
        private string _abstract;
        private string _subjectscd;
        private string _subjectsdetails;
        private int _currencytypeid;
        private decimal _exchangerate;
        private decimal _originalamount;
        private decimal _debitamount;
        private decimal _creditamount;
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
        /// 凭证号
        /// </summary>
        public int AttestBillID
        {
            set { _attestbillid = value; }
            get { return _attestbillid; }
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
        /// 原币金额
        /// </summary>
        public decimal OriginalAmount
        {
            set { _originalamount = value; }
            get { return _originalamount; }
        }
        /// <summary>
        /// 借方金额
        /// </summary>
        public decimal DebitAmount
        {
            set { _debitamount = value; }
            get { return _debitamount; }
        }
        /// <summary>
        /// 贷方金额
        /// </summary>
        public decimal CreditAmount
        {
            set { _creditamount = value; }
            get { return _creditamount; }
        }
        /// <summary>
        /// 辅助核算来源表
        /// </summary>
        public string FormTBName
        {
            set { _formtbname = value; }
            get { return _formtbname; }
        }
        /// <summary>
        /// 辅助核算项名称字段
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        #endregion Model

    }
}