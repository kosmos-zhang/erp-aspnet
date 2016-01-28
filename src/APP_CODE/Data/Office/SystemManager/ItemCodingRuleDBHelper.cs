using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SystemManager;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Collections;
namespace XBase.Data.Office.SystemManager
{
   public class ItemCodingRuleDBHelper
    {
        /// <summary>
        /// 插入编码规则
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static bool InsertItemCodingRule(ItemCodingRuleModel model)
        {
          
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.ItemCodingRule");
            sql.AppendLine("    (CompanyCD                         ");    
            sql.AppendLine("    ,CodingType                        ");    
            sql.AppendLine("    ,ItemTypeID                        ");    
            sql.AppendLine("    ,RuleName                          ");    
            sql.AppendLine("    ,RulePrefix                        ");    
            sql.AppendLine("    ,RuleDateType                      ");    
            sql.AppendLine("                  ,RuleNoLen           ");    
            sql.AppendLine("                  ,LastNo              ");    
            sql.AppendLine("                  ,RuleExample         ");    
            sql.AppendLine("                  ,IsDefault           ");    
            sql.AppendLine("                  ,Remark              ");    
            sql.AppendLine("                  ,UsedStatus          ");    
            sql.AppendLine("           ,ModifiedDate               ");    
            sql.AppendLine("           ,ModifiedUserID)            ");    
            sql.AppendLine("     VALUES                            ");  
            sql.AppendLine("           (@CompanyCD     ");                
            sql.AppendLine("           ,@CodingType       ");             
            sql.AppendLine("           ,@ItemTypeID           ");         
            sql.AppendLine("           ,@RuleName    ");                  
            sql.AppendLine("           ,@RulePrefix   ");                 
            sql.AppendLine("           ,@RuleDateType  ");                
            sql.AppendLine("           ,@RuleNoLen            ");         
            sql.AppendLine("           ,@LastNo              ");          
            sql.AppendLine("           ,@RuleExample  ");                 
            sql.AppendLine("           ,@IsDefault       ");              
            sql.AppendLine("           ,@Remark     ");                   
            sql.AppendLine("           ,@UsedStatus       ");             
            sql.AppendLine("           ,@ModifiedDate    ");              
            sql.AppendLine("           ,@ModifiedUserID) ");
        //设置参数
        SqlCommand comm = new SqlCommand();
        comm.CommandText = sql.ToString();

        //设置参数
        comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
        comm.Parameters.Add(SqlHelper.GetParameter("@CodingType", model.CodingType));
        comm.Parameters.Add(SqlHelper.GetParameter("@ItemTypeID", model.ItemTypeID));
        comm.Parameters.Add(SqlHelper.GetParameter("@RuleName", model.RuleName));
        comm.Parameters.Add(SqlHelper.GetParameter("@RulePrefix", model.RulePrefix));
        comm.Parameters.Add(SqlHelper.GetParameter("@RuleDateType", model.RuleDateType));
        comm.Parameters.Add(SqlHelper.GetParameter("@RuleNoLen", model.RuleNoLen));
        comm.Parameters.Add(SqlHelper.GetParameter("@LastNo", model.LastNo));
        comm.Parameters.Add(SqlHelper.GetParameter("@RuleExample", model.RuleExample));
        comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
        comm.Parameters.Add(SqlHelper.GetParameter("@IsDefault", model.IsDefault));
        comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
        comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now));
        comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
        ArrayList listadd= new ArrayList();
        //删除计提信息
        Updateinfo(listadd, model.CodingType,model.ItemTypeID,model.IsDefault, model.CompanyCD);
        listadd.Add(comm);
        return SqlHelper.ExecuteTransWithArrayList(listadd);
        }




       private static void Updateinfo(ArrayList list, string codetype, int typeid, string isdefault, string companycd)
       {
           if (isdefault == "1")
           {
               StringBuilder sql_update = new StringBuilder();
               sql_update.AppendLine("UPDATE [officedba].[ItemCodingRule] set IsDefault='0' where ItemTypeID =@ItemTypeID and CodingType=@CodingType and CompanyCD=@CompanyCD");
               //定义更新基本信息的命令
               SqlCommand comm = new SqlCommand();
               comm.CommandText = sql_update.ToString();
               comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companycd));
               comm.Parameters.Add(SqlHelper.GetParameter("@CodingType", codetype));
               comm.Parameters.Add(SqlHelper.GetParameter("@ItemTypeID", typeid));
               list.Add(comm);
           }
       }
        ///// <summary>
        ///// 修改编码规则
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
       public static bool UpdateItemCodingRule(ItemCodingRuleModel model)
        {
         StringBuilder sql = new StringBuilder();
         sql.AppendLine("UPDATE [officedba].[ItemCodingRule]    " );
         sql.AppendLine("   SET          ");        
         sql.AppendLine("       ItemTypeID = @ItemTypeID              "); 
         sql.AppendLine("      ,RuleName = @RuleName         ");          
         sql.AppendLine("      ,RulePrefix = @RulePrefix      ");         
         sql.AppendLine("      ,RuleDateType = @RuleDateType   ");        
         sql.AppendLine("      ,RuleNoLen = @RuleNoLen                "); 
         sql.AppendLine("      ,RuleExample = @RuleExample    ");         
         sql.AppendLine("      ,IsDefault = @IsDefault            ");     
         sql.AppendLine("      ,Remark = @Remark           ");            
         sql.AppendLine("      ,UsedStatus = @UsedStatus          ");     
         sql.AppendLine("      ,ModifiedDate = @ModifiedDate    ");       
         sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID ");      
         sql.AppendLine(" WHERE ID=@ID                        ");

         SqlCommand comm = new SqlCommand();
         comm.CommandText = sql.ToString();

         //设置参数
         comm.Parameters.Add(SqlHelper.GetParameter("@ItemTypeID", model.ItemTypeID));
         comm.Parameters.Add(SqlHelper.GetParameter("@RuleName", model.RuleName));
         comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
         comm.Parameters.Add(SqlHelper.GetParameter("@RulePrefix", model.RulePrefix));
         comm.Parameters.Add(SqlHelper.GetParameter("@RuleDateType", model.RuleDateType));
         comm.Parameters.Add(SqlHelper.GetParameter("@RuleNoLen", model.RuleNoLen));
         comm.Parameters.Add(SqlHelper.GetParameter("@RuleExample", model.RuleExample));
         comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
         comm.Parameters.Add(SqlHelper.GetParameter("@IsDefault", model.IsDefault));
         comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
         comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now));
         comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
         //SqlHelper.ExecuteTransSql(sql.ToString(), param);
         //return SqlHelper.Result.OprateCount > 0 ? true : false;




         ArrayList listadd = new ArrayList();
         //删除计提信息
         Updateinfo(listadd, model.CodingType, model.ItemTypeID, model.IsDefault, model.CompanyCD);
         listadd.Add(comm);
         return SqlHelper.ExecuteTransWithArrayList(listadd);

        }


       /// <summary>
       /// 获取编码规则信息
       /// </summary>
       /// <returns></returns>
       public static DataTable GetItemCodingRule(string TypeFlag, string CompanyCD, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
       {
           string sql = "select a.ID,a.ItemTypeID as typeid,a.UsedStatus as status,a.IsDefault as fault,a.CodingType,b.TypeName as ItemTypeID,a.RuleName,isnull(a.RulePrefix,'')as RulePrefix,isnull(a.RuleDateType,'')as RuleDateType,";
           sql += " isnull(a.RuleNoLen,'')as RuleNoLen,isnull(a.LastNo,'')as LastNo,isnull(a.RuleExample,'')as RuleExample,case when IsDefault='0'then '否' else '是'end as IsDefault";
           sql += ",isnull(a.Remark,'')as Remark,case when a.UsedStatus='0'then '停用' else '启用'end as UsedStatus,isnull(a.ModifiedDate,'')as ModifiedDate,isnull(a.ModifiedUserID,'') as ModifiedUserID from ";
           sql += "officedba.ItemCodingRule as a,pubdba.BillType as b where a.CodingType=@CodingType and a.CompanyCD=@CompanyCD and b.TypeCode=a.ItemTypeID and b.TypeFlag=@CodingType and b.typelabel='0'";
           SqlParameter[] param = new SqlParameter[2];
           param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           param[1] = SqlHelper.GetParameter("@CodingType", TypeFlag);
           DataTable dt = SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, OrderBy, param, ref totalCount);
           return dt;
       }

       /// <summary>
       /// 删除编码规则信息
       /// </summary>
       /// <param name="TypeFlag"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static bool DeleteItemCodingRule(string ID, string CompanyCD)
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
               //allUserID = sb.ToString();
               allID = sb.ToString().Replace("''", "','");
               Delsql[0] = "delete from  officedba.ItemCodingRule where ID IN (" + allID + ") and CompanyCD = @CompanyCD ";
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
       /// 验证编号
       /// </summary>
       /// <param name="RuleName"></param>
       /// <param name="CodingType"></param>
       /// <param name="ItemTypeID"></param>
       /// <param name="companyCD"></param>
       /// <returns></returns>
       public static bool CheckCodeUniq(string RuleName, string CodingType, string ItemTypeID, string companyCD)
       {
           string checkSql = " SELECT RuleName FROM officedba.ItemCodingRule WHERE CompanyCD = @CompanyCD AND RuleName = @RuleName and CodingType=@CodingType and ItemTypeID=@ItemTypeID ";

           //设置参数
           SqlParameter[] param = new SqlParameter[4];
           int i = 0;
           //公司代码
           param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
           //编码类型
           param[i++] = SqlHelper.GetParameter("@RuleName", RuleName);
           param[i++] = SqlHelper.GetParameter("@CodingType", CodingType);
           param[i++] = SqlHelper.GetParameter("@ItemTypeID", ItemTypeID);
           //校验存在性
           DataTable data = SqlHelper.ExecuteSql(checkSql, param);
           //数据不存在时，返回true
           if (data == null || data.Rows.Count < 1)
           {
               return true;
           }
           //数据存在时，返回false
           else
           {
               return false;
           }
       }
       /// <summary>
       /// 验证实例
       /// </summary>
       /// <param name="RuleName"></param>
       /// <param name="CodingType"></param>
       /// <param name="ItemTypeID"></param>
       /// <param name="companyCD"></param>
       /// <returns></returns>
       public static bool CheckCodeRuleExample(string RuleName, string CodingType, string ItemTypeID, string companyCD)
       {
           string checkSql = " SELECT RuleExample FROM officedba.ItemCodingRule WHERE CompanyCD = @CompanyCD  and CodingType=@CodingType and ItemTypeID=@ItemTypeID and RuleExample=@RuleExample ";

           //设置参数
           SqlParameter[] param = new SqlParameter[4];
           int i = 0;
           //公司代码
           param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
           //编码类型
           param[i++] = SqlHelper.GetParameter("@RuleExample", RuleName);
           param[i++] = SqlHelper.GetParameter("@CodingType", CodingType);
           param[i++] = SqlHelper.GetParameter("@ItemTypeID", ItemTypeID);
           //校验存在性
           DataTable data = SqlHelper.ExecuteSql(checkSql, param);
           //数据不存在时，返回true
           if (data == null || data.Rows.Count < 1)
           {
               return true;
           }
           //数据存在时，返回false
           else
           {
               return false;
           }
       }
    }
}
