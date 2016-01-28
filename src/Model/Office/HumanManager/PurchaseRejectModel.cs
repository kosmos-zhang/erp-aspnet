/***********************************************
 * 类作用：   PurchaseManager表数据模板        *
 * 建立人：  王保军                            *
 * 建立时间： 2009/06/12                      *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：PurchaseRejectModel
    /// 描述：PurchaseReject表数据模板
    /// 
    /// 作者：王保军  
    /// 创建时间：2009/06/12   
    /// 最后修改时间：2009/06/12   
    /// </summary>
    ///
    public class PurchaseRejectModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _rejectno;
        private string _title;
        private string _fromtype;
        private string  _rejectdate;
        private string _purchaser;
        private string _deptid;
        private string _rejectreason;
        private string _currencytype;
        private string _rate;
        private string _paytype;
        private string _moneytype;
        private string _sendaddress;
        private string _receiveoveraddress;
        private string _receiveman;
        private string _receivetel;
        private string _remark;
        private string _storageid;
        private string _totalprice;
        private string _totaltax;
        private string _totalfee;
        private string _discount;
        private string _discounttotal;
        private string _realtotal;
        private string _isaddtax;
        private string _counttotal;
        private string _billstatus;
        private string _creator;
        private string  _createdate;
        private string _confirmor;
        private string  _confirmdate;
        private string _closer;
        private string  _closedate;
        private string  _modifieddate;
        private string _modifieduserid;
        private string _typeid;
        private string _providerid;
        private string _taketype;
        private string _carrytype;
        private string _totaldyfzk;
        private string _totalythkhj;
        /// <summary>
        /// 自动生成
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string RejectNo
        {
            set { _rejectno = value; }
            get { return _rejectno; }
        }
        /// <summary>
        /// 退货主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 源单类型（0无来源，1采购到货通知单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 退货日期
        /// </summary>
        public string  RejectDate
        {
            set { _rejectdate = value; }
            get { return _rejectdate; }
        }
        /// <summary>
        /// 采购员ID
        /// </summary>
        public string Purchaser
        {
            set { _purchaser = value; }
            get { return _purchaser; }
        }
        /// <summary>
        /// 部门（对应部门表ID）
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 退货原因ID
        /// </summary>
        public string RejectReason
        {
            set { _rejectreason = value; }
            get { return _rejectreason; }
        }
        /// <summary>
        /// 币种ID
        /// </summary>
        public string CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public string Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 结算方式ID（对应分类代码表ID）
        /// </summary>
        public string PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 支付方式ID（对应分类代码表ID）
        /// </summary>
        public string MoneyType
        {
            set { _moneytype = value; }
            get { return _moneytype; }
        }
        /// <summary>
        /// 发货地址
        /// </summary>
        public string SendAddress
        {
            set { _sendaddress = value; }
            get { return _sendaddress; }
        }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ReceiveOverAddress
        {
            set { _receiveoveraddress = value; }
            get { return _receiveoveraddress; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiveMan
        {
            set { _receiveman = value; }
            get { return _receiveman; }
        }
        /// <summary>
        /// 收货人联系电话
        /// </summary>
        public string ReceiveTel
        {
            set { _receivetel = value; }
            get { return _receivetel; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 仓库ID(对应仓库表ID)
        /// </summary>
        public string StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 金额合计
        /// </summary>
        public string TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 税额合计
        /// </summary>
        public string TotalTax
        {
            set { _totaltax = value; }
            get { return _totaltax; }
        }
        /// <summary>
        /// 含税金额合计
        /// </summary>
        public string TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 整单折扣（%）
        /// </summary>
        public string Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public string DiscountTotal
        {
            set { _discounttotal = value; }
            get { return _discounttotal; }
        }
        /// <summary>
        /// 折后含税金额
        /// </summary>
        public string RealTotal
        {
            set { _realtotal = value; }
            get { return _realtotal; }
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
        public string CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public string  CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人ID
        /// </summary>
        public string Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        public string  ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 结单人
        /// </summary>
        public string Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单时间
        /// </summary>
        public string  CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public string  ModifiedDate
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
        /// 采购类别ID（分类代码表ID）
        /// </summary>
        public string TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public string ProviderID
        {
            set { _providerid = value; }
            get { return _providerid; }
        }
        /// <summary>
        /// 交货方式ID（对应分类代码表ID）
        /// </summary>
        public string TakeType
        {
            set { _taketype = value; }
            get { return _taketype; }
        }
        /// <summary>
        /// 运送方式ID（对应分类代码表ID）
        /// </summary>
        public string CarryType
        {
            set { _carrytype = value; }
            get { return _carrytype; }
        }
        /// <summary>
        /// 抵应付帐款
        /// </summary>
        public string TotalDyfzk
        {
            set { _totaldyfzk = value; }
            get { return _totaldyfzk; }
        }
        /// <summary>
        /// 应退货款合计
        /// </summary>
        public string TotalYthkhj
        {
            set { _totalythkhj = value; }
            get { return _totalythkhj; }
        }
        #endregion Model
    }
}
