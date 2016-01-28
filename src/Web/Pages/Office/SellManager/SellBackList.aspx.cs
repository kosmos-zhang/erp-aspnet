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

public partial class Pages_Office_SellManager_SellBackList : BasePage
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
            //扩展属性
            GetBillExAttrControl1.TableName = "officedba.SellBack";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();

            DataTable dt = SellBackBus.GetReasonType();
            if (dt.Rows.Count > 0)
            {
                ddlReasonType.DataSource = dt;
                ddlReasonType.DataTextField = "CodeName";
                ddlReasonType.DataValueField = "ID";
                ddlReasonType.DataBind();
            }
            ddlReasonType.Items.Insert(0, new ListItem("请选择", ""));
            btnImport.Attributes["onclick"] = "return fnIsSearch();";
            // 小数位数
            _selPoint = int.Parse(UserInfo.SelPoint);
        }
    }

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        //设置行为参数
        string orderString = hiddExpOrder.Value.Trim();//排序
        string order = "desc";//排序：降序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "BackDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：升序
        }
        int pageCount = int.Parse(hiddExpTotal.Value);//每页显示记录数
        int pageIndex = 1;//当前页     
        int TotalCount = 0;//总记录数
        string ord = orderBy + " " + order;//排序字段
        DataTable dt = new DataTable();

        string strorderNo = hiddExpOrderNo.Value.Trim();
        string strTitle = hiddExpTitle.Value.Trim();
        string strCustID = hiddExpCustID.Value.Trim();
        string strSeller = hiddExpSeller.Value.Trim();
        string strFromType = hiddExpFromType.Value.Trim();
        string strBillStatus = hiddExpBillStatus.Value.Trim();
        string strFlowStatus = hiddExpFlowStatus.Value.Trim();
        string strReason = hiddExpReasonType.Value.Trim();
        string strSttlDate1 = hiddExpOfferDate1.Value.Trim();
        string strSttlDate = hiddExpOfferDate.Value.Trim();
        string strProjectID = hiddProjectID.Value.Trim();

        string orderNo = strorderNo.Length == 0 ? null : strorderNo;
        string Title = strTitle.Length == 0 ? null : strTitle;
        int? CustID = strCustID.Length == 0 ? null : (int?)Convert.ToInt32(strCustID);
        int? Seller = strSeller.Length == 0 ? null : (int?)Convert.ToInt32(strSeller);
        string FromType = strFromType.Length == 0 ? null : strFromType;
        string BillStatus = strBillStatus.Length == 0 ? null : strBillStatus;
        DateTime? date = strSttlDate.Length == 0 ? null : (DateTime?)Convert.ToDateTime(strSttlDate);
        DateTime? date1 = strSttlDate1.Length == 0 ? null : (DateTime?)Convert.ToDateTime(strSttlDate1);
        int? FlowStatus = strFlowStatus.Length == 0 ? null : (int?)Convert.ToInt32(strFlowStatus);
        int? Reason = strReason.Length == 0 ? null : (int?)Convert.ToInt32(strReason);
        int? ProjectID = strProjectID.Length == 0 ? null : (int?)Convert.ToInt32(strProjectID);
        //扩展属性
        string EFIndex = Request.QueryString["EFIndex"];
        string EFDesc = Request.QueryString["EFDesc"];
        GetBillExAttrControl1.ExtIndex = EFIndex;
        GetBillExAttrControl1.ExtValue = EFDesc;
        GetBillExAttrControl1.SetExtControlValue();

        SellBackModel model = new SellBackModel();
        model.BillStatus = BillStatus;
        model.Seller = Seller;
        model.BackDate = date;

        model.FromType = FromType;
        model.BackNo = orderNo;
        model.CustID = CustID;
        model.Title = Title;
        model.ProjectID = ProjectID;
        dt = SellBackBus.GetOrderList(model, date1, Reason, FlowStatus,EFIndex,EFDesc, pageIndex, pageCount, ord, ref TotalCount);

        //导出标题
        string headerTitle = "单据编号|单据主题|源单类型|客户|业务员|退货日期|总金额|单据状态|审批状态";
        //string headerTitle = "建档日期|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "BackNo|Title|FromTypeText|CustName|EmployeeName|BackDate|TotalPrice|BillStatusText|FlowInstanceText";
        //string columnFiled = "CreateDate|strUsedStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "销售退货单列表");
    }
}
