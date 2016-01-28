/**********************************************
 * 类作用：   InSideChangeAccoModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/04/28
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
	/// <summary>
	/// 实体类InSideChangeAccoModel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public class InSideChangeAccoModel
	{
		public InSideChangeAccoModel()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _changeno;
		private string _changebillnum;
		private DateTime _changedate;
		private string _inaccountno;
		private string _inbankname;
		private string _outaccountno;
		private string _outbankname;
		private decimal _totalprice;
		private int _executor;
		private string _summary;
		private string _confirmstatus;
		private int _confirmor;
		private DateTime _confirmdate;
		private string _isaccount;
		private int _accountor;
		private DateTime _accountdate;
		private DateTime _modifieddate;
		private int _modifieduserid;
        private int _currencytype;
        private decimal _currencyrate;
		/// <summary>
		/// 标识
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
		/// 转账单编码
		/// </summary>
		public string ChangeNo
		{
		set{ _changeno=value;}
		get{return _changeno;}
		}
		/// <summary>
		/// 转账票号
		/// </summary>
		public string ChangeBillNum
		{
		set{ _changebillnum=value;}
		get{return _changebillnum;}
		}
		/// <summary>
		/// 转账日期
		/// </summary>
		public DateTime ChangeDate
		{
		set{ _changedate=value;}
		get{return _changedate;}
		}
		/// <summary>
		/// 转入帐户
		/// </summary>
		public string InAccountNo
		{
		set{ _inaccountno=value;}
		get{return _inaccountno;}
		}
		/// <summary>
		/// 转入银行名称
		/// </summary>
		public string InBankName
		{
		set{ _inbankname=value;}
		get{return _inbankname;}
		}
		/// <summary>
		/// 转出帐户
		/// </summary>
		public string OutAccountNo
		{
		set{ _outaccountno=value;}
		get{return _outaccountno;}
		}
		/// <summary>
		/// 转出银行名称
		/// </summary>
		public string OutBankName
		{
		set{ _outbankname=value;}
		get{return _outbankname;}
		}
		/// <summary>
		/// 转账金额
		/// </summary>
		public decimal TotalPrice
		{
		set{ _totalprice=value;}
		get{return _totalprice;}
		}
		/// <summary>
		/// 经办人
		/// </summary>
		public int Executor
		{
		set{ _executor=value;}
		get{return _executor;}
		}
		/// <summary>
		/// 摘要
		/// </summary>
		public string Summary
		{
		set{ _summary=value;}
		get{return _summary;}
		}
		/// <summary>
		/// 是否确认
		/// </summary>
		public string ConfirmStatus
		{
		set{ _confirmstatus=value;}
		get{return _confirmstatus;}
		}
		/// <summary>
		/// 确认人
		/// </summary>
		public int Confirmor
		{
		set{ _confirmor=value;}
		get{return _confirmor;}
		}
		/// <summary>
		/// 确认时间
		/// </summary>
		public DateTime ConfirmDate
		{
		set{ _confirmdate=value;}
		get{return _confirmdate;}
		}
		/// <summary>
		/// 是否登记
		/// </summary>
		public string IsAccount
		{
		set{ _isaccount=value;}
		get{return _isaccount;}
		}
		/// <summary>
		/// 登记人
		/// </summary>
		public int Accountor
		{
		set{ _accountor=value;}
		get{return _accountor;}
		}
		/// <summary>
		/// 登记时间
		/// </summary>
		public DateTime AccountDate
		{
		set{ _accountdate=value;}
		get{return _accountdate;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime ModifiedDate
		{
		set{ _modifieddate=value;}
		get{return _modifieddate;}
		}
		/// <summary>
		/// 最后修改人
		/// </summary>
		public int ModifiedUserID
		{
		set{ _modifieduserid=value;}
		get{return _modifieduserid;}
		}

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal CurrencyRate
        {
            set { _currencyrate = value; }
            get { return _currencyrate; }
        }
		#endregion Model

	}
}