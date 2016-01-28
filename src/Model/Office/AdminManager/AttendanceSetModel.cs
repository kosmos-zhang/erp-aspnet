/**********************************************
 * 类作用：   WorkDay表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/03/30
 ***********************************************/
using System;
namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：AttendanceSetModel
    /// 描述：WorkDay表数据模板
    /// 
    /// 作者：lysong
    /// 创建时间：2009/03/30
    /// </summary>
   public class AttendanceSetModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _weekday;
        private int _workflag;
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
        /// 星期
        /// </summary>
        public int WeekDay
        {
            set { _weekday = value; }
            get { return _weekday; }
        }
        /// <summary>
        /// 工作日区分
        /// </summary>
        public int WorkFlag
        {
            set { _workflag = value; }
            get { return _workflag; }
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
