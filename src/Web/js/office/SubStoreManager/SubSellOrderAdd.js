

//=================================== Start  ================================
$(document).ready(function() {
    //新增页面运送方式不可选
    fnGetExtAttr();
    $("#ddlCarryType_ddlCodeType").attr("disabled", true);
    $("#SearchCondition").val(location.search);
    var requestobj = GetRequest(location.search);
    if (requestobj['Page'] == "List") {//从列表页面过来的
        $("#imgBack").css("display", "inline");
    }
    var ID = requestobj['ID'];
    var OutSttl = requestobj['OutSttl'];
    FillDefault();
    if (ID > 0) {
        fnFill(ID, OutSttl);
    }
    else {
        document.getElementById("divTitle").innerHTML = "新建销售订单";

        // 填充扩展属性
        GetExtAttr("officedba.SubSellOrder", null);
    }
});

// 填充默认值
function FillDefault() {
    $("#txtCountTotal").val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
    $("#txtTotalPrice").val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
    $("#txtTotalTax").val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
    $("#txtTotalFee").val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
    $("#txtDiscountTotal").val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
    $("#txtRealTotal").val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
    $("#txtPayedTotal").val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
    $("#txtWairPayTotal").val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
}


/*重写toFixed*/
Number.prototype.toFixed = function(d) {
    return FormatAfterDotNumber(this, parseInt($("#hidSelPoint").val()));
}

//获取url中"?"符后的字串
function GetRequest(url) {
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

//=================================== End   界 面 取 值 ================================

//=================================== Start 界 面 取 值 ================================
//基本信息
function fnGetBaseInfo() {
    var Info = "";
    var Action = $("#HiddenAction").val();
    if (Action == "Add") {//新增状态判断但据是否输入
        var CodeRule = $("#OrderNo_ddlCodeRule").val();
        //手工输入的传编号，自动生成的传生成规则
        if (CodeRule == "") {
            var OrderNo = $("#OrderNo_txtCode").val();
            Info += "&OrderNo=" + $.trim(OrderNo);
        }
        else {
            Info += "&CodeRule=" + CodeRule;
        }
    }
    else if (Action == "Update") {//此处有些问题，回头处理
        Info += "&OrderNo=" + $("#OrderNo_txtCode").val();
    }
    Info += "&ID=" + $("#ThisID").val();
    Info += "&Title=" + escape($.trim($("#txtTitle").val()));
    Info += "&DeptID=" + $("#hidDeptID").val();
    Info += "&SendMode=" + $("#ddlSendMode").val();
    Info += "&Seller=" + $("#hidSellerID").val();
    Info += "&CustName=" + escape($.trim($("#txtCustName").val()));

    Info += "&CustTel=" + escape($.trim($("#txtCustTel").val()));
    Info += "&CustMobile=" + escape($.trim($("#txtCustMobile").val()));
    Info += "&CurrencyType=" + $("#CurrencyTypeID").val();
    Info += "&Rate=" + $("#txtRate").val();

    Info += "&OrderMethod=" + $("#ddlOrderMethod_ddlCodeType").val();
    Info += "&TakeType=" + $("#ddlTakeType_ddlCodeType").val();
    Info += "&PayType=" + $("#ddlPayType_ddlCodeType").val();
    Info += "&MoneyType=" + $("#ddlMoneyType_ddlCodeType").val();
    Info += "&OrderDate=" + $("#txtOrderDate").val();
    var isAddTax = $("#chkisAddTax").attr("checked") ? "1" : "0";
    Info += "&isAddTax=" + isAddTax;
    Info += "&Action=" + $("#HiddenAction").val();
    Info += GetExtAttrValue();
    return Info;
}
//发货信息
function fnGetSendInfo() {
    var Info = "";
    if ($("#txtPlanOutDate").val() != "") {
        Info += "&PlanOutDate=" + $("#txtPlanOutDate").val() + " " + $("#ddlPlanOutHour").val() + ":" + $("#ddlPlanOutMin").val();
    }
    else {
        Info += "&PlanOutDate=";
    }
    if ($("#txtOutDate").val() != "") {
        Info += "&OutDate=" + $("#txtOutDate").val() + " " + $("#ddlOutHour").val() + ":" + $("#ddlOutMin").val();
    }
    else {
        Info += "&OutDate=";
    }
    Info += "&CarryType=" + $("#ddlCarryType_ddlCodeType").val();
    Info += "&BusiStatus=" + $("#hidBusiStatus").val();
    Info += "&OutDeptID=" + $("#hidOutDeptID").val();
    Info += "&OutUserID=" + $("#hidOutUserID").val();
    Info += "&CustAddr=" + escape($("#txtCustAddr").val());
    return Info;
}
//安装信息
function fnGetSetupInfo() {
    var Info = "";
    var NeedSetup = $("#chkNeedSetup").attr("checked") ? "1" : "0";
    Info += "&NeedSetup=" + NeedSetup;
    if ($("#txtPlanSetDate").val() != "") {
        Info += "&PlanSetDate=" + $("#txtPlanSetDate").val() + " " + $("#ddlPlanSetHour").val() + ":" + $("#ddlPlanSetMin").val();
    }
    else {
        Info += "&PlanSetDate=";
    }
    if ($("#txtSetDate").val() != "") {
        Info += "&SetDate=" + $("#txtSetDate").val() + " " + $("#ddlSetHour").val() + ":" + $("#ddlSetMin").val();
    }
    else {
        Info += "&SetDate=";
    }
    Info += "&SetUserID=" + escape($("#UserSetUserID").val());
    return Info;
}
//合计信息
function fnGetTotalInfo() {
    var Info = "";
    Info += "&CountTotal=" + $("#txtCountTotal").val();
    Info += "&TotalPrice=" + $("#txtTotalPrice").val();
    Info += "&TotalTax=" + $("#txtTotalTax").val();
    Info += "&TotalFee=" + $("#txtTotalFee").val();
    Info += "&Discount=" + $("#txtDiscount").val();

    Info += "&DiscountTotal=" + $("#txtDiscountTotal").val();
    Info += "&RealTotal=" + $("#txtRealTotal").val();
    var ThisPayedTotal = 0;
    if ($("#txtThisPayed").val() != "") {
        ThisPayedTotal = parseFloat($("#txtThisPayed").val());
    }
    var PayedTotal = FormatAfterDotNumber(parseFloat($("#txtPayedTotal").val() == "" ? "0" : $("#txtPayedTotal").val()) + ThisPayedTotal, parseInt($("#hidSelPoint").val()));
    Info += "&PayedTotal=" + PayedTotal;
    var WairPayTotal = FormatAfterDotNumber(parseFloat($("#txtWairPayTotal").val()) - ThisPayedTotal, parseInt($("#hidSelPoint").val()));
    Info += "&WairPayTotal=" + WairPayTotal;
    return Info;
}
//备注信息
function fnGetRemarkInfo() {
    var Info = "";
    Info += "&Creator=" + $("#txtCreatorID").val();
    Info += "&CreateDate=" + $("#txtCreateDate").val();
    Info += "&BillStatus=" + $("#txtBillStatusID").val();
    Info += "&Confirmor=" + $("#txtConfirmorID").val();
    Info += "&ConfirmorDate=" + $("#txtConfirmorDate").val();

    Info += "&Closer=" + $("#txtCloserID").val();
    Info += "&CloseDate=" + $("#txtCloseDate").val();
    Info += "&ModifiedUserID=" + $("#txtModifiedUserID").val();
    Info += "&ModifiedDate=" + $("#txtModifiedDate").val();
    Info += "&Remark=" + escape($("#txtRemark").val());
    Info += "&Attachment=" + escape($("#hfPageAttachment").val().replace(/\\/g, "\\\\"));
    return Info;
}
//结算信息
function fnGetSettInfo() {
    var Info = "";
    var isOpenBill = document.getElementById("chkisOpenbill").checked == true ? "1" : "0";
    Info += "&isOpenBill=" + isOpenBill;
    return Info;

}
//明细信息
function fnGetDetailInfo() {
    var signFrame = findObj("DetailTable", document);
    var Edge = $("#txtTRLastIndex").val();
    var Info = "";
    var Index = 0;
    for (var i = 1; i < signFrame.rows.length; ++i) {
        if (signFrame.rows[i].style.display != "none") {
            Info += "&SortNo" + Index + "=" + $("#SortNo" + i).html();

            Info += "&ProductID" + Index + "=" + $("#ProductID" + i).val();
            Info += "&StorageID" + Index + "=" + $("#StorageID" + i).val();
            Info += "&ProductCount" + Index + "=" + $("#ProductCount" + i).val();
            Info += "&UnitID" + Index + "=" + $("#UnitID" + i).val();
            Info += "&UnitPrice" + Index + "=" + $("#UnitPrice" + i).val();

            Info += "&TaxPrice" + Index + "=" + $("#TaxPrice" + i).val();
            Info += "&Discount" + Index + "=" + $("#Discount" + i).val();
            Info += "&TaxRate" + Index + "=" + $("#TaxRate" + i).val();
            Info += "&TotalFee" + Index + "=" + $("#TotalFee" + i).val();
            Info += "&TotalPrice" + Index + "=" + $("#TotalPrice" + i).val();
            Info += "&TotalTax" + Index + "=" + $("#TotalTax" + i).val();
            Info += "&Remark" + Index + "=" + escape($("#Remark" + i).val());
            Info += "&BatchNo" + Index + "=" + escape($("#selBatch" + i).val()); // 批次
            //计量单位开启
            if ($("#txtIsMoreUnit").val() == "1") {
                Info += "&UsedUnitID" + Index + "=" + escape($("#txtUsedUnit" + i).val().split('|')[0]);
                Info += "&UsedUnitCount" + Index + "=" + escape($("#UsedUnitCount" + i).val());
                Info += "&UsedPrice" + Index + "=" + escape($("#UsedPrice" + i).val());
                Info += "&ExRate" + Index + "=" + escape($("#txtUsedUnit" + i).val().split('|')[1]);
            }
            ++Index;
        }
    }
    Info += "&length=" + Index;
    return Info;
}
//=================================== End   界 面 取 值 ================================




//=================================== Start 保      存 ================================
function fnSave() {
    //校验
    if (fnCheck())
        return false;
    //取值
    var URLParams = fnGetBaseInfo();
    URLParams += fnGetSendInfo();
    URLParams += fnGetSetupInfo();
    URLParams += fnGetTotalInfo();
    URLParams += fnGetSettInfo();
    URLParams += fnGetRemarkInfo();
    URLParams += fnGetDetailInfo();

    //保存
    $.ajax({
        type: 'POST',
        url: '../../../Handler/Office/SubStoreManager/SubSellOrderAdd.ashx',
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {
            AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.sta == 1) {//保存成功
                if ($("#HiddenAction").val() == "Add") {
                    $("#HiddenAction").val("Update");
                    $("#ddd").val(222);
                    var BackValue = data.data.split('#');

                    document.getElementById("OrderNo_txtCode").value = BackValue[0];
                    document.getElementById('OrderNo_txtCode').className = 'tdinput';
                    document.getElementById('OrderNo_txtCode').style.width = '90%';
                    $("#OrderNo_txtCode").attr("disabled", true);
                    document.getElementById('OrderNo_ddlCodeRule').style.display = 'none';

                    $("#ThisID").val(BackValue[1]);
                    $("#txtCreatorName").val(BackValue[2]);
                    $("#txtCreateDate").val(BackValue[3]);
                    $("#txtModifiedDate").val(BackValue[3]);
                    $("#txtModifiedUserName").val(BackValue[4]);

                }
                else {
                    var BackValue = data.data.split('#');
                    $("#txtModifiedDate").val(BackValue[0]);
                    $("#txtModifiedUserName").val(BackValue[1]);
                }
                $("#txtThisPayedHid").val(0);
                var ThisPayedTotal = 0;
                if ($("#txtThisPayed").val() != "")
                    ThisPayedTotal = parseFloat($("#txtThisPayed").val());
                $("#txtPayedTotal").val(FormatAfterDotNumber(parseFloat($("#txtPayedTotal").val()) + ThisPayedTotal, parseInt($("#hidSelPoint").val())))
                $("#txtWairPayTotal").val(FormatAfterDotNumber(parseFloat($("#txtWairPayTotal").val()) - ThisPayedTotal, parseInt($("#hidSelPoint").val())))
                $("#txtThisPayed").val(0);
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存成功！");
            }
            else if (data.sta == 2) {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该编号已被使用，请输入未使用的编号！");
            }
            else if (data.sta == 3) {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!");
            }
        },
        complete: function() { fnStatusAndTitle(); } //接收数据完毕
    });
    return true;
}
//=================================== End   保      存 ================================


//=================================== Start 确      认 ================================
function fnConfirm() {
    // 先保存
    if (!fnSave()) {
        return;
    }

    if ($("#txtThisPayed").val() != "" && $("#txtThisPayed").val() != "0") {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "本次结算金额不为零，请先保存再确认！");
        return;
    }

    //单据ID
    var ID = $("#ThisID").val();
    var OrderNo = $("#OrderNo_txtCode").val();
    //确认标志
    var Action = "Confirm";
    var URLParams = "Action=" + Action + "&ID=" + ID + "&OrderNo=" + OrderNo;
    //获取客户信息
    URLParams += "&DeptID=" + $("#hidDeptID").val();
    URLParams += "&CustName=" + escape($.trim($("#txtCustName").val()));
    URLParams += "&CustTel=" + escape($.trim($("#txtCustTel").val()));
    URLParams += "&CustMobile=" + escape($.trim($("#txtCustMobile").val()));
    URLParams += "&CustAddr=" + escape($.trim($("#txtCustAddr").val()));
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SubStoreManager/SubSellOrderAdd.ashx",
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {
            AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.sta == 1) {
                $("#txtConfirmorName").val(data.data.split('#')[2]);
                $("#txtConfirmorDate").val(data.data.split('#')[0]);
                $("#txtModifiedUserName").val(data.data.split('#')[1]);
                $("#txtModifiedDate").val(data.data.split('#')[0]);

                //
                $("#txtBillStatusID").val("2");
                $("#txtBillStatusName").val("执行");
                $("#hidBusiStatus").val("2");
                $("#txtBusiStatus").val("出库");
                $("#txtThisPayed").val(0);
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "确认成功！");
            }
            var ThisPayedTotal = 0;
            if ($("#txtThisPayed").val() != "")
                ThisPayedTotal = parseFloat($("#txtThisPayed").val());
            $("#txtPayedTotal").val(FormatAfterDotNumber(parseFloat($("#txtPayedTotal").val()) + ThisPayedTotal, 2))
            $("#txtWairPayTotal").val(FormatAfterDotNumber(parseFloat($("#txtWairPayTotal").val()) - ThisPayedTotal, 2))
            $("#txtThisPayed").val(0);
        },
        complete: function() { fnStatusAndTitle(); } //接收数据完毕
    });
}

function fnConcelConfirm() {//取消确认
    //操作条件：单据状态：2，业务状态：2
    //操作结果：单据状态：1，业务状态：1

    var BillStatus = $("#txtBillStatusID").val();
    var BusiStatus = $("#hidBusiStatus").val();

    if ((BillStatus != "2") || (BusiStatus != "2"))
        return;
    var URLParams = "ID=" + $("#ThisID").val();
    URLParams += "&Action=ConcelConfirm";
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SubStoreManager/SubSellOrderAdd.ashx",
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {
            AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.sta == 1) {
                $("#txtConfirmorName").val(data.data.split('#')[2]);
                $("#txtConfirmorDate").val(data.data.split('#')[0]);
                $("#txtModifiedUserName").val(data.data.split('#')[1]);
                $("#txtModifiedDate").val(data.data.split('#')[0]);

                //
                $("#txtBillStatusID").val("1");
                $("#txtBillStatusName").val("制单");
                $("#hidBusiStatus").val("1");
                $("#txtBusiStatus").val("下单");
                $("#txtThisPayed").val(0);

                var signFrame = findObj("DetailTable", document);
                for (var i = 1; i < signFrame.rows.length; ++i) {
                    if (signFrame.rows[i].style.display != "none") {
                        //数量
                        $("#UsedUnitCount" + i).attr("disabled", false);
                    }
                }
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "取消确认成功！");
            }
        },
        complete: function() { fnStatusAndTitle(); } //接收数据完毕
    });
}

//销售发货的确认
function fnConfirmOut() {
    if (!confirm("确认发货么？"))
        return;
    fnTotal(1);
    //校验
    if (fnCheckOut())
        return;
    var URLParams = fnGetBaseInfo();
    URLParams += fnGetSendInfo();
    URLParams += fnGetSetupInfo();
    URLParams += fnGetTotalInfo();
    URLParams += fnGetRemarkInfo();
    URLParams += fnGetDetailInfo();
    //

    URLParams = URLParams.replace("&Action=Update", "&Action=ConfirmOut");
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SubStoreManager/SubSellOrderAdd.ashx",
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {
            AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.sta == 1) {
                if (parseFloat($("#txtThisPayed").val()) == parseFloat($("#txtWairPayTotal").val())) {//完成
                    //结算信息可见
                    $("#Sttl").css("display", "inline");
                    $("#SttlUser").val(data.data.split('#')[2]);
                    $("#SettDate").val(data.data.split('#')[0]);
                    $("#txtCloserName").val(data.data.split('#')[2]);
                    $("#txtCloseDate").val(data.data.split('#')[0]);
                    $("#txtBillStatusName").val("自动结单");
                    $("#txtBillStatusID").val("5");
                    $("#hidBusiStatus").val("4");
                    $("#txtBusiStatus").val("完成");
                }
                else {
                    var ThisPayedTotal = 0;
                    if ($("#txtThisPayed").val() != "")
                        ThisPayedTotal = parseFloat($("#txtThisPayed").val());
                    $("#txtPayedTotal").val(FormatAfterDotNumber(parseFloat($("#txtPayedTotal").val()) + ThisPayedTotal, 2))
                    $("#txtWairPayTotal").val(FormatAfterDotNumber(parseFloat($("#txtWairPayTotal").val()) - ThisPayedTotal, 2))
                    $("#txtThisPayed").val(0);
                    //状态控制
                    $("#txtBillStatusName").val("执行");
                    $("#txtBillStatusID").val("2");
                    $("#hidBusiStatus").val("3");
                    $("#txtBusiStatus").val("结算");
                    $("#SttlUser").val(data.data.split('#')[2]);
                    $("#SettDate").val(data.data.split('#')[0]);
                }
                var ThisPayedTotal = 0;
                if ($("#txtThisPayed").val() != "")
                    ThisPayedTotal = parseFloat($("#txtThisPayed").val());
                $("#txtPayedTotal").val(FormatAfterDotNumber(parseFloat($("#txtPayedTotal").val()) + ThisPayedTotal, parseInt($("#hidSelPoint").val())))
                $("#txtWairPayTotal").val(FormatAfterDotNumber(parseFloat($("#txtWairPayTotal").val()) - ThisPayedTotal, parseInt($("#hidSelPoint").val())))
                $("#txtThisPayed").val(0);
                fnStatus();
                fnStatusAndTitle();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "确认成功！");
            }
            else if (data.sta == 2) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "确认后会出现负库存，但是物品[" + fnGetProductName(data.data) + "]不允许出现负库存！");
                return;
            }
            else if (data.sta == 3) {
                if (!confirm("确认物品[" + fnGetProductName(data.data) + "]后会出现负库存，继续确认么？")) {
                    hidePopup();
                    return;
                }

                URLParams = URLParams.replace("Action=ConfirmOut", "Action=ConfirmOutAgain");
                $.ajax(
                {
                    type: "POST",
                    url: "../../../Handler/Office/SubStoreManager/SubSellOrderAdd.ashx",
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    data: URLParams,
                    beforeSend: function() {
                        AddPop();
                    },
                    error: function() {
                        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
                    },
                    success: function(msg) {
                        if (msg.sta == 1) {
                            $("#Sttl").css("display", "inline");
                            $("#SttlUser").val(data.data.split('#')[2]);
                            $("#SettDate").val(data.data.split('#')[0]);
                            if (parseFloat($("#txtThisPayed").val()) == parseFloat($("#txtWairPayTotal").val())) {//完成
                                //结算信息可见
                                $("#txtCloserName").val(data.data.split('#')[2]);
                                $("#txtCloseDate").val(data.data.split('#')[0]);
                                $("#txtBillStatusName").val("自动结单");
                                $("#txtBillStatusID").val("5");
                                $("#hidBusiStatus").val("4");
                                $("#txtBusiStatus").val("完成");
                            }
                            else {
                                var ThisPayedTotal = 0;
                                if ($("#txtThisPayed").val() != "")
                                    ThisPayedTotal = parseFloat($("#txtThisPayed").val());
                                $("#txtPayedTotal").val(FormatAfterDotNumber(parseFloat($("#txtPayedTotal").val()) + ThisPayedTotal, 2))
                                $("#txtWairPayTotal").val(FormatAfterDotNumber(parseFloat($("#txtWairPayTotal").val()) - ThisPayedTotal, 2))
                                $("#txtThisPayed").val(0);
                                //状态控制
                                $("#txtBillStatusName").val("执行");
                                $("#txtBillStatusID").val("2");
                                $("#hidBusiStatus").val("3");
                                $("#txtBusiStatus").val("结算");
                            }
                            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "确认成功！");
                        }
                        var ThisPayedTotal = 0;
                        if ($("#txtThisPayed").val() != "")
                            ThisPayedTotal = parseFloat($("#txtThisPayed").val());
                        $("#txtPayedTotal").val(FormatAfterDotNumber(parseFloat($("#txtPayedTotal").val()) + ThisPayedTotal, parseInt($("#hidSelPoint").val())))
                        $("#txtWairPayTotal").val(FormatAfterDotNumber(parseFloat($("#txtWairPayTotal").val()) - ThisPayedTotal, parseInt($("#hidSelPoint").val())))
                        $("#txtThisPayed").val(0);
                        fnStatus();
                        fnStatusAndTitle();
                    },
                    complete: function() {
                    }
                })//接收数据完毕
            }
        },
        complete: function() {
        } //接收数据完毕

    });
}

// 根据产品ID获得产品名称
function fnGetProductName(ProductID) {
    ProductID = "," + ProductID + ",";
    var signFrame = findObj("DetailTable", document);
    var Edge = $("#txtTRLastIndex").val();
    var showmessage = "";
    for (var i = 1; i < signFrame.rows.length; ++i) {
        if (signFrame.rows[i].style.display != "none" && ProductID.indexOf("," + $("#ProductID" + i).val() + ",") > -1) {
            showmessage += $("#ProductName" + i).val() + ",";
        }
    }
    if (showmessage.length > 0) {
        showmessage = showmessage.substr(0, showmessage.length - 1);
    }
    return showmessage;
}

//结算确认
function fnConfirmSett() {
    if (!confirm("确认结算么？"))
        return;
    fnTotal(1);
    //校验
    if (fnCheckSett())
        return;
    var URLParams = fnGetBaseInfo();
    URLParams += fnGetSendInfo();
    URLParams += fnGetSetupInfo();
    URLParams += fnGetTotalInfo();
    URLParams += fnGetRemarkInfo();
    URLParams += fnGetDetailInfo();
    //
    URLParams = URLParams.replace("&Action=Update", "&Action=ConfirmSett");
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SubStoreManager/SubSellOrderAdd.ashx",
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {
            AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.sta == 1) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "确认成功！");
                var ThisPayedTotal = 0;
                if ($("#txtThisPayed").val() != "")
                    ThisPayedTotal = parseFloat($("#txtThisPayed").val());
                $("#txtPayedTotal").val(FormatAfterDotNumber(parseFloat($("#txtPayedTotal").val()) + ThisPayedTotal, parseInt($("#hidSelPoint").val())))
                $("#txtWairPayTotal").val(FormatAfterDotNumber(parseFloat($("#txtWairPayTotal").val()) - ThisPayedTotal, parseInt($("#hidSelPoint").val())))
                $("#txtThisPayed").val(0);
                if (parseFloat($("#txtWairPayTotal").val()) == 0) {//余额为0，则结算人，结算时间
                    //结算信息可见
                    $("#Sttl").css("display", "inline");
                    $("#SttlUser").val(data.data.split('#')[2]);
                    $("#SettDate").val(data.data.split('#')[0]);
                    $("#txtCloserName").val(data.data.split('#')[2]);
                    $("#txtCloseDate").val(data.data.split('#')[0]);
                    $("#txtBillStatusName").val("自动结单");
                    $("#txtBillStatusID").val("5");
                    $("#hidBusiStatus").val("4");
                    $("#txtBusiStatus").val("完成");

                }
                else {
                    $("#Sttl").css("display", "inline");
                    $("#SttlUser").val(data.data.split('#')[2]);
                    $("#SettDate").val(data.data.split('#')[0]);
                    $("#txtBillStatusName").val("执行");
                    $("#txtBillStatusID").val("2");
                    $("#hidBusiStatus").val("3");
                    $("#txtBusiStatus").val("结算");
                }
                $("#txtModifiedUserID").val(data.data.split('#')[1]);
                $("#txtModifiedUserName").val(data.data.split('#')[1]);
                $("#txtModifiedDate").val(data.data.split('#')[0]);
                if (parseFloat($("#txtThisPayed").val()) == parseFloat($("#txtWairPayTotal").val())) {//完成
                    //结算信息可见
                    $("#Sttl").css("display", "inline");
                    $("#SttlUser").val(data.data.split('#')[2]);
                    $("#SettDate").val(data.data.split('#')[0]);
                    $("#txtCloserName").val(data.data.split('#')[2]);
                    $("#txtCloseDate").val(data.data.split('#')[0]);
                    $("#txtBillStatusName").val("自动结单");
                    $("#txtBillStatusID").val("5");
                    $("#hidBusiStatus").val("4");
                    $("#txtBusiStatus").val("完成");

                    fnStatus();
                    hidePopup();
                }

            }
        },
        complete: function() { fnStatusAndTitle(); } //接收数据完毕
    });
}
//=================================== End   确      认 ================================


//=================================== Start 校      验 ================================
function fnCheck() {
    var fieldText = "";
    var msgText = "";
    var isErrorFlag = false;

    //基本信息
    //新增时校验单据编号
    if ($("#HiddenAction").val() == "Add") {
        if ($("#OrderNo_ddlCodeRule").val() == "") {
            if ($("#OrderNo_txtCode").val() == "") {
                isErrorFlag = true;
                fieldText += "订单编号|";
                msgText += "请输入订单编号|";
            }
            else {
                if (strlen($.trim($("#OrderNo_txtCode").val())) > 50) {
                    isErrorFlag = true;
                    fieldText += "订单编号|";
                    msgText += "订单编号长度不大于50|";
                }
                if (!CodeCheck($.trim($("#OrderNo_txtCode").val()))) {
                    isErrorFlag = true;
                    fieldText = fieldText + "订单编号|";
                    msgText = msgText + "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
                }
            }
        }
    }

    //主题
    if (strlen($.trim($("#txtTitle").val())) > 100) {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "主题长度不大于100|";
    }

    //销售分店
    if ($("#hidDeptID").val() == "") {
        isErrorFlag = true;
        fieldText += "销售分店|";
        msgText += "请选择销售分店|";
    }

    //业务员
    if ($("#hidSellerID").val() == "") {
        isErrorFlag = true;
        fieldText += "业务员|";
        msgText += "请选择业务员|";
    }

    //客户名称
    if ($("#txtCustName").val() == "") {
        isErrorFlag = true;
        fieldText += "客户名称|";
        msgText += "请输入客户名称|";
    }
    else if (strlen($("#txtCustName").val()) > 200) {
        isErrorFlag = true;
        fieldText += "客户名称|";
        msgText += "客户名称长度不大于200|";
    }
    if (strlen($("#txtCustMobile").val()) > 100) {
        isErrorFlag = true;
        fieldText += "客户手机号|";
        msgText += "客户手机号长度不大于200|";
    }
    if (strlen($("#txtCustAddr").val()) > 200) {
        isErrorFlag = true;
        fieldText += "客户地址|";
        msgText += "客户地址长度不大于200|";
    }

    //合计信息
    //折扣不大于100，不小于0
    var DisCount = $("#txtDiscount").val();
    if (DisCount > 100) {
        isErrorFlag = true;
        fieldText += "整单折扣|";
        msgText += "整单折扣不大于100|";
    }
    else if (DisCount < 0) {
        isErrorFlag = true;
        fieldText += "整单折扣|";
        msgText += "整单折扣不小于0|";
    }
    //结算金额不大于折后含税金额，不小于0
    var PayedTotal = parseFloat($("#txtPayedTotal").val());
    var RealTotal = parseFloat($("#txtRealTotal").val());
    if ($("#txtThisPayed").val() < 0) {
        isErrorFlag = true;
        fieldText += "本次结算金额|";
        msgText += "本次结算金额不能小于0|";
    }
    if (parseFloat($("#txtThisPayed").val()) > parseFloat($("#txtWairPayTotal").val())) {
        isErrorFlag = true;
        fieldText += "本次结算金额|";
        msgText += "本次结算金额不能大于货款余额|";
    }

    //备注信息
    //明细信息
    var signFrame = findObj("DetailTable", document);
    var RealCount = 0;
    var RowCount = signFrame.rows.length;
    for (var i = 1; i < RowCount; ++i) {
        if (signFrame.rows[i].style.display != "none") {
            if ($("#ProductID" + i).val() == "") {
                isErrorFlag = true;
                fieldText += "产品|";
                msgText += "请选择产品|";
            }
            var ProductCount = $("#ProductCount" + i).val();
            if (!IsNumberOrNumeric(ProductCount, 22, parseInt($("#hidSelPoint").val()))) {
                isErrorFlag = true;
                fieldText += "产品数量|";
                msgText += "请输入正确的产品数量|";
            }
            if ($("#ddlSendMode").val() == "2") {
                if ($("#StorageID" + i).val() == "") {
                    isErrorFlag = true;
                    fieldText += "仓库|";
                    msgText += "请选择仓库|";
                }
            }
            ++RealCount;
        }
    }
    if (RealCount == 0) {
        isErrorFlag = true;
        fieldText += "销售订单明细|";
        msgText += "请输入销售订单明细|";
    }
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isErrorFlag = true;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if (isErrorFlag == true) {
        popMsgObj.Show(fieldText, msgText);
    }
    return isErrorFlag;
}

//发货检验
function fnCheckOut() {
    var fieldText = "";
    var msgText = "";
    var isErrorFlag = false;
    if ($("#txtThisPayed").val() < 0) {
        isErrorFlag = true;
        fieldText += "本次结算金额|";
        msgText += "本次结算金额不能小于0|";
    }
    if (parseFloat($("#txtThisPayed").val()) > parseFloat($("#txtWairPayTotal").val())) {
        isErrorFlag = true;
        fieldText += "本次结算金额|";
        msgText += "本次结算金额不能大于货款余额|";
    }
    if (isErrorFlag == true) {
        popMsgObj.Show(fieldText, msgText);
    }
    return isErrorFlag;
}

//结算校验
function fnCheckSett() {
    var fieldText = "";
    var msgText = "";
    var isErrorFlag = false;
    if ($("#txtThisPayed").val() < 0) {
        isErrorFlag = true;
        fieldText += "本次结算金额|";
        msgText += "本次结算金额不能小于0|";
    }
    if (parseFloat($("#txtThisPayed").val()) > parseFloat($("#txtWairPayTotal").val())) {
        isErrorFlag = true;
        fieldText += "本次结算金额|";
        msgText += "本次结算金额不能大于货款余额|";
    }
    if (isErrorFlag == true) {
        popMsgObj.Show(fieldText, msgText);
    }
    return isErrorFlag;
}
//=================================== End   校      验 ================================


//=================================== Start 合      计 ================================
//Flag为点击的是否是增值税，若是则为1，不是为0
//点击的是增值税则将含税价，税率的隐藏域的值赋给显示域，否则将显示域的值赋给隐藏域
function fnTotal(Flag) {
    var isAddTax = document.getElementById("chkisAddTax").checked;
    if (Flag == 1) {
        if (isAddTax) {
            document.getElementById("AddTax").innerHTML = "是增值税";
        }
        else {
            document.getElementById("AddTax").innerHTML = "非增值税";
        }
    }
    var signFrame = findObj("DetailTable", document);
    var RowCount = signFrame.rows.length;
    var CountTotal = 0; //数量合计
    var TotalPrice = 0; //金额合计
    var Tax = 0;       //税额合计
    var TotalFee = 0;  //含税金额合计
    var DiscountTotal = 0; //折扣金额
    var RealTotal = 0;     //折后含税金额
    var ProductCount = 0;
    for (var i = 1; i < RowCount; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var productCountName = "ProductCount" + i;
            var unitPriceName = "UnitPrice" + i;
            //计量单位开启
            if ($("#txtIsMoreUnit").val() == "1") {
                productCountName = "UsedUnitCount" + i;
                unitPriceName = "UsedPrice" + i;
            }
            //数量
            if (!IsNumberOrNumeric($("#" + productCountName).val(), 12, parseInt($("#hidSelPoint").val()))) {
                $("#" + productCountName).val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
            }
            else {
                $("#" + productCountName).val(FormatAfterDotNumber($("#" + productCountName).val(), parseInt($("#hidSelPoint").val())));
            }
            //单价
            if (!IsNumberOrNumeric($("#UnitPrice" + i).val(), 12, parseInt($("#hidSelPoint").val()))) {
                $("#UnitPrice" + i).val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
            }
            if (Flag != 1) {//不是点击增值税激发的            
                if (isAddTax == true) {
                    //含税价
                    if (!IsNumberOrNumeric($("#TaxPrice" + i).val(), 12, parseInt($("#hidSelPoint").val()))) {
                        $("#TaxPrice" + i).val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
                    }
                    //隐藏的含税价
                    $("#TaxPriceHide" + i).val($("#TaxPrice" + i).val());

                    //税率
                    if (!IsNumberOrNumeric($("#TaxRate" + i).val(), 12, parseInt($("#hidSelPoint").val()))) {
                        $("#TaxRate" + i).val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
                    }
                    $("#TaxRateHide" + i).val($("#TaxRate" + i).val());
                }
                else if (isAddTax == false) {
                    $("#TaxPrice" + i).val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
                    $("#TaxRate" + i).val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
                }
            }
            else {//点击增值税激发的，将隐藏域中的值赋给显示域
                if (isAddTax == true) {
                    $("#TaxPrice" + i).val($("#TaxPriceHide" + i).val());
                    $("#TaxRate" + i).val($("#TaxRateHide" + i).val());
                }
                else {
                    $("#TaxPrice" + i).val(FormatAfterDotNumber($("#" + unitPriceName).val(), parseInt($("#hidSelPoint").val())));
                    $("#TaxRate" + i).val(FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())));
                }
            }
            //折扣
            if (!IsNumberOrNumeric($("#Discount" + i).val(), 12, parseInt($("#hidSelPoint").val()))) {
                $("#Discount" + i).val(100);
            }

            $("#TotalPrice" + i).val(FormatAfterDotNumber($("#" + productCountName).val() * $("#" + unitPriceName).val() * $("#Discount" + i).val() / 100, parseInt($("#hidSelPoint").val())));
            $("#TotalFee" + i).val(FormatAfterDotNumber($("#" + productCountName).val() * $("#TaxPrice" + i).val() * $("#Discount" + i).val() / 100, parseInt($("#hidSelPoint").val())));
            $("#TotalTax" + i).val(FormatAfterDotNumber($("#TotalPrice" + i).val() * $("#TaxRate" + i).val(), parseInt($("#hidSelPoint").val())));

            CountTotal = parseFloat(CountTotal) + parseFloat($("#" + productCountName).val());

            TotalPrice = parseFloat(TotalPrice) + parseFloat($("#TotalPrice" + i).val());
            Tax = parseFloat(Tax) + parseFloat($("#TotalTax" + i).val());
            TotalFee = parseFloat(TotalFee) + parseFloat($("#TotalFee" + i).val());
            DiscountTotal = parseFloat(DiscountTotal) + parseFloat($("#" + productCountName).val() * $("#TaxPrice" + i).val() * (100 - $("#Discount" + i).val()) / 100);
        }
    }
    DiscountTotal += TotalFee * (100 - $("#txtDiscount").val()) / 100;
    RealTotal = TotalFee * $("#txtDiscount").val() / 100;
    $("#txtCountTotal").val(FormatAfterDotNumber(CountTotal, parseInt($("#hidSelPoint").val())));
    $("#txtTotalPrice").val(FormatAfterDotNumber(TotalPrice, parseInt($("#hidSelPoint").val())));
    $("#txtTotalTax").val(FormatAfterDotNumber(Tax, parseInt($("#hidSelPoint").val())));
    $("#txtTotalFee").val(FormatAfterDotNumber(TotalFee, parseInt($("#hidSelPoint").val())));
    $("#txtDiscountTotal").val(FormatAfterDotNumber(DiscountTotal, parseInt($("#hidSelPoint").val())));
    $("#txtRealTotal").val(FormatAfterDotNumber(RealTotal, parseInt($("#hidSelPoint").val())));
    $("#txtWairPayTotal").val(FormatAfterDotNumber(RealTotal - parseFloat($("#txtPayedTotal").val() == "" ? "0" : $("#txtPayedTotal").val()), parseInt($("#hidSelPoint").val())));
}

//改变币种同时改变汇率
function fnChangeCurrency() {

    document.getElementById('hiddenRate').value = document.getElementById("txtRate").value;
    var TheSellRate = document.getElementById('hiddenRate').value;
    var IDExchangeRate = document.getElementById("ddlCurrencyType").value;
    document.getElementById("CurrencyTypeID").value = IDExchangeRate.split('_')[0];
    document.getElementById("txtRate").value = IDExchangeRate.split('_')[1];
    var myRate = IDExchangeRate.split('_')[1];
    var signFrame = findObj("DetailTable", document);
    for (var i = 1; i < signFrame.rows.length; ++i) {
        if (signFrame.rows[i].style.display != "none") {
            var myRate = IDExchangeRate.split('_')[1];

            var myUnitPrice = document.getElementById('UnitPrice' + (i).toString());
            var myTaxPrice = document.getElementById('TaxPrice' + (i).toString());
            var myTotalPrice = document.getElementById('TotalPrice' + (i).toString());
            var myTotalFee = document.getElementById('TotalFee' + (i).toString());
            var myTotalTax = document.getElementById('TotalTax' + (i).toString());

            if (parseFloat(TheSellRate) > parseFloat(myRate)) {
                myUnitPrice.value = parseFloat(myUnitPrice.value * TheSellRate).toFixed(2);
                myTaxPrice.value = parseFloat(myTaxPrice.value * TheSellRate).toFixed(2);
                myTotalPrice.value = parseFloat(myTotalPrice.value * TheSellRate).toFixed(2);
                myTotalFee.value = parseFloat(myTotalFee.value * TheSellRate).toFixed(2);
                myTotalTax.value = parseFloat(myTotalTax.value * TheSellRate).toFixed(2);
            }
            else {
                myUnitPrice.value = parseFloat(myUnitPrice.value / myRate).toFixed(2);
                myTaxPrice.value = parseFloat(myTaxPrice.value / myRate).toFixed(2);
                myTotalPrice.value = parseFloat(myTotalPrice.value / myRate).toFixed(2);
                myTotalFee.value = parseFloat(myTotalFee.value / myRate).toFixed(2);
                myTotalTax.value = parseFloat(myTotalTax.value / myRate).toFixed(2);
            }
        }
    }
}
//=================================== End   合      计 ================================



//=================================== Start 返      回 ================================
function fnBack() {
    var URLParams = $("#SearchCondition").val();
    URLParams = URLParams.replace("Page=List", "Page=Add");
    URLParams = URLParams.replace("ModuleID=2121201", "ModuleID=2121202");
    window.location.href = 'SubSellOrderList.aspx' + URLParams;
}
//=================================== End   返      回 ================================

//=================================== Start 明细行操作 ================================
//明细行新增,参数：操作的表格的id
function fnAddSignRow(table) {
    var txtTRLastIndex = findObj("txtTRLastIndex", document);

    var signFrame = findObj(table, document);
    var rowID = signFrame.rows.length;
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    //newTR.id = "DetailSignItem" + rowID; 
    newTR.id = rowID;
    newTR.className = "newrow";
    var i = 0;
    var newNameXH = newTR.insertCell(i++); //添加列:选择    
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input name='Dtlchk' id='Dtlchk" + rowID + "' value=" + rowID + " type='checkbox' size='20' class='tdinput' style='width:90%' />";


    var newNameTD = newTR.insertCell(i++); //添加列:序号
    newNameTD.className = "cell";
    newNameTD.id = "SortNo" + rowID;

    var newProductID = newTR.insertCell(i++); //添加列:物品ID
    newProductID.style.display = "none";
    newProductID.innerHTML = "<input id='ProductID" + rowID + "' type='text'  class='tdinput' style='width:90%' />";

    var newProductNo = newTR.insertCell(i++); //添加列:物品编号
    newProductNo.className = "cell";
    newProductNo.innerHTML = "<input id='ProductNo" + rowID + "' type='text'  onclick=\"GetProduct(" + rowID + ")\" class='tdinput' style='width:90%' />";

    var newProductName = newTR.insertCell(i++); //添加列:物品名称
    newProductName.className = "cell";
    newProductName.innerHTML = "<input id='ProductName" + rowID + "' type='text'  class='tdinput' style='width:90%'/>";
    $("#ProductName" + rowID).attr("disabled", true);

    var newselBatch = newTR.insertCell(i++); //添加列:批次
    newselBatch.className = "cell";
    newselBatch.innerHTML = "<select id='selBatch" + rowID + "' class='tdinput' style='width:90%' >";

    var newSpecification = newTR.insertCell(i++); //添加列:规格
    newSpecification.className = "cell";
    newSpecification.innerHTML = "<input id='Specification" + rowID + "' type='text'  class='tdinput' style='width:90%' />";
    $("#Specification" + rowID).attr("disabled", true);

    var newStorage = newTR.insertCell(i++); //添加列:仓库ID
    newStorage.className = "cell";
    newStorage.innerHTML = "<select class='tdinput' style='width:90%' id='StorageID" + rowID + "'>" + $("#StorageHid").html() + "</select>";
    if ($("#ddlSendMode").val() == "1") {//分店发货仓库不可选
        $("#StorageID" + rowID).attr("disabled", true);
    }

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        var newUnitName = newTR.insertCell(i++); //添加列:单位
        newUnitName.className = "cell";
        newUnitName.innerHTML = "<input  id='UnitName" + rowID + "'type='text'   class='tdinput' style='width:90%' />";
        $("#UnitName" + rowID).attr("disabled", true);

        var newProductCount = newTR.insertCell(i++); //添加列:基本数量
        newProductCount.className = "cell";
        newProductCount.innerHTML = "<input id='ProductCount" + rowID + "' type='text' disabled='disabled' class='tdinput' style='width:90%'/>";

        var newProductCount = newTR.insertCell(i++); //添加列:采购数量
        newProductCount.className = "cell";
        newProductCount.innerHTML = "<input id='UsedUnitCount" + rowID + "' value='" + FormatAfterDotNumber(1, parseInt($("#hidSelPoint").val())) + "' onblur=\"Number_round(this," + $("#hidSelPoint").val() + ");ChangeUnit(this,'" + rowID + "');\"  type='text'  class='tdinput' style='width:90%'/>";
    }
    else {
        var newProductCount = newTR.insertCell(i++); //添加列:采购数量
        newProductCount.className = "cell";
        newProductCount.innerHTML = "<input id='ProductCount" + rowID + "' type='text' value='" + FormatAfterDotNumber(1, parseInt($("#hidSelPoint").val())) + "' onblur=\"Number_round(this," + $("#hidSelPoint").val() + ");fnTotal(1);\" class='tdinput' style='width:90%'/>";
    }

    var newProductCountTH = newTR.insertCell(i++); //添加列:采购数量
    newProductCountTH.className = "cell";
    newProductCountTH.innerHTML = "<input id='ProductCountTH" + rowID + "' type='text' disabled=\"disabled\" class='tdinput' style='width:90%'/>";


    var newUnitID = newTR.insertCell(i++); //添加列:单位ID
    newUnitID.style.display = "none";
    newUnitID.innerHTML = "<input id='UnitID" + rowID + "' type='hidden' class='tdinput' style='width:90%'/>";

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        var newUnitName = newTR.insertCell(i++); //添加列:单位
        newUnitName.className = "cell";
        newUnitName.id = "td_UsedUnitID_" + rowID;
        newUnitName.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;";
    }
    else {
        var newUnitName = newTR.insertCell(i++); //添加列:单位
        newUnitName.className = "cell";
        newUnitName.innerHTML = "<input  id='UnitName" + rowID + "'type='text'  class='tdinput' style='width:90%'   />";
        $("#UnitName" + rowID).attr("disabled", true);
    }

    if ($("#txtIsMoreUnit").val() == "1") {
        var newUnitPrice = newTR.insertCell(i++); //添加列:单价
        newUnitPrice.className = "cell";
        newUnitPrice.innerHTML = "<input id='UnitPrice" + rowID + "'type='hidden'  class='tdinput' style='width:90%' /><input id='UsedPrice" + rowID + "'type='text' class='tdinput' style='width:90%' onchange=\"Number_round(this," + $("#hidSelPoint").val() + ");\" onblur=\"ChangeUnit(this,'" + rowID + "');\" />";
    }
    else {
        var newUnitPrice = newTR.insertCell(i++); //添加列:单价
        newUnitPrice.className = "cell";
        newUnitPrice.innerHTML = "<input id='UnitPrice" + rowID + "' type='text'  class='tdinput' style='width:90%' onchange=\"Number_round(this," + $("#hidSelPoint").val() + ");\" onblur=\"ChangeUnit(this,'" + rowID + "');\" />";
    }


    var newTaxPric = newTR.insertCell(i++); //添加列:含税价
    newTaxPric.className = "cell";
    newTaxPric.innerHTML = "<input id='TaxPrice" + rowID + "'type='text' disabled='disabled'   class='tdinput' style='width:90%'   />";


    var newTaxPricHide = newTR.insertCell(i++); //添加列:含税价隐藏
    newTaxPricHide.style.display = "none";
    newTaxPricHide.innerHTML = "<input id='TaxPriceHide" + rowID + "'type='text'  class='tdinput' style='width:90%'   />";

    var newDiscount = newTR.insertCell(i++); //添加列:折扣
    newDiscount.className = "cell";
    newDiscount.innerHTML = "<input id='Discount" + rowID + "'type='text' disabled='disabled'   class='tdinput' style='width:90%'   />";

    var newTaxRate = newTR.insertCell(i++); //添加列:税率
    newTaxRate.className = "cell";
    newTaxRate.innerHTML = "<input id='TaxRate" + rowID + "'type='text' disabled='disabled'  class='tdinput' style='width:90%'   />";

    var newTaxRateHide = newTR.insertCell(i++); //添加列:税率隐藏
    newTaxRateHide.style.display = "none";
    newTaxRateHide.innerHTML = "<input id='TaxRateHide" + rowID + "'type='text'  class='tdinput' style='width:90%'   />";

    var newTotalPrice = newTR.insertCell(i++); //添加列:金额
    newTotalPrice.className = "cell";
    newTotalPrice.innerHTML = "<input id='TotalPrice" + rowID + "'type='text'  class='tdinput' style='width:90%'  readonly />";
    $("#TotalPrice" + rowID).attr("disabled", true);

    var newTotalFee = newTR.insertCell(i++); //添加列:含税金额
    newTotalFee.className = "cell";
    newTotalFee.innerHTML = "<input id='TotalFee" + rowID + "'type='text'  class='tdinput' style='width:90%' readonly  />";
    $("#TotalFee" + rowID).attr("disabled", true);

    var newTotalTax = newTR.insertCell(i++); //添加列:税额
    newTotalTax.className = "cell";
    newTotalTax.innerHTML = "<input id='TotalTax" + rowID + "'type='text'  class='tdinput' style='width:90%'  readonly />";
    $("#TotalTax" + rowID).attr("disabled", true);

    var newRemark = newTR.insertCell(i++); //添加列:备注
    newRemark.style.display = "none";
    newRemark.innerHTML = "<input id='Remark" + rowID + "'type='text'  class='tdinput' style='width:90%'   />";

    document.getElementById('txtTRLastIndex').value = rowID; //将行号推进下一行
    fnGenerateNo(table);
    return rowID;
}



/***************************************************
*切换单位
***************************************************/
function ChangeUnit(own, rowID, calPrice) {
    var price = 0;
    var rate = 0;
    if ($("#txtIsMoreUnit").val() == "1") {
        CalCulateNum('txtUsedUnit' + rowID, 'UsedUnitCount' + rowID, 'ProductCount' + rowID, '', '', parseInt($("#hidSelPoint").val()));
        var unitPrice = $("#UnitPrice" + rowID).val();
        if (unitPrice != '' && (calPrice == "1") && parseFloat(unitPrice) > 0) {
            $("#UsedPrice" + rowID).val(FormatAfterDotNumber(parseFloat(unitPrice) * parseFloat($("#txtUsedUnit" + rowID).val().split('|')[1]), $("#hidSelPoint").val()));
        }
        price = parseFloat($("#UsedPrice" + rowID).val());
        rate = 1 + parseFloat($("#TaxRate" + rowID).val());
        $("#TaxPrice" + rowID).val(FormatAfterDotNumber(price * rate, parseInt($("#hidSelPoint").val())));
    }
    else {
        price = parseFloat($("#UnitPrice" + rowID).val());
        rate = 1 + parseFloat($("#TaxRate" + rowID).val());
        $("#TaxPrice" + rowID).val(FormatAfterDotNumber(price * rate, parseInt($("#hidSelPoint").val())));
    }
    fnTotal(1);
}

function GetProduct(rowID) {
    if ($("#ddlSendMode").val() == "1") {//分店发货
        popProductInfoSubUC.ShowList(rowID);

    }
    else {
        popTechObj.ShowList(rowID);
    }
}

//分店发货赋值
function popProductInfoSubUCFill(ProductID, ProductNo, ProductName, Specification, UnitID, UnitName, SubPriceTax, SubPrice, SubTax, Discount, IsBatchNo, BatchNo, ProductCount) {
    var RowID = popProductInfoSubUC.objRowID;
    $("#ProductID" + RowID).val(ProductID);
    $("#ProductNo" + RowID).val(ProductNo);
    $("#ProductName" + RowID).val(ProductName);
    $("#Specification" + RowID).val(Specification);
    $("#UnitID" + RowID).val(UnitID);
    $("#UnitName" + RowID).val(UnitName);
    $("#TaxPrice" + RowID).val(FormatAfterDotNumber(SubPriceTax, parseInt($("#hidSelPoint").val())));
    $("#TaxPriceHide" + RowID).val($("#TaxPrice" + RowID).val());
    $("#UnitPrice" + RowID).val(FormatAfterDotNumber(SubPrice, parseInt($("#hidSelPoint").val())));
    $("#TaxRate" + RowID).val(FormatAfterDotNumber(SubTax, parseInt($("#hidSelPoint").val())));
    $("#TaxRateHide" + RowID).val($("#TaxRate" + RowID).val());
    $("#Discount" + RowID).val(FormatAfterDotNumber(Discount, parseInt($("#hidSelPoint").val())));
    if ($("#txtIsMoreUnit").val() == "1") {
        $("#UsedPrice" + RowID).val(FormatAfterDotNumber(SubPrice, parseInt($("#hidSelPoint").val())));
        GetUnitGroupSelectEx(ProductID, 'SaleUnit', 'txtUsedUnit' + RowID, 'ChangeUnit(this,' + RowID + ',1)', "td_UsedUnitID_" + RowID, "", 'ChangeUnit(this,' + RowID + ',1)');
    }
    SetSubBatchSelect(RowID, ProductID, IsBatchNo, BatchNo)
    closeProductInfoSubUCdiv();
}


// 设置分店批次
function SetSubBatchSelect(rowID, ProductID, isBatchNo, BatchNo) {
    // 批次
    GetSubBatchList(ProductID, "selBatch" + rowID, isBatchNo, BatchNo, $("#hidDeptID").val(), $("#hidCompanyCD").val());
}


//明细行删除,参数：操作的表格的id
function fnDelSignRow(table) {
    var signFrame = findObj(table, document);
    for (var i = 1; i < signFrame.rows.length; i++) {
        if ($("#Dtlchk" + i).attr("checked")) {
            signFrame.rows[i].style.display = "none";
        }
    }
    fnGenerateNo(table);
    fnTotal(1); //计算合计信息
}

//生成序号,参数：操作的表格的id
function fnGenerateNo(table) {
    var signFrame = findObj(table, document);
    var SortNo = 1; //起始行号
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (var i2 = 1; i2 < signFrame.rows.length; i2++) {
            if (signFrame.rows[i2].style.display != "none") {
                document.getElementById("SortNo" + i2).innerHTML = SortNo++;
            }
        }
    }
}

//全选
function fnSelectAll(table) {
    $.each($("#" + table + " :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

//总店发货赋值
function Fun_FillParent_Content(id, no, productname, price, unitid, unit, taxrate, taxprice, discount, standard, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, GroupUnitNo,
    SaleUnitID, SaleUnitName, InUnitID, InUnitName, StockUnitID, StockUnitName, MakeUnitID, MakeUnitName, IsBatchNo) {
    var RowID = popTechObj.InputObj;
    $("#ProductID" + RowID).val(id);
    $("#ProductNo" + RowID).val(no);
    $("#ProductName" + RowID).val(productname);
    $("#Specification" + RowID).val(standard);
    $("#UnitPrice" + RowID).val(FormatAfterDotNumber(price, parseInt($("#hidSelPoint").val())));
    $("#UnitID" + RowID).val(unitid);
    $("#UnitName" + RowID).val(unit);
    $("#TaxRate" + RowID).val(FormatAfterDotNumber(taxrate, parseInt($("#hidSelPoint").val())));
    $("#TaxPrice" + RowID).val(FormatAfterDotNumber(taxprice, parseInt($("#hidSelPoint").val())));
    $("#TaxPriceHide" + RowID).val(FormatAfterDotNumber(taxprice, parseInt($("#hidSelPoint").val())));
    $("#Discount" + RowID).val(FormatAfterDotNumber(discount, parseInt($("#hidSelPoint").val())));
    if ($("#txtIsMoreUnit").val() == "1") {
        $("#UsedPrice" + RowID).val(FormatAfterDotNumber(price, parseInt($("#hidSelPoint").val())));
        GetUnitGroupSelectEx(id, 'SaleUnit', 'txtUsedUnit' + RowID, 'ChangeUnit(this,' + RowID + ',1)', "td_UsedUnitID_" + RowID, "", 'ChangeUnit(this,' + RowID + ',1)');
    }
    SetBatchSelect(RowID, id, IsBatchNo, "")
    fnTotal(1);
}


// 设置总店批次
function SetBatchSelect(rowID, ProductID, isBatchNo, BatchNo) {
    // 批次
    GetBatchList(ProductID, "StorageID" + rowID, "selBatch" + rowID, isBatchNo, BatchNo);
    $("#StorageID" + rowID).change(function() {
        GetBatchList(ProductID, "StorageID" + rowID, "selBatch" + rowID, isBatchNo, BatchNo);
    });
}
//清空物品
function fnDropProduct() {
    var signFrame = findObj("DetailTable", document);
    for (var i = signFrame.rows.length - 1; i > 0; --i) {
        signFrame.deleteRow(i);
    }
}


//更改发货模式时
function fnChangeSendMode() {
    //清空物品
    fnDropProduct();
    var SendMode = $("#ddlSendMode").val();
    var signFrame = findObj("DetailTable", document);
    var i;
    if (SendMode == "1") {//分店发货
        for (i = 1; i < signFrame.rows.length; ++i) {
            $("#StorageID" + i).val("");
            $("#StorageID" + i).attr("disabled", true);
        }
    }
    else if (SendMode == "2") {//总部发货
        for (i = 1; i < signFrame.rows.length; ++i) {
            $("#StorageID" + i).attr("disabled", false);
        }
    }
}
//=================================== End   明细行操作 ================================

//=================================== Start 门店人员弹出 ================================
//门店人员弹出
function fnAltShadeDiv() {
    /**第一步：创建DIV遮罩层。*/
    var sWidth, sHeight;
    sWidth = window.screen.availWidth;
    if (window.screen.availHeight > document.body.scrollHeight) {  //当高度少于一屏
        sHeight = window.screen.availHeight;
    } else {//当高度大于一屏
        sHeight = document.body.scrollHeight;
    }
    //创建遮罩背景
    var maskObj = document.createElement("div");
    maskObj.setAttribute('id', 'HidenDiv');
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

//门店人员关闭
function fnCloseSubUser() {

}

//=================================== End   门店人员弹出 ================================

//=================================== Start 客户弹出层处理 ================================
//显示
function fnShowCust() {
    $("#txtCustNameSelect").val("");
    $("#txtCustTelSelect").val("");
    $("#txtCustMobileSelect").val("");
    $("#txtCustAddrSelect").val("");

    fnSelectCust();
    $("#layoutCust").css("display", "");
    openRotoscopingDiv(true, "divCust", "frmCust");
}

//关闭
function fnCloseCust() {
    $("#layoutCust").css("display", "none");
    closeRotoscopingDiv(true, "divCust");
}

//获取检索条件
function fnGetSelectCustCondition() {
    var CustName = escape($("#txtCustNameSelect").val());
    var CustTel = escape($("#txtCustTelSelect").val());
    var CustMobile = escape($("#txtCustMobileSelect").val());
    var CustAddr = escape($("#txtCustAddrSelect").val());
    var DeptID = escape($("#hidDeptID").val());

    var URLParams = "Action=GetCust";
    URLParams += "&CustName=" + CustName;
    URLParams += "&CustTel=" + CustTel;
    URLParams += "&CustMobile=" + CustMobile;
    URLParams += "&CustAddr=" + CustAddr;
    URLParams += "&DeptID=" + DeptID;

    $("#hidCustSearch").val(URLParams);
    return URLParams;
}

function fnCheckCust() {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;


    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    return isFlag;
}

//检索
function fnSelectCust() {
    if (!fnCheckCust())
        return;
    fnGetSelectCustCondition();
    TurnToPageCustInfo(1)
}
var pageCountCustInfo = 10; //每页计数
var totalRecordCustInfo = 0;
var pagerStyleCustInfo = "flickr"; //jPagerBar样式
var ifshowCustInfo = "0";
var currentPageIndexCustInfo = 1;
var orderByCustInfo = ""; //排序字段
var reset = "1";
var action = "";
function TurnToPageCustInfo(pageIndex) {
    var URLParams = $("#hidCustSearch").val();

    URLParams += "&orderBy=" + orderByCustInfo;
    URLParams += "&pageCount=" + pageCountCustInfo;
    URLParams += "&pageIndex=" + pageIndex;
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SubStoreManager/SubSellOrderAdd.ashx",
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {
            AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(msg) {
            $("#pageDataList1CustInfo tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" onclick=\"fnFillCustInfo('" + item.CustName + "','" + item.CustTel + "','" + item.CustMobile + "','" + item.CustAddr + "');\" />" + "</td>" +
                    "<td height='22' align='center'>" + item.CustName + "</td>" +
                    "<td height='22' align='center'>" + item.CustTel + "</td>" +
                    "<td height='22' align='center'>" + item.CustMobile + "</td>" +
                    "<td height='22' align='center'>" + item.CustAddr + "</td>").appendTo($("#pageDataList1CustInfo tbody"));
            });
            //页码
            ShowPageBar("pageDataList1_PagerCustInfo", //[containerId]提供装载页码栏的容器标签的客户端ID
               "<%= Request.Url.AbsolutePath %>", //[url]
                {style: pagerStyleCustInfo, mark: "pageDataList1MarkCustInfo",

                totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCountCustInfo, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上页", nextWord: "下页", First: "首页", End: "末页",
                onclick: "TurnToPageCustInfo({pageindex});return false;"}//[attr]
                );

            totalRecordCustInfo = msg.totalCount;
            // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
            document.getElementById("Text2CustInfo").value = msg.totalCount;
            $("#ShowPageCountCustInfo").val(pageCountCustInfo);
            ShowTotalPage(msg.totalCount, pageCountCustInfo, pageIndex, $("#pageCustInfocount"));
            $("#ToPageCustInfo").val(pageIndex);
        },
        complete: function() {
            hidePopup();
            $("#pageDataList1_PagerCustInfo").show(); IfshowCustInfo(document.getElementById("Text2CustInfo").value); pageDataList1("pageDataList1CustInfo", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
        } //接收数据完毕
    });
}

function fnFillCustInfo(CustName, CustTel, CustMobile, CustAddr) {
    $("#txtCustName").val(CustName);
    $("#txtCustTel").val(CustTel);
    $("#txtCustMobile").val(CustMobile);
    $("#txtCustAddr").val(CustAddr);
    fnCloseCust();
}

//改变每页记录数及跳至页数
function ChangePageCountIndexCustInfo(newPageCountProvider, newPageIndexProvider) {
    if (newPageCountProvider <= 0 || newPageIndexProvider <= 0 || newPageIndexProvider > ((totalRecordCustInfo - 1) / newPageCountProvider) + 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
        return false;
    }
    else {
        ifshow = "0";
        this.pageCountCustInfo = parseInt(newPageCountProvider);
        TurnToPageCustInfo(parseInt(newPageIndexProvider));
    }
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

function IfshowCustInfo(count) {
    if (count == "0") {
        document.getElementById("divPageCustInfo").style.display = "none";
        document.getElementById("pageCustInfocount").style.display = "none";
    }
    else {
        document.getElementById("divPageCustInfo").style.display = "";
        document.getElementById("pageCustInfocount").style.display = "";
    }
}

//排序
function OrderBy(orderColum, orderTip) {
    ifshow = "0";
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
    orderByCustInfo = orderColum + "_" + ordering;
    TurnToPageCustInfo(1);
}
//=================================== End   客户弹出层处理 ================================

//=================================== Start   列表页面进入填充 ================================
function fnFill(ID, OutSttl) {
    var URLParams = "Action=Fill&ID=" + ID;
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SubStoreManager/SubSellOrderAdd.ashx",
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {
            AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(msg) {
            $.each(msg.SubSellOrderPri, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    fnFillSubSellOrder(item);

                    // 填充扩展属性
                    GetExtAttr("officedba.SubSellOrder", item);
                }
            });
            $.each(msg.SubSellOrderDetail, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    fnFillSubSellOrderDetail(item);
                }
            });
            fnStatus();
            //            if(OutSttl == "OutSttl")
            //            {//点击出库或结算
            if ($("#txtBillStatusID").val() == "1") {
                document.getElementById("divTitle").innerHTML = "销售订单";
            }
            if (($("#txtBillStatusID").val() == "2") && ($("#hidBusiStatus").val() == "2")) {//销售发货时界面上除了发货信息可改，其他信息不可改
                document.getElementById("divTitle").innerHTML = "销售订单--出库";
                fnStatusOut();
            }
            else if (($("#txtBillStatusID").val() == "2") && ($("#hidBusiStatus").val() == "3")) {//结算
                document.getElementById("divTitle").innerHTML = "销售订单--结算";
                fnStatusAfterOut();
            }
            else if ($("#hidBusiStatus").val() == "4") {
                document.getElementById("divTitle").innerHTML = "销售订单--完成";
                //更换标题
                $("#AddTitle").css("display", "none");
                $("#UpdateTitle").css("display", "none");
                $("#OutTitle").css("display", "none");
                $("#SettTitle").css("display", "inline");

                //按钮控制
                try {
                    document.getElementById('btnGetGoods').style.display = 'none';
                    document.getElementById('unbtnGetGoods').style.display = '';
                }
                catch (e) { }
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "none");
                $("#imgConfirm").css("display", "none");
                $("#imgConfirmOut").css("display", "none");
                $("#imgConfirmSett").css("display", "none");
                $("#imgUnConfirm").css("display", "inline");

                $("#imgAdd").css("display", "none");
                $("#imgDel").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgUnDel").css("display", "inline");
                //所有text不可用
                $(":text").each(function() {
                    this.disabled = true;
                });
                //所有checkbox不可用
                $(":checkbox").each(function() {
                    this.disabled = true;
                });

                //下拉列表不可用
                $("#ddlSendMode").attr("disabled", true);
                $("#ddlCurrencyType").attr("disabled", true);
                $("#ddlMoneyType_ddlCodeType").attr("disabled", true);
                $("#ddlTakeType_ddlCodeType").attr("disabled", true);
                $("#ddlPayType_ddlCodeType").attr("disabled", true);
                $("#ddlOrderMethod_ddlCodeType").attr("disabled", true);
                $("#ddlMoneyType_ddlCodeType").attr("disabled", true);
                $("#ddlPlanSetHour").attr("disabled", true);
                $("#ddlPlanSetMin").attr("disabled", true);
                $("#ddlSetHour").attr("disabled", true);
                $("#ddlSetMin").attr("disabled", true);

                //发货信息不可用
                $("#txtPlanOutDate").attr("disabled", true);
                $("#ddlPlanOutHour").attr("disabled", true);
                $("#ddlPlanOutMin").attr("disabled", true);
                $("#ddlCarryType_ddlCodeType").attr("disabled", true);
                $("#DeptOutDeptName").attr("disabled", true);

                $("#txtOutDate").attr("disabled", true);
                $("#ddlOutHour").attr("disabled", true);
                $("#ddlOutMin").attr("disabled", true);
                $("#UserOutUserName").attr("disabled", true);
                $("#txtCustAddr").attr("disabled", true);

                //安装信息不可用
                $("#chkNeedSetup").attr("disabled", true);
                fnChangeNeedSetup();


                $("#ColCust").css("display", "none");
                $("#ColCust2").css("display", "inline");
            }
            //            }
            //            else
            //            {//点击编号
            //                fnStatusFromBill();
            //            }
        },
        complete: function() {
            hidePopup();
        } //接收数据完毕
    });

}

//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
    //有扩展属性才取值
    if (strKey != '') {
        var arrKey = strKey.split('|');
        for (var t = 0; t < arrKey.length; t++) {
            //不为空的字段名才取值
            if ($.trim(arrKey[t]) != '') {
                $("#" + $.trim(arrKey[t])).val(data[$.trim(arrKey[t])]);
            }
        }
    }
}

//填充基本信息
function fnFillBaseInfo(item) {
    $("#HiddenAction").val("Update");

    //基本信息
    $("#AddTitle").css("display", "none");
    $("#UpdateTitle").css("display", "inline");

    document.getElementById('OrderNo_txtCode').className = 'tdinput';
    document.getElementById('OrderNo_txtCode').style.width = '90%';
    $("#OrderNo_txtCode").attr("disabled", true);
    $('#OrderNo_ddlCodeRule').css("display", "none");
    $("#OrderNo_txtCode").val(item.OrderNo);
    $("#ThisID").val(item.ID);
    $("#txtTitle").val(item.Title);
    $("#hidDeptID").val(item.DeptID);
    $("#txtDeptName").val(item.DeptName);
    $("#ddlSendMode").val(item.SendMode);
    $("#hidSellerID").val(item.Seller);
    $("#UserSellerName").val(item.SellerName);
    $("#txtCustName").val(item.CustName);

    $("#txtCustTel").val(item.CustTel);
    $("#txtCustMobile").val(item.CustMobile);
    $("#txtCustAddr").val(item.CustAddr);

    for (var i = 0; i < document.getElementById("ddlCurrencyType").options.length; ++i) {
        if (document.getElementById("ddlCurrencyType").options[i].value.split('_')[0] == item.CurrencyType) {
            $("#ddlCurrencyType").attr("selectedIndex", i);
            break;
        }
    }

    $("#txtRate").val(item.Rate);

    $("#ddlOrderMethod_ddlCodeType").val(item.OrderMethod);
    $("#ddlTakeType_ddlCodeType").val(item.TakeType);
    $("#ddlPayType_ddlCodeType").val(item.PayType);
    $("#ddlMoneyType_ddlCodeType").val(item.MoneyType);
    $("#txtOrderDate").val(item.OrderDate);
    $("#chkisAddTax").checked = item.isAddTax == "1" ? true : false;
}

//填充发货信息
function fnFillSendInfo(item) {
    if (item.PlanOutDate != "") {
        $("#txtPlanOutDate").val(item.PlanOutDate.split(' ')[0]);
        $("#ddlPlanOutHour").val(item.PlanOutDate.split(' ')[1].split(':')[0]);
        $("#ddlPlanOutMin").val(item.PlanOutDate.split(' ')[1].split(':')[1]);
    }
    if (item.OutDate != "") {
        $("#txtOutDate").val(item.OutDate.split(' ')[0])
        $("#ddlOutHour").val(item.OutDate.split(' ')[1].split(':')[0])
        $("#ddlOutMin").val(item.OutDate.split(' ')[1].split(':')[1]);
    }
    $("#ddlCarryType_ddlCodeType").val(item.CarryType);
    $("#hidBusiStatus").val(item.BusiStatus);
    $("#txtBusiStatus").val(item.BusiStatusName);
    $("#hidOutDeptID").val(item.OutDeptID);
    $("#DeptOutDeptName").val(item.OutDeptName);
    $("#hidOutUserID").val(item.OutUserID);
    $("#UserOutUserName").val(item.OutUserName);
}
//填充安装信息
function fnFillSetupInfo(item) {
    document.getElementById("chkNeedSetup").checked = item.NeedSetup == "1" ? true : false;
    var PlanSetDate = item.PlanSetDate;
    var Date;
    var Hour;
    var Min;
    if (item.PlanSetDate != "") {
        Date = PlanSetDate.split(' ')[0];
        Hour = PlanSetDate.split(' ')[1].split(':')[0];
        Min = PlanSetDate.split(' ')[1].split(':')[1];
        $("#txtPlanSetDate").val(Date)
        $("#ddlPlanSetHour").val(Hour)
        $("#ddlPlanSetMin").val(Min);
    }
    if (item.SetDate != "") {
        var SetDate = item.SetDate;
        Date = SetDate.split(' ')[0];
        Hour = SetDate.split(' ')[1].split(':')[0];
        Min = SetDate.split(' ')[1].split(':')[1];
        $("#txtSetDate").val(Date)
        $("#ddlSetHour").val(Hour)
        $("#ddlSetMin").val(Min);
    }
    $("#hidSetUserID").val(item.SetUserID);
    $("#UserSetUserID").val(item.SetUserInfo);
}

//填充合计信息
function fnFillTotalInfo(item) {
    $("#txtCountTotal").val(FormatAfterDotNumber(item.CountTotal, parseInt($("#hidSelPoint").val())));
    $("#txtTotalPrice").val(FormatAfterDotNumber(item.TotalPrice, parseInt($("#hidSelPoint").val())));
    $("#txtTotalTax").val(FormatAfterDotNumber(item.Tax, parseInt($("#hidSelPoint").val())));
    $("#txtTotalFee").val(FormatAfterDotNumber(item.TotalFee, parseInt($("#hidSelPoint").val())));
    $("#txtDiscount").val(FormatAfterDotNumber(item.Discount, parseInt($("#hidSelPoint").val())));

    $("#txtDiscountTotal").val(FormatAfterDotNumber(item.DiscountTotal, parseInt($("#hidSelPoint").val())));
    $("#txtRealTotal").val(FormatAfterDotNumber(item.RealTotal, parseInt($("#hidSelPoint").val())));
    $("#txtPayedTotal").val(FormatAfterDotNumber(item.PayedTotal, parseInt($("#hidSelPoint").val())));
    $("#txtWairPayTotal").val(FormatAfterDotNumber(item.WairPayTotal, parseInt($("#hidSelPoint").val())));
}

//填充备注信息
function fnFillRemarkInfo(item) {
    $("#txtCreatorID").val(item.Creator);
    $("#txtCreatorName").val(item.CreatorName);
    $("#txtCreateDate").val(item.CreateDate);
    $("#txtBillStatusID").val(item.BillStatus);
    $("#txtBillStatusName").val(item.BillStatusName);
    $("#txtConfirmorID").val(item.Confirmor);
    $("#txtConfirmorName").val(item.ConfirmorName);
    $("#txtConfirmorDate").val(item.ConfirmDate);

    $("#txtCloserID").val(item.Closer);
    $("#txtCloserName").val(item.CloserName);
    $("#txtCloseDate").val(item.CloseDate);
    $("#txtModifiedUserID").val(item.ModifiedUserID);
    $("#txtModifiedUserName").val(item.ModifiedUserID);
    $("#txtModifiedDate").val(item.ModifiedDate);
    $("#txtRemark").val(item.Remark);
    $("#hfPageAttachment").val(item.Attachment);

    if (item.Attachment != "") {
        //下载删除显示
        document.getElementById("divDealResume").style.display = "block";
        //上传不显示
        document.getElementById("divUploadResume").style.display = "none"
    }
}
//填充结算信息
function fnFillSttlInfo(item) {
    //结算信息可见
    $("#Sttl").css("display", "inline");

    var isOpenBill = item.isOpenbill == "1" ? true : false;
    $("#chkisOpenbill").attr("checked", isOpenBill);
    fnChangeOpenBill();
    $("#SttlUser").val(item.SttlUserName);
    $("#SttlUserID").val(item.SttlUserID);

    $("#SettDate").val(item.SttlDate);

}
//填充主表
function fnFillSubSellOrder(item) {
    fnFillBaseInfo(item);
    fnFillSendInfo(item);
    fnFillSetupInfo(item);
    fnFillTotalInfo(item);
    fnFillRemarkInfo(item);
    fnFillSttlInfo(item);
}

//填充明细
function fnFillSubSellOrderDetail(item) {
    var i = fnAddSignRow('DetailTable');
    $("#ProductID" + i).val(item.ProductID);
    $("#ProductNo" + i).val(item.ProductNo);
    $("#ProductName" + i).val(item.ProductName);
    $("#StorageID" + i).val(item.StorageID);
    $("#Specification" + i).val(item.Specification);
    $("#ProductCount" + i).val(FormatAfterDotNumber(item.ProductCount, parseInt($("#hidSelPoint").val())));
    $("#ProductCountTH" + i).val(FormatAfterDotNumber(item.BackCount, parseInt($("#hidSelPoint").val())));
    $("#UnitID" + i).val(item.UnitID);
    $("#UnitName" + i).val(item.UnitName);
    $("#UnitPrice" + i).val(FormatAfterDotNumber(item.UnitPrice, parseInt($("#hidSelPoint").val())));
    $("#TaxPrice" + i).val(FormatAfterDotNumber(item.TaxPrice, parseInt($("#hidSelPoint").val())));
    $("#TaxPriceHide" + i).val($("#TaxPrice" + i).val());
    $("#Discount" + i).val(FormatAfterDotNumber(item.Discount, parseInt($("#hidSelPoint").val())));
    $("#TaxRate" + i).val(FormatAfterDotNumber(item.TaxRate, parseInt($("#hidSelPoint").val())));
    $("#TaxRateHide" + i).val($("#TaxRate" + i).val());
    $("#TotalFee" + i).val(FormatAfterDotNumber(item.TotalFee, parseInt($("#hidSelPoint").val())));
    $("#TotalPrice" + i).val(FormatAfterDotNumber(item.TotalPrice, parseInt($("#hidSelPoint").val())));

    $("#TotalTax" + i).val(FormatAfterDotNumber(item.TotalTax, parseInt($("#hidSelPoint").val())));
    $("#Remark" + i).val(item.Remark);
    if ($("#txtIsMoreUnit").val() == "1") {
        $("#UsedPrice" + i).val(FormatAfterDotNumber(item.UsedPrice, parseInt($("#hidSelPoint").val())));
        $("#UsedUnitCount" + i).val(FormatAfterDotNumber(item.UsedUnitCount, parseInt($("#hidSelPoint").val())));
        GetUnitGroupSelect(item.ProductID, 'SaleUnit', 'txtUsedUnit' + i, 'ChangeUnit(this,' + i + ',1)', "td_UsedUnitID_" + i, item.UsedUnitID);
    }
    if ($("#ddlSendMode").val() == "1") {//分店发货
        SetSubBatchSelect(i, item.ProductID, item.IsBatchNo, item.BatchNo);
    }
    else {
        SetBatchSelect(i, item.ProductID, item.IsBatchNo, item.BatchNo);
    }
}



//点击出库链接过来时，页面显示的状态
function fnStatusOut() {
    //更换标题
    document.getElementById("divTitle").innerHTML = "销售订单--出库";
    //所有text不可用
    $(":text").each(function() {
        this.disabled = true;
    });
    //所有checkbox不可用
    $(":checkbox").each(function() {
        this.disabled = true;
    });

    //下拉列表不可用
    $("#ddlSendMode").attr("disabled", true);
    $("#ddlCurrencyType").attr("disabled", true);
    $("#ddlMoneyType_ddlCodeType").attr("disabled", true);
    $("#ddlTakeType_ddlCodeType").attr("disabled", true);
    $("#ddlPayType_ddlCodeType").attr("disabled", true);
    $("#ddlOrderMethod_ddlCodeType").attr("disabled", true);
    $("#ddlMoneyType_ddlCodeType").attr("disabled", true);
    $("#ddlPlanSetHour").attr("disabled", true);
    $("#ddlPlanSetMin").attr("disabled", true);
    $("#ddlSetHour").attr("disabled", true);
    $("#ddlSetMin").attr("disabled", true);

    //发货信息可用
    $("#txtPlanOutDate").attr("disabled", false);
    $("#ddlPlanOutHour").attr("disabled", false);
    $("#ddlPlanOutMin").attr("disabled", false);
    $("#ddlCarryType_ddlCodeType").attr("disabled", false);
    $("#DeptOutDeptName").attr("disabled", false);

    $("#txtOutDate").attr("disabled", false);
    $("#ddlOutHour").attr("disabled", false);
    $("#ddlOutMin").attr("disabled", false);
    $("#UserOutUserName").attr("disabled", false);
    $("#txtCustAddr").attr("disabled", false);

    $("#txtThisPayed").attr("disabled", false);

    //安装信息可用
    $("#chkNeedSetup").attr("disabled", false);
    fnChangeNeedSetup();

    //按钮控制
    try {
        document.getElementById('btnGetGoods').style.display = 'none';
        document.getElementById('unbtnGetGoods').style.display = '';
    }
    catch (e) { }
    $("#imgSave").css("display", "none");
    $("#imgUnSave").css("display", "none");
    $("#imgAdd").css("display", "none");
    $("#imgUnAdd").css("display", "inline");
    $("#imgDel").css("display", "none");
    $("#imgUnDel").css("display", "inline");
    $("#imgConfirm").css("display", "none");
    $("#imgUnConfirm").css("display", "none");
    $("#imgConfirmOut").css("display", "inline");
    $("#imgConfirmSett").css("display", "none");


    $("#ColCust").css("display", "none");
    $("#ColCust2").css("display", "inline");
}

function fnStatusAfterOut() {//销售发货确认后界面的状态
    //更换标题
    document.getElementById("divTitle").innerHTML = "销售订单--结算";

    //按钮控制
    try {
        document.getElementById('btnGetGoods').style.display = 'none';
        document.getElementById('unbtnGetGoods').style.display = '';
    }
    catch (e) { }
    $("#imgSave").css("display", "none");
    $("#imgUnSave").css("display", "none");
    $("#imgConfirm").css("display", "none");
    $("#imgConfirmOut").css("display", "none");
    $("#imgConfirmSett").css("display", "inline");
    $("#imgUnConfirm").css("display", "none");

    $("#imgAdd").css("display", "none");
    $("#imgDel").css("display", "none");
    $("#imgUnAdd").css("display", "inline");
    $("#imgUnDel").css("display", "inline");
    $("#btn_Qxconfirm").css("display", "none");
    $("#btn_UnQxconfirm").css("display", "none");

    //下拉列表不可用
    $("#ddlSendMode").attr("disabled", true);
    $("#ddlCurrencyType").attr("disabled", true);
    $("#ddlMoneyType_ddlCodeType").attr("disabled", true);
    $("#ddlTakeType_ddlCodeType").attr("disabled", true);
    $("#ddlPayType_ddlCodeType").attr("disabled", true);
    $("#ddlOrderMethod_ddlCodeType").attr("disabled", true);
    $("#ddlMoneyType_ddlCodeType").attr("disabled", true);
    $("#ddlPlanSetHour").attr("disabled", true);
    $("#ddlPlanSetMin").attr("disabled", true);
    $("#ddlSetHour").attr("disabled", true);
    $("#ddlSetMin").attr("disabled", true);

    //发货信息不可用
    $("#txtPlanOutDate").attr("disabled", true);
    $("#ddlPlanOutHour").attr("disabled", true);
    $("#ddlPlanOutMin").attr("disabled", true);
    $("#ddlCarryType_ddlCodeType").attr("disabled", true);
    $("#DeptOutDeptName").attr("disabled", true);

    $("#txtOutDate").attr("disabled", true);
    $("#ddlOutHour").attr("disabled", true);
    $("#ddlOutMin").attr("disabled", true);
    $("#UserOutUserName").attr("disabled", true);
    $("#txtCustAddr").attr("disabled", true);

    $("#txtThisPayed").attr("disabled", false);

    //安装信息可用
    $("#chkNeedSetup").attr("disabled", false);
    fnChangeNeedSetup();

    //结算信息显示
    $("#Sttl").css("display", "inline");
    $("#chkisOpenbill").attr("disabled", false);

    document.getElementById('txtDiscount').disabled = true;
    $("#ColCust").css("display", "none");
    $("#ColCust2").css("display", "inline");
    var signFrame = findObj("DetailTable", document);
    for (var i = 1; i < signFrame.rows.length; ++i) {
        if (signFrame.rows[i].style.display != "none") {
            document.getElementById('ProductCount' + i).disabled = true;
            document.getElementById('ProductNo' + i).disabled = true;
        }
    }
}

//结算时
function fnStatusSett() {
    //更换标题
    document.getElementById("divTitle").innerHTML = "销售订单--完成";

    //所有text不可用
    $(":text").each(function() {
        this.disabled = true;
    });
    //所有checkbox不可用
    $(":checkbox").each(function() {
        this.disabled = true;
    });

    //下拉列表不可用
    $("#ddlSendMode").attr("disabled", true);
    $("#ddlCurrencyType").attr("disabled", true);
    $("#ddlMoneyType_ddlCodeType").attr("disabled", true);
    $("#ddlTakeType_ddlCodeType").attr("disabled", true);
    $("#ddlPayType_ddlCodeType").attr("disabled", true);
    $("#ddlOrderMethod_ddlCodeType").attr("disabled", true);
    $("#ddlMoneyType_ddlCodeType").attr("disabled", true);
    $("#ddlPlanSetHour").attr("disabled", true);
    $("#ddlPlanSetMin").attr("disabled", true);
    $("#ddlSetHour").attr("disabled", true);
    $("#ddlSetMin").attr("disabled", true);

    $("#txtPlanOutDate").attr("disabled", true);
    $("#ddlPlanOutHour").attr("disabled", true);
    $("#ddlPlanOutMin").attr("disabled", true);

    //安装信息可用
    $("#chkNeedSetup").attr("disabled", false);
    fnChangeNeedSetup();

    $("#txtPayedTotal").attr("disabled", false);

    $("#ColCust").css("display", "none");
    $("#ColCust2").css("display", "inline");
}

//结算完成后
function fnStatusAfterSett() {
    //余额为0，业务状态为完成，大于0为结算
    if (parseFloat($("#txtWairPayTotal").val()) == 0) {
        $("#txtBusiStatus").val("完成");
        $("#hidBusiStatus").val("4");
        $("#txtBillStatusName").val("自动结单");
        $("#txtBillStatusID").val("5");

        //确认标灰
        $("#imgConfirmSett").css("display", "none");
        $("#imgUnConfirm").css("display", "inline");
    }
    $("#btn_Qxconfirm").css("display", "none");
    $("#btn_UnQxconfirm").css("display", "none");
}

//完成
function fnStatusOver() {

}
//=================================== End     列表页面进入填充 ================================

//=================================== Start   判断按钮状态 ================================
//从单据编号链接过来时按钮状态
function fnStatusFromBill() {
    var BillStatus = $("#txtBillStatusID").val()//单据状态
    var BusiStatus = $("#hidBusiStatus").val()//业务状态
    if (BillStatus == "1") {//制单状态
        //更换标题
        document.getElementById("divTitle").innerHTML = "销售订单";
        //按钮控制
        try {
            document.getElementById('btnGetGoods').style.display = '';
            document.getElementById('unbtnGetGoods').style.display = 'none';
        }
        catch (e) { }
        $("#imgSave").css("display", "inline");
        $("#imgUnSave").css("display", "none");
        $("#imgConfirm").css("display", "inline");
        $("#imgConfirmOut").css("display", "none");
        $("#imgConfirmSett").css("display", "none");
        $("#imgUnConfirm").css("display", "none");
        $("#imgAdd").css("display", "inline");
        $("#imgDel").css("display", "inline");
        $("#imgUnAdd").css("display", "none");
        $("#imgUnDel").css("display", "none");
    }
    else if (BillStatus == "2") {
        if (BusiStatus == "2") {//出库
            //更换标题
            document.getElementById("divTitle").innerHTML = "销售订单";
            //按钮控制
            try {
                document.getElementById('btnGetGoods').style.display = 'none';
                document.getElementById('unbtnGetGoods').style.display = '';
            }
            catch (e) { }
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgConfirm").css("display", "none");
            $("#imgConfirmOut").css("display", "none");
            $("#imgConfirmSett").css("display", "none");
            $("#imgUnConfirm").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#imgDel").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgUnDel").css("display", "inline");
        }
        else if (BusiStatus == "3") {//结算
            //更换标题
            document.getElementById("divTitle").innerHTML = "销售订单";
            //按钮控制
            try {
                document.getElementById('btnGetGoods').style.display = 'none';
                document.getElementById('unbtnGetGoods').style.display = '';
            }
            catch (e) { }
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgConfirm").css("display", "none");
            $("#imgConfirmOut").css("display", "none");
            $("#imgConfirmSett").css("display", "none");
            $("#imgUnConfirm").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#imgDel").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgUnDel").css("display", "inline");
        }
    }
    if (BusiStatus == "4") {
        //更换标题
        document.getElementById("divTitle").innerHTML = "销售订单--完成";

        //按钮控制
        try {
            document.getElementById('btnGetGoods').style.display = 'none';
            document.getElementById('unbtnGetGoods').style.display = '';
        }
        catch (e) { }
        $("#imgSave").css("display", "none");
        $("#imgUnSave").css("display", "none");
        $("#imgConfirm").css("display", "none");
        $("#imgConfirmOut").css("display", "none");
        $("#imgConfirmSett").css("display", "none");
        $("#imgUnConfirm").css("display", "inline");

        $("#imgAdd").css("display", "none");
        $("#imgDel").css("display", "none");
        $("#imgUnAdd").css("display", "inline");
        $("#imgUnDel").css("display", "inline");
        //所有text不可用
        $(":text").each(function() {
            this.disabled = true;
        });
        //所有checkbox不可用
        $(":checkbox").each(function() {
            this.disabled = true;
        });

        //下拉列表不可用
        $("#ddlSendMode").attr("disabled", true);
        $("#ddlCurrencyType").attr("disabled", true);
        $("#ddlMoneyType_ddlCodeType").attr("disabled", true);
        $("#ddlTakeType_ddlCodeType").attr("disabled", true);
        $("#ddlPayType_ddlCodeType").attr("disabled", true);
        $("#ddlOrderMethod_ddlCodeType").attr("disabled", true);
        $("#ddlMoneyType_ddlCodeType").attr("disabled", true);
        $("#ddlPlanSetHour").attr("disabled", true);
        $("#ddlPlanSetMin").attr("disabled", true);
        $("#ddlSetHour").attr("disabled", true);
        $("#ddlSetMin").attr("disabled", true);

        //发货信息不可用
        $("#txtPlanOutDate").attr("disabled", true);
        $("#ddlPlanOutHour").attr("disabled", true);
        $("#ddlPlanOutMin").attr("disabled", true);
        $("#ddlCarryType_ddlCodeType").attr("disabled", true);
        $("#DeptOutDeptName").attr("disabled", true);

        $("#txtOutDate").attr("disabled", true);
        $("#ddlOutHour").attr("disabled", true);
        $("#ddlOutMin").attr("disabled", true);
        $("#UserOutUserName").attr("disabled", true);
        $("#txtCustAddr").attr("disabled", true);

        //安装信息不可用
        $("#chkNeedSetup").attr("disabled", true);
        fnChangeNeedSetup();


        $("#ColCust").css("display", "none");
        $("#ColCust2").css("display", "inline");
    }
}
//=================================== End     判断按钮状态 ================================

//是否需要安装
function fnChangeNeedSetup() {
    //    var isNeedSetup = $("#chkNeedSetup").attr("checked");
    var isNeedSetup = document.getElementById("chkNeedSetup").checked;
    if (false == isNeedSetup) {
        var BusiStatus = $("#hidBusiStatus").val();
        $("#NeedSetup").html("不需要安装");
        //不需要安装，将安装信息清空
        if (BusiStatus == '1') {
            $("#txtPlanSetDate").val("");
            $("#ddlPlanSetHour").val("00");
            $("#ddlPlanSetMin").val("00");
            $("#txtSetDate").val("");
            $("#ddlSetHour").val("00");
            $("#ddlSetMin").val("00");
            $("#UserSetUserID").val("");
        }
        else {
            $("#txtSetDate").val("");
            $("#ddlSetHour").val("00");
            $("#ddlSetMin").val("00");
            $("#UserSetUserID").val("");
        }
        //安装信息不可用
        $("#txtPlanSetDate").attr("disables", true);
        $("#txtPlanSetDate").attr("disabled", true);
        $("#ddlPlanSetHour").attr("disabled", true);
        $("#ddlPlanSetMin").attr("disabled", true);
        $("#txtSetDate").attr("disabled", true);
        $("#ddlSetHour").attr("disabled", true);

        $("#ddlSetMin").attr("disabled", true);
        $("#UserSetUserID").attr("disabled", true);

    }
    else if (true == isNeedSetup) {
        $("#NeedSetup").html("需要安装");

        //业务状态为下单，只有预约安装时间可用
        var BusiStatus = $("#hidBusiStatus").val();
        if (BusiStatus == '1') {
            $("#txtPlanSetDate").attr("disabled", false);
            $("#ddlPlanSetHour").attr("disabled", false);
            $("#ddlPlanSetMin").attr("disabled", false);
        }
        if (BusiStatus == "2") {
            $("#txtPlanSetDate").attr("disabled", false);
            $("#ddlPlanSetHour").attr("disabled", false);
            $("#ddlPlanSetMin").attr("disabled", false);
            $("#txtSetDate").attr("disabled", false);
            $("#ddlSetHour").attr("disabled", false);
            $("#ddlSetMin").attr("disabled", false);
            $("#UserSetUserID").attr("disabled", false);
        }
        if (BusiStatus == "3") {
            $("#txtPlanSetDate").attr("disabled", true);
            $("#ddlPlanSetHour").attr("disabled", true);
            $("#ddlPlanSetMin").attr("disabled", true);
            $("#txtSetDate").attr("disabled", false);
            $("#ddlSetHour").attr("disabled", false);
            $("#ddlSetMin").attr("disabled", false);
            $("#UserSetUserID").attr("disabled", false);
        }

    }
}

//附件处理
/*
* 附件处理
*/
function DealResume(flag) {
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "") {
        return;
    }
    //上传简历
    else if ("upload" == flag) {
        ShowUploadFile();
    }
    //清除简历
    else if ("clear" == flag) {
        //设置简历路径
        document.getElementById("hfPageAttachment").value = "";
        //下载删除不显示
        document.getElementById("divDealResume").style.display = "none";
        //上传显示 
        document.getElementById("divUploadResume").style.display = "block";
    }
    //下载简历
    else if ("download" == flag) {
        //获取简历路径
        resumeUrl = document.getElementById("hfPageAttachment").value;
        window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + resumeUrl, "_blank");
    }
}

function AfterUploadFile(url) {
    if (url != "") {
        // alert(url);
        //        //设置简历路径
        //        document.getElementById("hfPageResume").value = url;
        //下载删除显示
        document.getElementById("divDealResume").style.display = "block";
        //上传不显示
        document.getElementById("divUploadResume").style.display = "none";


        //设置简历路径
        document.getElementById("hfPageAttachment").value = url;
        var testurl = url.lastIndexOf('\\');
        testurl = url.substring(testurl + 1, url.lenght);
        document.getElementById('attachname').innerHTML = testurl;
    }
}


function fnStatus() {
    var BillStatus = $("#txtBillStatusID").val();
    var BusiStatus = $("#hidBusiStatus").val();
    var Status = $("#HiddenAction").val();
    switch (BillStatus) {
        case "1": //制单
            if (status == "Add") {
                //保存
                try {
                    document.getElementById('btnGetGoods').style.display = '';
                    document.getElementById('unbtnGetGoods').style.display = 'none';
                }
                catch (e) { }
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
                //确认
                $("#imgConfirm").css("display", "none");
                $("#imgUnConfirm").css("display", "inline");
                //取消确认
                $("#btn_Qxconfirm").css("display", "none");
                $("#btn_UnQxconfirm").css("display", "inline");
                //出库
                $("#imgConfirmOut").css("display", "none");
                //结算
                $("#imgConfirmSett").css("display", "none");

            }
            else {
                //保存
                try {
                    document.getElementById('btnGetGoods').style.display = '';
                    document.getElementById('unbtnGetGoods').style.display = 'none';
                }
                catch (e) { }
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
                //确认
                $("#imgConfirm").css("display", "inline");
                $("#imgUnConfirm").css("display", "none");
                //取消确认
                $("#btn_Qxconfirm").css("display", "none");
                $("#btn_UnQxconfirm").css("display", "inline");
                //出库
                $("#imgConfirmOut").css("display", "none");
                //结算
                $("#imgConfirmSett").css("display", "none");
            }
            break;
        case "2": //执行
            switch (BusiStatus) {
                case "1": //下单
                    break;
                case "2": //出库
                    try {
                        document.getElementById('btnGetGoods').style.display = 'none';
                        document.getElementById('unbtnGetGoods').style.display = '';
                    }
                    catch (e) { }
                    //保存
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "none");
                    //确认
                    $("#imgConfirm").css("display", "none");
                    $("#imgUnConfirm").css("display", "none");
                    //取消确认
                    $("#btn_Qxconfirm").css("display", "inline");
                    $("#btn_UnQxconfirm").css("display", "none");
                    //出库
                    $("#imgConfirmOut").css("display", "inline");
                    //结算
                    $("#imgConfirmSett").css("display", "none");
                    break;
                case "3": //结算
                    //保存
                    try {
                        document.getElementById('btnGetGoods').style.display = 'none';
                        document.getElementById('unbtnGetGoods').style.display = '';
                    }
                    catch (e) { }
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "none");
                    //确认
                    $("#imgConfirm").css("display", "none");
                    $("#imgUnConfirm").css("display", "none");
                    //取消确认
                    $("#btn_Qxconfirm").css("display", "none");
                    $("#btn_UnQxconfirm").css("display", "none");
                    //出库
                    $("#imgConfirmOut").css("display", "none");
                    //结算
                    $("#imgConfirmSett").css("display", "inline");
                    break;
                case "4": //完成
                    break;
            }
            break;
        case "4": //结单
            switch (BusiStatus) {
                case "4": //完成
                    //保存
                    try {
                        document.getElementById('btnGetGoods').style.display = 'none';
                        document.getElementById('unbtnGetGoods').style.display = '';
                    }
                    catch (e) { }
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "none");
                    //确认
                    $("#imgConfirm").css("display", "none");
                    $("#imgUnConfirm").css("display", "inline");
                    //取消确认
                    $("#btn_Qxconfirm").css("display", "none");
                    $("#btn_UnQxconfirm").css("display", "none");
                    //出库
                    $("#imgConfirmOut").css("display", "none");
                    //结算
                    $("#imgConfirmSett").css("display", "none");
            }
            break;
        case "5": //完成
            //保存
            try {
                document.getElementById('btnGetGoods').style.display = 'none';
                document.getElementById('unbtnGetGoods').style.display = '';
            }
            catch (e) { }
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "none");
            //确认
            $("#imgConfirm").css("display", "none");
            $("#imgUnConfirm").css("display", "inline");
            //取消确认
            $("#btn_Qxconfirm").css("display", "none");
            $("#btn_UnQxconfirm").css("display", "none");
            //出库
            $("#imgConfirmOut").css("display", "none");
            //结算
            $("#imgConfirmSett").css("display", "none");
            document.getElementById("divTitle").innerHTML = "销售订单--完成";
            break;
    }
}

//改变开票信息
function fnChangeOpenBill() {
    var OpenBill = document.getElementById("chkisOpenbill").checked;
    if (OpenBill == true) {
        $("#OpenBill").html("已开票");
    }
    else {
        $("#OpenBill").html("未开票");
    }
}


function fnStatusAndTitle() {
    var BusiStatus = $("#hidBusiStatus").val();
    var IsSource = false;
    if (document.getElementById("imgBack").style.display != "none") {//从列表页面过来的，如果此时业务状态为1即下单时，标题为“新建销售订单”，否则为销售订单
        IsSource = true;
    }
    switch (BusiStatus) {
        case "1":
            //标题控制
            if (IsSource == true) {
                document.getElementById("divTitle").innerHTML = "销售订单";
            }
            else {
                document.getElementById("divTitle").innerHTML = "新建销售订单";
            }
            //按钮控制
            //保存
            try {
                document.getElementById('btnGetGoods').style.display = '';
                document.getElementById('unbtnGetGoods').style.display = 'none';
            }
            catch (e) { }
            $("#imgSave").css("display", "inline");
            $("#imgUnSave").css("display", "none");
            //确认
            var ThisID = $("#ThisID").val();
            if (ThisID > 0) {//已经保存过了，确认按钮可用
                $("#imgConfirm").css("display", "inline");
                $("#imgUnConfirm").css("display", "none");
            }
            else {
                $("#imgConfirm").css("display", "none");
                $("#imgUnConfirm").css("display", "inline");
            }
            //取消确认
            $("#btn_Qxconfirm").css("display", "none");
            $("#btn_UnQxconfirm").css("display", "inline");
            //出库
            $("#imgConfirmOut").css("display", "none");
            //结算
            $("#imgConfirmSett").css("display", "none");

            $("#imgAdd").css("display", "inline");
            $("#imgDel").css("display", "inline");
            $("#imgUnAdd").css("display", "none");
            $("#imgUnDel").css("display", "none");
            //所有text可用
            //            $(":text").each(function(){ 
            //                this.disabled=false; 
            //            });
            $("#DeptOutDeptName").attr("disabled", true);
            $("#UserOutUserName").attr("disabled", true);
            $("#txtOutDate").attr("disabled", true);
            $("#txtCustAddr").attr("disabled", true);
            //所有checkbox可用
            $(":checkbox").each(function() {
                this.disabled = false;
            });

            //下拉列表可用
            $("#ddlSendMode").attr("disabled", false);
            $("#ddlCurrencyType").attr("disabled", false);
            $("#ddlMoneyType_ddlCodeType").attr("disabled", false);
            $("#ddlTakeType_ddlCodeType").attr("disabled", false);
            $("#ddlPayType_ddlCodeType").attr("disabled", false);
            $("#ddlOrderMethod_ddlCodeType").attr("disabled", false);
            $("#ddlMoneyType_ddlCodeType").attr("disabled", false);
            $("#ddlPlanSetHour").attr("disabled", false);
            $("#ddlPlanSetMin").attr("disabled", false);
            $("#ddlSetHour").attr("disabled", false);
            $("#ddlSetMin").attr("disabled", false);
            break;
        case "2":
            document.getElementById("divTitle").innerHTML = "销售订单--出库";
            //按钮控制
            fnStatusOut();
            //保存
            try {
                document.getElementById('btnGetGoods').style.display = 'none';
                document.getElementById('unbtnGetGoods').style.display = '';
            }
            catch (e) { }
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "none");
            //确认
            $("#imgConfirm").css("display", "none");
            $("#imgUnConfirm").css("display", "none");
            //取消确认
            $("#btn_Qxconfirm").css("display", "inline");
            $("#btn_UnQxconfirm").css("display", "none");
            //出库
            $("#imgConfirmOut").css("display", "inline");
            //结算
            $("#imgConfirmSett").css("display", "none");

            $("#imgAdd").css("display", "none");
            $("#imgDel").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgUnDel").css("display", "inline");
            break;
        case "3":
            document.getElementById("divTitle").innerHTML = "销售订单--结算";
            //按钮控制
            //保存
            try {
                document.getElementById('btnGetGoods').style.display = 'none';
                document.getElementById('unbtnGetGoods').style.display = '';
            }
            catch (e) { }
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "none");
            //确认
            $("#imgConfirm").css("display", "none");
            $("#imgUnConfirm").css("display", "none");
            //取消确认
            $("#btn_Qxconfirm").css("display", "none");
            $("#btn_UnQxconfirm").css("display", "none");
            //出库
            $("#imgConfirmOut").css("display", "none");
            //结算
            $("#imgConfirmSett").css("display", "inline");

            $("$txtDiscount").attr("disables", true);
            $("#imgAdd").css("display", "none");
            $("#imgDel").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgUnDel").css("display", "inline");
            break;
        case "4":
            document.getElementById("divTitle").innerHTML = "销售订单--完成";
            //按钮控制
            //保存
            try {
                document.getElementById('btnGetGoods').style.display = 'none';
                document.getElementById('unbtnGetGoods').style.display = '';
            }
            catch (e) { }
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "none");
            //确认
            $("#imgConfirm").css("display", "none");
            $("#imgUnConfirm").css("display", "inline");
            //取消确认
            $("#btn_Qxconfirm").css("display", "none");
            $("#btn_UnQxconfirm").css("display", "none");
            //出库
            $("#imgConfirmOut").css("display", "none");
            //结算
            $("#imgConfirmSett").css("display", "none");
            $("$txtDiscount").attr("disables", true);
            $("#imgAdd").css("display", "none");
            $("#imgDel").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgUnDel").css("display", "inline");
            break;
    }

}
///打印单据
function PrintSubSellBill() {
    var BillID = $("#ThisID").val();
    if (BillID == "" || parseInt(BillID) < 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先保存！");
        return;
    }
    window.open("SubSellOrderPrint.aspx?BillID=" + BillID);
}

// 门店 条码扫描需要
function SubGetGoodsDataByBarCode(ProductID, ProductNo, ProductName, UnitID, UnitName, Specification, SubPriceTax, SubPrice, SubTax, Discount, IsBatchNo) {
    popProductInfoSubUCFillSearch(ProductID, ProductNo, ProductName, Specification, UnitID, UnitName, SubPriceTax, SubPrice, SubTax, Discount, IsBatchNo);
}


function GetGoodsDataByBarCode(ProductID, ProductNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount,
                    Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, IsBatchNo, BatchNo
                    , ProductCount, CurrentStore, Source, ColorName) {
    popProductInfoSubUCFillSearch(ProductID, ProductNo, ProductName, Specification, UnitID, UnitName, SellTax, StandardSell, TaxRate / 100, Discount, IsBatchNo);
}


// 条码扫描填充值
function popProductInfoSubUCFillSearch(ProductID, ProductNo, ProductName, Specification, UnitID, UnitName, SubPriceTax, SubPrice, SubTax, Discount, IsBatchNo) {
    var signFrame = findObj("DetailTable", document);
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var rowID = parseInt(txtTRLastIndex) + 1;

    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    else {
        for (var i = 1; i < signFrame.rows.length; i++) {
            if (signFrame.rows[i].style.display != "none") {
                var checkPro = document.getElementById('ProductID' + (i));
                if (checkPro.value == ProductID) {
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        if ($("#UsedUnitCount" + (i)).val() == "") {
                            $("#UsedUnitCount" + (i)).val(FormatAfterDotNumber(1, $("#hidSelPoint").val()));
                        }
                        $("#UsedUnitCount" + (i)).val(FormatAfterDotNumber(parseFloat($("#UsedUnitCount" + (i)).val()) + 1, $("#hidSelPoint").val()));
                    }
                    else {
                        if ($("#ProductCount" + (i)).val() == "") {
                            $("#ProductCount" + (i)).val(FormatAfterDotNumber(1, $("#hidSelPoint").val()));
                        }
                        $("#ProductCount" + (i)).val(FormatAfterDotNumber(parseFloat($("#ProductCount" + (i)).val()) + 1, $("#hidSelPoint").val()));
                    }
                    ChangeUnit(this, (i));
                    return;
                }
            }
        }
    }

    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    newTR.className = "newrow";
    var i = 0;
    var newNameXH = newTR.insertCell(i++); //添加列:选择    
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input name='Dtlchk' id='Dtlchk" + rowID + "' value=" + rowID + " type='checkbox' size='20' class='tdinput' style='width:90%' />";


    var newNameTD = newTR.insertCell(i++); //添加列:序号
    newNameTD.className = "cell";
    newNameTD.id = "SortNo" + rowID;

    var newProductID = newTR.insertCell(i++); //添加列:物品ID
    newProductID.style.display = "none";
    newProductID.innerHTML = "<input id='ProductID" + rowID + "' value=\"" + ProductID + "\" type='text' class='tdinput' style='width:90%' />";

    var newProductNo = newTR.insertCell(i++); //添加列:物品编号
    newProductNo.className = "cell";
    newProductNo.innerHTML = "<input id='ProductNo" + rowID + "' value=\"" + ProductNo + "\" type='text' class='tdinput' style='width:90%' onclick=\"GetProduct(" + rowID + ")\" />";

    var newProductName = newTR.insertCell(i++); //添加列:物品名称
    newProductName.className = "cell";
    newProductName.innerHTML = "<input id='ProductName" + rowID + "' type='text' value=\"" + ProductName + "\"  class='tdinput' style='width:90%' />";
    $("#ProductName" + rowID).attr("disabled", true);

    var newselBatch = newTR.insertCell(i++); //添加列:批次
    newselBatch.className = "cell";
    newselBatch.innerHTML = "<select id='selBatch" + rowID + "' class='tdinput' style='width:90%' >";

    var newSpecification = newTR.insertCell(i++); //添加列:规格
    newSpecification.className = "cell";
    newSpecification.innerHTML = "<input id='Specification" + rowID + "' type='text' value=\"" + Specification + "\"  class='tdinput' style='width:90%'/>";
    $("#Specification" + rowID).attr("disabled", true);

    var newStorage = newTR.insertCell(i++); //添加列:仓库ID
    newStorage.className = "cell";
    newStorage.innerHTML = "<select class='tdinput' style='width:90%' id='StorageID" + rowID + "'>" + document.getElementById("StorageHid").innerHTML + "</select>";
    if ($("#ddlSendMode").val() == "1") {   //分店发货仓库不可选
        $("#StorageID" + rowID).attr("disabled", true);
    }

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        var newUnitName = newTR.insertCell(i++); //添加列:单位
        newUnitName.className = "cell";
        newUnitName.innerHTML = "<input  id='UnitName" + rowID + "'type='text' value='" + UnitName + "'  class='tdinput' style='width:90%'   />";
        $("#UnitName" + rowID).attr("disabled", true);

        var newProductCount = newTR.insertCell(i++); //添加列:基本数量
        newProductCount.className = "cell";
        newProductCount.innerHTML = "<input id='ProductCount" + rowID + "' type='text' disabled='disabled' class='tdinput' style='width:90%'/>";

        var newProductCount = newTR.insertCell(i++); //添加列:采购数量
        newProductCount.className = "cell";
        newProductCount.innerHTML = "<input id='UsedUnitCount" + rowID + "' value='" + FormatAfterDotNumber(1, parseInt($("#hidSelPoint").val())) + "' onblur=\"Number_round(this," + $("#hidSelPoint").val() + ");ChangeUnit(this,'" + rowID + "');\"  type='text'  class='tdinput' style='width:90%'/>";
    }
    else {
        var newProductCount = newTR.insertCell(i++); //添加列:采购数量
        newProductCount.className = "cell";
        newProductCount.innerHTML = "<input id='ProductCount" + rowID + "' value='" + FormatAfterDotNumber(1, parseInt($("#hidSelPoint").val())) + "' type='text' onblur=\"Number_round(this," + $("#hidSelPoint").val() + ");fnTotal(1);\" class='tdinput' style='width:90%'/>";
    }

    var newProductCountTH = newTR.insertCell(i++); //添加列:采购数量
    newProductCountTH.className = "cell";
    newProductCountTH.innerHTML = "<input id='ProductCountTH" + rowID + "'  type='text' disabled=\"disabled\" class='tdinput' style='width:90%'/>";

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        var newUnitName = newTR.insertCell(i++); //添加列:单位
        newUnitName.className = "cell";
        newUnitName.id = "td_UsedUnitID_" + rowID;
        newUnitName.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;";
    }
    else {
        var newUnitName = newTR.insertCell(i++); //添加列:单位
        newUnitName.className = "cell";
        newUnitName.innerHTML = "<input  id='UnitName" + rowID + "'type='text' value='" + UnitName + "'  class='tdinput' style='width:90%'   />";
        $("#UnitName" + rowID).attr("disabled", true);
    }


    var newUnitID = newTR.insertCell(i++); //添加列:单位ID
    newUnitID.style.display = "none";
    newUnitID.innerHTML = "<input id='UnitID" + rowID + "' type='hidden' class='tdinput' style='width:90%' value='" + UnitID + "'/>";

    if ($("#txtIsMoreUnit").val() == "1") {
        var newUnitPrice = newTR.insertCell(i++); //添加列:单价
        newUnitPrice.className = "cell";
        newUnitPrice.innerHTML = "<input id='UnitPrice" + rowID + "'type='hidden' value=\"" + FormatAfterDotNumber(SubPrice, parseInt($("#hidSelPoint").val())) + "\"  class='tdinput' style='width:90%' /><input id='UsedPrice" + rowID + "'type='text'  value=\"" + FormatAfterDotNumber(SubPrice, parseInt($("#hidSelPoint").val())) + "\"  class='tdinput' style='width:90%' onchange=\"Number_round(this," + $("#hidSelPoint").val() + ");\" onblur=\"ChangeUnit(this,'" + rowID + "');\" />";
    }
    else {
        var newUnitPrice = newTR.insertCell(i++); //添加列:单价
        newUnitPrice.className = "cell";
        newUnitPrice.innerHTML = "<input id='UnitPrice" + rowID + "' type='text'  class='tdinput' style='width:90%' value=\"" + FormatAfterDotNumber(SubPrice, parseInt($("#hidSelPoint").val())) + "\" onchange=\"Number_round(this," + $("#hidSelPoint").val() + ");\" onblur=\"ChangeUnit(this,'" + rowID + "');\"/>";
    }

    var newTaxPric = newTR.insertCell(i++); //添加列:含税价
    newTaxPric.className = "cell";
    newTaxPric.innerHTML = "<input id='TaxPrice" + rowID + "' value=\"" + FormatAfterDotNumber(SubPriceTax, parseInt($("#hidSelPoint").val())) + "\" type='text' disabled='disabled'   class='tdinput' style='width:90%'   />";


    var newTaxPricHide = newTR.insertCell(i++); //添加列:含税价隐藏
    newTaxPricHide.style.display = "none";
    newTaxPricHide.innerHTML = "<input id='TaxPriceHide" + rowID + "'type='text'  value=\"" + FormatAfterDotNumber(SubPriceTax, parseInt($("#hidSelPoint").val())) + "\" class='tdinput' style='width:90%'   />";

    var newDiscount = newTR.insertCell(i++); //添加列:折扣
    newDiscount.className = "cell";
    newDiscount.innerHTML = "<input id='Discount" + rowID + "'type='text' disabled='disabled' value=\"" + Discount + "\"  class='tdinput' style='width:90%'   />";

    var newTaxRate = newTR.insertCell(i++); //添加列:税率
    newTaxRate.className = "cell";
    newTaxRate.innerHTML = "<input id='TaxRate" + rowID + "'type='text' value=\"" + FormatAfterDotNumber(SubTax, parseInt($("#hidSelPoint").val())) + "\" disabled='disabled'  class='tdinput' style='width:90%'   />";

    var newTaxRateHide = newTR.insertCell(i++); //添加列:税率隐藏
    newTaxRateHide.style.display = "none";
    newTaxRateHide.innerHTML = "<input id='TaxRateHide" + rowID + "'type='text' value=\"" + FormatAfterDotNumber(SubTax, parseInt($("#hidSelPoint").val())) + "\" class='tdinput' style='width:90%'   />";

    var newTotalPrice = newTR.insertCell(i++); //添加列:金额
    newTotalPrice.className = "cell";
    newTotalPrice.innerHTML = "<input id='TotalPrice" + rowID + "'type='text'  class='tdinput' style='width:90%'  readonly />";
    $("#TotalPrice" + rowID).attr("disabled", true);

    var newTotalFee = newTR.insertCell(i++); //添加列:含税金额
    newTotalFee.className = "cell";
    newTotalFee.innerHTML = "<input id='TotalFee" + rowID + "'type='text'  class='tdinput' style='width:90%' readonly  />";
    $("#TotalFee" + rowID).attr("disabled", true);

    var newTotalTax = newTR.insertCell(i++); //添加列:税额
    newTotalTax.className = "cell";
    newTotalTax.innerHTML = "<input id='TotalTax" + rowID + "'type='text'  class='tdinput' style='width:90%'  readonly />";
    $("#TotalTax" + rowID).attr("disabled", true);

    var newRemark = newTR.insertCell(i++); //添加列:备注
    newRemark.style.display = "none";
    newRemark.innerHTML = "<input id='Remark" + rowID + "'type='text'  class='tdinput' style='width:90%' />";

    GetUnitGroupSelectEx(ProductID, 'SaleUnit', 'txtUsedUnit' + rowID, 'ChangeUnit(this,' + rowID + ',1)', "td_UsedUnitID_" + rowID, '', 'ChangeUnit(this,' + rowID + ',1)');

    if ($("#ddlSendMode").val() == "1") {//分店发货
        SetSubBatchSelect(rowID, ProductID, IsBatchNo, "");
    }
    else {
        SetBatchSelect(rowID, ProductID, IsBatchNo, "");
    }

    document.getElementById('txtTRLastIndex').value = rowID; //将行号推进下一行
    fnGenerateNo('DetailTable');

}

function GetGoodsInfo() {
    if ($("#ddlSendMode").val() == "1") {
        SubGetGoodsInfoByBarCode();
    }
    else {
        GetGoodsInfoByBarCode();
    }
}

            