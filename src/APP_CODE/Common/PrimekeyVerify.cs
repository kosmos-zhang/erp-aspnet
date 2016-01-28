/**********************************************
 * 作    者： 陶春
 * 创建日期： 2009.03.02
 * 描    述： 字段唯一性验证
 * 修改日期： 2009.03.02
 * 版    本： 0.1.0
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Common
{
    public class PrimekeyVerify
    {
          //string select = "SELECT COUNT(*) AS UserCount FROM [Xgoss].[officedba].[UserInfo] WHERE CompanyCD = @CompanyCD";
          //  //设置参数
          //  SqlParameter[] param = new SqlParameter[1];
          //  param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

          //  DataTable userCount = SqlHelper.ExecuteSql(select, param);
          //  if (userCount != null && userCount.Rows.Count > 0)
          //  {
          //      return (int)userCount.Rows[0][0];
          //  }
          //  else
          //  {
          //      return 0;
          //  }
       /// <summary>
        ///  主键重复字段验证
       /// </summary>
       /// <param name="tablename">表名</param>
       /// <param name="arr">需要验证的数据库字段（数据库字段）数组</param>
       /// <param name="queryarr">验证字段如输入文本等的值</param>
       /// <returns></returns>

        public static bool PrimekeyVerifytc(string tablename, string[] arr, string[] queryarr)
        {
            int result = 0;
            DataTable dt;
            bool flag = false;
            if(arr.Length ==1&&(arr.Length !=0))///一个参数
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@queryarr", queryarr[0]);
                string sql = "select count(*) from " + tablename + " where " + arr[0] + "=@queryarr";
                dt = SqlHelper.ExecuteSql(sql, param);
                if (Convert.ToInt32(dt.Rows[0][0])>0)
                {
                    return !flag;
                }
                else
                {
                    return flag;
                }
            }
            else if (arr.Length > 1)///两个参数
             {
                for (int n = 0; n < arr.Length-1; n++)
                    {
                        string sql = "select count(*) from " + tablename + " where " + arr[n] + "='" + queryarr[n] + "'" + " and " + arr[n + 1] + " ='" + queryarr[n + 1] + "'";
                        dt = SqlHelper .ExecuteSql(sql);
                        if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                        {
                            return !flag;
                        }
                        else
                        {
                            return flag;
                        }
                    }
               }
            return flag;
        }
    }
}
