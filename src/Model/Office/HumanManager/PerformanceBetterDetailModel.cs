using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{  
    /// <summary>
    /// 类名：PerformanceBetterDetailModel
    /// 描述：PerformanceBetterDetail表数据模板（绩效改进计划详细表）
    /// 
    /// 作者：王保军
    /// 创建日期：2009/05/17
    /// 最后修改日期：2009/05/17
    /// </summary>
   public   class PerformanceBetterDetailModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _planno;
        private string _employeeid;
        private string _content;
        private string _completeaim;
        private string _completedate;
        private string _checker;
        private string _checkdate;
        private string _checkresult;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editFlag;
       /// <summary>
       ///编辑标识符
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
        /// 改进计划编号
        /// </summary>
        public string PlanNo
        {
            set { _planno = value; }
            get { return _planno; }
        }
        /// <summary>
        /// 改进员工(对应员工表ID)
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 待改进绩效
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 完成目标
        /// </summary>
        public string CompleteAim
        {
            set { _completeaim = value; }
            get { return _completeaim; }
        }
        /// <summary>
        /// 完成期限
        /// </summary>
        public string CompleteDate
        {
            set { _completedate = value; }
            get { return _completedate; }
        }
        /// <summary>
        /// 核查人
        /// </summary>
        public string Checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 核查时间
        /// </summary>
        public string CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 核查结果
        /// </summary>
        public string CheckResult
        {
            set { _checkresult = value; }
            get { return _checkresult; }
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
