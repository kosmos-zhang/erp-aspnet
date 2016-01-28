/****************************************
 * 修改记录：
 *    1.添加字段：可查看人员CanViewUser varchar(2048) 于2010-4-8 add by hexw
 * ***************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    /// <summary>
    /// 表SellContractModel实体
    /// </summary>
    public class SellContractModel
    {
        public SellContractModel()
		{}

        #region Model
        private int _id;
        private string _companycd;
        private string _fromtype;
        private int? _frombillid;
        private int? _custid;
        private string _custtel;
        private string _title;
        private string _contractno;
        private int? _selltype;
        private string _busitype;
        private int? _currencytype;
        private decimal? _rate;
        private decimal? _totalprice;
        private decimal? _tax;
        private decimal? _totalfee;
        private decimal? _discount;
        private decimal? _discounttotal;
        private decimal? _realtotal;
        private string _isaddtax;
        private decimal? _counttotal;
        private int? _paytype;
        private int? _moneytype;
        private int? _carrytype;
        private int? _taketype;
        private int? _seller;
        private int? _selldeptid;
        private DateTime? _signdate;
        private DateTime? _startdate;
        private DateTime? _enddate;
        private string _signaddr;
        private string _theydelegate;
        private int? _ourdelegate;
        private string _state;
        private string _endnote;
        private string _talkprocess;
        private string _remark;
        private string _attachment;
        private string _billstatus;
        private int? _confirmor;
        private DateTime? _confirmdate;
        private int? _closer;
        private DateTime? _closedate;
        private int? _creator;
        private DateTime? _createdate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _canViewUser;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 源单类型（0无来源，1销售报价单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 销售报价单ID
        /// </summary>
        public int? FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 客户ID（关联客户信息表）
        /// </summary>
        public int? CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string CustTel
        {
            set { _custtel = value; }
            get { return _custtel; }
        }
        /// <summary>
        /// 合同主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo
        {
            set { _contractno = value; }
            get { return _contractno; }
        }
        /// <summary>
        /// 销售类别ID（对应分类代码表ID）
        /// </summary>
        public int? SellType
        {
            set { _selltype = value; }
            get { return _selltype; }
        }
        /// <summary>
        /// 业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨）
        /// </summary>
        public string BusiType
        {
            set { _busitype = value; }
            get { return _busitype; }
        }
        /// <summary>
        /// 币种ID(对应货币代码表ID)
        /// </summary>
        public int? CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal? Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 金额合计
        /// </summary>
        public decimal? TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 税额合计
        /// </summary>
        public decimal? Tax
        {
            set { _tax = value; }
            get { return _tax; }
        }
        /// <summary>
        /// 含税金额合计
        /// </summary>
        public decimal? TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 整单折扣（%）
        /// </summary>
        public decimal? Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal? DiscountTotal
        {
            set { _discounttotal = value; }
            get { return _discounttotal; }
        }
        /// <summary>
        /// 折否含税金额
        /// </summary>
        public decimal? RealTotal
        {
            set { _realtotal = value; }
            get { return _realtotal; }
        }
        /// <summary>
        /// 是否增值税（0否,1是
        /// </summary>
        public string isAddTax
        {
            set { _isaddtax = value; }
            get { return _isaddtax; }
        }
        /// <summary>
        /// 合同数量合计
        /// </summary>
        public decimal? CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 结算方式ID
        /// </summary>
        public int? PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 支付方式ID
        /// </summary>
        public int? MoneyType
        {
            set { _moneytype = value; }
            get { return _moneytype; }
        }
        /// <summary>
        /// 运送方式ID（对应分类代码表ID）
        /// </summary>
        public int? CarryType
        {
            set { _carrytype = value; }
            get { return _carrytype; }
        }
        /// <summary>
        /// 交货方式ID（对应分类代码表ID）
        /// </summary>
        public int? TakeType
        {
            set { _taketype = value; }
            get { return _taketype; }
        }
        /// <summary>
        /// 业务员(对应员工表ID)
        /// </summary>
        public int? Seller
        {
            set { _seller = value; }
            get { return _seller; }
        }
        /// <summary>
        /// 部门(对部门表ID)
        /// </summary>
        public int? SellDeptId
        {
            set { _selldeptid = value; }
            get { return _selldeptid; }
        }
        /// <summary>
        /// 签约时间
        /// </summary>
        public DateTime? SignDate
        {
            set { _signdate = value; }
            get { return _signdate; }
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 签约地点
        /// </summary>
        public string SignAddr
        {
            set { _signaddr = value; }
            get { return _signaddr; }
        }
        /// <summary>
        /// 对方代表
        /// </summary>
        public string TheyDelegate
        {
            set { _theydelegate = value; }
            get { return _theydelegate; }
        }
        /// <summary>
        /// 我方代表（员工表ID）
        /// </summary>
        public int? OurDelegate
        {
            set { _ourdelegate = value; }
            get { return _ourdelegate; }
        }
        /// <summary>
        /// 状态（1执行中2意外终止3已执行）
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 终止原因
        /// </summary>
        public string EndNote
        {
            set { _endnote = value; }
            get { return _endnote; }
        }
        /// <summary>
        /// 洽谈进展
        /// </summary>
        public string TalkProcess
        {
            set { _talkprocess = value; }
            get { return _talkprocess; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 确认人ID
        /// </summary>
        public int? Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime? ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 结单人ID
        /// </summary>
        public int? Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单日期
        /// </summary>
        public DateTime? CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public int? Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID(对应操作用户UserID)
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 可查看人员
        /// </summary>
        public string CanViewUser
        {
            set { _canViewUser = value; }
            get { return _canViewUser; }
        }
        #endregion Model
    }
}
