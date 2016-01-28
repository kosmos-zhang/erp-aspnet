$(document).ready(function() {
    IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
    $("#TakeType_ddlCodeType").css("width", "120px");
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var isSeacher = requestObj['isSeacher']; //是否从列表页面进入
        if (isSeacher == 'true') {
            $("#orderNo").val(requestObj['orderNo']); //销售机会
            $("#Title").val(requestObj['Title']); //主题
            $("#Receiver").val(requestObj['Receiver']); //收货人
            $("#UserSender").val(requestObj['Sender']); //发货人
            $("#hiddSender").val(requestObj['SenderName']); //发货人
           
            $("#BillStatus").val(requestObj['BillStatus']);      //单据状态
            $("#FlowStatus").val(requestObj['FlowStatus']);      //单据状态
            $("#FromBillID").val(requestObj['FromBillNo']); //来源单据编号
            $("#FromBillID").attr("title", requestObj['FromBillID']); //来源单据编号
            $("#TakeType_ddlCodeType").val(requestObj['TakeType']); //交货方式

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


//选择源单
function fnSelectOfferInfo() {
   
        popSellOrderObj.ShowList('all');
  
}
//清除销售订单
function clearSellOrder() {
    $("#FromBillID").val(''); //发货单编号
    $("#FromBillID").attr("title", ''); //发货单ID

    closeSellOrderdiv();
}

//选择业务员
function fnSelectSeller() {
   alertdiv('UserSeller,hiddSeller');
}

////选择订单后带出发货单明细信息
function fnSelectSellOrder(OrderId) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SelectSellOrderDetail.ashx",
        data: 'actionDet=orderList&OrderID=' + OrderId,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取发货单数据失败,请确认"); },
        success: function(data) {
            //发货单基本信息    
            if (data.ord != null) {
                $("#FromBillID").val(data.ord[0].OrderNo); //发货单编号
                $("#FromBillID").attr("title", OrderId); //发货单ID
                
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该单据信息已不存在,请确认");
            }
        }
    });
    closeSellOrderdiv(); //关闭客户选择控件  
}



//全选
function selectall() {
    $.each($("#pageDataListSSendL :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
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
        url: '../../../Handler/Office/SellManager/SellSendList.ashx', //目标地址
        cache: false,
        data: strUrl,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataListSSendL tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
            if (item.ID != null && item.ID != "") {

                var OrderNo = item.OrderNo;
                var Title = item.Title;
                var SendNo = item.SendNo;
                var CustName = item.CustName;
                var SenderName = item.SenderName;
                var Receiver = item.Receiver;
                if (Title != null) {
                    if (Title.length > 6) {
                        Title = Title.substring(0, 6) + '...';
                    }
                }
                if (OrderNo != null) {
                    if (OrderNo.length > 10) {
                        OrderNo = OrderNo.substring(0, 10) + '...';
                    }
                }
                if (SendNo != null) {
                    if (SendNo.length > 15) {
                        SendNo = SendNo.substring(0, 15) + '...';
                    }
                }
                if (CustName != null) {
                    if (CustName.length > 6) {
                        CustName = CustName.substring(0, 6) + '...';
                    }
                }
                if (SenderName != null) {
                    if (SenderName.length > 6) {
                        SenderName = SenderName.substring(0,6) + '...';
                    }
                }
                if (Receiver != null) {
                    if (Receiver.length > 6) {
                        Receiver = Receiver.substring(0, 6) + '...';
                    }
                }
                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='chk" + i + "'  onclick = 'fnUnSelect(this)'  Title='" + item.SendNo + "'    value='" + item.ID + "'   type='checkbox'/>" + "</td>" +
                    "<td height='22' align='center'><a href='#' title=\"" + item.SendNo + "\" onclick=fnOrderInfo('" + item.ID + "','" + item.RefText + "')>" + SendNo + "</a></td>" +
                    "<td height='22' align='center'><span title=\"" + item.Title + "\">" + Title + "</a></td>" +
                  
                    "<td height='22' align='center'><span title=\"" + item.OrderNo + "\">" + OrderNo + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.CustName + "\">" + CustName + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.SellerName + "\">" + item.SellerName + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.SenderName + "\">" + SenderName + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.Receiver + "\">" + Receiver + "</td>" +
                    "<td height='22' align='center'>" + item.TypeName + "</td>" +
                    "<td height='22' align='center'>" + item.BillStatusText + "</td>" +
                    "<td height='22' align='center'>" + item.FlowInstanceText + "</td>").appendTo($("#pageDataListSSendL tbody"));
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
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.form1.elements["Text2"].value); pageDataList1("pageDataListSSendL", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
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
        var signFrame = findObj("pageDataListSSendL", document);
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

    var orderNo = $("#orderNo").val(); //销售机会
    var Title = $("#Title").val(); //主题
    var Receiver = $("#Receiver").val(); //收货人
    var SenderName = $("#UserSender").val(); //发货人
    if (SenderName != '') {
        var Sender = $("#hiddSender").val(); //发货人
    }
    else {
        var Sender = ''; //发货人
    }
    var FromType = '1';      //来源单据类型
    var BillStatus = $("#BillStatus").val();     //单据状态
    var FlowStatus = $("#FlowStatus").val(); //审批状 态
    var TakeType = $("#TakeType_ddlCodeType").val(); //发货方式
    var FromBillNo = $("#FromBillID").val(); // //来源单据编号
    if (FromBillNo != '') {
        var FromBillID = $("#FromBillID").attr("title"); // //来源单据id
    }
    else {
        var FromBillID = ''; //来源单据编号
    }
    var SellerName = $("#UserSeller").val(); //业务员
    if (SellerName != '') {
        var Seller = $("#hiddSeller").val(); //业务员
    }
    else {
        var Seller = ''; //业务员
    }
    var ProjectID = $("#hiddProjectID").val();//所属项目ID 
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
    
    var strUrl = 'orderNo=' + escape(orderNo) + '&Title=' + escape(Title) + '&Receiver=' + escape(Receiver)
                + '&SenderName=' + escape(SenderName) + '&Sender=' + escape(Sender)
                + '&FromType=' + escape(FromType) + '&BillStatus=' + escape(BillStatus)
                + '&FlowStatus=' + escape(FlowStatus) + '&FromBillNo=' + escape(FromBillNo)
                + '&FromBillID=' + escape(FromBillID) + '&TakeType=' + escape(TakeType)
                 +'&Seller=' + escape(Seller)+'&ProjectID=' + escape(ProjectID)
                 +'&EFIndex=' +escape(EFIndex) +'&EFDesc=' + escape(EFDesc);

    $("#hiddExpOrderNo").val(orderNo);
    $("#hiddExpTitle").val(Title);
    $("#hiddExpFromType").val(FromType);
    $("#hiddExpFromBillID").val(FromBillID);
    $("#hiddExpReceiver").val(Receiver);
    $("#hiddExpSender").val(Sender);
    $("#hiddExpTakeType").val(TakeType);
    $("#hiddExpBillStatus").val(BillStatus);
    $("#hiddExpFlowStatus").val(FlowStatus);
    $("#hiddExpSeller").val(Seller);
    
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
    window.location.href = 'SellSendAdd.aspx?' + getSearchParams() + '&islist=1&ModuleID=2031601';
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
    window.location.href = 'SellSendMod.aspx?' + getSearchParams() + '&id=' + escape(retval) + "&islist=1&ModuleID=2031601&isRef=" + isRef;
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


///删除销售机会
function fnDel() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var DetailID = '';
    var pageDataList1 = findObj("pageDataListSSendL", document);
    for (i = 0; i < pageDataList1.rows.length; i++) {
        if ($("#chk" + i).attr("checked")) {
            DetailID += $("#chk" + i).attr("title") + ',';
           
        }
    }
    if (DetailID == '') {
        popMsgObj.ShowMsg('请至少选择一条数据！');
    }
    else {
        if (isFlag) {
            if (confirm("数据删除后将不可恢复！您确定要删除？")) {
                //删除
                $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/SellManager/SellSendList.ashx",
                    data: "action=del&orderNos=" + escape(DetailID),
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {

                    },

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
        else {
            popMsgObj.Show(fieldText, msgText);
        }
    }
}

//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var orderNo = $("#orderNo").val();
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if (orderNo != '') {
        if (!CodeCheck(orderNo)) {
            isFlag = false;
            fieldText = fieldText + "发货单编号|";
            msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
        }
    }
    
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

////清除选择的所属项目
//function fnClearProject()
//{
//    $("#hiddProjectID").val('');//所属项目ID
//    $("#ProjectID").val('');//所属项目名称
//}