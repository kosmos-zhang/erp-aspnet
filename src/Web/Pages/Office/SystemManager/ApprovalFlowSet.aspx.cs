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
using XBase.Business.SystemManager;
public partial class Pages_Office_SystemManager_ApprovalFlowSet :BasePage
{
    string typeflag = null;
    string typecode = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTree();
        }
    }

    protected void imgbtn_add_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ApprovalFlowSetAdd.aspx?typeflag=" + typeflag + "&typecode=" + typecode);

    }
    private void BindTree()
    {
        try
        {
            string TypeFlag = Request["TypeFlag"].ToString();
            if (!string.IsNullOrEmpty(TypeFlag))
            {
                DataTable dt = ApprovalFlowSetBus.GetBillTypeByType(TypeFlag);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TreeNode node = new TreeNode();
                    node.Value = dt.Rows[0]["TypeFlag"].ToString();
                    node.Text = dt.Rows[0]["ModuleName"].ToString();
                    node.NavigateUrl = string.Format("javascript:javascript:void(0)");
                    BindTreeChildNodes(node);
                    Tree_BillTpye.Nodes.Add(node);
                    node.Expanded = true;
                }

                Tree_BillTpye.DataBind();
                Tree_BillTpye.Nodes[0].Selected = true;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
     
    
    }
    private void BindTreeChildNodes(TreeNode node)
    {
        DataTable dt = ApprovalFlowSetBus.GetBillFlowTypeByTypeFlag(node.Value);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow Row in dt.Rows)
            {
                TreeNode nodes = new TreeNode();
                nodes.Text = Row["TypeName"].ToString();
                nodes.Value = Row["TypeCode"].ToString();
                nodes.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}','{2}','{3}');", nodes.Text, nodes.Value, node.Value, node.Text);
                node.ChildNodes.Add(nodes);
            }
        }
    }
}
