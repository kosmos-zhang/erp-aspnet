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
using System.Xml.Linq;
using XBase.Common;
using XBase.Business.Office.SystemManager;
public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_User.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //this.txt_User.Text = "admin";
            //string companyCD = "AAAAAA";
            DataTable dt = UserInfoBus.GetUserInfoByID(txt_User.Text, companyCD);
            if (dt.Rows.Count > 0)
            {
                hf_psd.Value = dt.Rows[0]["password"].ToString();
                hfcommanycd.Value = dt.Rows[0]["CompanyCD"].ToString();
            }
        }
    }
}
