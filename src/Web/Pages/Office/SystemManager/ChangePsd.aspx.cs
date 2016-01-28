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
using System.Xml.Linq;
using XBase.Common;
using XBase.Business.Office.SystemManager;
public partial class Pages_Office_SystemManager_ChangePsd : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDplUsreInfo();
          //this.txt_User.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
          //  string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          //  //this.txt_User.Text = "admin";
          //  //string companyCD = "AAAAAA";
          //  DataTable dt = UserInfoBus.GetUserInfoByID(txt_User.Text, companyCD);
          //  if (dt.Rows.Count > 0)
          //  {
          //      hf_psd.Value = dt.Rows[0]["password"].ToString();
          //      hfcommanycd.Value = dt.Rows[0]["CompanyCD"].ToString();

          //      lblUserInfo.Text = this.txt_User.Text;
          //  }
        }
    }
    /// <summary>
    /// 绑定用户
    /// </summary>
    private void BindDplUsreInfo()
    {
        DataTable userList = XBase.Business.Office.SystemManager.UserInfoBus.GetUserList();

        //string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
         this.hfuserid.Value = userid;
        TreeNode node = new TreeNode();
        node.Text = "用户列表";
        node.NavigateUrl = string.Format("javascript:javascript:void(0)");

        DataRow[] rows = userList.Select("CompanyCD='" + companycd + "'");
        foreach (DataRow row in rows)//.GetEnumerator())
        {
            if (row["UserID"].ToString() != userid)
            {
                if (row["IsRoot"].ToString() != "1")
                {
                    TreeNode node2 = new TreeNode(row["UserID"].ToString());
                    DataTable dt = UserInfoBus.GetUserInfoByID(row["UserID"].ToString(), companycd);
                    if (dt.Rows.Count > 0)
                    {
                        hfcommanycd.Value = dt.Rows[0]["CompanyCD"].ToString();
                    }
                    node2.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}');", node2.Text);
                    node.ChildNodes.Add(node2);
                }
             
            }
        
        }
        this.Tree_BillTpye.Nodes.Add(node);
        Tree_BillTpye.ExpandAll();
    }
}
