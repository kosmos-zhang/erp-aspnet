/**********************************************
 * 类作用：   InsuEmployee表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/12
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：InsuEmployeeModel
    /// 描述：InsuEmployee表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/12
    /// 最后修改时间：2009/05/12
    /// </summary>
    ///
    public class InsuEmployeeModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _employeeid;
        private string _startdate;
        private string _addr;
        private string _insuranceid;
        private string _insurancebase;
        private string _editflag;
        private string _modifieduserid;
        private string _rowno;
        private string _insucolumnno;
        /// <summary>
        /// 保险列号
        /// </summary>
        public string InsuColumnNo
        {
            set { _insucolumnno = value; }
            get { return _insucolumnno; }
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
        /// 参保时间
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 参保地
        /// </summary>
        public string Addr
        {
            set { _addr = value; }
            get { return _addr; }
        }
        /// <summary>
        /// 保险ID
        /// </summary>
        public string InsuranceID
        {
            set { _insuranceid = value; }
            get { return _insuranceid; }
        }
        /// <summary>
        /// 缴费基数
        /// </summary>
        public string InsuranceBase
        {
            set { _insurancebase = value; }
            get { return _insurancebase; }
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
