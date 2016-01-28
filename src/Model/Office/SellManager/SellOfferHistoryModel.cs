using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
   public  class SellOfferHistoryModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _offerno;
        private int? _productid;
        private DateTime? _offerdate;
        private int? _offertime;
        private int? _seller;
        private decimal? _totalprice;
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
        /// 报价单编号
        /// </summary>
        public string OfferNo
        {
            set { _offerno = value; }
            get { return _offerno; }
        }
        /// <summary>
        /// 物品ID
        /// </summary>
        public int? ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 报价时间
        /// </summary>
        public DateTime? OfferDate
        {
            set { _offerdate = value; }
            get { return _offerdate; }
        }
        /// <summary>
        /// 报价次数
        /// </summary>
        public int? OfferTime
        {
            set { _offertime = value; }
            get { return _offertime; }
        }
        /// <summary>
        /// 报价员ID
        /// </summary>
        public int? Seller
        {
            set { _seller = value; }
            get { return _seller; }
        }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal? TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        #endregion Model
    }
}
