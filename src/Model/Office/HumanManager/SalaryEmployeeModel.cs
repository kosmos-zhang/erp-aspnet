/**********************************************
 * 类作用：   SalaryEmployee表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/08
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：SalaryEmployeeModel
    /// 描述：SalaryEmployee表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/08
    /// 最后修改时间：2009/05/08
    /// </summary>
    ///
    public class SalaryEmployeeModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _employeeid;
        private string _flag;
        private string _itemno;
        private string _salarymoney;
        private string _editflag;
        private string _modifieduserid;
        private string _rowno;
        private string _salarycolumnno;
        /// <summary>
        /// 工资列编号
        /// </summary>
        public string SalaryColumnNo
        {
            set { _salarycolumnno = value; }
            get { return _salarycolumnno; }
        }
        /// <summary>
        /// 行编号
        /// </summary>
        public string RowNo
        {
            set { _rowno = value; }
            get { return _rowno; }
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
        /// 区分(1基数，2实际)

        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
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
        /// 金额
        /// </summary>
        public string SalaryMoney
        {
            set { _salarymoney = value; }
            get { return _salarymoney; }
        }
        /// <summary>
        /// 编辑模式
        /// </summary>
        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
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
