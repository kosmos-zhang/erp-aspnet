using System;
 namespace XBase.Model.Office.FinanceManager
{
  public  class FixAssetPeriodDeprModel
    {
        private string _company;
        private string _fixno;
        private decimal _EndNetValue;
        private decimal _AmorDeprM;
        private decimal _AmorDeprRate;
        private decimal _TotalDeprPrice;

        public string CompanyCD
        {
            set { _company = value; }
            get { return _company; }
        }

        public string FixNo
        {
            set { _fixno = value; }
            get { return _fixno; }
        }

        public decimal EndNetValue
        {
            set { _EndNetValue = value; }
            get { return _EndNetValue; }
        }

        public decimal AmorDeprM
        {
            set { _AmorDeprM = value; }
            get { return _AmorDeprM; }
        }

        public decimal AmorDeprRate
        {
            set { _AmorDeprRate = value; }
            get { return _AmorDeprRate; }
        }

        public decimal TotalDeprPrice
        {
            set { _TotalDeprPrice = value; }
            get { return _TotalDeprPrice; }
        }
    }
}
