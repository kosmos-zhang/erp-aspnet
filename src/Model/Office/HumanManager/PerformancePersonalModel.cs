    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
   public  class PerformancePersonalModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _taskno;
        private string _title;
        private string _taskflag;
        private string _taskdate;
        private string  _tasknum;
        private string _startdate;
        private string _enddate;
        private string _workcontent;
        private string _complete;
        private string _afterwork;
        private string _defects;
        private string _problems;
        private string _advices;
        private string _remark;
        private string _status;
        private string  _creator;
        private string _createdate;
        private string  _checker;
        private string _checkdate;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editFlag;

       /// <summary>
       /// 编辑标示符
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
        /// 自我鉴定编号
        /// </summary>
        public string TaskNo
        {
            set { _taskno = value; }
            get { return _taskno; }
        }
        /// <summary>
        /// 自我鉴定主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 鉴定周期分类：
        /// </summary>
        public string TaskFlag
        {
            set { _taskflag = value; }
            get { return _taskflag; }
        }
        /// <summary>
        /// 鉴定年度（对应鉴定周期分类，保存是年份，如2009）
        /// </summary>
        public string TaskDate
        {
            set { _taskdate = value; }
            get { return _taskdate; }
        }
        /// <summary>
        /// 季/月/周（对应周期分类，例如第1季度）
        /// </summary>
        public string  TaskNum
        {
            set { _tasknum = value; }
            get { return _tasknum; }
        }
        /// <summary>
        /// 鉴定期间开始日期
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 鉴定期间结束日期
        /// </summary>
        public string EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 工作内容
        /// </summary>
        public string WorkContent
        {
            set { _workcontent = value; }
            get { return _workcontent; }
        }
        /// <summary>
        /// 完成情况
        /// </summary>
        public string Complete
        {
            set { _complete = value; }
            get { return _complete; }
        }
        /// <summary>
        /// 后续工作
        /// </summary>
        public string AfterWork
        {
            set { _afterwork = value; }
            get { return _afterwork; }
        }
        /// <summary>
        /// 存在不足
        /// </summary>
        public string Defects
        {
            set { _defects = value; }
            get { return _defects; }
        }
        /// <summary>
        /// 存在困难
        /// </summary>
        public string Problems
        {
            set { _problems = value; }
            get { return _problems; }
        }
        /// <summary>
        /// 个人建议
        /// </summary>
        public string Advices
        {
            set { _advices = value; }
            get { return _advices; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 鉴定状态（0草稿，1已确认）
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 创建人(对应员工表ID)
        /// </summary>
        public string  Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public string  Checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public string CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
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
