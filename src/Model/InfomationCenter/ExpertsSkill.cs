using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	///人才技能信息表 实体
	/// </summary>
	public class ExpertsSkill
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

		private int _expertsID;
		/// <summary>
		///人才ID
		/// </summary>
		public int ExpertsID
		{
			get{ return _expertsID;}
			set{ _expertsID=value;}
		}

		private string _skillName;
		/// <summary>
		///技能名称
		/// </summary>
		public string SkillName
		{
			get{ return _skillName;}
			set{ _skillName=value;}
		}

		private string _certificateName;
		/// <summary>
		///证件名称
		/// </summary>
		public string CertificateName
		{
			get{ return _certificateName;}
			set{ _certificateName=value;}
		}

		private string _certificateNo;
		/// <summary>
		///证件编号
		/// </summary>
		public string CertificateNo
		{
			get{ return _certificateNo;}
			set{ _certificateNo=value;}
		}

		private string _certificateLevel;
		/// <summary>
		///证件等级
		/// </summary>
		public string CertificateLevel
		{
			get{ return _certificateLevel;}
			set{ _certificateLevel=value;}
		}

		private string _issueCompany;
		/// <summary>
		///发证单位
		/// </summary>
		public string IssueCompany
		{
			get{ return _issueCompany;}
			set{ _issueCompany=value;}
		}

		private string _issueDate;
		/// <summary>
		///发证时间
		/// </summary>
		public string IssueDate
		{
			get{ return _issueDate;}
			set{ _issueDate=value;}
		}

		private string _validity;
		/// <summary>
		///有效期
		/// </summary>
		public string Validity
		{
			get{ return _validity;}
			set{ _validity=value;}
		}

		private string _deadDate;
		/// <summary>
		///失效时间
		/// </summary>
		public string DeadDate
		{
			get{ return _deadDate;}
			set{ _deadDate=value;}
		}

		private string _remark;
		/// <summary>
		///备注
		/// </summary>
		public string Remark
		{
			get{ return _remark;}
			set{ _remark=value;}
		}

	}
}