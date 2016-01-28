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

using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Common;


public partial class Pages_Office_StorageManager_StorageProductAlarm : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        StockAccountModel model = new StockAccountModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.ProductNo = txtProductNo.Value;
        model.ProductName = txtProductName.Value;
        string AlarmType = sltAlarmType.Value;
        string BarCode = HiddenBarCode.Value.Trim();

        string orderBy = txtorderBy.Value;
        if (!string.IsNullOrEmpty(orderBy))
        {
            if (orderBy.Split('_')[1] == "a")
            {
                orderBy = orderBy.Split('_')[0] + " asc";
            }
            else
            {
                orderBy = orderBy.Split('_')[0] + " desc";
            }
        }
        DataTable dt = StorageProductAlarmBus.GetStorageProductAlarm(AlarmType, model, orderBy, BarCode);

        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "物品编号", "物品名称", "物品分类", "规格", "单位", "库存下限", "库存上限", "安全库存", "现有存量", "报警类型" },
            new string[] { "ProductNo", "ProductName", "TypeID", "Specification", "UnitID", "MinStockNum", "MaxStockNum", "SafeStockNum", "ProductCount", "AlarmType" },
            "库存报警列表");
    }
}
