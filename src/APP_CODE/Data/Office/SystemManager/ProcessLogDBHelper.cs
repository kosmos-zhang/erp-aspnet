using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
namespace XBase.Data.Office.SystemManager
{
   public class ProcessLogDBHelper
    {
       public static DataTable SearchLog(string userid, string starttime, string endtime, string mod, string objid, string companycd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
       {
           string alluserid = "";
           string allmod = "";
           StringBuilder sb = new System.Text.StringBuilder();
           string[] IdS = null;
           string[] Idsm = null;
           mod=mod.Substring(0,mod.Length);
           userid = userid.Substring(0, userid.Length);
           IdS = userid.Split(',');
           Idsm = mod.Split(',');
           for (int i = 0; i < IdS.Length; i++)
           {
               IdS[i] = "'" + IdS[i] + "'";
               sb.Append(IdS[i]);
           }
           StringBuilder sbm = new System.Text.StringBuilder();
           for (int i = 0; i < Idsm.Length; i++)
           {
               Idsm[i] = "'" + Idsm[i] + "'";
               sbm.Append(Idsm[i]);
           }
           alluserid = sb.ToString().Replace("''", "','");
           allmod = sbm.ToString().Replace("''", "','");
           #region 查询SQL拼写
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select a.id,a.UserID,isnull( CONVERT(CHAR(19), OperateDate, 120),'') as OperateDate,isnull(b.ModuleName,'')as ModuleID,isnull(a.ObjectID,'')as ObjectID ,isnull(a.ObjectName,'') as ObjectName,isnull(a.Element,'')as Element ,isnull(a.Remark,'')as Remark  ");
           searchSql.AppendLine("      from officedba.ProcessLog as a left join pubdba.SysModule as b on a.ModuleID=b.ModuleID           ");
           searchSql.AppendLine(" WHERE                                                           ");
           searchSql.AppendLine(" 	a.CompanyCD = @CompanyCD  ");
           #endregion
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companycd));

           //用户编码
           if (!string.IsNullOrEmpty(userid))
           {
               searchSql.AppendLine("	AND a.UserID in  (" + alluserid + ") ");
           }
           //模块名称
           if (!string.IsNullOrEmpty(mod))
           {
               searchSql.AppendLine("	AND b.ModuleName in  (" + allmod + ") ");
           }
           if (!string.IsNullOrEmpty(objid))
           {
               searchSql.AppendLine("	AND A.ObjectID LIKE @ObjectID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ObjectID", "%" + objid + "%"));
           }
           if (!string.IsNullOrEmpty(starttime))
           {
               searchSql.AppendLine("	AND a.OperateDate >= @starttime  ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@starttime", starttime));
           }
           if (!string.IsNullOrEmpty(endtime))
           {
               searchSql.AppendLine("	AND a.OperateDate <= @endtime  ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@endtime", endtime + "  23:59:59"));
           }
           ////时间
           //if (!string.IsNullOrEmpty(starttime) || !string.IsNullOrEmpty(endtime))
           //{
           //    DateTime StartTime = DateTime.Parse(starttime);
           //    if (starttime.Equals(endtime))
           //    {
           //        DateTime EndTime = StartTime.AddDays(1);
           //        endtime = Convert.ToString(EndTime);
           //        searchSql.AppendLine("	AND a.OperateDate >= @starttime  ");
           //        comm.Parameters.Add(SqlHelper.GetParameterFromString("@starttime", starttime));
           //        searchSql.AppendLine("	AND a.OperateDate <= @endtime  ");
           //        comm.Parameters.Add(SqlHelper.GetParameterFromString("@endtime", endtime));
           //    }
           //    else
           //    {
           //        if (!string.IsNullOrEmpty(endtime))
           //        {
           //         DateTime Endtime = DateTime.Parse(endtime);
           //        DateTime EndTime = Endtime.AddDays(1);
           //        endtime = Convert.ToString(EndTime);
           //        searchSql.AppendLine("	AND a.OperateDate<=@endtime");
           //        comm.Parameters.Add(SqlHelper.GetParameterFromString("@endtime", endtime));
           //        }
           //        else if (!string.IsNullOrEmpty(starttime))
           //        {
           //            searchSql.AppendLine("	AND a.OperateDate >= @starttime ");
           //            comm.Parameters.Add(SqlHelper.GetParameterFromString("@starttime", starttime));
           //        }
           //        //DateTime Endtime = DateTime.Parse(endtime);
           //        //DateTime EndTime = Endtime.AddDays(1);
           //        //endtime = Convert.ToString(EndTime);
                  
                  
                  
           //    }
           //}
          

           comm.CommandText = searchSql.ToString();
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
       }
    }
}
