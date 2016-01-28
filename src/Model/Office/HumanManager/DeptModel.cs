/**********************************************
 * 类作用：   DeptInfo表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/09
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：DeptModel
    /// 描述：DeptInfo表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/09
    /// 最后修改时间：2009/04/09
    /// </summary>
    ///
    public class DeptModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _deptno;
        private string _superdeptid;
        private string _pyshort;
        private string _deptname;
        private string _accountflag;
        private string _saleflag;
        private string _subflag;
        private string _address;
        private string _post;
        private string _linkman;
        private string _tel;
        private string _fax;
        private string _email;
        private string _duty;
        private string _usedstatus;
        private string _description;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editflag;
        /// <summary>
        /// 
        /// </summary>
        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeptNO
        {
            set { _deptno = value; }
            get { return _deptno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SuperDeptID
        {
            set { _superdeptid = value; }
            get { return _superdeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PYShort
        {
            set { _pyshort = value; }
            get { return _pyshort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeptName
        {
            set { _deptname = value; }
            get { return _deptname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AccountFlag
        {
            set { _accountflag = value; }
            get { return _accountflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SaleFlag
        {
            set { _saleflag = value; }
            get { return _saleflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubFlag
        {
            set { _subflag = value; }
            get { return _subflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LinkMan
        {
            set { _linkman = value; }
            get { return _linkman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Duty
        {
            set { _duty = value; }
            get { return _duty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
