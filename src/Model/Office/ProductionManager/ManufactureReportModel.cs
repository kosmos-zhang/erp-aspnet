using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class ManufactureReportModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _reportno;
        private string _subject;
        private string _taskno;
        private int _deptid;
        private DateTime _dailydate;
        private int _planhrs;
        private int _realhrs;
        private decimal _planworktime;
        private decimal _addworktime;
        private decimal _stopworktime;
        private decimal _realworktime;
        private int _machinecount;
        private int _opencount;
        private int _opentime;
        private decimal _openpercent;
        private decimal _loadpercent;
        private decimal _usepercent;
        private int _stopcount;
        private int _stoptime;
        private string _stopreason;
        private decimal _productiontotal;
        private decimal _worktimetotal;
        private int _reporter;
        private DateTime _reportdate;
        private decimal _takenum;
        private decimal _usednum;
        private decimal _nownum;
        private string _remark;
        private int _billstatus;
        private int _creator;
        private int _confirmor;
        private DateTime _createdate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 汇报编号
        /// </summary>
        public string ReportNo
        {
            set { _reportno = value; }
            get { return _reportno; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 生产任务单编号
        /// </summary>
        public string TaskNo
        {
            set { _taskno = value; }
            get { return _taskno; }
        }
        /// <summary>
        /// 生产部门ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime DailyDate
        {
            set { _dailydate = value; }
            get { return _dailydate; }
        }
        /// <summary>
        /// 应到人数
        /// </summary>
        public int PlanHRs
        {
            set { _planhrs = value; }
            get { return _planhrs; }
        }
        /// <summary>
        /// 实到人数
        /// </summary>
        public int RealHRs
        {
            set { _realhrs = value; }
            get { return _realhrs; }
        }
        /// <summary>
        /// 应有工时
        /// </summary>
        public decimal PlanWorkTime
        {
            set { _planworktime = value; }
            get { return _planworktime; }
        }
        /// <summary>
        /// 加班工时
        /// </summary>
        public decimal AddWorkTime
        {
            set { _addworktime = value; }
            get { return _addworktime; }
        }
        /// <summary>
        /// 停工工时
        /// </summary>
        public decimal StopWorkTime
        {
            set { _stopworktime = value; }
            get { return _stopworktime; }
        }
        /// <summary>
        /// 有效工时
        /// </summary>
        public decimal RealWorktime
        {
            set { _realworktime = value; }
            get { return _realworktime; }
        }
        /// <summary>
        /// 设备总数
        /// </summary>
        public int MachineCount
        {
            set { _machinecount = value; }
            get { return _machinecount; }
        }
        /// <summary>
        /// 实际开动数
        /// </summary>
        public int OpenCount
        {
            set { _opencount = value; }
            get { return _opencount; }
        }
        /// <summary>
        /// 总开动时长（小时）
        /// </summary>
        public int OpenTime
        {
            set { _opentime = value; }
            get { return _opentime; }
        }
        /// <summary>
        /// 开动率
        /// </summary>
        public decimal OpenPercent
        {
            set { _openpercent = value; }
            get { return _openpercent; }
        }
        /// <summary>
        /// 负荷率
        /// </summary>
        public decimal LoadPercent
        {
            set { _loadpercent = value; }
            get { return _loadpercent; }
        }
        /// <summary>
        /// 设备使用率
        /// </summary>
        public decimal UsePercent
        {
            set { _usepercent = value; }
            get { return _usepercent; }
        }
        /// <summary>
        /// 停机数目
        /// </summary>
        public int StopCount
        {
            set { _stopcount = value; }
            get { return _stopcount; }
        }
        /// <summary>
        /// 停机时长
        /// </summary>
        public int StopTime
        {
            set { _stoptime = value; }
            get { return _stoptime; }
        }
        /// <summary>
        /// 停机原因
        /// </summary>
        public string StopReason
        {
            set { _stopreason = value; }
            get { return _stopreason; }
        }
        /// <summary>
        /// 产量合计
        /// </summary>
        public decimal ProductionTotal
        {
            set { _productiontotal = value; }
            get { return _productiontotal; }
        }
        /// <summary>
        /// 工时合计
        /// </summary>
        public decimal WorkTimeTotal
        {
            set { _worktimetotal = value; }
            get { return _worktimetotal; }
        }
        /// <summary>
        /// 汇报人
        /// </summary>
        public int Reporter
        {
            set { _reporter = value; }
            get { return _reporter; }
        }
        /// <summary>
        /// 汇报日期
        /// </summary>
        public DateTime ReportDate
        {
            set { _reportdate = value; }
            get { return _reportdate; }
        }
        /// <summary>
        /// 领入合计
        /// </summary>
        public decimal TakeNum
        {
            set { _takenum = value; }
            get { return _takenum; }
        }
        /// <summary>
        /// 耗用合计
        /// </summary>
        public decimal UsedNum
        {
            set { _usednum = value; }
            get { return _usednum; }
        }
        /// <summary>
        /// 结存合计
        /// </summary>
        public decimal NowNum
        {
            set { _nownum = value; }
            get { return _nownum; }
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
        /// 单据状态
        /// </summary>
        public int BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 制单人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID（对应操作用户表的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
