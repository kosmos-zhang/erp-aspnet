using System;
using System.Collections.Generic;
using System.Text;

namespace XBase.Model.Personal.Agenda
{
    /// <summary>
    /// 实体类PersonalDateArrange 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class PersonalDateArrange
    {
        public PersonalDateArrange()
        { }
        #region Model
        private string _editflag;
        private int _id;
        private string _companycd;
        private string _arrangeno;
        private string _arrangetitle;
        private string _critical;
        private string _arrangperson;
        private string _content;
        private DateTime? _startdate;
        private string _starttime;
        private string _endtime;
        private int _creator;
        private string _ispublic;
        private string _status;
        private DateTime? _createdate;
        private string _ismobilenotice;
        private int? _aheadtimes;
        private DateTime? _noticedtime;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _important;

        /// <sunmary>
        /// 
        /// </sunmary>
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
        public string ArrangeNo
        {
            set { _arrangeno = value; }
            get { return _arrangeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ArrangeTItle
        {
            set { _arrangetitle = value; }
            get { return _arrangetitle; }
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
        public string ArrangPerson
        {
            set { _arrangperson = value; }
            get { return _arrangperson; }
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
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
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
        public string IsPublic
        {
            set { _ispublic = value; }
            get { return _ispublic; }
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
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsMobileNotice
        {
            set { _ismobilenotice = value; }
            get { return _ismobilenotice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AheadTimes
        {
            set { _aheadtimes = value; }
            get { return _aheadtimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? NoticedTime
        {
            set { _noticedtime = value; }
            get { return _noticedtime; }
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

        /// <summary>
        /// 
        /// </summary>
        public string Important
        {
            set { _important = value; }
            get { return _important; }
        }
        #endregion Model


    }
}


namespace XBase.Model.Personal.Agenda
{
    /// <summary>
    /// 日程安排实体类
    /// </summary>
    [Serializable]
    public class PersonalDateArrangeModel
    {
        #region 字段

        private Nullable<int> iD = null; //自动生成
        private string companyCD = String.Empty; //企业代码
        private string arrangeTItle = String.Empty; //安排主题
        private string critical = String.Empty; //紧急程度(1宽松,2一般,3较急,4紧急,5特急)
        private string arrangPerson = String.Empty; //保留
        private string content = String.Empty; //日程内容
        private Nullable<DateTime> startDate = null; //日程日期
        private string startTime = String.Empty; //开始时间（时分）
        private string endTime = String.Empty; //结束时间（时分）
        private Nullable<int> creator = null; //日程安排人ID(对应员工表ID)
        private string isPublic = String.Empty; //保密度（0不公开，1公开）
        private Nullable<DateTime> createDate = null; //创建时间
        private string isMobileNotice = String.Empty; //是否手机短信提醒（0否，1是）
        private Nullable<int> aheadTimes = null; //提前时间（小时）
        private Nullable<DateTime> modifiedDate = null; //最后更新日期
        private string modifiedUserID = String.Empty; //最后更新用户ID（对应操作用户表中的UserID）
        private string important = String.Empty; //重要程度(1不重要,2普通,3重要,4关键)
        private Nullable<DateTime> endDate = null; //结束日期
        private Nullable<int> toManagerID = null; //点评人ID
        private string managerNote = String.Empty; //点评内容
        private Nullable<DateTime> managerDate = null; //点评日期
        private string canViewUser = String.Empty; //可查看人员ID
        private string canViewUserName = String.Empty; //可查看人员姓名
        private string status = String.Empty; //日程状态（0草稿，1提交,2已点评）

        #endregion


        #region 方法

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PersonalDateArrangeModel()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="iD">自动生成</param>
        ///<param name="companyCD">企业代码</param>
        ///<param name="arrangeTItle">安排主题</param>
        ///<param name="critical">紧急程度(1宽松,2一般,3较急,4紧急,5特急)</param>
        ///<param name="arrangPerson">保留</param>
        ///<param name="content">日程内容</param>
        ///<param name="startDate">日程日期</param>
        ///<param name="startTime">开始时间（时分）</param>
        ///<param name="endTime">结束时间（时分）</param>
        ///<param name="creator">日程安排人ID(对应员工表ID)</param>
        ///<param name="isPublic">保密度（0不公开，1公开）</param>
        ///<param name="createDate">创建时间</param>
        ///<param name="isMobileNotice">是否手机短信提醒（0否，1是）</param>
        ///<param name="aheadTimes">提前时间（小时）</param>
        ///<param name="modifiedDate">最后更新日期</param>
        ///<param name="modifiedUserID">最后更新用户ID（对应操作用户表中的UserID）</param>
        ///<param name="important">重要程度(1不重要,2普通,3重要,4关键)</param>
        ///<param name="endDate">结束日期</param>
        ///<param name="toManagerID">点评人ID</param>
        ///<param name="managerNote">点评内容</param>
        ///<param name="managerDate">点评日期</param>
        ///<param name="canViewUser">可查看人员ID</param>
        ///<param name="canViewUserName">可查看人员姓名</param>
        ///<param name="status">日程状态（0草稿，1提交,2已点评）</param>
        public PersonalDateArrangeModel(
                    int iD,
                    string companyCD,
                    string arrangeTItle,
                    string critical,
                    string arrangPerson,
                    string content,
                    Nullable<DateTime> startDate,
                    string startTime,
                    string endTime,
                    int creator,
                    string isPublic,
                    Nullable<DateTime> createDate,
                    string isMobileNotice,
                    int aheadTimes,
                    Nullable<DateTime> modifiedDate,
                    string modifiedUserID,
                    string important,
                    Nullable<DateTime> endDate,
                    int toManagerID,
                    string managerNote,
                    Nullable<DateTime> managerDate,
                    string canViewUser,
                    string canViewUserName,
                    string status)
        {
            this.iD = iD; //自动生成
            this.companyCD = companyCD; //企业代码
            this.arrangeTItle = arrangeTItle; //安排主题
            this.critical = critical; //紧急程度(1宽松,2一般,3较急,4紧急,5特急)
            this.arrangPerson = arrangPerson; //保留
            this.content = content; //日程内容
            this.startDate = startDate; //日程日期
            this.startTime = startTime; //开始时间（时分）
            this.endTime = endTime; //结束时间（时分）
            this.creator = creator; //日程安排人ID(对应员工表ID)
            this.isPublic = isPublic; //保密度（0不公开，1公开）
            this.createDate = createDate; //创建时间
            this.isMobileNotice = isMobileNotice; //是否手机短信提醒（0否，1是）
            this.aheadTimes = aheadTimes; //提前时间（小时）
            this.modifiedDate = modifiedDate; //最后更新日期
            this.modifiedUserID = modifiedUserID; //最后更新用户ID（对应操作用户表中的UserID）
            this.important = important; //重要程度(1不重要,2普通,3重要,4关键)
            this.endDate = endDate; //结束日期
            this.toManagerID = toManagerID; //点评人ID
            this.managerNote = managerNote; //点评内容
            this.managerDate = managerDate; //点评日期
            this.canViewUser = canViewUser; //可查看人员ID
            this.canViewUserName = canViewUserName; //可查看人员姓名
            this.status = status; //日程状态（0草稿，1提交,2已点评）
        }

        #endregion


        #region 属性

        /// <summary>
        /// 自动生成
        /// </summary>
        public Nullable<int> ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        /// <summary>
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            get
            {
                return companyCD;
            }
            set
            {
                companyCD = value;
            }
        }

        /// <summary>
        /// 安排主题
        /// </summary>
        public string ArrangeTItle
        {
            get
            {
                return arrangeTItle;
            }
            set
            {
                arrangeTItle = value;
            }
        }

        /// <summary>
        /// 紧急程度(1宽松,2一般,3较急,4紧急,5特急)
        /// </summary>
        public string Critical
        {
            get
            {
                return critical;
            }
            set
            {
                critical = value;
            }
        }

        /// <summary>
        /// 保留
        /// </summary>
        public string ArrangPerson
        {
            get
            {
                return arrangPerson;
            }
            set
            {
                arrangPerson = value;
            }
        }

        /// <summary>
        /// 日程内容
        /// </summary>
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }

        /// <summary>
        /// 日程日期
        /// </summary>
        public Nullable<DateTime> StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }

        /// <summary>
        /// 开始时间（时分）
        /// </summary>
        public string StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        /// <summary>
        /// 结束时间（时分）
        /// </summary>
        public string EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        /// <summary>
        /// 日程安排人ID(对应员工表ID)
        /// </summary>
        public Nullable<int> Creator
        {
            get
            {
                return creator;
            }
            set
            {
                creator = value;
            }
        }

        /// <summary>
        /// 保密度（0不公开，1公开）
        /// </summary>
        public string IsPublic
        {
            get
            {
                return isPublic;
            }
            set
            {
                isPublic = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<DateTime> CreateDate
        {
            get
            {
                return createDate;
            }
            set
            {
                createDate = value;
            }
        }

        /// <summary>
        /// 是否手机短信提醒（0否，1是）
        /// </summary>
        public string IsMobileNotice
        {
            get
            {
                return isMobileNotice;
            }
            set
            {
                isMobileNotice = value;
            }
        }

        /// <summary>
        /// 提前时间（小时）
        /// </summary>
        public Nullable<int> AheadTimes
        {
            get
            {
                return aheadTimes;
            }
            set
            {
                aheadTimes = value;
            }
        }

        /// <summary>
        /// 最后更新日期
        /// </summary>
        public Nullable<DateTime> ModifiedDate
        {
            get
            {
                return modifiedDate;
            }
            set
            {
                modifiedDate = value;
            }
        }

        /// <summary>
        /// 最后更新用户ID（对应操作用户表中的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            get
            {
                return modifiedUserID;
            }
            set
            {
                modifiedUserID = value;
            }
        }

        /// <summary>
        /// 重要程度(1不重要,2普通,3重要,4关键)
        /// </summary>
        public string Important
        {
            get
            {
                return important;
            }
            set
            {
                important = value;
            }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        public Nullable<DateTime> EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }

        /// <summary>
        /// 点评人ID
        /// </summary>
        public Nullable<int> ToManagerID
        {
            get
            {
                return toManagerID;
            }
            set
            {
                toManagerID = value;
            }
        }

        /// <summary>
        /// 点评内容
        /// </summary>
        public string ManagerNote
        {
            get
            {
                return managerNote;
            }
            set
            {
                managerNote = value;
            }
        }

        /// <summary>
        /// 点评日期
        /// </summary>
        public Nullable<DateTime> ManagerDate
        {
            get
            {
                return managerDate;
            }
            set
            {
                managerDate = value;
            }
        }

        /// <summary>
        /// 可查看人员ID
        /// </summary>
        public string CanViewUser
        {
            get
            {
                return canViewUser;
            }
            set
            {
                canViewUser = value;
            }
        }

        /// <summary>
        /// 可查看人员姓名
        /// </summary>
        public string CanViewUserName
        {
            get
            {
                return canViewUserName;
            }
            set
            {
                canViewUserName = value;
            }
        }

        /// <summary>
        /// 日程状态（0草稿，1提交,2已点评）
        /// </summary>
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        #endregion
    }
}
