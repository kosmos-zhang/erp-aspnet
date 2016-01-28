using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
  public   class PercentIncomeTaxModel
    {
        #region Model
        private string _companycd;
        private string _minmoney;
        private string  _maxmoney;
        private string _taxpercent;
        private string _minusmoney;
        private string _id;
        private string  _taxlevel;
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 应缴税下限
        /// </summary>
        public string MinMoney
        {
            set { _minmoney = value; }
            get { return _minmoney; }
        }
        /// <summary>
        /// 应缴税上限
        /// </summary>
        public string  MaxMoney
        {
            set { _maxmoney = value; }
            get { return _maxmoney; }
        }
        /// <summary>
        /// 税率
        /// </summary>
        public string TaxPercent
        {
            set { _taxpercent = value; }
            get { return _taxpercent; }
        }
        /// <summary>
        /// 速算扣除数（元）
        /// </summary>
        public string MinusMoney
        {
            set { _minusmoney = value; }
            get { return _minusmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 级数
        /// </summary>
        public string  TaxLevel
        {
            set { _taxlevel = value; }
            get { return _taxlevel; }
        }
        #endregion Model

    }
}
