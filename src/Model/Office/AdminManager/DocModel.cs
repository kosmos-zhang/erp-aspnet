/**********************************************
 * 类作用：   officedba. DocInfo表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/04/27
 ***********************************************/

using System;


namespace XBase.Model.Office.AdminManager
{
    public class DocModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _documentno;
        private int _doctype;
        private string _doctitle;
        private int _uploaduserid;
        private int _deptid;
        private DateTime? _uploaddate;
        private string _documentname;
        private string _documenttype;
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
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 文档编号
        /// </summary>
        public string DocumentNo
        {
            set { _documentno = value; }
            get { return _documentno; }
        }
        /// <summary>
        /// 文档分类ID
        /// </summary>
        public int DocType
        {
            set { _doctype = value; }
            get { return _doctype; }
        }
        /// <summary>
        /// 文档说明
        /// </summary>
        public string DocTitle
        {
            set { _doctitle = value; }
            get { return _doctitle; }
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
        /// 所属部门ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
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
        /// 类型
        /// </summary>
        public string DocumentType
        {
            set { _documenttype = value; }
            get { return _documenttype; }
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
