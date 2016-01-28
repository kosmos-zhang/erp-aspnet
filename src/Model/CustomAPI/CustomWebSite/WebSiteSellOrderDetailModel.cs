using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.CustomAPI.CustomWebSite
{
    public class WebSiteSellOrderDetailModel
    {
        #region 字段
        private int _id;
        private string _companycd;
        private string _orderno;
        private int _productid;
        private decimal _baseprice;
        private decimal _basecount;
        private int _usedunitid;
        private decimal _usedcount;
        private decimal _exrate;
        private int _baseunitid;
        private decimal _usedprice;
        private decimal _totalprice;
        #endregion

        #region 属性

        public decimal TotalPrice
        {
            get { return _totalprice; }
            set { _totalprice = value; }
        }


        public int BaseUnitID
        {
            get { return _baseunitid; }
            set { _baseunitid = value; }
        }

        public decimal UsedPrice
        {
            get { return _usedprice; }
            set { _usedprice = value; }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            get { return _companycd; }
            set { _companycd = value; }
        }


        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo
        {
            get { return _orderno; }
            set { _orderno = value; }
        }

        /// <summary>
        /// 物品ID
        /// </summary>
        public int ProductID
        {
            get { return _productid; }
            set { _productid = value; }
        }

        /// <summary>
        /// 基本价格
        /// </summary>
        public decimal BasePrice
        {
            get { return _baseprice; }
            set { _baseprice = value; }
        }

        /// <summary>
        ///基本数量
        /// </summary>
        public decimal BaseCount
        {
            get { return _basecount; }
            set { _basecount = value; }
        }
        
        /// <summary>
        /// 基本单位ID
        /// </summary>
        public int UsedUnitID
        {
            get { return _usedunitid; }
            set { _usedunitid = value; }
        }
        /// <summary>
        /// 基本数量
        /// </summary>
        public decimal UsedCount
        {
            get { return _usedcount; }
            set { _usedcount = value; }
        }

        /// <summary>
        /// 基本单位与使用单位换算率
        /// </summary>
        public decimal ExRate
        {
            get { return _exrate; }
            set { _exrate = value; }
        }
        #endregion
    }
}
