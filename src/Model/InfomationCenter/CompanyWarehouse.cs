using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	///企业黄页信息库 实体
	/// </summary>
	public class CompanyWarehouse
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

		private string _companyType;
		/// <summary>
		///1：制造型；2：贸易型；3：服务型；4：其他
		/// </summary>
		public string CompanyType
		{
			get{ return _companyType;}
			set{ _companyType=value;}
		}

		private string _company;
		/// <summary>
		///企业名称
		/// </summary>
		public string Company
		{
			get{ return _company;}
			set{ _company=value;}
		}

		private string _intro;
		/// <summary>
		///企业简介
		/// </summary>
		public string Intro
		{
			get{ return _intro;}
			set{ _intro=value;}
		}

		private string _country;
		/// <summary>
		///所在国家或地区
		/// </summary>
		public string Country
		{
			get{ return _country;}
			set{ _country=value;}
		}

		private string _city;
		/// <summary>
		///所在城市
		/// </summary>
		public string City
		{
			get{ return _city;}
			set{ _city=value;}
		}

		private string _contractName;
		/// <summary>
		///联系人
		/// </summary>
		public string ContractName
		{
			get{ return _contractName;}
			set{ _contractName=value;}
		}

		private string _tel;
		/// <summary>
		///联系电话
		/// </summary>
		public string Tel
		{
			get{ return _tel;}
			set{ _tel=value;}
		}

		private string _fax;
		/// <summary>
		///传真
		/// </summary>
		public string Fax
		{
			get{ return _fax;}
			set{ _fax=value;}
		}

		private string _email;
		/// <summary>
		///联系邮箱
		/// </summary>
		public string Email
		{
			get{ return _email;}
			set{ _email=value;}
		}

		private string _webSite;
		/// <summary>
		///网址
		/// </summary>
		public string WebSite
		{
			get{ return _webSite;}
			set{ _webSite=value;}
		}

		private string _address;
		/// <summary>
		///联系地址
		/// </summary>
		public string Address
		{
			get{ return _address;}
			set{ _address=value;}
		}

		private string _post;
		/// <summary>
		///邮编
		/// </summary>
		public string Post
		{
			get{ return _post;}
			set{ _post=value;}
		}

		private string _others;
		/// <summary>
		///其他联系方式
		/// </summary>
		public string Others
		{
			get{ return _others;}
			set{ _others=value;}
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