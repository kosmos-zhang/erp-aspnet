/**********************************************
 * 类作用：  成本核算数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/09/9
 ***********************************************/
using System;
using XBase.Model.Office.ProductionManager;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections;
namespace XBase.Data.Office.ProductionManager
{
    public class CostProductionDBHelper
    {
        #region 删除成本核算信息
        public static bool DelCostProductionInfo(int ID)
        {
            string[] arrayList = new string[2];
            string sql = "delete from officedba.CostProduction where ID=" + ID.ToString();
            string sqldetail = "delete from officedba.CostDetails where CTID=" + ID.ToString();

            arrayList[0] = sql;
            arrayList[1] = sqldetail;

            return SqlHelper.ExecuteTransForListWithSQL(arrayList);
        }
        #endregion

        #region 添加成本核算信息
        public static int InsertCostProductionInfo(ProcutionCostModel model, ArrayList Detailsmodels)
        {
            SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
            conn.Open();
            SqlTransaction mytran = conn.BeginTransaction();
            try
            {
                StringBuilder cmdsql = new StringBuilder();
                cmdsql.AppendLine("Insert into officedba.CostProduction");
                cmdsql.AppendLine("(CompanyCD,PeriodNum,ProductID,AccountMethod,");
                cmdsql.AppendLine("CompRatePro,IsInvestment,FinishedProductCount,");
                cmdsql.AppendLine("InProuctCount,MaterialsUnit,WageUnit,OverheadUnit,");
                cmdsql.AppendLine("BurningPowerUnit,CurrentMonthMaterials,CurrentMonthHours,");
                cmdsql.AppendLine("EndMonthHours,EndMonthMaterials,CurrencyType,CurrencyRate,Remark)");
                cmdsql.AppendLine("values(@CompanyCD,@PeriodNum,@ProductID,@AccountMethod,");
                cmdsql.AppendLine("@CompRatePro,@IsInvestment,@FinishedProductCount,");
                cmdsql.AppendLine("@InProuctCount,@MaterialsUnit,@WageUnit,@OverheadUnit,");
                cmdsql.AppendLine("@BurningPowerUnit,@CurrentMonthMaterials,@CurrentMonthHours,");
                cmdsql.AppendLine("@EndMonthHours,@EndMonthMaterials,@CurrencyType,@CurrencyRate,@Remark)");
                cmdsql.AppendLine("set @IntID= @@IDENTITY");
                SqlParameter[] parms = {
                                     new SqlParameter("@CompanyCD",SqlDbType.VarChar,8),
                                     new SqlParameter("@PeriodNum",SqlDbType.VarChar,8),
                                     new SqlParameter("@ProductID",SqlDbType.Int),
                                     new SqlParameter("@AccountMethod",SqlDbType.Char),
                                     new SqlParameter("@CompRatePro",SqlDbType.Decimal),
                                     new SqlParameter("@IsInvestment",SqlDbType.Char),
                                     new SqlParameter("@FinishedProductCount",SqlDbType.Decimal),
                                     new SqlParameter("@InProuctCount",SqlDbType.Decimal),
                                     new SqlParameter("@MaterialsUnit",SqlDbType.Decimal),
                                     new SqlParameter("@WageUnit",SqlDbType.Decimal),
                                     new SqlParameter("@OverheadUnit",SqlDbType.Decimal),
                                     new SqlParameter("@BurningPowerUnit",SqlDbType.Decimal),
                                     new SqlParameter("@CurrentMonthMaterials",SqlDbType.Decimal),
                                     new SqlParameter("@CurrentMonthHours",SqlDbType.Decimal),
                                     new SqlParameter("@EndMonthHours",SqlDbType.Decimal),
                                     new SqlParameter("@EndMonthMaterials",SqlDbType.Decimal),
                                     new SqlParameter("@CurrencyType",SqlDbType.Int),
                                     new SqlParameter("@CurrencyRate",SqlDbType.Decimal),
                                     new SqlParameter("@Remark",SqlDbType.VarChar,200),
                                     new SqlParameter("@IntID",SqlDbType.Int)
                                 };
                parms[0].Value = model.CompanyCD;
                parms[1].Value = model.PeriodNum;
                parms[2].Value = model.ProductID;
                parms[3].Value = model.AccountMethod;
                parms[4].Value = model.CompRatePro;
                parms[5].Value = model.IsInvestment;
                parms[6].Value = model.FinishedProductCount;
                parms[7].Value = model.InProuctCount;
                parms[8].Value = model.MaterialsUnit;
                parms[9].Value = model.WageUnit;
                parms[10].Value = model.OverheadUnit;
                parms[11].Value = model.BurningPowerUnit;
                parms[12].Value = model.CurrentMonthMaterials;
                parms[13].Value = model.CurrentMonthHours;
                parms[14].Value = model.EndMonthHours;
                parms[15].Value = model.EndMonthMaterials;
                parms[16].Value = model.CurrencyType;
                parms[17].Value = model.CurrencyRate;
                parms[18].Value = model.Remark;
                parms[19] = SqlHelper.GetOutputParameter("@IntID", SqlDbType.Int);
                int rev = SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, cmdsql.ToString(), parms);
                int ID = Convert.ToInt32(parms[19].Value);
                if (ID > 0)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("Insert into officedba.CostDetails");
                    sql.AppendLine("(CTID,ItemName,Materials,Wage,");
                    sql.AppendLine("Overhead,BurningPower,TotalCost)");
                    sql.AppendLine("values(@CTID,@ItemName,@Materials,@Wage,");
                    sql.AppendLine("@Overhead,@BurningPower,@TotalCost)");

                    SqlParameter[] paras = {                                             
                                             new SqlParameter("@CTID",SqlDbType.Int),
                                             new SqlParameter("@ItemName",SqlDbType.VarChar,50),
                                             new SqlParameter("@Materials",SqlDbType.Decimal),
                                             new SqlParameter("@Wage",SqlDbType.Decimal),
                                             new SqlParameter("@Overhead",SqlDbType.Decimal),
                                             new SqlParameter("@BurningPower",SqlDbType.Decimal),
                                             new SqlParameter("@TotalCost",SqlDbType.Decimal)
                                         };

                    foreach (CostDetailsModel mdl in Detailsmodels)
                    {
                        paras[0].Value = ID;
                        paras[1].Value = mdl.ItemName;
                        paras[2].Value = mdl.Materials;
                        paras[3].Value = mdl.Wage;
                        paras[4].Value = mdl.Overhead;
                        paras[5].Value = mdl.BurningPower;
                        paras[6].Value = mdl.TotalCost;

                        SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, sql.ToString(), paras);
                    }


                }
                mytran.Commit();
                return ID;
            }
            catch (Exception ex)
            {
                mytran.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        #endregion


        #region 获取成本核算信息
        public static DataSet GetCostProductionInfoByProductID(int PID)
        {
            SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
            conn.Open();


            string cmdsql = "SELECT TOP 1 a.*,b.ProductName,c.CurrencyName FROM officedba.CostProduction AS a LEFT JOIN officedba.ProductInfo AS b ON a.ProductID=b.ID";
            cmdsql += " LEFT JOIN  officedba.CurrencyTypeSetting AS c ON a.CurrencyType=c.ID";
            cmdsql += " WHERE a.ProductID=@ID ORDER BY a.ID DESC";
            SqlParameter[] parms = { new SqlParameter("@ID", SqlDbType.Int, 4) };
            parms[0].Value = PID;
            DataTable mainDatatable = SqlHelper.ExecuteSql(cmdsql, parms);

            DataSet ds = new DataSet();

            if (mainDatatable.Rows.Count == 0)
            {
                ds.Tables.Add(mainDatatable);
                return ds;
            }

            string sqldetail = "select * from officedba.CostDetails where CTID=@CTID";
            SqlParameter[] parms2 = { new SqlParameter("@CTID", SqlDbType.Int) };
            parms2[0].Value = int.Parse(mainDatatable.Rows[0]["ID"].ToString());
            DataTable detailDatatable = SqlHelper.ExecuteSql(sqldetail, parms2);



            ds.Tables.Add(mainDatatable);
            ds.Tables.Add(detailDatatable);



            conn.Close();
            conn.Dispose();


            return ds;

        }

        public static DataSet GetCostProductionInfo(int CPID)
        {
            SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
            conn.Open();


            string cmdsql = "SELECT a.*,b.ProductName,c.CurrencyName FROM officedba.CostProduction AS a LEFT JOIN officedba.ProductInfo AS b ON a.ProductID=b.ID";
            cmdsql += " LEFT JOIN  officedba.CurrencyTypeSetting AS c ON a.CurrencyType=c.ID";
            cmdsql += " WHERE a.ID=@ID";
            SqlParameter[] parms = { new SqlParameter("@ID", SqlDbType.Int, 4) };
            parms[0].Value = CPID;
            DataTable mainDatatable = SqlHelper.ExecuteSql(cmdsql, parms);

            string sqldetail = "select * from officedba.CostDetails where CTID=@CTID";
            SqlParameter[] parms2 = { new SqlParameter("@CTID", SqlDbType.Int) };
            parms2[0].Value = CPID;
            DataTable detailDatatable = SqlHelper.ExecuteSql(sqldetail, parms2);

            DataSet ds = new DataSet();

            ds.Tables.Add(mainDatatable);
            ds.Tables.Add(detailDatatable);



            conn.Close();
            conn.Dispose();


            return ds;

        }
        #endregion


        public static DataTable LoadList(string where, string orderby, int pageIndex, int pageSize, out int totalCnt)
        {
            string sqlStr = "SELECT a.*,b.ProductName,b.ProdNo FROM officedba.CostProduction  AS a LEFT JOIN officedba.ProductInfo AS b ON a.ProductID=b.ID";
            if (where.Trim() + "" != "")
            {
                sqlStr += " where" + where;
            }


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sqlStr;
            cmd.CommandType = CommandType.Text;

            int _totalCnt = 0;
            DataTable dt = SqlHelper.PagerWithCommand(cmd, pageIndex, pageSize, orderby, ref _totalCnt);
            totalCnt = _totalCnt;

            return dt;

        }

        public static DataTable ExportToExcel(string where, string orderby)
        {
            string sqlStr = "SELECT a.ID,a.ProductID,a.AccountMethod,a.CompRatePro,a.IsInvestment,a.FinishedProductCount,a.InProuctCount,a.MaterialsUnit,a.WageUnit,a.OverheadUnit,a.BurningPowerUnit,a.CurrentMonthMaterials,a.CurrentMonthHours,a.EndMonthHours,a.EndMonthMaterials,a.CurrencyType,a.CurrencyRate,a.Remark, " +
                " '   '+a.PeriodNum  AS  PeriodNum  ,b.ProductName,b.ProdNo FROM officedba.CostProduction  AS a LEFT JOIN officedba.ProductInfo AS b ON a.ProductID=b.ID";
            if (where.Trim() + "" != "")
            {
                sqlStr += " where" + where;
            }
            sqlStr += " " + orderby;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sqlStr;
            cmd.CommandType = CommandType.Text;
            DataTable dt = SqlHelper.ExecuteSearch(cmd);
            return dt;
        }


    }
}
