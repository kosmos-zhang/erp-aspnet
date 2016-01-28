using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public  class AdversaryDynamicModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _custno;
        private string _dynamic;
        private DateTime? _inputdate;
        private int? _inputuserid;
        /// <summary>
        /// 
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
        /// 竞争对手编号
        /// </summary>
        public string CustNo
        {
            set { _custno = value; }
            get { return _custno; }
        }
        /// <summary>
        /// 动态
        /// </summary>
        public string Dynamic
        {
            set { _dynamic = value; }
            get { return _dynamic; }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime? InputDate
        {
            set { _inputdate = value; }
            get { return _inputdate; }
        }
        /// <summary>
        /// 记录人
        /// </summary>
        public int? InputUserID
        {
            set { _inputuserid = value; }
            get { return _inputuserid; }
        }
        #endregion Model
    }
}
