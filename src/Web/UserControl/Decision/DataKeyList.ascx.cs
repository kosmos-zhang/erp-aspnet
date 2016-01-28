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
using System.Collections.Generic;

public partial class UserControl_Decision_DataKeyList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
         //strCompanyCD = UserInfo.CompanyCD;
         //LoadDataKeyList();
    }

    /// <summary>
    /// 页面数据绑定
    /// </summary>
    private void LoadDataKeyList()
    {
        ////XBase.Business.Decision.DataKeyPrepare list = new XBase.Business.Decision.DataKeyPrepare();
        ////DataSet ds = list.GetDataKeyPrepareAll();
        //XBase.Business.Decision.DataMySubscribe bll = new XBase.Business.Decision.DataMySubscribe();
        // string strwhere = "CompanyCD='"+strCompanyCD +"'and ID in (select Max(ID) from infodba.DataMySubscribe group by DataID)";
        // Repeater1.DataSource=bll.GetDataMySubscribeListbyCond(strwhere, "ID");
        // Repeater1.DataBind();

    }

   
}
