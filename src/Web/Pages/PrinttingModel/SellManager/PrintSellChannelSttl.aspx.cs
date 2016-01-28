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

public partial class Pages_PrinttingModel_SellManager_PrintSellChannelSttl : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_SALE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_SELLCHANNELSTTL.ToString();
        hiddckSellSttlNo.Value = Request.QueryString["no"].ToString();

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
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_SELLCHANNELSTTL;

        //    /*接受参数*/
        string SttlNo = Request.QueryString["no"].ToString();

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
                                { "单据编号", "SttlNo"}, 
                                { "主题", "Title"}, 
                                { "源单类型", "FromTypeText" },
                                { "源单编号", "SendNo" },
                                { "客户名称", "CustName"},
                                { "客户电话", "Tel"},
                                { "结算方式", "PayTypeName"},
                                { "支付方式", "MoneyTypeName"},
                                { "币种", "CurrencyName"},
                                { "汇率", "Rate"},
                                { "业务员", "SellerName"},
                                { "部门", "DeptName"},
                                { "结算日期", "SttlDate"},
                                { "代销金额", "TotalFee"},
                                { "代销提成率(%)", "PushMoneyPercent"},
                                { "代销提成额", "PushMoney"},
                                { "手续费合计", "HandFeeTotal"},
                                { "应结算金额合计", "SttlTotal"},
                                { "代销数量合计", "CountTotal"},
                                { "单据状态", "BillStatusText"},
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserName"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},
                                { "建单情况", "IsOpenbillText"},
                                { "回款状态", "IsAccText"},
                                { "已回款金额", "YAccounts"},
                              
                          };

        string[,] aDetail;
        //多计量单位
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        {
            aDetail=new string[,] { 
                                //{ "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "基本单位", "UnitName" },
                                { "基本数量", "SttlNumber"},
                                { "单位", "UsedUnitName" },
                                { "代销数量", "ProductCount"},
                                { "已结算数量", "SttlCount" },
                                { "本次结算数量", "UsedUnitCount"},
                                { "单价", "UsedPrice"},
                                { "本次结算代销金额", "TotalPrice"},
                                { "备注", "Remark"},
                           };
        }
        else
        { 
            aDetail=new string[,] { 
                                //{ "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "单位", "UnitName" },
                                { "代销数量", "ProductCount"},
                                { "已结算数量", "SttlCount" },
                                { "本次结算数量", "SttlNumber"},
                                { "单价", "UnitPrice"},
                                { "本次结算代销金额", "TotalPrice"},
                                { "备注", "Remark"},
                           };
        }
        

        #endregion

        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.SellChannelSttl");
        if (dtExtTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtExtTable.Rows.Count; i++)
            {
                for (int x = 0; x < (aBase.Length / 2) - 31; x++)
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
        DataTable dtMain = SellChannelSttlBus.GetRepOrder(SttlNo);//获取主表数据
        DataTable dtDetail = SellChannelSttlBus.GetRepOrderDetail(SttlNo);//获取明细表数据
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
            strBaseFields = "SttlNo|Title|FromTypeText|SendNo|CustName|Tel|PayTypeName|MoneyTypeName|CurrencyName|Rate|";
            strBaseFields = strBaseFields + "SellerName|DeptName|SttlDate|TotalFee|PushMoneyPercent|PushMoney|HandFeeTotal|SttlTotal|CountTotal|";
            strBaseFields = strBaseFields + "BillStatusText|CreatorName|CreateDate|ConfirmorName|ConfirmDate|CloserName|CloseDate|ModifiedUserID|ModifiedDate|Remark|";
            strBaseFields = strBaseFields + "IsOpenbillText|IsAccText|YAccounts";
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
                strDetailFields = "ProdNo|ProductName|Specification|UnitName|ProductCount|UsedUnitName|UsedUnitCount|SttlCount|SttlNumber|UsedPrice|TotalPrice|Remark";
            }
            else
            { 
               /*订单明细SortNo|*/
                strDetailFields = "ProdNo|ProductName|Specification|UnitName|ProductCount|SttlCount|SttlNumber|UnitPrice|TotalPrice|Remark"; 
            }
            
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
            tableBase.InnerHtml = WritePrintPageTable("委托代销结算单", strBaseFields, strDetailFields, aBase, aDetail, dtMain, dtDetail, true);
        }
        #endregion

        #region 3.明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("委托代销结算单", strBaseFields, strDetailFields, aBase, aDetail, dtMain, dtDetail, false);
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("委托代销结算单") + ".xls");
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
