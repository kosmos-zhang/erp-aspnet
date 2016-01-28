/**********************************************
 * 类作用：   财务预算相关表表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/09/10
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 类名：FinancialBudgetModel
    /// 描述：Financialbudgetbill表数据模板
    /// 作者：lysong
    /// 创建时间：2009/09/10
    /// </summary>
   public class FinancialBudgetModel
    {
        #region 财务预算实体类(主、明细表)
        #region 预算主字段属性
        private int _id;
        private string _companycd;
        private string _budgetcd;
        private string _title;
        private int _deptid;
        private DateTime _startdate;
        private DateTime _enddate;
        private string _billstatus;
        private int _creator;
        private DateTime _createdate;
        private string _paytype;
        private int _currencytype;
        private decimal _currencyrate;
        private decimal _budgetcost;
        private string _remark;
        private int _financialbudgettype;
        #endregion

        #region 预算明细字段属性
        private int _id_detail;
        private int _dudgetid_detail;
        private int _costtype_detail;
        private decimal _budgetcost_detail;
        private string _remark_detail;
        #endregion
        #region 预算主字段方法
        /// <summary>
        /// 自增ID
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
        /// 单据编号
        /// </summary>
        public string BudgetCD
        {
            set { _budgetcd = value; }
            get { return _budgetcd; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 制单状态
        /// </summary>
        public string Billstatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
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
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 支付方式（1现金，2银行存款）
        /// </summary>
        public string PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal CurrencyRate
        {
            set { _currencyrate = value; }
            get { return _currencyrate; }
        }
        /// <summary>
        /// 预算总费用
        /// </summary>
        public decimal Budgetcost
        {
            set { _budgetcost = value; }
            get { return _budgetcost; }
        }
       /// <summary>
        /// 预算类型
        /// </summary>
        public int FinancialBudgetType
        {
            set { _financialbudgettype = value; }
            get { return _financialbudgettype; }
        }
       
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
        #region 预算明细字段方法
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID_Detail
        {
            set { _id_detail = value; }
            get { return _id_detail; }
        }
        /// <summary>
        /// 预算单ID
        /// </summary>
        public int DudgetID_Detail
        {
            set { _dudgetid_detail = value; }
            get { return _dudgetid_detail; }
        }
        /// <summary>
        /// 费用类型（费用类别小类ID）
        /// </summary>
        public int CostType_Detail
        {
            set { _costtype_detail = value; }
            get { return _costtype_detail; }
        }
        /// <summary>
        /// 预算费用
        /// </summary>
        public decimal Budgetcost_Detail
        {
            set { _budgetcost_detail = value; }
            get { return _budgetcost_detail; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark_Detail
        {
            set { _remark_detail = value; }
            get { return _remark_detail; }
        }
        #endregion
        #endregion
    }
}
