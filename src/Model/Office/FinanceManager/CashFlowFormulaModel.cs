/**********************************************
 * 类作用：   CashFlowFormulaModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/05/26
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类CashFlowFormulaModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class CashFlowFormulaModel
    {
        public CashFlowFormulaModel()
        { }
        #region Model
        private int _id;
        private string _name;
        private int _line;
        /// <summary>
        /// 标识 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 项目名称
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
        #endregion Model

    }
}

