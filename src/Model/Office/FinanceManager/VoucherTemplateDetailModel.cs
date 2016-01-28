/**********************************************
 * 类作用：   VoucherTemplateDetailModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2010/03/29
 ***********************************************/

using System;
namespace XBase.Model.Office.FinanceManager
{
	/// <summary>
	/// 实体类VoucherTemplateDetailModel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class VoucherTemplateDetailModel
	{
		public VoucherTemplateDetailModel()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _temno;
		private string _subjectsno;
		private string _direction;
		private decimal _scale;
		private string _remark;
        private int _sortno;
		/// <summary>
		/// 标识
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 公司编号
		/// </summary>
		public string CompanyCD
		{
			set{ _companycd=value;}
			get{return _companycd;}
		}
		/// <summary>
		/// 模板编号
		/// </summary>
		public string TemNo
		{
			set{ _temno=value;}
			get{return _temno;}
		}
		/// <summary>
		/// 科目编码
		/// </summary>
		public string SubjectsNo
		{
			set{ _subjectsno=value;}
			get{return _subjectsno;}
		}
		/// <summary>
		/// 金额所在方向（0借，1贷）
		/// </summary>
		public string Direction
		{
			set{ _direction=value;}
			get{return _direction;}
		}
		/// <summary>
		/// 金额所占比例
		/// </summary>
		public decimal Scale
		{
			set{ _scale=value;}
			get{return _scale;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}

        /// <summary>
        /// 序号
        /// </summary>
        public int SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
		#endregion Model

	}
}

