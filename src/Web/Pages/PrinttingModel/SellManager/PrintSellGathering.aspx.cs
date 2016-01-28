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


public partial class Pages_PrinttingModel_SellManager_PrintSellGathering : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_SALE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_SELLGATHERING.ToString();
        hiddckSellGatheringNo.Value = Request.QueryString["no"].ToString();

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
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_SELLGATHERING;

        //    /*接受参数*/
        string GatheringNo = Request.QueryString["no"].ToString();

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
                                { "回款计划编号", "GatheringNo"}, 
                                { "主题", "Title"}, 
                                { "源单类型", "fromTypeName" },
                                { "源单编号", "BillNo" },
                                { "客户名称", "CustName"},
                                { "客户类型", "TypeName"},
                                { "客户电话", "Tel"},
                                { "币种", "CurrencyName"},
                                { "业务员", "SellerName"},
                                { "部门", "DeptName"},
                                { "计划回款金额", "PlanPrice"},
                                { "计划回款时间", "PlanGatherDate"},
                                { "实际回款金额", "FactPrice"},
                                { "实际回款时间", "FactGatherDate"},
                                { "回款相关单号", "LinkBillNo"},
                                { "期次", "GatheringTime"},
                                { "状态", "stateName"},
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},
                                { "备注", "Remark"},
                              
                          };

        #endregion

        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.SellGathering");
        if (dtExtTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtExtTable.Rows.Count; i++)
            {
                for (int x = 0; x < (aBase.Length / 2) - 22; x++)
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
        DataTable dtMain = SellGatheringBus.GetRepOrder(GatheringNo);
        string strBaseFields = "";

        if (dbPrint.Rows.Count > 0)
        {
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
        }
        else
        {
            isSeted.Value = "0";
            strBaseFields = "GatheringNo|Title|fromTypeName|BillNo|CustName|TypeName|Tel|CurrencyName|SellerName|DeptName|PlanPrice|PlanGatherDate|FactPrice|";
            strBaseFields = strBaseFields + "FactGatherDate|LinkBillNo|GatheringTime|stateName|CreatorName|CreateDate|ModifiedUserID|ModifiedDate|Remark";
            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "|" + "ExtField" + (i + 1);
                }
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
            tableBase.InnerHtml = WritePrintPageTable("销售回款计划", strBaseFields, "", aBase, null, dtMain, null, true);
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("销售回款计划") + ".xls");
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
