/**********************************************
 * 类作用：   CarMaintain表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/05/04
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：CarMaintainModel
    /// 描述：CarMaintain表数据模板
    /// 作者：lysong
    /// 创建时间：2009/05/04
    /// </summary>
   public class CarMaintainModel
   {
        #region CarMaintain表数据模板
        private int _id;
        private string _companycd;
        private string _carno;
        private int _employeeid;
        private DateTime _happendate;
        private decimal _fee;
        private string _billno;
        private string _items;
        private decimal _nowmileage;
        private decimal _nextmileage;
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
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 车辆编号（对应车辆信息表编号）
        /// </summary>
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 经办人（对应员工表ID）
        /// </summary>
        public int EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 保养日期
        /// </summary>
        public DateTime HappenDate
        {
            set { _happendate = value; }
            get { return _happendate; }
        }
        /// <summary>
        /// 保养费用（元）
        /// </summary>
        public decimal Fee
        {
            set { _fee = value; }
            get { return _fee; }
        }
        /// <summary>
        /// 相关单据号
        /// </summary>
        public string BillNo
        {
            set { _billno = value; }
            get { return _billno; }
        }
        /// <summary>
        /// 保养项目
        /// </summary>
        public string Items
        {
            set { _items = value; }
            get { return _items; }
        }
        /// <summary>
        /// 当前里程
        /// </summary>
        public decimal NowMileage
        {
            set { _nowmileage = value; }
            get { return _nowmileage; }
        }
        /// <summary>
        /// 下次保养公里数
        /// </summary>
        public decimal NextMileage
        {
            set { _nextmileage = value; }
            get { return _nextmileage; }
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
