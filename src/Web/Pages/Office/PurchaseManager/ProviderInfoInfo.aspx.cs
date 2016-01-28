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

public partial class Pages_Office_PurchaseManager_ProviderInfoInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //新建修改采购供应商档案模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_PROVIDERINFO_ADD;
            BinddrpCustType();//绑定供应商类别
            BinddrpAreaID();//绑定采购供应商所在区域
            BinddrpCreditGrade();//绑定采购供应商优质级别

            BindTreeChildNodes();//供应商分类

        }
    }

    #region 导出Excel
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string CustNo = this.txtCustNo.Text;
        string CustName = this.txtCustName.Text;
        string CustNam = this.txtCustNam.Text;
        string PYShort = this.txtPYShort.Text;
        string CustType = this.drpCustType.Value;
        if (CustType == "0")
        {
            CustType = "";
        }
        string CustClass = this.txtCustClass.Value;
        string AreaID = this.drpAreaID.Value;
        if (AreaID == "0")
        {
            AreaID = "";
        }
        string CreditGrade = this.drpCreditGrade.Value;
        if (CreditGrade == "0")
        {
            CreditGrade = "";
        }
        string Manager = this.HidManager.Value;
        string StartCreateDate = this.txtStartCreateDate.Text;
        string EndCreateDate = this.txtEndCreateDate.Text;



        int TotalCount = 0;
        DataTable dt = ProviderInfoBus.SelectProviderInfo(1, 1000000, "ID", ref TotalCount, CustNo, CustName, CustNam, PYShort, CustType, CustClass, AreaID, CreditGrade, Manager, StartCreateDate, EndCreateDate);



        //DataTable dt = WorkCenterBus.GetWorkCenterListBycondition(model, 1, 1000000, "ID desc", ref totalCount);

        //导出标题
        string headerTitle = "供应商编号|供应商名称|供应商简称|供应商拼音代码|供应商类别|供应商分类|所在区域|分管采购员|优质级别|建挡人|建挡日期";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "CustNo|CustName|CustNam|PYShort|CustTypeName|CustClassName|AreaName|ManagerName|CreditGradeName|CreatorName|CreateDate";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "供应商档案列表");
    }
    #endregion

    #region 绑定列表采购供应商类别
    private void BinddrpCustType()
    {
        DataTable dt = ProviderInfoBus.GetdrpCustType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpCustType.DataSource = dt;
            drpCustType.DataTextField = "TypeName";
            drpCustType.DataValueField = "ID";
            drpCustType.DataBind();
            ListItem Item = new ListItem("--请选择--", "");
            drpCustType.Items.Insert(0, Item);
        }
    }
    #endregion

    #region 绑定采购供应商所在区域
    private void BinddrpAreaID()
    {
        DataTable dt = ProviderInfoBus.GetdrpAreaID();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpAreaID.DataSource = dt;
            drpAreaID.DataTextField = "TypeName";
            drpAreaID.DataValueField = "ID";
            drpAreaID.DataBind();
            ListItem Item = new ListItem("--请选择--", "");
            drpAreaID.Items.Insert(0, Item);
        }
    }
    #endregion

    #region 绑定采购供应商优质级别
    private void BinddrpCreditGrade()
    {
        DataTable dt = ProviderInfoBus.GetdrpCreditGrade();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpCreditGrade.DataSource = dt;
            drpCreditGrade.DataTextField = "TypeName";
            drpCreditGrade.DataValueField = "ID";
            drpCreditGrade.DataBind();
            ListItem Item = new ListItem("--请选择--", "");
            drpCreditGrade.Items.Insert(0, Item);
        }
    }
    #endregion

    #region 绑定供应商分类
    private void BindTreeChildNodes()
    {
        DataTable dt_product = ProviderInfoBus.GetProviderClass();
        foreach (DataRow row in dt_product.Select("SupperID=0"))
        {
            TreeNode nodes = new TreeNode();
            nodes.Text = row["CodeName"].ToString();
            nodes.Value = row["ID"].ToString();
            nodes.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}');", nodes.Text, nodes.Value);

            LoadSubData(row["ID"].ToString(), nodes, dt_product);
            //node.ChildNodes.Add(nodes);
            this.TreeView1.Nodes.Add(nodes);
            nodes.Expanded = true;
        }
    }
    private void LoadSubData(string pid, TreeNode nodes, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=" + pid))
        {
            TreeNode nodess = new TreeNode();
            nodess.Text = row["CodeName"].ToString();
            nodess.Value = row["ID"].ToString();
            nodess.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}');", nodess.Text, nodess.Value);
            LoadSubData(row["ID"].ToString(), nodess, dt);
            nodes.ChildNodes.Add(nodess);
            nodes.Expanded = false;
        }
    }
    #endregion
}
