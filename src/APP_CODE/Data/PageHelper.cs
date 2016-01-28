/***********************************************************************
 * 
 * Module Name:APP_CODE.Common.XBase.Data.PageHelper
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2008-12-29 00:00:00
 * End Date:
 * Description:
 * Version History:
 ***********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
namespace XBase.Data
{
    public class PageHelper
    {

        /// <summary>
        /// 根据Sql语句返回结果集
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        public static DataTable PageExecSql(string sql)
        {
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 根据条件查询结果集、
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="GetFields">列数</param>
        /// <param name="OrderFieldName">排序的列名</param>
        /// <param name="PageSize">页显示的条数</param>
        /// <param name="PageIndex">也索引</param>
        /// <param name="docount">记录总数</param>
        /// <param name="OrderType">排序类型</param>
        /// <param name="Where">查询条件</param>
        /// <returns>DataTable</returns>
        public static int GetCount(string tablename, string strwhere, bool docount)
        {
            
            string sql = "";
            if (docount)
            {
                if (strwhere != "" && strwhere!=null)
                {
                    sql = "select count(*) as Total from " + tablename + "  where "+strwhere+" ";
                }
                else
                {
                    sql = "select count(*) as Total from " + tablename + "  ";
                }
            }
            string stringconnection = "server=(local);uid=sa;pwd=123;database=XGBOSS";
            SqlConnection conn = new SqlConnection(stringconnection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);


           // SqlHelper.Result.RecordCount;
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        /// <summary>
        /// 读取分页信息
        /// </summary>
        /// <param name="docount"></param>
        /// <param name="tablename">表名</param>
        /// <param name="getgetfields">获取显示字段</param>
        /// <param name="ordertype">排序类型</param>
        /// <param name="orderfieldname">排序字段</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="pagesize">页显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns></returns>
        public static DataTable GetDataReader(bool docount, string tablename, string getgetfields,
            bool ordertype, string orderfieldname, int pageindex, int pagesize, string strwhere)
        {
            string sql = "";
            string sqltem = "";
            string Order = "";
            if (ordertype)
            {
                sqltem = " <(select min ";
                Order = "order by   " + orderfieldname + "  desc";
            }
            else
            {
                sqltem = " >(select max ";
                Order = "order by  " + orderfieldname + "   asc";
            }
            if (pageindex == 1)
            {
                if (strwhere != "" && strwhere!=null)
                {
                    sql = "select top  " + pagesize + "  " + getgetfields + "  from " + tablename + "  where  " + strwhere + "  " + Order + " ";
                }
                else
                {
                    sql = "select top " + pagesize + " " + getgetfields + "  from " + tablename + " " + Order + " ";
                }
            }
            else
            {
                if (strwhere != "" && strwhere!=null)
                {
                    sql = "select top " + pagesize + " " + getgetfields + "   from  " + tablename + " where " + orderfieldname + " " +
                          " " + sqltem + "( " + orderfieldname + ") from (select    top  ((" + pageindex + "-1)*" + pagesize + ") " + orderfieldname + " " +
                          " from " + tablename + "  where  " + strwhere + " " + Order + ") as tblTmp) and " + strwhere + "  " + Order + " ";
                }
                else
                {
                    sql = "select top  " + pagesize + "  "+getgetfields+" from " + tablename + " where " + orderfieldname + "   " + sqltem + " (" + orderfieldname + ") " +
                    "from (select top  ((" + pageindex + "-1)*" + pagesize + ") " + orderfieldname + " from " + tablename + " " + Order + "  )" +
                    "as tblTmp)" + Order + " ";
                }
            }
            return  SqlHelper.ExecuteSql(sql);
        }
    }
}
