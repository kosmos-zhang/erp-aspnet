using System;

namespace XBase.Model.InfomationCenter
{
	/// <summary>
	///人才履历表 实体
	/// </summary>
	public class ExpertsWorkHistory
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

		private string _startDate;
		/// <summary>
		///开始时间
		/// </summary>
		public string StartDate
		{
			get{ return _startDate;}
			set{ _startDate=value;}
		}

		private string _endDate;
		/// <summary>
		///结束时间
		/// </summary>
		public string EndDate
		{
			get{ return _endDate;}
			set{ _endDate=value;}
		}

		private string _flag;
		/// <summary>
		///1工作，2 学习
		/// </summary>
		public string Flag
		{
			get{ return _flag;}
			set{ _flag=value;}
		}

		private string _company;
		/// <summary>
		///工作单位
		/// </summary>
		public string Company
		{
			get{ return _company;}
			set{ _company=value;}
		}

		private string _department;
		/// <summary>
		///所在部门
		/// </summary>
		public string Department
		{
			get{ return _department;}
			set{ _department=value;}
		}

		private string _workContent;
		/// <summary>
		///工作内容
		/// </summary>
		public string WorkContent
		{
			get{ return _workContent;}
			set{ _workContent=value;}
		}

		private string _leaveReason;
		/// <summary>
		///离职原因
		/// </summary>
		public string LeaveReason
		{
			get{ return _leaveReason;}
			set{ _leaveReason=value;}
		}

		private string _schoolName;
		/// <summary>
		///学校名称
		/// </summary>
		public string SchoolName
		{
			get{ return _schoolName;}
			set{ _schoolName=value;}
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

		private string _cultureLevel;
		/// <summary>
		///学历
		/// </summary>
		public string CultureLevel
		{
			get{ return _cultureLevel;}
			set{ _cultureLevel=value;}
		}

	}
}