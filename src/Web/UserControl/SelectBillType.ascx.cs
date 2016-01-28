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
using System.Collections.Generic;
public partial class UserControl_SelectBillType : System.Web.UI.UserControl
{
    private List<string> RetPollutionList = new List<string>();
    private List<string> RetList = new List<string>();
    private string QueryString;
    private string QueryText;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTree();
        }
        Button1.Attributes.Add("onclick", "return popBillObj.CheckSelect();");
    }

    private void BindTree()
    {
        string TypeFlag = "3";
        TreeNode BillTypeNode = new TreeNode();
        BillTypeNode.Text = "单据类型";
        BillTypeNode.Value = "0";
        Tree_BillTpye.Nodes.Add(BillTypeNode);
        BindTreeChildNodes(BillTypeNode, TypeFlag);
        Tree_BillTpye.DataBind();
    }

    private void BindTreeChildNodes(TreeNode node, string flag)
    {
        DataTable dt = ApprovalFlowSetBus.GetBillTypeByTypeFlag(flag);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow Row in dt.Rows)
            {
                TreeNode nodes = new TreeNode();
                nodes.Text = Row["TypeName"].ToString();
                nodes.Value = Row["TypeCode"].ToString();
                nodes.ShowCheckBox = true;
                node.ChildNodes.Add(nodes);
                nodes.NavigateUrl = "javascript:void(0)";
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CheckPollutionNode(Tree_BillTpye.CheckedNodes);
        for (int i = 0; i < RetPollutionList.Count; i++)
        {
            QueryString += "," + RetPollutionList[i];
        }
        for (int i = 0; i < RetList.Count; i++)
        {
            QueryText += "," + RetList[i];
        }
        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.Page.ToString(), "<script>getBillSelectOption('" + QueryString + "')</script>");
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.Page.ToString(), "<script>getBillSelectText('" + QueryText + "');getBillSelectOption('" + QueryString + "')</script>");
    }

    private void CheckPollutionNode(TreeNodeCollection TNC)
    {
        if (TNC.Count == 0) return;
        foreach (TreeNode TNode in TNC)
        {
            string NodeCode = TNode.Value;
            string NodeText = TNode.Text;
            RetPollutionList.Add(TNode.Value);
            RetList.Add(TNode.Text);
            TreeNodeCollection tnc = TNode.ChildNodes;
            CheckPollutionNode(tnc);
        }
    }
}
