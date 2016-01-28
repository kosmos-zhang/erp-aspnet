using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.LogisticsDistributionManager
{
    public class SubDeliveryBackDetail
    {

        #region Model
        private int _id;
        private string _companycd;
        private string _backno;
        private int? _sortno;
        private int? _productid;
        private int? _storageid;
        private decimal? _backcount;
        private decimal? _backprice;
        private decimal? _backpricetotal;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BackNo
        {
            set { _backno = value; }
            get { return _backno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? BackCount
        {
            set { _backcount = value; }
            get { return _backcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? BackPrice
        {
            set { _backprice = value; }
            get { return _backprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? BackPriceTotal
        {
            set { _backpricetotal = value; }
            get { return _backpricetotal; }
        }
        /// <summary>
        /// 基本计量数量
        /// </summary>
        private decimal? _usedUnitCount = null;

        /// <summary>
        /// 基本计量数量
        /// </summary>
        public decimal? UsedUnitCount
        {
            get
            {
                return _usedUnitCount;
            }
            set
            {
                _usedUnitCount = value;
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        private int? _unitID = null;

        /// <summary>
        /// 单位
        /// </summary>
        public int? UnitID
        {
            get
            {
                return _unitID;
            }
            set
            {
                _unitID = value;
            }
        }

        /// <summary>
        /// 单位换算率
        /// </summary>
        private decimal? _exRate = null;

        /// <summary>
        /// 单位换算率
        /// </summary>
        public decimal? ExRate
        {
            get
            {
                return _exRate;
            }
            set
            {
                _exRate = value;
            }
        }



        /// <summary>
        /// 配送批次
        /// </summary>
        private string _sendBatchNo = string.Empty;

        /// <summary>
        /// 配送批次
        /// </summary>
        public string SendBatchNo
        {
            get
            {
                return _sendBatchNo;
            }
            set
            {
                _sendBatchNo = value;
            }
        }


        /// <summary>
        /// 退货批次
        /// </summary>
        private string _backBatchNo = string.Empty;

        /// <summary>
        /// 退货批次
        /// </summary>
        public string BackBatchNo
        {
            get
            {
                return _backBatchNo;
            }
            set
            {
                _backBatchNo = value;
            }
        }
        #endregion Model

    }
}
