using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.Common;
using XBase.Business.Office.HumanManager;
using XBase.Common;

public partial class Pages_Office_HumanManager_DeptQuarterEmployee_Info : System.Web.UI.Page
{
    #region 私有成员
    //显示类别 1部门 2人员
    private string _ShowType = "2";

    //单选多选 1单选 2多选 默认为单选
    private string _OprtType = string.Empty;
    #endregion

    #region 属性
    public string ShowType
    {
        get { return _ShowType; }
        set { _ShowType = value; }
    }

    public string OprtType
    {
        get { return _OprtType; }
        set { _OprtType = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["ShowType"] != null &&
                Request.QueryString["OprtType"] != null)
            {
                ShowType = Request.QueryString["ShowType"].Trim().ToString();
                OprtType = Request.QueryString["OprtType"].Trim().ToString();
            }
            BindTree();
        }
    }

    #region 生成部门人员树
    private void BindTree()
    {
        DataTable dt = UserDeptSelectBus.GetDeptInfoByCompanyCD(ShowType, OprtType);
        DataTable Userdt = null;
        if (!string.IsNullOrEmpty(ShowType) && Convert.ToInt32(ShowType) > 1)
        {
            Userdt = DeptQuarterEmployeeBus.GetUserInfo(ShowType, OprtType);
        }
        //<img  src=\"../../../Images/folder-closed.gif\"/>
        //<img  src=\"../../../Images/jsdoc.gif\"/>
        //根节点
        TreeNode rootNode = new TreeNode();
        rootNode.Text = " <img  src=\"../../../Images/dep.jpg\" border=\"0\"/><span style=\"font-size:14px;height:25px\"> " + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyName + " </span>";

        rootNode.Value = "0";
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow[] rows = dt.Select("SuperDeptID IS NULL");
            if (rows.Length > 0)
            {
                TreeNode childNode = null;
                for (int i = 0; i < rows.Length; i++)
                {
                    childNode = new TreeNode();
                    childNode.Value = rows[i]["ID"].ToString();
                    childNode.Text = "<img  src=\"../../../Images/dep.jpg\" border=\"0\"/><span style=\"font-size:14px\"> " + rows[i]["DeptName"].ToString() + " </span>";
                    childNode.NavigateUrl = "javascript:void(0);";
                    if (!string.IsNullOrEmpty(ShowType) && Convert.ToInt32(ShowType) > 1)
                    {
                        TreeNode Usernode = null;
                        if (Userdt != null && Userdt.Rows.Count > 0)
                        {
                            string UserExprssion = "DeptID='" + childNode.Value + "'";
                            DataRow[] UserRow = Userdt.Select(UserExprssion);
                            if (UserRow.Length > 0)
                            {
                                for (int j = 0; j < UserRow.Length; j++)
                                {

                                    Usernode = new TreeNode();
                                    Usernode.Value = UserRow[j]["ID"].ToString();
                                    Usernode.Text = "<img  src=\"../../../Images/per.jpg\" border=\"0\" /><span style=\"font-size:12px;font-color:#006699;\"> " + UserRow[j]["QuarterName"].ToString() + "&nbsp;&nbsp;&nbsp; " + UserRow[j]["EmployeesName"].ToString() + " </span>";
                                    Usernode.NavigateUrl = string.Format("javascript:javascript:void(0)");
                                    childNode.ChildNodes.Add(Usernode);
                                }
                            }
                        }
                    }
                    rootNode.ChildNodes.Add(childNode);

                    BindChildNode(childNode.Value, childNode, dt, Userdt);
                }
            }
        }
        UserDeptTree.Nodes.Add(rootNode);
    }
    #endregion


    #region 递归子部门及人员
    private void BindChildNode(string SuperDeptID, TreeNode treenode, DataTable dt, DataTable Userdt)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            string Expression = "SuperDeptID='" + SuperDeptID + "'";
            DataRow[] rows = dt.Select(Expression);
            TreeNode node = null;
            TreeNode Usernode = null;
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    node = new TreeNode();
                    node.Value = rows[i]["ID"].ToString();
                    node.Text = "  <img  src=\"../../../Images/dep.jpg\" border=\"0\" /><span style=\"font-size:14px\"> " + rows[i]["DeptName"].ToString() + " </span>";
                    ///rows[i]["DeptName"].ToString();
                    node.NavigateUrl = string.Format("javascript:javascript:void(0)");
                    if (!string.IsNullOrEmpty(ShowType) && Convert.ToInt32(ShowType) > 1)
                    {
                        if (Userdt != null && Userdt.Rows.Count > 0)
                        {
                            Usernode = new TreeNode();
                            string UserExprssion = "DeptID='" + node.Value + "'";
                            DataRow[] UserRow = Userdt.Select(UserExprssion);
                            if (UserRow.Length > 0)
                            {
                                for (int j = 0; j < UserRow.Length; j++)
                                {
                                    Usernode = new TreeNode();
                                    Usernode.Value = UserRow[j]["ID"].ToString();
                                    Usernode.Text = "<img  src=\"../../../Images/per.jpg\" border=\"0\" /><span style=\"font-size:12px;font-color:#006699;\"> " + UserRow[j]["QuarterName"].ToString() + "&nbsp;&nbsp;&nbsp; " + UserRow[j]["EmployeesName"].ToString() + " </span>";
                                    Usernode.NavigateUrl = string.Format("javascript:javascript:void(0)");
                                    node.ChildNodes.Add(Usernode);
                                }
                            }
                        }
                    }
                    treenode.ChildNodes.Add(node);
                    BindChildNode(node.Value, node, dt, Userdt);
                }
            }
        }
    }
    #endregion
}
