using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.LogisticsDistributionManager
{
    public class SubDeliveryTransDetail
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _transno;
        private int? _sortno;
        private int? _productid;
        private decimal? _transcount;
        private decimal? _transprice;
        private decimal? _transpricetotal;




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
        public string TransNo
        {
            set { _transno = value; }
            get { return _transno; }
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
        public decimal? TransCount
        {
            set { _transcount = value; }
            get { return _transcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TransPrice
        {
            set { _transprice = value; }
            get { return _transprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TransPriceTotal
        {
            set { _transpricetotal = value; }
            get { return _transpricetotal; }
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

        
        /// <summary>
        /// 备注
        /// </summary>
        private string _remark = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
            }
        }
        #endregion Model

    }
}
