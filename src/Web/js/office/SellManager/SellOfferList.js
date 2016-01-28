$(document).ready(function() {

    $("#chanceTypeUC_ddlCodeType").css("width", "120px");
    $("#HapSourceUC_ddlCodeType").css("width", "120px");
    $("#FeasibilityUC_ddlCodeType").css("width", "120px");
    
//GetBillExtAttr('officedba.SellOffer');
IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue'); 

    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var isSeacher = requestObj['isSeacher']; //是否从列表页面进入
        if (isSeacher == 'true') {
            $("#OfferNo").val(requestObj['OfferNo']); //销售机会
            $("#Title").val(requestObj['Title']); //主题
            $("#CustID").val(requestObj['CustName']); //客户
            $("#CustID").attr("title", requestObj['CustID']); //客户
            $("#UserSeller").val(requestObj['UserSeller']); //业务员
            $("#hiddSeller").val(requestObj['Seller']); //业务员
            $("#FromType").val(requestObj['FromType']);      //来源单据类型
            $("#BillStatus").val(requestObj['BillStatus']);      //单据状态
            $("#FlowStatus").val(requestObj['FlowStatus']);      //单据状态
            $("#FromBillID").val(requestObj['FromBillNo']); //来源单据编号
            $("#FromBillID").attr("title", requestObj['FromBillID']); //来源单据编号

            $("#OfferDate").val(requestObj['OfferDate']);   //发现日期小
            $("#OfferDate1").val(requestObj['OfferDate1']);   //发现日期大
            
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


//选择源单
function fnSelectOfferInfo() {
    if ($("#FromType").val() == 1) {     
            popSellSendObj.ShowList('all');
    }
}

//获取销售机会详细信息
function fnSelectSellSend(orderID) {

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellChanceList.ashx",
        data: "action=orderInfo&orderID=" + escape(orderID),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            if (data.data.length == 1) {
                $("#FromBillID").val(data.data[0].ChanceNo); //机会编号
                $("#FromBillID").attr("title", orderID); //机会编号

                closeSellSenddiv();
            }
        }
    });
}
//选择业务员
function fnSelectSeller() {

    alertdiv('UserSeller,hiddSeller');
}

//选择客户
function fnSelectCustInfo() {

    popSellCustObj.ShowList('all');

}

//清除客户信息
function ClearSellModuCustdiv() {
    $("#CustID").val(''); //客户名称
    $("#CustID").attr("title", ''); //客户编号
    closeSellModuCustdiv(); //关闭客户选择控件
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
        url: '../../../Handler/Office/SellManager/SellOfferList.ashx', //目标地址
        cache: false,
        data: strUrl,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
            if (item.ID != null && item.ID != "") {
                    var OfferNo = item.OfferNo;
                    var Title = item.Title;
                    var ChanceNo = item.ChanceNo
                    if (Title != null) {
                        if (Title.length > 15) {
                            Title = Title.substring(0, 15) + '...';
                        }
                    }
                    if (OfferNo != null) {
                        if (OfferNo.length > 15) {
                            OfferNo = OfferNo.substring(0, 15) + '...';
                        }
                    }
                    if (ChanceNo != null) {
                        if (ChanceNo.length > 15) {
                            ChanceNo = ChanceNo.substring(0, 15) + '...';
                        }
                    }
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='chk" + i + "' onclick = 'fnUnSelect(this)'  Title='" + item.OfferNo + "'   value='" + item.ID + "'   type='checkbox'/>" + "</td>" +
                    "<td height='22' align='center'><a href='#' title=\"" + item.OfferNo + "\"  onclick=fnOrderInfo('" + item.ID + "','" + item.RefText + "')>" + OfferNo + "</a></td>" +
                    "<td height='22' align='center'><span title=\"" + item.Title + "\">" + Title + "</a></td>" +
                    "<td height='22' align='center'>" + item.FromTypeText + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.ChanceNo + "\">" + ChanceNo + "</td>" +
                    "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                    "<td height='22' align='center'>" + item.OfferDate + "</td>" +
                    "<td height='22' align='center'>" + item.QuoteTime + "</td>" +
                    "<td height='22' align='center'>" + FormatAfterDotNumber(item.TotalFee, precisionLength) + "</td>" +
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

//清除销售机会
function clearSellChance() {
    $("#FromBillID").val(''); //机会编号
    $("#FromBillID").attr("title", ''); //机会编号
    closeSellSenddiv();
}

//构造查询条件的URL参数
function getSearchParams1() {
    var OfferNo = $("#OfferNo").val(); //销售机会
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
    var BillStatus = $("#BillStatus").val();     //单据状态

    var OfferDate = $("#OfferDate").val();   //发现日期小
    var OfferDate1 = $("#OfferDate1").val();   //发现日期大
    var FlowStatus = $("#FlowStatus").val(); //审批状 态
    var FromBillNo = $("#FromBillID").val(); // //来源单据编号
    if (FromBillNo != '') {
        var FromBillID = $("#FromBillID").attr("title"); // //来源单据id
    }
    else {
        var FromBillID = ''; //来源单据编号
    }
    
   var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
    
    var strUrl = 'OfferNo=' + escape(OfferNo) + '&Title=' + escape(Title) + '&CustName=' + escape(CustName)
                + '&CustID=' + escape(CustID) + '&UserSeller=' + escape(UserSellerName) + '&Seller=' + escape(Seller)
                + '&FromType=' + escape(FromType) + '&BillStatus=' + escape(BillStatus) + '&OfferDate=' + escape(OfferDate)
                + '&OfferDate1=' + escape(OfferDate1) + '&FlowStatus=' + escape(FlowStatus) + '&FromBillNo=' + escape(FromBillNo)
                + '&FromBillID=' + escape(FromBillID) + '&EFIndex=' + escape(EFIndex) + '&EFDesc=' + escape(EFDesc);

    $("#hiddExpOrderNo").val(OfferNo);
    $("#hiddExpTitle").val(Title);
    $("#hiddExpFromType").val(FromType);

    $("#hiddExpSeller").val(Seller);
    $("#hiddExpFromBillID").val(FromBillID);
    $("#hiddExpOfferDate").val(OfferDate);
    $("#hiddExpOfferDate1").val(OfferDate1);

    $("#hiddExpCustID").val(CustID);
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
    window.location.href = 'SellOfferAdd.aspx?' + getSearchParams() + '&islist=1&ModuleID=2031301';
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
    window.location.href = 'SellOfferMod.aspx?' + getSearchParams() + '&id=' + escape(retval) + "&islist=1&ModuleID=2031301&isRef=" + isRef;
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
                url: "../../../Handler/Office/SellManager/SellOfferList.ashx",
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
    var FindDate = $.trim($("#OfferDate").val()); //发现日期
    var FindDate1 = $.trim($("#OfferDate1").val()); //发现日期
    var SendNo = $.trim($("#OfferNo").val()); //报价单编号    
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (SendNo != '') {
        if (!CodeCheck(SendNo)) {
            isFlag = false;
            fieldText = fieldText + "报价单编号|";
            msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
        }
    }
    
    if (FindDate != '' && FindDate1 != '') {
        if (CompareDate(FindDate, FindDate1) == 1) {
            isFlag = false;
            fieldText = fieldText + "报价日期|";
            msgText = msgText + "报价日期范围选择错误|";
        }
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