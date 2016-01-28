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

using XBase.Business.Office.DefManager;
using XBase.Common;
using XBase.Model.Office.DefManager;

/// <summary>
/// 业务表初始化界面
/// </summary>
public partial class Pages_Office_DefManager_BusTableInit : BasePage
{
    /// <summary>
    /// 业务表别名
    /// </summary>
    public string AliasTableName
    {
        get
        {
            return Request["AliasTableName1"];
        }
    }


    /// <summary>
    /// 界面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        UserInfoUtil userInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        this.hidCompany.Value = userInfo.CompanyCD;
        if (!this.IsPostBack)
        {// 第一次加载
            this.hidID.Value = Request["tableID"];
            this.hidSearchCondition.Value = Request.QueryString.ToString();
            DataTable dt = DefFormBus.GetDictionary(userInfo.CompanyCD);
            ListItem li = null;
            selDDLBinddic.Items.Add("");
            foreach (DataRow dr in dt.Rows)
            {
                li = new ListItem();
                selDDLBinddic.Items.Add(li);
                li.Text = dr["AliasTableName"].ToString();
                li.Attributes.Add("title", dr["CustomTableName"].ToString());
                li.Value = dr["ID"].ToString();
            }
        }
    }
}
