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
using XBase.Business.Office.CustManager;
using XBase.Business.Common;
using XBase.Common;
using XBase.Model.Office.CustManager;

public partial class Pages_Office_CustManager_LinkMan_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindLinkManLinkType();
            btnImport.Attributes["onclick"] = "return IfExp();";
            hiddUserId.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前登陆用户
        }
    }

    #region 绑定联系人类型
    private void BindLinkManLinkType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_LINK_LINKTYPE);
        if (dt.Rows.Count > 0)
        {
            ddlLinkType.DataTextField = "TypeName";
            ddlLinkType.DataValueField = "ID";
            ddlLinkType.DataSource = dt;
            ddlLinkType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlLinkType.Items.Insert(0, Item);
    }
    #endregion

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string orderString = hiddExpOrder.Value.Trim();//排序
            string order = "asc";//排序：降序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "linkmanname";//要排序的字段，如果为空，默认为"ID"
           
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string ord = " ORDER BY " + orderBy + " " + order;

            LinkManModel LinkManM = new LinkManModel();
            string CustID = hiddCustID.Value;//客户ID
            LinkManM.LinkManName = txtLinkManName.Value.Trim();
            LinkManM.Handset = txtHandset.Value.Trim();
            LinkManM.Important = seleImportant.Value;
            LinkManM.LinkType = ddlLinkType.SelectedItem.Value == "0" ? 0 : Convert.ToInt32(ddlLinkType.SelectedItem.Value);
            string DateBegin = txtDateBegin.Value.Trim();
            string DateEnd = txtDateEnd.Value.Trim();
            LinkManM.WorkTel = txtWorkTel.Value.Trim();

            LinkManM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            LinkManM.CanViewUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID

            DataTable dt = LinkManBus.ExportLinkManInfo(CustID, LinkManM, DateBegin, DateEnd, ord);

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "联系人姓名", "对应客户", "称谓", "联系人类型", "重要程度", "工作电话", "手机", "QQ", "生日"},
                new string[] { "linkmanname", "CustName", "Appellation", "TypeName", "Important", "WorkTel", "Handset", "QQ", "Birthday" },
                "联系人列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}
