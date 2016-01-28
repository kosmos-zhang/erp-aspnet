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
using XBase.Business.Office.SystemManager;
public partial class Main :System.Web.UI.Page
{

    public string Zxl100Path
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserSessionMinLife = System.Configuration.ConfigurationManager.AppSettings["UserSessionMinLife"];
        inpMessageTipTimerSpan.Value = System.Configuration.ConfigurationManager.AppSettings["DeskTipFrameTimeSpan"] == null ? "300" : System.Configuration.ConfigurationManager.AppSettings["DeskTipFrameTimeSpan"];
        ClientScript.RegisterClientScriptBlock(this.GetType(), "fdsfs", "var UserSessionMinLife=" + UserSessionMinLife,true);


        if (!IsPostBack)
        {
            switch (System.Configuration.ConfigurationManager.AppSettings["VerNameGuid"].ToString())
            {
                case XBase.Common.ConstUtil.Ver_ERP_Guid://生产版
                    Zxl100Path = "Login/";
                    Title = "进销存系统";
                    break;
                default://未匹配到
                    break;
            }


            this.txt_User.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //this.txt_User.Text = "admin";
            //string companyCD = "AAAAAA";
            DataTable dt = UserInfoBus.GetUserInfoByID(txt_User.Text, companyCD);
            if (dt.Rows.Count > 0)
            {
                hf_psd.Value = dt.Rows[0]["password"].ToString();
                hfcommanycd.Value = dt.Rows[0]["CompanyCD"].ToString();

                lblUserInfo.Text = this.txt_User.Text;
            }
        }









         //获得用户页面控制权限
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //btn_czkhlxr


        string[] AuthInfo = XBase.Business.Common.SafeUtil.GetPageAuthorityFromDB("2021202", UserInfo);
             

        //有权限操作页面
        if (AuthInfo != null && AuthInfo.Length > 0)
        {
            //可操作的控件显示
            for (int i = 0; i < AuthInfo.Length; i++)
            {
                if (AuthInfo[i].Trim() == "btn_search")
                {
                    this.btn_czkhlxr.Visible = true;
                    break;
                }
            }
        }
      

    }
}
