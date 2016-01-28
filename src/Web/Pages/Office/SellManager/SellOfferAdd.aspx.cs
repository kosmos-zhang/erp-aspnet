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
using XBase.Common;

public partial class Pages_Office_SellManager_SellOfferAdd : BasePage
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
        
            ////临时注释
            Creator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
            ////临时注释
            ModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            UserSeller.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
            hiddSeller.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
       
        CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        ModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

        OfferDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

        FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_SELL;
        FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_SELLOFFER_NO;

        orderNoUC.CodingType = ConstUtil.CODING_RULE_SELL;//销售报价单编号
        orderNoUC.ItemTypeID = ConstUtil.CODING_RULE_SELLOFFER_NO;//销售报价单编号

        PayType.TypeFlag = ConstUtil.CUST_TYPE_CUST;//结算方式
        PayType.TypeCode = ConstUtil.CUST_INFO_PAYTYPE;//结算方式
        PayType.IsInsertSelect = true;

        PackageUC.TypeFlag = ConstUtil.SELL_TYPE_SELL;//包装
        PackageUC.IsInsertSelect = true;
        PackageUC.TypeCode = ConstUtil.SELL_TYPE_PACKAGE;//包装

        MoneyType.IsInsertSelect = true;//支付方式
        MoneyType.TypeFlag = ConstUtil.CUST_TYPE_CUST;//支付方式
        MoneyType.TypeCode = ConstUtil.CUST_INFO_MONEYTYPE;//支付方式

        sellType.TypeFlag = ConstUtil.SELL_TYPE_SELL;//销售类别
        sellType.TypeCode = ConstUtil.SELL_TYPE_SELLTYPE;//销售类别
        sellType.IsInsertSelect = true;

        CarryType.TypeFlag = ConstUtil.SELL_TYPE_SELL;//运送方式
        CarryType.TypeCode = ConstUtil.SELL_TYPE_CARRYTYPE;//运送方式
        CarryType.IsInsertSelect = true;

        TakeType.TypeFlag = ConstUtil.SELL_TYPE_SELL;//交货方式
        TakeType.TypeCode = ConstUtil.SELL_TYPE_TAKETYPE;//交
        TakeType.IsInsertSelect = true;

        //是否启用条码
        bool BarFlag = UserInfo.IsBarCode;
        if (BarFlag)
        {
            GetGoods.Visible = true;
            UnGetGoods.Visible = true;
        }
        else
        {
            GetGoods.Visible = false;
            UnGetGoods.Visible = false;
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
