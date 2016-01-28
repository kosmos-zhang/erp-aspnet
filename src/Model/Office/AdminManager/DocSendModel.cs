/**********************************************
 * 类作用：   officedba. DocSendInfo表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/04/22
 ***********************************************/

using System;

namespace XBase.Model.Office.AdminManager
{
    public class DocSendModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _documentno;
        private int? _senddoctypeid;
        private string _secretlevel;
        private string _emerlevel;
        private int? _senddeptid;
        private string _fileno;
        private string _filetitle;
        private DateTime? _filedate;
        private string _mainsend;
        private string _ccsend;
        private string _outcompany;
        private string _keyword;
        private string _filereason;
        private string _description;
        private int? _registeruserid;
        private string _backer;
        private string _backerno;
        private DateTime? _backdate;
        private string _backcontent;
        private DateTime? _uploaddate;
        private string _documentname;
        private decimal? _documentsize;
        private string _documenturl;
        private string _remark;
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
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 发文编号
        /// </summary>
        public string DocumentNo
        {
            set { _documentno = value; }
            get { return _documentno; }
        }
        /// <summary>
        /// 发文类型ID(对应分类代码表ID)
        /// </summary>
        public int? SendDocTypeID
        {
            set { _senddoctypeid = value; }
            get { return _senddoctypeid; }
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
        /// 发文单位
        /// </summary>
        public int? SendDeptID
        {
            set { _senddeptid = value; }
            get { return _senddeptid; }
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
        /// 发文标题
        /// </summary>
        public string FileTitle
        {
            set { _filetitle = value; }
            get { return _filetitle; }
        }
        /// <summary>
        /// 发文日期
        /// </summary>
        public DateTime? FileDate
        {
            set { _filedate = value; }
            get { return _filedate; }
        }
        /// <summary>
        /// 主送部门（部门列表，用逗号隔开）
        /// </summary>
        public string MainSend
        {
            set { _mainsend = value; }
            get { return _mainsend; }
        }
        /// <summary>
        /// 抄送部门（部门列表，用逗号隔开）
        /// </summary>
        public string CCSend
        {
            set { _ccsend = value; }
            get { return _ccsend; }
        }
        /// <summary>
        /// 寄发单位（外单位）
        /// </summary>
        public string OutCompany
        {
            set { _outcompany = value; }
            get { return _outcompany; }
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
        /// 发文事由
        /// </summary>
        public string FileReason
        {
            set { _filereason = value; }
            get { return _filereason; }
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
        /// 发文人ID(对应员工表ID)
        /// </summary>
        public int? RegisterUserID
        {
            set { _registeruserid = value; }
            get { return _registeruserid; }
        }
        /// <summary>
        /// 回函人
        /// </summary>
        public string Backer
        {
            set { _backer = value; }
            get { return _backer; }
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
        /// 大小(单位M)
        /// </summary>
        public decimal? DocumentSize
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
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
