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
using XBase.Business.Common;
using XBase.Business.Office.HumanManager;
using XBase.Common;

public partial class Pages_Office_HumanManager_SalaryEmployeeStructureSet : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTree();
        }
    }



    #region 生成部门人员树
    private void BindTree()
    {
        //部门
        DataTable dt = SalaryEmployeeStructureSetBus.GetDeptInfoByCompanyCD();
        //人员
        DataTable Userdt = SalaryEmployeeStructureSetBus.GetUserInfo();
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
                                Usernode.Text = "<img  src=\"../../../Images/per.jpg\" border=\"0\" /><span style=\"font-size:12px;font-color:#006699;\">" + UserRow[j]["QuarterName"].ToString() + " &nbsp;&nbsp;&nbsp; " + UserRow[j]["EmployeesName"].ToString() + " </span>";
                                Usernode.NavigateUrl = string.Format("javascript:GetUserInfo({0},'{1}')", Usernode.Value, UserRow[j]["EmployeesName"].ToString());
                                childNode.ChildNodes.Add(Usernode);
                            }
                        }
                    }
                    rootNode.ChildNodes.Add(childNode);

                    BindChildNode(childNode.Value, childNode, dt, Userdt);
                }
            }
        }
        tvUserTree.Nodes.Add(rootNode);
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
                                Usernode.Text = "<img  src=\"../../../Images/per.jpg\" border=\"0\" /><span style=\"font-size:12px;font-color:#006699;\">" + UserRow[j]["QuarterName"].ToString() + " &nbsp;&nbsp;&nbsp; " + UserRow[j]["EmployeesName"].ToString() + " </span>";
                                Usernode.NavigateUrl = string.Format("javascript:GetUserInfo({0},'{1}')", Usernode.Value, UserRow[j]["EmployeesName"].ToString());
                                node.ChildNodes.Add(Usernode);
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
