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

using Microsoft.Win32;
using System.Xml;

public partial class Login : System.Web.UI.Page
{

    protected string CustomPath
    {
        get;
        set;
    }

    protected string CustomSysName
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            /*验证版本 
             * 原版GUID:025AA3C8-FE57-45B5-A995-A61C92C8AB74
             * 执行力GUID:88CB3C6D-2CC5-4680-8BEE-F7DD01A7D1B7
             * 当该配置节不存在时，直接抛出异常，不做任何操作。
             */
            if (System.Configuration.ConfigurationManager.AppSettings["VerNameGuid"] == null)
            {
                divDefault.InnerHtml = "<br /></br><br/><center><span style=\"font-size:18px;color:red;\"> 对不起，版本GUID配置节不存在，请联系客服。</span></center>";
                divVerConert.Visible = false;
               // divZxl100.InnerHtml = "";
                Page.Title = "错误信息";
                
                return;
            }
            else 
            {
                switch (System.Configuration.ConfigurationManager.AppSettings["VerNameGuid"].ToString())
                {
                    case XBase.Common.ConstUtil.Ver_ERP_Guid://生产版
                   //     divZxl100.InnerHtml = "";
                        Page.Title = "进销存系统";
                        CustomPath = "Login";
                        CustomSysName = "进销存系统，更多产品描述和帮助信息请参考产品服务网站";
                        break;
                    default://未匹配到
                        divDefault.InnerHtml = "<br /></br><br/><center><span style=\"font-size:18px;color:red;\"> 对不起，版本GUID配置节不存在，请联系客服。</span></center>";
                        divVerConert.Visible = false;

                     //   divZxl100.InnerHtml = "";
                        Page.Title = "错误信息";
                        break;
                }
                    
            }




            //判断是否是退出
            try
            {
                if (Request.QueryString["flag"].ToString() == "1")
                {
                    XBase.Common.SessionUtil.Session.Clear();
                    XBase.Common.UserSessionManager.Remove(Session.SessionID);
                }
            }
            catch
            { }
        }

        //验证License
        if (SetLicenseDisabled() && !XBase.Common.LicenseValidator.Check())
        {
            // license 验证 失败
            //throw new Exception("license验证失败,请联系客服！");
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("license验证失败,请联系客服！");
            HttpContext.Current.Response.End();
            return;
        }
        if (!this.IsPostBack)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (Cache["login_SysNotice"] == null)
            {               
                string fpath = Server.MapPath("App_Data/SysNotice.xml");
                if( !System.IO.File.Exists(fpath))
                {
                 //   panelNotice.Visible = false;
                    if (panelNotice != null)
                        panelNotice.Visible = false;

                    //if (panelNoticeZXL != null)
                    //    panelNoticeZXL.Visible = false;
                    return;
                }
                xmlDoc.Load(fpath);
                Cache.Insert("login_SysNotice", xmlDoc, new System.Web.Caching.CacheDependency(fpath));
            }
            else {
                xmlDoc = (XmlDocument)Cache["login_SysNotice"];
            }

            string pubDate = xmlDoc.SelectSingleNode("/notice/pubdate").InnerText;
            string overDate = xmlDoc.SelectSingleNode("/notice/overdate").InnerText;

            if (DateTime.Parse(overDate) <= DateTime.Now)
            {
                return;
            }
            
            this.lblSysNoticeContent.Text = xmlDoc.SelectSingleNode("/notice/content").InnerText;
           // lblSysNoticePubDate.Text = "&nbsp;&nbsp;" + pubDate;

        }
    }
    private bool SetLicenseDisabled()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["SetLicenseDisabledForSaaS"] == null)
            return true;
        else
            return false;
    }
}
