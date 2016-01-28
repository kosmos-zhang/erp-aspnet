using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.StorageManager;

namespace XBase.Data.Common
{
   public class StrongeInit
    {
       /// <summary>
       /// 获取已开通且使用进销存的企业
       /// </summary>
       /// <returns></returns>
       public static DataTable GetDistinctCompany()
       {
           string SQL = "SELECT distinct CompanyCD FROM officedba.GetStrongeInit ";
           return SqlHelper.ExecuteSql(SQL);
       }

       /// <summary>
       /// 获取某企业使用进销存的日期记录
       /// </summary>
       /// <returns></returns>
       public static DataTable GetDistinctDate(string CompanyCD)
       {
           string sql = "SELECT distinct EnterDate  FROM officedba.GetStrongeInit where CompanyCD=@CompanyCD order by EnterDate asc";
           SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD", CompanyCD) 
                                   };
           return SqlHelper.ExecuteSql(sql,paras);
       }



       /// <summary>
       /// 获取某企业使用进销存的商品及仓库对应的记录
       /// </summary>
       /// <returns></returns>
       public static DataTable GetDistinctProductAndStronge(string CompanyCD)
       {
           string sql = "SELECT ProductID,StorageID  FROM officedba.GetStrongeInit where CompanyCD=@CompanyCD group by ProductID,StorageID ";
           SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD", CompanyCD) 
                                   };
           return SqlHelper.ExecuteSql(sql, paras);
       }

       /// <summary>
       /// 获取该企业对应的物品，仓库的所有操作记录
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetCompanyInfo(string CompanyCD, string ProductID, string StorageID)
       {
           string sql = "select ProductID,StorageID,ProductCount,EnterDate,flag,Price,NoCode,CompanyCD from  officedba.GetStrongeInit where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=@StorageID order by EnterDate asc";
           SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD", CompanyCD),
                                       new SqlParameter("@ProductID", ProductID),
                                       new SqlParameter("@StorageID", StorageID)
                                   };
           return SqlHelper.ExecuteSql(sql, paras);

       }

       /// <summary>
       /// 插入库存流水账
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static bool InsertStorageAccount(StorageAccountModel model)
       {
           bool Rev = false;

           StringBuilder strSql = new StringBuilder();
           strSql.Append("insert into officedba.StorageAccount(");
           strSql.Append("CompanyCD,BillType,ProductID,StorageID,BillNo,HappenDate,HappenCount,ProductCount,Creator,Price,PageUrl,ReMark)");
           strSql.Append(" values (");
           strSql.Append("@CompanyCD,@BillType,@ProductID,@StorageID,@BillNo,@HappenDate,@HappenCount,@ProductCount,@Creator,@Price,@PageUrl,@ReMark)");
           strSql.Append(";select @@IDENTITY");
           SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BillType", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@StorageID", SqlDbType.Int,4),
					new SqlParameter("@BillNo", SqlDbType.VarChar,50),
					new SqlParameter("@HappenDate", SqlDbType.DateTime),
					new SqlParameter("@HappenCount", SqlDbType.Decimal,13),
					new SqlParameter("@ProductCount", SqlDbType.Decimal,13),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@PageUrl", SqlDbType.VarChar,500),
					new SqlParameter("@ReMark", SqlDbType.VarChar,500)};
           parameters[0].Value = model.CompanyCD;
           parameters[1].Value = model.BillType;
           parameters[2].Value = model.ProductID;
           parameters[3].Value = model.StorageID;
           parameters[4].Value = model.BillNo;
           parameters[5].Value = model.HappenDate;
           parameters[6].Value = model.HappenCount;
           parameters[7].Value = model.ProductCount;
           parameters[8].Value = model.Creator;
           parameters[9].Value = model.Price;
           parameters[10].Value = model.PageUrl;
           parameters[11].Value = model.ReMark;

           Rev = SqlHelper.ExecuteTransSql(strSql.ToString(), parameters) > 0 ? true : false;
           return Rev;
       }

       /// <summary>
       /// 获取某企业对应的某物品在某仓库的现有存量
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="ProductID"></param>
       /// <param name="StorageID"></param>
       /// <returns></returns>
       public static decimal GetStorageProduct(string CompanyCD, string ProductID, string StorageID)
       {
           decimal rev = 0;
           string sql = "select isnull(ProductCount,0) as ProductCount from officedba.StorageProduct where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=@StorageID  and BatchNo is null ";
           SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD", CompanyCD),
                                       new SqlParameter("@ProductID", ProductID),
                                       new SqlParameter("@StorageID", StorageID)
                                   };

           object obj = SqlHelper.ExecuteScalar(sql, paras);
           if (obj != null)
               rev = Convert.ToDecimal(obj);

           return rev;
       }

       /// <summary>
       /// 更新有批量导入记录物品的库存流水账记录
       /// </summary>
       /// <param name="QueryStr"></param>
       /// <returns></returns>
       public static bool UpdateStorageAccount(string CompanyCD, string ProductID, string StorageID,string MaxID)
       {
           bool rev = false;
           string sql = "update officedba.StorageAccount set ProductCount=null where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=@StorageID  and BatchNo is null and id<'"+MaxID+"' ";
           SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD", CompanyCD),
                                       new SqlParameter("@ProductID", ProductID),
                                       new SqlParameter("@StorageID", StorageID)
                                   };
           rev = SqlHelper.ExecuteTransSql(sql, paras) > 0 ? true : false;
           return rev;
       }

       /// <summary>
       /// 获取符合条件的库存流水账的最大ID
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="ProductID"></param>
       /// <param name="StorageID"></param>
       /// <returns></returns>
       public static int GetMaxID(string CompanyCD, string ProductID, string StorageID)
       {
           int rev = 0;
           string sql = "select max(ID) from officedba.StorageAccount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=@StorageID  and BatchNo is null ";
           SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD", CompanyCD),
                                       new SqlParameter("@ProductID", ProductID),
                                       new SqlParameter("@StorageID", StorageID)
                                   };

           object obj = SqlHelper.ExecuteScalar(sql, paras);
           if (obj != null)
               rev = Convert.ToInt32(obj);
           return rev;
       }

       /// <summary>
       /// 更新最大一条记录的现有存量（有批量导入的物品）
       /// </summary>
       /// <param name="QueryStr"></param>
       /// <param name="ProducntCount"></param>
       /// <returns></returns>
       public static bool UpdateMaxAccount(string maxid,decimal ProducntCount)
       {
           bool rev = false;
           string sql = "update officedba.StorageAccount set ProductCount=@ProductCount where ID=@ID ";
           SqlParameter[] paras = { 
                                       new SqlParameter("@ProductCount", ProducntCount),
                                       new SqlParameter("@ID", maxid)
                                   };
           
           rev = SqlHelper.ExecuteTransSql(sql, paras) > 0 ? true : false;
           return rev;
       }
    }
}
