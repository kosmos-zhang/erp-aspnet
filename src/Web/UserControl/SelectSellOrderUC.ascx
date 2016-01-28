<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectSellOrderUC.ascx.cs"
    Inherits="UserControl_SelectSellOrderUC" %>
<div id="divSellOrder">
    <a name="pageSellOrderMark"></a>
    <!--提示信息弹出详情start-->
    <div id="divSellOrderSelect" style="border: solid 10px #93BCDD; background: #fff;
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
                               <img src="../../../Images/Button/Bottom_btn_close.jpg" alt="关闭" onclick="closeSellOrderdiv()" />
                                
                            <img src="../../../Images/Button/Main_btn_qk.jpg" alt="清空" onclick="clearSellOrder()" />
                            
                              
                            </td>
                        </tr>
                        <tr class="table-item">
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                编号
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="OrderNoUc" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                主题
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="OrderTitle" class="tdinput" maxlength="50" type="text" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                客户
                            </td>
                            <td width="21%" bgcolor="#FFFFFF">
                                <input id="OrdCustName" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                            </td>
                        </tr>
                        <tr class="table-item">
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                币种
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <asp:DropDownList ID="OrdCurrencyType" runat="server" Width="120px" AppendDataBoundItems="True">
                                    <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                            </td>
                            <td width="21%" bgcolor="#FFFFFF">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" bgcolor="#FFFFFF">
                                <input type="hidden" id="hiddOrdSearchModel" value="all" />
                                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                    onclick='TurnToOrderPage(1)' />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="SellOrderDataList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderSellOrderBy('OrderNo','oGroupOrder');return false;">
                            订单编号<span id="oGroupOrder" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderSellOrderBy('Title','oCOrder1');return false;">
                            订单主题<span id="oCOrder1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderSellOrderBy('CustName','oCOrder2');return false;">
                            客户<span id="oCOrder2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderSellOrderBy('EmployeeName','oCOrder4');return false;">
                            业务员<span id="oCOrder4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderSellOrderBy('CurrencyName','oCOrder3');return false;">
                            币种<span id="oCOrder3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderSellOrderBy('Rate','oCOrder5');return false;">
                            汇率<span id="oCOrder5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderSellOrderBy('OrderDate','oCOrder6');return false;">
                            下单时间<span id="oCOrder6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderSellOrderBy('isSendText','oCOrder7');return false;">
                            发货情况<span id="oCOrder7" class="orderTip"></span></div>
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
                                <div id="pageSellOrderCount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageOrderList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divOrderPage">
                                    <span id="pageOrderList_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowOrderPageCount" style="width: 20px;" maxlength="3" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 20px;" id="OrderToPage" maxlength="7" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeOrderPageCountIndex($('#ShowOrderPageCount').val(),$('#OrderToPage').val());" />
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
    var popSellOrderObj = new Object();
    popSellOrderObj.InputObj = null;


    popSellOrderObj.ShowList = function(objInput) {
        popSellOrderObj.InputObj = objInput;
        ShowPreventReclickDiv();
        $("#OrderTitle").val('');
        $("#OrderNoUc").val('');
        $("#OrdCustName").val('');
        $("#SelectSellOrderUC1_OrdCurrencyType").val('');
        $("#hiddOrdSearchModel").val(objInput); //查询的模式，all是取出所有的销售合同，protion时取出所有执行状态的合同
        document.getElementById('divSellOrderSelect').style.display = 'block';
       TurnToOrderPage(1);
    }

    var pageOrderCount = 10; //每页计数
    var totalOrderRecord = 0;
    var pagerOrderStyle = "flickr"; //jPagerBar样式

    var currentOrderPageIndex = 1;
    var orderByOrder = ""; //排序字段
    //jQuery-ajax获取JSON数据
    function TurnToOrderPage(pageIndex) {
        currentOrderPageIndex = pageIndex;
        var CustName = $.trim($("#OrdCustName").val());
        var CurrencyType = $.trim($("#SelectSellOrderUC1_OrdCurrencyType").val());
        var Title = $.trim($("#OrderTitle").val());
        var OrderNo = $.trim($("#OrderNoUc").val());
        var model = $("#hiddOrdSearchModel").val();
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SellManager/SelectSellOrderDetail.ashx', //目标地址
            cache: false,
            data: "pageIndex=" + pageIndex + "&pageOrderCount=" + pageOrderCount + "&actionDet=order&orderByOrder=" + orderByOrder +
            "&Title=" + escape(Title) + "&orderNo=" + escape(OrderNo) + '&CustName=' + escape(CustName) + '&CurrencyType=' + escape(CurrencyType) + "&model=" + escape(model), //数据
            beforeSend: function() { $("#pageOrderList_Pager").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#SellOrderDataList tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item.ID != "") {
                        var OrderNo = item.OrderNo;
                        var Title = item.Title;
                        var CustName = item.CustName;
                        var EmployeeName = item.EmployeeName;
                        if (Title != null) {
                            if (Title.length > 6) {
                                Title = Title.substring(0, 6) + '...';
                            }
                        }
                        if (OrderNo != null) {
                            if (OrderNo.length > 20) {
                                OrderNo = OrderNo.substring(0, 20) + '...';
                            }
                        }
                        if (CustName != null) {
                            if (CustName.length > 6) {
                                CustName = CustName.substring(0, 6) + '...';
                            }
                        }
                        if (EmployeeName != null) {
                            if (EmployeeName.length > 6) {
                                EmployeeName = EmployeeName.substring(0, 6) + '...';
                            }
                        }
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\"  id=\"radioSellOrder_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"fnSelectSellOrder(" + item.ID + ",'" + item.OrderNo + "');\" name=\"radioOrder\" />" + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.OrderNo + "\">" + OrderNo + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.Title + "\">" + Title + "</td>" +
                          "<td height='22' align='center'><span title=\"" + item.CustName + "\">" + CustName + "</td>" +
                        "<td height='22' align='center'><span title=\"" + item.EmployeeName + "\">" + EmployeeName + "</td>" +
                        "<td height='22' align='center'>" + item.CurrencyName + "</td>" +
                        "<td height='22' align='center'>" + item.Rate + "</td>" +
                      "<td height='22' align='center'>" + item.OrderDate + "</td>"+
                      "<td height='22' align='center'>" + item.isSendText + "</td>").appendTo($("#SellOrderDataList tbody"));
                    }
                });
                //页码
                ShowPageBar("pageOrderList_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerOrderStyle
                    , mark: "pageSellOrderMark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageOrderCount,
                    currentPageIndex: pageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "TurnToOrderPage({pageindex});return false;"}//[attr]
                    );
                totalOrderRecord = msg.totalCount;
                $("#ShowOrderPageCount").val(pageOrderCount);
                ShowTotalPage(msg.totalCount, pageOrderCount, pageIndex, $("#pageSellOrderCount"));
                $("#OrderToPage").val(pageIndex);
            },
            error: function() { },
            complete: function() { $("#pageOrderList_Pager").show(); SellOrderDataList("SellOrderDataList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }

    //table行颜色
    function SellOrderDataList(o, a, b, c, d) {
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
    function ChangeOrderPageCountIndex(newPageCount, newPageIndex) {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;
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
            if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalOrderRecord - 1) / newPageCount) + 1) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
                return false;
            }
            else {
                this.pageOrderCount = parseInt(newPageCount);
                TurnToOrderPage(parseInt(newPageIndex));
            }
        }
    }

    //排序
    function OrderSellOrderBy(orderColum, orderTip) {
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
        orderByOrder = orderColum + "_" + ordering;
        TurnToOrderPage(1);
    }

    function closeSellOrderdiv() {
        obj = document.getElementById("divPreventReclick");
        //隐藏遮挡的DIV
        if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
        document.getElementById("divSellOrderSelect").style.display = "none";
    }
</script>

