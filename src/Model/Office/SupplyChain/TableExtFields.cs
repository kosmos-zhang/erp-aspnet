using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SupplyChain
{
    public class TableExtFields
    {
        public TableExtFields()
        { }
        #region Model
        private int _id;
        private string _tabname;
        private string _companycd;
        private string _branchid;
        private int? _efindex;
        private string _efdesc;
        private string _modelno;
        private string _eftype;
        private string _efvaluelist;
        private string _functionmodule;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 扩展的表名
        /// </summary>
        public string TabName
        {
            set { _tabname = value; }
            get { return _tabname; }
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
        /// 总店分店
        /// </summary>
        public string BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 模板代码
        /// </summary>
        public string ModelNo
        {
            set { _modelno = value; }
            get { return _modelno; }
        }
        /// <summary>
        /// 字段索引
        /// </summary>
        public int? EFIndex
        {
            set { _efindex = value; }
            get { return _efindex; }
        }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string EFDesc
        {
            set { _efdesc = value; }
            get { return _efdesc; }
        }
        /// <summary>
        /// 控件类型（1-TextBox,2-Select）
        /// </summary>
        public string EFType
        {
            set { _eftype = value; }
            get { return _eftype; }
        }
        /// <summary>
        /// Select控件对应 的可选值列表（逗号分隔）
        /// </summary>
        public string EFValueList
        {
            set { _efvaluelist = value; }
            get { return _efvaluelist; }
        }
        /// <summary>
        /// 功能模块
        /// </summary>
        public string FunctionModule
        {
            set { _functionmodule = value; }
            get { return _functionmodule; }
        }
        
        #endregion Model
    }
}
