using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using XBase.Data.DBHelper;

namespace XBase.Data.Office.StorageManager
{
    public class StorageCost
    {


        #region 验证是否为第一次计算存货成本
        /// <summary>
        /// 验证是否为第一次计算存货成本
        /// </summary>
        /// <param name="companyCD">公司编码</param>
        /// <returns></returns>
        private static bool ValideFirst(string companyCD)
        {
            //验证是否为第一次计算
            StringBuilder sbCount = new StringBuilder();
            sbCount.AppendLine(" SELECT TOP 1  * FROM officedba.StorageCost WHERE CompanyCD=@CompanyCD ");
            SqlParameter[] countParams = new SqlParameter[1];
            countParams[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable dtCount = SqlHelper.ExecuteSql(sbCount.ToString(), countParams);

            //如果没有数据 则表示该公司第一次计算 则可以计算
            if (dtCount == null || dtCount.Rows.Count <= 0)
                return true;
            else
                return false;
        }
        #endregion

        #region 验证是否可以计算存货成本
        /// <summary>
        /// 验证上期存货成本是否计算过
        /// </summary>
        /// <param name="yearMonth">上期期数</param>
        /// <param name="companyCD">公司编码</param>
        /// <returns>true :可以计算，false：不可计算 </returns>
        private static bool Valdate(string yearMonth, string companyCD)
        {
            if (ValideFirst(companyCD))
                return true;

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT TOP 1 * FROM officedba.StorageCost WHERE YearMonth=@YearMonth AND CompanyCD=@CompanyCD ");
            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", yearMonth);
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            //如果没有数据 则不可计算
            if (dt != null && dt.Rows.Count > 0)
                return false;
            //有数据 则可计算 
            else
                return true;
        }

        #endregion

        #region 获取上期的时间段
        /// <summary>
        /// 获取上期最后的处理时间段
        /// </summary>
        /// <param name="yearMonth">上期期数</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static string GetLastCalculationDate(string yearMonth, string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT TOP 1 * FROM officedba.StorageCost  ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND YearMonth=@YearMonth");

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", yearMonth);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["EndDate"].ToString();
            else
                return string.Empty;

        }
        #endregion

        #region  计算存货成本所需的数据
        /// <summary>
        /// 获取计算后的成本数据 除首次计算
        /// </summary>
        /// <param name="currentStartDate">本期开始日期</param>
        /// <param name="currentEndDate">本期结束日期</param>
        /// <param name="companyCD">公司编码</param>
        /// <param name="yearMonth">上期期数</param>
        /// <returns></returns>
        private static DataTable GetCurrentData(string currentStartDate, string currentEndDate, string companyCD, string yearMonth)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT a.* ");
            sbSql.AppendLine(" ,b.PeriodEndCost  --上期末成本(月初存货成本) ");
            sbSql.AppendLine(" ,b.PeriodEndCount --上期末数量(月初存货数量) ");
            sbSql.AppendLine(" ,((ISNULL(a.CurrentInCost,0)+ISNULL(b.PeriodEndCost,0))/((ISNULL(a.CurrentInCount,0)+ISNULL(b.PeriodEndCount,0)))) AS avgCost ");
            sbSql.AppendLine(" --加权平均单位成本 公式：(月初存货成本+本期存货成本)/(月初存货数量+本期存货数量) ");
            sbSql.AppendLine(" ,((ISNULL(a.CurrentInCount,0)+ISNULL(b.PeriodEndCount,0))*((ISNULL(a.CurrentInCost,0)+ISNULL(b.PeriodEndCost,0))/((ISNULL(a.CurrentInCount,0)+ISNULL(b.PeriodEndCount,0))))) AS CurrentCost ");
            sbSql.AppendLine(" --本期存货成本 公式：(本期购进存货数量+月初库存数量)*加权平均单位成本 ");
            sbSql.AppendLine(" ,(ISNULL(a.CurrentInCount,0)+ISNULL(b.PeriodEndCount,0)) AS CurrentCount ");
            sbSql.AppendLine(" FROM  ");
            sbSql.AppendLine(" ( ");
            sbSql.AppendLine(" SELECT SUM((ISNULL(a.ProductCount,0)*ISNULL(a.Price,0))) AS CurrentInCost --本期购进存货成本 ");
            sbSql.AppendLine(" ,SUM(ISNULL(a.ProductCount,0)) AS CurrentInCount --本期购进存货数量 ");
            sbSql.AppendLine(" ,a.ProductID,a.CompanyCD ");
            sbSql.AppendLine(" FROM  ");
            sbSql.AppendLine(" officedba.StorageAccount AS a ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD ");
            sbSql.AppendLine(" AND ");
            sbSql.AppendLine(" CONVERT(DATETIME,CONVERT(varchar(10),HappenDate))>@CurrentStartDate  ");
            sbSql.AppendLine(" AND ");
            sbSql.AppendLine(" CONVERT(DATETIME,CONVERT(varchar(10),HappenDate))<=@CurrentEndDate ");
            sbSql.AppendLine(" AND ");
            sbSql.AppendLine(" BillType IN (1,2,3,4,5,9,13,18,20,22) ");
            sbSql.AppendLine(" /*1期初库存录入、2期初库存批量导入、3采购入库单、 ");
            sbSql.AppendLine(" 4生产完工入库单、5其他入库单、9红冲出库单 ");
            sbSql.AppendLine(" 11借货返还单、13调拨入库、 ");
            sbSql.AppendLine(" 18退料单、20配送退货单（验收入库））、 ");
            sbSql.AppendLine(" 22 门店销售退货（发货模式为：总店发货）*/ ");
            sbSql.AppendLine(" GROUP BY a.ProductID,a.CompanyCD ");
            sbSql.AppendLine(" ) AS a --本期存货成本与存货数量 ");
            sbSql.AppendLine(" LEFT JOIN officedba.StorageCost AS b ON a.ProductID=b.ProductID ");
            sbSql.AppendLine(" WHERE b.YearMonth=@YearMonth ");


            SqlParameter[] sqlParams = new SqlParameter[4];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentStartDate", currentStartDate);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentStartDate", currentEndDate);
            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", yearMonth);
            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }


        /// <summary>
        /// 首次计算存货成本
        /// </summary>
        /// <param name="currentStartDate">开始日期</param>
        /// <param name="currtentEndDate">结束日期</param>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        private static DataTable GetCurrentData(string currentStartDate, string currtentEndDate, string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" ");
            sbSql.AppendLine(" SELECT SUM((ISNULL(a.ProductCount,0)*ISNULL(a.Price,0))) AS CurrentInCost --本期购进存货成本 ");
            sbSql.AppendLine(" ,SUM(ISNULL(a.ProductCount,0)) AS CurrentInCount --本期购进存货数量 ");
            sbSql.AppendLine(" ,a.ProductID,a.CompanyCD ");
            sbSql.AppendLine(" ,(SUM((ISNULL(a.ProductCount,0)*ISNULL(a.Price,0)))/SUM(ISNULL(a.ProductCount,0)) ) AS CurrentCost --加权存货成本 ");
            sbSql.AppendLine(" FROM  ");
            sbSql.AppendLine(" officedba.StorageAccount AS a ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD ");
            sbSql.AppendLine(" AND ");
            sbSql.AppendLine(" CONVERT(DATETIME,CONVERT(varchar(10),HappenDate))>@CurrentStartDate  ");
            sbSql.AppendLine(" AND ");
            sbSql.AppendLine(" CONVERT(DATETIME,CONVERT(varchar(10),HappenDate))<=@CurrentEndDate ");
            sbSql.AppendLine(" AND ");
            sbSql.AppendLine(" BillType IN (1,2,3,4,5,9,13,18,20,22) ");
            sbSql.AppendLine(" /*1期初库存录入、2期初库存批量导入、3采购入库单、 ");
            sbSql.AppendLine(" 4生产完工入库单、5其他入库单、9红冲出库单 ");
            sbSql.AppendLine(" 11借货返还单、13调拨入库、 ");
            sbSql.AppendLine(" 18退料单、20配送退货单（验收入库））、 ");
            sbSql.AppendLine(" 22 门店销售退货（发货模式为：总店发货）*/ ");
            sbSql.AppendLine(" GROUP BY a.ProductID,a.CompanyCD ");

            SqlParameter[] sqlParams = new SqlParameter[3];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentStartDate", currentStartDate);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentEndDate", currtentEndDate);

            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }

        #endregion

        #region 计算存货成本 并存入数据库
        public static string CalculationStorageCost(string companyCD, string currentYearMonth, string preYearMonth, string startDate, string endDate, int UserID)
        {
            try
            {

                //验证上期存货成本是否计算
                if (!Valdate(preYearMonth, companyCD))
                    return "1|对不起，上期存货成本还为计算，请先计算上期存货成本！";
                else
                {
                    List<SqlCommand> cmdList = new List<SqlCommand>();

                    #region 验证是否存在当期的存货成本
                    if (ValidateCurrentData(currentYearMonth, companyCD))
                    {
                        StringBuilder sbDel = new StringBuilder();
                        sbDel.AppendLine(" DELETE officedba.StorageCost ");
                        sbDel.AppendLine(" WHERE CompanyCD=@CompanyCD AND YearMonth=@YearMonth ");

                        SqlParameter[] delParams = new SqlParameter[2];
                        int delIndex = 0;
                        delParams[delIndex++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
                        delParams[delIndex++] = SqlHelper.GetParameter("@YearMonth", currentYearMonth);


                        SqlCommand delCmd = new SqlCommand();
                        delCmd.CommandText = sbDel.ToString();
                        delCmd.Parameters.AddRange(delParams);

                        cmdList.Add(delCmd);
                    }
                    #endregion

                    DataTable dt = null;
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendLine(" INSERT INTO officedba.StorageCost ");
                    sbSql.AppendLine("  (CompanyCD,YearMonth,StartDate,EndDate,ProductID,PeriodBeginCost,PeriodBeginCount,PeriodEndCost,PeriodEndCount,ActionUserID,ActionDate) ");
                    sbSql.AppendLine(" VALUES ");
                    sbSql.AppendLine("  (@CompanyCD,@YearMonth,@StartDate,@EndDate,@ProductID,@PeriodBeginCost,@PeriodBeginCount,@PeriodEndCost,@PeriodEndCount,@ActionUserID,ActionDate) ");
                    //验证是否为首次计算存货成本
                    if (ValideFirst(companyCD))
                    {
                        //调用第一次的计算方法
                        dt = GetCurrentData(startDate, endDate, companyCD);

                        #region 
                        foreach (DataRow row in dt.Rows)
                        {
                            SqlParameter[] sqlParams = new SqlParameter[10];
                            int index = 0;
                            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
                            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", currentYearMonth);
                            sqlParams[index++] = SqlHelper.GetParameter("@StartDate", startDate);
                            sqlParams[index++] = SqlHelper.GetParameter("@EndDate", endDate);
                            sqlParams[index++] = SqlHelper.GetParameter("@ProductID", row["ProductID"].ToString());
                            sqlParams[index++] = SqlHelper.GetParameter("@PeriodBeginCost", 0);
                            sqlParams[index++] = SqlHelper.GetParameter("@PeriodBeginCount", 0);
                            sqlParams[index++] = SqlHelper.GetParameter("@PeriodEndCost", row["CurrentCost"].ToString());
                            sqlParams[index++] = SqlHelper.GetParameter("@PeriodEndCount", row["CurrentInCount"].ToString());
                            sqlParams[index++] = SqlHelper.GetParameter("@ActionUserID", UserID);
                            sqlParams[index++] = SqlHelper.GetParameter("@ActionDate", DateTime.Now);

                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = sbSql.ToString();
                            cmd.Parameters.AddRange(sqlParams);

                            cmdList.Add(cmd);
                        }
                        #endregion
                    }
                    else
                    {
                        //使用除第一次之外的通过计算方法 获得基本数据
                        dt = GetCurrentData(startDate, endDate, companyCD, preYearMonth);

                        #region 构造SQL 将计算出的存货成本 存入数据库
                        foreach (DataRow row in dt.Rows)
                        {
                            SqlParameter[] sqlParams = new SqlParameter[10];
                            int index = 0;
                            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
                            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", currentYearMonth);
                            sqlParams[index++] = SqlHelper.GetParameter("@StartDate", startDate);
                            sqlParams[index++] = SqlHelper.GetParameter("@EndDate", endDate);
                            sqlParams[index++] = SqlHelper.GetParameter("@ProductID", row["ProductID"].ToString());
                            sqlParams[index++] = SqlHelper.GetParameter("@PeriodBeginCost", row["PeriodEndCost"].ToString());
                            sqlParams[index++] = SqlHelper.GetParameter("@PeriodBeginCount", row["PeriodEndCount"].ToString());
                            sqlParams[index++] = SqlHelper.GetParameter("@PeriodEndCost", row["CurrentCost"].ToString());
                            sqlParams[index++] = SqlHelper.GetParameter("@PeriodEndCount", row["CurrentCount"].ToString());
                            sqlParams[index++] = SqlHelper.GetParameter("@ActionUserID", UserID);
                            sqlParams[index++] = SqlHelper.GetParameter("@ActionDate", DateTime.Now);

                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = sbSql.ToString();
                            cmd.Parameters.AddRange(sqlParams);

                            cmdList.Add(cmd);
                        }
                        #endregion

                    }

                    //执行SQL 
                    bool res = SqlHelper.ExecuteTransWithCollections(cmdList);
                    if (res)
                        return "0|存货成本计算成功";
                    else
                        return "1|存货成本计算失败";
                }
            }
            catch (Exception ex)
            {
                return "1|存货成本计算失败，原因：" + ex.ToString();
            }

        }
        #endregion

        #region 判断是否存在指定期数的存货成本 
        private static bool ValidateCurrentData(string yearMonth, string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine(" SELECT TOP 1  * FROM officedba.StorageCost  ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND YearMonth=@YearMonth ");

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index=0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", yearMonth);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);

            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
            

 
            
        }
        #endregion



    }
}
