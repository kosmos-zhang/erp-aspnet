using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellReport
{
    public class UserProductInfo
    {
        public UserProductInfo()
        { }

        /// <summary>
        /// 构造函数 UserProductInfo
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="companyCD">CompanyCD</param>
        /// <param name="productNum">productNum</param>
        /// <param name="productName">productName</param>
        /// <param name="price">price</param>
        /// <param name="bref">bref</param>
        /// <param name="memo">memo</param>
        public UserProductInfo(int id, string companyCD, string productNum, string productName, decimal price, string bref, string memo)
        {
            _id = id;
            _companyCD = companyCD;
            _productNum = productNum;
            _productName = productName;
            _price = price;
            _bref = bref;
            _memo = memo;
        }

        public UserProductInfo(string companyCD, string productNum, string productName, decimal price, string bref, string memo)
        {
            _companyCD = companyCD;
            _productNum = productNum;
            _productName = productName;
            _price = price;
            _bref = bref;
            _memo = memo;
        }

        #region Model
        private int _id;
        private string _companyCD;
        private string _productNum;
        private string _productName;
        private decimal _price;
        private string _bref;
        private string _memo;
        /// <summary>
        /// id
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// CompanyCD
        /// </summary>
        public string CompanyCD
        {
            set { _companyCD = value; }
            get { return _companyCD; }
        }
        /// <summary>
        /// productNum
        /// </summary>
        public string productNum
        {
            set { _productNum = value; }
            get { return _productNum; }
        }
        /// <summary>
        /// productName
        /// </summary>
        public string productName
        {
            set { _productName = value; }
            get { return _productName; }
        }
        /// <summary>
        /// price
        /// </summary>
        public decimal price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// bref
        /// </summary>
        public string bref
        {
            set { _bref = value; }
            get { return _bref; }
        }
        /// <summary>
        /// memo
        /// </summary>
        public string memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        #endregion Model
    }
}
