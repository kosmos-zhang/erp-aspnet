/**********************************************
 * 类作用：   仓库物品数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/15
 ***********************************************/

using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;


namespace XBase.Data.Office.StorageManager
{
    public class StorageProductDBHelper
    {
        #region 查询：仓库物品
        /// <summary>
        /// 工艺档案
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageProductTableBycondition(StorageProductModel model)
        {
            string sql = "select * from officedba.StorageProduct where 1=1";
            if (int.Parse(model.ProductID.ToString()) > 0)
            {
                sql += " and ProductID='" + model.ProductID + "'";
            }
            if (model.StorageNo != "")
                sql += " and StorageNo='" + model.StorageNo + "'";
            //if (!string.IsNullOrEmpty( model.CostPrice.ToString())
            //{
            //    sql += " and CostPrice='" + model.CostPrice + "'";
            //}
            if (!string.IsNullOrEmpty(model.ProductCount.ToString()))
            {
                sql += " and ProductCount='" + model.ProductCount + "'";
            }

            return SqlHelper.ExecuteSql(sql);
        }
        #endregion


        #region 查看：物品库存
        /// <summary>
        /// 获取工艺档案详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageProductDetailInfo(string CompanyCD, int ID)
        {
            string sql = "select * from officedba.StorageProduct where CompanyCD='" + CompanyCD + "' and ID=" + ID;
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 添加：物品库存
        /// <summary>
        /// 添加工艺档案记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool InsertStorageProduct(StorageProductModel model, string loginUserID)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.StorageProduct");
            sql.AppendLine("	    (CompanyCD      ");
            sql.AppendLine("		,StorageNo         ");
            sql.AppendLine("		,StorageID       ");
            sql.AppendLine("		,ProductID        ");
            sql.AppendLine("		,CostPrice    ");
            sql.AppendLine("		,ProductCount     ");
            sql.AppendLine("		,LockCount        ");
            sql.AppendLine("		,Remark     ");
            sql.AppendLine("		,ModifiedDate         ");
            sql.AppendLine("		,ModifiedUserID   ");
            sql.AppendLine("VALUES                  ");
            sql.AppendLine("		(@CompanyCD     ");
            sql.AppendLine("		,@StorageNo        ");
            sql.AppendLine("		,@StorageID      ");
            sql.AppendLine("		,@ProductID       ");
            //sql.AppendLine("		,@CostPrice   ");
            sql.AppendLine("		,@ProductCount    ");
            //sql.AppendLine("		,@LockCount       ");
            sql.AppendLine("		,@Remark        ");
            sql.AppendLine("		,getdate()      ");
            sql.AppendLine("		,'" + loginUserID + "')       ");

            //设置参数
            SqlParameter[] param = new SqlParameter[8];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@StorageNo", model.StorageNo);
            param[2] = SqlHelper.GetParameter("@StorageID", model.StorageID);
            param[3] = SqlHelper.GetParameter("@ProductID", model.ProductID);
            //param[4] = SqlHelper.GetParameter("@CostPrice", model.CostPrice);
            param[4] = SqlHelper.GetParameter("@ProductCount", model.ProductCount);
            //param[6] = SqlHelper.GetParameter("@LockCount", model.LockCount);
            param[5] = SqlHelper.GetParameter("@Remark", model.Remark);


            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        #endregion

        #region 修改：仓库物品
        /// <summary>
        /// 更新工艺档案记录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>                                        
        /// <returns></returns>
        public static bool UpdateStorageProduct(StorageProductModel model, string loginUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageProduct SET");
            sql.AppendLine(" StorageNo         = @StorageNo,");
            sql.AppendLine(" StorageID       = @StorageID,");
            sql.AppendLine(" ProductID        = @ProductID,");
            //sql.AppendLine(" CostPrice    = @CostPrice,");
            sql.AppendLine(" ProductCount     = @ProductCount,");
            //sql.AppendLine(" LockCount         = @LockCount,");
            sql.AppendLine(" Remark         = @Remark,");
            sql.AppendLine(" ModifiedDate   = getdate(),");
            sql.AppendLine(" ModifiedUserID = '" + loginUserID + "' ");
            sql.AppendLine(" Where CompanyCD=@CompanyCD and ID=@ID");


            SqlParameter[] param = new SqlParameter[9];
            param[0] = SqlHelper.GetParameter("@StorageNo", model.StorageNo);
            param[1] = SqlHelper.GetParameter("@StorageID", model.StorageID);
            param[2] = SqlHelper.GetParameter("@ProductID", model.ProductID);
            //param[3] = SqlHelper.GetParameter("@CostPrice", model.CostPrice);
            param[3] = SqlHelper.GetParameter("@ProductCount", model.ProductCount);
            // param[5] = SqlHelper.GetParameter("@LockCount", model.LockCount);
            param[4] = SqlHelper.GetParameter("@Remark", model.Remark);
            param[5] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[6] = SqlHelper.GetParameter("@ID", model.ID);

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion
    }
}
