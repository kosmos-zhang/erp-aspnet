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
public partial class Pages_Office_SystemManager_SurePsd : System.Web.UI.Page
{
    private string oldpsd = "";
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        DataTable dt = UserInfoBus.GetUserInfoByID(userid, companyCD);
        if (dt.Rows.Count > 0)
        {
            oldpsd = dt.Rows[0]["password"].ToString();
        }
        string OldPassword = StringUtil.EncryptPasswordWitdhMD5(this.txt_oldpsd.Text.Trim());
        if (oldpsd == OldPassword)
        {
            Response.Write("<script charset=\"GB2312\" language='javascript' type='text/javascript'>window.returnValue ='" + oldpsd + "';window.close();</script>");
        }
        else 
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>alert('管理员密码不正确！')</script>");
        }
    }
}
