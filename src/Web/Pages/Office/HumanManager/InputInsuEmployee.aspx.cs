/**********************************************
 * 类作用：   社会保险录入
 * 建立人：   吴志强
 * 建立时间： 2009/05/12
 ***********************************************/
using System;
using XBase.Common;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;

public partial class Pages_Office_HumanManager_InputInsuEmployee : BasePage
{
    /// <summary>
    /// 类名：InputInsuEmployee
    /// 描述：社会保险录入
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/12
    /// 最后修改时间：2009/05/12
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //显示工资项列表
            //InitSalaryInfo();
            //岗位初期
            DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataSource = dtQuarter;
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            //添加一请选择选项
            ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            ddlQuarter.Items.Insert(0, Item);
        }
    }
}
