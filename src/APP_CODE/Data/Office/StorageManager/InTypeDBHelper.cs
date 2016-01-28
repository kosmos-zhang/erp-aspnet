/**********************************************
 * 类作用：   期初库存数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/17
 ***********************************************/
using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;

namespace XBase.Data.Office.StorageManager
{
    public class InTypeDBHelper
    {
        /// <summary>
        /// 查出对应入库类型的基本信息,弹出层信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="InType"></param>
        /// <returns></returns>
        public static DataTable GetInTypeInfo(string CompanyCD, string InType, string InNo, string Title)
        {
            DateTime nowDate = System.DateTime.Now;
            string monthdays = DateTime.DaysInMonth(nowDate.Year, nowDate.Month).ToString();
            string DateMonth = nowDate.ToString("yyyy-MM");
            string EndDate = DateMonth + "-" + monthdays;//本月的结束日期
            string StartDate = DateMonth + "-" + "01";//本月的开始日期（1号）
            StringBuilder strSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            switch (InType)
            {

                case "1":
                    strSql.AppendLine("select ID,");
                    strSql.AppendLine("ISNULL(InNo,'') as InNo,'采购入库单' as InType,");
                    strSql.AppendLine("ISNULL(Title,'') as Title,");
                    strSql.AppendLine("ISNULL(CountTotal,0) as CountTotal");
                    strSql.AppendLine("from officedba.StorageInPurchase where CompanyCD='" + CompanyCD + "' and BillStatus=2");
                    strSql.AppendLine(" and ConfirmDate>='" + StartDate + "'");
                    strSql.AppendLine(" and ConfirmDate<='" + EndDate + "'");
                    //定义查询的命令
                    //添加公司代码参数
                    if (!string.IsNullOrEmpty(InNo))
                    {
                        strSql.AppendLine(" and InNo like '%'+ @InNo +'%'");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo", InNo));
                    }
                    if (!string.IsNullOrEmpty(Title))
                    {
                        strSql.AppendLine(" and Title like '%'+ @Title +'%'");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
                    }
                    comm.CommandText = strSql.ToString();
                    break;
                case "2":
                    strSql.AppendLine("select ID,");
                    strSql.AppendLine("ISNULL(InNo,'') as InNo,'生产完工入库单' as InType,");
                    strSql.AppendLine("ISNULL(Title,'') as Title,");
                    strSql.AppendLine("ISNULL(CountTotal,0) as CountTotal");
                    strSql.AppendLine("from officedba.StorageInProcess where CompanyCD='" + CompanyCD + "' and BillStatus=2");
                    strSql.AppendLine(" and ConfirmDate>='" + StartDate + "'");
                    strSql.AppendLine(" and ConfirmDate<='" + EndDate + "'");
                    if (!string.IsNullOrEmpty(InNo))
                    {
                        strSql.AppendLine(" and InNo like '%'+ @InNo +'%'");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo", InNo));
                    }
                    if (!string.IsNullOrEmpty(Title))
                    {
                        strSql.AppendLine(" and Title like '%'+ @Title +'%'");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
                    }
                    comm.CommandText = strSql.ToString();
                    break;
                case "3":
                    strSql.AppendLine("select ID,");
                    strSql.AppendLine("ISNULL(InNo,'') as InNo,'其他入库单' as InType,");
                    strSql.AppendLine("ISNULL(Title,'') as Title,");
                    strSql.AppendLine("ISNULL(CountTotal,0) as CountTotal");
                    strSql.AppendLine("from officedba.StorageInOther where CompanyCD='" + CompanyCD + "' and BillStatus=2");
                    strSql.AppendLine(" and ConfirmDate>='" + StartDate + "'");
                    strSql.AppendLine(" and ConfirmDate<='" + EndDate + "'");
                    if (!string.IsNullOrEmpty(InNo))
                    {
                        strSql.AppendLine(" and InNo like '%'+ @InNo +'%'");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo", InNo));
                    }
                    if (!string.IsNullOrEmpty(Title))
                    {
                        strSql.AppendLine(" and Title like '%'+ @Title +'%'");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
                    }
                    comm.CommandText = strSql.ToString();
                    break;
                default:
                    break;
            }
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 根据的入库编号查询出相关信息及其明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="InType"></param>
        /// <param name="InNo"></param>
        /// <returns></returns>
        public static DataTable GetDetailInfo(string CompanyCD, string InType, string InNo)
        {
            string tablename = "";
            string detailtablename = "";
            StringBuilder strSql = new StringBuilder();

            switch (InType)
            {
                case "1":
                    tablename = "officedba.StorageInPurchase";
                    detailtablename = "officedba.StorageInPurchaseDetail";
                    break;
                case "2":
                    tablename = "officedba.StorageInProcess";
                    detailtablename = "officedba.StorageInProcessDetail";
                    break;
                case "3":
                    tablename = "officedba.StorageInOther";
                    detailtablename = "officedba.StorageInOtherDetail";
                    break;
                case "":
                    return null;
                default:
                    break;
            }
            strSql.AppendLine("select a.ID,a.InNo,'其他入库单' as InType,a.Title");
            strSql.AppendLine(",ISNULL(a.CountTotal,0) as CountTotal   ");
            strSql.AppendLine(",ISNULL(a.Summary,'') as Summary   ");
            strSql.AppendLine(",a.DeptID                                                          ");
            strSql.AppendLine(",r.EmployeeName as Executor                                       ");
            strSql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate");
            strSql.AppendLine(",ISNULL(s.DeptName,'') as DeptName                                ");
            strSql.AppendLine(",b.ProductID                                                      ");
            strSql.AppendLine(",p.ProdNo                                                         ");
            strSql.AppendLine(",p.ProductName                                                    ");
            strSql.AppendLine(",p.IsBatchNo ");
            strSql.AppendLine(",p.Specification                                                  ");
            strSql.AppendLine(",ISNULL(p.MinusIs,0) as MinusIs                                   ");
            strSql.AppendLine(",q.CodeName,p.UnitID                                              ");
            strSql.AppendLine(",b.ProductCount                                                   ");
            strSql.AppendLine(",b.UnitPrice                                                      ");
            strSql.AppendLine(",b.StorageID                                                      ");
            strSql.AppendLine(",b.SortNo,b.UsedUnitID, ISNULL(b.UsedPrice,0)UsedPrice,b.BatchNo");
            strSql.AppendLine(",b.Remark                                                         ");
            strSql.AppendLine(",ISNULL(t.ProductCount,0)+ISNULL(t.RoadCount,0)+ISNULL(t.InCount,0)-ISNULL(t.OrderCount,0)-ISNULL(t.OutCount,0) as UseCount ");
            strSql.AppendLine("from " + tablename + " a                                          ");
            strSql.AppendLine("left join " + detailtablename + " b on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD              ");
            strSql.AppendLine("left join officedba.ProductInfo p on p.ID=b.ProductID             ");
            strSql.AppendLine("left join officedba.CodeUnitType q on q.ID=p.UnitID               ");
            strSql.AppendLine("left join officedba.EmployeeInfo r on r.ID=a.Executor             ");
            strSql.AppendLine("left join officedba.DeptInfo s on s.ID=a.DeptID	        	 	 ");
            strSql.AppendLine("left join officedba.StorageProduct t on t.CompanyCD=a.CompanyCD and t.StorageID=b.StorageID and b.ProductID=t.ProductID and b.BatchNo = t.BatchNo ");
            strSql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.InNo='" + InNo + "'   	 ");

            return SqlHelper.ExecuteSql(strSql.ToString());
        }
    }
}
