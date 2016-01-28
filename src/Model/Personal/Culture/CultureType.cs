using System;
namespace XBase.Model.Personal.Culture
{
	/// <summary>
	/// 实体类CultureType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class CultureType
	{
		public CultureType()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _typename;
		private int _suppertypeid;
		private string _path;
		/// <summary>
		/// 自动生成
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
		/// 信息分类名称
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 上级分类ID
		/// </summary>
		public int SupperTypeID
		{
			set{ _suppertypeid=value;}
			get{return _suppertypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		#endregion Model

	}
}

