using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class ManufactureReportMeterialModel
    {
        #region Model
        private string _productid;
        private string _takenum;
        private string _beforenum;
        private string _usednum;
        private string _badnum;
        private string _nownum;
        private string _remark;
        /// <summary>
        /// 物料ID
        /// </summary>
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 本日领料数量
        /// </summary>
        public string TakeNum
        {
            set { _takenum = value; }
            get { return _takenum; }
        }
        /// <summary>
        /// 昨日结存数量
        /// </summary>
        public string BeforeNum
        {
            set { _beforenum = value; }
            get { return _beforenum; }
        }
        /// <summary>
        /// 本日耗用数量
        /// </summary>
        public string UsedNum
        {
            set { _usednum = value; }
            get { return _usednum; }
        }
        /// <summary>
        /// 本日损坏数量
        /// </summary>
        public string BadNum
        {
            set { _badnum = value; }
            get { return _badnum; }
        }
        /// <summary>
        /// 本日结存数量
        /// </summary>
        public string NowNum
        {
            set { _nownum = value; }
            get { return _nownum; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
