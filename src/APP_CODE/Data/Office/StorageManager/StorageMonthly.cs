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
using XBase.Data.Common;

namespace XBase.Data.Office.StorageManager
{
    public class StorageMonthly
    {


        /// <summary>
        /// 通过公司编号，为此公司做月结
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool StorageMonthEndForCompany(string CompanyCD)
        {
            //string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            string LoginUserID = "admin";

            ArrayList lstInsert = new ArrayList();//所有的插入sqlcommand命令集
            DateTime nowDate = System.DateTime.Now;
            string MonthNoNow = nowDate.ToString("yyyyMM");
            string monthdays = DateTime.DaysInMonth(nowDate.Year, nowDate.Month).ToString();
            string DateMonth = nowDate.ToString("yyyy-MM");
            string EndDate = DateMonth + "-" + monthdays;//本月的结束日期
            string StartDate = DateMonth + "-" + "01";//本月的开始日期（1号）

            DataTable dtsp = SPInfo(CompanyCD);//查询当前对应CompanyCD在分仓存量表中，ProductID，StorageID确定的数据
            if (dtsp.Rows.Count > 0)
            {
                for (int i = 0; i < dtsp.Rows.Count; i++)
                {
                    decimal CountInitail = 0;//取上个月的期末数量，作为这个月的期初数量
                    decimal TotalInitail = 0;//取上个月的期末总额，作为这个月的期初总额
                    decimal RealCostInitail = 0;////取上个月的期末单位成本，作为这个月的期初成本
                    decimal CountInPurchase = 0;//本月采购入库总量
                    decimal TotalInPurchase = 0;//本月采购入库总额
                    decimal CountInProcess = 0;//本月生产完工入库总量
                    decimal TotalInProcess = 0;//本月生产完工入库总额
                    decimal CountInOther = 0;//本月其他入库总量
                    decimal TotalInOther = 0;//本月其他入库总额
                    decimal CountInRed = 0;//本月红冲入库总量
                    decimal TotalInRed = 0;//本月红冲入库总额
                    StorageProductModel model = new StorageProductModel();
                    model.CompanyCD = CompanyCD;
                    model.ProductID = dtsp.Rows[i]["ProductID"].ToString();
                    model.StorageID = dtsp.Rows[i]["StorageID"].ToString();
                    StorageMonthlyModel SMmodel = new StorageMonthlyModel();
                    SMmodel.CompanyCD = CompanyCD;
                    SMmodel.ProductID = dtsp.Rows[i]["ProductID"].ToString();
                    SMmodel.StorageID = dtsp.Rows[i]["StorageID"].ToString();

                    DataTable dtInitail_first = GetCountFromInitailDetail(model);//当公司第一次做月结的时候，获取公司的期初入库的数据
                    DataTable dtInitail = GetLastMonthInfo(SMmodel);//获取上个月的期末信息
                    DataTable dtInPurchase = GetCountFromInPurchaseDetail(model, MonthNoNow);//采购入库
                    DataTable dtInProcess = GetCountFromInProcessDetail(model, MonthNoNow);//生产完工
                    DataTable dtInOther = GetCountFromInOtherDetail(model, MonthNoNow);//其他入库
                    DataTable dtInRed = GetCountFromInRedDetail(model, MonthNoNow);//红冲入库（减少数据的）
                    if (dtInitail.Rows.Count > 0)
                    {
                        CountInitail = decimal.Parse(dtInitail.Rows[0]["NowCount"].ToString());
                        TotalInitail = decimal.Parse(dtInitail.Rows[0]["NowTotal"].ToString());
                        RealCostInitail = decimal.Parse(dtInitail.Rows[0]["NowRealCost"].ToString());
                    }
                    else if (dtInitail_first.Rows.Count > 0)
                    {
                        CountInitail = decimal.Parse(dtInitail_first.Rows[0]["ProductCount"].ToString());
                        TotalInitail = decimal.Parse(dtInitail_first.Rows[0]["TotalPrice"].ToString());
                        if (CountInitail.ToString() != "0.0000")
                        {
                            RealCostInitail = TotalInitail / CountInitail;
                        }
                    }
                    if (dtInPurchase.Rows.Count > 0)
                    {
                        CountInPurchase = decimal.Parse(dtInPurchase.Rows[0]["ProductCount"].ToString());
                        TotalInPurchase = decimal.Parse(dtInPurchase.Rows[0]["TotalPrice"].ToString());
                    }
                    if (dtInProcess.Rows.Count > 0)
                    {
                        CountInProcess = decimal.Parse(dtInProcess.Rows[0]["ProductCount"].ToString());
                        TotalInProcess = decimal.Parse(dtInProcess.Rows[0]["TotalPrice"].ToString());
                    }
                    if (dtInOther.Rows.Count > 0)
                    {
                        CountInOther = decimal.Parse(dtInOther.Rows[0]["ProductCount"].ToString());
                        TotalInOther = decimal.Parse(dtInOther.Rows[0]["TotalPrice"].ToString());
                    }
                    if (dtInRed.Rows.Count > 0)
                    {
                        CountInRed = decimal.Parse(dtInRed.Rows[0]["ProductCount"].ToString());
                        TotalInRed = decimal.Parse(dtInRed.Rows[0]["TotalPrice"].ToString());
                    }
                    SqlCommand comm = new SqlCommand();
                    StorageMonthlyModel SMmodel_Insert = new StorageMonthlyModel();
                    SMmodel_Insert.CompanyCD = CompanyCD;
                    SMmodel_Insert.MonthNo = MonthNoNow;
                    SMmodel_Insert.StartDate = StartDate;
                    SMmodel_Insert.EndDate = EndDate;
                    SMmodel_Insert.StorageID = model.StorageID;
                    SMmodel_Insert.ProductID = model.ProductID;
                    SMmodel_Insert.OldRealCost = RealCostInitail.ToString();
                    SMmodel_Insert.OldCount = CountInitail.ToString();
                    SMmodel_Insert.OldTotal = TotalInitail.ToString();
                    SMmodel_Insert.NowCount = dtsp.Rows[i]["ProductCount"].ToString();

                    if ((CountInitail + CountInPurchase + CountInProcess + CountInOther - CountInRed).ToString() != "0.0000")
                    {
                        SMmodel_Insert.NowRealCost = ((TotalInitail + TotalInPurchase + TotalInProcess + TotalInOther - TotalInRed) / (CountInitail + CountInPurchase + CountInProcess + CountInOther - CountInRed)).ToString();
                        SMmodel_Insert.NowTotal = (decimal.Parse(SMmodel_Insert.NowCount) * decimal.Parse(SMmodel_Insert.NowRealCost)).ToString();
                    }

                    SMmodel_Insert.ModifiedUserID = LoginUserID;

                    comm = InsertMonthly(SMmodel_Insert);
                    lstInsert.Add(comm);
                }
                return SqlHelper.ExecuteTransWithArrayList(lstInsert);
            }
            else//分仓存量表中没有没有当前公司数据
            {
                return false;
            }
        }

        public static DataTable GetCompanyInfo()
        {
            string MonthFirstDay = System.DateTime.Now.ToString("yyyyMM") + "01";//本月的开始日期（1号）;
            string sqlstr = "SELECT CompanyCD FROM pubdba.companyOpenServ where " + MonthFirstDay + "< cast(CloseDate as int)";
            return SqlHelper.ExecuteSql(sqlstr.ToString());
        }

        #region 返回上个月的期末存量，期末成本，期末金额
        /// <summary>
        /// 返回上个月的期末存量，期末成本，期末金额
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        protected static DataTable GetLastMonthInfo(StorageMonthlyModel model)
        {
            DateTime nowDate = System.DateTime.Now;
            string MonthNoNow = nowDate.ToString("yyyyMM");
            DateTime lastDate = nowDate.AddMonths(-1);
            string lastMonthNo = lastDate.ToString("yyyyMM");

            string sql = "SELECT ID,ISNULL(NowRealCost,0) as NowRealCost,NowCount,NowTotal"
                          + " FROM officedba.StorageMonthly where MonthNo=" + lastMonthNo + " and CompanyCD='" + model.CompanyCD
                          + "' and ProductID=" + model.ProductID + " and StorageID=" + model.StorageID;
            return SqlHelper.ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 当公司第一次做月结的时候，需要从期初入库明细中获取所有数据作为第一个月的期初数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected static DataTable GetCountFromInitailDetail(StorageProductModel model)
        {
            string sql = "select ISNULL(sum(b.ProductCount),0) as ProductCount,ISNULL(sum(b.TotalPrice),0) as TotalPrice "
                        + "from officedba.StorageInitailDetail b "
                        + "inner join officedba.StorageInitail a on a.InNo=b.InNo"
                        + " where b.CompanyCD='" + model.CompanyCD + "' "
                        + " and b.ProductID=" + model.ProductID
                        + " and a.StorageID=" + model.StorageID;
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        /// <summary>
        /// 返回插入月结表的SqlCommand
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected static SqlCommand InsertMonthly(StorageMonthlyModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageMonthly(");
            strSql.Append("CompanyCD,MonthNo,StartDate,EndDate,StorageID,ProductID,OldRealCost,NowRealCost,OldCount,OldTotal,NowCount,NowTotal,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@MonthNo,@StartDate,@EndDate,@StorageID,@ProductID,@OldRealCost,@NowRealCost,@OldCount,@OldTotal,@NowCount,@NowTotal,getdate(),@ModifiedUserID)");
            strSql.Append(";select @@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MonthNo ", model.MonthNo));//编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate ", model.StartDate));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate ", model.EndDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OldRealCost ", model.OldRealCost));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowRealCost ", model.NowRealCost));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OldCount ", model.OldCount));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OldTotal ", model.OldTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowCount ", model.NowCount));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowTotal ", model.NowTotal));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//

            return comm;
        }

        /// <summary>
        /// 查询当前对应CompanyCD在分仓存量表中，ProductID，StorageID确定的数据
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        protected static DataTable SPInfo(string CompanyCD)
        {
            string sql = "select StorageID,ProductID,ISNULL(ProductCount,0) as ProductCount from officedba.StorageProduct where CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 采购到货明细中对应当前的ProductID，StorageID，CompanyCD，MonthNo查找出对应的数量和金额总数。
        /// </summary>
        /// <param name="model"></param>
        /// <param name="MonthNo"></param>
        /// <returns></returns>
        protected static DataTable GetCountFromInPurchaseDetail(StorageProductModel model, string MonthNo)
        {
            string sql = "select ISNULL(sum(b.ProductCount),0) as ProductCount,ISNULL(sum(b.TotalPrice),0) as TotalPrice "
                        + "from officedba.StorageInPurchaseDetail b "
                        + "inner join officedba.StorageInPurchase a on a.InNo=b.InNo"
                        + " where b.CompanyCD='" + model.CompanyCD + "' "
                        + " and b.ProductID=" + model.ProductID
                        + " and b.StorageID=" + model.StorageID
                        + " and SUBSTRING(CONVERT(VARCHAR, a.ConfirmDate, 112), 1, 6) = " + MonthNo;
            return SqlHelper.ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 生产完工明细中对应当前的ProductID，StorageID，CompanyCD，MonthNo查找出对应的数量和金额总数。
        /// </summary>
        /// <param name="model"></param>
        /// <param name="MonthNo"></param>
        /// <returns></returns>
        protected static DataTable GetCountFromInProcessDetail(StorageProductModel model, string MonthNo)
        {
            string sql = "select ISNULL(sum(b.ProductCount),0) as ProductCount,ISNULL(sum(b.TotalPrice),0) as TotalPrice "
                        + "from officedba.StorageInProcessDetail b "
                        + "inner join officedba.StorageInProcess a on a.InNo=b.InNo"
                        + " where b.CompanyCD='" + model.CompanyCD + "' "
                        + " and b.ProductID=" + model.ProductID
                        + " and b.StorageID=" + model.StorageID
                        + " and SUBSTRING(CONVERT(VARCHAR, a.ConfirmDate, 112), 1, 6) = " + MonthNo;
            return SqlHelper.ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 其他入库明细中对应当前的ProductID，StorageID，CompanyCD，MonthNo查找出对应的数量和金额总数。
        /// </summary>
        /// <param name="model"></param>
        /// <param name="MonthNo"></param>
        /// <returns></returns>
        protected static DataTable GetCountFromInOtherDetail(StorageProductModel model, string MonthNo)
        {
            string sql = "select ISNULL(sum(b.ProductCount),0) as ProductCount,ISNULL(sum(b.TotalPrice),0) as TotalPrice "
                        + "from officedba.StorageInOtherDetail b "
                        + "inner join officedba.StorageInOther a on a.InNo=b.InNo"
                        + " where b.CompanyCD='" + model.CompanyCD + "' "
                        + " and b.ProductID=" + model.ProductID
                        + " and b.StorageID=" + model.StorageID
                        + " and SUBSTRING(CONVERT(VARCHAR, a.ConfirmDate, 112), 1, 6) = " + MonthNo;
            return SqlHelper.ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 红冲入库明细中对应当前的ProductID，StorageID，CompanyCD，MonthNo查找出对应的数量和金额总数。
        /// </summary>
        /// <param name="model"></param>
        /// <param name="MonthNo"></param>
        /// <returns></returns>
        protected static DataTable GetCountFromInRedDetail(StorageProductModel model, string MonthNo)
        {
            string sql = "select ISNULL(sum(b.ProductCount),0) as ProductCount,ISNULL(sum(b.TotalPrice),0) as TotalPrice "
                        + "from officedba.StorageInRedDetail b "
                        + "inner join officedba.StorageInRed a on a.InNo=b.InNo"
                        + " where b.CompanyCD='" + model.CompanyCD + "' "
                        + " and b.ProductID=" + model.ProductID
                        + " and b.StorageID=" + model.StorageID
                        + " and SUBSTRING(CONVERT(VARCHAR, a.ConfirmDate, 112), 1, 6) = " + MonthNo;
            return SqlHelper.ExecuteSql(sql.ToString());
        }


        #endregion
    }
}
