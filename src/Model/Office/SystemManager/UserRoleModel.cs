/**********************************************
 * 类作用：   UserRole表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/01/12
 ***********************************************/

using System;

namespace XBase.Model.Office.SystemManager
{
    /// <summary>
    /// 类名：UserRoleModel
    /// 描述：UserRole表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/12
    /// 最后修改时间：2009/01/12
    /// </summary>
    ///
    public class UserRoleModel
    {
        //公司代码
        private string _CompanyCD;
        //用户ID
        private string _UserID;
        //角色ID
        private int _RoleID;
        //记录修改日期
        private DateTime _ModifiedDate;
        //记录修改人
        private string _ModifiedUserID;

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            get
            {
                return _CompanyCD;
            }
            set
            {
                _CompanyCD = value;
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }/// <summary>
        /// 记录修改日期
        /// </summary>
        public DateTime ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }
            set
            {
                _ModifiedDate = value;
            }
        }

        /// <summary>
        /// 记录修改人
        /// </summary>
        public string ModifiedUserID
        {
            get
            {
                return _ModifiedUserID;
            }
            set
            {
                _ModifiedUserID = value;
            }
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID
        {
            get
            {
                return _RoleID;
            }
            set
            {
                _RoleID = value;
            }
        }
    }
}
