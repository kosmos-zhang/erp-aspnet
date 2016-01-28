/**********************************************
 * 类作用：   EmployeeTraining表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/02
 ***********************************************/
using System;
using System.Collections;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：TrainingModel
    /// 描述：EmployeeTraining表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/02
    /// 最后修改时间：2009/04/02
    /// </summary>
    ///
    public class TrainingModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _trainingno;
        private string _trainingname;
        private string _applydate;
        private string _employeeid;
        private string _projectno;
        private string _projectname;
        private string _trainingorgan;
        private string _plancost;
        private string _trainingcount;
        private string _goal;
        private string _trainingplace;
        private string _trainingway;
        private string _trainingteacher;
        private string _startdate;
        private string _enddate;
        private string _trainingremark;
        private string _checkperson;
        private string _attachment;
        private string _pageAttachment;
        private string _modifieddate;
        private string _modifieduserid;
        private ArrayList _schedulelist;
        private ArrayList _userlist;
        private string _editflag;
        /// <summary>
        /// 编辑标识
        /// </summary>
        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
        }
        /// <summary>
        /// 参与人员列表
        /// </summary>
        public ArrayList UserList
        {
            set { _userlist = value; }
            get
            {
                if (_userlist == null) _userlist = new ArrayList();
                return _userlist;
            }
        }
        /// <summary>
        /// 进度安排列表
        /// </summary>
        public ArrayList ScheduleList
        {
            set { _schedulelist = value; }
            get
            {
                if (_schedulelist == null) _schedulelist = new ArrayList();
                return _schedulelist;
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
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 培训编号
        /// </summary>
        public string TrainingNo
        {
            set { _trainingno = value; }
            get { return _trainingno; }
        }
        /// <summary>
        /// 培训名称
        /// </summary>
        public string TrainingName
        {
            set { _trainingname = value; }
            get { return _trainingname; }
        }
        /// <summary>
        /// 发起时间
        /// </summary>
        public string ApplyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
        }
        /// <summary>
        /// 发起人ID(对应员工表ID)
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNo
        {
            set { _projectno = value; }
            get { return _projectno; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// 培训机构
        /// </summary>
        public string TrainingOrgan
        {
            set { _trainingorgan = value; }
            get { return _trainingorgan; }
        }
        /// <summary>
        /// 费用预算
        /// </summary>
        public string PlanCost
        {
            set { _plancost = value; }
            get { return _plancost; }
        }
        /// <summary>
        /// 培训天数
        /// </summary>
        public string TrainingCount
        {
            set { _trainingcount = value; }
            get { return _trainingcount; }
        }
        /// <summary>
        /// 目的
        /// </summary>
        public string Goal
        {
            set { _goal = value; }
            get { return _goal; }
        }
        /// <summary>
        /// 培训地点
        /// </summary>
        public string TrainingPlace
        {
            set { _trainingplace = value; }
            get { return _trainingplace; }
        }
        /// <summary>
        /// 培训方式ID（对应分类代码表ID
        /// </summary>
        public string TrainingWay
        {
            set { _trainingway = value; }
            get { return _trainingway; }
        }
        /// <summary>
        /// 培训老师
        /// </summary>
        public string TrainingTeacher
        {
            set { _trainingteacher = value; }
            get { return _trainingteacher; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 培训备注
        /// </summary>
        public string TrainingRemark
        {
            set { _trainingremark = value; }
            get { return _trainingremark; }
        }
        /// <summary>
        /// 考核人
        /// </summary>
        public string CheckPerson
        {
            set { _checkperson = value; }
            get { return _checkperson; }
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
        /// 附件
        /// </summary>
        public string PageAttachment
        {
            set { _pageAttachment = value; }
            get { return _pageAttachment; }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string AttachmentName
        {
            set;
            get;
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
