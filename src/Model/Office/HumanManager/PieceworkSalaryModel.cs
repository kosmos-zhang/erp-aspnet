/**********************************************
 * 类作用：   PieceworkSalary表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/13
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：PieceworkSalaryModel
    /// 描述：PieceworkSalary表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/13
    /// 最后修改时间：2009/05/13
    /// </summary>
    ///
    public class PieceworkSalaryModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _employeeid;
        private string _piecedate;
        private string _itemno;
        private string _amount;
        private string _salarymoney;
        private string _editflag;
        private string _modifieduserid;
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
        public string PieceDate
        {
            set { _piecedate = value; }
            get { return _piecedate; }
        }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ItemNo
        {
            set { _itemno = value; }
            get { return _itemno; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string Amount
        {
            set { _amount = value; }
            get { return _amount; }
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
