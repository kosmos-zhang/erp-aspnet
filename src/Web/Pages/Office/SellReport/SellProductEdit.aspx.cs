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
using XBase.Business.Office.SellReport;
using System.Text;

public partial class Pages_Office_SellReport_SellProductEdit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                DataTable dt = UserProjectInfoBus.GetDataDetailByID(Request.QueryString["id"].ToString(),userInfo);
                productNo.Text = dt.Rows[0]["productNum"].ToString();
                productname.Text = dt.Rows[0]["productName"].ToString();
                price.Text = dt.Rows[0]["price"].ToString();
                brief.Text = dt.Rows[0]["bref"].ToString();
                memo.Text = dt.Rows[0]["memo"].ToString();
                HidID.Value = dt.Rows[0]["ID"].ToString();
            }
        }
    }
}
