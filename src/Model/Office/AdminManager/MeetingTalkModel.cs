/**********************************************
 * 类作用：   officedba.MeetingTalk表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/05/06
 ***********************************************/

using System;

namespace XBase.Model.Office.AdminManager
{
    public class MeetingTalkModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _recordno;
        private int _talker;
        private string _topic;
        private string _contents;
        private string _important;
        private string _remark;
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
        /// 会议记录编号（对应会议记录编号）
        /// </summary>
        public string RecordNo
        {
            set { _recordno = value; }
            get { return _recordno; }
        }
        /// <summary>
        /// 发言人
        /// </summary>
        public int Talker
        {
            set { _talker = value; }
            get { return _talker; }
        }
        /// <summary>
        /// 主旨
        /// </summary>
        public string Topic
        {
            set { _topic = value; }
            get { return _topic; }
        }
        /// <summary>
        /// 发言要点
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 重要程度(1不重要,2普通,3重要,4关键)
        /// </summary>
        public string Important
        {
            set { _important = value; }
            get { return _important; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
