/**********************************************
 * 类作用：   培训列表维护处理
 * 建立人：   吴志强
 * 建立时间： 2009/04/03
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_Training_Info : BasePage
{
    /// <summary>
    /// 类名：Training_Info
    /// 描述：新建培训维护处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/03
    /// 最后修改时间：2009/04/03
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            btnImport.Attributes["onclick"] = "return IfExp();";

            //培训方式
            ddlTrainingWay.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlTrainingWay.TypeCode = ConstUtil.CODE_TYPE_TRAINING;
            ddlTrainingWay.IsInsertSelect = true;
            //新建修改培训的模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_TRAINING_EDIT;
            //考核模块ID
            hidModuleIDAsse.Value = ConstUtil.MODULE_ID_HUMAN_TRAININGASSE_EDIT;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //从列表过来时
            int firstIndex = requestParam.IndexOf("&");
            //返回回来时
            if (firstIndex > 0)
            {
                //获取是否查询的标识
                string flag = Request.QueryString["Flag"];
                //点击查询时，设置查询的条件，并执行查询
                if ("1".Equals(flag))
                {
                    //培训编号
                    txtTrainingNo.Value = Request.QueryString["TrainingNo"];
                    //培训名称
                    txtTrainingName.Value = Request.QueryString["TrainingName"];
                    //培训方式
                    ddlTrainingWay.SelectedValue = Request.QueryString["TrainingWay"];
                    //开始时间
                    txtStartDate.Text = Request.QueryString["StartDate"];
                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "SearchTrainingAsse"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");SearchTrainingInfo('" + pageIndex + "');</script>");
                }
            }
        }
    }

    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            string orderString = hiddExpOrder.Value.Trim();//排序
            string order = "asc";//排序：降序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"

            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            string ord = " ORDER BY " + orderBy + " " + order;

            //获取数据
            TrainingSearchModel searchModel = new TrainingSearchModel();
            //设置查询条件
            //培训编号
            searchModel.TrainingNo = txtTrainingNo.Value.Trim();
            //培训名称
            searchModel.TrainingName = txtTrainingName.Value.Trim();
            //培训方式
            searchModel.TrainingWayID = ddlTrainingWay.SelectedValue;
            //开始时间
            searchModel.StartDate = txtStartDate.Text.Trim();
            searchModel.StartToDate = txtAsseEndDate.Text.Trim();

            //查询数据
            DataTable dt = TrainingBus.SearchTrainingInfo(searchModel);

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "培训编号", "培训名称", "培训方式", "培训老师", "开始时间", "结束时间", "培训地点"},
                new string[] { "TrainingNo", "TrainingName", "TrainingWayName", "TrainingTeacher", "StartDate", "EndDate", "TrainingPlace" },
                "培训列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}
