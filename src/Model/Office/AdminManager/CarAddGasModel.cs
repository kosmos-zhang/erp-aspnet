/**********************************************
 * 类作用：   CarAddGas表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/05/04
 ***********************************************/
using System;


namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：CarAddGasModel
    /// 描述：CarAddGas表数据模板
    /// 作者：lysong
    /// 创建时间：2009/05/04
    /// </summary>
   public class CarAddGasModel
   {
        #region CarAddGas表数据模板
        private int _id;
        private string _companycd;
        private string _carno;
        private int _employeeid;
        private DateTime _happendate;
        private decimal _gascount;
        private decimal _fee;
        private string _billno;
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
        /// 经办人
        /// </summary>
        public int EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 加油时间
        /// </summary>
        public DateTime HappenDate
        {
            set { _happendate = value; }
            get { return _happendate; }
        }
        /// <summary>
        /// 加油量（升）
        /// </summary>
        public decimal GasCount
        {
            set { _gascount = value; }
            get { return _gascount; }
        }
        /// <summary>
        /// 加油金额（元）
        /// </summary>
        public decimal Fee
        {
            set { _fee = value; }
            get { return _fee; }
        }
        /// <summary>
        /// 相关发票号
        /// </summary>
        public string BillNo
        {
            set { _billno = value; }
            get { return _billno; }
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
