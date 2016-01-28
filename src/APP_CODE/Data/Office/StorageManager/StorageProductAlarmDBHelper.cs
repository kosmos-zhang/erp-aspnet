using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using System.Collections;
using XBase.Model.Office.StorageManager;

namespace XBase.Data.Office.StorageManager
{
    public class StorageProductAlarmDBHelper
    {
        #region 查询：库存限量报警
        /// <summary>
        /// 库存限量报警
        /// </summary>
        /// <param name="AlarmType">0-全部，1-上限报警，2-下限报警</param>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageProductAlarm(string AlarmType, StockAccountModel model, string BarCode, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("select ISNULL(a.ProdNo,'') as ProductNo,ISNULL(a.ProductName,'') as ProductName                           ");
            sql.AppendLine(",ISNULL(d.CodeName,'') as TypeID                                                                          ");
            sql.AppendLine(",ISNULL(a.Specification,'') as Specification                                                              ");
            sql.AppendLine(",ISNULL(c.CodeName,'') as UnitID                                                                          ");
            sql.AppendLine(",Convert(numeric(22," + userInfo.SelPoint + "),a.MinStockNum) as MinStockNum,Convert(numeric(22," + userInfo.SelPoint + "),a.MaxStockNum) as MaxStockNum,Convert(numeric(22," + userInfo.SelPoint + "),ISNULL(a.SafeStockNum,0)) as SafeStockNum                                     ");
            sql.AppendLine(",Convert(numeric(22," + userInfo.SelPoint + "),ISNULL(b.ProductCount,0)) as ProductCount                                                                 ");
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
            if (!string.IsNullOrEmpty(BarCode))
            {
                sql.AppendLine(" and a.BarCode like '%' + @BarCode + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", BarCode));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }


        public static DataTable GetStorageProductAlarm(string AlarmType, StockAccountModel model, string orderby, string BarCode)
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

            if (!string.IsNullOrEmpty(BarCode))
            {
                sql.AppendLine(" and a.BarCode like '%' + @BarCode + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", BarCode));
            }

            if (!string.IsNullOrEmpty(orderby))
            {
                sql.AppendLine(" order by " + orderby);
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #region 查询：库存限量报警
        /// <summary>
        /// 库存限量报警
        /// </summary>
        /// <param name="AlarmType">0-全部，1-上限报警，2-下限报警</param>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageProductAlarmForRemind(string AlarmType, StockAccountModel model, string BarCode, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("select ISNULL(a.ProdNo,'') as ProductNo,ISNULL(a.ProductName,'') as ProductName                           ");
            sql.AppendLine(",ISNULL(d.CodeName,'') as TypeID                                                                          ");
            sql.AppendLine(",ISNULL(a.Specification,'') as Specification                                                              ");
            sql.AppendLine(",ISNULL(c.CodeName,'') as UnitID                                                                          ");
            sql.AppendLine(",Convert(numeric(22,2),a.MinStockNum) as MinStockNum,Convert(numeric(22,2),a.MaxStockNum) as MaxStockNum,Convert(numeric(22,2),ISNULL(a.SafeStockNum,0)) as SafeStockNum                                     ");
            sql.AppendLine(",Convert(numeric(22,2),ISNULL(b.ProductCount,0)) as ProductCount                                                                 ");
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
            if (!string.IsNullOrEmpty(BarCode))
            {
                sql.AppendLine(" and a.BarCode like '%' + @BarCode + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", BarCode));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }        
        #endregion
    }
}
