using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProjectBudget
{
    public class BudgetSummary
    {
        public BudgetSummary()
        { }

        /// <summary>
        /// 构造函数 budgetSummary
        /// </summary>
        /// <param name="budgetID">budgetID</param>
        /// <param name="companyCD">CompanyCD</param>
        /// <param name="budgetName">BudgetName</param>
        /// <param name="budgetUnit">budgetUnit</param>
        /// <param name="budgetArea">budgetArea</param>
        public BudgetSummary(string companyCD, string budgetName, int budgetUnit, decimal budgetArea, int projectid, int seq, int subBudgetID)
        {
            _companyCD = companyCD;
            _budgetName = budgetName;
            _budgetUnit = budgetUnit;
            _budgetArea = budgetArea;
            _projectid = projectid;
            _seq = seq;
            _subBudgetID = subBudgetID;
        }

        public BudgetSummary(int budgetID, string companyCD, string budgetName, int budgetUnit, decimal budgetArea, int projectid, int seq, int subBudgetID)
        {
            _budgetID = budgetID;
            _companyCD = companyCD;
            _budgetName = budgetName;
            _budgetUnit = budgetUnit;
            _budgetArea = budgetArea;
            _projectid = projectid;
            _seq = seq;
            _subBudgetID = subBudgetID;
        }

        #region Model
        private int _budgetID;
        private string _companyCD;
        private string _budgetName;
        private int _budgetUnit;
        private decimal _budgetArea;
        private int _projectid;
        private int _seq;
        private int _subBudgetID; //分项预算概要ID
        /// <summary>
        /// 分项预算概要
        /// </summary>
        public int SubBudgetID
        {
            get
            {
                return _subBudgetID;
            }
            set
            {
                _subBudgetID = value;
            }
        }

        /// <summary>
        /// budgetID
        /// </summary>
        public int budgetID
        {
            set { _budgetID = value; }
            get { return _budgetID; }
        }
        /// <summary>
        /// CompanyCD
        /// </summary>
        public string CompanyCD
        {
            set { _companyCD = value; }
            get { return _companyCD; }
        }
        /// <summary>
        /// BudgetName
        /// </summary>
        public string BudgetName
        {
            set { _budgetName = value; }
            get { return _budgetName; }
        }
        /// <summary>
        /// budgetUnit
        /// </summary>
        public int budgetUnit
        {
            set { _budgetUnit = value; }
            get { return _budgetUnit; }
        }
        /// <summary>
        /// budgetArea
        /// </summary>
        public decimal budgetArea
        {
            set { _budgetArea = value; }
            get { return _budgetArea; }
        }

        public int projectid
        {
            set { _projectid = value; }
            get { return _projectid; }
        }

        public int seq
        {
            set { _seq = value; }
            get { return _seq; }
        }
        #endregion Model
    }
}
