/**********************************************
 * 类作用：   会计科目数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/09
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
namespace XBase.Data.Office.FinanceManager
{
   public class AccountSubjectsDBHelper
   {
       #region 新增会计科目信息
       /// <summary>
       /// 新增会计科目信息 
       /// </summary>
       /// <param name="Model">科目实体</param>
       /// <returns>True 成功，false失败</returns>
       public static bool InsertAccountSubjects(AccountSubjectsModel Model)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Insert into Officedba.AccountSubjects(CompanyCD,SubjectsCD,SubjectsName, ");
           sql.AppendLine("ParentID,SubjectsType,Type,BlanceDire,CreateDate,AuciliaryCD,ForCurrencyAcc,UsedStatus)");
           sql.AppendLine("Values(@CompanyCD,@SubjectsCD,@SubjectName,");
           sql.AppendLine("@ParentID,@SubjectsType,@Type,@BlanceDire,getdate(),@AuciliaryCD,@ForCurrencyAcc,@UsedStatus)");
           SqlParameter []parms=new SqlParameter[10];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
           parms[1] = SqlHelper.GetParameter("@SubjectsCD", Model.SubjectsCD);
           parms[2] = SqlHelper.GetParameter("@SubjectName", Model.SubjectsName);
           parms[3] = SqlHelper.GetParameter("@ParentID", Model.ParentID);
           parms[4] = SqlHelper.GetParameter("@SubjectsType", Model.SubjectsType);
           parms[5] = SqlHelper.GetParameter("@Type", Model.Type);
           parms[6] = SqlHelper.GetParameter("@BlanceDire", Model.BlanceDire);
           parms[7] = SqlHelper.GetParameter("@AuciliaryCD", Model.AuciliaryCD);
           parms[8] = SqlHelper.GetParameter("@ForCurrencyAcc", Model.ForCurrencyAcc);
           parms[9] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 修改会计科目信息
       /// <summary>
       /// 修改会计科目信息
       /// </summary>
       /// <param name="Model">科目实体</param>
       /// <returns>True 成功，false失败</returns>
       public static bool UpdateAccountSubjects(AccountSubjectsModel Model)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update  Officedba.AccountSubjects set SubjectsCD=@SubjectsCD, ");
           sql.AppendLine("SubjectsName=@SubjectsName,ParentID=@ParentID,SubjectsType=@SubjectsType,Type=@Type,BlanceDire=@BlanceDire,");
           sql.AppendLine("AuciliaryCD=@AuciliaryCD,UsedStatus=@UsedStatus,ForCurrencyAcc=@ForCurrencyAcc    where CompanyCD=@CompanyCD and ID=@ID ");
           SqlParameter[]parms=new  SqlParameter[11];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
           parms[1] = SqlHelper.GetParameter("@SubjectsName", Model.SubjectsName);
           parms[2] = SqlHelper.GetParameter("@ParentID", Model.ParentID);
           parms[3] = SqlHelper.GetParameter("@SubjectsType", Model.SubjectsType);
           parms[4] = SqlHelper.GetParameter("@Type", Model.Type);

           parms[5] = SqlHelper.GetParameter("@BlanceDire", Model.BlanceDire);
           parms[6] = SqlHelper.GetParameter("@AuciliaryCD", Model.AuciliaryCD);
           parms[7] = SqlHelper.GetParameter("@ForCurrencyAcc", Model.ForCurrencyAcc);
           parms[8] = SqlHelper.GetParameter("@ID", Model.ID);
           parms[9] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
           parms[10] = SqlHelper.GetParameter("@SubjectsCD", Model.SubjectsCD);
           SqlHelper.ExecuteTransSql(sql.ToString(),parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 更新科目期初值
       /// <summary>
       /// 更新科目期初值
       /// </summary>
       /// <param name="Model">科目实体</param>
       /// <returns>True 成功，false失败</returns>
       public static bool UpdateBeginMoney(AccountSubjectsModel Model)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.AccountSubjects set CreateDate=@CreateDate,");
           sql.AppendLine("YInitialValue=@YInitialValue,YTotalDebit=@YTotalDebit,");
           sql.AppendLine("YTotalLenders=@YTotalLenders,BeginMoney=@BeginMoney where ID=@ID and CompanyCD=@CompanyCD");
           SqlParameter[]parms=new SqlParameter[7];
           parms[0] = SqlHelper.GetParameter("@CreateDate", Model.CreateDate);
           parms[1] = SqlHelper.GetParameter("@YInitialValue", Model.YInitialValue);
           parms[2] = SqlHelper.GetParameter("@YTotalDebit", Model.YTotalDebit);
           parms[3] = SqlHelper.GetParameter("@YTotalLenders", Model.YTotalLenders);
           parms[4] = SqlHelper.GetParameter("@BeginMoney", Model.BeginMoney);
           parms[5] = SqlHelper.GetParameter("@ID", Model.ID);
           parms[6] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);

           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 删除会计科目
       /// <summary>
       /// 删除会计科目
       /// </summary>
       /// <param name="ID">主键编码</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>True 成功，false失败</returns>
       public static bool DelAccountSubjects(string []sql)
       {
           SqlHelper.ExecuteTransForListWithSQL(sql);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 获取会计科目信息
       /// <summary>
       /// 获取会计科目信息
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>DataTable</returns>
       public static DataTable GetAccountSubjects(string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select ID, CompanyCD,SubjectsCD,SubjectsName");
           sql.AppendLine(",Type,BlanceDire,CreateDate,YInitialValue,YTotalDebit,YTotalLenders,BeginMoney");
           sql.AppendLine(" from   officedba.AccountSubjects  where CompanyCD=@CompanyCD order by  SubjectsCD");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }
       #endregion

       #region 根据企业编码和主键科目详情
       /// <summary>
       /// 根据企业编码和主键科目详情
       /// </summary>
       /// <param name="CompanyCD">企业编码</param>
       /// <param name="ID">主键</param>
       /// <returns>DataTable</returns>
       public static DataTable GetAccountsByID(string CompanyCD, string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select CompanyCD,SubjectsCD,SubjectsName,SubjectsType, ParentID");
           sql.AppendLine(",Type,BlanceDire,AuciliaryCD,ForCurrencyAcc,UsedStatus");
           sql.AppendLine(" from   officedba.AccountSubjects  where CompanyCD=@CompanyCD and ID=@ID");
           SqlParameter[] parms = new SqlParameter[2];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           parms[1] = SqlHelper.GetParameter("@ID", ID);
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }
       #endregion

       #region 根据企业编码获取科目信息
       /// <summary>
       /// 根据企业编码获取科目信息
       /// </summary>
       /// <param name="CompanyCD">企业编码</param>
       /// <returns>DataTable</returns>
       public static DataTable GetASubjectsByCompanyCD(string CompanyCD,string ParentID)
       {
           string sql = "select CompanyCD,ID,SubjectsCD,SubjectsName,ParentID,SubjectsType,[Type],BlanceDire,ForCurrencyAcc from officedba.AccountSubjects where CompanyCD=@CompanyCD and ParentID=@ParentID order by subjectsCD ";
           SqlParameter[] parms = new SqlParameter[2];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           parms[1] = SqlHelper.GetParameter("@ParentID", ParentID);
           return SqlHelper.ExecuteSql(sql, parms);
       }
       #endregion

       #region 判断该结点下，是否还有子结点
       /// <summary>
       /// 判断该结点下，是否还有子结点
       /// </summary>
       /// <param name="ParentCode">上级编码</param>
       /// <returns>大于0还有子节点，否则无子节点</returns>
       public static int ChildrenCount(string ParentCode,string CompanyCD)
       {
           string sql = "select count(ID) from officedba.AccountSubjects  where ParentID=@ParentID and CompanyCD=@CompanyCD";
           SqlParameter []parms=new SqlParameter[2];
           parms[0] = SqlHelper.GetParameter("@ParentID", ParentCode);
           parms[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           object obj = SqlHelper.ExecuteScalar(sql, parms);
           return Convert.ToInt32(obj);
       }
       #endregion

       #region 判断科目编码是否存在
       /// <summary>
       /// 判断科目编码是否存在
       /// </summary>
       /// <param name="SubjectsCD">科目编码</param>
       /// <returns>true 是，false 否</returns>
       public static bool SubjectsCDIsExist(string CompanyCD, string SubjectsCD)
       {
           bool result = false;
           string sql = "select count(*) from Officedba.AccountSubjects where CompanyCD=@CompanyCD and SubjectsCD=@SubjectsCD";
           SqlParameter[] parms = new SqlParameter[2];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           parms[1] = SqlHelper.GetParameter("@SubjectsCD", SubjectsCD);
           object obj = SqlHelper.ExecuteScalar(sql,parms);
           if (Convert.ToInt32(obj) > 0)
           {
               result = true;   
           }
           return result;
       }
       #endregion

       #region 根据科目类别获取科目信息
       /// <summary>
       /// 根据科目类别获取科目信息
       /// </summary>
       /// <param name="TypeID">科目类别</param>
       /// <returns>DataTable</returns>
       public static DataTable GetSubjectsByTypeID(string CompanyCD,string TypeID,string ParentID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select CompanyCD,ID,SubjectsCD,SubjectsName,");
           sql.AppendLine("ParentID,SubjectsType,[Type],BlanceDire,ForCurrencyAcc from");
           sql.AppendLine("officedba.AccountSubjects where CompanyCD=@CompanyCD");
           sql.AppendLine("and SubjectsType=@SubjectsType and");
           sql.AppendLine("ParentID=@ParentID order by SubjectsCD ");
           SqlParameter []parms=new SqlParameter[3];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           parms[1] = SqlHelper.GetParameter("@SubjectsType", TypeID);
           parms[2]=SqlHelper.GetParameter("@ParentID",ParentID);
           return SqlHelper.ExecuteSql(sql.ToString(),parms);
       }
       #endregion

       #region 根据科目类别获取科目期初值
       /// <summary>
       /// 根据科目类别获取科目信息
       /// </summary>
       /// <param name="TypeID">科目类别</param>
       /// <returns>DataTable</returns>
       public static DataTable GetSubjectsInitAmountByTypeID(string CompanyCD, string TypeID, string ParentID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select ID, CompanyCD,SubjectsCD,SubjectsName,[Type],");
           sql.AppendLine("BlanceDire,CreateDate,YInitialValue,YTotalDebit,YTotalLenders,BeginMoney");
           sql.AppendLine("from officedba.AccountSubjects where CompanyCD=@CompanyCD ");
           sql.AppendLine("and SubjectsType=@SubjectsType  and ParentID=@ParentID ");
    
           
           SqlParameter[] parms = new SqlParameter[3];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           parms[1] = SqlHelper.GetParameter("@SubjectsType", TypeID);
           parms[2] = SqlHelper.GetParameter("@ParentID", ParentID);
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }
       #endregion

       #region 根据科目编码模糊查询科目
       /// <summary>
       /// 根据科目编码模糊查询科目
       /// </summary>
       /// <param name="SubjectsCD">科目编码</param>
       /// <returns>DataTable</returns>
       public static DataTable QuerySubjectsBySubjectsCD(string CompanyCD,string SubjectsCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select SubjectsCD,SubjectsName,BlanceDire,ForCurrencyAcc from ");
           sql.AppendLine("officedba.AccountSubjects ");
           sql.AppendLine(" where SubjectsCD like'"+SubjectsCD+"%' and CompanyCD=@CompanyCD");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@CompanyCD",CompanyCD);
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }
       #endregion

       #region 根据获取编码获取科目信息
       public static DataTable GetSubjectsInfo(string CompanyCD, string SubjectsCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select SubjectsType,Type,BlanceDire,SubjectsName, ");
           sql.AppendLine("ForCurrencyAcc,AuciliaryCD ");
           sql.AppendLine("from officedba.AccountSubjects ");
           sql.AppendLine("where CompanyCD=@CompanyCD  ");
           sql.AppendLine("and SubjectsCD=@SubjectsCD ");

           SqlParameter[] parms = 
           {
               new SqlParameter("@CompanyCD",CompanyCD),
               new SqlParameter("@SubjectsCD",SubjectsCD)
           };

           return SqlHelper.ExecuteSql(sql.ToString(),parms);
         
       }
       #endregion

       #region 根据科目编码汇总年期初值信息
       public static decimal GetYInitialValueBySubjectsCD(string CompanyCD, string SubjectsCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select  sum (isnull (YInitialValue,0)) as YInitialValue  from officedba.AccountSubjects ");
           sql.AppendLine("where CompanyCD=@CompanyCD");
           sql.AppendLine("and SubjectsCD in ("+SubjectsCD+")");

           SqlParameter[] parms = 
           {
               new SqlParameter("@CompanyCD",CompanyCD)
           };


           return Convert.ToDecimal(SqlHelper.ExecuteSql(sql.ToString(), parms).Rows[0]["YInitialValue"]);
       }
       #endregion


   }
}



