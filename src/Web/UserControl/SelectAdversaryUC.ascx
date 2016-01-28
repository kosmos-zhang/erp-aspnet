<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectAdversaryUC.ascx.cs"
    Inherits="UserControl_SelectAdversaryUC" %>
<div id="Adversary">
    <a name="pageAdDataList1Mark"></a>
    <!--提示信息弹出详情start-->
    <div id="divSellAdSelect" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 700px; z-index: 21; position: absolute; display: none; top: 20%; left: 60%;
        margin: 5px 0 0 -400px;">
        <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
            bgcolor="#CCCCCC">
            <tr>
                <td bgcolor="#FFFFFF">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                        class="table">
                        <tr class="table-item">
                            <td  height="25" colspan="6" bgcolor="#E7E7E7"  align="left" style="width: 50%; ">
                             <img src="../../../Images/Button/Bottom_btn_close.jpg" alt="关闭" onclick="closeAddiv()" />
                               
                            
                                <img src="../../../Images/Button/Main_btn_qk.jpg" alt="清空" onclick="clearAdversary()" />
                            </td>
                        </tr>
                        <tr class="table-item">
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                对手编号
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="CustNoAd" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                对手名称
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="CustNameAd" class="tdinput" maxlength="50" type="text" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                            </td>
                            <td width="21%" bgcolor="#FFFFFF">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" bgcolor="#FFFFFF">
                                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"  style='cursor: pointer;'
                                    onclick='TurnToAdPage(1)' />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="offerDataList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderAdBy('CustNo','oGroupAd');return false;">
                            对手编号<span id="oGroupAd" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderAdBy('CustName','oCAd1');return false;">
                            对手名称<span id="oCAd1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderAdBy('TypeName','oCAd4');return false;">
                            对手类别<span id="oCAd4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderAdBy('EmployeeName','oCAd5');return false;">
                            创建人<span id="oCAd5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderAdBy('CreatDate','oCAd6');return false;">
                            创建日期<span id="oCAd6" class="orderTip"></span></div>
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
                                <div id="pageAdcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageAdList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divAdpage">
                                    <span id="pageAdList_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowAdPageCount" style="width: 20px;" maxlength="4" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 20px;" id="AdToPage" maxlength="7" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        align="absmiddle" onclick="ChangeAdPageCountIndex($('#ShowAdPageCount').val(),$('#AdToPage').val());" />
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
    var popSellAdObj = new Object();
    popSellAdObj.InputObj = null;


    popSellAdObj.ShowList = function(objInput) {
        popSellAdObj.InputObj = objInput;
        ShowPreventReclickDiv();
        document.getElementById('divSellAdSelect').style.display = 'block';
        $("#CustNameAd").val('');
        $("#CustNoAd").val('');
        TurnToAdPage(1);
    }

    var pageAdCount = 10; //每页计数
    var totalAdRecord = 0;
    var pagerAdStyle = "flickr"; //jPagerBar样式

    var currentAdPageIndex = 1;
    var actionAd = ""; //操作
    var orderByAd = ""; //排序字段
    //jQuery-ajax获取JSON数据
    function TurnToAdPage(pageIndex) {
        currentAdPageIndex = pageIndex;
        var Title = $.trim($("#CustNameAd").val());
        var OrderNo = $.trim($("#CustNoAd").val());
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SellManager/SelectAdversary.ashx', //目标地址
            cache: false,
            data: "pageIndex=" + pageIndex + "&pageAdCount=" + pageAdCount + "&actionAd=getinfo&orderByAd=" + orderByAd +
            "&Title=" + escape(Title) + "&orderNo=" + escape(OrderNo), //数据
            beforeSend: function() { $("#pageAdList_Pager").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#offerDataList tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item.ID != "") {
                        var OrderNo = item.CustNo;
                        var Title = item.CustName;
                   
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
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" id=\"radioSellAd_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"fnSelectSellAd(" + item.ID + ");\" name=\"radioAdv\"/>" + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.CustNo + "\">" + OrderNo + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.CustName + "\">" + Title + "</a></td>" +
                        "<td height='22' align='center'>" + item.TypeName + "</td>" +
                        "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                        "<td height='22' align='center'>" + item.CreatDate + "</td>").appendTo($("#offerDataList tbody"));
                    }
                });
                //页码
                ShowPageBar("pageAdList_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerAdStyle
                    , mark: "pageAdDataList1Mark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageAdCount,
                    currentPageIndex: pageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "TurnToAdPage({pageindex});return false;"}//[attr]
                    );
                totalAdRecord = msg.totalCount;
                $("#ShowAdPageCount").val(pageAdCount);
                ShowTotalPage(msg.totalCount, pageAdCount, pageIndex, $("#pageAdcount"));
                $("#AdToPage").val(pageIndex);
            },
            error: function() { },
            complete: function() { $("#pageAdList_Pager").show(); offerDataList("offerDataList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
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
    function ChangeAdPageCountIndex(newPageCount, newPageIndex) {
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
            if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalAdRecord - 1) / newPageCount) + 1) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
                return false;
            }
            else {
                this.pageAdCount = parseInt(newPageCount);
                TurnToAdPage(parseInt(newPageIndex));
            }
        }
    }
    //排序
    function OrderAdBy(orderColum, orderTip) {
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
        orderByAd = orderColum + "_" + ordering;
        TurnToAdPage(1);
    }


    function closeAddiv() {
        obj = document.getElementById("divPreventReclick");
        //隐藏遮挡的DIV
        if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
        document.getElementById("divSellAdSelect").style.display = "none";
    }
</script>

