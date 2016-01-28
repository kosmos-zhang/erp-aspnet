/**********************************************
 * 类作用：   SalaryStandard表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/07
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：SalaryStandardModel
    /// 描述：SalaryStandard表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/07
    /// 最后修改时间：2009/05/07
    /// </summary>
    ///
    public class SalaryStandardModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _quarterid;
        private string _quartername;
        private string _adminlevel;
        private string _adminlevelname;
        private string _itemno;
        private string _itemname;
        private string _unitprice;
        private string _remark;
        private string _usedstatus;
        private string _usedstatusname;
        /// <summary>
        /// 内部ID
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string QuarterName
        {
            set { _quartername = value; }
            get { return _quartername; }
        }
        /// <summary>
        /// 岗位ID(对应岗位表ID)
        /// </summary>
        public string QuarterID
        {
            set { _quarterid = value; }
            get { return _quarterid; }
        }
        /// <summary>
        /// 岗位职等名称
        /// </summary>
        public string AdminLevelName
        {
            set { _adminlevelname = value; }
            get { return _adminlevelname; }
        }
        /// <summary>
        /// 岗位职等（分类代码表设置）
        /// </summary>
        public string AdminLevel
        {
            set { _adminlevel = value; }
            get { return _adminlevel; }
        }
        /// <summary>
        /// 工资项编号
        /// </summary>
        public string ItemNo
        {
            set { _itemno = value; }
            get { return _itemno; }
        }
        /// <summary>
        /// 工资项名称
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public string UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
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
        /// 启用状态名称

        /// </summary>
        public string UsedStatusName
        {
            set { _usedstatusname = value; }
            get { return _usedstatusname; }
        }
        /// <summary>
        /// 启用状态(0 停用，1 启用)

        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        #endregion Model
    }
}
