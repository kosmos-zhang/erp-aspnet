using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class SellGatheringModel
    {
        #region Model
        private int? _id;
        private string _title;
        private string _companycd;
        private string _gatheringno;
        private int? _custid;
        private string _fromtype;
        private int? _frombillid;
        private int? _currencytype;
        private DateTime? _plangatherdate;
        private decimal? _planprice;
        private string _gatheringtime;
        private decimal? _factprice;
        private DateTime? _factgatherdate;
        private int? _seller;
        private int? _selldeptid;
        private string _linkbillno;
        private string _state;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private int? _creator;
        private DateTime? _createdate;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public int? ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 回款计划编号
        /// </summary>
        public string GatheringNo
        {
            set { _gatheringno = value; }
            get { return _gatheringno; }
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
        /// 源单类型（0无来源，1发货通知单，2销售订单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 源单ID
        /// </summary>
        public int? FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 币种ID(对应货币代码表CD)
        /// </summary>
        public int? CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 计划回款日期
        /// </summary>
        public DateTime? PlanGatherDate
        {
            set { _plangatherdate = value; }
            get { return _plangatherdate; }
        }
        /// <summary>
        /// 计划回款金额
        /// </summary>
        public decimal? PlanPrice
        {
            set { _planprice = value; }
            get { return _planprice; }
        }
        /// <summary>
        /// 期次
        /// </summary>
        public string GatheringTime
        {
            set { _gatheringtime = value; }
            get { return _gatheringtime; }
        }
        /// <summary>
        /// 实际回款金额
        /// </summary>
        public decimal? FactPrice
        {
            set { _factprice = value; }
            get { return _factprice; }
        }
        /// <summary>
        /// 实际回款日期
        /// </summary>
        public DateTime? FactGatherDate
        {
            set { _factgatherdate = value; }
            get { return _factgatherdate; }
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
        /// 回款相关单据号
        /// </summary>
        public string LinkBillNo
        {
            set { _linkbillno = value; }
            get { return _linkbillno; }
        }
        /// <summary>
        /// 状态(1已回款2未回款 3部分回款)
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
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
        #endregion Model

    }
}
