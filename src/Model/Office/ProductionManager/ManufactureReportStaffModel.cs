using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class ManufactureReportStaffModel
    {
        #region Model
        private string _staffid;
        private string _staffname;
        private string _worktime;
        private string _finishnum;
        private string _passnum;
        private string _passpercent;
        private string _remark;
        /// <summary>
        /// 人员ID（对应员工表ID）
        /// </summary>
        public string StaffID
        {
            set { _staffid = value; }
            get { return _staffid; }
        }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string StaffName
        {
            set { _staffname = value; }
            get { return _staffname; }
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
        /// 完成数
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
        #endregion Model
    }
}
