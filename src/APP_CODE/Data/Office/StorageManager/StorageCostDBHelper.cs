using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using XBase.Data.DBHelper;

namespace XBase.Data.Office.StorageManager
{
    public class StorageCostDBHelper
    {


        private const string BILLTYPE = "1,2,3,4,5,9,13,18,20,22";



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
        /// <param name="yearMonth">期数</param>
        /// <param name="companyCD">公司编码</param>
        /// <returns> </returns>
        private static bool Validate(string yearMonth, string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT TOP 1 * FROM officedba.StorageCost WHERE YearMonth=@YearMonth AND CompanyCD=@CompanyCD ");
            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", yearMonth);
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            if (dt == null || dt.Rows.Count <= 0)
                return false;
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
                return dt.Rows[0]["StartDate"].ToString() + "|" + dt.Rows[0]["EndDate"].ToString();
            else
                return string.Empty;

        }
        #endregion

        #region  计算存货成本所需的数据
        /// <summary>
        /// 获得本期与上期的交集数据 计算出存货成本 即本期与上期中同一产品都发生库存变化
        /// </summary>
        /// <param name="currentStartDate">本期开始日期</param>
        /// <param name="currentEndDate">本期结束日期</param>
        /// <param name="companyCD">公司编码</param>
        /// <param name="yearMonth">上期期数</param>
        /// <returns></returns>
        private static DataTable GetCurrentIntersectionData(string currentStartDate, string currentEndDate, string companyCD, string yearMonth)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT a.*  ");
            sbSql.AppendLine(",(ISNULL(b.PeriodEndCost,0) ) AS PeriodBeginCost  --上期末单位成本(月初存货单位成本)  ");
            sbSql.AppendLine(",ISNULL(b.PeriodEndCount,0) AS PeriodBeginCount --上期末数量(月初存货数量)  ");
            sbSql.AppendLine(",(CASE ((ISNULL(a.CurrentInCount,0)+ISNULL(b.PeriodEndCount,0))) WHEN 0 ");
            sbSql.AppendLine("THEN 0 ");
            sbSql.AppendLine("ELSE ");
            sbSql.AppendLine("((ISNULL(a.CurrentInCost,0)+ISNULL((ISNULL(b.PeriodEndCost,0)*ISNULL(b.PeriodEndCount,0) ),0))/((ISNULL(a.CurrentInCount,0)+ISNULL(b.PeriodEndCount,0)))) END) AS PeriodEndCost  ");
            sbSql.AppendLine("--加权平均单位成本 公式：(月初存货成本+本期存货成本)/(月初存货数量+本期存货数量)  ");
            sbSql.AppendLine(",(CASE ((ISNULL(a.CurrentInCount,0)+ISNULL(b.PeriodEndCount,0))) WHEN 0 THEN 0 ");
            sbSql.AppendLine("ELSE ");
            sbSql.AppendLine("((ISNULL(a.CurrentInCount,0)+ISNULL(b.PeriodEndCount,0))*((ISNULL(a.CurrentInCost,0)+ISNULL(b.PeriodEndCost,0))/((ISNULL(a.CurrentInCount,0)+ISNULL(b.PeriodEndCount,0))))) END ) AS CurrentCost  ");
            sbSql.AppendLine(" --本期存货成本 公式：(本期购进存货数量+月初库存数量)*加权平均单位成本  ");
            sbSql.AppendLine(",(ISNULL(a.CurrentInCount,0)+ISNULL(b.PeriodEndCount,0)) AS PeriodEndCount --本期存货数量  ");
            sbSql.AppendLine("FROM   ");
            sbSql.AppendLine("(  ");
            sbSql.AppendLine("SELECT SUM((ISNULL(a.HappenCount,0)*ISNULL(a.Price,0))) AS CurrentInCost --本期购进存货成本  ");
            sbSql.AppendLine(",SUM(ISNULL(a.HappenCount,0)) AS CurrentInCount --本期购进存货数量  ");
            sbSql.AppendLine(",a.ProductID,a.CompanyCD  ");
            sbSql.AppendLine("FROM   ");
            sbSql.AppendLine("officedba.StorageAccount AS a  ");
            sbSql.AppendLine("WHERE CompanyCD=@CompanyCD  ");
            sbSql.AppendLine("AND  ");
            sbSql.AppendLine("CONVERT(DATETIME,CONVERT(varchar(10),HappenDate))>@CurrentStartDate   ");
            sbSql.AppendLine("AND  ");
            sbSql.AppendLine("CONVERT(DATETIME,CONVERT(varchar(10),HappenDate))<=@CurrentEndDate  ");
            sbSql.AppendLine("AND  ");
            sbSql.AppendLine("BillType IN (" + BILLTYPE + ")  ");
            sbSql.AppendLine("/*1期初库存录入、2期初库存批量导入、3采购入库单、  ");
            sbSql.AppendLine("4生产完工入库单、5其他入库单、9红冲出库单  ");
            sbSql.AppendLine("11借货返还单、13调拨入库、  ");
            sbSql.AppendLine("18退料单、20配送退货单（验收入库））、  ");
            sbSql.AppendLine("22 门店销售退货（发货模式为：总店发货）*/  ");
            sbSql.AppendLine("GROUP BY a.ProductID,a.CompanyCD  ");
            sbSql.AppendLine(") AS a --本期存货成本与存货数量  ");
            sbSql.AppendLine("LEFT JOIN officedba.StorageCost AS b ON a.ProductID=b.ProductID  ");
            sbSql.AppendLine("WHERE b.YearMonth=@YearMonth  ");

            SqlParameter[] sqlParams = new SqlParameter[4];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentStartDate", currentStartDate);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentEndDate", currentEndDate);
            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", yearMonth);
            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }

        /// <summary>
        /// 获得本期中未发生库存变化物品的上期的存货成本
        /// </summary>
        /// <param name="currentStartDate">本期开始日期</param>
        /// <param name="currentEndDate">本期结束日期</param>
        /// <param name="companyCD">公司编码</param>
        /// <param name="preYearMonth">上期年月</param>
        /// <returns></returns>
        private static DataTable GetCurrentPreData(string currentStartDate, string currentEndDate, string companyCD, string preYearMonth)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT a.PeriodBeginCost AS PeriodBeginCost --期初存货单位成本");
            sbSql.AppendLine(" ,a.PeriodBeginCount AS PeriodBeginCount --期初存货数量 ");
            sbSql.AppendLine(" ,a.PeriodEndCost AS PeriodEndCost --期末存货单位成本");
            sbSql.AppendLine(" ,a.PeriodEndCount AS PeriodEndCount --期末存货数量 ");
            sbSql.AppendLine(" ,a.ProductID  ");
            sbSql.AppendLine(" FROM officedba.StorageCost AS a ");
            sbSql.AppendLine(" WHERE a.ProductID NOT IN ");
            sbSql.AppendLine(" ( ");
            sbSql.AppendLine(" SELECT   ");
            sbSql.AppendLine(" a.ProductID ");
            sbSql.AppendLine(" FROM   ");
            sbSql.AppendLine(" officedba.StorageAccount AS a  ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD  ");
            sbSql.AppendLine(" AND  ");
            sbSql.AppendLine(" CONVERT(DATETIME,CONVERT(varchar(10),HappenDate))>@CurrentStartDate   ");
            sbSql.AppendLine(" AND  ");
            sbSql.AppendLine(" CONVERT(DATETIME,CONVERT(varchar(10),HappenDate))<=@CurrentEndDate  ");
            sbSql.AppendLine(" AND  ");
            sbSql.AppendLine(" BillType IN (" + BILLTYPE + ")  ");
            sbSql.AppendLine(" /*1期初库存录入、2期初库存批量导入、3采购入库单、  ");
            sbSql.AppendLine(" 4生产完工入库单、5其他入库单、9红冲出库单  ");
            sbSql.AppendLine(" 11借货返还单、13调拨入库、  ");
            sbSql.AppendLine(" 18退料单、20配送退货单（验收入库））、  ");
            sbSql.AppendLine(" 22 门店销售退货（发货模式为：总店发货）*/  ");
            sbSql.AppendLine(" GROUP BY a.ProductID,a.CompanyCD  ");
            sbSql.AppendLine(" ) ");
            sbSql.AppendLine(" AND YearMonth=@YearMonth ");


            SqlParameter[] sqlParams = new SqlParameter[4];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentStartDate", currentStartDate);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentEndDate", currentEndDate);
            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", preYearMonth);


            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }

        /// <summary>
        /// 计算某些物品首次存货成本，即在上期中不存在该物品的存货成本
        /// </summary>
        /// <param name="currentStartDate">开始日期</param>
        /// <param name="currtentEndDate">结束日期</param>
        /// <param name="companyCD">公司编码</param>
        /// <param name="preYearMonth">上期期数</param>
        /// <returns></returns>
        private static DataTable GetCurrentData(string currentStartDate, string currtentEndDate, string companyCD, string preYearMonth)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT SUM((ISNULL(a.HappenCount,0)*ISNULL(a.Price,0))) AS PeriodEndCost1 --期末成本 ");
            sbSql.AppendLine(" ,SUM(ISNULL(a.HappenCount,0)) AS PeriodEndCount --期末存货数量 ");
            sbSql.AppendLine(", CONVERT(numeric(22,6),0) AS PeriodBeginCost --期初单位成本 为0");
            sbSql.AppendLine(", CONVERT(numeric(22,6),0) AS PeriodBeginCount --期初存货数量 为0");
            sbSql.AppendLine(" ,a.ProductID,a.CompanyCD ");
            sbSql.AppendLine(" ,(CASE SUM(ISNULL(a.HappenCount,0))  ");
            sbSql.AppendLine("WHEN 0 THEN 0 ");
            sbSql.AppendLine("ELSE ");
            sbSql.AppendLine("(SUM((ISNULL(a.HappenCount,0)*ISNULL(a.Price,0)))/SUM(ISNULL(a.HappenCount,0)) ) END ) AS PeriodEndCost  --加权存货成本");
            //  sbSql.AppendLine(" ,(SUM((ISNULL(a.ProductCount,0)*ISNULL(a.Price,0)))/SUM(ISNULL(a.ProductCount,0)) ) AS CurrentCost --加权存货成本 ");
            sbSql.AppendLine(" FROM  ");
            sbSql.AppendLine(" officedba.StorageAccount AS a ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD ");
            sbSql.AppendLine(" AND ");
            sbSql.AppendLine(" CONVERT(DATETIME,CONVERT(varchar(10),HappenDate))>@CurrentStartDate  ");
            sbSql.AppendLine(" AND ");
            sbSql.AppendLine(" CONVERT(DATETIME,CONVERT(varchar(10),HappenDate))<=@CurrentEndDate ");
            sbSql.AppendLine(" AND ");
            sbSql.AppendLine(" BillType IN ("+BILLTYPE+") ");
            sbSql.AppendLine(" /*1期初库存录入、2期初库存批量导入、3采购入库单、 ");
            sbSql.AppendLine(" 4生产完工入库单、5其他入库单、9红冲出库单 ");
            sbSql.AppendLine(" 11借货返还单、13调拨入库、 ");
            sbSql.AppendLine(" 18退料单、20配送退货单（验收入库））、 ");
            sbSql.AppendLine(" 22 门店销售退货（发货模式为：总店发货）*/ ");
            sbSql.AppendLine(" AND a.ProductID NOT IN ");
            sbSql.AppendLine(" ( SELECT ProductID FROM officedba.StorageCost WHERE YearMonth=@YearMonth  AND CompanyCD=@CompanyCD ) ");
            sbSql.AppendLine(" GROUP BY a.ProductID,a.CompanyCD ");

            SqlParameter[] sqlParams = new SqlParameter[4];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentStartDate", currentStartDate);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentEndDate", currtentEndDate);
            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", preYearMonth);

            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }

        #endregion

        #region 计算存货成本 并存入数据库
        public static string CalculationStorageCost(string companyCD, string currentYearMonth, string preYearMonth, string startDate, string endDate, int UserID)
        {
            try
            {
                List<SqlCommand> cmdList = new List<SqlCommand>();

                #region 验证是否存在当期的存货成本
                if (Validate(currentYearMonth, companyCD))
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

                //获取交集部分数据
                DataTable dtIntersection = GetCurrentIntersectionData(startDate, endDate, companyCD, preYearMonth);
                //本期首次发生库存变化的存货成本
                DataTable dtCurrent = GetCurrentData(startDate, endDate, companyCD, preYearMonth);
                //本期未出现库存变化的存货成本
                DataTable dtPre = GetCurrentPreData(startDate, endDate, companyCD, preYearMonth);

                SqlCommand[] intersectionCmd = GetSotrageCostSqlCmd(dtIntersection, companyCD, currentYearMonth, preYearMonth, startDate, endDate, UserID);
                SqlCommand[] currentCmd = GetSotrageCostSqlCmd(dtCurrent, companyCD, currentYearMonth, preYearMonth, startDate, endDate, UserID);
                SqlCommand[] preCmd = GetSotrageCostSqlCmd(dtPre, companyCD, currentYearMonth, preYearMonth, startDate, endDate, UserID);

                if (intersectionCmd != null)
                    cmdList.AddRange(intersectionCmd);
                if (currentCmd != null)
                    cmdList.AddRange(currentCmd);
                if (preCmd != null)
                    cmdList.AddRange(preCmd);
                //执行SQL 
                bool res = SqlHelper.ExecuteTransWithCollections(cmdList);
                if (res)
                    return "0|存货成本计算成功";
                else
                    return "1|存货成本计算失败";

            }
            catch (Exception ex)
            {
                return "1|存货成本计算失败，原因：" + ex.ToString();
            }

        }
        #endregion

        #region 构造存货成本插入SqlCommond
        /// <summary>
        /// 返货构造好的存货成本插入SQL
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="companyCD"></param>
        /// <param name="currentYearMonth"></param>
        /// <param name="preYearMonth"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        private static SqlCommand[] GetSotrageCostSqlCmd(DataTable dt, string companyCD, string currentYearMonth, string preYearMonth, string startDate, string endDate, int UserID)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                SqlCommand[] arrCmd = new SqlCommand[dt.Rows.Count];
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" INSERT INTO officedba.StorageCost ");
                sbSql.AppendLine("  (CompanyCD,YearMonth,StartDate,EndDate,ProductID,PeriodBeginCost,PeriodBeginCount,PeriodEndCost,PeriodEndCount,ActionUserID,ActionDate) ");
                sbSql.AppendLine(" VALUES ");
                sbSql.AppendLine("  (@CompanyCD,@YearMonth,@StartDate,@EndDate,@ProductID,@PeriodBeginCost,@PeriodBeginCount,@PeriodEndCost,@PeriodEndCount,@ActionUserID,@ActionDate) ");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];

                    SqlParameter[] sqlParams = new SqlParameter[11];
                    int index = 0;
                    sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
                    sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", currentYearMonth);
                    sqlParams[index++] = SqlHelper.GetParameter("@StartDate", startDate);
                    sqlParams[index++] = SqlHelper.GetParameter("@EndDate", endDate);
                    sqlParams[index++] = SqlHelper.GetParameter("@ProductID", row["ProductID"].ToString());
                    sqlParams[index++] = SqlHelper.GetParameter("@PeriodBeginCost", row["PeriodBeginCost"].ToString());
                    sqlParams[index++] = SqlHelper.GetParameter("@PeriodBeginCount", row["PeriodBeginCount"].ToString());
                    sqlParams[index++] = SqlHelper.GetParameter("@PeriodEndCost", row["PeriodEndCost"].ToString());
                    sqlParams[index++] = SqlHelper.GetParameter("@PeriodEndCount", row["PeriodEndCount"].ToString());
                    sqlParams[index++] = SqlHelper.GetParameter("@ActionUserID", UserID);
                    sqlParams[index++] = SqlHelper.GetParameter("@ActionDate", DateTime.Now);

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sbSql.ToString();
                    cmd.Parameters.AddRange(sqlParams);
                    arrCmd[i] = cmd;
                }
                return arrCmd;
            }
            else
                return null;
        }
        #endregion

        #region 判断是否存在指定期数的存货成本
        private static bool ValidateCurrentData(string yearMonth, string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine(" SELECT TOP 1  * FROM officedba.StorageCost  ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND YearMonth=@YearMonth ");

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@YearMonth", yearMonth);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);

            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;




        }
        #endregion

        #region 读取存货成本列表
        public static DataTable GetStorageCostList(Hashtable htParams, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT a.*,b.ProdNo,b.ProductName,b.Specification,c.CodeName AS UnitName,d.TypeName AS ColorName  ");
            sbSql.AppendLine(" ,(ISNULL(a.PeriodBeginCost,0)*ISNULL(a.PeriodBeginCount,0) ) AS LastTotalPrice ");//期初金额
            sbSql.AppendLine(",(ISNULL(a.PeriodEndCost,0)*ISNULL(a.PeriodEndCount,0) ) AS CurrentTotalPrice ");//期末金额
            sbSql.AppendLine(" FROM officedba.StorageCost AS a");
            sbSql.AppendLine(" LEFT JOIN officedba.ProductInfo AS b ON a.ProductID=b.ID ");
            sbSql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS c ON b.UnitID=c.ID ");
            sbSql.AppendLine(" LEFT JOIN officedba.CodePublicType AS d ON d.ID=b.ColorID ");
            sbSql.AppendLine(" WHERE a.CompanyCD=@CompanyCD ");

            SqlParameter[] sqlParams = new SqlParameter[htParams.Count];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", htParams["CompanyCD"].ToString());

            #region  构造查询条件
            if (htParams.ContainsKey("ProductID"))
            {
                sbSql.AppendLine(" AND a.ProductID=@ProductID ");
                sqlParams[index++] = SqlHelper.GetParameter("@ProductID", htParams["ProductID"].ToString());
            }
            if (htParams.ContainsKey("StartYearMonth"))
            {
                sbSql.AppendLine("AND YearMonth>=@StartYearMonth ");
                sqlParams[index++] = SqlHelper.GetParameter("@StartYearMonth", htParams["StartYearMonth"].ToString());
            }
            if (htParams.ContainsKey("EndYearMonth"))
            {
                sbSql.AppendLine("AND YearMonth<=@EndYearMonth ");
                sqlParams[index++] = SqlHelper.GetParameter("@EndYearMonth", htParams["EndYearMonth"].ToString());
            }

            #endregion


            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, sqlParams, ref TotalCount);

        }
        #endregion

        #region 修改指定的期末成本
        /// <summary>
        /// 修改制定的期末成本
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool EditPeriodEndCost(decimal cost, int ID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" UPDATE officedba.StorageCost SET PeriodEndCost=@PeriodEndCost ");
            sbSql.AppendLine(" WHERE ID=@ID ");

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@PeriodEndCost", cost);
            sqlParams[index++] = SqlHelper.GetParameter("@ID", ID);


            if (SqlHelper.ExecuteTransSql(sbSql.ToString(),sqlParams) > 0)
                return true;
            else
                return false;
        }
        #endregion

    }
}
