using System;
namespace XBase.Model.Personal.MessageBox
{
	/// <summary>
	/// 实体类MessageInputBox 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class MessageInputBox
	{
		public MessageInputBox()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _title;
		private string _content;
		private int _fromid;
		private int _senduserid;
		private int _receiveuserid;
		private string _status;
		private DateTime _readdate;
		private DateTime _createdate;
		private string _msgurl;
		private DateTime _modifieddate;
		private string _modifieduserid;
		/// <summary>
		/// 自动生成
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 企业代码
		/// </summary>
		public string CompanyCD
		{
			set{ _companycd=value;}
			get{return _companycd;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 来源信息ID（对应发件夹ID）
		/// </summary>
		public int FromID
		{
			set{ _fromid=value;}
			get{return _fromid;}
		}
		/// <summary>
		/// 信息发送人ID
		/// </summary>
		public int SendUserID
		{
			set{ _senduserid=value;}
			get{return _senduserid;}
		}
		/// <summary>
		/// 信息接收人ID
		/// </summary>
		public int ReceiveUserID
		{
			set{ _receiveuserid=value;}
			get{return _receiveuserid;}
		}
		/// <summary>
		/// 阅读状态（0：未读；1：已读）
		/// </summary>
		public string Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 阅读时间（精确到秒）
		/// </summary>
		public DateTime ReadDate
		{
			set{ _readdate=value;}
			get{return _readdate;}
		}
		/// <summary>
		/// 发布时间（精确到秒）
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 相关链接URL
		/// </summary>
		public string MsgURL
		{
			set{ _msgurl=value;}
			get{return _msgurl;}
		}
		/// <summary>
		/// 最后更新日期
		/// </summary>
		public DateTime ModifiedDate
		{
			set{ _modifieddate=value;}
			get{return _modifieddate;}
		}
		/// <summary>
		/// 最后更新用户ID（对应操作用户表中的UserID）
		/// </summary>
		public string ModifiedUserID
		{
			set{ _modifieduserid=value;}
			get{return _modifieduserid;}
		}
		#endregion Model

	}
}

