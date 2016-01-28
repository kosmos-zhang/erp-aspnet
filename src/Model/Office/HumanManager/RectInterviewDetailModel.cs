/**********************************************
 * 类作用：   RectInterviewDetail表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/18
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectInterviewDetailModel
    /// 描述：RectInterviewDetail表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/18
    /// 最后修改时间：2009/04/18
    /// </summary>
    ///
    public class RectInterviewDetailModel
    {
        #region Model
        private string _id;
		private string _companycd;
		private string _interviewno;
        private string _checkelemid;
        private string _realscore;
		private string _remark;
		/// <summary>
		/// 内部id，自动生成
		/// </summary>
        public string ID
		{
		set{ _id=value;}
		get{return _id;}
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
		/// 面试记录编号（对应面试记录表中的编号）
		/// </summary>
		public string InterviewNo
		{
		set{ _interviewno=value;}
		get{return _interviewno;}
		}
		/// <summary>
		/// 评测要素ID（对应面试评测要素表ID）
		/// </summary>
        public string CheckElemID
		{
		set{ _checkelemid=value;}
		get{return _checkelemid;}
		}
		/// <summary>
		/// 面试得分
		/// </summary>
        public string RealScore
		{
		set{ _realscore=value;}
		get{return _realscore;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
		set{ _remark=value;}
		get{return _remark;}
		}
		#endregion Model
    }
}
