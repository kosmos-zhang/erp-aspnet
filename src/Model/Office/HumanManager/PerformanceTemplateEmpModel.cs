using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
   public  class PerformanceTemplateEmpModel
    {
        #region Model
        private string  _id;
        private string _companycd;
        private string _templateno;
        private string  _employeeid;
        private string  _stepno;
        private string _stepname;
        private string  _rate;
        private string  _scoreemployee;
        private string _remark;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _editFlag;
        private string _templateName;
        private string _employeeName;
        private string _scoreEmployeeName;
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string  ID
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
        public string  EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 步骤顺序
        /// </summary>
        public string  StepNo
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
        /// 考核得分权重
        /// </summary>
        public string  Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 考核打分人员（对应员工表ID）
        /// </summary>
        public string  ScoreEmployee
        {
            set { _scoreemployee = value; }
            get { return _scoreemployee; }
        }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime ModifiedDate
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
        /// <summary>
        ///编辑标示 
        /// </summary>
        public string EditFlag
        {
            set
            {
                _editFlag = value;
            }
            get
            {
                return _editFlag;
            }
        }
       /// <summary>
        /// 模板名称 EmployeeName  ScoreEmployeeName
       /// </summary>
        public string TemplateName
        {
            set { _templateName =value ;}
            get { return _templateName; }
        }
        /// <summary>
        ///被考评人名称 
        /// </summary>
        public string EmployeeName
        {
            set { _employeeName  = value; }
            get { return _employeeName; }
        }
        /// <summary>
        ///考评人名称
        /// </summary>
        public string ScoreEmployeeName
        {
            set { _scoreEmployeeName = value; }
            get { return _scoreEmployeeName; }
        }
    
        #endregion Model
    }
}
