/**********************************************
 * 类作用：   FixAssetDeprDetail表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/05/7
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
   public class FixAssetDeprDetailModel
    {
        private int _id;
        private string _companycd;
        private string _fixno;
        private string _fixname;
        private int _fixtype;
        private DateTime _useddate;
        private DateTime _deprdate;
        private int _number;
        private decimal _usedyears;
        private decimal _estimateuse;
        private decimal _originalvalue;
        private decimal _mdeprprice;
        private decimal _totaldeprprice;
        private decimal _endnetvalue;
        private decimal _totalimpairment;
        private int _creator;

        /// <summary>
        /// 主键
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
        /// 固定资产编号
        /// </summary>
        public string FixNo
        {
            set { _fixno = value; }
            get { return _fixno; }
        }
        /// <summary>
        /// 资产名称
        /// </summary>
        public string FixName
        {
            set { _fixname = value; }
            get { return _fixname; }
        }
        /// <summary>
        /// 资产类别
        /// </summary>
        public int FixType
        {
            set { _fixtype = value; }
            get { return _fixtype; }
        }
        /// <summary>
        /// 使用日期
        /// </summary>
        public DateTime UsedDate
        {
            set { _useddate = value; }
            get { return _useddate; }
        }
        /// <summary>
        /// 折旧日期
        /// </summary>
        public DateTime DeprDate
        {
            set { _deprdate = value; }
            get { return _deprdate; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 使用年限
        /// </summary>
        public decimal UsedYears
        {
            set { _usedyears = value; }
            get { return _usedyears; }
        }
        /// <summary>
        /// 原值
        /// </summary>
        public decimal OriginalValue
        {
            set { _originalvalue = value; }
            get { return _originalvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MDeprPrice
        {
            set { _mdeprprice = value; }
            get { return _mdeprprice; }
        }
        /// <summary>
        /// 累计折旧额
        /// </summary>
        public decimal TotalDeprPrice
        {
            set { _totaldeprprice = value; }
            get { return _totaldeprprice; }
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
        /// 累计减值
        /// </summary>
        public decimal TotalImpairment
        {
            set { _totalimpairment = value; }
            get { return _totalimpairment; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }

        /// <summary>
        /// 预计使用年限
        /// </summary>
        public decimal EstimateUse
        {
            set { _estimateuse = value; }
            get { return _estimateuse; }
        }

       
    }
}
