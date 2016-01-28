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
using XBase.Model.Office.StorageManager ;
using XBase.Business.Office.StorageManager;

public partial class Pages_Office_StorageManager_DayEnd : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartPlanDate.Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_DayEnd;
        }
    }

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        string orderString = hidOrderby .Value .Trim ();//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreateDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }
        if (string.IsNullOrEmpty(hidPageCount.Value.Trim()))
        {

        }
        else
        {
            int pageCount = int.Parse(hidPageCount.Value.Trim());//每页显示记录数
            int pageIndex = int.Parse(hidPageIndex.Value.Trim());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            string day = txtStartPlanDate.Value.Trim();

            int TotalCount = 0;

            orderBy = orderBy + " " + order;
            DataTable dt = DayEndBus.SelectDayInfo(pageIndex, pageCount, orderBy, ref TotalCount, day);


            //导出标题
            string headerTitle = "日期|物品编号|品名|规格|批次|仓库|入库总数量|出库总数量|当日结存量";
            string[] header = headerTitle.Split('|');

            //导出标题所对应的列字段名称
            string columnFiled = "DailyDate|ProductNo|ProductName|Specification|BatchNo|StorageName|InTotal|OutTotal|TodayCount";
            string[] field = columnFiled.Split('|');

            XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "库存日结列表");
        }
    }
}
