using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Office_StorageManager_StorageCost : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //获取小数位数
            DigitalLength.Value = UserInfo.SelPoint;
            //绑定查询条件
            BindYearMonth();
        }

    }


    #region 绑定查询列表年月
    /// <summary>
    /// 绑定查询列表年月
    /// </summary>
    protected void BindYearMonth()
    {

        for (int i = 0; i >= -24; i--)
        {
            DateTime now = DateTime.Now;
            DateTime newYearMonth = now.AddMonths(i-1);
            string item = newYearMonth.ToString("yyyyMM");
            txtEndYearMonth.Items.Add(new ListItem(item, item));
            txtStartYearMonth.Items.Add(new ListItem(item, item));
            selYearMonthC.Items.Add(new ListItem(item, item));
        }
    }
    #endregion


}
