/**********************************************
 * 类作用：   资产类别数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/30
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
namespace XBase.Data.Office.FinanceManager
{
   public  class AssetTypeSettingDBHelper
   {
       #region 添加固定资产类别
       /// <summary>
       /// 添加固定资产类别
       /// </summary>
       /// <param name="Model">实体</param>
       /// <returns>true 成功，false失败</returns>
       public static bool InsertAssetType(AssetTypeSettingModel Model,out int ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Insert into officedba.AssetTypeSetting");
           sql.AppendLine("(CompanyCD,TypeName,CountMethod,");
           sql.AppendLine("EstimateUseYear,EstiResiValue,SubjectsCD,");
           sql.AppendLine("UsedStatus,Remark)values(@CompanyCD,");
           sql.AppendLine("@TypeName,@CountMethod");
           sql.AppendLine(",@EstimateUseYear,@EstiResiValue,@SubjectsCD");
           sql.AppendLine(",@UsedStatus,@Remark)");
           sql.AppendLine("set @IntID= @@IDENTITY");

           SqlParameter[]parms=new SqlParameter[9];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
           parms[1] = SqlHelper.GetParameter("@TypeName", Model.TypeName);
           parms[2] = SqlHelper.GetParameter("@CountMethod", Model.CountMethod);
           parms[3] = SqlHelper.GetParameter("@EstimateUseYear", Model.EstimateUseYear);
           parms[4] = SqlHelper.GetParameter("@EstiResiValue", Model.EstiResiValue);
           parms[5] = SqlHelper.GetParameter("@SubjectsCD", Model.SubjectsCD);
           parms[6] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
           parms[7] = SqlHelper.GetParameter("@Remark", Model.Remark);
           parms[8] = SqlHelper.GetOutputParameter("@IntID", SqlDbType.Int);

           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           ID = Convert.ToInt32(parms[8].Value);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 修改固定资产类别
       /// <summary>
       /// 修改固定资产类别
       /// </summary>
       /// <param name="Model">实体</param>
       /// <returns>true 成功，false失败</returns>
       public static bool UpdateAssetType(AssetTypeSettingModel Model)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.AssetTypeSetting set TypeName=@TypeName");
           sql.AppendLine(",CountMethod=@CountMethod,EstimateUseYear=@EstimateUseYear");
           sql.AppendLine(",EstiResiValue=@EstiResiValue,SubjectsCD=@SubjectsCD,UsedStatus=@UsedStatus,Remark=@Remark");
           sql.AppendLine(" where CompanyCD=@CompanyCD and ID=@ID");
           SqlParameter[]parms=new SqlParameter[9];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
           parms[1] = SqlHelper.GetParameter("@ID", Model.ID);
           parms[2] = SqlHelper.GetParameter("@TypeName", Model.TypeName);
           parms[3] = SqlHelper.GetParameter("@CountMethod", Model.CountMethod);
           parms[4] = SqlHelper.GetParameter("@EstimateUseYear", Model.EstimateUseYear);
           parms[5] = SqlHelper.GetParameter("@EstiResiValue", Model.EstiResiValue);
           parms[6] = SqlHelper.GetParameter("@SubjectsCD", Model.SubjectsCD);
           parms[7] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
           parms[8] = SqlHelper.GetParameter("@Remark", Model.Remark);
           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 删除固定资产类别
       /// <summary>
       /// 删除固定资产类别
       /// </summary>
       /// <param name="CompanyCD">企业编码</param>
       /// <param name="ID">主键</param>
       /// <returns>true 成功，false失败</returns>
       public static bool DeleteAssetType(string CompanyCD, string ID)
       {
           StringBuilder sql = new StringBuilder();

           sql.AppendLine("Delete from officedba.AssetTypeSetting");
           sql.AppendLine("where CompanyCD=@CompanyCD and ID in("+ID+") ");


           SqlParameter[] parms = 
           {
                new SqlParameter("@CompanyCD",CompanyCD)
           };
           
           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 获取类别名称
       /// <summary>
       /// 获取类别名称
       /// </summary>
       /// <param name="CompanyCD">企业编码</param>
       /// <returns>DataTable</returns>
       public static DataTable GetAssetType(string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select ID,TypeName from officedba.AssetTypeSetting ");
           sql.AppendLine("where CompanyCD=@CompanyCD");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }
       #endregion

       #region 获取固定资产类别信息
       /// <summary>
       /// 获取固定资产类别信息
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAssetTypeInfo(string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.TypeName,");
           sql.AppendLine("a.CountMethod,a.EstimateUseYear,a.EstiResiValue");
           sql.AppendLine(",a.SubjectsCD,a.UsedStatus,a.Remark,b.SubjectsName from  officedba.AssetTypeSetting as a");
           sql.AppendLine("inner join officedba.AccountSubjects as b on a.SubjectsCD=b.SubjectsCD and  a.CompanyCD=b.CompanyCD");
           sql.AppendLine(" where a.CompanyCD=@CompanyCD ");
           SqlParameter []parms=new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }
       #endregion

       #region 根据类别ID获取类别信息
       /// <summary>
       /// 根据类别ID获取类别信息
       /// </summary>
       /// <param name="TypeID">类别ID</param>
       /// <returns>DataTable</returns>
       public static DataTable GetAssetTypeByTypeID(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.CountMethod,a.EstimateUseYear, ");
           sql.AppendLine("a.EstiResiValue,a.SubjectsCD,b.SubjectsName");
           sql.AppendLine(" from officedba.AssetTypeSetting as a");
           sql.AppendLine(" inner join  officedba.AccountSubjects as  b ");
           sql.AppendLine(" on a.SubjectsCD=b.SubjectsCD");
           sql.AppendLine("where a.UsedStatus='1' and  a.ID=@ID ");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ID", ID);
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }
       #endregion

       #region 判断资产类别是否被引用
       public static bool IsAssetTypeReference(int TypeID)
       {
           bool result = false;

           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select count(*) from officedba.FixAssetInfo ");
           sql.AppendLine("where FixType=@TypeID");

           SqlParameter[] parm = { new SqlParameter("@TypeID",TypeID) };

           object objs = SqlHelper.ExecuteScalar(sql.ToString(),parm);
           if (Convert.ToInt32(objs) > 0)
           {
               result = true;
           }

           return result;
       }
       #endregion

       #region 根据类别ID获取类别名称 add by MoShenlin
       /// <summary>
       /// 根据类别ID获取类别信息
       /// </summary>
       /// <param name="TypeID">类别ID</param>
       /// <returns>DataTable</returns>
       public static string GetAssetTypeNameByTypeID(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select TypeName from officedba.AssetTypeSetting ");
           sql.AppendLine("where ID=@ID");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ID", ID);
           string nev = string.Empty;
           object obj = SqlHelper.ExecuteScalar(sql.ToString(), parms);

           if (obj != null)
           {
               nev = Convert.ToString(obj);
           }
           return nev;
       }
       #endregion

    }
}
