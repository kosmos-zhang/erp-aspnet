/**********************************************
 * 类作用：   MoveNotify表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/24
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：MoveNotifyModel
    /// 描述：MoveNotify表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/24
    /// 最后修改时间：2009/04/24
    /// </summary>
    ///
    public class MoveNotifyModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _notifyno;
        private string _title;
        private string _moveapply;
        private string _employeeid;
        private string _employeeno;
        private string _employeename;
        private string _deptname;
        private string _reason;
        private string _outdate;
        private string _jobnote;
        private string _remark;
        private string _billstatus;
        private string _creator;
        private string _createdate;
        private string _confirmor;
        private string _confirmdate;
        private string _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 所属部门
        /// </summary>
        public string DeptName
        {
            set { _deptname = value; }
            get { return _deptname; }
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
        /// 离职单编号
        /// </summary>
        public string NotifyNo
        {
            set { _notifyno = value; }
            get { return _notifyno; }
        }
        /// <summary>
        /// 离职单主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 离职申请编号
        /// </summary>
        public string MoveApplyNo
        {
            set { _moveapply = value; }
            get { return _moveapply; }
        }
        /// <summary>
        /// 离职人
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 离职事由
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        /// <summary>
        /// 离职时间
        /// </summary>
        public string OutDate
        {
            set { _outdate = value; }
            get { return _outdate; }
        }
        public string OutToDate
        {
            set;
            get;
        }
        /// <summary>
        /// 离职交接说明
        /// </summary>
        public string JobNote
        {
            set { _jobnote = value; }
            get { return _jobnote; }
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
