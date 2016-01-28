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

public partial class Pages_Office_PurchaseManager_ProviderContactHistoryInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hidModuleID.Value = ConstUtil.MODULE_ID_PROVIDERCONTRACTHISTORY_ADD;
        }
    }

    #region 导出Excel
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        
        string CustID = this.txtCustID.Value;
        string Linker = this.HidLinker.Value;
        string StartLinkDate = this.txtStartLinkDate.Text;
        string EndLinkDate = this.txtEndLinkDate.Text;
        
        int TotalCount = 0;
        DataTable dt = ProviderContactHistoryBus.SelectProviderContactHistory(1, 1000000, "ID", ref TotalCount, CustID, Linker, StartLinkDate, EndLinkDate);



        //DataTable dt = WorkCenterBus.GetWorkCenterListBycondition(model, 1, 1000000, "ID desc", ref totalCount);

        //导出标题
        string headerTitle = "联络单编号|供应商名称|供应商类型|联络人|联络时间|被联络人";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "ContactNo|CustName|CustTypeName|LinkerName|LinkDate|LinkManName";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "供应商联络列表");
    }
    #endregion

}
