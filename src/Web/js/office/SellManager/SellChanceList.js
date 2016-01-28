$(document).ready(function() {

    $("#chanceTypeUC_ddlCodeType").css("width", "120px");
    $("#HapSourceUC_ddlCodeType").css("width", "120px");
    $("#FeasibilityUC_ddlCodeType").css("width", "120px");
    
//GetBillExtAttr('officedba.SellChance');
IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue'); 

    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var isSeacher = requestObj['isSeacher']; //是否从列表页面进入
        if (isSeacher == 'true') {
            $("#ChanceNo").val(requestObj['ChanceNo']); //销售机会
            $("#Title").val(requestObj['Title']); //主题
            $("#CustID").val(requestObj['CustName']); //客户
            $("#CustID").attr("title", requestObj['CustID']); //客户
            $("#UserSeller").val(requestObj['UserSeller']); //业务员
            $("#hiddSeller").val(requestObj['Seller']); //业务员
            $("#Phase").val(requestObj['Phase']);      //阶段（1初期沟通、2立项评估、3需求分析、4方案指定、5招投标/竞争、6商务谈判、7合同签约）
            $("#State").val(requestObj['State']);      //状态（1跟踪2成功3失败4搁置5失效）
            $("#chanceTypeUC_ddlCodeType").val(requestObj['ChanceType']); //销售机会类型(对应分类代码表ID)
            $("#HapSourceUC_ddlCodeType").val(requestObj['HapSource']);  //销售机会来源ID(对应分类代码表ID)
            $("#FindDate").val(requestObj['FindDate']);   //发现日期小
            $("#FindDate1").val(requestObj['FindDate1']);   //发现日期大
            
             document.getElementById("GetBillExAttrControl1_SelExtValue").value = requestObj['EFIndex'];//扩展属性select值
            document.getElementById("GetBillExAttrControl1_TxtExtValue").value = requestObj['EFDesc'];//扩展属性文本框值
            
            IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
            
            pageCount = requestObj['pageCount']; //每页计数
            orderBy = requestObj['orderby']; //排序字段
            TurnToPage(parseInt(requestObj['pageIndex']));
        }
    }
});

//获取url中"?"符后的字串
function GetRequest() {
    var url = location.search;
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    return theRequest;
}

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var currentPageIndex = 1;
var orderBy = ""; //排序字段
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    if (!CheckInput()) {
        return;
    }
    currentPageIndex = pageIndex;
    $("#checkall").removeAttr("checked");
    var ChanceNo = $("#ChanceNo").val(); //销售机会
    var Title = $("#Title").val(); //主题
    var CustName = $("#CustID").val(); //客户
    if (CustName != '') {
        var CustID = $("#CustID").attr("title"); //客户
    }
    else {
        var CustID = '';
    }
    var UserSellerName = $("#UserSeller").val(); //业务员
    if (UserSellerName != '') {
        var Seller = $("#hiddSeller").val(); //业务员
    }
    else {
        var Seller = ''; //业务员
    }
    var Phase = $("#Phase").val();      //阶段（1初期沟通、2立项评估、3需求分析、4方案指定、5招投标/竞争、6商务谈判、7合同签约）
    var State = $("#State").val();      //状态（1跟踪2成功3失败4搁置5失效）
    var ChanceType = $("#chanceTypeUC_ddlCodeType").val(); //销售机会类型(对应分类代码表ID)
    var HapSource = $("#HapSourceUC_ddlCodeType").val();  //销售机会来源ID(对应分类代码表ID)
    var FindDate = $("#FindDate").val();   //发现日期小
    var FindDate1 = $("#FindDate1").val();   //发现日期大
        
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
    
    var action = "getinfo";

    var strUrl = 'pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy) +
              '&ChanceNo=' + escape(ChanceNo) + '&Title=' + escape(Title) + '&CustID=' + escape(CustID) +
              '&Seller=' + escape(Seller) + '&Phase=' + escape(Phase) + '&State=' + escape(State) +
              '&ChanceType=' + escape(ChanceType) + '&HapSource=' + escape(HapSource) + '&FindDate=' + escape(FindDate) +
              '&FindDate1=' + escape(FindDate1) + '&action=' + escape(action) + '&EFIndex=' + escape(EFIndex) + '&EFDesc=' + escape(EFDesc);
    $("#hiddUrl").val(strUrl);

    $("#hiddExpChanceNo").val(ChanceNo);
    $("#hiddExpTitle").val(Title);
    $("#hiddExpCustID").val(CustID);

    $("#hiddExpSeller").val(Seller);
    $("#hiddExpChanceType").val(ChanceType);
    $("#hiddExpHapSource").val(HapSource);
    
    $("#hidEFIndex").val(EFIndex);
    $("#hidEFDesc").val(EFDesc);
    
    $("#hiddExpFindDate").val(FindDate);
    $("#hiddExpFindDate1").val(FindDate1);
    $("#hiddExpState").val(State);
    $("#hiddExpPhase").val(HapSource);
    
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/SellManager/SellChanceList.ashx', //目标地址
        cache: false,
        data: strUrl,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
            if (item.ID != null && item.ID != "") {
                var OrderNo = item.ChanceNo;
                var Title = item.Title;

                var CustName = item.CustName;


                if (Title != null) {
                    if (Title.length > 6) {
                        Title = Title.substring(0, 6) + '...';
                    }
                }
                if (OrderNo != null) {
                    if (OrderNo.length > 15) {
                        OrderNo = OrderNo.substring(0, 15) + '...';
                    }
                }

                if (CustName != null) {
                    if (CustName.length > 6) {
                        CustName = CustName.substring(0, 6) + '...';
                    }
                }
                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='chk" + i + "'  onclick='fnUnSelect(this)'  Title='" + item.ChanceNo + "' Class='" + item.FromBillText + "'  value='" + item.OfferNo + "'   type='checkbox'/>" + "</td>" +
                    "<td height='22' align='center'><a href='#' title=\"" + item.ChanceNo + "\" onclick=fnOrderInfo('" + item.ID + "')>" + OrderNo + "</a></td>" +
                    "<td height='22' align='center'><span title=\"" + item.Title + "\">" + Title + "</td>" +
                    "<td height='22' align='center'><span title=\"" + item.CustName + "\">" + CustName + "</td>" +
                    "<td height='22' align='center'>" + item.ChanceTypeName + "</td>" +
                    "<td height='22' align='center'>" + item.HapSourceName + "</td>" +
                    "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                    "<td height='22' align='center'>" + item.FindDate + "</td>" +
                    "<td height='22' align='center'>" + item.PhaseName + "</td>" +
                    "<td height='22' align='center'>" + item.StateName + "</td>").appendTo($("#pageDataList1 tbody"));
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
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pageSellOffcount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.form1.elements["Text2"].value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });

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

//选择客户
function fnSelectCustInfo() {
    popSellCustObj.ShowList('all');

}

//清除客户信息
function ClearSellModuCustdiv() {
    $("#CustID").val(''); //客户名称
    $("#CustID").attr("title", ''); //客户编号
    closeSellModuCustdiv(); //关闭客户选择控件
}

//选择客户后，为页面填充数据
function fnSelectCust(custID) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellModuleSelectCustUC.ashx",
        data: 'actionSellCust=info&id=' + custID,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取客户数据失败,请确认"); },
        success: function(data) {
            $("#CustID").val(data.CustName); //客户名称
            $("#CustID").attr("title", custID); //客户编号
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
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

//构造查询条件的URL参数
function getSearchParams() {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        strUrl += 'isSeacher=false';
    }
    else {
        strUrl += '&isSeacher=true';
    }
    return strUrl;
}

//新建
function fnNew() {
    window.location.href = 'SellChance_Add.aspx?' + getSearchParams() + '&islist=1&ModuleID=2031201';
}

function Ifshow(count) {
    if (count == "0") {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pageSellOffcount").style.display = "none";
    }
    else {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pageSellOffcount").style.display = "block";
    }
}

//查看明细
function fnOrderInfo(retval) {
    window.location.href = 'SellChanceMod.aspx?' + getSearchParams() + '&id=' + escape(retval) + "&islist=1&ModuleID=2031201";
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
    $("#checkall").removeAttr("checked");
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

//全选
function selectall() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
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
    $("#hiddExpOrder").val(orderBy);
    TurnToPage(1);
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
            DetailID += $("#chk" + i).attr("title") + ',';
            var OfferNo = $("#chk" + i).val();
            var FromBillText = $.trim($("#chk" + i).attr("class"));
            if (FromBillText == '存在') {
                isFlag = false;
                fieldText = fieldText + "销售机会：" + $("#chk" + i).attr("title") + "|";
                msgText = msgText + "已被其他单据引用，不可删除|";
            }
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
                    url: "../../../Handler/Office/SellManager/SellChanceList.ashx",
                    data: "action=del&orderNos=" + escape(DetailID),
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {

                    },

                    error: function() {

                        popMsgObj.ShowMsg('请求发生错误！');
                    },
                    success: function(data) {
                        if (data.sta == 1) {
                            TurnToPage(1);
                            popMsgObj.ShowMsg('删除成功！');
                        }
                        else {
                            popMsgObj.ShowMsg('删除失败！');
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

//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var FindDate = $.trim($("#FindDate").val()); //发现日期
    var FindDate1 = $.trim($("#FindDate1").val()); //发现日期

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if (FindDate != '' && FindDate1 != '') {
        if (CompareDate(FindDate, FindDate1) == 1) {
            isFlag = false;
            fieldText = fieldText + "发现日期|";
            msgText = msgText + "发现日期范围选择错误|";
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

//判断是否可以导出Excel
function fnIsSearch() {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "您没有检索任何数据！");
        return false;
    }
    if (totalRecord == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "没有任何数据可以被导出！");
        return false;
    }
    $("#hiddExpTotal").val(totalRecord);
    return true;
}