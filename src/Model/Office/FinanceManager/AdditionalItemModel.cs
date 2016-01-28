/**********************************************
 * 类作用：   AdditionalItemModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/06/01
 ***********************************************/

using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类AdditionalItemModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class AdditionalItemModel
    {
        public AdditionalItemModel()
        { }
        #region Model
        private int _id; 
        private string _duringdate;
        private string _itemname;
        private int _line;
        private decimal _currentamount;
        private decimal _preamount;
        private string _companycd;
        /// <summary>
        /// 标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 会计期间
        /// </summary>
        public string DuringDate
        {
            set { _duringdate = value; }
            get { return _duringdate; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        /// <summary>
        /// 行次
        /// </summary>
        public int Line
        {
            set { _line = value; }
            get { return _line; }
        }
        /// <summary>
        /// 本期金额
        /// </summary>
        public decimal CurrentAmount
        {
            set { _currentamount = value; }
            get { return _currentamount; }
        }
        /// <summary>
        /// 上期金额
        /// </summary>
        public decimal PreAmount
        {
            set { _preamount = value; }
            get { return _preamount; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        #endregion Model

    }
}


