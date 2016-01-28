/**********************************************
 * 作    者： 张玉圆
 * 创建日期： 2009/03/19
 * 描    述： 根据创建人获取客户名称、编号。经测试支持IE7和火狐浏览器。
 *            使用请参考例子Office/CustManager/LinkMan_Add.aspx
 * 修改日期： 2009/03/20
 * 版    本： 0.5.0
 ***********************************************/
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

public partial class UserControl_CustNameSel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 客户ID
    /// </summary>
    public string CustIDContrl
    {
        get
        {
            return hfCustID_Ser.Value;
        }
    }
    //客户编号
    public string CustNoContrl 
    {
        set
        {
            //hfCustNoControl.Value = value;
        }
    }
}
