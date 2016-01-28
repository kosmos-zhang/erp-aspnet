using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	///政策法规信息库 实体
	/// </summary>
	public class PolicyWarehouse
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

		private string _safeLevel;
		/// <summary>
		///
		/// </summary>
		public string SafeLevel
		{
			get{ return _safeLevel;}
			set{ _safeLevel=value;}
		}

		private string _companyCD;
		/// <summary>
		///企业发布信息自动记录
		/// </summary>
		public string CompanyCD
		{
			get{ return _companyCD;}
			set{ _companyCD=value;}
		}

		private string _infoTitle;
		/// <summary>
		///信息主题
		/// </summary>
		public string InfoTitle
		{
			get{ return _infoTitle;}
			set{ _infoTitle=value;}
		}

		private int _typeID;
		/// <summary>
		///所属信息分类ID
		/// </summary>
		public int TypeID
		{
			get{ return _typeID;}
			set{ _typeID=value;}
		}

		private string _sourceFrom;
		/// <summary>
		///
		/// </summary>
		public string SourceFrom
		{
			get{ return _sourceFrom;}
			set{ _sourceFrom=value;}
		}

		private string _releaseFlag;
		/// <summary>
		///
		/// </summary>
		public string ReleaseFlag
		{
			get{ return _releaseFlag;}
			set{ _releaseFlag=value;}
		}

		private string _remark;
		/// <summary>
		///信息内容
		/// </summary>
		public string Remark
		{
			get{ return _remark;}
			set{ _remark=value;}
		}

		private string _releaseCompany;
		/// <summary>
		///政策发布的政府机关单位
		/// </summary>
		public string ReleaseCompany
		{
			get{ return _releaseCompany;}
			set{ _releaseCompany=value;}
		}

		private DateTime _releaseDate;
		/// <summary>
		///发布时间
		/// </summary>
		public DateTime ReleaseDate
		{
			get{ return _releaseDate;}
			set{ _releaseDate=value;}
		}

		private string _attachment;
		/// <summary>
		///附件
		/// </summary>
		public string Attachment
		{
			get{ return _attachment;}
			set{ _attachment=value;}
		}

		private string _isShow;
		/// <summary>
		///1：是；0：否。默认为1
		/// </summary>
		public string IsShow
		{
			get{ return _isShow;}
			set{ _isShow=value;}
		}

		private string _isAudite;
		/// <summary>
		///0：未审核；1：已审核；2：审核未通过
		/// </summary>
		public string IsAudite
		{
			get{ return _isAudite;}
			set{ _isAudite=value;}
		}

		private string _createUserID;
		/// <summary>
		///创建人ID
		/// </summary>
		public string CreateUserID
		{
			get{ return _createUserID;}
			set{ _createUserID=value;}
		}

		private DateTime _createDate;
		/// <summary>
		///创建时间
		/// </summary>
		public DateTime CreateDate
		{
			get{ return _createDate;}
			set{ _createDate=value;}
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