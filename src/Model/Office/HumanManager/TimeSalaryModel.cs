/**********************************************
 * 类作用：   TimeSalary表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/13
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：TimeSalaryModel
    /// 描述：TimeSalary表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/13
    /// 最后修改时间：2009/05/13
    /// </summary>
    ///
    public class TimeSalaryModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _employeeid;
        private string _timedate;
        private string _timeno;
        private string _timecount;
        private string _salarymoney;
        private string _modifieduserid;
        private string _editflag;
        /// <summary>
        /// 编辑模式
        /// </summary>
        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
        }
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string ID
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
        /// 员工ID
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public string TimeDate
        {
            set { _timedate = value; }
            get { return _timedate; }
        }
        /// <summary>
        /// 计时项目编号
        /// </summary>
        public string TimeNo
        {
            set { _timeno = value; }
            get { return _timeno; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string TimeCount
        {
            set { _timecount = value; }
            get { return _timecount; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public string SalaryMoney
        {
            set { _salarymoney = value; }
            get { return _salarymoney; }
        }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
