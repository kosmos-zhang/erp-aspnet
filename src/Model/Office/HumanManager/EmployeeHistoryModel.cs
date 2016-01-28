/**********************************************
 * 类作用：   EmployeeHistory表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/09
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：EmployeeHistoryModel
    /// 描述：EmployeeHistory表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/09
    /// 最后修改时间：2009/03/09
    /// </summary>
    ///
    public class EmployeeHistoryModel
    {

        #region Model
        private int _id;
        private string _companycd;
        private string _employeeid;
        private string _startdate;
        private string _enddate;
        private string _flag;
        private string _company;
        private string _department;
        private string _workcontent;
        private string _leavereason;
        private string _schoolname;
        private string _professional;
        private string _culturelevel;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public int ID
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
        /// 员工信息ID
        /// </summary>
        public string EmployeeNo
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 区分(1工作，2 学习)
        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string Company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 所在部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 工作内容
        /// </summary>
        public string WorkContent
        {
            set { _workcontent = value; }
            get { return _workcontent; }
        }
        /// <summary>
        /// 离职原因
        /// </summary>
        public string LeaveReason
        {
            set { _leavereason = value; }
            get { return _leavereason; }
        }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName
        {
            set { _schoolname = value; }
            get { return _schoolname; }
        }
        /// <summary>
        /// 专业ID(对应分类代码表ID)
        /// </summary>
        public string Professional
        {
            set { _professional = value; }
            get { return _professional; }
        }
        /// <summary>
        /// 学历ID(对应分类代码表ID)
        /// </summary>
        public string CultureLevel
        {
            set { _culturelevel = value; }
            get { return _culturelevel; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime ModifiedDate
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
