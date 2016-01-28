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

public partial class UserControl_StorageSellBack : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        {
            hidMoreUnit_C.Value = "true";
            th_UnitName.Attributes.Add("style", "display:block;");
            th_JeBenCount.Attributes.Add("style", "display:block;");
            th_UnitPrice.Attributes.Add("style", "display:block;");
        }
        else
        {
            hidMoreUnit_C.Value = "false";
            th_UnitName.Attributes.Add("style", "display:none;");
            th_JeBenCount.Attributes.Add("style", "display:none;");
            th_UnitPrice.Attributes.Add("style", "display:none;");
        }
    }
}
