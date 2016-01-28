/**********************************************
 * 类作用：   AttendanceReport表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/23
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：AttendanceReportModel
    /// 描述：AttendanceReport表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/23
    /// </summary>
   public class AttendanceReportModel
   {
        #region AttendanceReport表数据模板
        private int _id;
        private string _reprotno;
        private string _companycd;
        private string _reportname;
        private string _month;
        private DateTime _startdate;
        private DateTime _enddate;
        private string _createuserid;
        private DateTime _createdate;
        private string _status;
        private string _reporturl;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 报表编号
        /// </summary>
        public string ReprotNo
        {
            set { _reprotno = value; }
            get { return _reprotno; }
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
        /// 报表名称
        /// </summary>
        public string ReportName
        {
            set { _reportname = value; }
            get { return _reportname; }
        }
        /// <summary>
        /// 所属月份
        /// </summary>
        public string Month
        {
            set { _month = value; }
            get { return _month; }
        }
        /// <summary>
        /// 考核开始时间
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 考核结束时间
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 编制人ID
        /// </summary>
        public string CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 编制日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 状态(0草稿，1提交)
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 报表文件URL
        /// </summary>
        public string ReportURL
        {
            set { _reporturl = value; }
            get { return _reporturl; }
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model

    }
}
