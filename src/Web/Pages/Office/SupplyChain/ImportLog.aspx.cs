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
using XBase.Business.Office.SupplyChain;
public partial class Pages_Office_SupplyChain_ImportLog : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtOpenDate.Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtCloseDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        lbl_title.Text = ProductInfoBus.GetLogTitle(Request.QueryString["ModuleID"].ToString()) + "列表";
        BindUserTree();
        //获取查询版块
        try
        {
            string mod = string.Empty;
            this.btn_mod.Value = Convert.ToString(Convert.ToInt32(Request.QueryString["ModuleID"].ToString()) - 1);
        }
        catch { }
    }

    private void BindUserTree()
    {
        string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


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
}
