/**********************************************
 * 类作用：   CashFlowFormulaDetailsModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/05/26
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类CashFlowFormulaDetailsModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class CashFlowFormulaDetailsModel
    {
        public CashFlowFormulaDetailsModel()
        { }
        #region Model
        private int _id;
        private int _formulaid;
        private string _subjectscd;
        private string _subjectsname;
        private string _direction; 
        private string _operator;
        private string _companycd;
        private string _operatorType;
        /// <summary>
        /// 标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 现金流量表项目ID
        /// </summary>
        public int FormulaID
        {
            set { _formulaid = value; }
            get { return _formulaid; }
        }
        /// <summary>
        /// 科目编码
        /// </summary>
        public string SubjectsCD
        {
            set { _subjectscd = value; }
            get { return _subjectscd; }
        }
        /// <summary>
        /// 科目名称
        /// </summary>
        public string SubjectsName
        {
            set { _subjectsname = value; }
            get { return _subjectsname; }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public string Direction
        {
            set { _direction = value; }
            get { return _direction; }
        }
        /// <summary>
        /// 运算符
        /// </summary>
        public string Operator
        {
            set { _operator = value; }
            get { return _operator; }
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
        /// 运算方式
        /// </summary>
        public string OperatorType
        {
            set {_operatorType=value; }
            get { return _operatorType;}
        }
        #endregion Model

    }
}

