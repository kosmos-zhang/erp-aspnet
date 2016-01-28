using System;
namespace XBase.Model.Personal.MessageBox
{
	/// <summary>
	/// 实体类PersonalAdviceInput 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class PersonalAdviceInput
	{
		public PersonalAdviceInput()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _advicetype;
		private int _fromuserid;
		private int _douserid;
		private string _title;
		private string _content;
		private DateTime _createdate;
		private string _displayname;
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
		/// 建议类型（1建议，2意见）
		/// </summary>
		public string AdviceType
		{
			set{ _advicetype=value;}
			get{return _advicetype;}
		}
		/// <summary>
		/// 建议提交人ID
		/// </summary>
		public int FromUserID
		{
			set{ _fromuserid=value;}
			get{return _fromuserid;}
		}
		/// <summary>
		/// 建议处理人ID
		/// </summary>
		public int DoUserID
		{
			set{ _douserid=value;}
			get{return _douserid;}
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
		/// 发布时间（精确到秒）
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 匿名（0：否；1：是）
		/// </summary>
		public string DisplayName
		{
			set{ _displayname=value;}
			get{return _displayname;}
		}
		#endregion Model

	}
}

