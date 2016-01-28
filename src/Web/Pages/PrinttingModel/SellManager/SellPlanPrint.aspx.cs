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

using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Business.Office.SellManager;
using XBase.Business.Office.SupplyChain;

public partial class Pages_PrinttingModel_SellManager_SellPlanPrint : BasePage
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

    public string PlanNo
    {
        get
        {               
            return  Request["no"].ToString();
        }
    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_SALE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_SELLPLAN.ToString();
        hidPlanNo.Value = PlanNo;

        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_SALE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_SELLPLAN;

        SellPlanModel modelMRP = new SellPlanModel();
        modelMRP.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelMRP.ID = this.intMrpID;

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

                                { "计划编号", "PlanNo"}, 
                                { "计划名称", "Title"}, 
                                { "计划类型", "PlanType" },

                                { "计划时期", "PlanTime" },
                                { "开始时间", "StartDate"},
                                { "结束时间", "EndDate"},

                                { "最低计划额(元)", "MinPlanTotal"},
                                { "计划总金额(元)", "PlanTotal"},

                                { "激励方案", "Hortation"},

                                { "可查看人员", "CanViewUserName"},

                                { "单据状态", "BillStatus"},
                                { "制单人", "Creator"},
                                { "制单日期 ", "CreateDate"},

                                { "确认人", "Confirmor"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "Closer"},

                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},

                                { "备注", "Remark"},
                          };

        string[,] aDetail = {
                                { "部门/物品/人员", "DetailTypeName"}, 
                                { "名称", "DetailName"}, 
                                { "最低目标额(元)", "MinDetailotal"}, 
                                { "目标额(元)", "DetailTotal" },
                                { " 完成情况", "AddOrCutText2" },
                                { "目标达成率", "CompletePercent"},
                           };

        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.SellPlan");
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
        DataTable dtMRP = SellPlanBus.GetRepOrder(PlanNo); //主表 MRPBus.GetMRPInfo(modelMRP);
        DataTable dtDetail = SellPlanBus.GetOrderDetail(PlanNo); //明细表 MRPBus.GetMRPDetailInfo(modelMRP);
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
            strBaseFields = "PlanNo|Title|PlanType|PlanTime|StartDate|EndDate|MinPlanTotal|PlanTotal|Hortation|CanViewUserName|BillStatus|Creator|CreateDate|Confirmor|ConfirmDate|Closer|CloseDate|ModifiedUserID|ModifiedDate|Remark";
            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "|" + "ExtField" + (i + 1);
                }
            }

            strDetailFields = "DetailTypeName|DetailName|MinDetailotal|DetailTotal|AddOrCutText2|CompletePercent";           
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("销售计划", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("销售计划", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, false);
        }
        #endregion

    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region 导出
    protected void btnImport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        Response.Clear();
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("销售计划") + ".xls");
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
