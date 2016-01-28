using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public class SalaryEmployeeStructureSetModel
    {
        public SalaryEmployeeStructureSetModel()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private string _companycd;
        private string _iscompanyroyaltyset;
        private string _isdeptroyaltyset;
        private string _isproductroyaltyset;
        private string _isfixsalaryset;
        private string _ispieceworkset;
        private string _isinsurenceset;
        private string _isperincometaxset;
        private string _isquteerset;
        private string _istimeworkset;
        private string _ispersonalroyaltyset;
        private string _modifieduserid;
        private string _modifieddate;
        private string _isperformanceset;
        private string _companyratepercent;
        private string _deptratepercent;
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
        public string IsCompanyRoyaltySet
        {
            set { _iscompanyroyaltyset = value; }
            get { return _iscompanyroyaltyset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsDeptRoyaltySet
        {
            set { _isdeptroyaltyset = value; }
            get { return _isdeptroyaltyset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsProductRoyaltySet
        {
            set { _isproductroyaltyset = value; }
            get { return _isproductroyaltyset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsFixSalarySet
        {
            set { _isfixsalaryset = value; }
            get { return _isfixsalaryset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsPieceWorkSet
        {
            set { _ispieceworkset = value; }
            get { return _ispieceworkset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsInsurenceSet
        {
            set { _isinsurenceset = value; }
            get { return _isinsurenceset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsPerIncomeTaxSet
        {
            set { _isperincometaxset = value; }
            get { return _isperincometaxset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsQuteerSet
        {
            set { _isquteerset = value; }
            get { return _isquteerset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsTimeWorkSet
        {
            set { _istimeworkset = value; }
            get { return _istimeworkset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsPersonalRoyaltySet
        {
            set { _ispersonalroyaltyset = value; }
            get { return _ispersonalroyaltyset; }
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
        public string IsPerformanceSet
        {
            set { _isperformanceset = value; }
            get { return _isperformanceset; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string CompanyRatePercent
        {
            set { _companyratepercent = value; }
            get { return _companyratepercent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeptRatePercent
        {
            set { _deptratepercent = value; }
            get { return _deptratepercent; }
        }
        #endregion Model

    }
}
