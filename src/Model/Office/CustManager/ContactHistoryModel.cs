/**********************************************
 * 类作用：   CustContact表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/03/10
 ***********************************************/

using System;


namespace XBase.Model.Office.CustManager
{
    public class ContactHistoryModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _custid;
        private int _custlinkman;
        private string _contactno;
        private string _title;
        private int _linkreasonid;
        private int _linkmode;
        private DateTime? _linkdate;
        private int _linker;
        private string _contents;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _canviewuser;
        private string _canviewusername;

        /// <summary>
        /// 可查看该客户档案的人员ID
        /// </summary>
        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        /// <summary>
        /// 可查看该客户档案的人员姓名
        /// </summary>
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
        }

        /// <summary>
        /// 客户联络ID，自动生成
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
        /// 客户ID（关联客户信息表）
        /// </summary>
        public int CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 客户被联络人
        /// </summary>
        public int CustLinkMan
        {
            set { _custlinkman = value; }
            get { return _custlinkman; }
        }
        /// <summary>
        /// 客户联络单编号
        /// </summary>
        public string ContactNo
        {
            set { _contactno = value; }
            get { return _contactno; }
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
        /// 联络事由ID(系统设置)
        /// </summary>
        public int LinkReasonID
        {
            set { _linkreasonid = value; }
            get { return _linkreasonid; }
        }
        /// <summary>
        /// 联络方式ID(系统设置)
        /// </summary>
        public int LinkMode
        {
            set { _linkmode = value; }
            get { return _linkmode; }
        }
        /// <summary>
        /// 联络时间
        /// </summary>
        public DateTime? LinkDate
        {
            set { _linkdate = value; }
            get { return _linkdate; }
        }
        /// <summary>
        /// 联络人
        /// </summary>
        public int Linker
        {
            set { _linker = value; }
            get { return _linker; }
        }
        /// <summary>
        /// 联络内容
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        public DateTime? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
