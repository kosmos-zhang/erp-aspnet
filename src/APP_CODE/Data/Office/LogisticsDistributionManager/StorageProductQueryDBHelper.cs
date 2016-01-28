using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using XBase.Data.DBHelper;
using XBase.Common;
using XBase.Model.Office.LogisticsDistributionManager;

namespace XBase.Data.Office.LogisticsDistributionManager
{
    public class StorageProductQueryDBHelper
    {

        #region 读取分店存量表
        public static DataTable GetSubStorageProductList(int PageIndex, int PageSize, string OrderBy, Hashtable htPara, ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT ssp.*,( SELECT di.DeptName FROM officedba.DeptInfo as di  WHERE di.ID=ssp.DeptID )  as DetpName, ");
            sbSql.Append(" pi.ProductName ,pi.ProdNo,pi.Specification,(select ct.CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName  ");
            sbSql.Append(" FROM officedba.SubStorageProduct as ssp  inner join officedba.ProductInfo as pi on  ssp.ProductID=pi.ID ");
            sbSql.Append(" where  ssp.CompanyCD=@CompanyCD ");
            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            Paras[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[0].Value = htPara["@CompanyCD"];
            int index = 1;
            if (htPara.ContainsKey("@DeptID"))
            {
                sbSql.Append(" AND ssp.DeptID=@DeptID ");
                Paras[index] = new SqlParameter("@DeptID", SqlDbType.Int);
                Paras[index].Value = htPara["@DeptID"];
                index++;
            }
            if (htPara.ContainsKey("@ProdNo"))
            {
                sbSql.Append(" AND pi.ProdNo=@ProdNo");
                Paras[index] = new SqlParameter("@ProdNo", SqlDbType.VarChar);
                Paras[index].Value = htPara["@ProdNo"];
                index++;
            }
            if (htPara.ContainsKey("@ProductName"))
            {
                sbSql.Append(" AND pi.ProductName LIKE @ProductName ");
                Paras[index] = new SqlParameter("@ProductName", SqlDbType.VarChar);
                Paras[index].Value = htPara["@ProductName"];
                index++;
            }

            if (htPara.ContainsKey("@BatchNo"))
            {
                sbSql.Append(" AND ssp.BatchNo LIKE @BatchNo ");
                Paras[index] = new SqlParameter("@BatchNo", SqlDbType.VarChar);
                Paras[index].Value = htPara["@BatchNo"];
                index++;
            }
            if (htPara.ContainsKey("@BarCode"))
            {
                sbSql.Append(" AND pi.BarCode=@BarCode ");
                Paras[index++] = SqlHelper.GetParameter("@BarCode", htPara["@BarCode"]);
            }

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);


        }

        /*不分页*/
        public static DataTable GetSubStorageProductList(string OrderBy, Hashtable htPara)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT ssp.*,( SELECT di.DeptName FROM officedba.DeptInfo as di  WHERE di.ID=ssp.DeptID )  as DetpName, ");
            sbSql.Append(" pi.ProductName ,pi.ProdNo,pi.Specification,(select ct.CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName  ");
            sbSql.Append(" FROM officedba.SubStorageProduct as ssp  inner join officedba.ProductInfo as pi on  ssp.ProductID=pi.ID ");
            sbSql.Append(" where  ssp.CompanyCD=@CompanyCD ");
            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            Paras[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[0].Value = htPara["@CompanyCD"];
            int index = 1;
            if (htPara.ContainsKey("@DeptID"))
            {
                sbSql.Append(" AND ssp.DeptID=@DeptID ");
                Paras[index] = new SqlParameter("@DeptID", SqlDbType.Int);
                Paras[index].Value = htPara["@DeptID"];
                index++;
            }
            if (htPara.ContainsKey("@ProdNo"))
            {
                sbSql.Append(" AND pi.ProdNo=@ProdNo");
                Paras[index] = new SqlParameter("@ProdNo", SqlDbType.VarChar);
                Paras[index].Value = htPara["@ProdNo"];
                index++;
            }
            if (htPara.ContainsKey("@ProductName"))
            {
                sbSql.Append(" AND pi.ProductName LIKE @ProductName ");
                Paras[index] = new SqlParameter("@ProductName", SqlDbType.VarChar);
                Paras[index].Value = htPara["@ProductName"];
                index++;
            }
            if (htPara.ContainsKey("@BarCode"))
            {
                sbSql.Append(" AND pi.BarCode =@BarCode ");
                Paras[index++] = SqlHelper.GetParameter("@BarCode", htPara["@BarCode"]);
            }
            sbSql.Append(" ORDER BY " + OrderBy);
            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
            //return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);


        }
        #endregion

        #region 读取分仓存量表
        public static DataTable GetStorageProductList(int PageIndex, int PageSize, string OrderBy, Hashtable htPara, ref int TotalCount)
        {

            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            int index = 0;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index].Value = htPara["@CompanyCD"];
            index++;

            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" SELECT sp.* , ");
            sbSql.Append(" pi.ProductName ,pi.ProdNo,pi.Specification,(select ct.CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName ,");
            sbSql.Append(" si.StorageName,si.StorageNo ,");
            sbSql.Append(" (isnull(sp.ProductCount,0)+isnull(sp.InCount,0)+isnull(sp.RoadCount,0)-isnull(sp.OutCount,0)-isnull(sp.OrderCount,0)) as UseCount");
            sbSql.Append("  FROM officedba.StorageProduct as sp inner join officedba.ProductInfo as pi on sp.ProductID = pi.ID  inner join officedba.StorageInfo as si on sp.StorageID=si.ID  ");
            sbSql.Append(" WHERE sp.CompanyCD=@CompanyCD ");
            if (htPara.ContainsKey("@ProdNo"))
            {
                sbSql.Append(" AND pi.ProdNo=@ProdNo");
                Paras[index] = new SqlParameter("@ProdNo", SqlDbType.VarChar);
                Paras[index].Value = htPara["@ProdNo"];
                index++;
            }
            if (htPara.ContainsKey("@ProductName"))
            {
                sbSql.Append(" AND pi.ProductName LIKE @ProductName ");
                Paras[index] = new SqlParameter("@ProductName", SqlDbType.VarChar);
                Paras[index].Value = htPara["@ProductName"];
                index++;
            }
            if (htPara.ContainsKey("@BatchNo"))
            {
                sbSql.Append(" AND sp.BatchNo LIKE @BatchNo ");
                Paras[index] = new SqlParameter("@BatchNo", SqlDbType.VarChar);
                Paras[index].Value = htPara["@BatchNo"];
                index++;
            }
            if (htPara.ContainsKey("@StorageID"))
            {
                sbSql.Append(" AND si.ID=@StorageID ");
                Paras[index] = new SqlParameter("@StorageID", SqlDbType.Int);
                Paras[index].Value = htPara["@StorageID"];
                index++;
            }
            if (htPara.ContainsKey("@BarCode"))
            {
                sbSql.Append(" AND pi.BarCode=@BarCode ");
                Paras[index++] = SqlHelper.GetParameter("@BarCode", htPara["@BarCode"]);
            }


            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);
        }


        /*不分页*/
        public static DataTable GetStorageProductList(string OrderBy, Hashtable htPara)
        {

            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            int index = 0;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index].Value = htPara["@CompanyCD"];
            index++;

            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" SELECT sp.* , ");
            sbSql.Append(" pi.ProductName ,pi.ProdNo,pi.Specification,(select ct.CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName ,");
            sbSql.Append(" si.StorageName,si.StorageNo ,");
            sbSql.Append(" (isnull(sp.ProductCount,0)+isnull(sp.InCount,0)+isnull(sp.RoadCount,0)-isnull(sp.OutCount,0)-isnull(sp.OrderCount,0)) as UseCount");
            sbSql.Append("  FROM officedba.StorageProduct as sp inner join officedba.ProductInfo as pi on sp.ProductID = pi.ID  inner join officedba.StorageInfo as si on sp.StorageID=si.ID  ");
            sbSql.Append(" WHERE sp.CompanyCD=@CompanyCD ");
            if (htPara.ContainsKey("@ProdNo"))
            {
                sbSql.Append(" AND pi.ProdNo=@ProdNo");
                Paras[index] = new SqlParameter("@ProdNo", SqlDbType.VarChar);
                Paras[index].Value = htPara["@ProdNo"];
                index++;
            }
            if (htPara.ContainsKey("@ProductName"))
            {
                sbSql.Append(" AND pi.ProductName LIKE @ProductName ");
                Paras[index] = new SqlParameter("@ProductName", SqlDbType.VarChar);
                Paras[index].Value = htPara["@ProductName"];
                index++;
            }
            if (htPara.ContainsKey("@StorageID"))
            {
                sbSql.Append(" AND si.ID=@StorageID ");
                Paras[index] = new SqlParameter("@StorageID", SqlDbType.Int);
                Paras[index].Value = htPara["@StorageID"];
                index++;
            }
            if (htPara.ContainsKey("@BarCode"))
            {
                sbSql.Append(" AND pi.BarCode =@BarCode ");
                Paras[index++] = SqlHelper.GetParameter("@BarCode", htPara["@BarCode"]);

            }

            sbSql.Append(" ORDER BY " + OrderBy);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
            // return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);
        }
        #endregion

        /// <summary>
        /// 读取分店存量表物品批次
        /// </summary>
        /// <param name="CompanyCD">机构</param>
        /// <param name="ProductID">物品ID</param>
        /// <param name="DeptID">分店ID</param>
        /// <returns></returns>
        public static DataTable GetSubBatchNo(string CompanyCD, int ProductID, int DeptID)
        {
            string sqlStr = @"SELECT DISTINCT ssp.BatchNo
FROM officedba.SubStorageProduct ssp
WHERE ssp.CompanyCD=@CompanyCD AND ssp.ProductID=@ProductID ";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CompanyCD", CompanyCD));
            para.Add(new SqlParameter("@ProductID", ProductID));
            para.Add(new SqlParameter("@DeptID", DeptID));

            return SqlHelper.ExecuteSql(sqlStr, para.ToArray());
        }

        /// <summary>
        /// 更新分店库存存量表
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="ProductID">产品ID</param>
        /// <param name="DeptID">部门ID</param>
        /// <param name="BatchNo">批次</param>
        /// <param name="ProductCount">数量(正数为入库，负数为出库)</param>
        /// <returns></returns>
        public static SqlCommand UpdateProductCount(string CompanyCD, string ProductID, string DeptID, string BatchNo, decimal ProductCount)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            bool isIn = false;
            DataTable dt = new DataTable();
            if (ProductCount >= 0)
            {// 入库数量为正数
                dt = GetSubProductCount(CompanyCD, ProductID, DeptID, BatchNo);
                isIn = dt.Rows.Count < 1;
            }
            if (isIn)
            {
                strSql.Append("insert into officedba.SubStorageProduct(");
                strSql.Append("CompanyCD");
                strSql.Append(",ProductID");
                strSql.Append(",DeptID");
                strSql.Append(",ProductCount");
                if (!String.IsNullOrEmpty(BatchNo))
                {
                    strSql.Append(",BatchNo");
                }
                strSql.Append(")");
                strSql.Append(" values (");
                strSql.Append("@CompanyCD");
                strSql.Append(",@ProductID");
                strSql.Append(",@DeptID");
                strSql.Append(",@ProductCount");
                if (!String.IsNullOrEmpty(BatchNo))
                {
                    strSql.Append(",@BatchNo");
                }
                strSql.Append(")");
            }
            else
            {
                strSql.Append("update officedba.SubStorageProduct set ");
                strSql.Append("ProductCount=(isnull(ProductCount,0)+@ProductCount)");
                strSql.Append(" where CompanyCD=@CompanyCD ");
                strSql.Append(" AND ProductID=@ProductID");
                strSql.Append(" AND DeptID=@DeptID");
                if (!String.IsNullOrEmpty(BatchNo))
                {
                    strSql.Append(" AND BatchNo=@BatchNo");
                }
                else
                {
                    strSql.Append(" AND BatchNo IS NULL");
                }
            }
            if (!String.IsNullOrEmpty(BatchNo))
            {
                SqlParameter[] parameters = {
					        new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					        new SqlParameter("@ProductID", SqlDbType.Int,4),
					        new SqlParameter("@DeptID", SqlDbType.Int,4),
					        new SqlParameter("@ProductCount", SqlDbType.Decimal,9),
					        new SqlParameter("@BatchNo", SqlDbType.VarChar,50)
                                        };
                parameters[0].Value = CompanyCD;
                parameters[1].Value = ProductID;
                parameters[2].Value = DeptID;
                parameters[3].Value = ProductCount;
                parameters[4].Value = BatchNo;
                cmd.CommandText = strSql.ToString();
                cmd.Parameters.AddRange(parameters);
            }
            else
            {
                SqlParameter[] parameters = {
					        new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					        new SqlParameter("@ProductID", SqlDbType.Int,4),
					        new SqlParameter("@DeptID", SqlDbType.Int,4),
					        new SqlParameter("@ProductCount", SqlDbType.Decimal,9)};
                parameters[0].Value = CompanyCD;
                parameters[1].Value = ProductID;
                parameters[2].Value = DeptID;
                parameters[3].Value = ProductCount;
                cmd.CommandText = strSql.ToString();
                cmd.Parameters.AddRange(parameters);
            }


            return cmd;
        }

        /// <summary>
        /// 获得分店库存
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="ProductID">产品代码</param>
        /// <param name="DeptID">部门代码</param>
        /// <param name="BatchNo">批次</param>
        /// <returns></returns>
        public static DataTable GetSubProductCount(string CompanyCD, string ProductID, string DeptID, string BatchNo)
        {
            SqlParameter[] subPara = { 
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                          new SqlParameter("@ProductID",SqlDbType.Int),
                                          new SqlParameter("@DeptID",SqlDbType.Int),
                                          new SqlParameter("@BatchNo",SqlDbType.VarChar),};
            subPara[0].Value = CompanyCD;
            subPara[1].Value = ProductID;
            subPara[2].Value = DeptID;
            subPara[3].Value = BatchNo;
            return SqlHelper.ExecuteSql(@"SELECT * FROM officedba.SubStorageProduct 
                                WHERE ProductID=@ProductID AND DeptID=@DeptID AND CompanyCD=@CompanyCD AND ISNULL(BatchNo,'')=@BatchNo ", subPara);

        }


    }
}
