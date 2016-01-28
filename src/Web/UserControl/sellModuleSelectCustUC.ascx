<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sellModuleSelectCustUC.ascx.cs"
    Inherits="UserControl_sellModuleSelectCustUC" %>
<div id="sellModuleCust">
    <!--提示信息弹出详情start-->
    <a name="pageCustDataList1Mark"></a>
    <div id="divSellModuleCustSelect" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 21; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="99%" border="0" align="center" cellpadding="0" id="Table1" cellspacing="0"
            bgcolor="#CCCCCC">
            <tr>
                <td bgcolor="#FFFFFF">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                        class="table">
                        <tr class="table-item">
                            <td height="25" colspan="6" bgcolor="#E7E7E7" align="left" style="width: 50%;">
                             <img src="../../../Images/Button/Bottom_btn_close.jpg" alt="关闭" onclick="closeSellModuCustdiv()" />
                                
                                
                                <img src="../../../Images/Button/Main_btn_qk.jpg" alt="清空" onclick="ClearSellModuCustdiv()" />
                            
                               
                            </td>
                        </tr>
                        <tr class="table-item">
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                客户编号
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="CustNoUC" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                客户名称
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="CustNameUC" class="tdinput" maxlength="50" type="text" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                            </td>
                            <td width="21%" bgcolor="#FFFFFF">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" bgcolor="#FFFFFF">
                                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                    onclick='SellCustTurnToPage(1)' />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="sellCustList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellCustOrderBy('CustNo','oSellCustUC');return false;">
                            客户编号<span id="oSellCustUC" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellCustOrderBy('CustName','oSCustUC1');return false;">
                            客户名称<span id="oSCustUC1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellCustOrderBy('ArtiPerson','oSCustUC2');return false;">
                            法人代表<span id="oSCustUC2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellCustOrderBy('CustNote','oSCustUC3');return false;">
                            客户简介<span id="oSCustUC3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellCustOrderBy('Relation','oSCustUC4');return false;">
                            关系描述<span id="oSCustUC4" class="orderTip"></span></div>
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
                                <div id="pageSellCustcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="sellCustList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSellCustPage">
                                    <span id="pageSellCustList_Total"></span>每页显示
                                    <input name="text" type="text" style="width: 30px;" id="ShowSellCustPageCount" maxlength="3" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 30px;" id="ToSellCustPage" maxlength="7" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeSellCustPageCountIndex($('#ShowSellCustPageCount').val(),$('#ToSellCustPage').val());" />
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

<script language="javascript">
    var popSellCustObj = new Object();
    popSellCustObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popSellCustObj.returnName = false;

    popSellCustObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popSellCustObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
        $("#CustNoUC").val('');
        $("#CustNameUC").val('');
        document.getElementById('divSellModuleCustSelect').style.display = 'block';
        SellCustTurnToPage(currentSellCustPageIndex);
    }

    var pageSellCustCount = 10; //每页计数
    var totalSellCustRecord = 0;
    var pagerSellCustStyle = "flickr"; //jPagerBar样式

    var currentSellCustPageIndex = 1;
    var actionSellCust = ""; //操作
    var orderSellCustBy = ""; //排序字段
    //jQuery-ajax获取JSON数据
    function SellCustTurnToPage(pageIndex) {
        
        if(!CheckCustName())
        {
            return;
        }
        var Title = $.trim($("#CustNameUC").val());
        var OrderNo = $.trim($("#CustNoUC").val());
        var model = popSellCustObj.SearchModel;
        currentSellCustPageIndex = pageIndex;
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SellManager/SellModuleSelectCustUC.ashx', //目标地址
            cache: false,
            data: "pageIndex=" + pageIndex + "&pageSellCustCount=" + pageSellCustCount + "&actionSellCust=getinfo&orderby=" + orderSellCustBy +
            "&Title=" + escape(Title) + "&orderNo=" + escape(OrderNo) + "&model=" + escape(model),
            beforeSend: function() { $("#sellCustList_Pager").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#sellCustList tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item.ID != "") {
                        var OrderNo = item.CustNo;
                        var Title = item.CustName;

                        if (Title != null) {
                            if (Title.length > 15) {
                                Title = Title.substring(0, 15) + '...';
                            }
                        }
                        if (OrderNo != null) {
                            if (OrderNo.length > 20) {
                                OrderNo = OrderNo.substring(0, 20) + '...';
                            }
                        }

                        var clickhandler = "fnSelectCust(" + item.ID + ");";
                        if (popSellCustObj.returnName) {
                            clickhandler = "fnSelectCust(" + item.ID + ",'" + item.CustName + "');";
                        }

                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" name=\"radioCust\" id=\"radioCust_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\""+clickhandler+"\"/>" + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.CustNo + "\">" + OrderNo + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.CustName + "\">" + Title + "</td>" +
                         "<td height='22' align='center'>" + item.ArtiPerson + "</td>" +
                         "<td height='22' align='center'>" + item.CustNote + "</td>" +
                         "<td height='22' align='center'>" + item.Relation + "</td>").appendTo($("#sellCustList tbody"));
                    }
                });
                //页码
                ShowPageBar("sellCustList_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerSellCustStyle, mark: "pageCustDataList1Mark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageSellCustCount,
                    currentPageIndex: pageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "SellCustTurnToPage({pageindex});return false;"}//[attr]
                    );
                totalSellCustRecord = msg.totalCount;
                $("#ShowSellCustPageCount").val(pageSellCustCount);
                ShowTotalPage(msg.totalCount, pageSellCustCount, pageIndex, $("#pageSellCustcount"));
                $("#ToSellCustPage").val(pageIndex);
            },
            error: function() { },
            complete: function() { $("#sellCustList_Pager").show(); pageSellCustDataList1("sellCustList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }
    //table行颜色
    function pageSellCustDataList1(o, a, b, c, d) {
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
    function ChangeSellCustPageCountIndex(newPageCount, newPageIndex) {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;
        $("#checkall").removeAttr("checked")
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
            if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalSellCustRecord - 1) / newPageCount) + 1) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
                return false;
            }
            else {
                this.pageSellCustCount = parseInt(newPageCount);
                SellCustTurnToPage(parseInt(newPageIndex));
            }
        }
    }
    //排序
    function SellCustOrderBy(orderColum, orderTip) {
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
        orderSellCustBy = orderColum + "_" + ordering;
        SellCustTurnToPage(1);
    }

    function closeSellModuCustdiv() {
        obj = document.getElementById("divPreventReclick");
        //隐藏遮挡的DIV
        if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
        document.getElementById("divSellModuleCustSelect").style.display = "none";
    }
    //主表单验证
function CheckCustName()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var txtUcCustNo = document.getElementById('CustNoUC').value;//客户编号
    var txtUcCustName = document.getElementById('CustNameUC').value;//客户名称
    

    if(txtUcCustNo.length>0 && txtUcCustNo.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
	    msgText = msgText + "客户编号输入不正确|";
    }    
    if(txtUcCustName.length>0 && ValidText(txtUcCustName) == false)
    {
        isFlag = false;       
	    msgText = msgText + "客户名称输入不正确|";
    }  
    
    if(!isFlag)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}

</script>

