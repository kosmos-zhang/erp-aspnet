using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public  class RectApplyDetailModel
    {
        	private string _id;
		private string _rectapplyno;
		private string _companycd;
		private string _jobname;
		private string _rectcount;
		private string _workplace;
		private string _worknature;
		private string _useddate;
		private string _maxage;
		private string _minage;
		private string _sex;
		private string _workage;
		private string _culturelevel;
		private string _professional;
		private string _workneeds;
		private string _otherability;
		private string _salarynote;
		private string _jobdescripe;
        private string _jobID;
        /// <summary>
        /// 岗位ID
        /// </summary>
        public string JobID
        {
            set { _jobID = value; }
            get { return _jobID; }
        }

		/// <summary>
		/// 内部id，自动生成
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 申请编号
		/// </summary>
		public string RectApplyNo
		{
			set{ _rectapplyno=value;}
			get{return _rectapplyno;}
		}
		/// <summary>
		/// 企业代码
		/// </summary>
		public string CompanyCD
		{
			set{ _companycd=value;}
			get{return _companycd;}
		}
		/// <summary>
		/// 招聘岗位
		/// </summary>
		public string JobName
		{
			set{ _jobname=value;}
			get{return _jobname;}
		}
		/// <summary>
		/// 需求人数
		/// </summary>
		public string RectCount
		{
			set{ _rectcount=value;}
			get{return _rectcount;}
		}
		/// <summary>
		/// 工作地点
		/// </summary>
		public string WorkPlace
		{
			set{ _workplace=value;}
			get{return _workplace;}
		}
		/// <summary>
		/// 工作性质
		/// </summary>
		public string WorkNature
		{
			set{ _worknature=value;}
			get{return _worknature;}
		}
		/// <summary>
		/// 最迟上岗日期
		/// </summary>
		public string UsedDate
		{
			set{ _useddate=value;}
			get{return _useddate;}
		}
		/// <summary>
		/// 截止年龄（岁）
		/// </summary>
		public string MaxAge
		{
			set{ _maxage=value;}
			get{return _maxage;}
		}
		/// <summary>
		/// 起始年龄（岁）
		/// </summary>
		public string MinAge
		{
			set{ _minage=value;}
			get{return _minage;}
		}
		/// <summary>
		/// 性别(1 男，2 女)
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 工作年限（1在读学生,2应届毕业生,3 一年以内,4一年以上,5三年以上,6五年以上,7十年以上,8二十年以上,9退休人员）
		/// </summary>
		public string WorkAge
		{
			set{ _workage=value;}
			get{return _workage;}
		}
		/// <summary>
		/// 学历ID(对应分类代码表ID)
		/// </summary>
		public string CultureLevel
		{
			set{ _culturelevel=value;}
			get{return _culturelevel;}
		}
		/// <summary>
		/// 专业ID(对应分类代码表ID)
		/// </summary>
		public string Professional
		{
			set{ _professional=value;}
			get{return _professional;}
		}
		/// <summary>
		/// 工作要求
		/// </summary>
		public string WorkNeeds
		{
			set{ _workneeds=value;}
			get{return _workneeds;}
		}
		/// <summary>
		/// 其他要求
		/// </summary>
		public string OtherAbility
		{
			set{ _otherability=value;}
			get{return _otherability;}
		}
		/// <summary>
		/// 可提供的待遇
		/// </summary>
		public string SalaryNote
		{
			set{ _salarynote=value;}
			get{return _salarynote;}
		}
		/// <summary>
		/// 职务说明
		/// </summary>
		public string JobDescripe
		{
			set{ _jobdescripe=value;}
			get{return _jobdescripe;}
		}
	 

	}
 
}
