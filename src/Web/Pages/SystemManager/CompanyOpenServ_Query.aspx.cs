/**********************************************
 * 类作用：   客户公司业务开通处理
 * 建立人：   吴志强
 * 建立时间： 2009/01/21
 ***********************************************/

using System;
using XBase.Business.SystemManager;

public partial class Pages_SystemManager_CompanyOpenServ_Query : System.Web.UI.Page
{

    /// <summary>
    /// 类名：UserInfoBus
    /// 描述：客户公司业务开通处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/21
    /// 最后修改时间：2009/01/21
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //第一次登陆页面
        if (!IsPostBack)
        {
            //获取用户ID
            string companyCD = Request.QueryString["CompanyCD"];
            //初期设置页面
            InitPagedContrl(false, companyCD);
        }
    }

    #region "查询操作"

    /// <summary>
    /// 查询操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        InitPagedContrl(true, "search");
    }

    #endregion

    /// <summary>
    /// 分页控件设置
    /// </summary>
    /// <param name="isPostBack">是否初期显示</param>
    /// <param name="companyCD">公司代码</param>
    private void InitPagedContrl(bool isPostBack, string companyCD)
    {
        //if (!isPostBack && string.IsNullOrEmpty(companyCD))
        //{
        //    //一览不显示
        //    //ucCompanyInfo.Visible = false;
        //    //return;
        //}
        ////选择CheckBox
        //ucCompanyInfo.CheckBox = true;

        //string sql = "SELECT CompanyCD,MaxRoles,MaxUers,MaxDocSize,SingleDocSize,MaxDocNum,"
        //            + "Case when OpenDate <> '' then SUBSTRING(OpenDate, 1, 4) + '-' + SUBSTRING(OpenDate, 5, 2) + '-' + SUBSTRING(OpenDate, 7, 2)	else ''	end	AS OpenDate,"
        //            + "Case when CloseDate <> '' then SUBSTRING(CloseDate, 1, 4) + '-' + SUBSTRING(CloseDate, 5, 2) + '-' + SUBSTRING(CloseDate, 7, 2)	else ''	end	AS CloseDate " 
        //            + " FROM pubdba.companyOpenServ WHERE 1 = 1";
        ////从修改增加页面后退回来的初期显示
        //if (!isPostBack)
        //{
        //    //显示修改的数据
        //    sql += " AND CompanyCD = '" + companyCD + "'";
        //}
        //else
        //{
        //    //用户ID输入的场合，添加为条件
        //    if (!string.IsNullOrEmpty(txtCompanyCD.Text))
        //    {
        //        sql += " AND CompanyCD = '" + txtCompanyCD.Text + "'";
        //    }        
        //    //开始时间输入的场合，添加为条件
        //    if (!string.IsNullOrEmpty(txtOpenDate.Text))
        //    {
        //        sql += " AND OpenDate >= '" + txtOpenDate.Text.Replace("-", "") + "'";
        //    }
        //    //结束时间输入的场合，添加为条件
        //    if (!string.IsNullOrEmpty(txtCloseDate.Text))
        //    {
        //        sql += " AND CloseDate <= '" + txtCloseDate.Text.Replace("-", "") + "'";
        //    }
        //}
        ////设置用户控件的查询语句
        ////ucCompanyInfo.Setsql = sql;
        ////定义用户控件的标题
        //string[,] Title = { { "公司代码", "CompanyCD" }, { "角色上限", "MaxRoles" }, { "用户上限", "MaxUers" }, { "文档上限", "MaxDocSize" }, { "单个文件上限", "SingleDocSize" }, { "文档上限", "MaxDocNum" }, { "生效日期", "OpenDate" }, { "失效日期", "CloseDate" } };
        ////设置用户控件的标题
        //ucCompanyInfo.TableTitle = Title;
        ////列表可见
        //ucCompanyInfo.Visible = true;
        //ucCompanyInfo.IsSort = true;
        ////设置高度
        ////ucCompanyInfo.TableHeight = "300";
        //////设置宽度
        ////ucCompanyInfo.TableWidth = "800";
        //ucCompanyInfo.GetData();
        ////
        //ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script>hidePopup();</script>");
    }

    /// <summary>
    /// 删除操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //获取删除的用户ID
        string deleteCompanyCD = hidDelete.Value;
        //替换最后的分隔符
        deleteCompanyCD = deleteCompanyCD.TrimEnd(',');

        bool isDelete = CompanyOpenServBus.DeleteCompanyOpenServInfo(deleteCompanyCD);

        if (isDelete)
        {
            InitPagedContrl(true, "delete");
        }
    }

}
