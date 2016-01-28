/**********************************************
 * 类作用：   EmployeeContract表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/28
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：EmployeeContractModel
    /// 描述：EmployeeContract表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/28
    /// 最后修改时间：2009/04/28
    /// </summary>
    ///
    public class EmployeeContractModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _contractno;
        private string _employeeid;
        private string _employeeno;
        private string _employeename;
        private string _title;
        private string _contractname;
        private string _contractkind;
        private string _contracttype;
        private string _contractproperty;
        private string _contractstatus;
        private string _contractperiod;
        private string _testwage;
        private string _wage;
        private string _signingdate;
        private string _startdate;
        private string _enddate;
        private string _trialmonthcount;
        private string _flag;
        private string _attachment;
        private string _pageattachment;
        private string _modifieddate;
        private string _modifieduserid;
        private string _attachmentname;
        private string _reminder;
        private string _aheadDay;
        /// <summary>
        /// 提醒人
        /// </summary>
        public string Reminder
        {
            set { _reminder = value; }
            get { return _reminder; }
        }
        /// <summary>
        /// 提前时间
        /// </summary>
        public string AheadDay
        {
            set { _aheadDay = value; }
            get { return _aheadDay; }
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
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo
        {
            set { _contractno = value; }
            get { return _contractno; }
        }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string AttachmentName
        {
            set { _attachmentname = value; }
            get { return _attachmentname; }
        }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo
        {
            set { _employeeno = value; }
            get { return _employeeno; }
        }
        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName
        {
            set { _contractname = value; }
            get { return _contractname; }
        }
        /// <summary>
        /// 工种(1合同工，2临时工，3兼职)
        /// </summary>
        public string ContractKind
        {
            set { _contractkind = value; }
            get { return _contractkind; }
        }
        /// <summary>
        /// 合同类型(1普通员工，2部门经理)
        /// </summary>
        public string ContractType
        {
            set { _contracttype = value; }
            get { return _contracttype; }
        }
        /// <summary>
        /// 合同属性(1程序员，2技术总监)
        /// </summary>
        public string ContractProperty
        {
            set { _contractproperty = value; }
            get { return _contractproperty; }
        }
        /// <summary>
        /// 合同状态(0失效，1有效)
        /// </summary>
        public string ContractStatus
        {
            set { _contractstatus = value; }
            get { return _contractstatus; }
        }
        /// <summary>
        /// 合同期限(1固定期限，2非固定期限)
        /// </summary>
        public string ContractPeriod
        {
            set { _contractperiod = value; }
            get { return _contractperiod; }
        }
        /// <summary>
        /// 试用工资(元)
        /// </summary>
        public string TestWage
        {
            set { _testwage = value; }
            get { return _testwage; }
        }
        /// <summary>
        /// 转正工资(元)
        /// </summary>
        public string Wage
        {
            set { _wage = value; }
            get { return _wage; }
        }
        /// <summary>
        /// 签约时间
        /// </summary>
        public string SigningDate
        {
            set { _signingdate = value; }
            get { return _signingdate; }
        }
        /// <summary>
        /// 生效时间
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 失效时间
        /// </summary>
        public string EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 试用月数
        /// </summary>
        public string TrialMonthCount
        {
            set { _trialmonthcount = value; }
            get { return _trialmonthcount; }
        }
        /// <summary>
        /// 转正标识(0 否、1 是)
        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
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
            set { _pageattachment = value; }
            get { return _pageattachment; }
        }
        /// <summary>
        /// 最后更新时间
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
