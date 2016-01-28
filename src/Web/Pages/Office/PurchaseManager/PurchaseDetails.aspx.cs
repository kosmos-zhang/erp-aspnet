using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Office_PurchaseManager_PurchaseDetails : BasePage  
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
