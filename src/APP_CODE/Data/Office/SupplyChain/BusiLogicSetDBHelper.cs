using System;
using System.Collections.Generic;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using XBase.Model.Office.SupplyChain;
using System.Data.SqlClient;
namespace XBase.Data.Office.SupplyChain
{
   public class BusiLogicSetDBHelper
    {
       /// <summary>
       /// 获取业务规则
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="Name"></param>
       /// <param name="UsedStatus"></param>
       /// <returns></returns>
       public static DataTable GetBusiLogicSet(string CompanyCD, string Name)
       {
           string sql = "select ID, LogicID,LogicName,LogicSet,Description  from officedba.BusiLogicSet where 1=1";
           int i = 0;
           int j = 0;
           if (!string.IsNullOrEmpty(CompanyCD))
           {
               i++;
           }
           if (!string.IsNullOrEmpty(Name))
           {
               i++;
           }
      
           SqlParameter[] parms = new SqlParameter[i];
           if (!string.IsNullOrEmpty(CompanyCD))
           {
               sql += " and  CompanyCD=@CompanyCD";
               parms[j] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
               j++;
           }
           if (!string.IsNullOrEmpty(Name))
           {
               sql += "  and LogicName=@LogicName";
               parms[j] = SqlHelper.GetParameter("@LogicName", Name);
               j++;

           }
           return SqlHelper.ExecuteSql(sql, parms);
       }



  

       /// <summary>
       /// 修改业务规则
       /// </summary>
       /// <param name="Name">名称</param>
       /// <param name="UsedStatus">使用状态</param>
       /// <returns>true 成功,false 失败</returns>
       public static bool UpdateBusiLogic(string CompanyCD, int ID, string LogicSet)
       {
           string sql = "Update  officedba.BusiLogicSet set LogicSet=@LogicSet" +
               " where CompanyCD=@CompanyCD and ID=@ID";
           SqlParameter[] parms = new SqlParameter[3];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           parms[1] = SqlHelper.GetParameter("@ID", ID);
           parms[2] = SqlHelper.GetParameter("@LogicSet", LogicSet);
           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }


   
    }
}
