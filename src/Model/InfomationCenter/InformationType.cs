using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	///信息分类表 实体
	/// </summary>
	public class InformationType
	{
		private int _iD;
		/// <summary>
		///ID
		/// </summary>
		public int ID
		{
			get{ return _iD;}
			set{ _iD=value;}
		}

		private string _typeName;
		/// <summary>
		///信息分类名称
		/// </summary>
		public string TypeName
		{
			get{ return _typeName;}
			set{ _typeName=value;}
		}

		private int _supperTypeID;
		/// <summary>
		///上级分类ID
		/// </summary>
		public int SupperTypeID
		{
			get{ return _supperTypeID;}
			set{ _supperTypeID=value;}
		}

		private DateTime _modifiedDate;
		/// <summary>
		///修改时间
		/// </summary>
		public DateTime ModifiedDate
		{
			get{ return _modifiedDate;}
			set{ _modifiedDate=value;}
		}

		private string _modifiedUserID;
		/// <summary>
		///修改用户ID
		/// </summary>
		public string ModifiedUserID
		{
			get{ return _modifiedUserID;}
			set{ _modifiedUserID=value;}
		}

	}
}