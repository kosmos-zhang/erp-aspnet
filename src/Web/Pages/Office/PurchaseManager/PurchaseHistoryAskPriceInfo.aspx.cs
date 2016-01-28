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
using XBase.Common;
using XBase.Business.Office.PurchaseManager;

public partial class Pages_Office_PurchaseManager_PurchaseHistoryAskPriceInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartPurchaseDate.Value = DateTime.Now.ToString("yyyy-MM-01");
            txtEndPurchaseDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string ProductID = HidProductID.Value;
            string StartPurchaseDate = txtStartPurchaseDate.Value.Trim();
            string EndPurchaseDate = txtEndPurchaseDate.Value.Trim();
            string orderBy = "ProductID desc";
            DataTable dt = PurchaseOrderBus.SelectPurchaseHistoryAskPricePrint(orderBy, ProductID, StartPurchaseDate, EndPurchaseDate);
            //OutputToExecl.ExportToTableFormat(this, dt,
            //    new string[] { "物品编号", "物品名称", "规格", "单位", "最高采购价", "最低采购价", "平均采购价", "最近采购价" },
            //    new string[] { "ProductNo", "ProductName", "Specification", "UnitName", "LargeTaxPrice", "SmallTaxPrice", "AverageTaxPrice", "NewTaxPrice" },
            //    "采购价格分析");
            string[,] ht =   { 
                           { "物品编号", "ProductNo" },
                           { "物品名称", "ProductName" },
                           { "规格", "Specification" },
                           { "单位", "UnitName" },
                           { "最高采购价", "LargeTaxPrice" },
                           { "最低采购价", "SmallTaxPrice" },
                           { "平均采购价", "AverageTaxPrice" },
                           { "最近采购价", "NewTaxPrice" }
                         };

            ExportExcel(dt, ht, "", "采购价格统计");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}
