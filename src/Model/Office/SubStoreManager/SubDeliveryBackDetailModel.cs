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
    /// 类名：SubDeliveryBackDetailModel
    /// 描述：SubDeliveryBackDetail表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/18
    /// 最后修改时间：2009/05/18
    /// </summary>
    ///
    public class SubDeliveryBackDetailModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _deptid;
        private string _backno;
        private int _sortno;
        private int _productid;
        private int _storageid;
        private decimal _productcount;
        private decimal _unitprice;
        private decimal _totalprice;
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
        /// 分店ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 配送退货单号
        /// </summary>
        public string BackNo
        {
            set { _backno = value; }
            get { return _backno; }
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
        /// 入库仓库（对应仓库表ID）
        /// </summary>
        public int StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 退货数量
        /// </summary>
        public decimal ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 退货单价
        /// </summary>
        public decimal UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }
        /// <summary>
        /// 退货金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        #endregion Model
    }
}
