<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectSellOfferUC.ascx.cs"
    Inherits="UserControl_SelectSellOfferUC" %>
<div id="sellOffer">
    <a name="pageOffDataList1Mark"></a>
    <!--提示信息弹出详情start-->
    <div id="divSellOfferSelect" style="border: solid 10px #93BCDD; background: #fff;
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
                    
                                 <img src="../../../Images/Button/Bottom_btn_close.jpg" alt="关闭" onclick="closeOffdiv()" />
                                
                            <img src="../../../Images/Button/Main_btn_qk.jpg" alt="清空" onclick="clearSellOff()" />
                              
                            </td>
                        </tr>
                        <tr class="table-item">
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                编号
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="OffNo" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                主题
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="OffTitle" class="tdinput" maxlength="50" type="text" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                客户
                            </td>
                            <td width="21%" bgcolor="#FFFFFF">
                                <input id="OffCustName" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                            </td>
                        </tr>
                        <tr class="table-item">
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                币种
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <asp:DropDownList ID="OffCurrencyType" runat="server" Width="120px" AppendDataBoundItems="True">
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
                                <input type="hidden" id="hiddOffSearchModel" value="all" />
                                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                    onclick='TurnToOffPage(1)' />&nbsp;
                            </td>
                        </tr>
                    </table>
        </td> </tr> </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="offerDataList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('OfferNo','OffUC0');return false;">
                            报价单号<span id="OffUC0" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('Title','OffUC1');return false;">
                            主题<span id="OffUC1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('CustName','OffUC2');return false;">
                            客户<span id="OffUC2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('EmployeeName','OffUC3');return false;">
                            业务员<span id="OffUC3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('QuoteTime','OffUC4');return false;">
                            报价次数<span id="OffUC4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('CurrencyName','OffUC5');return false;">
                            币种<span id="OffUC5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('Rate','OffUC6');return false;">
                            汇率<span id="OffUC6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('OfferDate','OffUC7');return false;">
                            报价日期<span id="OffUC7" class="orderTip"></span></div>
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
                                <div id="pageOffcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageOffList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divOffpage">
                                    <span id="pageOffList_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowOffPageCount" style="width: 20px;" maxlength="4" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 20px;" id="OffToPage" maxlength="7" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeOffPageCountIndex($('#ShowOffPageCount').val(),$('#OffToPage').val());" />
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
    var popSellOfferObj = new Object();
    popSellOfferObj.InputObj = null;


    popSellOfferObj.ShowList = function(objInput) {
        popSellOfferObj.InputObj = objInput;
        ShowPreventReclickDiv();
        $("#OffTitle").val('');
        $("#OffNo").val('');
        $("#ConCustName").val('');
        $("#SelectSellOfferUC1_OffCurrencyType").val('');
        $("#hiddOffSearchModel").val(objInput); //查询的模式，all是取出所有的销售报价单，protion时取出所有执行状态的报价单
        document.getElementById('divSellOfferSelect').style.display = 'block';
     TurnToOffPage(1);
    }

    var pageOffCount = 10; //每页计数
    var totalOffRecord = 0;
    var pagerOffStyle = "flickr"; //jPagerBar样式

    var currentOffPageIndex = 1;
    var actionOff = ""; //操作
    var orderByOff = ""; //排序字段
    //jQuery-ajax获取JSON数据
    function TurnToOffPage(pageIndex) {
        currentOffPageIndex = pageIndex;
        var Title = $.trim($("#OffTitle").val());
        var OrderNo = $.trim($("#OffNo").val());
        var model = $("#hiddOffSearchModel").val();
        var CustName = $.trim($("#ConCustName").val());
        var CurrencyType = $.trim($("#SelectSellOfferUC1_OffCurrencyType").val());
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SellManager/SelectSellOfferUC.ashx', //目标地址
            cache: false,
            data: "pageIndex=" + pageIndex + "&pageOffCount=" + pageOffCount + "&actionOff=UC&orderByOff=" + orderByOff +
            "&Title=" + escape(Title) + "&orderNo=" + escape(OrderNo) + '&CustName=' + escape(CustName) + '&CurrencyType=' + escape(CurrencyType) + "&model=" + escape(model), //数据,//数据
            beforeSend: function() { $("#pageOffList_Pager").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#offerDataList tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item.ID != "") {
                        var OrderNo = item.OfferNo;
                        var Title = item.Title;
                        var CustName = item.CustName;
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
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" name=\"radioOff\" id=\"radioSellOff_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"fnSelectSellOffer(" + item.ID + ",'" + item.OfferNo + "');\" />" + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.OfferNo + "\">" + OrderNo + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.Title + "\">" + Title + "</td>" +
                          "<td height='22' align='center'><span title=\"" + item.CustName + "\">" + CustName + "</td>" +
                        "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                        
                        "<td height='22' align='center'>" + item.QuoteTime + "</td>" +
                        "<td height='22' align='center'>" + item.CurrencyName + "</td>" +
                        "<td height='22' align='center'>" + item.Rate + "</td>" +
                      "<td height='22' align='center'>" + item.OfferDate + "</td>" ).appendTo($("#offerDataList tbody"));
                    }
                });
                //页码
                ShowPageBar("pageOffList_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerOffStyle
                    , mark: "pageOffDataList1Mark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageOffCount,
                    currentPageIndex: pageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "TurnToOffPage({pageindex});return false;"}//[attr]
                    );
                totalOffRecord = msg.totalCount;
                $("#ShowOffPageCount").val(pageOffCount);
                ShowTotalPage(msg.totalCount, pageOffCount, pageIndex, $("#pageOffcount"));
                $("#OffToPage").val(pageIndex);
            },
            error: function() { },
            complete: function() { $("#pageOffList_Pager").show(); offerDataList("offerDataList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }
    //table行颜色
    function offerDataList(o, a, b, c, d) {
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
    function ChangeOffPageCountIndex(newPageCount, newPageIndex) {
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
            if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalOffRecord - 1) / newPageCount) + 1) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
                return false;
            }
            else {
                this.pageOffCount = parseInt(newPageCount);
                TurnToOffPage(parseInt(newPageIndex));
            }
        }
    }
    //排序
    function OrderOffBy(orderColum, orderTip) {
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
        orderByOff = orderColum + "_" + ordering;
        TurnToOffPage(1);
    }


    function closeOffdiv() {
        obj = document.getElementById("divPreventReclick");
        //隐藏遮挡的DIV
        if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
        document.getElementById("divSellOfferSelect").style.display = "none";
    }
</script>

