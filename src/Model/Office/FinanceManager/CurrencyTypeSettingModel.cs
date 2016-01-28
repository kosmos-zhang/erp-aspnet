/**********************************************
 * 类作用：   CurrencyTypeSettingModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/04/07
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类CurrencyTypeSettingModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class CurrencyTypeSettingModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _currencyname;
        private string _currencysymbol;
        private string _ismaster;
        private decimal _exchangerate;
        private int _convertway;
        private DateTime _changetime;
        private string _usedstatus;
        private decimal _endrate;
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
        /// 币种名称
        /// </summary>
        public string CurrencyName
        {
            set { _currencyname = value; }
            get { return _currencyname; }
        }
        /// <summary>
        /// 币符
        /// </summary>
        public string CurrencySymbol
        {
            set { _currencysymbol = value; }
            get { return _currencysymbol; }
        }
        /// <summary>
        /// 是否为本币（0否，1是）
        /// </summary>
        public string isMaster
        {
            set { _ismaster = value; }
            get { return _ismaster; }
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
        /// 折算方式
        /// </summary>
        public int ConvertWay
        {
            set { _convertway = value; }
            get { return _convertway; }
        }
        /// <summary>
        /// 汇率调整时间
        /// </summary>
        public DateTime ChangeTime
        {
            set { _changetime = value; }
            get { return _changetime; }
        }
        /// <summary>
        /// 启用状态（0停用，1启用）
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 期末汇率
        /// </summary>
        public decimal EndRate
        {
            set { _endrate = value; }
            get { return _endrate; }
        }
        #endregion Model

    }
}

