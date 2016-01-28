using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Office_DefManager_CreateBusTable:BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["pageIndex"] != null && Request.QueryString["pageCount"] != null)
        {
            Hidpagestate.Value = "&pageIndex=" + Request.QueryString["pageIndex"].ToString() + "&pageCount=" + Request.QueryString["pageCount"].ToString();
        }
        else
        {
            Hidpagestate.Value = "&pageIndex=1&pageCount=10";
        }

        #region 邦定从属表
        dllParentID.DataSource = XBase.Business.Office.DefManager.DefFormBus.GetParentDT(UserInfo.CompanyCD);
        dllParentID.DataTextField = "AliasTableName";
        dllParentID.DataValueField = "ID";
        dllParentID.DataBind();
        dllParentID.Items.Insert(0, new ListItem("请选择表", "0"));
        this.hidSearchCondition.Value = Request.QueryString.ToString();
        #endregion

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            hidID.Value = Request.QueryString["ID"];
            for (int i = 0; i < dllParentID.Items.Count;i++)
            {
                if (dllParentID.Items[i].Value == Request.QueryString["ID"])
                {
                    dllParentID.Items.RemoveAt(i);
                }
               
            }
            
        }
    }
}
