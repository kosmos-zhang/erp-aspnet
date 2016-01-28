/***********************************************************************
 * 
 * Module Name:Web.Pages.Office.SystemManager.RoleLicense_Operation.cs
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2009-01-12
 * End Date:
 * Description:角色赋权查询操作
 * Version History:
 ***********************************************************************/
using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using XBase.Common;
using XBase.Business.Office.SystemManager;
using XBase.Business.SystemManager;
using XBase.Model.Office.SystemManager;
public partial class Pages_Office_SystemManager_RoleLicense_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindModuleTree();
        }
    }
    #region 授权
    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("RoleFunction_Edit.aspx");
    }
    #endregion
    //private void BindModuleTree()
    //{
    //    //string CompanyID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
    //    string CompanyID = "Itest";
    //    DataTable moduleList = XBase.Business.Common.SafeUtil.GetCompanyModule(CompanyID);
    //    DataRow[] rows = moduleList.Select("ParentID is null");
    //    foreach (DataRow row in rows)
    //    {
    //        TreeNode node = new TreeNode();
    //        node.Value = row["ModuleID"].ToString();
    //        node.Text = row["ModuleName"].ToString();
    //        node.NavigateUrl = "#";
    //        BindSubTree(node, moduleList);
    //        TreeView2.Nodes.Add(node);
    //        TreeView2.ShowCheckBoxes = TreeNodeTypes.All;
    //        node.Expanded = false;
    //        TreeView2.Attributes.Add("onclick", "OnTreeNodeMChecked()");
    //    }
    //}

    //private void BindSubTree(TreeNode nodes, DataTable dt)
    //{
    //    DataRow[] rows = dt.Select("ParentID='" + nodes.Value + "'");
    //    TreeNode node = null;

    //    foreach (DataRow row in rows)
    //    {
    //        node = new TreeNode();
    //        node.Value = row["ModuleID"].ToString();
    //        node.Text = row["ModuleName"].ToString();
    //        node.NavigateUrl = "#";
    //        nodes.ChildNodes.Add(node);
    //        BindSubTree(node, dt);
    //        node.Expanded = false;
    //    }

    //}




    private void BindModuleTree()
    {
        string CompanyID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        DataTable moduleList = XBase.Business.Common.SafeUtil.InitMenuData(UserID, CompanyID);
        DataRow[] rows = moduleList.Select("ParentID is null");
        foreach (DataRow row in rows)
        {
            TreeNode node = new TreeNode();
            node.Value = row["ModuleID"].ToString();
            node.Text = row["ModuleName"].ToString();
            node.NavigateUrl = "#";
            BindSubTree(node, moduleList);
            TreeView2.Nodes.Add(node);
            //TreeView2.ShowCheckBoxes = TreeNodeTypes.All;
            node.Expanded = false;
            TreeView2.Attributes.Add("onclick", "OnTreeNodeMChecked()");
        }
    }

    private void BindSubTree(TreeNode nodes, DataTable dt)
    {
        DataRow[] rows = dt.Select("ParentID='" + nodes.Value + "'");
        TreeNode node = null;
        foreach (DataRow row in rows)
        {
            node = new TreeNode();
            node.Value = row["ModuleID"].ToString();
            node.Text = row["ModuleName"].ToString();
            node.NavigateUrl = "#";
            nodes.ChildNodes.Add(node);
            BindSubTree1(node, dt);
            node.Expanded = false;
        }

    }
    private void BindSubTree1(TreeNode nodes, DataTable dt)
    {
        DataRow[] rows = dt.Select("ParentID='" + nodes.Value + "'");
        TreeNode node = null;
        foreach (DataRow row in rows)
        {
            node = new TreeNode();
            node.Value = row["ModuleID"].ToString();
            node.Text = row["ModuleName"].ToString();
            node.NavigateUrl = "#";
            nodes.ChildNodes.Add(node);
            BindSubTree2(node, dt);
        }

    }
    private void BindSubTree2(TreeNode nodes, DataTable dt)
    {
        DataRow[] rows = dt.Select("ParentID='" + nodes.Value + "'");
        TreeNode node = null;
        foreach (DataRow row in rows)
        {
            node = new TreeNode();
            node.Value = row["ModuleID"].ToString();
            node.Text = row["ModuleName"].ToString();
            node.NavigateUrl = "#";
            nodes.ChildNodes.Add(node);
            node.ShowCheckBox = true;
        }
        if (rows.Length == 0)
        {
            nodes.ShowCheckBox = true;
        }

    }
}
