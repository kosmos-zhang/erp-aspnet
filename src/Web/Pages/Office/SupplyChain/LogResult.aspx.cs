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

using XBase.Business.Office.SupplyChain;
public partial class Handler_Office_SupplyChain_LogResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_back.Attributes.Add("onclick","window.close()");
        DataSet ds = ProductInfoBus.GetError(Request.QueryString["getid"].ToString());
        this.lbl_result.Text = ds.Tables[0].Rows[0]["ExportError"].ToString();
    }
}
