<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProviderSelect.ascx.cs"
    Inherits="UserControl_ProviderSelect" %>
<style type="text/css">
    .tdinput
    {
        border-width: 0pt;
        background-color: #ffffff;
        height: 21px;
        margin-left: 2px;
    }
</style>
<div id="divProviderInfohhh" style="display: none">
    <a name="pageDataList1MarkProvider"></a>
    <!--提示信息弹出详情start-->
    <div id="divProviderInfo" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 750px; z-index: 1001; position: absolute; display: block; top: 40%; left: 50%;
        margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeProviderdiv()" style="text-align: right; cursor: pointer">
                        <img src="../../../images/Button/Bottom_btn_close.jpg" /></a><a onclick="clearProviderdiv()"
                            style="text-align: right; cursor: pointer"><img src="../../../images/Button/Bottom_btn_del.jpg" /></a>
                </td>
            </tr>
        </table>
        <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClickPurchaseProvider'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('PurchaseProvidersearchtable','searchClickPurchaseProvider')" /></div>
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
                    <table width="100%" border="0" align="left" cellpadding="0" id="PurchaseProvidersearchtable"
                        cellspacing="0" bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                    class="table">
                                    <tr class="table-item">
                                        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                            供应商编号
                                        </td>
                                        <td width="40%" bgcolor="#FFFFFF">
                                            <input type="text" id="txt_ProviderNo" class="tdinput" specialworkcheck="供应商编号" />
                                        </td>
                                        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                            供应商名称
                                        </td>
                                        <td width="40%" bgcolor="#FFFFFF">
                                            <input type="text" id="txt_ProviderName" class="tdinput" specialworkcheck="供应商名称" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center" bgcolor="#FFFFFF">
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                onclick='Fun_Search_Providers()' id="btn_search" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1Provider"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            供应商编号<span id="Span2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            供应商名称<span id="oC2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            交货方式名称<span id="Span4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            运送方式<span id="Span6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            结算方式<span id="Span8" class="orderTip"></span></div>
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
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                <div id="providerPageCount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerProvider" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divPageSellBillChoose">
                                    <input name="text" type="text" id="Text2Provider" style="display: none" />
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountProvider" style="width: 22px" />条
                                    转到第
                                    <input name="text" type="text" id="ToPageProvider" style="width: 35px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexProvider($('#ShowPageCountProvider').val(),$('#ToPageProvider').val());" />
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
    var pageCount = 10; //每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr"; //jPagerBar样式
    var currentPageIndex = 1;
    var orderBy = ""; //排序字段

    var popProviderObj = new Object();
    popProviderObj.InputObj = null;


    popProviderObj.ShowList = function(objInput) {
        popProviderObj.InputObj = objInput;
        document.getElementById('divProviderInfohhh').style.display = 'block';
    }


    var popProviderIDObj = new Object();
    popProviderIDObj.InputObjID = null;
    popProviderIDObj.InputObjName = null;
    popProviderIDObj.ShowProviderList = function(objProviderID, objProviderName) {
        popProviderIDObj.InputObjID = objProviderID;
        popProviderIDObj.InputObjName = objProviderName;
        document.getElementById('divProviderInfohhh').style.display = 'block';
    }

    $(document).ready(function() {
        TurnToPageProvider(1);
    });

    var pageCountProvider = 10; //每页计数
    var totalRecordProvider = 0;
    var pagerStyleProvider = "flickr"; //jPagerBar样式

    var currentPageIndexProvider = 1;
    var actionProvider = ""; //操作
    var orderByProvider = ""; //排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageProvider(pageIndexProvider) {
        currentPageIndexProvider = pageIndexProvider;
        var RetVal = CheckSpecialWords();
        if (RetVal != "") {
            alert(RetVal + "不能含有特殊字符");
            return;
        }
        var ProviderID = "";
        var ProviderNo = $("#txt_ProviderNo").val().Trim();
        var ProviderName = $("#txt_ProviderName").val().Trim();

        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/PurchaseManager/ProviderSelect.ashx', //目标地址
            cache: false,
            data: "pageIndexProvider=" + pageIndexProvider
                        + "&pageCountProvider=" + pageCountProvider
                        + "&actionProvider=" + actionProvider
                        + "&orderbyProvider=" + orderByProvider
                        + "&ProviderID=" + escape(ProviderID)
                        + "&ProviderNo=" + escape(ProviderNo)
                        + "&ProviderName=" + escape(ProviderName) + "", //数据
            beforeSend: function() { AddPop(); $("#pageDataList1_PagerProvider").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataList1Provider tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ProviderID != null && item.ProviderID != "")
                        var ProviderName = item.ProviderName;
                    if (ProviderName != null) {
                        if (ProviderName.length > 6) {
                            ProviderName = ProviderName.substring(0, 6) + '...';
                        }
                    }
                    $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center'>" + " <input type=\"radio\" name=\"radioTech\" id=\"radioTech_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"FillProvider('" + item.ProviderID + "','" + item.ProviderNo + "','" + item.ProviderName + "','" + item.TakeType + "','" + item.TakeTypeName + "','" + item.CarryType + "','" + item.CarryTypeName + "','" + item.PayType + "','" + item.PayTypeName + "');\" />" + "</td>" +
                        "<td height='22' align='center'>" + item.ProviderNo + "</td>" +
                        "<td height='22' align='center'><span title=\"" + item.ProviderName + "\">" + ProviderName + "</td>" +
                        "<td height='22' align='center'>" + item.TakeTypeName + "</td>" +
                        "<td height='22' align='center'>" + item.CarryTypeName + "</td>" +
                        "<td height='22' align='center'>" + item.PayTypeName + "</td>"
                        ).appendTo($("#pageDataList1Provider tbody"));
                });
                //页码
                ShowPageBar("pageDataList1_PagerProvider", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1MarkProvider",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndexProvider, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPageProvider({pageindex});return false;"}//[attr]
                    );

                totalRecordProvider = msg.totalCount;
                document.getElementById("Text2Provider").value = msg.totalCount;
                $("#ShowPageCountProvider").val(pageCount);
                ShowTotalPage(msg.totalCount, pageCountProvider, pageIndexProvider, $("#providerPageCount"));
                $("#ToPageProvider").val(pageIndexProvider);
            },
            error: function() { },
            complete: function() { hidePopup(); $("#pageDataList1_PagerProvider").show(); IfshowProvider(document.getElementById("Text2Provider").value); pageDataList1("pageDataList1Provider", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
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

    function Fun_Search_Providers() {
        search = "1";
        TurnToPageProvider(1);
    }

    function IfshowProvider(count) {
        if (count == "0") {
            document.getElementById("divPageSellBillChoose").style.display = "none";
            document.getElementById("providerPageCount").style.display = "none";
        }
        else {
            document.getElementById("divPageSellBillChoose").style.display = "block";
            document.getElementById("providerPageCount").style.display = "block";
        }
    }

    function SelectDeptProvider(retval) {
        alert(retval);
    }

    //改变每页记录数及跳至页数
    function ChangePageCountIndexProvider(newPageCountProvider, newPageIndexProvider) {
        pageCount = newPageCountProvider;
        if (newPageCountProvider <= 0 || newPageIndexProvider <= 0 || newPageIndexProvider > ((totalRecordProvider - 1) / newPageCountProvider) + 1) {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else {
            this.pageCountProvider = parseInt(newPageCountProvider);
            TurnToPageProvider(parseInt(newPageIndexProvider));
        }
    }

    function Create_Div(img, img1, bool) {
        FormStr = "<table width=100% height='104' border='0' cellpadding=0 cellspacing=0 bgcolor=#FFFFFF>"
        FormStr += "<tr>"
        FormStr += "<td width=90%  bgcolor=#3F6C96>&nbsp;</td>"
        FormStr += "<td width=10% height=20 bgcolor=#3F6C96>"
        if (bool) {
            FormStr += "<img src='" + img + "' style='cursor:hand;display:block;' id='CloseImg' onClick=document.getElementById('Forms').style.display='none';>"
        }
        FormStr += "</td></tr><tr><td height='1' colspan='2' background='../../../Images/Pic/bg_03.gif'></td></tr><tr><td valign=top height=104 id='FormsContent' colspan=2 align=center>"
        FormStr += "<table width=100% border=0 cellspacing=0 cellpadding=0>"
        FormStr += "<tr>"
        FormStr += "<td width=25% align='center' valign=top style='padding-top:20px;'>"
        FormStr += "<img name=exit src='" + img1 + "' border=0></td>";
        FormStr += "<td width=75% height=50 id='FormContent' style='padding-left:5pt;padding-top:20px;line-height:17pt;' valign=top></td>"
        FormStr += "</tr></table>"
        FormStr += "</td></tr></table>"
        return FormStr;
    } 
</script>

