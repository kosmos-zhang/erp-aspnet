/**********************************************
 * 类作用：   企业与模块关联数据库层处理
 * 建立人：   吴成好
 * 建立时间： 2009/01/22
 ***********************************************/

using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Model.SystemManager;

namespace XBase.Data.SystemManager
{
    public class CompanyModuleDBHelper
    {
        /// <summary>
        /// 增加企业的功能模块
        /// </summary>
        /// <param name="sql"> 执行sql语句数组</param>
        /// <returns>True 成功，False 失败</returns>
        public static bool AddCompanyModule(string companyCD, string[] module, string loginUserID)
        {
            //执行SQL拼写
            string[] executeSQL = new string[module.Length + 1];
            //删除SQL拼写
            string deleteSQL = "DELETE FROM pubdba.CompanyModule WHERE companyCD = '" + companyCD + "'";
            //追加SQL拼写
            string insertSQL1 = "INSERT INTO pubdba.CompanyModule(CompanyCD, ModuleID, OrderNO, ModifiedDate, ModifiedUserID) VALUES('" + companyCD + "'";
            string insertSQL2 = ",1,getdate(),'" + loginUserID + "');";
            executeSQL[0] = deleteSQL;
            int j = 1;
            for (int i = 0; i < module.Length; i++)
            {
                if (module[i] != "" && module[i]!=null)
                {
                    executeSQL[j] = insertSQL1 + ",'" + module[i] + "'" + insertSQL2;
                    j++;
                }                
            }
            return SqlHelper.ExecuteTransForListWithSQL(executeSQL);
        }

        /// <summary>
        /// 根据企业编码获取企业功能模块信息
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetCompanyModuleInfo(string CompanyCD)
        {
            string sql = "select ID,CompanyCD,OrderNO,ModuleID,ModuleName,ParentID,ModuleType from V_CompanyModule where CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 所有功能模块信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetSysModuleInfo()
        {
            string sql = "select ModuleID,ModuleName,ParentID,ModuleType,PropertyType,PropertyValue from SysModule";
            return SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 删除企业的功能模块
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static int DelCompanyModule(CompanyModuleModel Model)
        {
            string sql = "Delete from pubdba.CompanyModule where CompanyCD=@CompanyCD and ModuleID=@ModuleID";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
            parms[1] = SqlHelper.GetParameter("@ModuleID", Model.ModuleID);
            return SqlHelper.ExecuteTransSql(sql, parms);
        }

    }
}
