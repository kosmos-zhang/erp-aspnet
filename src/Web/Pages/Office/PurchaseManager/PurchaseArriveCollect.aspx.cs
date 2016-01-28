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

public partial class Pages_Office_PurchaseManager_PurchaseArriveCollect : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartConfirmDate.Text = DateTime.Now.ToString("yyyy-MM-01");
            txtEndConfirmDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); 
        }
    }
}
