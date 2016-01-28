/*****************************************
 *创建人：何小武
 *创建日期：2009-9-14
 *描述：费用报销
 *修改：
 * 1. 2010-3-30 添加所属项目ID：ProjectID字段
 * 2. 2010-4-27 添加字段：SubjectsNo,IsAccount,AccountDate,Accountor,AttestBillID. 
 * 3. 2010-5-11 添加字段：CustID，ContactsUnitID，FromTBName,ContactsUnitName。
 * 4. 2010-6-23 添加字段：Attachment。
 *****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Personal.Expenses
{
    public class ReimbursementModel
    {
        private int _id;
        private string _companycd;
        private string _reimbno;
        private string _title;
        private int _applyor;
        private DateTime? _reimbdate;
        private decimal _expallamount;
        private decimal? _reimballamount;
        private decimal _restoreallamount;
        private string _status;
        private int _creator;
        private DateTime _createdate;
        private int _confirmor;
        private DateTime _confirmdate;
        private string _modifieduserid;
        private DateTime _modifieddate;
        private string _remark;
        private int _reimbDeptID;
        private int _userReimbID;
        private string _fromType;
        private string _canViewUser;
        private int? _projectID;
        private string _subjectsNo;
        private string _isAccount;
        private DateTime _accountDate;
        private int _accountor;
        private int _attestBillID;
        private string _custID;
        private string _contactsUnitID;
        private string _fromTBName;
        private string _contactsUnitName;
        private string _attachment;

        /// <summary>
        /// ID自动增长
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
        /// 报销单编号
        /// </summary>
        public string ReimbNo
        {
            set { _reimbno = value; }
            get { return _reimbno; }
        }
        /// <summary>
        /// 源单类型：0无来源，1费用申请
        /// </summary>
        public string FromType
        {
            set { _fromType = value; }
            get { return _fromType; }
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
        /// 报销日期
        /// </summary>
        public DateTime? ReimbDate
        {
            set { _reimbdate = value; }
            get { return _reimbdate; }
        }
        /// <summary>
        /// 申请费用总金额
        /// </summary>
        public decimal ExpAllAmount
        {
            set { _expallamount = value; }
            get { return _expallamount; }
        }
        /// <summary>
        /// 报销总金额
        /// </summary>
        public decimal? ReimbAllAmount
        {
            set { _reimballamount = value; }
            get { return _reimballamount; }
        }
        /// <summary>
        /// 归还总金额
        /// </summary>
        public decimal RestoreAllAmount
        {
            set { _restoreallamount = value; }
            get { return _restoreallamount; }
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 制单人ID
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
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人ID
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
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 最后更新人
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
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
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 报销人部门ID
        /// </summary>
        public int ReimbDeptID
        {
            set { _reimbDeptID = value; }
            get { return _reimbDeptID; }
        }
        /// <summary>
        /// 报销人ID
        /// </summary>
        public int UserReimbID
        {
            set { _userReimbID = value; }
            get { return _userReimbID; }
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
        /// 结算科目
        /// </summary>
        public string SubjectsNo
        {
            set { _subjectsNo = value; }
            get { return _subjectsNo; }
        }
        /// <summary>
        /// 是否登记凭证
        /// </summary>
        public string IsAccount
        {
            set { _isAccount = value; }
            get { return _isAccount; }
        }
        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime AccountDate
        {
            set { _accountDate = value; }
            get { return _accountDate; }
        }
        /// <summary>
        /// 登记凭证人
        /// </summary>
        public int Accountor
        {
            set { _accountor = value; }
            get { return _accountor; }
        }
        /// <summary>
        /// 生成凭证ID
        /// </summary>
        public int AttestBillID
        {
            set { _attestBillID = value; }
            get { return _attestBillID; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string CustID
        {
            set { _custID = value; }
            get { return _custID; }
        }
        /// <summary>
        /// 往来单位ID
        /// </summary>
        public string ContactsUnitID
        {
            set { _contactsUnitID = value; }
            get { return _contactsUnitID; }
        }
        /// <summary>
        /// 往来单位来源表
        /// </summary>
        public string FromTBName
        {
            set { _fromTBName = value; }
            get { return _fromTBName; }
        }
        /// <summary>
        /// 往来单位名称
        /// </summary>
        public string ContactsUnitName
        {
            set { _contactsUnitName = value; }
            get { return _contactsUnitName; }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
    }
}
