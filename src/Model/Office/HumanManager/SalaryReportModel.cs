/**********************************************
 * 类作用：   SalaryReport表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/20
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：SalaryReportModel
    /// 描述：SalaryReport表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/20
    /// 最后修改时间：2009/05/20
    /// </summary>
    ///
    public class SalaryReportModel
    {

        #region Model
        private string _id;
        private string _companycd;
        private string _reprotno;
        private string _reportname;
        private string _reportmonth;
        private string _startdate;
        private string _enddate;
        private string _creator;
        private string _createdate;
        private string _status;
        private string _modifieduserid;
        private string _flowstatus;
        private string _statusname;
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
        /// 工资报表编号
        /// </summary>
        public string ReprotNo
        {
            set { _reprotno = value; }
            get { return _reprotno; }
        }
        /// <summary>
        /// 工资报表主题
        /// </summary>
        public string ReportName
        {
            set { _reportname = value; }
            get { return _reportname; }
        }
        /// <summary>
        /// 工资月份
        /// </summary>
        public string ReportMonth
        {
            set { _reportmonth = value; }
            get { return _reportmonth; }
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
        /// 创建人ID
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 编制日期
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 状态(0待提交，1已生成，2已提交，3已确认)
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 状态(0待提交，1已生成，2已提交，3已确认)
        /// </summary>
        public string StatusName
        {
            set { _statusname = value; }
            get { return _statusname; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }

        public string FlowStatus
        {
            get { return _flowstatus; }
            set { _flowstatus = value; }
        }
        #endregion Model
    }
}
