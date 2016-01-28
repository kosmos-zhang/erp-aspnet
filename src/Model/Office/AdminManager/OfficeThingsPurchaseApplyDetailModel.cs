using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.AdminManager
{
  public   class OfficeThingsPurchaseApplyDetailModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _applyno;
        private string _sortno;
        private string _thingid;
        private string _requirecount;
        private string _requiredate;
        private string _incount;
        /// <summary>
        /// 自动生成
        /// </summary>
        public string ID
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
        /// 采购申请单编号（对应办公用品采购申请单表中的单据编号）
        /// </summary>
        public string ApplyNo
        {
            set { _applyno = value; }
            get { return _applyno; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 用品ID（对应用品档案表ID）
        /// </summary>
        public string ThingID
        {
            set { _thingid = value; }
            get { return _thingid; }
        }
        /// <summary>
        /// 申请数量
        /// </summary>
        public string RequireCount
        {
            set { _requirecount = value; }
            get { return _requirecount; }
        }
        /// <summary>
        /// 需求日期
        /// </summary>
        public string RequireDate
        {
            set { _requiredate = value; }
            get { return _requiredate; }
        }
        /// <summary>
        /// 已入库数量（由用品入库模块更新）
        /// </summary>
        public string InCount
        {
            set { _incount = value; }
            get { return _incount; }
        }
        #endregion Model
    }
}
