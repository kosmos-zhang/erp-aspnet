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
using XBase.Business.Office.CustManager;
using XBase.Business.Office.SystemManager;
using XBase.Business.Office.StorageManager;
public partial class Pages_Office_StorageManager_StorageReportCust : System.Web.UI.Page
{
    #region 客户类别定义
    private const string CUST_PROVIDER = "供应商";
    private const string CUST_CUST = "客户";
    private const string CUST_FOREIGN = "外协加工厂";
    private const string CUST_OTHER = "其他客户";
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Custom"] != null)
                BindCustOnly();
            else
                BindCust();
        }
        
    }

    #region 绑定往来客户
    private void BindCust()
    {
        TreeNode rootNode = new TreeNode();
        rootNode.Text = "往来客户选择";
        rootNode.NavigateUrl = string.Format("javascript:javascript:void(0)");
        rootNode.Value = "0";
        CustTree.Nodes.Add(rootNode);
        TreeNode custNode = new TreeNode();
        custNode.Value = "1";
        custNode.Text = "客户";
        custNode.NavigateUrl = string.Format("javascript:javascript:void(0)");

        TreeNode ProrootNode = new TreeNode();
        ProrootNode.Value = "2";
        ProrootNode.Text = "供应商";
        ProrootNode.NavigateUrl = string.Format("javascript:javascript:void(0)");

        TreeNode fordignNode = new TreeNode();
        fordignNode.Value = "5";
        fordignNode.Text = "外协加工厂";
        fordignNode.NavigateUrl = string.Format("javascript:javascript:void(0)");

        TreeNode OtherNode = new TreeNode();
        OtherNode.Value = "7";
        OtherNode.Text = "其他客户";
        OtherNode.NavigateUrl = string.Format("javascript:javascript:void(0)");


        rootNode.ChildNodes.Add(ProrootNode);
        rootNode.ChildNodes.Add(custNode);
        rootNode.ChildNodes.Add(fordignNode);
        rootNode.ChildNodes.Add(OtherNode);

        CustTreeShow(CUST_PROVIDER, ProrootNode);
        CustTreeShow(CUST_CUST, custNode);
        CustTreeShow(CUST_FOREIGN, fordignNode);
        CustTreeShow(CUST_OTHER, OtherNode);

    }
    #endregion


    #region 只绑定客户
    private void BindCustOnly()
    {
        TreeNode rootNode = new TreeNode();
        rootNode.Text = "往来客户选择";
        rootNode.NavigateUrl = string.Format("javascript:javascript:void(0)");
        rootNode.Value = "0";
        CustTree.Nodes.Add(rootNode);
        TreeNode custNode = new TreeNode();
        custNode.Value = "1";
        custNode.Text = "客户";
        custNode.NavigateUrl = string.Format("javascript:javascript:void(0)");


    



        rootNode.ChildNodes.Add(custNode);



        CustTreeShow(CUST_CUST, custNode);

    }
    #endregion



    #region 根据部同类别取节点信息
    private void CustTreeShow(string CustType, TreeNode treenode)
    {
        if (CustType == CUST_PROVIDER)
        {
            //获取供应商信息
            DataTable dt = ProviderInfoBus.GetProviderInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    string spanId = "node_" + rows["ID"].ToString();
                    node.Text = " <input name=\"select\" id=\"ra" + rows["ID"] + "\" value=\"" + rows["ID"].ToString() + "|" + rows["CustName"].ToString() + "|2|供应商" + "\" type=\"radio\" />" + rows["CustName"].ToString() + "";
                    //node.Value = rows["ID"].ToString();
                    //node.ImageUrl = "../../../Images/treeimg/page.gif";
                    // node.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}');", rows["CustName"].ToString() + "|" + rows["ID"].ToString() + "|2|供应商", spanId);
                    treenode.ChildNodes.Add(node);
                }
            }
        }
        else if (CustType == CUST_CUST)
        {
            //获取客户信息
            DataTable dt = CustInfoBus.GetCustInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    string spanId = "node_" + rows["ID"].ToString();
                    node.Text = " <input name=\"select\" id=\"ra" + rows["ID"] + "\" value=\"" + rows["ID"].ToString() + "|" + rows["CustName"].ToString() + "|1|客户" + "\" type=\"radio\" />" + rows["CustName"].ToString() + "";
                    //node.Value = rows["ID"].ToString();
                    //node.ImageUrl = "../../../Images/treeimg/page.gif";
                    //node.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}');", rows["CustName"].ToString() + "|" + rows["ID"].ToString() + "|1|客户", spanId);
                    treenode.ChildNodes.Add(node);
                }
            }
        }
        else if (CustType == CUST_OTHER)
        {
            //获取其他客户信息
            DataTable dt = CheckReportBus.GetOtherInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    string spanId = "node_" + rows["ID"].ToString();
                    node.Text = " <input name=\"select\" id=\"ra" + rows["ID"] + "\" value=\"" + rows["ID"].ToString() + "|" + rows["CustName"].ToString() + "|7|其他客户" + "\" type=\"radio\" />" + rows["CustName"].ToString() + "";
                    //node.Value = rows["ID"].ToString();
                    //node.ImageUrl = "../../../Images/treeimg/page.gif";
                    //  node.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}');", rows["CustName"].ToString() + "|" + rows["ID"].ToString() + "|7|其他客户", spanId);
                    treenode.ChildNodes.Add(node);
                }
            }
        }
        else if (CustType == CUST_FOREIGN)
        {//获取外协
            DataTable dt = CheckReportBus.GetForeignInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    string spanId = "node_" + rows["ID"].ToString();
                    node.Text = " <input name=\"select\" id=\"ra" + rows["ID"] + "\" value=\"" + rows["ID"].ToString() + "|" + rows["CustName"].ToString() + "|5|外协加工厂" + "\" type=\"radio\" />" + rows["CustName"].ToString() + "";
                    //node.Value = rows["ID"].ToString();
                    //node.ImageUrl = "../../../Images/treeimg/page.gif";
                    // node.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}');", rows["CustName"].ToString() + "|" + rows["ID"].ToString() + "|5|外协加工厂", spanId);
                    treenode.ChildNodes.Add(node);
                }
            }
        }
    }
    #endregion
}
