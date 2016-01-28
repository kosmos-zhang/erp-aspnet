/**********************************************
 * 类作用：   用户登陆
 * 建立人：   吴志强
 * 建立时间： 2008/12/30
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using XBase.Data;

namespace XBase.Business
{
    /// <summary>
    /// 类名：LoginBus
    /// 描述：获取登陆用户的信息
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/30
    /// 最后修改时间：2008/12/30
    /// </summary>
    ///
    public class LoginBus
    {

        #region 获取用户登陆信息

        /// <summary>
        /// 获取登陆用户的信息
        /// </summary>
        /// <param name="UserID">用户名</param>
        /// <returns>DataTable 用户信息</returns>
        public static DataTable DoLogin(string UserID)
        {
            return LoginDBHelper.GetUserInfo(UserID);
        }

        #endregion

        #region 获取用户登陆信息

        /// <summary>
        /// 获取登陆用户的信息
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 用户信息</returns>
        public static void UpdateUserFlag(string userID, string companyCD)
        {
            LoginDBHelper.UpdateUserFlag(userID, companyCD);
        }

        #endregion

        #region 获取部门角色信息

        /// <summary>
        /// 获取部门角色信息
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 部门角色信息</returns>
        public static int[] GetRoleInfo(string userID, string companyCD)
        {
            //定义返回的角色数组
            int[] role = new int[0];
            //获取角色信息
            DataTable dtRole = LoginDBHelper.GetRoleInfo(userID, companyCD);
            //当角色存在的时候，设置角色到数组中
            if (dtRole != null && dtRole.Rows.Count > 0)
            {
                //获取角色个数
                int roleCount = dtRole.Rows.Count;
                role = new int[roleCount];
                //遍历所有角色，并设值
                for (int i = 0; i < roleCount; i++)
                {
                    //设置角色
                    role[i] = (int)dtRole.Rows[0]["RoleID"];
                }
            }
            return role;
        }

        #endregion

        /// <summary>
        /// 读取企业的USBKEY列表
        /// </summary>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static DataTable GetUSBKEYListByCompnayCD(string companyCD)
        {
            return LoginDBHelper.GetUSBKEYListByCompnayCD(companyCD);
        }
    }
}
