var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式

var currentPageIndex = 1;
var action = ""; //操作
var orderBy = ""; //排序字段
$(document).ready(function() {
    IsDiplayOther('GetBillExAttrControl1_SelExtValue');
});

/*
* 查询
*/
function DoSearch(currPage) {

    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    var InNo = document.getElementById("txtInNo").value;
    var BatchNo= document.getElementById("txtBatchNo").value;//批次
    var Title = document.getElementById("txtTitle").value;
    //var FromBillID=$("#txtFromBillID").attr("title");
    var Taker = $("#txtTakerID").val();
    var Checker = $("#txtCheckerID").val();
    var InPutDept = $("#txtDeptID").val();
    var sltBillStatus = document.getElementById("sltBillStatus").value;
    var Executor = $("#txtExecutorID").val();
    var EnterDateStart = document.getElementById("txtEnterDateStart").value;
    var EnterDateEnd = document.getElementById("txtEnterDateEnd").value;
    var StorageID = document.getElementById("sltStorageID").value;
    var UserTaker = $("#UserTaker").val();
    var UserChecker = $("#UserChecker").val();
    var DeptName = $("#DeptName").val();
    var UserExecutor = $("#UserExecutor").val();
    var EFIndex = document.getElementById("GetBillExAttrControl1_SelExtValue").value; //扩展属性select值
    var EFDesc = document.getElementById("GetBillExAttrControl1_TxtExtValue").value; //扩展属性文本框值
    var ProjectID = document.getElementById("HidProjectID").value;
    var SelProjectName = document.getElementById("txtSelProject").value;

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (CompareDate(EnterDateStart, EnterDateEnd) == 1) {
        isFlag = false;
        fieldText = fieldText + "查询时间段|";
        msgText = msgText + "起始时间不能大于终止时间|";
    }
    if (EFIndex == "" && EFDesc != "") {
        isFlag = false;
        fieldText = fieldText + "其他条件|";
        msgText = msgText + "请选择其他条件|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
    }

    var UrlParam = "action=" + action +
   "&InNo=" + escape(InNo) + "&Title=" + escape(Title) + "&BatchNo="+escape(BatchNo) + 
   "&BillStatus=" + escape(sltBillStatus) + "&ProjectID="+escape(ProjectID) + "&SelProjectName="+escape(SelProjectName) + 
   "&Taker=" + escape(Taker) + "&Checker=" + escape(Checker) +
   "&InPutDept=" + escape(InPutDept) +
   "&Executor=" + escape(Executor) +
   "&EnterDateStart=" + escape(EnterDateStart) + "&EnterDateEnd=" + escape(EnterDateEnd) + "&StorageID=" + escape(StorageID) +
   "&UserTaker=" + escape(UserTaker) + "&UserChecker=" + escape(UserChecker) + "&DeptName=" + escape(DeptName) + "&UserExecutor=" + escape(UserExecutor) +
   "&EFIndex=" + escape(EFIndex) +
   "&EFDesc=" + escape(EFDesc) +
   "&Flag=1";

    //设置检索条件
    document.getElementById("hidSearchCondition").value = UrlParam;
    if (currPage == null || typeof (currPage) == "undefined") {
        TurnToPage(1);
    }
    else {
        TurnToPage(parseInt(currPage, 10));
    }
}
/*-----------------------------------------上面是查询-----------------------------------------------------------*/

//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    $("#checkall").attr("checked", false);
    currentPageIndex = pageIndex;
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;

    var UrlParam = "pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderBy=" + orderBy + "&" + searchCondition;
    //进行查询获取数据 

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/StorageManager/StorageInOtherList.ashx?' + UrlParam, //目标地址
        cache: false,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='OptionCheck_" + item.ID + "' name='Checkbox1'  value=" + item.ID + " onclick=IfSelectAll('Checkbox1','checkall')  type='checkbox'/>" + "</td>" +
                        "<td height='22' align='center' title=\"" + item.InNo + "\"><a href='" + GetLinkParam() + "&InNoID=" + item.ID + "'>" + fnjiequ(item.InNo, 16) + "</a></td>" +
                        "<td height='22' align='center' title=\"" + item.Title + "\">" + fnjiequ(item.Title, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.TakerName + "\">" + fnjiequ(item.TakerName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.CheckerName + "\">" + fnjiequ(item.CheckerName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.InPutDept + "\">" + fnjiequ(item.InPutDept, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ExecutorName + "\">" + fnjiequ(item.ExecutorName, 10) + "</td>" +
                        "<td height='22' align='center' >" + item.EnterDate + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ReasonTypeName + "\">" + fnjiequ(item.ReasonTypeName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.CountTotal + "\">" + NumberSetPoint(item.CountTotal) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.TotalPrice.slice(0, item.TotalPrice.length - 2) + "\" style=\"display:" + IsDisplayPrice + "\">" + NumberSetPoint(item.TotalPrice) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.Summary + "\">" + fnjiequ(item.Summary, 10) + "</td>" +
                        "<td height='22' align='center' >" + item.BillStatusName + "</td>").appendTo($("#pageDataList1 tbody"));

            });

            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
            document.getElementById("Text2").value = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.getElementById("Text2").value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });
}
//table行颜色
function pageDataList1(o, a, b, c, d) {
    var t = document.getElementById(o).getElementsByTagName("tr");
    for (var i = 1; i < t.length; i++) {
        t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
        t[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = c;
        }
        t[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
        }
    }
}

function Fun_Search_StorageInOther(aa) {
    search = "1";
    TurnToPage(1);
}
function Fun_ClearInput() {
    document.getElementById('txtInNo').value = "";
    document.getElementById('txtTitle').value = "";
    //document.getElementById('txtFromBillID').value = "";
    //$("#txtFromBillID").attr('title',"");
    document.getElementById('txtTaker').value = "";
    $("#txtTaker").attr('title', "");
    document.getElementById('txtChecker').value = "";
    $("#txtChecker").attr('title', "");
    document.getElementById('txtInPutDept').value = "";
    $("#txtInPutDept").attr('title', "");
    $("#sltBillStatus").val("");
    document.getElementById('txtExecutor').value = "";
    $("#txtExecutor").attr('title', "");
    document.getElementById('txtEnterDateStart').value = "";
    document.getElementById('txtEnterDateEnd').value = "";
    document.getElementById('sltStorageID').value = "";

}
function Ifshow(count) {
    if (count == "0") {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
    }
    else {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagecount").style.display = "block";
    }
}

function SelectDept(retval) {
    alert(retval);
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {


    //判断是否是数字
    if (!PositiveInteger(newPageCount)) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "每页显示应为正整数！");
        return;
    }
    if (!PositiveInteger(newPageIndex)) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数应为正整数！");
        return;
    }

    if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
        return false;
    }
    else {
        this.pageCount = parseInt(newPageCount, 10);
        TurnToPage(parseInt(newPageIndex, 10));
    }
}
//排序
function OrderBy(orderColum, orderTip) {
    if (document.getElementById("hidSearchCondition").value == "" || document.getElementById("hidSearchCondition").value == null) return;
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
    $("#txtorderBy").val(orderBy); //把排序字段放到隐藏域中，

    TurnToPage(1);
}

function AddPop() {
    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/extanim64.gif", "数据处理中，请稍候……");
}
function showPopup(img, img1, retstr) {
    document.all.Forms.style.display = "block";
    document.all.Forms.innerHTML = Create_Div(img, img1, true);
    document.all.FormContent.innerText = retstr;
}
function hidePopup() {
    document.all.Forms.style.display = "none";
}
function Create_Div(img, img1, bool) {
    FormStr = "<table width=100% height='104' border='0' cellpadding=0 cellspacing=0 bgcolor=#FFFFFF>"
    FormStr += "<tr>"
    FormStr += "<td width=90%  bgcolor=#3F6C96>&nbsp;</td>"
    FormStr += "<td width=10% height=20 bgcolor=#3F6C96>"
    if (bool) {
        FormStr += "<img src='" + img + "' style='cursor:hand;display:block;' id='CloseImg' onClick=document.all['Forms'].style.display='none';>"
    }
    FormStr += "</td></tr><tr><td height='1' colspan='2' background='../../../Images/Pic/bg_03.gif'></td></tr><tr><td valign=top height=104 id='FormsContent' colspan=2 align=center>"
    FormStr += "<table width=100% border=0 cellspacing=0 cellpadding=0>"
    FormStr += "<tr>"
    FormStr += "<td width=25% align='center' valign=top style='padding-top:20px;'>"
    FormStr += "<img name=exit src='" + img1 + "' border=0></td>";
    FormStr += "<td width=75% height=50 id='FormContent' style='padding-left:5pt;padding-top:20px;line-height:17pt;' valign=top></td>"
    FormStr += "</tr></table>"
    FormStr += "</td></tr></table>"
    return FormStr;
}

var OptionCheckArray = new Array();
function OptionCheckOnes(obj) {
    with (obj) {
        if (checked) {
            OptionCheckArray.push(value);
        }
        else {
            var OptionCheckArrayTM = new Array();
            for (var i = 0; i < OptionCheckArray.length; i++) {
                if (OptionCheckArray[i] != value) {
                    OptionCheckArrayTM.push(OptionCheckArray[i]);
                }
            }
            OptionCheckArray = OptionCheckArrayTM;
        }
    }
}

function Fun_Delete_StorageInOther() {

    if (confirm('删除后不可恢复，你确定要删除吗？')) {
        var ck = document.getElementsByName("Checkbox1");
        var ck2 = "";
        for (var i = 0; i < ck.length; i++) {
            if (ck[i].checked) {
                ck2 += ck[i].value + ',';
            }
        }
        var IDArray = ck2.substring(0, ck2.length - 1);
        x = ck2.split(',');
        if (x.length - 1 <= 0) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请至少选择一项删除！");
            return;
        }
        else {
            var Act = 'Del';
            var UrlParam = "Act=" + Act + "\
                                &strID=" + IDArray;
            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/StorageManager/StorageInOtherAdd.ashx?" + UrlParam,
                dataType: 'json', //返回json格式数据
                cache: false,
                beforeSend: function() {
                    //AddPop();
                },
                //complete :function(){ //hidePopup();},
                error: function() {
                    popMsgObj.ShowMsg('请求发生错误');

                },
                success: function(data) {
                    popMsgObj.ShowMsg(data.info);

                    if (data.sta > 0) {
                        Fun_Search_StorageInOther();
                    }
                }
            });
        }
    }
    else {
        return false;
    }
}

function fnSelectMTInfo() {
    popManufactureTaskObj.ShowList('txtFromBillID');
}


/*------------------------------------新建-----------------------------------*/
/*
* 新建
*/
function DoNew() {
    window.location.href = GetLinkParam();
}

/*
* 获取链接的参数
*/
function GetLinkParam() {
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    linkParam = "StorageInOtherAdd.aspx?ModuleID=" + ModuleID
                            + "&pageIndex=" + currentPageIndex + "&pageCount=" + pageCount + "&orderBy=" + orderBy + "&" + searchCondition;
    //返回链接的字符串
    return linkParam;
}
/*------------------------------------新建-----------------------------------*/

/*--------------------------------Start 全选---------------------------------------------*/
//全选
function SelectAllCk() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}
/*--------------------------------End 全选---------------------------------------------*/

function NumberSetPoint(num)
{
    var SetPoint = parseFloat(num).toFixed($("#hidSelPoint").val());
    return SetPoint;
}