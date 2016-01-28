<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseAskPriceSelectUC.ascx.cs" Inherits="UserControl_PurchaseAskPriceSelectUC" %>

<style type="text/css">
.tdinput
{
	border-width:0pt;
	background-color:#ffffff;
	height:21px;
	margin-left:2px;
}
</style>

<div id="divPurchaseAskPricehhh" style="display:none" >
<%--    <iframe id="aPurchaseAskPrice" style=" filter: Alpha(opacity=0); border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 1000px; height:440px; z-index: 1000; position: absolute;display:block;
        top: 35%; left: 40%; margin: 5px 0 0 -400px;"></iframe>--%>
    <!--提示信息弹出详情start-->
    <div id="divPurchaseAskPrice" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 1000px; z-index: 20; position: absolute;display:block;
        top: 35%; left: 40%; margin: 5px 0 0 -400px;">
        <table width="100%"><tr><td><a onclick="closeAskPricediv()" style="text-align:right; cursor:pointer"><img  src="../../../images/Button/Bottom_btn_close.jpg"/></a></td></tr></table>
        <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" 
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClickAskPrice'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('PurchasePlansearchdsdfdftable','searchClickAskPrice')" /></div>
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
                <table width="100%" border="0" align="left" cellpadding="0" id="PurchasePlansearchdsdfdftable" cellspacing="0"
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
                                       <input type="text" id="txt_ProductNo2" class="tdinput" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                       物品名称
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                    <input type="text" id="txt_ProductName2" class="tdinput"/>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        源单编号
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <input type="text" id="txt_FromBillNo2" class="tdinput" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                       <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                     onclick='Fun_Search_PurchaseAskPrice()' id="btn_search" />
                                       <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                                            onclick="GetValuePPurAK();" id="imgsure" />
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
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('PurchaseApplysearchtable','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
            </tr>
            <tr>
                <td valign="top" class="Blue">
                    <img src="../../../images/Main/Arrow.jpg"  align="absmiddle" />检索条件
                </td>
            </tr>
            <tr id="PurchaseApplysearchtable">
                <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                    物品编号
                </td>
                <td width="23%" bgcolor="#FFFFFF">
                    <input type="text" id="txt_ProductNo2" class="tdinput" />
                </td>
                <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                    物品名称
                </td>
                <td width="23%" bgcolor="#FFFFFF">
                    <input type="text" id="txt_ProductName2" class="tdinput" />
                </td>
                <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                    源单编号
                </td>
                <td width="24%" bgcolor="#FFFFFF">
                    <input type="text" id="txt_FromBillNo2" class="tdinput" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                        onclick='Fun_Search_PurchaseAskPrice()' id="btn_search" />
                </td>
            </tr>
        </table>--%>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1AskPrice" bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" id="btnPPurAKAll" name="btnPPurAKAll" onclick="OptionCheckPPurAKAll()"</th>     
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('ProductNo','oc1');return false;">物品编号<span id="oc1" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('ProductName','oc2');return false;">物品名称<span id="oc2" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('standard','oc3');return false;">规格<span id="oc3" class="orderTip"></span></div></th>
                       <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('ColorName','ocColorName');return false;">颜色<span id="ocColorName" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('Unit','oc4');return false;"><span id="sspPurchaseAskUnit">基本单位</span> <span id="oc4" class="orderTip"></span></div></th>
                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchaseAskUsedUnitName"><div class="orderClick" onclick="popAskPrice.orderByP('UsedUnitName','ocUsedUnitName');return false;">单位<span id="ocUsedUnitName" class="orderTip"></span></div></th>
                   
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('ProductCount','oc5');return false;"><span id="sspPurchaseAskCount">基本数量</span> <span id="oc5" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"  id="sspPurchaseAskUsedUnitCount"><div class="orderClick" onclick="popAskPrice.orderByP('UsedUnitCount','ocUsedUnitCount');return false;">采购数量<span id="ocUsedUnitCount" class="orderTip"></span></div></th>
                    
                    
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('UnitPrice','oc6');return false;"> <span id="sspPurchaseAskUniprice">基本单价</span> <span id="oc6" class="orderTip"></span></div></th>
                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchaseAskUsedPrice"><div class="orderClick" onclick="popAskPrice.orderByP('UsedPrice','ocUsedPrice');return false;">单价<span id="ocUsedPrice" class="orderTip"></span></div></th>
                    
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('TaxPrice','oc7');return false;">含税价<span id="oc7" class="orderTip"></span></div></th>
            <%--        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">折扣(%)<span id="Span4" class="orderTip"></span></div></th>--%>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('TaxRate','oc8');return false;">税率(%)<span id="oc8" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('TotalPrice','oc9');return false;">金额<span id="oc9" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('TotalFee','oc10');return false;">含税金额<span id="oc10" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('TotalTax','oc11');return false;">税额<span id="oc11" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('RequireDate','oc12');return false;">交货日期<span id="oc12" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('ApplyReason','oc13');return false;">申请原因<span id="oc13" class="orderTip"></span></div></th>
                    <%--<th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">备注<span id="Span18" class="orderTip"></span></div></th>--%>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('FromBillNo','oc14');return false;">源单编号<span id="oc14" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('FromLineNo','oc15');return false;">源单序号<span id="oc15" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('ProviderName','oc16');return false;">供应商<span id="oc16" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('CurrencyTypeName','oc17');return false;">币种<span id="oc17" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="popAskPrice.orderByP('Rate','oc18');return false;">汇率<span id="oc18" class="orderTip"></span></div></th>
                </tr>
            </tbody>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%"><div id="pageAskPricecount"></div></td>
                            <td height="28" align="right"><div id="pageDataList1_PagerPurchaseAskPrice" class="jPagerBar"></div></td>
                            <td height="28" align="right">
                                <div id="divAskPriceSelect">
                                    <input name="text" type="text" id="Text2AskPrice" style="display: none" />
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountAskPrice" style="width: 22px" />条 转到第
                                    <input name="text" type="text" id="ToPageAskPrice" style="width: 35px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexAskPrice($('#ShowPageCountAskPrice').val(),$('#ToPageAskPrice').val());" />
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


var popAskPrice=new Object();
popAskPrice.InputObj = null;
popAskPrice.ProviderID= null;
popAskPrice.CurrencyTypeID= null;
popAskPrice.Rate = null;

    var pageCountPurOrder = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var currentPageIndex = 1;
    var orderBy = "RequireDate_d";//排序字段
    
    var actionPurchaseOrder = "";//操作
    
    popAskPrice.ShowList = function(objInput,ProviderID,CurrencyTypeID,Rate)
{
    
//    if((objInput == "Order")&&(document.getElementById("txtProviderID").value == ""))
//    {
//        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择供应商！");
//        return;
//    }
    openRotoscopingDiv(false,'divPBackShadow','PBackShadowIframe');
    popAskPrice.InputObj = objInput;
    popAskPrice.ProviderID = ProviderID;
    popAskPrice.CurrencyTypeID= CurrencyTypeID;
    popAskPrice.Rate = Rate;
    document.getElementById('divPurchaseAskPricehhh').style.display='block';
    
    TurnToPageAskPrice(1);
}
function closeAskPricediv()
{

    document.getElementById("divPurchaseAskPricehhh").style.display="none";
    closeRotoscopingDiv(false,'divPBackShadow');
}
    

    //jQuery-ajax获取JSON数据
    function TurnToPageAskPrice(pageIndexProvider)
    {

           currentPageIndexProvider = pageIndexProvider;
           
           var ProductNo= $("#txt_ProductNo2").val().Trim();
           var ProductName= $("#txt_ProductName2").val().Trim();
           var FromBillNo= $("#txt_FromBillNo2").val().Trim();
   if($("#HiddenMoreUnit").val()=="True")
     {
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseAskPriceSelect.ashx',//目标地址
           cache:false,
           data: "pageIndexPurchaseAskPrice="+pageIndexProvider+"&pageCountPurchaseAskPrice="+pageCountPurOrder+"&Provider00ID="+popAskPrice.ProviderID+"&CurrencyTypeID="+popAskPrice.CurrencyTypeID+"&Rate="+popAskPrice.Rate+"&ProductNo="+escape(ProductNo)+"&ProductName="+escape(ProductName)+"&FromBillNo="+escape(FromBillNo)+"&actionPurchaseAskPrice="+actionPurchaseOrder+"&orderbyPurchaseAskPrice="+orderBy+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_PagerPurchaseAskPrice").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1AskPrice tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
           
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPPurAK\" onclick=\"PPurAKClickChkBox("+i+");\" id=\"ChkBoxPPurAK"+i+"\" value=\""+item.ID+"\"/>"+"</td>"+
                    "<td height='22' style='display:none' id='PPurAKProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' align='center' id='PPurAKProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' align='center'  id='PPurAKProductName"+i+"'   align='center'>" + item.ProductName + "</td>"+
                    "<td height='22' align='center' id='PPurAKstandard" + i + "' align='center'>" + item.standard + "</td>" +
                     "<td height='22' align='center' id='PPurAKColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                    "<td height='22' style='display:none' id='PPurAKUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                    "<td height='22' style='display:none' id='PPurAKUsedUnitID"+i+"' align='center'>" + item.UsedUnitID + "</td>"+
                    "<td height='22' align='center' id='PPurAKUnit"+i+"' align='center'  >" + item.Unit + "</td>"+
                      "<td height='22' align='center' id='PPurAKUsedUnitName"+i+"' align='center'   >" + item.UsedUnitName + "</td>"+
                    "<td height='22' align='center' id='PPurAKProductCount"+i+"' align='center'   >" + item.ProductCount + "</td>"+
                      "<td height='22' align='center' id='PPurAKUsedUnitCount"+i+"' align='center'   >" + item.UsedUnitCount + "</td>"+ 
                    "<td height='22' align='center' id='PPurAKUnitPrice"+i+"' align='center'  >" + item.UnitPrice + "</td>"+ 
                     "<td height='22' align='center' id='PPurAKUsedPrice"+i+"' align='center'>" + item.UsedPrice + "</td>"+ 
                    "<td height='22' align='center' id='PPurAKTaxPrice"+i+"' align='center'>" + item.TaxPrice + "</td>"+
//                    "<td height='22' align='center' id='PPurAKDiscount"+i+"' align='center'>" + item.Discount + "</td>"+
                    "<td height='22' align='center' id='PPurAKTaxRate"+i+"' align='center'>" + item.TaxRate + "</td>"+
                    "<td height='22' align='center' id='PPurAKTotalPrice"+i+"' align='center'>" + item.TotalPrice + "</td>"+
                    "<td height='22' align='center' id='PPurAKTotalFee"+i+"' align='center'>" + item.TotalFee + "</td>"+
                    "<td height='22' align='center' id='PPurAKTotalTax"+i+"' align='center'>" + item.TotalTax + "</td>"+
                    "<td height='22' align='center' id='PPurAKRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKApplyReasonID"+i+"' align='center'>" + item.ApplyReasonID + "</td>"+
                    "<td height='22' align='center' id='PPurAKApplyReason"+i+"' align='center'>" + item.ApplyReason + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKFromBillID"+i+"' align='center'>" + item.FromBillID + "</td>"+
                    "<td height='22' align='center' id='PPurAKFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                    "<td height='22' align='center' id='PPurAKFromLineNo"+i+"' align='center'>" + item.FromLineNo + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKProviderID"+i+"' align='center'>" + item.ProviderID + "</td>"+
                    "<td height='22' align='center' id='PPurAKProviderName"+i+"' align='center'>" + item.ProviderName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKCurrencyType"+i+"' align='center'>" + item.CurrencyType + "</td>"+
                    "<td height='22' align='center' id='PPurAKCurrencyTypeName"+i+"' align='center'>" + item.CurrencyTypeName + "</td>"+
                    "<td height='22' align='center' id='PPurAKRate"+i+"' align='center'>" + item.Rate + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKSeller"+i+"' align='center'>" + item.Seller + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKSellerName"+i+"' align='center'>" + item.SellerName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKDeptID"+i+"' align='center'>" + item.DeptID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKDeptIDName"+i+"' align='center'>" + item.DeptIDName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKisAddTax"+i+"' align='center'>"+item.isAddTax+"</td>").appendTo($("#pageDataList1AskPrice tbody"));
                    }
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchaseAskPrice",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkProvider",
                    
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountPurOrder,currentPageIndex:pageIndexProvider,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPageAskPrice({pageindex});return false;"}//[attr]
                    );document.getElementById("btnPPurAKAll").checked = false;

                  totalRecordProvider = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2AskPrice").value=msg.totalCount;
                  $("#ShowPageCountAskPrice").val(pageCountPurOrder);
                  ShowTotalPage(msg.totalCount,pageCountPurOrder,pageIndexProvider,$("#pageAskPricecount"));
                  $("#ToPageAskPrice").val(pageIndexProvider);
                  },
           error: function() {}, 
           complete:function(){hidePopup();    
$("#pageDataList1_PagerPurchaseAskPrice").show();IfshowOrderSelect(document.getElementById("Text2AskPrice").value);pageDataList1("pageDataList1AskPrice","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
           }
           else
           {
             document .getElementById ("sspPurchaseAskUsedUnitCount").style.display ="none"; 
    document .getElementById ("sspPurchaseAskUsedUnitName").style.display ="none"; 
      document .getElementById ("sspPurchaseAskUsedPrice").style.display ="none";
      document.getElementById("sspPurchaseAskCount").innerHTML = "采购数量";
      document.getElementById("sspPurchaseAskUnit").innerHTML = "单位";
      document.getElementById("sspPurchaseAskUniprice").innerHTML = "单价";
           
           
               $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseAskPriceSelect.ashx',//目标地址
           cache:false,
           data: "pageIndexPurchaseAskPrice="+pageIndexProvider+"&pageCountPurchaseAskPrice="+pageCountPurOrder+"&Provider00ID="+popAskPrice.ProviderID+"&CurrencyTypeID="+popAskPrice.CurrencyTypeID+"&Rate="+popAskPrice.Rate+"&ProductNo="+escape(ProductNo)+"&ProductName="+escape(ProductName)+"&FromBillNo="+escape(FromBillNo)+"&actionPurchaseAskPrice="+actionPurchaseOrder+"&orderbyPurchaseAskPrice="+orderBy+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_PagerPurchaseAskPrice").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1AskPrice tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
           
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPPurAK\" onclick=\"PPurAKClickChkBox("+i+");\" id=\"ChkBoxPPurAK"+i+"\" value=\""+item.ID+"\"/>"+"</td>"+
                    "<td height='22' style='display:none' id='PPurAKProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' align='center' id='PPurAKProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' align='center'  id='PPurAKProductName"+i+"'   align='center'>" + item.ProductName + "</td>"+
                    "<td height='22' align='center' id='PPurAKstandard" + i + "' align='center'>" + item.standard + "</td>" +
                     "<td height='22' align='center' id='PPurAKColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                    "<td height='22' style='display:none' id='PPurAKUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                    "<td height='22' align='center' id='PPurAKUnit"+i+"' align='center'>" + item.Unit + "</td>"+
                    "<td height='22' align='center' id='PPurAKProductCount"+i+"' align='center'>" + item.ProductCount + "</td>"+
                    "<td height='22' align='center' id='PPurAKUnitPrice"+i+"' align='center'>" + item.UnitPrice + "</td>"+
                    "<td height='22' align='center' id='PPurAKTaxPrice"+i+"' align='center'>" + item.TaxPrice + "</td>"+
//                    "<td height='22' align='center' id='PPurAKDiscount"+i+"' align='center'>" + item.Discount + "</td>"+
                    "<td height='22' align='center' id='PPurAKTaxRate"+i+"' align='center'>" + item.TaxRate + "</td>"+
                    "<td height='22' align='center' id='PPurAKTotalPrice"+i+"' align='center'>" + item.TotalPrice + "</td>"+
                    "<td height='22' align='center' id='PPurAKTotalFee"+i+"' align='center'>" + item.TotalFee + "</td>"+
                    "<td height='22' align='center' id='PPurAKTotalTax"+i+"' align='center'>" + item.TotalTax + "</td>"+
                    "<td height='22' align='center' id='PPurAKRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKApplyReasonID"+i+"' align='center'>" + item.ApplyReasonID + "</td>"+
                    "<td height='22' align='center' id='PPurAKApplyReason"+i+"' align='center'>" + item.ApplyReason + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKFromBillID"+i+"' align='center'>" + item.FromBillID + "</td>"+
                    "<td height='22' align='center' id='PPurAKFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                    "<td height='22' align='center' id='PPurAKFromLineNo"+i+"' align='center'>" + item.FromLineNo + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKProviderID"+i+"' align='center'>" + item.ProviderID + "</td>"+
                    "<td height='22' align='center' id='PPurAKProviderName"+i+"' align='center'>" + item.ProviderName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKCurrencyType"+i+"' align='center'>" + item.CurrencyType + "</td>"+
                    "<td height='22' align='center' id='PPurAKCurrencyTypeName"+i+"' align='center'>" + item.CurrencyTypeName + "</td>"+
                    "<td height='22' align='center' id='PPurAKRate"+i+"' align='center'>" + item.Rate + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKSeller"+i+"' align='center'>" + item.Seller + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKSellerName"+i+"' align='center'>" + item.SellerName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKDeptID"+i+"' align='center'>" + item.DeptID + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKDeptIDName"+i+"' align='center'>" + item.DeptIDName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurAKisAddTax"+i+"' align='center'>"+item.isAddTax+"</td>").appendTo($("#pageDataList1AskPrice tbody"));
                    }
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchaseAskPrice",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkProvider",
                    
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountPurOrder,currentPageIndex:pageIndexProvider,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPageAskPrice({pageindex});return false;"}//[attr]
                    );document.getElementById("btnPPurAKAll").checked = false;

                  totalRecordProvider = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2AskPrice").value=msg.totalCount;
                  $("#ShowPageCountAskPrice").val(pageCountPurOrder);
                  ShowTotalPage(msg.totalCount,pageCountPurOrder,pageIndexProvider,$("#pageAskPricecount"));
                  $("#ToPageAskPrice").val(pageIndexProvider);
                  },
           error: function() {}, 
           complete:function(){hidePopup();    
$("#pageDataList1_PagerPurchaseAskPrice").show();IfshowOrderSelect(document.getElementById("Text2AskPrice").value);pageDataList1("pageDataList1AskPrice","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

 popAskPrice.orderByP=function(orderColum,orderTip)
{
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderBy = orderColum+"_"+ordering;
    TurnToPageAskPrice(1);
}

function PPurAKClickChkBox(rowID)
{
    var ck = document.getElementsByName("ChkBoxPPurAK");
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked && i!= rowID )
        {//选中某行
            if($("#PPurAKProviderID"+i).html() != $("#PPurAKProviderID"+rowID).html())
            {
               
                //如果选择的不是一个合同则不让选
                document.getElementById("ChkBoxPPurAK"+rowID).checked = false;
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的询价明细只能来自一个供应商！");
                return;
            }
        }
    }
}

function Fun_Search_PurchaseAskPrice(aa)
{
      search="1";
      TurnToPageAskPrice(1);
}
function IfshowOrderSelect(count)
    {
        if(count=="0")
        {
            document.getElementById("divAskPriceSelect").style.display = "none";
            document.getElementById("pageAskPricecount").style.display = "none";
        }
        else
        {
            document.getElementById("divAskPriceSelect").style.display="";
            document.getElementById("pageAskPricecount").style.display = "";
        }
    }
    
    function SelectDeptProvider(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexAskPrice(newPageCountProvider,newPageIndexProvider)
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
            TurnToPageAskPrice(parseInt(newPageIndexProvider));
        }
    }
   
   
   


function OptionCheckPPurAKAll()
{
  if(document.getElementById("btnPPurAKAll").checked)
  {
     var ck = document.getElementsByName("ChkBoxPPurAK");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=true ;
        }
  }
  else
  {
    var ck = document.getElementsByName("ChkBoxPPurAK");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=false ;
        }
  }
}    

///遍历选择的是否都是同一个供应商
function CheckOneProvider()
{
    var ck = document.getElementsByName("ChkBoxPPurAK");
    var  getCheck=new Array ();
    for( var i = 0; i <parseInt ( ck.length,10); i++ )
    {
        if ( ck[i].checked )
        {
                  getCheck.push (  $("#PPurAKProviderID"+i).html());
        }
    }  
    if (getCheck.length >1)
    {
         
          for (var a=0;a<getCheck.length;a++)
          {
          if (getCheck [a]==getCheck [0])
          {
          continue ;
          }
          else
          {
          return false;
          break ;
          }
          }
    }
   return true ;
}

function GetValuePPurAK()
{
 
   if ( !CheckOneProvider())
   {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的明细只能来自一个供应商！");
        return ;
   }
    var ck = document.getElementsByName("ChkBoxPPurAK");
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
      if($("#HiddenMoreUnit").val()=="True") {

          FillFromAskPrice($("#PPurAKColorName" + i).html(), $("#PPurAKProductID" + i).html(), $("#PPurAKProductNo" + i).html(), $("#PPurAKProductName" + i).html(), $("#PPurAKstandard" + i).html(), $("#PPurAKUnitID" + i).html(), 
            $("#PPurAKUnit"+i).html(), $("#PPurAKProductCount"+i).html(), $("#PPurAKUnitPrice"+i).html(), $("#PPurAKTaxPrice"+i).html(),  "100.00", 
            $("#PPurAKTaxRate"+i).html(), $("#PPurAKTotalPrice"+i).html(), $("#PPurAKTotalFee"+i).html(), $("#PPurAKTotalTax"+i).html(), $("#PPurAKRequireDate"+i).html(), 
            $("#PPurAKApplyReasonID"+i).html(), $("#PPurAKApplyReason"+i).html(), $("#PPurAKFromBillID"+i).html(), $("#PPurAKFromBillNo"+i).html(), $("#PPurAKFromLineNo"+i).html(), 
            $("#PPurAKProviderID"+i).html(), $("#PPurAKProviderName"+i).html(), $("#PPurAKCurrencyType"+i).html(), $("#PPurAKCurrencyTypeName"+i).html(), $("#PPurAKRate"+i).html(), 
            $("#PPurAKSeller"+i).html(), $("#PPurAKSellerName"+i).html(), $("#PPurAKDeptID"+i).html(), $("#PPurAKDeptIDName"+i).html(), $("#PPurAKisAddTax"+i).html() , $("#PPurAKUsedUnitID"+i).html(), $("#PPurAKUsedUnitCount"+i).html(), $("#PPurAKUsedPrice"+i).html());
            }
            else
            {
                FillFromAskPrice($("#PPurAKColorName" + i).html(), $("#PPurAKProductID" + i).html(), $("#PPurAKProductNo" + i).html(), $("#PPurAKProductName" + i).html(), $("#PPurAKstandard" + i).html(), $("#PPurAKUnitID" + i).html(), 
            $("#PPurAKUnit"+i).html(), $("#PPurAKProductCount"+i).html(), $("#PPurAKUnitPrice"+i).html(), $("#PPurAKTaxPrice"+i).html(),  "100.00", 
            $("#PPurAKTaxRate"+i).html(), $("#PPurAKTotalPrice"+i).html(), $("#PPurAKTotalFee"+i).html(), $("#PPurAKTotalTax"+i).html(), $("#PPurAKRequireDate"+i).html(), 
            $("#PPurAKApplyReasonID"+i).html(), $("#PPurAKApplyReason"+i).html(), $("#PPurAKFromBillID"+i).html(), $("#PPurAKFromBillNo"+i).html(), $("#PPurAKFromLineNo"+i).html(), 
            $("#PPurAKProviderID"+i).html(), $("#PPurAKProviderName"+i).html(), $("#PPurAKCurrencyType"+i).html(), $("#PPurAKCurrencyTypeName"+i).html(), $("#PPurAKRate"+i).html(), 
            $("#PPurAKSeller"+i).html(), $("#PPurAKSellerName"+i).html(), $("#PPurAKDeptID"+i).html(), $("#PPurAKDeptIDName"+i).html(), $("#PPurAKisAddTax"+i).html() );
            }
            
            
            
        }
    }  
    closeAskPricediv();
}



//popAskPrice.ExistInPurOrder = function(productid,frombillid,fromlineno)
//{//判断在采购合同中是否已填充，留作以后判断
//    var PurSortNo=$("#PurPlnFromLineNo"+i).html();
//      var PurFromBillNo=$("#PurPlnFromBillNo"+i).html();
//            if (OrdertExistFrom(PurFromBillNo,PurSortNo))
//            {
//            continue ;
//            }
//}
 
function FillFromAskPrice(ColorName,productid,productno,productname,standard,unitid,unit,productcount,unitprice,taxprice,discount,taxrate,totalprice,totalfee,totaltax,
                            requiredate,applyreasonid,applyreason,frombillid,frombillno,fromLineno,providerid,providername,currencytype,currencytypename,rate,seller,sellername,deptid,deptidname,isaddtax,UsedUnitID,UsedUnitCount,UsedPrice)
{
 
    if(popAskPrice.InputObj == "Order")
    {//采购订单调用
        if(!OrdertExistFrom(frombillno,fromLineno))
    
        {//没有填充过则填充
            var rowID = AddSignRow();
            document.getElementById("DtlSColor" + rowID).value = ColorName;
            document.getElementById("OrderProductID"+rowID).value = productid;
            document.getElementById("OrderProductNo"+rowID).value = productno;
            document.getElementById("OrderProductName"+rowID).value = productname;
            document.getElementById("OrderSpecification"+rowID).value = standard;
            document.getElementById("OrderUnitID"+rowID).value = unitid;
            document.getElementById("OrderUnitName"+rowID).value = unit;
            
            document.getElementById("OrderProductCount"+rowID).value = (parseFloat (productcount)).toFixed($("#HiddenPoint").val());
            var ProductCount2=(parseFloat (productcount)).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderUnitPrice"+rowID).value = (parseFloat (unitprice)).toFixed($("#HiddenPoint").val());
             var UnitPrice2=(parseFloat (unitprice)).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderTaxPrice"+rowID).value = (parseFloat (taxprice)).toFixed($("#HiddenPoint").val());
                   var taxPrice2=(parseFloat (taxprice)).toFixed($("#HiddenPoint").val()); 
            document.getElementById("OrderTaxRate"+rowID).value = (parseFloat (taxrate)).toFixed($("#HiddenPoint").val());
         
            
            document.getElementById("OrderTotalPrice"+rowID).value =   (parseFloat (totalprice)).toFixed($("#HiddenPoint").val());
            
            document.getElementById("OrderTotalFee"+rowID).value =     (parseFloat (totalfee)).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderTotalTax"+rowID).value =(parseFloat (totaltax)).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderRequireDate"+rowID).value = requiredate; 
            document.getElementById("OrderFromBillID"+rowID).value = frombillid;
            document.getElementById("OrderFromBillNo"+rowID).value = frombillno;
            
            document.getElementById("OrderFromLineNo"+rowID).value = fromLineno;
            
            
            document.getElementById("txtPurchaserID").value=seller;
            document.getElementById("UserPurchaserName").value=sellername;
            document.getElementById("UserPurchaserName").disabled = "disabled";
            document.getElementById("txtDeptID").value=deptid;
            document.getElementById("DeptName").value=deptidname;
            document.getElementById("DeptName").disabled = "disabled";
            var isAddTax = isaddtax?true:false;
//            alert(isAddTax)
            document.getElementById("chkIsAddTax").checked=isAddTax;
            document.getElementById("txtProviderID").value = providerid;
            document.getElementById("txtProviderName").value = providername;
            document.getElementById("txtProviderName").disabled = "disabled";     
            
              if($("#HiddenMoreUnit").val()=="False")
            {
                  
            }
            else
            {
            
             GetUnitGroupSelectEx( productid,"InUnit","SignItem_TD_UnitID_Select" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice2+","+taxPrice2+")","unitdiv" + rowID,'',"FillApplyContent("+rowID+","+UnitPrice2+","+taxPrice2+","+ProductCount2+","+UsedUnitCount +",'"+UsedUnitID+"',"+UsedPrice +",'Bill')");//绑定单位组
            }
            
                       
        }
    }
    else
    {//采购合同调用
        if(!ContractExistFromAskPrice(frombillno,fromLineno))
            {
                var Index = findObj("txtTRLastIndex",document).value;
                AddSignRow("Bill");
                var ProductID = 'txtProductID'+Index;
                var ProductNo = 'txtProductNo'+Index;
                var ProductName = 'txtProductName'+Index;
                var Standard = 'txtstandard'+Index;
                var UnitID = 'txtUnitID2'+Index;
                var Unit = 'txtUnitID'+Index;
                var ProductCount = 'txtProductCount'+Index;
                var UnitPrice = 'txtUnitPrice'+Index;
                var TaxPrice = 'txtTaxPrice'+Index;
                var Discount = 'txtDiscount'+Index;
                var TaxRate = 'txtTaxRate'+Index;
                var TotalPrice = 'txtTotalPrice'+Index;
                var TotalFee = 'txtTotalFee'+Index;
                var TotalTax = 'txtTotalTax'+Index;
                var RequireDate = 'txtRequireDate'+Index;
                var ApplyReason = 'txtApplyReason'+Index;
//                var Remark = 'txtRemark'+Index;
                var FromBillID = 'txtFromBillID'+Index;
                var FromBillNo = 'txtFromBillNo'+Index;
                var FromLineNo = 'txtFromLineNo'+Index;
                var HiddTaxPrice = 'hiddTaxPrice'+Index;
                var HiddTaxRate = 'hiddTaxRate'+Index;
                var HiddUnitPrice = 'hiddUnitPrice'+Index;

                document.getElementById("DtlSColor" + Index).value = ColorName;
                document.getElementById(ProductID).value = productid;
                document.getElementById(ProductNo).value = productno;
                document.getElementById(ProductName).value = productname;
                document.getElementById(Standard).value = standard;
                document.getElementById(UnitID).value = unitid;
                document.getElementById(Unit).value = unit;
                document.getElementById(ProductCount).value = (parseFloat(productcount)).toFixed($("#HiddenPoint").val());
                var ProductCount1= (parseFloat(productcount)).toFixed($("#HiddenPoint").val());
                document.getElementById(UnitPrice).value = (parseFloat(unitprice)).toFixed($("#HiddenPoint").val());
                var UnitPrice1= (parseFloat(unitprice)).toFixed($("#HiddenPoint").val());
                document.getElementById(TaxPrice).value = (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
                var taxPrice1= (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
//                document.getElementById(Discount).value = discount;
                document.getElementById(TaxRate).value = (parseFloat(taxrate)).toFixed($("#HiddenPoint").val());
                document.getElementById(TotalPrice).value = totalprice;
                document.getElementById(TotalFee).value = totalfee;
                document.getElementById(TotalTax).value = totaltax;
                document.getElementById(RequireDate).value = requiredate;
                document.getElementById(ApplyReason).value = applyreasonid;
//                document.getElementById(Remark).value = remark;
                document.getElementById(FromBillID).value = frombillid;
                document.getElementById(FromBillNo).value = frombillno;
                document.getElementById(FromLineNo).value = fromLineno;
                
                document.getElementById(HiddTaxPrice).value =  (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
                document.getElementById(HiddTaxRate).value = (parseFloat(taxrate)).toFixed($("#HiddenPoint").val());
                document.getElementById(HiddUnitPrice).value = (parseFloat(unitprice)).toFixed($("#HiddenPoint").val());
                
                document.getElementById("txtHidOurUserID").value=seller;
                document.getElementById("UsertxtSeller").value=sellername;
                document.getElementById("HidDeptID").value=deptid;
                document.getElementById("DeptDeptID").value=deptidname;
                document.getElementById("chkIsZzs").value=isaddtax;
                
                //带出供应商及币种等信息
                document.getElementById("txtHidProviderID").value = providerid;
                document.getElementById("txtProviderID").value = providername;
                document.getElementById("txtHiddenProviderID1").value = providername;
                for(var i=0;i<document.getElementById("drpCurrencyType").options.length;++i)
                {
                    if(document.getElementById("drpCurrencyType").options[i].value.split('_')[0]== currencytype)
                    {
                        $("#drpCurrencyType").attr("selectedIndex",i);
                        break;
                    }
                }
                document.getElementById("CurrencyTypeID").value = currencytype;
                document.getElementById("txtRate").value = rate;
                            
                document.getElementById('txtProviderID').disabled=true;
                document.getElementById('drpCurrencyType').disabled=true;
                    if($("#HiddenMoreUnit").val()=="False")
            {
                  
            }
            else
            {
            
             GetUnitGroupSelectEx( productid,"InUnit","SignItem_TD_UnitID_Select" + Index,"ChangeUnit(this.id,"+Index+","+UnitPrice1+","+taxPrice1+")","unitdiv" + Index,'',"FillApplyContent("+Index+","+UnitPrice1+","+taxPrice1+","+ProductCount1+","+UsedUnitCount +",'"+UsedUnitID+"',"+UsedPrice +",'Bill')");//绑定单位组
            }
                
                
                
                
            }
            var isAddTax = document.getElementById("chkIsZzs").checked;
            //新加币种的汇率问题
            var Rate = document.getElementById("txtRate").value.Trim();
            
            var signFrame = findObj("dg_Log",document);
            for (i = 1; i < signFrame.rows.length; i++)
            {
                if (signFrame.rows[i].style.display != "none")
                {
                    var rowIndex = i;
                    if(isAddTax == true)
                    {//是增值税则
//                        document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("hiddTaxPrice"+rowIndex).value;//含税价等于隐藏域含税价

                        document.getElementById("txtTaxPrice"+rowIndex).value= (parseFloat(document.getElementById("hiddTaxPrice"+rowIndex).value/Rate)).toFixed($("#HiddenPoint").val());;//含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                        document.getElementById("txtUnitPrice"+rowIndex).value = (parseFloat(document.getElementById("hiddUnitPrice"+rowIndex).value/Rate)).toFixed($("#HiddenPoint").val());//单价则改为隐藏域里的单价除以汇率
                        document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value;//税率等于隐藏域税率
                        document.getElementById("chkisAddTaxText1").style.display="inline";
                        document.getElementById("chkisAddTaxText2").style.display="none";
                    }
                    else
                    {
//                        document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("txtUnitPrice"+rowIndex).value;//含税价等于单价
 
                        document.getElementById("txtTaxPrice"+rowIndex).value= (parseFloat(document.getElementById("hiddUnitPrice"+rowIndex).value/Rate)).toFixed($("#HiddenPoint").val());//含税价等于单价 再除以汇率(币种要求修改)
                        document.getElementById("txtUnitPrice"+rowIndex).value =  (parseFloat(document.getElementById("hiddUnitPrice"+rowIndex).value/Rate)).toFixed($("#HiddenPoint").val());//单价则改为隐藏域里的单价除以汇率
                        document.getElementById("txtTaxRate"+rowIndex).value="0.00";//税率等于0
                        document.getElementById("chkisAddTaxText1").style.display="none";
                        document.getElementById("chkisAddTaxText2").style.display="inline";
                    }
                }
            }
//            document.getElementById("txtProviderID").style.display="none";
//            document.getElementById("txtHiddenProviderID1").style.display="block";
            
    
        }
}


</script>