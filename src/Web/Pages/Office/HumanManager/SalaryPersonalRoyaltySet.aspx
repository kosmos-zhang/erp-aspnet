<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryPersonalRoyaltySet.aspx.cs"
    Inherits="Pages_Office_HumanManager_SalaryPersonalRoyaltySet" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>个人提成设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/office/HumanManager/SalaryPersonalRoyaltySet.js" type="text/javascript"></script>

    <style type="text/css">
        #tblMain
        {
            margin-top: 0px;
            margin-left: 0px;
            background-color: #F0f0f0;
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
        }
        .errorMsg
        {
            filter: progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true);
            position: absolute;
            top: 240px;
            left: 450px;
            border-width: 1pt;
            border-color: #666666;
            border-style: solid;
            width: 290px;
            display: none; 9margin-top:10px;z-index:21;}</style>
</head>
<body>
    <form id="frmMain" runat="server">
    <input id="Hdflag" type="hidden" value="0" />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="tblMain">
        <tr>
            <td valign="top" colspan="2">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr height="20" align="right" bgcolor="#f0f0f0">
            <td colspan='2' width='100%'>
                &nbsp; <a href="SalaryCompanyRoyaltySet.aspx?ModuleID=2011701" style="text-decoration: none;
                    color: Blue">公司业务提成</a>&nbsp;&nbsp; <a href="SalaryDepatmentRoyaltySet.aspx?ModuleID=2011701"
                        style="text-decoration: none; color: Blue">部门业务提成</a>&nbsp; &nbsp; <a href="SalaryPersonalRoyaltySet.aspx?ModuleID=2011701"
                            style="text-decoration: none; color: Blue">个人业务提成</a>&nbsp; &nbsp; <a href="SalaryPiecework.aspx?ModuleID=2011701"
                                style="text-decoration: none; color: Blue">计件工资</a>&nbsp; &nbsp;
                <a href="SalaryTime.aspx?ModuleID=2011701" style="text-decoration: none; color: Blue">
                    计时工资</a> &nbsp; &nbsp; <a href="SalaryCommission.aspx?ModuleID=2011701" style="text-decoration: none;
                        color: Blue">产品单品提成</a> &nbsp; &nbsp; <a href="PerformanceRoyaltyBase.aspx?ModuleID=2011701"
                            style="text-decoration: none; color: Blue">绩效薪资设置</a>&nbsp; &nbsp; <a href="SalaryPerformanceRoyaltySet.aspx?ModuleID=2011701"
                                style="text-decoration: none; color: Blue">绩效系数设置</a>&nbsp;
            </td>
        </tr>
        <tr>
            <td height="30" align="center" colspan="2" class="Title">
                个人业务提成设置
            </td>
        </tr>
        <tr>
            <td height="40" valign="top" colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="30" class="tdColInput">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img src="../../../images/Button/Main_btn_add.jpg" alt="添加" id="btnAdd" visible="false"
                                            style="cursor: hand" onclick="DoAdd();" runat="server" />
                                        <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false"
                                            alt="保存" id="btnSave" style="cursor: hand" onclick="DoSave();" />
                                        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDelete"
                                            runat="server" onclick="DoDelete()" style='cursor: hand;' />
                                        <%--  <input id="Button1" type="button" value="button"   onclick="getMoney()"   />--%>
                                    </td>
                                    <td align="right" class="tdColInput">
                                        <%--<img src="../../../Images/Button/Main_btn_print.jpg" runat="server" visible="true" alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand"   />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" align="center">
                    <tr>
                        <td width="10%" align="right">
                            员工姓名
                        </td>
                        <td width="20%" align="left">
                            <input type="hidden" id="txtExecutorID" value="" />
                            <input type="hidden" id="HidDeptName" value="" />
                            <input type="hidden" id="HidDeptID" value="" />
                            <input id="UserExecutor" type="text" class="tdinput" size="19" readonly="readonly"
                                onclick="alertdiv('UserExecutor,txtExecutorID');" />
                        </td>
                        <td width="10%" align="right">
                        
                            客户姓名
                          
                        </td>
                        <td width="15%">
                        <input name="CustID" id="CustID" class="tdinput" size="19" readonly="readonly"
                                type="text" readonly="readonly" onclick="fnSelectCustInfo()" /><input id="HidtxtCus"
                                    type="hidden" />
                            <input id="CustName" type="hidden" /></td>
                        <td align="left">
                          <img alt="检索" id="btnSearch" runat="server" src="../../../images/Button/Bottom_btn_search.jpg"
                                style='cursor: hand;' onclick='DoSearch();' visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow-y: auto; width: 100%; line-height: 14pt; letter-spacing: 0.2em;
                    overflow-x: auto; vertical-align: top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table1" style="height: 500px;
                        vertical-align: top">
                        <tr>
                            <td colspan="2" valign="top">
                                <div id="divSalaryList" runat="server">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="10">
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="10">
            </td>
        </tr>
    </table>
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span><span id="spanMsg" class="errorMsg"></span>
    <uc1:Message ID="msgError" runat="server" />
    <!--提示信息弹出详情start-->
    <a name="pageCustDataList1Mark"></a>
    <div id="divSellModuleCustSelect" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 21; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;" class="checktable">
        <table width="99%" border="0" align="center" cellpadding="0" id="Table2" cellspacing="0"
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

    <script language="javascript">
    var popSellCustObj = new Object();
    popSellCustObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户


    popSellCustObj.ShowList = function(model,k) {
        if (model != null && typeof (model) != "undefined") {
            popSellCustObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
        $("#CustNoUC").val('');
        $("#CustNameUC").val('');
        document.getElementById('divSellModuleCustSelect').style.display = 'block';
        SellCustTurnToPage(currentSellCustPageIndex,k);
    }

    var pageSellCustCount = 10; //每页计数
    var totalSellCustRecord = 0;
    var pagerSellCustStyle = "flickr"; //jPagerBar样式

    var currentSellCustPageIndex = 1;
    var actionSellCust = ""; //操作
    var orderSellCustBy = ""; //排序字段
    //jQuery-ajax获取JSON数据
    function SellCustTurnToPage(pageIndex,k) {
        var Title = $.trim($("#CustNameUC").val());
        var OrderNo = $.trim($("#CustNoUC").val());
        var model = popSellCustObj.SearchModel;
        currentSellCustPageIndex = pageIndex;
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/HumanManager/SellModuleSelectCustUC.ashx', //目标地址
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
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" name=\"radioCust\" id=\"radioCust_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"fnSelectCust(" + item.ID + ","+k+");\" />" + "</td>" +
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
    function ClearSellModuCustdiv()
    {
      document.getElementById("CustID").value="";
      document.getElementById("HidtxtCus").value="";
      closeSellModuCustdiv();
    }
    </script>

    </form>
</body>
</html>
