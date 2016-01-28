/********************
**销售汇报列表js
*创建人：hexw
*创建时间：2010-7-6
********************/

$(document).ready(function() {
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var isSeacher = requestObj['isSeacher']; //是否从列表页面进入
        if (isSeacher == 'true') {
            $("#hiddDeptID").val(requestObj['DeptID']); //申请人部门
            $("#DeptName").val(requestObj['DeptName']); //
            $("#CreateDate").val(requestObj['CreateDate']); // 日期
            $("#CreateDate1").val(requestObj['CreateDate1']); //日期
    
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

//新建费用申请单
function fnNew() {
    window.location.href = 'SellProductReportAdd.aspx?' + getSearchParams() + '&islist=1';
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

//删除费用申请单(非制单状态不可删)
function fnDel()
{
     var fieldText = "";
        var isFlag = true;
        var msgText = "";
        var DetailID = '';
        var pageDataList1 = findObj("pageDataListExpense", document);
        for (i = 0; i < pageDataList1.rows.length; i++) {
            if ($("#chk" + i).attr("checked")) {
                DetailID+=$("#chk"+i).val()+',';
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
                        url: "../../../Handler/Office/SellReport/SellProductReport.ashx",
                        data: "action=del&BillIDs=" + escape(DetailID),
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
            }else
            {
                popMsgObj.Show(fieldText, msgText);
            }
        }
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
    var action = "getdatalist";

    var strUrl = getSearchParams1() + '&pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy) +
               '&action=' + escape(action);
    $("#hiddUrl").val(strUrl);

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/SellReport/SellProductReport.ashx', //目标地址
        cache: false,
        data: strUrl,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
          
            $("#pageDataListExpense tbody").find("tr.newrow").remove();
          
            $.each(msg.data, function(i, item) {
            
            if (item.id != null) {

                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'><input id='chk" + i + "'  onclick = 'fnUnSelect(this)' value='" + item.id + "'  type='checkbox'/></td>" +
                    "<td height='22' align='center'>" + item.createdate + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.SellDeptName + "\">" + item.SellDeptName + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.productName + "\">" + item.productName + "</td>" +
                    "<td height='22' align='center'>" + item.sellNum + "</td>" +
                    "<td height='22' align='center'>" + item.sellPrice + "</td>" +
                    "<td height='22' align='center'>" + item.SellerRate + "</td>" +
                    "<td height='22' align='center'><a href='#' title=\"编辑\" onclick=fnExpApplyInfo('" + item.id + "')>编辑</a></td>" ).appendTo($("#pageDataListExpense tbody"));
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
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pageExpApplyCount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.form1.elements["Text2"].value); pageDataList1("pageDataListExpense", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });

}
  
//是否显示列表记录和翻页Bar
function Ifshow(count)
{
    if (count == "0") {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pageExpApplyCount").style.display = "none";
    }
    else {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pageExpApplyCount").style.display = "block";
    }
}

 //构造查询条件的URL参数
function getSearchParams1() {
    var CreateDate = $("#CreateDate").val(); //申请日期          
    var CreateDate1 = $("#CreateDate1").val(); //申请日期
    var DeptID = $("#hiddDeptID").val(); //申请人部门
    var DeptName=$("#DeptName").val();
    
    var strUrl = '&createdate=' + escape(CreateDate) +'&createdate1=' + escape(CreateDate1)+'&SellDept='+escape(DeptID)+
                '&SellDeptName='+escape(DeptName);

    $("#hiddCreateDate").val(CreateDate);
    $("#hiddCreateDate1").val(CreateDate1);
    $("#hiddSellDeptID").val(DeptID);
    
    return strUrl;
}
 
 //表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var CreateDate = $("#CreateDate").val(); //申请日期
    var CreateDate1 = $("#CreateDate1").val(); //申请日期

    if (CreateDate != '' && CreateDate1 != '') {
        if (CreateDate > CreateDate1) {
            isFlag = false;
            fieldText = fieldText + "日期范围|";
            msgText = msgText + "日期范围输入错误|";
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
   
//查看明细
function fnExpApplyInfo(retval) {
    window.location.href = 'SellProductReportAdd.aspx?' + getSearchParams() + '&id=' + escape(retval) + "&islist=1";
}
    
//全选
function selectall() {
    $.each($("#pageDataListExpense :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
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
        var signFrame = findObj("pageDataListExpense", document);
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

//排序
function OrderBy(orderColum, orderTip) {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        return;
    }
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
    $("#hiddExpOrder").val(orderBy);
    TurnToPage(1);
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

////判断是否可以导出Excel
//function fnIsSearch() {
//    var strUrl = $.trim($("#hiddUrl").val());
//    if (strUrl.length == 0) {
//        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "您没有检索任何数据！");
//        return false;
//    }
//    if (totalRecord == 0) {
//        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "没有任何数据可以被导出！");
//        return false;
//    }
//    $("#hiddPageCount").val(totalRecord);
//    return true;
//}