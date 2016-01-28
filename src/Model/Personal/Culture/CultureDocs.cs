using System;
namespace XBase.Model.Personal.Culture
{
	/// <summary>
	/// 实体类CultureDocs 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class CultureDocs
	{
		public CultureDocs()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private int _culturetypeid;
		private string _title;
		private string _culturetent;
		private string _attachment;
		private int _creator;
		private DateTime _createdate;
		private DateTime _modifieddate;
		private string _modifieduserid;

        private int _CreateDeptID;

        public int CreateDeptID
        {
            get { return _CreateDeptID; }
            set { _CreateDeptID = value; }
        }

		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CompanyCD
		{
			set{ _companycd=value;}
			get{return _companycd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CultureTypeID
		{
			set{ _culturetypeid=value;}
			get{return _culturetypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Culturetent
		{
			set{ _culturetent=value;}
			get{return _culturetent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Attachment
		{
			set{ _attachment=value;}
			get{return _attachment;}
		}
		/// <summary>
		/// 
		/// </summary>
        public int Creator
		{
            set { _creator = value; }
            get { return _creator; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ModifiedDate
		{
			set{ _modifieddate=value;}
			get{return _modifieddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModifiedUserID
		{
			set{ _modifieduserid=value;}
			get{return _modifieduserid;}
		}
		#endregion Model

	}
}

