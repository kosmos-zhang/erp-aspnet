using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
    public class ProductModel
    {
        #region Model
        private string _sortno;
        private string _productid;
        private string _productcount;
        private string _frombillid;
        private string _frombillno;
        private string _fromlineno;
        private string _fromtype;
        private string _UsedUnitCount;
        /// <summary>
        /// 
        /// </summary>
        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromBillNo
        {
            set { _frombillno = value; }
            get { return _frombillno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromLineNo
        {
            set { _fromlineno = value; }
            get { return _fromlineno; }
        }
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        public string UsedUnitCount
        {
            set { _UsedUnitCount = value; }
            get { return _UsedUnitCount; }
        }



        #endregion
    }
}
