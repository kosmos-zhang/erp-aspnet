using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.Office.SupplyChain;
using XBase.Common;
using XBase.Model.Office.SupplyChain;
public partial class Pages_Office_SupplyChain_BusiLogicSet : BasePage
{
    public int TypeID
    {
        set { ViewState["ID"] = value; }
        get { return Convert.ToInt32(ViewState["ID"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPageinfo();
        }
        this.Save_SubjAssistant.Attributes.Add("onclick", "return Check();");
    }

    #region LoadInfo
    private void LoadPageinfo()
    {
        DataTable dt = BusiLogicSetBus.GetBusiLogicSet("");
        if (dt != null && dt.Rows.Count > 0)
        {
            BusiLogicSetList.DataSource = dt;
            BusiLogicSetList.DataBind();
        }
    }
    #endregion
    #region 判断是否选择
    private bool Check()
    {
        bool result = false;
        if (BusiLogicSetList.Items.Count > 0)
        {
            for (int i = 0; i < BusiLogicSetList.Items.Count; i++)
            {
                CheckBox check = BusiLogicSetList.Items[i].FindControl("CheckStatus") as CheckBox;
                if (check.Checked)
                {
                    result = true;
                    break;
                }
            }
        }
        return result;
    }
    #endregion

  

    #region 保存
    protected void Save_SubjAssistant_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            if (!Check())
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('请选择项目！');</script>");
            }
            else
            {
                if (BusiLogicSetList.Items.Count > 0)
                {
                    for (int i = 0; i < BusiLogicSetList.Items.Count; i++)
                    {
                        CheckBox check = BusiLogicSetList.Items[i].FindControl("CheckStatus") as CheckBox;
                        if (check.Checked)
                        {
                            string Id = BusiLogicSetList.DataKeys[i].ToString();
                            DropDownList DrpList = BusiLogicSetList.Items[i].FindControl("DrpLogicSet") as DropDownList;
                            string UsedStatus = "";
                            if (DrpList != null)
                            {
                                UsedStatus = DrpList.SelectedValue;
                            }
                            if (Convert.ToInt32(Id) > 0)
                            {
                                bool result = BusiLogicSetBus.UpdateBusiLogic(Convert.ToInt32(Id), UsedStatus);
                                if (result)
                                {
                                    this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('修改成功');</script>");
                                    LoadPageinfo();
                                }
                            }
                        }
                    }
                }
            }
         
        }
        catch
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('修改失败，请联系管理员');</script>");
        }


    }
    #endregion
    protected void BusiLogicSetList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
        e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label UsedStatus = e.Item.FindControl("Lab_Status") as Label;
            DropDownList ListUsedStatus = e.Item.FindControl("DrpLogicSet") as DropDownList;
            string te = UsedStatus.Text;
            if (UsedStatus.Text =="否")
            {
                if (ListUsedStatus != null)
                {
                    ListUsedStatus.SelectedValue = "0";
                }
            }
            if (UsedStatus.Text == "是")
            {
                if (ListUsedStatus != null)
                {
                    ListUsedStatus.SelectedValue = "1";
                }
            }
        }
    }
}
