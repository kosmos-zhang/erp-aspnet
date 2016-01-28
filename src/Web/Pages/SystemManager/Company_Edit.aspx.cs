using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.SystemManager;
using XBase.Common;
using XBase.Model.SystemManager;
public partial class Pages_SystemManager_Company_Edit : System.Web.UI.Page
{
    public string Action
    {
        get { return ViewState["ActionOprt"].ToString(); }
        set { ViewState["ActionOprt"] = value; }
    }
    public string CompanyCD
    {
        get { return ViewState["CompanyCD"].ToString(); }
        set { ViewState["CompanyCD"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDrpList();
            if (Request.QueryString["Action"] != null)
            {
                Action = Request.QueryString["Action"].ToString();
            }
            if (Request.QueryString["CompanyCD"] != null)
            {
                CompanyCD = Request.QueryString["CompanyCD"].ToString();
            }
            if (Action == ActionUtil.Edit.ToString())
            {
                txtCompanyCD.Enabled = false;
                LoadCompanyInfo(CompanyCD);
            }
        }
    }

    #region BingingProvince
    private void BindDrpList()
     {
        DataTable dt = ProvinceBus.GetProvinceInfo();
        if (dt != null && dt.Rows.Count > 0)
        {
            SetPro.Items.Clear();
            foreach (DataRow Row in dt.Rows)
            {
                ListItem Province = new ListItem();
                Province.Text = Row["ProvName"].ToString();
                Province.Value = Row["ProvCD"].ToString();
                SetPro.Items.Add(Province);
            }
            ListItem select = new ListItem();
            select.Text = "请选择";
            select.Value = "0";
            SetPro.Items.Insert(0, select);
        }
    }
    #endregion


    #region 获取省下所有城市
    private void BindCity(string ProCode)
    {
        if (string.IsNullOrEmpty(ProCode)) return;
        string ProvinceCode = SetPro.Value;
        if (!string.IsNullOrEmpty(ProvinceCode))
        {
            DataTable dt = CityBus.GetCityByCode(ProvinceCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                SetCity.Items.Clear();
                foreach (DataRow Row in dt.Rows)
                {
                    ListItem CityItem = new ListItem();
                    CityItem.Text = Row["CityName"].ToString();
                    CityItem.Value = Row["CityCD"].ToString();
                    this.SetCity.Items.Add(CityItem);
                }
                SetCity.Items.Insert(0, "请选择");
            }
        }
    }
    #endregion

    #region 初始化页面信息
    private void LoadCompanyInfo(string CompanyCD)
    {
        if (string.IsNullOrEmpty(CompanyCD)) return;
        try
        {
            string StrWhere = " CompanyCD='" + CompanyCD + "'";
            DataTable dt = CompanyBus.GetCompanyByCD(StrWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow Row in dt.Rows)
                {
                    this.txtCompanyCD.Text =Row["CompanyCD"]!=DBNull.Value?Row["CompanyCD"].ToString():"";
                    this.txtCompanyCn.Text = Row["CompanyNameCn"] != DBNull.Value ? Row["CompanyNameCn"].ToString() : "";
                    this.txtCompanyEn.Text = Row["CompanyNameEn"] != DBNull.Value ? Row["CompanyNameEn"].ToString() : "";
                    this.txtTel.Text = Row["Tel"] != DBNull.Value ? Row["Tel"].ToString() : "";
                    this.txtFax.Text = Row["Fax"] != DBNull.Value ? Row["Fax"].ToString(): "";
                    this.txtPost.Text = Row["Post"] != DBNull.Value ? Row["Post"].ToString(): "";
                    this.txtHomePage.Text = Row["HomePage"] != DBNull.Value ? Row["HomePage"].ToString() : "";
                    this.txtMail.Text = Row["Email"] != DBNull.Value ? Row["Email"].ToString() : "";
                    this.txtQQ.Text = Row["QQ"] != DBNull.Value ? Row["QQ"].ToString() : "";
                    this.txtMSN.Text = Row["MSN"] != DBNull.Value ? Row["MSN"].ToString() : "";
                    this.txtIM.Text = Row["IM"] != DBNull.Value ? Row["IM"].ToString() : "";
                    this.txtContact.Text=Row["Contact"]!=DBNull.Value?Row["Contact"].ToString():"";
                    this.txtAddress.Text = Row["Addr"] != DBNull.Value ? Row["Addr"].ToString() : "";
                    this.txtTradeCD.Text = Row["TradeCD"] != DBNull.Value ? Row["TradeCD"].ToString() : "";
                    this.txtStaff.Text = Row["Staff"] != DBNull.Value ? Row["Staff"].ToString() : "";
                    this.DrpSize.SelectedValue = Row["Size"] != DBNull.Value ? Row["Size"].ToString() : "";
                    this.txtProduction.Text = Row["Production"] != DBNull.Value ? Row["Production"].ToString() : "";
                    this.txtSale.Text = Row["Sale"] != DBNull.Value ? Row["Sale"].ToString():"";
                    this.txtCredit.Text = Row["Credit"] != DBNull.Value ? Row["Credit"].ToString() : "";
                    this.txtRemark.Text = Row["Remark"] != DBNull.Value ? Row["Remark"].ToString() : "";
                    //New add
                    this.SetPro.Value = Row["ProvCD"] != DBNull.Value ? Row["ProvCD"].ToString() : "";
                    if (Row["ProvCD"] != DBNull.Value)
                    {
                        BindCity(Row["ProvCD"].ToString());
                    }
                    this.SetCity.Value = Row["CityCD"] != DBNull.Value ? Row["CityCD"].ToString() : "";
                    HideAction.Value = Row["CityCD"].ToString();
                }
            }
        }
        catch
        {
            lblMessage.Text = "数据初始化失败，请联系管理员！";
        }
    }
    #endregion

    #region 保存信息
    protected void btnModify_Click(object sender, ImageClickEventArgs e)
    {
      
        CompanyModel Model = new CompanyModel();
        Model.CityCD = HideAction.Value;


        Model.CompanyCD = this.txtCompanyCD.Text.Trim();
        Model.CompanyNameCn = this.txtCompanyCn.Text.Trim();
        Model.CompanyNameEn = this.txtCompanyEn.Text.Trim();
        Model.ProvCD = this.SetPro.Value;
      
        Model.Addr = this.txtAddress.Text.Trim();
        Model.Contact = this.txtContact.Text.Trim();
        Model.Tel = this.txtTel.Text.Trim();
        Model.Fax = this.txtFax.Text.Trim();
        Model.Post = this.txtPost.Text.Trim();
        Model.HomePage = this.txtHomePage.Text.Trim();
        Model.Email = this.txtMail.Text.Trim();
        Model.QQ = this.txtQQ.Text.Trim();
        Model.MSN = this.txtMSN.Text.Trim();
        Model.IM = this.txtIM.Text.Trim();
        if (this.txtTradeCD.Text != "")
        {
            Model.TradeCD = Convert.ToInt32(this.txtTradeCD.Text);
        }
        if (this.txtStaff.Text != "")
        {
            Model.Staff = Convert.ToInt32(this.txtStaff.Text.Trim());
        }
        Model.Size = DrpSize.SelectedValue;
        Model.Production = this.txtProduction.Text.Trim();
        if (this.txtSale.Text != "")
        {
            Model.Sale = Convert.ToDecimal(this.txtSale.Text);
        }
        Model.Credit =this.txtCredit.Text.Trim();
        Model.Remark = this.txtRemark.Text.Trim();
        Model.ModifiedUserID = "admin";
        try
        {
            if (Action == ActionUtil.Add.ToString())
            {
                CompanyBus.AddCompany(Model);
                string url = "Company_Query.aspx?CompanyCD=" + Model.CompanyCD;
                Response.Redirect(url);
                //Response.Write("<script language='javascript'>window.location='Company_Query.aspx'</script>");
            }
            else if (Action == ActionUtil.Edit.ToString())
            {
                bool reult=CompanyBus.ModifyCompany(Model);

                string url = "Company_Query.aspx?CompanyCD=" + Model.CompanyCD;
                Response.Redirect(url);
              //  Response.Write("<script language='javascript'>window.location='Company_Query.aspx?CompanyCD='"++"''</script>");
            }
        }
        catch
        {
            lblMessage.Text = "数据操作失败，请联系管理员！";
        }
    }
    #endregion

    //#region 返回查询页面
    //protected void btnBack_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Write("<script language='javascript'>window.location='Company_Query.aspx'</script>");
    //}
    //#endregion
}
