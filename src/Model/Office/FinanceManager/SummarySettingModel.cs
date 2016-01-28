/**********************************************
 * 类作用：   Officedba.SummarySetting表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/03/10
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    /// <summary>
    /// 实体类SummarySetting 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class SummarySettingModel
    {
        private string _companycd;
        private int _summarycd;
        private int _summtypeid;
        private string _name;
        private string _usedstatus;
        private string _remark;
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 摘要编码
        /// </summary>
        public int SummaryCD
        {
            set { _summarycd = value; }
            get { return _summarycd; }
        }
        /// <summary>
        /// 摘要类别
        /// </summary>
        public int SummTypeID
        {
            set { _summtypeid = value; }
            get { return _summtypeid; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
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
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }
}
