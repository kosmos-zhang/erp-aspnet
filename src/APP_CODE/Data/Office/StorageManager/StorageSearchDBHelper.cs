/**********************************************
 * 类作用：   报损数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/29
 ***********************************************/


using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using System.Collections.Generic;
using System.Collections;


namespace XBase.Data.Office.StorageManager
{
    /// <summary>
    /// 库存处理
    /// </summary>
    public class StorageSearchDBHelper
    {
        #region 查询：库存查询
        /// <summary>
        /// 查询库存报损单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetProductStorageTableBycondition(StorageProductModel model, XBase.Model.Office.SupplyChain.ProductInfoModel pdtModel, string ProductCount1, string EFIndex, string EFDesc, int pageIndex, int pageCount, string ord, string BatchNo, ref int TotalCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine("SELECT d.ExtField" + EFIndex + ",a.ID                                           ");
            }
            else
            {
                sql.AppendLine("SELECT a.ID                                           ");
            }
            sql.AppendLine("			,ISNULL(b.StorageNo,'') as StorageNo ,isnull(a.BatchNo,'') as BatchNo        ");
            sql.AppendLine("			,ISNULL(b.StorageName,'') as StorageName        ");
            sql.AppendLine("			,ISNULL(d.ProdNo,'') as ProductNo               ");
            sql.AppendLine("			,ISNULL(d.ProductName,'') as ProductName        ");
            sql.AppendLine("			,ISNULL(d.Specification,'') as Specification    ");
            sql.AppendLine("			,ISNULL(e.CodeName,'') as UnitID                ");
            sql.AppendLine("			,ISNULL(c.DeptName,'') as DeptName              ");
            sql.AppendLine("			,ISNULL(a.ProductCount,0) as ProductCount       ");
            sql.AppendLine("			,ISNULL(a.ProductCount,0)+ISNULL(a.RoadCount,0)+ISNULL(a.InCount,0)-ISNULL(a.OrderCount,0)-ISNULL(a.OutCount,0) as UseCount       ");
            sql.AppendLine("			,ISNULL(a.OrderCount,0) as OrderCount       ");
            sql.AppendLine("			,ISNULL(a.RoadCount,0) as RoadCount       ");
            sql.AppendLine("			,ISNULL(a.OutCount,0) as OutCount,a.ProductID,g.TypeName as ColorName  ");
            sql.AppendLine("FROM officedba.StorageProduct a                       ");
            sql.AppendLine("left join officedba.StorageInfo b on a.StorageID=b.ID ");
            sql.AppendLine("left join officedba.DeptInfo c on c.ID=a.DeptID       ");
            sql.AppendLine("left join officedba.ProductInfo d on d.ID=a.ProductID ");
            sql.AppendLine("left join officedba.CodeUnitType e on e.ID=d.UnitID	  left outer join officedba.CodePublicType g on d.ColorID=g.ID 	");
            sql.AppendLine("   where a.CompanyCD='" + model.CompanyCD + "'");

            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("	and a.StorageID = @StorageID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            else
            {
                string ListID = StorageDBHelper.GetStorageIDStr(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, model.CompanyCD);
                sql.AppendLine(" and a.StorageID  in(" + ListID + ")");
            }
            if (!string.IsNullOrEmpty(model.ProductCount))
            {
                sql.AppendLine(" and a.ProductCount >= @ProductCount ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount", model.ProductCount));
            }
            if (!string.IsNullOrEmpty(ProductCount1))
            {
                sql.AppendLine(" and a.ProductCount <= @ProductCount1 ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount1", ProductCount1));
            }
            if (!string.IsNullOrEmpty(pdtModel.ProdNo))
            {
                sql.AppendLine(" and d.ProdNo=@ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", pdtModel.ProdNo));
            }
            if (!string.IsNullOrEmpty(pdtModel.ProductName))
            {
                sql.AppendLine(" and d.ProductName like '%' + @ProductName + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", pdtModel.ProductName));
            }

            if (!string.IsNullOrEmpty(pdtModel.ColorID))
            {
                sql.AppendLine(" and d.ColorID =@ColorID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", pdtModel.ColorID));
            }

            if (!string.IsNullOrEmpty(pdtModel.TypeID))
            {
                sql.AppendLine(" and d.TypeID =@TypeID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", pdtModel.TypeID));
            }

            if (!string.IsNullOrEmpty(pdtModel.BarCode))
            {
                sql.AppendLine(" and d.BarCode like '%' + @BarCode + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", pdtModel.BarCode));
            }
            if (!string.IsNullOrEmpty(pdtModel.Manufacturer))
            {
                sql.AppendLine(" and d.Manufacturer like '%' + @Manufacturer + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", pdtModel.Manufacturer));
            }
            if (!string.IsNullOrEmpty(pdtModel.Specification))
            {
                sql.AppendLine(" and d.Specification like '%' + @Specification + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", pdtModel.Specification));
            }
            if (!string.IsNullOrEmpty(pdtModel.FromAddr))
            {
                sql.AppendLine(" and d.FromAddr like '%' + @FromAddr + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", pdtModel.FromAddr));
            }
            if (!string.IsNullOrEmpty(pdtModel.Material))
            {
                sql.AppendLine(" and d.Material=@Material ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", pdtModel.Material));
            }
            ////过滤单据：显示当前用户拥有权限查看的单据
            //int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            //sql.AppendLine(" and ( charindex('," + empid + ",' , ','+b.CanViewUser+',')>0 or b.StorageAdmin=" + empid + " OR b.CanViewUser='' OR b.CanViewUser is null) ");

            //扩展属性
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and d.ExtField" + EFIndex + " like @EFDesc ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }

            if (BatchNo != "0")
            {
                if (BatchNo == "未设置批次")
                {
                    sql.AppendLine(" and (a.BatchNo is null or a.BatchNo='') ");
                }
                else
                {
                    sql.AppendLine(" and a.BatchNo=@BatchNo ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
                }
            }


            string ResulSQL = "select a.*,ProductCount/ExRate as StoreCount from ( select a.*,isnull(h.ExRate,1) as ExRate,isnull(j.CodeName,'') as CodeName from (" + sql.ToString() + ") a left outer join officedba.ProductInfo g on a.ProductID=g.ID left outer join Officedba.UnitGroup p on (g.GroupUnitNo=p.GroupUnitNo and p.CompanyCD='" + model.CompanyCD + "') LEFT OUTER JOIN Officedba.UnitGroupDetail h  ON (p.GroupUnitNo=h.GroupUnitNo and h.CompanyCD='" + model.CompanyCD + "' and g.StockUnitID=h.UnitID)  left outer  join officedba.CodeUnitType j on h.UnitID=j.ID  ) a ";

            comm.CommandText = ResulSQL;
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }


        public static DataTable GetProductStorageTableBycondition(StorageProductModel model, XBase.Model.Office.SupplyChain.ProductInfoModel pdtModel, string ProductCount1, string EFIndex, string EFDesc, string orderby, string BatchNo)
        {
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine("SELECT d.ExtField" + EFIndex + ",a.ID                                           ");
            }
            else
            {
                sql.AppendLine("SELECT a.ID                                           ");
            }
            sql.AppendLine("			,ISNULL(b.StorageNo,'') as StorageNo,isnull(a.BatchNo,'') as BatchNo        ");
            sql.AppendLine("			,ISNULL(b.StorageName,'') as StorageName        ");
            sql.AppendLine("			,ISNULL(d.ProdNo,'') as ProductNo               ");
            sql.AppendLine("			,ISNULL(d.ProductName,'') as ProductName        ");
            sql.AppendLine("			,ISNULL(d.Specification,'') as Specification    ");
            sql.AppendLine("			,ISNULL(e.CodeName,'') as UnitID                ");
            sql.AppendLine("			,ISNULL(c.DeptName,'') as DeptName              ");
            sql.AppendLine("			,ISNULL(a.ProductCount,0) as ProductCount       ");
            sql.AppendLine("			,ISNULL(a.ProductCount,0)+ISNULL(a.RoadCount,0)+ISNULL(a.InCount,0)-ISNULL(a.OrderCount,0)-ISNULL(a.OutCount,0) as UseCount       ");
            sql.AppendLine("			,ISNULL(a.OrderCount,0) as OrderCount       ");
            sql.AppendLine("			,ISNULL(a.RoadCount,0) as RoadCount       ");
            sql.AppendLine("			,ISNULL(a.OutCount,0) as OutCount,a.ProductID,g.TypeName as ColorName       ");
            sql.AppendLine("FROM officedba.StorageProduct a                       ");
            sql.AppendLine("left join officedba.StorageInfo b on a.StorageID=b.ID ");
            sql.AppendLine("left join officedba.DeptInfo c on c.ID=a.DeptID       ");
            sql.AppendLine("left join officedba.ProductInfo d on d.ID=a.ProductID ");
            sql.AppendLine("left join officedba.CodeUnitType e on e.ID=d.UnitID	  left outer join officedba.CodePublicType g on d.ColorID=g.ID 	");
            sql.AppendLine("   where a.CompanyCD='" + model.CompanyCD + "'");

            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("	and a.StorageID = @StorageID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            else
            {
                string ListID = StorageDBHelper.GetStorageIDStr(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, model.CompanyCD);
                sql.AppendLine(" and a.StorageID  in(" + ListID + ")");
            }
            if (!string.IsNullOrEmpty(model.ProductCount))
            {
                sql.AppendLine(" and a.ProductCount >= @ProductCount ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount", model.ProductCount));
            }
            if (!string.IsNullOrEmpty(ProductCount1))
            {
                sql.AppendLine(" and a.ProductCount <= @ProductCount1 ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount1", ProductCount1));
            }
            if (!string.IsNullOrEmpty(pdtModel.ProdNo))
            {
                sql.AppendLine(" and d.ProdNo=@ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", pdtModel.ProdNo));
            }

            if (!string.IsNullOrEmpty(pdtModel.ColorID))
            {
                sql.AppendLine(" and d.ColorID =@ColorID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", pdtModel.ColorID));
            }

            if (!string.IsNullOrEmpty(pdtModel.TypeID))
            {
                sql.AppendLine(" and d.TypeID =@TypeID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", pdtModel.TypeID));
            }

            if (!string.IsNullOrEmpty(pdtModel.ProductName))
            {
                sql.AppendLine(" and d.ProductName like '%' + @ProductName + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", pdtModel.ProductName));
            }
            if (!string.IsNullOrEmpty(pdtModel.BarCode))
            {
                sql.AppendLine(" and d.BarCode like '%' + @BarCode + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", pdtModel.BarCode));
            }
            if (!string.IsNullOrEmpty(pdtModel.Manufacturer))
            {
                sql.AppendLine(" and d.Manufacturer like '%' + @Manufacturer + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", pdtModel.Manufacturer));
            }
            if (!string.IsNullOrEmpty(pdtModel.Specification))
            {
                sql.AppendLine(" and d.Specification like '%' + @Specification + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", pdtModel.Specification));
            }
            if (!string.IsNullOrEmpty(pdtModel.FromAddr))
            {
                sql.AppendLine(" and d.FromAddr like '%' + @FromAddr + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", pdtModel.FromAddr));
            }
            if (!string.IsNullOrEmpty(pdtModel.Material))
            {
                sql.AppendLine(" and d.Material=@Material + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", pdtModel.Material));
            }
            ////过滤单据：显示当前用户拥有权限查看的单据
            //int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            //sql.AppendLine(" and ( charindex('," + empid + ",' , ','+b.CanViewUser+',')>0 or b.StorageAdmin=" + empid + " OR b.CanViewUser='' OR b.CanViewUser is null) ");

            //扩展属性
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and d.ExtField" + EFIndex + " like @EFDesc ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }

            if (BatchNo != "0")
            {
                if (BatchNo == "未设置批次")
                {
                    sql.AppendLine(" and (a.BatchNo is null or a.BatchNo='') ");
                }
                else
                {
                    sql.AppendLine(" and a.BatchNo=@BatchNo ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
                }
            }


            string ResulSQL = "select a.*,ProductCount/ExRate as StoreCount from ( select a.*,isnull(h.ExRate,1) as ExRate,isnull(j.CodeName,'') as CodeName from (" + sql.ToString() + ") a left outer join officedba.ProductInfo g on a.ProductID=g.ID left outer join Officedba.UnitGroup p on (g.GroupUnitNo=p.GroupUnitNo and p.CompanyCD='" + model.CompanyCD + "') LEFT OUTER JOIN Officedba.UnitGroupDetail h  ON (p.GroupUnitNo=h.GroupUnitNo and h.CompanyCD='" + model.CompanyCD + "' and g.StockUnitID=h.UnitID)  left outer  join officedba.CodeUnitType j on h.UnitID=j.ID  ) a ";

            comm.CommandText = ResulSQL;
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion



        /// <summary>
        /// 现有存量汇总
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pdtModel"></param>
        /// <param name="ProductCount1"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="orderby"></param>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public static string GetSumStorageInfo(StorageProductModel model, XBase.Model.Office.SupplyChain.ProductInfoModel pdtModel, string ProductCount1, string EFIndex, string EFDesc, string orderby, string BatchNo)
        {
            string rev = string.Empty;



            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine("SELECT d.ExtField" + EFIndex + ",a.ID                                           ");
            }
            else
            {
                sql.AppendLine("SELECT a.ID                                           ");
            }
            sql.AppendLine("			,ISNULL(b.StorageNo,'') as StorageNo,isnull(a.BatchNo,'') as BatchNo        ");
            sql.AppendLine("			,ISNULL(b.StorageName,'') as StorageName        ");
            sql.AppendLine("			,ISNULL(d.ProdNo,'') as ProductNo               ");
            sql.AppendLine("			,ISNULL(d.ProductName,'') as ProductName        ");
            sql.AppendLine("			,ISNULL(d.Specification,'') as Specification    ");
            sql.AppendLine("			,ISNULL(e.CodeName,'') as UnitID                ");
            sql.AppendLine("			,ISNULL(c.DeptName,'') as DeptName              ");
            sql.AppendLine("			,ISNULL(a.ProductCount,0) as ProductCount       ");
            sql.AppendLine("			,ISNULL(a.ProductCount,0)+ISNULL(a.RoadCount,0)+ISNULL(a.InCount,0)-ISNULL(a.OrderCount,0)-ISNULL(a.OutCount,0) as UseCount       ");
            sql.AppendLine("			,ISNULL(a.OrderCount,0) as OrderCount       ");
            sql.AppendLine("			,ISNULL(a.RoadCount,0) as RoadCount       ");
            sql.AppendLine("			,ISNULL(a.OutCount,0) as OutCount,a.ProductID,g.TypeName as ColorName       ");
            sql.AppendLine("FROM officedba.StorageProduct a                       ");
            sql.AppendLine("left join officedba.StorageInfo b on a.StorageID=b.ID ");
            sql.AppendLine("left join officedba.DeptInfo c on c.ID=a.DeptID       ");
            sql.AppendLine("left join officedba.ProductInfo d on d.ID=a.ProductID ");
            sql.AppendLine("left join officedba.CodeUnitType e on e.ID=d.UnitID	  left outer join officedba.CodePublicType g on d.ColorID=g.ID 	");
            sql.AppendLine("   where a.CompanyCD='" + model.CompanyCD + "'");

            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("	and a.StorageID = @StorageID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            else
            {
                string ListID = StorageDBHelper.GetStorageIDStr(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, model.CompanyCD);
                sql.AppendLine(" and a.StorageID  in(" + ListID + ")");
            }
            if (!string.IsNullOrEmpty(model.ProductCount))
            {
                sql.AppendLine(" and a.ProductCount >= @ProductCount ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount", model.ProductCount));
            }
            if (!string.IsNullOrEmpty(ProductCount1))
            {
                sql.AppendLine(" and a.ProductCount <= @ProductCount1 ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount1", ProductCount1));
            }
            if (!string.IsNullOrEmpty(pdtModel.ProdNo))
            {
                sql.AppendLine(" and d.ProdNo=@ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", pdtModel.ProdNo));
            }

            if (!string.IsNullOrEmpty(pdtModel.ColorID))
            {
                sql.AppendLine(" and d.ColorID =@ColorID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", pdtModel.ColorID));
            }
            if (!string.IsNullOrEmpty(pdtModel.TypeID))
            {
                sql.AppendLine(" and d.TypeID =@TypeID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", pdtModel.TypeID));
            }
            if (!string.IsNullOrEmpty(pdtModel.ProductName))
            {
                sql.AppendLine(" and d.ProductName like '%' + @ProductName + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", pdtModel.ProductName));
            }
            if (!string.IsNullOrEmpty(pdtModel.BarCode))
            {
                sql.AppendLine(" and d.BarCode like '%' + @BarCode + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", pdtModel.BarCode));
            }
            if (!string.IsNullOrEmpty(pdtModel.Manufacturer))
            {
                sql.AppendLine(" and d.Manufacturer like '%' + @Manufacturer + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", pdtModel.Manufacturer));
            }
            if (!string.IsNullOrEmpty(pdtModel.Specification))
            {
                sql.AppendLine(" and d.Specification like '%' + @Specification + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", pdtModel.Specification));
            }
            if (!string.IsNullOrEmpty(pdtModel.FromAddr))
            {
                sql.AppendLine(" and d.FromAddr like '%' + @FromAddr + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", pdtModel.FromAddr));
            }
            if (!string.IsNullOrEmpty(pdtModel.Material))
            {
                sql.AppendLine(" and d.Material=@Material + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", pdtModel.Material));
            }
            ////过滤单据：显示当前用户拥有权限查看的单据
            //int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            //sql.AppendLine(" and ( charindex('," + empid + ",' , ','+b.CanViewUser+',')>0 or b.StorageAdmin=" + empid + " OR b.CanViewUser='' OR b.CanViewUser is null) ");

            //扩展属性
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and d.ExtField" + EFIndex + " like @EFDesc ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }

            if (BatchNo != "0")
            {
                if (BatchNo == "未设置批次")
                {
                    sql.AppendLine(" and (a.BatchNo is null or a.BatchNo='') ");
                }
                else
                {
                    sql.AppendLine(" and a.BatchNo=@BatchNo ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
                }
            }


            string ResulSQL = "select sum(isnull(StoreCount,0)) as StoreCount,sum(isnull(ProductCount,0)) as ProductCount from (  select a.*,ProductCount/ExRate as StoreCount from ( select a.*,isnull(h.ExRate,1) as ExRate,isnull(j.CodeName,'') as CodeName from (" + sql.ToString() + ") a left outer join officedba.ProductInfo g on a.ProductID=g.ID left outer join Officedba.UnitGroup p on (g.GroupUnitNo=p.GroupUnitNo and p.CompanyCD='" + model.CompanyCD + "') LEFT OUTER JOIN Officedba.UnitGroupDetail h  ON (p.GroupUnitNo=h.GroupUnitNo and h.CompanyCD='" + model.CompanyCD + "' and g.StockUnitID=h.UnitID)  left outer  join officedba.CodeUnitType j on h.UnitID=j.ID  ) a ) a ";

            comm.CommandText = ResulSQL;
            DataTable dt = SqlHelper.ExecuteSearch(comm);

            if (dt.Rows.Count > 0)
            {
                rev = dt.Rows[0]["StoreCount"].ToString() + "|" + dt.Rows[0]["ProductCount"].ToString();
            }

            return rev;
        }

        /// <summary>
        /// 获取对应的批次
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ProductNo"></param>
        /// <param name="StorageID"></param>
        /// <returns></returns>
        public static string GetBatchNo(string CompanyCD, string ProductNo, string StorageID)
        {
            string rev = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  distinct  isnull(BatchNo,'未设置批次') as BatchNo           ");
            sql.AppendLine("FROM officedba.StorageProduct a    ");
            sql.AppendLine("left join officedba.ProductInfo b on b.ID=a.ProductID ");
            sql.AppendLine("   where a.CompanyCD=@CompanyCD");

            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            if (!string.IsNullOrEmpty(StorageID))
            {
                sql.AppendLine("	and a.StorageID=@StorageID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", StorageID));
            }

            if (!string.IsNullOrEmpty(ProductNo))
            {
                sql.AppendLine(" and b.ProdNo=@ProductNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", ProductNo));
            }
            comm.CommandText = sql.ToString();
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            foreach (DataRow row in dt.Rows)
            {
                string res = row["BatchNo"].ToString();
                rev += res + ",";
            }
            return rev.TrimEnd(new char[] { ',' });
        }

        /// <summary>
        /// 更新库存存量表
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="ProductID">产品ID</param>
        /// <param name="StorageID">仓库ID</param>
        /// <param name="BatchNo">批次</param>
        /// <param name="ProductCount">数量(正数为入库，负数为出库)</param>
        /// <returns></returns>
        public static SqlCommand UpdateProductCount(string CompanyCD, string ProductID, string StorageID, string BatchNo, decimal ProductCount)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            bool isIn = false;
            DataTable dt = new DataTable();
            if (ProductCount > 0)
            {// 入库数量为正数
                dt = GetProductCount(CompanyCD, ProductID, StorageID, BatchNo);
                isIn = dt.Rows.Count < 1;
            }
            if (isIn)
            {
                strSql.Append("INSERT INTO officedba.StorageProduct(");
                strSql.Append("CompanyCD,ProductID,DeptID,ProductCount,BatchNo)");
                strSql.Append(" VALUES (");
                strSql.Append("@CompanyCD,@ProductID,@StorageID,@ProductCount,@BatchNo)");
            }
            else
            {
                strSql.Append("UPDATE officedba.StorageProduct set ");
                strSql.Append("ProductCount=(isnull(ProductCount,0)+@ProductCount)");
                strSql.Append(" WHERE CompanyCD=@CompanyCD AND ProductID=@ProductID AND StorageID=@StorageID AND ISNULL(BatchNo,'')=@BatchNo ");
            }
            SqlParameter[] parameters = {
					        new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					        new SqlParameter("@ProductID", SqlDbType.Int,4),
					        new SqlParameter("@StorageID", SqlDbType.Int,4),
					        new SqlParameter("@ProductCount", SqlDbType.Decimal,9),
					        new SqlParameter("@BatchNo", SqlDbType.VarChar,50)};
            parameters[0].Value = CompanyCD;
            parameters[1].Value = ProductID;
            parameters[2].Value = StorageID;
            parameters[3].Value = ProductCount;
            parameters[4].Value = BatchNo;

            cmd.CommandText = strSql.ToString();
            cmd.Parameters.AddRange(parameters);
            return cmd;
        }

        /// <summary>
        /// 获得库存
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="ProductID">产品代码</param>
        /// <param name="StorageID">仓库代码</param>
        /// <param name="BatchNo">批次</param>
        /// <returns></returns>
        public static DataTable GetProductCount(string CompanyCD, string ProductID, string StorageID, string BatchNo)
        {
            SqlParameter[] subPara = { 
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                          new SqlParameter("@ProductID",SqlDbType.Int),
                                          new SqlParameter("@StorageID",SqlDbType.Int),
                                          new SqlParameter("@BatchNo",SqlDbType.VarChar),};
            subPara[0].Value = CompanyCD;
            subPara[1].Value = ProductID;
            subPara[2].Value = StorageID;
            subPara[3].Value = BatchNo;
            return SqlHelper.ExecuteSql(@"SELECT * FROM officedba.StorageProduct 
                                WHERE ProductID=@ProductID AND StorageID=@StorageID AND CompanyCD=@CompanyCD AND ISNULL(BatchNo,'')=@BatchNo ", subPara);

        }



        /// <summary>
        /// 获取门店对应的批次
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ProductNo"></param>
        /// <param name="StorageID"></param>
        /// <returns></returns>
        public static string GetSubBatchNo(string CompanyCD)
        {
            string rev = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  distinct  isnull(BatchNo,'未设置批次') as BatchNo           ");
            sql.AppendLine("FROM officedba.SubStorageProduct    ");
            sql.AppendLine("   where CompanyCD=@CompanyCD");

            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

         
            comm.CommandText = sql.ToString();
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            foreach (DataRow row in dt.Rows)
            {
                string res = row["BatchNo"].ToString();
                rev += res + ",";
            }
            return rev.TrimEnd(new char[] { ',' });
        }



    }
}
