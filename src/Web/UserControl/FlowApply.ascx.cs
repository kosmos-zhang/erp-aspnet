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
using XBase.Business.Common;
using System.Text;

public partial class UserControl_FlowApply : System.Web.UI.UserControl
{
    #region 属性设置
    private string _billTypeFlag;
    private string _billTypeCode;

    /// <summary>
    /// 单据分类标识
    /// </summary>
    public string BillTypeFlag
    {
        get
        {
            return _billTypeFlag;
        }
        set
        {
            _billTypeFlag = value;
        }
    }

    /// <summary>
    /// 单据分类编码
    /// </summary>
    public string BillTypeCode
    {
        get
        {
            return _billTypeCode;
        }
        set
        {
            _billTypeCode = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

}
