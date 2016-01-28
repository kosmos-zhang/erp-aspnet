/**********************************************
 * 类作用：   RectInterview表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/18
 ***********************************************/
using System;
using System.Collections;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectInterviewModel
    /// 描述：RectInterview表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/18
    /// 最后修改时间：2009/04/18
    /// </summary>
    ///
    public class RectInterviewModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _planid;
        private string _interviewno;
        private string _staffname;
        private string _quarterid;
        private string _recttype;
        private string _interviewdate;
        private string _interviewtype;
        private string _interviewplace;
        private string _interviewnote;
        private string _testscore;
        private string _interviewresult;
        private string _checkdate;
        private string _checktype;
        private string _checkplace;
        private string _checknote;
        private string _mannote;
        private string _knownote;
        private string _worknote;
        private string _salarynote;
        private string _finalresult;
        private string _remark;
        private string _attachment;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editflag;
        private ArrayList _elemscorelist;
        private string _templateNo;
        private string _pageAttachment;
        private string _attachmentName;
       private string _quarterName;
       private string _interviewPerson;

       private string _planName;
       private string _finalSalary;
       private string _ourSalary;

       private string _otherNote;


       /// <summary>
       /// 其他方面
       /// </summary>
       public string OtherNote
       {
           set { _otherNote = value; }
           get { return _otherNote; }
       }
       /// <summary>
       /// 可提供的待遇
       /// </summary>
       public string OurSalary
       {    
           set { _ourSalary = value; }
           get { return _ourSalary; }
       }

       /// <summary>
       /// 确认工资
       /// </summary>
       public string FinalSalary
       {
           set { _finalSalary = value; }
           get { return _finalSalary; }
       }

       /// <summary>
       /// 活动名称
       /// </summary>
       public string PlanName
       {
           set { _planName = value; }
           get { return _planName; }
       }
        /// <summary>
        /// 
        /// </summary>
              public string InterviewPerson
       {
           set { _interviewPerson = value; }
           get { return _interviewPerson; }
       }

        /// <summary>
        /// 
        /// </summary>
       public string QuarterName
       {
           set { _quarterName = value; }
           get { return _quarterName; }
       }
        /// <summary>
        /// 
        /// </summary>
        public string AttachmentName
        {
            set { _attachmentName = value; }
            get { return _attachmentName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PageAttachment
        {
            set { _pageAttachment = value; }
            get { return _pageAttachment; }
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateNo
        {
            set { _templateNo = value; }
            get { return _templateNo; }
        }
        /// <summary>
        /// 要素得分列表
        /// </summary>
        public ArrayList ElemScoreList
        {
            get
            {
                if (_elemscorelist == null) _elemscorelist = new ArrayList();
                return _elemscorelist;
            }
            set
            {
                _elemscorelist = value;
            }
        }
        /// <summary>
        /// 编辑标识
        /// </summary>
        public string EditFlag
        {
            get
            {
                return _editflag;
            }
            set
            {
                _editflag = value;
            }
        }
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string ID
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
        /// 招聘计划ID
        /// </summary>
        public string PlanID
        {
            set { _planid = value; }
            get { return _planid; }
        }
        /// <summary>
        /// 面试记录编号
        /// </summary>
        public string InterviewNo
        {
            set { _interviewno = value; }
            get { return _interviewno; }
        }
        /// <summary>
        /// 面试者
        /// </summary>
        public string StaffName
        {
            set { _staffname = value; }
            get { return _staffname; }
        }
        /// <summary>
        /// 应聘岗位ID
        /// </summary>
        public string QuarterID
        {
            set { _quarterid = value; }
            get { return _quarterid; }
        }
        /// <summary>
        /// 招聘方式(1公开招聘,2推荐，3内部竞聘)
        /// </summary>
        public string RectType
        {
            set { _recttype = value; }
            get { return _recttype; }
        }
        /// <summary>
        /// 初试日期
        /// </summary>
        public string InterviewDate
        {
            set { _interviewdate = value; }
            get { return _interviewdate; }
        }
        /// <summary>
        /// 初试方式（对应系统分类代码表ID）
        /// </summary>
        public string InterviewType
        {
            set { _interviewtype = value; }
            get { return _interviewtype; }
        }
        /// <summary>
        /// 初试地点
        /// </summary>
        public string InterviewPlace
        {
            set { _interviewplace = value; }
            get { return _interviewplace; }
        }
        /// <summary>
        /// 初试人员意见
        /// </summary>
        public string InterviewNote
        {
            set { _interviewnote = value; }
            get { return _interviewnote; }
        }
        /// <summary>
        /// 初试评测得分
        /// </summary>
        public string TestScore
        {
            set { _testscore = value; }
            get { return _testscore; }
        }
        /// <summary>
        /// 初试结果(1列入考虑，2不予考虑)
        /// </summary>
        public string InterviewResult
        {
            set { _interviewresult = value; }
            get { return _interviewresult; }
        }
        /// <summary>
        /// 复试日期
        /// </summary>
        public string CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 复试方式（对应系统分类代码表ID）
        /// </summary>
        public string CheckType
        {
            set { _checktype = value; }
            get { return _checktype; }
        }
        /// <summary>
        /// 复试地点
        /// </summary>
        public string CheckPlace
        {
            set { _checkplace = value; }
            get { return _checkplace; }
        }
        /// <summary>
        /// 复试人员意见
        /// </summary>
        public string CheckNote
        {
            set { _checknote = value; }
            get { return _checknote; }
        }
        /// <summary>
        /// 综合素质
        /// </summary>
        public string ManNote
        {
            set { _mannote = value; }
            get { return _mannote; }
        }
        /// <summary>
        /// 专业知识
        /// </summary>
        public string KnowNote
        {
            set { _knownote = value; }
            get { return _knownote; }
        }
        /// <summary>
        /// 工作经验
        /// </summary>
        public string WorkNote
        {
            set { _worknote = value; }
            get { return _worknote; }
        }
        /// <summary>
        /// 要求待遇
        /// </summary>
        public string SalaryNote
        {
            set { _salarynote = value; }
            get { return _salarynote; }
        }
        /// <summary>
        /// 复试结果(0不予考虑，1拟予试用)
        /// </summary>
        public string FinalResult
        {
            set { _finalresult = value; }
            get { return _finalresult; }
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
        /// 附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
