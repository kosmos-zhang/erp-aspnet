using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.LogisticsDistributionManager
{
    /// <summary>
    /// 类名：SubStorageProductModel
    /// 描述：SubStorageProduct表数据模板
    /// 物流配送模块
    /// 作者：宋飞
    /// 创建时间：2009/05/21
    /// 最后修改时间：2009/05/21
    /// </summary>
    ///
    public class SubStorageProductModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _productid;
        private int _deptid;
        private decimal _productcount;
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
        /// 产品ID（对应产品档案表ID）
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 分店ID（对应组织结构表ID）
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 现有存量
        /// </summary>
        public decimal ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        #endregion Model
    }
}
