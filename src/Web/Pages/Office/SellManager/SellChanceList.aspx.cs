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
using XBase.Business.Office.SellManager;
using XBase.Model.Office.SellManager;
public partial class Pages_Office_SellManager_SellChanceList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            chanceTypeUC.TypeFlag = ConstUtil.SELL_TYPE_SELL;//销售机会类型
            chanceTypeUC.TypeCode = ConstUtil.SELL_TYPE_CHANCETYPE;//销售机会类型
            chanceTypeUC.IsInsertSelect = true;
            HapSourceUC.TypeFlag = ConstUtil.SELL_TYPE_SELL;//销售机会来源
            HapSourceUC.TypeCode = ConstUtil.SELL_TYPE_HAPSOURCE;//销售机会来源
            HapSourceUC.IsInsertSelect = true;

            btnImport.Attributes["onclick"] = "return fnIsSearch();";
            GetBillExAttrControl1.TableName = "officedba.SellChance";
        }
    }

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        //设置行为参数
        string orderString = hiddExpOrder.Value.Trim();//排序
        string order = "desc";//排序：降序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "FindDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：升序
        }
        int pageCount = int.Parse(hiddExpTotal.Value);//每页显示记录数
        int pageIndex = 1;//当前页     
        int TotalCount = 0;//总记录数
        string ord = orderBy + " " + order;//排序字段
        DataTable dt = new DataTable();

        string strChanceNo = hiddExpChanceNo.Value.Trim();
        string strTitle = hiddExpTitle.Value.Trim();
        string strCustID = hiddExpCustID.Value.Trim();
        string strSeller = hiddExpSeller.Value.Trim();
        string strPhase = hiddExpPhase.Value.Trim();
        string strState = hiddExpState.Value.Trim();
        string strChanceType = hiddExpChanceType.Value.Trim();
        string strHapSource = hiddExpHapSource.Value.Trim();
        string strFindDate = hiddExpFindDate.Value.Trim();
        string strFindDate1 = hiddExpFindDate1.Value.ToString().Trim();

        string ChanceNo = strChanceNo.Length == 0 ? null : strChanceNo;
        string Title = strTitle.Length == 0 ? null : strTitle;
        int? CustID = strCustID.Length == 0 ? null : (int?)Convert.ToInt32(strCustID);
        int? Seller = strSeller.Length == 0 ? null : (int?)Convert.ToInt32(strSeller);
        string Phase = strPhase.Length == 0 ? null : strPhase;
        string State = strState.Length == 0 ? null : strState;
        int? ChanceType = strChanceType.Length == 0 ? null : (int?)Convert.ToInt32(strChanceType);
        int? HapSource = strHapSource.Length == 0 ? null : (int?)Convert.ToInt32(strHapSource);
        DateTime? FindDate = strFindDate.Length == 0 ? null : (DateTime?)Convert.ToDateTime(strFindDate);
        DateTime? FindDate1 = strFindDate1.Length == 0 ? null : (DateTime?)Convert.ToDateTime(strFindDate1);

        string EFIndex = hidEFIndex.Value;
        string EFDesc = hidEFDesc.Value;

        dt = SellChanceBus.GetChanceList(EFIndex, EFDesc, ChanceNo, Title, CustID, Seller, Phase, State, ChanceType, HapSource, FindDate, FindDate1, pageIndex, pageCount, ord, ref TotalCount);

        //导出标题
        string headerTitle = "机会编号|机会主题|客户|机会类型|机会来源|业务员|发现时间|阶段|状态";
        //string headerTitle = "建档日期|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "ChanceNo|Title|CustName|ChanceTypeName|HapSourceName|EmployeeName|FindDate|PhaseName|StateName";
        //string columnFiled = "CreateDate|strUsedStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "销售机会列表");
    }
}
