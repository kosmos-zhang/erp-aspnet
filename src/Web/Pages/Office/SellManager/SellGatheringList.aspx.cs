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

public partial class Pages_Office_SellManager_SellGatheringList : BasePage
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
            GetBillExAttrControl1.TableName = "officedba.SellGathering";
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
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "GatheringNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：升序
        }
        int pageCount = int.Parse(hiddExpTotal.Value);//每页显示记录数
        int pageIndex = 1;//当前页     
        int TotalCount = 0;//总记录数
        string ord = orderBy + " " + order;//排序字段
        DataTable dt = new DataTable();

        SellGatheringModel sellGatheringModel = new SellGatheringModel();
        GetSellGatheringModel(sellGatheringModel);
        string PlanPrice0 = hiddExpPlanPrice0.Value.Trim();      //计划回款金额  
        PlanPrice0 = PlanPrice0.Length == 0 ? null : PlanPrice0;
        string EFIndex = Request.QueryString["EFIndex"];
        string EFDesc = Request.QueryString["EFDesc"];
        GetBillExAttrControl1.ExtIndex = EFIndex;
        GetBillExAttrControl1.ExtValue = EFDesc;
        GetBillExAttrControl1.SetExtControlValue();
       
        dt = SellGatheringBus.GetSellGathering(sellGatheringModel, PlanPrice0,EFIndex,EFDesc, pageIndex, pageCount, ord, ref TotalCount);

        //导出标题
        string headerTitle = "单据编号|主题|客户|回款状态|源单类型|源单编号|业务员|总金额|期次|计划日期";
        
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "GatheringNo|Title|CustName|stateName|fromTypeName|BillNo|EmployeeName|PlanPrice|GatheringTime|PlanGatherDate";
        //string columnFiled = "CreateDate|strUsedStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "回款计划列表");
    }

    /// <summary>
    /// 获取实体
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    private void GetSellGatheringModel(SellGatheringModel sellGatheringModel)
    {
        string GatheringNo = hiddExpGatheringNo.Value.Trim();
        string Title = hiddExpTitle.Value.Trim();         //主题                      
        string CustID = hiddExpCustID.Value.Trim();         //客户ID（关联客户信息表）                    
        string FromType = hiddExpFromType.Value.Trim();       //源单类型（0无来源，1发货通知单，2销售订单） 
        string FromBillID = hiddExpFromBillID.Value.Trim();     //源单ID                                                            
        string PlanPrice = hiddExpPlanPrice.Value.Trim();      //计划回款金额   

        string GatheringTime = hiddExpGatheringTime.Value.Trim();  //期次                                                                            
        string Seller = hiddExpSeller.Value.Trim();         //业务员(对应员工表ID)                                                                         

        if (Title.Length != 0)
        {
            sellGatheringModel.Title = Title;//主题
        }
        if (GatheringNo.Length != 0)
        {
            sellGatheringModel.GatheringNo = GatheringNo;    //回款计划编号       
        }
        if (CustID.Length != 0)
        {
            sellGatheringModel.CustID = Convert.ToInt32(CustID);         //客户ID（关联客户信息表） 
        }
        if (FromType.Length != 0)
        {
            sellGatheringModel.FromType = FromType;       //源单类型（0无来源，1发货通知单，2销售订单）
        }
        if (FromBillID.Length != 0)
        {
            sellGatheringModel.FromBillID = Convert.ToInt32(FromBillID);     //源单ID
        }
        if (PlanPrice.Length != 0)
        {
            sellGatheringModel.PlanPrice = Convert.ToDecimal(PlanPrice);      //计划回款金额 
        }
        if (GatheringTime.Length != 0)
        {
            sellGatheringModel.GatheringTime = GatheringTime;  //期次  
        }
        if (Seller.Length != 0)
        {
            sellGatheringModel.Seller = Convert.ToInt32(Seller);         //业务员(对应员工表ID)  
        }
       
            sellGatheringModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
        
    }

}
