using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public class SalaryPerformanceRoyaltySetModel
    {
        public SalaryPerformanceRoyaltySetModel()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private string _companycd;
        private string _miniscore;
        private string _maxscore;
        private string _confficent;
        private string _modifieduserid;
        private string _modifieddate;
        private string _taskflag;
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MiniScore
        {
            set { _miniscore = value; }
            get { return _miniscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MaxScore
        {
            set { _maxscore = value; }
            get { return _maxscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Confficent
        {
            set { _confficent = value; }
            get { return _confficent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Taskflag
        {
            set { _taskflag = value; }
            get { return _taskflag; }
        }
        #endregion Model

    }
}
