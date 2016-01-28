using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.ProjectBudget;
using XBase.Common;

namespace XBase.Business.Office.ProjectBudget
{
   public class ProjectTotalBus
    {
       /// <summary>
       /// 项目核算汇总表--不按往来单位核算
       /// </summary>
       /// <param name="ProjectID">项目ID</param>
        /// <param name="PurchaseType">采购数据来源：1采购订单2采购到货通知单3采购订单+无来源采购到货通知单</param>
       /// <returns></returns>
       public static DataTable GetProjectTotal(int ProjectID, string StartDate, string EndDate, string PurchaseType)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
           DataTable dt = new DataTable();
           dt.Columns.Add("Purchase");
           dt.Columns.Add("Pay");
           dt.Columns.Add("ShoudPay");
           dt.Columns.Add("Product");
           dt.Columns.Add("Fees");
           dt.Columns.Add("StorageInOther");
           dt.Columns.Add("StorageOutOther");
           dt.Columns.Add("Sell");
           dt.Columns.Add("InCome");
           dt.Columns.Add("ShoudInCome");

           DataRow row = dt.NewRow();
           switch (int.Parse(PurchaseType))
           {
               case 1:
                   row["Purchase"] = ProjectTotalDBHelper.GetSumTypeAmount("'1','2'", ProjectID, StartDate, EndDate, CompanyCD);
                   break;
               case 2:
                   row["Purchase"] = ProjectTotalDBHelper.GetSumTypeAmount("'13','2'", ProjectID, StartDate, EndDate, CompanyCD);
                   break;
               case 3:
                   row["Purchase"] = ProjectTotalDBHelper.GetSumTypeAmount("'1','2','14'", ProjectID, StartDate, EndDate, CompanyCD);
                   break;
               default :
                   row["Purchase"] = ProjectTotalDBHelper.GetSumTypeAmount("'1','2'", ProjectID, StartDate, EndDate, CompanyCD);
                   break;
           }
           
           row["Pay"] = ProjectTotalDBHelper.GetSumTypeAmount("'3'", ProjectID, StartDate, EndDate, CompanyCD);
           row["ShoudPay"] = Convert.ToDecimal(row["Purchase"].ToString()) - Convert.ToDecimal(row["Pay"].ToString());
           row["Product"] = ProjectTotalDBHelper.GetSumTypeAmount("'4','5'", ProjectID, StartDate, EndDate, CompanyCD);
           row["Fees"] = ProjectTotalDBHelper.GetSumTypeAmount("'6','15'", ProjectID, StartDate, EndDate, CompanyCD);
           row["StorageInOther"] = ProjectTotalDBHelper.GetSumTypeAmount("'8'", ProjectID, StartDate, EndDate, CompanyCD);
           row["StorageOutOther"] = ProjectTotalDBHelper.GetSumTypeAmount("'9'", ProjectID, StartDate, EndDate, CompanyCD);
           row["Sell"] = ProjectTotalDBHelper.GetSumTypeAmount("'10','11'", ProjectID, StartDate, EndDate, CompanyCD);
           row["InCome"] = ProjectTotalDBHelper.GetSumTypeAmount("'12'", ProjectID, StartDate, EndDate, CompanyCD);
           row["ShoudInCome"] = Convert.ToDecimal(row["Sell"].ToString()) - Convert.ToDecimal(row["InCome"].ToString());

           dt.Rows.Add(row);
           return dt;
       }

       /// <summary>
       /// 项目核算汇总表--按往来单位核算
       /// </summary>
       /// <param name="ProjectID"></param>
       /// <param name="StartDate"></param>
       /// <param name="EndDate"></param>
       /// <returns></returns>
       public static DataTable GetProjectTotalByCust(int ProjectID, string StartDate, string EndDate, string PurchaseType)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
           DataTable dt = new DataTable();
           dt.Columns.Add("CustName");
           dt.Columns.Add("CustType");
           dt.Columns.Add("AmountType");
           dt.Columns.Add("Amount");
           dt.Columns.Add("AccountAmount");
           dt.Columns.Add("ShoundPay");
           dt.Columns.Add("ShoundIncome");

           DataTable custdt = ProjectTotalDBHelper.GetDistinctCustInfo("", ProjectID, StartDate, EndDate, CompanyCD);
           for (int i = 0; i < custdt.Rows.Count; i++)
           {
               #region 销售收入，收款单及应收款
               DataRow row = dt.NewRow();
               row["CustName"] = custdt.Rows[i]["CustName"].ToString();
               row["CustType"] = CustType(custdt.Rows[i]["CustType"].ToString());
               row["AmountType"] = "收入";
               row["Amount"] = ProjectTotalDBHelper.GetSumTypeAmount("'10','11'", ProjectID, StartDate, EndDate, CompanyCD, custdt.Rows[i]["CustID"].ToString(), custdt.Rows[i]["CustType"].ToString());
               row["AccountAmount"] = ProjectTotalDBHelper.GetSumTypeAmount("'12'", ProjectID, StartDate, EndDate, CompanyCD, custdt.Rows[i]["CustID"].ToString(), custdt.Rows[i]["CustType"].ToString());
               row["ShoundPay"] = "";
               row["ShoundIncome"] = Convert.ToDecimal(row["Amount"].ToString()) - Convert.ToDecimal(row["AccountAmount"].ToString());
               dt.Rows.Add(row);
               #endregion

               #region 采购支出，付款单及应付款
               DataRow row1 = dt.NewRow();
               row1["CustName"] = custdt.Rows[i]["CustName"].ToString();
               row1["CustType"] = CustType(custdt.Rows[i]["CustType"].ToString());
               row1["AmountType"] = "支出";

               switch (int.Parse(PurchaseType))
               {
                   case 1:
                       row1["Amount"] = ProjectTotalDBHelper.GetSumTypeAmount("'1','2'", ProjectID, StartDate, EndDate, CompanyCD, custdt.Rows[i]["CustID"].ToString(), custdt.Rows[i]["CustType"].ToString());
                       break;
                   case 2:
                       row1["Amount"] = ProjectTotalDBHelper.GetSumTypeAmount("'13','2'", ProjectID, StartDate, EndDate, CompanyCD, custdt.Rows[i]["CustID"].ToString(), custdt.Rows[i]["CustType"].ToString());
                       break;
                   case 3:
                       row1["Amount"] = ProjectTotalDBHelper.GetSumTypeAmount("'1','2','14'", ProjectID, StartDate, EndDate, CompanyCD, custdt.Rows[i]["CustID"].ToString(), custdt.Rows[i]["CustType"].ToString());
                       break;
                   default:
                       row1["Amount"] = ProjectTotalDBHelper.GetSumTypeAmount("'1','2'", ProjectID, StartDate, EndDate, CompanyCD, custdt.Rows[i]["CustID"].ToString(), custdt.Rows[i]["CustType"].ToString());
                       break;
               }

              
               row1["AccountAmount"] = ProjectTotalDBHelper.GetSumTypeAmount("'3'", ProjectID, StartDate, EndDate, CompanyCD, custdt.Rows[i]["CustID"].ToString(), custdt.Rows[i]["CustType"].ToString());
               row1["ShoundPay"] = Convert.ToDecimal(row1["Amount"].ToString()) - Convert.ToDecimal(row1["AccountAmount"].ToString());
               row1["ShoundIncome"] = "";
               dt.Rows.Add(row1);
               #endregion

               #region 费用票据及费用报销单
               DataRow row2 = dt.NewRow();
               row2["CustName"] = custdt.Rows[i]["CustName"].ToString();
               row2["CustType"] = CustType(custdt.Rows[i]["CustType"].ToString());
               row2["AmountType"] = "费用";
               row2["Amount"] = ProjectTotalDBHelper.GetSumTypeAmount("'6','7'", ProjectID, StartDate, EndDate, CompanyCD, custdt.Rows[i]["CustID"].ToString(), custdt.Rows[i]["CustType"].ToString());
               row2["AccountAmount"] = row2["Amount"].ToString();
               row2["ShoundPay"] = "";
               row2["ShoundIncome"] = "";
               dt.Rows.Add(row2);
               #endregion

           }


           DataRow otherOutRow = dt.NewRow();

           otherOutRow["CustName"] = "";
           otherOutRow["CustType"] = "";
           otherOutRow["AmountType"] = "其他支出";
           otherOutRow["Amount"] = ProjectTotalDBHelper.GetSumTypeAmount("'4','5','9'", ProjectID, StartDate, EndDate, CompanyCD);
           otherOutRow["AccountAmount"] = otherOutRow["Amount"].ToString();
           otherOutRow["ShoundPay"] = "";
           otherOutRow["ShoundIncome"] = "";
           dt.Rows.Add(otherOutRow);


           DataRow otherInRow = dt.NewRow();

           otherInRow["CustName"] = "";
           otherInRow["CustType"] = "";
           otherInRow["AmountType"] = "其他收入";
           otherInRow["Amount"] = ProjectTotalDBHelper.GetSumTypeAmount("'8'", ProjectID, StartDate, EndDate, CompanyCD);
           otherInRow["AccountAmount"] = otherInRow["Amount"].ToString();
           otherInRow["ShoundPay"] = "";
           otherInRow["ShoundIncome"] = "";
           dt.Rows.Add(otherInRow);


           return dt;
       }


       public static string CustType(string custtype)
       {
           string rev = string.Empty;
           switch(custtype)
           {
               case "1": rev = "供应商";
                   break;
               case "2": rev = "客户";
                   break;
               case "3": rev = "职员";
                   break;

           }

           return rev;
       }
    }
}
