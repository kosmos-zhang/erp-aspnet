/***********************************
 * 修改记录：
 *  1.添加"所属项目ID"字段：ProjectID    于2010-3-31 by hexw
 *  2.添加"可查看人员"字段：CanViewUser varchar(2048) 于2010-4-7 add by hexw
 *  
 * ********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public  class SellSendModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int? _custid;
        private string _sendno;
        private string _title;
        private string _fromtype;
        private int? _frombillid;
        private int? _selltype;
        private string _busitype;
        private int? _paytype;
        private int? _moneytype;
        private int? _taketype;
        private int? _carrytype;
        private string _sendaddr;
        private int? _sender;
        private string _receiveaddr;
        private string _receiver;
        private string _tel;
        private string _modile;
        private string _post;
        private DateTime? _intendsenddate;
        private int? _transporter;
        private decimal? _transportfee;
        private int? _transpaytype;
        private int? _currencytype;
        private decimal? _rate;
        private decimal? _totalprice;
        private decimal? _tax;
        private decimal? _totalfee;
        private decimal? _discount;
        private string _payremark;
        private string _deliverremark;
        private string _packtransit;
        private string _remark;
        private string _billstatus;
        private int? _creator;
        private DateTime? _createdate;
        private int? _confirmor;
        private DateTime? _confirmdate;
        private int? _closer;
        private DateTime? _closedate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private int? _selldeptid;
        private decimal? _discounttotal;
        private decimal? _realtotal;
        private string _isaddtax;
        private decimal? _counttotal;
        private int? _seller;
        private int? _projectID;
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
        /// 客户ID（关联客户信息表）
        /// </summary>
        public int? CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 发货单编号
        /// </summary>
        public string SendNo
        {
            set { _sendno = value; }
            get { return _sendno; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 源单类型（0无源单，1销售订单，2销售合同）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 来源单据ID（销售订单）
        /// </summary>
        public int? FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
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
        /// 业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨，6分期付款）
        /// </summary>
        public string BusiType
        {
            set { _busitype = value; }
            get { return _busitype; }
        }
        /// <summary>
        /// 结算方式ID（对应分类代码表ID）
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
        /// 交货方式ID
        /// </summary>
        public int? TakeType
        {
            set { _taketype = value; }
            get { return _taketype; }
        }
        /// <summary>
        /// 运送方式ID
        /// </summary>
        public int? CarryType
        {
            set { _carrytype = value; }
            get { return _carrytype; }
        }
        /// <summary>
        /// 发货地址
        /// </summary>
        public string SendAddr
        {
            set { _sendaddr = value; }
            get { return _sendaddr; }
        }
        /// <summary>
        /// 发货人ID（对应员工表ID）
        /// </summary>
        public int? Sender
        {
            set { _sender = value; }
            get { return _sender; }
        }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ReceiveAddr
        {
            set { _receiveaddr = value; }
            get { return _receiveaddr; }
        }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 收货人移动电话
        /// </summary>
        public string Modile
        {
            set { _modile = value; }
            get { return _modile; }
        }
        /// <summary>
        /// 收货人邮编
        /// </summary>
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }
        /// <summary>
        /// 预计发货时间
        /// </summary>
        public DateTime? IntendSendDate
        {
            set { _intendsenddate = value; }
            get { return _intendsenddate; }
        }
        /// <summary>
        /// 运输商
        /// </summary>
        public int? Transporter
        {
            set { _transporter = value; }
            get { return _transporter; }
        }
        /// <summary>
        /// 运费合计
        /// </summary>
        public decimal? TransportFee
        {
            set { _transportfee = value; }
            get { return _transportfee; }
        }
        /// <summary>
        /// 运费结算方式ID
        /// </summary>
        public int? TransPayType
        {
            set { _transpaytype = value; }
            get { return _transpaytype; }
        }
        /// <summary>
        /// 币种ID
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
        /// 价格合计
        /// </summary>
        public decimal? TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 税额
        /// </summary>
        public decimal? Tax
        {
            set { _tax = value; }
            get { return _tax; }
        }
        /// <summary>
        /// 含税价格
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
        /// 付款说明
        /// </summary>
        public string PayRemark
        {
            set { _payremark = value; }
            get { return _payremark; }
        }
        /// <summary>
        /// 交付说明
        /// </summary>
        public string DeliverRemark
        {
            set { _deliverremark = value; }
            get { return _deliverremark; }
        }
        /// <summary>
        /// 包装运输说明
        /// </summary>
        public string PackTransit
        {
            set { _packtransit = value; }
            get { return _packtransit; }
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
        /// 单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
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
        /// 部门(对部门表ID)
        /// </summary>
        public int? SellDeptId
        {
            set { _selldeptid = value; }
            get { return _selldeptid; }
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
        /// 折后含税金额
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
        /// 发货数量合计
        /// </summary>
        public decimal? CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
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
        /// 所属项目ID
        /// </summary>
        public int? ProjectID
        {
            set { _projectID = value; }
            get { return _projectID; }
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
