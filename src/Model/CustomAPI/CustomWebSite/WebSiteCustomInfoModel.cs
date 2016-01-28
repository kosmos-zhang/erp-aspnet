/**********************************************
 * 类作用   站点用户实体类层
 * 创建人   xz
 * 创建时间 2010-3-15 16:11:03 
 ***********************************************/

using System;

namespace XBase.Model.CustomAPI.CustomWebSite
{
    /// <summary>
    /// 站点用户实体类
    /// </summary>
    [Serializable]
    public class WebSiteCustomInfoModel
    {
        #region 字段

        private Nullable<int> iD = null; //主键，自动生成
        private Nullable<int> customID = null; //往来单位ID
        private string loginUserName = String.Empty; //站点登陆用户名
        private string loginPassword = String.Empty; //站点登陆密码
        private string status = String.Empty; //状态1 启用0 禁用
        private string isMember = String.Empty; //是否会员1 是0 否

        #endregion


        #region 方法

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public WebSiteCustomInfoModel()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="iD">主键，自动生成</param>
        ///<param name="customID">往来单位ID</param>
        ///<param name="loginUserName">站点登陆用户名</param>
        ///<param name="loginPassword">站点登陆密码</param>
        ///<param name="status">状态1 启用0 禁用</param>
        ///<param name="isMember">是否会员1 是0 否</param>
        public WebSiteCustomInfoModel(
                    int iD,
                    int customID,
                    string loginUserName,
                    string loginPassword,
                    string status,
                    string isMember)
        {
            this.iD = iD; //主键，自动生成
            this.customID = customID; //往来单位ID
            this.loginUserName = loginUserName; //站点登陆用户名
            this.loginPassword = loginPassword; //站点登陆密码
            this.status = status; //状态1 启用0 禁用
            this.isMember = isMember; //是否会员1 是0 否
        }

        #endregion


        #region 属性

        /// <summary>
        /// 主键，自动生成
        /// </summary>
        public Nullable<int> ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        /// <summary>
        /// 往来单位ID
        /// </summary>
        public Nullable<int> CustomID
        {
            get
            {
                return customID;
            }
            set
            {
                customID = value;
            }
        }

        /// <summary>
        /// 站点登陆用户名
        /// </summary>
        public string LoginUserName
        {
            get
            {
                return loginUserName;
            }
            set
            {
                loginUserName = value;
            }
        }

        /// <summary>
        /// 站点登陆密码
        /// </summary>
        public string LoginPassword
        {
            get
            {
                return loginPassword;
            }
            set
            {
                loginPassword = value;
            }
        }

        /// <summary>
        /// 状态1 启用0 禁用
        /// </summary>
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        /// <summary>
        /// 是否会员1 是0 否
        /// </summary>
        public string IsMember
        {
            get
            {
                return isMember;
            }
            set
            {
                isMember = value;
            }
        }

        #endregion
    }
}