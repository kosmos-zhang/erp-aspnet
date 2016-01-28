using System;

namespace XBase.Model.Office.PurchaseManager
{
    /// <summary>
    /// 实体类PurchaseApply 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class PurchaseApplyModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _applyno;
        private string _title;
        private string _fromtype;
        private string _typeid;
        private string _applyuserid;
        private string _applydeptid;
        private string _address;
        private string _counttotal;
        private string _confirmor;
        private string _confirmdate;
        private string _closer;
        private string _closedate;
        private string _applydate;
        private string _remark;
        private string _billstatus;
        private string _creator;
        private string _createdate;
        private string _modifieduserid;
        private string _modifieddate;

        private string _startapplydate;
        private string _endapplydate;
        private string _flowstatus;

        public string EFIndex
        {
            set;
            get;
        }
        public string EFDesc
        {
            set;
            get;
        }

        /// <summary>
        /// 自动生成
        /// </summary>
        public string id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string ApplyNo
        {
            set { _applyno = value; }
            get { return _applyno; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 源单类型（0无来源，1销售订单，2物料需求计划）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 采购类别ID（分类代码表ID）
        /// </summary>
        public string TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 申请人ID（对应员工表ID）
        /// </summary>
        public string ApplyUserID
        {
            set { _applyuserid = value; }
            get { return _applyuserid; }
        }
        /// <summary>
        /// 申请部门ID（对应部门表ID）
        /// </summary>
        public string ApplyDeptID
        {
            set { _applydeptid = value; }
            get { return _applydeptid; }
        }
        /// <summary>
        /// 到货地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 合计数量
        /// </summary>
        public string CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 确认人ID
        /// </summary>
        public string Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        public string ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 结单人ID
        /// </summary>
        public string Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单时间
        /// </summary>
        public string CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public string ApplyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
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
        /// 单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        ///  制单人ID(对应员工表ID)
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        ///  最后更新用户ID(对应操作用户UserID) 
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }

        
        /// <summary>
        ///  起始申请时间
        /// </summary>
        public string StartApplyDate
        {
            set { _startapplydate = value; }
            get { return _startapplydate; }
        }
        /// <summary>
        ///  终止申请时间 
        /// </summary>
        public string EndApplyDate
        {
            set { _endapplydate = value; }
            get { return _endapplydate; }
        }
        /// <summary>
        ///  审批状态
        /// </summary>
        public string FlowStatus
        {
            set { _flowstatus = value; }
            get { return _flowstatus; }
        }
        #endregion Model
    }
}

