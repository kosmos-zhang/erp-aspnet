/**********************************************
 * 类作用：   YearHoliday表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/03/31
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
   public class YearHolidayModel
   {
       #region YearHoliday表数据模板
       private int _id;
        private string _companycd;
        private int _employeeid;
        private decimal _holidayhours;
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
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 年休时长
        /// </summary>
        public decimal HolidayHours
        {
            set { _holidayhours = value; }
            get { return _holidayhours; }
        }
        /// <summary>
        /// 最后更新时长
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
