using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using XBase.Business.SystemManager;
using System.Collections.Generic;
public partial class Pages_Office_SystemManager_test : System.Web.UI.Page
{
    private List<string> RetPollutionList = new List<string>();
    private string QueryString;
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
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CheckPollutionNode(Tree_BillTpye.CheckedNodes);
        for (int i = 0; i < RetPollutionList.Count; i++)
        {
         QueryString += "|" + RetPollutionList[i];
        }
        //if (QueryString == null)
        //{
        //    this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('请选择单据！');</script>");
        //    return;
        //}
        Response.Write("<script language='javascript'>window.returnValue ='" + QueryString + "';window.close();</script>");
    }

    private void CheckPollutionNode(TreeNodeCollection TNC)
    {
        if (TNC.Count == 0) return;
        foreach (TreeNode TNode in TNC)
        {
            string NodeCode = TNode.Value;
            RetPollutionList.Add(TNode.Value);
            TreeNodeCollection tnc = TNode.ChildNodes;
            CheckPollutionNode(tnc);
        }
    }
}
