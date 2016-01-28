/**********************************************
 * 类作用：   EmplApply表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/22
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：EmplApplyModel
    /// 描述：EmplApply表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/22
    /// 最后修改时间：2009/04/22
    /// </summary>
    ///
    public class EmplApplyModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _emplapplyno;
        private string _employeeid;
        private string _employeename;
        private string _enterdate;
        private string _applydate;
        private string _hopedate;
        private string _nowdeptid;
        private string _nowdeptname;
        private string _nowquarterid;
        private string _nowquartername;
        private string _nowadminlevelid;
        private string _nowadminlevelname;
        private string _nowwage;
        private string _newdeptid;
        private string _newdeptname;
        private string _newquarterid;
        private string _newquartername;
        private string _newadminlevelid;
        private string _newadminlevelname;
        private string _newwage;
        private string _applytype;
        private string _reason;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
        private string _flowstatusid;
        private string _flowstatusname;
        private string _title;

        /// <summary>
        /// 调至岗位职等ID
        /// </summary>
        public string NewAdminLevelID
        {
            set { _newadminlevelid = value; }
            get { return _newadminlevelid; }
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
        /// 目前岗位职等ID
        /// </summary>
        public string NowAdminLevelID
        {
            set { _nowadminlevelid = value; }
            get { return _nowadminlevelid; }
        }
        /// <summary>
        /// 目前岗位职等名称
        /// </summary>
        public string NowAdminLevelName
        {
            set { _nowadminlevelname = value; }
            get { return _nowadminlevelname; }
        }
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
        /// 调至岗位名
        /// </summary>
        public string NewQuarterName
        {
            set { _newquartername = value; }
            get { return _newquartername; }
        }
        /// <summary>
        /// 调至部门名
        /// </summary>
        public string NewDeptName
        {
            set { _newdeptname = value; }
            get { return _newdeptname; }
        }
        /// <summary>
        /// 目前岗位名
        /// </summary>
        public string NowQuarterName
        {
            set { _nowquartername = value; }
            get { return _nowquartername; }
        }
        /// <summary>
        /// 目前部门名
        /// </summary>
        public string NowDeptName
        {
            set { _nowdeptname = value; }
            get { return _nowdeptname; }
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
        /// 调职申请编号
        /// </summary>
        public string EmplApplyNo
        {
            set { _emplapplyno = value; }
            get { return _emplapplyno; }
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
        /// 希望日期
        /// </summary>
        public string HopeDate
        {
            set { _hopedate = value; }
            get { return _hopedate; }
        }
        /// <summary>
        /// 部门(对应部门表ID)
        /// </summary>
        public string NowDeptID
        {
            set { _nowdeptid = value; }
            get { return _nowdeptid; }
        }
        /// <summary>
        /// 岗位(对应岗位表ID)
        /// </summary>
        public string NowQuarterID
        {
            set { _nowquarterid = value; }
            get { return _nowquarterid; }
        }
        /// <summary>
        /// 调职前工资
        /// </summary>
        public string NowWage
        {
            set { _nowwage = value; }
            get { return _nowwage; }
        }
        /// <summary>
        /// 调至部门
        /// </summary>
        public string NewDeptID
        {
            set { _newdeptid = value; }
            get { return _newdeptid; }
        }
        /// <summary>
        /// 调至岗位
        /// </summary>
        public string NewQuarterID
        {
            set { _newquarterid = value; }
            get { return _newquarterid; }
        }
        /// <summary>
        /// 调职后工资
        /// </summary>
        public string NewWage
        {
            set { _newwage = value; }
            get { return _newwage; }
        }
        /// <summary>
        /// 申报类别
        /// </summary>
        public string ApplyType
        {
            set { _applytype = value; }
            get { return _applytype; }
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
