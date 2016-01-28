var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var currentPageIndex = 1;
var orderBy = ""; //排序字段

$(document).ready(function() {
   TurnToPage(1);
});


function TurnToPage(pageIndex) {
     
    currentPageIndex = pageIndex;
    var Action=$("#HAction").val();
    var DateType=$("#HDateType").val();
    var BeginDate=$("#HBeginDate").val();
    var EndDate=$("#HEndDate").val();
    var DeptId=$("#HDeptId").val();
    var ProviderId=$("#HProviderId").val();
    var DateValue=$("#HDateValue").val();
    var ProductId=$("#HProductId").val();
    var strUrl ='ProductId='+escape(ProductId)+'&DateType='+escape(DateType)+ '&StartDate='+escape(BeginDate)+ '&EndDate='+escape(EndDate)+ '&DeptId='+escape(DeptId)+ '&ProviderId='+escape(ProviderId)+ '&DateValue='+escape(DateValue)+'&pageIndex=' + pageIndex + '&PageSize=' + pageCount + '&orderby=' + escape(orderBy) +'&action='+Action;
        
    $.ajax({

        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/OperatingModel/PurchaseManager/PurchaseRejectProduct.ashx', //目标地址
        cache: false,
        data: strUrl,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前
        success: function(msg) {
               //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.OrderNo != null && item.OrderNoNO != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center' id='OrderNo"+ (i + 1) +"' title=\""+item.OrderNo+"\" >"+ GetStandardString(item.OrderNo,25) +"</td>"+
                        "<td height='22' align='center' title=\""+item.ProductNo+"\">"+ GetStandardString(item.ProductNo,15) +"</td>"+
                        "<td height='22' align='center'>"+ item.ProductName +"</td>"+
                        "<td height='22' align='center'>" + (parseFloat(item.ProductCount)).toFixed($("#hidPoint").val()) + "</td>" + 
                        "<td height='22' align='center'>"+ (parseFloat(item.UnitPrice)).toFixed($("#hidPoint").val()) +"</td>"+  
                        "<td height='22' align='center'>"+  (parseFloat(item.TotalPrice)).toFixed($("#hidPoint").val()) +"</td>"+
                        "<td height='22' align='center'>"+(parseFloat(item.TaxRate)).toFixed($("#hidPoint").val())+"</td>"+
                        "<td height='22' align='center'>"+ item.BillStatusName+"</td>"
                        ).appendTo($("#pageDataList1 tbody"));
                   });
                   
            //页码
            ShowPageBar("pageDataList1_Pager",
                   "<%= Request.Url.AbsolutePath %>",
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pageSellOffcount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() {},
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.form1.elements["Text2"].value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
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



function Ifshow(count) {
    if (count == "0") {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pageSellOffcount").style.display = "none";
    }
    else {
        document.getElementById("divpage").style.display = "";
        document.getElementById("pageSellOffcount").style.display = "";
    }
}



function fnOrderInfo(retval, isRef) {
    window.location.href = '../../../Pages/Office/SellManager/SellOrderMod.aspx?id=' + escape(retval) + "&islist=1&ModuleID=2031401&isRef=" + isRef;
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {

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

function OrderBy(orderColum, orderTip) {

    var ordering = "d";
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
    TurnToPage(1);
}
