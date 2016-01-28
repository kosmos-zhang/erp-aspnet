using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
   public  class PerformanceSummaryModel
    {
        #region Model
        private string  _id;
        private string _companycd;
        private string _taskno;
        private string _templateno;
        private string  _employeeid;
        private string  _totalscore;
        private string _rewardnote;
        private string  _killscore;
        private string  _addscore;
        private string  _realscore;
        private string _summarynote;
        private string _leveltype;
        private string _advicetype;
        private string _advicenote;
        private string _remark;
        private string  _evaluater;
        private string  _evaluatedate;
        private string  _signdate;
        private string _signnote;
        private string  _modifieddate;
        private string _modifieduserid;
        private string _title;
        private string _taskFlag;
        private string _taskNum;
        private string _countPerson;
        private string _deptName;
        /// <summary> 
        /// 被考核人所在部门(考核统计时用)
        /// </summary>
        public string DeptName
        {
            set { _deptName = value; }
            get { return _deptName; }
        }
        /// <summary> 
        /// 统计人数(考核统计时用)
        /// </summary>
        public string CountPerson
        {
            set { _countPerson = value; }
            get { return _countPerson; }
        }
        /// <summary> 
        /// 考核期间(考核统计时用)
        /// </summary>
        public string TaskNum
        {
            set { _taskNum = value; }
            get { return _taskNum; }
        }
        /// <summary> 
        /// 考核期间类型(考核统计时用)
        /// </summary>
        public string TaskFlag
        {
            set { _taskFlag = value; }
            get { return _taskFlag; }
        }
        /// <summary>  
        /// 考核任务名称(考核统计时用)
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
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
        /// 考核任务编号
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
        public string  EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 考核总得分
        /// </summary>
        public string  TotalScore
        {
            set { _totalscore = value; }
            get { return _totalscore; }
        }
        /// <summary>
        /// 奖惩说明
        /// </summary>
        public string RewardNote
        {
            set { _rewardnote = value; }
            get { return _rewardnote; }
        }
        /// <summary>
        /// 累计扣分
        /// </summary>
        public string  KillScore
        {
            set { _killscore = value; }
            get { return _killscore; }
        }
        /// <summary>
        /// 累计加分
        /// </summary>
        public string  AddScore
        {
            set { _addscore = value; }
            get { return _addscore; }
        }
        /// <summary>
        /// 实际得分
        /// </summary>
        public string  RealScore
        {
            set { _realscore = value; }
            get { return _realscore; }
        }
        /// <summary>
        /// 总评
        /// </summary>
        public string SummaryNote
        {
            set { _summarynote = value; }
            get { return _summarynote; }
        }
        /// <summary>
        /// 考核等级（1达到要求，2超过要求，3表现突出，4需要改进，5不合格）
        /// </summary>
        public string LevelType
        {
            set { _leveltype = value; }
            get { return _leveltype; }
        }
        /// <summary>
        /// 考核建议（1不做处理，2调整薪资，3晋升，4调职，5辅导，6培训，7辞退）
        /// </summary>
        public string AdviceType
        {
            set { _advicetype = value; }
            get { return _advicetype; }
        }
        /// <summary>
        /// 建议说明
        /// </summary>
        public string AdviceNote
        {
            set { _advicenote = value; }
            get { return _advicenote; }
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
        /// 总评人
        /// </summary>
        public string  Evaluater
        {
            set { _evaluater = value; }
            get { return _evaluater; }
        }
        /// <summary>
        /// 总评日期
        /// </summary>
        public string  EvaluateDate
        {
            set { _evaluatedate = value; }
            get { return _evaluatedate; }
        }
        /// <summary>
        /// 被考核人确认时间
        /// </summary>
        public string  SignDate
        {
            set { _signdate = value; }
            get { return _signdate; }
        }
        /// <summary>
        /// 被考核人反馈
        /// </summary>
        public string SignNote
        {
            set { _signnote = value; }
            get { return _signnote; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string  ModifiedDate
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
