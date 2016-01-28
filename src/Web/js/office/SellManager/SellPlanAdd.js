var treeview1;


$(document).ready(function() {

fnGetExtAttr();//物品扩展属性

GetExtAttr("officedba.SellPlan",null);//销售计划扩展属性

    $("#PlanDateTime").attr("readonly", "readonly");
    $("#SellPlanUC_ddlCodeRule").css("width", "80px");
    $("#SellPlanUC_txtCode").css("width", "80px");

    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['islist']; //是否从列表页面进入
        var intFromType = requestObj['intFromType']; //是否从列表页面进入 
        var orderID = requestObj['id']; //销售机会Id
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031001', 'ModuleID=2031002');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (flag != null || intFromType != null) {
            $("#ibtnBack").css("display", "inline");
        }
        if (orderID != null) {
            $("#hiddOrderID").val(orderID);
            fnGetOrder(); //获取报价单详细信息
            $("#lblTitle").html('销售计划');
        }
    }
    GetFlowButton_DisplayControl();
});

//获取审批状态，判断操作按钮
function fnGetBillStatus(BillTypeFlag, BillTypeCode, BillID) {
    var Action = "Get";
    var UrlParam = "Action=Get&BillTypeFlag=" + BillTypeFlag + "&BillTypeCode=" + BillTypeCode + "&BillID=" + BillID + "";
    $.ajax({
        type: "POST",
        url: "../../../Handler/Common/Flow.ashx?" + UrlParam,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取单据审批状态出错！");
        },
        success: function(data) {
            try {
                var values = data.sta;
                if (values != 0 && values != 4 && values != 5) {
                    $("#btn_save").css("display", "none");
                    $("#btn_save0").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#type2").attr("disabled", "disabled");
                    $("#type3").attr("disabled", "disabled");
                    $("#type4").attr("disabled", "disabled");
                    $("#type5").attr("disabled", "disabled");
                    $("#type6").attr("disabled", "disabled");

                    $("#btnType2").css("display", "none");
                    $("#btnType3").css("display", "none");
                    $("#btnType4").css("display", "none");
                    $("#btnType5").css("display", "none");
                    $("#btnType6").css("display", "none");

                    $("#ubtnType2").css("display", "inline");
                    $("#ubtnType3").css("display", "inline");
                    $("#ubtnType4").css("display", "inline");
                    $("#ubtnType5").css("display", "inline");
                    $("#ubtnType6").css("display", "inline");

                }
                else {
                    $("#btn_save").css("display", "inline");
                    $("#btn_save0").css("display", "inline");
                    $("#imgUnSave").css("display", "none");
                    $("#type2").removeAttr("disabled");
                    $("#type3").removeAttr("disabled");
                    $("#type4").removeAttr("disabled");
                    $("#type5").removeAttr("disabled");
                    $("#type6").attr("disabled", "disabled");

                    $("#btnType2").css("display", "inline");
                    $("#btnType3").css("display", "inline");
                    $("#btnType4").css("display", "inline");
                    $("#btnType5").css("display", "inline");
                    $("#btnType6").css("display", "none");

                    $("#ubtnType2").css("display", "none");
                    $("#ubtnType3").css("display", "none");
                    $("#ubtnType4").css("display", "none");
                    $("#ubtnType5").css("display", "none");
                    $("#ubtnType6").css("display", "inline");
                }

            } catch (e) { }
        }
    });
}


//返回按钮
function fnBack() {
    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['islist']; //是否从列表页面进入
        if (flag != null && typeof (flag) != "undefined") {
            var URLParams = document.getElementById("HiddenURLParams").value;
            window.location.href = 'SellPlanList.aspx' + URLParams;
        }
        else {
            var intFromType = requestObj['intFromType']; //是否从列表页面进入           
            var ListModuleID = requestObj['ListModuleID']; //来源页MODULEID       
            if (intFromType == 2) {
                window.location.href = '../../../DeskTop.aspx';
            }
            if (intFromType == 3) {
                window.location.href = '../../Personal/WorkFlow/FlowMyApply.aspx?ModuleID=' + ListModuleID;
            }
            if (intFromType == 4) {
                window.location.href = '../../Personal/WorkFlow/FlowMyProcess.aspx?ModuleID=' + ListModuleID;
            }
            if (intFromType == 5) {
                window.location.href = '../../Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID=' + ListModuleID;
            }
        }
    }

}

//获取查询参数
function GetRequestToHidden() {
    var url = location.search;
    return url;
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

//选择物品
function Fun_FillParent_Content(ID, ProdNo, ProductName, StandardCost, UnitID, CodeName, TaxRate, SellTax, Discount, Specification) {

    $("#ProductID").val(ProductName); //商品ID
    $("#hiddProID").val(ID); //商品名
}
//验证借点层次数目，不允许添加10级以上
function fnCheckLeve() {
    var arr = treeview1.getValue().val.split('|');
    if (arr[7] > 8) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "不可以添加超过十级的子节点！");
        return false;
    }
    else {
        return true;
    }
}

//保存数据
function InsertSellOfferData() {
    if (!CheckInput()) {
        return;
    }
    if (document.getElementById("type3").checked) {
        if (!fnCheckLeve()) {
            return;
        }
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    if (document.getElementById("type5").checked) {
        if (!confirm('删除本条数据将连同其下级数据一起删除，您确认删除？')) {
            return;
        }
    }
    var strInfo = fnGetInfo();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellPlanAdd.ashx",
        data: strInfo,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {
                $("#hiddOrderID").val(data.id);
                fnGetOrder();
            }
            else {
                hidePopup();
            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//获取主表信息
function fnGetInfo() {
    var strInfo = '';
    var CodeType = $("#SellPlanUC_ddlCodeRule").val(); //计划编号产生的规则
    var PlanNo = $("#SellPlanUC_txtCode").val();  //计划编号
    var OrderID = $("#hiddOrderID").val();         //计划id
    var Title = $.trim($("#Title").val());         //计划主题
    var PlanType = $("#PlanType").val();      //计划类型（1.月计划2.季计划3.年计划
    var StartDate = $("#StartDate").val();         //开始日期
    var EndDate = $("#EndDate").val();         //截止日期
    var Hortation = $("#Hortation").val();      //奖励方案
    var CanViewUser = $.trim($("#CanViewUserNameHidden").val());         //可查看该销售机会的员工（ID，多个）
    var CanViewUserName = $("#UserCanViewUser").val();      //可查看该销售机会的员工姓名

    if (PlanType == 1) {//月计划
        var PlanYear = $("#ddlYear").val(); //计划年份
        var PlanTime = $("#ddlMonth").val();
    }
    if (PlanType == 2) {//季计划
        var PlanYear = $("#ddlYear").val(); //计划年份
        var PlanTime = $("#ddlSeason").val();
    }
    if (PlanType == 3) {//年计划
        var PlanYear = $("#ddlYear").val(); //计划年份
        var PlanTime = '';
    }
    if (PlanType == 4) {//日计划
        var PlanYear = $("#PlanDateTime").val(); //计划年份
        var PlanTime = '';
    }
    if (PlanType == 5) {//周计划
        var PlanYear = $("#ddlYear").val(); //计划年份
        var PlanTime = $("#ddlWeek").val();

    }
    if (PlanType == 6) {//半年计划
        var PlanYear = $("#ddlYear").val(); //计划年份
        var PlanTime = $("#Half").val();
    }
    if (PlanType == 7) {//其他计划
        var PlanYear = $("#ddlYear").val(); //计划年份
        var PlanTime = '';
    }
    var PlanTotal = $.trim($("#PlanTotal").val());    //计划总金额
    var MinPlanTotal = $.trim($("#MinPlanTotal").val());           //最低计划额
    var Remark = $.trim($("#Remark").val());      //备注
    if (document.getElementById("type1").checked) {
        var detailAction = '1';      //明细操作类型,无操作
        var ID = ''; //明细ID
        var ParentID = ''; //上级明细的id
    }
    if (document.getElementById("type2").checked) {
        var detailAction = '2';      //明细操作类型，添加同级
        var DetailCount = $("#hiddDetailCount").val(); //明细数据条数
        if (DetailCount != '0' && DetailCount != '') {
            var ID = ''; //明细ID
            var arr = treeview1.getValue().val.split('|');
            var ParentID = arr[1]; //上级明细的id，从树中取值
        }
        else {
            var ID = '';
            var ParentID = '0'; //
        }
    }
    if (document.getElementById("type3").checked) {
        var detailAction = '2';      //明细操作类型，添加下级
        var ID = ''; //明细ID
        var arr = treeview1.getValue().val.split('|');
        var ParentID = arr[0]; //上级明细的id，从树中获取
    }
    if (document.getElementById("type4").checked) {
        var detailAction = '3';      //明细操作类型，修改
        var arr = treeview1.getValue().val.split('|');
        var ID = arr[0]; //明细ID，从树中获取
        var ParentID = ''; //上级明细的id
    }
    if (document.getElementById("type5").checked) {
        var detailAction = '4';      //明细操作类型，删除
        var arr = treeview1.getValue().val.split('|');
        var ID = arr[0]; //明细ID，从树中获取
        var ParentID = ''; //上级明细的id
    }
    if (OrderID == '0' || OrderID == '') {
        var action = 'insert';
    }
    else {
        var action = 'update';
    }

    var DetailType = $("#ddlDetailType").val(); //明细种类（1.部门明细2.员工明细3.物品明细）
    if (DetailType == 1) {//部门明细
        var DetailID = $("#hiddDeptID").val();
    }
    else if (DetailType == 2) {//员工明细
        var DetailID = $("#hiddSeller").val();
    }
    else if (DetailType == 3) {//物品明细
        var DetailID = $("#hiddProID").val();
    }
    else {
        var DetailID = '';
    }
    var DetailTotal = $.trim($("#DetailTotal").val()); //明细计划总金额
    var MinDetailotal = $.trim($("#MinDetailotal").val()); //mingxi 最低计划额

    strInfo = 'CodeType=' + escape(CodeType) + '&PlanNo=' + escape(PlanNo) + '&OrderID=' + escape(OrderID) +
            '&Title=' + escape(Title) + '&PlanType=' + escape(PlanType) + '&PlanYear=' + escape(PlanYear) +
            '&PlanTime=' + escape(PlanTime) + '&PlanTotal=' + escape(PlanTotal) + '&MinPlanTotal=' + escape(MinPlanTotal) +
            '&Remark=' + escape(Remark) + '&ID=' + escape(ID) + '&ParentID=' + escape(ParentID) +
            '&action=' + escape(action) + '&detailAction=' + escape(detailAction) + '&DetailType=' + escape(DetailType) +
            '&DetailID=' + escape(DetailID) + '&DetailTotal=' + escape(DetailTotal) + '&MinDetailotal=' + escape(MinDetailotal) +
            '&StartDate=' + escape(StartDate) + '&EndDate=' + escape(EndDate) + '&Hortation=' + escape(Hortation) +
'&CanViewUser=' + escape(CanViewUser) + '&CanViewUserName=' + escape(CanViewUserName)+GetExtAttrValue();
    return strInfo;

}
//确认按钮的调用的函数名称，业务逻辑不同模块各自处理
function Fun_ConfirmOperate() {
    if (confirm("是否确认单据？")) {
        var CodeType = $("#SellPlanUC_ddlCodeRule").val(); //报价单编号产生的规则
        var PlanNo = $("#SellPlanUC_txtCode").val();        //退货单编号
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/SellPlanAdd.ashx",
            data: 'CodeType=' + escape(CodeType) + '&PlanNo=' + escape(PlanNo) + '&action=confirm',
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            //complete :function(){hidePopup();},
            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
            success: function(data) {
                if (data.sta == 1) {
                    $("#hiddOrderID").val(data.id);
                    //                    $("#Confirmor").val($("#Creator").val());
                    //                    $("#ConfirmDate").val($("#CreateDate").val());


                    //                    $("#BillStatus").html("执行");
                    //                    $("#hiddBillStatus").val('2');
                    //                    $("#imgUnSave").css("display", "inline");
                    //                    $("#btn_save").css("display", "none");
                    fnGetOrder();
                    //GetFlowButton_DisplayControl();
                }
                else {
                    hidePopup();
                }
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
            }
        });
    }
}

//结单按钮调用的函数名称，业务逻辑不同模块各自处理
//isFlag=true结单操作，isFlag=false取消结单
function Fun_CompleteOperate(isFlag) {
    var CodeType = $("#SellPlanUC_ddlCodeRule").val(); //报价单编号产生的规则
    var PlanNo = $("#SellPlanUC_txtCode").val();        //退货单编号
    var acction = '';
    if (isFlag) {
        acction = 'close';
        if (!confirm("是否结单？")) {
            return;
        }
    }
    else {
        if (!confirm("是否取消结单？")) {
            return;
        }
        acction = 'unClose';
    }
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellPlanAdd.ashx",
        data: 'CodeType=' + escape(CodeType) + '&PlanNo=' + escape(PlanNo) + '&action=' + acction,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {
                //                if (isFlag) {
                //                    $("#BillStatus").html("手工结单");
                //                    $("#Closer").val($("#Creator").val());
                //                    $("#CloseDate").val($("#CreateDate").val());
                //                    $("#imgUnSave").css("display", "inline");
                //                    $("#btn_save").css("display", "none");
                //                    $("#hiddBillStatus").val('4');
                //                }
                //                else {
                //                    $("#BillStatus").html("执行");
                //                    $("#Closer").val('');
                //                    $("#CloseDate").val('');
                //                    $("#imgUnSave").css("display", "inline");
                //                    $("#btn_save").css("display", "none");
                //                    $("#hiddBillStatus").val('2');
                //                }

                $("#hiddOrderID").val(data.id);
                fnGetOrder();
                //GetFlowButton_DisplayControl();
            }
            else {
                hidePopup();
            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//取消确认
function Fun_UnConfirmOperate() {
    if (confirm("是否取消确认？")) {
        var CodeType = $("#SellPlanUC_ddlCodeRule").val(); //报价单编号产生的规则
        var PlanNo = $("#SellPlanUC_txtCode").val();        //退货单编号
        var acction = 'UnConfirm';

        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/SellPlanAdd.ashx",
            data: 'CodeType=' + escape(CodeType) + '&PlanNo=' + escape(PlanNo) + '&action=' + acction,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            //complete :function(){hidePopup();},
            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
            success: function(data) {
                if (data.sta == 1) {
                    //                    $("#BillStatus").html("制单");
                    //                    $("#Closer").val('');
                    //                    $("#CloseDate").val('');
                    //                    $("#imgUnSave").css("display", "none");
                    //                    $("#Confirmor").val('');
                    //                    $("#ConfirmDate").val('');
                    //                    $("#hiddBillStatus").val('1');
                    //                    $("#btn_save").css("display", "inline");
                    $("#hiddOrderID").val(data.id);
                    fnGetOrder();
                    // GetFlowButton_DisplayControl();
                }
                else {
                    hidePopup();
                }
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
            }
        });
    }
}

//获取主表信息
function fnGetOrder() {
    var orderID = $("#hiddOrderID").val();         //计划id
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellPlanList.ashx",
        data: 'orderID=' + escape(orderID) + '&action=orderInfo',
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            $("#Title").val(data.data[0].Title)
            $("#hiddBillStatus").val(data.data[0].BillStatus);
            $("#PlanType").val(data.data[0].PlanType);      //计划类型（1.月计划2.季计划3.年计划
            fnType();
            ExtAttControl_FillValue(data.data[0]);//扩展属性
            var PlanType = data.data[0].PlanType;
            if (PlanType == 1) {//月计划
                var PlanYear = $("#ddlYear").val(data.data[0].PlanYear); //计划年份
                var PlanTime = $("#ddlMonth").val(data.data[0].PlanTime);
            }
            if (PlanType == 2) {//季计划
                var PlanYear = $("#ddlYear").val(data.data[0].PlanYear); //计划年份
                var PlanTime = $("#ddlSeason").val(data.data[0].PlanTime);
            }
            if (PlanType == 3) {//年计划
                var PlanYear = $("#ddlYear").val(data.data[0].PlanYear); //计划年份

            }
            if (PlanType == 4) {//日计划
                var PlanYear = $("#PlanDateTime").val(data.data[0].PlanYear); //计划年份

            }
            if (PlanType == 5) {//周计划
                var PlanYear = $("#ddlYear").val(data.data[0].PlanYear); //计划年份
                var PlanTime = $("#ddlWeek").val(data.data[0].PlanTime);

            }
            if (PlanType == 6) {//半年计划
                var PlanYear = $("#ddlYear").val(data.data[0].PlanYear); //计划年份
                var PlanTime = $("#Half").val(data.data[0].PlanTime);
            }
            if (PlanType == 7) {//其他计划
                var PlanYear = $("#ddlYear").val(data.data[0].PlanYear); //计划年份

            }
            $("#PlanTotal").val(FormatAfterDotNumber(data.data[0].PlanTotal,precisionLength));    //计划总金额
            $("#MinPlanTotal").val(FormatAfterDotNumber(data.data[0].MinPlanTotal,precisionLength));           //最低计划额
            $("#Remark").val(data.data[0].Remark);      //备注
            $("#StartDate").val(data.data[0].StartDate);         //开始日期
            $("#EndDate").val(data.data[0].EndDate);         //截止日期
            $("#Hortation").val(data.data[0].Hortation);      //奖励方案
            $("#UserCanViewUser").val(data.data[0].CanViewUserName);         //可查看该销售机会的员工（ID，多个）
            $("#CanViewUserNameHidden").val(data.data[0].CanViewUser);      //可查看该销售机会的员工姓名
            $("#BillStatus").html(data.data[0].BillStatusText);      //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
            $("#SellPlanUC_txtCode").val(data.data[0].PlanNo);
            $("#SellPlanUC_txtCode").attr("disabled", "disabled");
            $("#SellPlanUC_txtCode").css("width", "95%");
            $("#SellPlanUC_ddlCodeRule").css("display", "none");
            $("#Creator").val(data.data[0].CreatorName);        //制单人ID
            $("#CreateDate").val(data.data[0].CreateDate);     //制单日期
            $("#Confirmor").val(data.data[0].ConfirmorName);      //确认人ID
            $("#ConfirmDate").val(data.data[0].ConfirmDate);    //确认日期
            $("#Closer").val(data.data[0].CloserName);         //结单人ID
            $("#CloseDate").val(data.data[0].CloseDate);      //结单日期
            $("#ModifiedDate").val(data.data[0].ModifiedDate);   //最后更新日期
            $("#ModifiedUserID").val(data.data[0].ModifiedUserID); //最后更新用户ID(对应操作用户UserID)
            try {
                if ($("#hiddBillStatus").val() == 1) {
                    $("#btn_save").css("display", "inline");
                    $("#btn_save0").css("display", "inline");
                    $("#imgUnSave").css("display", "none");
                    $("#type2").removeAttr("disabled");
                    $("#type3").removeAttr("disabled");
                    $("#type4").removeAttr("disabled");
                    $("#type5").removeAttr("disabled");
                    $("#type6").attr("disabled", "disabled");

                    $("#btnType2").css("display", "inline");
                    $("#btnType3").css("display", "inline");
                    $("#btnType4").css("display", "inline");
                    $("#btnType5").css("display", "inline");
                    $("#btnType6").css("display", "none");

                    $("#ubtnType2").css("display", "none");
                    $("#ubtnType3").css("display", "none");
                    $("#ubtnType4").css("display", "none");
                    $("#ubtnType5").css("display", "none");
                    $("#ubtnType6").css("display", "inline");
                    fnGetBillStatus('5', '8', orderID); //获取审批状态，判断操作按钮       
                } else if ($("#hiddBillStatus").val() == 2) {

                $("#btn_save").css("display", "none");
                $("#btn_save0").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#type2").attr("disabled", "disabled");
                $("#type3").attr("disabled", "disabled");
                $("#type4").attr("disabled", "disabled");
                $("#type5").attr("disabled", "disabled");
                $("#type6").removeAttr("disabled");


                $("#btnType2").css("display", "none");
                $("#btnType3").css("display", "none");
                $("#btnType4").css("display", "none");
                $("#btnType5").css("display", "none");
                $("#btnType6").css("display", "inline");

                $("#ubtnType2").css("display", "inline");
                $("#ubtnType3").css("display", "inline");
                $("#ubtnType4").css("display", "inline");
                $("#ubtnType5").css("display", "inline");
                $("#ubtnType6").css("display", "none");
                }
                else {
                        $("#btn_save").css("display", "none");
                        $("#btn_save0").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#type2").attr("disabled", "disabled");
                        $("#type3").attr("disabled", "disabled");
                        $("#type4").attr("disabled", "disabled");
                        $("#type5").attr("disabled", "disabled");
                        $("#type6").attr("disabled", "disabled");

                        $("#btnType2").css("display", "none");
                        $("#btnType3").css("display", "none");
                        $("#btnType4").css("display", "none");
                        $("#btnType5").css("display", "none");
                        $("#btnType6").css("display", "none");

                        $("#ubtnType2").css("display", "inline");
                        $("#ubtnType3").css("display", "inline");
                        $("#ubtnType4").css("display", "inline");
                        $("#ubtnType5").css("display", "inline");
                        $("#ubtnType6").css("display", "inline");
                }
            } catch (e) { }
            fnGetDetailOrder(data.data[0].PlanNo);
            GetFlowButton_DisplayControl();
        }
    });
}

//获取主表信息
function fnGetDetailOrder(orderNo) {

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellPlanList.ashx",
        data: 'orderNo=' + escape(orderNo) + '&action=detail',
        dataType: 'string', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {

            var result = null;
            eval("result = " + data);

            if (result.result) {
                /// <summary>
                /// TreeView
                /// </summary>
                /// <param name="containerID">TreeView的容器元素的ID</param>
                /// <param name="nodes">节点数组</param>
                /// <param name="selMode">选择模式0:多选;1:单选</param>
                /// <param name="selNodeType">可选节点类型:0不限制</param>
                /// <param name="expandLevel">默认展开层级(数字)</param>      
                /// <param name="mode">弹出(0) OR 平板(1) 显示方式</param>      
                /// <param name="valNodeType">取值节点类型</param>     
                /// <param name="selDuplicate">取值是否允许重复（1:启用；0:禁用）</param>
                /// <param name="enableLinkage">是否启用联动效果(1:启用；0:禁用,默认：1)</param>
                treeview1 = new TreeView("detailPanel", result.data, 1, 2, 10000000, 0, 2, 1, 0);
                treeview1.callback = fnSelectNote;
                $("#hiddDetailCount").val(result.data.length);
                //treeview2 = new TreeView("treeDiv2",result.data,1,2,2,1,2,false);
                if (result.data.length > 0) {
                    if (result.data[0].subNodes.length > 0) {
                        if (!document.getElementById("type6").checked) {
                            var selectVaule = $("#hiddSelectValue").val();
                            if (selectVaule == '') {
                                treeview1.select([result.data[0].subNodes[0].value]);
                            }

                            else {
                                treeview1.select([selectVaule]);
                            }
                        }
                        else {
                            treeview1.select([result.data[0].subNodes[0].value]);
                        }

                    }
                }

            }
        }
    });
}

//修改明细操作时给页面赋值
function fnSelectNote(value) {
    $("#hiddSelectValue").val(value.val);
    if (document.getElementById("type4").checked) {//修改本级操作
        var arr = value.val.split('|');
        var typeVaule = arr[2];
        $("#ddlDetailType").val(typeVaule);
        if (typeVaule == 1) {//部门明细
            $("#DeptId").css("display", "");
            $("#DeptId").val(arr[6]);
            $("#hiddDeptID").val(arr[3]);
            $("#UserSeller").css("display", "none");
            $("#ProductID").css("display", "none");
            $("#lblDetailType").html('部门');
        }
        if (typeVaule == 2) {//员工明细
            $("#DeptId").css("display", "none");
            $("#UserSeller").css("display", "");
            $("#UserSeller").val(arr[6]);
            $("#hiddSeller").val(arr[3]);
            $("#ProductID").css("display", "none");
            $("#lblDetailType").html('员工');
        }
        if (typeVaule == 3) {//物品明细
            $("#DeptId").css("display", "none");
            $("#UserSeller").css("display", "none");
            $("#ProductID").css("display", "");
            $("#ProductID").val(arr[6]);
            $("#hiddProID").val(arr[3]);
            $("#lblDetailType").html('物品');
        }
        $("#DetailTotal").val(FormatAfterDotNumber(arr[4],precisionLength)); //明细计划总金额
        $("#MinDetailotal").val(FormatAfterDotNumber(arr[5],precisionLength)); //mingxi 最低计划额
    }
    if (document.getElementById("type6").checked) {//修改本级操作
        Show();
    }
}

//提交审批成功和审批成功后调用一个函数0:提交审批成功  1:审批成功,2审批拒绝3撤销审批
function Fun_FlowApply_Operate_Succeed(values) {
    try {
        if (values != 2 && values != 3) {
            $("#btn_save").css("display", "none");
            $("#btn_save0").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#type2").attr("disabled", "disabled");
            $("#type3").attr("disabled", "disabled");
            $("#type4").attr("disabled", "disabled");
            $("#type5").attr("disabled", "disabled");
            $("#type6").attr("disabled", "disabled");

            $("#btnType2").css("display", "none");
            $("#btnType3").css("display", "none");
            $("#btnType4").css("display", "none");
            $("#btnType5").css("display", "none");
            $("#btnType6").css("display", "none");

            $("#ubtnType2").css("display", "inline");
            $("#ubtnType3").css("display", "inline");
            $("#ubtnType4").css("display", "inline");
            $("#ubtnType5").css("display", "inline");
            $("#ubtnType6").css("display", "inline");
        }
        else {
            $("#btn_save").css("display", "inline");
            $("#btn_save0").css("display", "inline");
            $("#imgUnSave").css("display", "none");
            $("#type2").removeAttr("disabled");
            $("#type3").removeAttr("disabled");
            $("#type4").removeAttr("disabled");
            $("#type5").removeAttr("disabled");
            $("#type6").attr("disabled", "disabled");

            $("#btnType2").css("display", "inline");
            $("#btnType3").css("display", "inline");
            $("#btnType4").css("display", "inline");
            $("#btnType5").css("display", "inline");
            $("#btnType6").css("display", "none");

            $("#ubtnType2").css("display", "none");
            $("#ubtnType3").css("display", "none");
            $("#ubtnType4").css("display", "none");
            $("#ubtnType5").css("display", "none");
            $("#ubtnType6").css("display", "inline");
        }
    } catch (e) { }
}


//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var CodeType = $.trim($("#SellPlanUC_ddlCodeRule").val()); //计划编号产生的规则
    var PlanNo = $.trim($("#SellPlanUC_txtCode").val());  //计划编号
    var StartDate = $("#StartDate").val(); //开始日期
    var EndDate = $("#EndDate").val(); //截止日期
    var PlanTotal = $.trim($("#PlanTotal").val());    //计划总金额
    var MinPlanTotal = $.trim($("#MinPlanTotal").val());           //最低计划额
    var Remark = $.trim($("#Remark").val());      //备注
    var Hortation = $("#Hortation").val();      //奖励方案

    var CanViewUserName = $("#UserCanViewUser").val();      //可查看该销售机会的员工姓名
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if (CodeType == "") {
        if (PlanNo == "") {
            isFlag = false;
            fieldText = fieldText + "计划编号|";
            msgText = msgText + "请输入计划编号|";
        }
        else {
            if (!CodeCheck(PlanNo)) {
                isFlag = false;
                fieldText = fieldText + "计划编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
    }
    var PlanType = $("#PlanType").val();      //计划类型（1.月计划2.季计划3.年计
    if (PlanType == 4) {//日计划
        var PlanYear = $("#PlanDateTime").val(); //计划年份
        if (PlanYear == '') {
            isFlag = false;
            fieldText = fieldText + "计划时期|";
            msgText = msgText + "请选择计划时期|";
        }
    }
    var Title = $.trim($("#Title").val());      //计划主题
    if (Title == "") {
        isFlag = false;
        fieldText = fieldText + "计划名称|";
        msgText = msgText + "请输入计划名称|";
    }
    if (StartDate != "" && EndDate != "") {
        if (CompareDate(StartDate, EndDate) == '1') {
            isFlag = false;
            fieldText = fieldText + "开始、截止日期|";
            msgText = msgText + "开始日期不能晚于截止日期|";
        }
    }
    if (PlanTotal == "") {
        isFlag = false;
        fieldText = fieldText + "计划总金额|";
        msgText = msgText + "请输入计划总金额|";
    }
    else {
        if (!IsNumeric(PlanTotal, 22, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "计划总金额|";
            msgText = msgText + "计划总金额输入有误，请输入有效的数值|";
        }
    }
    if (MinPlanTotal == "") {
        isFlag = false;
        fieldText = fieldText + "最低计划额|";
        msgText = msgText + "请输入最低计划额|";
    }
    else {
        if (!IsNumeric(MinPlanTotal, 22, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "最低计划额|";
            msgText = msgText + "最低计划额输入有误，请输入有效的数值|";
        }
    }
    if (MinPlanTotal != "" && PlanTotal != "") {

        if (IsNumeric(PlanTotal, 22, precisionLength) && IsNumeric(MinPlanTotal, 22, precisionLength)) {
            if (parseFloat(PlanTotal) < parseFloat(MinPlanTotal)) {
                isFlag = false;
                fieldText = fieldText + "最低计划额|";
                msgText = msgText + "最低计划额不可大于计划总金额|";
            }
        }
    } if (!fnCheckStrLen(Hortation, 2048)) {
        isFlag = false;
        fieldText = fieldText + "激励方案|";
        msgText = msgText + "激励方案最多只允许输入2048个字符！|";
    }
    if (!fnCheckStrLen(CanViewUserName, 2048)) {
        isFlag = false;
        fieldText = fieldText + "可查看人员|";
        msgText = msgText + "可查看人员选择过多！|";
    }

    if (!fnCheckStrLen(Remark, 1024)) {
        isFlag = false;
        fieldText = fieldText + "备注|";
        msgText = msgText + "备注最多只允许输入1024个字符！|";
    }

    if (document.getElementById("type2").checked || document.getElementById("type3").checked || document.getElementById("type4").checked) {
        var DetailType = $("#ddlDetailType").val(); //明细种类（1.部门明细2.员工明细3.物品明细）
        if (DetailType == 1) {//部门明细
            var DetailID = $("#hiddDeptID").val();
            if (DetailID == "") {
                isFlag = false;
                fieldText = fieldText + "部门|";
                msgText = msgText + "请选择部门|";
            }
        }
        else if (DetailType == 2) {//员工明细
            var DetailID = $("#hiddSeller").val();
            if (DetailID == "") {
                isFlag = false;
                fieldText = fieldText + "员工|";
                msgText = msgText + "请选择员工|";
            }
        }
        else if (DetailType == 3) {//物品明细
            var DetailID = $("#hiddProID").val();
            if (DetailID == "") {
                isFlag = false;
                fieldText = fieldText + "物品|";
                msgText = msgText + "请选择物品|";
            }
        }
        var DetailTotal = $.trim($("#DetailTotal").val()); //明细计划总金额
        var MinDetailotal = $.trim($("#MinDetailotal").val()); //mingxi 最低计划额

        if (DetailTotal == "") {
            isFlag = false;
            fieldText = fieldText + "明细计划额|";
            msgText = msgText + "请输入明细计划额|";
        } else {
            if (!IsNumeric(DetailTotal, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "明细计划额|";
                msgText = msgText + "明细计划额输入有误，请输入有效的数值|";
            }
        }
        if (MinDetailotal == "") {
            isFlag = false;
            fieldText = fieldText + "明细最低计划额|";
            msgText = msgText + "请输入明细最低计划额|";
        }
        else {
            if (!IsNumeric(MinDetailotal, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "明细最低计划额|";
                msgText = msgText + "明细最低计划额输入有误，请输入有效的数值|";
            }
        }
        if (MinDetailotal != "" && DetailTotal != "") {

            if (IsNumeric(DetailTotal, 22, precisionLength) && IsNumeric(MinDetailotal, 22, precisionLength)) {
                if (parseFloat(DetailTotal) < parseFloat(MinDetailotal)) {
                    isFlag = false;
                    fieldText = fieldText + "明细最低计划额|";
                    msgText = msgText + "明细最低计划额不可大于明细计划额|";
                }
            }
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




//修改计划类型
function fnType() {
    var typeVaule = $("#PlanType").val();
    if (typeVaule == 1) {//月计划
        $("#ddlMonth").css("display", "");
        $("#ddlSeason").css("display", "none");
        $("#PlanDateTime").css("display", "none");
        $("#ddlYear").css("display", "");
        $("#ddlWeek").css("display", "none");
        $("#Half").css("display", "none");
    }
    if (typeVaule == 2) {//季计划
        $("#ddlMonth").css("display", "none");
        $("#ddlSeason").css("display", "");
        $("#PlanDateTime").css("display", "none");
        $("#ddlYear").css("display", "");
        $("#ddlWeek").css("display", "none");
        $("#Half").css("display", "none");
    }
    if (typeVaule == 3) {//年计划
        $("#ddlMonth").css("display", "none");
        $("#ddlSeason").css("display", "none");
        $("#PlanDateTime").css("display", "none");
        $("#ddlYear").css("display", "");
        $("#ddlWeek").css("display", "none");
        $("#Half").css("display", "none");
    }
    if (typeVaule == 4) {//日计划
        $("#ddlMonth").css("display", "none");
        $("#ddlSeason").css("display", "none");
        $("#PlanDateTime").css("display", "");
        $("#ddlYear").css("display", "none");
        $("#ddlWeek").css("display", "none");
        $("#Half").css("display", "none");
    }
    if (typeVaule == 5) {//周计划
        $("#ddlMonth").css("display", "none");
        $("#ddlSeason").css("display", "none");
        $("#PlanDateTime").css("display", "none");
        $("#ddlYear").css("display", "");
        $("#ddlWeek").css("display", "");
        $("#Half").css("display", "none");
    }
    if (typeVaule == 6) {//半年计划
        $("#ddlMonth").css("display", "none");
        $("#ddlSeason").css("display", "none");
        $("#PlanDateTime").css("display", "none");
        $("#ddlYear").css("display", "");
        $("#ddlWeek").css("display", "none");
        $("#Half").css("display", "");
    }
    if (typeVaule == 7) {//其他计划
        $("#ddlMonth").css("display", "none");
        $("#ddlSeason").css("display", "none");
        $("#PlanDateTime").css("display", "none");
        $("#ddlYear").css("display", "");
        $("#ddlWeek").css("display", "none");
        $("#Half").css("display", "none");
    }
}

//更改操作类型时去除已填的明细数据
function fnClearDetailData() {
    $("#DeptId").val('');
    $("#UserSeller").val('');
    $("#ProductID").val('');
    $("#DetailTotal").val('');
    $("#MinDetailotal").val('');
    $("#hiddSeller").val('');
    $("#hiddProID").val('');
    $("#hiddDeptID").val('');
}

//修改操作类型
function fnChangeType(obj) {
    var orderID = $("#hiddOrderID").val();         //计划id
    if (orderID == '0' || orderID == '') {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先保存基本信息！");

        return;
    }
    var DetailCount = $("#hiddDetailCount").val();
    switch (obj) {
        case 1:


            $("#ddlDetailType").val('1');
            $("#lblDetailType").html('部门');
            $("#type1").attr("checked", "checked");
            CloseDiv();
            document.getElementById('divDetalShow').style.display = 'none';
            fnChangeDetailType();
            fnClearDetailData();
            break;
        case 2:

            $("#ddlDetailType").val('1');
            $("#lblDetailType").html('部门');
            $("#type2").attr("checked", "checked");

            fnChangeDetailType();
            fnClearDetailData();
            openRotoscopingDiv(false, 'divBackShadow', 'BackShadowIframe');
            document.getElementById("divDetalShow").style.zIndex = "2";
            document.getElementById("divBackShadow").style.zIndex = "1";
            document.getElementById('divDetalShow').style.display = 'block';
            break;
        case 3:
            if (DetailCount == '0') {
                $("#type1").attr("checked", "checked");


                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "无明细数据可进行当前操作！");
            }
            else {
                $("#type3").attr("checked", "checked");

                document.getElementById("divDetalShow").style.zIndex = "2";
                document.getElementById("divBackShadow").style.zIndex = "1";
                document.getElementById('divDetalShow').style.display = 'block';
            }
            $("#ddlDetailType").val('1');
            $("#lblDetailType").html('部门');
            fnChangeDetailType();
            fnClearDetailData();

            break;
        case 4:
            if (DetailCount == '0') {
                $("#type1").attr("checked", "checked");

                $("#ddlDetailType").val('1');
                $("#lblDetailType").html('部门');
                fnClearDetailData();
                fnChangeDetailType();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "无明细数据可进行当前操作！");
            }
            else {
                $("#type4").attr("checked", "checked");


                fnSelectNote(treeview1.getValue());
                openRotoscopingDiv(false, 'divBackShadow', 'BackShadowIframe');
                document.getElementById("divDetalShow").style.zIndex = "2";
                document.getElementById("divBackShadow").style.zIndex = "1";
                document.getElementById('divDetalShow').style.display = 'block';
            }

            break;
        case 5:
            $("#ddlDetailType").val('1');
            $("#lblDetailType").html('部门');
            fnChangeDetailType();
            fnClearDetailData();
            if (DetailCount == '0') {
                $("#type1").attr("checked", "checked");

                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "无明细数据可进行当前操作！");
            }
            else {
                $("#type5").attr("checked", "checked");

                InsertSellOfferData();
                $("#hiddSelectValue").val('');
            }

            break;
        case 6:
            if (DetailCount == '0') {
                $("#type1").attr("checked", "checked");

                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "无明细数据可进行当前操作！");
            }
            else {
                $("#type6").attr("checked", "checked");
                var arr = treeview1.getValue().val.split('|');

                Show();

            }

            $("#ddlDetailType").val('1');
            $("#lblDetailType").html('部门');
            fnChangeDetailType();
            fnClearDetailData();
            break;
        default:
            break;
    }
}

//改变明细类型
function fnChangeDetailType() {
    var typeVaule = $("#ddlDetailType").val();
    if (typeVaule == 1) {//部门明细
        $("#DeptId").css("display", "");
        $("#UserSeller").css("display", "none");
        $("#ProductID").css("display", "none");
        $("#lblDetailType").html('部门');
    }
    if (typeVaule == 2) {//员工明细
        $("#DeptId").css("display", "none");
        $("#UserSeller").css("display", "");
        $("#ProductID").css("display", "none");
        $("#lblDetailType").html('员工');
    }
    if (typeVaule == 3) {//物品明细
        $("#DeptId").css("display", "none");
        $("#UserSeller").css("display", "none");
        $("#ProductID").css("display", "");
        $("#lblDetailType").html('物品');
    }
    fnClearDetailData();
}

//打印功能
function fnPrintOrder() {
    
    var orderID = $("#hiddOrderID").val();
    if(orderID == '0' || orderID == '')
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
        return;
    }      
    var PlanNo = $("#SellPlanUC_txtCode").val();        //退货单编号
    window.open('../../../Pages/PrinttingModel/SellManager/SellPlanPrint.aspx?no=' + PlanNo + '&ID=' + orderID);
}



//新建时显示弹出层
function Show() {
    openRotoscopingDiv(false, 'divBackShadow', 'BackShadowIframe');
    document.getElementById("div_Add").style.zIndex = "2";
    document.getElementById("divBackShadow").style.zIndex = "1";
    document.getElementById('div_Add').style.display = 'block';
    var arr = treeview1.getValue().val.split('|');
    try {
        if (arr[8] == 1) {

            $("#SummarizeNote").val(arr[10]);         //总结内容
            $("#AimRealResult").val(arr[11]);      //目标实绩
            $("#Difference").val(arr[13]);         //差额
            $("#AddOrCut").val(arr[12]);         //成情况（0低于目标值，1等于目标值，2超过目标值）
            $("#CompletePercent").val(arr[14]);      //目标达成率%
        }      
    } catch (e) { }
}

//隐藏弹出层
function Hide() {
    CloseDiv();
    document.getElementById('div_Add').style.display = 'none';
    Clear();
}

//清空弹出层内容
function Clear() {
    $("#SummarizeNote").val('');
    $("#AimRealResult").val('');
    $("#Difference").val('');
    $("#CompletePercent").val('');
    $("#Summarizer").val($("#hiddSummarizer").val());
    $("#SummarizeDate").val($("#hiddSummarizeDate").val());
    $("#AddOrCut").val('0');
}

//关闭遮罩层
function CloseDiv() {
    closeRotoscopingDiv(false, 'divBackShadow');
}

//总结
function fnSumm() {
    if (!CheckInput1()) {
        return;
    }

    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    var strInfo = fnGetInfo1();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellPlanAdd.ashx",
        data: strInfo,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {
                $("#hiddOrderID").val(data.id);
                fnGetOrder();
            }
            else {
                hidePopup();
            }
            Hide();
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//获取总结信息
function fnGetInfo1() {
    var strInfo = '';
    var CodeType = $("#SellPlanUC_ddlCodeRule").val(); //计划编号产生的规则
    var PlanNo = $("#SellPlanUC_txtCode").val();  //计划编号
    var arr = treeview1.getValue().val.split('|');
    var ID = arr[0]; //明细ID，从树中获取
    var SummarizeNote = $.trim($("#SummarizeNote").val());         //总结内容
    var AimRealResult = $.trim($("#AimRealResult").val());      //目标实绩
    var Difference = $.trim($("#Difference").val());         //差额
    var AddOrCut = $("#AddOrCut").val();         //成情况（0低于目标值，1等于目标值，2超过目标值）
    var CompletePercent = $("#CompletePercent").val();      //目标达成率%

    strInfo = 'CodeType=' + escape(CodeType) + '&PlanNo=' + escape(PlanNo) + '&ID=' + escape(ID) +
            '&SummarizeNote=' + escape(SummarizeNote) + '&AimRealResult=' + escape(AimRealResult) + '&Difference=' + escape(Difference) +
            '&AddOrCut=' + escape(AddOrCut) + '&CompletePercent=' + escape(CompletePercent) + '&action=summ';
    return strInfo;
}

//表单验证
function CheckInput1() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var SummarizeNote = $.trim($("#SummarizeNote").val());         //总结内容
    var AimRealResult = $.trim($("#AimRealResult").val());      //目标实绩
    var Difference = $.trim($("#Difference").val());         //差额
   
    var CompletePercent = $.trim($("#CompletePercent").val());      //目标达成率%
    
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (CompletePercent == "") {
        isFlag = false;
        fieldText = fieldText + "计划达成率|";
        msgText = msgText + "请输入计划达成率|";
    }
    else {
        if (!IsNumeric(CompletePercent, 12, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "计划达成率|";
            msgText = msgText + "计划达成率输入有误，请输入有效的数值|";
        }
    }
    if (!fnCheckStrLen(SummarizeNote, 1024)) {
        isFlag = false;
        fieldText = fieldText + "总结内容|";
        msgText = msgText + "总结内容最多只允许输入1024个字符！|";
    }
    if (!fnCheckStrLen(AimRealResult, 1024)) {
        isFlag = false;
        fieldText = fieldText + "计划实绩|";
        msgText = msgText + "计划实绩最多只允许输入1024个字符！！|";
    }
    if (!fnCheckStrLen(Difference, 100)) {
        isFlag = false;
        fieldText = fieldText + "备注|";
        msgText = msgText + "备注最多只允许输入100个字符！|";
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

//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) 
{
   try{
        $("#ExtField1").val(data.ExtField1);
        $("#ExtField2").val(data.ExtField2);
        $("#ExtField3").val(data.ExtField3);
        $("#ExtField4").val(data.ExtField4);
        $("#ExtField5").val(data.ExtField5);
        $("#ExtField6").val(data.ExtField6);
        $("#ExtField7").val(data.ExtField7);
        $("#ExtField8").val(data.ExtField8);
        $("#ExtField9").val(data.ExtField9);
        $("#ExtField10").val(data.ExtField10);
    }
    catch(Error){}
}