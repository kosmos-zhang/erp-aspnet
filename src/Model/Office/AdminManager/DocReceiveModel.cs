/**********************************************
 * 类作用：   officedba. DocReceiveInfo表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/04/20
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    public class DocReceiveModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _receivedocno;
        private int _receivedoctypeid;
        private string _secretlevel;
        private string _critical;
        private DateTime? _filedate;
        private string _fileno;
        private string _filecompany;
        private string _filetitle;
        private string _filereason;
        private string _keyword;
        private string _description;
        private int _deptid;
        private string _backerno;
        private int _backer;
        private DateTime? _backdate;
        private string _backcontent;
        private string _remark;
        private int _registeruserid;
        private DateTime? _uploaddate;
        private string _documentname;
        private decimal _documentsize;
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
        /// 收文编号
        /// </summary>
        public string ReceiveDocNo
        {
            set { _receivedocno = value; }
            get { return _receivedocno; }
        }
        /// <summary>
        /// 收文类别
        /// </summary>
        public int ReceiveDocTypeID
        {
            set { _receivedoctypeid = value; }
            get { return _receivedoctypeid; }
        }
        /// <summary>
        /// 秘密等级
        /// </summary>
        public string SecretLevel
        {
            set { _secretlevel = value; }
            get { return _secretlevel; }
        }
        /// <summary>
        /// 紧急程度(1宽松,2一般,3较急,4紧急,5特急)
        /// </summary>
        public string Critical
        {
            set { _critical = value; }
            get { return _critical; }
        }
        /// <summary>
        /// 收文日期
        /// </summary>
        public DateTime? FileDate
        {
            set { _filedate = value; }
            get { return _filedate; }
        }
        /// <summary>
        /// 文号
        /// </summary>
        public string FileNo
        {
            set { _fileno = value; }
            get { return _fileno; }
        }
        /// <summary>
        /// 来文单位
        /// </summary>
        public string FileCompany
        {
            set { _filecompany = value; }
            get { return _filecompany; }
        }
        /// <summary>
        /// 来文标题
        /// </summary>
        public string FileTitle
        {
            set { _filetitle = value; }
            get { return _filetitle; }
        }
        /// <summary>
        /// 事由
        /// </summary>
        public string FileReason
        {
            set { _filereason = value; }
            get { return _filereason; }
        }
        /// <summary>
        /// 主题词
        /// </summary>
        public string KeyWord
        {
            set { _keyword = value; }
            get { return _keyword; }
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
        /// 收文单位(对应组织结构表ID)
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 回函编号
        /// </summary>
        public string BackerNo
        {
            set { _backerno = value; }
            get { return _backerno; }
        }
        /// <summary>
        /// 回函人
        /// </summary>
        public int Backer
        {
            set { _backer = value; }
            get { return _backer; }
        }
        /// <summary>
        /// 回函日期
        /// </summary>
        public DateTime? BackDate
        {
            set { _backdate = value; }
            get { return _backdate; }
        }
        /// <summary>
        /// 回函内容
        /// </summary>
        public string BackContent
        {
            set { _backcontent = value; }
            get { return _backcontent; }
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
        /// 登记人ID(对应员工表ID)
        /// </summary>
        public int RegisterUserID
        {
            set { _registeruserid = value; }
            get { return _registeruserid; }
        }
        /// <summary>
        /// 
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
        /// 大小(单位M)
        /// </summary>
        public decimal DocumentSize
        {
            set { _documentsize = value; }
            get { return _documentsize; }
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
        /// 
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
