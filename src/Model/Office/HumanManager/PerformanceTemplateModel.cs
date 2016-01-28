using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：PerformanceTemplateModel
    /// 描述：PerformanceTemplate表数据模板（考核模板设置）
    /// 
    /// 作者：王保军
    /// 创建日期：2009/04/23
    /// 最后修改日期：2009/04/23
    /// </summary>
   public  class PerformanceTemplateModel
    {

        #region Model
        private string  _id;
        private string _companycd;
        private string _templateno;
        private string _title;
        private string  _typeid;
        private string _description;
        private string  _creator;
        private string  _createdate;
        private string  _modifieddate;
        private string _modifieduserid;
        private string _editFlag;
        private string _usedStatus;
        private string _createrName;
        private string _typeName;
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
        /// 考核模板编号
        /// </summary>
        public string TemplateNo
        {
            set { _templateno = value; }
            get { return _templateno; }
        }
        /// <summary>
        /// 考核模板主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 考核类型（对应考核类型表ID）
        /// </summary>
        public string  TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string  Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string  CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string  ModifiedDate
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
       /// <summary>
       /// 启用状态(0 停用，1 启用)

       /// </summary>
        public string UsedStatus
        {
            set { _usedStatus  = value; }
            get { return _usedStatus; }
        }
       /// <summary>
       /// 创建人名称
       /// </summary>
        public  string CreaterName
        {
            set { _createrName  = value; }
            get { return _createrName; }


        }
       /// <summary>
       /// 考核类型名称
       /// </summary>
        public string TypeName
        {
            set { _typeName = value; }
            get { return _typeName; }

        }



        #endregion Model

    }
}
