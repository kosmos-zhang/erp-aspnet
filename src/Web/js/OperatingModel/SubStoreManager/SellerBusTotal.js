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
    var SellDate = $.trim($("#txtStartDate").val()); //销售日期
      var SellEndDate = $.trim($("#txtEndDate").val()); //销售日期
    var DeptID = $.trim($("#DeptID").val()); //
       var UserSellerName = $.trim($("#txtUserSellerName").val()); //
         var DeptName=$.trim($("#DeptNameSell").val()); 
    strUrl = 'SellDate=' + escape(SellDate) + '&DeptID=' + escape(DeptID)+'&SellEndDate=' + escape(SellEndDate)+'&UserSellerName=' + escape(UserSellerName);
    $("#hiddSearch").val(strUrl);
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
     var flag=$("#hf_flag").val();
    currentPageIndex = pageIndex;
    var strUrl = fnGetSearch() + '&pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy);
    $.ajax({
        type: "POST", //用POST方式传输
        url: '../../../Handler/OperatingModel/SubStoreManager/SellerBusTotal.ashx', //目标地址
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
                    $.each(msg.data, function(i, item) {if(item.Seller != null && item.Seller != "") {var Fee=parseFloat(item.SellFee);var temp='';if(flag!='0')temp="<td height='22' align='center'>" + item.DeptName + "</td>";
                        $("<tr class='newrow'></tr>").append(""+temp+""
                        + "<td height='22' align='center'>" + item.EmployeeName + "</td>"
                                      + "<td height='22' align='center'>" +FormatAfterDotNumber(Fee,selPoint) + "</td>"
                       ).appendTo($("#tblDetailInfo tbody")
                    );}
                      else {
                    $("#btnPrint").css("display", "none");}
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
 var ordering = "a";
function OrderBy(orderColum, orderTip) {
        if(parseFloat(totalRecord)<1)
        {
          return false;
        }
   ordering = "a";
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
   if(ordering=="a")
    {
        ordering="asc";
    }
    else
    {
        ordering="desc"
    }
    orderBy=orderBy.split('_')[0];
    var strRul = $("#hiddSearch").val();
      var flag=$("#hf_flag").val();
      if(flag=="0")
   { window.open("PrintOfficeSellerBusTotal.aspx?orderby="+orderBy+"&ordering="+ordering+"&" + strRul);}
   else if(flag=="1")
     { window.open("PrintSellerSellTotal.aspx?orderby="+orderBy+"&ordering="+ordering+"&" + strRul);}
    
}


//表单验证
function CheckInput() {
   var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var DeptID = $.trim($("#DeptID").val()); //业务员ID（对应员工表ID）
    var FindDate = $.trim($("#txtStartDate").val()); //发现日期
    var FindDate1 = $.trim($("#txtEndDate").val()); //发现日期
       var UserSellerName = $.trim($("#txtUserSellerName").val()); //
//    if (DeptID == '') {
//        isFlag = false;
//        fieldText = fieldText + "分店名称|";
//        msgText = msgText + "请选择分店名称|";
//    }
//      if (UserSellerName == '') {
//        isFlag = false;
//        fieldText = fieldText + "业务员";
//        msgText = msgText + "请选择业务员|";
//    }
    if (FindDate == '') {
        isFlag = false;
        fieldText = fieldText + "起始日期|";
        msgText = msgText + "请选择起始日期|";
    }

    if (FindDate1 == '') {
        isFlag = false;
        fieldText = fieldText + "结束日期|";
        msgText = msgText + "请选择结束日期|";
    }
    if (FindDate != '' && FindDate1 != '') {
        if (CompareDate(FindDate, FindDate1) == 1) {
            isFlag = false;
            fieldText = fieldText + "日期|";
            msgText = msgText + "结束日期不能早于起始日期|";
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
