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

public partial class Pages_Office_HumanManager_PerformanceTaskCheck_Edit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TaskNo"] != null && Request.QueryString["EmployeeId"] != null && Request.QueryString["TemplateNo"] != null)
            {
                hidElemID.Value = Request.QueryString["TaskNo"].ToString();
                hidEmployeId.Value = Request.QueryString["EmployeeId"].ToString();
                hidTemplateNo.Value = Request.QueryString["TemplateNo"].ToString();
                string sign = Request.QueryString["signDate"].ToString();
                if (string.IsNullOrEmpty(sign.Trim ()))
                {
                    hidSign.Value = "1";
                }
                else
                {
                    hidSign.Value = "0";
                }
            }



        }
    }
}
