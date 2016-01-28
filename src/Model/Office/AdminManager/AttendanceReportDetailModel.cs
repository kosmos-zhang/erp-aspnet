/**********************************************
 * 类作用：   AttendanceReportDetail表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/23
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：AttendanceReportDetailModel
    /// 描述：AttendanceReportDetail表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/23
    /// </summary>
   public class AttendanceReportDetailModel
    {
        #region Model
        private int _id;
        private string _reprotno;
        private string _companycd;
        private int _employeeid;
        private DateTime _startdate;
        private DateTime _enddate;
        private decimal _hours;
        private decimal _workhour;
        private decimal _overtime;
        private decimal _leave;
        private decimal _out;
        private decimal _business;
        private decimal _instead;
        private decimal _transferred;
        private decimal _late;
        private decimal _lateminute;
        private decimal _leaveearly;
        private decimal _leaveearlyminute;
        private decimal _absence;
        private decimal _changetimes;
        private string _changetype;
        private string _changenote;
        private decimal _attendancerate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 报表编号
        /// </summary>
        public string ReprotNo
        {
            set { _reprotno = value; }
            get { return _reprotno; }
        }
        /// <summary>
        /// 企业代码
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
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 应出勤时间
        /// </summary>
        public decimal Hours
        {
            set { _hours = value; }
            get { return _hours; }
        }
        /// <summary>
        /// 实际出勤时间
        /// </summary>
        public decimal WorkHour
        {
            set { _workhour = value; }
            get { return _workhour; }
        }
        /// <summary>
        /// 加班（时）
        /// </summary>
        public decimal Overtime
        {
            set { _overtime = value; }
            get { return _overtime; }
        }
        /// <summary>
        /// 请假（时）
        /// </summary>
        public decimal Leave
        {
            set { _leave = value; }
            get { return _leave; }
        }
        /// <summary>
        /// 外出
        /// </summary>
        public decimal Out
        {
            set { _out = value; }
            get { return _out; }
        }
        /// <summary>
        /// 出差（天）
        /// </summary>
        public decimal Business
        {
            set { _business = value; }
            get { return _business; }
        }
        /// <summary>
        /// 替班（时）
        /// </summary>
        public decimal Instead
        {
            set { _instead = value; }
            get { return _instead; }
        }
        /// <summary>
        /// 调休（时）
        /// </summary>
        public decimal Transferred
        {
            set { _transferred = value; }
            get { return _transferred; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Late
        {
            set { _late = value; }
            get { return _late; }
        }
        /// <summary>
        /// 迟到分钟数
        /// </summary>
        public decimal LateMinute
        {
            set { _lateminute = value; }
            get { return _lateminute; }
        }
        /// <summary>
        /// 早退（天）
        /// </summary>
        public decimal LeaveEarly
        {
            set { _leaveearly = value; }
            get { return _leaveearly; }
        }
        /// <summary>
        /// 早退分钟数
        /// </summary>
        public decimal LeaveEarlyMinute
        {
            set { _leaveearlyminute = value; }
            get { return _leaveearlyminute; }
        }
        /// <summary>
        /// 旷工（天）
        /// </summary>
        public decimal Absence
        {
            set { _absence = value; }
            get { return _absence; }
        }
        /// <summary>
        /// 调整数
        /// </summary>
        public decimal ChangeTimes
        {
            set { _changetimes = value; }
            get { return _changetimes; }
        }
        /// <summary>
        /// 调整方向（0调减，1调增）
        /// </summary>
        public string ChangeType
        {
            set { _changetype = value; }
            get { return _changetype; }
        }
        /// <summary>
        /// 调整原因
        /// </summary>
        public string ChangeNote
        {
            set { _changenote = value; }
            get { return _changenote; }
        }
        /// <summary>
        /// 出勤率
        /// </summary>
        public decimal AttendanceRate
        {
            set { _attendancerate = value; }
            get { return _attendancerate; }
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
