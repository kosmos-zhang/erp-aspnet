/***********************************
 * 描述：销售发货明细列表检索条件Model
 * 创建人：hexw
 * 创建时间：2010-06-18  
 * ********************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class SellSendDetailsListModel
    {
        private string _companyCD;
        private string _productID;
        private string _custID;
        private string _beginDate;
        private string _endDate;
        private string _isOpenBill;
        private string _selPointLen;
        private bool _isMoreUnit;

        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companyCD = value; }
            get { return _companyCD;}
        }
        /// <summary>
        ///  商品ID
        /// </summary>
        public string ProductID
        {
            set { _productID = value; }
            get { return _productID; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string CustID
        {

            set { _custID = value; }
            get { return _custID; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginDate
        {
            set { _beginDate = value; }
            get { return _beginDate; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate
        {
            set { _endDate = value; }
            get { return _endDate; }
        }
        /// <summary>
        /// 是否已开票：1已开票，0未开票
        /// </summary>
        public string IsOpenBill
        {
            set { _isOpenBill = value; }
            get { return _isOpenBill; }
        }
        /// <summary>
        /// 当前session销售位数
        /// </summary>
        public string SelPointLen
        {
            set { _selPointLen = value; }
            get { return _selPointLen; }
        }
        /// <summary>
        /// 是否启用多计量单位:true启用， false不启用
        /// </summary>
        public bool IsMoreUnit
        {
            set { _isMoreUnit = value; }
            get { return _isMoreUnit; }
        }
    }
}
