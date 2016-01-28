using System.Text;
using System.Data;
using System.Data.SqlClient;

using XBase.Data.DBHelper;

namespace XBase.Data.CustomAPI.CustomWebSite
{
    public class WebSiteUnitControlDBHelper
    {
        /*多单位标志*/
        private const string MULUNIT_KEY = "3";

        #region 判断是否启用多单位
        /// <summary>
        /// 判断是否启用多单位
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns>true：启用，false：不启用</returns>
        public static bool IsMulUnit(string CompanyCD)
        {
            DataTable dtInfo = Data.Office.SystemManager.ParameterSettingDBHelper.Get(CompanyCD, MULUNIT_KEY);
            if (dtInfo == null || dtInfo.Rows.Count <= 0)
                return false;
            else
            {
                if (dtInfo.Rows[0]["UsedStatus"] != null && dtInfo.Rows[0]["UsedStatus"].ToString() == "1")
                    return true;
                else
                    return false;
            }

        }
        #endregion

        #region 读取指定单位换算组
        /// <summary>
        /// 读取指定单位换算组明细
        /// </summary>
        /// <param name="GroupID">单位组主表ID</param>
        /// <returns>包含指定单位组的明细的数据表</returns>
        public static DataTable GetUnitGroup(string UnitGroupNo,string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            //sbSql.AppendLine(" SELECT a.ID,b.* FROM officedba.UintGroup AS a ");
            //sbSql.AppendLine(" LEFT JOIN officedba.UnitGroupDetail AS b ON a.GroupNo=b.GroupNo  ");
            //sbSql.AppendLine(" WHERE a.ID=@GroupID ");

            sbSql.AppendLine(" SELECT a.*,b.CodeName AS UnitName FROM officedba.UnitGroupDetail AS a");
            sbSql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS b ON b.ID=a.UnitID ");
            sbSql.AppendLine(" WHERE a.GroupUnitNo=@GroupUnitNo AND a.CompanyCD=@CompanyCD");

            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = SqlHelper.GetParameter("@GroupUnitNo", UnitGroupNo);
            Params[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);

        }
        #endregion
    }
}
