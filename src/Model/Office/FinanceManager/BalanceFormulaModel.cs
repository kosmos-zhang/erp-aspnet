/**********************************************
 * 类作用：   BalanceFormulaModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/05/08
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类BalanceFormulaModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class BalanceFormulaModel
    {
        public BalanceFormulaModel()
        { }
        #region Model
        private int _id;
        private string _name;
        private int _line;
        private string _formula;
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
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 行号
        /// </summary>
        public int Line
        {
            set { _line = value; }
            get { return _line; }
        }
        /// <summary>
        /// 计算公式
        /// </summary>
        public string Formula
        {
            set { _formula = value; }
            get { return _formula; }
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

