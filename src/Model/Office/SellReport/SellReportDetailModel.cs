using System;
using System.Collections.Generic;
using System.Text;

namespace XBase.Model.Office.SellReport
{
    public class SellReportDetailModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int? _sellreportid;
        private int? _sellerid;
        private decimal? _rate;
        /// <summary>
        /// 
        /// </summary>
        public int id
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
        /// 销售汇报ID
        /// </summary>
        public int? SellReportID
        {
            set { _sellreportid = value; }
            get { return _sellreportid; }
        }
        /// <summary>
        /// 营业员
        /// </summary>
        public int? SellerID
        {
            set { _sellerid = value; }
            get { return _sellerid; }
        }
        /// <summary>
        /// 提成比例
        /// </summary>
        public decimal? Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        #endregion Model
    }
}
