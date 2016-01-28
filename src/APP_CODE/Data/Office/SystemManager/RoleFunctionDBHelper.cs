/***********************************************************************
 * 
 * Module Name:XBase.Data.Office.SystemManager.RoleFunctionDBHelper.cs
 * Current Version: 1.0 
 * Creator: jiangym && edit by taochun 
 * Auditor:2009-01-10
 * End Date:
 * Description: 角色授权功能数据库层处理
 * Version History:
 ***********************************************************************/
using System;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using XBase.Model.Office.SystemManager;
using System.Collections;
namespace XBase.Data.Office.SystemManager
{
  public  class RoleFunctionDBHelper
    {
      /// <summary>
      /// 增加角色菜单功能
      /// </summary>
      /// <param name="sql"> 执行sql语句数组</param>
      /// <returns>True 成功，False 失败</returns>
      public static bool AddRoleFun(string []sql)
      {
          return SqlHelper.ExecuteTransForListWithSQL(sql);
      }


      /// <summary>
      /// 根据企业编码和角色编码获取角色信息
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <param name="RoleID">角色编码</param>
      /// <returns>DataTable</returns>
      public static DataTable GetRoleFunInfo(string CompanyCD, string RoleID)
      {
          string sql = "select CompanyCD,RoleID,ModuleID,FunctionID," +
                       "ModifiedDate,ModifiedUserID from officedba.RoleFunction where CompanyCD=@CompanyCD and RoleID=@RoleID";
          SqlParameter[] parms = new SqlParameter[2];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          parms[1] = SqlHelper.GetParameter("@RoleID", RoleID);
          return SqlHelper.ExecuteSql(sql,parms);
      }

      /// <summary>
      /// 根据企业编码和角色编码获取角色信息（包括操作名称和类型）
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <param name="RoleID">角色编码</param>
      /// <returns>DataTable</returns>
      public static DataTable GetRoleFunInfo2(string CompanyCD, string RoleID)
      {
          string sql = "select a.ModuleID,a.FunctionID,b.FunctionType,b.FunctionName";
          sql += " from officedba.RoleFunction as a left join pubdba.ModuleFunction as b";
          sql += " on (a.FunctionID=b.FunctionID AND a.ModuleID=b.ModuleID)";
          sql += " where CompanyCD=@CompanyCD and RoleID=@RoleID";

          SqlParameter[] parms = new SqlParameter[2];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          parms[1] = SqlHelper.GetParameter("@RoleID", RoleID);
          return SqlHelper.ExecuteSql(sql, parms);
      }


      /// <summary>
      /// 删除角色功能
      /// </summary>
      /// <param name="Model"></param>
      /// <returns></returns>
      public static int DelRoleFun(RoleFunModel Model)
      {
          string sql = "Delete from officedba.RoleFunction where CompanyCD=@CompanyCD  and RoleID=@RoleID " +
                       "and ModuleID=@ModuleID and FunctionID=@FunctionID ";
          SqlParameter[] parms = new SqlParameter[4];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
          parms[1] = SqlHelper.GetParameter("@RoleID", Model.RoleID);
          parms[2] = SqlHelper.GetParameter("@ModuleID", Model.ModuleID);
          parms[3] = SqlHelper.GetParameter("@FunctionID", Model.FunctionID);
          return SqlHelper.ExecuteTransSql(sql,parms);
      }

      public static int DelRoleFunInfo(RoleFunModel Model)
      {
          string sql = "Delete from officedba.RoleFunction where CompanyCD=@CompanyCD  and RoleID=@RoleID " +
                       "and ModuleID like @ModuleID ";
          SqlParameter[] parms = new SqlParameter[3];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
          parms[1] = SqlHelper.GetParameter("@RoleID", Model.RoleID);
          parms[2] = SqlHelper.GetParameter("@ModuleID", "%" + Model.ModuleID.ToString().Substring(0,1) + "%");
          return SqlHelper.ExecuteTransSql(sql, parms);
      }
      /// <summary>
      /// 删除角色功能
      /// </summary>
      /// <param name="Model"></param>
      /// <returns></returns>
      public static bool DelRoleFun(string CompanyCD, string ID)
      {
          string allID = "";
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          string[] Delsql = new string[1];
          try
          {
              string[] IdS = null;
              ID = ID.Substring(0, ID.Length);
              IdS = ID.Split(',');

              for (int i = 0; i < IdS.Length; i++)
              {
                  IdS[i] = "'" + IdS[i] + "'";
                  sb.Append(IdS[i]);
              }
              allID = sb.ToString().Replace("''", "','");
              Delsql[0] = "Delete from officedba.RoleFunction where ID IN (" + allID + ") and CompanyCD=@CompanyCD";
              SqlCommand comm = new SqlCommand();
              comm.CommandText = Delsql[0].ToString();
              //设置参数
              comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
              ArrayList lstDelete = new ArrayList();
              comm.CommandText = Delsql[0].ToString();
              //添加基本信息更新命令
              lstDelete.Add(comm);
              return SqlHelper.ExecuteTransWithArrayList(lstDelete);
          }
          catch (Exception ex)
          {
              throw ex;
          }



      }
      /// <summary>
      /// 根据角色编码获取权限分配信息
      /// </summary>
      /// <param name="RoleId"></param>
      /// <returns></returns>
      public static DataTable GetRoleFunction(int RoleId, string CompanyCD,int ID)
      {
          string sql = "select ModuleID,RoleID,FunctionCD,FunctionID from officedba.V_RoleFunction where RoleID=@RoleID and CompanyCD=@CompanyCD" +
              " and ID=@ID";
          SqlParameter[] parms = new SqlParameter[3];
          parms[0] = SqlHelper.GetParameter("@RoleID", RoleId);
          parms[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          parms[2] = SqlHelper.GetParameter("@ID", ID);
          return SqlHelper.ExecuteSql(sql, parms);
      }




        /*
         ALTER PROC [officedba].[RoleFunctionSetBatch] 
  (
  @companyCD VARCHAR(50),
  @roleID int,
  @funcList1 varchar(8000),
  @funcList2 varchar(8000),
  @funcList3 varchar(8000),
  @funcList4 varchar(8000)
  )
         */

      /// <summary>
      /// 
      /// </summary>
      /// <param name="companyCD"></param>
      /// <param name="roleID"></param>
      /// <param name="funcList1"></param>
      /// <param name="funcList2"></param>
      /// <param name="funcList3"></param>
      /// <param name="funcList4"></param>
      public static void SetFunctionBatch(string companyCD, int roleID, string funcList1, string funcList2, string funcList3, string funcList4)
      {
          SqlParameter[] parms = new SqlParameter[6];
          parms[0] = SqlHelper.GetParameter("@companyCD", companyCD);
          parms[1] = SqlHelper.GetParameter("@roleID", roleID);
          parms[2] = SqlHelper.GetParameter("@funcList1", funcList1);
          parms[3] = SqlHelper.GetParameter("@funcList2", funcList2);
          parms[4] = SqlHelper.GetParameter("@funcList3", funcList3);
          parms[5] = SqlHelper.GetParameter("@funcList4", funcList4);


          SqlHelper.ExecuteNonQuery("","[officedba].[RoleFunctionSetBatch]", parms);
      }

    }
}
