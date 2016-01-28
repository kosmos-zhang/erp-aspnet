/**********************************************
 * 类作用：   勾兑操作步骤明细表数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/07/02
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using XBase.Common;
using System.Collections;

namespace XBase.Data.Office.FinanceManager
{
    public class StepsDetailsDBHelper
    {
        #region 添加勾兑明细操作记录
        /// <summary>
        /// 添加勾兑明细操作记录
        /// </summary>
        /// <param name="MyList">ArrayList</param>
        /// <returns></returns>
        public static bool InSertStepsDetails(ArrayList MyList)
        {
            try
            {
                int rev = 0;
                if (MyList.Count > 0)
                {
                    for (int i = 0; i < MyList.Count; i++)
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into officedba.StepsDetails(");
                        strSql.Append("CompanyCD,PayOrInComeType,SourceID,BlendingID,BlendingAmount)");
                        strSql.Append(" values (");
                        strSql.Append("@CompanyCD" + i + ",@PayOrInComeType" + i + ",@SourceID" + i + ",@BlendingID" + i + ",@BlendingAmount" + i + ")");
                        SqlParameter[] parameters = {
					        new SqlParameter("@CompanyCD"+i+"", (MyList[i] as StepsDetailsModel).CompanyCD),
					        new SqlParameter("@PayOrInComeType"+i+"",(MyList[i] as StepsDetailsModel).PayOrInComeType),
					        new SqlParameter("@SourceID"+i+"", (MyList[i] as StepsDetailsModel).SourceID),
					        new SqlParameter("@BlendingID"+i+"", (MyList[i] as StepsDetailsModel).BlendingID),
					        new SqlParameter("@BlendingAmount"+i+"", (MyList[i] as StepsDetailsModel).BlendingAmount),
                                        };
                        SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
                        rev += SqlHelper.Result.OprateCount;
                    }
                }
                return rev > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改勾兑明细操作记录
        /// <summary>
        /// 修改勾兑明细操作记录
        /// </summary>
        /// <param name="MyList">ArrayList</param>
        /// <returns></returns>
        public static bool UpdateStepsDetails(ArrayList MyList)
        {
            try
            {
                int rev = 0;
                if (MyList.Count > 0)
                {
                    for (int i = 0; i < MyList.Count; i++)
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update officedba.StepsDetails set ");
                        strSql.Append("BlendingAmount=@BlendingAmount" + i + "");
                        strSql.Append(" where ID=@ID" + i + "");
                        SqlParameter[] parameters = {
					        new SqlParameter("@BlendingAmount" + i + "",(MyList[i] as StepsDetailsModel).BlendingAmount),
                            new SqlParameter("@ID" + i + "", (MyList[i] as StepsDetailsModel).ID),
                         };
                        SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
                        rev += SqlHelper.Result.OprateCount;
                    }
                }
                return rev > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 是否存在记录
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="payOrIncometype">收付款类别</param>
        /// <param name="SourceID">收付款单ID</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static bool IsExits(string payOrIncometype, string SourceID,string CompanyCD,out string IDS)
        {
            try
            {
                int nev = 0;
                string nevSql = "select ID from officedba.StepsDetails where PayOrInComeType=@PayOrInComeType and CompanyCD=@CompanyCD and SourceID=@SourceID ";
                SqlParameter[] parmss = 
                {
                     new SqlParameter("@PayOrInComeType",payOrIncometype),
                     new SqlParameter("@CompanyCD",CompanyCD),
                     new SqlParameter("@SourceID",SourceID),

                };
                DataTable DT = SqlHelper.ExecuteSql(nevSql, parmss);
                nev = DT.Rows.Count;
                string IDStr = string.Empty;
                foreach (DataRow dr in DT.Rows)
                {
                    IDStr += dr["ID"].ToString() + ",";
                }
                IDS = IDStr.TrimEnd(new char[] { ',' });
                return nev > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取勾兑操作明细记录
        /// <summary>
        /// 获取勾兑操作明细记录
        /// </summary>
        /// <param name="SourceID">收付款单ID</param>
        /// <param name="BlendingID">勾兑明细ID</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="PayOrIncomeType">收付款类别</param>
        /// <returns></returns>
        public static DataTable GetStepsDetails(int SourceID, int BlendingID, string CompanyCD, string PayOrIncomeType)
        {
            string sql = "select ID,CompanyCD,PayOrInComeType,SourceID,BlendingID,BlendingAmount from officedba.StepsDetails where CompanyCD='" + CompanyCD + "' and  PayOrInComeType='" + PayOrIncomeType + "' {0} ";
            string QuerString=string.Empty;
            if(SourceID.ToString().Trim().Length>0)
            {
                QuerString+=" and SourceID='";
                QuerString+=SourceID.ToString();
                QuerString+="' ";
            }
            if(BlendingID.ToString().Trim().Length>0)
            {
                QuerString+=" and BlendingID='";
                QuerString+=BlendingID.ToString();
                QuerString+="' ";
            }
            string EXCsql = string.Format(sql,QuerString);
            return SqlHelper.ExecuteSql(EXCsql);
        }
        #endregion

        #region 删除勾兑操作明细记录
        /// <summary>
        /// 删除勾兑操作明细记录
        /// </summary>
        /// <param name="SourceID">收付款单ID</param>
        /// <param name="PayOrIncomeType">收付款单类别</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static bool DeleteStepsDetails(int SourceID, string PayOrIncomeType, string CompanyCD)
        {
            try
            {
                string sql = "delete from officedba.StepsDetails where CompanyCD=@CompanyCD and  PayOrInComeType=@PayOrInComeType and SourceID=@SourceID  ";
                SqlParameter[] parmss = 
                {
                     new SqlParameter("@CompanyCD",CompanyCD),
                     new SqlParameter("@PayOrInComeType",PayOrIncomeType),
                     new SqlParameter("@SourceID",SourceID),

                };

                SqlHelper.ExecuteTransSql(sql, parmss);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
