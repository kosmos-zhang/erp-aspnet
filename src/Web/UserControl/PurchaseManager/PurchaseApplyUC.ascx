<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseApplyUC.ascx.cs"
    Inherits="UserControl_PurchaseManager_PurchaseApplyUC" %>
<div id="divPurchaseApplyUC" style="display:none">
   <div id="divPurchaseApplyUC2"> <iframe id="frmPurchaseApplyUC" >
</iframe></div>
<div  style="border: solid 10px #93BCDD; background: #fff;
    padding: 10px; width: 1000px; z-index: 1001; position: absolute; top: 50%; left: 40%;
    margin: 5px 0 0 -400px;">
    <table width="100%">
        <tr>
            <td>
          
                <a onclick="popPurchaseApplyUC.CloseList()" style="text-align: left; cursor: pointer">
                    <img  src="../../../images/Button/Bottom_btn_close.jpg"/></a>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
        class="table">
        <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                物品编号
            </td>
            <td width="15%" bgcolor="#FFFFFF">
                <input type="text" id="txtProdNoPurApp" class="tdinput" />
            </td>
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                物品名称
            </td>
            <td width="15%" bgcolor="#FFFFFF">
                <input type="text" id="txtProdNamePurApp" class="tdinput" />
            </td>
            <td width="10%" bgcolor="#E7E7E7" align="right">
                需求时间
            </td>
            <td bgcolor="#FFFFFF" style="width: 15%">
                <input type="text" id="txtStartDatePurApp" class="tdinput" readonly="readonly" onclick="WdatePicker()"/>
            </td>
            <td width="2%" bgcolor="#E7E7E7" align="right">
                ~
            </td>
            <td bgcolor="#FFFFFF" style="width: 15%">
                <input type="text" id="txtEndDatePurApp"  class="tdinput" readonly="readonly" onclick="WdatePicker()"/>
                <input type="text" id="hidPurAppSltCnd" style="display:none"  class="tdinput" />
            </td>
        </tr>
            <tr id="trPurchaseArriveNewAttr" style="display:none">  
                                     <td height="20" align="right" class="tdColTitle" width="10%">
                                      <span id="PurchaseArrivespanOther" style="display:none">其他条件</span>
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                             <div id="divPurchaseArriveProductDiyAttr" style="display:none" >
<select id="selPurchaseArriveEFIndex"   onchange="clearPurchaseArriveEFDesc();"></select><input  type="text" id="txtPurchaseArriveEFDesc"  style="width:30%"/>
</div>
                                          </td>
                                        <td height="20" align="right" class="tdColTitle" colspan="6">
                                          
                                        </td>
                                        </tr>
        <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                    onclick='fnGetPurApp()' id="btn_search" />
                <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                    onclick="fnFillPurApp();" id="imgsure" />
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListPurchaseApplyUC"
        bgcolor="#999999">
        <tbody>
            <tr>
                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    选择
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('ProductNo','PurchaseApplyUCProductNo');return false;">
                        物品编号<span id="PurchaseApplyUCProductNo" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('ProductName','PurchaseApplyUCProductName');return false;">
                        物品名称<span id="PurchaseApplyUCProductName" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('Specification','PurchaseApplyUCSpecification');return false;">
                        规格<span id="PurchaseApplyUCSpecification" class="orderTip"></span></div>
                </th>
                 <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('ColorName','PurchaseApplyUCColorName');return false;">
                        颜色<span id="PurchaseApplyUCColorName" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('UnitName','PurchaseApplyUCUnitName');return false;">
                      <span id="sspPurchaseApplyUnit">基本单位</span>  <span id="PurchaseApplyUCUnitName" class="orderTip"></span></div>
                </th>
                 <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchaseApplyUsedUnitName">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('UsedUnitName','PurchaseApplyUCUsedUnitName');return false;">
                        单位<span id="PurchaseApplyUCUsedUnitName" class="orderTip"></span></div>
                </th>
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('UnitPrice','PurchaseApplyUCUnitPrice');return false;">
                        单价<span id="PurchaseApplyUCUnitPrice" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('StandardBuy','PurchaseApplyUCStandardBuy');return false;">
                        含税价<span id="PurchaseApplyUCStandardBuy" class="orderTip"></span></div>
                </th>
                
                
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchaseApplyUsedUnitCount">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('UsedUnitCount','PurchaseApplyUCUsedUnitCount');return false;">
                        需求数量<span id="PurchaseApplyUCUsedUnitCount" class="orderTip"></span></div>
                </th>
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('RequireCount','PurchaseApplyUCRequireCount');return false;">
                      <span id="sspPurchaseApplyCount">  基本数量</span><span id="PurchaseApplyUCRequireCount" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('PlanedCount','Span1');return false;">
                        已计划数量<span id="Span1" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('RequireDate','PurchaseApplyUCRequireDate');return false;">
                        需求日期<span id="PurchaseApplyUCRequireDate" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('ApplyReasonName','PurchaseApplyUCApplyReasonName');return false;">
                        申请原因<span id="PurchaseApplyUCApplyReasonName" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('FromBillNo','PurchaseApplyUCFromBillNo');return false;">
                        源单编号<span id="PurchaseApplyUCFromBillNo" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('SortNo','PurchaseApplyUCFromSortNo');return false;">
                        源单序号<span id="PurchaseApplyUCFromSortNo" class="orderTip"></span></div>
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
                        <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                            <div id="PageCountPurchaseApplyUC">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="pageDataList1_PagerPurchaseApplyUC" class="jPagerBar">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="divpagePurchaseApplyUC">
                                <input name="TotalRecordPurchaseApplyUC" type="text" id="TotalRecordPurchaseApplyUC"
                                    style="display: none" />
                                <span id="TotalPagePurchaseApplyUC"></span>每页显示
                                <input name="PerPageCountPurchaseApplyUC" type="text" id="PerPageCountPurchaseApplyUC"
                                    style="width: 30px" />条 转到第
                                <input name="ToPagePurchaseApplyUC" type="text" id="ToPagePurchaseApplyUC" style="width: 30px" />页
                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                    width="36" height="28" align="absmiddle" onclick="popPurchaseApplyUC.ChangePageCountIndex($('#PerPageCountPurchaseApplyUC').val(),$('#ToPagePurchaseApplyUC').val());" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
</div>
</div>
<script type="text/javascript">
var popPurchaseApplyUC = new Object();
popPurchaseApplyUC.Type = null;
//分页等显示信息
popPurchaseApplyUC.pageCount = 10;
popPurchaseApplyUC.totalRecord = 0;
popPurchaseApplyUC.pagerStyle = "flickr";
popPurchaseApplyUC.currentPageIndex = 1;
popPurchaseApplyUC.action = "";
popPurchaseApplyUC.orderBy = "RequireDate";
popPurchaseApplyUC.orderByType ="desc" 

popPurchaseApplyUC.ShowList = function(Type)
{
    popPurchaseApplyUC.Type = Type;
    openRotoscopingDiv(true,"divPurchaseApplyUC2","frmPurchaseApplyUC")
    document.getElementById("divPurchaseApplyUC").style.display='block';
    
    popPurchaseApplyUC.TurnToPage(1)
}

popPurchaseApplyUC.CloseList = function()
{
    closeRotoscopingDiv(true,"divPurchaseApplyUC2")
    document.getElementById("divPurchaseApplyUC").style.display='none';
}

function fnGetPurApp()
{
    fnGetPurAppSlt();
    popPurchaseApplyUC.TurnToPage(1);
}

function fnGetPurAppSlt()
{
      var PurchaseArriveEFIndex="";
       var PurchaseArriveEFDesc="";
      if(document.getElementById("trPurchaseArriveNewAttr").style.display!="none")
      {
        PurchaseArriveEFIndex=document.getElementById("selPurchaseArriveEFIndex").value;
        PurchaseArriveEFDesc=document.getElementById("txtPurchaseArriveEFDesc").value;
      }
     var str = "";
    str += "&ProductNo="+escape($("#txtProdNoPurApp").val());
    str += "&ProductName="+escape($("#txtProdNamePurApp").val());
    str += "&StartDate="+escape($("#txtStartDatePurApp").val());
    str += "&EndDate="+escape($("#txtEndDatePurApp").val());
    str += "&PurchaseArriveEFIndex="+escape($("#PurchaseArriveEFIndex").val());
    str += "&PurchaseArriveEFDesc="+escape($("#PurchaseArriveEFDesc").val());
    $("#hidPurAppSltCnd").val(str);
    return str;
}

function fnFillPurApp()
{
    try {
        var Type = popPurchaseApplyUC.Type;
        var ck = document.getElementsByName("ChkBoxPurApp");
        for (var i = 0; i < ck.length; i++) {
            if (ck[i].checked) {//选中某行，将该行的值赋到界面上
                if (Type == "Order") {
                    var PurASortNo = $("#PurAppSortNo" + i).html();
                    var PurAFromBillNo = $("#PurAppFromBillNo" + i).html();
                    if (OrdertExistFrom(PurAFromBillNo, PurASortNo)) {
                        continue;
                    }
                    var rowID = AddSignRow("From");

                    document.getElementById("OrderProductID" + rowID).value = $("#PurAppProductID" + i).html();
                    document.getElementById("OrderProductNo" + rowID).value = $("#PurAppProductNo" + i).html();
                    document.getElementById("OrderProductName" + rowID).value = $("#PurAppProductName" + i).html();
                    document.getElementById("OrderSpecification" + rowID).value = $("#PurAppSpecification" + i).html();
                    document.getElementById("OrderUnitID" + rowID).value = $("#PurAppUnitID" + i).html();
                    document.getElementById("OrderUnitName" + rowID).value = $("#PurAppUnitName" + i).html();
                    document.getElementById("OrderUnitPrice" + rowID).value = $("#PurAppUnitPrice" + i).html();
                    var unitPrice = (parseFloat($("#PurAppUnitPrice" + i).html())).toFixed($("#HiddenPoint").val());
                    document.getElementById("OrderTaxPrice" + rowID).value = $("#PurAppStandardBuy" + i).html();
                    var taxPrice = (parseFloat($("#PurAppStandardBuy" + i).html())).toFixed($("#HiddenPoint").val());
                    document.getElementById("OrderProductCount" + rowID).value = $("#PurAppRequireCount" + i).html();
                    var ProductCount = (parseFloat($("#PurAppRequireCount" + i).html())).toFixed($("#HiddenPoint").val());
                    document.getElementById("OrderRequireDate" + rowID).value = $("#PurAppRequireDate" + i).html();
                    document.getElementById("OrderFromBillID" + rowID).value = $("#PurAppFromBillID" + i).html();
                    document.getElementById("OrderFromBillNo" + rowID).value = $("#PurAppFromBillNo" + i).html();
                    document.getElementById("OrderFromLineNo" + rowID).value = $("#PurAppSortNo" + i).html();
                    document.getElementById("DtlSColor" + rowID).value = $("#PurAppColorName" + i).html();

                    
                    if ($("#ddlTypeID_ddlCodeType").val() != "") {
                        $("#ddlTypeID_ddlCodeType").val($("#PurAppTypeID" + i).html());
                    }
                    if ($("#HiddenMoreUnit").val() == "False")
                    { }
                    else {
                        var UsedUnitID = $("#PurAppUsedUnitID" + i).html();
                        var UsedUnitCount = $("#PurAppUsedUnitCount" + i).html();
                        GetUnitGroupSelectEx($("#PurAppProductID" + i).html(), "InUnit", "SignItem_TD_UnitID_Select" + rowID, "ChangeUnit(this.id," + rowID + "," + unitPrice + "," + taxPrice + ")", "unitdiv" + rowID, '', "FillApplyContent(" + rowID + "," + unitPrice + "," + taxPrice + "," + ProductCount + "," + UsedUnitCount + ",'" + UsedUnitID + "','','Bill')"); //绑定单位组

                    }


                }
                else if (Type == "AskPrice") {

                    var PurAppSortNo = $("#PurAppSortNo" + i).html();
                    var PurAppFromBillNo = $("#PurAppFromBillNo" + i).html();
                    if (AskPriceExistFromApply(PurAppFromBillNo, PurAppSortNo)) {
                        continue;
                    }
                    var rowID = AddAskPriceSignRow("From");
                    var DisCount = 100;
                    
                    $("#ProductID" + rowID).val($("#PurAppProductID" + i).html());
                    $("#ProductNo" + rowID).val($("#PurAppProductNo" + i).html());
                    $("#ProductName" + rowID).val($("#PurAppProductName" + i).html());
                    $("#Specification" + rowID).val($("#PurAppSpecification" + i).html());
                    $("#ProductCount" + rowID).val($("#PurAppRequireCount" + i).html());
                    var producount = $("#PurAppRequireCount" + i).html();
                    $("#RequireDate" + rowID).val($("#PurAppRequireDate" + i).html());
                    $("#UnitID" + rowID).val($("#PurAppUnitID" + i).html());
                    $("#UnitName" + rowID).val($("#PurAppUnitName" + i).html());
                    $("#UnitPrice" + rowID).val($("#PurAppUnitPrice" + i).html());
                    var unitPrice = $("#PurAppUnitPrice" + i).html();
                    $("#TaxPrice" + rowID).val($("#PurAppStandardBuy" + i).html());
                    var taxPrice = $("#PurAppStandardBuy" + i).html();
                    $("#TaxPriceHide" + rowID).val($("#PurAppStandardBuy" + i).html());
                    $("#Discount" + rowID).val(100);
                    document.getElementById("DtlSColor" + rowID).value = $("#PurAppColorName" + i).html();
                    $("#TaxRate" + rowID).val();
                    $("#TaxRateHide" + rowID).val();
                    $("#FromBillID" + rowID).val($("#PurAppFromBillID" + i).html());
                    $("#FromBillNo" + rowID).val($("#PurAppFromBillNo" + i).html());
                    $("#FromLineNo" + rowID).val($("#PurAppSortNo" + i).html());

                    if ($("#ddlTypeID_ddlCodeType").val() == "") {
                        $("#ddlTypeID_ddlCodeType").val($("#PurAppTypeID" + i).html());
                    }

                    if ($("#HiddenMoreUnit").val() == "False") {
                        fnMergeDetail();
                    }
                    else {
                        var UsedUnitID = $("#PurAppUsedUnitID" + i).html();
                        var UsedUnitCount = $("#PurAppUsedUnitCount" + i).html();
                        GetUnitGroupSelectEx($("#PurAppProductID" + i).html(), "InUnit", "SignItem_TD_UnitID_Select" + rowID, "ChangeUnit(this.id," + rowID + "," + unitPrice + "," + taxPrice + ")", "unitdiv" + rowID, '', "FillApplyContent(" + rowID + "," + unitPrice + "," + taxPrice + "," + producount + "," + UsedUnitCount + ",'" + UsedUnitID + "','','Bill')"); //绑定单位组
                    }

                }
                else {
                    var AppSortNo = $("#PurAppSortNo" + i).html();
                    var AppFromBillNo = $("#PurAppFromBillNo" + i).html();
                    if (ExistFromApply(AppFromBillNo, AppSortNo)) {
                        continue;
                    }
                    var rowID = AddDtlSSignRow();
                    document.getElementById("DtlSColor" + rowID).value = $("#PurAppColorName" + i).html();
                    document.getElementById("DtlSProductID" + rowID).value = $("#PurAppProductID" + i).html();
                    document.getElementById("DtlSProductNo" + rowID).value = $("#PurAppProductNo" + i).html();
                    document.getElementById("DtlSProductName" + rowID).value = $("#PurAppProductName" + i).html();
                    document.getElementById("DtlSSpecification" + rowID).value = $("#PurAppSpecification" + i).html();
                    document.getElementById("DtlSUnitID" + rowID).value = $("#PurAppUnitID" + i).html();
                    document.getElementById("DtlSUnitName" + rowID).value = $("#PurAppUnitName" + i).html();
                    var unitPrice = (parseFloat($("#PurAppStandardBuy" + i).html())).toFixed($("#HiddenPoint").val());
                    document.getElementById("DtlSUnitPrice" + rowID).value = unitPrice; //含税进价
                    var producount4 = (parseFloat($("#PurAppRequireCount" + i).html())).toFixed($("#HiddenPoint").val());
                    document.getElementById("DtlSRequireCount" + rowID).value = $("#PurAppRequireCount" + i).html();
                    document.getElementById("DtlSPlanCount" + rowID).value = $("#PurAppRequireCount" + i).html();
                    document.getElementById("DtlSRequireDate" + rowID).value = $("#PurAppRequireDate" + i).html();
                    document.getElementById("DtlSPlanTakeDate" + rowID).value = $("#PurAppRequireDate" + i).html();
                    document.getElementById("DtlSApplyReason" + rowID).value = $("#PurAppApplyReasonID" + i).html();
                    document.getElementById("DtlSFromBillID" + rowID).value = $("#PurAppFromBillID" + i).html();
                    document.getElementById("DtlSFromBillNo" + rowID).value = $("#PurAppFromBillNo" + i).html();
                    document.getElementById("DtlSFromLineNo" + rowID).value = $("#PurAppSortNo" + i).html();
                    document.getElementById("DtlSTotalPrice" + rowID).value = parseFloat($("#PurAppUnitPrice" + i).html()) * parseFloat($("#PurAppRequireCount" + i).html()); //总金额

                    //合计信息
                    document.getElementById("txtPlanCnt").value = parseInt(document.getElementById("txtPlanCnt").value) + parseInt(document.getElementById("DtlSPlanCount" + rowID).value);
                    document.getElementById("txtPlanMoney").value = parseFloat(document.getElementById("txtPlanMoney").value) + parseInt(document.getElementById("DtlSPlanCount" + rowID).value) * parseFloat(document.getElementById("DtlSUnitPrice" + rowID).value);

                    if ($("#HiddenMoreUnit").val() == "False") {
                        fnMergeDetail();
                    }
                    else {

                        var UsedUnitID = $("#PurAppUsedUnitID" + i).html();
                        var UsedUnitCount = $("#PurAppUsedUnitCount" + i).html();
                        GetUnitGroupSelectEx($("#PurAppProductID" + i).html(), "InUnit", "SignItem_TD_UnitID_Select" + rowID, "ChangeUnit(this.id," + rowID + "," + unitPrice + ")", "unitdiv" + rowID, '', "FillApplyContent(" + rowID + "," + unitPrice + "," + producount4 + "," + UsedUnitCount + ",'" + UsedUnitID + "')"); //绑定单位组
                    }

                }
            }
        }
        popPurchaseApplyUC.CloseList();

    }
    catch (Error)
    { }
}



popPurchaseApplyUC.TurnToPage = function(pageIndex)
{
    popPurchaseApplyUC.currentPageIndex = pageIndex;
    
    var strParams = $("#hidPurAppSltCnd").val();
    strParams += "&pageIndex="+popPurchaseApplyUC.currentPageIndex;
    strParams += "&pageCount="+popPurchaseApplyUC.pageCount;
    strParams += "&orderby="+popPurchaseApplyUC.orderBy
    strParams += "&OrderByType="+popPurchaseApplyUC.orderByType;
 if($("#HiddenMoreUnit").val()=="True")
 {    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/PurchaseApplyUC.ashx',//目标地址
           cache:false,
           data: strParams,//数据
           beforeSend:function(){$("#pageDataList1_PagerPurchaseApplyUC").hide();},//发送数据之前         
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListPurchaseApplyUC tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ProductID != null && item.ProductID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPurApp\" id=\"ChkBoxPurApp"+i+"\" value=\""+item.ID+"\"  />"+"</td>"+
                        "<td height='22' style='display:none' id='PurAppProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                        "<td height='22' id='PurAppProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' id='PurAppProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' id='PurAppSpecification" + i + "' align='center'>" + item.Specification + "</td>" +
                        "<td height='22' id='PurAppColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                        "<td height='22' style='display:none' id='PurAppUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                        "<td height='22' id='PurAppUnitName"+i+"' align='center' >" + item.UnitName + "</td>"+
                          "<td height='22' id='PurAppUsedUnitName"+i+"' align='center'>" + item.UsedUnitName + "</td>"+
                        "<td height='22' id='PurAppUnitPrice"+i+"' align='center'>" + (parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurAppStandardBuy"+i+"' align='center'>" +   (parseFloat(item.StandardBuy)).toFixed($("#HiddenPoint").val())+ "</td>"+
                            "<td height='22' id='PurAppUsedUnitCount"+i+"' align='center'>" +  (parseFloat(item.UsedUnitCount)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurAppRequireCount"+i+"' align='center'  >" +  (parseFloat(item.RequireCount)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurAppPlanedCount"+i+"' align='center'>" + (parseFloat(item.PlanedCount)).toFixed($("#HiddenPoint").val()) + "</td>"+ 
                        "<td height='22' id='PurAppRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                        "<td height='22' style='display:none' id='PurAppUsedUnitID"+i+"' align='center'>" + item.UsedUnitID + "</td>"+
                          "<td height='22' style='display:none' id='PurAppTypeID"+i+"' align='center'>" + item.TypeID + "</td>"+
                        "<td height='22' style='display:none' id='PurAppApplyReasonID"+i+"' align='center'>" + item.ApplyReasonID + "</td>"+
                        "<td height='22' id='PurAppApplyReasonName"+i+"' align='center'>" + item.ApplyReasonName + "</td>"+
                        "<td height='22' style='display:none' id='PurAppFromBillID"+i+"' align='center'>" + item.FromBillID + "</td>"+
                        "<td height='22' id='PurAppFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                        "<td height='22' id='PurAppSortNo"+i+"' align='center'>"+item.SortNo+"</td>").appendTo($("#pageDataListPurchaseApplyUC tbody"));
                   });
                    //页码
    
                   ShowPageBar("pageDataList1_PagerPurchaseApplyUC",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {
                        style:popPurchaseApplyUC.pagerStyle,mark:"pageDataList1Mark",
                        totalCount:msg.totalCount,
                        showPageNumber:3,
                        pageCount:popPurchaseApplyUC.pageCount,
                        currentPageIndex:popPurchaseApplyUC.currentPageIndex,
                        noRecordTip:"没有符合条件的记录",
                        preWord:"上页",
                        nextWord:"下页",
                        First:"首页",
                        End:"末页",
                        onclick:"popPurchaseApplyUC.TurnToPage({pageindex});return false;"}//[attr]
                    );
                 popPurchaseApplyUC.totalRecord = msg.totalCount;
                 $("#PerPageCountPurchaseApplyUC").val(popPurchaseApplyUC.pageCount);
                 ShowTotalPage(msg.totalCount,popPurchaseApplyUC.pageCount,pageIndex,$("#PageCountPurchaseApplyUC"));
                 $("#ToPagePurchaseApplyUC").val(pageIndex);
                  },
           error: function() {},
           complete:function(){hidePopup();$("#pageDataList1_PagerPurchaseApplyUC").show();popPurchaseApplyUC.pageDataList1("pageDataListPurchaseApplyUC","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
 }
 else
 {
 
 
        document .getElementById ("sspPurchaseApplyUsedUnitCount").style.display ="none"; 
    document .getElementById ("sspPurchaseApplyUsedUnitName").style.display ="none";  
       document .getElementById ("sspPurchaseApplyCount").innerHTML="需求数量";
       document.getElementById("sspPurchaseApplyUnit").innerHTML = "单位"; 
         
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/PurchaseApplyUC.ashx',//目标地址
           cache:false,
           data: strParams,//数据
           beforeSend:function(){$("#pageDataList1_PagerPurchaseApplyUC").hide();},//发送数据之前         
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListPurchaseApplyUC tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ProductID != null && item.ProductID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPurApp\" id=\"ChkBoxPurApp"+i+"\" value=\""+item.ID+"\"  />"+"</td>"+
                        "<td height='22' style='display:none' id='PurAppProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                        "<td height='22' id='PurAppProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' id='PurAppProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' id='PurAppSpecification" + i + "' align='center'>" + item.Specification + "</td>" +
                         "<td height='22' id='PurAppColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                        "<td height='22' style='display:none' id='PurAppUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                        "<td height='22' id='PurAppUnitName"+i+"' align='center'>" + item.UnitName + "</td>"+
                        "<td height='22' id='PurAppUnitPrice"+i+"' align='center'>" +  (parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurAppStandardBuy"+i+"' align='center'>" +   (parseFloat(item.StandardBuy)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurAppRequireCount"+i+"' align='center'>" +  (parseFloat(item.RequireCount)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurAppPlanedCount"+i+"' align='center'>" +   (parseFloat(item.PlanedCount)).toFixed($("#HiddenPoint").val())+ "</td>"+ 
                        "<td height='22' id='PurAppRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                        "<td height='22' style='display:none' id='PurAppTypeID"+i+"' align='center'>" + item.TypeID + "</td>"+
                        "<td height='22' style='display:none' id='PurAppApplyReasonID"+i+"' align='center'>" + item.ApplyReasonID + "</td>"+
                        "<td height='22' id='PurAppApplyReasonName"+i+"' align='center'>" + item.ApplyReasonName + "</td>"+
                        "<td height='22' style='display:none' id='PurAppFromBillID"+i+"' align='center'>" + item.FromBillID + "</td>"+
                        "<td height='22' id='PurAppFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                        "<td height='22' id='PurAppSortNo"+i+"' align='center'>"+item.SortNo+"</td>").appendTo($("#pageDataListPurchaseApplyUC tbody"));
                   });
                    //页码
    
                   ShowPageBar("pageDataList1_PagerPurchaseApplyUC",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {
                        style:popPurchaseApplyUC.pagerStyle,mark:"pageDataList1Mark",
                        totalCount:msg.totalCount,
                        showPageNumber:3,
                        pageCount:popPurchaseApplyUC.pageCount,
                        currentPageIndex:popPurchaseApplyUC.currentPageIndex,
                        noRecordTip:"没有符合条件的记录",
                        preWord:"上页",
                        nextWord:"下页",
                        First:"首页",
                        End:"末页",
                        onclick:"popPurchaseApplyUC.TurnToPage({pageindex});return false;"}//[attr]
                    );
                 popPurchaseApplyUC.totalRecord = msg.totalCount;
                 $("#PerPageCountPurchaseApplyUC").val(popPurchaseApplyUC.pageCount);
                 ShowTotalPage(msg.totalCount,popPurchaseApplyUC.pageCount,pageIndex,$("#PageCountPurchaseApplyUC"));
                 $("#ToPagePurchaseApplyUC").val(pageIndex);
                  },
           error: function() {},
           complete:function(){hidePopup();$("#pageDataList1_PagerPurchaseApplyUC").show();popPurchaseApplyUC.pageDataList1("pageDataListPurchaseApplyUC","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
           }
           
           
           
}

popPurchaseApplyUC.pageDataList1= function(o,a,b,c,d)
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


popPurchaseApplyUC.ChangePageCountIndex= function(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((popPurchaseApplyUC.totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        popPurchaseApplyUC.pageCount=parseInt(newPageCount);
        popPurchaseApplyUC.TurnToPage(parseInt(newPageIndex));
    }
}
popPurchaseApplyUC.orderByP = function(orderColum,orderTip)
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
    orderPurchaseContractUCBy = orderColum+"_"+ordering;
    popPurchaseApplyUC.orderBy=orderColum;
    popPurchaseApplyUC.orderByType=ordering;
    popPurchaseApplyUC.TurnToPage(1);
}
</script>

