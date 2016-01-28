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
using XBase.Business.Office.SystemManager;

public partial class UserControl_ProductInfoBatchControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindM();
            BindTree();
            BindColor();
        }
    }

    private void BindM()
    {
        string Code = "5";
        string TypeFlag = "5";
        DataTable dt_Mater = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, Code);
        if (dt_Mater.Rows.Count > 0)
        {
            this.sel_Material.DataTextField = "TypeName";
            sel_Material.DataValueField = "ID";
            sel_Material.DataSource = dt_Mater;
            sel_Material.DataBind();
        }
        sel_Material.Items.Insert(0, new ListItem("--请选择--", "0"));
    }
    /// <summary>
    /// 绑定树
    /// </summary>
    private void BindTree()
    {
        DataTable dt_product = CategorySetBus.GetProductType();
        DataView dataView = dt_product.DefaultView;
        string BigtypeName = "";
        for (int i = 1; i < 8; i++)
        {
            dataView.RowFilter = "TypeFlag='" + i + "'";
            DataTable dtnew = new DataTable();
            dtnew = dataView.ToTable();
            TreeNode node = new TreeNode();
            switch (i)
            {
                case 1:
                    BigtypeName = "成品";
                    break;
                case 2:
                    BigtypeName = "原材料";
                    break;
                case 3:
                    BigtypeName = "固定资产";
                    break;
                case 4:
                    BigtypeName = "低值易耗";
                    break;
                case 5:
                    BigtypeName = "包装物";
                    break;
                case 6:
                    BigtypeName = "服务产品";
                    break;
                case 7:
                    BigtypeName = "半成品";
                    break;
            }
            try
            {
                node.Value = dtnew.Rows[0]["TypeFlag"].ToString();
                node.Text = BigtypeName;
                node.NavigateUrl = string.Format("javascript:javascript:void(0)");
                BindTreeChildNodes(node, dtnew);
                this.TreeView1.Nodes.Add(node);
                //TreeView1.Attributes.Add("onclick", "OnTreeNodeClick()");
                node.Expanded = false;
            }
            catch
            {

            }

        }

    }

    #region 绑定颜色
    /// <summary>
    /// 绑定颜色
    /// </summary>
    private void BindColor()
    {
        string TypeFlag = "5";
        string Code = "3";
        DataTable dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, Code);
        if (dt.Rows.Count > 0)
        {
            this.selCorlor.DataTextField = "TypeName";
            selCorlor.DataValueField = "ID";
            selCorlor.DataSource = dt;
            selCorlor.DataBind();
        }
        selCorlor.Items.Insert(0, new ListItem("--请选择--", ""));
    }
    #endregion

    private void BindTreeChildNodes(TreeNode node, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=0"))
        {
            TreeNode nodes = new TreeNode();
            nodes.Text = row["CodeName"].ToString();
            nodes.Value = row["ID"].ToString();
            //TypeFlag = row["TypeFlag"].ToString();
            nodes.NavigateUrl = string.Format("javascript:SelectedNodeChanged1('{0}','{1}');", nodes.Text, nodes.Value);

            LoadSubData(row["ID"].ToString(), nodes, dt);
            node.ChildNodes.Add(nodes);
            node.Expanded = false;
        }
    }
    private void LoadSubData(string pid, TreeNode nodes, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=" + pid))
        {
            TreeNode nodess = new TreeNode();
            nodess.Text = row["CodeName"].ToString();
            nodess.Value = row["ID"].ToString();
            //TypeFlag = row["TypeFlag"].ToString();
            nodess.NavigateUrl = string.Format("javascript:SelectedNodeChanged1('{0}','{1}');", nodess.Text, nodess.Value);
            LoadSubData(row["ID"].ToString(), nodess, dt);
            nodes.ChildNodes.Add(nodess);
            nodes.Expanded = false;
        }
    }
}
