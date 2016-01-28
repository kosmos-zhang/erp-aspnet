/********************************
*描述：销售发货明细 js
*创建人：hexw
*创建时间：2010-06-18
********************************/

$(document).ready(function() {

    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var isSeacher = requestObj['isSeacher']; //是否从列表页面进入
        if (isSeacher == 'true') {
            $("#hiddProductID").val(requestObj['ProductID']); //物品
            $("#ProductID").val(requestObj['ProductNo']); //
            $("#hiddCustID").val(requestObj['CustID']); //客户
            $("#CustID").val(requestObj['CustName']); //
            $("#BeginDate").val(requestObj['BeginDate']); //开始时间
            $("#EndDate").val(requestObj['EndDate']);      //结束时间
            $("#isOpenbill").val(requestObj['isOpenbill']);      //是否开票
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

//选择客户
function fnSelectCustInfo() {
    popSellCustObj.ShowList('protion');
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
            $("#hiddCustID").val(custID);//客户ID
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}
//清除客户信息
function ClearSellModuCustdiv() {
    $("#CustID").val(''); //客户名称
    $("#CustID").attr("title", ''); //客户编号
    $("#hiddCustID").val('');//客户ID
    closeSellModuCustdiv(); //关闭客户选择控件
}

/*物品控件选择*/
function Fun_FillParent_Content(id, ProNo, ProdName) {
    document.getElementById('ProductNo').value = ProNo;
    document.getElementById('hiddProductID').value = id;
}

/*清空物品控件选择*/
function ClearPkroductInfo() {
    document.getElementById('ProductNo').value = '';
    document.getElementById('hiddProductID').value = '';
    closeProductdiv();
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
    //$("#checkall").removeAttr("checked")
    var action = "getinfolist";

    var strUrl = getSearchParams1() + '&pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy) +
               '&action=' + escape(action);
    $("#hiddUrl").val(strUrl);
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/SellManager/SellSendDetailsList.ashx', //目标地址
        cache: false,
        data: strUrl,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataListSSendDL tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
            if (item.ProdNo != null && item.ProdNo != "") {

                var ParamLoc="";
                var hrefStr="";
                ParamLoc=item.SendID+"|"+item.SendNo+"|"+item.CustID+"|"+item.CustName+"|"+item.CurrencyType+"|"+item.CurrencyName+"|"+item.Rate+"|"+item.TaxTotalPrice+"|7";
                if(item.IsOpenBillText=="已开票")
                {
                    hrefStr="开票";
                }
                else if(item.BillStatus=="2")
                {
                    hrefStr="<a href='#' onclick=\"fnTurnToBilling('"+ParamLoc+"')\">开票</a>";
                }
                else
                {
                    hrefStr="开票";
                }
                
                $("<tr class='newrow'></tr>").append(//"<td height='22' align='center'>" + "<input id='chk" + i + "'  onclick = 'fnUnSelect(this)'  Title='" + item.SendNo + "'    value='" + item.ID + "'   type='checkbox'/>" + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.SendDate + "\">" + item.SendDate + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.CustName + "\">" + item.CustName + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.SendNo + "\" >" + item.SendNo + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.ProdNo + "\">" + item.ProdNo + "</a></td>" +
                    "<td height='22' align='center'><span title=\"" + item.ProductName + "\">" + item.ProductName + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.Specification + "\">" + item.Specification + "</td>" +
                    "<td height='22' align='center'>" + item.ColorName + "</td>" +
                    //"<td height='22' align='center'><span title=\"" + item.TaxPrice + "\">" + item.TaxPrice + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.ProductCount + "\">" + item.ProductCount + "</td>" +
                    //"<td height='22' align='center'><span title=\"" + item.Discount + "\">" + item.Discount + "</td>" +
                    //"<td height='22' align='center'><span title=\"" + item.TotalTax + "\">" + item.TotalTax + "</td>" +
                    //"<td height='22' align='center'>" + item.SendDate + "</td>" +
                    "<td height='22' align='center'>" + item.IsOpenBillText + "</td>"+
                    "<td height='22' align='center'>" + item.InvoiceTypeText + "</td>"+
                    "<td height='22' align='center'>" + item.BillingNum + "</td>"+
                    "<td height='22' align='center'>" + item.BillExecutorName + "</td>"+
                    "<td height='22' align='center'>" + item.BillCreateDate + "</td>"+
                    "<td height='22' align='center'><a href='#' onclick=\"fnTurnToSellSend('"+item.SendID+"','"+item.RefText+"');\">查看发货单</a>  | "+hrefStr+"</td>"
                    ).appendTo($("#pageDataListSSendDL tbody"));
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
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.form1.elements["Text2"].value); pageDataList1("pageDataListSSendDL", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
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

//构造查询条件的URL参数
function getSearchParams1() {

    var ProductID = $("#hiddProductID").val(); //物品
    var CustID = $("#hiddCustID").val(); //客户
    var BeginDate = $("#BeginDate").val(); //开始时间
    var EndDate = $("#EndDate").val(); //结束时间
    var isOpenbill = $("#isOpenbill").val();     //是否已开票
    var ProductNo = $("#ProductID").val();//物品编号
    var CustName = $("#CustID").val();//客户名称
    
    var strUrl = 'ProductID=' + escape(ProductID) + '&CustID=' + escape(CustID) + '&BeginDate=' + escape(BeginDate)
                + '&EndDate=' + escape(EndDate) + '&isOpenbill=' + escape(isOpenbill)
                + '&ProductNo=' + escape(ProductNo) + '&CustName=' + escape(CustName);

    $("#hiddExcelProductID").val(ProductID);
    $("#hiddExcelCustID").val(CustID);
    $("#hiddBeginDate").val(BeginDate);
    $("#hiddEndDate").val(EndDate);
    $("#hiddIsOpenbill").val(isOpenbill);
    
    return strUrl;
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
function OrderBySD(orderColum, orderTip) {
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

//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var BeginDate = $("#BeginDate").val();
    var EndDate = $("#EndDate").val();
    if (BeginDate != '' && EndDate!='') {
        if (CompareDate(BeginDate, EndDate) == 1) {
            isFlag = false;
            fieldText = fieldText + "发货日期|";
            msgText = msgText + "发货日期范围选择错误|";
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

//跳转到销售发货单页面
function fnTurnToSellSend(retval,isRef)
{
    var sellmoduleid=$("#hiddSendAddModuleid").val();
    var moduleid=$("#hiddModuleID").val();
    window.location.href = "SellSendMod.aspx?"+getSearchParams()+"&id=" + escape(retval) + "&ModuleID="+sellmoduleid+"&isRef=" + isRef+"&ListModuleID="+moduleid+"&intFromType=6";
}

//跳转到开票页面
function fnTurnToBilling(ParamLoc)
{
    var billingmoduleid=$("#hiddBillingAddModuleid").val();
    var moduleid=$("#hiddModuleID").val();
    window.location.href="../FinanceManager/Billing_Add.aspx?"+getSearchParams()+"&ParamLoc="+ParamLoc+"&SellDModuleID="+moduleid+"&ModuleID="+billingmoduleid;
}
