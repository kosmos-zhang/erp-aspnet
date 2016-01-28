/*************************************
 * 描述：批次规则设置实体类
 * 创建人：何小武
 * 创建时间“2010-3-24
 * ***********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SystemManager
{
    /// <summary>
    /// 实体类BatchNoRuleSet 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class BatchNoRuleSet
	{
        public BatchNoRuleSet()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _rulename;
		private string _ruleprefix;
		private string _ruledatetype;
		private int? _rulenolen;
		private int _lastno;
		private string _ruleexample;
		private string _remark;
		private string _isdefault;
		private string _usedstatus;
		private DateTime? _modifieddate;
		private string _modifieduserid;
		/// <summary>
		/// ID,自动生成
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 公司编码
		/// </summary>
		public string CompanyCD
		{
			set{ _companycd=value;}
			get{return _companycd;}
		}
		/// <summary>
		/// 规则名称
		/// </summary>
		public string RuleName
		{
			set{ _rulename=value;}
			get{return _rulename;}
		}
		/// <summary>
		/// 前缀
		/// </summary>
		public string RulePrefix
		{
			set{ _ruleprefix=value;}
			get{return _ruleprefix;}
		}
		/// <summary>
		/// 编号中的日期类型（仅对单据编码有效）：年编号：yyyy,年月编号：yyyymm,年月日编号：yyyymmdd
		/// </summary>
		public string RuleDateType
		{
			set{ _ruledatetype=value;}
			get{return _ruledatetype;}
		}
		/// <summary>
		/// 编号流水号长度（1-8位，最大支持8位）
		/// </summary>
		public int? RuleNoLen
		{
			set{ _rulenolen=value;}
			get{return _rulenolen;}
		}
		/// <summary>
		/// 当前流水号最大值（每次更新加1
		/// </summary>
		public int LastNo
		{
			set{ _lastno=value;}
			get{return _lastno;}
		}
		/// <summary>
		/// 编号示例，根据编号前缀、编码日期类型、流水号长度组合生成示例，例如：DJ{yyyymmdd}{NNNN}
		/// </summary>
		public string RuleExample
		{
			set{ _ruleexample=value;}
			get{return _ruleexample;}
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
		/// 是否为缺省编号规则:0-否,1-是
		/// </summary>
		public string IsDefault
		{
			set{ _isdefault=value;}
			get{return _isdefault;}
		}
		/// <summary>
		/// 启用状态(1：启用 0：停用)
		/// </summary>
		public string UsedStatus
		{
			set{ _usedstatus=value;}
			get{return _usedstatus;}
		}
		/// <summary>
		/// 最后更新日期
		/// </summary>
		public DateTime? ModifiedDate
		{
			set{ _modifieddate=value;}
			get{return _modifieddate;}
		}
		/// <summary>
		/// 最后更新用户ID
		/// </summary>
		public string ModifiedUserID
		{
			set{ _modifieduserid=value;}
			get{return _modifieduserid;}
		}
		#endregion Model

	}
}
