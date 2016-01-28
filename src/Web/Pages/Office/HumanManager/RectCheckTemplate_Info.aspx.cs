/**********************************************
 * 类作用：   面试考评模板表
 * 建立人：   吴志强
 * 建立时间： 2009/04/16
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Web.UI.WebControls;
using System.Collections;
public partial class Pages_Office_HumanManager_RectCheckTemplate_Info : BasePage
{
    /// <summary>
    /// 类名：RectCheckTemplate_Info
    /// 描述：面试考评模板表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/16
    /// 最后修改时间：2009/04/16
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //新建面试模板的模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_RECTCHECKTEMPLATE_EDIT;
            //岗位信息
            DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataSource = dtQuarter;
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            ddlQuarter.Items.Insert(0
                        , new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE));
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
                    //考试编号
                    txtTemplateNo.Value = Request.QueryString["TemplateNo"];
                    //主题
                    txtTitle.Value = Request.QueryString["Title"];
                    //考试地点
                    ddlQuarter.SelectedValue = Request.QueryString["QuarterID"];
                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "SearchTrainingAsse"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");DoSearch('" + pageIndex + "');</script>");
                }
            }
        }
    }


    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        //从请求中获取排序列
        string orderString = hidOrderBy.Value.Trim();

        //排序：默认为升序
        string orderBy = "asc";
        //要排序的字段，如果为空，默认为"RectApplyNo"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TemplateNo";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "desc";
        }
        //从请求中获取当前页

        //int pageIndex = Convert.ToInt32(txtToPage.Value);
        ////从请求中获取每页显示记录数
        //int pageCount = Convert.ToInt32(txtShowPageCount.Value);
        ////跳过记录数
        //int skipRecord = (pageIndex - 1) * pageCount;


        RectCheckTemplateModel searchModel = new RectCheckTemplateModel();
        //设置查询条件
        //考核编号
        searchModel.TemplateNo = txtTemplateNo.Value.Trim();
        //主题
        searchModel.Title = txtTitle.Value.Trim();
        //岗位
        searchModel.QuarterID = Request.Form["ddlQuarter"].ToString();

        if (searchModel.Title != null)
        {
            int bbb = searchModel.Title.IndexOf('%');///过滤字符串
            if (bbb != -1)
            {
                searchModel.Title = searchModel.Title.Replace('%', ' ');
            }
        }
        string ord = orderByCol + " " + orderBy;
        int TotalCount = 0;
        //查询数据
        DataTable dtTemp = new DataTable();
        if (!string.IsNullOrEmpty(txtToPage.Value))
        {
            dtTemp = RectCheckTemplateBus.SearchTemplateCSInfo(searchModel, 1, 10000, ord, ref TotalCount);
        }
        string[,] ht = { 
                            { "模板编号", "TemplateNo"}, 
                            { "主题", "Title"}, 
                            { "岗位", "QuarterName" }
                          
                        };
        ExportExcel(dtTemp, ht, "", "面试评测模板列表");
    }
}
