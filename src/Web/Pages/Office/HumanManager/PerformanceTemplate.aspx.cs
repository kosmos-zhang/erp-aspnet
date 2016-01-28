/**********************************************
 * 类作用：       绩效考核指标       
 * 建立人：    王保军
 * 建立时间： 2009.4.22
 ***********************************************/
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using XBase.Common;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;

public partial class Pages_Office_HumanManager_PerformanceTemplate : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //绩效考核指标编号初期处理
            AimNum.CodingType = ConstUtil.CODING_RULE_TYPE_TEMPLATE;
            AimNum.ItemTypeID = ConstUtil.CODING_RULE_HUMEN_TEMPLATE;
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    { 
        //获取数据
        PerformanceTemplateModel searchModel = new PerformanceTemplateModel();
        searchModel.TypeID = selSearchPerformanceType .Value ;
        searchModel.Title = inpSearchTitle.Value .Trim ();
        //设置查询条件
        //要素名称
        /// searchModel.ElemName = context.Request.QueryString["ElemName"];
        //启用状态
        /// searchModel.UsedStatus = context.Request.QueryString["UsedStatus"];

        //查询数据
    
        DataTable dt = PerformanceTemplateBus.SearchFlowInfo(searchModel);


        //导出标题
        string headerTitle = "模板主题|考核类型|创建人|创建日期";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "Title|TypeName|CreaterName|CreateDate";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "考核模板设置");




    }
}
