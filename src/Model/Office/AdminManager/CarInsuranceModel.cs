/**********************************************
 * 类作用：   CarInsurance表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/05/04
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：CarInsuranceModel
    /// 描述：CarInsurance表数据模板
    /// 作者：lysong
    /// 创建时间：2009/05/04
    /// </summary>
   public class CarInsuranceModel
   {
        #region CarInsurance表数据模板
        private int _id;
        private string _companycd;
        private string _carno;
        private int _employeeid;
        private DateTime _happendate;
        private string _insurancecompany;
        private decimal _fee;
        private string _contactuser;
        private string _contact;
        private string _billno;
        private string _items;
        private DateTime _startdate;
        private DateTime _enddate;
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
        /// 办理时间
        /// </summary>
        public DateTime HappenDate
        {
            set { _happendate = value; }
            get { return _happendate; }
        }
        /// <summary>
        /// 保险公司
        /// </summary>
        public string InsuranceCompany
        {
            set { _insurancecompany = value; }
            get { return _insurancecompany; }
        }
        /// <summary>
        /// 保险费用
        /// </summary>
        public decimal Fee
        {
            set { _fee = value; }
            get { return _fee; }
        }
        /// <summary>
        /// 保险公司联系人
        /// </summary>
        public string ContactUser
        {
            set { _contactuser = value; }
            get { return _contactuser; }
        }
        /// <summary>
        /// 保险公司联系电话
        /// </summary>
        public string Contact
        {
            set { _contact = value; }
            get { return _contact; }
        }
        /// <summary>
        /// 保单号
        /// </summary>
        public string BillNo
        {
            set { _billno = value; }
            get { return _billno; }
        }
        /// <summary>
        /// 保险项目
        /// </summary>
        public string Items
        {
            set { _items = value; }
            get { return _items; }
        }
        /// <summary>
        /// 保险开始日期
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 保险失效日期
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
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
