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
using XBase.Model.Office.PurchaseManager;
using XBase.Common;
using XBase.Business.Office.PurchaseManager;
using XBase.Business.Office.SystemManager;
public partial class Pages_Office_PurchaseManager_PurchaseRequireInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        if (!IsPostBack)
        {
            TreeNode root = new TreeNode();
            root.Value = "0";
            root.Text = "全部";
            this.TreeView1.Nodes.Add(root);
            BindTreeChildNodes(root);
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        PurchaseRequireModel PurchaseRequireM = new PurchaseRequireModel();
        PurchaseRequireM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PurchaseRequireM.ProdTypeID = txtProductTypeID.Value;
        PurchaseRequireM.ProdID = txtProductID.Value;
        PurchaseRequireM.CreateCondition = ddlCreate.Value;
        PurchaseRequireM.RequireDate = txtStartRequireDate.Value;
        PurchaseRequireM.EndRequireDate = txtEndRequireDate.Value;

        string OrderBy = hidOrderBy.Value;


        DataTable dt = PurchaseRequireBus.GetPurchaseRequireInfo(PurchaseRequireM, OrderBy);

        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "物料需求计划单编号", "物料编码", "物料名称", "物料分类", "规格", "单位", "订单需求量", "现有存量", "需申购数量", "采购提前期", "已计划数量", "需求日期" },
            new string[] { "MRPNo", "ProdNo", "ProductName", "ProductTypeName", "Specification", "UnitName", "NeedCount", "HasNum", "WantingNum", "WaitingDays", "OrderCount", "RequireDate" },
            "采购需求列表");
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
                nodes.NavigateUrl = string.Format("javascript:SelectedTypeChanged1('{0}','{1}');", nodes.Text, nodes.Value);
                node.ChildNodes.Add(nodes);
                BindTreeChildNodes(nodes);
            }

        }
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
