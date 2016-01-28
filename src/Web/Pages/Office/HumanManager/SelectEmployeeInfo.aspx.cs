/**********************************************
 * 类作用：   选择人员列表
 * 建立人：   吴志强
 * 建立时间： 2009/04/27
 ***********************************************/
using System;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using System.Web.UI.WebControls;

public partial class Pages_Office_HumanManager_SelectEmployeeInfo : System.Web.UI.Page
{
    /// <summary>
    /// 类名：SelectEmployeeInfo
    /// 描述：选择人员列表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/27
    /// 最后修改时间：2009/04/27
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //获取岗位列表数据
            ddlQuarter.DataSource = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            ddlQuarter.Items.Insert(0, new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE));
            //岗位职等
            ddlAdminLevel.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlAdminLevel.TypeCode = ConstUtil.CODE_TYPE_QUARTER_ADMIN;
            ddlAdminLevel.IsInsertSelect = true;
        }
    }
}
