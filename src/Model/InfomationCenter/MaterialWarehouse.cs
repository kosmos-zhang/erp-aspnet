using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	///设备材料信息库 实体
	/// </summary>
	public class MaterialWarehouse
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

		private string _productName;
		/// <summary>
		///产品名称
		/// </summary>
		public string ProductName
		{
			get{ return _productName;}
			set{ _productName=value;}
		}

		private string _company;
		/// <summary>
		///制造商
		/// </summary>
		public string Company
		{
			get{ return _company;}
			set{ _company=value;}
		}

		private string _modeType;
		/// <summary>
		///型号
		/// </summary>
		public string ModeType
		{
			get{ return _modeType;}
			set{ _modeType=value;}
		}

		private string _specification;
		/// <summary>
		///规格
		/// </summary>
		public string Specification
		{
			get{ return _specification;}
			set{ _specification=value;}
		}

		private string _photoURL;
		/// <summary>
		///产品图片（附件）
		/// </summary>
		public string PhotoURL
		{
			get{ return _photoURL;}
			set{ _photoURL=value;}
		}

		private string _manaulURL;
		/// <summary>
		///产品规格（附件）
		/// </summary>
		public string ManaulURL
		{
			get{ return _manaulURL;}
			set{ _manaulURL=value;}
		}

		private string _remark;
		/// <summary>
		///详细信息
		/// </summary>
		public string Remark
		{
			get{ return _remark;}
			set{ _remark=value;}
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