<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SellModuleSelectCurrency.ascx.cs"
    Inherits="UserControl_SellModuleSelectCurrency" %>
<div id="sellModuleCurrency">
    <!--提示信息弹出详情start-->
    <a name="pageCurrencyDataList1Mark"></a>
    <div id="divSellModuleCurrencySelect" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 21; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="6" bgcolor="#E7E7E7" align="left" style="width: 50%;">
                 <img src="../../../Images/Button/Bottom_btn_close.jpg" alt="关闭" onclick="closeSellModuCurrencydiv()" />
                                
                                
                                <img src="../../../Images/Button/Main_btn_qk.jpg" alt="清空" onclick="clearSellModuCurrency()" />
                
                   
                </td>
            </tr>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="sellCurrList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellCurrOrderBy('CurrencyName','oSellCurr');return false;">
                            币种名称<span id="oSellCurr" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellCurrOrderBy('CurrencySymbol','oSCurr1');return false;">
                            币符<span id="oSCurr1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellCurrOrderBy('ExchangeRate','oSCurr2');return false;">
                            汇率<span id="oSCurr2" class="orderTip"></span></div>
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
                                <div id="pageSellCurrency">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="CurrList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divCurrencyPage">
                                    <span id="pageCurrencyList_Total"></span>每页显示
                                    <input name="text" type="text" style="width: 30px;" id="ShowCurrPageCount" maxlength="3" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 30px;" id="ToCurrPage" maxlength="7" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeCurrPageCountIndex($('#ShowCurrPageCount').val(),$('#ToCurrPage').val());" />
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
    var popSellCurrObj = new Object();
    popSellCurrObj.InputObj1 = null;
    popSellCurrObj.InputObj2 = null;

    popSellCurrObj.ShowList = function(objInput1, objInput2) {
        popSellCurrObj.InputObj1 = objInput1;
        popSellCurrObj.InputObj2 = objInput2;
        ShowPreventReclickDiv();
        document.getElementById('divSellModuleCurrencySelect').style.display = 'block';
        CurrencyTurnToPage(currentCurrencyPageIndex, objInput1, objInput2);
    }

    var pageCurrencyCount = 10; //每页计数
    var totalCurrRecord = 0;
    var pagerCurrStyle = "flickr"; //jPagerBar样式

    var currentCurrencyPageIndex = 1;
    var actionCurr = ""; //操作
    var orderCurr = ""; //排序字段
    //jQuery-ajax获取JSON数据
    function CurrencyTurnToPage(pageIndex, objInput1, objInput2) {
        currentCurrencyPageIndex = pageIndex;
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SellManager/SellModuleSelectCurrency.ashx', //目标地址
            cache: false,
            data: "pageIndex=" + pageIndex + "&pageCurrencyCount=" + pageCurrencyCount + "&orderby=" + orderCurr, //数据
            beforeSend: function() { $("#CurrList_Pager").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#sellCurrList tbody").find("tr.newrowCurr").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item.ID != "")
                        $("<tr class='newrowCurr'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" name=\"radioCurr_\" id=\"radioCurr_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"fnSelectCurr(" + item.ID + ",'" + item.CurrencyName + "','" + item.CurrencySymbol + "','" + item.ExchangeRate + "','" + objInput1 + "','" + objInput2 + "');\" />" + "</td>" +
                         "<td height='22' align='center'>" + item.CurrencyName + "</td>" +
                         "<td height='22' align='center'>" + item.CurrencySymbol + "</td>" +
                         "<td height='22' align='center'>" + item.ExchangeRate + "</td>").appendTo($("#sellCurrList tbody"));
                });
                //页码
                ShowPageBar("CurrList_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerCurrStyle, mark: "pageCurrencyDataList1Mark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageCurrencyCount,
                    currentPageIndex: pageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "CurrencyTurnToPage({pageindex});return false;"}//[attr]
                    );
                totalCurrRecord = msg.totalCount;
                $("#ShowCurrPageCount").val(pageCurrencyCount);
                ShowTotalPage(msg.totalCount, pageCurrencyCount, pageIndex, $("#pageSellCurrency"));
                $("#ToCurrPage").val(pageIndex);
            },
            error: function() { },
            complete: function() { $("#CurrList_Pager").show(); pageCurrDataList1("sellCurrList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }
    //table行颜色
    function pageCurrDataList1(o, a, b, c, d) {
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
    function ChangeCurrPageCountIndex(newPageCount, newPageIndex) {
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
            if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalCurrRecord - 1) / newPageCount) + 1) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
                return false;
            }
            else {
                this.pageCurrencyCount = parseInt(newPageCount);
                CurrencyTurnToPage(parseInt(newPageIndex));
            }
        }
    }
    //排序
    function SellCurrOrderBy(orderColum, orderTip) {
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
        orderCurr = orderColum + "_" + ordering;
        CurrencyTurnToPage(1);
    }

    function closeSellModuCurrencydiv() {
        obj = document.getElementById("divPreventReclick");
        //隐藏遮挡的DIV
        if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
        document.getElementById("divSellModuleCurrencySelect").style.display = "none";
    }

    function clearSellModuCurrency() {
        $("#" + popSellCurrObj.InputObj1).val('');
        $("#" + popSellCurrObj.InputObj1).removeAttr("title");
        $("#" + popSellCurrObj.InputObj2).val('');
        closeSellModuCurrencydiv();
    }
    
    function fnSelectCurr(ID, CurrencyName, CurrencySymbol, ExchangeRate, objInput1, objInput2) {
        $("#" + objInput1).val(CurrencyName);
        $("#" + objInput1).attr("title", ID);
        $("#" + objInput2).val(ExchangeRate);
        try{fnGetPriceByRate();
        }catch(e){}
        closeSellModuCurrencydiv();
    }
</script>

