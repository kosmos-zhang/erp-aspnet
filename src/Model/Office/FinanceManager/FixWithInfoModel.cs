/**********************************************
 * 类作用：   FixWithInfo表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/04/03
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
   public class FixWithInfoModel
    {
        private int _id;
        private string _companycd;
        private string _fixno;
        private DateTime _usedate;
        private string _countmethod;
        private decimal _estimateuse;
        private decimal _usedyear;
        private int _estiresivalue;
        private string _accudeprsubjecd;
        private string _deprcostsubjecd;
        private int _estiworkload;
        private decimal _amordeprrate; 
        private decimal _amordeprm;
        private decimal _currvaluere;
        private string _deprstatus;
        private decimal _endnetvalue;
        private int _monthworkload;
        private decimal _totaldeprprice;
        private int _depryears;
        private string _remark;
        /// <summary>
        /// 主键
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
        /// 固定资产编号
        /// </summary>
        public string FixNo
        {
            set { _fixno = value; }
            get { return _fixno; }
        }
        /// <summary>
        /// 使用日期
        /// </summary>
        public DateTime UseDate
        {
            set { _usedate = value; }
            get { return _usedate; }
        }
        /// <summary>
        /// 计提方法
        /// </summary>
        public string CountMethod
        {
            set { _countmethod = value; }
            get { return _countmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal EstimateUse
        {
            set { _estimateuse = value; }
            get { return _estimateuse; }
        }
        /// <summary>
        /// 预计使用年限
        /// </summary>
        public decimal UsedYear
        {
            set { _usedyear = value; }
            get { return _usedyear; }
        }
        /// <summary>
        /// 预计净残值率
        /// </summary>
        public int EstiResiValue
        {
            set { _estiresivalue = value; }
            get { return _estiresivalue; }
        }
        /// <summary>
        /// 累计折旧科目
        /// </summary>
        public string AccuDeprSubjeCD
        {
            set { _accudeprsubjecd = value; }
            get { return _accudeprsubjecd; }
        }
        /// <summary>
        /// 折旧费用科目
        /// </summary>
        public string DeprCostSubjeCD
        {
            set { _deprcostsubjecd = value; }
            get { return _deprcostsubjecd; }
        }
        /// <summary>
        /// 预计总工作量
        /// </summary>
        public int EstiWorkLoad
        {
            set { _estiworkload = value; }
            get { return _estiworkload; }
        }
        /// <summary>
        /// 待摊折旧率/月
        /// </summary>
        public decimal AmorDeprRate
        {
            set { _amordeprrate = value; }
            get { return _amordeprrate; }
        }
        /// <summary>
        /// 待摊折旧额/月
        /// </summary>
        public decimal AmorDeprM
        {
            set { _amordeprm = value; }
            get { return _amordeprm; }
        }
        /// <summary>
        /// 本期减值准备
        /// </summary>
        public decimal CurrValueRe
        {
            set { _currvaluere = value; }
            get { return _currvaluere; }
        }
        /// <summary>
        /// 折旧状态
        /// </summary>
        public string DeprStatus
        {
            set { _deprstatus = value; }
            get { return _deprstatus; }
        }
        /// <summary>
        /// 期末净值
        /// </summary>
        public decimal EndNetValue
        {
            set { _endnetvalue = value; }
            get { return _endnetvalue; }
        }
        /// <summary>
        /// 月工作量
        /// </summary>
        public int MonthWorkLoad
        {
            set { _monthworkload = value; }
            get { return _monthworkload; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalDeprPrice
        {
            set { _totaldeprprice = value; }
            get { return _totaldeprprice; }
        }
        /// <summary>
        /// 折旧年限
        /// </summary>
        public int DeprYears
        {
            set { _depryears = value; }
            get { return _depryears; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }
}
