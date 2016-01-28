/**********************************************
 * 类作用：   决策支持分析
 * 建立人：   莫申林
 * 建立时间： 2010/06/01
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
   public class PaybleAccountBus
    {
       public static DataTable GetPaybleAccountInfo(string ProviderID)
       {

           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

           try
           {

               string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
               //获取供应商
               DataTable providerdt = PaybleAccoutDBHelper.GetDistinctProviderByPucharOrder(CompanyCD);

               if (!string.IsNullOrEmpty(ProviderID))
               {
                   providerdt.Clear();
                   DataRow row = providerdt.NewRow();
                   row["ID"] = ProviderID;
                   providerdt.Rows.Add(row);
               }

               DataTable dt = new DataTable();
               dt.Columns.Add("OrderAmount");
               dt.Columns.Add("PayAmount");
               dt.Columns.Add("BackAmount");
               dt.Columns.Add("ProviderName");
               dt.Columns.Add("PaybleAmount");
               dt.Columns.Add("yufu");

               foreach (DataRow rw in providerdt.Rows)
               {
                   DataTable Infodt = PaybleAccoutDBHelper.GetPaybleAccountInfo(CompanyCD, rw["ID"].ToString());

                   if (Infodt != null && Infodt.Rows.Count > 0)
                   {
                       DataRow dtrow = dt.NewRow();
                       dtrow["OrderAmount"] =Math.Round(Convert.ToDecimal(Infodt.Rows[0]["OrderAmount"].ToString()),int.Parse(point));
                       dtrow["PayAmount"] = Math.Round(Convert.ToDecimal(Infodt.Rows[0]["PayAmount"].ToString()),int.Parse(point));
                       dtrow["BackAmount"] = Math.Round(Convert.ToDecimal(Infodt.Rows[0]["BackAmount"].ToString()),int.Parse(point));
                       dtrow["ProviderName"] =Infodt.Rows[0]["ProviderName"].ToString(); 
                       dtrow["PaybleAmount"] =Math.Round(( Convert.ToDecimal(dtrow["OrderAmount"]) - Convert.ToDecimal(dtrow["PayAmount"]) - Convert.ToDecimal(dtrow["BackAmount"])),int.Parse(point));
                       dtrow["yufu"] = Math.Round(Convert.ToDecimal(Infodt.Rows[0]["yufu"].ToString()), int.Parse(point));
                       dt.Rows.Add(dtrow);
                   }
               }

               return dt;




           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
