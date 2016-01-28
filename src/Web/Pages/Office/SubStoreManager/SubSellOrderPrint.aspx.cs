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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using XBase.Business.Office.SubStoreManager;
using XBase.Model.Office.SubStoreManager;
using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;

/// <summary>
/// 销售订单打印界面
/// </summary>
public partial class Pages_Office_SubStoreManager_SubSellOrderPrint : BasePage
{
    #region 销售订单ID
    public string BillID
    {
        get
        {
            return Convert.ToString(Request["BillID"]);
        }
    }
    #endregion

    /// <summary>
    /// 是否启用多计量单位(true：启用；false：不启用)
    /// </summary>
    private bool _isMoreUnit = false;


    /// <summary>
    /// 是否启用多计量单位(ture：启用；false：不启用)
    /// </summary>
    public bool IsMoreUnit
    {
        get
        {
            return _isMoreUnit;
        }
        set
        {
            _isMoreUnit = value;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        UserInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        _isMoreUnit = UserInfo.IsMoreUnit;
        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel pModel = new PrintParameterSettingModel();
        pModel.CompanyCD = UserInfo.CompanyCD;
        pModel.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_SUBSTORAGE);
        pModel.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_SUBSELLORDER;


        #region 初始化 取基本信息及明细信息的字段以及对应的标题
        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase = { 
                                { "{ExtField1}", "ExtField1"},
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
                                { "销售分店", "DeptName" },
                                { "发货模式", "SendModeName" },
                                { "业务员", "SellerName" },
                                { "客户名称", "CustName" },
                                { "客户联系电话", "CustTel" },
                                { "客户手机号", "CustMobile" },
                                { "下单日期", "OrderDate" },
                                { "币种", "CurrencyName" },
                                { "汇率", "Rate" },
                                { "订货方式", "OrderMethodTypeName" },
                                { "交货方式", "TakeTypeTypeName" },
                                { "结算方式", "PayTypeTypeName" },
                                { "支付方式", "MoneyTypeTypeName" },
                                { "是否为增值税", "isAddTax" },
                                { "预约发货时间", "PlanOutDate" },
                                { "运送方式", "CarryTypeTypeName" },
                                { "配送部门", "OutDeptName" },
                                { "实际发货时间", "OutDate" },
                                { "发货人", "OutUserName" },
                                { "业务状态", "BusiStatusName" },
                                { "发货地址", "CustAddr" },
                                { "是否需要安装", "NeedSetup" },
                                { "预约安装时间", "PlanSetDate" },
                                { "实际安装时间", "SetDate" },
                                { "安装工人及联系电话", "SetUserInfo" },
                                { "数量总计", "CountTotal" },
                                { "金额合计", "TotalPrice" },
                                { "税额合计", "Tax" },
                                { "含税金额合计", "TotalFee" },
                                { "整单折扣", "Discount" },
                                { "折扣金额合计", "DiscountTotal" },
                                { "折后含税金额", "RealTotal" },
                                { "已结金额", "PayedTotal" },
                                { "货款余额", "WairPayTotal" },
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "单据状态", "BillStatusName"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "结单人", "CloserName"},
                                { "结单日期", "CloseDate"},
                                { "最后更新日期", "ModifiedDate"},
                                { "备注", "Remark"},
                                { "是否已建单", "isOpenbill"},
                                { "结算人", "SttlUserName"},
                                { "结算时间", "SttlDate"},
                          };

        string[,] aDetail = { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName"}, 
                                { "批次", "BatchNo"}, 
                                { "规格", "Specification"}, 
                                { "仓库", "StorageID"}, 
                                { "数量", "ProductCount"}, 
                                { "退货数量", "BackCount"}, 
                                { "单位", "UnitName"}, 
                                { "单价", "UnitPrice"}, 
                                { "含税价", "TaxPrice"}, 
                                { "折扣", "Discount"}, 
                                { "税率", "TaxRate"}, 
                                { "金额", "TotalPrice"}, 
                                { "含税金额", "TotalFee"}, 
                                { "税额", "TotalTax"}
                           };
        if (_isMoreUnit)
        {// 启用多计量单位
            aDetail = new string[,]
                            { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName"}, 
                                { "批次", "BatchNo"}, 
                                { "规格", "Specification"},
                                { "基本单位", "UnitName" },
                                { "基本数量", "ProductCount"}, 
                                { "仓库", "StorageID"}, 
                                { "数量", "UsedUnitCount"}, 
                                { "退货数量", "BackCount"}, 
                                { "单位", "UsedUnitName"}, 
                                { "单价", "UnitPrice"}, 
                                { "含税价", "TaxPrice"}, 
                                { "折扣", "Discount"}, 
                                { "税率", "TaxRate"}, 
                                { "金额", "TotalPrice"}, 
                                { "含税金额", "TotalFee"}, 
                                { "税额", "TotalTax"}
                           };
        }
        #endregion

        #region 1.扩展属性
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(UserInfo.CompanyCD, "", "officedba.SubSellOrder");
        int countExt = 0;
        for (int i = 0; i < dtExtTable.Rows.Count; i++)
        {
            aBase[i, 0] = dtExtTable.Rows[i]["EFDesc"].ToString();
            countExt++;
        }
        #endregion

        #region 2.所设的打印模板设置
        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(pModel);
        DataTable dt = SubSellOrderBus.GetSubSellOrderPrint(BillID);
        DataTable dtDetail = SubSellOrderBus.GetSubSellOrderDetailPrint(BillID);

        string strBaseFields = "";
        string strDetailFields = "";

        if (dbPrint.Rows.Count > 0)
        {
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
            strDetailFields = dbPrint.Rows[0]["DetailFields"].ToString();
        }
        else
        {
            isSeted.Value = "0";
            strBaseFields = GetDefaultFields(aBase);
            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields += "|ExtField" + (i + 1);
                }
            }
            strDetailFields = GetDefaultFields(aDetail);
        }
        #endregion

        #region 3.主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("销售订单", strBaseFields, strDetailFields, aBase, aDetail, dt, dtDetail, true);
        }
        #endregion

        #region 4.明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("销售订单", strBaseFields, strDetailFields, aBase, aDetail, dt, dtDetail, false);
        }
        #endregion

    }

    /// <summary>
    /// 获得默认字段
    /// </summary>
    /// <param name="a">二维字符串数组</param>
    /// <returns></returns>
    private string GetDefaultFields(string[,] aBase)
    {
        StringBuilder sb = new StringBuilder();
        int len = aBase.Length / 2;
        for (int i = 0; i < len; i++)
        {
            if (aBase[i, 1].StartsWith("ExtField"))
            {
                continue;
            }
            sb.AppendFormat("{0}|", aBase[i, 1]);
        }
        return sb.ToString().TrimEnd('|');
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("分店销售订单录入") + ".xls");
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
