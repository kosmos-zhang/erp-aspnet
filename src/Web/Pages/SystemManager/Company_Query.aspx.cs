using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBase.Business.SystemManager;
public partial class Pages_SystemManager_Company_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string CompanyCD = Request.QueryString["CompanyCD"];
            if (CompanyCD != null)
            {
                InitPage(false, " CompanyCD= '" + CompanyCD + "'");
            }
           // InitPage(false, "");
        }
    }

    private void InitPage(bool isPostBack, string where)
    {
        //if (!isPostBack && string.IsNullOrEmpty(where))
        //{
        //    PageList.Visible = false;
        //    return;
        //}
        //DataTable dt = null;
        //if (string.IsNullOrEmpty(where))
        //{
        //    dt = CompanyBus.GetCompanyInfo();
        //}
        //if (!isPostBack)
        //{
        //    dt = CompanyBus.GetCompanyByCD(where);
        //}
        ////else
        ////{
 
        ////}
        //string[] width = { "150", "100", "100", "200" };
        //string[,] title = { { "企业名称", "CompanyNameCn" }, { "所属省份", "ProvName" },
        //              { "所属城市", "CityName" }, { "行业类型", "TradeCD" } };
       
        //string[] columnName = { "", "", "", "" };
        //PageList.CheckBox = true;
        //PageList.IsSort = true;
        //PageList.TableTitle = title;
        //PageList.TableData = dt;
        //PageList.DisplayColumnName = columnName;
        //PageList.Visible = true;
        //PageList.GetData();
    }

    #region 查询
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string StrWhere = "";
        if (txtCompanyCD.Text.Trim() != "")
        {
            StrWhere += " CompanyCD='" + txtCompanyCD.Text.Trim() + "' and ";
        }
        if (txtCompanyName.Text.Trim() != "")
        {
            StrWhere += " CompanyNameCn='" + txtCompanyName.Text.Trim() + "' and ";
        }
        if (!string.IsNullOrEmpty(StrWhere))
        {
            StrWhere += "1=1";
        }
        InitPage(true,StrWhere);
    }
    #endregion

    #region 删除
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string Companycd = "";
            Companycd= Request.Form["hidDelete"].ToString();
            Companycd = Companycd.Remove(Companycd.Length-1);
            CompanyBus.DelCompany(Companycd);
            InitPage(true,"");
        }
        catch
        {
            lblMessage.Text = "删除失败，请联系管理员。";
        }
    }
    #endregion
}
