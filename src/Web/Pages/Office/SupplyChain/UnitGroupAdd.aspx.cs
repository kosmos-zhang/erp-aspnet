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
using XBase.Business.Office.SupplyChain;

using XBase.Common;

/// <summary>
/// 计量单位组
/// </summary>
public partial class Pages_Office_SupplyChain_UnitGroupAdd : BasePage
{

    #region 变量
    /// <summary>
    /// 小数位数
    /// </summary>
    private int _selPoint = 2;
    #endregion

    #region 属性

    /// <summary>
    /// 小数位数
    /// </summary>
    public int SelPoint
    {
        get
        {
            return _selPoint;
        }
        set
        {
            _selPoint = value;
        }
    }
    #endregion

    #region 事件、方法

    #region 页面

    #region 事件

    /// <summary>
    /// 加载窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        // 小数位数
        _selPoint = int.Parse(UserInfo.SelPoint);
        if (!IsPostBack)
        {// 第一次加载
            DataTable dt = CodeReasonFeeBus.GetCodeUnitType();
            this.selUnit.DataSource = dt;
            this.selUnit.DataTextField = "CodeName";
            this.selUnit.DataValueField = "ID";
            this.selUnit.DataBind();
            DataView dv = dt.Copy().DefaultView;
            dv.Table.Rows.Add("0", "--请选择--");
            dv.Sort = "ID";
            this.selBU.DataSource = dv;
            this.selBU.DataTextField = "CodeName";
            this.selBU.DataValueField = "ID";
            this.selBU.DataBind();
        }
    }

    #endregion

    #region 方法

    #endregion

    #endregion

    #endregion
}
