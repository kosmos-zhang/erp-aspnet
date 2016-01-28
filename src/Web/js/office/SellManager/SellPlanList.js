$(document).ready(function() {
    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
     //GetBillExtAttr('officedba.SellPlan'); 
     IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue'); 
    if (requestObj != null) {
        var isSeacher = requestObj['isSeacher']; //是否从列表页面进入
        if (isSeacher == 'true') {
            $("#OfferNo").val(requestObj['PlanNo']); //销售机会
            $("#Title").val(requestObj['Title']); //主题
            $("#FromType").val(requestObj['PlanType']);      //来源单据类型
            $("#BillStatus").val(requestObj['BillStatus']);      //单据状态
            $("#FlowStatus").val(requestObj['FlowStatus']);      //单据状态  
            
            document.getElementById("GetBillExAttrControl1_SelExtValue").value = requestObj['EFIndex'];//扩展属性select值
            document.getElementById("GetBillExAttrControl1_TxtExtValue").value = requestObj['EFDesc'];//扩展属性文本框值
            
            IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');

            pageCount = parseInt(requestObj['pageCount']); //每页计数
            orderBy = requestObj['orderby']; //排序字段
            TurnToPage(parseInt(requestObj['pageIndex']));
        }
    }
});

//获取url中"?"符后的字串
function GetRequest() {
    var url = location.search;
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    return theRequest;
}

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var currentPageIndex = 1;
var orderBy = ""; //排序字段
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    if (!CheckInput()) {
        return;
    }
    currentPageIndex = pageIndex;


    var action = "getinfo";


    var strUrl = getSearchParams1() + '&pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy) +
               '&action=' + escape(action);
    $("#hiddUrl").val(strUrl);

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/SellManager/SellPlanList.ashx', //计划地址
        cache: false,
        data: strUrl,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    var PlanNo = item.PlanNo;
                    var Title = item.Title;
                   
                    if (Title != null) {
                        if (Title.length > 15) {
                            Title = Title.substring(0, 15) + '...';
                        }
                    }
                    if (PlanNo != null) {
                        if (PlanNo.length > 15) {
                            PlanNo = PlanNo.substring(0, 15) + '...';
                        }
                    }

                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='chk" + i + "' onclick = 'fnUnSelect(this)'  Title='" + item.PlanNo + "'   value='" + item.ID + "'   type='checkbox'/>" + "</td>" +
                    "<td height='22' align='center'><a href='#' title=\"" + item.PlanNo + "\"  onclick=fnOrderInfo('" + item.ID + "')>" + PlanNo + "</a></td>" +
                    "<td height='22' align='center'><span title=\"" + item.Title + "\">" + Title + "</a></td>" +
                    "<td height='22' align='center'>" + item.PlanTypeText + "</td>" +
                    "<td height='22' align='center'>" + item.PlanDate + "</td>" +

                    "<td height='22' align='center'>" + FormatAfterDotNumber(item.MinPlanTotal,precisionLength) + "</td>" +
                    "<td height='22' align='center'>" + FormatAfterDotNumber(item.PlanTotal,precisionLength) + "</td>" +
                    "<td height='22' align='center'>" + item.BillStatusText + "</td>" +
                    "<td height='22' align='center'>" + item.FlowInstanceText + "</td>").appendTo($("#pageDataList1 tbody"));
                }
            });
            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pageSellOffcount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.form1.elements["Text2"].value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });

}

//去除全选按钮
function fnUnSelect(obj) {


    if (!obj.checked) {
        $("#checkall").removeAttr("checked");
        return;
    }

    else {
        //验证明细信息
        var signFrame = findObj("pageDataList1", document);
        var iCount = 0; //明细中总数据数目
        var checkCount = 0; //明细中选择的数据数目
        for (i = 0; i < signFrame.rows.length - 1; i++) {

            iCount = iCount + 1;

            if ($("#chk" + i).attr("checked")) {
                checkCount = checkCount + 1;
            }

        }
        if (checkCount == iCount) {

            $("#checkall").attr("checked", "checked");
        }

    }
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



//构造查询条件的URL参数
function getSearchParams1() {
    var PlanNo = $("#OfferNo").val(); //销售机会
    var Title = $("#Title").val(); //主题
    var PlanType = $("#FromType").val();      //来源单据类型
    var BillStatus = $("#BillStatus").val();     //单据状态
    var FlowStatus = $("#FlowStatus").val(); //审批状 态
       
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
    
    var strUrl = 'PlanNo=' + escape(PlanNo) + '&Title=' + escape(Title) + '&PlanType=' + escape(PlanType)
                +'&BillStatus=' + escape(BillStatus)  + '&FlowStatus=' + escape(FlowStatus) + '&EFIndex=' + escape(EFIndex) + '&EFDesc=' + escape(EFDesc);

    $("#hiddExpOrderNo").val(PlanNo);
    $("#hiddExpTitle").val(Title);
    $("#hiddExpFromType").val(PlanType);
    
    $("#hiddExpBillStatus").val(BillStatus);
    $("#hiddExpFlowStatus").val(FlowStatus);
    
    $("#hidEFIndex").val(EFIndex);
    $("#hidEFDesc").val(EFDesc);

    return strUrl;
}

//构造查询条件的URL参数
function getSearchParams() {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        strUrl += 'isSeacher=false';
    }
    else {
        strUrl += '&isSeacher=true';
    }
    return strUrl;
}

//新建
function fnNew() {
    window.location.href = 'SellPlanAdd.aspx?' + getSearchParams() + '&islist=1&ModuleID=2031001';
}

function Ifshow(count) {
    if (count == "0") {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pageSellOffcount").style.display = "none";
    }
    else {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pageSellOffcount").style.display = "block";
    }
}

//查看明细
function fnOrderInfo(retval, isRef) {
    window.location.href = 'SellPlanAdd.aspx?' + getSearchParams() + '&id=' + escape(retval) + "&islist=1&ModuleID=2031001&isRef=" + isRef;
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        return;
    }
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
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            this.pageCount = parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
}

//排序
function OrderBy(orderColum, orderTip) {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        return;
    }
    var ordering = "d";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM = $(".orderTip");
    if ($("#" + orderTip).html() == "↑") {
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↓");
    }
    else {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↑");
    }
    orderBy = orderColum + "_" + ordering;
    $("#hiddExpOrder").val(orderBy);
    TurnToPage(1);
}

//全选
function selectall() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}
///删除销售机会
function fnDel() {
    var DetailID = '';
    var pageDataList1 = findObj("pageDataList1", document);
    for (i = 0; i < pageDataList1.rows.length; i++) {
        if ($("#chk" + i).attr("checked")) {
            DetailID += $("#chk" + i).attr("title") + ',';
        }
    }
    if (DetailID == '') {
        popMsgObj.ShowMsg('请至少选择一条数据！');
    }
    else {

        if (confirm("数据删除后将不可恢复！您确定要删除？")) {
            //删除
            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/SellManager/SellPlanList.ashx",
                data: "action=del&orderNos=" + escape(DetailID),
                dataType: 'json', //返回json格式数据
                cache: false,
                beforeSend: function() {
                    AddPop();
                },
                error: function() {
                    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
                },
                success: function(data) {
                    hidePopup();
                    if (data.sta == 1) {
                        TurnToPage(1);

                    }
                    if (data.data.length != 0) {
                        popMsgObj.Show(data.data, data.Msg);
                    }
                    else {
                        popMsgObj.ShowMsg(data.Msg);
                    }
                }
            });
        }
    }
}

//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if (!isFlag) {
        //注：两个方法显示弹出提示信息层,方法一是对字段用红色处理，方法二是所有的提示信息传入未处理颜色

        //方法一
        popMsgObj.Show(fieldText, msgText);

        //方法二
        //popMsgObj.ShowMsg('所有的错误信息字符串');
    }
    return isFlag;
}

//判断是否可以导出Excel
function fnIsSearch() {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "您没有检索任何数据！");
        return false;
    }
    if (totalRecord == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "没有任何数据可以被导出！");
        return false;
    }
    $("#hiddExpTotal").val(totalRecord);
    return true;
}