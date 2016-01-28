using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.Common;
using XBase.Common;
public partial class Pages_Common_SelectUserOrDept : System.Web.UI.Page
{
    #region 私有成员
    //显示类别 1部门 2人员
    private string _ShowType = string.Empty;

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
            else
            {
                ShowType = Request.QueryString["ShowType"].Trim().ToString();
            }
            if (Request.QueryString["Subflag"] == null)
            {
                if (ShowType == "2" && OprtType == "2")
                {
                    UserDeptTree.ShowCheckBoxes = TreeNodeTypes.All;

                    UserDeptTree.Attributes.Add("onclick", "CheckEvent()");
                }
                else if (ShowType == "1" && OprtType == "2")
                {
                    UserDeptTree.ShowCheckBoxes = TreeNodeTypes.All;

                    UserDeptTree.Attributes.Add("onclick", "CheckEvent()");
                }
                
                BindTree();
            }
            else
            {
                
                getsubCompanyinfo();
            }

            if (ShowType == "1")
            {
                this.IsShowLizhi.Visible = false;
                this.RblIsShowLizhi.Visible = false;
            }
            else
            {
                this.IsShowLizhi.Visible = true;
                this.RblIsShowLizhi.Visible = true;
            }
        }
    }

    #region 生成子公司树结构
    protected void getsubCompanyinfo()
    {
        DataTable dt = UserDeptSelectBus.GetSubCompanyinfo(ShowType);
        //根节点
        TreeNode rootNode = new TreeNode();
        rootNode.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyName + "组织机构";
        rootNode.Value = "0";
        if (dt != null && dt.Rows.Count > 0)
        {
            TreeNode childNode = null;
            foreach (DataRow rows in dt.Rows)
            {
                childNode = new TreeNode();
                childNode.Value = rows["ID"].ToString();
                childNode.Text = rows["DeptName"].ToString();
                childNode.NavigateUrl = "javascript:void(0);";
                rootNode.ChildNodes.Add(childNode);
            }
            UserDeptTree.Nodes.Add(rootNode);
        }
    }
    #endregion

    #region 生成部门人员树
    private void BindTree()
    {
        DataTable dt = UserDeptSelectBus.GetDeptInfoByCompanyCD(ShowType, OprtType);
        DataTable Userdt = null;
        if (!string.IsNullOrEmpty(ShowType) && Convert.ToInt32(ShowType) > 1)
        {
            string isShowLizhi = "0";
            if (this.RblIsShowLizhi.Checked)
            {
                isShowLizhi = "1";
            }
            Userdt = UserDeptSelectBus.GetUserInfo(ShowType, OprtType,isShowLizhi);
        }
        //根节点
        TreeNode rootNode = new TreeNode();
        rootNode.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyName + "组织机构";
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
                    if (ShowType == "1" && OprtType == "2")
                    {
                        if (rows[i]["SuperDeptID"] == DBNull.Value)
                        {
                            /* Edit by ellen at 2010-08-23 16:18 */
                            if (rows[i]["subflag"].ToString()=="0")
                            {
                                childNode.Value = rows[i]["ID"].ToString();
                                childNode.Text = rows[i]["DeptName"].ToString();
                                childNode.NavigateUrl = string.Format("javascript:void('{0},{1}');", childNode.Text, childNode.Value);
                                childNode.ToolTip = "部门";

                            }
                            else
                            {
                                childNode.Value = rows[i]["ID"].ToString();
                                childNode.Text = rows[i]["DeptName"].ToString();
                                childNode.NavigateUrl = "javascript:void(0);";
                                childNode.ToolTip = "分公司";
                            }
                        }
                 
                    }
                    else
                    {
                        childNode.Value = rows[i]["ID"].ToString();
                        childNode.Text = rows[i]["DeptName"].ToString();
                        childNode.NavigateUrl = "javascript:void(0);";
                    }
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
                                    Usernode.Text = UserRow[j]["EmployeesName"].ToString();

                                    if (ShowType == "2" && OprtType == "2")
                                    {
                                        Usernode.NavigateUrl = string.Format("javascript:void('{0},{1}');", Usernode.Text, Usernode.Value);
                                    }
                                    else
                                    {
                                        Usernode.NavigateUrl = string.Format("javascript:void(0);");
                                    }
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
                    if (ShowType == "1" && OprtType == "2")
                    {
                        /* Edit by ellen at 2010-08-23 16:18 */
                        if (rows[i]["subflag"].ToString() == "0")
                        {
                            node.Value = rows[i]["ID"].ToString();
                            node.Text = rows[i]["DeptName"].ToString();
                            node.NavigateUrl = string.Format("javascript:void('{0},{1}');", node.Text, node.Value);
                            node.ToolTip = "部门";
                        }
                        else
                        {
                            node.Value = rows[i]["ID"].ToString();
                            node.Text = rows[i]["DeptName"].ToString();
                            node.NavigateUrl = string.Format("javascript:void(0);");
                            node.ToolTip = "分公司";
                        }
                    }
                    else
                    {
                        node.Value = rows[i]["ID"].ToString();
                        node.Text = rows[i]["DeptName"].ToString();
                        node.NavigateUrl = "javascript:void(0);";
                    }
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
                                    Usernode.Text = UserRow[j]["EmployeesName"].ToString();

                                    if (ShowType == "2" && OprtType == "2")
                                    {
                                        Usernode.NavigateUrl = string.Format("javascript:void('{0},{1}');", Usernode.Text, Usernode.Value);
                                    }
                                    else
                                    {
                                        Usernode.NavigateUrl = string.Format("javascript:void(0);");
                                    }

                                    node.ChildNodes.Add(Usernode);
                                }
                            }
                        }
                    }
                    BindChildNode(node.Value, node, dt, Userdt);
                    treenode.ChildNodes.Add(node);
                }
            }
        }
    }
    #endregion
    protected void RblIsShowLizhi_CheckedChanged(object sender, EventArgs e)
    {
        UserDeptTree.Nodes.Clear();
        if (Request.QueryString["ShowType"] != null &&
                Request.QueryString["OprtType"] != null)
        {
            ShowType = Request.QueryString["ShowType"].Trim().ToString();
            OprtType = Request.QueryString["OprtType"].Trim().ToString();
        }
        else
        {
            ShowType = Request.QueryString["ShowType"].Trim().ToString();
        }
        if (Request.QueryString["Subflag"] == null)
        {
            if (ShowType == "2" && OprtType == "2")
            {
                UserDeptTree.ShowCheckBoxes = TreeNodeTypes.All;

                UserDeptTree.Attributes.Add("onclick", "CheckEvent()");
            }
            else if (ShowType == "1" && OprtType == "2")
            {
                UserDeptTree.ShowCheckBoxes = TreeNodeTypes.All;

                UserDeptTree.Attributes.Add("onclick", "CheckEvent()");
            }

            BindTree();
        }
        else
        {

            getsubCompanyinfo();
        }

        this.UserDeptTree.ExpandAll();
    }
}
