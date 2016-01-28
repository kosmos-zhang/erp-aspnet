using System;
using System.Collections;
using System.Data;
using System.Web;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;
using XBase.Common;
public partial class Pages_Office_SystemManager_Company_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //获取参数公司编码
            string CompanyNo = Request.QueryString["CompanyNo"];
            //页面初期设置
            InitPage(CompanyNo);

        }
       
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    /// <param name="CompanyNo"></param>
    private void InitPage(string CompanyNo)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        txtCompanyCD.Text=companyCD;
        if (string.IsNullOrEmpty(CompanyNo))
        {
            return;
        }
        //修改企业信息
        //获取企业信息
        CompanyBaseInfoModel CompanyBaseInfoModel = new CompanyBaseInfoModel();
        CompanyBaseInfoModel model=CompanyBaseInfoBus.GetCompanyUnitInfo(companyCD,CompanyNo);
        txtCompanyNo.Text=model .CompanyNo;
        txtCompanyName.Text=model .CompanyName;
        txtDescription.Text=model.Description;
    }
}
