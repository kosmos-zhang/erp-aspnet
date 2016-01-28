$(document).ready(function() {

    //获取URl参数
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var orderID = requestObj['id']; //销售机会Id
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031103', 'ModuleID=2031104');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (orderID != null) {
            $("#hiddOrderID").val(orderID);
            fnGetOrderInfo(orderID); //获取报价单详细信息         
        }
    }
});

//清除对手编号
function clearAdversary() {
    $("#CustNo").val(''); //机会编号
    $("#CustNo").attr("title", ''); //机会编号
    closeAddiv();
}
//清除销售机会
function clearSellChance() {
    $("#ChanceID").val(''); //机会编号
    $("#ChanceID").attr("title", ''); //机会编号
    closeSellSenddiv();
}
//获取详细信息
function fnGetOrderInfo(orderID) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/AdversarySellList.ashx",
        data: "action=orderInfo&orderID=" + escape(orderID),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            if (data.data.length == 1) {
                fnInitPage(data.data[0]); //给页面赋值
               
            }
        }
    });
}

//给页面赋值
function fnInitPage(data) {

    $("#CustNo").val(data.CustNo); //竞争对手编号
    $("#ChanceID").val(data.ChanceNo); //销售机会
    $("#ChanceID").attr("title", data.ChanceID); //销售机会
    $("#CustID").val(data.CustName1); //竞争客户
    $("#CustID").attr("title", data.CustID); //竞争客户

    $("#Project").val(data.Project);
    $("#Price").val(FormatAfterDotNumber( data.Price,precisionLength));
    $("#Power").val(data.Power);
    $("#Advantage").val(data.Advantage);
    $("#disadvantage").val(data.disadvantage);
    $("#Policy").val(data.Policy);
    $("#Remark").val(data.Remark);      

    $("#Creator").val(data.EmployeeName);        //制单人ID
    $("#CreateDate").val(data.CreatDate);     //制单日期
    $("#ModifiedDate").val(data.ModifiedDate);   //最后更新日期
    $("#ModifiedUserID").val(data.ModifiedUserID); //最后更新用户ID(对应操作用户UserID)
}

//打印功能
function fnPrintOrder() {
    //if (confirm('请确认页面信息是否已保存！')) {
        var OrderNo = $.trim($("#hiddOrderID").val());
        if (OrderNo == '保存时自动生成' || OrderNo == '' || OrderNo == '0') {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存您所填的信息！");
        }
        else {

            window.open('../../../Pages/PrinttingModel/SellManager/AdversarySellPrint.aspx?no=' + OrderNo);
        }
   // }
}



//获取查询参数
function GetRequestToHidden() {
    var url = location.search;
    return url;
}

//返回按钮
function fnBack() {
    var URLParams = document.getElementById("HiddenURLParams").value;
    window.location.href = 'AdversarySellList.aspx' + URLParams;
}

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


//获取销售机会详细信息
function fnSelectSellSend(orderID) {

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellChanceList.ashx",
        data: "action=orderInfo&orderID=" + escape(orderID),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            if (data.data.length == 1) {
                $("#ChanceID").val(data.data[0].ChanceNo); //机会编号
                $("#ChanceID").attr("title", orderID); //机会编号

                closeSellSenddiv();
            }
        }
    });
}

//清除客户信息
function ClearSellModuCustdiv() {
    $("#CustID").val(''); //客户名称
    $("#CustID").attr("title", ''); //客户编号

    closeSellModuCustdiv(); //关闭客户选择控件
}


//获取对手详细信息
function fnSelectSellAd(orderID) {

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SelectAdversary.ashx",
        data: "actionAd=info&id=" + escape(orderID),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            if (data.data.length == 1) {
                $("#CustNo").val(data.data[0].CustNo); //机会编号
                $("#CustNo").attr("title", orderID); //机会编号
                closeAddiv();
            }
        }
    });
}

//保存数据
function InsertSellOfferData() {
    if (!CheckInput()) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var strInfo = fnGetInfo();

    var str = $("#hiddAcction").val();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/AdversarySellAdd.ashx",
        data: strInfo + '&action=' + escape(str),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {
                if ($("#hiddAcction").val() == "insert") {
                    $("#hiddAcction").val("update")
                }
                $("#hiddOrderID").val(data.data);

                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存成功！");
            }
            else {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存失败,请确认！");
            }
        }
    });
}
//获取主表信息
function fnGetInfo() {
    var strInfo = '';
    var ID = $("#hiddOrderID").val();
    var CustNo = $("#CustNo").val();
    var ChanceID = $("#ChanceID").attr("title");
    var CustID = $("#CustID").attr("title");
    var Project = $("#Project").val();
    var Price = $("#Price").val();
    var Power = $("#Power").val();
    var Advantage = $("#Advantage").val();
    var disadvantage = $("#disadvantage").val();
    var Policy = $("#Policy").val();
    var Remark = $("#Remark").val();


    strInfo = 'ID=' + escape(ID) + '&CustNo=' + escape(CustNo) + '&ChanceID=' + escape(ChanceID) + '&CustID=' + escape(CustID)
            + '&Project=' + escape(Project) + '&Price=' + escape(Price) + '&Power=' + escape(Power)
            + '&Advantage=' + escape(Advantage) + '&disadvantage=' + escape(disadvantage) + '&Policy=' + escape(Policy)
            + '&Remark=' + escape(Remark);
    return strInfo;
}




//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var CustNo = $.trim($("#CustNo").val());
    var ChanceID = $.trim($("#ChanceID").val());
    var CustID = $.trim($("#CustID").val());
    var Price = $.trim($("#Price").val());

    if (CustNo == "") {
        isFlag = false;
        fieldText = fieldText + "对手编号|";
        msgText = msgText + "请选择对手编号|";
    }

    if (ChanceID == "") {
        isFlag = false;
        fieldText = fieldText + "销售机会|";
        msgText = msgText + "请选择销售机会|";
    }
    if (CustID == "") {
        isFlag = false;
        fieldText = fieldText + "销售机会|";
        msgText = msgText + "请选择销售机会|";
    }

    if (Price.length > 0) {
        if (!IsNumeric(Price, 14, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "价格|";
            msgText = msgText + "价格输入有误，请输入有效的数值！|";
        }
    }

    var Project = $("#Project").val(); //竞争产品/方案
    if (!fnCheckStrLen(Project, 2000)) {
        isFlag = false;
        fieldText = fieldText + "竞争产品/方案|";
        msgText = msgText + "竞争产品/方案最多只允许输入2000个字符|";
    }
    var Power = $("#Power").val(); //竞争能力
    if (!fnCheckStrLen(Power, 2000)) {
        isFlag = false;
        fieldText = fieldText + "竞争能力|";
        msgText = msgText + "竞争能力最多只允许输入2000个字符|";
    }
    var Advantage = $("#Advantage").val(); //竞争优势
    if (!fnCheckStrLen(Advantage, 2000)) {
        isFlag = false;
        fieldText = fieldText + "竞争优势|";
        msgText = msgText + "竞争优势最多只允许输入2000个字符|";
    }
    var disadvantage = $("#disadvantage").val(); //竞争劣势
    if (!fnCheckStrLen(disadvantage, 2000)) {
        isFlag = false;
        fieldText = fieldText + "竞争劣势|";
        msgText = msgText + "竞争劣势最多只允许输入2000个字符|";
    }
    var Policy = $("#Policy").val(); //应对策略
    if (!fnCheckStrLen(Policy, 1024)) {
        isFlag = false;
        fieldText = fieldText + "应对策略|";
        msgText = msgText + "应对策略最多只允许输入2000个字符|";
    }
    var Remark = $("#Remark").val(); //备注
    if (!fnCheckStrLen(Remark, 2000)) {
        isFlag = false;
        fieldText = fieldText + "备注|";
        msgText = msgText + "备注最多只允许输入2000个字符|";
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


