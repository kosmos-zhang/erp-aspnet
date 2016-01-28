$(document).ready(function() {
    TurnToPage(currentPageIndex);
});
var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var flag = "";
var ActionFlag = ""
var str = "";
var currentPageIndex = 1;
var action = ""; //操作
var orderBy = ""; //排序字段
var TypeFlag = document.getElementById("hf_typeflag").value;
var Year = "{yyyy}";
var YearM = "{yyyyMM}";
var YearMD = "{yyyyMMdd}";
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    document.getElementById("btnAll").checked = false;
    currentPageIndex = pageIndex;
    ActionFlag = "Search"
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/SystemManager/ItemCodingRule.ashx?str=' + str, //目标地址
        cache: false,
        data: "pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&action=" + action + "&orderby=" + orderBy + "&TypeFlag=" + escape(TypeFlag) + "&ActionFlag=" + escape(ActionFlag), //数据
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ItemTypeID != null && item.ItemTypeID != "") {
                    var temp = "";
                    if (TypeFlag != "0") temp = "<td id='datetype' height='22' align='center'>" + item.RuleDateType + "</td>";

                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1' name='Checkbox1'  value=" + item.ID + " onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>" + "</td>" +
                        "<td height='22' align='center' title=\"" + item.RuleName + "\"><a href='#' onclick=Show('" + item.typeid + "','" + item.RuleName + "','" + item.RulePrefix + "','" + item.RuleDateType + "','" + item.RuleNoLen + "','" + item.RuleExample + "','" + item.fault + "','" + item.Remark + "','" + item.status + "','" + item.ID + "')>" + fnjiequ(item.RuleName, 10) + "</a></td>" +
                        "<td height='22' align='center' title=\"" + item.ItemTypeID + "\">" + fnjiequ(item.ItemTypeID, 10) + "</td>" +
                         "<td height='22' align='center'>" + item.RulePrefix + "</td>" +
                          "" + temp + "" +
                           "<td height='22' align='center'>" + item.RuleNoLen + "</td>" +
                            "<td height='22' nowrap  align='center' title=\"" + item.RuleExample + "\">" + fnjiequ(item.RuleExample, 10) + "</td>" +
                             "<td height='22' align='center'>" + item.IsDefault + "</td>" +
                    //                        "<td height='22' align='center'>"+item.ModifiedDate.substring(0,10)+"</td>"+
                    //                        "<td height='22' align='center'>"+item.ModifiedUserID+"</td>"+
                        "<td height='22' nowrap  align='center' style='overflow:hidden;text-overflow:ellipsis' onmouseover='this.title=this.innerText'>" + item.UsedStatus + "</td>").appendTo($("#pageDataList1 tbody"));
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
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            document.getElementById('Text2').value = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex);
            $("#ToPage").val(pageIndex);
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.getElementById('Text2').value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
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
function OptionCheckAll() {

    if (document.getElementById("btnAll").checked) {
        var ck = document.getElementsByName("Checkbox1");
        for (var i = 0; i < ck.length; i++) {
            ck[i].checked = true;
        }
    }
    else if (!document.getElementById("btnAll").checked) {
        var ck = document.getElementsByName("Checkbox1");
        for (var i = 0; i < ck.length; i++) {
            ck[i].checked = false;
        }
    }
}
function Fun_Search_UserInfo(aa) {
    search = "1";
    TurnToPage(1);
}
function DelItemCodingRule() {

    var c = window.confirm("确认执行删除操作吗？")
    if (c == true) {
        var ck = document.getElementsByName("Checkbox1");
        var x = Array();
        var ck2 = "";
        var str = "";
        for (var i = 0; i < ck.length; i++) {
            if (ck[i].checked) {
                ck2 += ck[i].value + ',';
            }
        }
        x = ck2;
        var str = ck2.substring(0, ck2.length - 1);
        if (x.length - 1 < 1)
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "删除编号规则信息至少要选择一项！");
        else {
            CodePublic = str;
            ActionFlag = "Del";
            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/SystemManager/ItemCodingRule.ashx?str=" + str,
                dataType: 'json', //返回json格式数据
                data: "ActionFlag=" + escape(ActionFlag), //数据
                cache: false,
                beforeSend: function() {
                    //AddPop();
                },
                //complete :function(){ //hidePopup();},
                error: function() {
                    popMsgObj.ShowMsg('请求发生错误');

                },
                success: function(data) {
                    if (data.sta == 1) {
                        popMsgObj.ShowMsg(data.info);
                        Fun_Search_UserInfo();
                    }
                    else {
                        popMsgObj.ShowMsg(data.info);
                    }
                }
            });

        }
    }
    else return false;

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



//    //改变每页记录数及跳至页数
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
        document.getElementById("btnAll").checked = false;
    }
}
//    //排序
function OrderBy(orderColum, orderTip) {
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
function Show(ItemTypeID, RuleName, RulePrefix, RuleDateType, RuleNoLen, RuleExample, IsDefault, Remark, UsedStatus, ID) {

    openRotoscopingDiv(false, 'divBackShadow', 'BackShadowIframe');
    flag = "1";
    document.getElementById("drp_typecode").disabled = false;
    document.getElementById('div_Add').style.display = 'block';
    document.getElementById("drp_use").value = '1';
    document.getElementById("div_Add").style.zIndex = "2";
    document.getElementById("divBackShadow").style.zIndex = "1";
    if (document.getElementById("hf_typeflag").value == "0") {
        document.getElementById("name").innerText = "基础数据名称：";
        document.getElementById("datetype").style.display = "none";

    }
    else {
        document.getElementById("name").innerText = "单据名称：";
    }
    if (typeof (ItemTypeID) != "undefined") {
        document.getElementById("hf_name").value = RuleName;
        document.getElementById("drp_typecode").value = ItemTypeID;
        document.getElementById("txt_RuleName").value = RuleName;
        document.getElementById("txt_RulePrefix").value = RulePrefix;
        document.getElementById("txt_RuleNoLen").value = RuleNoLen;
        document.getElementById("txt_RuleExample").value = RuleExample
        document.getElementById("chk_default").checked = IsDefault == "1" ? true : false;
        document.getElementById("txt_Remark").value = Remark;
        document.getElementById("drp_use").value = UsedStatus; //使用状态
        document.getElementById("hf_ID").value = ID;
        document.getElementById("drp_typecode").disabled = "disabled";
        if (document.getElementById("hf_typeflag").value == "0") {

        }
        else {
            if ("{" + RuleDateType + "}" == Year) {
                document.getElementById("rd_year").checked = true;
            }
            else if ("{" + RuleDateType + "}" == YearM) {
                document.getElementById("rd_yearm").checked = true;
            }
            else if ("{" + RuleDateType + "}" == YearMD) {
                document.getElementById("rd_yearmd").checked = true;
            }
        }
        flag = "2";
    }

}



function AlertMsg() {

    /**第一步：创建DIV遮罩层。*/
    var sWidth, sHeight;
    sWidth = window.screen.availWidth;
    //屏幕可用工作区高度： window.screen.availHeight;
    //屏幕可用工作区宽度： window.screen.availWidth;
    //网页正文全文宽：     document.body.scrollWidth;
    //网页正文全文高：     document.body.scrollHeight;
    if (window.screen.availHeight > document.body.scrollHeight) {  //当高度少于一屏
        sHeight = window.screen.availHeight;
    } else {//当高度大于一屏
        sHeight = document.body.scrollHeight;
    }
    //创建遮罩背景
    var maskObj = document.createElement("div");
    maskObj.setAttribute('id', 'BigDiv');
    maskObj.style.position = "absolute";
    maskObj.style.top = "0";
    maskObj.style.left = "0";
    maskObj.style.background = "#777";
    maskObj.style.filter = "Alpha(opacity=30);";
    maskObj.style.opacity = "0.3";
    maskObj.style.width = sWidth + "px";
    maskObj.style.height = sHeight + "px";
    maskObj.style.zIndex = "900";
    document.body.appendChild(maskObj);

}

function CloseDiv() {
    closeRotoscopingDiv(false, 'divBackShadow');
}

function InsertItemCodingRule() {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var UseStatus = null;
    var DefalutStatus = null;
    var RuleDateType = null;
    if (flag == "1") {
        ActionFlag = "Add"
    }
    else if (flag == "2") {
        ActionFlag = "Edit";
    }
    var ItemTypeID = trim(document.getElementById("drp_typecode").value);
    var RuleName = trim(document.getElementById("txt_RuleName").value);
    var RulePrefix = trim(document.getElementById("txt_RulePrefix").value);
    var RuleNoLen = trim(document.getElementById("txt_RuleNoLen").value);
    var RuleExample = trim(document.getElementById("txt_RuleExample").value);
    var Remark = trim(document.getElementById("txt_Remark").value);
    var name = document.getElementById("hf_name").value;
    if (RuleName.length <= 0) {
        isFlag = false;
        fieldText = fieldText + "编号规则名称|";
        msgText = msgText + "编号规则名称不能为空|";
    }
    if (strlen(RuleName) > 50) {
        isFlag = false;
        fieldText = fieldText + "编号规则名称|";
        msgText = msgText + "编号规则名称仅限于50个字符以内|";
    }
    if (RulePrefix.length <= 0) {
        isFlag = false;
        fieldText = fieldText + "编号前缀|";
        msgText = msgText + "编号前缀不能为空|";
    }
    if (!IsLetterStr(RulePrefix)) {
        isFlag = false;
        fieldText = fieldText + "编号前缀|";
        msgText = msgText + "编号前缀只能输入字母|";
    }
    if (strlen(RulePrefix) > 10) {
        isFlag = false;
        fieldText = fieldText + "编号前缀|";
        msgText = msgText + "编号前缀仅限于10个字符以内|";
    }
    if (RuleNoLen.length <= 0) {
        isFlag = false;
        fieldText = fieldText + "流水号长度|";
        msgText = msgText + "流水号长度不能为空|";
    }
    else {
//        if (RuleNoLen == '0') {
//            isFlag = false;
//            fieldText = fieldText + "流水号长度|";
//            msgText = msgText + "流水号长度不能为零|";
//        }
        rulelength = parseInt(RuleNoLen);
        if (rulelength < 3) {
            isFlag = false;
            fieldText = fieldText + "流水号长度|";
            msgText = msgText + "流水号长度至少为3位|";
        }
        if (rulelength > 8) {
            isFlag = false;
            fieldText = fieldText + "流水号长度|";
            msgText = msgText + "流水号长度不能超过8|";
        }
        if (!IsNumber(RuleNoLen)) {
            isFlag = false;
            fieldText = fieldText + "流水号长度|";
            msgText = msgText + "流水号长度必须是数字|";
        }
    }


    if (Remark != "")
        if (strlen(Remark) > 100) {
        isFlag = false;
        fieldText = fieldText + "备注|";
        msgText = msgText + "备注仅限于100个字符以内|";
    }
    var RetVal = CheckSpecialWords();

    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
    }
    if (document.getElementById("chk_default").checked) {
        DefalutStatus = "1";
    }
    else {
        DefalutStatus = "0";
    }

    if (document.getElementById("hf_typeflag").value == "0") {
        RuleDateType = "";
    }
    else {
        if (document.getElementById("rd_year").checked) {
            RuleDateType = Year;
        }
        else if (document.getElementById("rd_yearm").checked) {
            RuleDateType = YearM;
        }
        else if (document.getElementById("rd_yearmd").checked) {
            RuleDateType = YearMD;
        }
        RuleDateType = RuleDateType.substring(1, RuleDateType.length - 1)
    }
    UseStatus = document.getElementById("drp_use").value;
    var ID = document.getElementById("hf_ID").value;
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SystemManager/ItemCodingRule.ashx?str=" + str,
        dataType: 'json', //返回json格式数据
        cache: false,
        data: "ActionFlag=" + escape(ActionFlag) + "&TypeFlag=" + escape(TypeFlag) + "&ItemTypeID=" + escape(ItemTypeID) + "&RuleName=" + escape(RuleName) + "&RulePrefix=" + escape(RulePrefix) + "&RuleNoLen=" + escape(RuleNoLen) + "&RuleExample=" + escape(RuleExample) + "&Remark=" + escape(Remark) + "&DefalutStatus=" + escape(DefalutStatus) + "&RuleDateType=" + escape(RuleDateType) + "&UseStatus=" + escape(UseStatus) + "&ID=" + ID + "&name=" + escape(name), //数据
        beforeSend: function() {
            //AddPop();
        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误');

        },
        success: function(data) {
            if (data.sta == 1) {
                popMsgObj.ShowMsg(data.info);
                Fun_Search_UserInfo();
                Hide();
            }
            else {
                popMsgObj.ShowMsg(data.info);
            }
        }
    });
}
function Hide() {
    CloseDiv();
    document.getElementById('div_Add').style.display = 'none';
    New();
}
function New() {
    document.getElementById("txt_RuleName").value = "";
    document.getElementById("txt_RulePrefix").value = "";
    document.getElementById("txt_RuleNoLen").value = "";
    document.getElementById("txt_RuleExample").value = ""
    document.getElementById("txt_Remark").value = "";
    document.getElementById("rd_year").checked = true;
    document.getElementById("chk_default").checked = true;
}
function fillRuleExample() {
    var RuleDateType = null;
    var RulePrefix = document.getElementById("txt_RulePrefix").value;
    var RuleNoLen = document.getElementById("txt_RuleNoLen").value;
    rulelength = parseInt(RuleNoLen);
    //     alert(parseInt(RuleNoLen))
    var x = "";
    var t = null;
    for (var i = 1; i <= rulelength; i++) {
        x = x + "N";
    }
    var n = "{" + x + "}"
    if (document.getElementById("hf_typeflag").value == "0") {
        document.getElementById("txt_RuleExample").value = RulePrefix + n;
    }
    else {
        if (document.getElementById("rd_year").checked) {
            RuleDateType = Year;
        }
        else if (document.getElementById("rd_yearm").checked) {
            RuleDateType = YearM;
        }
        else if (document.getElementById("rd_yearmd").checked) {
            RuleDateType = YearMD;
        }
        document.getElementById("txt_RuleExample").value = RulePrefix + RuleDateType + n;
    }

}