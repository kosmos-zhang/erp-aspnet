/***********************************
 * 描述：销售状况分析
 * 创建人：何小武
 * 创建时间：2010-6-1
 * ********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.OperatingModel
{
    public class SalesFiguresAnalysisModel
    {
        private string _productNo;
        private string _productName;
        private string _custID;
        private string _dealerID;
        private string _beginDate;
        private string _endDate;
        private string _companyCD;

        /// <summary>
        /// 公司编码 
        /// </summary>
        public string CompanyCD
        {
            set { _companyCD = value; }
            get { return _companyCD; }
        }
        /// <summary>
        /// 物品编号
        /// </summary>
        public string ProductNo
        {
            set { _productNo = value; }
            get { return _productNo; }
        }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string ProductName
        {
            set { _productName = value; }
            get { return _productName; }
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
        /// 业务员ID
        /// </summary>
        public string DealerID
        {
            set { _dealerID = value; }
            get { return _dealerID; }
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
    }
}
