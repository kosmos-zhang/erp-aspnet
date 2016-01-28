using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;

namespace XBase.Data.Personal
{
    /// <![CDATA[
    /// 提供个人桌面数据层类
    /// ]]>

    public class Commission
    {
        #region
        /// <![CDATA[
         /// 首界面代办任务
        /// <param name="UserID">用户ID</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="LookNumber">显示条数</param>
        /// <returns>返回代办任务</returns>
        /// ]]>
        public static DataTable CommissionTask(string UserID, string CompanyCD, int LookNumber)
        {
            StringBuilder SearchSql = new StringBuilder();


            return null;

        }
      #endregion


        #region
        /// <![CDATA[
        /// 首界面未读信息
        /// <param name="UserID">用户ID</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="LookNumber">显示条数</param>
        /// <returns>返回未读短信</returns>
        /// ]]>
        public static DataTable CommissionNote(string UserID, string CompanyCD, int LookNumber)
        {
            return null;

        }
        #endregion

        #region
        /// <![CDATA[
        /// 首界面公告列表
        /// <param name="UserID">用户ID</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="LookNumber">显示条数</param>
        /// <returns>返回公告列表</returns>
        /// ]]>
        public static DataTable CommissionBulletin(string UserID, string CompanyCD, int LookNumber)
        {
            return null;

        }
        #endregion



    }
}
