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
using XBase.Business.Office.PurchaseManager;

public partial class Handler_Office_PurchaseManager_ProductType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TreeNode root = new TreeNode();
            root.Value = "0";
            root.Text = "全部";
            //root.NavigateUrl = string.Format("javascript:popProductTypeObj.SelectedNodeChanged('{0}','{1}');", root.Value, root.Text);
            ProductTypeTree.Nodes.Add(root);
            BindTreeChildNodes(root);
        }
    }

    private void BindTreeChildNodes(TreeNode node)
    {
        DataTable dt = PurchaseRequireBus.GetProductType(Convert.ToInt32(node.Value));
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow Row in dt.Rows)
            {
                TreeNode nodes = new TreeNode();
                nodes.Text = Row["TypeName"].ToString();
                nodes.Value = Row["ID"].ToString();

                node.ChildNodes.Add(nodes);
                BindTreeChildNodes(nodes);
            }

        }
    }


    protected void ProductTypeTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        int a = this.ProductTypeTree.SelectedNode.ChildNodes.Count;

        string b = this.ProductTypeTree.SelectedNode.Value.ToString();
        string c = this.ProductTypeTree.SelectedNode.Text.ToString();
        if (a > 0)
        {
            foreach (TreeNode node in this.ProductTypeTree.SelectedNode.ChildNodes)
            {
                b += "," + node.Value;
                b += get(node);
            }
        }

        string bc = b +"|"+ c;
       ClientScript.RegisterClientScriptBlock(this.GetType(),"ProductType", "<script language=javascript>window.returnValue ='" + bc + "';window.close()</script>");
    }


    private string get(TreeNode node)
    {
        string aa = "";
        if (node != null)
        {
            foreach (TreeNode noded in node.ChildNodes)
            {
                aa += "," + noded.Value;
                aa += get(noded);
            }
        }
        return aa;
    }
}
