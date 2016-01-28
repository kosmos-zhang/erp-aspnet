/**********************************************
 * 类作用：   AttendanceApply表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/14
 ***********************************************/
using System;
namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：AttendanceApplyModel
    /// 描述：AttendanceApply表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/14
    /// </summary>
   public class AttendanceApplyModel
   {
        #region AttendanceApply表数据模板
        private int _id;
        private string _companycd;
        private int _employeeid;
        private int _applyuserid;
        private string _flag;
        private string _applyno;
        private DateTime _applydate;
        private DateTime _startdate;
        private DateTime _enddate;
        private string _starttime;
        private string _endtime;
        private DateTime _factstartdate;
        private DateTime _factenddate;
        private DateTime _factstarttime;
        private DateTime _factendtime;
        private string _applyreason;
        private int _leavetype;
        private DateTime _businessdate;
        private DateTime _businessplandate;
        private string _businessplace;
        private string _businesspeer;
        private string _businesstransport;
        private decimal _businessadvance;
        private string _businessremark;
        private string _overtimetype;
        private decimal _overtimehours;
        private string _insteaemployees;
        private string _insteademployees;
        private string _insteadstarttime;
        private string _insteadendtime;
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
        /// 申请人ID
        /// </summary>
        public int ApplyUserID
        {
            set { _applyuserid = value; }
            get { return _applyuserid; }
        }
        /// <summary>
        /// 考勤申请单据编号
        /// </summary>
        public string ApplyNo
        {
            set { _applyno = value; }
            get { return _applyno; }
        }
        /// <summary>
        /// 区分(1请假，2加班，3外出，4出差，5替班)
        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
        }
        /// <summary>
        /// 考勤开始日期
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 考勤结束日期
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime FactStartDate
        {
            set { _factstartdate = value; }
            get { return _factstartdate; }
        }
        /// <summary>
        /// 实际结束日期
        /// </summary>
        public DateTime FactEndDate
        {
            set { _factenddate = value; }
            get { return _factenddate; }
        }
        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime FactStartTime
        {
            set { _factstarttime = value; }
            get { return _factstarttime; }
        }
        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime FactEndTime
        {
            set { _factendtime = value; }
            get { return _factendtime; }
        }
        /// <summary>
        /// 申请事由
        /// </summary>
        public string ApplyReason
        {
            set { _applyreason = value; }
            get { return _applyreason; }
        }
        /// <summary>
        /// 请假类别
        /// </summary>
        public int LeaveType
        {
            set { _leavetype = value; }
            get { return _leavetype; }
        }
        /// <summary>
        ///出差时间
        /// </summary>
        public DateTime BusinessDate
        {
            set { _businessdate = value; }
            get { return _businessdate; }
        }
        /// <summary>
        ///预计时间
        /// </summary>
        public DateTime BusinessPlanDate
        {
            set { _businessplandate = value; }
            get { return _businessplandate; }
        }
        /// <summary>
        /// 地点（出差或外出）
        /// </summary>
        public string BusinessPlace
        {
            set { _businessplace = value; }
            get { return _businessplace; }
        }
        /// <summary>
        /// 出差通行人
        /// </summary>
        public string BusinessPeer
        {
            set { _businesspeer = value; }
            get { return _businesspeer; }
        }
        /// <summary>
        /// 出差交通工具
        /// </summary>
        public string BusinessTransport
        {
            set { _businesstransport = value; }
            get { return _businesstransport; }
        }
        /// <summary>
        /// 出差预借费用
        /// </summary>
        public decimal BusinessAdvance
        {
            set { _businessadvance = value; }
            get { return _businessadvance; }
        }
        /// <summary>
        /// 出差备注
        /// </summary>
        public string BusinessRemark
        {
            set { _businessremark = value; }
            get { return _businessremark; }
        }
        /// <summary>
        /// 加班事由
        /// </summary>
        public string OvertimeType
        {
            set { _overtimetype = value; }
            get { return _overtimetype; }
        }
        /// <summary>
        /// 加班工时
        /// </summary>
        public decimal OvertimeHours
        {
            set { _overtimehours = value; }
            get { return _overtimehours; }
        }
        /// <summary>
        /// 替班人
        /// </summary>
        public string InsteaEmployees
        {
            set { _insteaemployees = value; }
            get { return _insteaemployees; }
        }
        /// <summary>
        /// 被替班人
        /// </summary>
        public string InsteadEmployees
        {
            set { _insteademployees = value; }
            get { return _insteademployees; }
        }
        /// <summary>
        /// 被替开始时间
        /// </summary>
        public string InsteadStartTime
        {
            set { _insteadstarttime = value; }
            get { return _insteadstarttime; }
        }
        /// <summary>
        /// 被替结束时间
        /// </summary>
        public string InsteadEndTime
        {
            set { _insteadendtime = value; }
            get { return _insteadendtime; }
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
        /// 最后更新用户id
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
