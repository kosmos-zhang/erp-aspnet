/**********************************************
 * 类作用：   客户公司开通业务
 * 建立人：   吴志强
 * 建立时间： 2009/01/21
 ***********************************************/

using System;
using XBase.Model.SystemManager;
using XBase.Business.SystemManager;
using XBase.Common;

public partial class Pages_SystemManager_CompanyOpenServ_Modify : System.Web.UI.Page
{
    /// <summary>
    /// 类名：CompanyOpenServ_Modify
    /// 描述：公司业务开通处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/21
    /// 最后修改时间：2009/01/21
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = string.Empty;
        //第一次初期化页面
        if (!IsPostBack)
        {
            //获取参数公司代码
            string companyCD = Request.QueryString["CompanyCD"];
            //页面初期设置
            InitPage(companyCD);
        }
    }

    #region 初期化页面
    /// <summary>
    /// 初期化页面
    /// </summary>
    /// <param name="companyCD">公司代码</param>
    private void InitPage(string companyCD)
    {
        //没有参数时，新规追加用户
        if (string.IsNullOrEmpty(companyCD))
        {
            return;
        }
        //修改用户
        //获取用户信息
        CompanyOpenServModel model = CompanyOpenServBus.GetCompanyOpenServInfo(companyCD);
        //公司代码
        txtCompanyCD.Text = companyCD;
        //CompanyCDTip.Visible = false;
        //公司代码变更不可
        //txtCompanyCD.Enabled = false;
        if (model == null)
        {
            //设置错误信息
            lblMessage.Text = MessageUtil.GetMessage("Common", "E007");
            return;
        }
        //设置页面值
        //最大角色数
        txtMaxRoles.Text = model.MaxRoles;
        //最大用户数
        txtMaxUser.Text = model.MaxUers;
        //文件总大小
        txtMaxDocSize.Text = model.MaxDocSize;
        //最大单个文件大小
        txtSingleDocSize.Text = model.SingleDocSize;
        //最大文件个数
        txtMaxDocNum.Text = model.MaxDocNum;        
        //开始时间
        txtOpenDate.Text = model.OpenDate;
        //结束时间
        txtCloseDate.Text = model.CloseDate;
        //备注
        txtRemark.Text = model.Remark;
    }
    #endregion

    #region 修改客户信息
    /// <summary>
    /// 修改客户信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnModify_Click(object sender, EventArgs e)
    {
        //插入和更新标志
        bool isInsert = txtCompanyCD.Enabled;
        //公司代码
        string companyCD = txtCompanyCD.Text;
        //最大角色数
        string maxRoles = txtMaxRoles.Text;
        //最大用户数
        string maxUser = txtMaxUser.Text;
        //文件总大小
        string maxDocSize = txtMaxDocSize.Text;
        //最大单个文件大小
        string singleDocSize = txtSingleDocSize.Text;
        //最大文件个数
        string maxDocNum = txtMaxDocNum.Text;
        //开始时间
        string openDate = txtOpenDate.Text.Replace("-", "");
        //结束时间
        string closeDate = txtCloseDate.Text.Replace("-", "");

        //用户信息模板类
        CompanyOpenServModel model = new CompanyOpenServModel();
        //公司代码
        model.CompanyCD = companyCD;
        //最大角色数
        model.MaxRoles = maxRoles;
        //最大用户数
        model.MaxUers = maxUser;
        //文件总大小
        model.MaxDocSize = maxDocSize;
        //最大单个文件大小
        model.SingleDocSize = singleDocSize;
        //最大文件个数
        model.MaxDocNum = maxDocNum;
        //开始时间
        model.OpenDate = openDate;
        //结束时间
        model.CloseDate = closeDate;
        //备注
        model.Remark = txtRemark.Text;
        //更新插入标志
        if (isInsert)
        {
            model.IsInsert = true;
        }
        else
        {
            model.IsInsert = false;
        }

        //数据更新或插入
        if (CompanyOpenServBus.ModifyCompanyOpenServInfo(model))
        {
            string url = "CompanyOpenServ_Query.aspx?CompanyCD=" + companyCD;
            Response.Redirect(url);
        }
    }
     #endregion

}
