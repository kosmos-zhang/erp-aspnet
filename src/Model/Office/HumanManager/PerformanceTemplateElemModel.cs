using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{

    /// <summary>
    /// 类名：PerformanceTemplateElemModel
    /// 描述：PerformanceTemplateElem表数据模板（考核模板指标表）
    /// 
    /// 作者：王保军
    /// 创建日期：2009/04/23
    /// 最后修改日期：2009/04/23
    /// </summary>
   public   class PerformanceTemplateElemModel
    {
        #region Model
        private string  _id;
        private string _companycd;
        private string _templateno;
        private string  _elemid;
        private string  _elemorder;
        private decimal _rate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _editFlag;
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string  ID
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
        /// 模板编号
        /// </summary>
        public string TemplateNo
        {
            set { _templateno = value; }
            get { return _templateno; }
        }
        /// <summary>
        /// 指标ID
        /// </summary>
        public string  ElemID
        {
            set { _elemid = value; }
            get { return _elemid; }
        }
        /// <summary>
        /// 指标顺序
        /// </summary>
        public string  ElemOrder
        {
            set { _elemorder = value; }
            get { return _elemorder; }
        }
        /// <summary>
        /// 权重
        /// </summary>
        public decimal Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 编辑标识
        /// </summary>
        public string EditFlag
        {
            set { _editFlag = value; }
            get { return _editFlag; }
        }
        #endregion Model
    }
}
