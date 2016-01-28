using System;
namespace XBase.Model.Personal.MessageBox
{
	/// <summary>
	/// 实体类PublicNotice 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class PublicNotice
	{
		public PublicNotice()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _newstitle;
		private string _newscontent;
		private string _isshow;
		private string _status;
		private int _comfirmor;
		private DateTime _comfirmdate;
		private int _creator;
		private DateTime _createdate;
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
		public string NewsTitle
		{
			set{ _newstitle=value;}
			get{return _newstitle;}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string NewsContent
		{
			set{ _newscontent=value;}
			get{return _newscontent;}
		}
		/// <summary>
		/// 是否显示（0：否；1：是）
		/// </summary>
		public string IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		/// <summary>
		/// 审核状态：0未审核，1已审核
		/// </summary>
		public string Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		
		/// <summary>
		/// 确认人ID
		/// </summary>
		public int Comfirmor
		{
			set{ _comfirmor=value;}
			get{return _comfirmor;}
		}
		/// <summary>
		/// 确认时间
		/// </summary>
		public DateTime ComfirmDate
		{
			set{ _comfirmdate=value;}
			get{return _comfirmdate;}
		}
		/// <summary>
		/// 发布人ID（对应员工表ID）
		/// </summary>
		public int Creator
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 发布时间（精确到秒）
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		#endregion Model

	}
}

