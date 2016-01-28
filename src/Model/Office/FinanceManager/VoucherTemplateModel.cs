/**********************************************
 * 类作用：   VoucherTemplateModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2010/03/29
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类VoucherTemplateModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class VoucherTemplateModel
    {
        public VoucherTemplateModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _temno;
        private string _temname;
        private int _temtype;
        private string _abstract;
        private string _remark;
        private string _usedstatus;
        private int _creator;
        private DateTime? _createdate;
        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemNo
        {
            set { _temno = value; }
            get { return _temno; }
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemName
        {
            set { _temname = value; }
            get { return _temname; }
        }
        /// <summary>
        /// 模板类型（1.采购订单，2. 销售订单，3. 委托代销单，4. 销售退货单，5. 采购入库，6. 其他出库单，7. 销售出库单，8. 其他入库单，9.收款单，10.付款单）
        /// </summary>
        public int TemType
        {
            set { _temtype = value; }
            get { return _temtype; }
        }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstract
        {
            set { _abstract = value; }
            get { return _abstract; }
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
        /// 启用状态(0 停用，1 启用)
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model

    }
}

