/**********************************************
 * 类作用：   WorkShiftSet表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/07
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：WorkShiftSetModel
    /// 描述：WorkShiftSet表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/07
    /// </summary>
   public class WorkShiftSetModel
   {
       #region WorkShiftSet表数据模板
       private int _id;
        private string _companycd;
        private string _workshiftno;
        private string _workshiftname;
        private int _dayworklong;
        private int _lateabsent;
        private int _forwardabsent;
        private int _workovertime;
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
        /// 班次编号
        /// </summary>
        public string WorkShiftNo
        {
            set { _workshiftno = value; }
            get { return _workshiftno; }
        }
        /// <summary>
        /// 班次名称
        /// </summary>
        public string WorkShiftName
        {
            set { _workshiftname = value; }
            get { return _workshiftname; }
        }
        /// <summary>
        /// 日工作时间（分钟）
        /// </summary>
        public int DayWorkLong
        {
            set { _dayworklong = value; }
            get { return _dayworklong; }
        }
        /// <summary>
        /// 迟到多长时间算旷工(分)
        /// </summary>
        public int LateAbsent
        {
            set { _lateabsent = value; }
            get { return _lateabsent; }
        }
        /// <summary>
        /// 早退多长时间算旷工
        /// </summary>
        public int ForwardAbsent
        {
            set { _forwardabsent = value; }
            get { return _forwardabsent; }
        }
        /// <summary>
        /// 加班多长时间算加班（分）
        /// </summary>
        public int WorkOvertime
        {
            set { _workovertime = value; }
            get { return _workovertime; }
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
