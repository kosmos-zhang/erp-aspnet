<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubProductSendPriceControl.ascx.cs"
    Inherits="UserControl_SubProductSendPriceControl" %>
<%@ Register Src="ProductDiyAttr.ascx" TagName="ProductDiyAttr" TagPrefix="uc1" %>
<div id="divModuleProdcutInfo">
    <!--提示信息弹出详情start-->
    <a name="pageProductInfoMark"></a>
    <input id="txtOrderBy" type="hidden" value="ID DESC" />
    <div id="divGetProductInfo" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 70%; z-index: 300; position: absolute; display: none;">
        <table width="100%" style="padding-top: 0px">
            <tr>
                <td style="width: 100%">
                    <img alt="关闭" id="btn_Close1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
                        onclick='closeProductInfodiv();' />
                </td>
            </tr>
        </table>
        <table width="99%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClick'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />
                    </div>
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
                    <table width="99%" border="0" align="center" cellpadding="1" id="searchtable" cellspacing="0"
                        bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                    id="tblInterviewInfo">
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            产品名称
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <input id="txtProductName" type="text" class="tdinput tboxsize" maxlength="25" specialworkcheck="产品名称" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            产品编号
                                        </td>
                                        <td height="20" class="tdColInput" width="24%">
                                            <input id="txtProdNo" type="text" class="tdinput tboxsize" maxlength="25" specialworkcheck="产品编号" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            规格
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <input id="txtSpecification" type="text" class="tdinput tboxsize" maxlength="25" />
                                        </td>
                                    </tr>
                                    <tr style="display: none" id="trNewAttr">
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <span id="spanOther" style="display: none">其他条件</span>
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <uc1:ProductDiyAttr ID="ProductDiyAttr1" runat="server" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                        </td>
                                        <td height="20" class="tdColInput" width="24%">
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                                onclick='ProductInfoTurnToPage(1);' id="img_btn_search" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="divProductInfoList"
                        bgcolor="#999999">
                        <tbody>
                            <tr>
                                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    选择
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="CreateSort('DeptID');return false;">
                                        分店名称<span id="DeptID" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="CreateSort('ProdNo');return false;">
                                        产品编号<span id="ProdNo" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="CreateSort('ProductName');return false;">
                                        产品名称<span id="ProductName" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="CreateSort('Specification');return false;">
                                        规格<span id="Specification" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="CreateSort('UnitName');return false;">
                                        单位<span id="UnitName" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="CreateSort('SendPriceTax');return false;">
                                        含税配送价<span id="SendPriceTax" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="CreateSort('SendPrice');return false;">
                                        去税配送价 <span id="SendPrice" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="CreateSort('Discount');return false;">
                                        折扣(%) <span id="Discount" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="CreateSort('StorageName');return false;">
                                        仓库 <span id="StorageName" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="CreateSort('BatchNo');return false;">
                                        批次 <span id="BatchNo" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    商品信息
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
                                            <div id="pageProductInfocount">
                                            </div>
                                        </td>
                                        <td height="28" align="right">
                                            <div id="getproductlist_Pager" class="jPagerBar">
                                            </div>
                                        </td>
                                        <td height="28" align="right">
                                            <div id="divProductInfoPage">
                                                <span id="pageProductInfo_Total"></span>每页显示
                                                <input name="text" type="text" style="width: 30px;" id="ShowProductInfoPageCount" />
                                                条 转到第
                                                <input name="text" type="text" style="width: 30px;" id="ToProductInfoPage" />
                                                页
                                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                    width="36" height="28" align="absmiddle" onclick="ChangeProductInfoPageCountIndex($('#ShowProductInfoPageCount').val(),$('#ToProductInfoPage').val());" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
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
    var popProductInfoObj = new Object();
    popProductInfoObj.InputObj = null;

    popProductInfoObj.ShowList = function(pageIndex) {
        document.getElementById('divGetProductInfo').style.display = 'block';
        CenterToDocument("divGetProductInfo", true);
        ProductInfoTurnToPage(pageIndex);
    }

    var pageProductInfocount = 10; //每页计数
    var totalSellEmpRecord = 0;
    var pagerSellEmpStyle = "flickr"; //jPagerBar样式

    var currentSellEmpPageIndex = 1;
    var actionSellEmp = ""; //操作
    var orderSellEmpBy = ""; //排序字段
    //jQuery-ajax获取JSON数据
    var pageCount = 10; //每页计数
    var totalRecord = 0;
    function ProductInfoTurnToPage(pageIndex) {

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

        /*构造参数*/
        var SubStore = document.getElementById("ddlSubStore").value;
        if (document.getElementById("txtIsDefault").value == "1")
            SubStore = "0";
        document.getElementById("txtIsDefault").value == "0"
        var para = "action=GETLIST&";
        para = para + "DeptID=" + SubStore +
                "&ProductID=-1" +
                "&pageIndex=" + pageIndex + "&PageSize=" + pageCount +
                "&OrderBy=" + document.getElementById("txtOrderBy").value +
                "&ProductName=" + escape(document.getElementById("txtProductName").value) +
                "&ProdNo=" + escape(document.getElementById("txtProdNo").value) +
                "&Specification=" + escape(document.getElementById("txtSpecification").value) +
                "&EFIndex=" + escape($("#selEFIndex").val()) +
                "&EFDesc=" + escape($("#txtEFDesc").val());
        /*异步页面*/
        var url = "../../../Handler/Office/LogisticsDistributionManager/SubProductSendPriceSetting.ashx";
        currentSellEmpPageIndex = pageIndex;
        $.ajax({
            type: "POST",
            dataType: "json",
            url: url,
            cache: false,
            data: para,
            beforeSend: function() { openRotoscopingDiv(false, "divPageMask", "PageMaskIframe"); },
            success: function(msg) {

                if (msg.totalCount <= 0 && document.getElementById("txtIsDefault").value != "1") {
                    document.getElementById("txtIsDefault").value = "1";
                    ProductInfoTurnToPage(pageIndex);

                }
                else
                    document.getElementById("txtIsDefault").value = "2";

                $("#divProductInfoList tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item != "") {
                        $("<tr class='newrow'></tr>").append("<td align=\"center\" ><input type=\"radio\" name=\"radioEmp_\" id=\"radioEmp_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"checkProdcut(" + item.ID + ",'" + item.ProdNo + "','" + item.ProductName + "','" + item.Specification + "','" + item.UnitName + "','" + parseFloat(item.SendPrice).toFixed(2) + "','" + getSendPrice(item) + "','" + getDiscount(item.Discount) + "'," + item.ProductID + "," + item.UnitID + ",'" + item.IsBatchNo + "','" + item.BatchNo + "','" + item.StorageID + "');\" /></td>" +
                            "<td align=\"center\" >" + getSubStore(item.DetpName) + "<input type=\"hidden\"  value=\"" + item.DeptID + "\" id=\"txtHDeptID_" + item.ID + "\"/></td>" +
                             "<td align=\"center\" >" + item.ProdNo + "<input type=\"hidden\" id=\"txtHProduct_" + item.ID + "\" value=\"" + item.ProductName + "\" title=\"" + item.ProductID + "\"></td>" +
                             "<td align=\"center\" >" + item.ProductName + "<input type=\"hidden\" id=\"txtHSendPriceTax_" + item.ID + "\" value=\"" + parseFloat(item.SendPriceTax).toFixed(2) + "\" title=\"" + parseFloat(item.SendTax).toFixed(2) + "\"></td>" +
                             "<td align=\"center\" >" + item.Specification + "<input type=\"hidden\" id=\"txtHSendPrice_" + item.ID + "\" value=\"" + parseFloat(item.SendPrice).toFixed(2) + "\" /></td>" +
                             "<td align=\"center\" >" + item.UnitName + "<input type=\"hidden\" id=\"txtHDiscount_" + item.ID + "\" value=\"" + getDiscount(item.Discount) + "\" /></td>" +
                             "<td align=\"center\" >" + getSendPrice(item) + "</td>" +
                             "<td align=\"center\" >" + FormatAfterDotNumber(item.SendPrice, selPoint) + "</td>" +
                             "<td align=\"center\" >" + FormatAfterDotNumber(item.Discount, selPoint) + "</td>" +
                             "<td align=\"center\" >" + item.StorageName + "</td>" +
                             "<td align=\"center\" >" + item.BatchNo + "</td>" +
                             "<td align=\"center\"><a href=\"../SupplyChain/ProductInfoAdd.aspx?intOtherCorpInfoID=" + item.ProductID + "\"  target=\"_blank\">查看</a></td>").appendTo($("#divProductInfoList tbody"));
                    }
                });
                //页码
                ShowPageBar("getproductlist_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerSellEmpStyle, mark: "pageProductInfoMark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageCount,
                    currentPageIndex: pageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "ProductInfoTurnToPage({pageindex});return false;"}//[attr]
                    );
                totalSellEmpRecord = msg.totalCount;
                $("#ShowProductInfoPageCount").val(pageCount);
                ShowTotalPage(msg.totalCount, pageProductInfocount, pageIndex, $("#pageProductInfocount"));
                $("#ToProductInfoPage").val(pageIndex);
            },
            error: function(msg) { },
            complete: function() { $("#getproductlist_Pager").show(); pageProductInfoDataList1("divProductInfoList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }
    //table行颜色
    function pageProductInfoDataList1(o, a, b, c, d) {
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

    /*设置配送价*/
    function getSendPrice(item) {
        if (document.getElementById("txtIsDefault").value == "2" && (item.SendPriceTax == "0.0000" || item.SendPriceTax == "")) {
            return FormatAfterDotNumber(item.SellPrice, selPoint);
        }
        else {
            return FormatAfterDotNumber(item.SendPriceTax, selPoint);
        }
    }
    //改变每页记录数及跳至页数
    function ChangeProductInfoPageCountIndex(newPageCount, newPageIndex, rowid) {

        if (!IsNumOrFloat(newPageCount, true)) {
            popMsgObj.Show("配置价格列表|", "每页显示数必须为数值|");
            document.getElementById("ShowPageCount").value = "10";
            return;
        }
        if (!IsNumOrFloat(newPageIndex, true)) {
            popMsgObj.Show("配置价格列表|", "页数必须为数值|");
            document.getElementById("ToPage").value = "1";
            return;
        }
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalSellEmpRecord - 1) / newPageCount) + 1) {
            popMsgObj.Show("配置价格列表|", "转到页数超出查询范围！|");
            return false;
        }
        else {
            this.pageCount = parseInt(newPageCount);
            ProductInfoTurnToPage(parseInt(newPageIndex))
        }
    }
    //排序
    /*设置排序字段*/
    function CreateSort(control) {
        var ordering = document.getElementById("txtOrderBy");
        var obj = document.getElementById(control);
        var allOrderTipDOM = $(".orderTip");
        allOrderTipDOM.empty();
        if (ordering.value == (control + " ASC")) {
            ordering.value = control + " DESC";
            obj.innerHTML = "↓";
        }
        else {
            ordering.value = control + " ASC";
            obj.innerHTML = "↑";
        }
        ProductInfoTurnToPage(1);
    }


    function closeProductInfodiv() {
        document.getElementById("divGetProductInfo").style.display = "none";
        closeRotoscopingDiv(false, "divPageMask");
    }

    /*格式化折扣*/
    function getDiscount(discount) {

        return discount;
        //    if(discount!="100.00")
        //        return discount;
        //    else
        //        return "";
    }


    /*判断是默认配送还是分店*/
    function getSubStore(name) {
        if (name == "")
            name = "默认";
        return name;
    }
</script>

