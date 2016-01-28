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
using XBase.Business.Office.SystemManager;

public partial class Pages_Office_SystemManager_BatchNoRuleSet :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //批次规则是否已启用
            bool IsBatch = false;
            if (BatchNoRuleSetBus.GetBatchStatus(UserInfo.CompanyCD))
            {
                IsBatch = true;
            }
            dioBatch1.Checked = IsBatch;
            dioBatch2.Checked = !IsBatch;
            DataTable dt = BatchNoRuleSetBus.GetBatchRuleInfo(UserInfo.CompanyCD);
            if (dt.Rows.Count > 0)
            {
                batchRuleID.Value = dt.Rows[0]["ID"].ToString();
            }
        }
    }
}
