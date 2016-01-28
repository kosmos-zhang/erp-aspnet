/**********************************************
 * 类作用：   SalaryItem表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/04
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：SalaryItemModel
    /// 描述：SalaryItem表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/04
    /// 最后修改时间：2009/05/04
    /// </summary>
    ///
    public class SalaryItemModel
    {
        #region Model
        private string _itemno;
        private string _companycd;
        private string _itemname;
        private string _itemorder;
        private string _calculate;
        private string _calculateParam;
        private string _payflag;
        private string _changeflag;
        private string _usedstatus;
        private string _remark;
        private string _editflag;
        /// <summary>
        /// 编辑模式
        /// </summary>
        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
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
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
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
        /// 排列先后顺序
        /// </summary>
        public string ItemOrder
        {
            set { _itemorder = value; }
            get { return _itemorder; }
        }
        /// <summary>
        /// 计算公式
        /// </summary>
        public string Calculate
        {
            set { _calculate = value; }
            get { return _calculate; }
        }
        /// <summary>
        /// 公式参数
        /// </summary>
        public string CalculateParam
        {
            set { _calculateParam  = value; }
            get { return _calculateParam; }
        }
        /// <summary>
        /// 是否扣款(0 否，1是)

        /// </summary>
        public string PayFlag
        {
            set { _payflag = value; }
            get { return _payflag; }
        }
        /// <summary>
        /// 是否为固定项（0否，1是）
        /// </summary>
        public string ChangeFlag
        {
            set { _changeflag = value; }
            get { return _changeflag; }
        }
        /// <summary>
        /// 启用状态(0 停用，1 启用)

        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
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
