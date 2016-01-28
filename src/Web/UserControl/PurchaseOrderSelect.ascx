<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseOrderSelect.ascx.cs"
    Inherits="UserControl_PurchaseOrderSelect" %>
<style type="text/css">
    .tdinput
    {
        border-width: 0pt;
        background-color: #ffffff;
        height: 21px;
        margin-left: 2px;
    }
</style>
<div id="divPurchaseOrderhhh" style="display: none">
    <div id="divPurchaseOrder">
        <iframe id="frmPurchaseOrder"></iframe>
    </div>
    <div id="divPurchaseOrder" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 1000px; z-index: 20; position: absolute; display: block; top: 35%; left: 40%;
        margin: 5px 0 0 -400px;">
        <table width="99%">
            <tr>
                <td>
                    <a onclick="closeOrderdiv()" style="text-align: right; cursor: pointer">
                        <img src="../../../images/Button/Bottom_btn_close.jpg" /></a>
                </td>
            </tr>
        </table>
        <table width="99%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClickOrder'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('PurchaseOrdersearchtable','searchClickOrder')" /></div>
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
                    <table width="100%" border="0" align="left" cellpadding="0" id="PurchaseOrdersearchtable"
                        cellspacing="0" bgcolor="#CCCCCC">
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
                                            <input type="text" id="txt_ProductName" class="tdinput" />
                                        </td>
                                        <td width="10%" bgcolor="#E7E7E7" align="right">
                                            源单编号
                                        </td>
                                        <td width="24%" bgcolor="#FFFFFF">
                                            <input type="text" id="txt_FromBillNo" class="tdinput" />
                                        </td>
                                    </tr>
                                    <tr id="trPurchaseArriveNewAttr" style="display: none">
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <span id="PurchaseArrivespanOther" style="display: none">其他条件</span>
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <div id="divPurchaseArriveProductDiyAttr" style="display: none">
                                                <select id="selPurchaseArriveEFIndex" onchange="clearPurchaseArriveEFDesc();">
                                                </select>
                                                <input type="text" id="txtPurchaseArriveEFDesc" style="width: 30%" />
                                            </div>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" colspan="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                onclick='Fun_Search_PurchaseOrder()' id="btn_search" />
                                            <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                                                onclick="GetValuePPurArrive();" id="imgsure" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="divDetail" style="width: 99%; overflow-x: auto; overflow-y: auto; " border="0" align="center" cellpadding="0" cellspacing="1">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1Order" 
                bgcolor="#999999">
                <tbody>
                    <tr>
                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" id="btnPPurArriveAll" name="btnPPurArriveAll" onclick="OptionCheckPPurArriveAll()"</th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('ProductNo','ss1');return false;">物品编号<span id="ss1" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('ProductName','ss2');return false;">物品名称<span id="ss2" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('standard','ss3');return false;">规格<span id="Span1" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('ColorName','sColor');return false;">颜色<span id="sColor" class="orderTip"></span></div></th>
                        <th id="thUsedUnitName_divPurchaseOrder" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('UsedUnitName','ss21');return false;">基本单位<span id="ss21" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('UnitName','ss4');return false;">单位<span id="ss4" class="orderTip"></span></div></th>
                        <th id="thUsedUnitCount_divPurchaseOrder" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('UsedUnitCount','ss22');return false;">基本数量<span id="ss22" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('ProductOrder','ss5');return false;">采购数量<span id="ss5" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('RequireDate','ss6');return false;">交货日期<span id="ss6" class="orderTip"></span></div></th>
                        <th id="thUsedPrice_divPurchaseOrder" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('UsedPrice','ss23');return false;">基本单价<span id="ss23" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('UnitPrice','ss7');return false;">单价<span id="ss7" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('TaxPrice','ss8');return false;">含税价<span id="ss8" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('Discount','ss9');return false;">折扣(%)<span id="ss9" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('TaxRate','ss10');return false;">税率(%)<span id="ss10" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('TotalPrice','ss11');return false;">金额<span id="ss11" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('TotalFee','ss12');return false;">含税金额<span id="ss12" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('TotalTax','ss13');return false;">税额<span id="ss13" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('Remark','ss14');return false;">备注<span id="ss14" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('FromBillNo','ss15');return false;">源单编号<span id="ss15" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('FromLineNo','ss16');return false;">源单序号<span id="ss16" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('ArrivedCount','ss17');return false;">已到货数量<span id="ss17" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('ProviderName','ss18');return false;">供应商<span id="ss18" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('CurrencyTypeName','ss19');return false;">币种<span id="ss19" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="popOrder.orderByP('Rate','ss20');return false;">汇率<span id="ss20" class="orderTip"></span></div></th>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
            class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                <div id="pageOrdercount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerPurchaseOrder" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divOrderSelect">
                                    <input name="text" type="text" id="Text2Order" style="display: none" />
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountOrder" style="width: 22px" />条 转到第
                                    <input name="text" type="text" id="ToPageOrder" style="width: 35px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexOrder($('#ShowPageCountOrder').val(),$('#ToPageOrder').val());" />
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

<script language="javascript">


var popOrder=new Object();
popOrder.InputObj = null;
popOrder.ProviderID= null;
popOrder.CurrencyTypeID= null;
popOrder.Rate= null;
popOrder.isMoreUnit=null;

    var pageCountPurOrder = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var currentPageIndex = 1;
    var popOrderorderBy = "ProductNo_d";//排序字段
    
    var actionPurchaseOrder = "";//操作
    popOrder.orderByP = function(orderColum,orderTip)
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
   popOrderorderBy = orderColum+"_"+ordering;
  TurnToPageOrder(1);
}

    popOrder.ShowList = function(objInput,ProviderID,CurrencyTypeID,Rate,isMoreUnit)
{
    popOrder.InputObj = objInput;
    popOrder.ProviderID = ProviderID;
    popOrder.CurrencyTypeID = CurrencyTypeID;
    popOrder.Rate = Rate;
    popOrder.isMoreUnit=isMoreUnit;
    document.getElementById('divPurchaseOrderhhh').style.display='block';
    openRotoscopingDiv(true,"divPurchaseOrder","frmPurchaseOrder")
    SetCloumnHide();
    TurnToPageOrder(1);
}

// 隐藏列
function SetCloumnHide()
{
    if(!popOrder.isMoreUnit)
    {// 隐藏非多计量单位
        $("#thUsedUnitName_divPurchaseOrder").hide();
        $("#thUsedUnitCount_divPurchaseOrder").hide();
        $("#thUsedPrice_divPurchaseOrder").hide();
    }
}


function closeOrderdiv()
{

    document.getElementById("divPurchaseOrderhhh").style.display="none";
    closeRotoscopingDiv(true,"divPurchaseOrder")
}


function OptionCheckPPurArriveAll()
{
  if(document.getElementById("btnPPurArriveAll").checked)
  {
     var ck = document.getElementsByName("ChkBoxPPurArrive");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=true ;
        }
  }
  else
  {
    var ck = document.getElementsByName("ChkBoxPPurArrive");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=false ;
        }
  }
}    

    function clearPurchaseArriveEFDesc()
{
    if($("#selPurchaseArriveEFIndex").val()=="-1")
   {
    return;
   }
    $("#txtPurchaseArriveEFDesc").val("");
}



function fnPurchaseArriveGetExtAttrOther() {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx", //目标地址
        cache: false,
        data: 'action=all',
        beforeSend: function() { AddPop();  }, //发送数据之前

        success: function(msg) {
  
  
            
            //数据获取完毕，填充页面据显示
            //存在扩展属性显示页面扩展属性表格
            if (parseInt(msg.totalCount) > 0) {
                $("<option value=\" \">--请选择--</option>").appendTo($("#selPurchaseArriveEFIndex"));
                $("#divPurchaseArriveProductDiyAttr").show();
                $("#PurchaseArrivespanOther").show();
                $("#trPurchaseArriveNewAttr").show();
                $.each(msg.data, function(i, item) 
                {
                    $("<option value=\""+item.EFIndex+"\">"+item.EFDesc+"</option>").appendTo($("#selPurchaseArriveEFIndex"));
                });
                document.getElementById("selPurchaseArriveEFIndex").selectedIndex=0;
            }
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); } //接收数据完毕
    });
} 

    //jQuery-ajax获取JSON数据
    function TurnToPageOrder(pageIndexProvider)
    {

           currentPageIndexProvider = pageIndexProvider;

           var ProviderID= "";
           var ProviderNo="";
           var ProviderName="";
           
           var ProductNo= $("#txt_ProductNo").val().Trim();
           var ProductName= $("#txt_ProductName").val().Trim();
           var FromBillNo= $("#txt_FromBillNo").val().Trim();
           var PurchaseArriveEFIndex="";
           var PurchaseArriveEFDesc="";
          if(document.getElementById("trPurchaseArriveNewAttr").style.display!="none")
          {
            PurchaseArriveEFIndex=document.getElementById("selPurchaseArriveEFIndex").value;
            PurchaseArriveEFDesc=document.getElementById("txtPurchaseArriveEFDesc").value;
          }
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseOrderSelect.ashx',//目标地址
           cache:false,
           data: "pageIndexPurchaseOrder="+pageIndexProvider
                    +"&pageCountPurchaseOrder="+pageCountPurOrder
                    +"&Provider00ID="+popOrder.ProviderID
                    +"&CurrencyTypeID="+popOrder.CurrencyTypeID
                    +"&Rate="+popOrder.Rate
                    +"&ProductNo="+escape(ProductNo)
                    +"&ProductName="+escape(ProductName)
                    +"&PurchaseArriveEFIndex="+escape(PurchaseArriveEFIndex)
                    +"&PurchaseArriveEFDesc="+escape(PurchaseArriveEFDesc)
                    +"&FromBillNo="+escape(FromBillNo)
                    +"&actionPurchaseOrder="+actionPurchaseOrder
                    +"&orderbyPurchaseOrder="+popOrderorderBy+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_PagerPurchaseOrder").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1Order tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPPurArrive\" onclick=\"PurArriveClickChkBox("+i+")\"  id=\"ChkBoxPPurArrive"+i+"\" value=\""+item.ID+"\"/>"+"</td>"+
                     "<td height='22' style='display:none' id='PPurArriveProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' align='center' id='PPurArriveProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' align='center' id='PPurArriveProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                    "<td height='22' align='center' id='PPurArrivestandard"+i+"' align='center'>" + item.standard + "</td>"+
                    "<td height='22' align='center' id='PPurArriveColorName"+i+"' align='center'>" + item.ColorName + "</td>"+
                    (popOrder.isMoreUnit?(
                    "<td height='22' style='display:none' id='PPurArriveUsedUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                    "<td height='22' align='center' id='PPurArriveUsedUnitName"+i+"' align='center'>" + item.UnitName + "</td>"+
                    "<td height='22' style='display:none' id='PPurArriveUnitID"+i+"' align='center'>" + item.UsedUnitID + "</td>"+
                    "<td height='22' align='center' id='PPurArriveUnitName"+i+"' align='center'>" + item.UsedUnitName + "</td>"+
                    "<td height='22' align='center' id='PPurArriveUsedUnitCount"+i+"' align='center'>" + item.ProductOrder + "</td>"+
                    "<td height='22' align='center' id='PPurArriveProductOrder"+i+"' align='center'>" + item.UsedUnitCount + "</td>")
                    :("<td height='22' style='display:none' id='PPurArriveUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                    "<td height='22' align='center' id='PPurArriveUnitName"+i+"' align='center'>" + item.UnitName + "</td>"+
                    "<td height='22' align='center' id='PPurArriveProductOrder"+i+"' align='center'>" + item.ProductOrder + "</td>"))+
                    "<td height='22' align='center' id='PPurArriveRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                    (popOrder.isMoreUnit?("<td height='22' align='center' id='PPurArriveUsedPrice"+i+"' align='center'>" +FormatAfterDotNumber(item.UnitPrice,selPoint) + "</td>"+
                    "<td height='22' align='center' id='PPurArriveUnitPrice"+i+"' align='center'>" +FormatAfterDotNumber(item.UsedPrice,selPoint) + "</td>"):
                    "<td height='22' align='center' id='PPurArriveUnitPrice"+i+"' align='center'>" +FormatAfterDotNumber(item.UnitPrice,selPoint) + "</td>")+
                    "<td height='22' align='center' id='PPurArriveTaxPrice"+i+"' align='center'>" + FormatAfterDotNumber(item.TaxPrice,selPoint) + "</td>"+
                    "<td height='22' align='center' id='PPurArriveDiscount"+i+"' align='center'>" + FormatAfterDotNumber(item.Discount,selPoint) + "</td>"+
                    "<td height='22' align='center' id='PPurArriveTaxRate"+i+"' align='center'>" + FormatAfterDotNumber(item.TaxRate,selPoint) + "</td>"+
                    "<td height='22' align='center' id='PPurArriveTotalPrice"+i+"' align='center'>" + FormatAfterDotNumber(item.TotalPrice,selPoint) + "</td>"+
                    "<td height='22' align='center' id='PPurArriveTotalFee"+i+"' align='center'>" + FormatAfterDotNumber(item.TotalFee,selPoint) + "</td>"+
                    "<td height='22' align='center' id='PPurArriveTotalTax"+i+"' align='center'>" + FormatAfterDotNumber(item.TotalTax,selPoint) + "</td>"+
                    "<td height='22' align='center' id='PPurArriveRemark"+i+"' align='center'>" + item.Remark + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurArriveFromBillID"+i+"' align='center'>" + item.FromBillID + "</td>"+
                    "<td height='22' align='center' id='PPurArriveFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                    "<td height='22' align='center' id='PPurArriveFromLineNo"+i+"' align='center'>" + item.FromLineNo + "</td>"+
                    "<td height='22' align='center' id='PPurArriveArrivedCount"+i+"' align='center'>" + FormatAfterDotNumber(item.ArrivedCount,selPoint) + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurArriveProviderID"+i+"' align='center'>" + item.ProviderID + "</td>"+
                    "<td height='22' align='center' id='PPurArriveProviderName"+i+"' align='center'>" + item.ProviderName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurArriveCurrencyType"+i+"' align='center'>" + item.CurrencyType + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurArrivePurchaser"+i+"' align='center'>" + item.Purchaser + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PPurArrivePurchaserName"+i+"' align='center'>" + item.PurchaserName + "</td>"+
                    "<td height='22' align='center' id='PPurArriveCurrencyTypeName"+i+"' align='center'>" + item.CurrencyTypeName + "</td>"+
                    "<td height='22' align='center' id='PPurArriveRate"+i+"' align='center'>"+FormatAfterDotNumber(item.Rate,4)+"</td>").appendTo($("#pageDataList1Order tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchaseOrder",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkProvider",
                    
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountPurOrder,currentPageIndex:pageIndexProvider,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPageOrder({pageindex});return false;"}//[attr]
                    );document.getElementById("btnPPurArriveAll").checked = false;

                  totalRecordProvider = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2Order").value=msg.totalCount;
                  $("#ShowPageCountOrder").val(pageCountPurOrder);
                  ShowTotalPage(msg.totalCount,pageCountPurOrder,pageIndexProvider,$("#pageOrdercount"));
                  $("#ToPageOrder").val(pageIndexProvider);
                  },
           error: function() {}, 
           complete:function(){hidePopup();    
$("#pageDataList1_PagerPurchaseOrder").show();IfshowOrderSelect(document.getElementById("Text2Order").value);pageDataList1("pageDataList1Order","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
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


function PurArriveClickChkBox(rowID)
{
    var ck = document.getElementsByName("ChkBoxPPurArrive");
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked && i!= rowID )
        {//选中某行
            if($("#PPurArriveProviderID"+i).html() != $("#PPurArriveProviderID"+rowID).html())
            {               
                //如果选择的不是一个合同则不让选
                document.getElementById("ChkBoxPPurArrive"+rowID).checked = false;
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的订单明细只能来自一个供应商！");
                return;
            }
            if($("#PPurArriveCurrencyType"+i).html() != $("#PPurArriveCurrencyType"+rowID).html() || $("#PPurArriveRate"+i).html() != $("#PPurArriveRate"+rowID).html())
            {               
                //如果选择的不是一个合同则不让选
                document.getElementById("ChkBoxPPurArrive"+rowID).checked = false;
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的订单明细的币种和汇率必须相同！");
                return;
            }
        }
    }
}

function Fun_Search_PurchaseOrder(aa)
{
      search="1";
      TurnToPageOrder(1);
}
function IfshowOrderSelect(count)
    {
        if(count=="0")
        {
            document.getElementById("divOrderSelect").style.display = "none";
            document.getElementById("pageOrdercount").style.display = "none";
        }
        else
        {
            document.getElementById("divOrderSelect").style.display="";
            document.getElementById("pageOrdercount").style.display = "";
        }
    }
    
    function SelectDeptProvider(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexOrder(newPageCountProvider,newPageIndexProvider)
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
            TurnToPageOrder(parseInt(newPageIndexProvider));
        }
    }
    

</script>

