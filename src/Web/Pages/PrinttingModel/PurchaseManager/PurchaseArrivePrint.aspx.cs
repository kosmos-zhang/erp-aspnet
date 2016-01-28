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
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
using System.Collections.Generic;

public partial class Pages_PrinttingModel_PurchaseManager_PurchaseArrivePrint : BasePage
{
    #region MRP ID
    public int intMrpID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
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

    /// <summary>
    /// 主表字段
    /// </summary>
    private string _sbase = "";

    /// <summary>
    /// 主表字段
    /// </summary>
    public string SBase
    {
        get
        {
            return _sbase;
        }
    }


    /// <summary>
    /// 明细表字段
    /// </summary>
    private string _sdetail = "";

    /// <summary>
    /// 明细表字段
    /// </summary>
    public string SDetail
    {
        get
        {
            return _sdetail;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        UserInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        _isMoreUnit = UserInfo.IsMoreUnit;
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_PURCHASE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_PurchaseArrive.ToString();

        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = UserInfo.CompanyCD;
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_PURCHASE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_PurchaseArrive;

        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("ExtField1", "{ExtField1}");
        dic.Add("ExtField2", "{ExtField2}");
        dic.Add("ExtField3", "{ExtField3}");
        dic.Add("ExtField4", "{ExtField4}");
        dic.Add("ExtField5", "{ExtField5}");
        dic.Add("ExtField6", "{ExtField6}");
        dic.Add("ExtField7", "{ExtField7}");
        dic.Add("ExtField8", "{ExtField8}");
        dic.Add("ExtField9", "{ExtField9}");
        dic.Add("ExtField10", "{ExtField10}");
        dic.Add("ArriveNo", "单据编号");
        dic.Add("Title", "主题");
        dic.Add("TypeIDName", "采购类别");
        dic.Add("ProviderName", "供应商");
        dic.Add("DeptName", "部门");
        dic.Add("CheckUserName", "点收人");
        dic.Add("PurchaserName", "采购员");
        dic.Add("CheckDate", "点收日期");
        dic.Add("FromTypeName", "源单类型");
        dic.Add("MoneyTypeName", "支付方式");
        dic.Add("SendAddress", "发货地址");
        dic.Add("ReceiveOverAddress", "收货地址");
        dic.Add("isAddTaxName", "是否为增值税");
        dic.Add("TakeTypeName", "交货方式");
        dic.Add("CarryTypeName", "运送方式");
        dic.Add("PayTypeName", "结算方式");
        dic.Add("CurrencyTypeName", "币种");
        dic.Add("Rate", "汇率");
        dic.Add("ArriveDate", "到货时间");
        dic.Add("ProjectName", "所属项目");


        dic.Add("CountTotal", "数量总计");
        if (UserInfo.IsDisplayPrice)
        {// 出入库显示单价
            dic.Add("TotalMoney", "金额总计");
            dic.Add("TotalTax", "税额合计");
            dic.Add("TotalFee", "含税总额总计");
            dic.Add("Discount", "整单折扣");
            dic.Add("DiscountTotal", "折扣金额");
            dic.Add("RealTotal", "折后含税额");
            dic.Add("OtherTotal", "其他费用支出合计");
        }


        dic.Add("BillStatusName", "单据状态");
        dic.Add("CreatorName", "制单人");
        dic.Add("CreateDate", "制单日期");
        dic.Add("ConfirmorName", "确认人");
        dic.Add("ConfirmDate", "确认日期");
        dic.Add("CloserName", "结单人");
        dic.Add("CloseDate", "结单日期");
        dic.Add("ModifiedUserID", "最后更新人");
        dic.Add("ModifiedDate", "最后更新日期");
        dic.Add("Remark", "备注");
        string[,] aBase = ConvertDictionaryToString(dic, out _sbase);

        dic.Clear();
        dic.Add("SortNo", "序号");
        dic.Add("ProductNo", "物品编号");
        dic.Add("ProductName", "物品名称");
        dic.Add("Specification", "规格");
        dic.Add("ColorName", "颜色");
        if (_isMoreUnit)
        {// 启用多计量单位
            dic.Add("UnitName", "基本单位");
            dic.Add("ProductCount", "基本数量");
            dic.Add("UsedUnitName", "单位");
            dic.Add("UsedUnitCount", "到货数量");
        }
        else
        {
            dic.Add("UnitName", "单位");
            dic.Add("ProductCount", "到货数量");
        }
        if (UserInfo.IsDisplayPrice)
        {// 出入库显示单价
            dic.Add("TotalPrice", "金额");
            dic.Add("UnitPrice", "单价");
            dic.Add("TaxPrice", "含税价");
            dic.Add("TaxRate", "税率");
            dic.Add("TotalFee", "含税金额");
            dic.Add("TotalTax", "税额");
        }

        string[,] aDetail = ConvertDictionaryToString(dic, out _sdetail);

        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(UserInfo.CompanyCD, "", "officedba." + ConstUtil.CODING_RULE_TABLE_PURCHASEARRIVE);
        if (dtExtTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtExtTable.Rows.Count; i++)
            {
                for (int x = 0; x < (aBase.Length / 2) - 15; x++)
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
        DataTable dtMRP = PurchaseArriveBus.SelectArrive(this.intMrpID);
        DataTable dtDetail = PurchaseArriveBus.Details(this.intMrpID);

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
                    strBaseFields = strBaseFields + "|" + "ExtField" + (i + 1);
                }
            }
            strDetailFields = GetDefaultFields(aDetail);
        }

        #region 2.主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("采购到货", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, true);
        }
        #endregion

        #region 3.明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("采购到货", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, false);
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

    /// <summary>
    /// 字典转换成字符串数组
    /// </summary>
    /// <param name="dic"></param>
    /// <param name="str"></param>
    /// <returns></returns>
    private string[,] ConvertDictionaryToString(Dictionary<string, string> dic, out string str)
    {
        StringBuilder sb = new StringBuilder();
        string[,] a = new string[dic.Count, 2];
        int i = 0;
        foreach (string item in dic.Keys)
        {
            a[i, 0] = dic[item];
            a[i, 1] = item;
            sb.AppendFormat("'{0}',", item);
            i++;
        }
        str = sb.ToString().Trim(',');
        return a;
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("采购到货") + ".xls");
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
