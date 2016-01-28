using System;

namespace XBase.Model.Office.SellManager
{
    /// <summary>
    /// 实体类SellChancePush 。
    /// </summary>
    public class SellChancePushModel
    {
        public SellChancePushModel()
		{}
        #region Model
        private int _id;
        private string _companycd;
        private string _chanceno;
        private DateTime? _pushdate;
        private int? _seller;
        private string _phase;
        private string _state;
        private int? _feasibility;
        private int? _delaydate;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public int ID
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
        /// 销售机会编号（对应销售机会表中的机会编号）
        /// </summary>
        public string ChanceNo
        {
            set { _chanceno = value; }
            get { return _chanceno; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? PushDate
        {
            set { _pushdate = value; }
            get { return _pushdate; }
        }
        /// <summary>
        /// 业务员ID(对应员工表ID)
        /// </summary>
        public int? Seller
        {
            set { _seller = value; }
            get { return _seller; }
        }
        /// <summary>
        /// 阶段（1初期沟通、2立项评估、3需求分析、4方案指定、5招投标/竞争、6商务谈判、7合同签约）
        /// </summary>
        public string Phase
        {
            set { _phase = value; }
            get { return _phase; }
        }
        /// <summary>
        /// 状态（1跟踪2成功3失败4搁置5失效）
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 机会可能性ID(对应分类代码表ID)
        /// </summary>
        public int? Feasibility
        {
            set { _feasibility = value; }
            get { return _feasibility; }
        }
        /// <summary>
        /// 阶段滞留时间(天)
        /// </summary>
        public int? DelayDate
        {
            set { _delaydate = value; }
            get { return _delaydate; }
        }
        /// <summary>
        /// 阶段备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime? ModifiedDate
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
        #endregion Model
    }
}
