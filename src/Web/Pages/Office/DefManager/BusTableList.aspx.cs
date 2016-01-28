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

using XBase.Business.Office.DefManager;
using XBase.Common;
using XBase.Model.Office.DefManager;

/// <summary>
/// 业务表列表界面
/// </summary>
public partial class Pages_Office_DefManager_BusTableList : BasePage
{
    /// <summary>
    /// 界面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.isreturn.Value = Request["isreturn"];
        }
    }
}
