/**********************************************
 * 类作用：   选择人才储备处理
 * 建立人：   吴志强
 * 建立时间： 2009/04/21
 ***********************************************/
using System;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using System.Web.UI.WebControls;

public partial class Pages_Office_HumanManager_SelectReserveInfo : System.Web.UI.Page
{
    /// <summary>
    /// 类名：SelectReserveInfo
    /// 描述：选择人才储备处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/21
    /// 最后修改时间：2009/04/21
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //获取应聘岗位列表数据
            ddlQuarter.DataSource = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            ddlQuarter.Items.Insert(0, new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE));
            //学历
            ddlCulture.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlCulture.TypeCode = ConstUtil.CODE_TYPE_CULTURE;
            ddlCulture.IsInsertSelect = true;
            //专业
            ddlProfessional.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlProfessional.TypeCode = ConstUtil.CODE_TYPE_PROFESSIONAL;
            ddlProfessional.IsInsertSelect = true;
        }
    }
}
