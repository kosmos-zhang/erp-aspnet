using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	///招聘需求信息库 实体
	/// </summary>
	public class RecruitmentWarehouse
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

		private string _infoFlag;
		/// <summary>
		///1供应，2求购，3代工需求，4代工服务，5项目合作，6其他服务
		/// </summary>
		public string InfoFlag
		{
			get{ return _infoFlag;}
			set{ _infoFlag=value;}
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

		private string _company;
		/// <summary>
		///招聘企业名称
		/// </summary>
		public string Company
		{
			get{ return _company;}
			set{ _company=value;}
		}

		private string _jobPosition;
		/// <summary>
		///招聘职位
		/// </summary>
		public string JobPosition
		{
			get{ return _jobPosition;}
			set{ _jobPosition=value;}
		}

		private string _field;
		/// <summary>
		///不限/管理/销售/技术/采购/财务/生产/质量/行政/其他
		/// </summary>
		public string Field
		{
			get{ return _field;}
			set{ _field=value;}
		}

		private string _wordPlace;
		/// <summary>
		///工作地点
		/// </summary>
		public string WordPlace
		{
			get{ return _wordPlace;}
			set{ _wordPlace=value;}
		}

		private string _sex;
		/// <summary>
		///不限/男/女
		/// </summary>
		public string Sex
		{
			get{ return _sex;}
			set{ _sex=value;}
		}

		private int _startAge;
		/// <summary>
		///起始年龄
		/// </summary>
		public int StartAge
		{
			get{ return _startAge;}
			set{ _startAge=value;}
		}

		private int _endtAge;
		/// <summary>
		///截止年龄
		/// </summary>
		public int EndtAge
		{
			get{ return _endtAge;}
			set{ _endtAge=value;}
		}

		private string _experience;
		/// <summary>
		///不限/在读学生/应届毕业生/一年以内/一年以上/三年以上/五年以上/十年以上/退休人员
		/// </summary>
		public string Experience
		{
			get{ return _experience;}
			set{ _experience=value;}
		}

		private string _education;
		/// <summary>
		///不限/博士/硕士/本科/大专/中专/高中或以下
		/// </summary>
		public string Education
		{
			get{ return _education;}
			set{ _education=value;}
		}

		private string _professional;
		/// <summary>
		///专业
		/// </summary>
		public string Professional
		{
			get{ return _professional;}
			set{ _professional=value;}
		}

		private string _salary;
		/// <summary>
		///面议/500以下/500-1000/1000-2000/2000-3000/3000-4000/5000-6000/7000-8000/8000-9000/10000以上
		/// </summary>
		public string Salary
		{
			get{ return _salary;}
			set{ _salary=value;}
		}

		private string _note;
		/// <summary>
		///招聘说明
		/// </summary>
		public string Note
		{
			get{ return _note;}
			set{ _note=value;}
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