using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.Office.SystemManager;
public partial class Pages_Office_SystemManager_CodePublicType : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.hf_typeflag.Value = Request["TypeFlag"].ToString();
                BindTypeCode();
                BindTree();
            }
            catch (Exception)
            {
            }
           
        }
    }
    /// <summary>
    /// 绑定到单据
    /// </summary>
    private void BindTypeCode()
    {
        DataTable dt = XBase.Business.SystemManager.ApprovalFlowSetBus.GetCodePublicByTypeFlag(this.hf_typeflag.Value);
       
        if (dt!=null&&dt.Rows.Count > 0)
        {
            drp_typecode.DataTextField = "TypeName";
            drp_typecode.DataValueField = "TypeCode";
            drp_typecode.DataSource = dt;
            drp_typecode.DataBind();
        }

    }
    /// <summary>
    /// 加载树
    /// </summary>
    private void BindTree()
    {
        DataTable dt = CodePublicTypeBus.GetCodePublicByTypeLabel(this.hf_typeflag.Value);
        if (dt != null&&dt.Rows.Count > 0)
        {
            TreeNode node = new TreeNode();
            node.Text = "类别列表";
            node.NavigateUrl = "#";
            foreach (DataRow row in dt.Rows)//.GetEnumerator())
            {
                TreeNode node2 = new TreeNode();
                node2.Value = row["TypeCode"].ToString();
                node2.Text = row["TypeName"].ToString();
                node2.NavigateUrl = "#";
                node.ChildNodes.Add(node2);
            }

            TreeView1.Nodes.Add(node);
            TreeView1.ShowCheckBoxes = TreeNodeTypes.All;
            TreeView1.Attributes.Add("onclick", "OnTreeNodeChecked()");
        }
    }
}
