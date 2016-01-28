using System;
using System.Collections.Generic;
using System.Text;
using XBase.Model.Office.SystemManager;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Data.Office.SystemManager
{
   public class CodePublicTypeDBHelper
    {
       /// <summary>
       /// 插入公共分类
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        public static bool InsertCodePublicType(CodePublicTypeModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.CodePublicType");
            sql.AppendLine("           (CompanyCD                     ");
            sql.AppendLine("           ,TypeFlag                      ");
            sql.AppendLine("           ,TypeCode                      ");
            sql.AppendLine("           ,TypeName                      ");
            sql.AppendLine("           ,Description                   ");
            sql.AppendLine("           ,UsedStatus                    ");
            sql.AppendLine("           ,ModifiedDate                  ");
            sql.AppendLine("           ,ModifiedUserID)               ");
            sql.AppendLine("     VALUES                               ");
            sql.AppendLine("           (@CompanyCD                    ");
            sql.AppendLine("           ,@TypeFlag                     ");
            sql.AppendLine("           ,@TypeCode                     ");
            sql.AppendLine("           ,@TypeName                     ");
            sql.AppendLine("           ,@Description                  ");
            sql.AppendLine("           ,@UsedStatus                   ");
            sql.AppendLine("           ,@ModifiedDate                 ");
            sql.AppendLine("           ,@ModifiedUserID)               ");
            //设置参数
            SqlParameter[] param = new SqlParameter[8];
            int i = 0;
            param[i++] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[i++] = SqlHelper.GetParameter("@TypeFlag", model.TypeFlag);
            param[i++] = SqlHelper.GetParameter("@TypeCode", model.TypeCode);
            param[i++] = SqlHelper.GetParameter("@TypeName", model.TypeName);
            param[i++] = SqlHelper.GetParameter("@Description", model.Description);
            param[i++] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[i++] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
            param[i++] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        /// <summary>
        /// 修改公共分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateCodePublicType(CodePublicTypeModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.CodePublicType   ");
            sql.AppendLine("   SET                                   ");
            sql.AppendLine("       TypeName = @TypeName              ");
            sql.AppendLine("      ,Description = @Description        ");
            sql.AppendLine("      ,UsedStatus = @UsedStatus          ");
            sql.AppendLine("      ,ModifiedDate = @ModifiedDate      ");
            sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID  ");
            sql.AppendLine(" WHERE CompanyCD = @CompanyCD and ID=@ID ");

            //设置参数
            SqlParameter[] param = new SqlParameter[7];
            int i = 0;
            param[i++] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[i++] = SqlHelper.GetParameter("@ID", model.ID);
            param[i++] = SqlHelper.GetParameter("@TypeName", model.TypeName);
            param[i++] = SqlHelper.GetParameter("@Description", model.Description);
            param[i++] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[i++] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
            param[i++] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }

        /// <summary>
        /// 获取公共分类信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCodePublicType(string TypeFlag, string TypeName, string UsedStatus, string CompanyCD, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string allTypeName = "";
            StringBuilder sb = new System.Text.StringBuilder();
            string[] IdS = null;
            TypeName = TypeName.Substring(0, TypeName.Length);
            IdS = TypeName.Split(',');
            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allTypeName = sb.ToString().Replace("''", "','");
            string sql = "select distinct(a.ID) as ID,a.CompanyCD,a.TypeFlag,a.TypeCode,a.UsedStatus,a.TypeName,isnull(a.ModifiedDate,'')as ModifiedDate,isnull(a.ModifiedUserID,'')as ModifiedUserID,isnull(a.Description,'') as Description,b.typename as typecodeflag  from officedba.CodePublicType as a , pubdba.BillType as b ";
            sql += " where(a.typeflag=b.typeflag and a.typeflag=@TypeFlag and b.typelabel='1' and a.typecode=b.typecode  and a.CompanyCD=@CompanyCD";
            if (!string.IsNullOrEmpty(TypeName))
            {
                sql += "	AND b.typename in  (" + allTypeName + ")";
            }
            if (!string.IsNullOrEmpty(UsedStatus))
            {
                sql += " and a.UsedStatus=@UsedStatus ";
            }
            sql += ")";
            //add by hexw  另给ID添加了限制条件distinct 
            //费用设置中用到
            //TypeFlag=5供应链，TypeCode=6费用分类；TypeFlag=1个人桌面，TypeCode=4费用分类。
            if (TypeFlag == "1")
            {
                sql += "or ( a.typeflag=5  and b.typelabel='1' and a.typecode=6  and a.CompanyCD=@CompanyCD and b.typename='费用分类' ";
                if (!string.IsNullOrEmpty(TypeName))
                {
                    sql += "	AND b.typename in  (" + allTypeName + ")";
                }
                if (!string.IsNullOrEmpty(UsedStatus))
                {
                    sql += " and a.UsedStatus=@UsedStatus ";
                }
                sql += ")";
            }
            if (TypeFlag == "5")
            {
                sql += "or ( a.typeflag=1  and b.typelabel='1' and a.typecode=4  and a.CompanyCD=@CompanyCD and b.typename='费用分类' ";
                if (!string.IsNullOrEmpty(TypeName))
                {
                    sql += "	AND b.typename in  (" + allTypeName + ")";
                }
                if (!string.IsNullOrEmpty(UsedStatus))
                {
                    sql += " and a.UsedStatus=@UsedStatus ";
                }
                sql += ")";
            }
            //---------------------------------
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@TypeFlag", TypeFlag);
            param[2] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);

            DataTable dt = SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, OrderBy, param, ref totalCount);
            return dt;
            //DataTable dt = SqlHelper.ExecuteSql(sql, param);
            //return dt;
        }
       
       /// <summary>
       /// 加载树专用
       /// </summary>
       /// <param name="TypeFlag"></param>
       /// <returns></returns>
        public static DataTable GetCodePublicByTypeLabel(string TypeFlag,string CompanyCD)
        {
            string sql = "select TypeCode,TypeName from pubdba.billtype where TypeFlag=@TypeFlag and TypeLabel='1' ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@TypeFlag", TypeFlag);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }
       /// <summary>
       /// 根据分类编码和分类类别获取分类信息
       /// </summary>
       /// <param name="TypeFlag"></param>
       /// <param name="TypeCode"></param>
       /// <returns></returns>
       public static DataTable GetCodePublicByCode(string TypeFlag,string TypeCode,string CompanyCD)
       {
           string sql = "select ID,TypeFlag, TypeCode,TypeName from officedba.CodePublicType where TypeFlag=@TypeFlag and TypeCode=@TypeCode and UsedStatus='1' and CompanyCD=@CompanyCD";
          //add by hexw 费用设置中用到
           //TypeFlag=5供应链，TypeCode=6费用分类；TypeFlag=1个人桌面，TypeCode=4费用分类。
           if (TypeFlag == "1")
           {
               sql += " or (TypeFlag=5 and TypeCode=6 and UsedStatus='1' and CompanyCD=@CompanyCD)";
           }
           if (TypeFlag == "5")
           {
               if (TypeCode == "6")
               {
                   sql += " or (TypeFlag=1 and TypeCode=4 and UsedStatus='1' and CompanyCD=@CompanyCD)";
               }
           }
           //----------------------
           SqlParameter[] param = new SqlParameter[3];
           param[0] = SqlHelper.GetParameter("@TypeFlag", TypeFlag);
           param[1] = SqlHelper.GetParameter("@TypeCode", TypeCode);
           param[2] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           DataTable dt = SqlHelper.ExecuteSql(sql, param);
           return dt;
       }
       /// <summary>
       /// 删除公共分类信息
       /// </summary>
       /// <param name="TypeFlag"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
        public static bool DeleteCodePublicType(string  ID, string CompanyCD)
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
                Delsql[0] = "delete from  officedba.CodePublicType where ID IN (" + allID + ") and CompanyCD = '" + CompanyCD + "'";
                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
    }
}
