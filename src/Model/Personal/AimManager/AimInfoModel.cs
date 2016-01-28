using System;
using System.Collections.Generic;
using System.Text;

/********************
 创建人：王乾睿
 创建日期：2009.4.7
 功能：目标表数据模板
 ********************/

namespace XBase.Model.Personal.AimManager
{
    public class AimInfoModel
    {
        #region  AimInfoModel
        private string _editFlag;
        private int _id;
        private string _companycd;
        private string _aimno;
        private int _aimtypeid;
        private string _aimtitle;
        private string _scorenote;
        private string _aimcontent;
        private string _aimstandard;
        private int _summarizer;
        private string _aimrealresult;
        private string _addorcut;
        private string _difference;
        private decimal _completepercent;
        private string _aimstep;
        private string _resources;
        private string _qustion;
        private string _method;
        private string _aimflag;
        private string _joinuseridlist;
        private string _joinusernamelist;
        private int _principalid;

        private string _attachment;
        private string _memo;
        private string _critical;
        private string _important;
        private string _priority;
        private string _attentionname;
        private string _status;
        private string _remark;
        private int _creator;
        private DateTime _createdate;

        private int _coordinator;
        private DateTime _summarizedate;
        private string _summarizenote;
        private int _checkscore;
        private int _checkuserid;
        private string _checknote;
        private DateTime _checkdate;
        private DateTime _startdate;
        private DateTime _enddate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private int _aimNum;
        private string _aimDate;
        private int _deptid;

        private  string _canviewuser;
        private  string _canviewusername;
        private  string _ismobilenotice;
        private  DateTime  _remindtime;

        private int _checkor;
        private string _isdirection; 

        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 企业编码
        /// </summary>
        public string CompanyCD
        {
            get { return _companycd; }
            set { _companycd = value; }
        }
        /// <summary>
        /// 目标编号
        /// </summary>
        public string AimNO
        {
            get { return _aimno; }
            set { _aimno = value; }
        }
        /// <summary>
        /// 目标类型ID
        /// </summary>
        public int AimTypeID
        {
            get { return _aimtypeid; }
            set { _aimtypeid = value; }
        }
        /// <summary>
        /// 目标名称
        /// </summary>
        public string AimTitle
        {
            get { return _aimtitle; }
            set { _aimtitle = value; }
        }
        /// <summary>
        /// 目标内容
        /// </summary>
        public string AimContent
        {
            get { return _aimcontent; }
            set { _aimcontent = value; }
        }
        /// <summary>
        /// 目标指标
        /// </summary>
        public string AimStandard
        {
            get { return _aimstandard; }
            set { _aimstandard = value; }
        }
        /// <summary>
        /// 目标分类
        /// </summary>
        public string AimFlag
        {
            get { return _aimflag; }
            set { _aimflag = value; }
        }
        /// <summary>
        /// 参与人ID列表
        /// </summary>
        public string JoinUserIDList
        {
            get { return _joinuseridlist; }
            set { _joinuseridlist = value; }
        }
        /// <summary>
        /// 参与人姓名列表
        /// </summary>
        public string JoinUserNameList
        {
            get { return _joinusernamelist; }
            set { _joinusernamelist = value; }
        }
        /// <summary>
        /// 负责人ID
        /// </summary>
        public int PrincipalID
        {
            get { return _principalid; }
            set { _principalid = value; }
        }


        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            get { return _attachment; }
            set { _attachment = value; }
        }
        /// <summary>
        /// 附件说明
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public string Critical
        {
            get { return _critical; }
            set { _critical = value; }
        }
        /// <summary>
        /// 重要程度
        /// </summary>
        public string Important
        {
            get { return _important; }
            set { _important = value; }
        }
        /// <summary>
        /// 优先级
        /// </summary>
        public string Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        /// <summary>
        /// 关注点
        /// </summary>
        public string AttentionName
        {
            get { return _attentionname; }
            set { _attentionname = value; }
        }
        /// <summary>
        /// 目标状态
        /// </summary>
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 目标备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

        /// <summary>
        /// 协调人ID
        /// </summary>
        public int Coordinator
        {
            get { return _coordinator; }
            set { _coordinator = value; }
        }
        /// <summary>
        /// 总结时间
        /// </summary>
        public DateTime SummarizeDate
        {
            get { return _summarizedate; }
            set { _summarizedate = value; }
        }
        /// <summary>
        /// 总结内容
        /// </summary>
        public string SummarizeNote
        {
            get { return _summarizenote; }
            set { _summarizenote = value; }
        }
        /// <summary>
        /// 点评评分ID
        /// </summary>
        public int CheckScore
        {
            get { return _checkscore; }
            set { _checkscore = value; }
        }
        /// <summary>
        /// 点评人员
        /// </summary>
        public int CheckUserID
        {
            get { return _checkuserid; }
            set { _checkuserid = value; }
        }
        /// <summary>
        /// 点评内容
        /// </summary>
        public string CheckNote
        {
            get { return _checknote; }
            set { _checknote = value; }
        }
        /// <summary>
        /// 点评时间
        /// </summary>
        public DateTime CheckDate
        {
            get { return _checkdate; }
            set { _checkdate = value; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            get { return _modifieddate; }
            set { _modifieddate = value; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            get { return _modifieduserid; }
            set { _modifieduserid = value; }
        }

        /// <summary>
        ///开始日期
        /// </summary>
        public DateTime StartDate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }
        /// <summary>
        ///结束日期
        /// </summary>
        public DateTime EndDate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }

        /// <summary>
        ///  编辑标识
        /// </summary>
        public string EditFlag
        {
            get
            {
                return _editFlag;
            }
            set
            {
                _editFlag = value;
            }
        }
        /// <summary>
        ///  目标日期
        /// </summary>
        public string AimDate
        {
            get
            {
                return _aimDate;
            }
            set
            {
                _aimDate = value;
            }
        }
        /// <summary>
        ///  目标类别内小序号
        /// </summary>
        public int AimNum
        {
            get
            {
                return _aimNum;
            }
            set
            {
                _aimNum = value;
            }
        }


        /// <summary>
        /// 完成步骤
        /// </summary>
        public string AimStep
        {
            get { return _aimstep; }
            set { _aimstep = value; }
        }


        /// <summary>
        /// 所需资源
        /// </summary>
        public string Resources
        {
            get { return _resources; }
            set { _resources = value; }
        }

        /// <summary>
        /// 潜在问题分析
        /// </summary>
        public string Qustion
        {
            get { return _qustion; }
            set { _qustion = value; }
        }

        /// <summary>
        /// 应对措施
        /// </summary>
        public string Method
        {
            get { return _method; }
            set { _method = value; }
        }


        /// <summary>
        /// 总结人
        /// </summary>
        public int Summarizer
        {
            get { return _summarizer; }
            set { _summarizer = value; }
        }

        /// <summary>
        /// 目标实绩
        /// </summary>
        public string AimRealResult
        {
            get { return _aimrealresult; }
            set { _aimrealresult = value; }
        }

        /// <summary>
        /// 完成情况
        /// </summary>
        public string AddOrCut
        {
            get { return _addorcut; }
            set { _addorcut = value; }
        }

        /// <summary>
        /// 差额
        /// </summary>
        public string Difference
        {
            get { return _difference; }
            set { _difference = value; }
        }

        /// <summary>
        /// 目标达成率
        /// </summary>
        public decimal CompletePercent
        {
            get { return _completepercent; }
            set { _completepercent = value; }
        }

        public string ScoreNote
        {
            get { return _scorenote; }
            set { _scorenote = value; }
        }

        public int DeptID
        {
            get { return _deptid; }
            set { _deptid = value; }
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

        public string IsDirection
        {
            get { return _isdirection; }
            set { _isdirection = value; }
        }

        public int Checkor {
            get { return _checkor; }
            set { _checkor = value; }
        }

        #endregion
    }
}

