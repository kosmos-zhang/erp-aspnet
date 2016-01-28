/***********************************************
 * 类作用：   SubStorageManager表数据模板      *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/05/18                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SubStoreManager
{
    /// <summary>
    /// 类名：SubSellCustInfoModel
    /// 描述：SubSellCustInfo表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/18
    /// 最后修改时间：2009/05/18
    /// </summary>
    ///
    public class SubSellCustInfoModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _deptid;
        private string _custname;
        private string _custtel;
        private string _custmobile;
        private string _custaddr;
        private string _creator;
        private string _createdate;
        /// <summary>
        /// 自动生成
        /// </summary>
        public string ID
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
        /// 分店ID（对应组织机构表ID）
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
        }
        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string CustTel
        {
            set { _custtel = value; }
            get { return _custtel; }
        }
        /// <summary>
        /// 客户手机号
        /// </summary>
        public string CustMobile
        {
            set { _custmobile = value; }
            get { return _custmobile; }
        }
        /// <summary>
        /// 送货地址
        /// </summary>
        public string CustAddr
        {
            set { _custaddr = value; }
            get { return _custaddr; }
        }
        /// <summary>
        /// 建档人
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 建档日期
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model
    }
}
