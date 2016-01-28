//站点用户设置

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var flag = "";
var ActionFlag = ""
var str = "";
var currentPageIndex = 1;
var action = ""; //操作
var orderBy = ""; //排序字段

// 界面加载时
$(document).ready(function() {
    SearchData();
});


//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {

    //    var customName = escape($("#txtSCustom").val());
    //    var loginName = escape($("#txtSName").val());

    //    if (!CheckSpecialWord(customName)) {
    //        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "客户名称不能包含特殊字符");
    //        return;
    //    }

    //    if (!CheckSpecialWord(loginName)) {
    //        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "站点登陆用户名不能包含特殊字符");
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


    var para = "action=searchlist"
            + "&pageIndex=" + pageIndex
            + "&pageCount=" + pageCount
            + "&orderby=" + orderBy
            + "&CustomName=" + escape($("#txtSCustom").val())
            + "&LoginUserName=" + escape($("#txtSName").val())
            + "&IsMember=" + escape($("#selUse").val())
            + "&Status=" + escape($("#selSStatus").val());
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/CustomWebSiteManager/WebSiteCustomInfoAdd.ashx', //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() {
            AddPop();
            $("#pageDataList1_Pager").hide();
        }, //发送数据之前
        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1' name='Checkbox1'  value='" + item.ID + "' onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/></td>" +
                    "<td height='22' align='center'>" + item.CustName + "</td>" +
                    "<td height='22' align='center'><a href='#' onclick=EditGU(" + item.ID + ",'" + item.CustName + "')>" + item.LoginUserName + "</a></td>" +
                    "<td height='22' align='center'>" + ConvertIsMember(item.IsMember) + "</td>" +
                    "<td height='22' align='center'>" + ConvertStatus(item.Status) + "</td>").appendTo($("#pageDataList1 tbody"));
            });
            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
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
            howPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        complete: function() {
            hidePopup();
            $("#pageDataList1_Pager").show();
            Ifshow(document.getElementById('Text2').value);
            pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
            $("#btnAll").attr("checked", false);
        } //接收数据完毕
    });
}

// 状态转化
function ConvertIsMember(status) {
    if (status == "1") {
        return "是";
    }
    else {
        return "否";
    }
}

// 状态转化
function ConvertStatus(status) {
    if (status == "1") {
        return "启用";
    }
    else {
        return "停用";
    }
}

// 编辑修改
function EditGU(id, CustName) {
    // 显示界面
    ShowDiv();
    $("#hidID").val(id);
    var para = "action=search"
            + "&ID=" + escape($("#hidID").val());
    $.ajax
    ({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/CustomWebSiteManager/WebSiteCustomInfoAdd.ashx', //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() {
            AddPop();
        }, //发送数据之前
        success: function(msg) {
            $.each(msg.data, function(i, item) {
                $("#txtCustomName").val(CustName);
                $("#hidCustomID").val(item.CustomID);
                $("#txtName").val(item.LoginUserName);
                $("#txtPwd").val(item.LoginPassword);
                $("#selStatus").val(item.Status);
                if (item.IsMember == "1") {
                    $("#rdUse").attr("checked", "checked");
                }
                else {
                    $("#rdNotUse").attr("checked", "checked");
                }
            });
        },
        error: function() { },
        complete: function() {
            hidePopup();
        } //接收数据完毕
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

// 查询数据
function SearchData() {
    TurnToPage(1);
}

// 删除数据
function DeleteData() {
    var temp = "";
    var id = new Array();
    $("#pageDataList1 tbody").find("tr.newrow").find("input[type='checkbox']").each(function(i) {
        if ($(this).attr("checked")) {
            id.push(escape($(this).val()));
        }
    });
    if (id.length < 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "删除列表信息至少要选择一项！");
        return;
    }
    if (!window.confirm("确认执行删除操作吗？")) {
        return;
    }

    var para = "action=delete"
            + "&ID=" + id.toString();
    $.ajax({
        type: "POST",
        url: '../../../Handler/Office/CustomWebSiteManager/WebSiteCustomInfoAdd.ashx', //目标地址
        dataType: 'json', //返回json格式数据
        data: para, //数据
        cache: false,
        beforeSend: function() {
        },
        error: function(e) {
            popMsgObj.ShowMsg('请求发生错误');
        },
        success: function(data) {
            if (data.sta == 1) {
                popMsgObj.ShowMsg(data.info);
                SearchData();
                $("#btnAll").attr("checked", false);
            }
            else {
                popMsgObj.ShowMsg(data.info);
            }
        }
    });
}

function Ifshow(count) {
    if (count == "0") {
        document.getElementById('divpage').style.display = "none";
        document.getElementById('pagecount').style.display = "none";
    }
    else {
        document.getElementById('divpage').style.display = "block";
        document.getElementById('pagecount').style.display = "block";
    }
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
// 排序
function OrderBy(orderColum, orderTip) {
    var ordering = "a";
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

// 隐藏新增修改界面
function Hide() {
    CloseDiv();
    document.getElementById('divAdd').style.display = 'none';
}

//关闭遮挡界面
function CloseDiv() {
    closeRotoscopingDiv(false, 'divBackShadow');
}

// 显示新增修改界面
function ShowDiv() {
    openRotoscopingDiv(false, 'divBackShadow', 'BackShadowIframe');
    document.getElementById('divAdd').style.display = 'block';
    document.getElementById("divBackShadow").style.zIndex = "1";
    document.getElementById("divAdd").style.zIndex = "2";
    $("#txtCustomName").val("");
    $("#txtName").val("");
    $("#txtPwd").val("");
    $("#hidID").val("");
    $("#hidCustomID").val("");
    $("#rdNotUse").attr("checked", "checked");
    $("#selStatus").val("1");
}

// 打开新增修改界面
function ShowAdd(isEdit) {
    //界面处理
    ShowDiv();
}

//全选
function SelectAll() {
    if ($("#checkall").attr("checked")) {
        $("#dg_Log tbody").find("input[type='checkbox']").each(function(i) {
            $(this).attr("checked", true);
        });
    }
    else {
        $("#dg_Log tbody").find("input[type='checkbox']").each(function(i) {
            $(this).attr("checked", false);
        });
    }
}

// 保存数据
function SaveData() {
    if (!CheckData()) {
        return;
    }
    var para = "action=save"
            + "&ID=" + $("#hidID").val()
            + "&CustomID=" + escape($("#hidCustomID").val())
            + "&LoginUserName=" + escape($("#txtName").val())
            + "&LoginPassword=" + escape($("#txtPwd").val())
            + "&Status=" + $("#selStatus").val()
            + "&IsMember=" + $("#rdUse").attr("checked");

    $.ajax
    ({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/CustomWebSiteManager/WebSiteCustomInfoAdd.ashx', //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() {
            AddPop();
        }, //发送数据之前
        success: function(msg) {
            if (msg.sta == "1") {
                popMsgObj.ShowMsg(msg.info);
                Hide();
                SearchData();
            }
            else {
                if (msg.sta == "2") {
                    $("#txtName").focus();
                }
                popMsgObj.ShowMsg(msg.info);
            }
        },
        error: function() { },
        complete: function() {
            hidePopup();
        } //接收数据完毕
    });
}


function CheckData() {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var temp = $("#hidCustomID").val();
    var iRow;
    if (temp == "") {
        isFlag = false;
        fieldText = fieldText + "客户名称|";
        msgText = msgText + "请选择客户|";
    }
    var temp = $("#txtName").val();
    if (temp == '') {
        isFlag = false;
        fieldText = fieldText + "站点登陆用户名|";
        msgText = msgText + "请输入站点登陆用户名|";
    }
    if (strlen(temp) > 50) {
        isFlag = false;
        fieldText = fieldText + "站点登陆用户名|";
        msgText = msgText + "站点登陆用户名仅限于50个字符以内|";
    }
    var temp = $("#txtPwd").val();
    if (temp == '') {
        isFlag = false;
        fieldText = fieldText + "站点登陆密码|";
        msgText = msgText + "请输入站点登陆密码|";
    }
    if (strlen(temp) > 50) {
        isFlag = false;
        fieldText = fieldText + "站点登陆密码|";
        msgText = msgText + "站点登陆密码仅限于50个字符以内|";
    }

    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return false;
    }
    return true;
}
// 查询界面全选
function OptionCheckAll() {
    if (document.getElementById("btnAll").checked) {
        var ck = document.getElementsByName("Checkbox1");
        for (var i = 0; i < ck.length; i++) {
            ck[i].checked = true;
        }
    }
    else {
        var ck = document.getElementsByName("Checkbox1");
        for (var i = 0; i < ck.length; i++) {
            ck[i].checked = false;
        }
    }
}

// 客户选择
function SelectCust() {
  
    popSellCustObj.returnName = true;
    popSellCustObj.ShowList("protion");
    //    var url = "../../../Pages/Office/StorageManager/StorageReportCust.aspx?Custom=1";
    //    var returnValue = window.showModalDialog(url, "", "dialogWidth=300px;dialogHeight=450px");
    //    if(returnValue!="" && returnValue!=null && returnValue!='clear')
    //    {
    //        var  value = returnValue.split("|");
    //        document.getElementById("txtCustomName").value=value[1].toString();
    //        document.getElementById('hidCustomID').value=value[0].toString();
    //    }
    //    else if(returnValue=='clear')
    //    {
    //        document.getElementById("txtCustomName").value='';
    //        document.getElementById('hidCustomID').value='';
    //    }
}


function fnSelectCust(id, custName) {
    $("#txtCustomName").val(custName);
    $("#hidCustomID").val(id);
    closeSellModuCustdiv();
}

