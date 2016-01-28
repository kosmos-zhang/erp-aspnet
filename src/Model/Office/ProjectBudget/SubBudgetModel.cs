/****************************
 * 描述：分项预算概要
 * 创建人：何小武
 * 创建时间：2010-5-24
 * *************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProjectBudget
{
    public class SubBudgetModel
    {
        public SubBudgetModel()
        { }

        /// <summary>
        /// 构造函数 ProjectStruct
        /// </summary>
        /// <param name="iD">ID</param>
        /// <param name="companyCD">CompanyCD</param>
        /// <param name="projectid">projectid</param>
        /// <param name="budgetName">StructName</param>
        public SubBudgetModel(int iD, string companyCD, int projectid, string budgetName)
        {
            _iD = iD;
            _companyCD = companyCD;
            _projectid = projectid;
            _budgetName = budgetName;
        }

        public SubBudgetModel(string companyCD, int projectid, string budgetName)
        {
            _companyCD = companyCD;
            _projectid = projectid;
            _budgetName = budgetName;
        }

        #region Model
        private int _iD;
        private string _companyCD;
        private int _projectid;
        private string _budgetName;
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
        /// projectid
        /// </summary>
        public int Projectid
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// StructName
        /// </summary>
        public string BudgetName
        {
            set { _budgetName = value; }
            get { return _budgetName; }
        }
        #endregion Model
    }
}
