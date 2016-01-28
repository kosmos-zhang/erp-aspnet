using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
   public   class PersonTrueIncomeTaxModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _employeeid;
        private string _startdate;
        private string _salarycount;
        private string _taxpercent;
        private string _taxcount;
        private string _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 开始缴税时间
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 缴税工资额
        /// </summary>
        public string SalaryCount
        {
            set { _salarycount = value; }
            get { return _salarycount; }
        }
        /// <summary>
        /// 税率
        /// </summary>
        public string TaxPercent
        {
            set { _taxpercent = value; }
            get { return _taxpercent; }
        }
        /// <summary>
        /// 税额
        /// </summary>
        public string TaxCount
        {
            set { _taxcount = value; }
            get { return _taxcount; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
