/***********************************************
 * 类作用：   SubStorageManager表数据模板      *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/05/18                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SubStoreManager
{
    /// <summary>
    /// 类名：SubStorageInDetailModel
    /// 描述：SubStorageInDetail表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/18
    /// 最后修改时间：2009/05/18
    /// </summary>
    ///
    public class SubStorageInDetailModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _inno;
        private int _sortno;
        private int _productid;
        private decimal _sendcount;
        private decimal _sendprice;
        private decimal _sendpricetotal;
        private int _deptid;
        /// <summary>
        /// 自动生成
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
        /// 入库单号
        /// </summary>
        public string InNo
        {
            set { _inno = value; }
            get { return _inno; }
        }
        /// <summary>
        /// 行号（序号）
        /// </summary>
        public int SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 物品ID
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 入库数量
        /// </summary>
        public decimal SendCount
        {
            set { _sendcount = value; }
            get { return _sendcount; }
        }
        /// <summary>
        /// 入库价格
        /// </summary>
        public decimal SendPrice
        {
            set { _sendprice = value; }
            get { return _sendprice; }
        }
        /// <summary>
        /// 入库金额
        /// </summary>
        public decimal SendPriceTotal
        {
            set { _sendpricetotal = value; }
            get { return _sendpricetotal; }
        }
        /// <summary>
        /// 分店ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        #endregion Model
    }
}
