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
using XBase.Business.Office.SystemManager;

public partial class Pages_Office_StorageManager_Init : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region 库存流水账初始化
    protected void Button1_Click(object sender, EventArgs e)
    {
        XBase.Business.Common.StrongeInitBus.InitInfo();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        XBase.Business.Common.SubStorageInitBus.InitInfo();
    }
    #endregion
}
