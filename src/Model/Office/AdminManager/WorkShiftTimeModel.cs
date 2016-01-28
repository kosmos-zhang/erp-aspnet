/**********************************************
 * 类作用：   WorkShiftTime表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/07
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：WorkShiftTimeModel
    /// 描述：WorkShiftTime表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/07
    /// </summary>
   public class WorkShiftTimeModel
   {
       #region WorkShiftTime表数据模板
        private int _id;
        private string _workshiftno;
        private string _shifttimename;
        private string _ontime;
        private int _onforwordtime;
        private int _onlatetime;
        private string _ifattendance;
        private string _ifflag;
        private string _offtime;
        private int _offforwordtime;
        private int _offlatetime;
        private int _attendancetimelong;
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
        /// 班次编号（对应班次设置表中WorkShiftNo）
        /// </summary>
        public string WorkShiftNo
        {
            set { _workshiftno = value; }
            get { return _workshiftno; }
        }
        /// <summary>
        /// 班段名称
        /// </summary>
        public string ShiftTimeName
        {
            set { _shifttimename = value; }
            get { return _shifttimename; }
        }
        /// <summary>
        /// 上班时间
        /// </summary>
        public string OnTime
        {
            set { _ontime = value; }
            get { return _ontime; }
        }
        /// <summary>
        /// 上班可提前
        /// </summary>
        public int OnForwordTime
        {
            set { _onforwordtime = value; }
            get { return _onforwordtime; }
        }
        /// <summary>
        /// 上班可迟到
        /// </summary>
        public int OnLateTime
        {
            set { _onlatetime = value; }
            get { return _onlatetime; }
        }
        /// <summary>
        /// 是否考勤
        /// </summary>
        public string IfAttendance
        {
            set { _ifattendance = value; }
            get { return _ifattendance; }
        }
        /// <summary>
        /// 是否跨日
        /// </summary>
        public string IfFlag
        {
            set { _ifflag = value; }
            get { return _ifflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OffTime
        {
            set { _offtime = value; }
            get { return _offtime; }
        }
        /// <summary>
        /// 下班可早退
        /// </summary>
        public int OffForwordTime
        {
            set { _offforwordtime = value; }
            get { return _offforwordtime; }
        }
        /// <summary>
        /// 下班可推迟
        /// </summary>
        public int OffLateTime
        {
            set { _offlatetime = value; }
            get { return _offlatetime; }
        }
        /// <summary>
        /// 考勤总工时（分）
        /// </summary>
        public int AttendanceTimeLong
        {
            set { _attendancetimelong = value; }
            get { return _attendancetimelong; }
        }
        /// <summary>
        /// 最后更新日期
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
