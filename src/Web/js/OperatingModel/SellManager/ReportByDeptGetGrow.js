$(document).ready(function() {
    var endDate = $.trim($("#OrderDate").val()); //截止日期
    var iCount = $("#YearLength").val()
    if (iCount == '3') {
        $("#lbl1").html(parseInt(endDate) - 2);
        $("#lbl2").html(parseInt(endDate) - 1);
        $("#lbl3").html(parseInt(endDate));
    }
    if (iCount == '5') {
        $("#lbl1").html(parseInt(endDate) - 4);
        $("#lbl2").html(parseInt(endDate) - 3);
        $("#lbl3").html(parseInt(endDate)-2);
        $("#lbl4").html(parseInt(endDate) - 1);
        $("#lbl5").html(parseInt(endDate));
    }
});


/* 分页相关变量定义 */
var pageCount = 10; //每页显示记录数
var totalRecord = 0; //总记录数
var pagerStyle = "flickr"; //jPagerBar样式
var currentPageIndex = 1; //当前页
var orderBy = ""; //排序字段


/*
* 改页显示
*/
function ChangePageCountIndex(newPageCount, newPageIndex) {
    var strUrl = $.trim($("#hiddSearch").val());
    if (strUrl.length == 0) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
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

function fnChange() {
    /* 分页相关变量定义 */
     pageCount = 10; //每页显示记录数
     totalRecord = 0; //总记录数
     pagerStyle = "flickr"; //jPagerBar样式
     currentPageIndex = 1; //当前页
     orderBy = ""; //排序字段
     
    var endDate = $.trim($("#OrderDate").val()); //截止日期
    var iCount = $("#YearLength").val();
    $("#tblDetailInfo tbody").find("tr.newrow").remove();
    if (iCount == '3') {
        $("#lbl1").html(parseInt(endDate) - 2);
        $("#lbl2").html(parseInt(endDate) - 1);
        $("#lbl3").html(parseInt(endDate));
        $("#year4").css("display", "none");
        $("#year5").css("display", "none");
        $("#Y4").css("display", "none");
        $("#G4").css("display", "none");
        $("#Y5").css("display", "none");
        $("#G5").css("display", "none");
    }
    if (iCount == '5') {
        $("#year4").css("display", "");
        $("#year5").css("display", "");
        $("#Y4").css("display", "");
        $("#G4").css("display", "");
        $("#Y5").css("display", "");
        $("#G5").css("display", "");
        $("#lbl1").html(parseInt(endDate) - 4);
        $("#lbl2").html(parseInt(endDate) - 3);
        $("#lbl3").html(parseInt(endDate) - 2);
        $("#lbl4").html(parseInt(endDate) - 1);
        $("#lbl5").html(parseInt(endDate));
    }
}

//查询
function fnGetSearch() {

    var strUrl = "";

    var endDate = $.trim($("#OrderDate").val()); //截止日期

    var SellDeptId = $("#SellDeptId").val();          //部门（对应员工表ID）
    var CurrencyTypeName = $.trim($("#CurrencyType").val());
    var iCount = $("#YearLength").val();
    if (CurrencyTypeName == '') {
        CurrencyType = '';
    }
    else {
        CurrencyType = $("#CurrencyType").attr("title");
    }
    strUrl = 'endDate=' + escape(endDate) + '&iCount=' + escape(iCount) 
              + '&SellDeptId=' + escape(SellDeptId) + '&CurrencyType=' + escape(CurrencyType) + '&CurrencyTypeName=' + escape(CurrencyTypeName);

    return strUrl;
}

/*
* 翻页处理
*/
function TurnToPage(pageIndex) {
    if (!CheckInput()) {
        return;
    }
    fnChange();
    //设置当前页
    currentPageIndex = pageIndex;

    var strUrl = fnGetSearch() + '&pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy);
    $("#hiddSearch").val(strUrl);
   // fnChange();
    $.ajax({
        type: "POST", //用POST方式传输
        url: '../../../Handler/OperatingModel/SellManager/ReportByDeptGetGrow.ashx', //目标地址
        dataType: "json", //数据格式:JSON
        cache: false,
        data: strUrl,
        beforeSend: function() {
            AddPop();
        }, //发送数据之前
        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#tblDetailInfo tbody").find("tr.newrow").remove();
            if (msg != null) {
                if (msg.data.length != 0) {
                    $("#btnPrint").css("display", "inline");
                    if ($("#YearLength").val() == '3') {
                        $.each(msg.data, function(i, item) {
                            if (item.DeptName != null && item.DeptName != "") {

                                $("<tr class='newrow'></tr>").append(
                                "<td height='22px'align='center'>" + item.DeptName + "</td>"
                                + "<td height='22px'align='right'>" + FormatAfterDotNumber(item.Y1, 2) + "</td>"
                                + "<td height='22px'align='right'>" + NumRoundOfSellModule(item.Grow1, 2) + "</td>"
                                + "<td height='22px'align='right'>" + FormatAfterDotNumber(item.Y2, 2) + "</td>"
                                + "<td height='22px'align='right'>" + NumRoundOfSellModule(item.Grow2, 2) + "</td>"
                                + "<td height='22px'align='right'>" + FormatAfterDotNumber(item.Y3, 2) + "</td>"
                                + "<td height='22px'align='right'>" + NumRoundOfSellModule(item.Grow3, 2) + "</td>"
                                ).appendTo($("#tblDetailInfo tbody"));
                            }
                        });
                    }
                    else if ($("#YearLength").val() == '5') {
                        $.each(msg.data, function(i, item) {
                            if (item.DeptName != null && item.DeptName != "") {
                                $("<tr class='newrow'></tr>").append(
                                    "<td height='22px'align='center'>" + item.DeptName + "</td>"
                                    + "<td height='22px'align='right'>" + FormatAfterDotNumber(item.Y1, 2) + "</td>"
                                    + "<td height='22px'align='right'>" + NumRoundOfSellModule(item.Grow1, 2) + "</td>"
                                    + "<td height='22px'align='right'>" + FormatAfterDotNumber(item.Y2, 2) + "</td>"
                                    + "<td height='22px'align='right'>" + NumRoundOfSellModule(item.Grow2, 2) + "</td>"
                                    + "<td height='22px'align='right'>" + FormatAfterDotNumber(item.Y3, 2) + "</td>"
                                    + "<td height='22px'align='right'>" + NumRoundOfSellModule(item.Grow3, 2) + "</td>"
                                    + "<td height='22px'align='right'>" + FormatAfterDotNumber(item.Y4, 2) + "</td>"
                                    + "<td height='22px'align='right'>" + NumRoundOfSellModule(item.Grow4, 2) + "</td>"
                                    + "<td height='22px'align='right'>" + FormatAfterDotNumber(item.Y5, 2) + "</td>"
                                    + "<td height='22px'align='right'>" + NumRoundOfSellModule(item.Grow5, 2) + "</td>"
                                    ).appendTo($("#tblDetailInfo tbody"));
                            }
                        });
                    }
                }
                else {
                    $("#btnPrint").css("display", "none");
                }
            }
            else {
                $("#btnPrint").css("display", "none");
            }


            //页码
            ShowPageBar(
                "divPageClickInfo", //[containerId]提供装载页码栏的容器标签的客户端ID
                "<%= Request.Url.AbsolutePath %>", //[url]
                {
                style: pagerStyle, mark: "DetailListMark",
                totalCount: msg.totalCount,
                showPageNumber: 3,
                pageCount: pageCount,
                currentPageIndex: pageIndex,
                noRecordTip: "没有符合条件的记录",
                preWord: "上一页",
                nextWord: "下一页",
                First: "首页",
                End: "末页",
                onclick: "TurnToPage({pageindex});return false;"
            }
            );
            totalRecord = msg.totalCount;
            $("#txtShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#txtToPage").val(pageIndex);
        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete: function() {
            hidePopup();
            $("#divPageClickInfo").show();
            SetTableRowColor("tblDetailInfo", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
        }
    });
}


/*

*
* 设置数据明细表的行颜色
*/
function SetTableRowColor(elem, colora, colorb, colorc, colord) {
    //获取DIV中 行数据
    var tableTr = document.getElementById(elem).getElementsByTagName("tr");
    for (var i = 0; i < tableTr.length; i++) {
        //设置行颜色
        tableTr[i].style.backgroundColor = (tableTr[i].sectionRowIndex % 2 == 0) ? colora : colorb;
        //设置鼠标落在行上时的颜色
        tableTr[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = colorc;
        }
        //设置鼠标离开行时的颜色
        tableTr[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? colora : colorb;
        }
    }
}


/*
* 排序处理
*/
function OrderBy(orderColum, orderTip) {
    var strUrl = $.trim($("#hiddSearch").val());
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
    TurnToPage(1);
}

//打印
function PrintSubjectsTotal() {
    var strRul = $("#hiddSearch").val();
    window.open("PrintReportByDeptGetGrow.aspx?" + strRul);
}



//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var FindDate = $.trim($("#OrderDate").val()); //发现日期
   
    if (FindDate == '') {
        isFlag = false;
        fieldText = fieldText + "截止年份|";
        msgText = msgText + "请选择截止年份|";
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