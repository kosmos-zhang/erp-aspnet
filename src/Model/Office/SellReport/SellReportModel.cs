/********************
**销售汇报
*创建人：hexw
*创建时间：2010-7-6
********************/
using System;
using System.Collections.Generic;
using System.Text;

namespace XBase.Model.Office.SellReport
{
    public class SellReportModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int? _selldept;
        private int? _productid;
        private string _productname;
        private decimal? _price;
        private decimal? _sellnum;
        private decimal? _sellprice;
        private DateTime? _createdate;
        private string _memo;
        private string _selPointLen;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 销售部门
        /// </summary>
        public int? SellDept
        {
            set { _selldept = value; }
            get { return _selldept; }
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
        /// 物品名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? SellNum
        {
            set { _sellnum = value; }
            get { return _sellnum; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? SellPrice
        {
            set { _sellprice = value; }
            get { return _sellprice; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 精度
        /// </summary>
        public string SelPointLen
        {
            set { _selPointLen = value; }
            get { return _selPointLen; }
        }
        #endregion Model
    }
}
