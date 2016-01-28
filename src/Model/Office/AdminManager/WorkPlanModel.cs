/**********************************************
 * 类作用：   WorkPlan表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/10
 ***********************************************/
using System;


namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：WorkPlanModel
    /// 描述：WorkPlan表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/10
    /// </summary>
   public class WorkPlanModel
   {
        #region WorkPlan表数据模板
        private int _id;
        private string _companycd;
        private string _workshiftindex;
        private string _workshiftno;
        private DateTime _workplanstartdate;
        private DateTime _workplanenddate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _workgroupno;
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
        /// 班组编号
        /// </summary>
        public string WorkGroupNo
        {
            set { _workgroupno = value; }
            get { return _workgroupno; }
        }
        /// <summary>
        /// 对应班次序号
        /// </summary>
        public string WorkShiftIndex
        {
            set { _workshiftindex = value; }
            get { return _workshiftindex; }
        }
        /// <summary>
        /// 对应班次编号
        /// </summary>
        public string WorkShiftNo
        {
            set { _workshiftno = value; }
            get { return _workshiftno; }
        }
        /// <summary>
        /// 排班开始日
        /// </summary>
        public DateTime WorkPlanStartDate
        {
            set { _workplanstartdate = value; }
            get { return _workplanstartdate; }
        }
        /// <summary>
        /// 排班结束日
        /// </summary>
        public DateTime WorkPlanEndDate
        {
            set { _workplanenddate = value; }
            get { return _workplanenddate; }
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
