using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：PerformanceBetterModel
    /// 描述：PerformanceBetter表数据模板（绩效改进计划表）
    /// 
    /// 作者：王保军
    /// 创建日期：2009/05/17
    /// 最后修改日期：2009/05/17
    /// </summary>
    public  class PerformanceBetterModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _planno;
        private string _title;
        private string _remark;
        private string _creator;
        private string _createdate;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editFlag;
        private string _employeeID;
        private string _endDate;
        /// <summary>
        /// 用于查询时用的员工编号
        /// </summary>
        public string EmployeeId
        {
            set { _employeeID = value; }
            get { return _employeeID; }
        }
        /// <summary>
        /// 用于查询时用的结束日期
        /// </summary>
        public string EndDate
        {
            set { _endDate = value; }
            get { return _endDate; }
        }
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
        /// 改进计划主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
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
