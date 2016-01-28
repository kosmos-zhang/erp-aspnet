/**********************************************
 * 类作用：   MoveApply表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/22
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：MoveApplyModel
    /// 描述：MoveApply表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/22
    /// 最后修改时间：2009/04/22
    /// </summary>
    ///
    public class MoveApplyModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _moveapply;
        private string _title;
        private string _employeeid;
        private string _employeename;
        private string _enterdate;
        private string _applydate;
        private string _hopedate;
        private string _deptid;
        private string _deptname;
        private string _quarterid;
        private string _quartername;
        private string _contractvalid;
        private string _movedate;
        private string _movetype;
        private string _interview;
        private string _reason;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
        private string _flowstatusid;
        private string _flowstatusname;
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 申请人名
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 目前岗位名
        /// </summary>
        public string QuarterName
        {
            set { _quartername = value; }
            get { return _quartername; }
        }
        /// <summary>
        /// 目前部门名
        /// </summary>
        public string DeptName
        {
            set { _deptname = value; }
            get { return _deptname; }
        }
        /// <summary>
        /// 流程状态名称
        /// </summary>
        public string FlowStatusName
        {
            set { _flowstatusname = value; }
            get { return _flowstatusname; }
        }
        /// <summary>
        /// 流程状态ID
        /// </summary>
        public string FlowStatusID
        {
            set { _flowstatusid = value; }
            get { return _flowstatusid; }
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
        /// 离职申请编号
        /// </summary>
        public string MoveApplyNo
        {
            set { _moveapply = value; }
            get { return _moveapply; }
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 入职时间
        /// </summary>
        public string EnterDate
        {
            set { _enterdate = value; }
            get { return _enterdate; }
        }
        /// <summary>
        /// 申请日期
        /// </summary>
        public string ApplyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
        }
        public string ApplyToDate
        {
            set;
            get;
        }
        /// <summary>
        /// 希望离职日期
        /// </summary>
        public string HopeDate
        {
            set { _hopedate = value; }
            get { return _hopedate; }
        }
        /// <summary>
        /// 部门(对应部门表ID)
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 岗位(对应岗位表ID)
        /// </summary>
        public string QuarterID
        {
            set { _quarterid = value; }
            get { return _quarterid; }
        }
        /// <summary>
        /// 合同有效期
        /// </summary>
        public string ContractValid
        {
            set { _contractvalid = value; }
            get { return _contractvalid; }
        }
        /// <summary>
        /// 通知离职日期
        /// </summary>
        public string MoveDate
        {
            set { _movedate = value; }
            get { return _movedate; }
        }
        /// <summary>
        /// 离职类型(1主动辞职,2辞退,3合同期满离职)
        /// </summary>
        public string MoveType
        {
            set { _movetype = value; }
            get { return _movetype; }
        }
        /// <summary>
        /// 访谈记录
        /// </summary>
        public string Interview
        {
            set { _interview = value; }
            get { return _interview; }
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
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
