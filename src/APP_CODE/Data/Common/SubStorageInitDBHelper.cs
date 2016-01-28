using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.StorageManager;

namespace XBase.Data.Common
{
   public class SubStorageInitDBHelper
    {
        /// <summary>
        /// 获取已开通且使用进销存的企业
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDistinctCompany()
        {
            string SQL = "SELECT distinct CompanyCD FROM officedba.GetSubStrongeInit ";
            return SqlHelper.ExecuteSql(SQL);
        }

        /// <summary>
        /// 获取某企业使用进销存的日期记录
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDistinctDate(string CompanyCD)
        {
            string sql = "SELECT distinct EnterDate  FROM officedba.GetSubStrongeInit where CompanyCD=@CompanyCD order by EnterDate asc";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD", CompanyCD) 
                                   };
            return SqlHelper.ExecuteSql(sql, paras);
        }



        /// <summary>
        /// 获取某企业使用进销存的商品及仓库对应的记录
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDistinctProductAndStronge(string CompanyCD)
        {
            string sql = "SELECT ProductID,StorageID  FROM officedba.GetSubStrongeInit where CompanyCD=@CompanyCD group by ProductID,StorageID ";
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
            string sql = "select ProductID,StorageID,ProductCount,EnterDate,flag,Price,NoCode,CompanyCD from  officedba.GetSubStrongeInit where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=@StorageID order by EnterDate asc";
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
        public static bool InsertStorageAccount(SubStorageAccountModel model)
        {
            bool Rev = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SubStorageAccount(");
            strSql.Append("CompanyCD,BillType,ProductID,DeptID,BillNo,HappenDate,HappenCount,ProductCount,Creator,Price,PageUrl,ReMark)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@BillType,@ProductID,@DeptID,@BillNo,@HappenDate,@HappenCount,@ProductCount,@Creator,@Price,@PageUrl,@ReMark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BillType", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
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
            parameters[3].Value = model.DeptID;
            parameters[4].Value = model.BillNo;
            parameters[5].Value = model.HappenDate;
            parameters[6].Value = model.HappenCount;
            parameters[7].Value = model.ProductCount;
            parameters[8].Value = model.Creator;
            parameters[9].Value = model.Price;
            parameters[10].Value = model.PageUrl;
            parameters[11].Value = model.Remark;

            Rev = SqlHelper.ExecuteTransSql(strSql.ToString(), parameters) > 0 ? true : false;
            return Rev;
        }
    }
}
