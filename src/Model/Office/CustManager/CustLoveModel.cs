/**********************************************
 * 类作用：   CustLove表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/04/08
 ***********************************************/

using System;

namespace XBase.Model.Office.CustManager
{
    public class CustLoveModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _custid;
        private int _custlinkman;
        private string _loveno;
        private string _title;
        private int _lovetype;
        private string _contents;
        private int _linker;
        private DateTime? _lovedate;
        private string _feedback;
        private string _remarks;
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
        /// 客户ID
        /// </summary>
        public int CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 客户联系人
        /// </summary>
        public int CustLinkMan
        {
            set { _custlinkman = value; }
            get { return _custlinkman; }
        }
        /// <summary>
        /// 关怀编号
        /// </summary>
        public string LoveNo
        {
            set { _loveno = value; }
            get { return _loveno; }
        }
        /// <summary>
        /// 关怀主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 客户关怀分类
        /// </summary>
        public int LoveType
        {
            set { _lovetype = value; }
            get { return _lovetype; }
        }
        /// <summary>
        /// 关怀内容
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 执行人
        /// </summary>
        public int Linker
        {
            set { _linker = value; }
            get { return _linker; }
        }
        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime? LoveDate
        {
            set { _lovedate = value; }
            get { return _lovedate; }
        }
        /// <summary>
        /// 客户反馈
        /// </summary>
        public string Feedback
        {
            set { _feedback = value; }
            get { return _feedback; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
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
