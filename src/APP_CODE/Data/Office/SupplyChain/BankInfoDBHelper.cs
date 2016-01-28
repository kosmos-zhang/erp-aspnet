/**********************************************
 * 类作用：   供应链设置
 * 建立人：   陶春
 * 建立时间： 2009/4/23
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SupplyChain;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Data;
using XBase.Common;
using System.Data.SqlTypes;
using System.Collections;
namespace XBase.Data.Office.SupplyChain
{
   public class BankInfoDBHelper
    {
        /// <summary>
       /// 插入银行分类
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        public static bool InsertBankInfo(BankInfoModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO [officedba].[BankInfo]");   
            sql.AppendLine("           (CompanyCD                   ");     
            sql.AppendLine("           ,BigType                     ");     
            sql.AppendLine("           ,CustNo                      ");     
            sql.AppendLine("           ,CustName                   ");      
            sql.AppendLine("           ,CustNam                     ");     
            sql.AppendLine("           ,PYShort                     ");     
            sql.AppendLine("           ,ContactName                  ");    
            sql.AppendLine("           ,Tel                          ");    
            sql.AppendLine("           ,Fax                          ");    
            sql.AppendLine("           ,Mobile                       ");    
            sql.AppendLine("           ,Addr                         ");    
            sql.AppendLine("           ,Remark                      ");     
            sql.AppendLine("           ,UsedStatus                  ");     
            sql.AppendLine("           ,Creator                      ");    
            sql.AppendLine("           ,CreateDate                   ");    
            sql.AppendLine("           ,ModifiedDate                 ");    
            sql.AppendLine("           ,ModifiedUserID)              ");    
            sql.AppendLine("     VALUES                                ");  
            sql.AppendLine("           (@CompanyCD       ");                 
            sql.AppendLine("           ,@BigType           ");               
            sql.AppendLine("           ,@CustNo         ");                  
            sql.AppendLine("           ,@CustName      ");                   
            sql.AppendLine("           ,@CustNam        ");                  
            sql.AppendLine("           ,@PYShort       ");                   
            sql.AppendLine("           ,@ContactName    ");                  
            sql.AppendLine("           ,@Tel           ");                   
            sql.AppendLine("           ,@Fax           ");                   
            sql.AppendLine("           ,@Mobile        ");                   
            sql.AppendLine("           ,@Addr          ");                   
            sql.AppendLine("           ,@Remark        ");                   
            sql.AppendLine("           ,@UsedStatus        ");               
            sql.AppendLine("           ,@Creator                ");          
            sql.AppendLine("           ,@CreateDate        ");               
            sql.AppendLine("           ,@ModifiedDate     ");                
            sql.AppendLine("           ,@ModifiedUserID)");                  

            //设置参数
            SqlParameter[] param = new SqlParameter[17];
            int i = 0;
            param[i++] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[i++] = SqlHelper.GetParameter("@BigType", ConstUtil.BankTypeFlag);
            param[i++] = SqlHelper.GetParameter("@CustNo", model.CustNo);
            param[i++] = SqlHelper.GetParameter("@CustName", model.CustName);
            param[i++] = SqlHelper.GetParameter("@CustNam", model.CustNam);
            param[i++] = SqlHelper.GetParameter("@PYShort", model.PYShort);
            param[i++] = SqlHelper.GetParameter("@ContactName",model.ContactName );
            param[i++] = SqlHelper.GetParameter("@Tel", model.Tel);
           param[i++] = SqlHelper.GetParameter("@Fax", model.Fax);
           param[i++] = SqlHelper.GetParameter("@Mobile", model.Mobile);
           param[i++] = SqlHelper.GetParameter("@Addr", model.Addr);
           param[i++] = SqlHelper.GetParameter("@Remark", model.Remark);
           param[i++] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
           param[i++] = SqlHelper.GetParameter("@Creator", model.Creator == null
                                           ? SqlInt32.Null
                                           : SqlInt32.Parse(model.Creator.ToString()));
           param[i++] = SqlHelper.GetParameter("@CreateDate", model.CreateDate == null
                                         ? SqlDateTime.Null
                                         : SqlDateTime.Parse(model.CreateDate.ToString()));
           param[i++] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
            param[i++] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        /// <summary>
        /// 修改银行信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateBankInfo(BankInfoModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE [officedba].[BankInfo]       ");  
            sql.AppendLine("     SET                                    ");  
            sql.AppendLine("       CustName =   @CustName               ");  
            sql.AppendLine("      ,CustNam =     @CustNam               ");  
            sql.AppendLine("      ,PYShort =     @PYShort               ");  
            sql.AppendLine("      ,ContactName = @ContactName           ");  
            sql.AppendLine("      ,Tel =         @Tel                   ");  
            sql.AppendLine("      ,Fax =         @Fax                   ");  
            sql.AppendLine("      ,Mobile =      @Mobile                ");  
            sql.AppendLine("      ,Addr =        @Addr                  ");  
            sql.AppendLine("      ,Remark =      @Remark                ");  
            sql.AppendLine("      ,UsedStatus =  @UsedStatus            ");  
            sql.AppendLine("      ,Creator =     @Creator               ");  
            sql.AppendLine("      ,CreateDate =  @CreateDate            ");  
            sql.AppendLine("      ,ModifiedDate = @ModifiedDate        ");    
            sql.AppendLine("      ,ModifiedUserID =@ModifiedUserID ");       
            sql.AppendLine(" WHERE   ID=@ID and CompanyCD=@CompanyCD    "); 
            //设置参数
            SqlParameter[] param = new SqlParameter[16];
            int i = 0;
            param[i++] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[i++] = SqlHelper.GetParameter("@ID", model.ID);
            param[i++] = SqlHelper.GetParameter("@CustNam", model.CustNam);
            param[i++] = SqlHelper.GetParameter("@CustName", model.CustName);
            param[i++] = SqlHelper.GetParameter("@PYShort", model.PYShort);
            param[i++] = SqlHelper.GetParameter("@ContactName", model.ContactName);
             param[i++] = SqlHelper.GetParameter("@Tel", model.Tel);
             param[i++] = SqlHelper.GetParameter("@Fax", model.Fax);
             param[i++] = SqlHelper.GetParameter("@Mobile", model.Mobile);
             param[i++] = SqlHelper.GetParameter("@Addr", model.Addr);
             param[i++] = SqlHelper.GetParameter("@Remark", model.Remark);
             param[i++] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[i++] = SqlHelper.GetParameter("@Creator", model.Creator);
              param[i++] = SqlHelper.GetParameter("@CreateDate", model.CreateDate);
            param[i++] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
            param[i++] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }

        /// <summary>
        /// 获取银行信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBankInfo(BankInfoModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            StringBuilder sql = new StringBuilder();
             sql.AppendLine("SELECT a.ID                              "); 
           sql.AppendLine("      ,a.CustNo                            ");
           sql.AppendLine("      ,isnull(a.CustName,'')as  CustName   ");
           sql.AppendLine("      ,isnull(a.CustNam,'') as  CustNam    "); 
           sql.AppendLine("      ,a.PYShort                           "); 
           sql.AppendLine("      ,a.ContactName                       "); 
           sql.AppendLine("      ,a.Tel                               "); 
           sql.AppendLine("      ,a.Fax                               "); 
           sql.AppendLine("      ,a.Mobile                            "); 
           sql.AppendLine("      ,a.Addr                              "); 
           sql.AppendLine("      ,a.Remark                            "); 
           sql.AppendLine("      ,a.UsedStatus                        ");
           sql.AppendLine("      ,isnull(a.Creator,'') as Creator        ");
           sql.AppendLine("      ,isnull(b.EmployeeName,'') as  CreaterName      ");
           sql.AppendLine("      ,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate                        "); 
           sql.AppendLine("      ,a.ModifiedDate                      "); 
           sql.AppendLine("      ,a.ModifiedUserID                    "); 
           sql.AppendLine("  FROM officedba.BankInfo as a left join officedba.EmployeeInfo as b on a.Creator =b.ID where a.CompanyCD=@CompanyCD   "); 
            if (!string.IsNullOrEmpty(model.CustNo))
            {
                sql.AppendLine("     and a.CustNo LIKE @CustNo                    "); 
            }
             if (!string.IsNullOrEmpty(model.CustName))
            {
                sql.AppendLine("     and a.CustName LIKE @CustName                    ");
            }
             if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                sql.AppendLine("     and a.UsedStatus =@UsedStatus                   ");
            }
             if (!string.IsNullOrEmpty(model.ContactName))
            {
                sql.AppendLine("     and a.ContactName LIKE @ContactName                    ");
            }
             if (!string.IsNullOrEmpty(model.PYShort))
            {
                sql.AppendLine("     and a.PYShort like @PYShort                   ");
            }
            SqlParameter[] param = new SqlParameter[6];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@CustNo", "%" + model.CustNo + "%");
            param[2] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[3] = SqlHelper.GetParameter("@CustName", "%" + model.CustName + "%");
            param[4] = SqlHelper.GetParameter("@ContactName", "%" + model.ContactName + "%");
            param[5] = SqlHelper.GetParameter("@PYShort", "%" + model.PYShort + "%");
            DataTable dt = SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, OrderBy, param, ref totalCount);
            return dt;
        }
       /// <summary>
       /// 删除银行信息
       /// </summary>
       /// <param name="TypeFlag"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
        public static bool DeleteBankInfo(string  ID, string CompanyCD)
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
                Delsql[0] = "delete from  officedba.BankInfo where ID IN (" + allID + ") and CompanyCD =@CompanyCD";
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

    }

    
}
