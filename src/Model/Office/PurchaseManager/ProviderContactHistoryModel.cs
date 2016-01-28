/***********************************************
 * 类作用：   PurchaseManager表数据模板        *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/25                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
    /// <summary>
    /// 类名：ProviderContractHistoryModel
    /// 描述：ProviderContractHistory表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/25
    /// 最后修改时间：2009/04/25
    /// </summary>
    ///
    public class ProviderContactHistoryModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _custid;
        private string _contactno;
        private string _title;
        private int _linkmanid;
        private string _linkmanname;
        private int _linkreasonid;
        private int _linkmode;
        private DateTime ? _linkdate;
        private int _linker;
        private string _contents;
        /// <summary>
        /// 自动生成
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
        /// 供应商编号(对应供应商信息表中的ID)
        /// </summary>
        public string CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 联络单编号
        /// </summary>
        public string ContactNo
        {
            set { _contactno = value; }
            get { return _contactno; }
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
        /// 供应商被联络人ID
        /// </summary>
        public int LinkManID
        {
            set { _linkmanid = value; }
            get { return _linkmanid; }
        }
        /// <summary>
        /// 供应商被联络人
        /// </summary>
        public string LinkManName
        {
            set { _linkmanname = value; }
            get { return _linkmanname; }
        }
        /// <summary>
        /// 联络事由ID(分类代码表设置)
        /// </summary>
        public int LinkReasonID
        {
            set { _linkreasonid = value; }
            get { return _linkreasonid; }
        }
        /// <summary>
        /// 联络方式ID(分类代码表设置)
        /// </summary>
        public int LinkMode
        {
            set { _linkmode = value; }
            get { return _linkmode; }
        }
        /// <summary>
        /// 联络时间
        /// </summary>
        public DateTime ? LinkDate
        {
            set { _linkdate = value; }
            get { return _linkdate; }
        }
        /// <summary>
        /// 联络人
        /// </summary>
        public int Linker
        {
            set { _linker = value; }
            get { return _linker; }
        }
        /// <summary>
        /// 联络内容
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        #endregion Model
    }
}
