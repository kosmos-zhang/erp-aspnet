<%@ WebHandler Language="C#" Class="PurchaseCollectQuery" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Business.Common;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Data;

public class PurchaseCollectQuery : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string Action = context.Request.Form["Action"];
            switch (Action)
            {
                case "Select":
                    GetPurCll(context);
                    break;
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public void GetPurCll(HttpContext context)
    {
        string orderString = (context.Request.Form["orderBy"]);//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "Product";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        } 
        string CompanyCD = string.Empty;
        try
        {
            CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        }
        catch
        {
     
        }    
        string orderName=orderBy ;
        string orderNum=order ;
        orderBy = orderBy+" "+order;
   
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        string Field = context.Request.Form["Field"];//统计字段
        string ProviderID = context.Request.Form["ProviderID"];
        string ProviderName = context.Request.Form["ProviderName"];
        string ProductID = context.Request.Form["ProductID"];
        string StartDate = context.Request.Form["StartDate"];
        string EndDate = context.Request.Form["EndDate"];
        int totalCount = 0;
        
        string[] ProviderNameS = ProviderName.Split(',');
        DataTable dtp = PurchaseOrderBus.GetPurCll(CompanyCD,Field, ProviderID,ProviderName, ProductID, StartDate, EndDate, pageIndex, pageCount, orderBy, ref totalCount);
        DataTable dt = GetNewDataTable(dtp, "", orderBy);
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<table  id='dt_Rate' border=\"1\" cellpadding=\"0\" cellspacing=\"0\"  width=\"100%\">");
        //sb.Append("<tr style='display:none'><td id='totalRecord' value='" + dt.Rows.Count + "'></td></tr>");
        if (orderName == "Product")
        {
            if (orderNum.Equals("asc"))
            {
                sb.Append("<tr><td style=\"width:150;font-size:14;font-weight:bolder;\" align=\"center\" background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"><div class=\"orderClick\" onclick=\"Order('Product','oop');return false;\">物品\\供应商<span id=\"oop\" class=\"orderTip\">↑</span></div></td>");
            }
            else
            {
                sb.Append("<tr><td style=\"width:150;font-size:14;font-weight:bolder;\" align=\"center\" background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"><div class=\"orderClick\" onclick=\"Order('Product','oop');return false;\">物品\\供应商<span id=\"oop\" class=\"orderTip\">↓</span></div></td>");
            }
        }
        else
        {
            sb.Append("<tr><td style=\"width:150;font-size:14;font-weight:bolder;\" align=\"center\" background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"><div class=\"orderClick\" onclick=\"Order('Product','oop');return false;\">物品\\供应商<span id=\"oop\" class=\"orderTip\"> </span></div></td>");
        }
        for (int i = 0; i < ProviderNameS.Length; ++i)
        {
               //<div class=\"orderClick\" onclick=\"OrderBy('TypeName','oC0');return false;\">
               //                                 类型名称<span id=\"oC0\" class=\"orderTip\"></span>
               //                             </div>
            if (orderName == ProviderNameS[i])
            {
                if (orderNum.Equals("asc"))
                {
                    sb.Append("<td  align=\"center\" style=\"font-size:14;font-weight:bolder;\" background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"> <div class=\"orderClick\" onclick=\"Order('" + ProviderNameS[i] + "','oC" + i + "');return false;\">" + ProviderNameS[i] + "<span id=\"oC" + i + "\" class=\"orderTip\">↑</span> </div></td>");
                }
                else
                {
                    sb.Append("<td  align=\"center\" style=\"font-size:14;font-weight:bolder;\" background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"> <div class=\"orderClick\" onclick=\"Order('" + ProviderNameS[i] + "','oC" + i + "');return false;\">" + ProviderNameS[i] + "<span id=\"oC" + i + "\" class=\"orderTip\">↓</span> </div></td>");
                }
            }
            else
            {
                sb.Append("<td  align=\"center\" style=\"font-size:14;font-weight:bolder;\" background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"> <div class=\"orderClick\" onclick=\"Order('" + ProviderNameS[i] + "','oC" + i + "');return false;\">" + ProviderNameS[i] + "<span id=\"oC" + i + "\" class=\"orderTip\"></span> </div></td>");
            }
        }


        sb.Append("<td  align=\"center\" style=\"font-size:14;font-weight:bolder;\" background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\">  合计</td>");
        
        
        
        
        sb.Append("</tr>");
        if (dt != null && dt.Rows.Count > 0)
        {

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                
                
                sb.Append("<tr >");
                sb.Append("<td style=\"width:150;font-size:14;font-weight:bolder;\"  align=\"center\" background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\">&nbsp;" + dt.Rows[j]["Product"].ToString() + "</td>");
                decimal  total = 0;
                for (int i = 0; i < ProviderNameS.Length; ++i)
                {
                    sb.Append("<td  align=\"center\" >&nbsp;" + dt.Rows[j][ProviderNameS[i]].ToString () + "</td>");
                    total = total + Convert .ToDecimal ( dt.Rows[j][ProviderNameS[i]] == null ? "0" : dt.Rows[j][ProviderNameS[i]].ToString());
                }

                sb.Append("<td  align=\"center\" >&nbsp;" +total .ToString ()+ "</td>");
                sb.Append("</tr>");
                
                
                
            }
        }
                    sb.Append("<tr >"); 
        sb.Append("<td style=\"width:150;font-size:14;font-weight:bolder;\"  align=\"center\" background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\">&nbsp;合计</td>");
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 1; i < dt.Columns .Count; ++i)
                {
                   decimal total = 0;
                   for (int j = 0; j < dt.Rows.Count ; j++)
                   {
                   
                       total = total + Convert.ToDecimal(dt.Rows[j][i] == null ? "0" : dt.Rows[j][i].ToString());
                   }
                sb.Append("<td  align=\"center\" >&nbsp;" + total.ToString() + "</td>");
            }
        }

        sb.Append("<td  align=\"center\" >&nbsp; </td>");
                sb.Append("</tr>");
        sb.Append("</table>");
        //sb.Append("{");
        //sb.Append("totalCount:");
        //sb.Append(totalCount.ToString());
        //sb.Append(",data:");
        //if (dt.Rows.Count == 0)
        //{
        //    sb.Append("\"\"");
        //}
        //else
        //{
        //    sb.Append(JsonClass.DataTable2Json(dt));
        //}
        //sb.Append("}");
        //context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    private DataTable GetNewDataTable(DataTable dt, string condition,string Sort)
    {
        
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition, Sort);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }
}
