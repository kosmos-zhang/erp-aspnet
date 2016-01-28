/**********************************************
 * 类作用：   更改数据库连接信息接口
 * 建立人：   吴志强
 * 建立时间： 2008/12/30
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Data.DBHelper.Utility
{
    /// <summary>
    /// 连接助手接口
    /// </summary>
    public interface IConnectAssist
    {
        /// <summary>
        /// 更改服务器实例
        /// </summary>
        /// <param name="server">服务器名</param>
        void ChangeServer(string server);
        /// <summary>
        /// 更改连接的数据库
        /// </summary>
        /// <param name="database">数据库名</param>
        void ChangeDatabase(string database);
        /// <summary>
        /// 更改登录用户名
        /// </summary>
        /// <param name="user">用户名</param>
        void ChangeUser(string user);
        /// <summary>
        /// 更改登录密码
        /// </summary>
        /// <param name="password">密码</param>
        void ChangePassword(string password);
        /// <summary>
        /// 重置连接字符串为默认配置
        /// </summary>
        void ReSet();
        /// <summary>
        /// 重置连接字符串为指定字符串
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        void ReSet(string connectionString);
    }
}
