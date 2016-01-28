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

//查询
function fnGetSearch() {

    var strUrl = "";
    var OrderDate = $.trim($("#OrderDate").val()); //销售日期

    var SellDeptId = $("#SellDeptId").val();          //部门（对应员工表ID）

    var CurrencyTypeName = $.trim($("#CurrencyType").val());

    if (CurrencyTypeName == '') {
        CurrencyType = '';
    }
    else {
        CurrencyType = $("#CurrencyType").attr("title");
    }

    strUrl = 'OrderDate=' + escape(OrderDate) + '&SellDeptId=' + escape(SellDeptId) + '&CurrencyType=' + escape(CurrencyType) + '&CurrencyTypeName=' + escape(CurrencyTypeName);
   
    return strUrl;
}

/*
* 翻页处理
*/
function TurnToPage(pageIndex) {
    if (!CheckInput()) {
        return;
    }
    
    //设置当前页
    currentPageIndex = pageIndex;

    var strUrl = fnGetSearch() + '&pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy);
    $("#hiddSearch").val(strUrl);
    $.ajax({
        type: "POST", //用POST方式传输
        url: '../../../Handler/OperatingModel/SellManager/ReportByDeptAndMonth.ashx', //目标地址
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

                    $.each(msg.data, function(i, item) {
                        if (item.DeptName != null && item.DeptName != "") {
                            $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center'>" + item.DeptName + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR1, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC1, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC2, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR2, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC3, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR3, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC4, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR4, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC5, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR5, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC6, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR6, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC7, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR7, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC8, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR8, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC9, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR9, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC10, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR10, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC11, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR11, 2) + "</td>"

                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MC12, 2) + "</td>"
                        + "<td height='22' align='right'>" + FormatAfterDotNumber(item.MR12, 2) + "</td>"



                       ).appendTo($("#tblDetailInfo tbody")
                    );
                        }
                    });
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

    $("#divDetail").css("width", "99%");
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
    window.open("PrintReportByDeptAndMonth.aspx?" + strRul);
}



//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var OrderDate = $.trim($("#OrderDate").val()); //销售日期


    if (OrderDate == '') {
        isFlag = false;
        fieldText = fieldText + "销售年份|";
        msgText = msgText + "请选择年份|";
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