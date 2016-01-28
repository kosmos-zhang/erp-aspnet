/**********************************************
 * 类作用：   仓库数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/06/17
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
    public class StockSearchDBHelper
    {
        #region 库存流水账查询
        //库存流水账查询
        public static DataTable GetLineInfo(string Confirmor, string type, StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("SELECT a.*,convert(char(20),Convert(decimal(18," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),ProductCount))+'&nbsp;' ProductCount1 FROM officedba.V_StockLineInfo a");
            sql.AppendLine(" where a.CompanyCD='" + model.CompanyCD + "' and a.ProdNo='" + model.ProductNo + "'");
            if (!string.IsNullOrEmpty(Confirmor))
            {
                sql.AppendLine(" and a.ConfirmorID=@ConfirmorID                                                 ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmorID", Confirmor));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("and a.StorageID=@StorageID                                                     ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            if (!string.IsNullOrEmpty(type))
            {
                sql.AppendLine(" and a.Type=@type                                                     ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@type", type));
            }
            sql.AppendLine(" and a.Date>='" + model.StartDate + "'                                              ");
            sql.AppendLine(" and a.Date<'" + Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd") + "'                                                ");

            if (!iflist)
            {
                if (ord != "asc" && ord != "desc")
                {
                    sql.AppendLine(" order by " + ord);
                }
                else
                {
                    sql.AppendLine(" order by Date");
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

        #region 入库查询
        //入库查询
        public static DataTable InStockSearch(string Confirmor, StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("select ISNULL(convert(varchar(10),b.ConfirmDate,21),'') as Date,a.StorageID 		");
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
            sql.AppendLine("inner join officedba.StorageInPurchase b on b.InNo=a.InNo and b.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD  ");
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
            sql.AppendLine("and b.ConfirmDate<'" + Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd") + "'                                          ");
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
            sql.AppendLine("inner join officedba.StorageInProcess b on b.InNo=a.InNo and b.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD         ");
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
            sql.AppendLine("and b.ConfirmDate<'" + Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd") + "'                                          ");
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
            sql.AppendLine("inner join officedba.StorageInOther b on b.InNo=a.InNo and b.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD             ");
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
            sql.AppendLine("and b.ConfirmDate<'" + Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd") + "'                                          ");
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
            sql.AppendLine("inner join officedba.StorageInRed b on b.InNo=a.InNo and b.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD   ");
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
            sql.AppendLine("and b.ConfirmDate<'" + Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd") + "'                                          ");
            comm.CommandText = sql.ToString();
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

        #region 出库查询
        //出库查询

        public static DataTable OutStockSearch(string Confirmor, StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
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
            sql.AppendLine("inner join officedba.StorageOutSell b on b.OutNo=a.OutNo and b.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD   ");
            sql.AppendLine("left join officedba.StorageInfo c on c.ID=a.StorageID                               ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=b.Confirmor                              ");
            sql.AppendLine("left join officedba.ProductInfo e on e.ID=a.ProductID                               ");
            sql.AppendLine("left join officedba.CodeUnitType f on f.ID=e.UnitID                                 ");
            sql.AppendLine("where b.CompanyCD='" + model.CompanyCD + "' and e.ProdNo='" + model.ProductNo + "'  ");
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
            sql.AppendLine("and b.ConfirmDate<'" + Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd") + "'                                          ");
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
            sql.AppendLine("inner join officedba.StorageOutOther b on b.OutNo=a.OutNo and b.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD ");
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
            sql.AppendLine("and b.ConfirmDate<'" + Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd") + "'                                          ");
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
            sql.AppendLine("inner join officedba.StorageOutRed b on b.OutNo=a.OutNo  and b.CompanyCD='" + model.CompanyCD + "' and a.CompanyCD=b.CompanyCD  ");
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
            sql.AppendLine("and b.ConfirmDate<'" + Convert.ToDateTime(model.EndDate).AddDays(1).ToString("yyyy-MM-dd") + "'                                          ");
            comm.CommandText = sql.ToString();
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

        #region 查询：库存报损单
        /// <summary>
        /// 查询库存报损单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageLossTableBycondition(StorageLossModel model, string timeStart, string timeEnd, string TotalPriceStart, string TotalPriceEnd, string FlowStatus, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a.ID                                                                        ");
            sql.AppendLine(",a.LossNo                                                                          ");
            sql.AppendLine(",a.Title                                                                           ");
            sql.AppendLine(",ISNULL(e.EmployeeName,'') as Executor                                             ");
            sql.AppendLine(",CASE isnull(i.FlowStatus,0)                                                       ");
            sql.AppendLine("WHEN 0 THEN ''                                                               ");
            sql.AppendLine("WHEN 1 THEN '待审批'                                                               ");
            sql.AppendLine("WHEN 2 THEN '审批中'                                                               ");
            sql.AppendLine("WHEN 3 THEN '审批通过'                                                             ");
            sql.AppendLine("WHEN 4 THEN '审批不通过'                                                       ");
            sql.AppendLine("WHEN 5 THEN '撤销审批' else ''                                                     ");
            sql.AppendLine("END FlowStatus                                                                     ");
            sql.AppendLine(",ISNULL(f.DeptName,'') as DeptName                                                 ");
            sql.AppendLine(",ISNULL(g.StorageName,'') as StorageName                                           ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),a.LossDate,21),'') AS LossDate                         ");
            sql.AppendLine(",ISNull(h.CodeName,'') as ReasonTypeName                                           ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0) as TotalPrice                                              ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更' ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName    ");
            sql.AppendLine("FROM officedba.StorageLoss a                                                 ");
            sql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Executor                              ");
            sql.AppendLine("left join officedba.DeptInfo f on f.ID=a.DeptID                                    ");
            sql.AppendLine("left join officedba.StorageInfo g on g.ID=a.StorageID                              ");
            sql.AppendLine("left join officedba.CodeReasonType h on h.ID=a.ReasonType                          ");
            sql.AppendLine("LEFT OUTER JOIN officedba.FlowInstance i ON a.LossNo=i.BillNo AND i.BillTypeFlag=" + ConstUtil.CODING_RULE_Storage_NO + "");
            sql.AppendLine("AND i.BillTypeCode='" + ConstUtil.CODING_RULE_StorageLoss_NO + "'							         ");
            sql.AppendLine(" AND i.ID=(SELECT max(ID) FROM officedba.FlowInstance AS j");
            sql.AppendLine(" WHERE a.ID = j.BillID AND j.BillTypeFlag = " + ConstUtil.CODING_RULE_Storage_NO + " AND j.BillTypeCode = " + ConstUtil.CODING_RULE_StorageLoss_NO + " )");
            sql.AppendLine("where a.CompanyCD=@CompanyCD");

            // 库存报损单编号、库存报损单主题、报损部门（弹出窗口选择）、报损仓库（选择）、经办人（弹出窗口选择）、报损时间（日期段，日期控件）
            //、报损原因（下拉列表）、成本金额合计（区间）、单据状态（下拉列表）、审批状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.LossNo))
            {
                sql.AppendLine("	and a.LossNo like '%'+ @LossNo +'%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@LossNo", model.LossNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine(" and a.StorageID = @StorageID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor = @Executor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor", model.Executor));
            }
            if (!string.IsNullOrEmpty(FlowStatus))
            {
                if (FlowStatus == "0")
                {
                    sql.AppendLine(" and i.FlowStatus is null");
                }
                else
                {
                    sql.AppendLine(" and i.FlowStatus=@FlowStatus");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", FlowStatus));
                }
            }
            if (!string.IsNullOrEmpty(timeStart))
            {
                sql.AppendLine(" and a.LossDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }

            if (!string.IsNullOrEmpty(timeEnd))
            {
                sql.AppendLine(" and a.LossDate<=@timeEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeEnd", timeEnd));
            }
            if (!string.IsNullOrEmpty(model.ReasonType))
            {
                sql.AppendLine(" and a.ReasonType=@ReasonType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType", model.ReasonType));
            }
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                sql.AppendLine(" and a.BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
            }
            if (!string.IsNullOrEmpty(TotalPriceStart))
            {
                sql.AppendLine(" and a.TotalPrice>=@TotalPriceStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPriceStart", TotalPriceStart));
            }

            if (!string.IsNullOrEmpty(TotalPriceEnd))
            {
                sql.AppendLine(" and a.TotalPrice<=@TotalPriceEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPriceEnd", TotalPriceEnd));
            }

            comm.CommandText = sql.ToString();
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

        #region 查询：库存查询
        /// <summary>
        /// 查询库存报损单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetProductStorageTableBycondition(StorageProductModel model, string ProductNo, string ProductName, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a.ID                                           ");
            sql.AppendLine("			,ISNULL(b.StorageNo,'') as StorageNo        ");
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
            sql.AppendLine("			,ISNULL(a.OutCount,0) as OutCount       ");
            sql.AppendLine("FROM officedba.StorageProduct a                       ");
            sql.AppendLine("left join officedba.StorageInfo b on a.StorageID=b.ID ");
            sql.AppendLine("left join officedba.DeptInfo c on c.ID=a.DeptID       ");
            sql.AppendLine("left join officedba.ProductInfo d on d.ID=a.ProductID ");
            sql.AppendLine("left join officedba.CodeUnitType e on e.ID=d.UnitID		");
            sql.AppendLine("   where a.CompanyCD='" + model.CompanyCD + "'");

            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine("	and a.StorageID = @StorageID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            if (!string.IsNullOrEmpty(ProductNo))
            {
                sql.AppendLine(" and d.ProdNo=@ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", ProductNo));
            }
            if (!string.IsNullOrEmpty(ProductName))
            {
                sql.AppendLine(" and d.ProductName like '%' + @ProductName + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", ProductName));
            }

            comm.CommandText = sql.ToString();
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


        #region 查询：库存限量报警
        /// <summary>
        /// 库存限量报警
        /// </summary>
        /// <param name="AlarmType">0-全部，1-上限报警，2-下限报警</param>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageProductAlarm(string AlarmType, StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("select ISNULL(a.ProdNo,'') as ProductNo,ISNULL(a.ProductName,'') as ProductName                           ");
            sql.AppendLine(",ISNULL(d.CodeName,'') as TypeID                                                                          ");
            sql.AppendLine(",ISNULL(a.Specification,'') as Specification                                                              ");
            sql.AppendLine(",ISNULL(c.CodeName,'') as UnitID                                                                          ");
            sql.AppendLine(",a.MinStockNum,a.MaxStockNum,ISNULL(a.SafeStockNum,0) as SafeStockNum                                     ");
            sql.AppendLine(",ISNULL(b.ProductCount,0) as ProductCount                                                                 ");
            sql.AppendLine(",case                                                                                                     ");
            sql.AppendLine("when (b.ProductCount > a.MaxStockNum) then '上限报警'                                                     ");
            sql.AppendLine("when (b.ProductCount < a.MinStockNum) then '下限报警'                                                     ");
            sql.AppendLine("else ''                                                                                                   ");
            sql.AppendLine("end AS AlarmType                                                                                          ");
            sql.AppendLine("from officedba.ProductInfo a                                                                              ");
            sql.AppendLine("right join (select a.ProductID,sum(ISNULL(a.ProductCount,0)) as ProductCount                              ");
            sql.AppendLine("				from officedba.StorageProduct a where a.CompanyCD=@CompanyCD                                  ");
            sql.AppendLine("				group by a.ProductID) b on a.ID=b.ProductID  --从分仓存量表中查询出group by ProductID的数据 ");
            sql.AppendLine("left join officedba.CodeUnitType c on c.ID=a.UnitID	                                                      ");
            sql.AppendLine("left join officedba.CodeProductType d on d.ID=a.TypeID                                                    ");
            sql.AppendLine("   where a.CompanyCD=@CompanyCD																																							");

            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            switch (AlarmType)
            {
                case "0":
                    sql.AppendLine("and (b.ProductCount>a.MaxStockNum                         ");
                    sql.AppendLine("or b.ProductCount<a.MinStockNum)   					      ");
                    break;
                case "1":
                    sql.AppendLine("and b.ProductCount>a.MaxStockNum                          ");
                    break;
                case "2":
                    sql.AppendLine("and b.ProductCount<a.MinStockNum						  ");
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(model.ProductNo))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProductNo));
            }
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                sql.AppendLine(" and a.ProductName like '%' + @ProductName + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", model.ProductName));
            }

            comm.CommandText = sql.ToString();
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


        #region 月结查询
        public static DataTable GetMothlyInfo(string MonthNo, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            sql.AppendLine("SELECT a.*,convert(char(20),Convert(decimal(18," + point + "),OldRealCost))+'&nbsp;' OldRealCost1,");
            sql.AppendLine(" Convert(char(20),Convert(decimal(18," + point + "),NowRealCost))+'&nbsp;' NowRealCost1, ");
            sql.AppendLine(" Convert(char(20),Convert(decimal(18," + point + "),OldCount))+'&nbsp;' OldCount1, ");
            sql.AppendLine(" Convert(char(20),Convert(decimal(18," + point + "),NowCount))+'&nbsp;' NowCount1, ");
            sql.AppendLine(" Convert(char(20),Convert(decimal(18," + point + "),OldTotal))+'&nbsp;' OldTotal1, ");
            sql.AppendLine(" Convert(char(20),Convert(decimal(18," + point + "),NowTotal))+'&nbsp;' NowTotal1 ");
            sql.AppendLine(" FROM officedba.V_StorageMonthlyEndSearch a");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "'");
            if (!string.IsNullOrEmpty(MonthNo))
            {
                sql.AppendLine(" and a.MonthNo=@MonthNo                                                 ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MonthNo", MonthNo));
            }
            if (!iflist)
            {
                if (ord != "asc" && ord != "desc")
                {
                    sql.AppendLine(" order by " + ord);
                }
                else
                {
                    sql.AppendLine(" order by ProdNo");
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


    }
}
