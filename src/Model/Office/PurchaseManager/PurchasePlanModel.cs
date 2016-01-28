using System;

namespace XBase.Model.Office.PurchaseManager
{
    public class PurchasePlanModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _planno;
        private string _title;
        private string _fromtype;
        private string _planuserid;
        private string _purchaser;
        private string _plandate;
        private string _plandeptid;
        private string _planmoney;
        private string _counttotal;
        private string _currencytype;
        private string _rate;
        private string _billstatus;
        private string _typeid;
        private string _remark;
        private string _creator;
        private string _createdate;
        private string _confirmor;
        private string _confirmdate;
        private string _closer;
        private string _closedate;
        private string _modifieddate;
        private string _modifieduserid;
        private string _endplandate;
        private string _flowstatus;
        private string _totalmoneymax;
        /// <summary>
        /// 自动生成
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }

        public string EFIndex
        { get; set; }
        public string EFDesc
        { get; set; }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 采购计划编号
        /// </summary>
        public string PlanNo
        {
            set { _planno = value; }
            get { return _planno; }
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
        /// 源单类型（0无来源，1采购申请单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 计划员（对应员工表ID）
        /// </summary>
        public string PlanUserID
        {
            set { _planuserid = value; }
            get { return _planuserid; }
        }
        /// <summary>
        /// 采购员（对应员工表ID）
        /// </summary>
        public string Purchaser
        {
            set { _purchaser = value; }
            get { return _purchaser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PlanDate
        {
            set { _plandate = value; }
            get { return _plandate; }
        }
        /// <summary>
        /// 采购部门（部门表ID）
        /// </summary>
        public string PlanDeptID
        {
            set { _plandeptid = value; }
            get { return _plandeptid; }
        }
        /// <summary>
        /// 计划金额合计
        /// </summary>
        public string PlanMoney
        {
            set { _planmoney = value; }
            get { return _planmoney; }
        }
        /// <summary>
        /// 计划数量合计
        /// </summary>
        public string CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 币种ID(对应货币代码表CD)
        /// </summary>
        public string CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public string Rate
        {
            set { _rate = value; }
            get { return _rate; }
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
        /// 采购类别ID（分类代码表ID）
        /// </summary>
        public string TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
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
        /// 制单人ID(对应员工表ID)
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
        /// 结单人
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
        /// 最后更新日期
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID(对应操作用户UserID)
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EndPlanDate
        {
            set { _endplandate = value; }
            get { return _endplandate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FlowStatus
        {
            set { _flowstatus = value; }
            get { return _flowstatus; }
        }
        public string TotalMoneyMax
        {
            set { _totalmoneymax = value; }
            get { return _totalmoneymax; }
        }
        #endregion Model

    }
}
