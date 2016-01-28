using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
  public   class QuterModuleSetModel
    {

        #region Model
        private string _id;
        private string _companycd;
        private string _deptid;
        private string _quarterno;
        private string _moduleid;
        private string _typeid;
        private string _sign;
        private string _quterdescribid;
        /// <summary>
        /// 内部id，自动生成
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
        /// 部门ID
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 岗位编号
        /// </summary>
        public string QuarterNo
        {
            set { _quarterno = value; }
            get { return _quarterno; }
        }
        /// <summary>
        /// 关联模块ID
        /// </summary>
        public string ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }
        /// <summary>
        /// 开启的模块中分类类型
        /// </summary>
        public string TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 1 为岗位说明书模块设置 2 新建岗位模块设置
        /// </summary>
        public string Sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        /// <summary>
        /// 岗位说明书ID 
        /// </summary>
        public string QuterDescribID
        {
            set { _quterdescribid = value; }
            get { return _quterdescribid; }
        }
        #endregion Model
    }
}
