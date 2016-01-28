/**********************************************
 * 类作用：   RectCheckTemplateElem表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/16
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectCheckTemplateElemModel
    /// 描述：RectCheckTemplateElem表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/16
    /// 最后修改时间：2009/04/16
    /// </summary>
    ///
    public class RectCheckTemplateElemModel
    {
        #region Model
        private string _id;
		private string _companycd;
		private string _templateno;
        private string _checkelemid;
        private string _maxscore;
        private string _rate;
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
		/// 评测模板编号
		/// </summary>
		public string TemplateNo
		{
		set{ _templateno=value;}
		get{return _templateno;}
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
		/// 满分
		/// </summary>
		public string MaxScore
		{
		set{ _maxscore=value;}
		get{return _maxscore;}
		}
		/// <summary>
		/// 权重
		/// </summary>
		public string Rate
		{
		set{ _rate=value;}
		get{return _rate;}
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
		#endregion Model
    }
}
