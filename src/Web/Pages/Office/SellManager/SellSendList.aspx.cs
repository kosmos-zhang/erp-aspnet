
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

public partial class Pages_Office_SellManager_SellSendList : BasePage
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
            TakeType.TypeFlag = ConstUtil.SELL_TYPE_SELL;//交货方式
            TakeType.TypeCode = ConstUtil.SELL_TYPE_TAKETYPE;//交
            TakeType.IsInsertSelect = true;
            //扩展属性
            GetBillExAttrControl1.TableName = "officedba.SellSend";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();
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
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "SendNo";//要排序的字段，如果为空，默认为"ID"
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
        string strTakeType = hiddExpTakeType.Value.Trim();
        string strSender = hiddExpSender.Value.Trim();
        string strFromType = "1";
        string strBillStatus = hiddExpBillStatus.Value.Trim();
        string strReceiver = hiddExpReceiver.Value.Trim();
        string strFlowStatus = hiddExpFlowStatus.Value.Trim();
        string strFromBillID = hiddExpFromBillID.Value.Trim();
        string strSeller = hiddExpSeller.Value.Trim();
        string strProjectID = hiddProjectID.Value.Trim();

        string orderNo = strorderNo.Length == 0 ? null : strorderNo;
        string Title = strTitle.Length == 0 ? null : strTitle;
        string Receiver = strReceiver.Length == 0 ? null : strReceiver;
        int? TakeType = strTakeType.Length == 0 ? null : (int?)Convert.ToInt32(strTakeType);
        int? Sender = strSender.Length == 0 ? null : (int?)Convert.ToInt32(strSender);
        string FromType = strFromType.Length == 0 ? null : strFromType;
        string BillStatus = strBillStatus.Length == 0 ? null : strBillStatus;
        int? FlowStatus = strFlowStatus.Length == 0 ? null : (int?)Convert.ToInt32(strFlowStatus);
        int? FromBillID = strFromBillID.Length == 0 ? null : (int?)Convert.ToInt32(strFromBillID);
        int? Seller = strSeller.Length == 0 ? null : (int?)Convert.ToInt32(strSeller);
        int? ProjectID = strProjectID.Length == 0 ? null : (int?)Convert.ToInt32(strProjectID);
        //扩展属性
        string EFIndex = Request.QueryString["EFIndex"];
        string EFDesc = Request.QueryString["EFDesc"];
        GetBillExAttrControl1.ExtIndex = EFIndex;
        GetBillExAttrControl1.ExtValue = EFDesc;
        GetBillExAttrControl1.SetExtControlValue();

        SellSendModel model = new SellSendModel();
        model.BillStatus = BillStatus;
        model.TakeType = TakeType;
        model.Receiver = Receiver;
        model.FromBillID = FromBillID;
        model.FromType = FromType;
        model.SendNo = orderNo;
        model.Sender = Sender;
        model.Title = Title;
        model.Seller = Seller;
        model.ProjectID = ProjectID;
        dt = SellSendBus.GetOrderList(model, FlowStatus,EFIndex,EFDesc, pageIndex, pageCount, ord, ref TotalCount);

        //导出标题
        string headerTitle = "发货单编号|发货单主题|销售订单|客户|业务员|发货人|收货人|交货方式|单据状态|审批状态";
        //string headerTitle = "建档日期|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "SendNo|Title|OrderNo|CustName|SellerName|Receiver|SenderName|TypeName|BillStatusText|FlowInstanceText";
        //string columnFiled = "CreateDate|strUsedStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "销售发货单列表");
    }
}
