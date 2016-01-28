using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public class PerformanceTaskModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _taskno;
        private string _title;
        private string _taskflag;
        private string _taskdate;
        private string _tasknum;
        private string _startdate;
        private string _enddate;
        private string _status;
        private string _creator;
        private string _createdate;
        private string _summaryer;
        private string _summarydate;
        private string _completeDate;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editFlag;
       
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
        /// 考核任务编号
        /// </summary>
        public string TaskNo
        {
            set { _taskno = value; }
            get { return _taskno; }
        }
        /// <summary>
        /// 考核任务主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 考核周期分类：
        /// </summary>
        public string TaskFlag
        {
            set { _taskflag = value; }
            get { return _taskflag; }
        }
        /// <summary>
        /// 考核年度（对应考核周期分类，保存是年份，如2009）
        /// </summary>
        public string TaskDate
        {
            set { _taskdate = value; }
            get { return _taskdate; }
        }
        /// <summary>
        /// 季/月/周（对应目标分类，例如第1季度）
        /// </summary>
        public string TaskNum
        {
            set { _tasknum = value; }
            get { return _tasknum; }
        }
        /// <summary>
        /// 考核开始时间
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 考核结束时间
        /// </summary>
        public string EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 任务状态（1待评分，2待总评，3已完成）
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 创建人(对应员工表ID)
        /// </summary>
        public string Creator
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
        /// 汇总人
        /// </summary>
        public string Summaryer
        {
            set { _summaryer = value; }
            get { return _summaryer; }
        }
        /// <summary>
        /// 汇总日期
        /// </summary>
        public string SummaryDate
        {
            set { _summarydate = value; }
            get { return _summarydate; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public string CompleteDate
        {
            set { _completeDate = value; }
            get { return _completeDate; }
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
