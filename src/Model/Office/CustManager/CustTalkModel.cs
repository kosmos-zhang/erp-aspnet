using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.CustManager
{
    public class CustTalkModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int? _custid;
        private int? _custlinkman;
        private string _talkno;
        private string _title;
        private string _priority;
        private int? _talktype;
        private string _linker;
        private DateTime? _completedate;
        private string _status;
        private string _contents;
        private string _feedback;
        private string _result;
        private string _remark;
        private int? _creator;
        private DateTime? _createddate;
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
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int? CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 客户联系人
        /// </summary>
        public int? CustLinkMan
        {
            set { _custlinkman = value; }
            get { return _custlinkman; }
        }
        /// <summary>
        /// 洽谈编号
        /// </summary>
        public string TalkNo
        {
            set { _talkno = value; }
            get { return _talkno; }
        }
        /// <summary>
        /// 洽谈主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 优先级
        /// </summary>
        public string Priority
        {
            set { _priority = value; }
            get { return _priority; }
        }
        /// <summary>
        /// 跟踪方式
        /// </summary>
        public int? TalkType
        {
            set { _talktype = value; }
            get { return _talktype; }
        }
        /// <summary>
        /// 执行人
        /// </summary>
        public string Linker
        {
            set { _linker = value; }
            get { return _linker; }
        }
        /// <summary>
        /// 完成期限
        /// </summary>
        public DateTime? CompleteDate
        {
            set { _completedate = value; }
            get { return _completedate; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 行动描述
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
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
        /// 效果评估
        /// </summary>
        public string Result
        {
            set { _result = value; }
            get { return _result; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int? Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
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
