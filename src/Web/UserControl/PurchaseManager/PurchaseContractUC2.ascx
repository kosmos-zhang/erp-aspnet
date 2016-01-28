<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseContractUC2.ascx.cs" Inherits="UserControl_PurchaseManager_PurchaseContractUC2" %>
<div id="divPurchaseContractUC" style="display:none">
   <div id="divPurchaseContractUC3"> <iframe id="frmPurchaseContractUC" >
</iframe></div>
<div id="divPurchaseContractUC2" style="border: solid 10px #93BCDD; background: #fff;
     padding: 10px; width: 1000px; z-index: 20; position: absolute;display:block;
    top: 50%; left: 40%; margin: 5px 0 0 -400px;">
    <table width="100%">
        <tr>
            <td>
                <a onclick="popPurchaseContractUC2.CloseList()" style="text-align: left; cursor: pointer">
                    <img  src="../../../images/Button/Bottom_btn_close.jpg"/></a>
            </td>
        </tr>
        <tr>
            <td>
                <table id="mainindex">
                <tr >
        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
            合同编号
        </td>
        <td width="15%" bgcolor="#FFFFFF">
            <input type="text" id="txtPurConNo" class="tdinput" specialworkcheck="合同编号" 
                style="height: 22px" />
        </td>
        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
            合同主题
        </td>
        <td width="15%" bgcolor="#FFFFFF">
            <input type="text" id="txtPurConTitle" class="tdinput" specialworkcheck="合同主题" />
        </td>
        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
            供应商
        </td>
        <td width="15%" bgcolor="#FFFFFF">
            <input type="text" id="txtPurConProv" class="tdinput" readonly="readonly" onclick="popProviderObj.ShowProviderList('txtPurConProvID','txtPurConProv');" specialworkcheck="供应商" />
            <input type="hidden" id="txtPurConProvID" />
        </td>
        <%--<td width="10%" height="20" bgcolor="#E7E7E7" align="right">
            币种
        </td>
        <td width="15%" bgcolor="#FFFFFF">
            <select name="ddlCurrencyType2" class="tdinput" runat="server" width="119px" id="ddlCurrencyType2" onchange="fnPurConChangeCurrency();">
            </select>
            <input type="hidden"  runat="server" id="hidPurConCurrency" />
        </td>--%>
    </tr>
                </table>
            </td>
        </tr>
        
    <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                    onclick='popPurchaseContractUC2.TurnToPage(1)' id="btn_searchPurCon" />
                    <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                    onclick="fnFillPurContract();" id="imgsure" />
            </td>
        </tr>
    </table>
    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListPurchaseContractUC2"
        bgcolor="#999999">
        <tbody>
            <tr>
                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    选择<input type="checkbox" name="ChkBoxPurContract2" id="ChkBoxPurContract2" onclick="SelectAllPurContract2()" />
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('ContractNo','PurchaseContractUC2ContractNo');return false;">
                        单据编号<span id="PurchaseContractUC2ContractNo" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('ProductNo','PurchaseContractUC2ProductNo');return false;">
                        物品编号<span id="PurchaseContractUC2ProductNo" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('ProductName','PurchaseContractUC2ProductName');return false;">
                        物品名称<span id="PurchaseContractUC2ProductName" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('Specification','PurchaseContractUC2Specification');return false;">
                        规格<span id="PurchaseContractUC2Specification" class="orderTip"></span></div>
                </th>
                  <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('ColorName','PurchaseContractUC2ColorName');return false;">
                        颜色<span id="PurchaseContractUC2ColorName" class="orderTip"></span></div>
                </th>
                
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('ProductCount','PurchaseContractUC2ProductCount');return false;">
                     <span id="sspPurchaseContract2Count">基本数量</span>   <span id="PurchaseContractUC2ProductCount" class="orderTip"></span></div>
                </th>
                 <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchaseContract2Unit">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('UsedUnitCount','PurchaseContractUC2UsedUnitCount');return false;">
                        数量<span id="PurchaseContractUC2UsedUnitCount" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('OrderCount','PurchaseContractUC2OrderCount');return false;">
                        已订购数量<span id="PurchaseContractUC2OrderCount" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('UnitName','PurchaseContractUC2UnitName');return false;">
                       <span id="sspPurchaseContract2Unit">基本单位</span> <span id="PurchaseContractUC2UnitName" class="orderTip"></span></div>
                </th>
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchaseContract2UsedUnitName">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('UsedUnitName','PurchaseContractUC2UsedUnitName');return false;">
                        单位<span id="PurchaseContractUC2UsedUnitName" class="orderTip"></span></div>
                </th>
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('UnitPrice','PurchaseContractUC2UnitPrice');return false;">
                   <span id="sspPurchaseContract2Uniprice">基本单价</span>     <span id="PurchaseContractUC2UnitPrice" class="orderTip"></span></div>
                </th>
                
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchaseContract2UsedPrice">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('UsedPrice','PurchaseContractUC2UsedPrice');return false;">
                        单价<span id="PurchaseContractUC2UsedPrice" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('TaxPrice','PurchaseContractUC2TaxPrice');return false;">
                        含税价<span id="PurchaseContractUC2TaxPrice" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('TaxRate','PurchaseContractUC2TaxRate');return false;">
                        税率<span id="PurchaseContractUC2TaxRate" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('TotalPrice','PurchaseContractUC2TotalPrice');return false;">
                        金额<span id="PurchaseContractUC2TotalPrice" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('TotalFee','PurchaseContractUC2TotalFee');return false;">
                        含税金额<span id="PurchaseContractUC2TotalFee" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('TotalTax','PurchaseContractUC2TotalTax');return false;">
                        税额<span id="PurchaseContractUC2TotalTax" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('RequireDate','PurchaseContractUC2RequireDate');return false;">
                        交货日期<span id="PurchaseContractUC2RequireDate" class="orderTip"></span></div>
                </th>
                <%--<th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('FromBillNo','PurchaseContractUC2FromBillNo');return false;">
                        源单编号<span id="PurchaseContractUC2FromBillNo" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseContractUC2.orderByP('FromLineNo','PurchaseContractUC2FromLineNo');return false;">
                        源单序号<span id="PurchaseContractUC2FromLineNo" class="orderTip"></span></div>
                </th>--%>
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
                        <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                            <div id="PageCountPurchaseContractUC2">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="pageDataList1_PagerPurchaseContractUC2" class="jPagerBar">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="divpagePurchaseContractUC2">
                                <input name="TotalRecordPurchaseContractUC2" type="text" id="TotalRecordPurchaseContractUC2"
                                    style="display: none" />
                                <span id="TotalPagePurchaseContractUC2"></span>每页显示
                                <input name="PerPageCountPurchaseContractUC2" type="text" id="PerPageCountPurchaseContractUC2"
                                    style="width: 24px" />条 转到第
                                <input name="ToPagePurchaseContractUC2" type="text" value="1" id="ToPagePurchaseContractUC2"
                                    style="width: 26px" />页
                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                    width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexPurContract2($('#PerPageCountPurchaseContractUC2').val(),$('#ToPagePurchaseContractUC2').val());" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
</div>
</div>

<script type="text/javascript">
var popPurchaseContractUC2 = new Object();

popPurchaseContractUC2.pageCount = 10;
popPurchaseContractUC2.totalRecord = 0;
popPurchaseContractUC2.pagerStyle = "flickr";
popPurchaseContractUC2.currentPageIndex = 1;
popPurchaseContractUC2.action = "Select";
popPurchaseContractUC2.orderBy = "ContractNo desc";
popPurchaseContractUC2.Currency = null;

popPurchaseContractUC2.orderByP = function(orderColum,orderTip)
{
    var ordering = "desc";
    var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
        ordering = "asc";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↑");
    }
    else
    {
        
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    popPurchaseContractUC2.orderBy = orderColum+" "+ordering;
    popPurchaseContractUC2.TurnToPage(1);
}


function fnPurConChangeCurrency()
{
    var ff = document.getElementById("PurchaseContractUC21_ddlCurrencyType2").value.split('_')[0];
    $("#PurchaseContractUC21_hidPurConCurrency").val(ff);
}

//全选
function SelectAllPurContract2()
{
    $.each($("#pageDataListPurchaseContractUC2 :checkbox"), function(i, obj) {
        obj.checked = $("#ChkBoxPurContract2").attr("checked");
    });
}

function fnGetPurCont()
{
    var str = "";
    str += "No="+$("#txtPurConNo").val();
    str += "&Title="+$("#txtPurConTitle").val();
    str += "&ProviderID="+$("#txtPurConProvID").val();
    str += "&CurrencyID="+popPurchaseContractUC2.Currency;
//    str += "&CurrencyID="+document.getElementById("PurchaseContractUC21_hidPurConCurrency").value;
    return str;
}

popPurchaseContractUC2.ShowList = function(Currency)
{    
    popPurchaseContractUC2.Currency = Currency;
    document.getElementById("divPurchaseContractUC").style.display='block';
    openRotoscopingDiv(true,"divPurchaseContractUC3","frmPurchaseContractUC")
    
    popPurchaseContractUC2.TurnToPage(1)
}

popPurchaseContractUC2.CloseList = function()
{
    document.getElementById("divPurchaseContractUC").style.display='none';
    closeRotoscopingDiv(true,"divPurchaseContractUC3")
}



popPurchaseContractUC2.TurnToPage = function(pageIndex)
{
    popPurchaseContractUC2.currentPageIndex = pageIndex;
 
   if($("#HiddenMoreUnit").val()=="False")
            {
              document .getElementById ("sspPurchaseContract2Unit").style.display ="none"; 
    document .getElementById ("sspPurchaseContract2UsedUnitName").style.display ="none"; 
      document .getElementById ("sspPurchaseContract2UsedPrice").style.display ="none";
      document.getElementById("sspPurchaseContract2Count").innerHTML = "数量";
      document.getElementById("sspPurchaseContract2Unit").innerHTML = "单位";
      document.getElementById("sspPurchaseContract2Uniprice").innerHTML = "单价";
 
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/PurchaseContractUC2.ashx',//目标地址
           cache:false,
           data: "pageIndex="+popPurchaseContractUC2.currentPageIndex+"&pageSize="+popPurchaseContractUC2.pageCount+"&action="+popPurchaseContractUC2.action+
                 "&orderby="+popPurchaseContractUC2.orderBy+"&"+fnGetPurCont(),//数据
           beforeSend:function(){$("#pageDataList1_PagerPurchaseContractUC2").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListPurchaseContractUC2 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ProductID != null && item.ProductID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPurCnt2\" id=\"ChkBoxPurCnt2"+i+"\" onclick=\"PurContractClickChkBox("+i+");\" value=\""+item.ID+"\"  />"+"</td>"+
                        "<td height='22' id='PurCnt2ContractID"+i+"' style='display:none'  align='center'>" + item.ID + "</td>"+
                        "<td height='22' id='PurCnt2ContractNo"+i+"'   align='center'>" + item.ContractNo + "</td>"+
                        "<td height='22' id='PurCnt2SortNo"+i+"'  style='display:none' align='center'>" + item.SortNo + "</td>"+
                        "<td height='22' id='PurCnt2ProductID"+i+"' style='display:none' align='center'>" + item.ProductID + "</td>"+
                        "<td height='22' id='PurCnt2ProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' id='PurCnt2ProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' id='PurCnt2Specification" + i + "' align='center'>" + item.Specification + "</td>" +
                         "<td height='22' id='PurCnt2ColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                        "<td height='22' id='PurCnt2ProductCount"+i+"' align='center'>" +  (parseFloat ( item.ProductCount)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2OrderCount"+i+"' align='center'>" +   (parseFloat ( item.OrderCount)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2DisCount"+i+"' style='display:none' align='center'>" + item.Discount + "</td>"+
                        "<td height='22' id='PurCnt2UnitID"+i+"' style='display:none' align='center'>" + item.UnitID + "</td>"+
                        "<td height='22' id='PurCnt2UnitName"+i+"' align='center'>" + item.UnitName + "</td>"+
                        "<td height='22' id='PurCnt2UnitPrice"+i+"' align='center'>" +  (parseFloat ( item.UnitPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2TaxPrice"+i+"' align='center'>" +   (parseFloat ( item.TaxPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2TaxRate"+i+"' align='center'>" +    (parseFloat ( item.TaxRate)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2TotalPrice"+i+"' align='center'>" +      (parseFloat ( item.TotalPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2TotalFee"+i+"' align='center'>" +   (parseFloat ( item.TotalFee)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2TotalTax"+i+"' align='center'>" +   (parseFloat ( item.TotalTax)).toFixed($("#HiddenPoint").val())+ "</td>"+ 
                        "<td height='22' id='PurCnt2RequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                        "<td height='22' id='PurCnt2FromBillID"+i+"' style='display:none' align='center'>" + item.ID + "</td>").appendTo($("#pageDataListPurchaseContractUC2 tbody"));

                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchaseContractUC2",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:popPurchaseContractUC2.pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:popPurchaseContractUC2.pageCount,currentPageIndex:popPurchaseContractUC2.currentPageIndex,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"popPurchaseContractUC2.TurnToPage({pageindex});return false;"}//[attr]
                    );
                  popPurchaseContractUC2.totalRecord = msg.totalCount;
                  document.getElementById("TotalRecordPurchaseContractUC2").value=msg.totalCount;
                  $("#PerPageCountPurchaseContractUC2").val(popPurchaseContractUC2.pageCount);
                  ShowTotalPage(msg.totalCount,popPurchaseContractUC2.pageCount,pageIndex,$("#PageCountPurchaseContractUC2"));
                  
                  $("#ToPageProductInfoUC").val(popPurchaseContractUC2.pageIndex);
                  },
           error: function() {},
           complete:function(){hidePopup();$("#pageDataList1_PagerPurchaseContractUC2").show();IfshowPurContract2(document.getElementById("TotalRecordPurchaseContractUC2").value);pageDataList1("pageDataListPurchaseContractUC2","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕 
           
           });
           
           }
           else
           {
             $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/PurchaseContractUC2.ashx',//目标地址
           cache:false,
           data: "pageIndex="+popPurchaseContractUC2.currentPageIndex+"&pageSize="+popPurchaseContractUC2.pageCount+"&action="+popPurchaseContractUC2.action+
                 "&orderby="+popPurchaseContractUC2.orderBy+"&"+fnGetPurCont(),//数据
           beforeSend:function(){$("#pageDataList1_PagerPurchaseContractUC2").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListPurchaseContractUC2 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ProductID != null && item.ProductID != "") 
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPurCnt2\" id=\"ChkBoxPurCnt2"+i+"\" onclick=\"PurContractClickChkBox("+i+");\" value=\""+item.ID+"\"  />"+"</td>"+
                        "<td height='22' id='PurCnt2ContractID"+i+"' style='display:none'  align='center'>" + item.ID + "</td>"+
                        "<td height='22' id='PurCnt2ContractNo"+i+"'   align='center'>" + item.ContractNo + "</td>"+
                        "<td height='22' id='PurCnt2SortNo"+i+"'  style='display:none' align='center'>" + item.SortNo + "</td>"+
                        "<td height='22' id='PurCnt2ProductID"+i+"' style='display:none' align='center'>" + item.ProductID + "</td>"+
                        "<td height='22' id='PurCnt2ProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' id='PurCnt2ProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' id='PurCnt2Specification" + i + "' align='center'>" + item.Specification + "</td>" +
                         "<td height='22' id='PurCnt2ColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                        "<td height='22' id='PurCnt2ProductCount"+i+"' align='center' >" +   (parseFloat ( item.ProductCount)).toFixed($("#HiddenPoint").val())+ "</td>"+  
                        "<td height='22' id='PurCnt2UsedUnitCount"+i+"' align='center'>" + (parseFloat ( item.UsedUnitCount)).toFixed($("#HiddenPoint").val())+ "</td>"+  
                        "<td height='22' id='PurCnt2OrderCount"+i+"' align='center'  >" + (parseFloat ( item.OrderCount)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2DisCount"+i+"' style='display:none' align='center'>" + item.Discount + "</td>"+
                        "<td height='22' id='PurCnt2UnitID"+i+"' style='display:none' align='center'>" + item.UnitID + "</td>"+
                         "<td height='22' id='PurCnt2UsedUnitID"+i+"' style='display:none' align='center'>" + item.UsedUnitID + "</td>"+
                        "<td height='22' id='PurCnt2UnitName"+i+"' align='center'  >" + item.UnitName + "</td>"+
                          "<td height='22' id='PurCnt2UsedUnitName"+i+"' align='center'>" + item.UsedUnitName + "</td>"+ 
                        "<td height='22' id='PurCnt2UnitPrice"+i+"' align='center'>" +  (parseFloat ( item.UnitPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+ 
                           "<td height='22' id='PurCnt2UsedPrice"+i+"' align='center'>" +(parseFloat ( item.UsedPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+ 
                        "<td height='22' id='PurCnt2TaxPrice"+i+"' align='center'>" +  (parseFloat ( item.TaxPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2TaxRate"+i+"' align='center'>" + (parseFloat ( item.TaxRate)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2TotalPrice"+i+"' align='center'>" + (parseFloat ( item.TotalPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurCnt2TotalFee"+i+"' align='center'>" +  (parseFloat ( item.TotalFee)).toFixed($("#HiddenPoint").val()) + "</td>"+
                        "<td height='22' id='PurCnt2TotalTax"+i+"' align='center'>" +   (parseFloat ( item.TotalTax)).toFixed($("#HiddenPoint").val())+ "</td>"+ 
                        "<td height='22' id='PurCnt2RequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                        "<td height='22' id='PurCnt2FromBillID"+i+"' style='display:none' align='center'>" + item.ID + "</td>").appendTo($("#pageDataListPurchaseContractUC2 tbody"));

                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchaseContractUC2",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:popPurchaseContractUC2.pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:popPurchaseContractUC2.pageCount,currentPageIndex:popPurchaseContractUC2.currentPageIndex,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"popPurchaseContractUC2.TurnToPage({pageindex});return false;"}//[attr]
                    );
                  popPurchaseContractUC2.totalRecord = msg.totalCount;
                  document.getElementById("TotalRecordPurchaseContractUC2").value=msg.totalCount;
                  $("#PerPageCountPurchaseContractUC2").val(popPurchaseContractUC2.pageCount);
                  ShowTotalPage(msg.totalCount,popPurchaseContractUC2.pageCount,pageIndex,$("#PageCountPurchaseContractUC2"));
                  
                  $("#ToPageProductInfoUC").val(popPurchaseContractUC2.pageIndex);
                  },
           error: function() {},
           complete:function(){hidePopup();$("#pageDataList1_PagerPurchaseContractUC2").show();IfshowPurContract2(document.getElementById("TotalRecordPurchaseContractUC2").value);pageDataList1("pageDataListPurchaseContractUC2","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕 
           
           });
           
           
           }
           
           
}


//改变每页记录数及跳至页数
function ChangePageCountIndexPurContract2(newPageCountProvider,newPageIndexProvider)
{
    if(newPageCountProvider <=0 || newPageIndexProvider <= 0 ||  newPageIndexProvider  > ((popPurchaseContractUC2.totalRecord-1)/newPageCountProvider)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        popPurchaseContractUC2.pageCount=parseInt(newPageCountProvider);
        popPurchaseContractUC2.TurnToPage(parseInt(newPageIndexProvider));
    }
}

function IfshowPurContract2(count)
{
    if(count=="0")
    {
        document.getElementById("divpagePurchaseContractUC2").style.display = "none";
        document.getElementById("PageCountPurchaseContractUC2").style.display = "none";
    }
    else
    {
        document.getElementById("divpagePurchaseContractUC2").style.display = "block";
        document.getElementById("PageCountPurchaseContractUC2").style.display = "block";
    }
}

function fnFillPurContract()
{
    DeleteAll();
    var ck = document.getElementsByName("ChkBoxPurCnt2");
    var ID=0;
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {//选中某行，将该行的值赋到界面上
            ID = $("#PurCnt2ContractID"+i).html();
            var rowID = AddSignRow("From"); //添加明细行


            document.getElementById("DtlSColor" + rowID).value = $("#PurCnt2ColorName" + i).html();
            document.getElementById("OrderProductID"+rowID).value = $("#PurCnt2ProductID"+i).html();
            document.getElementById("OrderProductNo"+rowID).value = $("#PurCnt2ProductNo"+i).html();
            document.getElementById("OrderProductName"+rowID).value = $("#PurCnt2ProductName"+i).html();
            document.getElementById("OrderSpecification"+rowID).value = $("#PurCnt2Specification"+i).html();
            document.getElementById("OrderUnitID"+rowID).value = $("#PurCnt2UnitID"+i).html();
            document.getElementById("OrderUnitName"+rowID).value = $("#PurCnt2UnitName"+i).html();
              var producount1=(parseFloat($("#PurCnt2ProductCount"+i).html())-parseFloat($("#PurCnt2OrderCount"+i).html())).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderProductCount"+rowID).value = producount1;
                
              var unitPrice1=(parseFloat ($("#PurCnt2UnitPrice"+i).html())).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderUnitPrice"+rowID).value = unitPrice1;
            var taxPrice1=(parseFloat ($("#PurCnt2TaxPrice"+i).html())).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderTaxPrice"+rowID).value = taxPrice1;
            document.getElementById("OrderTaxPriceHide"+rowID).value = taxPrice1;
            
      
//            document.getElementById("OrderDiscount"+rowID).value = $("#PurCnt2DisCount"+i).html();
            document.getElementById("OrderTaxRate"+rowID).value = (parseFloat ($("#PurCnt2TaxRate"+i).html())).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderTaxRateHide"+rowID).value = (parseFloat ($("#PurCnt2TaxRate"+i).html())).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderTotalPrice"+rowID).value =  (parseFloat ($("#PurCnt2TotalPrice"+i).html())).toFixed($("#HiddenPoint").val());
            
            document.getElementById("OrderTotalFee"+rowID).value =  (parseFloat ($("#PurCnt2TotalFee"+i).html())).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderTotalTax"+rowID).value =  (parseFloat ($("#PurCnt2TotalTax"+i).html())).toFixed($("#HiddenPoint").val());
            document.getElementById("OrderRequireDate"+rowID).value = $("#PurCnt2RequireDate"+i).html();
//            document.getElementById("OrderRemark"+rowID).value = $("#PurCnt2ProductID"+i).html();
            document.getElementById("OrderFromBillID"+rowID).value = $("#ChkBoxPurCnt2"+i).val();
            document.getElementById("OrderFromBillNo"+rowID).value = $("#PurCnt2ContractNo"+i).html();
            
            document.getElementById("OrderFromLineNo"+rowID).value = $("#PurCnt2SortNo"+i).html();
//            document.getElementById("OrderRemark"+rowID).value = $("#PurCnt2ProductID"+i).html();
            

             if($("#HiddenMoreUnit").val()=="False")
            {
            fnMergeDetail();
            }
            else
            {
                  


            var UsedUnitID=$("#PurCnt2UsedUnitID"+i).html();
            var UsedUnitCount=$("#PurCnt2UsedUnitCount"+i).html();
            var UsedPrice=$("#PurCnt2UsedPrice"+i).html();
             GetUnitGroupSelectEx($("#PurCnt2ProductID"+i).html(),"InUnit","SignItem_TD_UnitID_Select" + rowID,"ChangeUnit(this.id,"+rowID+","+unitPrice1+","+taxPrice1+")","unitdiv" + rowID,'',"FillApplyContent("+rowID+","+unitPrice1+","+taxPrice1+","+producount1+","+UsedUnitCount+",'"+UsedUnitID+"',"+UsedPrice+",'Bill')");//绑定单位组
            }
        }
    }
    
    
    fnFillPurContractPri(ID);
    
    popPurchaseContractUC2.CloseList();
    //币种不可选
    $("#ddlCurrencyType").attr("disabled","disabled");
}

function fnFillPurContractPri(ID)
{//将合同主表信息带入
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/PurchaseContractUC2.ashx',//目标地址
           cache:false,
           data: "action=Pri&ID="+ID,
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                $.each(msg.data,function(i,item)
                {
                    //采购员
                    if(document.getElementById("txtPurchaserID").value == "")
                    {
                        document.getElementById("UserPurchaserName").value = item.PurchaserName;
                        document.getElementById("txtPurchaserID").value = item.Purchaser;
                    }
                    //部门
                    if(document.getElementById("txtDeptID").value == "")
                    {
                        document.getElementById("DeptName").value = item.DeptName;
                        document.getElementById("txtDeptID").value = item.DeptID;
                    }
                    //供应商
                    if(document.getElementById("txtProviderID").value == "")
                    {
                        document.getElementById("txtProviderName").value = item.ProviderName;
                        document.getElementById("txtProviderID").value = item.ProviderID;
                    }
                    //币种
                    for(var i=0;i<document.getElementById("ddlCurrencyType").options.length;++i)
                    {
                        if(document.getElementById("ddlCurrencyType").options[i].value.split('_')[0]== item.CurrencyType)
                        {
                            $("#ddlCurrencyType").attr("selectedIndex",i);
                            break;
                        }                        
                    }
                    //汇率
                    document.getElementById("txtExchangeRate").value = item.Rate;
                    //我方代表
                    if(document.getElementById("txtOurDelegateID").value == "")
                    {
                        document.getElementById("UserOurDelegate").value = item.OurDelegateName;
                        document.getElementById("txtOurDelegateID").value = item.OurDelegate;
                    }
                    //对方代表
                    if(document.getElementById("txtTheyDelegate").value == "")
                    {
                        document.getElementById("txtTheyDelegate").value = item.TheyDelegate;
                    }
                    //增值税
                    var isAddTax = (item.isAddTax == 1)?true:false;
                    document.getElementById("chkIsAddTax").checked = isAddTax;
                    
            
             
                    if( item.CarryType != "0")
                    {
                        document.getElementById("ddlCarryType_ddlCodeType").value = item.CarryType;
                    } 
                      if( item.PayType != "0")
                    {
                        document.getElementById("ddlPayType_ddlCodeType").value = item.PayType;
                    } 
                    
                   if( item.MoneyType != "0")
                    {
                        document.getElementById("ddlMoneyType_ddlCodeType").value = item.MoneyType;
                    } 
                       if( item.TakeType != "0")
                    {
                        document.getElementById("ddlTakeType_ddlCodeType").value = item.TakeType;
                    } 
                          if( item.TypeID != "0")
                    {
                        document.getElementById("ddlTypeID_ddlCodeType").value = item.TypeID;
                    } 
                     
                    
                })
            },
           error: function() {},
           complete:function(){}//接收数据完毕 
           
           });
}



function PurContractClickChkBox(rowID)
{
    //判断选择的是不是来自同一个单据
    var ck = document.getElementsByName("ChkBoxPurCnt2");
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked && i!= rowID )
        {//选中某行
            if($("#PurCnt2ContractNo"+i).html() != $("#PurCnt2ContractNo"+rowID).html())
            {
                //如果选择的不是一个合同则不让选
                document.getElementById("ChkBoxPurCnt2"+rowID).checked = false;
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的合同明细只能来自一个合同！");
                return;
            }
        }
    }
        
    IfSelectAll('ChkBoxPurCnt2','ChkBoxPurContract2');
}

</script>