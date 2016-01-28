/**********************************************
 * 类作用：   浮动工资录入
 * 建立人：   吴志强
 * 建立时间： 2009/05/13
 ***********************************************/
using System;
using XBase.Common;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;

public partial class Pages_Office_HumanManager_InputFloatSalary : BasePage
{
    /// <summary>
    /// 类名：InputFloatSalary
    /// 描述：浮动工资录入
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/13
    /// 最后修改时间：2009/05/13
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //岗位初期
            DataTable dtData = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataSource = dtData;
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            //添加一请选择选项
            ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            ddlQuarter.Items.Insert(0, Item);
            //计件项目
            //dtData = PieceworkItemBus.GetPieceworkItemInfo(true);
            //ddlPiecework.DataSource = dtData;
            //ddlPiecework.DataValueField = "ItemNo";
            //ddlPiecework.DataTextField = "ItemName";
            //ddlPiecework.DataBind();
            //ddlPiecework.Items.Insert(0, Item);
            //ddlNewPiecework.DataSource = dtData;
            //ddlNewPiecework.DataValueField = "ItemNo";
            //ddlNewPiecework.DataTextField = "ItemName";
            //ddlNewPiecework.DataBind();
            //ddlNewPiecework.Items.Insert(0, Item);
            //ddlNewPiecework.Attributes.Add("onchange", "ChangeItemNo();");
            //计时项目
            //dtData = TimeItemBus.GetTimeItemInfo(true);
            //ddlTime.DataSource = dtData;
            //ddlTime.DataValueField = "TimeNo";
            //ddlTime.DataTextField = "TimeName";
            //ddlTime.DataBind();
            //ddlTime.Items.Insert(0, Item);
            //ddlNewTime.DataSource = dtData;
            //ddlNewTime.DataValueField = "TimeNo";
            //ddlNewTime.DataTextField = "TimeName";
            //ddlNewTime.DataBind();
            //ddlNewTime.Items.Insert(0, Item);
            //ddlNewTime.Attributes.Add("onchange", "ChangeItemNo();");
            //提成项目
            //dtData = CommissionItemBus.GetCommissionItemInfo(true);
            //ddlCommission.DataSource = dtData;
            //ddlCommission.DataValueField = "ItemNo";
            //ddlCommission.DataTextField = "ItemName";
            //ddlCommission.DataBind();
            //ddlCommission.Items.Insert(0, Item);
            //ddlNewCommission.DataSource = dtData;
            //ddlNewCommission.DataValueField = "ItemNo";
            //ddlNewCommission.DataTextField = "ItemName";
            //ddlNewCommission.DataBind();
            //ddlNewCommission.Items.Insert(0, Item);
            //ddlNewCommission.Attributes.Add("onchange", "ChangeItemNo();");
        }
    }
}
