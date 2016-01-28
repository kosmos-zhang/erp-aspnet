/**********************************************
 * 类作用：   SalaryReportDetail表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/20
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：SalaryReportDetailModel
    /// 描述：SalaryReportDetail表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/20
    /// 最后修改时间：2009/05/20
    /// </summary>
    ///
    public class SalaryReportDetailModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _reprotno;
        private string _employeeid;
        private string _itemno;
        private string _itemname;
        private string _itemorder;
        private string _payflag;
        private string _salarymoney;
        private string _modifieddate;
        private string _modifieduserid;
        private string _changeFlag;
        /// <summary>
        /// 是否固定
        /// </summary>
        public string ChangeFlag
        {
            set { _changeFlag = value; }
            get { return _changeFlag; }
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
        /// 工资报表编号
        /// </summary>
        public string ReprotNo
        {
            set { _reprotno = value; }
            get { return _reprotno; }
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
        /// 工资项编号
        /// </summary>
        public string ItemNo
        {
            set { _itemno = value; }
            get { return _itemno; }
        }
        /// <summary>
        /// 工资项名称
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        /// <summary>
        /// 排列先后顺序
        /// </summary>
        public string ItemOrder
        {
            set { _itemorder = value; }
            get { return _itemorder; }
        }
        /// <summary>
        /// 是否扣款(0 否，1是)

        /// </summary>
        public string PayFlag
        {
            set { _payflag = value; }
            get { return _payflag; }
        }
        /// <summary>
        /// 工资额
        /// </summary>
        public string SalaryMoney
        {
            set { _salarymoney = value; }
            get { return _salarymoney; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
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
