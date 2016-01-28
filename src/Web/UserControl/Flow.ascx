<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Flow.ascx.cs" Inherits="UserControl_Flow" %>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divFlow">
        <table width="100%">
        </table>
        <table style="margin-left: 75px" width="800px" border="0" align="center" cellpadding="0"
            cellspacing="1" id="pageDataList1" bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择<input type="checkbox" id="btnAll" name="btnAll" onclick="OptionCheckAll();" />
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('FlowNo','SpanFlowNo');return false;">
                            流程编号<span id="SpanFlowNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('FlowName','SpanFlowName');return false;">
                            流程名称<span id="SpanFlowName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('UsedStatus','SpanUsedStatus');return false;">
                            流程使用状态<span id="SpanUsedStatus" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('UsedStatus','SpanDeptID');return false;">
                            所属部门<span id="SpanDeptID" class="orderTip"></span></div>
                    </th>
                    <%--           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            步骤序号<span id="Span5" class="orderTip"></span></div>
                    </th>
                      <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                           步骤描述<span id="Span6" class="orderTip"></span></div>
                    </th>
                      <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            步骤处理人<span id="Span7" class="orderTip"></span></div>
                    </th>--%>
                </tr>
            </tbody>
        </table>
        <input id="hfModuleID" type="hidden" />
        <br />
        <table width="800px" style="margin-left: 75px" border="0" align="center" cellpadding="0"
            cellspacing="1" bgcolor="#999999" class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                <div id="pagecount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpage">
                                    <input name="text" type="text" id="Text2" style="display: none" />
                                    <span id="pageDataList1_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCount" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
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
    var popTechObj = new Object();
    popTechObj.InputObj = null;


    popTechObj.ShowList = function() {
        TurnToPage(currentPageIndex);
    }

    $(document).ready(function() {

        requestobj = GetRequest();
        recordnoparamcode = requestobj['TypeCodeflag'];
        recordnoparamflag = requestobj['TypeFlag'];
        document.getElementById('hfModuleID').Value = requestobj['ModuleID'];
        if (typeof (recordnoparamcode) != "undefined") {
            document.getElementById('hd_typecode').value = recordnoparamcode;
            document.getElementById('hd_typeflag').value = recordnoparamflag;

            document.getElementById('sel_status').value = requestobj['UseStatusflag'];
            document.getElementById("sel_status").disabled = false;

            TurnToPage(1);
        }
    });
    var pageCount = 10; //每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr"; //jPagerBar样式

    var currentPageIndex = 1;
    var action = ""; //操作
    var orderBy = ""; //排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex) {
        document.getElementById("btnAll").checked = false;
        currentPageIndex = pageIndex;
        var TypeCode = document.getElementById('hd_typecode').value;
        var TypeFlag = document.getElementById('hd_typeflag').value;
        var UseStatus = document.getElementById('sel_status').value;
        var FlowName = document.getElementById('txtFlowName').value;
        if (!CheckSpecialWord(FlowName)) {
            popMsgObj.ShowMsg("流程名称包含特殊字符！");
            return;
        }
        var MID = document.getElementById('hfModuleID').Value;
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SystemManager/ApprovalFlowSetList.ashx?TypeCode=' + TypeCode
                    + '&TypeFlag=' + TypeFlag
                    + '&orderBy=' + orderBy
                    + '&FlowName=' + FlowName
                    + '&UseStatus=' + UseStatus + '', //目标地址
            cache: false,
            data: "pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&action=" + action + "&TypeCode=" + escape(TypeCode) + "", //数据
            beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前           
            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataList1 tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    var temp = item.FlowNo + "|" + item.UsedStatus;
                    if (item.FlowNo != null && item.FlowNo != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1' name='Checkbox1'  value=" + temp + "  type='checkbox'/>" + "</td>" +
                       "<td height='22' align='center'><a href=\"ApprovalFlowSetAdd.aspx?FlowNo=" + item.FlowNo + "&typeflag=" + TypeFlag + "&typecode=" + TypeCode + "&usestatus=" + UseStatus + "&ModuleID=" + MID + "\">" + item.FlowNo + "</a></td>" +
                        "<td height='22' align='center'>" + item.FlowName + "</td>" +
                        "<td height='22' align='center'>" + item.UsedStatus + "</td>" +
                         "<td height='22' align='center'>" + item.DeptID + "</td>").appendTo($("#pageDataList1 tbody"));
                });
                //页码
                //页码
                ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                       "<%= Request.Url.AbsolutePath %>", //[url]
                        {style: pagerStyle, mark: "pageDataList1Mark",
                        totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                        onclick: "TurnToPage({pageindex});return false;"}//[attr]
                        );
                totalRecord = msg.totalCount;
                // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                document.getElementById('Text2').value = msg.totalCount;
                $("#ShowPageCount").val(pageCount);
                ShowTotalPage(msg.totalCount, pageCount, pageIndex);
                $("#ToPage").val(pageIndex);
                ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
                //document.getElementById('tdResult').style.display='block';
            },
            error: function() { },
            complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.getElementById('Text2').value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }
    //table行颜色
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

    function delflowstep() {
        var action = "Del";
        var c = window.confirm("确认删除此流程吗？")
        if (c == true) {
            var ck = document.getElementsByName("Checkbox1");
            var x = Array();
            var ck2 = "";
            var str = "";
            for (var i = 0; i < ck.length; i++) {
                if (ck[i].checked) {
                    ck2 += ck[i].value + ',';
                }
            }
            var str = ck2.substring(0, ck2.length - 1);
            if (str.length - 1 < 1)
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "删除流程至少要选择一项！");
            else {
                $.ajax({
                    type: "POST", //用POST方式传输
                    dataType: "json", //数据格式:JSON
                    url: '../../../Handler/Office/SystemManager/ApprovalFlowSetAdd.ashx?str=' + str + '&action=' + action + '', //目标地址
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {
                    },
                    error: function() {
                        popMsgObj.ShowMsg('请求发生错误');

                    },
                    success: function(data) {
                        if (data.sta == 1) {
                            popMsgObj.ShowMsg(data.info);
                            Fun_Search_Flow();
                        }
                        else {
                            popMsgObj.ShowMsg(data.info);

                        }
                    }
                });
            }
        }
    }
    function Fun_Search_Flow(aa) {
        search = "1";
        TurnToPage(1);
    }
    function Ifshow(count) {
        if (count == "0") {
            document.getElementById('divpage').style.display = "none";
            document.getElementById('pagecount').style.display = "none";
        }
        else {
            document.getElementById('divpage').style.display = "block";
            document.getElementById('pagecount').style.display = "block";
        }
    }

    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount, newPageIndex) {
        if (!IsZint(newPageCount)) {
            popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
            return;
        }
        if (!IsZint(newPageIndex)) {
            popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
            return;
        }
        if (newPageCount <= 0) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "显示页数超出显示范围！");
            return false;
        }
        if (newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            this.pageCount = parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
            document.getElementById("btnAll").checked = false;
        }
    }
    //排序
    function OrderBy(orderColum, orderTip) {
        var ordering = "a";
        //var orderTipDOM = $("#"+orderTip);
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
        orderBy = orderColum + "_" + ordering;
        TurnToPage(1);
    }
    function OptionCheckAll() {

        if (document.getElementById("btnAll").checked) {
            var ck = document.getElementsByName("Checkbox1");
            for (var i = 0; i < ck.length; i++) {
                ck[i].checked = true;
            }
        }
        else if (!document.getElementById("btnAll").checked) {
            var ck = document.getElementsByName("Checkbox1");
            for (var i = 0; i < ck.length; i++) {
                ck[i].checked = false;
            }
        }
    }


</script>

