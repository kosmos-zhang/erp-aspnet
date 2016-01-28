using System;
namespace XBase.Model.Office.ProductionManager
{
    public  class ProcutionCostModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _periodnum;
        private int _productid;
        private string _accountmethod;
        private decimal _compratepro;
        private string _isinvestment;
        private decimal _finishedproductcount;
        private decimal _inprouctcount;
        private decimal _materialsunit;
        private decimal _wageunit;
        private decimal _overheadunit;
        private decimal _burningpowerunit;
        private decimal _currentmonthmaterials;
        private decimal _currentmonthhours;
        private decimal _endmonthhours;
        private decimal _endmonthmaterials;
        private int _currencytype;
        private decimal _currencyrate;
        private string _remark;
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
        public string PeriodNum
        {
            set { _periodnum = value; }
            get { return _periodnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AccountMethod
        {
            set { _accountmethod = value; }
            get { return _accountmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CompRatePro
        {
            set { _compratepro = value; }
            get { return _compratepro; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsInvestment
        {
            set { _isinvestment = value; }
            get { return _isinvestment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal FinishedProductCount
        {
            set { _finishedproductcount = value; }
            get { return _finishedproductcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal InProuctCount
        {
            set { _inprouctcount = value; }
            get { return _inprouctcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MaterialsUnit
        {
            set { _materialsunit = value; }
            get { return _materialsunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal WageUnit
        {
            set { _wageunit = value; }
            get { return _wageunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OverheadUnit
        {
            set { _overheadunit = value; }
            get { return _overheadunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal BurningPowerUnit
        {
            set { _burningpowerunit = value; }
            get { return _burningpowerunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CurrentMonthMaterials
        {
            set { _currentmonthmaterials = value; }
            get { return _currentmonthmaterials; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CurrentMonthHours
        {
            set { _currentmonthhours = value; }
            get { return _currentmonthhours; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal EndMonthHours
        {
            set { _endmonthhours = value; }
            get { return _endmonthhours; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal EndMonthMaterials
        {
            set { _endmonthmaterials = value; }
            get { return _endmonthmaterials; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CurrencyRate
        {
            set { _currencyrate = value; }
            get { return _currencyrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
