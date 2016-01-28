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
using XBase.Business.Office.HumanManager ;
using XBase.Model.Office.HumanManager;

public partial class Pages_Office_HumanManager_PerformanceType : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PerformanceTypeModel searchModel = new PerformanceTypeModel();
        //设置查询条件
        //要素名称
        searchModel.TypeName = this.txtSearchElemName.Value; 
        //启用状态
        searchModel.UsedStatus = this.sltSearchUsedStatus.Value;
        searchModel.CompanyCD = companyCD; 
        //查询数据
        //DataTable dtData = PerformanceTypeBus.SearchRectCheckElemInfo(searchModel);
        ////导出标题
        //string headerTitle = "类型名称|启用状态";
        //string[] header = headerTitle.Split('|');

        ////导出标题所对应的列字段名称
        //string columnFiled = "TypeName|UsedStatusName";
        //string[] field = columnFiled.Split('|');

        //XBase.Common.OutputToExecl.ExportToTable(this.Page, dtData, header, field, "考核类型设置");
        DataTable dt = PerformanceTypeBus.SearchRectCheckElemInfo(searchModel);

        //导出标题
        string headerTitle = "类型名称|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "TypeName|UsedStatusName";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "考核类型设置");



   
    }
}
