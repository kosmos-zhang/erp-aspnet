using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
namespace XBase.Common
{
   public class CommonYearSelectControl
    {
        /// <summary>
        /// 初始化年份
        /// </summary>
       public void InintDDL(DropDownList CommonYearSelectControl)
        {
            int StartYear = 2007;
            int Year = System.DateTime.Now.Year;
            int EndYear = Year + 10 + Year - StartYear;

            int j = 0;

            for (int i = StartYear; i < EndYear; i++)
            {
                ListItem NoYearItem = new ListItem(i.ToString(), i.ToString());
                CommonYearSelectControl.Items.Insert(j, NoYearItem);
                if (Year == i) CommonYearSelectControl.SelectedIndex = j;
                j++;
            }
        }


       /// <summary>
       /// 获得某年某月最后一天的数值
       /// </summary>
       /// <param name="Month">String类型</param>
       /// <param name="Year">String类型</param>
       /// <returns></returns>
       public static string GetMonthLastDay(string month, string year)
       {
           string day = "";
           try
           {
               if (month == "01" || month == "03" || month == "05" || month == "07" || month == "08" || month == "10" || month == "12")
               {
                   day = "31";
               }
               else if (month == "04" || month == "06" || month == "09" || month == "11")
               {
                   day = "30";
               }
               else
               {
                   if (DateTime.IsLeapYear(Convert.ToInt32(year)))
                   {
                       day = "29";
                   }
                   else
                   {
                       day = "28";
                   }
               }
               return day;
           }
           catch (Exception e)
           {
               day = e.Message ;
               return day;
           }
   



       }

       /// <summary>
       /// 过滤datatable，进行搜索
       /// </summary>
       /// <param name="dt"></param>
       /// <param name="condition"></param>
       /// <returns></returns>
       public  static DataTable GetNewDataTable(DataTable dt, string condition)
       {
           DataTable newdt = new DataTable();
           newdt = dt.Clone();
           DataRow[] dr = dt.Select(condition);
           for (int i = 0; i < dr.Length; i++)
           {
               newdt.ImportRow((DataRow)dr[i]);
           }
           return newdt;//返回的查询结果
       }


       /// <summary>
       /// 过滤datatable，进行搜索,并加以排序等操作 Example:GetNewDataTable(dtNew, "", "operateDate desc");
       /// </summary>
       /// <param name="dt"></param>
       /// <param name="condition"></param>
       /// <returns></returns>
       public static DataTable GetNewDataTable(DataTable dt, string condition, string sort)
       {
           DataTable newdt = new DataTable();
           newdt = dt.Clone();
           DataRow[] dr = dt.Select(condition, sort);
           for (int i = 0; i < dr.Length; i++)
           {
               newdt.ImportRow((DataRow)dr[i]);
           }
           return newdt;//返回的查询结果
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="dt"></param>
       public  static string  FillTableList(DataTable dt,string dataTableColumnsName)
       {
           try
           {
               string CustIdStr = "";
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   CustIdStr += "," + dt.Rows[i][dataTableColumnsName].ToString();
               }
               return CustIdStr.TrimStart(',');
           }
           catch
           {
               return "";
           }
       }
    }
}
