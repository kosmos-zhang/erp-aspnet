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

public partial class Pages_Office_SellManager_SellPlanList : BasePage
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
        if (!IsPostBack)
        {
            btnImport.Attributes["onclick"] = "return fnIsSearch();";
            GetBillExAttrControl1.TableName = "officedba.SellPlan";
            // 小数位数
            _selPoint = int.Parse(UserInfo.SelPoint);
        }
    }

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        //设置行为参数
        string orderString = hiddExpOrder.Value.Trim();//排序
        string order = "desc";//排序：降序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：升序
        }
        int pageCount = int.Parse(hiddExpTotal.Value);//每页显示记录数
        int pageIndex = 1;//当前页     
        int TotalCount = 0;//总记录数
        string ord = orderBy + " " + order;//排序字段
        DataTable dt = new DataTable();
        string strPlanNo = hiddExpOrderNo.Value.Trim();
        string strTitle = hiddExpTitle.Value.Trim();
        string strPlanType = hiddExpFromType.Value.Trim();
        string strBillStatus = hiddExpBillStatus.Value.Trim();   
        string strFlowStatus = hiddExpFlowStatus.Value.Trim();

        string OfferNo = strPlanNo.Length == 0 ? null : strPlanNo;
        string Title = strTitle.Length == 0 ? null : strTitle;
        string PlanType = strPlanType.Length == 0 ? null : strPlanType;
        string BillStatus = strBillStatus.Length == 0 ? null : strBillStatus;
        int? FlowStatus = strFlowStatus.Length == 0 ? null : (int?)Convert.ToInt32(strFlowStatus);

        string EFIndex = hidEFIndex.Value;
        string EFDesc = hidEFDesc.Value;

        SellPlanModel model = new SellPlanModel();
        model.BillStatus = BillStatus;
        model.PlanType = PlanType;
        model.PlanNo = strPlanNo;
        model.Title = Title;
        dt = SellPlanBus.GetOrderList(EFIndex,EFDesc, model, FlowStatus, pageIndex, pageCount, ord, ref TotalCount);

        //导出标题
        string headerTitle = "计划编号|计划名称|计划类型|计划时期|最低计划额(元)|计划总金额(元)|单据状态|审批状态";
        //string headerTitle = "建档日期|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "PlanNo|Title|PlanTypeText|PlanDate|MinPlanTotal|PlanTotal|BillStatusText|FlowInstanceText";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "销售计划列表");
    }
}
