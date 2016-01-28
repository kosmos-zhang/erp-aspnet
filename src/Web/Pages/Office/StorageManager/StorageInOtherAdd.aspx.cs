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
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Common;

public partial class Pages_Office_StorageManager_StorageInOtherAdd : BasePage
{
    private string BigType = "";//类型
    private string companyCD = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string InNoID = Request.QueryString["InNoID"];
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        //明细单价是否允许为0
        hidZero.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsZero.ToString();

        //是否启用批次
        BatchRuleControl1.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
        BatchRuleControl1.ItemTypeID = ConstUtil.CODING_RULE_BATCH_NO;


        ////是否启用单位组
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        {
            hidMoreUnit.Value = "true";
            tdDanWei.Attributes.Add("style", "display:block;");
            tdDanWei.Attributes.Add("style", "width: 8%");
            tdShuLiang.Attributes.Add("style", "display:block;");
            tdShuLiang.Attributes.Add("style", "width: 8%");
            divSpan.Attributes.Add("style", "display:block;");
            divRedSpan.Attributes.Add("style", "display:none;");
            divJiBendw.Attributes.Add("style", "display:block;");
            divDanWei.Attributes.Add("style", "display:none;");
        }
        else
        {
            hidMoreUnit.Value = "false";
            tdDanWei.Attributes.Add("style", "display:none;");
            tdShuLiang.Attributes.Add("style", "display:none;");
            divSpan.Attributes.Add("style", "display:none;");
            divRedSpan.Attributes.Add("style", "display:block;");
            divJiBendw.Attributes.Add("style", "display:none;");
            divDanWei.Attributes.Add("style", "display:block;");
        }

        //启用小数位数,默认2位
        hidSelPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;

        if (!IsPostBack)
        {
            BindTree();//这个是绑定数据源

            ddlFromType.Attributes.Add("onchange", "DoChange();");

            txtInNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
            txtInNo.ItemTypeID = ConstUtil.CODING_RULE_StorageIn_NO;
            if (InNoID != "" && InNoID != null)
            {
                txtIndentityID.Value = InNoID;//给隐藏域赋主键
            }
            else
            {
                this.txtCreator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();//制单人
                this.txtCreateDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

                this.txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();//最后更新人
                this.txtModifiedDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//新建的时候给最后更新时间日期赋值
                UserExecutor.Value = UserInfo.EmployeeName;
                txtExecutorID.Value = UserInfo.EmployeeID.ToString();
                txtEnterDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            //控制条码扫描
            if (!((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode)
            {
                btnGetGoods.Visible = false;
            }
            if (InNoID != "" && InNoID != null)
            {
                this.div_InNo_uc.Attributes.Add("style", "display:none;");
                this.div_InNo_Lable.Attributes.Add("style", "display:block;");
            }
            else
            {
                this.div_InNo_uc.Attributes.Add("style", "display:block;");
                this.div_InNo_Lable.Attributes.Add("style", "display:none;");
            }
            DataTable dt = CodeReasonTypeBus.GetReasonType(companyCD);
            if (dt.Rows.Count > 0)
            {
                ddlReasonType.DataSource = dt;
                ddlReasonType.DataTextField = "CodeName";
                ddlReasonType.DataValueField = "ID";
                ddlReasonType.DataBind();
            }
            ddlReasonType.Items.Insert(0, new ListItem("--请选择--", ""));
            StorageModel model = new StorageModel();
            model.CompanyCD = companyCD;
            model.UsedStatus = "1";
            DataTable dt1 = StorageBus.GetStorageListBycondition(model);
            if (dt1.Rows.Count > 0)
            {
                ddlStorageInfo.DataSource = dt1;
                ddlStorageInfo.DataTextField = "StorageName";
                ddlStorageInfo.DataValueField = "ID";
                ddlStorageInfo.DataBind();
            }

            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEINOTHER_LIST;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见
                btnBack.Visible = true;
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //去除参数
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_STORAGE_STORAGEINOTHER_LIST, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }
        }
    }

    #region 绑定往来单位
    private void BindTree()
    {
        DataTable dt_Company = StorageInOtherBus.GetCodeCompany();
        DataView dataView = dt_Company.DefaultView;
        dataView.RowFilter = "BigType='1'";
        DataTable dtnew = new DataTable();
        dtnew = dataView.ToTable();
        TreeNode tempNode = new TreeNode("客户");
        tempNode.NavigateUrl = string.Format("javascript:javascript:void(0)");
        this.TreeView1.Nodes.Add(tempNode);
        BindTreeChildNodes(tempNode, dtnew);
        tempNode.Expanded = false;

        dataView.RowFilter = "BigType='2'";
        DataTable dtnew1 = new DataTable();
        dtnew1 = dataView.ToTable();
        TreeNode tempNode2 = new TreeNode("供应商");
        tempNode2.NavigateUrl = string.Format("javascript:javascript:void(0)");
        this.TreeView1.Nodes.Add(tempNode2);
        BindTreeChildNodes(tempNode2, dtnew1);
        tempNode.Expanded = false;

        dataView.RowFilter = "BigType='5'";
        DataTable dtnew2 = new DataTable();
        dtnew2 = dataView.ToTable();
        TreeNode tempNode3 = new TreeNode("外协加工厂");
        tempNode3.NavigateUrl = string.Format("javascript:javascript:void(0)");
        this.TreeView1.Nodes.Add(tempNode3);
        BindTreeChildNodes(tempNode3, dtnew2);
        tempNode.Expanded = false;
    }

    private void BindTreeChildNodes(TreeNode node, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=0"))
        {
            TreeNode nodes = new TreeNode();
            nodes.Text = row["CodeName"].ToString();
            nodes.Value = row["ID"].ToString();
            BigType = row["BigType"].ToString();
            nodes.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}','{2}');", nodes.Text, nodes.Value, BigType);

            LoadSubData(row["ID"].ToString(), nodes, dt);
            node.ChildNodes.Add(nodes);
            node.Expanded = false;
        }
    }
    private void LoadSubData(string pid, TreeNode nodes, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=" + pid))
        {
            TreeNode nodess = new TreeNode();
            nodess.Text = row["CodeName"].ToString();
            nodess.Value = row["ID"].ToString();
            BigType = row["BigType"].ToString();
            nodess.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}','{2}');", nodess.Text, nodess.Value, BigType);
            LoadSubData(row["ID"].ToString(), nodess, dt);
            nodes.ChildNodes.Add(nodess);
            nodes.Expanded = false;
        }
    }
    #endregion
}
