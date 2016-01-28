using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public class SalaryCustomerRoyaltySetModel
    {
        public SalaryCustomerRoyaltySetModel()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private string _companycd;
        private string _miniscore;
        private string _maxscore;
        private string _yearconfficent;
        private string _modifieduserid;
        private string _modifieddate;
        private string _seasonconfficent;
        private string _halfconfficent;
        private string _monthconfficent;
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
        public string YearConfficent
        {
            set { _yearconfficent = value; }
            get { return _yearconfficent; }
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
        public string SeasonConfficent
        {
            set { _seasonconfficent = value; }
            get { return _seasonconfficent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HalfConfficent
        {
            set { _halfconfficent = value; }
            get { return _halfconfficent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MonthConfficent
        {
            set { _monthconfficent = value; }
            get { return _monthconfficent; }
        }
        #endregion Model

    }
}
