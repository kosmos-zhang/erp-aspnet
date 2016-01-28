/**********************************************
 * 类作用：   StepsDetailsModel表数据模板
 * 建立人：   莫申林
 * 建立时间： 2009/07/02
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类StepsDetailsModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class StepsDetailsModel
    {
        public StepsDetailsModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _payorincometype;
        private int _sourceid;
        private int _blendingid;
        private decimal _blendingamount;
        /// <summary>
        /// 标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 收付款类别：1.收款单 2.付款单
        /// </summary>
        public string PayOrInComeType
        {
            set { _payorincometype = value; }
            get { return _payorincometype; }
        }
        /// <summary>
        /// 收付款单ID
        /// </summary>
        public int SourceID
        {
            set { _sourceid = value; }
            get { return _sourceid; }
        }
        /// <summary>
        /// 勾兑明细ID
        /// </summary>
        public int BlendingID
        {
            set { _blendingid = value; }
            get { return _blendingid; }
        }
        /// <summary>
        /// 勾兑金额
        /// </summary>
        public decimal BlendingAmount
        {
            set { _blendingamount = value; }
            get { return _blendingamount; }
        }
        #endregion Model

    }
}

