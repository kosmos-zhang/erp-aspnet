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
using XBase.Business.Office.SellManager;
using XBase.Model.Office.SellManager;
public partial class Pages_Office_SellManager_AdversaryInfoMod : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindTreeChildNodes();


        CustType.TypeFlag = ConstUtil.SELL_TYPE_SELL;//竞争对手类型
        CustType.TypeCode = ConstUtil.SELL_TYPE_ADVERSARY;//竞争对手类型
        CustType.IsInsertSelect = true;

        AreaID.TypeFlag = ConstUtil.CUST_TYPE_CUST;//竞争对手类型
        AreaID.TypeCode = ConstUtil.CUST_INFO_AREAID;//竞争对手类型
        AreaID.IsInsertSelect = true;
    }

    #region 绑定竞争对手分类
    private void BindTreeChildNodes()
    {
        DataTable dt_product = AdversaryInfoBus.GetAdversaryType();
        DataView dataView = dt_product.DefaultView;
        foreach (DataRow row in dt_product.Select("SupperID=0"))
        {
            TreeNode nodes = new TreeNode();
            nodes.Text = row["CodeName"].ToString();
            nodes.Value = row["ID"].ToString();

            nodes.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}');", nodes.Text, nodes.Value);

            LoadSubData(row["ID"].ToString(), nodes, dt_product);
            //node.ChildNodes.Add(nodes);
            this.TreeView1.Nodes.Add(nodes);
            nodes.Expanded = false;
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
