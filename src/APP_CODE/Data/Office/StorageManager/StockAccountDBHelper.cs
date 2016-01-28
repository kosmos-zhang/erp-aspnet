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
    public class StockAccountDBHelper
    {
        #region 产品出入库报表表
        public static DataTable GetProductInOutInfo(StockAccountModel model)
        {
            DataTable DTReturn = new DataTable();
            DTReturn.Columns.Add("ProductNo");
            DTReturn.Columns.Add("ProductName");
            DTReturn.Columns.Add("Specification");
            DTReturn.Columns.Add("UnitID");
            DTReturn.Columns.Add("InCountTotal");
            //DTReturn.Columns.Add("InPriceTotal");
            DTReturn.Columns.Add("OutCountTotal");
            //DTReturn.Columns.Add("OutPriceTotal");


            DataTable dt_SPInfo = GetSPInfo(model);//ProductNo,ProductName,模糊查询
            if (dt_SPInfo.Rows.Count > 0)
            {
                for (int i = 0; i < dt_SPInfo.Rows.Count; i++)
                {
                    DataRow dr = DTReturn.NewRow();
                    dr["ProductNo"] = dt_SPInfo.Rows[i]["ProductNo"];
                    dr["ProductName"] = dt_SPInfo.Rows[i]["ProductName"];
                    dr["Specification"] = dt_SPInfo.Rows[i]["Specification"];
                    dr["UnitID"] = dt_SPInfo.Rows[i]["UnitID"];
                    DataTable dt_InPurchase = GetInfoFromInPurchase(dt_SPInfo.Rows[i]["ProductID"].ToString(), model);
                    DataTable dt_InProcess = GetInfoFromInProcess(dt_SPInfo.Rows[i]["ProductID"].ToString(), model);
                    DataTable dt_InOther = GetInfoFromInOther(dt_SPInfo.Rows[i]["ProductID"].ToString(), model);
                    DataTable dt_InRed = GetInfoFromInRed(dt_SPInfo.Rows[i]["ProductID"].ToString(), model);
                    DataTable dt_OutSell = GetInfoFromOutSell(dt_SPInfo.Rows[i]["ProductID"].ToString(), model);
                    DataTable dt_OutOther = GetInfoFromOutOther(dt_SPInfo.Rows[i]["ProductID"].ToString(), model);
                    DataTable dt_OutRed = GetInfoFromOutRed(dt_SPInfo.Rows[i]["ProductID"].ToString(), model);

                    decimal InCountTotal = 0;
                    decimal OutCountTotal = 0;
                    if (dt_InPurchase.Rows.Count > 0)
                    {
                        InCountTotal += decimal.Parse(dt_InPurchase.Rows[0]["CountTotal"].ToString());
                        //InPriceTotal += decimal.Parse(dt_InPurchase.Rows[0]["PriceTotal"].ToString());
                    }
                    if (dt_InProcess.Rows.Count > 0)
                    {
                        InCountTotal += decimal.Parse(dt_InProcess.Rows[0]["CountTotal"].ToString());
                        //InPriceTotal += decimal.Parse(dt_InProcess.Rows[0]["PriceTotal"].ToString());
                    }
                    if (dt_InOther.Rows.Count > 0)
                    {
                        InCountTotal += decimal.Parse(dt_InOther.Rows[0]["CountTotal"].ToString());
                        //InPriceTotal += decimal.Parse(dt_InOther.Rows[0]["PriceTotal"].ToString());
                    }
                    if (dt_InRed.Rows.Count > 0)
                    {
                        InCountTotal -= decimal.Parse(dt_InRed.Rows[0]["CountTotal"].ToString());
                        //InPriceTotal -= decimal.Parse(dt_InRed.Rows[0]["PriceTotal"].ToString());
                    }

                    if (dt_OutSell.Rows.Count > 0)
                    {
                        OutCountTotal += decimal.Parse(dt_OutSell.Rows[0]["CountTotal"].ToString());
                        //OutPriceTotal += decimal.Parse(dt_OutSell.Rows[0]["PriceTotal"].ToString());
                    }
                    if (dt_OutOther.Rows.Count > 0)
                    {
                        OutCountTotal += decimal.Parse(dt_OutOther.Rows[0]["CountTotal"].ToString());
                        //OutPriceTotal += decimal.Parse(dt_OutOther.Rows[0]["PriceTotal"].ToString());
                    }
                    if (dt_OutRed.Rows.Count > 0)
                    {
                        OutCountTotal -= decimal.Parse(dt_OutRed.Rows[0]["CountTotal"].ToString());
                        //OutPriceTotal -= decimal.Parse(dt_OutRed.Rows[0]["PriceTotal"].ToString());
                    }
                    dr["InCountTotal"] = Math.Round(InCountTotal, 2);
                    //dr["InPriceTotal"] = Math.Round(InPriceTotal, 2);
                    dr["OutCountTotal"] = Math.Round(OutCountTotal, 2);
                    //dr["OutPriceTotal"] = Math.Round(OutPriceTotal, 2);
                    DTReturn.Rows.Add(dr);
                }
            }
            return DTReturn;
        }

        #region 指定物品在时间段内，在采购入库中入库了总数量和总金额（增加）
        public static DataTable GetInfoFromInPurchase(string ProductID, StockAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select sum(a.ProductCount) as CountTotal,sum(a.TotalPrice) as PriceTotal,a.ProductID                                      ");
            sql.AppendLine("from officedba.StorageInPurchaseDetail a inner join officedba.StorageInPurchase b on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD and b.BillStatus!='1'  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.ProductID=" + ProductID + "                                                                ");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine("and b.ConfirmDate>=@StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                sql.AppendLine("and b.ConfirmDate<@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", Convert.ToDateTime(Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd")).AddDays(1).ToString("yyyy-MM-dd")));
            }
            sql.AppendLine("group by a.ProductID                                                   													  ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);


        }
        #endregion

        #region 指定物品在时间段内，在生产完工入库中入库了总数量和总金额（增加）
        public static DataTable GetInfoFromInProcess(string ProductID, StockAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select sum(a.ProductCount) as CountTotal,sum(a.TotalPrice) as PriceTotal,a.ProductID                                      ");
            sql.AppendLine("from officedba.StorageInProcessDetail a inner join officedba.StorageInProcess b on a.InNo=b.InNo  and a.CompanyCD=b.CompanyCD and b.BillStatus!='1'  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.ProductID=" + ProductID + "                                                                ");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine("and b.ConfirmDate>=@StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                sql.AppendLine("and b.ConfirmDate<@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd")));
            }
            sql.AppendLine("group by a.ProductID                                                   													  ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);


        }
        #endregion

        #region 指定物品在时间段内，在其他入库中入库了总数量和总金额（增加）
        public static DataTable GetInfoFromInOther(string ProductID, StockAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select sum(a.ProductCount) as CountTotal,sum(a.TotalPrice) as PriceTotal,a.ProductID                                      ");
            sql.AppendLine("from officedba.StorageInOtherDetail a inner join officedba.StorageInOther b on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD and b.BillStatus!='1'  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.ProductID=" + ProductID + "                                                                ");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine("and b.ConfirmDate>=@StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                sql.AppendLine("and b.ConfirmDate<@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));
            }
            sql.AppendLine("group by a.ProductID                                                   													  ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);


        }
        #endregion

        #region 指定物品在时间段内，在红冲入库中红冲了总数量和总金额（减少）
        public static DataTable GetInfoFromInRed(string ProductID, StockAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select sum(a.ProductCount) as CountTotal,sum(a.TotalPrice) as PriceTotal,a.ProductID                                      ");
            sql.AppendLine("from officedba.StorageInRedDetail a inner join officedba.StorageInRed b on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD and b.BillStatus!='1'  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.ProductID=" + ProductID + "                                                                ");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine("and b.ConfirmDate>=@StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                sql.AppendLine("and b.ConfirmDate<@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));
            }
            sql.AppendLine("group by a.ProductID                                                   													  ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);


        }
        #endregion

        #region 指定物品在时间段内，在销售出库库中出库了总数量和总金额（减少）
        public static DataTable GetInfoFromOutSell(string ProductID, StockAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select sum(a.ProductCount) as CountTotal,sum(a.TotalPrice) as PriceTotal,a.ProductID                                      ");
            sql.AppendLine("from officedba.StorageOutSellDetail a inner join officedba.StorageOutSell b on a.OutNo=b.OutNo and a.CompanyCD=b.CompanyCD and b.BillStatus!='1'  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.ProductID=" + ProductID + "                                                                ");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine("and b.ConfirmDate>=@StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                sql.AppendLine("and b.ConfirmDate<@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd")));
            }
            sql.AppendLine("group by a.ProductID                                                   													  ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 指定物品在时间段内，在其他出库库中出库了总数量和总金额（减少）
        public static DataTable GetInfoFromOutOther(string ProductID, StockAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select sum(a.ProductCount) as CountTotal,sum(a.TotalPrice) as PriceTotal,a.ProductID                                      ");
            sql.AppendLine("from officedba.StorageOutOtherDetail a inner join officedba.StorageOutOther b on a.OutNo=b.OutNo and a.CompanyCD=b.CompanyCD and b.BillStatus!='1'  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.ProductID=" + ProductID + "                                                                ");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine("and b.ConfirmDate>=@StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                sql.AppendLine("and b.ConfirmDate<@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd")));
            }
            sql.AppendLine("group by a.ProductID                                                   													  ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);


        }
        #endregion

        #region 指定物品在时间段内，在红冲出库库中红冲了总数量和总金额（增加）
        public static DataTable GetInfoFromOutRed(string ProductID, StockAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select sum(a.ProductCount) as CountTotal,sum(a.TotalPrice) as PriceTotal,a.ProductID                                      ");
            sql.AppendLine("from officedba.StorageOutRedDetail a inner join officedba.StorageOutRed b on a.OutNo=b.OutNo and a.CompanyCD=b.CompanyCD and b.BillStatus!='1'  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.ProductID=" + ProductID + "                                                                ");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine("and b.ConfirmDate>=@StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                sql.AppendLine("and b.ConfirmDate<@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd")));
            }
            sql.AppendLine("group by a.ProductID                                                   													  ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);


        }
        #endregion

        #region 通过物品ID获取物品的编号、名称、规格、单位
        private static DataTable GetProductInfoByID(string ProductID)
        {
            string strSql = "select a.ProdNo,a.ProductName,a.Specification,q.CodeName as UnitID"
                            + " from officedba.ProductInfo as a"
                            + " left join officedba.CodeUnitType q on q.ID=a.UnitID"
                            + " where a.ID=" + ProductID;
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion

        #region 通过条件（ProductNo,ProductName）查询当前对应CompanyCD在分仓存量表中，ProductID有多少种物品数据
        /// <summary>
        /// 查询当前对应CompanyCD在分仓存量表中，ProductID，StorageID确定的数据
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns>DataTable</returns>
        protected static DataTable GetSPInfo(StockAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.*,ISNULL(c.ProdNo,'') as ProductNo                                       ");
            sql.AppendLine(",ISNULL(c.ProductName,'') as ProductName                                          ");
            sql.AppendLine(",ISNULL(c.Specification,'') as Specification                                      ");
            sql.AppendLine(",ISNULL(q.CodeName,'') as UnitID                                                  ");
            sql.AppendLine(" from (select a.ProductID                                                         ");
            sql.AppendLine(" from officedba.StorageProduct a where a.CompanyCD=@CompanyCD group by a.ProductID) a ");
            sql.AppendLine(" left join officedba.ProductInfo c on a.ProductID=c.ID	                          ");
            sql.AppendLine(" left join officedba.CodeUnitType q on q.ID=c.UnitID                              ");
            sql.AppendLine(" where 1=1                                           							  ");

            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.ProductNo))
            {
                sql.AppendLine("and c.ProdNo = @ProductNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", model.ProductNo));
            }
            //if (!string.IsNullOrEmpty(model.ProductName))
            //{
            //    sql.AppendLine("and c.ProductName like '%'+ @ProductName +'%'");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", model.ProductName));
            //}
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 产品销售信息（销售出库）
        /// <summary>
        /// 产品销售信息（销售出库）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetInfoByOutSell(StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.OutNo,a.ProductID,c.ProdNo,c.ProductName,c.Specification,q.CodeName as UnitID  ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),b.ConfirmDate, 21),'') as OutDate--这里把确认日期改成了页面显示日期     ");
            sql.AppendLine(",ISNULL(y.CustNo,'') as CustNo                                                          ");
            sql.AppendLine(",ISNULL(y.CustName,'') as CustName                                                       ");
            sql.AppendLine(",ISNULL(a.ProductCount,0) as CountTotal                                                 ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0) as Tax_TotalPrice --含税金额=数量 * 含税单价                    ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*ISNULL(l.UnitPrice,0) as TotalPrice --不含税金额=数量 * 单价  ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0)-ISNULL(a.ProductCount,0)*ISNULL(l.UnitPrice,0) as Tax           ");
            sql.AppendLine("  from officedba.StorageOutSellDetail a                                                 ");
            sql.AppendLine("left join officedba.StorageOutSell b on a.OutNo=b.OutNo and b.BillStatus!='1'           ");
            sql.AppendLine("left join officedba.ProductInfo c on a.ProductID=c.ID                                   ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=c.UnitID                                     ");
            sql.AppendLine("left join officedba.SellSend k on k.ID=b.FromBillID                                     ");
            sql.AppendLine("left join officedba.SellSendDetail l on l.SendNo=k.SendNo and l.SortNo=a.FromLineNo and l.CompanyCD=a.CompanyCD    ");
            sql.AppendLine("left join officedba.CustInfo y on y.ID=k.CustID                                         ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD	   													    ");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.ProductNo))
            {
                sql.AppendLine(" and c.ProdNo like '%'+ @ProdNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProductNo));
            }
            //if (!string.IsNullOrEmpty(model.ProductName))
            //{
            //    sql.AppendLine(" and c.ProductName like '%'+ @ProductName +'%'");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", model.ProductName));
            //}
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine(" and b.ConfirmDate>=@StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                sql.AppendLine(" and b.ConfirmDate<@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd")));
            }

            if (!iflist)
            {
                if (ord != "asc" && ord != "desc")
                {
                    sql.AppendLine(" order by " + ord);
                }
                comm.CommandText = sql.ToString();
                return SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                comm.CommandText = sql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
        }
        #endregion

        #region 产品入库信息（采购入库）
        /// <summary>
        /// 产品入库信息（采购入库）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetInfoByInPurchase(StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.InNo,a.ProductID,c.ProdNo,c.ProductName,c.Specification,q.CodeName as UnitID           ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),b.ConfirmDate, 21),'') as EnterDate --这里把入库时间改成了确认日期     ");
            sql.AppendLine(",ISNULL(o.CustNo,'') as ProviderNo                                                              ");
            sql.AppendLine(",ISNULL(o.CustName,'') as ProviderName                                                          ");
            sql.AppendLine(",ISNULL(a.ProductCount,0) as CountTotal                                                         ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0) as Tax_TotalPrice --含税金额=数量 * 含税单价                            ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*ISNULL(l.UnitPrice,0) as TotalPrice --不含税金额=数量 * 单价          ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0)-ISNULL(a.ProductCount,0)*ISNULL(l.UnitPrice,0) as Tax                   ");
            sql.AppendLine("  from officedba.StorageInPurchaseDetail a                                                      ");
            sql.AppendLine("left join officedba.StorageInPurchase b on a.InNo=b.InNo and b.BillStatus!='1'                  ");
            sql.AppendLine("left join officedba.ProductInfo c on a.ProductID=c.ID                                           ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=c.UnitID                                             ");
            sql.AppendLine("left join officedba.PurchaseArrive k on k.ID=b.FromBillID                                       ");
            sql.AppendLine("left join officedba.PurchaseArriveDetail l on l.ArriveNo=k.ArriveNo and l.SortNo=a.FromLineNo  and l.CompanyCD=a.CompanyCD  ");
            sql.AppendLine("left join officedba.ProviderInfo o on k.ProviderID=o.ID                                         ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD	   													    ");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.ProductNo))
            {
                sql.AppendLine(" and c.ProdNo like '%'+ @ProdNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProductNo));
            }
            //if (!string.IsNullOrEmpty(model.ProductName))
            //{
            //    sql.AppendLine(" and c.ProductName like '%'+ @ProductName +'%'");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", model.ProductName));
            //}
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine(" and b.ConfirmDate>=@StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                sql.AppendLine(" and b.ConfirmDate<@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd")));
            }

            if (!iflist)
            {
                if (ord != "asc" && ord != "desc")
                {
                    sql.AppendLine(" order by " + ord);
                }
                comm.CommandText = sql.ToString();
                return SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                comm.CommandText = sql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
        }
        #endregion
        #endregion

        #region 产品库存余额
        public static DataTable GetProductStockTotalPrice(StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("			select a.StorageID                                              ");
            sql.AppendLine(",ISNULL(s.StorageName,'') as StorageName                                    ");
            sql.AppendLine(",a.ProductID,ISNULL(a.ProductCount,0) as ProductCount                       ");
            sql.AppendLine(",ISNULL(b.ProdNo,'') as ProductNo                                           ");
            sql.AppendLine(",ISNULL(b.ProductName,'') as ProductName                                    ");
            sql.AppendLine(",ISNULL(b.Specification,'') as Specification                                ");
            sql.AppendLine(",ISNULL(q.CodeName,'') as UnitID                                            ");
            sql.AppendLine(",ISNULL(b.StandardCost,0) as UnitPrice--成本单价                            ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*ISNULL(b.StandardCost,0) as TotalPrice--库存金额  ");
            sql.AppendLine(",ISNULL(b.TaxRate,0) as TaxRate--税率                                       ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*ISNULL(b.StandardCost,0)*ISNULL(b.TaxRate,0)/100 as TaxPrice--税额             ");
            sql.AppendLine(",ISNULL(b.SellTax,0) as TaxUnitPrice--含税单价（含税售价）                  ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*ISNULL(b.SellTax,0) as TaxTotalPrice--含税金额    ");
            sql.AppendLine(" from officedba.StorageProduct a                                            ");
            sql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID                       ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=b.UnitID                         ");
            sql.AppendLine("left join officedba.StorageInfo s on s.ID=a.StorageID                       ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD												");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //入库单编号、入库单主题、交货人（弹出窗口选择）、 验收人（弹出窗口选择）、入库人（选择）
            //、入库时间（日期段，日期控件）、仓库（下拉列表）、入库部门、单据状态（下拉列表）
            if (!string.IsNullOrEmpty(model.ProductNo))
            {
                sql.AppendLine(" and b.ProdNo like @ProductNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", model.ProductNo));
            }
            //if (!string.IsNullOrEmpty(model.ProductName))
            //{
            //    sql.AppendLine(" and b.ProductName like '%'+ @ProductName +'%'");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", model.ProductName));
            //}
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine(" and a.StorageID = @StorageID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            if (!iflist)
            {
                if (ord != "asc" && ord != "desc")
                {
                    sql.AppendLine(" order by " + ord);
                }
                else
                {
                    sql.AppendLine(" order by ProductNo");
                }
                comm.CommandText = sql.ToString();
                return SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                comm.CommandText = sql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
        }
        #endregion

        #region 库存结构分析

        public static DataTable GetStockStructAnalysis(StockAccountModel model, string ordercolumn, string ordertype)
        {
            DataTable DTReturn = new DataTable();
            DTReturn.Columns.Add("ProductNo", typeof(string));
            DTReturn.Columns.Add("ProductName", typeof(string));
            DTReturn.Columns.Add("Specification", typeof(string));
            DTReturn.Columns.Add("UnitID", typeof(string));
            DTReturn.Columns.Add("ProductCount", typeof(decimal));
            DTReturn.Columns.Add("TaxTotalPrice", typeof(decimal));
            DTReturn.Columns.Add("ZanYaTotalPrice", typeof(decimal));
            DTReturn.Columns.Add("AllTotalPrice", typeof(decimal));
            DTReturn.Columns.Add("StockBizhong", typeof(decimal));
            DTReturn.Columns.Add("OutCountPerDay", typeof(decimal));
            DTReturn.Columns.Add("OutSellCountPerDay", typeof(decimal));


            string[] dateStr = model.StartDate.Split('-');
            DateTime dateNow = DateTime.Now;
            DateTime d2 = new DateTime(int.Parse(dateStr[0]), int.Parse(dateStr[1]), int.Parse(dateStr[2]));
            TimeSpan ts = dateNow.Subtract(d2);
            int days = ts.Days;//指定时间到当前时间的间隔天数
            days += 1;//日期算法：1号-3号；表示3天
            model.EndDate = dateNow.ToString("yyyy-MM-dd");
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select aa.*                                                                                         ");
            sql.AppendLine(",b.ID as ProductID");
            sql.AppendLine(",ISNULL(b.ProdNo,' ') as ProductNo                                                                   ");
            sql.AppendLine(",ISNULL(b.ProductName,' ') as ProductName                                                            ");
            sql.AppendLine(",ISNULL(b.Specification,' ') as Specification                                                        ");
            sql.AppendLine(",ISNULL(q.CodeName,' ') as UnitID                                                                    ");
            sql.AppendLine(",ISNULL(aa.TotalCount,0) as ProductCount");
            sql.AppendLine(",ISNULL(aa.TotalCount,0)*ISNULL(b.SellTax,0) as TaxTotalPrice--含税金额(库存金额)，含税单价乘以数量 ");
            sql.AppendLine(",ISNULL(aa.TotalCount,0)*ISNULL(b.StandardCost,0) as ZanYaTotalPrice--占压金额，表准成本乘以数量");
            sql.AppendLine("--算出总价                                                                                          ");
            sql.AppendLine(",(select Sum(bb.TaxTotalPrice) from (select aa.*                                                    ");
            sql.AppendLine("	,ISNULL(aa.TotalCount,0)*ISNULL(b.SellTax,0) as TaxTotalPrice--含税金额(库存金额)               ");
            sql.AppendLine("	from (select a.ProductID,sum(ISNULL(a.ProductCount,0)) as TotalCount                            ");
            sql.AppendLine("		from officedba.StorageProduct a                                                             ");
            sql.AppendLine("	where a.CompanyCD='" + model.CompanyCD + "' group by a.ProductID) aa                            ");
            sql.AppendLine("	left join officedba.ProductInfo b on aa.ProductID=b.ID) bb) as AllTotalPrice--总价结束          ");
            sql.AppendLine("from (select a.ProductID,sum(ISNULL(a.ProductCount,0)) as TotalCount                                ");
            sql.AppendLine("		from officedba.StorageProduct a where a.CompanyCD='" + model.CompanyCD + "' group by a.ProductID) aa ");
            sql.AppendLine("left join officedba.ProductInfo b on aa.ProductID=b.ID                                              ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=b.UnitID                                               	");
            sql.AppendLine(" order by ProductCount");
            if (!string.IsNullOrEmpty(ordertype))
            {
                sql.AppendLine(ordertype);
            }
            DataTable dt_GetBasicInfo = SqlHelper.ExecuteSql(sql.ToString());
            if (dt_GetBasicInfo.Rows.Count > 0)
            {
                for (int i = 0; i < dt_GetBasicInfo.Rows.Count; i++)
                {
                    DataRow dr = DTReturn.NewRow();
                    dr["ProductNo"] = dt_GetBasicInfo.Rows[i]["ProductNo"];
                    dr["ProductName"] = dt_GetBasicInfo.Rows[i]["ProductName"];
                    dr["Specification"] = dt_GetBasicInfo.Rows[i]["Specification"];
                    dr["UnitID"] = dt_GetBasicInfo.Rows[i]["UnitID"];
                    dr["ProductCount"] = dt_GetBasicInfo.Rows[i]["ProductCount"];
                    dr["TaxTotalPrice"] = Math.Round(decimal.Parse(dt_GetBasicInfo.Rows[i]["TaxTotalPrice"].ToString()), 4);
                    dr["ZanYaTotalPrice"] = Math.Round(decimal.Parse(dt_GetBasicInfo.Rows[i]["ZanYaTotalPrice"].ToString()), 4);
                    dr["AllTotalPrice"] = Math.Round(decimal.Parse(dt_GetBasicInfo.Rows[i]["AllTotalPrice"].ToString()), 4);
                    dr["StockBizhong"] = Math.Round(decimal.Parse(dt_GetBasicInfo.Rows[i]["TaxTotalPrice"].ToString()) * 100 / decimal.Parse(dt_GetBasicInfo.Rows[i]["AllTotalPrice"].ToString()), 4);

                    decimal OutCountTotal = 0;
                    decimal OutSellCountTotal = 0;
                    DataTable dt_OutSell = GetInfoFromOutSell(dt_GetBasicInfo.Rows[i]["ProductID"].ToString(), model);
                    DataTable dt_OutOther = GetInfoFromOutOther(dt_GetBasicInfo.Rows[i]["ProductID"].ToString(), model);
                    DataTable dt_OutRed = GetInfoFromOutRed(dt_GetBasicInfo.Rows[i]["ProductID"].ToString(), model);
                    DataTable dt_OutRed_ofSell = GetInfoFromOutRedOfOutSell(dt_GetBasicInfo.Rows[i]["ProductID"].ToString(), model);

                    if (dt_OutSell.Rows.Count > 0)
                    {
                        OutCountTotal += decimal.Parse(dt_OutSell.Rows[0]["CountTotal"].ToString());
                    }
                    if (dt_OutOther.Rows.Count > 0)
                    {
                        OutCountTotal += decimal.Parse(dt_OutOther.Rows[0]["CountTotal"].ToString());
                    }
                    if (dt_OutRed.Rows.Count > 0)
                    {
                        OutCountTotal -= decimal.Parse(dt_OutRed.Rows[0]["CountTotal"].ToString());
                    }
                    dr["OutCountPerDay"] = Math.Round(OutCountTotal / days, 4);

                    if (dt_OutSell.Rows.Count > 0)
                    {
                        OutSellCountTotal += decimal.Parse(dt_OutSell.Rows[0]["CountTotal"].ToString());
                    }
                    if (dt_OutRed_ofSell.Rows.Count > 0)
                    {
                        OutSellCountTotal -= decimal.Parse(dt_OutRed_ofSell.Rows[0]["CountTotal"].ToString());
                    }
                    dr["OutSellCountPerDay"] = Math.Round(OutSellCountTotal / days, 4);
                    DTReturn.Rows.Add(dr);
                }
            }
            return DTReturn;

        }

        #region 指定物品在时间段内，在红冲出库(红冲销售出库)红冲了总数量和总金额
        public static DataTable GetInfoFromOutRedOfOutSell(string ProductID, StockAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select sum(a.ProductCount) as CountTotal,sum(a.TotalPrice) as PriceTotal,a.ProductID                                      ");
            sql.AppendLine("from officedba.StorageOutRedDetail a inner join officedba.StorageOutRed b on a.OutNo=b.OutNo and b.BillStatus!='1'  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.ProductID=" + ProductID + " and b.FromType='1'                                                        ");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine("and b.ConfirmDate>=@StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                sql.AppendLine("and b.ConfirmDate<@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd")));
            }
            sql.AppendLine("group by a.ProductID                                                   													  ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #endregion


        #region 库存单品分析

        #region 物品销售出库和采购一起的
        /// <summary>
        /// 查出指定物品的销售信息，物品必填
        /// </summary>
        /// <param name="model"></param>
        /// <returns>得到的是一行数据：数量总和，含税总额，不含税总额，税额</returns>
        public static DataTable GetProdcutSellandPurchaseInfo(StockAccountModel model)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select '销售' as type, sum(ProductCount) as CountTotal,sum(TaxTotalPrice) as TaxTotalPrice                                                           ");
            sql.AppendLine(",sum(TotalPrice) as TotalPrice,sum(TaxTotalPric) as  TaxTotalPric                                                                                    ");
            sql.AppendLine("from (                                                                                                                                               ");
            sql.AppendLine("select sum(a.ProductCount) as ProductCount,sum(a.TaxTotalPrice) as TaxTotalPrice                                                                     ");
            sql.AppendLine(",sum(a.TotalPrice) as TotalPrice,sum(a.TaxTotalPric) as TaxTotalPric  from                                                                           ");
            sql.AppendLine("(select a.ProductID,ISNULL(a.ProductCount,0) as ProductCount                                                                                         ");
            sql.AppendLine(",d.TaxPrice--含税价                                                                                                                                  ");
            sql.AppendLine(",d.UnitPrice--单价，不含税价                                                                                                                         ");
            sql.AppendLine(",d.Discount/100 as Discount                                                                                                                          ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*d.TaxPrice*(d.Discount/100)*(c.Discount/100) as TaxTotalPrice--总额（含税）                                                ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*d.UnitPrice*(d.Discount/100)*(c.Discount/100) as TotalPrice--金额（不含税）                                                ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*d.TaxPrice*(d.Discount/100)*(c.Discount/100)                                                                               ");
            sql.AppendLine("		-ISNULL(a.ProductCount,0)*d.UnitPrice*(d.Discount/100)*(c.Discount/100) as TaxTotalPric--税额                                                    ");
            sql.AppendLine(" from officedba.StorageOutSellDetail a                                                                                                               ");
            sql.AppendLine("inner join officedba.StorageOutSell b on a.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD and a.OutNo=b.OutNo and b.BillStatus!=1   ");
            sql.AppendLine("inner join officedba.SellSend c on b.FromBillID=c.ID                                                                                                 ");
            sql.AppendLine("inner join officedba.SellSendDetail d on c.SendNo=d.SendNo and c.CompanyCD=d.CompanyCD and d.SortNo=a.FromLineNo                                                              ");
            sql.AppendLine("inner join officedba.ProductInfo g on g.ID=a.ProductID                                                                                               ");
            sql.AppendLine("where a.CompanyCD='" + model.CompanyCD + "' and g.ProdNo=@ProductNo) as a                                                                            ");
            sql.AppendLine("                                                                                                                                                     ");
            sql.AppendLine("union                                                                                                                                                ");
            sql.AppendLine("-------红冲                                                                                                                                          ");
            sql.AppendLine("select -1*sum(b.OutRedCount) as ProductCount,-1*sum(b.TaxTotalPrice) as TaxTotalPrice                                                                ");
            sql.AppendLine(",-1*sum(b.TotalPrice) as TotalPrice,-1*sum(b.TaxTotalPric) as TaxTotalPric from                                                                      ");
            sql.AppendLine("(select                                                                                                                                              ");
            sql.AppendLine("ISNULL(f.ProductCount,0) as OutRedCount                                                                                                              ");
            sql.AppendLine(",ISNULL(f.ProductCount,0)*d.TaxPrice*(d.Discount/100)*(c.Discount/100) as TaxTotalPrice--总额（含税）                                                ");
            sql.AppendLine(",ISNULL(f.ProductCount,0)*d.UnitPrice*(d.Discount/100)*(c.Discount/100) as TotalPrice--金额（不含税）                                                ");
            sql.AppendLine(",ISNULL(f.ProductCount,0)*d.TaxPrice*(d.Discount/100)*(c.Discount/100)                                                                               ");
            sql.AppendLine("		-ISNULL(f.ProductCount,0)*d.UnitPrice*(d.Discount/100)*(c.Discount/100) as TaxTotalPric--税额                                                    ");
            sql.AppendLine(" from officedba.StorageOutSellDetail a                                                                                                               ");
            sql.AppendLine("inner join officedba.StorageOutSell b on a.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD and a.OutNo=b.OutNo and b.BillStatus!=1   ");
            sql.AppendLine("inner join officedba.SellSend c on b.FromBillID=c.ID                                                                                                 ");
            sql.AppendLine("inner join officedba.SellSendDetail d on c.SendNo=d.SendNo and c.CompanyCD=d.CompanyCD  and d.SortNo=a.FromLineNo                                                             ");
            sql.AppendLine("left join officedba.StorageOutRed e on e.FromBillID=b.ID and e.FromType='1' and e.BillStatus!=1                                                      ");
            sql.AppendLine("left join officedba.StorageOutRedDetail f on e.OutNo=f.OutNo and e.CompanyCD=f.CompanyCD                                                             ");
            sql.AppendLine("inner join officedba.ProductInfo g on g.ID=a.ProductID                                                                                               ");
            sql.AppendLine("where a.CompanyCD='" + model.CompanyCD + "' and g.ProdNo=@ProductNo) as b) as Info                                                                   ");
            sql.AppendLine("                                                                                                                                                     ");
            sql.AppendLine("Union                                                                                                                                                ");
            sql.AppendLine("                                                                                                                                                     ");
            sql.AppendLine("select '采购' as type, sum(ProductCount) as CountTotal,sum(TaxTotalPrice) as TaxTotalPrice                                                           ");
            sql.AppendLine(",sum(TotalPrice) as TotalPrice,sum(TaxTotalPric) as  TaxTotalPric                                                                                    ");
            sql.AppendLine("from (                                                                                                                                               ");
            sql.AppendLine("select sum(a.ProductCount) as ProductCount,sum(a.TaxTotalPrice) as TaxTotalPrice                                                                     ");
            sql.AppendLine(",sum(a.TotalPrice) as TotalPrice,sum(a.TaxTotalPric) as TaxTotalPric  from                                                                           ");
            sql.AppendLine("(select a.ProductID,ISNULL(a.ProductCount,0) as ProductCount                                                                                         ");
            sql.AppendLine(",d.TaxPrice--含税价                                                                                                                                  ");
            sql.AppendLine(",d.UnitPrice--单价，不含税价                                                                                                                         ");
            sql.AppendLine(",d.Discount/100 as Discount                                                                                                                          ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*d.TaxPrice*(c.Discount/100) as TaxTotalPrice--总额（含税）                                                ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*d.UnitPrice*(c.Discount/100) as TotalPrice--金额（不含税）                                                ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)*d.TaxPrice*(c.Discount/100)                                                                               ");
            sql.AppendLine("		-ISNULL(a.ProductCount,0)*d.UnitPrice*(c.Discount/100) as TaxTotalPric--税额                                                    ");
            sql.AppendLine(" from officedba.StorageInPurchaseDetail a                                                                                                            ");
            sql.AppendLine("inner join officedba.StorageInPurchase b on a.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD and a.InNo=b.InNo and b.BillStatus!=1  ");
            sql.AppendLine("inner join officedba.PurchaseArrive c on b.FromBillID=c.ID                                                                                           ");
            sql.AppendLine("inner join officedba.PurchaseArriveDetail d on c.ArriveNo=d.ArriveNo and c.CompanyCD=d.CompanyCD  and d.SortNo=a.FromLineNo                                                   ");
            sql.AppendLine("inner join officedba.ProductInfo g on g.ID=a.ProductID                                                                                               ");
            sql.AppendLine("where a.CompanyCD='" + model.CompanyCD + "' and g.ProdNo=@ProductNo) as a                                                                            ");
            sql.AppendLine("                                                                                                                                                     ");
            sql.AppendLine("union                                                                                                                                                ");
            sql.AppendLine("-------红冲                                                                                                                                          ");
            sql.AppendLine("select -1*sum(b.OutRedCount) as ProductCount,-1*sum(b.TaxTotalPrice) as TaxTotalPrice                                                                ");
            sql.AppendLine(",-1*sum(b.TotalPrice) as TotalPrice,-1*sum(b.TaxTotalPric) as TaxTotalPric from                                                                      ");
            sql.AppendLine("(select                                                                                                                                              ");
            sql.AppendLine("ISNULL(f.ProductCount,0) as OutRedCount                                                                                                              ");
            sql.AppendLine(",ISNULL(f.ProductCount,0)*d.TaxPrice*(c.Discount/100) as TaxTotalPrice--总额（含税）                                                ");
            sql.AppendLine(",ISNULL(f.ProductCount,0)*d.UnitPrice*(c.Discount/100) as TotalPrice--金额（不含税）                                                ");
            sql.AppendLine(",ISNULL(f.ProductCount,0)*d.TaxPrice*(c.Discount/100)                                                                               ");
            sql.AppendLine("		-ISNULL(f.ProductCount,0)*d.UnitPrice*(c.Discount/100) as TaxTotalPric--税额                                                    ");
            sql.AppendLine(" from officedba.StorageInPurchaseDetail a                                                                                                            ");
            sql.AppendLine("inner join officedba.StorageInPurchase b on a.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD and a.InNo=b.InNo and b.BillStatus!=1  ");
            sql.AppendLine("inner join officedba.PurchaseArrive c on b.FromBillID=c.ID                                                                                           ");
            sql.AppendLine("inner join officedba.PurchaseArriveDetail d on c.ArriveNo=d.ArriveNo and c.CompanyCD=d.CompanyCD  and d.SortNo=a.FromLineNo                                                   ");
            sql.AppendLine("left join officedba.StorageInRed e on e.FromBillID=b.ID and e.FromType='1' and e.BillStatus!=1                                                       ");
            sql.AppendLine("left join officedba.StorageInRedDetail f on e.InNo=f.InNo and e.CompanyCD=f.CompanyCD                                                                ");
            sql.AppendLine("inner join officedba.ProductInfo g on g.ID=a.ProductID                                                                                               ");
            sql.AppendLine("where a.CompanyCD='" + model.CompanyCD + "' and g.ProdNo=@ProductNo) as b) as Info                                                                   ");
            //comm.AppendLine.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", model.ProductNo));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }

        #endregion


        #region 物品采购库的
        /// <summary>
        /// 查出指定物品的采购信息，物品必填
        /// </summary>
        /// <param name="model"></param>
        /// <returns>得到的是一行数据：数量总和，含税总额，不含税总额，税额</returns>
        public static DataTable GetProdcutPurchaseInfo(StockAccountModel model)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select '购进' as type, sum(aa.CountTotal) as CountTotal,sum(aa.TaxTotalPrice) as TaxTotalPrice                                ");
            sql.AppendLine("		,sum(aa.TotalPrice) as TotalPrice,sum(aa.TaxTotalPric) as TaxTotalPric from --零时表                      ");
            sql.AppendLine("(select a.ProductID,ISNULL(a.ProductCount,0) as ProductCount                                                  ");
            sql.AppendLine(",d.TaxPrice--含税价                                                                                           ");
            sql.AppendLine(",d.UnitPrice--单价，不含税价                                                                                  ");
            sql.AppendLine(",d.Discount/100 as Discount                                                                                   ");
            sql.AppendLine(",ISNULL(f.ProductCount,0) as OutRedCount                                                                      ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)-ISNULL(f.ProductCount,0) as CountTotal--总数量                                      ");
            sql.AppendLine(",(ISNULL(a.ProductCount,0)-ISNULL(f.ProductCount,0))*d.TaxPrice*(d.Discount/100)*(c.Discount/100) as TaxTotalPrice--总额（含税） ");
            sql.AppendLine(",(ISNULL(a.ProductCount,0)-ISNULL(f.ProductCount,0))*d.UnitPrice*(d.Discount/100)*(c.Discount/100) as TotalPrice--金额（不含税） ");
            sql.AppendLine(",(ISNULL(a.ProductCount,0)-ISNULL(f.ProductCount,0))*d.TaxPrice*(d.Discount/100)*(c.Discount/100)                                ");
            sql.AppendLine("	-(ISNULL(a.ProductCount,0)-ISNULL(f.ProductCount,0))*d.UnitPrice*(d.Discount/100)*(c.Discount/100) as TaxTotalPric--税额     ");
            sql.AppendLine(" from officedba.StorageInPurchaseDetail a                                                                     ");
            sql.AppendLine("inner join officedba.StorageInPurchase b on a.InNo=b.InNo                                                     ");
            sql.AppendLine("inner join officedba.PurchaseArrive c on b.FromBillID=c.ID                                                    ");
            sql.AppendLine("inner join officedba.PurchaseArriveDetail d on c.ArriveNo=d.ArriveNo                                          ");
            sql.AppendLine("left join officedba.StorageInRed e on e.FromBillID=b.ID and e.FromType='1'                                    ");
            sql.AppendLine("left join officedba.StorageInRedDetail f on e.InNO=f.InNo                                                     ");
            sql.AppendLine("inner join officedba.ProductInfo g on g.ID=a.ProductID                                                        ");
            sql.AppendLine("where a.CompanyCD='" + model.CompanyCD + "' and g.ProdNo=@ProductNo) as aa 	");
            //comm.AppendLine.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", model.ProductNo));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }

        #endregion

        #region 库存
        //获取当前物品的库存信息和价格信息
        public static DataTable GetProductStockAndPriceInfo(StockAccountModel model)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("--------------------------------------                    ");
            sql.AppendLine("select aa.*,b.ProductName,b.SellTax--含税售价             ");
            sql.AppendLine("	,aa.ProductCount*b.SellTax as TotalPrice--库存金额      ");
            sql.AppendLine("	,b.TaxRate  --税率                                    ");
            sql.AppendLine("	,aa.ProductCount*b.SellTax*b.TaxRate/100 as TaxPrice --总库存税额");
            sql.AppendLine("	,b.MaxStockNum--库存上限                                ");
            sql.AppendLine("	,b.MinStockNum--库存下限                                ");
            sql.AppendLine("----------------价格信息-----------------------           ");
            sql.AppendLine("	,b.StandardBuy--含税进价                                ");
            sql.AppendLine("	,b.TaxBuy--不含税进价                                   ");
            sql.AppendLine("	,b.StandardCost--成本单价                               ");
            sql.AppendLine("	,b.SellTax--含税批发价(含税售价)                        ");
            sql.AppendLine("	,b.StandardSell--不含税批发价(去税售价)                 ");
            sql.AppendLine("	,b.SellPrice		--零售价                                ");
            sql.AppendLine(" from                                                     ");
            sql.AppendLine("	(select a.ProductID,sum(a.ProductCount) as ProductCount ");
            sql.AppendLine("	from officedba.StorageProduct a                         ");
            sql.AppendLine("	left join officedba.ProductInfo b on a.ProductID=b.ID   ");
            sql.AppendLine("	group by a.ProductID) as aa                             ");
            sql.AppendLine("left join officedba.ProductInfo b on aa.ProductID=b.ID    ");
            sql.AppendLine("where b.ProdNo=@ProductNo and b.CompanyCD='" + model.CompanyCD + "'   ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", model.ProductNo));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }

        //获取指定物品的损益信息
        public static DataTable GetProdcutLoseInfo(StockAccountModel model)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select temp_Pan.*,temp_Loss.LossCount,temp_Loss.LossTotalPrice");
            sql.AppendLine(" from ");
            sql.AppendLine("(select P.ID as ProductID,temp_kui.ProductCount-temp_ying.ProductCount as TotalCount,temp_kui.TotalPrice-temp_ying.TotalPrice as TotalPrice--,temp_Loss.LossCount,temp_Loss.LossTotalPrice ");
            sql.AppendLine(" from                                                                                                                                                                 ");
            sql.AppendLine("	(select aa.*,b.StandardCost,aa.ProductCount*b.StandardCost as TotalPrice                                                                                            ");
            sql.AppendLine("	 from                                                                                                                                                               ");
            sql.AppendLine("		(select ProductCount=ISNULL(sum(a.DiffCount),0),a.ProductID                                                                                                       ");
            sql.AppendLine("		 from officedba.StorageCheckDetail a                                                                                                                              ");
            sql.AppendLine("		 inner join officedba.StorageCheck b on a.CheckNo=b.CheckNo and a.CompanyCD=b.CompanyCD                                                                          ");
            sql.AppendLine("		 where a.DiffType=2 and b.BillStatus!=1 and a.CompanyCD='" + model.CompanyCD + "' group by a.ProductID) aa                                                         ");
            sql.AppendLine("		left join officedba.ProductInfo b on aa.ProductID=b.ID where b.ProdNo=@ProductNo) as temp_kui  ---盘亏                                                          ");
            sql.AppendLine("full join                                                                                                                                                             ");
            sql.AppendLine("	(select aa.*,b.StandardCost,aa.ProductCount*b.StandardCost as TotalPrice                                                                                            ");
            sql.AppendLine("	 from                                                                                                                                                               ");
            sql.AppendLine("		(select ProductCount=ISNULL(sum(a.DiffCount),0),a.ProductID                                                                                                       ");
            sql.AppendLine("		 from officedba.StorageCheckDetail a                                                                                                                              ");
            sql.AppendLine("		 inner join officedba.StorageCheck b on a.CheckNo=b.CheckNo  and a.CompanyCD=b.CompanyCD                                                                           ");
            sql.AppendLine("		 where a.DiffType=1 and b.BillStatus!=1 and a.CompanyCD='" + model.CompanyCD + "' group by a.ProductID) aa                                                        ");
            sql.AppendLine("		left join officedba.ProductInfo b on aa.ProductID=b.ID where b.ProdNo=@ProductNo) as temp_ying on temp_kui.ProductID=temp_ying.ProductID     ---盘赢             ");
            sql.AppendLine(" inner join officedba.ProductInfo P on p.CompanyCD='" + model.CompanyCD + "' and P.ProdNo=@ProductNo) as temp_Pan                                                                   ");
            sql.AppendLine("full join                                                                                                                                                             ");
            sql.AppendLine("	(select a.ProductID,LossCount=ISNULL(sum(a.ProductCount),0),LossTotalPrice=ISNULL(sum(c.StandardCost*a.ProductCount),0) from officedba.StorageLossDetail a                            ");
            sql.AppendLine("	inner join officedba.StorageLoss b on a.LossNo=b.LossNo and a.CompanyCD=b.CompanyCD                                                                                  ");
            sql.AppendLine("	inner join officedba.ProductInfo c on c.ID=a.ProductID                                                                                                              ");
            sql.AppendLine("	where b.BillStatus!=1 and a.CompanyCD='" + model.CompanyCD + "' and c.ProdNo=@ProductNo group by a.ProductID ) as temp_Loss on temp_Pan.ProductID=temp_Loss.ProductID	--报损");
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", model.ProductNo));
            return SqlHelper.ExecuteSearch(comm);

        }

        //库存分析:仓库、仓库数量、成本单价、仓库金额库存上限、库存下限
        public static DataTable GetStoProductInfo(StockAccountModel model)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ISNULL(c.StorageName,'') as StorageName,isnull(a.ProductCount,0) as ProductCount ");
            sql.AppendLine(",b.StandardCost                                       ");
            sql.AppendLine(",b.StandardCost*isnull(a.ProductCount,0) as TotalPrice          ");
            sql.AppendLine(",b.MaxStockNum,b.MinStockNum                          ");
            sql.AppendLine("from officedba.StorageProduct a                       ");
            sql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID ");
            sql.AppendLine("left join officedba.StorageInfo c on a.StorageID=c.ID ");
            sql.AppendLine(" where b.ProdNo=@ProductNo and a.CompanyCD='" + model.CompanyCD + "'");
            sql.AppendLine(" order by ProductCount desc");
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", model.ProductNo));
            return SqlHelper.ExecuteSearch(comm);
        }

        //购进明细：单据编号、日期、单位编号、单位名称、仓库、单位、数量
        public static DataTable GetPurchaseDetail(StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.InNo,ISNULL(convert(varchar(10),a.ConfirmDate,21),'') as Date  ");
            sql.AppendLine(",ISNULL(c.DeptNo,'') as DeptNo                                          ");
            sql.AppendLine(",ISNULL(c.DeptName,'') as DeptName                                      ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                ");
            sql.AppendLine(",ISNULL(d.StorageName,'') as StorageName                                ");
            sql.AppendLine(",ISNULL(q.CodeName,'') as UnitID                                        ");
            sql.AppendLine(",b.ProductCount                                                         ");
            sql.AppendLine(" from officedba.StorageInPurchase a                                     ");
            sql.AppendLine("inner join officedba.StorageInPurchaseDetail b on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD        ");
            sql.AppendLine("left join officedba.DeptInfo c on a.DeptID=c.ID                         ");
            sql.AppendLine("left join officedba.StorageInfo d on d.ID=b.StorageID                   ");
            sql.AppendLine("left join officedba.ProductInfo e on b.ProductID=e.ID                   ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=e.UnitID                     ");
            sql.AppendLine(" where a.BillStatus!='1' and e.ProdNo=@ProductNo and a.CompanyCD='" + model.CompanyCD + "'		");
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", model.ProductNo));
            if (!iflist)
            {
                if (!string.IsNullOrEmpty(ord))
                {
                    sql.AppendLine(" order by " + ord);
                }
                return SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
        }

        //销往明细：单据编号、日期、单位编号、单位名称、货位编号、货位名称、批号、包装单位、包装数量、零散数量、数量、含税价
        public static DataTable GetSellDetail(StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.OutNo,ISNULL(convert(varchar(10),a.ConfirmDate,21),'') as Date ");
            sql.AppendLine(",ISNULL(c.DeptNo,'') as DeptNo--单位编号                            ");
            sql.AppendLine(",ISNULL(c.DeptName,'') as DeptName--单位名称                            ");
            sql.AppendLine(",d.ProductName                                                          ");
            sql.AppendLine(",b.ProductCount--数量                                                   ");
            sql.AppendLine(",b.UnitPrice--含税价                                                    ");
            sql.AppendLine("from officedba.StorageOutSell a                                         ");
            sql.AppendLine("inner join officedba.StorageOutSellDetail b on a.OutNo=b.OutNo and a.CompanyCD=b.CompanyCD  and a.BillStatus!='1'        ");
            sql.AppendLine("left join officedba.DeptInfo c on c.ID=a.DeptID                         ");
            sql.AppendLine("inner join officedba.ProductInfo d on d.ID=b.ProductID                  ");
            sql.AppendLine("where d.ProdNo=@ProductNo and a.CompanyCD='" + model.CompanyCD + "' 	");
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", model.ProductNo));
            if (!iflist)
            {
                if (!string.IsNullOrEmpty(ord))
                {
                    sql.AppendLine(" order by " + ord);
                }
                return SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
        }

        #endregion

        //库存流水账查询
        public static DataTable GetLineInfo(string Confirmor, StockAccountModel model)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,a.StorageID 				");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'采购入库' as [Type]                                                               ");
            sql.AppendLine(",b.InNo as BillNo                                                                   ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.ProductCount                                                                     ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageInPurchaseDetail a                                            ");
            sql.AppendLine("inner join officedba.StorageInPurchase b on b.InNo=a.InNo and a.CompanyCD=b.CompanyCD                          ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=a.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'      ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor1                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor1", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and a.StorageID=@StorageID1                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID2", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,a.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'生产完工入库' as [Type]                                                           ");
            sql.AppendLine(",b.InNo as BillNo                                                                   ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.ProductCount                                                                     ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageInProcessDetail a                                             ");
            sql.AppendLine("inner join officedba.StorageInProcess b on b.InNo=a.InNo and a.CompanyCD=b.CompanyCD                           ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=a.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor2                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor2", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and a.StorageID=@StorageID2                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID2", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,a.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'其他入库' as [Type]                                                               ");
            sql.AppendLine(",b.InNo as BillNo                                                                   ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.ProductCount                                                                     ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageInOtherDetail a                                               ");
            sql.AppendLine("inner join officedba.StorageInOther b on b.InNo=a.InNo and a.CompanyCD=b.CompanyCD                             ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=a.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor3                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor3", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and a.StorageID=@StorageID3                                                     ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID3", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,a.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'红冲入库' as [Type]                                                               ");
            sql.AppendLine(",b.InNo as BillNo                                                                   ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.ProductCount                                                                     ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageInRedDetail a                                                 ");
            sql.AppendLine("inner join officedba.StorageInRed b on b.InNo=a.InNo and a.CompanyCD=b.CompanyCD                               ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=a.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor4                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor4", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and a.StorageID=@StorageID4                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID4", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("--销售出库                                                                          ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,a.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'销售出库' as [Type]                                                               ");
            sql.AppendLine(",b.OutNo as BillNo                                                                  ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.ProductCount                                                                     ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageOutSellDetail a                                               ");
            sql.AppendLine("inner join officedba.StorageOutSell b on b.OutNo=a.OutNo  and a.CompanyCD=b.CompanyCD                          ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=a.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor5                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor5", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and a.StorageID=@StorageID5                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID5", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("--其他出库                                                                          ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,a.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'其他出库' as [Type]                                                               ");
            sql.AppendLine(",b.OutNo as BillNo                                                                  ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.ProductCount                                                                     ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageOutOtherDetail a                                              ");
            sql.AppendLine("inner join officedba.StorageOutOther b on b.OutNo=a.OutNo and a.CompanyCD=b.CompanyCD                         ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=a.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor6                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor6", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and a.StorageID=@StorageID6                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID6", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("--红冲出库                                                                          ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,a.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'红冲出库' as [Type]                                                               ");
            sql.AppendLine(",b.OutNo as BillNo                                                                  ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.ProductCount                                                                     ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageOutRedDetail a                                                ");
            sql.AppendLine("inner join officedba.StorageOutRed b on b.OutNo=a.OutNo and a.CompanyCD=b.CompanyCD                            ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=a.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor7                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor7", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and a.StorageID=@StorageID7                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID7", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("--------------------借货                                                            ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("--借货                                                                              ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,b.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'借货' as [Type]                                                                   ");
            sql.AppendLine(",b.BorrowNo as BillNo                                                               ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.ProductCount                                                                     ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageBorrowDetail a                                                ");
            sql.AppendLine("inner join officedba.StorageBorrow b on b.BorrowNo=a.BorrowNo and a.CompanyCD=b.CompanyCD                     ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=b.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor8                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor8", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and a.StorageID=@StorageID8                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID8", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("--------------------返还                                                            ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("--返还                                                                              ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,b.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'返还' as [Type]                                                                   ");
            sql.AppendLine(",b.ReturnNo as BillNo                                                               ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.ProductCount                                                                     ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageReturnDetail a                                                ");
            sql.AppendLine("inner join officedba.StorageReturn b on b.ReturnNo=a.ReturnNo and a.CompanyCD=b.CompanyCD                      ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=b.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor9                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor9", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and b.StorageID=@StorageID9                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID9", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("--------------------调整                                                            ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("--调整                                                                              ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,b.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'调整' as [Type]                                                                   ");
            sql.AppendLine(",b.AdjustNo as BillNo                                                               ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.AdjustCount as ProductCount                                                      ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageAdjustDetail a                                                ");
            sql.AppendLine("inner join officedba.StorageAdjust b on b.AdjustNo=a.AdjustNo and a.CompanyCD=b.CompanyCD                      ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=b.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor10                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor10", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and b.StorageID=@StorageID10                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID10", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("--------------------报损                                                            ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("--报损                                                                              ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,b.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'报损' as [Type]                                                                   ");
            sql.AppendLine(",b.LossNo as BillNo                                                                 ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",a.ProductCount                                                                     ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageLossDetail a                                                  ");
            sql.AppendLine("inner join officedba.StorageLoss b on b.LossNo=a.LossNo and a.CompanyCD=b.CompanyCD                            ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=b.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor11                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor11", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and b.StorageID=@StorageID11                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID11", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("--------------------调拨                                                            ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("--调拨调入                                                                          ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,b.InStorageID       ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'库存调入' as [Type]                                                               ");
            sql.AppendLine(",b.TransferNo as BillNo                                                             ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",ISNULL(a.InCount,0) as ProductCount                                                ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageTransferDetail a                                              ");
            sql.AppendLine("inner join officedba.StorageTransfer b on b.TransferNo=a.TransferNo and a.CompanyCD=b.CompanyCD                ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=b.InStorageID                             ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor12                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor12", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and b.InStorageID=@StorageID12                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID12", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("--调拨调出                                                                          ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,b.OutStorageID      ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'库存调出' as [Type]                                                               ");
            sql.AppendLine(",b.TransferNo as BillNo                                                             ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",ISNULL(a.OutCount,0) as ProductCount                                               ");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageTransferDetail a                                              ");
            sql.AppendLine("inner join officedba.StorageTransfer b on b.TransferNo=a.TransferNo and a.CompanyCD=b.CompanyCD                ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=b.OutStorageID                            ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor13                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor13", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and b.OutStorageID=@StorageID13                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID13", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("--------------------盘点                                                            ");
            sql.AppendLine("                                                                                    ");
            sql.AppendLine("union all                                                                           ");
            sql.AppendLine("--盘点                                                                              ");
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,b.StorageID         ");
            sql.AppendLine(",ISNULL(c.StorageName,'') as StorageName                                            ");
            sql.AppendLine(",'盘点' as [Type]                                                                   ");
            sql.AppendLine(",b.CheckNo as BillNo                                                                ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Confirmor                                             ");
            sql.AppendLine(",a.ProductID                                                                        ");
            sql.AppendLine(",e.ProdNo                                                                           ");
            sql.AppendLine(",ISNULL(e.ProductName,'') as ProductName                                            ");
            sql.AppendLine(",ISNULL(f.CodeName,'') as UnitID                                                    ");
            sql.AppendLine(",ISNULL(e.Specification,'') as Specification                                        ");
            sql.AppendLine(",case when (a.DiffType='2') then -1*a.DiffCount else a.DiffCount end as ProductCount");
            sql.AppendLine(",ISNULL(b.Summary,'') as Summary                                                    ");
            sql.AppendLine("from officedba.StorageCheckDetail a                                                 ");
            sql.AppendLine("inner join officedba.StorageCheck b on b.CheckNo=a.CheckNo and a.CompanyCD=b.CompanyCD                         ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=b.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'              ");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine("and b.Confirmor=@Confirmor14                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor14", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and b.StorageID=@StorageID14                                                      ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID14", model.StorageID));
            }
            sql.AppendLine("and b.ConfirmDate>='" + model.StartDate + "'                                        ");
            sql.AppendLine("and b.ConfirmDate<='" + model.EndDate + "'                                          ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }

        #endregion

    }
}
