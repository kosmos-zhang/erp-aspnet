/**********************************************
 * 类作用：   officedba. DocRequstInfo表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/04/23
 ***********************************************/

using System;

namespace XBase.Model.Office.AdminManager
{
    public class DocRequstModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _documentno;
        private int _requstdoctype;
        private string _secretlevel;
        private string _emerlevel;
        private string _main;
        private decimal _requestmoney;
        private string _requesttitle;
        private string _description;
        private string _requstno;
        private DateTime? _requestdate;
        private int _requestdept;
        private int _employeeid;
        private int _uploaduserid;
        private DateTime? _uploaddate;
        private string _documentname;
        private string _documenturl;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 请示编号
        /// </summary>
        public string DocumentNo
        {
            set { _documentno = value; }
            get { return _documentno; }
        }
        /// <summary>
        /// 请示类型
        /// </summary>
        public int RequstDocType
        {
            set { _requstdoctype = value; }
            get { return _requstdoctype; }
        }
        /// <summary>
        /// 秘密等级(1秘密，2机密，3绝密，4不公开，5一般)
        /// </summary>
        public string SecretLevel
        {
            set { _secretlevel = value; }
            get { return _secretlevel; }
        }
        /// <summary>
        /// 紧急程度(1特提、2特急、3加急、4平急)
        /// </summary>
        public string EmerLevel
        {
            set { _emerlevel = value; }
            get { return _emerlevel; }
        }
        /// <summary>
        /// 主送领导
        /// </summary>
        public string Main
        {
            set { _main = value; }
            get { return _main; }
        }
        /// <summary>
        /// 请示金额
        /// </summary>
        public decimal RequestMoney
        {
            set { _requestmoney = value; }
            get { return _requestmoney; }
        }
        /// <summary>
        /// 请示标题
        /// </summary>
        public string RequestTitle
        {
            set { _requesttitle = value; }
            get { return _requesttitle; }
        }
        /// <summary>
        /// 内容摘要
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 请示文号
        /// </summary>
        public string RequstNo
        {
            set { _requstno = value; }
            get { return _requstno; }
        }
        /// <summary>
        /// 请示日期
        /// </summary>
        public DateTime? RequestDate
        {
            set { _requestdate = value; }
            get { return _requestdate; }
        }
        /// <summary>
        /// 请示部门
        /// </summary>
        public int RequestDept
        {
            set { _requestdept = value; }
            get { return _requestdept; }
        }
        /// <summary>
        /// 拟稿人
        /// </summary>
        public int EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 上传人ID
        /// </summary>
        public int UploadUserID
        {
            set { _uploaduserid = value; }
            get { return _uploaduserid; }
        }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime? UploadDate
        {
            set { _uploaddate = value; }
            get { return _uploaddate; }
        }
        /// <summary>
        /// 文档名称
        /// </summary>
        public string DocumentName
        {
            set { _documentname = value; }
            get { return _documentname; }
        }
        /// <summary>
        /// 文档URL
        /// </summary>
        public string DocumentURL
        {
            set { _documenturl = value; }
            get { return _documenturl; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime? ModifiedDate
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
