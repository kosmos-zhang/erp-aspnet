var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var flag = "";
var ActionFlag = ""
var str = "";
var currentPageIndex = 1;
var action = ""; //操作
var orderBy = ""; //排序字段
var pageCount = 10;


/* 保留小数位数 */
function ToFixed(num) {
    var point = $("#DigitalLength").val();
    return parseFloat(num).toFixed(point);
}


// 界面加载时
$(document).ready(function() {
    //    SearchData();
getPreYearMonth();
fnGetExtAttr(); //物品控件拓展属性
});



/* 显示计算层 */
function showCalculationLayer() {
    $("#divAdd").css("display", "");
}

/* 隐藏计算层 */
function hideCalculationLayer() {
    $("#divAdd").css("display", "none");
}


/* 根据选择的期数 获得开始日期和结束日期 */
function getPreYearMonth() {
    var params = "action=getpreyearmonth&yearMonth=" + $("#selYearMonthC").val();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: '../../../Handler/Office/StorageManager/StorageCost.ashx',
        cache: false,
        data: params,
        beforeSend: function() {
        },
        success: function(msg) {
            if (msg.result) {
                $("#txtStartDateC").val(msg.data.split('|')[0]);
                $("#txtEndDateC").val(msg.data.split('|')[1]);
            }

        },
        error: function() {
        },
        complete: function() {
        }
    });

}

//计算存货成本
function CalculationStorageCost() {
    var yearMonth = $("#selYearMonthC").val();
    var startDate = $("#txtStartDateC").val();
    var endDate = $("#txtEndDateC").val();

    var title = "", msg = "";
    if (startDate == "") {
        title += "开始日期|";
        msg += "开始日期不能为空|";
    }
    if (endDate == "") {
        title += "结束日期|";
        msg += "结束日期不能为空|";
    }

    if (title != "" && msg != "") {
        popMsgObj.Show(title, msg);
    }

    var params = "action=calculation" +
                         "&yearMonth=" + yearMonth +
                         "&StartDate=" + startDate +
                         "&EndDate=" + endDate;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: '../../../Handler/Office/StorageManager/StorageCost.ashx',
        cache: false,
        data: params,
        beforeSend: function() {
        },
        success: function(msg) {
            popMsgObj.Show("计算存货成本|", msg.data + "|");
        },
        error: function() {
            popMsgObj.Show("计算存货成本|", "计算存货成发生错误|");
        },
        complete: function() {
        }
    });


}



//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {

    //    var orderNo = escape($("#txtOrderNo").val());

    //    if (!CheckSpecialWord(orderNo)) {
    //        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "订单编号不能包含特殊字符");
    //        return;
    //    }

    var isFlag = true;
    var RetVal = CheckSpecialWords();
    var fieldText = "";
    var msgText = "";
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
        //showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "订单编号不能包含特殊字符");
    }



    var para = "action=get" +
                   "&pageIndex=" + pageIndex +
                   "&pageCount=" + pageCount +
                   "&orderby=" + $("#txtOrderBy").val() +
                   "&ProductID=" + escape($("#txtProductID").val()) +
                   "&StartYearMonth=" + escape($("#txtStartYearMonth").val()) +
                   "&OrderDateEnd=" + escape($("#txtEndYearMonth").val());
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/StorageManager/StorageCost.ashx', //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() {
            AddPop();
            $("#pageDataList1_Pager").hide();
        }, //发送数据之前
        success: function(msg) {

            //数据获取完毕，填充页面据显示
            //数据列表 createTd( "<input id='Checkbox1' name='Checkbox1'  value='"+item.ID+"' onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>")+
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                $("<tr class='newrow'></tr>").append(createTd(item.YearMonth) +
                   createTd(item.ProdNo) +
                  createTd(item.ProductName) +
                  createTd(item.Specification) +
                  createTd(item.UnitName) +
                  createTd(item.ColorName) +
                  createTd(ToFixed(item.PeriodBeginCost)) +
                  createTd(ToFixed(item.PeriodBeginCount)) +
                  createTd(ToFixed(item.LastTotalPrice)) +
                  createTd(ToFixed(item.PeriodEndCost) + "<a href=\"javascript:void(0);\" onclick=\"editPeriodEndCost(" + item.ID + ",'" + item.PeriodEndCost + "'," + item.PeriodEndCount + "," + i + ");\">修改</a>") +
                  createTd(ToFixed(item.PeriodEndCount)) +
                  createTd("<span id=\"span_CurrentTotalPrice_" + i.toString() + "\">" + ToFixed(item.CurrentTotalPrice) + "</span>")).appendTo($("#pageDataList1 tbody"));
            });
            //页码
            ShowPageBar("pageDataList1_PagerList", //[containerId]提供装载页码栏的容器标签的客户端ID
            "<%= Request.Url.AbsolutePath %>", //[url]
            {style: pagerStyle, mark: "pageDataList1Mark",
            totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
            onclick: "TurnToPage({pageindex});return false;"}//[attr]
            );
            totalRecord = msg.totalCount;
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            document.getElementById('Text2').value = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex);
            $("#ToPage").val(pageIndex);
        },
        error: function() {
            popMsgObj.Show("存货成本列表|", "读取存货成本列表失败|");
        },
        complete: function() {
            hidePopup();
            $("#pageDataList1_PagerList").show();
            pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
            $("#btnAll").attr("checked", false);
        } //接收数据完毕
    });
}

var fid = 0, index = 0, fcount = 0;

function editPeriodEndCost(id, cost, count, i) {
    $("#divEdit").css("display", "");
    $("#txtPeriodCost").val(ToFixed(cost));
    fid = id;
    fcount = count;
    index = i;
}

function hideEditCost() {
    $("#divEdit").css("display", "none");
}


/* 修改期末成本 */
function editEndCost() {

    var cost = $("#txtPeriodCost").val();

    if (!IsNumOrFloat(cost, false)) {
        popMsgObj.Show("期末成本|", "期末成本格式不正确|");
        return;
    }


    var params = "action=edit" +
                         "&cost=" + cost +
                         "&id=" + fid;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: '../../../Handler/Office/StorageManager/StorageCost.ashx',
        cache: false,
        data: params,
        beforeSend: function() {
        },
        success: function(msg) {
            if (msg.result) {
                popMsgObj.Show("期末成本|", "调整期末成本成功|");
                $("#span_CurrentTotalPrice_" + index).html(ToFixed(parseFloat(cost) * parseFloat(fcount)));
            }
            else
                popMsgObj.Show("期末成本|", "调整期末成本失败|");

        },
        error: function() {
        },
        complete: function() {
        }
    });

}



/* 获取订单信息 */
function getInfo(ordeno) {
    window.location.href = "WebSiteOrderInfo.aspx?OrderNo=" + ordeno;
}

/*构造td*/
function createTd(value) {
    return "<td align=\"center\" height='22' >" + value + "</td>";
}

/*设置排序字段*/
function CreateSort(control) {

    var ordering = document.getElementById("txtOrderBy");
    var obj = document.getElementById(control);
    var allOrderTipDOM = $(".orderTip");
    allOrderTipDOM.empty();
    if (ordering.value == (control + " ASC")) {
        ordering.value = control + " DESC";
        obj.innerHTML = "↓";
    }
    else {
        ordering.value = control + " ASC";
        obj.innerHTML = "↑";
    }
    TurnToPage(1);
}


//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {
    if (!IsZint(newPageCount)) {
        popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
        return;
    }
    if (!IsZint(newPageIndex)) {
        popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
        return;
    }
    if (newPageCount <= 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "显示页数超出显示范围！");
        return false;
    }
    if (newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
        return false;
    }
    else {
        this.pageCount = parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
        $("#btnAll").attr("checked", false);
    }
}


/* 填充物品 */
function Fun_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, CodeName, TaxRate, SellTax, Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, GroupUnitNo, SaleUnitID, SaleUnitName, InUnitID, InUnitName, StockUnitID, StockUnitName, MakeUnitID, MakeUnitName, IsBatchNo) {
    $("#txtProductID").val(ID);
    $("#txtProductName").val(ProductName);
}

/* 选择物品 */
function getProductInfo() {
    popTechObj.ShowList('txtProductName','txtProductName','txtProductID');
}

/* 清除选择的物品  */
function ClearPkroductInfo() {
    $("#txtProductID").val("");
    $("#txtProductName").val("");
    closeProductdiv();
}