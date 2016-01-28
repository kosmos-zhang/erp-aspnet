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

public partial class Pages_Office_SystemManager_ParameterSetting :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GetSetting();
        }

    }

    protected void GetSetting()
    {
        //出库是否显示价格
        bool IsPriceOn=false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "1",true))
        {
            IsPriceOn = true;
        }
        dioBN1.Checked = IsPriceOn;
        dioBN2.Checked = !IsPriceOn;

        //条码是否启用
        bool IsBarCode = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "2",true))
        {
            IsBarCode = true;
        }
        dioCB1.Checked = IsBarCode;
        dioCB2.Checked = !IsBarCode;

        //多计量单位是否启用
        bool IsMoreUnit = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "3",false))
        {
            IsMoreUnit = true;
        }
        dioMU1.Checked = IsMoreUnit;
        dioMU2.Checked = !IsMoreUnit;


        //自动生成凭证
        bool Isvoucher = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "6", false))
        {
            Isvoucher = true;
        }
        radvoucher1.Checked = Isvoucher;
        radvoucher2.Checked = !Isvoucher;

        //自动审核登帐
        bool Isapply = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "7", false))
        {
            Isapply = true;
        }
        radapply1.Checked = Isapply;
        radapply2.Checked = !Isapply;

        //超订单发货
        bool IsOverOrder = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "8", false))
        {
            IsOverOrder = true;
        }
        radOver1.Checked = IsOverOrder;
        radOver2.Checked = !IsOverOrder;

        //允许出入库价格为零
        bool IsZero = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "9", false))
        {
            IsZero = true;
        }
        dioZero1.Checked = IsZero;
        dioZero2.Checked = !IsZero;

        DataTable dt = ParameterSettingBus.GetPoint(UserInfo.CompanyCD,"5");
        if (dt != null) 
        {
            if (dt.Rows.Count > 0) 
            {
                SelPoint.Value = dt.Rows[0]["SelPoint"].ToString();
            }
        }

    }
}
