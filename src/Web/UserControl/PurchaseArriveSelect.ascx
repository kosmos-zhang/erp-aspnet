<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseArriveSelect.ascx.cs"
    Inherits="UserControl_PurchaseArriveSelect" %>
<style type="text/css">
    .tdinput
    {
        border-width: 0pt;
        background-color: #ffffff;
        height: 21px;
        margin-left: 2px;
    }
</style>
<div id="divPurchaseArrivehhh" style="display: none">
    <!--提示信息弹出详情start-->
    <div id="divPurchaseArrive3">
        <iframe id="frmPurchaseArrive"></iframe>
    </div>
    <div id="divPurchaseArrive" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 90%; z-index: 20; position: absolute; display: block; top: 38%;
        left: 35%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeMaterialdiv()" style="text-align: left; cursor: pointer">
                        <img src="../../../images/Button/Bottom_btn_close.jpg" /></a>
                </td>
            </tr>
        </table>
        <table width="99%" height="57" border="0" cellpadding="0" cellspacing="1" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClickArrive'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('PurchaseArrivesearchtable','searchClickArrive')" /></div>
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
                    <table width="100%" border="0" align="left" cellpadding="0" id="PurchaseArrivesearchtable"
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
                                                </select><input type="text" id="txtPurchaseArriveEFDesc" style="width: 30%" />
                                            </div>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" colspan="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                onclick='Fun_Search_PurchaseArrive()' id="btn_search" />
                                            <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                                                onclick="GetValuePPurReject();" id="imgsure" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="divDetail" style="width: 100%; overflow-x: auto; overflow-y: auto;" border="0"
            align="center" cellpadding="0" cellspacing="1">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListPurchaseArrive"
                bgcolor="#999999">
                <tbody>
                    <tr>
                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            选择<input type="checkbox" id="btnPPurRejectAll" name="btnPPurRejectAll" onclick="OptionCheckPPurRejectAll()" />
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('ProductNo','pc1');return false;">
                                物品编号<span id="pc1" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('ProductName','pc2');return false;">
                                物品名称<span id="pc2" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('standard','pc3');return false;">
                                规格<span id="pc3" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('ColorName','pColorName');return false;">
                                颜色<span id="pColorName" class="orderTip"></span></div>
                        </th>
                        <th id="thUsedUnitName_divPurchaseArrive" align="center" background="../../../images/Main/Table_bg.jpg"
                            bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('UsedUnitName','pc21');return false;">
                                基本单位<span id="pc21" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('UnitName','pc20');return false;">
                                单位<span id="pc20" class="orderTip"></span></div>
                        </th>
                        <th id="thUsedUnitCount_divPurchaseArrive" align="center" background="../../../images/Main/Table_bg.jpg"
                            bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('UsedUnitCount','pc22');return false;">
                                基本数量<span id="pc22" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('ProductCount','pc4');return false;">
                                到货数量<span id="pc4" class="orderTip"></span></div>
                        </th>
                        <th id="thUsedPrice_divPurchaseArrive" align="center" background="../../../images/Main/Table_bg.jpg"
                            bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('UsedPrice','pc23');return false;">
                                基本单价<span id="pc23" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('UnitPrice','pc5');return false;">
                                单价<span id="pc5" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('TaxPrice','pc6');return false;">
                                含税价<span id="pc6" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('Discount','pc7');return false;">
                                折扣(%)<span id="pc7" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('TaxRate','pc8');return false;">
                                税率(%)<span id="pc8" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('TotalPrice','pc9');return false;">
                                金额<span id="pc9" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('TotalFee','pc10');return false;">
                                含税金额<span id="pc10" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('TotalTax','pc11');return false;">
                                税额<span id="pc11" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('Remark','pc12');return false;">
                                备注<span id="pc12" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('FromBillNo','pc13');return false;">
                                源单编号<span id="pc13" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('FromLineNo','pc14');return false;">
                                源单序号<span id="pc14" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('BackCount','pc15');return false;">
                                退货数量<span id="pc15" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('ProviderName','pc16');return false;">
                                供应商<span id="pc16" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('CurrencyTypeName','pc17');return false;">
                                币种<span id="pc17" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('Rate','pc18');return false;">
                                汇率<span id="pc18" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="popOrder.orderByP('InCount','pc19');return false;">
                                入库数量<span id="pc19" class="orderTip"></span></div>
                        </th>
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
                                <div id="pagecounttest">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerPurchaseArrive" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpagetest">
                                    <input name="TextPurchaseOrder" type="text" id="TextPurchaseOrder" style="display: none" />
                                    <span id="pageDataList1_Total"></span>每页显示
                                    <input name="ShowPageCountPurchaseOrder" type="text" id="ShowPageCountPurchaseOrder"
                                        style="width: 22px" />条 转到第
                                    <input name="ToPagePurchaseOrder" type="text" id="ToPagePurchaseOrder" style="width: 35px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexPurchaseArrive($('#ShowPageCountPurchaseOrder').val(),$('#ToPagePurchaseOrder').val());" />
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
    var popOrder = new Object();
    popOrder.InputObj = null;
    popOrder.ProviderID = null;
    popOrder.CurrencyTypeID = null;
    popOrder.Rate = null;
    popOrder.isMoreUnit = null;

    var pageCountPurchaseOrder = 10; //每页计数
    var totalRecordPurchaseArrive = 0;
    var pagerStylePurchaseOrder = "flickr"; //jPagerBar样式

    var currentPageIndexPurchaseOrder = 1;
    var actionPurchaseOrder = ""; //操作
    var orderbyPurchaseArrive = ""; //排序字段
    var pageCount = '';

    popOrder.orderByP = function(orderColum, orderTip) {
        var ordering = "a";
        var allOrderTipDOM = $(".orderTip");
        if ($("#" + orderTip).html() == "↓") {
            allOrderTipDOM.empty();
            $("#" + orderTip).html("↑");
        }
        else {
            ordering = "d";
            allOrderTipDOM.empty();
            $("#" + orderTip).html("↓");
        }
        orderbyPurchaseArrive = orderColum + "_" + ordering;
        TurnToPagePurchaseOrder(1);
    }

    popOrder.ShowList = function(objInput, ProviderID, CurrencyTypeID, Rate, isMoreUnit) {

        popOrder.InputObj = objInput;
        popOrder.ProviderID = ProviderID;
        popOrder.CurrencyTypeID = CurrencyTypeID;
        popOrder.Rate = Rate;
        popOrder.isMoreUnit = isMoreUnit;
        document.getElementById('divPurchaseArrivehhh').style.display = 'block';
        openRotoscopingDiv(true, "divPurchaseArrive3", "frmPurchaseArrive")

        PASSetHide();
        TurnToPagePurchaseOrder(1);
    }


    // 隐藏列
    function PASSetHide() {
        if (!popOrder.isMoreUnit) {// 隐藏非多计量单位
            $("#thUsedUnitName_divPurchaseArrive").hide();
            $("#thUsedUnitCount_divPurchaseArrive").hide();
            $("#thUsedPrice_divPurchaseArrive").hide();
        }
    }

    function clearPurchaseArriveEFDesc() {
        if ($("#selPurchaseArriveEFIndex").val() == "-1") {
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
            beforeSend: function() { AddPop(); }, //发送数据之前

            success: function(msg) {



                //数据获取完毕，填充页面据显示
                //存在扩展属性显示页面扩展属性表格
                if (parseInt(msg.totalCount) > 0) {
                    $("<option value=\" \">--请选择--</option>").appendTo($("#selPurchaseArriveEFIndex"));
                    $("#divPurchaseArriveProductDiyAttr").show();
                    $("#PurchaseArrivespanOther").show();
                    $("#trPurchaseArriveNewAttr").show();
                    $.each(msg.data, function(i, item) {
                        $("<option value=\"" + item.EFIndex + "\">" + item.EFDesc + "</option>").appendTo($("#selPurchaseArriveEFIndex"));
                    });
                    document.getElementById("selPurchaseArriveEFIndex").selectedIndex = 0;
                }
            },
            error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
            complete: function() { hidePopup(); } //接收数据完毕
        });
    }


    function OptionCheckPPurRejectAll() {
        if (document.getElementById("btnPPurRejectAll").checked) {
            var ck = document.getElementsByName("ChkBoxPPurReject");
            for (var i = 0; i < ck.length; i++) {
                ck[i].checked = true;
            }
        }
        else {
            var ck = document.getElementsByName("ChkBoxPPurReject");
            for (var i = 0; i < ck.length; i++) {
                ck[i].checked = false;
            }
        }
    }




    function IfshowPurchaseArrive(count) {
        if (count == "0") {
            document.getElementById("divpagetest").style.display = "none";
            document.getElementById("pagecounttest").style.display = "none";
        }
        else {
            document.getElementById("divpagetest").style.display = "block";
            document.getElementById("pagecounttest").style.display = "block";
        }
    }

    function closeMaterialdiv() {

        document.getElementById("divPurchaseArrivehhh").style.display = "none";
        closeRotoscopingDiv(true, "divPurchaseArrive3")
    }

    function TurnToPagePurchaseOrder(pageIndexPurchaseOrder) {
        currentPageIndexPurchaseOrder = pageIndexPurchaseOrder;


        var ProductNo = $("#txt_ProductNo").val().Trim();
        var ProductName = $("#txt_ProductName").val().Trim();
        var FromBillNo = $("#txt_FromBillNo").val().Trim();
        var PurchaseArriveEFIndex = "";
        var PurchaseArriveEFDesc = "";
        if (document.getElementById("trPurchaseArriveNewAttr").style.display != "none") {
            PurchaseArriveEFIndex = document.getElementById("selPurchaseArriveEFIndex").value;
            PurchaseArriveEFDesc = document.getElementById("txtPurchaseArriveEFDesc").value;
        }
        $.ajax({
            type: "POST",
            dataType: "json",
            url: '../../../Handler/Office/PurchaseManager/PurchaseArriveSelect.ashx',
            cache: false,
            data: "pageIndexPurchaseOrder=" + pageIndexPurchaseOrder
                + "&pageCountPurchaseOrder=" + pageCountPurchaseOrder
                + "&Provider00ID=" + popOrder.ProviderID
                + "&CurrencyTypeID=" + popOrder.CurrencyTypeID
                + "&Rate=" + popOrder.Rate
                + "&ProductNo=" + escape(ProductNo)
                + "&PurchaseArriveEFIndex=" + escape(PurchaseArriveEFIndex)
                + "&PurchaseArriveEFDesc=" + escape(PurchaseArriveEFDesc)
                + "&ProductName=" + escape(ProductName)
                + "&FromBillNo=" + escape(FromBillNo)
                + "&actionPurchaseOrder=" + actionPurchaseOrder
                + "&orderbyPurchaseArrive=" + orderbyPurchaseArrive + "",
            beforeSend: function() { AddPop(); $("#pageDataList1_PagerPurchaseArrive").hide(); },

            success: function(msg) {
                $("#pageDataListPurchaseArrive tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"checkbox\" onclick=\"PurRejectClickChkBox(" + i + ")\" name=\"ChkBoxPPurReject\" id=\"ChkBoxPPurReject" + i + "\" value=\"" + item.ID + "\"/>" + "</td>" +
                    "<td height='22' style='display:none' id='PPurRejectProductID" + i + "' align='center'>" + item.ProductID + "</td>" +
                    "<td height='22' align='center' id='PPurRejectProductNo" + i + "' align='center'>" + item.ProductNo + "</td>" +
                    "<td height='22' align='center' id='PPurRejectProductName" + i + "' align='center'>" + item.ProductName + "</td>" +
                    "<td height='22' align='center' id='PPurRejectstandard" + i + "' align='center'>" + item.standard + "</td>" +
                    "<td height='22' align='center' id='PPurRejectColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                    (popOrder.isMoreUnit ? ("<td height='22' style='display:none' id='PPurRejectUsedUnitID" + i + "' align='center'>" + item.UnitID + "</td>" +
                    "<td height='22' align='center' id='PPurRejectUsedUnitName" + i + "' align='center'>" + item.UnitName + "</td>" +
                    "<td height='22' style='display:none' id='PPurRejectUnitID" + i + "' align='center'>" + item.UsedUnitID + "</td>" +
                    "<td height='22' align='center' id='PPurRejectUnitName" + i + "' align='center'>" + item.UsedUnitName + "</td>" +
                    "<td height='22' align='center' id='PPurRejectUsedUnitCount" + i + "' align='center'>" + FormatAfterDotNumber(item.ProductCount, selPoint) + "</td>" +
                    "<td height='22' align='center' id='PPurRejectProductCount" + i + "' align='center'>" + FormatAfterDotNumber(item.UsedUnitCount, selPoint) + "</td>"
                    ) : (
                    "<td height='22' style='display:none' id='PPurRejectUnitID" + i + "' align='center'>" + item.UnitID + "</td>" +
                    "<td height='22' align='center' id='PPurRejectUnitName" + i + "' align='center'>" + item.UnitName + "</td>" +
                    "<td height='22' align='center' id='PPurRejectProductCount" + i + "' align='center'>" + FormatAfterDotNumber(item.ProductCount, selPoint) + "</td>")) +
                    (popOrder.isMoreUnit ? ("<td height='22' align='center' id='PPurRejectUsedPrice" + i + "' align='center'>" + FormatAfterDotNumber(item.UnitPrice, selPoint) + "</td>" +
                    "<td height='22' align='center' id='PPurRejectUnitPrice" + i + "' align='center'>" + FormatAfterDotNumber(item.UsedPrice, selPoint) + "</td>") :
                    "<td height='22' align='center' id='PPurRejectUnitPrice" + i + "' align='center'>" + FormatAfterDotNumber(item.UnitPrice, selPoint) + "</td>") +
                    "<td height='22' align='center' id='PPurRejectTaxPrice" + i + "' align='center'>" + FormatAfterDotNumber(item.TaxPrice, selPoint) + "</td>" +
                    "<td height='22' align='center' id='PPurRejectDiscount" + i + "' align='center'>" + FormatAfterDotNumber(item.Discount, selPoint) + "</td>" +
                    "<td height='22' align='center' id='PPurRejectTaxRate" + i + "' align='center'>" + FormatAfterDotNumber(item.TaxRate, selPoint) + "</td>" +
                    "<td height='22' align='center' id='PPurRejectTotalPrice" + i + "' align='center'>" + FormatAfterDotNumber(item.TotalPrice, selPoint) + "</td>" +
                    "<td height='22' align='center' id='PPurRejectTotalFee" + i + "' align='center'>" + FormatAfterDotNumber(item.TotalFee, selPoint) + "</td>" +
                    "<td height='22' align='center' id='PPurRejectTotalTax" + i + "' align='center'>" + FormatAfterDotNumber(item.TotalTax, selPoint) + "</td>" +
                    "<td height='22' align='center' id='PPurRejectRemark" + i + "' align='center'>" + item.Remark + "</td>" +
                    "<td height='22' style='display:none' align='center' id='PPurRejectFromBillID" + i + "' align='center'>" + item.FromBillID + "</td>" +
                    "<td height='22' align='center' id='PPurRejectFromBillNo" + i + "' align='center'>" + item.FromBillNo + "</td>" +
                    "<td height='22' align='center' id='PPurRejectFromLineNo" + i + "' align='center'>" + item.FromLineNo + "</td>" +
                    "<td height='22' align='center' id='PPurRejectBackCount" + i + "' align='center'>" + item.BackCount + "</td>" +
                    "<td height='22' style='display:none' align='center' id='PPurRejectProviderID" + i + "' align='center'>" + item.ProviderID + "</td>" +
                    "<td height='22' align='center' id='PPurRejectProviderName" + i + "' align='center'>" + item.ProviderName + "</td>" +
                    "<td height='22' style='display:none' align='center' id='PPurRejectCurrencyType" + i + "' align='center'>" + item.CurrencyType + "</td>" +
                    "<td height='22' align='center' id='PPurRejectCurrencyTypeName" + i + "' align='center'>" + item.CurrencyTypeName + "</td>" +
                    "<td height='22' align='center' id='PPurRejectRate" + i + "' align='center'>" + FormatAfterDotNumber(item.Rate, 4) + "</td>" +
                       "<td height='22' align='center' id='PPurRejectInCount" + i + "' align='center'>" + item.InCount + "</td>").appendTo($("#pageDataListPurchaseArrive tbody"));
                });
                ShowPageBar("pageDataList1_PagerPurchaseArrive",
               "<%= Request.Url.AbsolutePath %>",
                { style: pagerStylePurchaseOrder, mark: "pageDataList1MarkPurchaseOrder",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCountPurchaseOrder, currentPageIndex: pageIndexPurchaseOrder, noRecordTip: "没有符合条件的记录", preWord: "上页", nextWord: "下页", First: "首页", End: "末页",
                    onclick: "TurnToPagePurchaseOrder({pageindex});return false;"
                }
                ); document.getElementById("btnPPurRejectAll").checked = false;

                totalRecordPurchaseArrive = msg.totalCount;
                $("#ShowPageCountPurchaseOrder").val(pageCountPurchaseOrder);
                ShowTotalPage(msg.totalCount, pageCountPurchaseOrder, pageIndexPurchaseOrder, $("#pagecounttest"));
                $("#ToPagePurchaseOrder").val(pageIndexPurchaseOrder);
            },
            error: function() { },
            complete: function() { hidePopup(); $("#pageDataList1_PagerPurchaseArrive").show(); IfshowPurchaseArrive(document.getElementById("TextPurchaseOrder").value); pageDataList1("pageDataListPurchaseArrive", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); }
        });
    }

    function pageDataList1(o, a, b, c, d) {
        var t = document.getElementById(o).getElementsByTagName("tr");
        for (var i = 0; i < t.length; i++) {
            t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
            t[i].onmouseover = function() {
                if (this.x != "1") this.style.backgroundColor = c;
            }
            t[i].onmouseout = function() {
                if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
            }
        }
    }

    function PurRejectClickChkBox(rowID) {
        var ck = document.getElementsByName("ChkBoxPPurReject");
        for (var i = 0; i < ck.length; i++) {
            if (ck[i].checked && i != rowID) {//选中某行
                if ($("#PPurRejectProviderID" + i).html() != $("#PPurRejectProviderID" + rowID).html()) {
                    //如果选择的不是一个合同则不让选
                    document.getElementById("ChkBoxPPurReject" + rowID).checked = false;
                    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "选择的明细只能来自一个供应商！");
                    return;
                }
                if ($("#PPurRejectCurrencyType" + i).html() != $("#PPurRejectCurrencyType" + rowID).html() || $("#PPurRejectRate" + i).html() != $("#PPurRejectRate" + rowID).html()) {
                    //如果选择的不是一个合同则不让选
                    document.getElementById("ChkBoxPPurArrive" + rowID).checked = false;
                    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "选择的到货单明细的币种和汇率必须相同！");
                    return;
                }
            }
        }
    }

    function Fun_Search_PurchaseArrive(aa) {
        search = "1";
        TurnToPagePurchaseOrder(1);
    }
    function Ifshow(count) {
        if (count == "0") {
            document.all["divPurchaseArrive"].style.display = "none";
            document.all["pagecountPurchaseOrder"].style.display = "none";
        }
        else {
            document.all["divPurchaseArrive"].style.display = "block";
            document.all["pagecountPurchaseOrder"].style.display = "block";
        }
    }

    function SelectMaterial(retval) {
        alert(retval);
    }

    //改变每页记录数及跳至页数
    function ChangePageCountIndexPurchaseArrive(newPageCountPurchaseOrder, newPageIndexPurchaseOrder) {
        if (newPageCountPurchaseOrder <= 0 || newPageIndexPurchaseOrder <= 0 || newPageIndexPurchaseOrder > ((totalRecordPurchaseArrive - 1) / newPageCountPurchaseOrder) + 1) {
            return false;
        }
        else {
            this.pageCountPurchaseOrder = parseInt(newPageCountPurchaseOrder);
            TurnToPagePurchaseOrder(parseInt(newPageIndexPurchaseOrder));
        }
    }

</script>

