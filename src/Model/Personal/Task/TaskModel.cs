using System;
using System.Collections.Generic;
using System.Text;

namespace XBase.Model.Personal.Task
{
    /// <summary>
    /// 实体类Task 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class TaskModel
    {
        #region Model
        private string _editflag;
        private int _id;
        private string _companycd;
        private string _taskno;
        private string _tasktype;
        private int _tasktypeid;
        private string _title;
        private string _content;
        private string _taskaim;
        private string _taskstep;
        private DateTime _completedate;
        private string _completetime;
        private int _principal;
        private string _joins;
        private string _critical;
        private string _important;
        private string _priority;
        private string _status;
        private string _attachment;
        private string _remark;
        private int _creator;
        private DateTime _createdate;
        private int _sendor;
        private DateTime _senddate;
        private int _cancelor;
        private DateTime _canceldate;
        private string _resultreport;
        private DateTime _enddate;
        private DateTime _reportdate;
        private string _addorcut;
        private string _checknote;
        private int _checkscore;
        private DateTime _checkdate;
        private int _checkuserid;
        private string _modifieduserid;
        private int _deptid;

        private string _canviewuser;
        private string _canviewusername;
        private string _ismobilenotice;
        private DateTime _remindtime;


        /// <summary>
        /// 
        /// </summary>
        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        public string TaskNo
        {
            set { _taskno = value; }
            get { return _taskno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskType
        {
            set { _tasktype = value; }
            get { return _tasktype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TaskTypeID
        {
            set { _tasktypeid = value; }
            get { return _tasktypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskAim
        {
            set { _taskaim = value; }
            get { return _taskaim; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskStep
        {
            set { _taskstep = value; }
            get { return _taskstep; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CompleteDate
        {
            set { _completedate = value; }
            get { return _completedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompleteTime
        {
            set { _completetime = value; }
            get { return _completetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Principal
        {
            set { _principal = value; }
            get { return _principal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Joins
        {
            set { _joins = value; }
            get { return _joins; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Critical
        {
            set { _critical = value; }
            get { return _critical; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Important
        {
            set { _important = value; }
            get { return _important; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Priority
        {
            set { _priority = value; }
            get { return _priority; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Sendor
        {
            set { _sendor = value; }
            get { return _sendor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime SendDate
        {
            set { _senddate = value; }
            get { return _senddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Cancelor
        {
            set { _cancelor = value; }
            get { return _cancelor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CancelDate
        {
            set { _canceldate = value; }
            get { return _canceldate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ResultReport
        {
            set { _resultreport = value; }
            get { return _resultreport; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ReportDate
        {
            set { _reportdate = value; }
            get { return _reportdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddOrCut
        {
            set { _addorcut = value; }
            get { return _addorcut; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckNote
        {
            set { _checknote = value; }
            get { return _checknote; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CheckScore
        {
            set { _checkscore = value; }
            get { return _checkscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CheckUserID
        {
            set { _checkuserid = value; }
            get { return _checkuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }

        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        public DateTime RemindTime
        {
            get { return _remindtime; }
            set { _remindtime = value; }
        }
        public string CanViewUser
        {
            get { return _canviewuser; }
            set { _canviewuser = value; }
        }
        public string CanViewUserName
        {
            get { return _canviewusername; }
            set { _canviewusername = value; }
        }
        public string IsMobileNotice
        {
            get { return _ismobilenotice; }
            set { _ismobilenotice = value; }
        }
        #endregion
    }
}
