using System;
namespace XBase.Model.Personal.MessageBox
{
	/// <summary>
	/// 实体类MyContactGroup 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class MyContactGroup
	{
		public MyContactGroup()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _groupname;
		private int _creator;
		private DateTime _createdate;
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
		/// 分组ID(对应分组表)
		/// </summary>
		public string CompanyCD
		{
			set{ _companycd=value;}
			get{return _companycd;}
		}
		/// <summary>
		/// 企业代码
		/// </summary>
		public string GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 所属人ID(对应员工表ID)
		/// </summary>
		public int Creator
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 建立时间
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
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

