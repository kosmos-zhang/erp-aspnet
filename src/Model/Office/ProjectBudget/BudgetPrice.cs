using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProjectBudget
{
    public class BudgetPrice
    {
        public BudgetPrice()
        { }

        /// <summary>
        /// 构造函数 budgetPrice
        /// </summary>
        /// <param name="budgetpriceID">budgetpriceID</param>
        /// <param name="companyCD">CompanyCD</param>
        /// <param name="budgetPriceName">BudgetPriceName</param>
        /// <param name="unitPrice">UnitPrice</param>
        /// <param name="formula">Formula</param>
        /// <param name="projectID">projectID</param>
        /// <param name="codeType">codeType</param>
        public BudgetPrice(string companyCD, string budgetPriceName, decimal unitPrice, string formula, int projectID, int codeType)
        {
            _companyCD = companyCD;
            _budgetPriceName = budgetPriceName;
            _unitPrice = unitPrice;
            _formula = formula;
            _projectID = projectID;
            _codeType = codeType;
        }

        #region Model
        private int _budgetpriceID;
        private string _companyCD;
        private string _budgetPriceName;
        private decimal _unitPrice;
        private string _formula;
        private int _projectID;
        private int _codeType;
        /// <summary>
        /// budgetpriceID
        /// </summary>
        public int budgetpriceID
        {
            set { _budgetpriceID = value; }
            get { return _budgetpriceID; }
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
        /// BudgetPriceName
        /// </summary>
        public string BudgetPriceName
        {
            set { _budgetPriceName = value; }
            get { return _budgetPriceName; }
        }
        /// <summary>
        /// UnitPrice
        /// </summary>
        public decimal UnitPrice
        {
            set { _unitPrice = value; }
            get { return _unitPrice; }
        }
        /// <summary>
        /// Formula
        /// </summary>
        public string Formula
        {
            set { _formula = value; }
            get { return _formula; }
        }
        /// <summary>
        /// projectID
        /// </summary>
        public int projectID
        {
            set { _projectID = value; }
            get { return _projectID; }
        }
        /// <summary>
        /// codeType
        /// </summary>
        public int codeType
        {
            set { _codeType = value; }
            get { return _codeType; }
        }
        #endregion Model
    }
}
