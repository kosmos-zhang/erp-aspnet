using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class ManufactureReportMachineModel
    {
        #region Model
        private string _machineid;
        private string _machinename;
        private string _machineno;
        private string _usehour;
        private string _usepercent;
        private string _finishnum;
        private string _passnum;
        private string _passpercent;
        private string _remark;
        /// <summary>
        /// 设备ID（对应设备表ID）
        /// </summary>
        public string MachineID
        {
            set { _machineid = value; }
            get { return _machineid; }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string MachineName
        {
            set { _machinename = value; }
            get { return _machinename; }
        }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string MachineNo
        {
            set { _machineno = value; }
            get { return _machineno; }
        }
        /// <summary>
        /// 使用小时
        /// </summary>
        public string UseHour
        {
            set { _usehour = value; }
            get { return _usehour; }
        }
        /// <summary>
        /// 使用率
        /// </summary>
        public string UsePercent
        {
            set { _usepercent = value; }
            get { return _usepercent; }
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
