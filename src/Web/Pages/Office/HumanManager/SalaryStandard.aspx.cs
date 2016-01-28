/**********************************************
 * 类作用：   工资标准设置
 * 建立人：   吴志强
 * 建立时间： 2009/05/07
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Web.UI.WebControls;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
public partial class Pages_Office_HumanManager_SalaryStandard : BasePage
{
    /// <summary>
    /// 类名：SalaryStandard
    /// 描述：工资标准设置
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/07
    /// 最后修改时间：2009/05/07
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    { 
        //页面初期表示设置
        if (!IsPostBack)
        {
            ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            //岗位
            DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataSource = dtQuarter;
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            ddlQuarter.Items.Add(Item);
            ddlQuarter.SelectedValue = ConstUtil.CODE_TYPE_INSERT_VALUE;

            ddlSearchQuarter.DataSource = dtQuarter;
            ddlSearchQuarter.DataValueField = "ID";
            ddlSearchQuarter.DataTextField = "QuarterName";
            ddlSearchQuarter.DataBind();
            //添加一请选择选项
         
            //ddlSearchQuarter.Items.Insert(0, Item);
            ddlSearchQuarter.Items.Add(Item);
            ddlSearchQuarter.SelectedValue = "";
            //岗位职等
            ctQuaterAdmin.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ctQuaterAdmin.TypeCode = ConstUtil.CODE_TYPE_QUARTER_ADMIN;
            ctQuaterAdmin.IsInsertSelect = true;
            //ctQuaterAdmin.Items.Add(Item);
            //ctQuaterAdmin.SelectedValue = ConstUtil.CODE_TYPE_INSERT_VALUE;


            ctSearchQuaterAdmin.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ctSearchQuaterAdmin.TypeCode = ConstUtil.CODE_TYPE_QUARTER_ADMIN;
            ctSearchQuaterAdmin.IsInsertSelect = true;
        
            //工资项
            DataTable dtSalary = SalaryItemBus.GetSalaryItemInfo(true);
            ddlSalaryItem.DataSource = dtSalary;
            ddlSalaryItem.DataValueField = "ItemNo";
            ddlSalaryItem.DataTextField = "ItemName";
            ddlSalaryItem.DataBind();
            ddlSalaryItem.Items.Add(Item);
            ddlSalaryItem.SelectedValue = ConstUtil.CODE_TYPE_INSERT_VALUE;

        }
    }
   
    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {

        //获取数据
        SalaryStandardModel searchModel = new SalaryStandardModel();
        //设置查询条件
        //岗位
        searchModel.QuarterID = Request.Form["ddlSearchQuarter"].ToString();
        //岗位职等
       //string I= Request.Form.AllKeys.ToString();
       searchModel.AdminLevel = Request.Form["ctSearchQuaterAdmin$ddlCodeType"].ToString();
        //启用状态
        searchModel.UsedStatus = Request.Form["ddlSearchUsedStatus"].ToString();

        //查询数据
        DataTable dtNotify = SalaryStandardBus.SearchSalaryStandardInfo(searchModel);

        //导出标题
        string headerTitle = "岗位|岗位职等|工资项名称|金额|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "QuarterName|AdminLevelName|ItemName|UnitPrice|UsedStatusName";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dtNotify, header, field, "岗位工资设置");
    }
}
