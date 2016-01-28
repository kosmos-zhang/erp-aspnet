<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProviderLinkManSelect.ascx.cs"
    Inherits="UserControl_ProviderLinkManSelect" %>
<div id="divProviderLinkManhhh" style="display: none">
    <iframe id="aProviderLinkMan" style="width: 1000px; z-index: 1000; position: absolute;
        display: block; top: 30%; left: 40%; margin: 5px 0 0 -400px;"></iframe>
    <!--提示信息弹出详情start-->
    <div id="divProviderLinkMan" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 1000px; z-index: 1001; position: absolute; display: block;
        top: 30%; left: 40%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeProviderLinkMandiv()" style="text-align: right; cursor: pointer">
                        <img src="../../../images/Button/Bottom_btn_close.jpg" /></a> <a onclick="clearProviderLinkMandiv()"
                            id="clearProviderLinkMan" style="text-align: right; cursor: pointer;">
                            <img src="../../../images/Button/Bottom_btn_del.jpg" /></a>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1ProviderLinkMan"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            客户名称<span id="Span12" class="orderTip"></span></div>
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
                                <div id="pagePlancount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerProviderLinkMan" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divProviderLinkManSelect">
                                    <input name="text" type="text" id="Text2Plan" style="display: none" />
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountProviderLinkMan" style="width: 22px" />条
                                    转到第
                                    <input name="text" type="text" id="ToPagePlanProviderLinkMan" style="width: 35px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexPlan($('#ShowPageCountProviderLinkMan').val(),$('#ToPagePlanProviderLinkMan').val());" />
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


    var popProviderLinkManSelectObj = new Object();
    popProviderLinkManSelectObj.InputObj = null;
    //popProviderLinkManSelectObj.ProviderID= null;

    var pageCountPurOrder = 10; //每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr"; //jPagerBar样式
    var currentPageIndex = 1;
    var orderBy = ""; //排序字段

    var actionPurchaseOrder = ""; //操作

    popProviderLinkManSelectObj.ShowList = function(objInput, CustNo) {
        openRotoscopingDiv(false, 'divPBackShadow', 'PBackShadowIframe');
        popProviderLinkManSelectObj.InputObj = objInput;
        popProviderLinkManSelectObj.CustNo = CustNo;
        document.getElementById('divProviderLinkManhhh').style.display = 'block';

        TurnToPagePlanProviderLinkMan(1);
    }
    function closeProviderLinkMandiv() {

        document.getElementById("divProviderLinkManhhh").style.display = "none";
        closeRotoscopingDiv(false, 'divPBackShadow');
    }

    function clearProviderLinkMandiv() {
        document.getElementById("UsertxtLinkManName").value = "";
        document.getElementById("HidLinkManID").value = "";
        closeProviderLinkMandiv();
    
    }
    //jQuery-ajax获取JSON数据
    function TurnToPagePlanProviderLinkMan(pageIndexProvider) {

        currentPageIndexProvider = pageIndexProvider;

        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/PurchaseManager/ProviderLinkManSelect.ashx', //目标地址
            cache: false,
            data: "pageIndexPurchasePlanSelectUC=" + pageIndexProvider + "&pageCountPurchasePlanSelectUC=" + pageCountPurOrder + "&CustNo=" + popProviderLinkManSelectObj.CustNo + "&actionPurchasePlanSelectUC=" + actionPurchaseOrder + "&orderbyPurchasePlanSelectUC=" + orderBy + "", //数据
            beforeSend: function() { AddPop(); $("#pageDataList1_PagerProviderLinkMan").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataList1ProviderLinkMan tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item.ID != "") {
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" name=\"radioTech\" id=\"radioTech_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"FillFromProviderLink('" + item.ID + "','" + item.LinkManName + "');\" />" + "</td>" +
                    "<td height='22' align='center'>" + item.LinkManName + "</td>").appendTo($("#pageDataList1ProviderLinkMan tbody"));
                    }
                });
                //页码
                ShowPageBar("pageDataList1_PagerProviderLinkMan", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1MarkProvider",

                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCountPurOrder, currentPageIndex: pageIndexProvider, noRecordTip: "没有符合条件的记录", preWord: "上页", nextWord: "下页", First: "首页", End: "末页",
                    onclick: "TurnToPagePlanProviderLinkMan({pageindex});return false;"}//[attr]
                    );

                totalRecordProvider = msg.totalCount;
                // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                document.getElementById("Text2Plan").value = msg.totalCount;
                $("#ShowPageCountProviderLinkMan").val(pageCountPurOrder);
                ShowTotalPage(msg.totalCount, pageCountPurOrder, pageIndexProvider, $("#pagePlancount"));
                $("#ToPagePlanProviderLinkMan").val(pageIndexProvider);
            },
            error: function() { },
            complete: function() {
                hidePopup();
                $("#pageDataList1_PagerProviderLinkMan").show(); IfshowOrderSelect(document.getElementById("Text2Plan").value); pageDataList1("pageDataList1ProviderLinkMan", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
            } //接收数据完毕
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

    function Fun_Search_Provider(aa) {
        search = "1";
        TurnToPage(1);
    }
    function IfshowOrderSelect(count) {
        if (count == "0") {
            document.getElementById("divProviderLinkManSelect").style.display = "none";
            document.getElementById("pagePlancount").style.display = "none";
        }
        else {
            document.getElementById("divProviderLinkManSelect").style.display = "";
            document.getElementById("pagePlancount").style.display = "";
        }
    }

    function SelectDeptProvider(retval) {
        alert(retval);
    }

    //改变每页记录数及跳至页数
    function ChangePageCountIndexPlan(newPageCountProvider, newPageIndexProvider) {
        if (!IsZint(newPageCountProvider)) {
            popMsgObj.ShowMsg('显示条数格式不对，必须输入正整数！');
            return;
        }
        if (!IsZint(newPageIndexProvider)) {
            popMsgObj.ShowMsg('跳转页数格式不对，必须输入正整数！');
            return;
        }
        if (newPageCountProvider <= 0 || newPageIndexProvider <= 0 || newPageIndexProvider > ((totalRecordProvider - 1) / newPageCountProvider) + 1) {
            //            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            popMsgObj.ShowMsg('转到页数超出查询范围！');
            return;
        }
        else {
            ifshow = "0";
            this.pageCountPurOrder = parseInt(newPageCountProvider);
            TurnToPagePlanProviderLinkMan(parseInt(newPageIndexProvider));
        }
    }
    
 

</script>

