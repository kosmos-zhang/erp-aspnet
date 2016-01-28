/**********************************************
 * 类作用：   SalaryReportSummary表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/20
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：SalaryReportSummaryModel
    /// 描述：SalaryReportSummary表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/20
    /// 最后修改时间：2009/05/20
    /// </summary>
    ///
    public class SalaryReportSummaryModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _reprotno;
        private string _employeeid;
        private string _fixedmoney;
        private string _workmoney;
        private string _timemoney;
        private string _commissionmoney;
        private string _othergetmoney;
        private string _allgetmoney;
        private string _incometax;
        private string _insurance;
        private string _otherkillmoney;
        private string _allkillmoney;
        private string _salarymoney;
        private string _employeeNo;
        private string _employeeName;
        private string _deptName;
        private string _quarterName;
        private string _adminLevelName;
        private string _performanceMoney;
        private string _companyComMoney;
        private string _deptComMoney;
        private string _personComMoney;
        /// <summary>
        /// 个人业务提成金额
        /// </summary>
        public string PersonComMoney
        {
            set { _personComMoney = value; }
            get { return _personComMoney; }
        }
        /// <summary>
        /// 部门提成金额
        /// </summary>
        public string DeptComMoney
        {
            set { _deptComMoney = value; }
            get { return _deptComMoney; }
        }
        /// <summary>
        /// 公司提成金额
        /// </summary>
        public string CompanyComMoney
        {
            set { _companyComMoney = value; }
            get { return _companyComMoney; }
        }
        /// <summary>
        /// 绩效金额
        /// </summary>
        public string PerformanceMoney
        {
            set { _performanceMoney = value; }
            get { return _performanceMoney; }
        }
        /// <summary>
        /// 岗位职等
        /// </summary>
        public string AdminLevelName
        {
            set { _adminLevelName = value; }
            get { return _adminLevelName; }
        }
        /// <summary>
        /// 所在岗位
        /// </summary>
        public string QuarterName
        {
            set { _quarterName = value; }
            get { return _quarterName; }
        }
        /// <summary>
        /// 员工所在部门
        /// </summary>
        public string DeptName
        {
            set { _deptName = value; }
            get { return _deptName; }
        }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName
        {
            set { _employeeName = value; }
            get { return _employeeName; }
        }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo
        {
            set { _employeeNo = value; }
            get { return _employeeNo; }
        }
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
        /// 工资报表编号
        /// </summary>
        public string ReprotNo
        {
            set { _reprotno = value; }
            get { return _reprotno; }
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
        /// 固定工资
        /// </summary>
        public string FixedMoney
        {
            set { _fixedmoney = value; }
            get { return _fixedmoney; }
        }
        /// <summary>
        /// 计件工资
        /// </summary>
        public string WorkMoney
        {
            set { _workmoney = value; }
            get { return _workmoney; }
        }
        /// <summary>
        /// 计时工资
        /// </summary>
        public string TimeMoney
        {
            set { _timemoney = value; }
            get { return _timemoney; }
        }
        /// <summary>
        /// 单品提成工资
        /// </summary>
        public string CommissionMoney
        {
            set { _commissionmoney = value; }
            get { return _commissionmoney; }
        }
        /// <summary>
        /// 其他应付工资额
        /// </summary>
        public string OtherGetMoney
        {
            set { _othergetmoney = value; }
            get { return _othergetmoney; }
        }
        /// <summary>
        /// 应付工资额合计
        /// </summary>
        public string AllGetMoney
        {
            set { _allgetmoney = value; }
            get { return _allgetmoney; }
        }
        /// <summary>
        /// 个人所得税（扣款）
        /// </summary>
        public string IncomeTax
        {
            set { _incometax = value; }
            get { return _incometax; }
        }
        /// <summary>
        /// 社保（扣款）
        /// </summary>
        public string Insurance
        {
            set { _insurance = value; }
            get { return _insurance; }
        }
        /// <summary>
        /// 其他应扣款额
        /// </summary>
        public string OtherKillMoney
        {
            set { _otherkillmoney = value; }
            get { return _otherkillmoney; }
        }
        /// <summary>
        /// 应扣款额合计
        /// </summary>
        public string AllKillMoney
        {
            set { _allkillmoney = value; }
            get { return _allkillmoney; }
        }
        /// <summary>
        /// 实发工资额
        /// </summary>
        public string SalaryMoney
        {
            set { _salarymoney = value; }
            get { return _salarymoney; }
        }
        #endregion Model
    }
}
