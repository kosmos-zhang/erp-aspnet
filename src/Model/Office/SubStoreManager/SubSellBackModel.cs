/***********************************************
 * 类作用：   SubStorageManager表数据模板      *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/05/18                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SubStoreManager
{
    /// <summary>
    /// 类名：SubSellBackModel
    /// 描述：SubSellBack表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/18
    /// 最后修改时间：2009/06/04
    /// </summary>
    ///
    public class SubSellBackModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _deptid;
        private string _backno;
        private string _title;
        private int _orderid;
        private int _seller;
        private DateTime ? _backdate;
        private string _backreason;
        private string _busistatus;
        private int _currencytype;
        private decimal _rate;
        private decimal _totalprice;
        private decimal _tax;
        private decimal _totalfee;
        private decimal _discount;
        private decimal _discounttotal;
        private decimal _realtotal;
        private decimal _payedtotal;
        private decimal _wairpaytotal;
        private string _isaddtax;
        private decimal _counttotal;
        private string _remark;
        private string _attachment;
        private string _billstatus;
        private int _creator;
        private DateTime ? _createdate;
        private int _confirmor;
        private DateTime ? _confirmdate;
        private int _outuserid;
        private DateTime ? _outdate;
        private int _sttluserid;
        private DateTime ? _sttldate;
        private int _closer;
        private DateTime ? _closedate;
        private DateTime ? _modifieddate;
        private string _modifieduserid;
        private string _fromtype;
        private string _sendmode;
        private string _custname;
        private string _custtel;
        private string _custmobile;
        private string _custaddr;
        private int _inuserid;
        private DateTime ? _indate;
        /// <summary>
        /// 自动生成
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
        /// 分店ID（对应组织机构表ID）
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 销售退货单编号
        /// </summary>
        public string BackNo
        {
            set { _backno = value; }
            get { return _backno; }
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
        /// 对应销售订单ID
        /// </summary>
        public int OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 退货处理人(对应员工表ID)
        /// </summary>
        public int Seller
        {
            set { _seller = value; }
            get { return _seller; }
        }
        /// <summary>
        /// 退货日期
        /// </summary>
        public DateTime ? BackDate
        {
            set { _backdate = value; }
            get { return _backdate; }
        }
        /// <summary>
        /// 退货理由描述
        /// </summary>
        public string BackReason
        {
            set { _backreason = value; }
            get { return _backreason; }
        }
        /// <summary>
        /// 业务状态（1申请，2入库,3结算，4完成）
        /// </summary>
        public string BusiStatus
        {
            set { _busistatus = value; }
            get { return _busistatus; }
        }
        /// <summary>
        /// 币种ID
        /// </summary>
        public int CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 金额合计
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 税额合计
        /// </summary>
        public decimal Tax
        {
            set { _tax = value; }
            get { return _tax; }
        }
        /// <summary>
        /// 含税金额合计
        /// </summary>
        public decimal TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 整单折扣（%）
        /// </summary>
        public decimal Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal DiscountTotal
        {
            set { _discounttotal = value; }
            get { return _discounttotal; }
        }
        /// <summary>
        /// 折后含税金额
        /// </summary>
        public decimal RealTotal
        {
            set { _realtotal = value; }
            get { return _realtotal; }
        }
        /// <summary>
        /// 已退货款
        /// </summary>
        public decimal PayedTotal
        {
            set { _payedtotal = value; }
            get { return _payedtotal; }
        }
        /// <summary>
        /// 应退货款
        /// </summary>
        public decimal WairPayTotal
        {
            set { _wairpaytotal = value; }
            get { return _wairpaytotal; }
        }
        /// <summary>
        /// 是否增值税（0否,1是）
        /// </summary>
        public string isAddTax
        {
            set { _isaddtax = value; }
            get { return _isaddtax; }
        }
        /// <summary>
        /// 退货数量合计
        /// </summary>
        public decimal CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
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
        /// 制单人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime ? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime ? ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 发货出库人
        /// </summary>
        public int OutUserID
        {
            set { _outuserid = value; }
            get { return _outuserid; }
        }
        /// <summary>
        /// 发货出库时间
        /// </summary>
        public DateTime ? OutDate
        {
            set { _outdate = value; }
            get { return _outdate; }
        }
        /// <summary>
        /// 结算人
        /// </summary>
        public int SttlUserID
        {
            set { _sttluserid = value; }
            get { return _sttluserid; }
        }
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime ? SttlDate
        {
            set { _sttldate = value; }
            get { return _sttldate; }
        }
        /// <summary>
        /// 结单人
        /// </summary>
        public int Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单日期
        /// </summary>
        public DateTime ? CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ? ModifiedDate
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
        /// 源单类型（0无来源，1销售订单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 发货模式（1分店发货，2总部发货）
        /// </summary>
        public string SendMode
        {
            set { _sendmode = value; }
            get { return _sendmode; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
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
        /// 客户手机号
        /// </summary>
        public string CustMobile
        {
            set { _custmobile = value; }
            get { return _custmobile; }
        }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustAddr
        {
            set { _custaddr = value; }
            get { return _custaddr; }
        }
        /// <summary>
        /// 验货入库人
        /// </summary>
        public int InUserID
        {
            set { _inuserid = value; }
            get { return _inuserid; }
        }
        /// <summary>
        /// 验货入库时间
        /// </summary>
        public DateTime ? InDate
        {
            set { _indate = value; }
            get { return _indate; }
        }
        #endregion Model
    }
}
