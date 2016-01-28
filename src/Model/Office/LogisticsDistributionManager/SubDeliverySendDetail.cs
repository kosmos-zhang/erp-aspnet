using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.LogisticsDistributionManager
{
    public class SubDeliverySendDetail
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _sendno;
        private int? _sortno;
        private int? _productid;
        private int? _storageid;
        private decimal? _sendcount;
        private decimal? _sendprice;
        private decimal? _sendpricetotal;
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
        public string SendNo
        {
            set { _sendno = value; }
            get { return _sendno; }
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
        public decimal? SendCount
        {
            set { _sendcount = value; }
            get { return _sendcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SendPrice
        {
            set { _sendprice = value; }
            get { return _sendprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SendPriceTotal
        {
            set { _sendpricetotal = value; }
            get { return _sendpricetotal; }
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
        /// 批次
        /// </summary>
        private string _batchNo = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo
        {
            get
            {
                return _batchNo;
            }
            set
            {
                _batchNo = value;
            }
        }

        #endregion Model
    }
}
