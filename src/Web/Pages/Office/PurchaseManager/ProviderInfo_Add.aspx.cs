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
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;

public partial class Pages_Office_PurchaseManager_ProviderInfo_Add : BasePage
{
    #region Master Arrive ID
    public int intMasterProviderID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intMasterProviderID"], out tempID);
            return tempID;
        }
    }
    #endregion

    string CompanyCD = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        this.txtIndentityID.Value = this.intMasterProviderID.ToString();
        if (!Page.IsPostBack)
        {
            hidModuleID.Value = ConstUtil.MODULE_ID_PROVIDERINFOINFO;
            UserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            UserName.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserName;
            SystemTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserIDReal.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            txtModifiedUserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();

            BinddrpCustType();//绑定采购供应商类别
            BinddrpCreditGrade();//绑定采购供应商优质级别
            BinddrpAreaID();//绑定采购供应商所在区域
            BinddrpLinkCycle();//绑定采购供应商联络期限
            BinddrpCountryID();//绑定国家
            BinddrpTakeType();//绑定交货方式
            BinddrpCarryType();//绑定运送方式
            BinddrpPayType();//绑定结算方式
            binddrpCurrencyType();//绑定币种

            HidCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            #region 采购供应商编号规则
            CodingRuleControl1.CodingType = ConstUtil.CODING_RULE_PURCHASE;
            CodingRuleControl1.ItemTypeID = ConstUtil.CODING_RULE_PURCHASE_PROVIDERINFO;
            //CodingRuleControl1.TableName = "ProviderInfo";
            //CodingRuleControl1.ColumnName = "CustNo";
            UserInfoUtil userInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            usernametemp.Value = userInfo.UserName;
            txtCreatorReal.Value = userInfo.EmployeeName;
            datetemp.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            txtModifiedUserIDReal.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            txtModifiedDate2.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID2.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            #endregion

            BindTreeChildNodes();//供应商分类
        }
    }

    #region 绑定采购供应商类别
    private void BinddrpCustType()
    {
        DataTable dt = ProviderInfoBus.GetdrpCustType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpCustType.DataSource = dt;
            drpCustType.DataTextField = "TypeName";
            drpCustType.DataValueField = "ID";
            drpCustType.DataBind();
        }
    }
    #endregion

    #region 绑定采购供应商优质级别
    private void BinddrpCreditGrade()
    {
        DataTable dt = ProviderInfoBus.GetdrpCreditGrade();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpCreditGrade.DataSource = dt;
            drpCreditGrade.DataTextField = "TypeName";
            drpCreditGrade.DataValueField = "ID";
            drpCreditGrade.DataBind();
            //ListItem Item = new ListItem("--请选择--", "");
            //drpCreditGrade.Items.Insert(0, Item);
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpCreditGrade.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定采购供应商所在区域
    private void BinddrpAreaID()
    {
        DataTable dt = ProviderInfoBus.GetdrpAreaID();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpAreaID.DataSource = dt;
            drpAreaID.DataTextField = "TypeName";
            drpAreaID.DataValueField = "ID";
            drpAreaID.DataBind();
            //ListItem Item = new ListItem("--请选择--", "");
            //drpAreaID.Items.Insert(0, Item);
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpAreaID.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定采购供应商联络期限
    private void BinddrpLinkCycle()
    {
        DataTable dt = ProviderInfoBus.GetdrpLinkCycle();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpLinkCycle.DataSource = dt;
            drpLinkCycle.DataTextField = "TypeName";
            drpLinkCycle.DataValueField = "ID";
            drpLinkCycle.DataBind();
        }
    }
    #endregion

    #region 绑定国家
    private void BinddrpCountryID()
    {
        DataTable dt = ProviderInfoBus.GetdrpCountryID();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpCountryID.DataSource = dt;
            drpCountryID.DataTextField = "TypeName";
            drpCountryID.DataValueField = "ID";
            drpCountryID.DataBind();
            //ListItem Item = new ListItem("--请选择--", "");
            //drpCountryID.Items.Insert(0, Item);
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpCountryID.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定列表交货方式
    private void BinddrpTakeType()
    {
        DataTable dt = ProviderInfoBus.GetDrpTakeType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpTakeType.DataSource = dt;
            drpTakeType.DataTextField = "TypeName";
            drpTakeType.DataValueField = "ID";
            drpTakeType.DataBind();
            //ListItem Item = new ListItem("--请选择--", "");
            //drpTakeType.Items.Insert(0, Item);
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpTakeType.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定列表运送方式
    private void BinddrpCarryType()
    {
        DataTable dt = ProviderInfoBus.GetDrpCarryType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpCarryType.DataSource = dt;
            drpCarryType.DataTextField = "TypeName";
            drpCarryType.DataValueField = "ID";
            drpCarryType.DataBind();
            //ListItem Item = new ListItem("--请选择--", "");
            //drpCarryType.Items.Insert(0, Item);
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpCarryType.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定列表结算方式
    private void BinddrpPayType()
    {
        DataTable dt = ProviderInfoBus.GetDrpPayType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpPayType.DataSource = dt;
            drpPayType.DataTextField = "TypeName";
            drpPayType.DataValueField = "ID";
            drpPayType.DataBind();
            //ListItem Item = new ListItem("--请选择--", "");
            //drpPayType.Items.Insert(0, Item);
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpPayType.Items.Insert(0, Item);
    }
    #endregion
    
    #region 绑定币种
    private void binddrpCurrencyType()
    {
        DataTable dt = ProviderInfoBus.GetdrpCurrencyType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpCurrencyType.DataSource = dt;
            drpCurrencyType.DataTextField = "CurrencyName";
            drpCurrencyType.DataValueField = "ID";
            drpCurrencyType.DataBind();
            //ListItem Item = new ListItem("--请选择--", "");
            //drpCurrencyType.Items.Insert(0, Item);
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpCurrencyType.Items.Insert(0, Item);
    }
    #endregion



    #region 绑定供应商分类
    private void BindTreeChildNodes()
    {
        DataTable dt_product = ProviderInfoBus.GetProviderClass();
        foreach (DataRow row in dt_product.Select("SupperID=0"))
        {
            TreeNode nodes = new TreeNode();
            nodes.Text = row["CodeName"].ToString();
            nodes.Value = row["ID"].ToString();
            nodes.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}');", nodes.Text, nodes.Value);

            LoadSubData(row["ID"].ToString(), nodes, dt_product);
            //node.ChildNodes.Add(nodes);
            this.TreeView1.Nodes.Add(nodes);
            nodes.Expanded = true;
        }
    }
    private void LoadSubData(string pid, TreeNode nodes, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=" + pid))
        {
            TreeNode nodess = new TreeNode();
            nodess.Text = row["CodeName"].ToString();
            nodess.Value = row["ID"].ToString();
            nodess.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}');", nodess.Text, nodess.Value);
            LoadSubData(row["ID"].ToString(), nodess, dt);
            nodes.ChildNodes.Add(nodess);
            nodes.Expanded = false;
        }
    }
    #endregion
    

}
