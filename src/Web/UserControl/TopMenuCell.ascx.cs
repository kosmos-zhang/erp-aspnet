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

public partial class UserControl_TopMenuCell : System.Web.UI.UserControl
{
    private string _flashPath;
    public string FlashPath
    {
        set { _flashPath = value; }
        get { return _flashPath; }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}
