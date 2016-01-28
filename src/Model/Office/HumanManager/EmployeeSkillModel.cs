/**********************************************
 * 类作用：   EmployeeSkill表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/09
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：EmployeeSkillModel
    /// 描述：EmployeeSkill表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/09
    /// 最后修改时间：2009/03/09
    /// </summary>
    ///
    public class EmployeeSkillModel
    {

        #region Model
        private int _id;
        private string _companycd;
        private string _employeesid;
        private string _skillname;
        private string _certificatename;
        private string _certificateno;
        private string _certificatelevel;
        private string _issuecompany;
        private string _issuedate;
        private string _validity;
        private string _deaddate;
        private string _remark;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public int ID
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
        /// 员工ID
        /// </summary>
        public string EmployeeNo
        {
            set { _employeesid = value; }
            get { return _employeesid; }
        }
        /// <summary>
        /// 技能名称
        /// </summary>
        public string SkillName
        {
            set { _skillname = value; }
            get { return _skillname; }
        }
        /// <summary>
        /// 证件名称
        /// </summary>
        public string CertificateName
        {
            set { _certificatename = value; }
            get { return _certificatename; }
        }
        /// <summary>
        /// 证件编号
        /// </summary>
        public string CertificateNo
        {
            set { _certificateno = value; }
            get { return _certificateno; }
        }
        /// <summary>
        /// 证件等级
        /// </summary>
        public string CertificateLevel
        {
            set { _certificatelevel = value; }
            get { return _certificatelevel; }
        }
        /// <summary>
        /// 发证单位
        /// </summary>
        public string IssueCompany
        {
            set { _issuecompany = value; }
            get { return _issuecompany; }
        }
        /// <summary>
        /// 发证时间
        /// </summary>
        public string IssueDate
        {
            set { _issuedate = value; }
            get { return _issuedate; }
        }
        /// <summary>
        /// 有效期
        /// </summary>
        public string Validity
        {
            set { _validity = value; }
            get { return _validity; }
        }
        /// <summary>
        /// 失效时间
        /// </summary>
        public string DeadDate
        {
            set { _deaddate = value; }
            get { return _deaddate; }
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
        #endregion Model

    }
}
