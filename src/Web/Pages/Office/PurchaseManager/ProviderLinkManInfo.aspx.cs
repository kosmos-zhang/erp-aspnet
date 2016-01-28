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
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;

public partial class Pages_Office_PurchaseManager_ProviderLinkManInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //新建修改采购供应商联系人模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_PROVIDERLINKMAN_ADD;
            BinddrpLinkType();//绑定采购供应商联系人类型
        }
    }

    #region 导出Excel
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string CustNo = this.txtCustNo.Value;
        string LinkManName = this.txtLinkManName.Text;
        string Handset = this.txtHandset.Text;;
        string Important = this.drpImportant.Value;
        if (Important == "0")
        {
            Important = "";
        }
        string LinkType = this.drpLinkType.Value;
        if (LinkType == "0")
        {
            LinkType = "";
        }
        string StartBirthday = this.txtStartBirthday.Text;
        string EndBirthday = this.txtEndBirthday.Text;
        


        int TotalCount = 0;
        DataTable dt = ProviderLinkManBus.SelectProviderLinkMan(1, 1000000, "ID", ref TotalCount, CustNo, LinkManName, Handset, Important, LinkType, StartBirthday, EndBirthday);



        //DataTable dt = WorkCenterBus.GetWorkCenterListBycondition(model, 1, 1000000, "ID desc", ref totalCount);

        //导出标题
        string headerTitle = "联系人姓名|供应商名称|称谓|联系人类型|重要程度|电话|手机|邮件|MSN|QQ|生日";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "LinkManName|CustName|Appellation|LinkTypeName|Important|WorkTel|Handset|MailAddress|MSN|QQ|Birthday";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "供应商联系人列表");
    }
    #endregion

    #region 绑定采购供应商联系人类型
    private void BinddrpLinkType()
    {
        DataTable dt = ProviderLinkManBus.GetdrpLinkType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpLinkType.DataSource = dt;
            drpLinkType.DataTextField = "TypeName";
            drpLinkType.DataValueField = "ID";
            drpLinkType.DataBind();
            ListItem Item = new ListItem("--请选择--", "");
            drpLinkType.Items.Insert(0, Item);
        }
    }
    #endregion
}
