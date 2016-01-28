using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProjectBudget
{
    public class ProjectBudgetInfo
    {
        public ProjectBudgetInfo()
        { }

        /// <summary>
        /// 构造函数 ProjectBudget
        /// </summary>
        /// <param name="iD">ID</param>
        /// <param name="companyCD">CompanyCD</param>
        /// <param name="projectID">ProjectID</param>
        /// <param name="totalPrice">TotalPrice</param>
        /// <param name="budgetPerson">BudgetPerson</param>
        /// <param name="budgetTime">BudgetTime</param>
        public ProjectBudgetInfo(string companyCD, int projectID, decimal totalPrice, int budgetPerson, DateTime budgetTime,string userlist)
        {
            _companyCD = companyCD;
            _projectID = projectID;
            _totalPrice = totalPrice;
            _budgetPerson = budgetPerson;
            _budgetTime = budgetTime;
            _userlist = userlist;
        }

        #region Model
        private int _iD;
        private string _companyCD;
        private int _projectID;
        private decimal _totalPrice;
        private int _budgetPerson;
        private DateTime _budgetTime;
        private string _userlist;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _iD = value; }
            get { return _iD; }
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
        /// ProjectID
        /// </summary>
        public int ProjectID
        {
            set { _projectID = value; }
            get { return _projectID; }
        }
        /// <summary>
        /// TotalPrice
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalPrice = value; }
            get { return _totalPrice; }
        }
        /// <summary>
        /// BudgetPerson
        /// </summary>
        public int BudgetPerson
        {
            set { _budgetPerson = value; }
            get { return _budgetPerson; }
        }
        /// <summary>
        /// BudgetTime
        /// </summary>
        public DateTime BudgetTime
        {
            set { _budgetTime = value; }
            get { return _budgetTime; }
        }

        public string Userlist
        {
            set { _userlist = value; }
            get { return _userlist; }
        }
        #endregion Model
    }
}
