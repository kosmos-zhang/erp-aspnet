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

public partial class Pages_Office_PurchaseManager_ProviderProductInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //新建修改采购供应商物品推荐模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_PROVIDERPRODUCT_ADD;

        }

    }

    #region 导出Excel
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string CustNo = this.txtCustNo.Value;
        string ProductID = this.HidProductID.Value;
        string Grade = this.drpGrade.Value;
        if (Grade == "0")
        {
            Grade = "";
        }
        string Joiner = this.HidJoiner.Value;
        string StartJoinDate = this.txtStartJoinDate.Text;
        string EndJoinDate = this.txtEndJoinDate.Text;

        int TotalCount = 0;
        DataTable dt = ProviderProductBus.SelectProviderProduct(1, 1000000, "ID", ref TotalCount, CustNo, ProductID, Grade, Joiner, StartJoinDate, EndJoinDate);



        //DataTable dt = WorkCenterBus.GetWorkCenterListBycondition(model, 1, 1000000, "ID desc", ref totalCount);

        //导出标题
        string headerTitle = "供应商名称|物品名称|推荐程度|推荐日期|推荐人";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "CustName|ProductName|GradeName|JoinDate|JoinerName";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "供应商推荐物品列表");
    }
    #endregion
}
