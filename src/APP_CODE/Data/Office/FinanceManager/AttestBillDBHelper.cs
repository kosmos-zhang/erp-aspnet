/**********************************************
 * 类作用：   凭证主表数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/08
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;

namespace XBase.Data.Office.FinanceManager
{
    public class AttestBillDBHelper
    {
        /// <summary>
        ///保存数据信息至凭证主表
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool InsertIntoAttestBill(AttestBillModel Model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Insert into Officedba.CurrencyTypeSetting ( CompanyCD,CurrencyName, ");
            sql.AppendLine("CurrencySymbol,isMaster,ExchangeRate,ConvertWay,ChangeTime,UsedStatus )");
            sql.AppendLine("Values(@CompanyCD,@CurrencyName,@CurrencySymbol,");
            sql.AppendLine("@isMaster,@ExchangeRate,@ConvertWay,@ChangeTime,@UsedStatus )");
            SqlParameter[] parms = new SqlParameter[8];
            //parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
            //parms[1] = SqlHelper.GetParameter("@CurrencyName", Model.CurrencyName);
            //parms[2] = SqlHelper.GetParameter("@CurrencySymbol", Model.CurrencySymbol);
            //parms[3] = SqlHelper.GetParameter("@isMaster", Model.isMaster);
            //parms[4] = SqlHelper.GetParameter("@ExchangeRate", Model.ExchangeRate);
            //parms[5] = SqlHelper.GetParameter("@ConvertWay", Model.ConvertWay);
            //parms[6] = SqlHelper.GetParameter("@ChangeTime", Model.ChangeTime);
            //parms[7] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
            SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        ///// <summary>
        ///// 更新凭证主表信息
        ///// </summary>
        ///// <param name="Model"></param>
        ///// <returns></returns>
        //public static bool UpdateAttestBill(AttestBillModel Model)
        //{

        //}
    }
}
