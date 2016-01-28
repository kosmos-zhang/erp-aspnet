/**********************************************
 * 类作用：   Holiday表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/03/30
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：HolidayModel
    /// 描述：Holiday表数据模板
    /// 
    /// 作者：lysong
    /// 创建时间：2009/03/30
    /// </summary>
   public class HolidayModel
   {
       #region Holiday表数据模板
        private int _id;
        private string _companycd;
        private string _attendancedate;
        private DateTime _startdate;
        private DateTime _enddate;
        private string _datelong;
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
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 节日名称
        /// </summary>
        public string AttendanceDate
        {
            set { _attendancedate = value; }
            get { return _attendancedate; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 时长
        /// </summary>
        public string DateLong
        {
            set { _datelong = value; }
            get { return _datelong; }
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
