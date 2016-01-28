using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{ 
   
    /// <summary>
    /// 类名：PerformanceTypeModel
    /// 描述：PerformanceType表数据模板（考核类型设置）
    /// 
    /// 作者：王保军
    /// 创建日期：2009/04/20
    /// 最后修改日期：2009/04/20
    /// </summary>
   public  class PerformanceTypeModel
    {
        #region Model

        private string  _id;
        private string _companyCD;
        private string _typeName;
        private int _createUser;
        private DateTime _createDate;
        private string _usedStatus;
        private DateTime _modifiedDate;
        private string _modifiedUserID;
        private string _editFlag;
        private string _usedstatusname;
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string  ID
        {
            set
            {
                 _id=value ;
            }
            get 
            {
                return _id;
            }
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set
            {
                _companyCD = value;
            }
            get
            {
                return _companyCD;
            }
        }

        /// <summary>
        /// 考核类型名称
        /// </summary>
        public string TypeName
        {
            set 
            {
                _typeName = value;
            }
            get
            {
                return _typeName;
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUser
        {
            set
            {
                _createUser = value;
            }
            get
            {
                return _createUser;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            set
            {
                _createDate = value;
            }
            get
            {
                return _createDate;
            }

        }

        /// <summary>
        /// 启用状态
        /// </summary>
        public string UsedStatus
        {
            set
            {
                _usedStatus = value;
            }
            get
            {
                return _usedStatus;
            }
        }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set
            {
                _modifiedDate = value;
            }
            get
            {
                return _modifiedDate;
            }
        }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set
            {
                _modifiedUserID = value;
            }
            get
            {
                return _modifiedUserID;
            }
        }

       /// <summary>
       ///编辑标示 
       /// </summary>
        public string EditFlag
        {
            set
            {
                _editFlag = value;
            }
            get
            {
                return _editFlag;
            }
        }
        /// <summary>
        /// 启用状态(0 停用，1 启用) 
        /// </summary>
        public string UsedStatusName
        {
            set { _usedstatusname = value; }
            get { return _usedstatusname; }
        }


        #endregion

    }
}
