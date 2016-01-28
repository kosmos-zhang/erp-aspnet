/**********************************************
 * 类作用：   OfficeThingsBuyModel表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/05/08
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：OfficeThingsBuyModel
    /// 描述：OfficeThingsBuy表数据模板
    /// 作者：lysong
    /// 创建时间：2009/05/08
    /// </summary>
   public class OfficeThingsBuyModel
   {
        #region OfficeThingsBuy表数据模板
        private int _id;
        private string _companycd;
        private string _buyrecordno;
        private string _title;
        private int _buyerid;
        private int _buydeptid;
        private DateTime _buydate;
        private DateTime _towarehousedate;
        private decimal _totalcount;
        private decimal _totalprice;
        private string _billstatus;
        private int _creator;
        private DateTime _createdate;
        private int _confirmor;
        private DateTime _confirmdate;
        private string _remark;
        private DateTime _modifieddate;
        private string _modifieduserid;


        private string _FromType;
           /// <summary>
        /// 源单来源
        /// </summary>
           public string FromType
        {
            set { _FromType = value; }
            get { return _FromType; }
        }
       
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
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
        public string BuyRecordNo
        {
            set { _buyrecordno = value; }
            get { return _buyrecordno; }
        }
        /// <summary>
        /// 单据主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 采购人ID
        /// </summary>
        public int BuyerID
        {
            set { _buyerid = value; }
            get { return _buyerid; }
        }
        /// <summary>
        /// 采购部门ID
        /// </summary>
        public int BuyDeptID
        {
            set { _buydeptid = value; }
            get { return _buydeptid; }
        }
        /// <summary>
        /// 采购日期
        /// </summary>
        public DateTime BuyDate
        {
            set { _buydate = value; }
            get { return _buydate; }
        }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime ToWarehouseDate
        {
            set { _towarehousedate = value; }
            get { return _towarehousedate; }
        }
        /// <summary>
        /// 采购总数量
        /// </summary>
        public decimal TotalCount
        {
            set { _totalcount = value; }
            get { return _totalcount; }
        }
        /// <summary>
        /// 采购总金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 单据状态（1制单，2结单）
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
        public DateTime CreateDate
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
        public DateTime ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
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
        /// 最后更新时间
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
