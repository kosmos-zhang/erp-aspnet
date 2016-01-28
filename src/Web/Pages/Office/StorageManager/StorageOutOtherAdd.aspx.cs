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

public partial class Pages_Office_StorageManager_StorageOutOtherAdd : BasePage
{
    private string BigType = "";//类型
    private string companyCD = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenNow.Value = System.DateTime.Now.ToShortDateString();
        HiddenIsZero.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsZero.ToString();//是否允许价格为零
        string OutNoID = Request.QueryString["OutNoID"];
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
        UserTransactor.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        txtTransactorID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        txtOutDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
            IsDisplayPrice.Value = "true";
        else
            IsDisplayPrice.Value = "false";
        btnGetGoods.Visible = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode;
        if (!IsPostBack)
        {
            BindTree();//这个是绑定数据源
            ddlFromType.Attributes.Add("onchange", "DoChange();");
            DataTable dt = CodeReasonTypeBus.GetReasonType(companyCD);
            if (dt.Rows.Count > 0)
            {
                ddlReason.DataSource = dt;
                ddlReason.DataTextField = "CodeName";
                ddlReason.DataValueField = "ID";
                ddlReason.DataBind();
            }
            ddlReason.Items.Insert(0, new ListItem("--请选择--", ""));

            txtOutNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
            txtOutNo.ItemTypeID = ConstUtil.CODING_RULE_StorageOut_NO;
            if (OutNoID != "" && OutNoID != null)
            {
                txtIndentityID.Value = OutNoID;//给隐藏域赋主键
            }
            else
            {
                this.txtCreator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();//制单人
                this.txtCreateDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

                this.txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();//最后更新人
                this.txtModifiedDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//新建的时候给最后更新时间日期赋值
            }
            if (OutNoID != "" && OutNoID != null)
            {
                this.div_OutNo_uc.Attributes.Add("style", "display:none;");
                this.div_OutNo_Lable.Attributes.Add("style", "display:block;");
            }
            else
            {
                this.div_OutNo_uc.Attributes.Add("style", "display:block;");
                this.div_OutNo_Lable.Attributes.Add("style", "display:none;");
            }
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
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEOUTOTHER_LIST;
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
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_STORAGE_STORAGEOUTOTHER_LIST, string.Empty);
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
