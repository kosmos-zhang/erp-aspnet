/**********************************************
 * 类作用：   DailyAttendance表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/02
 ***********************************************/
using System;


namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：DailyAttendanceModel
    /// 描述：DailyAttendance表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/02
    /// </summary>
    public class DailyAttendanceModel
    {
        #region 日常考勤数据Model
        private int _id;
        private string _companycd;
        private int _employeeid;
        private DateTime _date;
        private int _workshifttimeid;
        private DateTime _starttime;
        private DateTime _endtime;
        private string _signinremark;
        private string _signoutremark;
        private System.Decimal _delaytimelong;
        private System.Decimal _forwardofftimelong;
        private string _isdelay;
        private string _isforwaroff;
        private string _attendancetype;
        private int _employeeattendancesetid;
        public string IsExtraSignIn;
        public string IsExtraSignOut;
        public string ProcessSignInReason;
        public string ProcessSignOutReason;
        public int ProcessUserID;
        public DateTime ProcessDateTime;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 人员考勤设置ID
        /// </summary>
        public int EmployeeAttendanceSetID
        {
            set { _employeeattendancesetid = value; }
            get { return _employeeattendancesetid; }
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
        /// 考勤日期
        /// </summary>
        public DateTime Date
        {
            set { _date = value; }
            get { return _date; }
        }
        /// <summary>
        /// 对应班段ID
        /// </summary>
        public int WorkShiftTimeID
        {
            set { _workshifttimeid = value; }
            get { return _workshifttimeid; }
        }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 签退时间
        /// </summary>
        public DateTime EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 签到备注
        /// </summary>
        public string SignInRemark
        {
            set { _signinremark = value; }
            get { return _signinremark; }
        }
        /// <summary>
        /// 签退备注
        /// </summary>
        public string SignOutRemark
        {
            set { _signoutremark = value; }
            get { return _signoutremark; }
        }
        /// <summary>
        /// 迟到多长时间
        /// </summary>
        public decimal DelayTimeLong
        {
            set { _delaytimelong = value; }
            get { return _delaytimelong; }
        }
        /// <summary>
        /// 早退多长时间
        /// </summary>
        public decimal ForWardOffTimeLong
        {
            set { _forwardofftimelong = value; }
            get { return _forwardofftimelong; }
        }
        /// <summary>
        /// 是否迟到
        /// </summary>
        public string IsDelay
        {
            set { _isdelay = value; }
            get { return _isdelay; }
        }
        /// <summary>
        /// 是否早退
        /// </summary>
        public string IsForwarOff
        {
            set { _isforwaroff = value; }
            get { return _isforwaroff; }
        }
        /// <summary>
        /// 考勤类型
        /// </summary>
        public string AttendanceType
        {
            set { _attendancetype = value; }
            get { return _attendancetype; }
        }

        #endregion Model
    }
}
