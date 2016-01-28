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

public partial class Pages_Office_HumanManager_RectCheckElem_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        RectCheckElemModel searchModel = new RectCheckElemModel();
        //设置查询条件
        //要素名称
        searchModel.ElemName = txtSearchElemName.Value.Trim();
        //启用状态
        searchModel.UsedStatus = Request.Form["sltSearchUsedStatus"].ToString();


        if (searchModel.ElemName != null)
        {
            int bbb = searchModel.ElemName.IndexOf('%');///过滤字符串
            if (bbb != -1)
            {
                searchModel.ElemName = searchModel.ElemName.Replace('%', ' ');
            }
        }


        //查询数据
        DataTable dtData = RectCheckElemBus.SearchRectCheckElemInfo(searchModel);
        //导出标题
        string headerTitle = "要素名称|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "ElemName|UsedStatusName";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dtData, header, field, "面试评测要素设置");




    }
}
