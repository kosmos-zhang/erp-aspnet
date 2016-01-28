/**********************************************
 * 类作用：   EmployeeAttendanceSet表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/12
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：EmployeeAttendanceSetModel
    /// 描述：EmployeeAttendanceSet表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/12
    /// </summary>
   public class EmployeeAttendanceSetModel
   {
        #region EmployeeAttendanceSet表数据模板
        private int _id;
        private string _companycd;
        private int _employeeid;
        private string _workgroupno;
        private string _attendancetype;
        private string _workovertimetype;
        private string _weekrestday;
        private string _weekisallday;
        private string _monthrestday;
        private DateTime _startdate;
        private DateTime _enddate;
        private DateTime _modifileddate;
        private string _modifileduserid;
        /// <summary>
        /// 自增ID
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
        /// 员工ID
        /// </summary>
        public int EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 班组编号
        /// </summary>
        public string WorkGroupNo
        {
            set { _workgroupno = value; }
            get { return _workgroupno; }
        }
        /// <summary>
        /// 考勤类型(正常考勤、免签、不处理)
        /// </summary>
        public string AttendanceType
        {
            set { _attendancetype = value; }
            get { return _attendancetype; }
        }
        /// <summary>
        /// 加班类型(自动计算加班、不计算加班、只计算节假日加班)
        /// </summary>
        public string WorkOverTimeType
        {
            set { _workovertimetype = value; }
            get { return _workovertimetype; }
        }
        /// <summary>
        /// 每周休息日
        /// </summary>
        public string WeekRestDay
        {
            set { _weekrestday = value; }
            get { return _weekrestday; }
        }
        /// <summary>
        /// 每周休息日是否全天
        /// </summary>
        public string WeekIsAllDay
        {
            set { _weekisallday = value; }
            get { return _weekisallday; }
        }
        /// <summary>
        /// 每月休息日
        /// </summary>
        public string MonthRestDay
        {
            set { _monthrestday = value; }
            get { return _monthrestday; }
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EnddDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiledDate
        {
            set { _modifileddate = value; }
            get { return _modifileddate; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string ModifiledUserID
        {
            set { _modifileduserid = value; }
            get { return _modifileduserid; }
        }
        #endregion Model
    }
}
