using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Personal.Memo
{
    public class PersonalMemo
    {
        public PersonalMemo()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _memono;
        private string _title;
        private string _content;
        private string _canviewuser;
        private string _canviewusername;
        private string _attachment;
        private int _memoer;
        private DateTime _memodate;
        private int _creator;
        private DateTime _createdate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _status;

        /// <summary>
        /// 处理状态
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
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
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 备忘录编号(年月日时分秒+用户id)
        /// </summary>
        public string MemoNo
        {
            set { _memono = value; }
            get { return _memono; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string TItle
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 日记内容
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 可以查看备忘录的人员（ID，多个）
        /// </summary>
        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        /// <summary>
        /// 可以查看备忘录的人员姓名
        /// </summary>
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
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
        /// 备忘人
        /// </summary>
        public int Memoer
        {
            set { _memoer = value; }
            get { return _memoer; }
        }
        /// <summary>
        /// 备忘时间
        /// </summary>
        public DateTime MemoDate
        {
            set { _memodate = value; }
            get { return _memodate; }
        }
        /// <summary>
        /// 创建人ID(对应员工表ID)
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
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
        /// 最后更新用户ID（对应操作用户表中的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
