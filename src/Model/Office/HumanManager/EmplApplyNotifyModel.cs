/**********************************************
 * 类作用：   EmplApplyNotify表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/24
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：EmplApplyNotifyModel
    /// 描述：EmplApplyNotify表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/24
    /// 最后修改时间：2009/04/24
    /// </summary>
    ///
    public class EmplApplyNotifyModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _notifyno;
        private string _title;
        private string _emplapplyno;
        private string _employeeid;
        private string _employeename;
        private string _employeeno;
        private string _nowdeptid;
        private string _nowdeptname;
        private string _nowquarterid;
        private string _nowquartername;
        private string _nowadminlevel;
        private string _nowadminlevelname;
        private string _newdeptid;
        private string _newdeptname;
        private string _newquarterid;
        private string _newquartername;
        private string _newadminlevel;
        private string _newadminlevelname;
        private string _reason;
        private string _outdate;
        private string _stringdate;
        private string _remark;
        private string _billstatus;
        private string _creator;
        private string _createdate;
        private string _confirmor;
        private string _confirmdate;
        private string _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo
        {
            set { _employeeno = value; }
            get { return _employeeno; }
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
        /// 调至岗位职等名称
        /// </summary>
        public string NewAdminLevelName
        {
            set { _newadminlevelname = value; }
            get { return _newadminlevelname; }
        }
        /// <summary>
        /// 调至岗位名称
        /// </summary>
        public string NewQuarterName
        {
            set { _newquartername = value; }
            get { return _newquartername; }
        }
        /// <summary>
        /// 调至部门名称
        /// </summary>
        public string NewDeptName
        {
            set { _newdeptname = value; }
            get { return _newdeptname; }
        }
        /// <summary>
        /// 原岗位职等名称
        /// </summary>
        public string NowAdminLevelName
        {
            set { _nowadminlevelname = value; }
            get { return _nowadminlevelname; }
        }
        /// <summary>
        /// 原岗位名称
        /// </summary>
        public string NowQuarterName
        {
            set { _nowquartername = value; }
            get { return _nowquartername; }
        }
        /// <summary>
        /// 原部门名称
        /// </summary>
        public string NowDeptName
        {
            set { _nowdeptname = value; }
            get { return _nowdeptname; }
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
        /// 调职单编号
        /// </summary>
        public string NotifyNo
        {
            set { _notifyno = value; }
            get { return _notifyno; }
        }
        /// <summary>
        /// 调职单主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 对应调职申请编号
        /// </summary>
        public string EmplApplyNo
        {
            set { _emplapplyno = value; }
            get { return _emplapplyno; }
        }
        /// <summary>
        /// 被调职人
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 原部门(对应部门表ID)
        /// </summary>
        public string NowDeptID
        {
            set { _nowdeptid = value; }
            get { return _nowdeptid; }
        }
        /// <summary>
        /// 原岗位(对应岗位表ID)
        /// </summary>
        public string NowQuarterID
        {
            set { _nowquarterid = value; }
            get { return _nowquarterid; }
        }
        /// <summary>
        /// 原岗位职等（分类代码表设置）
        /// </summary>
        public string NowAdminLevel
        {
            set { _nowadminlevel = value; }
            get { return _nowadminlevel; }
        }
        /// <summary>
        /// 调至部门ID
        /// </summary>
        public string NewDeptID
        {
            set { _newdeptid = value; }
            get { return _newdeptid; }
        }
        /// <summary>
        /// 调至岗位ID
        /// </summary>
        public string NewQuarterID
        {
            set { _newquarterid = value; }
            get { return _newquarterid; }
        }
        /// <summary>
        /// 调至岗位职等（分类代码表设置）
        /// </summary>
        public string NewAdminLevel
        {
            set { _newadminlevel = value; }
            get { return _newadminlevel; }
        }
        /// <summary>
        /// 事由
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        /// <summary>
        /// 调出时间
        /// </summary>
        public string OutDate
        {
            set { _outdate = value; }
            get { return _outdate; }
        }
        /// <summary>
        /// 调入时间
        /// </summary>
        public string IntDate
        {
            set { _stringdate = value; }
            get { return _stringdate; }
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
        /// 单据状态（1制单，2结单）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 制单人
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public string Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public string ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
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
