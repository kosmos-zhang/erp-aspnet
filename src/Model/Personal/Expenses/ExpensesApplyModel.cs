/*************************
 * 创建人：何小武
 * 创建日期：2009-9-4
 * 描述：费用申请单model
 *
 * 1. 2010-3-30 添加所属项目ID：ProjectID字段
 * 2. 2010-6-23 添加附件Attachment字段
 * 3. 2010-6-29 添加“报销截止日期”EndReimbTime字段
 **************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Personal.Expenses
{
    public class ExpensesApplyModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _expcode;
        private int _applyor;
        private DateTime? _arisedate;
        private DateTime? _needdate;
        private decimal _totalamount;
        private string _paytype;
        //private int _currencyType;
        //private decimal _currencyRate;
        private string _reason;
        private int _deptid;
        private int _transactorID;
        private string _status;
        private string _title;
        private int _creator;
        private DateTime _createDate;
        private int _expType;
        private int _custID;
        private string _sellChanceNo;
        private string _isReimburse;
        private string _modifiedUserID;
        private DateTime _modifiedDate;
        private int _confirmor;
        private DateTime _confirmDate;
        private string _canViewUser;
        private int? _projectID;
        private string _attachment;
        private DateTime? _endReimbTime;

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string ExpCode
        {
            set { _expcode = value; }
            get { return _expcode; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public int Applyor
        {
            set { _applyor = value; }
            get { return _applyor; }
        }
        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime? AriseDate
        {
            set { _arisedate = value; }
            get { return _arisedate; }
        }
        /// <summary>
        /// 所需日期
        /// </summary>
        public DateTime? NeedDate
        {
            set { _needdate = value; }
            get { return _needdate; }
        }
        /// <summary>
        /// 申请金额
        /// </summary>
        public decimal TotalAmount
        {
            set { _totalamount = value; }
            get { return _totalamount; }
        }
        /// <summary>
        /// 支付方式（现金，银行存款）
        /// </summary>
        public string PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        ///// <summary>
        ///// 币种
        ///// </summary>
        //public int CurrencyType
        //{
        //    set { _currencyType = value; }
        //    get { return _currencyType; }
        //}
        ///// <summary>
        ///// 汇率
        ///// </summary>
        //public decimal CurrencyRate
        //{
        //    set { _currencyRate = value; }
        //    get { return _currencyRate; }
        //}
        /// <summary>
        /// 原因
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        /// <summary>
        /// 所在部门
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 经办人
        /// </summary>
        public int TransactorID
        {
            set { _transactorID = value; }
            get { return _transactorID; }
        }
        /// <summary>
        /// 单据状态（制单，执行，报废）
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
         /// <summary>
         /// 制单人
         /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createDate = value; }
            get { return _createDate; }
        }
        /// <summary>
        /// 费用类别(对应费用类别中的大类ID(公共分类代码表))
        /// </summary>
        public int ExpType
        {
            set { _expType = value; }
            get { return _expType; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustID
        {
            set { _custID = value; }
            get { return _custID; }
        }
        /// <summary>
        /// 销售机会编号
        /// </summary>
        public string SellChanceNo
        {
            set { _sellChanceNo = value; }
            get { return _sellChanceNo; }
        }
        /// <summary>
        /// 是否报销（0 未报销，1 已报销）
        /// </summary>
        public string IsReimburse
        {
            set { _isReimburse = value; }
            get { return _isReimburse; }
        }
        /// <summary>
        /// 最后更新人
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifiedUserID = value; }
            get { return _modifiedUserID; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifiedDate = value; }
            get { return _modifiedDate; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime ConfirmDate
        {
            set { _confirmDate = value; }
            get { return _confirmDate; }
        }
        /// <summary>
        /// 可查看人员
        /// </summary>
        public string CanViewUser
        {
            set { _canViewUser = value; }
            get { return _canViewUser; }
        }
        /// <summary>
        /// 所属项目ID
        /// </summary>
        public int? ProjectID
        {
            set { _projectID = value; }
            get { return _projectID; }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 截止报销日期
        /// </summary>
        public DateTime? EndReimbTime
        {
            set { _endReimbTime = value; }
            get { return _endReimbTime; }
        }
        #endregion Model
    }
}
