/**********************************************
 * 类作用：   决策支持分析
 * 建立人：   莫申林
 * 建立时间： 2010/06/04
 ***********************************************/

using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using XBase.Model.Office.SupplyChain;
using XBase.Data.OperatingModel.DecisionData;

namespace XBase.Business.OperatingModel.DecisionData
{
    public class StorageAccountAnalysisBus
    {
        /// <summary>
        /// 库存状况分析--分页查询
        /// </summary>
        /// <param name="QueryStr">查询条件</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public static DataTable GetSotrageAccountAnalysis(string QueryStr, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            try
            {
                return StorageAccountAnalysisDBHelper.GetSotrageAccountAnalysis(QueryStr,pageIndex,pageSize,OrderBy,ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 库存状况分析--导出
        /// </summary>
        /// <param name="QueryStr"></param>
        /// <returns></returns>
        public static DataTable GetSotrageAccountAnalysis(string QueryStr)
        {
            try
            {
                string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
                DataTable dt = StorageAccountAnalysisDBHelper.GetSotrageAccountAnalysis(QueryStr);
                DataTable dtnew = dt.Clone();
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    DataRow row = dtnew.NewRow();
                    row["ProductCount"] = Decimal.Round(Decimal.Parse(dt.Rows[i]["ProductCount"].ToString()), int.Parse(point)).ToString();
                    row["Chaochu"] = Decimal.Round(Decimal.Parse(dt.Rows[i]["Chaochu"].ToString()), int.Parse(point)).ToString();
                    row["Duanque"] = Decimal.Round(Decimal.Parse(dt.Rows[i]["Duanque"].ToString()), int.Parse(point)).ToString();
                    row["MaxStockNum"] = Decimal.Round(Decimal.Parse(dt.Rows[i]["MaxStockNum"].ToString()), int.Parse(point)).ToString();
                    row["MinStockNum"] = Decimal.Round(Decimal.Parse(dt.Rows[i]["MinStockNum"].ToString()), int.Parse(point)).ToString();
                    row["ColorName"]=dt.Rows[i]["ColorName"].ToString();
                    row["UnitName"]=dt.Rows[i]["UnitName"].ToString();
                    row["ProductName"]=dt.Rows[i]["ProductName"].ToString();
                    row["ProductNo"]=dt.Rows[i]["ProductNo"].ToString();
                    row["Specification"]=dt.Rows[i]["Specification"].ToString();

                    dtnew.Rows.Add(row);

                      
                }

                return dtnew;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
