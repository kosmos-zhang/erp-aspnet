<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseContractUC.ascx.cs" Inherits="UserControl_PurchaseManager_PurchaseContractUC" %>
<div id="PurchaseContractUC">
    <!--提示信息弹出详情start-->
    <a name="pagePurContractDataList1Mark"></a>
    <iframe id="frmPurchaseContractUC" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 1000; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;"></iframe>
    <div id="divPurchaseContractUC" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closePurchaseContractUCdiv()" style="text-align: right; cursor: pointer"><img  src="../../../images/Button/Bottom_btn_close.jpg"/></a>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
        class="table">
        <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                合同编号
            </td>
            <td width="15%" bgcolor="#FFFFFF">
                <input type="text" id="txtConNoPurCon" class="tdinput" />
            </td>
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                合同主题
            </td>
            <td width="15%" bgcolor="#FFFFFF">
                <input type="text" id="txtConTtlPurCon" class="tdinput" />
            </td>
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                供应商
            </td>
            <td width="15%" bgcolor="#FFFFFF">
                <input type="text" id="txtPrvPurCon" class="tdinput" onclick="return;txtPrvPurCon_onclick()" />
            </td>
        </tr>
        <tr>
            <td width="10%" bgcolor="#E7E7E7" align="right">
                签约时间
            </td>
            <td bgcolor="#FFFFFF" style="width: 15%">
                <input type="text" id="txtStartDatePurCon" class="tdinput" readonly="readonly" onclick="WdatePicker()"/>
            </td>
            <td width="2%" bgcolor="#E7E7E7" align="right">
                ~
            </td>
            <td bgcolor="#FFFFFF" style="width: 15%">
                <input type="text" id="txtEndDatePurCon"  class="tdinput" readonly="readonly" onclick="WdatePicker()"/>
                <input type="text" id="hidPurConSltCnd" style="display:none"  class="tdinput" />
            </td>
        </tr>
        <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                    onclick='fnGetPurCon()' id="btn_search" />
            </td>
        </tr>
    </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="PurchaseContractUCTable"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurchaseContractUCOrderBy('ProviderName','spPrvPurCon');return false;">
                            供应商<span id="spPrvPurCon" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurchaseContractUCOrderBy('ContractNo','spConNoPurCon');return false;">
                            采购合同编号<span id="spConNoPurCon" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurchaseContractUCOrderBy('Title','spConTtlPurCon');return false;">
                            采购合同主题<span id="spConTtlPurCon" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurchaseContractUCOrderBy('SignDate','spSgnDatPurCon');return false;">
                            签单日期<span id="spSgnDatPurCon" class="orderTip"></span></div>
                    </th>
                </tr>
            </tbody>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
            class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="28%">
                                <div id="pagePurchaseContractUCcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="PurchaseContractUC_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divPurchaseContractUCPage">
                                    <span id="pagePurchaseContractUC_Total"></span>每页显示
                                    <input name="text" type="text"  style=" width:30px;" id="ShowPurchaseContractUCPageCount" />
                                    条 转到第
                                    <input name="text" type="text" style=" width:30px;" id="ToPurchaseContractUCPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePurchaseContractUCPageCountIndex($('#ShowPurchaseContractUCPageCount').val(),$('#ToPurchaseContractUCPage').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <!--提示信息弹出详情end-->
</div>

<script type="text/javascript">
var popPurchaseContractUCObj=new Object();
popPurchaseContractUCObj.ParentWindow = null;
popPurchaseContractUCObj.ProviderID = null;
popPurchaseContractUCObj.ContractNo = null;//合同编号

popPurchaseContractUCObj.PurchaseType = null;
popPurchaseContractUCObj.TakeType = null;
popPurchaseContractUCObj.CarryType = null;
popPurchaseContractUCObj.PayType = null;
popPurchaseContractUCObj.MoneyType = null;

popPurchaseContractUCObj.ShowList=function(ParentWindow,ProviderID)
{
//    if((ParentWindow == "Order")&&(document.getElementById("txtProviderID").value == ""))
//    {
//        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择供应商！");
//        return;
//    }
    popPurchaseContractUCObj.ParentWindow = ParentWindow;//调用该控件的父窗口的标志
    popPurchaseContractUCObj.ProviderID = ProviderID;//供应商ID
    document.getElementById("frmPurchaseContractUC").style.display="block";
    document.getElementById('divPurchaseContractUC').style.display='block';
    PurchaseContractUCTurnToPage(currentPurchaseContractUCPageIndex);
}
  
    var pagePurchaseContractUCCount = 10;//每页计数
    var totalPurchaseContractUCRecord = 0;
    var pagerPurchaseContractUCStyle = "flickr";//jPagerBar样式
    
    var currentPurchaseContractUCPageIndex = 1;
    var actionPurchaseContractUC = "";//操作
    var orderPurchaseContractUCBy = "ID";//排序字段
    var orderPurchaseContractUCByType = "ASC ";
    
    
function fnGetPurCon()
{
    var str = "";
    str += "&ContactNo="+$("#txtConNoPurCon").val();
    str += "&Title="+escape($("#txtConTtlPurCon").val());
    str += "&Provider="+escape($.trim($("#txtPrvPurCon").val()));
    str += "&StartDate="+$("#txtStartDatePurCon").val();
    str += "&EndDate="+$("#txtEndDatePurCon").val();
    $("#hidPurConSltCnd").val(str);
    
    PurchaseContractUCTurnToPage(1);
}
    
    
    //jQuery-ajax获取JSON数据
    function PurchaseContractUCTurnToPage(pageIndex)
    {
           currentPurchaseContractUCPageIndex = pageIndex;
           var URLParams = "pageIndex="+pageIndex+
                           "&pageCount="+pagePurchaseContractUCCount+
                           "&OrderBy="+orderPurchaseContractUCBy+
                           "&OrderByType="+orderPurchaseContractUCByType+
                           $("#hidPurConSltCnd").val();   
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/PurchaseContractUC.ashx',//目标地址
           cache:false,
           data: URLParams,//数据
           beforeSend:function(){$("#PurchaseContractUC_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#PurchaseContractUCTable tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioEmp_\" id=\"radioEmp_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"fnSelectPurchaseContractUC('"+item.PurchaserID+"','"+item.PurchaserName+"','"+item.DeptID+"','"+item.DeptName+"','"+item.ProviderID+"','"+item.ProviderName+"','"+item.TheyDelegate+"','"+item.OurDelegate+"','"+item.OurDelegateName+"','"+item.isAddTax+"','"+item.ContractNo+"','"+item.TypeID+"','"+item.TakeType+"','"+item.CarryType+"','"+item.PayType+"','"+item.MoneyType+"');\" />"+"</td>"+
                             "<td height='22' align='center'>"+ item.ProviderName +"</td>"+
                             "<td height='22' align='center'><a href='#' onclick=popPurchaseContractUCObj.GetPurContractDetail('"+item.ContractNo+"')>" + item.ContractNo + "</td>"+
                             "<td height='22' align='center'>"+ item.Title +"</td>"+
                             "<td height='22' align='center'>"+ item.SignDate +"</td>").appendTo($("#PurchaseContractUCTable tbody"));                             
                         }
                   });
                   
                   
                    //页码
                    ShowPageBar("PurchaseContractUC_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                        {
                            style:pagerPurchaseContractUCStyle,mark:"pagePurContractDataList1Mark",
                            totalCount:msg.totalCount,
                            showPageNumber:3,
                            pageCount:pagePurchaseContractUCCount,
                            currentPageIndex:pageIndex,
                            noRecordTip:"没有符合条件的记录",
                            preWord:"上页",
                            nextWord:"下页",
                            First:"首页",
                            End:"末页",
                            onclick:"PurchaseContractUCTurnToPage({pageindex});return false;"
                        }
                    );
                  totalPurchaseContractUCRecord = msg.totalCount;
                  $("#ShowPurchaseContractUCPageCount").val(pagePurchaseContractUCCount);
                  ShowTotalPage(msg.totalCount,pagePurchaseContractUCCount,pageIndex,$("#pagePurchaseContractUCcount"));
                  $("#ToPurchaseContractUCPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#PurchaseContractUC_Pager").show();pagePurchaseContractUCDataList1("PurchaseContractUCTable","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pagePurchaseContractUCDataList1(o,a,b,c,d)
{
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

popPurchaseContractUCObj.GetPurContractDetail = function(ContractNo)
{
}

//改变每页记录数及跳至页数
function ChangePurchaseContractUCPageCountIndex(newPageCount,newPageIndex)
{

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalPurchaseContractUCRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pagePurchaseContractUCCount=parseInt(newPageCount);
        PurchaseContractUCTurnToPage(parseInt(newPageIndex));
    }
}
//排序
function PurchaseContractUCOrderBy(orderColum,orderTip)
{
    var ordering = "d";
    var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↑");
    }
    else
    {
        
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderPurchaseContractUCBy = orderColum+"_"+ordering;
    PurchaseContractUCTurnToPage(1);
}

   
function closePurchaseContractUCdiv()
{
    document.getElementById("frmPurchaseContractUC").style.display="none";
    document.getElementById("divPurchaseContractUC").style.display="none";
}
//fnSelectPurchaseContractUC('"+item.ContractNo+"','"+item.TypeID+"','"+item.TakeType+"','"+item.CarryType+"','"+item.PayType+"'
//,'"+item.MoneyType+"','"+item.isAddTax+"','"+item.PurchaserID+"','"+item.PurchaserName+"','"+item.DeptID+"','"+item.DeptName+"'
//,'"+item.TheyDelegate+"','"+item.OurDelegate+"','"+item.OurDelegateName+"','"+item.PurchaserName+"','"+item.DeptID+"','"+item.DeptName+"'
function fnSelectPurchaseContractUC(PurchaserID,PurchaserName,DeptID,DeptName,ProviderID,ProviderName,TheyDelegate
,OurDelegate,OurDelegateName,isAddTax,ContractNo,TypeID,TakeType,CarryType,PayType,MoneyType)
{
    //清空明细
    DeleteAll();
    $("#txtPurchaserID").val(PurchaserID);
    $("#UserPurchaserName").val(PurchaserName);
    $("#txtDeptID").val(PurchaserID);
    $("#DeptName").val(PurchaserName);
    $("#txtProviderID").val(ProviderID);
    $("#txtProviderName").val(ProviderName);
    //将供应商设为不可选
    $("#txtProviderName").attr("disabled","disabled");
    //币种汇率先留着
    if(TypeID != 0)
    document.getElementById("ddlTypeID_ddlCodeType").value = TypeID;
    if(TakeType != 0)
    document.getElementById("ddlTakeType_ddlCodeType").value = TakeType;
    if(CarryType != 0)
    document.getElementById("ddlCarryType_ddlCodeType").value = CarryType;
    if(PayType != 0)
    document.getElementById("ddlPayType_ddlCodeType").value = PayType;
    if(MoneyType != 0)
    document.getElementById("ddlMoneyType_ddlCodeType").value = MoneyType;
       
    
    
    
    if(isAddTax == 0)
    {//0则不选中
        document.getElementById("chkIsAddTax").checked = false;
    }
    else
    {
        document.getElementById("chkIsAddTax").checked = true;
    }
    fnChangeAddTax();
    
    var pagePurchaseContractDetailCount = 10;
    var pagePurchaseContractDetailIndex = 1;
    var orderPurchaseContractDetailBy = "ProductID";
    var action = "GetDetail";
    var URLParams = "action="+action+
                   "&pageCount="+pagePurchaseContractDetailCount+
                   "&pageIndex="+pagePurchaseContractDetailIndex+
                   "&orderby="+orderPurchaseContractDetailBy+
                   "&ContractNo="+ContractNo+"";
   
   $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:  '../../../Handler/Office/PurchaseManager/UC/PurchaseContractUC.ashx?'+URLParams,//目标地址
       cache:false,

       beforeSend:function(){},//发送数据之前
       
       success: function(msg)
       {
            
            $.each(msg.data,function(i,item)
            {
                if(item.ProductID != null && item.ProductID != "")
                {
                    var rowID = AddOrderSignRow("From");//添加明细行
                    document.getElementById("OrderProductID"+rowID).value = item.ProductID;
                    document.getElementById("OrderProductNo"+rowID).value = item.ProductNo;
                    document.getElementById("OrderProductName"+rowID).value = item.ProductName;
                    document.getElementById("OrderSpecification"+rowID).value = item.standard;
                    document.getElementById("OrderUnitID"+rowID).value = item.UnitID;
                    document.getElementById("OrderUnitName"+rowID).value = item.UnitName;
                    
                    document.getElementById("OrderProductCount"+rowID).value = FormatAfterDotNumber(item.ProductCount-item.OrderCount,2);
                    document.getElementById("OrderUnitPrice"+rowID).value = FormatAfterDotNumber(item.UnitPrice,2);
                    document.getElementById("OrderTaxPrice"+rowID).value = FormatAfterDotNumber(item.TaxPrice,2);
                    document.getElementById("OrderTaxPriceHide"+rowID).value = FormatAfterDotNumber(item.TaxPrice,2);
//                    document.getElementById("OrderDiscount"+rowID).value = FormatAfterDotNumber(item.Discount,2);
                    document.getElementById("OrderTaxRate"+rowID).value = FormatAfterDotNumber(item.TaxRate,2);
                    document.getElementById("OrderTaxRateHide"+rowID).value = FormatAfterDotNumber(item.TaxRate,2);
                    document.getElementById("OrderTotalPrice"+rowID).value = FormatAfterDotNumber(item.TotalPrice,2);
                    
                    document.getElementById("OrderTotalFee"+rowID).value = FormatAfterDotNumber(item.TotalFee,2);
                    document.getElementById("OrderTotalTax"+rowID).value = FormatAfterDotNumber(item.TotalTax,2);
                    document.getElementById("OrderRequireDate"+rowID).value = item.RequireDate;
                    document.getElementById("OrderRemark"+rowID).value = item.Remark;
                    document.getElementById("OrderFromBillID"+rowID).value = item.FromBillID;
                    document.getElementById("OrderFromBillNo"+rowID).value = item.FromBillNo;
                    
                    document.getElementById("OrderFromLineNo"+rowID).value = item.FromLineNo;
                    document.getElementById("OrderRemark"+rowID).value = item.Remark;
                    
                    if($("#UserPurchaserName").attr("value") == null)
                    {
                        $("#UserPurchaserName").attr("value",item.PurchaserName);
                        $("#txtPurchaserID").attr("value",item.Purchaser);
//                        $("#UserPurchaserName").attr("disabled","disabled");
                    }
                    if($("#DeptName").attr("value") == null)
                    {
                        $("#DeptName").attr("value",item.DeptName);
                        $("#txtDeptID").attr("value",item.DeptID);
//                        $("#DeptName").attr("disabled","disabled");
                    }
                    fnMergeDetail();
                }
            });
       },
       error: function() {}, 
       complete:function(){}//接收数据完毕
   });
           
    
    closePurchaseContractUCdiv();
}
function txtPrvPurCon_onclick() {

}

</script>
