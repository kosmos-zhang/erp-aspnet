using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Personal.MessageBox
{
    /// <summary>
    /// 实体类MobileMsgMonitor 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class MobileMsgMonitor
    {
        public MobileMsgMonitor()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _msgtype;
        private int _senduserid;
        private string _sendusername;
        private int _receiveuserid;
        private string _receiveusername;
        private string _receivemobile;
        private string _content;
        private string _status;
        private DateTime _createdate;
        private DateTime _senddate;
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
        /// 短信区分标识（0：内部员工，1：领导汇报，2：客户联系人）
        /// </summary>
        public string MsgType
        {
            set { _msgtype = value; }
            get { return _msgtype; }
        }

        /// <summary>
        /// 信息发送人ID
        /// </summary>
        public int SendUserID
        {
            set { _senduserid = value; }
            get { return _senduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SendUserName
        {
            set { _sendusername = value; }
            get { return _sendusername; }
        }
        /// <summary>
        /// 信息接收人ID（多个，用逗号分隔）
        /// </summary>
        public int ReceiveUserID
        {
            set { _receiveuserid = value; }
            get { return _receiveuserid; }
        }
        /// <summary>
        /// 发信人名称（落款人）
        /// </summary>
        public string ReceiveUserName
        {
            set { _receiveusername = value; }
            get { return _receiveusername; }
        }
        /// <summary>
        /// 接收手机号码（多个，用逗号分隔）
        /// </summary>
        public string ReceiveMobile
        {
            set { _receivemobile = value; }
            get { return _receivemobile; }
        }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 发送状态（0待发，1已发）
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
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
        /// 发送时间
        /// </summary>
        public DateTime SendDate
        {
            set { _senddate = value; }
            get { return _senddate; }
        }
        #endregion Model

    }
}
