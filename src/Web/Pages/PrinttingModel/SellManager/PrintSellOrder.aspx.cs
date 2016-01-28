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
using XBase.Model.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Office.SupplyChain;

public partial class Pages_PrinttingModel_SellManager_PrintSellOrder : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_SALE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_SELLORDER.ToString();
        hiddckSellOrderNo.Value = Request.QueryString["no"].ToString();

        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//UserInfo.CompanyCD;
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_SALE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_SELLORDER;

        //    /*接受参数*/
        string OrderNo = Request.QueryString["no"].ToString();

        #region 基本信息 明细信息
        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase = {     { "{ExtField1}", "ExtField1"},
                                { "{ExtField2}", "ExtField2"},
                                { "{ExtField3}", "ExtField3"},
                                { "{ExtField4}", "ExtField4"},
                                { "{ExtField5}", "ExtField5"},
                                { "{ExtField6}", "ExtField6"},
                                { "{ExtField7}", "ExtField7"},
                                { "{ExtField8}", "ExtField8"},
                                { "{ExtField9}", "ExtField9"},
                                { "{ExtField10}", "ExtField10"},
                                { "单据编号", "OrderNo"}, 
                                { "主题", "Title"}, 
                                { "源单类型", "FromTypeText" },
                                { "源单编号", "FromBillNo" },
                                { "客户名称", "CustName"},
                                { "客户电话", "CustTel"},
                                { "业务类型", "BusiTypeName"},
                                { "销售类别", "SellTypeName"},
                                { "结算方式", "PayTypeName"},
                                { "支付方式", "MoneyTypeName"},
                                { "订货方式", "OrderMethodName"},
                                { "交货方式", "TakeTypeName"},
                                { "运送方式", "CarryTypeName"},
                                { "币种", "CurrencyName"},
                                { "汇率", "Rate"},
                                { "业务员", "SellerName"},
                                { "部门", "DeptName"},
                                { "客户方代表", "TheyDelegate"},
                                { "我方代表", "OurDelegateName"},
                                { "签单日期", "OrderDate"},
                                { "开始日期", "StartDate"},
                                { "截止日期", "EndDate"},
                                { "最迟发货时间", "SendDate"},
                                { "订单状态", "stateText"},
                                { "异常终止原因", "StatusNote"},
                                { "客户订单号", "CustOrderNo"},
                                { "是否增值税", "isAddTaxName"},
                                { "可查看订单的人员", "CanViewUserName"},
                                { "所属项目", "ProjectName"},
                                { "金额合计", "TotalPrice"},
                                { "税额合计", "Tax"},
                                { "含税金额合计", "TotalFee"},
                                { "整单折扣(%)", "Discount"},
                                { "折扣含税金额", "RealTotal"},
                                { "折扣合计", "DiscountTotal"},
                                { "订单数量合计", "CountTotal"},
                                { "销售费用合计", "SaleFeeTotal"},
                                { "单据状态", "BillStatusText"},
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserName"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},
                                { "备注", "Remark"},
                                { "包装运输说明", "PackTransit"},
                                { "付款说明", "PayRemark"},
                                { "交付说明", "DeliverRemark"},
                                { "建单情况", "IsOpenbillText"},
                                { "回款状态", "IsAccText"},
                                { "已回款金额", "YAccounts"},
                                { "发货情况", "IsSendText"},
                              
                          };
        
        string[,] aDetail;
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        { 
            aDetail = new string[,]{ 
                                //{ "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "颜色", "ColorName" },
                                { "基本单位", "UnitName" },
                                { "基本数量", "ProductCount"},
                                { "单位", "UsedUnitName" },
                                { "数量", "UsedUnitCount"},
                                { "交货期限(天)", "SendTime" },
                                { "包装要求", "PackageName"},
                                { "单价", "UsedPrice"},
                                { "含税价", "TaxPrice"},
                                { "折扣(%)", "Discount"},
                                { "税率(%)", "TaxRate"},
                                { "含税金额", "TotalFee"},
                                { "金额", "TotalPrice"},
                                { "税额", "TotalTax"},
                                { "备注", "Remark"},
                           };
        }
        else
        { 
            aDetail = new string[,]{ 
                                //{ "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "颜色", "ColorName" },
                                { "单位", "UnitName" },
                                { "交货期限(天)", "SendTime" },
                                { "数量", "ProductCount"},
                                { "包装要求", "PackageName"},
                                { "单价", "UnitPrice"},
                                { "含税价", "TaxPrice"},
                                { "折扣(%)", "Discount"},
                                { "税率(%)", "TaxRate"},
                                { "含税金额", "TotalFee"},
                                { "金额", "TotalPrice"},
                                { "税额", "TotalTax"},
                                { "备注", "Remark"},
                           };
        }

        /*第二明细*/
        string[,] aSecondDetail = { 
                                //{ "序号", "SortNo"}, 
                                { "费用名称", "CodeName"}, 
                                { "金额", "FeeTotal" },
                                { "备注", "Remark"},
                           };
        #endregion

        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.SellOrder");
        if (dtExtTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtExtTable.Rows.Count; i++)
            {
                for (int x = 0; x < (aBase.Length / 2) - 52; x++)
                {
                    if (x == i)
                    {
                        aBase[x, 0] = dtExtTable.Rows[i]["EFDesc"].ToString();
                        countExt++;
                    }
                }
            }
        }
        #endregion

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);
        DataTable dtMain = SellOrderBus.GetRepOrder(OrderNo);//获取主表数据
        DataTable dtDetail = SellOrderBus.GetRepOrderDetail(OrderNo);//获取明细表：订单明细数据
        DataTable dtFee = SellOrderBus.GetRepOrderFee(OrderNo);//获取明细表：销售费用明细数据
        string strBaseFields = "";
        string strDetailFields = "";
        /*第二明细*/
        string strDetailSecondFields = "";

        if (dbPrint.Rows.Count > 0)
        {
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
            strDetailFields = dbPrint.Rows[0]["DetailFields"].ToString();
            /*第二明细*/
            strDetailSecondFields = dbPrint.Rows[0]["DetailSecondFields"].ToString();
        }
        else
        {
            isSeted.Value = "0";
            strBaseFields = "OrderNo|Title|FromTypeText|FromBillNo|CustName|CustTel|BusiTypeName|SellTypeName|PayTypeName|MoneyTypeName|OrderMethodName|TakeTypeName|CarryTypeName|CurrencyName|Rate|";
            strBaseFields = strBaseFields + "SellerName|DeptName|TheyDelegate|OurDelegateName|OrderDate|StartDate|EndDate|SendDate|stateText|StatusNote|CustOrderNo|isAddTaxName|CanViewUserName|ProjectName|TotalPrice|Tax|TotalFee|Discount|";
            strBaseFields = strBaseFields + "RealTotal|DiscountTotal|CountTotal|SaleFeeTotal|BillStatusText|CreatorName|CreateDate|ConfirmorName|ConfirmDate|CloserName|CloseDate|ModifiedUserID|ModifiedDate|Remark|";
            strBaseFields = strBaseFields + "PackTransit|PayRemark|DeliverRemark|IsOpenbillText|IsAccText|YAccounts|IsSendText";
            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "|" + "ExtField" + (i + 1);
                }
            }
            //多计量单位
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                /*订单明细SortNo|*/
                strDetailFields = "ProdNo|ProductName|Specification|ColorName|UnitName|ProductCount|UsedUnitName|UsedUnitCount|SendTime|PackageName|UsedPrice|TaxPrice|Discount|TaxRate|TotalFee|TotalPrice|TotalTax|Remark";
            }
            else
            { 
                /*订单明细SortNo|*/
                strDetailFields = "ProdNo|ProductName|Specification|ColorName|UnitName|ProductCount|SendTime|PackageName|UnitPrice|TaxPrice|Discount|TaxRate|TotalFee|TotalPrice|TotalTax|Remark";
            }
           
            /*费用明细SortNo|*/
            strDetailSecondFields = "CodeName|FeeTotal|Remark";
        }

        #region 2.主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            //第一个是打印页面的正标题,strBaseFields是在PrintParameterSetting表里设置的主表字段,
            //strDetailFields是在PrintParameterSetting表里设置的明细表的字段,
            //aBase,是打印设置页面上定义取出来的字段及对应的名称,
            //aDetail是打印设置页面上定义取出来的字段及对应的名称,
            //例如明细中的物品编号,对应的就是物品编号及取数据的字段ProdNo,
            //dtMRP是主表的数据集,
            //dtDetail是明细表的数据集,最后一个参数,用来区别是主表信息还是明细信息..
            tableBase.InnerHtml = WritePrintPageTable("销售订单", strBaseFields, strDetailFields, aBase, aDetail, dtMain, dtDetail, true);
        }
        #endregion

        #region 3.明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("销售订单", strBaseFields, strDetailFields, aBase, aDetail, dtMain, dtDetail, false);
        }
        #endregion

        /*第二明细*/
        #region 4.明细信息2
        if (!string.IsNullOrEmpty(strDetailSecondFields))
        {
            tableDetail2.InnerHtml = WritePrintPageTable("销售订单", strBaseFields, strDetailSecondFields, aBase, aSecondDetail, dtMain, dtFee, false);
        }
        #endregion

    }
    #endregion

    #region 导出
    protected void btnImport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        Response.Clear();
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("销售订单") + ".xls");
        Response.Write("<html><head><META http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"></head><body>");
        Response.Write(hiddExcel.Value);
        Response.Write(tw.ToString());
        Response.Write("</body></html>");
        Response.End();
        hw.Close();
        hw.Flush();
        tw.Close();
        tw.Flush();
    }
    #endregion
}
