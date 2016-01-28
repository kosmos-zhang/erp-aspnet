<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseApplySelectUC.ascx.cs" Inherits="UserControl_PurchaseApplySelectUC" %>

<style type="text/css">
.tdinput
{
	border-width:0pt;
	background-color:#ffffff;
	height:21px;
	margin-left:2px;
}
</style>

<div id="divPurchaseApplyhhh" style="display:none" >
    <!--提示信息弹出详情start-->
    <iframe id="aPurchaseApply" style=" filter: Alpha(opacity=0); border: solid 10px #93BCDD; background: #fff;
    padding: 10px; width: 1000px; height:440px; z-index: 1000; position: absolute; display:block; top: 35%;
    left: 40%; margin: 5px 0 0 -400px;"> 
     </iframe>
    <div id="divPurchaseApply" style="border: solid 10px #93BCDD; background: #fff;
    padding: 10px; width: 1000px; z-index: 1001; position: absolute; display:block; top: 35%;left: 40%; margin: 5px 0 0 -400px;">
        
        <table width="100%"><tr><td><a onclick="closeApplydiv()" style="text-align:right; cursor:pointer"><img  src="../../../images/Button/Bottom_btn_close.jpg"/></a></td></tr></table>
        <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" 
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClickApply'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('PurchaseApplysearchtable','searchClickApply')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" border="0" align="left" cellpadding="0" id="PurchaseApplysearchtable" cellspacing="0"
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
                                       <input type="text" id="txt_ProductNo" class="tdinput" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                       物品名称
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                    <input type="text" id="txt_ProductName" class="tdinput"/>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        源单编号
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <input type="text" id="txt_FromBillNo" class="tdinput" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                       <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                     onclick='Fun_Search_PurchaseApply()' id="btn_search" />
                                       <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                                            onclick="GetValuePPurApp();" id="imgsure" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1Apply" bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" id="btnPPurAPPAll" name="btnPPurAPPAll" onclick="OptionCheckPPurAPPAll()" /></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('ProductNo','cc1');return false;">物品编号<span id="cc1" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('ProductName','cc2');return false;">物品名称<span id="cc2" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('standard','cc3');return false;">规格<span id="cc3" class="orderTip"></span></div></th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('ColorName','ccColorName');return false;">颜色<span id="ccColorName" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchaseApp2UsedUnitName"><div class="orderClick" onclick="popApplySelect.orderByP('UsedUnitName','ccUsedUnitName');return false;">单位<span id="ccUsedUnitName" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('Unit','cc4');return false;"><span id="sspPurchaseApp2Unit">基本单位</span>  <span id="cc4" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('ProductCount','cc5');return false;"> <span id="sspPurchaseApp2Count">基本数量</span> <span id="cc5" class="orderTip"></span></div></th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchaseApp2UsedUnitCount"><div class="orderClick" onclick="popApplySelect.orderByP('UsedUnitCount','ccUsedUnitCount');return false;">采购数量<span id="ccUsedUnitCount" class="orderTip"></span></div></th>
                    
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('UnitPrice','cc6');return false;">单价<span id="cc6" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('TaxPrice','cc7');return false;">含税价<span id="cc7" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('Discount','cc8');return false;">折扣(%)<span id="cc8" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('TaxRate','cc9');return false;">税率(%)<span id="cc9" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('RequireDate','cc10');return false;">交货日期<span id="cc10" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('ApplyReason','cc11');return false;">申请原因<span id="cc11" class="orderTip"></span></div></th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('FromBillNo','cc12');return false;">源单编号<span id="cc12" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popApplySelect.orderByP('FromLineNo','cc13');return false;">源单序号<span id="cc13" class="orderTip"></span></div></th>
                </tr>
            </tbody>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%"><div id="pageApplycount"></div></td>
                            <td height="28" align="right"><div id="pageDataList1_PagerPurchaseApply" class="jPagerBar"></div></td>
                            <td height="28" align="right">
                                <div id="divApplySelect">
                                    <input name="text" type="text" id="Text2Apply" style="display: none" />
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountApply" style="width: 22px" />条 转到第
                                    <input name="text" type="text" id="ToPageApply" style="width: 35px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexApply($('#ShowPageCountApply').val(),$('#ToPageApply').val());" />
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

<script language="javascript" type="text/javascript">


var popApplySelect=new Object();
popApplySelect.InputObj = null;
//popApplySelect.ProviderID= null;

    var pageCountPurOrder = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var currentPageIndex = 1;
    var orderBy = "";//排序字段
    
    var actionPurchaseOrder = "";//操作
    
    popApplySelect.ShowList = function(objInput,ProviderID)
{
    openRotoscopingDiv(false,'divPBackShadow','PBackShadowIframe');
    popApplySelect.InputObj = objInput;
    popApplySelect.ProviderID = ProviderID;
    document.getElementById('divPurchaseApplyhhh').style.display='block';
//    document.getElementById("aaaa").style.display="block";
    
    TurnToPageApply(1);
}
function closeApplydiv()
{
//    document.getElementById("aaaa").style.display="none";
    document.getElementById("divPurchaseApplyhhh").style.display="none";
    closeRotoscopingDiv(false,'divPBackShadow');
}

function OptionCheckPPurAPPAll()
{
  if(document.getElementById("btnPPurAPPAll").checked)
  {
     var ck = document.getElementsByName("ChkBoxPPurApp");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=true ;
        }
  }
  else
  {
    var ck = document.getElementsByName("ChkBoxPPurApp");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=false ;
        }
  }
}    
popApplySelect.orderByP = function(orderColum,orderTip)
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
  TurnToPageApply(1);
}
    //jQuery-ajax获取JSON数据
    function TurnToPageApply(pageIndexProvider)
    {
            ShowPreventReclickDiv();
           currentPageIndexProvider = pageIndexProvider;
           
           var ProductNo= $("#txt_ProductNo").val().Trim();
           var ProductName= $("#txt_ProductName").val().Trim();
           var FromBillNo= $("#txt_FromBillNo").val().Trim();
  if($("#HiddenMoreUnit").val()=="True")
  {
             $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseApplySelect.ashx',//目标地址
           cache:false,
           data: "pageIndexBillChoose="+pageIndexProvider+"&pageCountBillChoose="+pageCountPurOrder+"&Provider00ID="+popApplySelect.ProviderID+"&ProductNo="+escape(ProductNo)+"&ProductName="+escape(ProductName)+"&FromBillNo="+escape(FromBillNo)+"&actionBillChoose="+actionPurchaseOrder+"&orderbyBillChoose="+orderBy+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_PagerPurchaseApply").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1Apply tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPPurApp\" id=\"ChkBoxPPurApp"+i+"\" value=\""+item.ID+"\"  />"+"</td>"+
                    "<td height='22' style='display:none' id='PPurAppProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' align='center' id='PPurAppProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' align='center' id='PurAppProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                    "<td height='22' align='center' id='PPurAppstandard" + i + "' align='center'>" + item.standard + "</td>" +
                     "<td height='22' align='center' id='PPurAppColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                    "<td height='22' style='display:none' id='PPurAppUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                        "<td height='22' align='center' id='PPurAppUsedUnitNam"+i+"' align='center'>" + item.UsedUnitName + "</td>"+
                    "<td height='22' align='center' id='PPurAppUnit"+i+"' align='center'  >" + item.Unit + "</td>"+
                     "<td height='22' align='center' id='PPurAppUsedUnitID"+i+"' align='center' style='display:none'>" + item.UsedUnitID + "</td>"+
                    "<td height='22' align='center' id='PPurAppProductCount"+i+"' align='center'  >" +  (parseFloat(item.ProductCount )).toFixed($("#HiddenPoint").val())+ "</td>"+
                    "<td height='22' align='center' id='PPurAppUsedUnitCount"+i+"' align='center'>" + (parseFloat(item.UsedUnitCount )).toFixed($("#HiddenPoint").val())+ "</td>"+ 
                    "<td height='22' align='center' id='PPurAppUnitPrice"+i+"' align='center'>" +    (parseFloat(item.UnitPrice )).toFixed($("#HiddenPoint").val()) + "</td>"+
                    "<td height='22' align='center' id='PPurAppTaxPrice"+i+"' align='center'>" +         (parseFloat(item.TaxPrice )).toFixed($("#HiddenPoint").val())+ "</td>"+
                    "<td height='22' align='center' id='PPurAppDiscount"+i+"' align='center'>" +    (parseFloat(item.Discount )).toFixed($("#HiddenPoint").val()) + "</td>"+
                    "<td height='22' align='center' id='PPurAppTaxRate"+i+"' align='center'>" +  (parseFloat(item.TaxRate )).toFixed($("#HiddenPoint").val())+ "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppTotalPrice"+i+"' align='center'>" +    (parseFloat(item.TotalPrice )).toFixed($("#HiddenPoint").val())+ "</td>"+
                    "<td height='22' align='center' id='PPurAppRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppApplyReasonID"+i+"' align='center'>" + item.ApplyReasonID + "</td>"+
                    "<td height='22' align='center' id='PPurAppApplyReason"+i+"' align='center'>" + item.ApplyReason + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppFromBillID"+i+"' align='center'>" + item.FromBillID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppTypeID"+i+"' align='center'>" + item.TypeID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppSeller"+i+"' align='center'>" + item.Seller + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppSellerName"+i+"' align='center'>" + item.SellerName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppDeptID"+i+"' align='center'>" + item.DeptID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppDeptIDName"+i+"' align='center'>" + item.DeptIDName + "</td>"+
                    "<td height='22' align='center' id='PPurAppFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                    "<td height='22' align='center' id='PPurAppFromLineNo"+i+"' align='center'>"+item.FromLineNo+"</td>").appendTo($("#pageDataList1Apply tbody"));
                    }
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchaseApply",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkProvider",
                    
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountPurOrder,currentPageIndex:pageIndexProvider,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPageApply({pageindex});return false;"}//[attr]
                    );document.getElementById("btnPPurAPPAll").checked = false;

                  totalRecordProvider = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2Apply").value=msg.totalCount;
                  $("#ShowPageCountApply").val(pageCountPurOrder);
                  ShowTotalPage(msg.totalCount,pageCountPurOrder,pageIndexProvider,$("#pageApplycount"));
                  $("#ToPageApply").val(pageIndexProvider);
                  },
           error: function() {}, 
           complete:function(){hidePopup();    
$("#pageDataList1_PagerPurchaseApply").show();IfshowOrderSelect(document.getElementById("Text2Apply").value);pageDataList1("pageDataList1Apply","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
  }
  else
  {
  
  
         document .getElementById ("sspPurchaseApp2UsedUnitCount").style.display ="none"; 
    document .getElementById ("sspPurchaseApp2UsedUnitName").style.display ="none";
    document.getElementById("sspPurchaseApp2Count").innerHTML = "采购数量";
    document.getElementById("sspPurchaseApp2Unit").innerHTML = "单位"; 
         
         
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseApplySelect.ashx',//目标地址
           cache:false,
           data: "pageIndexBillChoose="+pageIndexProvider+"&pageCountBillChoose="+pageCountPurOrder+"&Provider00ID="+popApplySelect.ProviderID+"&ProductNo="+escape(ProductNo)+"&ProductName="+escape(ProductName)+"&FromBillNo="+escape(FromBillNo)+"&actionBillChoose="+actionPurchaseOrder+"&orderbyBillChoose="+orderBy+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_PagerPurchaseApply").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1Apply tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPPurApp\" id=\"ChkBoxPPurApp"+i+"\" value=\""+item.ID+"\"  />"+"</td>"+
                    "<td height='22' style='display:none' id='PPurAppProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' align='center' id='PPurAppProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' align='center' id='PurAppProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                    "<td height='22' align='center' id='PPurAppstandard" + i + "' align='center'>" + item.standard + "</td>" +
                     "<td height='22' align='center' id='PPurAppColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                    "<td height='22' style='display:none' id='PPurAppUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                    "<td height='22' align='center' id='PPurAppUnit"+i+"' align='center'>" + item.Unit + "</td>"+ 
                    "<td height='22' align='center' id='PPurAppProductCount"+i+"' align='center'>" +   (parseFloat(item.ProductCount )).toFixed($("#HiddenPoint").val())+ "</td>"+
                    "<td height='22' align='center' id='PPurAppUnitPrice"+i+"' align='center'>" +  (parseFloat(item.UnitPrice )).toFixed($("#HiddenPoint").val())+ "</td>"+
                    "<td height='22' align='center' id='PPurAppTaxPrice"+i+"' align='center'>" + (parseFloat(item.TaxPrice )).toFixed($("#HiddenPoint").val())+ "</td>"+
                    "<td height='22' align='center' id='PPurAppDiscount"+i+"' align='center'>" +     (parseFloat(item.Discount )).toFixed($("#HiddenPoint").val())+ "</td>"+
                    "<td height='22' align='center' id='PPurAppTaxRate"+i+"' align='center'>" +  (parseFloat(item.TaxRate )).toFixed($("#HiddenPoint").val())+ "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppTotalPrice"+i+"' align='center'>" +       (parseFloat(item.TotalPrice )).toFixed($("#HiddenPoint").val())+ "</td>"+
                    "<td height='22' align='center' id='PPurAppRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppApplyReasonID"+i+"' align='center'>" + item.ApplyReasonID + "</td>"+
                    "<td height='22' align='center' id='PPurAppApplyReason"+i+"' align='center'>" + item.ApplyReason + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppFromBillID"+i+"' align='center'>" + item.FromBillID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppTypeID"+i+"' align='center'>" + item.TypeID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppSeller"+i+"' align='center'>" + item.Seller + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppSellerName"+i+"' align='center'>" + item.SellerName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppDeptID"+i+"' align='center'>" + item.DeptID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAppDeptIDName"+i+"' align='center'>" + item.DeptIDName + "</td>"+
                    "<td height='22' align='center' id='PPurAppFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                    "<td height='22' align='center' id='PPurAppFromLineNo"+i+"' align='center'>"+item.FromLineNo+"</td>").appendTo($("#pageDataList1Apply tbody"));
                    }
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchaseApply",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkProvider",
                    
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountPurOrder,currentPageIndex:pageIndexProvider,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPageApply({pageindex});return false;"}//[attr]
                    );document.getElementById("btnPPurAPPAll").checked = false;

                  totalRecordProvider = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2Apply").value=msg.totalCount;
                  $("#ShowPageCountApply").val(pageCountPurOrder);
                  ShowTotalPage(msg.totalCount,pageCountPurOrder,pageIndexProvider,$("#pageApplycount"));
                  $("#ToPageApply").val(pageIndexProvider);
                  },
           error: function() {}, 
           complete:function(){hidePopup();    
$("#pageDataList1_PagerPurchaseApply").show();IfshowOrderSelect(document.getElementById("Text2Apply").value);pageDataList1("pageDataList1Apply","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

function Fun_Search_PurchaseApply(aa)
{
      search="1";
      TurnToPageApply(1);
}
function IfshowOrderSelect(count)
    {
        if(count=="0")
        {
            document.getElementById("divApplySelect").style.display = "none";
            document.getElementById("pageApplycount").style.display = "none";
        }
        else
        {
            document.getElementById("divApplySelect").style.display="";
            document.getElementById("pageApplycount").style.display = "";
        }
    }
    
    function SelectDeptProvider(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexApply(newPageCountProvider,newPageIndexProvider)
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
            TurnToPageApply(parseInt(newPageIndexProvider));
        }
    }
    

</script>