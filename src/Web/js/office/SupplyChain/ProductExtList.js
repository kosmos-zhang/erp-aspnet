$(document).ready(function() {
    TurnToPage(1);
});

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式

var currentPageIndex = 1;
var orderBy = ""; //排序字段
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    if (!CheckInput1()) {
        return;
    }
    currentPageIndex = pageIndex;
    $("#checkall").removeAttr("checked")
    var EFDesc = $.trim($("#EFDesc").val());
    var EFType = $("#EFType").val();
    var action = "list";
    var strUrl = 'EFDesc=' + escape(EFDesc) + '&EFType=' + escape(EFType) + '&pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy) +
               '&action=' + escape(action);
    $("#hiddUrl").val(strUrl);

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/SupplyChain/ProductExtList.ashx", //目标地址
        cache: false,
        data: strUrl,

        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {

                    var tt = item.EFValueList.replace(' ','');

                    $("<tr class='newrow'></tr>").append(
                    "<td height='22' align='center'>" + "<input id='chk" + i + "'  value='" + item.ID + "' onclick = 'fnUnSelect(this)'   type='checkbox'/>" + "</td>" +
                    "<td height='22' align='center'><a href='#' onclick=fnUpdate('" + item.ID + "','" + item.EFDesc + "','" +
                    item.EFIndex + "','" + item.EFType + "','" + tt + "')>" + item.EFDesc + "</a></td>" +
                    "<td height='22' align='center'> " + item.EFTypeText + "</td>" +
                    "<td height='22' align='center'>" + item.EFValueList + "</td>"
                    ).appendTo($("#pageDataList1 tbody"));
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
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.frmMain.elements["Text2"].value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });
}

//更新打开窗口
function fnUpdate(ID, EFDesc, EFIndex, EFType, EFValueList) {
    $("#hiddAction").val('update');
    openRotoscopingDiv(false, 'divBackShadow', 'BackShadowIframe');
    document.getElementById("div_Add").style.zIndex = "2";
    document.getElementById("divBackShadow").style.zIndex = "1";
    document.getElementById('div_Add').style.display = 'block';
    $("#hiddID").val(ID);
    $("#hiddEFIndex").val(EFIndex);
    $("#EFDescUC").val(EFDesc);
    $("#EFTypeUC").val(EFType);
    $("#EFValueListUC").val(EFValueList);
    $("#HdEFDesc").val(EFDesc);
    fnChange();
}


//去除全选按钮
function fnUnSelect(obj) {

    if (!obj.checked) {
        $("#checkall").removeAttr("checked");
        return;
    }

    else {
        //验证明细信息
        var signFrame = findObj("pageDataList1", document);
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

//全选
function selectall() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
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

    TurnToPage(1);
}

//新建时显示弹出层
function Show() {
    $("#hiddAction").val('insert');
    openRotoscopingDiv(false, 'divBackShadow', 'BackShadowIframe');
    document.getElementById("div_Add").style.zIndex = "2";
    document.getElementById("divBackShadow").style.zIndex = "1";
    document.getElementById('div_Add').style.display = 'block';
}

//隐藏弹出层
function Hide() {
    CloseDiv();
    document.getElementById('div_Add').style.display = 'none';
    Clear();
}

//清空弹出层内容
function Clear() {
    $("#EFDescUC").val('');
    $("#EFTypeUC").val('1');
    $("#EFValueListUC").val('');
    fnChange();
}

//关闭遮罩层
function CloseDiv() {
    closeRotoscopingDiv(false, 'divBackShadow');
}

//
function fnChange() {
    var value = $("#EFTypeUC").val();
    //下拉列表时时输入框可用
    if (value == '2') {
        $("#EFValueListUC").removeAttr("disabled");
    }
    else {
        $("#EFValueListUC").val('');
        $("#EFValueListUC").attr("disabled", "disabled");
    }
}

///删除销售机会
function fnDel() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var DetailID = '';
    var pageDataList1 = findObj("pageDataList1", document);
    for (i = 0; i < pageDataList1.rows.length; i++) {
        if ($("#chk" + i).attr("checked")) {
            DetailID += $("#chk" + i).val() + ',';

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
                    url: "../../../Handler/Office/SupplyChain/ProductExtList.ashx",
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

//保存数据
function Insert() {
    if (!CheckInput()) {
        return;
    }
    var ID = $("#hiddID").val();
    var action = $("#hiddAction").val();
    var EFDesc = $.trim($("#EFDescUC").val());
    var EFType = $("#EFTypeUC").val();
    var EFValueList = $.trim($("#EFValueListUC").val().replace(' ',''));
    var EFIndex = $("#hiddEFIndex").val();
    var HdEFDesc=$("#HdEFDesc").val();
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SupplyChain/ProductExtList.ashx",
        data: 'action=' + escape(action) + '&ID=' + escape(ID) + '&HdEFDesc=' + escape(HdEFDesc) + '&EFDesc=' + escape(EFDesc) + '&EFType=' + escape(EFType) +
        '&EFValueList=' + escape(EFValueList) + '&EFIndex=' + escape(EFIndex),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { hidePopup(); showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            hidePopup();
            if(data.sta=="1")
            {popMsgObj.ShowMsg(data.Msg);}
            else if(data.sta=="0")
            {
             popMsgObj.ShowMsg("描述不允许重复！");
            }
        }

    });
    Hide();
    TurnToPage(1);
}



//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var EFValueList = $.trim($("#EFValueListUC").val()); //应对策略
    var value = $("#EFTypeUC").val();
    var EFDesc = $.trim($("#EFDescUC").val());


    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (EFDesc == '') {
        isFlag = false;
        fieldText = fieldText + "物品特性描述|";
        msgText = msgText + "请输入物品特性描述|";
    }

    //下拉列表时时输入框可用
    if (value == '2') {
        if (EFValueList == '') {
            isFlag = false;
            fieldText = fieldText + "选择列表值列表|";
            msgText = msgText + "请输入选择列表值列表|";
        }
    }

    if (!fnCheckStrLen(EFValueList, 256)) {
        isFlag = false;
        fieldText = fieldText + "选择列表值列表|";
        msgText = msgText + "应对策略最多只允许输入256个字符|";
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

//表单验证
function CheckInput1() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

   


    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
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