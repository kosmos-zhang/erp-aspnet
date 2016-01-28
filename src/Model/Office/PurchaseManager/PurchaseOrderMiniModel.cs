/***********************************************
 * 类作用：   PurchaseManager表数据模板        *
 * 建立人：   王超                             *
 * 建立时间： 2009/04/27                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XBase.Model.Office.PurchaseManager
{
    public class PurchaseOrderMiniModel
    {
        #region Model
        private string _frombillid;
        private string _fromlineno;
        private string _productcount;
        /// <summary>
        /// 源单ID
        /// </summary>
        public string FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 源单行号
        /// </summary>
        public string FromLineNo
        {
            set { _fromlineno = value; }
            get { return _fromlineno; }
        }
        /// <summary>
        /// 订购数量
        /// </summary>
        public string ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        #endregion Model
    }
}
