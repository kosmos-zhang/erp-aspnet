using System;
namespace XBase.Model.Decision
{
	/// <summary>
	/// 实体类DataCustLevel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class DataCustLevel
	{
		public DataCustLevel()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _gname;
		private int _gup;
		private int _gdown;
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
		public string GName
		{
			set{ _gname=value;}
			get{return _gname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int GUp
		{
			set{ _gup=value;}
			get{return _gup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int GDown
		{
			set{ _gdown=value;}
			get{return _gdown;}
		}
		#endregion Model

	}
}

