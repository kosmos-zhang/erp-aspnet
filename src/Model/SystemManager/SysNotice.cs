using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.SystemManager
{
    /// <summary>
    /// 实体类SysNotice 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class SysNotice
    {
        public SysNotice()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _content;
        private DateTime _pubdate;
        private DateTime _overdate;
        private string _creatoruserid;
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PubDate
        {
            set { _pubdate = value; }
            get { return _pubdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OverDate
        {
            set { _overdate = value; }
            get { return _overdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserID
        {
            set { _creatoruserid = value; }
            get { return _creatoruserid; }
        }
        #endregion Model

    }
}
