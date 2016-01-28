using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
  public   class PerformanceScoreModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _taskno;
        private string _templateno;
        private string _employeeid;
        private string _elemid;
        private string _elemname;
        private string _stepno;
        private string _stepname;
        private string _scoreemployee;
        private string _score;
        private string _scoredate;
        private string _advicenote;
        private string _feeback;
        private string _note;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editFlag;
        private string _rate;
        private string _taskFlag;
        private string _title;
        private string _templateName;
        private string _employeeName;
        private string _scoreEmployeeName;
        private string _createEmployeeName;
        private string _createDate;
        private string _taskFlagName;
        private string _status;

      /// <summary>
        /// 评分状态 0表示草稿状态,1代表已确认状态
      /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }

      /// <summary>
      /// 周期类型名称
      /// </summary>
        public string TaskFlagName
        {
            set { _taskFlagName = value; }
            get { return _taskFlagName; }
        }
      /// <summary>
      /// 创建日期
      /// </summary>
        public string CreateDate
        {
            set { _createDate = value; }
            get { return _createDate; }
        
        }
      /// <summary>
      /// 创建人姓名
      /// </summary>
        public string CreateEmployeeName
        {
            set { _createEmployeeName = value; }
            get { return _createEmployeeName; }
        }
      /// <summary>
        /// 考评人姓名
      /// </summary>
        public string ScoreEmployeeName
        {
            set { _scoreEmployeeName = value; }
            get { return _scoreEmployeeName; }
        }

      /// <summary>
        /// 被考评人姓名
      /// </summary>
        public string EmployeeName
        {
            set { _employeeName = value; }
            get { return _employeeName; }
        }
      /// <summary>
      /// 考核模板主题
      /// </summary>
        public string TemplateName
        {
            set { _templateName = value; }
            get { return _templateName; }
        }

      /// <summary>
        /// 考核任务主题
      /// </summary>
        public string TaskTitle
        {
            set { _title = value; }
            get { return _title; }
        }
      /// <summary>
        /// 考核周期分类
      /// </summary>
        public string TaskFlag
        {
            set { _taskFlag = value; }
            get { return _taskFlag; }
        }
        /// <summary>
        /// 考核权重信息
        /// </summary>
        public string Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
      /// <summary>
      /// 编辑标示
      /// </summary>
        public string EditFlag
        {
            set { _editFlag = value; }
            get { return _editFlag; }
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
        /// 考核任务编号（对应考核任务表中的编号）
        /// </summary>
        public string TaskNo
        {
            set { _taskno = value; }
            get { return _taskno; }
        }
        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateNo
        {
            set { _templateno = value; }
            get { return _templateno; }
        }
        /// <summary>
        /// 被考核人（对应员工表ID）
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 指标ID
        /// </summary>
        public string ElemID
        {
            set { _elemid = value; }
            get { return _elemid; }
        }
        /// <summary>
        /// 指标名称
        /// </summary>
        public string ElemName
        {
            set { _elemname = value; }
            get { return _elemname; }
        }
        /// <summary>
        /// 步骤顺序
        /// </summary>
        public string StepNo
        {
            set { _stepno = value; }
            get { return _stepno; }
        }
        /// <summary>
        /// 步骤名称（如：初评，复评）
        /// </summary>
        public string StepName
        {
            set { _stepname = value; }
            get { return _stepname; }
        }
        /// <summary>
        /// 评分人员(对应员工表ID)
        /// </summary>
        public string ScoreEmployee
        {
            set { _scoreemployee = value; }
            get { return _scoreemployee; }
        }
        /// <summary>
        /// 打分
        /// </summary>
        public string Score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 评分日期
        /// </summary>
        public string ScoreDate
        {
            set { _scoredate = value; }
            get { return _scoredate; }
        }
        /// <summary>
        /// 评语
        /// </summary>
        public string AdviceNote
        {
            set { _advicenote = value; }
            get { return _advicenote; }
        }
        /// <summary>
        /// 被考核人反馈
        /// </summary>
        public string FeeBack
        {
            set { _feeback = value; }
            get { return _feeback; }
        }
        /// <summary>
        /// 说明事项
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
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
