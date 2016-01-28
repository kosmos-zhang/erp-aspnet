using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageQualityCheckApplyDetail
    {

        public StorageQualityCheckApplyDetail()
        { }

        #region Model
        private int _id;
        public int ID
        { get { return _id; } set { _id = value; } }
        private string _applyNo;
        public string ApplyNo
        { get { return _applyNo; } set { _applyNo = value; } }
        private int _sortNo;
        public int SortNo
        { get { return _sortNo; } set { _sortNo = value; } }
        private int _productID;
        public int ProductID
        { get { return _productID; } set { _productID = value; } }
        private int _unitID;
        public int UnitID
        { get { return _unitID; } set { _unitID = value; } }
        private decimal _productCount;
        public decimal ProductCount
        { get { return _productCount; } set { _productCount = value; } }
        private string _remark;
        public string Remark
        { get { return _remark; } set { _remark = value; } }
        private string _fromType;
        public string FromType
        { get { return _fromType; } set { _fromType = value; } }
        private int _fromBillID;
        public int FromBillID
        { get { return _fromBillID; } set { _fromBillID = value; } }
        private int _fromLineNo;
        public int FromLineNo
        { get { return _fromLineNo; } set { _fromLineNo = value; } }
        private decimal _checkedCount;
        public decimal CheckedCount
        { get { return _checkedCount; } set { _checkedCount = value; } }
        private decimal _RealCheckedCount;
        public decimal RealCheckedCount
        {
            get { return _RealCheckedCount; }
            set { _RealCheckedCount = value; }
        }

        private int? _usedUnitID = null;

        /// <summary>
        /// 基本计量单位代码
        /// </summary>
        public int? UsedUnitID
        {
            get
            {
                return _usedUnitID;
            }
            set
            {
                _usedUnitID = value;
            }
        }

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

        #endregion
    }
}
