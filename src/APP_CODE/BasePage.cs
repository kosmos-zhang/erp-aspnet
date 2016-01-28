/**********************************************
 * 类作用：   页面基类
 * 建立人：   吴志强
 * 建立时间： 2008/12/30
 ***********************************************/

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
using XBase.Business.Common;
using XBase.Common;


public class BasePage : System.Web.UI.Page
{
    

    protected void Page_PreLoad(object sender, EventArgs e)
    {       

        //获得用户页面控制权限
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        //XBase.Common.CRCer.GetValidateCode(this.Page);


        //获得工程路径
        String currentDomainPath = System.AppDomain.CurrentDomain.BaseDirectory;
        //Session时间过期
        if (UserInfo == null)
        {
            Response.Redirect("~/Pages/SystemErrorPage/TimeOutPage.aspx");
            return;
        }
        //获得ModuleID
        string moduleID = (string)Request.QueryString["ModuleID"];
        //ModuleID为空时，默认为不对页面进行权限控制
        if (string.IsNullOrEmpty(moduleID))
        {
            moduleID = (string)Session["curpage_ModuleID"];
            if (string.IsNullOrEmpty(moduleID))
                return;
        }
        else {
            Session["curpage_ModuleID"] = moduleID;
        }

        //ModuleID类型判断，如果不为数字，则输出Error
        if (!ValidateUtil.IsInt(moduleID))
        {
            // ModuleID不为数字时，为错误ID，页面跳转去没有权限的页面
            //Response.Redirect("~/Pages/SystemErrorPage/NoAuthorityPage.aspx");
            return;
        }
        //获得页面控制权限
        string[] AuthInfo = SafeUtil.GetPageAuthority(moduleID, UserInfo);
        //有权限操作页面
        if (AuthInfo != null && AuthInfo.Length > 0)
        {
            //可操作的控件显示
            for (int i = 0; i < AuthInfo.Length; i++)
            {
                try
                {
                    //设置可见
                    this.FindControl(AuthInfo[i].Trim()).Visible = true;
                }
                catch (NullReferenceException ex) //页面没有此控件时
                {
                    //TODO
                    continue;
                }
            }
        }
        //没有权限操作页面，页面跳转
        else
        {
           // Response.Redirect("~/Pages/SystemErrorPage/NoAuthorityPage.aspx");
        }
    }
}
