/**********************************************
 * 类作用：   FinanceWarning表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/03/24
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
 public  class FinanceWarningModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _warningitem;
        private decimal _uplimit;
        private decimal _lowerlimit;
        private int _warningpersonid;
        private string _usedstatus;
        private string _warningway;
        private string _remark;
        /// <summary>
        /// 企业编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 报警项目
        /// </summary>
        public string WarningItem
        {
            set { _warningitem = value; }
            get { return _warningitem; }
        }
        /// <summary>
        /// 上限
        /// </summary>
        public decimal UpLimit
        {
            set { _uplimit = value; }
            get { return _uplimit; }
        }
        /// <summary>
        /// 下限
        /// </summary>
        public decimal Lowerlimit
        {
            set { _lowerlimit = value; }
            get { return _lowerlimit; }
        }
        /// <summary>
        /// 报警人
        /// </summary>
        public int WarningPersonID
        {
            set { _warningpersonid = value; }
            get { return _warningpersonid; }
        }
        /// <summary>
        /// 使用状态
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 报警方式
        /// </summary>
        public string WarningWay
        {
            set { _warningway = value; }
            get { return _warningway; }
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
