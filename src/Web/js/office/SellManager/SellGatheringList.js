$(document).ready(function() {

    IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');//扩展属性
    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var isSeacher = requestObj['isSeacher']; //是否从列表页面进入
        if (isSeacher == 'true') {
            $("#GatheringNo").val(requestObj['OfferNo']); //销售机会
            $("#Title").val(requestObj['Title']); //主题
            $("#CustID").val(requestObj['CustName']); //客户
            $("#CustID").attr("title", requestObj['CustID']); //客户
            $("#UserSeller").val(requestObj['UserSeller']); //业务员
            $("#hiddSeller").val(requestObj['Seller']); //业务员
            $("#FromType").val(requestObj['FromType']);      //来源单据类型
            $("#GatheringTime").val(requestObj['GatheringTime']);      //期次
            $("#FromBillID").val(requestObj['FromBillNo']); //来源单据编号
            $("#FromBillID").attr("title", requestObj['FromBillID']); //来源单据编号

            pageCount = requestObj['pageCount']; //每页计数
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


//清除销售订单
function clearSellOrder() {
    $("#FromBillID").val(''); //来源单据编号
    $("#FromBillID").attr("title", ''); //来源单据编号

    closeSellOrderdiv();
}
//清除销售订单
function clearSellSend() {
    $("#FromBillID").val(''); //来源单据编号
    $("#FromBillID").attr("title", ''); //来源单据编号

    closeSellSenddiv();
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

    $("#checkall").removeAttr("checked")
    var action = "getinfo";

    var strUrl = getSearchParams1() + '&pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy) +
               '&action=' + escape(action);
    $("#hiddUrl").val(strUrl);
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/SellManager/SellGatheringList.ashx', //目标地址
        cache: false,
        data: strUrl,

        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    var OrderNo = item.GatheringNo;
                    var Title = item.Title;
                    var SendNo = item.BillNo;
                    var CustName = item.CustName;
                    var SenderName = item.EmployeeName;

                    if (Title != null) {
                        if (Title.length > 6) {
                            Title = Title.substring(0, 6) + '...';
                        }
                    }
                    if (OrderNo != null) {
                        if (OrderNo.length > 15) {
                            OrderNo = OrderNo.substring(0, 15) + '...';
                        }
                    }
                    if (SendNo != null) {
                        if (SendNo.length > 10) {
                            SendNo = SendNo.substring(0, 10) + '...';
                        }
                    }
                    if (CustName != null) {
                        if (CustName.length > 6) {
                            CustName = CustName.substring(0, 6) + '...';
                        }
                    }
                    if (SenderName != null) {
                        if (SenderName.length > 6) {
                            SenderName = SenderName.substring(0, 6) + '...';
                        }
                    }

                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='chk" + i + "'   onclick = 'fnUnSelect(this)'   value=" + item.ID + "  type='checkbox'/>" + "</td>" +
                    "<td height='22' align='center'><a href='#' title=\"" + item.GatheringNo + "\" onclick=SelectDept('" + item.ID + "')>" + OrderNo + "</a></td>" +
                    "<td height='22' align='center'><span title=\"" + item.Title + "\">" + Title + "</a></td>" +
                    "<td height='22' align='center'><span title=\"" + item.CustName + "\">" + CustName + "</td>" +
                    "<td height='22' align='center'>" + item.stateName + "</td>" +
                    "<td height='22' align='center'>" + item.fromTypeName + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.BillNo + "\">" + SendNo + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.EmployeeName + "\">" + SenderName + "</td>" +
                    "<td height='22' align='center'>" + FormatAfterDotNumber(item.PlanPrice, precisionLength) + "</td>" +
                    "<td height='22' align='center'>" + item.GatheringTime + "</td>" +
                    "<td height='22' align='center'>" + item.PlanGatherDate + "</td>").appendTo($("#pageDataList1 tbody"));
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


//构造查询条件的URL参数
function getSearchParams1() {

    var OfferNo = $("#GatheringNo").val(); //销售机会
    var Title = $("#Title").val(); //主题
    var CustName = $("#CustID").val(); //客户
    if (CustName != '') {
        var CustID = $("#CustID").attr("title"); //客户
    }
    else {
        var CustID = '';
    }
    var UserSellerName = $("#UserSeller").val(); //业务员
    if (UserSellerName != '') {
        var Seller = $("#hiddSeller").val(); //业务员
    }
    else {
        var Seller = ''; //业务员
    }
    var FromType = $("#FromType").val();      //来源单据类型

    var PlanPrice = $("#PlanPrice").val();      //计划回款金额
    var PlanPrice0 = $("#PlanPrice0").val();      //计划回款金额

    var FromBillNo = $("#FromBillID").val(); // //来源单据编号
    if (FromBillNo != '') {
        var FromBillID = $("#FromBillID").attr("title"); // //来源单据id
    }
    else {
        var FromBillID = ''; //来源单据编号
    }
    var GatheringTime = $("#GatheringTime").val();  //期次
    //扩展属性
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值

    var strUrl = 'OfferNo=' + escape(OfferNo) + '&Title=' + escape(Title) + '&CustName=' + escape(CustName)
                + '&CustID=' + escape(CustID) + '&UserSeller=' + escape(UserSellerName) + '&Seller=' + escape(Seller)
                + '&FromType=' + escape(FromType) + '&GatheringTime=' + escape(GatheringTime) + '&PlanPrice=' + escape(PlanPrice)
                + '&PlanPrice0=' + escape(PlanPrice0) + '&FromBillNo=' + escape(FromBillNo)
                + '&FromBillID=' + escape(FromBillID)+ '&EFIndex=' +escape(EFIndex) +'&EFDesc=' + escape(EFDesc);

    $("#hiddExpGatheringNo").val(OfferNo);
    $("#hiddExpTitle").val(Title);
    $("#hiddExpCustID").val(CustID);
    $("#hiddExpFromType").val(FromType);
    $("#hiddExpSeller").val(Seller);
    $("#hiddExpFromBillID").val(FromBillID);
    $("#hiddExpPlanPrice").val(PlanPrice);
    $("#hiddExpPlanPrice0").val(PlanPrice0);
    $("#hiddExpGatheringTime").val(GatheringTime);
    
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


//选择源单
function fnSelectOfferInfo() {
    if ($("#FromType").val() == 1) {
        if ($("#FromType").attr("disabled") != true) {
            popSellOrderObj.ShowList('');
        }
    }
    if ($("#FromType").val() == 2) {
        if ($("#FromType").attr("disabled") != true) {
            popSellSendObj.ShowList('');
        }
    }
}

//清除客户信息
function ClearSellModuCustdiv() {
    $("#CustID").val(''); //客户名称
    $("#CustID").removeAttr("title"); //客户编号

    closeSellModuCustdiv();
}

//选择业务员
function fnSelectSeller() {
    alertdiv('UserSeller,hiddSeller');
}

//全选
function selectall() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

//新建
function fnNew() {
    window.location.href = 'SellGatheringAdd.aspx?' + getSearchParams() + '&islist=1&ModuleID=2031701';
}

//选择客户
function fnSelectCustInfo() {
    popSellCustObj.ShowList('all');
}

//选择客户后，为页面填充数据
function fnSelectCust(custID) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellModuleSelectCustUC.ashx",
        data: 'actionSellCust=info&id=' + custID,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取客户数据失败,请确认"); },
        success: function(data) {
            $("#CustID").val(data.CustName); //客户名称
            $("#CustID").attr("title", custID); //客户编号
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}

////选择订单后带出订单明细信息
function fnSelectSellOrder(OrderId) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SelectSellOrderDetail.ashx",
        data: 'actionDet=orderList&OrderID=' + OrderId,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取订单数据失败,请确认"); },
        success: function(data) {
            //订单基本信息    
            if (data.ord != null) {
                $("#FromBillID").val(data.ord[0].OrderNo); //订单编号
                $("#FromBillID").attr("title", OrderId); //订单ID
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该单据信息已不存在,请确认");
            }
        }
    });
    closeSellOrderdiv(); //关闭客户选择控件
}

////选择发货单后带出发货单明细信息
function fnSelectSellSend(OrderId) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SelectSellSend.ashx",
        data: 'actionSend=orderList&OrderID=' + OrderId,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取发货单数据失败,请确认"); },
        success: function(data) {
            //订单基本信息    
            if (data.ord != null) {
                $("#FromBillID").val(data.ord[0].SendNo); //订单编号
                $("#FromBillID").attr("title", OrderId); //订单ID

            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该单据信息已不存在,请确认");
            }
        }
    });
    closeSellSenddiv(); //关闭客户选择控件
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
function SelectDept(retval) {
    window.location.href = 'SellGatheringMod.aspx?' + getSearchParams() + '&id=' + escape(retval) + "&islist=1&ModuleID=2031701&isRef=11";
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


///删除回款计划信息
function DelEquipmentInfo() {
    var DetailID = '';
    var pageDataList1 = findObj("pageDataList1", document);
    for (i = 0; i < pageDataList1.rows.length; i++) {
        if ($("#chk" + i).attr("checked")) {
            DetailID += $("#chk" + i).val() + ',';
        }
    }
    if (DetailID == '') {
        popMsgObj.ShowMsg('请至少选择一条数据！');
    }
    else {
        if (confirm("数据删除后将不可恢复！您确定要删除？")) {
            $.ajax({
                type: "POST", //用POST方式传输
                dataType: "json", //数据格式:JSON
                url: '../../../Handler/Office/SellManager/SellGatheringList.ashx', //目标地址
                cache: false,
                data: '&GatheringNos=' + escape(DetailID) + '&action=del',
                beforeSend: function() { }, //发送数据之前
                error: function() {

                    popMsgObj.ShowMsg('请求发生错误！');
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

    var SendNo = $("#GatheringNo").val(); //回款计划编号
    var PlanPrice = $("#PlanPrice").val(); //计划回款金额
    var PlanPrice0 = $("#PlanPrice0").val(); //计划回款金额                                                                                                       
    var GatheringTime = $("#GatheringTime").val(); //期次
var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (SendNo != "") {
        if (isnumberorLetters(SendNo)) {
            isFlag = false;
            fieldText = fieldText + "回款计划编号|";
            msgText = msgText + "回款计划编号只能为数字字母组合|";
        }
    }
    if (GatheringTime != "") {
        if (!IsZint(GatheringTime)) {
            isFlag = false;
            fieldText = fieldText + "期次|";
            msgText = msgText + "期次只能为正整数|";
        }
    }
    if (PlanPrice.length > 0) {
        if (!IsNumeric(PlanPrice, 8, 2)) {
            isFlag = false;
            fieldText = fieldText + "总金额范围|";
            msgText = msgText + "总金额范围格式不正确|";
        }
    }
    if (PlanPrice0.length > 0) {
        if (!IsNumeric(PlanPrice0, 8,2)) {
            isFlag = false;
            fieldText = fieldText + "总金额范围|";
            msgText = msgText + "总金额范围格式不正确|";
        }
    }

    if (PlanPrice != '' && PlanPrice0 != '') {
        if (parseFloat(PlanPrice) > parseFloat(PlanPrice0)) {
            isFlag = false;
            fieldText = fieldText + "总金额范围|";
            msgText = msgText + "总金额范围输入错误|";
        }
    }
//    //扩展属性
//    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
//    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
//    if(EFIndex!="" && EFDesc=="")
//    {
//        isFlag=false;
//        fieldText=fieldText + "其他条件|";
//        msgText = msgText +  "其他条件不能为空|";
//    }
//    if(EFIndex=="" && EFDesc!="")
//    {
//        isFlag=false;
//        fieldText=fieldText + "其他条件|";
//        msgText = msgText +  "请选择其他条件|";
//    }
       
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