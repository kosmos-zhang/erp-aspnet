/**********************************************
 * 类作用：   币种类别数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/07
 * 修改时间： 2009/05/05
 * 修改人： 江贻明
 * 修改内容：添加、修改方法增加EndRate 字段。
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using XBase.Common;
namespace XBase.Data.Office.FinanceManager
{
    public  class CurrTypeSettingDBHelper
    {
         
      #region 修改期末汇率信息
        /// <summary>
        /// 修改币种期末汇率信息
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="EndRate">汇率</param>
        /// <returns>true 成功,false 失败</returns>
        public static bool UpdateEndRate(string ID,decimal EndRate)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update officedba.CurrencyTypeSetting set EndRate=@EndRate");
            sql.AppendLine("where ID=@ID");

            SqlParameter[] parms = 
            {
               new SqlParameter("@ID",ID),
               new SqlParameter("@EndRate",EndRate)

            };

            SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
      #endregion

        #region 获取币种的汇率及相关信息
        public static DataTable  GetCurrencyRate(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select isMaster,ExchangeRate from officedba.CurrencyTypeSetting ");
            sql.AppendLine("where ID=@ID and UsedStatus='1' ");
            SqlParameter [] parms = 
            {
                 new SqlParameter("@ID",ID)
            };
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
        #endregion

        #region 获取非本币的信息
        /// <summary>
        ///  获取非本币的信息
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetNotMasterCurrency(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ID,CurrencyName,CurrencySymbol,");
            sql.AppendLine("ExchangeRate from officedba.CurrencyTypeSetting");
            sql.AppendLine("where CompanyCD=@CompanyCD and ");
            sql.AppendLine("UsedStatus='1' and  isMaster='0' ");
            SqlParameter[] parms = 
            {
                 new SqlParameter("@CompanyCD",CompanyCD)
            };
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
      #endregion


        /// <summary>
        ///  获取本币的信息
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetMasterCurrency(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ID,CurrencyName,CurrencySymbol,");
            sql.AppendLine("ExchangeRate  from officedba.CurrencyTypeSetting");
            sql.AppendLine("where CompanyCD=@CompanyCD and ");
            sql.AppendLine("UsedStatus='1' and  isMaster='1' ");
            SqlParameter[] parms = 
            {
                 new SqlParameter("@CompanyCD",CompanyCD)
            };
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }

      #region 获取币种类别信息
        /// <summary>
      /// 获取币种类别信息
      /// </summary>
      /// <param name="CompanyCD">公司编码</param>
      /// <returns>DataTable</returns>
      public static DataTable GetCurrTypeByCompanyCD(string CompanyCD)
      {
          string sql = "select cast(ID as varchar) as ID ,CompanyCD,CurrencyName,CurrencySymbol,case(isMaster) when 1 then '是' when 0 then  '否' end as isMaster,ExchangeRate,ConvertWay AS ConvertWayID,case(ConvertWay) when 1 then '人民币' when 2 then '美元' when 3 then '英镑' end as ConvertWay  ,   CONVERT(VARCHAR(10),ChangeTime,21) as ChangeTime ,case(UsedStatus) when 1 then '启用' when 0 then '停用' end as UsedStatus   from officedba.CurrencyTypeSetting where CompanyCD=@CompanyCD order by isMaster desc ";//and UsedStatus=1";
          SqlParameter[] parms = new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
         return  SqlHelper.ExecuteSql(sql,parms);
      }
      #endregion



      #region 获取启用状态的币种类别信息
      /// <summary>
      /// 获取币种类别信息
      /// </summary>
      /// <param name="CompanyCD">公司编码</param>
      /// <returns>DataTable</returns>
      public static DataTable GetUsedCurrTypeByCompanyCD(string CompanyCD)
      {
          string sql = "select cast(ID as varchar) as ID ,CompanyCD,CurrencyName,CurrencySymbol,case(isMaster) when 1 then '是' when 0 then  '否' end as isMaster,ExchangeRate,ConvertWay AS ConvertWayID,case(ConvertWay) when 1 then '人民币' when 2 then '美元' when 3 then '英镑' end as ConvertWay  ,ChangeTime,case(UsedStatus) when 1 then '启用' when 0 then '停用' end as UsedStatus   from officedba.CurrencyTypeSetting where CompanyCD=@CompanyCD and UsedStatus=1 order by isMaster desc ";//and UsedStatus=1";
          SqlParameter[] parms = new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          return SqlHelper.ExecuteSql(sql, parms);
      }
      #endregion


      #region 插入币种类别信息
      /// <summary>
      /// 插入币种类别信息
      /// </summary>
      /// <param name="Model"></param>
      /// <returns>true 添加成功 : false 添加失败</returns>
      public static bool InSertCurrTypeSetting(CurrencyTypeSettingModel Model)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Insert into Officedba.CurrencyTypeSetting ( CompanyCD,CurrencyName, ");
          sql.AppendLine("CurrencySymbol,isMaster,ExchangeRate,ConvertWay,ChangeTime,UsedStatus,EndRate )");
          sql.AppendLine("Values(@CompanyCD,@CurrencyName,@CurrencySymbol,");
          sql.AppendLine("@isMaster,@ExchangeRate,@ConvertWay,@ChangeTime,@UsedStatus,@EndRate )");
          SqlParameter[] parms = new SqlParameter[9];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
          parms[1] = SqlHelper.GetParameter("@CurrencyName", Model.CurrencyName);
          parms[2] = SqlHelper.GetParameter("@CurrencySymbol", Model.CurrencySymbol);
          parms[3] = SqlHelper.GetParameter("@isMaster", Model.isMaster);
          parms[4] = SqlHelper.GetParameter("@ExchangeRate", Model.ExchangeRate);
          parms[5] = SqlHelper.GetParameter("@ConvertWay", Model.ConvertWay);
          parms[6] = SqlHelper.GetParameter("@ChangeTime", Model.ChangeTime);
          parms[7] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
          parms[8] = SqlHelper.GetParameter("@EndRate", Model.ExchangeRate);
          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion

      #region  更新币种信息操作
      /// <summary>
        /// 更新币种信息操作
       /// </summary>
       /// <param name="Model"></param>
      /// <returns>true 更新成功 : false 更新失败</returns>
      public static bool UpdateCurrTypeSetting(CurrencyTypeSettingModel Model)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update  Officedba.CurrencyTypeSetting set CompanyCD=@CompanyCD, ");
          sql.AppendLine("CurrencyName=@CurrencyName,CurrencySymbol=@CurrencySymbol,isMaster=@isMaster,ExchangeRate=@ExchangeRate,ConvertWay=@ConvertWay,");
          sql.AppendLine("ChangeTime=@ChangeTime,UsedStatus=@UsedStatus,EndRate=@EndRate  where CompanyCD=@CompanyCD and ID=@ID ");
          SqlParameter[] parms = new SqlParameter[10];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
          parms[1] = SqlHelper.GetParameter("@CurrencyName", Model.CurrencyName);
          parms[2] = SqlHelper.GetParameter("@CurrencySymbol", Model.CurrencySymbol);
          parms[3] = SqlHelper.GetParameter("@isMaster", Model.isMaster);
          parms[4] = SqlHelper.GetParameter("@ExchangeRate", Model.ExchangeRate);
          parms[5] = SqlHelper.GetParameter("@ConvertWay", Model.ConvertWay);
          parms[6] = SqlHelper.GetParameter("@ChangeTime", Model.ChangeTime);
          parms[7] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
          parms[8] = SqlHelper.GetParameter("@ID", Model.ID);
          parms[9] = SqlHelper.GetParameter("@EndRate", Model.ExchangeRate);

          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion

      #region 删除币种信息操作
      /// <summary>
      /// 删除币种信息操作
      /// </summary>
      /// <param name="CompanyCD">公司编码</param>
      /// <param name="ID">主键标识ID</param>
      /// <returns>true 删除成功 : false 删除失败</returns>
      public static bool DeleteCurrTypeSetting(string CompanyCD, string ID)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Delete from officedba.CurrencyTypeSetting ");
          sql.AppendLine("where CompanyCD=@CompanyCD and ID in ( "+ID+" )");
          SqlParameter[] parms = new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion

      #region 查询
      #endregion

      #region 币种
      /// <summary>
      /// 获取币种类别信息及汇率信息 采购模块 Added By songfei 2009-04-21
      /// </summary>
      /// <returns>DataTable</returns>
      /// 
      public static DataTable GetCurrenyType()
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("SELECT ID                                           ");
          sql.AppendLine("      ,isnull(CurrencyName   ,'') AS CurrencyName   ");
          sql.AppendLine("      ,(CONVERT(varchar(50),ID))+'_'+(CONVERT(varchar(50),ExchangeRate)) as hhh");
          sql.AppendLine("  FROM officedba.CurrencyTypeSetting                ");
          sql.AppendLine(" WHERE  CompanyCD=@CompanyCD              ");
          sql.AppendLine(" AND UsedStatus=@UsedStatus            ");
          SqlCommand comm = new SqlCommand();
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));

          comm.CommandText = sql.ToString();
          return SqlHelper.ExecuteSearch(comm);
      }
      #endregion
        /// <summary>
        /// 根据ID获取币种对应的汇率信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
      public static decimal GetCuerryTypeExchangeRate(int ID)
      {
          decimal nev = 0;
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select ");
          sql.AppendLine(" isnull(ExchangeRate,1) as ExchangeRate  from officedba.CurrencyTypeSetting");
          sql.AppendLine("where ID=@ID ");
          SqlParameter[] parms = 
            {
                 new SqlParameter("@ID",ID)
            };
          object obj = SqlHelper.ExecuteScalar(sql.ToString(),parms);
          if (obj != null)
          {
              nev = Convert.ToDecimal(obj);
          }
          return nev;
      }
        /// <summary>
        /// 根据ID获取币种名称
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
      public static string GetCuerryTypeName(int ID)
      {
          string nev = string.Empty;
          string sql = "select CurrencyName from officedba.CurrencyTypeSetting where ID=@ID ";//and UsedStatus=1";
          SqlParameter[] parms = new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@ID", ID);
          object obj = SqlHelper.ExecuteScalar(sql,parms);
          if (obj != null)
          {
              nev = Convert.ToString(obj);
          }
          return nev;
      }


      #region 是否已存在本位币
      /// <summary>
      /// 是否已存在本位币
      /// </summary>
      /// <param name="CompanyCD">公司编码</param>
      /// <returns></returns>
      public static bool IsExitsMasterCurrency(string CompanyCD,string ID)
      {
          int nev=0;
          string sql = "select count(id) from officedba.CurrencyTypeSetting where isMaster=1 and CompanyCD=@CompanyCD and ID not in ( "+ID+" )";
          SqlParameter[] parms = new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          object obj = SqlHelper.ExecuteScalar(sql,parms);
          if (obj != null)
          {
              nev = Convert.ToInt32(obj);
          }

          return nev > 0 ? true : false;

      }
        #endregion

        #region 判断币种名称或币种符是否重复
        /// <summary>
      /// 判断币种名称或币种符是否重复
        /// </summary>
        /// <param name="CurrencyName">币种名称</param>
        /// <param name="CurrencySymbol">币种符</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
      public static int IsSame(string CurrencyName, string CurrencySymbol,string CompanyCD,string ID)
      {
          int nev = 0;
          string sql = "select count(ID) from officedba.CurrencyTypeSetting where ID not in ( " + ID + " ) and CurrencyName=@CurrencyName and CompanyCD=@CompanyCD";
          SqlParameter[] parms = new SqlParameter[2];
          parms[0] = SqlHelper.GetParameter("@CurrencyName", CurrencyName);
          parms[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          object obj = SqlHelper.ExecuteScalar(sql, parms);
          int CurrencyNameCount=0;
          if (obj != null)
          {
              CurrencyNameCount = Convert.ToInt32(obj);
              if (CurrencyNameCount > 0)
              {
                  nev = 1;
              }
          }
          string sql2 = "select count(ID) from officedba.CurrencyTypeSetting where ID not in ( " + ID + " ) and CurrencySymbol=@CurrencySymbol and CompanyCD=@CompanyCD";
          SqlParameter[] parmss = new SqlParameter[2];
          parmss[0] = SqlHelper.GetParameter("@CurrencySymbol", CurrencySymbol);
          parmss[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          object objj = SqlHelper.ExecuteScalar(sql2, parmss);
          int CurrencySymbolCount = 0;
          if (objj != null)
          {
              CurrencySymbolCount = Convert.ToInt32(objj);
              if (CurrencySymbolCount > 0)
              {
                  nev = 2;
              }
          }

          string sql3 = "select count(ID) from officedba.CurrencyTypeSetting where ID not in ( " + ID + " ) and CurrencySymbol=@CurrencySymbol and CompanyCD=@CompanyCD and CurrencyName=@CurrencyName";
          SqlParameter[] parmsst = new SqlParameter[3];
          parmsst[0] = SqlHelper.GetParameter("@CurrencySymbol", CurrencySymbol);
          parmsst[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          parmsst[2] = SqlHelper.GetParameter("@CurrencyName", CurrencyName);
          object objjt = SqlHelper.ExecuteScalar(sql3, parmsst);
          int bolCount = 0;
          if (objjt != null)
          {
              bolCount = Convert.ToInt32(objjt);
              if (bolCount > 0)
              {
                  nev = 3;
              }
          }

          return nev;

      }
        #endregion
    }
}
