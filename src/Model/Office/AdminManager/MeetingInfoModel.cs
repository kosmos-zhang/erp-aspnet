/**********************************************
 * 类作用：   officedba.MeetingInfo表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/04/28
 ***********************************************/

using System;

namespace XBase.Model.Office.AdminManager
{
    public class MeetingInfoModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _meetingno;
        private string _title;
        private int _typeid;
        private int _caller;
        private int _deptid;
        private int _chairman;
        private DateTime? _startdate;
        private string _starttime;
        private decimal _timelong;
        private int _place;
        private string _topic;
        private string _contents;
        private string _readyinfo;
        private string _attachment;
        private string _remark;
        private string _meetingstatus;
        private string _ismobilecall;
        private string _callcontent;
        private string _joinuser;
        private int _sender;
        private DateTime? _senddate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
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
        /// 会议编号
        /// </summary>
        public string MeetingNo
        {
            set { _meetingno = value; }
            get { return _meetingno; }
        }
        /// <summary>
        /// 会议主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 会议类型ID
        /// </summary>
        public int TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 召集人
        /// </summary>
        public int Caller
        {
            set { _caller = value; }
            get { return _caller; }
        }
        /// <summary>
        /// 召开部门
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 主持人
        /// </summary>
        public int Chairman
        {
            set { _chairman = value; }
            get { return _chairman; }
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
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
        /// 预计时长（小时）
        /// </summary>
        public decimal TimeLong
        {
            set { _timelong = value; }
            get { return _timelong; }
        }
        /// <summary>
        /// 会议地点(会议室ID，对应会议室信息表ID)
        /// </summary>
        public int Place
        {
            set { _place = value; }
            get { return _place; }
        }
        /// <summary>
        /// 会议议题
        /// </summary>
        public string Topic
        {
            set { _topic = value; }
            get { return _topic; }
        }
        /// <summary>
        /// 会议内容
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 资料准备
        /// </summary>
        public string ReadyInfo
        {
            set { _readyinfo = value; }
            get { return _readyinfo; }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MeetingStatus
        {
            set { _meetingstatus = value; }
            get { return _meetingstatus; }
        }
        /// <summary>
        /// 是否手机短信通知（0否，1是）
        /// </summary>
        public string IsMobileCall
        {
            set { _ismobilecall = value; }
            get { return _ismobilecall; }
        }
        /// <summary>
        /// 手机短信通知内容
        /// </summary>
        public string CallContent
        {
            set { _callcontent = value; }
            get { return _callcontent; }
        }
        /// <summary>
        /// 与会人员
        /// </summary>
        public string JoinUser
        {
            set { _joinuser = value; }
            get { return _joinuser; }
        }   
        /// <summary>
        /// 通知人
        /// </summary>
        public int Sender
        {
            set { _sender = value; }
            get { return _sender; }
        }
        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTime? SendDate
        {
            set { _senddate = value; }
            get { return _senddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
