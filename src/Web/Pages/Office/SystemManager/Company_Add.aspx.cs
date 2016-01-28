using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using XBase.Common;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;
using System.Web.UI.WebControls;
public partial class Pages_Office_SystemManager_Company_Add : System.Web.UI.Page
{
    private string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDrpList();
            txtCompanyCD.Text = companyCD;
            txtCompanyCD.Enabled = false;
        }
    }
     protected void btnModify_Click(object sender, EventArgs e)
    {
        CompanyBaseInfoModel Model = new CompanyBaseInfoModel();
        Model.CompanyCD = companyCD;
        Model.CompanyNo = this.txtCompanyNo.Text.Trim();
        Model.CompanyName = this.txtCompanyName.Text.Trim();
        Model.Description = this.txtDescription.Text.Trim();
        Model.UsedStatus = chkUsingFlag.Checked ? "1" : "0";
        if (SuperCompany.Value=="0")
        {
            Model.SuperCompanyID =0;
        }
        else
        {
            Model.SuperCompanyID =Convert .ToInt32 ( SuperCompany.Value);
        }
        if (CompanyBaseInfoBus.InsertCompanyBaseInfo(Model))
        {
            Response.Write("添加成功");
        }
      
    }
     #region BingingSuperCompany
     private void BindDrpList()
     {
       DataTable dt_companybaseinfo = CompanyBaseInfoBus.GetCompanyModuleInfo(companyCD);
       if (dt_companybaseinfo != null && dt_companybaseinfo.Rows.Count > 0)
         {
             SuperCompany.Items.Clear();
             foreach (DataRow Row in dt_companybaseinfo.Rows)
             {
                 ListItem Province = new ListItem();
                 Province.Text = Row["CompanyName"].ToString();
                 Province.Value = Row["ID"].ToString();
                 SuperCompany.Items.Add(Province);
             }
             ListItem select = new ListItem();
             select.Text = "请选择";
             select.Value = "0";
             SuperCompany.Items.Insert(0, select);
         }
     }
     #endregion
}
