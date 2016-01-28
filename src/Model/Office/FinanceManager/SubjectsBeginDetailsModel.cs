/**********************************************
 * 类作用：   科目期初明细表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/06/19
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
 public  class SubjectsBeginDetailsModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _subjectscd;
        private string _dirt;
        private decimal _ytotaldebit;
        private decimal _ytotallenders;
        private decimal _originalcurrency;
        private decimal _standardcurrency;
        private decimal _beginmoney;
        private int _currencytype;
        private decimal _sumoriginalcurrency;
        private decimal _ytotaldebity;
        private decimal _ytotallendersy;
        private string _subjectsdetails;
        private string _formtbname;
        private string _filename;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubjectsCD
        {
            set { _subjectscd = value; }
            get { return _subjectscd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Dirt
        {
            set { _dirt = value; }
            get { return _dirt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal YTotalDebit
        {
            set { _ytotaldebit = value; }
            get { return _ytotaldebit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal YTotalLenders
        {
            set { _ytotallenders = value; }
            get { return _ytotallenders; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OriginalCurrency
        {
            set { _originalcurrency = value; }
            get { return _originalcurrency; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal StandardCurrency
        {
            set { _standardcurrency = value; }
            get { return _standardcurrency; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal BeginMoney
        {
            set { _beginmoney = value; }
            get { return _beginmoney; }
        }

        public int CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal SumOriginalCurrency
        {
            set { _sumoriginalcurrency = value; }
            get { return _sumoriginalcurrency; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal YTotalDebitY
        {
            set { _ytotaldebity = value; }
            get { return _ytotaldebity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal YTotalLendersY
        {
            set { _ytotallendersy = value; }
            get { return _ytotallendersy; }
        }

        /// <summary>
        /// 辅助核算主键
        /// </summary>
        public string SubjectsDetails
        {
            set { _subjectsdetails = value; }
            get { return _subjectsdetails; }
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
