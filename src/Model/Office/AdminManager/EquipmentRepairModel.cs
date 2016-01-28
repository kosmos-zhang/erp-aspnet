
/**********************************************
 * 类作用：   EquipmentRepair表实体类
 * 建立时间： 2009/03/26
 ***********************************************/

using System;


namespace XBase.Model.Office.AdminManager
{
   public class EquipmentRepairModel
    {
        #region 设备维修实体类
        private int _id;
        private string _recordno;
        private string _companycd;
        private string _equipmentno;
        private int _reportuserid;
        private int _deptid;
        private string _trouble;
        private DateTime _date;
        private string _troublelevel;
        private DateTime _hoperepairdate;
        private string _repairtype;
        private DateTime _torepairdate;
        private DateTime _solvedate;
        private string _repairuser;
        private decimal _repairhours;
        private string _repairparts;
        private string _partscheck;
        private string _remark;
        private string _troublestatus;
        private decimal _plancost;
        private decimal _factcost;
        private string _troubledescription;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private DateTime _plandate;
        private DateTime _completedate;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string RecordNo
        {
            set { _recordno = value; }
            get { return _recordno; }
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
        /// 设备编号
        /// </summary>
        public string EquipmentNo
        {
            set { _equipmentno = value; }
            get { return _equipmentno; }
        }
        /// <summary>
        /// 报修人ID
        /// </summary>
        public int ReportUserID
        {
            set { _reportuserid = value; }
            get { return _reportuserid; }
        }
        /// <summary>
        /// 报修人部门ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 故障症状
        /// </summary>
        public string Trouble
        {
            set { _trouble = value; }
            get { return _trouble; }
        }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime Date
        {
            set { _date = value; }
            get { return _date; }
        }
        /// <summary>
        /// 故障程度
        /// </summary>
        public string TroubleLevel
        {
            set { _troublelevel = value; }
            get { return _troublelevel; }
        }
        /// <summary>
        /// 希望修复时间
        /// </summary>
        public DateTime HopeRepairDate
        {
            set { _hoperepairdate = value; }
            get { return _hoperepairdate; }
        }
        /// <summary>
        /// 修理方式
        /// </summary>
        public string RepairType
        {
            set { _repairtype = value; }
            get { return _repairtype; }
        }
        /// <summary>
        /// 送修（自修）时间
        /// </summary>
        public DateTime ToRepairDate
        {
            set { _torepairdate = value; }
            get { return _torepairdate; }
        }
        /// <summary>
        /// 实际修复时间
        /// </summary>
        public DateTime SolveDate
        {
            set { _solvedate = value; }
            get { return _solvedate; }
        }
        /// <summary>
        /// 修理人员
        /// </summary>
        public string RepairUser
        {
            set { _repairuser = value; }
            get { return _repairuser; }
        }
        /// <summary>
        /// 修理工时
        /// </summary>
        public decimal RepairHours
        {
            set { _repairhours = value; }
            get { return _repairhours; }
        }
        /// <summary>
        /// 更换零件
        /// </summary>
        public string RepairParts
        {
            set { _repairparts = value; }
            get { return _repairparts; }
        }
        /// <summary>
        /// 更换零件验收
        /// </summary>
        public string PartsCheck
        {
            set { _partscheck = value; }
            get { return _partscheck; }
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
        /// 故障状态
        /// </summary>
        public string TroubleStatus
        {
            set { _troublestatus = value; }
            get { return _troublestatus; }
        }
        /// <summary>
        /// 预计维修费用
        /// </summary>
        public decimal PlanCost
        {
            set { _plancost = value; }
            get { return _plancost; }
        }
        /// <summary>
        /// 实际维修费用
        /// </summary>
        public decimal FactCost
        {
            set { _factcost = value; }
            get { return _factcost; }
        }
        /// <summary>
        /// 故障及维护描述
        /// </summary>
        public string TroubleDescription
        {
            set { _troubledescription = value; }
            get { return _troubledescription; }
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
        /// 预计修复时间
        /// </summary>
        public DateTime PlanDate
        {
            set { _plandate = value; }
            get { return _plandate; }
        }
        /// <summary>
        /// 完成日期
        /// </summary>
        public DateTime CompleteDate
        {
            set { _completedate = value; }
            get { return _completedate; }
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
