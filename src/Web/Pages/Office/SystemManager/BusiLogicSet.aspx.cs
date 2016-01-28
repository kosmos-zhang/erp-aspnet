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
using XBase.Business.SystemManager;
using XBase.Common;
public partial class Pages_Office_SystemManager_BusiLogicSet :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          this.txtOpenDate.Value  =  DateTime.Now.AddDays (-1).ToShortDateString();
          this.txtCloseDate .Value = DateTime.Now.ToShortDateString();
        }
        BindUserTree();
        BindModuleTree();
    }
    private void BindUserTree()
    {
        string companycd= ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


        DataTable userList = XBase.Business.Office.SystemManager.UserInfoBus.GetUserList();
        DataView dataView = userList.DefaultView;
        dataView.RowFilter = "CompanyCD='" + companycd + "'";
        DataTable dtnew = new DataTable();
        dtnew = dataView.ToTable();
        

        ArrayList companyList = new ArrayList();
        foreach (DataRow row in dtnew.Rows)
        {
            string company = row["CompanyCD"].ToString();
            if (!companyList.Contains(company))
            {
                companyList.Add(company);
            }
        }

        foreach (string comn in companyList)
        {
            TreeNode node = new TreeNode();
            node.Text = "用户列表";
            node.NavigateUrl = "#";

            DataRow[] rows = userList.Select("CompanyCD='" + comn + "'");
            foreach (DataRow row in rows)//.GetEnumerator())
            {
                TreeNode node2 = new TreeNode(row["UserID"].ToString());
                node2.NavigateUrl = "#";
                node.ChildNodes.Add(node2);
            }

            TreeView1.Nodes.Add(node);
            TreeView1.ShowCheckBoxes = TreeNodeTypes.All;
            TreeView1.Attributes.Add("onclick", "OnTreeNodeChecked()");
        }
    }
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
