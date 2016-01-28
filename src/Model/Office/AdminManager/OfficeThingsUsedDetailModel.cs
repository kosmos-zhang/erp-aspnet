/**********************************************
 * 类作用：   OfficeThingsUsedDetail表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/05/07
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：OfficeThingsUsedDetailModel
    /// 描述：OfficeThingsUsedDetail表数据模板
    /// 作者：lysong
    /// 创建时间：2009/05/09
    /// </summary>
   public class OfficeThingsUsedDetailModel
   {
        #region OfficeThingsUsedDetail表数据模板
        private int _id;
        private string _companycd;
        private string _applyno;
        private string _thingno;
        private decimal _count;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 自增ID
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
        /// 领用单号
        /// </summary>
        public string ApplyNo
        {
            set { _applyno = value; }
            get { return _applyno; }
        }
        /// <summary>
        /// 用品编号（对应用品表中的用品编号）
        /// </summary>
        public string ThingNo
        {
            set { _thingno = value; }
            get { return _thingno; }
        }
        /// <summary>
        /// 领用数量
        /// </summary>
        public decimal Count
        {
            set { _count = value; }
            get { return _count; }
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
