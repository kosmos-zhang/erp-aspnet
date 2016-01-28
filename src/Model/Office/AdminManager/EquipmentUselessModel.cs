
/**********************************************
 * 类作用：   Equipmentuseless表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/03/27
 ***********************************************/

using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：EquipmentUselessModel
    /// 描述：Equipmentuseless表数据模板
    /// 
    /// 作者：lysong
    /// 创建时间：2009/03/27
    /// 最后修改时间：2009/02/26
    /// </summary>
   public class EquipmentUselessModel
   {
       #region Equipmentuseless的实体类
       private int _id;
        private string _recordno;
        private string _companycd;
        private string _equipmentno;
        private int _applyuserid;
        private int _deptid;
        private DateTime _applydate;
        private DateTime _uselessdate;
        private string _useddescription;
        private string _uselessreason;
        private decimal _cost;
        private string _uselessstatus;
        private string _remark;
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
        /// 报废单据编号
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
        /// 申请人ID
        /// </summary>
        public int ApplyUserID
        {
            set { _applyuserid = value; }
            get { return _applyuserid; }
        }
        /// <summary>
        /// 申请人部门ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
        }
        /// <summary>
        /// 报废时间
        /// </summary>
        public DateTime UselessDate
        {
            set { _uselessdate = value; }
            get { return _uselessdate; }
        }
        /// <summary>
        /// 设备使用情况说明
        /// </summary>
        public string UsedDescription
        {
            set { _useddescription = value; }
            get { return _useddescription; }
        }
        /// <summary>
        /// 报废原因
        /// </summary>
        public string UselessReason
        {
            set { _uselessreason = value; }
            get { return _uselessreason; }
        }
        /// <summary>
        /// 报废费用
        /// </summary>
        public decimal Cost
        {
            set { _cost = value; }
            get { return _cost; }
        }
        /// <summary>
        /// 报废状态
        /// </summary>
        public string UselessStatus
        {
            set { _uselessstatus = value; }
            get { return _uselessstatus; }
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
        /// 最后更新日期
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
