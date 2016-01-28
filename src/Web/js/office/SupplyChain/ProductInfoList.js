$(document).ready(function() {
    IsDiplayOther('GetBillExAttrControl1_SelExtValue', 'GetBillExAttrControl1_TxtExtValue'); //扩展属性
    IsDisplayDiv();
});
///如果没有扩展字段，就隐藏其他条件
function IsDisplayDiv() {
    if (document.getElementById('OtherConditon').style.display == 'none')
        document.getElementById('DivTd').style.display = 'none';
    else
        document.getElementById('DivTd').style.display = '';
}
//去左空格;
function ltrim(s) {
    return s.replace(/^\s*/, "");
}
//去右空格;
function rtrim(s) {
    return s.replace(/\s*$/, "");
}
//左右空格;
function trim(s) {
    return rtrim(ltrim(s));
}
var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var flag = "";
var str = "";
var currentPageIndex = 1;
var action = ""; //操作
var orderBy = ""; //排序字段
//jQuery-ajax获取JSON数据
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    //扩展属性
    var EFIndex = document.getElementById("GetBillExAttrControl1_SelExtValue").value; //扩展属性select值
    var EFDesc = document.getElementById("GetBillExAttrControl1_TxtExtValue").value; //扩展属性文本框值
    var serch = document.getElementById("hidSearchCondition").value;
    currentPageIndex = pageIndex;
    action = "Load";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/SupplyChain/ProductInfo.ashx?action=' + action, //目标地址
        cache: false,
        data: "pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&action=" + action + "&orderby=" + orderBy + "&" + serch, //数据
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                var temp = item.ID + "|" + item.intCheckStatus;
                if (item.UsedStatus == '0') item.UsedStatus = '停用';
                if (item.UsedStatus == '1') item.UsedStatus = '启用';
                if (item.CheckStatus == '0') item.CheckStatus = '草稿';
                if (item.CheckStatus == '1') item.CheckStatus = '已审';
                var otherwhere = '';
                if (EFIndex != "" && EFIndex != null && (EFDesc != '')) {
                    otherwhere = "<td height='22' align='center'>" + item['ExtField' + EFIndex] + "</td>";
                }
                if (item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1'  name='Checkbox1'  value=" + temp + " onclick=IfSelectAll('Checkbox1','btnAll')  type='checkbox'/>" + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProdNo + "\">  <a href='" + GetLinkParam() + "&intOtherCorpInfoID=" + item.ID + "')>" + fnjiequ(item.ProdNo, 10) + "</a></td>" +
                        "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.TypeName + "\">" + fnjiequ(item.TypeName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.UnitName + "\">" + fnjiequ(item.UnitName, 10) + "</td>" +
                        "<td height='22' align='center'>" + item.Specification + "</td>" +
                        "<td height='22' align='center'>" + item.ColorName + "</td>" +
                        "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                        "<td height='22' align='center'>" + item.CreateDate.substring(0, 10) + "</td>" +
                        "<td height='22' align='center'>" + item.CheckStatus + "</td>" +
                         "<td height='22' align='center'>" + item.UsedStatus + "</td>" +
                      otherwhere).appendTo($("#pageDataList1 tbody"));
            });
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                       "<%= Request.Url.AbsolutePath %>", //[url]
                        {style: pagerStyle, mark: "pageDataList1Mark",
                        totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                        onclick: "TurnToPage({pageindex});return false;"}//[attr]
                        );
            totalRecord = msg.totalCount;
            document.getElementById('Text2').value = msg.totalCount;
            //document.getElementById("Text2").value=msg.totalCount;
            $("#Text2").val(msg.totalCount);
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex);
            $("#ToPage").val(pageIndex);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#txt_BarCode").val(""); //清空条码
        },
        error: function() { },
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

function GetLinkParam() {
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //是否点击了查询标识
    var flag = "0"; //默认为未点击查询的时候
    if (searchCondition != "") flag = "1"; //设置了查询条件时
    linkParam = "ProductInfoAdd.aspx?orderby=" + orderBy + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag + "&ModuleID=" + ModuleID;
    //返回链接的字符串
    return linkParam;
}

function selall() {
    var j = 0;
    var m = parseInt(j);
    var ck2 = document.getElementsByName("Checkbox1");
    for (var i = 0; i < ck2.length; i++) {
        if (ck2[i].checked) {
            j++
        }
    }
    if (j == ck2.length) {
        document.getElementById("btnAll").checked = true;
    }
    else {
        document.getElementById("btnAll").checked = false;
    }
}

function Fun_Search_ProductInfo(currPage) {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var search = "";
    var txt_TypeID = document.getElementById("txt_TypeID").value
    var TypeID = document.getElementById("txt_ID").value;
    var ProdNo = document.getElementById("txt_ProdNo").value;
    var PYShort = document.getElementById("txt_PYShort").value;
    var ProductName = document.getElementById("txt_ProductName").value;
    var Color = document.getElementById("selCorlor").value;

    //           var BarCode=document.getElementById("txt_BarCode").value;

    if (document.getElementById("txt_BarCode").value != "") {
        document.getElementById("HiddenBarCode").value = document.getElementById("txt_BarCode").value;
    }
    else {
        $("#HiddenBarCode").val("");
    }
    var BarCode = document.getElementById("HiddenBarCode").value; //商品条码
    var Specification = document.getElementById("txt_Specification").value;
    var UsedStatus = document.getElementById("UsedStatus").value;
    var CheckStatus = document.getElementById("CheckStatus").value;
    //扩展属性
    var EFIndex = document.getElementById("GetBillExAttrControl1_SelExtValue").value; //扩展属性select值
    var EFDesc = document.getElementById("GetBillExAttrControl1_TxtExtValue").value; //扩展属性文本框值


    if (!CheckSpecification(Specification)) {
        isFlag = false;
        fieldText = "规格型号|";
        msgText = msgText + "不能含有特殊字符|";
    }

    var tmpSpecification = '';

    /*处理规格型号*/
    for (var i = 0; i < Specification.length; i++) {
        if (Specification.charAt(i) == '+') {
            tmpSpecification = tmpSpecification + '＋';
        }
        else {
            tmpSpecification = tmpSpecification + Specification.charAt(i);
        }
    }

    Specification = tmpSpecification.replace('×', '&#174');
    search += "TypeID=" + escape(TypeID);
    search += "&ProdNo=" + escape(ProdNo);
    search += "&PYShort=" + escape(PYShort);
    search += "&ProductName=" + escape(ProductName);
    search += "&BarCode=" + escape(BarCode);
    search += "&Specification=" + escape(Specification);
    search += "&UsedStatus=" + escape(UsedStatus);
    search += "&CheckStatus=" + escape(CheckStatus);
    search += "&EFIndex=" + escape(EFIndex);
    search += "&Color=" + escape(Color);
    search += "&EFDesc=" + escape(EFDesc);
    search += "&txt_TypeID=" + escape(txt_TypeID)
    var RetVal = CheckSpecialWords();
    if (EFIndex != "" && EFIndex != null && (EFDesc != '')) {
        $("#ThShow").show();
        $("#DivOtherC").html(document.getElementById("GetBillExAttrControl1_SelExtValue").options[document.getElementById("GetBillExAttrControl1_SelExtValue").selectedIndex].text);
    }
    else {
        $("#ThShow").hide();
    }
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
    }
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    if (currPage == null || typeof (currPage) == "undefined") {
        TurnToPage(1);
    }
    else {
        TurnToPage(parseInt(currPage));
    }
}
function Ifshow(count) {
    if (count == "0") {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
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
    if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
        return false;
    }
    else {
        this.pageCount = parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}
//排序
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

//function AddPop()
//{
//    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
//}
//function showPopup(img,img1,retstr)
//{
//	document.all.Forms.style.display = "block";
//	document.all.Forms.innerHTML = Create_Div(img,img1,true);
//	document.all.FormContent.innerText = retstr;
//}  
//function hidePopup()
//{
//    document.all.Forms.style.display = "none";
//}
function SelectedNodeChanged(text, code, typeflag) {
    document.getElementById("txt_TypeID").value = text;
    action = "Change";
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SupplyChain/ProductInfo.ashx?action=" + action + "&ID=" + code + "&TypeFlag=" + typeflag,
        dataType: 'json',
        data: '',
        cache: false,
        success: function(data) {
            document.getElementById("txt_ID").value = data.data;
        }

    });
    hideUserList()
}
//function Create_Div(img,img1,bool)
//{
//	FormStr = "<table width=100% height='104' border='0' cellpadding=0 cellspacing=0 bgcolor=#FFFFFF>"
//	FormStr += "<tr>"
//	FormStr += "<td width=90%  bgcolor=#3F6C96>&nbsp;</td>"
//	FormStr += "<td width=10% height=20 bgcolor=#3F6C96>"
//	if(bool)
//	{
//		FormStr += "<img src='" + img +"' style='cursor:hand;display:block;' id='CloseImg' onClick=document.all['Forms'].style.display='none';>"
//	}
//	FormStr += "</td></tr><tr><td height='1' colspan='2' background='../../../Images/Pic/bg_03.gif'></td></tr><tr><td valign=top height=104 id='FormsContent' colspan=2 align=center>"
//	FormStr += "<table width=100% border=0 cellspacing=0 cellpadding=0>"
//	FormStr += "<tr>"
//	FormStr += "<td width=25% align='center' valign=top style='padding-top:20px;'>"
//	FormStr += "<img name=exit src='" + img1 + "' border=0></td>";
//	FormStr += "<td width=75% height=50 id='FormContent' style='padding-left:5pt;padding-top:20px;line-height:17pt;' valign=top></td>"
//	FormStr += "</tr></table>"
//	FormStr += "</td></tr></table>"
//	return FormStr;
//} 
///
function Fun_Delete_ProductInfo() {
    var c = window.confirm("确认执行删除操作吗？")
    if (c == true) {
        var ck = document.getElementsByName("Checkbox1");
        var x = Array();
        var ck2 = "";
        var str = "";
        Action = "Del";
        for (var i = 0; i < ck.length; i++) {
            if (ck[i].checked) {
                ck2 += ck[i].value + ',';
            }
        }
        x = ck2;
        var str = ck2.substring(0, ck2.length - 1);
        if (x.length - 1 < 1)
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "删除物品信息至少要选择一项！");
        else {
            var UrlParam = '';
            var UrlParam = "&str=" + str + "\
                     &Action=" + Action
            $.ajax({
                type: "POST",
                url: '../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx?str=' + UrlParam, //目标地址
                dataType: 'json', //返回json格式数据
                cache: false,
                beforeSend: function() {
                    //AddPop();
                },
                error: function() {
                    popMsgObj.ShowMsg('请求发生错误');

                },
                success: function(data) {
                    popMsgObj.ShowMsg(data.info);
                    document.getElementById("btnAll").checked = false;
                    var ck2 = document.getElementsByName("Checkbox1");
                    for (var i = 0; i < ck2.length; i++) {
                        ck2[i].checked = false;
                    }
                    if (data.sta > 0) {
                        Fun_Search_ProductInfo();
                    }
                }
            });
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
function Show() {
    window.location.href = GetLinkParam();
}

document.onkeydown = ScanBarCodeSearch;
/*列表条码扫描检索*/
function ScanBarCodeSearch() {
    var evt = event ? event : (window.event ? window.event : null);
    var el; var theEvent
    var browser = IsBrowser();
    if (browser == "IE") {
        el = window.event.srcElement;
        theEvent = window.event;
    }
    else {
        el = evt.target;
        theEvent = evt;
    }
    if (el.id != "txt_BarCode") {
        return;
    }
    else {
        var code = theEvent.keyCode || theEvent.which;
        if (code == 13) {
            Fun_Search_ProductInfo(1);
            evt.returnValue = false;
            evt.cancel = true;
        }
    }
}

function IsBrowser() {
    var isBrowser;
    if (window.ActiveXObject) {
        isBrowser = "IE";
    } else if (window.XMLHttpRequest) {
        isBrowser = "FireFox";
    }
    return isBrowser;
}


/*验证规格只允许+和*特殊字符可以输入*/
//function CheckSpecification(str) {
//    var SpWord = new Array("'", "\\", "<", ">", "%", "?", "/"); //可以继续添加特殊字符 此 /  字符也不可输入 输出时会破坏JSON格式
//    for (var i = 0; i < SpWord.length; i++) {
//        for (var j = 0; j < str.length; j++) {
//            if (SpWord[i] == str.charAt(j)) {
//                return false;
//                break;
//            }
//        }
//    }
//    return true;
//}


/*批量操作
1：批量确认(批量修改审核状态为“已审核”)
2：批量停用(批量修改启用状态为“停用”)
3：批量删除(启用状态为“停用”的，已启用的不能删除)
*/
function Fun_OperateProduct(operateType) {
    alert(operateType);
}

