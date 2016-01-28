<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductSubSellOrder.ascx.cs"
    Inherits="UserControl_ProductSubSellOrder" %>
<%@ Register Src="SubProductDiyAttr.ascx" TagName="SubProductDiyAttr" TagPrefix="uc1" %>
<div id="div1ProductInfoSubUC" style="display: none">
    <div id="div1ProductInfoSubUC2">
        <iframe id="frmProductInfoSubUC"></iframe>
    </div>
    <div id="div1ProductInfoSubUC3" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 93%; z-index: 300; position: absolute; margin-left: 5px;">
        <table width="99%">
            <tr>
                <td>
                    <img alt="关闭" id="btn_Close1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;'
                        onclick='closeProductInfoSubUCdiv();' />
                    <img alt="清除" id="ClearInputProduct" src="../../../images/Button/Bottom_btn_del.jpg"
                        style='cursor: hand; display: none' onclick='ClearProductInfo();' />
                    <input id="hf_typeid" type="hidden" />
                </td>
            </tr>
        </table>
        <table width="99%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClick12'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable12','searchClick12')" /></div>
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
                    <table width="100%" border="0" align="center" cellpadding="0" id="searchtable12"
                        cellspacing="0" bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                    class="table">
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            物品名称
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <input id="txtProductName" type="text" class="tdinput tboxsize" maxlength="25" specialworkcheck="物品名称" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            物品编号
                                        </td>
                                        <td height="20" class="tdColInput" width="24%">
                                            <input id="txtProdNo" type="text" class="tdinput tboxsize" maxlength="25" specialworkcheck="物品编号" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            规格
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <input id="txtSpecification" type="text" class="tdinput tboxsize" maxlength="25" />
                                        </td>
                                    </tr>
                                    <tr id="trSubNewAttr" style="display: none">
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <span id="SubspanOther" style="display: none">其他条件</span>
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <uc1:SubProductDiyAttr ID="SubProductDiyAttr1" runat="server" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                        </td>
                                        <td height="20" class="tdColInput" width="24%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" align="center" bgcolor="#FFFFFF">
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                onclick='Fun_Search_Productshit()' id="btn_search" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListProductInfoSubUC"
                        bgcolor="#999999">
                        <tbody>
                            <tr>
                                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    选择
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderByProductSub('ProductNo','SpanSub1');return false;">
                                        物品编号<span id="SpanSub1" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderByProductSub('ProductName','SpanSub2');return false;">
                                        物品名称<span id="SpanSub2" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderByProductSub('Specification','SpanSub3');return false;">
                                        规格<span id="SpanSub3" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderByProductSub('UnitName','SpanSub4');return false;">
                                        单位<span id="SpanSub4" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderByProductSub('SubPriceTax','SpanSub5');return false;">
                                        含税售价<span id="SpanSub5" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderByProductSub('SubPrice','SpanSub6');return false;">
                                        去税售价<span id="SpanSub6" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderByProductSub('SubTax','SpanSub7');return false;">
                                        税率<span id="SpanSub7" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderByProductSub('Discount','SpanSub8');return false;">
                                        折扣<span id="SpanSub8" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderByProductSub('ProductCount','SpanSub9');return false;">
                                        现有库存量<span id="SpanSub9" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderByProductSub('BatchNo','SpanSub10');return false;">
                                        批次<span id="SpanSub10" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick">
                                        商品信息<span id="Span1" class="orderTip"></span></div>
                                </th>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
            class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                <div id="PageCountProductInfoSubUC">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerProductInfoSubUC" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageProductInfoSubUC">
                                    <input name="TotalRecordProductInfoSubUC" type="text" id="TotalRecordProductInfoSubUC"
                                        style="display: none" />
                                    <span id="TotalPageProductInfoSubUC"></span>每页显示
                                    <input name="PerPageCountProductInfoSubUC" type="text" id="PerPageCountProductInfoSubUC"
                                        style="width: 20px" />条 转到第
                                    <input name="ToPageProductInfoSubUC" type="text" id="ToPageProductInfoSubUC" style="width: 22px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexProductSub($('#PerPageCountProductInfoSubUC').val(),$('#ToPageProductInfoSubUC').val());" />
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
    var popProductInfoSubUC = new Object();
    //界面上的ID
    popProductInfoSubUC.objProductID = null;
    popProductInfoSubUC.objProductNo = null;
    popProductInfoSubUC.objProductName = null;
    popProductInfoSubUC.objSpecification = null;
    popProductInfoSubUC.objUnitID = null;
    popProductInfoSubUC.objUnitName = null;
    popProductInfoSubUC.objSubPriceTax = null;
    popProductInfoSubUC.objSubPrice = null;
    popProductInfoSubUC.objSubTax = null;
    popProductInfoSubUC.objDiscount = null;
    popProductInfoSubUC.objRowID = null;

    //分页等显示信息
    popProductInfoSubUC.pageCount = 10;
    popProductInfoSubUC.totalRecord = 0;
    popProductInfoSubUC.pagerStyle = "flickr";
    popProductInfoSubUC.currentPageIndex = 1;
    popProductInfoSubUC.action = "Select";
    popProductInfoSubUC.orderBy = "ProductID_a";

    popProductInfoSubUC.ShowList = function(
    objInputRowID, objInputProductID, objInputProductNo, objInputProductName, objInputSpecification, objInputUnitID, objInputUnitName
, objInputSubPriceTax, objInputSubPrice, objInputSubTax, objInputDisCount) {
        popProductInfoSubUC.objRowID = objInputRowID;
        popProductInfoSubUC.objProductID = objInputProductID;
        popProductInfoSubUC.objProductNo = objInputProductNo;
        popProductInfoSubUC.objProductName = objInputProductName;
        popProductInfoSubUC.objSpecification = objInputSpecification;
        popProductInfoSubUC.objUnitID = objInputUnitID;
        popProductInfoSubUC.objUnitName = objInputUnitName;
        popProductInfoSubUC.objSubPriceTax = objInputSubPriceTax;
        popProductInfoSubUC.objSubPrice = objInputSubPrice;
        popProductInfoSubUC.objSubTax = objInputSubTax;
        popProductInfoSubUC.objDiscount = objInputDisCount;
        var h1 = Math.max(document.body.scrollHeight, document.body.clientHeight) - 450;
        $("#div1ProductInfoSubUC3").css("top", h1);
        document.getElementById("div1ProductInfoSubUC").style.display = 'block';
        openRotoscopingDiv(true, "div1ProductInfoSubUC2", "frmProductInfoSubUC")

        popProductInfoSubUC.TurnToPage(1)
    }

    popProductInfoSubUC.CloseList = function() {
        document.getElementById("div1ProductInfoSubUC").style.display = 'none';
        closeRotoscopingDiv(true, "div1ProductInfoSubUC2")
    }


    function Fun_Search_Productshit() {
        search = "1";
        popProductInfoSubUC.TurnToPage(1)
    }

    //排序
    function OrderByProductSub(orderColum, orderTip) {
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
        popProductInfoSubUC.orderBy = orderColum + "_" + ordering;
        popProductInfoSubUC.TurnToPage(1);
    }

    popProductInfoSubUC.Ifshow = function(count) {
        if (count == "0") {
            document.getElementById("divpageProductInfoSubUC").style.display = "none";
            document.getElementById("TotalPageProductInfoSubUC").style.display = "none";
        }
        else {
            document.getElementById("divpageProductInfoSubUC").style.display = "block";
            document.getElementById("TotalPageProductInfoSubUC").style.display = "block";
        }
    }

    function ChangePageCountIndexProductSub(newPageCountProvider, newPageIndexProvider) {
        if (newPageCountProvider <= 0 || newPageIndexProvider <= 0 || newPageIndexProvider > ((popProductInfoSubUC.totalRecord - 1) / newPageCountProvider) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            ifshow = "0";
            popProductInfoSubUC.pageCount = parseInt(newPageCountProvider);
            popProductInfoSubUC.TurnToPage(parseInt(newPageIndexProvider));
        }
    }

    function closeProductInfoSubUCdiv() {
        popProductInfoSubUC.CloseList();
    }

    function pageProductInfoSubUCDataList1(o, a, b, c, d) {
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

    popProductInfoSubUC.TurnToPage = function(pageIndex) {


        var fieldText = "";
        var msgText = "";
        var checkedit = false;

        //先检验页面上的特殊字符
        if (!CheckSpecification($("#txtSpecification").val())) {
            fieldText += "规格|";
            msgText += "规格不能含有特殊字符|";
            checkedit = true;
        }
        var RetVal = CheckSpecialWords();
        if (RetVal != "") {
            fieldText = fieldText + RetVal + "|";
            msgText = msgText + RetVal + "不能含有特殊字符|";
            checkedit = true;
        }
        if (checkedit) {
            popMsgObj.Show(fieldText, msgText);
            fieldText = "";
            msgText = "";
            return;
        }

        popProductInfoSubUC.currentPageIndex = pageIndex;
        var txtRate = document.getElementById('txtRate').value;
        var LastRate = document.getElementById('hiddenRate').value;

        var SubEFIndex = "";
        var SubEFDesc = "";
        if (document.getElementById("trSubNewAttr").style.display != "none") {
            SubEFIndex = document.getElementById("SubselEFIndex").value;
            SubEFDesc = document.getElementById("SubtxtEFDesc").value;
        }

        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SubStoreManager/ProductInfoSubUC.ashx', //目标地址
            cache: false,
            data: "pageIndex=" + popProductInfoSubUC.currentPageIndex
                    + "&pageCount=" + popProductInfoSubUC.pageCount
                    + "&Action=" + popProductInfoSubUC.action
                    + "&orderby=" + popProductInfoSubUC.orderBy
                    + "&ProductName=" + escape(document.getElementById("txtProductName").value)
                    + "&ProductNo=" + escape(document.getElementById("txtProdNo").value)
                    + "&Specification=" + escape(document.getElementById("txtSpecification").value)
                    + "&LastRate=" + escape(LastRate)
                    + "&Rate=" + escape(txtRate)
                    + "&SubEFIndex=" + escape(SubEFIndex)
                    + "&SubEFDesc=" + escape(SubEFDesc), //数据
            beforeSend: function() { $("#pageDataList1_PagerProductInfoSubUC").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataListProductInfoSubUC tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ProductID != null && item.ProductID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" name=\"radioTech\" id=\"radioTech_" + item.ProductID + "\" value=\"" + item.ProductID + "\" onclick=\"popProductInfoSubUCFill(" + item.ProductID + ",'" + item.ProductNo + "','" + item.ProductName + "','" + item.Specification + "','" + item.UnitID + "','" + item.UnitName + "','" + item.SubPriceTax + "','" + item.SubPrice + "','" + item.SubTax + "','" + item.Discount + "','" + item.IsBatchNo + "','" + item.BatchNo + "','" + item.ProductCount + "');\" />" + "</td>" +
                        "<td height='22' align='center'>" + item.ProductNo + "</td>" +
                        "<td height='22' align='center'>" + item.ProductName + "</td>" +
                        "<td height='22' align='center'>" + item.Specification + "</td>" +
                        "<td height='22' align='center'>" + item.UnitName + "</td>" +
                        "<td height='22' align='center'>" + item.SubPriceTax + "</td>" +
                        "<td height='22' align='center'>" + item.SubPrice + "</td>" +
                        "<td height='22' align='center'>" + item.SubTax + "</td>" +
                        "<td height='22' align='center'>" + item.Discount + "</td>" +
                        "<td height='22' align='center'>" + item.ProductCount + "</td>" +
                        "<td height='22' align='center'>" + item.BatchNo + "</td>" +
                        "<td align=\"center\"><a href=\"../../../Pages/Office/SupplyChain/ProductInfoAdd.aspx?intOtherCorpInfoID=" + item.ProductID + "\"  target=\"_blank\">查看</a></td>").appendTo($("#pageDataListProductInfoSubUC tbody"));
                });
                //页码
                ShowPageBar("pageDataList1_PagerProductInfoSubUC", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: popProductInfoSubUC.pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount
                    , showPageNumber: 3
                    , pageCount: popProductInfoSubUC.pageCount
                    , currentPageIndex: popProductInfoSubUC.currentPageIndex
                    , noRecordTip: "没有符合条件的记录"
                    , preWord: "上页"
                    , nextWord: "下页", First: "首页", End: "末页",
                    onclick: "popProductInfoSubUC.TurnToPage({pageindex});return false;"}//[attr]
                    );
                popProductInfoSubUC.totalRecord = msg.totalCount;
                document.getElementById("TotalRecordProductInfoSubUC").value = msg.totalCount;
                $("#PerPageCountProductInfoSubUC").val(popProductInfoSubUC.pageCount);
                ShowTotalPage(msg.totalCount, popProductInfoSubUC.pageCount, popProductInfoSubUC.pageIndex, $("#PageCountProductInfoSubUC"));
                $("#ToPageProductInfoSubUC").val(pageIndex);
            },
            error: function() {
            },
            complete: function() { hidePopup(); $("#pageDataList1_PagerProductInfoSubUC").show(); popProductInfoSubUC.Ifshow(document.getElementById("TotalRecordProductInfoSubUC").value); pageProductInfoSubUCDataList1("pageDataListProductInfoSubUC", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }
</script>

