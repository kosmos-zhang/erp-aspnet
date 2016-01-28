/***********************************
 * 描述：门店进销存日报表（分店报表）检索条件类
 * 创建人：何小武
 * 创建时间：2010-5-05
 * 修改记录：
 *      1、添加字段：Manufacturer，FromAddr，ColorID，Size，BarCode，MaterialID。
 *          2010-5-9 by hexw
 * ***********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.OperatingModel
{
    public class SubStoreInvoicingDateRptModel
    {
        private string _initDate;
        private string _productID;
        private string _productNo;
        private string _productName;
        private string _specification;
        private string _batchNo;
        private string _storageID;
        private string _companyCD;
        private string _subDeptID;
        private string _preLength;
        private string _manufacturer;
        private string _fromAddr;
        private string _colorID;
        private string _size;
        private string _barCode;
        private string _materialID;
        
        /// <summary>
        /// 日期
        /// </summary>
        public string InitDate
        {
            set { _initDate = value; }
            get { return _initDate; }
        }
        /// <summary>
        /// 物品ID
        /// </summary>
        public string ProductID
        {
            set { _productID = value; }
            get { return _productID; }
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
        /// 规格
        /// </summary>
        public string Specification
        {
            set { _specification = value; }
            get { return _specification; }
        }
        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo
        {
            set { _batchNo = value; }
            get { return _batchNo; }
        }
        /// <summary>
        /// 仓库
        /// </summary>
        public string StorageID
        {
            set { _storageID = value; }
            get { return _storageID; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companyCD = value; }
            get { return _companyCD; }
        }
        /// <summary>
        /// 分店ID
        /// </summary>
        public string SubDeptID
        {
            set { _subDeptID = value; }
            get { return _subDeptID; }
        }
        /// <summary>
        /// 小数精度
        /// </summary>
        public string PreLength
        {
            set { _preLength = value; }
            get { return _preLength; }
        }
        /// <summary>
        /// 厂家
        /// </summary>
        public string Manufacturer
        {
            set { _manufacturer = value; }
            get { return _manufacturer; }
        }
        /// <summary>
        /// 产地
        /// </summary>
        public string FromAddr
        {
            set { _fromAddr = value; }
            get { return _fromAddr; }
        }
        /// <summary>
        /// 颜色
        /// </summary>
        public string ColorID
        {
            set { _colorID = value; }
            get { return _colorID; }
        }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size
        {
            set { _size = value; }
            get { return _size; }
        }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode
        {
            set { _barCode = value; }
            get { return _barCode; }
        }
        /// <summary>
        /// 材质
        /// </summary>
        public string MaterialID
        {
            set { _materialID = value; }
            get { return _materialID; }
        }
    }
}
