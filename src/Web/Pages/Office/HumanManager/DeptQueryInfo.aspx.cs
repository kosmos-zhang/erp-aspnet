using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.Common;
using XBase.Business.Office.HumanManager;
using XBase.Common;

public partial class Pages_Office_HumanManager_DeptQueryInfo : System.Web.UI.Page
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
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
         
            codeRule.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
            codeRule.ItemTypeID = ConstUtil.CODING_BASE_ITEM_QUARTER;
            //岗位分类
            ddlQuarterType.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlQuarterType.TypeCode = ConstUtil.CODE_TYPE_QUARTER;
            ddlQuarterType.IsInsertSelect = true;
            //岗位级别
            ddlQuarterLevel.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlQuarterLevel.TypeCode = ConstUtil.CODE_TYPE_QUARTER_LEVEL;
            ddlQuarterLevel.IsInsertSelect = true;

           

            BindTree();
            BindTemplateTree();

        }

        ddlQuarterLevel.Enabled = false;
        ddlQuarterType.Enabled = false;
        if (Request.QueryString["QuarterID"] != null)
        {

         
            hidEditFlag.Value = "UPDATE";
            string quterID = Request.QueryString["QuarterID"].ToString();
            if (Request.QueryString["Sign"].ToString() == "1")
            {
                GettEMPLATE(quterID);
            }
            else
            {
                DataTable dtDeptInfo = DeptQuarterBus.GetDeptQuarterInfoWithID(quterID);
                if (dtDeptInfo.Rows.Count > 0)
                {

                    txtDeptID.Value = dtDeptInfo.Rows[0]["DeptID"] == null ? "" : dtDeptInfo.Rows[0]["DeptID"].ToString();

                    txtDisplayCode.Value = dtDeptInfo.Rows[0]["QuarterNo"] == null ? "" : dtDeptInfo.Rows[0]["QuarterNo"].ToString();
                    txtDisplayCode.Disabled = true;
                    txtDeptName.Text = dtDeptInfo.Rows[0]["DeptName"] == null ? "" : dtDeptInfo.Rows[0]["DeptName"].ToString();
                    txtSuperQuarterName.Text = dtDeptInfo.Rows[0]["SuperQuarterName"] == null ? "" : dtDeptInfo.Rows[0]["SuperQuarterName"].ToString(); //上级岗位

                    txtQuarterName.Text = dtDeptInfo.Rows[0]["QuarterName"] == null ? "" : dtDeptInfo.Rows[0]["QuarterName"].ToString(); //岗位名称
                    txtPYShort.Text = dtDeptInfo.Rows[0]["PYShort"] == null ? "" : dtDeptInfo.Rows[0]["PYShort"].ToString(); //拼音代码


                    ddlKeyFlag.SelectedValue = dtDeptInfo.Rows[0]["KeyFlag"] == null ? "" : dtDeptInfo.Rows[0]["KeyFlag"].ToString(); //是否关键岗位

                    ddlQuarterType.SelectedValue = dtDeptInfo.Rows[0]["TypeID"] == null ? "" : dtDeptInfo.Rows[0]["TypeID"].ToString(); //岗位分类

                    ddlQuarterLevel.SelectedValue = dtDeptInfo.Rows[0]["LevelID"] == null ? "" : dtDeptInfo.Rows[0]["LevelID"].ToString(); //岗位级别


                    txtDescription.Text = dtDeptInfo.Rows[0]["Description"] == null ? "" : dtDeptInfo.Rows[0]["Description"].ToString(); //描述信息
                    ddlUsedStatus.SelectedValue = dtDeptInfo.Rows[0]["UsedStatus"] == null ? "" : dtDeptInfo.Rows[0]["UsedStatus"].ToString(); //启用状态

                    string attachment = dtDeptInfo.Rows[0]["Attachment"] == null ? "" : dtDeptInfo.Rows[0]["Attachment"].ToString(); //附件
                    hidaddd.Value = attachment;

                    hfAttachment.Value = dtDeptInfo.Rows[0]["Attachment"] == null ? "" : dtDeptInfo.Rows[0]["Attachment"].ToString(); //启用状态
                    hfPageAttachment.Value = dtDeptInfo.Rows[0]["Attachment"] == null ? "" : dtDeptInfo.Rows[0]["Attachment"].ToString(); //启用状态
                    txtDuty.Text = dtDeptInfo.Rows[0]["Duty"] == null ? "" : dtDeptInfo.Rows[0]["Duty"].ToString(); //岗位职责
                    txtDutyRequire.Text = dtDeptInfo.Rows[0]["DutyRequire"] == null ? "" : dtDeptInfo.Rows[0]["DutyRequire"].ToString(); //任职资格

                    FCKeditor1.Value = dtDeptInfo.Rows[0]["QuterContent"] == null ? "" : dtDeptInfo.Rows[0]["QuterContent"].ToString(); //任职资格


                    DataTable dtSet = DeptQuarterBus.GetQuarterModelSet(txtDeptID.Value, txtDisplayCode.Value);




                    if (dtSet.Rows.Count > 0)
                    {



                        DataTable dtMUbIAO = GetNewDataTable(dtSet, "ModuleID='1001'", "TypeID asc");
                        if (dtMUbIAO.Rows.Count > 0)
                        {
                            chMMubiao.Checked = true;

                            for (int a = 0; a < dtMUbIAO.Rows.Count; a++)
                            {
                                string mubiao = dtMUbIAO.Rows[a]["TypeID"] == null ? "" : dtMUbIAO.Rows[a]["TypeID"].ToString();
                                if (mubiao == "1")
                                {
                                    chMRi.Checked = true;
                                }
                                else if (mubiao == "2")
                                {
                                    chMZhou.Checked = true;
                                }
                                else if (mubiao == "3")
                                {
                                    chMYue.Checked = true;
                                }
                                else if (mubiao == "4")
                                {
                                    chMJi.Checked = true;
                                }
                                else if (mubiao == "5")
                                {
                                    chMNian.Checked = true;
                                }
                            }
                        }
                        else
                        {
                            chMMubiao.Checked = false;
                            chMRi.Checked = false;
                            chMZhou.Checked = false;
                            chMYue.Checked = false;
                            chMJi.Checked = false;
                            chMNian.Checked = false;
                        }



                        DataTable dtRenwu = GetNewDataTable(dtSet, "ModuleID='1011'", "TypeID asc");
                        if (dtRenwu.Rows.Count > 0)
                        {
                            chRRenWu.Checked = true;
                            for (int a = 0; a < dtRenwu.Rows.Count; a++)
                            {
                                string mubiao = dtRenwu.Rows[a]["TypeID"] == null ? "" : dtRenwu.Rows[a]["TypeID"].ToString();
                                if (mubiao == "1")
                                {
                                    chRGEren.Checked = true;
                                }
                                else if (mubiao == "2")
                                {
                                    chRZhipai.Checked = true;
                                }

                            }
                        }
                        else
                        {
                            chRGEren.Checked = false;
                            chRRenWu.Checked = false;
                            chRZhipai.Checked = false;
                        }






                        DataTable dtrIZHI = GetNewDataTable(dtSet, "ModuleID='1021'", "TypeID asc");
                        if (dtrIZHI.Rows.Count > 0)
                        {
                            chGgongzuo.Checked = true;
                        }
                        else
                        {
                            chGgongzuo.Checked = false;
                        }
                        DataTable dtRICHENG = GetNewDataTable(dtSet, "ModuleID='10411'", "TypeID asc");
                        if (dtRICHENG.Rows.Count > 0)
                        {
                            chCricheng.Checked = true;
                        }
                        else
                        {
                            chCricheng.Checked = false;
                        }
                    }

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> readAfter(); </script> ");

                }
            }

        
        }
    }


    protected void GettEMPLATE(string ID)
    {
        DataTable DT = DeptQuarterBus.selectQuarterDescrible(ID);
        if (DT.Rows.Count > 0)
        {
            FCKeditor1.Value = DT.Rows[0]["QuterContent"].ToString();

            txtTmpleateName.Text = DT.Rows[0]["QuterName"].ToString();
            DataTable dtSet = DeptQuarterBus.selectQuarterSet(ID);




            if (dtSet.Rows.Count > 0)
            {



                DataTable dtMUbIAO = GetNewDataTable(dtSet, "ModuleID='1001'", "TypeID asc");
                if (dtMUbIAO.Rows.Count > 0)
                {
                    chMMubiao.Checked = true;

                    for (int a = 0; a < dtMUbIAO.Rows.Count; a++)
                    {
                        string mubiao = dtMUbIAO.Rows[a]["TypeID"] == null ? "" : dtMUbIAO.Rows[a]["TypeID"].ToString();
                        if (mubiao == "1")
                        {
                            chMRi.Checked = true;
                        }
                        else if (mubiao == "2")
                        {
                            chMZhou.Checked = true;
                        }
                        else if (mubiao == "3")
                        {
                            chMYue.Checked = true;
                        }
                        else if (mubiao == "4")
                        {
                            chMJi.Checked = true;
                        }
                        else if (mubiao == "5")
                        {
                            chMNian.Checked = true;
                        }
                    }
                }
                else
                {
                    chMMubiao.Checked = false;
                    chMRi.Checked = false;
                    chMZhou.Checked = false;
                    chMYue.Checked = false;
                    chMJi.Checked = false;
                    chMNian.Checked = false;
                }



                DataTable dtRenwu = GetNewDataTable(dtSet, "ModuleID='1011'", "TypeID asc");
                if (dtRenwu.Rows.Count > 0)
                {
                    chRRenWu.Checked = true;
                    for (int a = 0; a < dtRenwu.Rows.Count; a++)
                    {
                        string mubiao = dtRenwu.Rows[a]["TypeID"] == null ? "" : dtRenwu.Rows[a]["TypeID"].ToString();
                        if (mubiao == "1")
                        {
                            chRGEren.Checked = true;
                        }
                        else if (mubiao == "2")
                        {
                            chRZhipai.Checked = true;
                        }

                    }
                }
                else
                {
                    chRGEren.Checked = false;
                    chRRenWu.Checked = false;
                    chRZhipai.Checked = false;
                }






                DataTable dtrIZHI = GetNewDataTable(dtSet, "ModuleID='1021'", "TypeID asc");
                if (dtrIZHI.Rows.Count > 0)
                {
                    chGgongzuo.Checked = true;
                }
                else
                {
                    chGgongzuo.Checked = false;
                }
                DataTable dtRICHENG = GetNewDataTable(dtSet, "ModuleID='10411'", "TypeID asc");
                if (dtRICHENG.Rows.Count > 0)
                {
                    chCricheng.Checked = true;
                }
                else
                {
                    chCricheng.Checked = false;
                }
            }



        }
        else
        {
            FCKeditor1.Value = "";
        }
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> readAfter2(); </script> ");
    }
    private DataTable GetNewDataTable(DataTable dt, string condition, string Order)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition, Order);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }


    private void BindTemplateTree()
    { 
        DataTable dt = DeptQuarterBus.GetQuarterDescrible();
     
        
     
        //根节点
        TreeNode rootNode = new TreeNode();
        rootNode.Text = " <img  src=\"../../../Images/DeptMage.jpg\" border=\"0\"/> <span style=\"font-size:14px;height:28px\"> 职位说明书模板库 </span>";

      //  rootNode.Value = "0";
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow[] rows = dt.Select("1=1");
            if (rows.Length > 0)
            {
                TreeNode childNode = null;
                for (int i = 0; i < rows.Length; i++)
                {
                    childNode = new TreeNode();
                    childNode.Value = rows[i]["ID"].ToString();
                    childNode.Text = "<img  src=\"../../../Images/DeptMage.jpg\" border=\"0\"/><span style=\"font-size:12px;font-color:#006699;\"> " + rows[i]["QuterName"].ToString() + " </span>";
                    childNode.NavigateUrl = "DeptQueryInfo.aspx?ModuleID=2011104&&Sign=1&&QuarterID=" + rows[i]["ID"].ToString();
                    rootNode.ChildNodes.Add(childNode); 
                }
            }
        }
        trTemplate.Nodes.Add(rootNode);
    }


    #region 生成部门人员树
    private void BindTree()
    {
        DataTable dt = UserDeptSelectBus.GetDeptInfoByCompanyCD(ShowType, OprtType);
        DataTable Userdt = null;
        if (!string.IsNullOrEmpty(ShowType) && Convert.ToInt32(ShowType) > 1)
        {
            Userdt = DeptQuarterEmployeeBus.GetUserQuterInfo(ShowType, OprtType);
        }
        //<img  src=\"../../../Images/folder-closed.gif\"/>
        //<img  src=\"../../../Images/jsdoc.gif\"/>
        //根节点
        TreeNode rootNode = new TreeNode();
        rootNode.Text = " <img  src=\"../../../Images/DeptMage.jpg\" border=\"0\"/><span style=\"font-size:14px;height:25px\"> " + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyName + " </span>";

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
                    childNode.Text = "<img  src=\"../../../Images/DeptMage.jpg\" border=\"0\"/><span style=\"font-size:14px\"> " + rows[i]["DeptName"].ToString() + " </span>";
                    childNode.NavigateUrl = "javascript:void(0);";
                    if (!string.IsNullOrEmpty(ShowType) && Convert.ToInt32(ShowType) > 1)
                    {
                        TreeNode Usernode = null;
                        if (Userdt != null && Userdt.Rows.Count > 0)
                        {
                            string UserExprssion = "DeptID='" + childNode.Value + "' and SuperQuarterID is null";
                            DataRow[] UserRow = Userdt.Select(UserExprssion);
                            if (UserRow.Length > 0)
                            {
                                for (int j = 0; j < UserRow.Length; j++)
                                {

                                    Usernode = new TreeNode();
                                    Usernode.Value = UserRow[j]["DeptID"].ToString();
      Usernode.Text = "<img  src=\"../../../Images/QuterMage.jpg\" border=\"0\" /><span style=\"font-size:12px;font-color:#006699;\"> " + UserRow[j]["QuarterName"].ToString()   + " </span>";
                                    //Usernode.NavigateUrl = string.Format("javascript:javascript:void(0)");
      Usernode.NavigateUrl = "DeptQueryInfo.aspx?ModuleID=2011104&&Sign=2&&QuarterID=" + UserRow[j]["QuarterID"].ToString();

                                    
                                    childNode.ChildNodes.Add(Usernode);
                                    string SuperQuarterID = UserRow[j]["QuarterID"].ToString();
                                    BindChildParend(SuperQuarterID, Usernode, UserRow[j]["DeptID"].ToString(), Userdt);
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
                    node.Text = "  <img  src=\"../../../Images/DeptMage.jpg\" border=\"0\" /><span style=\"font-size:14px\"> " + rows[i]["DeptName"].ToString() + " </span>";
                    ///rows[i]["DeptName"].ToString();
                    node.NavigateUrl = string.Format("javascript:javascript:void(0)");
                    if (!string.IsNullOrEmpty(ShowType) && Convert.ToInt32(ShowType) > 1)
                    {
                        if (Userdt != null && Userdt.Rows.Count > 0)
                        {
                            Usernode = new TreeNode();
                            string UserExprssion = "DeptID='" + node.Value + "' and SuperQuarterID is null";
                            DataRow[] UserRow = Userdt.Select(UserExprssion);
                            if (UserRow.Length > 0)
                            {
                                for (int j = 0; j < UserRow.Length; j++)
                                {
                                    Usernode = new TreeNode();
                                    Usernode.Value = UserRow[j]["DeptID"].ToString();
  Usernode.Text = "<img  src=\"../../../Images/QuterMage.jpg\" border=\"0\" /><span style=\"font-size:12px;font-color:#006699;\"> " + UserRow[j]["QuarterName"].ToString()  + " </span>";
                                    //Usernode.NavigateUrl = string.Format("javascript:javascript:void(0)");
  Usernode.NavigateUrl = "DeptQueryInfo.aspx?ModuleID=2011104&&Sign=2&&QuarterID=" + UserRow[j]["QuarterID"].ToString();
                                    node.ChildNodes.Add(Usernode);

                                    //string SuperQuarterID=UserRow[j]["QuarterID"].ToString();
                                    //BindChildParend(SuperQuarterID, Usernode, UserRow[j]["DeptID"].ToString(), Userdt);





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



    private void BindChildParend(string SuperDeptID, TreeNode treenode, string DeptID, DataTable Userdt)
    {
        if (Userdt != null && Userdt.Rows.Count > 0)
        {
            string Expression = "SuperQuarterID='" + SuperDeptID + "'";
            DataRow[] rows = Userdt.Select(Expression);
            TreeNode node = null;
            TreeNode Usernode = null;
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    node = new TreeNode();
                    node.Value = DeptID;
                    node.Text = "  <img  src=\"../../../Images/QuterMage.jpg\" border=\"0\" /><span style=\"font-size:14px\"> " + rows[i]["QuarterName"].ToString() + " </span>";
                    ///rows[i]["DeptName"].ToString();
                   // node.NavigateUrl = string.Format("javascript:javascript:void(0)");
                    node.NavigateUrl = "DeptQueryInfo.aspx?ModuleID=2011104&&sign=2&&QuarterID=" + rows[i]["QuarterID"].ToString();
                    if (!string.IsNullOrEmpty(ShowType) && Convert.ToInt32(ShowType) > 1)
                    {
                        if (Userdt != null && Userdt.Rows.Count > 0)
                        {
                            Usernode = new TreeNode();
                            string UserExprssion = "SuperQuarterID='" + rows[i]["QuarterID"].ToString() + "'  ";
                            DataRow[] UserRow = Userdt.Select(UserExprssion);
                            if (UserRow.Length > 0)
                            {
                                for (int j = 0; j < UserRow.Length; j++)
                                {
                                    Usernode = new TreeNode();
                                    Usernode.Value = DeptID;
                                    Usernode.Text = "<img  src=\"../../../Images/QuterMage.jpg\" border=\"0\" /><span style=\"font-size:12px;font-color:#006699;\"> " + UserRow[j]["QuarterName"].ToString() + " </span>";
                                   // Usernode.NavigateUrl = string.Format("javascript:javascript:void(0)");
                                    Usernode.NavigateUrl = "DeptQueryInfo.aspx?ModuleID=2011104&&Sign=2&&QuarterID=" + UserRow[j]["QuarterID"].ToString();
                                    node.ChildNodes.Add(Usernode);




                                    string SuperQuarterID = UserRow[j]["QuarterID"].ToString();
                                    BindChildParendst(SuperQuarterID, Usernode, UserRow[j]["DeptID"].ToString(), Userdt);



                                }
                            }
                        }
                    }
                    treenode.ChildNodes.Add(node);
                     
                }
            }
        }
    }


    private void BindChildParendst(string SuperDeptID, TreeNode treenode, string DeptID, DataTable Userdt)
    {
        if (Userdt != null && Userdt.Rows.Count > 0)
        {
            string Expression = "SuperQuarterID='" + SuperDeptID + "'";
            DataRow[] rows = Userdt.Select(Expression);
            TreeNode node = null;
            TreeNode Usernode = null;
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    node = new TreeNode();
                    node.Value = DeptID;
                    node.Text = "  <img  src=\"../../../Images/QuterMage.jpg\" border=\"0\" /><span style=\"font-size:14px\"> " + rows[i]["QuarterName"].ToString() + " </span>";
                    ///rows[i]["DeptName"].ToString();
                    //node.NavigateUrl = string.Format("javascript:javascript:void(0)");
                    node.NavigateUrl = "DeptQueryInfo.aspx?ModuleID=2011104&&sign=2&&QuarterID=" + rows[i]["QuarterID"].ToString();
                    if (!string.IsNullOrEmpty(ShowType) && Convert.ToInt32(ShowType) > 1)
                    {
                        if (Userdt != null && Userdt.Rows.Count > 0)
                        {
                            Usernode = new TreeNode();
                            string UserExprssion = "SuperQuarterID='" + rows[i]["QuarterID"].ToString() + "'  ";
                            DataRow[] UserRow = Userdt.Select(UserExprssion);
                            if (UserRow.Length > 0)
                            {
                                for (int j = 0; j < UserRow.Length; j++)
                                {
                                    Usernode = new TreeNode();
                                    Usernode.Value = DeptID;
                                    Usernode.Text = "<img  src=\"../../../Images/QuterMage.jpg\" border=\"0\" /><span style=\"font-size:12px;font-color:#006699;\"> " + UserRow[j]["QuarterName"].ToString() + " </span>";
                                    //Usernode.NavigateUrl = string.Format("javascript:javascript:void(0)");
                                    Usernode.NavigateUrl = "DeptQueryInfo.aspx?ModuleID=2011104&&Sign=2&&QuarterID=" + UserRow[j]["QuarterID"].ToString();
                                    node.ChildNodes.Add(Usernode);








                                }
                            }
                        }
                    }
                    treenode.ChildNodes.Add(node);

                }
            }
        }
    }
}
