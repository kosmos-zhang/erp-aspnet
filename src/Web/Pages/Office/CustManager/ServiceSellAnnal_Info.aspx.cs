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

public partial class Pages_Office_CustManager_ServiceSellAnnal_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            hiddUserId.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            //多计量单位
            if (UserInfo.IsMoreUnit)
            {
                BaseUnit.Visible = true;
                isMoreUnitID.Value = "1";
            }
            else
            {
                BaseUnit.Visible = false;
                isMoreUnitID.Value = "0";
            }
        }
    }
}
