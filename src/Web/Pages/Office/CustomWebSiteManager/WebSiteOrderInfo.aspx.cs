using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

public partial class Pages_Office_CustomWebSiteManager_WebSiteOrderInfo : BasePage
{

    private bool _IsMul = false;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["OrderNo"] != null)
            {
                string OrderNo = Request.QueryString["OrderNo"].ToString();
                GetMainOrderInfo(OrderNo);
               
            }
        }

    }

    /* 读取单据主要信息 */
    protected void GetMainOrderInfo(string OrderNo)
    {
        DataTable dtInfo = XBase.Business.CustomAPI.CustomWebSite.WebSiteOrderBus.GetOrderInfo(OrderNo, UserInfo.CompanyCD);

        if (dtInfo != null && dtInfo.Rows.Count > 0)
        {
            DataRow row = dtInfo.Rows[0];
            divOrderNo.InnerHtml = row["OrderNo"].ToString();
            divStatus.InnerHtml = row["StatusName"].ToString();
            divOrderTitle.InnerHtml = row["Title"].ToString();
            divCustomName.InnerHtml = row["CustName"].ToString();
            divLoginUserName.InnerHtml = row["LoginUserName"].ToString();
            divConDate.InnerHtml = Convert.ToDateTime(row["ConsignmentDate"].ToString()).ToString("yyyy-MM-dd");

        }
        GetDetailOrderInfo(OrderNo);
    }


    protected void GetDetailOrderInfo(string OrderNo)
    {
        DataTable dtInfo = XBase.Business.CustomAPI.CustomWebSite.WebSiteOrderBus.GetDetailInfo(OrderNo, UserInfo.CompanyCD);

        StringBuilder sbHtml = new StringBuilder();


       
        sbHtml.AppendLine("<table width=\"99%\" border=\"0\" id=\"tblDetail\" style=\"behavior: url(../../../draggrid.htc)\"");
        sbHtml.AppendLine("align=\"center\" cellpadding=\"2\" cellspacing=\"1\" bgcolor=\"#999999\">");
        sbHtml.AppendLine("<tbody>");
        /*构建列头*/
        sbHtml.AppendLine(GetColName());
        /* 构建明细 */
        if (dtInfo != null && dtInfo.Rows.Count > 0)
        {
            int index = 0;
            foreach (DataRow row in dtInfo.Rows)
            {
                if (index % 2 != 0)
                {
                    sbHtml.AppendLine("<tr style=\"background-color:#E7E7E7\" onmouseover=\"c=this.style.backgroundColor;this.style.backgroundColor='#cfc';\" onmouseout=\"this.style.backgroundColor=c\">");
                }
                else
                {
                    sbHtml.AppendLine("<tr style=\"background-color:#FFFFFF\" onmouseover=\"c=this.style.backgroundColor;this.style.backgroundColor='#cfc';\" onmouseout=\"this.style.backgroundColor=c\">");
                }

                if (_IsMul)
                {
                    sbHtml.AppendLine("<td>"+row["ProdNo"].ToString()+"</td>");
                    sbHtml.AppendLine("<td>"+row["ProductName"].ToString()+"</td>");
                    sbHtml.AppendLine("<td>"+row["BaseUnitName"].ToString()+"</td>");
                    sbHtml.AppendLine("<td>"+row["BaseCount"].ToString()+"</td>");
                    sbHtml.AppendLine("<td>"+row["UsedUnitName"].ToString()+"</td>");
                    sbHtml.AppendLine("<td>"+row["UsedCount"].ToString()+"</td>");
                    sbHtml.AppendLine("<td>"+row["UsedPrice"].ToString()+"</td>");
                    sbHtml.AppendLine("<td>"+row["TotalPrice"].ToString()+"</td>");
                }
                else
                {
                    sbHtml.AppendLine("<td>" + row["ProdNo"].ToString() + "</td>");
                    sbHtml.AppendLine("<td>" + row["ProductName"].ToString() + "</td>");
                    sbHtml.AppendLine("<td>" + row["BaseUnitName"].ToString() + "</td>");
                    sbHtml.AppendLine("<td>" + row["BaseCount"].ToString() + "</td>");
                    sbHtml.AppendLine("<td>" + row["BasePrice"].ToString() + "</td>");
                    sbHtml.AppendLine("<td>" + row["TotalPrice"].ToString() + "</td>");
                }
                sbHtml.AppendLine("</tr>");
                index++;
            }
        }

        sbHtml.AppendLine("</tbody></table>");

        divlendList.InnerHtml = sbHtml.ToString();
    }


    protected string GetColName()
    {
        //判断是否启用多单位
         _IsMul = XBase.Business.CustomAPI.CustomWebSite.WebSiteUnitControlBus.IsMulUnit(UserInfo.CompanyCD);
        if (_IsMul)
        {
            return "<tr><td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">商品编号</td>" +
                       "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">商品名称</td>" +
                       "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">基本单位</td>" +
                       "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">基本数量</td>" +
                        "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">单位</td>" +
                        "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">数量</td>" +
                        "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">单价</td>" +
                        "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">金额</td>" +
                        "</tr>";
        }
        else
        {
            return "<tr><td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">商品编号</td>" +
                  "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">商品名称</td>" +
                   "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">单位</td>" +
                   "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">数量</td>" +
                   "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">单价</td>" +
                   "<td align=\"center\" bgcolor=\"#E6E6E6\" class=\"Blue\" style=\"width: 4%\">金额</td>" +
                   "</tr>";
        }
    }


}
