/**********************************************
 * 类作用：   OfficeThingsBuyDetail表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/05/08
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：OfficeThingsBuyDetailModel
    /// 描述：OfficeThingsBuyDetail表数据模板
    /// 作者：lysong
    /// 创建时间：2009/05/08
    /// </summary>
   public class OfficeThingsBuyDetailModel
   {
        #region OfficeThingsBuyDetail表数据模板
        private int _id;
        private string _companycd;
        private string _buyrecordno;
        private string _thingno;
        private string _provider;
        private decimal _unitprice;
        private decimal _buycount;
        private decimal _buymoney;
        private DateTime _modifieddate;
        private string _modifieduserid;


        private string _FromSortNo;
       private string _FromBillID;



       private string _fromBillNo;

       /// <summary>
       ///原单编号
       /// </summary>
       public string fromBillNo
       {
           set { _fromBillNo = value; }
           get { return _fromBillNo; }
       }



         /// <summary>
        ///原单ID
        /// </summary>
        public string FromBillID
        {
            set { _FromBillID = value; }
            get { return _FromBillID; }
        }


     

         /// <summary>
        /// 原单序号
        /// </summary>
        public string FromSortNo
        {
            set { _FromSortNo = value; }
            get { return _FromSortNo; }
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
        /// 采购单编号
        /// </summary>
        public string BuyRecordNo
        {
            set { _buyrecordno = value; }
            get { return _buyrecordno; }
        }
        /// <summary>
        /// 用品编号（对应用品表中的用品编号）
        /// </summary>
        public string ThingNo
        {
            set { _thingno = value; }
            get { return _thingno; }
        }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Provider
        {
            set { _provider = value; }
            get { return _provider; }
        }
        /// <summary>
        /// 采购单价
        /// </summary>
        public decimal UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }
        /// <summary>
        /// 采购数量
        /// </summary>
        public decimal BuyCount
        {
            set { _buycount = value; }
            get { return _buycount; }
        }
        /// <summary>
        /// 采购金额
        /// </summary>
        public decimal BuyMoney
        {
            set { _buymoney = value; }
            get { return _buymoney; }
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
