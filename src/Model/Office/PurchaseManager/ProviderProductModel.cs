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
    /// 类名：ProviderInfoAccountModel
    /// 描述：ProviderInfoAccount表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/25
    /// 最后修改时间：2009/04/28
    /// </summary>
    ///
    public class ProviderProductModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _custno;
        private int _productid;
        private string _grade;
        private string _remark;
        private DateTime ? _joindate;
        private int _joiner;
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
        /// 供应商编号(对应供应商信息表中的编号)
        /// </summary>
        public string CustNo
        {
            set { _custno = value; }
            get { return _custno; }
        }
        /// <summary>
        /// 物料ID
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 推荐程度（1低，2中，3高）
        /// </summary>
        public string Grade
        {
            set { _grade = value; }
            get { return _grade; }
        }
        /// <summary>
        /// 推荐理由
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 推荐日期
        /// </summary>
        public DateTime ? JoinDate
        {
            set { _joindate = value; }
            get { return _joindate; }
        }
        /// <summary>
        /// 推荐人
        /// </summary>
        public int Joiner
        {
            set { _joiner = value; }
            get { return _joiner; }
        }
        #endregion Model
    }
}
