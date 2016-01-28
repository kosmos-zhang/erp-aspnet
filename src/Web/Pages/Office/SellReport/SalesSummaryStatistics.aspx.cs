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

public partial class Pages_Office_SellReport_SalesSummaryStatistics : BasePage
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Value = DateTime.Now.ToString("yyyy-MM-01");
            txtEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
