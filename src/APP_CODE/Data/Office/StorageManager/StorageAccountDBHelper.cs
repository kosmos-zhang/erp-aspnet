/**********************************************
 * 类作用：   库存流水账数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2010/04/09
 ***********************************************/

using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Model.Office.StorageManager;

namespace XBase.Data.Office.StorageManager
{
    public class StorageAccountDBHelper
    {
        #region 添加：库存流水账
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool InsertStorageAccount(StorageAccountModel model, string loginUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.StorageAccount");
            sql.AppendLine("           (CompanyCD");
            sql.AppendLine("           ,BillType");
            sql.AppendLine("           ,ProductID");
            sql.AppendLine("           ,StorageID");
            sql.AppendLine("           ,BatchNo");
            sql.AppendLine("           ,BillNo");
            sql.AppendLine("           ,HappenDate");
            sql.AppendLine("           ,HappenCount");
            sql.AppendLine("           ,ProductCount");
            sql.AppendLine("           ,Creator");
            sql.AppendLine("           ,Price");
            sql.AppendLine("           ,PageUrl)");
            sql.AppendLine("     VALUES");
            sql.AppendLine("           (@CompanyCD");
            sql.AppendLine("           ,@BillType");
            sql.AppendLine("           ,@ProductID");
            sql.AppendLine("           ,@StorageID");
            sql.AppendLine("           ,@BatchNo");
            sql.AppendLine("           ,@BillNo");
            sql.AppendLine("           ,getdate()");
            sql.AppendLine("           ,@HappenCount");
            sql.AppendLine("           ,@ProductCount");
            sql.AppendLine("           ,@Creator");
            sql.AppendLine("           ,@Price");
            sql.AppendLine("           ,@PageUrl)");


            //设置参数
            SqlParameter[] param = new SqlParameter[11];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@BillType", model.BillType);
            param[2] = SqlHelper.GetParameter("@ProductID", model.ProductID);
            param[3] = SqlHelper.GetParameter("@StorageID", model.StorageID);
            param[4] = SqlHelper.GetParameter("@BatchNo", model.BatchNo);
            param[5] = SqlHelper.GetParameter("@BillNo", model.BillNo);
            param[6] = SqlHelper.GetParameter("@HappenCount", model.HappenCount);
            param[7] = SqlHelper.GetParameter("@ProductCount", model.ProductCount);
            param[8] = SqlHelper.GetParameter("@Creator", model.Creator);
            param[9] = SqlHelper.GetParameter("@Price", model.Price);
            param[10] = SqlHelper.GetParameter("@PageUrl", model.PageUrl);

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        #endregion

        #region 添加：库存流水账返回 SQLCommand
        /// <summary>
        /// 返回 SQLCommand
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SqlCommand InsertStorageAccountBySqlCommand(StorageAccountModel model)
        {
            #region  库存流水账表SQL语句
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.StorageAccount");
            sql.AppendLine("           (CompanyCD");
            sql.AppendLine("           ,BillType");
            sql.AppendLine("           ,ProductID");
            sql.AppendLine("           ,StorageID");
            sql.AppendLine("           ,BatchNo");
            sql.AppendLine("           ,BillNo");
            sql.AppendLine("           ,HappenDate");
            sql.AppendLine("           ,HappenCount");
            sql.AppendLine("           ,ProductCount");
            sql.AppendLine("           ,Creator");
            sql.AppendLine("           ,Price");
            sql.AppendLine("           ,PageUrl)");
            sql.AppendLine("     VALUES");
            sql.AppendLine("           (@CompanyCD");
            sql.AppendLine("           ,@BillType");
            sql.AppendLine("           ,@ProductID");
            sql.AppendLine("           ,@StorageID");
            sql.AppendLine("           ,@BatchNo");
            sql.AppendLine("           ,@BillNo");
            sql.AppendLine("           ,getdate()");
            sql.AppendLine("           ,@HappenCount");
            sql.AppendLine("           ,@ProductCount");
            sql.AppendLine("           ,@Creator");
            sql.AppendLine("           ,@Price");
            sql.AppendLine("           ,@PageUrl)");


            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillType", model.BillType));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@StorageID", model.StorageID));
            comm.Parameters.Add(SqlHelper.GetParameter("@BatchNo", model.BatchNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillNo", model.BillNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@HappenCount", model.HappenCount));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductCount", model.ProductCount));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@Price", model.Price));
            comm.Parameters.Add(SqlHelper.GetParameter("@PageUrl", model.PageUrl));


            return comm;
            #endregion
        }
        #endregion

        #region 返回操作库存流水账SqlCommand
        /// <summary>
        /// InOrOut:入库“0”，出库：“1”
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static SqlCommand InsertStorageAccountCommand(StorageAccountModel model,string InOrOut)
        {
            if (IsExist(model))
            {
                DataTable dt = RetProductCount(model);
                if (dt.Rows.Count > 0)
                {
                    if (InOrOut == "0")
                        model.ProductCount = Convert.ToDecimal(dt.Rows[0]["ProductCount"].ToString()) + model.HappenCount;
                    else
                        model.ProductCount = Convert.ToDecimal(dt.Rows[0]["ProductCount"].ToString()) - model.HappenCount;
                }

            }
            else 
            {
                if (InOrOut == "0")
                    model.ProductCount = 0 + model.HappenCount;
                else
                    model.ProductCount = 0 - model.HappenCount;
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.StorageAccount");
            sql.AppendLine("           (CompanyCD");
            sql.AppendLine("           ,BillType");
            sql.AppendLine("           ,ProductID");
            sql.AppendLine("           ,StorageID");
            sql.AppendLine("           ,BatchNo");
            sql.AppendLine("           ,BillNo");
            sql.AppendLine("           ,HappenDate");
            sql.AppendLine("           ,HappenCount");
            sql.AppendLine("           ,ProductCount");
            sql.AppendLine("           ,Creator");
            sql.AppendLine("           ,Price");
            sql.AppendLine("           ,PageUrl)");
            sql.AppendLine("     VALUES");
            sql.AppendLine("           (@CompanyCD");
            sql.AppendLine("           ,@BillType");
            sql.AppendLine("           ,@ProductID");
            sql.AppendLine("           ,@StorageID");
            sql.AppendLine("           ,@BatchNo");
            sql.AppendLine("           ,@BillNo");
            sql.AppendLine("           ,getdate()");
            sql.AppendLine("           ,@HappenCount");
            sql.AppendLine("           ,@ProductCount");
            sql.AppendLine("           ,@Creator");
            sql.AppendLine("           ,@Price");
            sql.AppendLine("           ,@PageUrl)");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillType", model.BillType.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", model.ProductID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", model.BatchNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillNo", model.BillNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@HappenCount", model.HappenCount.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount", model.ProductCount.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Price", model.Price.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PageUrl", model.PageUrl));
            return comm;

        }
        #endregion
        #region 记录是否存在
        /// <summary>
        /// 记录是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool IsExist(StorageAccountModel model)
        {
            bool Result = true;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from officedba.StorageAccount where ProductID="+model.ProductID+" AND StorageID="+model.StorageID+" AND CompanyCD='"+model.CompanyCD+"' ");
            if (!string.IsNullOrEmpty(model.BatchNo))
                sql.AppendLine(" and BatchNo='" + model.BatchNo.Trim() + "' ");
            else
                sql.AppendLine(" and (BatchNo is null or BatchNo='') ");
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            if (dt.Rows.Count > 0)
                Result = true;
            else
                Result = false;
            return Result;
        }
        #endregion
        #region 取得库存流量最后更新的现有存量
        /// <summary>
        /// 取得库存流量最后更新的现有存量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable RetProductCount(StorageAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ProductCount From  officedba.StorageAccount ");
            sql.AppendLine("WHERE ID=(select MAX(ID) ID from officedba.StorageAccount where ProductID=" + model.ProductID + " AND StorageID=" + model.StorageID + " AND CompanyCD='" + model.CompanyCD + "' AND (Remark IS NULL OR Remark='') ");
            if (!string.IsNullOrEmpty(model.BatchNo))
                sql.AppendLine(" and BatchNo='" + model.BatchNo.Trim() + "' ");
            else
                sql.AppendLine(" and (BatchNo is null or BatchNo='') ");
            sql.AppendLine(" ) ");
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            return dt;
        }
        #endregion
    }
}
