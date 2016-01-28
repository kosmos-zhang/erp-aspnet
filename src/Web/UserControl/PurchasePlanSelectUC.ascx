<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchasePlanSelectUC.ascx.cs" Inherits="UserControl_WebUserControl" %>

<style type="text/css">
.tdinput
{
	border-width:0pt;
	background-color:#ffffff;
	height:21px;
	margin-left:2px;
}
</style>

<div id="divPurchasePlanhhh" style="display:none" >
   <%-- <iframe id="aPurchasePlan" style=" filter: Alpha(opacity=0); border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 1000px; height:440px; z-index: 100; position: absolute;display:block;
        top: 35%; left: 40%; margin: 5px 0 0 -400px;"></iframe>--%>
    <!--提示信息弹出详情start-->
    <div id="divPurchasePlan" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 1000px; z-index: 20; position: absolute;display:block;
        top: 35%; left: 40%; margin: 5px 0 0 -400px;">
        <table width="100%"><tr><td><a onclick="closePlandiv()" style="text-align:right; cursor:pointer"><img  src="../../../images/Button/Bottom_btn_close.jpg"/></a></td></tr></table>
        <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" 
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClickPlandsfdf'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('dfdsfPurchase','searchClickPlandsfdf')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
                </td>
        </tr>
        <tr>
            <td colspan="2" id="dfdsfPurchase">
                <table width="100%" border="0" align="left" cellpadding="0" id="PurchasePlansearchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        物品编号
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                       <input type="text" id="txt_ProductNo1" class="tdinput" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                       物品名称
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                    <input type="text" id="txt_ProductName1" class="tdinput"/>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        源单编号
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <input type="text" id="txt_FromBillNo1" class="tdinput" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                       <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                     onclick='Fun_Search_PurchasePlan()' id="btn_search" />
                                       <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                                            onclick="GetValuePPurPlan();" id="imgsure" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        <%--<table>
            <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('PurchasePlaysearchtable','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
            </tr>
            <tr>
                <td valign="top" class="Blue">
                    <img src="../../../images/Main/Arrow.jpg"  align="absmiddle" />检索条件
                </td>
            </tr>
            <tr id="PurchasePlansearchtable">
                <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                    物品编号
                </td>
                <td width="23%" bgcolor="#FFFFFF">
                    <input type="text" id="txt_ProductNo1" class="tdinput" />
                </td>
                <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                    物品名称
                </td>
                <td width="23%" bgcolor="#FFFFFF">
                    <input type="text" id="txt_ProductName1" class="tdinput" />
                </td>
                <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                    源单编号
                </td>
                <td width="24%" bgcolor="#FFFFFF">
                    <input type="text" id="txt_FromBillNo1" class="tdinput" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                        onclick='Fun_Search_PurchasePlan()' id="btn_search" />
                </td>
            </tr>--%>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1Plan"bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" id="btnPPurPlanAll" name="btnPPurPlanAll" onclick="OptionCheckPPurPlanAll()"</th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('ProductNo','pp1');return false;">物品编号<span id="pp1" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('ProductName','pp2');return false;">物品名称<span id="pp2" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('standard','pp3');return false;">规格<span id="pp3" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('ColorName','pppColorName');return false;">颜色<span id="pppColorName" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('Unit','pp4');return false;"><span id="sspPurchasePlannUnit">基本单位</span>  <span id="pp4" class="orderTip"></span></div></th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchasePlannUsedUnitName"><div class="orderClick" onclick="popPlanSelectObj.orderByP('UsedUnitName','ppUsedUnitName');return false;">单位<span id="ppUsedUnitName" class="orderTip"></span></div></th>
                    
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('ProductCount','pp5');return false;"><span id="sspPurchasePlannCount">基本数量</span> <span id="pp5" class="orderTip"></span></div></th>
                   <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchasePlannUsedUnitCount"><div class="orderClick" onclick="popPlanSelectObj.orderByP('UsedUnitCount','ppUsedUnitCount');return false;">采购数量<span id="ppUsedUnitCount" class="orderTip"></span></div></th>
                     
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('UnitPrice','pp6');return false;"><span id="sspPurchasePlannUniprice">基本单价</span> <span id="pp6" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchasePlannUsedPrice"><div class="orderClick" onclick="popPlanSelectObj.orderByP('UsedPrice','ppUsedPrice');return false;">单价<span id="ppUsedPrice" class="orderTip"></span></div></th>
                    
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('TaxPrice','pp7');return false;">含税价<span id="pp7" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('Discount','pp8');return false;">折扣(%)<span id="pp8" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('TaxRate','pp9');return false;">税率(%)<span id="pp9" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('TotalPrice','pp10');return false;">金额<span id="pp10" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('RequireDate','pp11');return false;">交货日期<span id="pp11" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('ApplyReason','pp12');return false;">申请原因<span id="pp12" class="orderTip"></span></div></th>
                    <%--<th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">备注<span id="Span18" class="orderTip"></span></div></th>--%>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('FromBillNo','pp13');return false;">源单编号<span id="pp13" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('FromLineNo','pp14');return false;">源单序号<span id="pp14" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('OrderCount','pp15');return false;">已订购数量<span id="pp15" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popPlanSelectObj.orderByP('ProviderName','pp16');return false;">供应商<span id="pp16" class="orderTip"></span></div></th>
                </tr>
            </tbody>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%"><div id="pagePlancount"></div></td>
                            <td height="28" align="right"><div id="pageDataList1_PagerPurchasePlan" class="jPagerBar"></div></td>
                            <td height="28" align="right">
                                <div id="divPlanSelect">
                                    <input name="text" type="text" id="Text2Plan" style="display: none" />
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountPlan" style="width: 22px" />条 转到第
                                    <input name="text" type="text" id="ToPagePlan" style="width: 35px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexPlan($('#ShowPageCountPlan').val(),$('#ToPagePlan').val());" />
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

<script language="javascript" type="text/javascript"]>


var popPlanSelectObj=new Object();
popPlanSelectObj.InputObj = null;
popPlanSelectObj.ProviderID = null;
//popPlanSelectObj.ProviderID= null;

    var pageCountPurOrder = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var currentPageIndex = 1;
    var orderBy = "RequireDate_d";//排序字段
    
    var actionPurchaseOrder = "";//操作
    
    popPlanSelectObj.ShowList = function(objInput,ProviderID)
{
    openRotoscopingDiv(false,'divPBackShadow','PBackShadowIframe');
    popPlanSelectObj.InputObj = objInput;
    popPlanSelectObj.ProviderID = ProviderID;
    document.getElementById('divPurchasePlanhhh').style.display='block';
    
    TurnToPagePlan(1);
}
function closePlandiv()
{

    document.getElementById("divPurchasePlanhhh").style.display="none";
    closeRotoscopingDiv(false,'divPBackShadow');
}
    
popPlanSelectObj.orderByP = function(orderColum,orderTip)
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
   orderBy = orderColum+"_"+ordering;
  TurnToPagePlan(1);
}

function OptionCheckPPurPlanAll()
{
  if(document.getElementById("btnPPurPlanAll").checked)
  {
     var ck = document.getElementsByName("ChkBoxPPurPlan");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=true ;
        }
  }
  else
  {
    var ck = document.getElementsByName("ChkBoxPPurPlan");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=false ;
        }
  }
}    


    //jQuery-ajax获取JSON数据
    function TurnToPagePlan(pageIndexProvider)
    {

           currentPageIndexProvider = pageIndexProvider;
           
           
           var ProductNo= $("#txt_ProductNo1").val().Trim();
           var ProductName= $("#txt_ProductName1").val().Trim();
           var FromBillNo= $("#txt_FromBillNo1").val().Trim();
 if($("#HiddenMoreUnit").val()=="True")
 {
          $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchasePlanSelect.ashx',//目标地址
           cache:false,
           data: "pageIndexPurchasePlanSelectUC="+pageIndexProvider+"&pageCountPurchasePlanSelectUC="+pageCountPurOrder+"&Provider00ID="+popPlanSelectObj.ProviderID+"&ProductNo="+escape(ProductNo)+"&ProductName="+escape(ProductName)+"&FromBillNo="+escape(FromBillNo)+"&actionPurchasePlanSelectUC="+actionPurchaseOrder+"&orderbyPurchasePlanSelectUC="+orderBy+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_PagerPurchasePlan").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1Plan tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPPurPlan\" onclick=\"PurPlanChkBox("+i+")\"  id=\"ChkBoxPPurPlan"+i+"\" value=\""+item.ID+"\"/>"+"</td>"+
                    "<td height='22' style='display:none' id='PPurPlanProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' align='center' id='PPurPlanProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' align='center' id='PPurPlanProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                    "<td height='22' align='center' id='PPurPlanstandard" + i + "' align='center'>" + item.standard + "</td>" +
                     "<td height='22' align='center' id='PPurPlanColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                    "<td height='22' style='display:none' id='PPurPlanUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                      "<td height='22' style='display:none' id='PPurPlanUsedUnitID"+i+"' align='center'>" + item.UsedUnitID + "</td>"+
                    "<td height='22' align='center' id='PPurPlanUnit"+i+"' align='center'  >" + item.Unit + "</td>"+
                     "<td height='22' align='center' id='PPurPlanUsedUnitName"+i+"' align='center'  >" + item.UsedUnitName + "</td>"+
                     
                    "<td height='22' align='center' id='PPurPlanProductCount"+i+"' align='center'   >" + item.ProductCount + "</td>"+
                    "<td height='22' align='center' id='PPurPlanUsedUnitCount"+i+"' align='center'   >" + item.UsedUnitCount + "</td>"+
                    
                    "<td height='22' align='center' id='PPurPlanUnitPrice"+i+"' align='center'   >" + item.UnitPrice + "</td>"+
                     "<td height='22' align='center' id='PPurPlanUsedPrice"+i+"' align='center'>" + item.UsedPrice + "</td>"+
                    "<td height='22' align='center' id='PPurPlanTaxPrice"+i+"' align='center'>" + item.TaxPrice + "</td>"+
                    "<td height='22' align='center' id='PPurPlanDiscount"+i+"' align='center'>" + item.Discount + "</td>"+
                    "<td height='22' align='center' id='PPurPlanTaxRate"+i+"' align='center'>" + item.TaxRate + "</td>"+
                    "<td height='22' align='center' id='PPurPlanTotalPrice"+i+"' align='center'>" + item.TotalPrice + "</td>"+
                    "<td height='22' align='center' id='PPurPlanRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanApplyReasonID"+i+"' align='center'>" + item.ApplyReasonID + "</td>"+
                    "<td height='22' align='center' id='PPurPlanApplyReason"+i+"' align='center'>" + item.ApplyReason + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanFromBillID"+i+"' align='center'>" + item.FromBillID + "</td>"+
                    "<td height='22' align='center' id='PPurPlanFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                    "<td height='22' align='center' id='PPurPlanFromLineNo"+i+"' align='center'>" + item.FromLineNo + "</td>"+
                     "<td height='22' style='display:none' align='center' id='PPurPlanTypeID"+i+"' align='center'>" + item.TypeID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanSeller"+i+"' align='center'>" + item.Seller + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanSellerName"+i+"' align='center'>" + item.SellerName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanDeptID"+i+"' align='center'>" + item.DeptID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanDeptIDName"+i+"' align='center'>" + item.DeptIDName + "</td>"+
                    "<td height='22' align='center' id='PPurPlanOrderCount"+i+"' align='center'>" + item.OrderCount + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanProviderID"+i+"' align='center'>" + item.ProviderID + "</td>"+
                    "<td height='22' align='center' id='PPurPlanProviderName"+i+"' align='center'>"+item.ProviderName+"</td>").appendTo($("#pageDataList1Plan tbody"));
                    }
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchasePlan",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkProvider",
                    
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountPurOrder,currentPageIndex:pageIndexProvider,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPagePlan({pageindex});return false;"}//[attr]
                    );document.getElementById("btnPPurPlanAll").checked = false;

                  totalRecordProvider = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2Plan").value=msg.totalCount;
                  $("#ShowPageCountPlan").val(pageCountPurOrder);
                  ShowTotalPage(msg.totalCount,pageCountPurOrder,pageIndexProvider,$("#pagePlancount"));
                  $("#ToPagePlan").val(pageIndexProvider);
                  },
           error: function() {}, 
           complete:function(){hidePopup();    
$("#pageDataList1_PagerPurchasePlan").show();IfshowOrderSelect(document.getElementById("Text2Plan").value);pageDataList1("pageDataList1Plan","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
 }
 else
 {
 
 
   document .getElementById ("sspPurchasePlannUsedUnitCount").style.display ="none"; 
    document .getElementById ("sspPurchasePlannUsedUnitName").style.display ="none"; 
      document .getElementById ("sspPurchasePlannUsedPrice").style.display ="none";
      document.getElementById("sspPurchasePlannCount").innerHTML = "采购数量";
      document.getElementById("sspPurchasePlannUnit").innerHTML = "单位";
      document.getElementById("sspPurchasePlannUniprice").innerHTML = "单价";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchasePlanSelect.ashx',//目标地址
           cache:false,
           data: "pageIndexPurchasePlanSelectUC="+pageIndexProvider+"&pageCountPurchasePlanSelectUC="+pageCountPurOrder+"&Provider00ID="+popPlanSelectObj.ProviderID+"&ProductNo="+escape(ProductNo)+"&ProductName="+escape(ProductName)+"&FromBillNo="+escape(FromBillNo)+"&actionPurchasePlanSelectUC="+actionPurchaseOrder+"&orderbyPurchasePlanSelectUC="+orderBy+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_PagerPurchasePlan").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1Plan tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPPurPlan\" onclick=\"PurPlanChkBox("+i+")\"  id=\"ChkBoxPPurPlan"+i+"\" value=\""+item.ID+"\"/>"+"</td>"+
                    "<td height='22' style='display:none' id='PPurPlanProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' align='center' id='PPurPlanProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' align='center' id='PPurPlanProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                    "<td height='22' align='center' id='PPurPlanstandard" + i + "' align='center'>" + item.standard + "</td>" +
                    "<td height='22' align='center' id='PPurPlanColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                    "<td height='22' style='display:none' id='PPurPlanUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                    "<td height='22' align='center' id='PPurPlanUnit"+i+"' align='center'>" + item.Unit + "</td>"+
                    "<td height='22' align='center' id='PPurPlanProductCount"+i+"' align='center'>" + item.ProductCount + "</td>"+
                    "<td height='22' align='center' id='PPurPlanUnitPrice"+i+"' align='center'>" + item.UnitPrice + "</td>"+
                    "<td height='22' align='center' id='PPurPlanTaxPrice"+i+"' align='center'>" + item.TaxPrice + "</td>"+
                    "<td height='22' align='center' id='PPurPlanDiscount"+i+"' align='center'>" + item.Discount + "</td>"+
                    "<td height='22' align='center' id='PPurPlanTaxRate"+i+"' align='center'>" + item.TaxRate + "</td>"+
                    "<td height='22' align='center' id='PPurPlanTotalPrice"+i+"' align='center'>" + item.TotalPrice + "</td>"+
                    "<td height='22' align='center' id='PPurPlanRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanApplyReasonID"+i+"' align='center'>" + item.ApplyReasonID + "</td>"+
                    "<td height='22' align='center' id='PPurPlanApplyReason"+i+"' align='center'>" + item.ApplyReason + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanFromBillID"+i+"' align='center'>" + item.FromBillID + "</td>"+
                    "<td height='22' align='center' id='PPurPlanFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                    "<td height='22' align='center' id='PPurPlanFromLineNo"+i+"' align='center'>" + item.FromLineNo + "</td>"+
                     "<td height='22' style='display:none' align='center' id='PPurPlanTypeID"+i+"' align='center'>" + item.TypeID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanSeller"+i+"' align='center'>" + item.Seller + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanSellerName"+i+"' align='center'>" + item.SellerName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanDeptID"+i+"' align='center'>" + item.DeptID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanDeptIDName"+i+"' align='center'>" + item.DeptIDName + "</td>"+
                    "<td height='22' align='center' id='PPurPlanOrderCount"+i+"' align='center'>" + item.OrderCount + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurPlanProviderID"+i+"' align='center'>" + item.ProviderID + "</td>"+
                    "<td height='22' align='center' id='PPurPlanProviderName"+i+"' align='center'>"+item.ProviderName+"</td>").appendTo($("#pageDataList1Plan tbody"));
                    }
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchasePlan",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkProvider",
                    
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountPurOrder,currentPageIndex:pageIndexProvider,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPagePlan({pageindex});return false;"}//[attr]
                    );document.getElementById("btnPPurPlanAll").checked = false;

                  totalRecordProvider = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2Plan").value=msg.totalCount;
                  $("#ShowPageCountPlan").val(pageCountPurOrder);
                  ShowTotalPage(msg.totalCount,pageCountPurOrder,pageIndexProvider,$("#pagePlancount"));
                  $("#ToPagePlan").val(pageIndexProvider);
                  },
           error: function() {}, 
           complete:function(){hidePopup();    
$("#pageDataList1_PagerPurchasePlan").show();IfshowOrderSelect(document.getElementById("Text2Plan").value);pageDataList1("pageDataList1Plan","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
           }
    }
    //table行颜色
function pageDataList1(o,a,b,c,d){
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


function PurPlanChkBox(rowID)
{
    if(popPlanSelectObj.ProviderID != null && popPlanSelectObj.ProviderID != ""&& popPlanSelectObj.ProviderID != 0)
    {
        if(document.getElementById("PPurPlanProviderID"+rowID).innerHTML != popPlanSelectObj.ProviderID)
        {
//        alert(1)
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的计划明细只能来自一个供应商！");
            return;
        }
    }
    else 
    {
        var ck = document.getElementsByName("ChkBoxPPurPlan");
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked && i!= rowID )
            {//选中某行
                if($("#PPurPlanProviderID"+i).html() != $("#PPurPlanProviderID"+rowID).html())
                {
                    document.getElementById("ChkBoxPPurPlan"+rowID).checked = false;
//                    popMsgObj.ShowMsg('选择的计划明细只能来自一个供应商！');
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的计划明细只能来自一个供应商！");
                    return;
                }
//                if($("#PurPlnProviderName"+i).html() != $("#PurPlnProviderName"+rowID).html())
//                {
//                   
//                    //如果选择的不是一个合同则不让选
//                    document.getElementById("ChkBoxPurPln"+rowID).checked = false;
//                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的计划明细只能来自一个供应商！");
//                    return;
//                }
            }
        }
    }
}

function Fun_Search_PurchasePlan(aa)
{
      search="1";
      TurnToPagePlan(1);
}
function IfshowOrderSelect(count)
    {
        if(count=="0")
        {
            document.getElementById("divPlanSelect").style.display = "none";
            document.getElementById("pagePlancount").style.display = "none";
        }
        else
        {
            document.getElementById("divPlanSelect").style.display="";
            document.getElementById("pagePlancount").style.display = "";
        }
    }
    
    function SelectDeptProvider(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexPlan(newPageCountProvider,newPageIndexProvider)
    {
        if(!IsZint(newPageCountProvider))
       {
          popMsgObj.ShowMsg('显示条数格式不对，必须输入正整数！');
          return;
       }
       if(!IsZint(newPageIndexProvider))
       {
          popMsgObj.ShowMsg('跳转页数格式不对，必须输入正整数！');
          return;
       }
        if(newPageCountProvider <=0 || newPageIndexProvider <= 0 ||  newPageIndexProvider  > ((totalRecordProvider-1)/newPageCountProvider)+1 )
        {
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            popMsgObj.ShowMsg('转到页数超出查询范围！');
            return;
        }
        else
        {
        ifshow="0";
            this.pageCountPurOrder=parseInt(newPageCountProvider);
            TurnToPagePlan(parseInt(newPageIndexProvider));
        }
    }
    
 

</script>