<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectSellOrderDetailUC.ascx.cs"
    Inherits="UserControl_SelectSellOrderDetailUC" %>
<div id="divSellOrderDetail">
    <a name="pageOrderDetailMark"></a>
    <!--提示信息弹出详情start-->
    <div id="divOederDtailSelect" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 800px; z-index: 21; position: absolute; display: none;
        top: 20%; left: 50%; margin: 5px 0 0 -400px;">
        <table width="99%" border="0" align="center" cellpadding="0" id="Table1" cellspacing="0"
            bgcolor="#CCCCCC">
            <tr>
                <td bgcolor="#FFFFFF">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                        class="table">
                        <tr class="table-item">
                            <td height="25" colspan="6" bgcolor="#E7E7E7" align="left" style="width: 50%;">
                                <img src="../../../Images/Button/Bottom_btn_close.jpg" alt="关闭" onclick="closeOrderDetaildiv()" />
                                <img src="../../../Images/Button/Bottom_btn_ok.jpg" alt="确定" onclick="fnSelectOrderDetail()" />
                            </td>
                        </tr>
                        <tr class="table-item">
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                订单编号
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="OrderDetNoUc" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                主题
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="OrderDetTitle" class="tdinput" maxlength="50" type="text" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                            </td>
                            <td width="21%" bgcolor="#FFFFFF">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" bgcolor="#FFFFFF">
                                <input type="hidden" id="hiddOrdDetSearchModel" value="all" />
                                <input type="hidden" id="hiddOrdCust" />
                                <input type="hidden" id="hiddOrdRate" />
                                <input type="hidden" id="hiddOrdCurrencyType" />
                                <input type="hidden" id="hiddOrdIdUC" />
                                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                    onclick='TurnToDetPage(1)' />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="OrderDetailDataList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择<input type="checkbox" visible="false" id="chkOrderDetail" onclick="selectAllDetail()"
                            value="checkbox" />
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderDetailBy('OrderNo','oGroupDet');return false;">
                            订单编号<span id="oGroupDet" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderDetailBy('SortNo','oDet4');return false;">
                            明细行号<span id="oDet4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderDetailBy('ProdNo','oDet5');return false;">
                            物品编号<span id="oDet5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderDetailBy('ProductName','oDet6');return false;">
                            物品名称<span id="oDet6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderDetailBy('ColorName','oColorName');return false;">
                            颜色<span id="oColorName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderDetailBy('pCount','oDet7');return false;">
                            未执行数量<span id="oDet7" class="orderTip"></span></div>
                    </th>
                    <% if (((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString() == "True")
                       {%>
                       
                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderDetailBy('PProductCount','oDet8');return false;">
                            基本数量<span id="oDet8" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderDetailBy('UsedPProductCount','oDet9');return false;">
                            计划生产数量<span id="oDet9" class="orderTip"></span></div>
                    </th>
                       <%}
                       else
                       { %>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderDetailBy('PProductCount','oDet10');return false;">
                            计划生产数量<span id="oDet10" class="orderTip"></span></div>
                    </th>
                       
                       <%} %>

                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderDetailBy('UseStockCount','oDet11');return false;">
                            使用库存数量<span id="oDet11" class="orderTip"></span></div>
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
                                <div id="pageDetail">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDetList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divDetpage">
                                    <span id="pageDetList_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowDetPageCount" style="width: 20px;" maxlength="3" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 20px;" id="DetToPage" maxlength="7" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        align="absmiddle" onclick="ChangeDetPageCountIndex($('#ShowDetPageCount').val(),$('#DetToPage').val());" />
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
    var glbControl_IsMoreUnit = '<%=((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString() %>'; /*是否启用多计量单位*/
    var popOrderDetailObj = new Object();

    popOrderDetailObj.ShowList = function(CustID, CurrencyType, Rate, OrderID) {
        $("#hiddOrdCust").val(CustID); //客户
        $("#hiddOrdCurrencyType").val(CurrencyType); //币种
        $("#hiddOrdRate").val(Rate); //汇率
        $("#hiddOrdIdUC").val(OrderID); //已选择的源单id
        ShowPreventReclickDiv();
        $("#OrderDetTitle").val('');
        $("#OrderDetNoUc").val('');

        document.getElementById('divOederDtailSelect').style.display = 'block';
        TurnToDetPage(1);
    }

    var pageDetCount = 10; //每页计数
    var totalDetRecord = 0;
    var pagerDetStyle = "flickr"; //jPagerBar样式

    var currentDetPageIndex = 1;
    var actionDet = ""; //操作
    var orderByDet = ""; //排序字段
    //jQuery-ajax获取JSON数据
    function TurnToDetPage(pageIndex) {
        currentDetPageIndex = pageIndex;
        var Title = $.trim($("#OrderDetTitle").val());
        var OrderNo = $.trim($("#OrderDetNoUc").val());
        var Rate = $("#hiddOrdRate").val(); //汇率
        var CustID = $("#hiddOrdCust").val();
        var CurrencyType = $("#hiddOrdCurrencyType").val();
        var OrderID = $("#hiddOrdIdUC").val();
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SellManager/SelectSellOrderDetail.ashx', //目标地址
            cache: false,
            data: "pageIndex=" + pageIndex + "&pageDetCount=" + pageDetCount + "&actionDet=detail&orderByDet=" + orderByDet + "&CustID=" + CustID +
            "&Title=" + escape(Title) + "&orderNo=" + escape(OrderNo) + "&OrderID=" + escape(OrderID) + "&CurrencyType=" + escape(CurrencyType) + '&Rate=' + escape(Rate), //数据
            beforeSend: function() { $("#pageDetList_Pager").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#OrderDetailDataList tbody").find("tr.newrowOrdDet").remove();
                $.each(msg.data, function(i, item) {
                    if (item.OrderID != null && item.OrderID != "") {
                        var OrderNo = item.OrderNo;



                        if (OrderNo != null) {
                            if (OrderNo.length > 20) {
                                OrderNo = OrderNo.substring(0, 20) + '...';
                            }
                        }
                        if (glbControl_IsMoreUnit == 'True') {
                            $("<tr class='newrowOrdDet'></tr>").append("<td height='22' align='center'><input id='chkDet" + i + "' value=" + item.DetailID + " type='checkbox'  /></td>" +
                        "<td height='22' align='center'><span title=\"" + item.OrderNo + "\">" + OrderNo + "</td>" +

                        "<td height='22' align='center'>" + item.SortNo + "</td>" +
                        "<td height='22' align='center'>" + item.ProdNo + "</td>" +
                        "<td height='22' align='center'>" + item.ProductName + "</td>" +
                        "<td height='22' align='center'>" + item.ColorName + "</td>" +
                        "<td height='22' align='center'>" + item.pCount + "</td>" +
                        "<td height='22' align='center'>" + item.PProductCount + "</td>" +
                        "<td height='22' align='center'>" + item.UsedPProductCount + "</td>" +
                        "<td height='22' align='center'>" + item.UseStockCount + "</td>").appendTo($("#OrderDetailDataList tbody"));
                        }
                        else {
                            $("<tr class='newrowOrdDet'></tr>").append("<td height='22' align='center'><input id='chkDet" + i + "' value=" + item.DetailID + " type='checkbox'  /></td>" +
                        "<td height='22' align='center'><span title=\"" + item.OrderNo + "\">" + OrderNo + "</td>" +

                        "<td height='22' align='center'>" + item.SortNo + "</td>" +
                        "<td height='22' align='center'>" + item.ProdNo + "</td>" +
                        "<td height='22' align='center'>" + item.ProductName + "</td>" +
                        "<td height='22' align='center'>" + item.ColorName + "</td>" +
                        "<td height='22' align='center'>" + item.pCount + "</td>" +
                        "<td height='22' align='center'>" + item.PProductCount + "</td>" +
                        "<td height='22' align='center'>" + item.UseStockCount + "</td>").appendTo($("#OrderDetailDataList tbody"));
                        }
                    }
                });
                //页码
                ShowPageBar("pageDetList_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerDetStyle
                    , mark: "pageOrderDetailMark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageDetCount,
                    currentPageIndex: pageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "TurnToDetPage({pageindex});return false; "}//[attr]
                    );
                totalDetRecord = msg.totalCount;
                $("#ShowDetPageCount").val(pageDetCount);
                ShowTotalPage(msg.totalCount, pageDetCount, pageIndex, $("#pageDetail"));
                $("#DetToPage").val(pageIndex);
            },
            error: function() { },
            complete: function() { $("#pageDetList_Pager").show(); OrderDetailDataList("OrderDetailDataList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕

        });
        $("#chkOrderDetail").removeAttr("checked");
    }
    //table行颜色
    function OrderDetailDataList(o, a, b, c, d) {
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

    //改变每页记录数及跳至页数
    function ChangeDetPageCountIndex(newPageCount, newPageIndex) {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;
        $("#chkOrderDetail").removeAttr("checked");
        if (!IsNumber(newPageIndex) || newPageIndex == 0) {
            isFlag = false;
            fieldText = fieldText + "跳转页面|";
            msgText = msgText + "必须为正整数格式|";
        }
        if (!IsNumber(newPageCount) || newPageCount == 0) {
            isFlag = false;
            fieldText = fieldText + "每页显示|";
            msgText = msgText + "必须为正整数格式|";
        }
        if (!isFlag) {
            popMsgObj.Show(fieldText, msgText);
        }
        else {
            if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalDetRecord - 1) / newPageCount) + 1) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
                return false;
            }
            else {
                this.pageDetCount = parseInt(newPageCount);
                TurnToDetPage(parseInt(newPageIndex));
            }
        }
    }
    //排序
    function OrderDetailBy(orderColum, orderTip) {
        var ordering = "d";
        var orderTipDOM = $("#" + orderTip);
        var allOrderTipDOM = $(".orderTip");
        if ($("#" + orderTip).html() == "↓") {
            ordering = "a";
            allOrderTipDOM.empty();
            $("#" + orderTip).html("↑");
        }
        else {

            allOrderTipDOM.empty();
            $("#" + orderTip).html("↓");
        }
        orderByDet = orderColum + "_" + ordering;
        TurnToDetPage(1);
    }


    function closeOrderDetaildiv() {
        obj = document.getElementById("divPreventReclick");
        //隐藏遮挡的DIV
        if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
        document.getElementById("divOederDtailSelect").style.display = "none";
    }

    function fnSelectOrderDetail() {
        var DetailID = '';
        var OrderDetailDataList = findObj("OrderDetailDataList", document);
        for (i = 0; i < OrderDetailDataList.rows.length; i++) {
            if ($("#chkDet" + i).attr("checked")) {
                DetailID += $("#chkDet" + i).val() + ',';
            }
        }
        if (DetailID == '') {
            alert("您没有选择任何数据！");
        }
        else {
            $.ajax({
                type: "POST", //用POST方式传输
                dataType: "json", //数据格式:JSON
                url: '../../../Handler/Office/SellManager/SelectSellOrderDetail.ashx', //目标地址
                cache: false,
                data: "actionDet=getLsit&DetailID=" + DetailID, //数据
                beforeSend: function() { }, //发送数据之前      
                success: function(msg) {
                    if (msg.data.length == 0) {
                        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取数据失败！");
                    }
                    else {
                        fnSetDetailData(msg.data);
                    }
                },
                error: function() { },
                complete: function() { } //接收数据完毕 
            });

        }
    }

    //全选
    function selectAllDetail() {
        var OrderDetailDataList = findObj("OrderDetailDataList", document);
        for (i = 0; i < OrderDetailDataList.rows.length; i++) {
            if ($("#chkOrderDetail").attr("checked")) {
                $("#chkDet" + i).attr("checked", "true");
            }
            else {
                $("#chkDet" + i).removeAttr("checked");
            }
        }
    }
</script>

