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
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using XBase.Model.Common;
using XBase.Common;
using System.Text;

public partial class Pages_PrinttingModel_StorageManager_StorageOutSellPrint : BasePage
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

    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_STORAGE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_OUTSELL.ToString();

        if (!IsPostBack)
        {
            HiddenPrice.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice.ToString();//是否启用显示单价金额
            HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_STORAGE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_OUTSELL;

        StorageOutSellModel OutSellM_ = new StorageOutSellModel();
        OutSellM_.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        OutSellM_.ID = this.intMrpID.ToString();

        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase;
        string[,] aDetail;
        if (HiddenPrice.Value == "True")
        {
             aBase =new string[,]{ 
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
                                { "出库单编号", "OutNo"}, 
                                { "出库单主题", "Title"}, 
                                { "源单类型", "FromTypeName" },
                                { "销售发货通知单", "FromBillNo" },
                                { "客户名称", "CustName"},
                                { "销售部门", "SellDeptName"},
                                { "业务员", "SellerName"},
                                { "发货地址", "SendAddr"},
                                { "收货地址", "ReceiveAddr"},
                                { "经办人", "SenderName"},
                                { "出库部门", "DeptName"},
                                { "出库人", "TransactorName"},
                                { "出库时间", "OutDate"},
                                { "数量合计", "CountTotal"},
                                { "金额合计", "A_TotalPrice"},
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserName"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserName"},
                                { "最后更新日期", "ModifiedDate"},
                                { "单据状态", "BillStatusName"},
                                { "备注", "Remark"},
                                { "摘要", "Summary"},

                          };
             if (HiddenMoreUnit.Value == "True")
             {
                 aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName" },
                                { "批次", "BatchNo" },
                                { "规格", "Specification" },
                                { "包装要求", "Package" },
                                { "基本单位", "UnitID" },
                                { "基本数量", "ProductCount" },
                                { "单位", "UsedUnitName"},
                                { "仓库", "StorageName"},
                                { "通知数量", "FromBillCount"},
                                { "已出库数量", "OutCount"},
                                { "未出库数量", "NotOutCount"},
                                { "出库数量", "UsedUnitCount"},
                                { "出库单价", "UsedPrice"},
                                { "出库金额", "B_TotalPrice"},
                                { "备注", "DetaiRemark"},
                           };
             }
             else 
             {
                 aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName" },
                                { "批次", "BatchNo" },
                                { "规格", "Specification" },
                                { "包装要求", "Package" },
                                { "单位", "UnitID"},
                                { "仓库", "StorageName"},
                                { "通知数量", "FromBillCount"},
                                { "已出库数量", "OutCount"},
                                { "未出库数量", "NotOutCount"},
                                { "出库数量", "ProductCount"},
                                { "出库单价", "UnitPrice"},
                                { "出库金额", "B_TotalPrice"},
                                { "备注", "DetaiRemark"},
                           };
             }
        }
        else 
        {
            aBase =new string[,]{ 
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
                                { "出库单编号", "OutNo"}, 
                                { "出库单主题", "Title"}, 
                                { "源单类型", "FromTypeName" },
                                { "销售发货通知单", "FromBillNo" },
                                { "客户名称", "CustName"},
                                { "销售部门", "SellDeptName"},
                                { "业务员", "SellerName"},
                                { "发货地址", "SendAddr"},
                                { "收货地址", "ReceiveAddr"},
                                { "经办人", "SenderName"},
                                { "出库部门", "DeptName"},
                                { "出库人", "TransactorName"},
                                { "出库时间", "OutDate"},
                                { "数量合计", "CountTotal"},
                                //{ "金额合计", "A_TotalPrice"},
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserName"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserName"},
                                { "最后更新日期", "ModifiedDate"},
                                { "单据状态", "BillStatusName"},
                                { "备注", "Remark"},
                                { "摘要", "Summary"},

                          };
            if (HiddenMoreUnit.Value == "True")
            {
                aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName" },
                                { "批次", "BatchNo" },
                                { "规格", "Specification" },
                                { "包装要求", "Package" },
                                { "基本单位", "UnitID" },
                                { "基本数量", "ProductCount" },
                                { "单位", "UsedUnitName"},
                                { "仓库", "StorageName"},
                                { "通知数量", "FromBillCount"},
                                { "已出库数量", "OutCount"},
                                { "未出库数量", "NotOutCount"},
                                { "出库数量", "UsedUnitCount"},
                                { "出库单价", "UsedPrice"},
                                { "出库金额", "B_TotalPrice"},
                                { "备注", "DetaiRemark"},
                           };
            }
            else
            {
                aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName" },
                                { "批次", "BatchNo" },
                                { "规格", "Specification" },
                                { "包装要求", "Package" },
                                { "单位", "UnitID"},
                                { "仓库", "StorageName"},
                                { "通知数量", "FromBillCount"},
                                { "已出库数量", "OutCount"},
                                { "未出库数量", "NotOutCount"},
                                { "出库数量", "ProductCount"},
                                { "出库单价", "UnitPrice"},
                                { "出库金额", "B_TotalPrice"},
                                { "备注", "DetaiRemark"},
                           };
            }
        }


        
        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = XBase.Business.Office.SupplyChain.TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.StorageOutSell");
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
        DataTable dtMain = StorageOutSellBus.GetStorageOutSellDetailInfo(OutSellM_);
       // DataTable dtDetail = MRPBus.GetMRPDetailInfo(modelMRP);
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
            #region 未设置过打印模板设置 默认显示所有的
            isSeted.Value = "0";

            /*未设置过打印模板设置时，默认显示的字段  基本信息字段*/
            for (int m = 10; m < aBase.Length / 2; m++)
            {
                strBaseFields = strBaseFields + aBase[m, 1] + "|";
            }
            /*未设置过打印模板设置时，默认显示的字段 基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "ExtField" + (i + 1) + "|";
                }
            }
            /*未设置过打印模板设置时，默认显示的字段 明细信息字段*/
            for (int n = 0; n < aDetail.Length / 2; n++)
            {
                strDetailFields = strDetailFields + aDetail[n, 1] + "|";
            }
            #endregion
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("销售出库单", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aDetail, dtMain, dtMain, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("销售出库单", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aDetail, dtMain, dtMain, false);
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("销售出库单") + ".xls");
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
