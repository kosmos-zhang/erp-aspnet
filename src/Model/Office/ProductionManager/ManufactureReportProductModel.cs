using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class ManufactureReportProductModel
    {
        #region Model
        private string _productid;
        private string _worktime;
        private string _finishnum;
        private string _passnum;
        private string _passpercent;
        private string _remark;
        private string _frombillno;
        private string _frombillid;
        private string _fromlineno;

        /// <summary>
        /// 物品ID
        /// </summary>
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 工时
        /// </summary>
        public string WorkTime
        {
            set { _worktime = value; }
            get { return _worktime; }
        }
        /// <summary>
        /// 本日完成数
        /// </summary>
        public string FinishNum
        {
            set { _finishnum = value; }
            get { return _finishnum; }
        }
        /// <summary>
        /// 合格数
        /// </summary>
        public string PassNum
        {
            set { _passnum = value; }
            get { return _passnum; }
        }
        /// <summary>
        /// 合格率
        /// </summary>
        public string PassPercent
        {
            set { _passpercent = value; }
            get { return _passpercent; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 源单编号
        /// </summary>
        public string FromBillNo
        {
            set { _frombillno = value; }
            get { return _frombillno; }
        }
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
        #endregion Model
    }
}
