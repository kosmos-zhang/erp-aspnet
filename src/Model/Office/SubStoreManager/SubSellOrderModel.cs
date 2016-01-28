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
    /// 类名：SubSellOrderModel
    /// 描述：SubSellOrder表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/18
    /// 最后修改时间：2009/05/18
    /// </summary>
    ///
    public class SubSellOrderModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _orderno;
        private string _title;
        private string _deptid;
        private string _sendmode;
        private string _seller;
        private string _custname;
        private string _custtel;
        private string _custmobile;
        private string _custaddr;
        private string _currencytype;
        private string _rate;
        private string _ordermethod;
        private string _taketype;
        private string _paytype;
        private string _moneytype;
        private string _orderdate;
        private string _isaddtax;
        private string _planoutdate;
        private string _outdate;
        private string _carrytype;
        private string _busistatus;
        private string _outdeptid;
        private string _outuserid;
        private string _needsetup;
        private string _plansetdate;
        private string _setdate;
        private string _setuserinfo;
        private string _totalprice;
        private string _tax;
        private string _totalfee;
        private string _discount;
        private string _salefeetotal;
        private string _discounttotal;
        private string _realtotal;
        private string _payedtotal;
        private string _wairpaytotal;
        private string _counttotal;
        private string _billstatus;
        private string _creator;
        private string _createdate;
        private string _confirmor;
        private string _confirmdate;
        private string _closer;
        private string _closedate;
        private string _modifieddate;
        private string _modifieduserid;
        private string _attachment;
        private string _remark;
        private string _isopenbill;
        private string _sttluserid;
        private string _sttldate;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public string ID
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
        /// 订单编号
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
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
        /// 分店ID（对应组织机构表ID）
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
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
        /// 业务员(对应员工表ID)
        /// </summary>
        public string Seller
        {
            set { _seller = value; }
            get { return _seller; }
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
        /// 送货地址
        /// </summary>
        public string CustAddr
        {
            set { _custaddr = value; }
            get { return _custaddr; }
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
        /// 订货方式ID（对应分类代码表ID）
        /// </summary>
        public string OrderMethod
        {
            set { _ordermethod = value; }
            get { return _ordermethod; }
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
        /// 下单日期
        /// </summary>
        public string OrderDate
        {
            set { _orderdate = value; }
            get { return _orderdate; }
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
        /// 预约发货时间
        /// </summary>
        public string PlanOutDate
        {
            set { _planoutdate = value; }
            get { return _planoutdate; }
        }
        /// <summary>
        /// 实际发货出库时间
        /// </summary>
        public string OutDate
        {
            set { _outdate = value; }
            get { return _outdate; }
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
        /// 业务状态（1下单，2出库，3结算，4完成）
        /// </summary>
        public string BusiStatus
        {
            set { _busistatus = value; }
            get { return _busistatus; }
        }
        /// <summary>
        /// 配送部门
        /// </summary>
        public string OutDeptID
        {
            set { _outdeptid = value; }
            get { return _outdeptid; }
        }
        /// <summary>
        /// 发货出库人
        /// </summary>
        public string OutUserID
        {
            set { _outuserid = value; }
            get { return _outuserid; }
        }
        /// <summary>
        /// 是否需要安装（0否，1是）
        /// </summary>
        public string NeedSetup
        {
            set { _needsetup = value; }
            get { return _needsetup; }
        }
        /// <summary>
        /// 预约安装时间
        /// </summary>
        public string PlanSetDate
        {
            set { _plansetdate = value; }
            get { return _plansetdate; }
        }
        /// <summary>
        /// 实际安装时间
        /// </summary>
        public string SetDate
        {
            set { _setdate = value; }
            get { return _setdate; }
        }
        /// <summary>
        /// 安装工人及联系电话
        /// </summary>
        public string SetUserInfo
        {
            set { _setuserinfo = value; }
            get { return _setuserinfo; }
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
        public string Tax
        {
            set { _tax = value; }
            get { return _tax; }
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
        /// 销售费用合计
        /// </summary>
        public string SaleFeeTotal
        {
            set { _salefeetotal = value; }
            get { return _salefeetotal; }
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
        /// 已付款金额
        /// </summary>
        public string PayedTotal
        {
            set { _payedtotal = value; }
            get { return _payedtotal; }
        }
        /// <summary>
        /// 余款金额
        /// </summary>
        public string WairPayTotal
        {
            set { _wairpaytotal = value; }
            get { return _wairpaytotal; }
        }
        /// <summary>
        /// 订单数量合计
        /// </summary>
        public string CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
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
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public string Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public string ConfirmDate
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
        /// 结单日期
        /// </summary>
        public string CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public string ModifiedDate
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
        /// 附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
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
        /// 是否已开票（0否，1是）（由开票管理更新）
        /// </summary>
        public string isOpenbill
        {
            set { _isopenbill = value; }
            get { return _isopenbill; }
        }
        /// <summary>
        /// 结算人
        /// </summary>
        public string SttlUserID
        {
            set { _sttluserid = value; }
            get { return _sttluserid; }
        }
        /// <summary>
        /// 结算时间
        /// </summary>
        public string SttlDate
        {
            set { _sttldate = value; }
            get { return _sttldate; }
        }
        #endregion Model

        #region 扩展属性

        private string extField1 = String.Empty; //扩展属性1
        private string extField2 = String.Empty; //扩展属性2
        private string extField3 = String.Empty; //扩展属性3
        private string extField4 = String.Empty; //扩展属性4
        private string extField5 = String.Empty; //扩展属性5
        private string extField6 = String.Empty; //扩展属性6
        private string extField7 = String.Empty; //扩展属性7
        private string extField8 = String.Empty; //扩展属性8
        private string extField9 = String.Empty; //扩展属性9
        private string extField10 = String.Empty; //扩展属性10

        /// <summary>
        /// 扩展属性1
        /// </summary>
        public string ExtField1
        {
            get
            {
                return extField1;
            }
            set
            {
                extField1 = value;
            }
        }

        /// <summary>
        /// 扩展属性2
        /// </summary>
        public string ExtField2
        {
            get
            {
                return extField2;
            }
            set
            {
                extField2 = value;
            }
        }

        /// <summary>
        /// 扩展属性3
        /// </summary>
        public string ExtField3
        {
            get
            {
                return extField3;
            }
            set
            {
                extField3 = value;
            }
        }

        /// <summary>
        /// 扩展属性4
        /// </summary>
        public string ExtField4
        {
            get
            {
                return extField4;
            }
            set
            {
                extField4 = value;
            }
        }

        /// <summary>
        /// 扩展属性5
        /// </summary>
        public string ExtField5
        {
            get
            {
                return extField5;
            }
            set
            {
                extField5 = value;
            }
        }

        /// <summary>
        /// 扩展属性6
        /// </summary>
        public string ExtField6
        {
            get
            {
                return extField6;
            }
            set
            {
                extField6 = value;
            }
        }

        /// <summary>
        /// 扩展属性7
        /// </summary>
        public string ExtField7
        {
            get
            {
                return extField7;
            }
            set
            {
                extField7 = value;
            }
        }

        /// <summary>
        /// 扩展属性8
        /// </summary>
        public string ExtField8
        {
            get
            {
                return extField8;
            }
            set
            {
                extField8 = value;
            }
        }

        /// <summary>
        /// 扩展属性9
        /// </summary>
        public string ExtField9
        {
            get
            {
                return extField9;
            }
            set
            {
                extField9 = value;
            }
        }

        /// <summary>
        /// 扩展属性10
        /// </summary>
        public string ExtField10
        {
            get
            {
                return extField10;
            }
            set
            {
                extField10 = value;
            }
        }
        #endregion

    }
}
