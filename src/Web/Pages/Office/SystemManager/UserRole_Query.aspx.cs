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
using XBase.Common;

public partial class Pages_Office_SystemManager_UserRole_Query : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDplUsreInfo();
            BindRoleInfo();
            this.hidModuleID.Value = ConstUtil.Menu_AddUserRole;

            string requestParam = Request.QueryString.ToString();
            //从列表过来时
            int firstIndex = requestParam.IndexOf("&");
            //返回回来时
            if (firstIndex > 0)
            {
                //获取是否查询的标识
                string flag = Request.QueryString["Flag"];
                //点击查询时，设置查询的条件，并执行查询
                if ("1".Equals(flag))
                {

                    this.lstRoleID_Drp_RoleInfo.SelectedValue = Request.QueryString["RoleID"];
                    Drp_UserInfo.Value = Request.QueryString["UserID"];
                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");Fun_Search_UserRoleInfo('" + pageIndex + "');</script>");
                }
            }

        }
    }

    private void BindDplUsreInfo()
    {
        DataTable userList = XBase.Business.Office.SystemManager.UserInfoBus.GetUserList();

        string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
      
            TreeNode node = new TreeNode();
            node.Text = "用户列表";
            node.NavigateUrl = "#";

            DataRow[] rows = userList.Select("CompanyCD='" + companycd + "'");
            foreach (DataRow row in rows)//.GetEnumerator())
            {
                TreeNode node2 = new TreeNode(row["UserID"].ToString());
                node2.NavigateUrl = "#";
                node.ChildNodes.Add(node2);
            }
            TreeView1.Nodes.Add(node);
            //TreeView1.ShowCheckBoxes = TreeNodeTypes.All;
            TreeView1.ExpandDepth = 0;
            TreeView1.Attributes.Add("onclick", "OnTreeNodeClick()");
    }
    #region 根据企业编码获取企业角色
    private void BindRoleInfo()
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        DataTable dt = RoleInfoBus.GetRoleInfo(CompanyCD);
        if (dt.Rows.Count > 0)
        {
            lstRoleID_Drp_RoleInfo.DataTextField = "RoleName";
            lstRoleID_Drp_RoleInfo.DataValueField = "RoleID";
            lstRoleID_Drp_RoleInfo.DataSource = dt;
            lstRoleID_Drp_RoleInfo.DataBind();
            ListItem Item = new ListItem();
            Item.Value = "0";
            Item.Text = "--请选择--";
            lstRoleID_Drp_RoleInfo.Items.Insert(0, Item);
        }
    }
    #endregion
}
