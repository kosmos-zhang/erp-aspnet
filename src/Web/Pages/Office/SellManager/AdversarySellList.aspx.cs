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

public partial class Pages_Office_SellManager_AdversarySellList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnImport.Attributes["onclick"] = "return fnIsSearch();";
        }
    }

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        //设置行为参数
        string orderString = hiddExpOrder.Value.Trim();//排序
        string order = "desc";//排序：降序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreatDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：升序
        }
        int pageCount = int.Parse(hiddExpTotal.Value);//每页显示记录数
        int pageIndex = 1;//当前页     
        int TotalCount = 0;//总记录数
        string ord = orderBy + " " + order;//排序字段
        DataTable dt = new DataTable();

        AdversarySellModel adversarySellModel = new AdversarySellModel();
        string strCustNo = hiddExpCustNo.Value.Trim();
        string strChanceID = hiddExpChanceID.Value.Trim();
        string strCustID = hiddExpCustID.Value.Trim();



        string CustNo = strCustNo.Length == 0 ? null : strCustNo;
        int? ChanceID = strChanceID.Length == 0 ? null : (int?)Convert.ToInt32(strChanceID);
        int? CustID = strCustID.Length == 0 ? null : (int?)Convert.ToInt32(strCustID);

        adversarySellModel.CustNo = CustNo;
        adversarySellModel.ChanceID = ChanceID;
        adversarySellModel.CustID = CustID;


        dt = AdversarySellBus.GetOrderList(adversarySellModel, pageIndex, pageCount, ord, ref TotalCount);

        //导出标题
        string headerTitle = "对手编号|对手名称|竞争客户|销售机会|创建人|创建日期";
        //string headerTitle = "建档日期|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "CustNo|CustName|CustName1|ChanceNo|EmployeeName|CreatDate";
        //string columnFiled = "CreateDate|strUsedStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "销售竞争分析列表");

    }
}
