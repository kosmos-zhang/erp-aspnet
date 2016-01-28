/**********************************************
 * 类作用：   AttestBillModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/04/09
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类AttestBillModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class AttestBillModel
    {
        public AttestBillModel()
        { }
        #region Model
        private int _id;
        private DateTime _voucherdate;
        private string _attestno;
        private int _attachment;
        private string _attestname;
        private int _creator;
        private DateTime _createdate;
        private int _auditor;
        private DateTime _auditordate;
        private int _status;
        private int _accountor;
        private DateTime _accountdate;
        private string _fromtbale;
        private string _fromname;
        private string _fromvalue;
        private int _accountstatus;
        private string _note;
        private string _companycd;
        /// <summary>
        /// 标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 凭证日期
        /// </summary>
        public DateTime VoucherDate
        {
            set { _voucherdate = value; }
            get { return _voucherdate; }
        }
        /// <summary>
        /// 凭证号
        /// </summary>
        public string AttestNo
        {
            set { _attestno = value; }
            get { return _attestno; }
        }
        /// <summary>
        /// 附件数
        /// </summary>
        public int Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 凭证名称
        /// </summary>
        public string AttestName
        {
            set { _attestname = value; }
            get { return _attestname; }
        }
        /// <summary>
        /// 制表人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制表日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public int Auditor
        {
            set { _auditor = value; }
            get { return _auditor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AuditorDate
        {
            set { _auditordate = value; }
            get { return _auditordate; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 登帐人
        /// </summary>
        public int Accountor
        {
            set { _accountor = value; }
            get { return _accountor; }
        }
        /// <summary>
        /// 登帐日期
        /// </summary>
        public DateTime AccountDate
        {
            set { _accountdate = value; }
            get { return _accountdate; }
        }
        /// <summary>
        /// 凭证来源表
        /// </summary>
        public string FromTbale
        {
            set { _fromtbale = value; }
            get { return _fromtbale; }
        }
        /// <summary>
        /// 标志位字段
        /// </summary>
        public string FromName
        {
            set { _fromname = value; }
            get { return _fromname; }
        }
        /// <summary>
        /// 凭证来源表ID
        /// </summary>
        public string FromValue
        {
            set { _fromvalue = value; }
            get { return _fromvalue; }
        }
        /// <summary>
        /// 登帐标记
        /// </summary>
        public int AccountStatus
        {
            set { _accountstatus = value; }
            get { return _accountstatus; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        #endregion Model

    }
}

