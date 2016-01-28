/**********************************************
 * 类作用：   OfficeThingsStorage表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/05/08
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：OfficeThingsStorageModel
    /// 描述：OfficeThingsStorage表数据模板
    /// 作者：lysong
    /// 创建时间：2009/05/08
    /// </summary>
   public class OfficeThingsStorageModel
   {
        #region OfficeThingsStorage表数据模板
        private int _id;
        private string _companycd;
        private string _thingno;
        private int _totalcount;
        private int _surpluscount;
        private int _usedcount;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 内部自增ID
        /// </summary>
        public int ID
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
        /// 用品编号
        /// </summary>
        public string ThingNo
        {
            set { _thingno = value; }
            get { return _thingno; }
        }
        /// <summary>
        /// 累计总数
        /// </summary>
        public int TotalCount
        {
            set { _totalcount = value; }
            get { return _totalcount; }
        }
        /// <summary>
        /// 可用数量
        /// </summary>
        public int SurplusCount
        {
            set { _surpluscount = value; }
            get { return _surpluscount; }
        }
        /// <summary>
        /// 可用数量
        /// </summary>
        public int UsedCount
        {
            set { _usedcount = value; }
            get { return _usedcount; }
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model

    }
}
