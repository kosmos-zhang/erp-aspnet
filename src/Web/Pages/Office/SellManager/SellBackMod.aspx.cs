using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBase.Common;
using XBase.Business.Office.SellManager;
public partial class Pages_Office_SellManager_SellBackMod : BasePage
{
    //小数精度
    private int _selPoint = 2;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = SellBackBus.GetReasonType();
        if (dt.Rows.Count > 0)
        {
            ddlReasonType.DataSource = dt;
            ddlReasonType.DataTextField = "CodeName";
            ddlReasonType.DataValueField = "ID";
            ddlReasonType.DataBind();
        }
        CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        ModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_SELL;
        FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_SELLBACK_NO;
      
        PayTypeUC.TypeFlag = ConstUtil.CUST_TYPE_CUST;//结算方式
        PayTypeUC.TypeCode = ConstUtil.CUST_INFO_PAYTYPE;//结算方式
        PayTypeUC.IsInsertSelect = true;
        PackageUC.TypeFlag = ConstUtil.SELL_TYPE_SELL;//包装
        PackageUC.IsInsertSelect = true;
        PackageUC.TypeCode = ConstUtil.SELL_TYPE_PACKAGE;//包装
        MoneyTypeUC.IsInsertSelect = true;
        MoneyTypeUC.TypeFlag = ConstUtil.CUST_TYPE_CUST;
        MoneyTypeUC.TypeCode = ConstUtil.CUST_INFO_MONEYTYPE;

        CarryType.TypeFlag = ConstUtil.SELL_TYPE_SELL;//运送方式
        CarryType.TypeCode = ConstUtil.SELL_TYPE_CARRYTYPE;//运送方式
        CarryType.IsInsertSelect = true;

        //设置隐藏域条码的值
        if (UserInfo.IsBarCode)
        {
            hidBarCode.Value = "1";
        }
        else
        {
            hidBarCode.Value = "0";
        }
        //是否启用多单位
        if (UserInfo.IsMoreUnit)
        {
            txtIsMoreUnit.Value = "1";
            BaseUnitD.Visible = true;
            BaseCountD.Visible = true;
        }
        else
        {
            txtIsMoreUnit.Value = "0";
            BaseUnitD.Visible = false;
            BaseCountD.Visible = false;
        }
        // 小数位数
        _selPoint = int.Parse(UserInfo.SelPoint);
    }
}
