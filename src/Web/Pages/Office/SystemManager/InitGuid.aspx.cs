using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using XBase.Common;
using XBase.Business.Office.SystemManager;

public partial class Pages_Office_SystemManager_InitGuid : BasePage
{
    protected DataTable db = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        if (!IsPostBack)
        {
            db = RoleInfoBus.GetRoleModuleByUser(userInfo.CompanyCD, userInfo.UserID);
        }
    }


    #region 获取各菜单的链接地址URL
    /// <summary>
    /// 获取各菜单的链接地址URL
    /// </summary>
    /// <param name="moduleID">ModuleID</param>
    /// <param name="url">URL地址</param>
    /// <returns></returns>
    protected string GetLinks(int moduleID, string url)
    {
        string currLinks = "#this";
        if (db.Rows.Count > 0)
        {
            for (int i = 0; i < db.Rows.Count; i++)
            {
                if (moduleID == int.Parse(db.Rows[i]["ModuleID"].ToString()))
                {
                    currLinks = url + moduleID.ToString();
                }
            }
        }
        return currLinks;
    }
    #endregion
}
