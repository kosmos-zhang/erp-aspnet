using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Personal.Note
{
    /// <summary>
    /// 实体类PersonalNote 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class NoteInfoModel
    {
        public NoteInfoModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _noteno;
        private DateTime _notedate;
        private string _notecontent;
        private string _attachment;
        private string _canviewuser;
        private string _canviewusername;
        private int _tomanagerid;
        private string _managernote;
        private DateTime _noteddate;
        private string _status;
        private int _creator;
        private string _creatorUserName;
        private DateTime _createdate;
        private DateTime _modifieddate;
        private string _modifieduserid;

        private string _studycontent;
        private string _mycheckcontent;
        private string _upcontent;

        private int _RenZhenDF;
        private int _KuaiDF;
        private int _ChengNuoDF;
        private int _RenWuDF;
        private int _LeGuanDF;
        private int _ZiXinDF;
        private int _FenXianDF;
        private int _JieKouDF;

        public int RenZhenDF
        {
            set { _RenZhenDF = value; }
            get { return _RenZhenDF; }
        }
        public int KuaiDF
        {
            set { _KuaiDF = value; }
            get { return _KuaiDF; }
        }
        public int ChengNuoDF
        {
            set { _ChengNuoDF = value; }
            get { return _ChengNuoDF; }
        }
        public int RenWuDF
        {
            set { _RenWuDF = value; }
            get { return _RenWuDF; }
        }
        public int LeGuanDF
        {
            set { _LeGuanDF = value; }
            get { return _LeGuanDF; }
        }
        public int FenXianDF
        {
            set { _FenXianDF = value; }
            get { return _FenXianDF; }
        }
        public int ZiXinDF
        {
            set { _ZiXinDF = value; }
            get { return _ZiXinDF; }
        }
        public int JieKouDF
        {
            set { _JieKouDF = value; }
            get { return _JieKouDF; }
        }

        public string StudyContent
        {
            set { _studycontent = value; }
            get { return _studycontent; }
        }
        public string MyCheckContent
        {
            set { _mycheckcontent = value; }
            get { return _mycheckcontent; }
        }
        public string UpContent
        {
            set { _upcontent = value; }
            get { return _upcontent; }
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
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 日记编号(年月日时分秒+用户id)
        /// </summary>
        public string NoteNo
        {
            set { _noteno = value; }
            get { return _noteno; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime NoteDate
        {
            set { _notedate = value; }
            get { return _notedate; }
        }
        /// <summary>
        /// 日记内容
        /// </summary>
        public string NoteContent
        {
            set { _notecontent = value; }
            get { return _notecontent; }
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
        /// 可以查看工作日志的人员（ID，多个）
        /// </summary>
        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        /// <summary>
        /// 可以查看工作日志的人员姓名
        /// </summary>
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
        }
        /// <summary>
        /// 提交主管(对应员工表ID)
        /// </summary>
        public int ToManagerID
        {
            set { _tomanagerid = value; }
            get { return _tomanagerid; }
        }
        /// <summary>
        /// 主管点评
        /// </summary>
        public string ManagerNote
        {
            set { _managernote = value; }
            get { return _managernote; }
        }
        /// <summary>
        /// 点评日期
        /// </summary>
        public DateTime NotedDate
        {
            set { _noteddate = value; }
            get { return _noteddate; }
        }
        /// <summary>
        /// 日志状态（0草稿，1提交,2已点评）
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 创建人ID(对应员工表ID)
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorUserName
        {
            set { _creatorUserName = value; }
            get { return _creatorUserName; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
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
        /// 最后更新用户ID（对应操作用户表中的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model

    }
}

/**********************************************
 * 类作用   日志表实体类层
 * 创建人   xz
 * 创建时间 2010-5-26 14:23:04 
 ***********************************************/

namespace XBase.Model.Personal.Note
{
    /// <summary>
    /// 日志表实体类
    /// </summary>
    [Serializable]
    public class PersonalNoteModel
    {
        #region 字段

        private Nullable<int> iD = null; //自动生成流水号
        private string companyCD = String.Empty; //企业代码
        private string noteNo = String.Empty; //日记编号(年月日时分秒+用户id)
        private Nullable<DateTime> noteDate = null; //日期
        private string noteContent = String.Empty; //日记内容
        private string attachment = String.Empty; //附件
        private string canViewUser = String.Empty; //可以查看工作日志的人员（ID，多个）
        private string canViewUserName = String.Empty; //可以查看工作日志的人员姓名
        private Nullable<int> toManagerID = null; //提交主管(对应员工表ID)
        private string managerNote = String.Empty; //主管点评
        private Nullable<DateTime> notedDate = null; //点评日期
        private string status = String.Empty; //日志状态（0草稿，1提交,2已点评）
        private Nullable<int> creator = null; //创建人ID(对应员工表ID)
        private string creatorUserName = String.Empty; //创建人名称
        private Nullable<DateTime> createDate = null; //创建时间
        private Nullable<DateTime> modifiedDate = null; //最后更新日期
        private string modifiedUserID = String.Empty; //最后更新用户ID（对应操作用户表中的UserID）
        private string studyContent = String.Empty; //今日学习
        private string myCheckContent = String.Empty; //今日反省
        private string upContent = String.Empty; //改进办法
        private Nullable<int> renZhenDF = null; //认真(分)
        private Nullable<int> kuaiDF = null; //快(分)
        private Nullable<int> chengNuoDF = null; //坚守承诺(分)
        private Nullable<int> renWuDF = null; //保证完成任务(分)
        private Nullable<int> leGuanDF = null; //乐观(分)
        private Nullable<int> ziXinDF = null; //自信(分)
        private Nullable<int> fenXianDF = null; //爱与奉献(分)
        private Nullable<int> jieKouDF = null; //决不找借口(分)

        #endregion


        #region 方法

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PersonalNoteModel()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="iD">自动生成流水号</param>
        ///<param name="companyCD">企业代码</param>
        ///<param name="noteNo">日记编号(年月日时分秒+用户id)</param>
        ///<param name="noteDate">日期</param>
        ///<param name="noteContent">日记内容</param>
        ///<param name="attachment">附件</param>
        ///<param name="canViewUser">可以查看工作日志的人员（ID，多个）</param>
        ///<param name="canViewUserName">可以查看工作日志的人员姓名</param>
        ///<param name="toManagerID">提交主管(对应员工表ID)</param>
        ///<param name="managerNote">主管点评</param>
        ///<param name="notedDate">点评日期</param>
        ///<param name="status">日志状态（0草稿，1提交,2已点评）</param>
        ///<param name="creator">创建人ID(对应员工表ID)</param>
        ///<param name="creatorUserName">创建人名称</param>
        ///<param name="createDate">创建时间</param>
        ///<param name="modifiedDate">最后更新日期</param>
        ///<param name="modifiedUserID">最后更新用户ID（对应操作用户表中的UserID）</param>
        ///<param name="studyContent">今日学习</param>
        ///<param name="myCheckContent">今日反省</param>
        ///<param name="upContent">改进办法</param>
        ///<param name="renZhenDF">认真(分)</param>
        ///<param name="kuaiDF">快(分)</param>
        ///<param name="chengNuoDF">坚守承诺(分)</param>
        ///<param name="renWuDF">保证完成任务(分)</param>
        ///<param name="leGuanDF">乐观(分)</param>
        ///<param name="ziXinDF">自信(分)</param>
        ///<param name="fenXianDF">爱与奉献(分)</param>
        ///<param name="jieKouDF">决不找借口(分)</param>
        public PersonalNoteModel(
                    int iD,
                    string companyCD,
                    string noteNo,
                    Nullable<DateTime> noteDate,
                    string noteContent,
                    string attachment,
                    string canViewUser,
                    string canViewUserName,
                    int toManagerID,
                    string managerNote,
                    Nullable<DateTime> notedDate,
                    string status,
                    int creator,
                    string creatorUserName,
                    Nullable<DateTime> createDate,
                    Nullable<DateTime> modifiedDate,
                    string modifiedUserID,
                    string studyContent,
                    string myCheckContent,
                    string upContent,
                    int renZhenDF,
                    int kuaiDF,
                    int chengNuoDF,
                    int renWuDF,
                    int leGuanDF,
                    int ziXinDF,
                    int fenXianDF,
                    int jieKouDF)
        {
            this.iD = iD; //自动生成流水号
            this.companyCD = companyCD; //企业代码
            this.noteNo = noteNo; //日记编号(年月日时分秒+用户id)
            this.noteDate = noteDate; //日期
            this.noteContent = noteContent; //日记内容
            this.attachment = attachment; //附件
            this.canViewUser = canViewUser; //可以查看工作日志的人员（ID，多个）
            this.canViewUserName = canViewUserName; //可以查看工作日志的人员姓名
            this.toManagerID = toManagerID; //提交主管(对应员工表ID)
            this.managerNote = managerNote; //主管点评
            this.notedDate = notedDate; //点评日期
            this.status = status; //日志状态（0草稿，1提交,2已点评）
            this.creator = creator; //创建人ID(对应员工表ID)
            this.creatorUserName = creatorUserName; //创建人名称
            this.createDate = createDate; //创建时间
            this.modifiedDate = modifiedDate; //最后更新日期
            this.modifiedUserID = modifiedUserID; //最后更新用户ID（对应操作用户表中的UserID）
            this.studyContent = studyContent; //今日学习
            this.myCheckContent = myCheckContent; //今日反省
            this.upContent = upContent; //改进办法
            this.renZhenDF = renZhenDF; //认真(分)
            this.kuaiDF = kuaiDF; //快(分)
            this.chengNuoDF = chengNuoDF; //坚守承诺(分)
            this.renWuDF = renWuDF; //保证完成任务(分)
            this.leGuanDF = leGuanDF; //乐观(分)
            this.ziXinDF = ziXinDF; //自信(分)
            this.fenXianDF = fenXianDF; //爱与奉献(分)
            this.jieKouDF = jieKouDF; //决不找借口(分)
        }

        #endregion


        #region 属性

        /// <summary>
        /// 自动生成流水号
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
        /// 日记编号(年月日时分秒+用户id)
        /// </summary>
        public string NoteNo
        {
            get
            {
                return noteNo;
            }
            set
            {
                noteNo = value;
            }
        }

        /// <summary>
        /// 日期
        /// </summary>
        public Nullable<DateTime> NoteDate
        {
            get
            {
                return noteDate;
            }
            set
            {
                noteDate = value;
            }
        }

        /// <summary>
        /// 日记内容
        /// </summary>
        public string NoteContent
        {
            get
            {
                return noteContent;
            }
            set
            {
                noteContent = value;
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            get
            {
                return attachment;
            }
            set
            {
                attachment = value;
            }
        }

        /// <summary>
        /// 可以查看工作日志的人员（ID，多个）
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
        /// 可以查看工作日志的人员姓名
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
        /// 提交主管(对应员工表ID)
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
        /// 主管点评
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
        public Nullable<DateTime> NotedDate
        {
            get
            {
                return notedDate;
            }
            set
            {
                notedDate = value;
            }
        }

        /// <summary>
        /// 日志状态（0草稿，1提交,2已点评）
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

        /// <summary>
        /// 创建人ID(对应员工表ID)
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
        /// 创建人名称
        /// </summary>
        public string CreatorUserName
        {
            get
            {
                return creatorUserName;
            }
            set
            {
                creatorUserName = value;
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
        /// 今日学习
        /// </summary>
        public string StudyContent
        {
            get
            {
                return studyContent;
            }
            set
            {
                studyContent = value;
            }
        }

        /// <summary>
        /// 今日反省
        /// </summary>
        public string MyCheckContent
        {
            get
            {
                return myCheckContent;
            }
            set
            {
                myCheckContent = value;
            }
        }

        /// <summary>
        /// 改进办法
        /// </summary>
        public string UpContent
        {
            get
            {
                return upContent;
            }
            set
            {
                upContent = value;
            }
        }

        /// <summary>
        /// 认真(分)
        /// </summary>
        public Nullable<int> RenZhenDF
        {
            get
            {
                return renZhenDF;
            }
            set
            {
                renZhenDF = value;
            }
        }

        /// <summary>
        /// 快(分)
        /// </summary>
        public Nullable<int> KuaiDF
        {
            get
            {
                return kuaiDF;
            }
            set
            {
                kuaiDF = value;
            }
        }

        /// <summary>
        /// 坚守承诺(分)
        /// </summary>
        public Nullable<int> ChengNuoDF
        {
            get
            {
                return chengNuoDF;
            }
            set
            {
                chengNuoDF = value;
            }
        }

        /// <summary>
        /// 保证完成任务(分)
        /// </summary>
        public Nullable<int> RenWuDF
        {
            get
            {
                return renWuDF;
            }
            set
            {
                renWuDF = value;
            }
        }

        /// <summary>
        /// 乐观(分)
        /// </summary>
        public Nullable<int> LeGuanDF
        {
            get
            {
                return leGuanDF;
            }
            set
            {
                leGuanDF = value;
            }
        }

        /// <summary>
        /// 自信(分)
        /// </summary>
        public Nullable<int> ZiXinDF
        {
            get
            {
                return ziXinDF;
            }
            set
            {
                ziXinDF = value;
            }
        }

        /// <summary>
        /// 爱与奉献(分)
        /// </summary>
        public Nullable<int> FenXianDF
        {
            get
            {
                return fenXianDF;
            }
            set
            {
                fenXianDF = value;
            }
        }

        /// <summary>
        /// 决不找借口(分)
        /// </summary>
        public Nullable<int> JieKouDF
        {
            get
            {
                return jieKouDF;
            }
            set
            {
                jieKouDF = value;
            }
        }

        #endregion
    }
}

