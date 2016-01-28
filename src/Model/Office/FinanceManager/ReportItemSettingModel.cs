/**********************************************
 * 类作用：   ReportItemSetting表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/03/10
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
   public class ReportItemSettingModel
    {
        private string _companycd;
        private int _id;
        private string _itemtype;
        private int _itemrow;
        private string _itemname;
        private string _usedstatus;
        /// <summary>
        /// 公司编码
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
        /// 项目类别
        /// </summary>
        public string ItemType
        {
            set { _itemtype = value; }
            get { return _itemtype; }
        }
        /// <summary>
        /// 项目行号
        /// </summary>
        public int ItemRow
        {
            set { _itemrow = value; }
            get { return _itemrow; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        /// <summary>
        /// 使用状态
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
    }
}
