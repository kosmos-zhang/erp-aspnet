using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Caching;
using XBase.Data.DBHelper;

namespace XBase.Data.Office.SystemManager
{
    public class RoleInfoHelper
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetRoleInfo(string CompanyCD)
        {
            string sql = "select * from dbo.RoleInfo where CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 删除角色记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static void DeleteRoleInfo(string RoleIDArray)
        {
            string sql = "delete from dbo.RoleInfo where RoleID in(" + RoleIDArray + ")";
            SqlHelper.ExecuteTransSql(sql);
        }

    }
}
