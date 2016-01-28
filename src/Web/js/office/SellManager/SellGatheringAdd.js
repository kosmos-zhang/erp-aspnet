
$(document).ready(function() {
$("#SellGathering_ddlCodeRule").css("width", "80px");
$("#SellGathering_txtCode").css("width", "80px");
    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['islist']; //是否从列表页面进入
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031701', 'ModuleID=2031702');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (flag != null) {
            $("#ibtnBack").css("display", "inline");

        }
    }
    GetExtAttr("officedba.SellGathering",null);
});
//清除销售订单
function clearSellOrder() {
    fnFromBill(document.getElementById("FromType"));

    closeSellOrderdiv();
}
//清除销售订单
function clearSellSend() {
    fnFromBill(document.getElementById("FromType"));

    closeSellSenddiv();
}

//获取查询参数
function GetRequestToHidden() {
    var url = location.search;
    return url;
}

//返回按钮
function fnBack() {
    var URLParams = document.getElementById("HiddenURLParams").value;
    window.location.href = 'SellGatheringList.aspx' + URLParams;
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

//选择部门
function fnSelectDept() {
   
            alertdiv('DeptId,hiddDeptID');
       
}

//选择回款计划来源时
function fnFromBill(obj) {
    if (obj.selectedIndex == 0) {
        //清除已填写数据
        $("#FromBillID").val(''); //订单编号
        $("#FromBillID").removeAttr("title"); //订单ID
        $("#CustID").val(''); //客户名称
        $("#CustType").val(''); //客户类型
        $("#CustTel").val(''); //客户电话
        $("#CurrencyType").val(''); //币种
        $("#CurrencyType").attr("title", ''); //币种
        $("#Rate").val(FormatAfterDotNumber(0,precisionLength)); //汇率
        $("#UserSeller").val('')//业务员名称
        $("#hiddSeller").val();
        $("#DeptId").val(''); //部门   
        $("#hiddDeptID").val(''); //部门编号  
    }
    if (obj.selectedIndex == 1 || obj.selectedIndex == 2) {
        //清除已填写数据
        $("#FromBillID").val(''); //订单编号
        $("#FromBillID").removeAttr("title"); //订单ID
        $("#CustID").val(''); //客户名称
        $("#CustType").val(''); //客户类型
        $("#CustTel").val(''); //客户电话
        $("#CustID").removeAttr("title"); //客户编号
        $("#CurrencyType").val(''); //币种
        $("#CurrencyType").attr("title", ''); //币种
        $("#UserSeller").val('')//业务员名称
        $("#hiddSeller").val();
        $("#DeptId").val(''); //部门名称
        $("#hiddDeptID").val(''); //部门编号
    }
}

//选择源单
function fnSelectOfferInfo() {
    if ($("#FromType").val() == 1) {
        if ($("#FromType").attr("disabled") != true) {
            popSellOrderObj.ShowList('');
        }
    }
    if ($("#FromType").val() == 2) {
        if ($("#FromType").attr("disabled") != true) {
            popSellSendObj.ShowList('');
        }
    }
}


//选择业务员
function fnSelectSeller() {
    
        alertdiv('UserSeller,hiddSeller');
    
}
//选择币种
function fnSelectCurrency() {
    if ($("#FromType").val() == 0) {
        popSellCurrObj.ShowList('CurrencyType');
    }
}

//选择客户
function fnSelectCustInfo() {
    if ($("#FromType").val() == 0) {
        popSellCustObj.ShowList('protion');
    }
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
            $("#CustTel").val(data.Tel); //客户名称
            $("#CustType").val(data.TypeName); //客户类型
            $("#CurrencyType").val(data.CurrencyName); //币种
            $("#CurrencyType").attr("title", data.CurrencyType); //币种
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}

//清除客户信息
function ClearSellModuCustdiv() {
    $("#CustID").val(''); //客户名称
    $("#CustID").removeAttr("title"); //客户编号
    $("#CustTel").val(''); //客户名称
    $("#CustType").val(''); //客户类型
    $("#CurrencyType").val(''); //币种
    $("#CurrencyType").removeAttr("title"); //币种
    closeSellModuCustdiv();
}

////选择订单后带出订单明细信息
function fnSelectSellOrder(OrderId,OrderNo) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SelectSellOrderDetail.ashx",
        data: 'actionDet=orderList&OrderID=' + OrderId,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取订单数据失败,请确认"); },
        success: function(data) {
            //订单基本信息    
            if (data.ord != null) {
                $("#FromBillID").val(data.ord[0].OrderNo); //订单编号
                $("#FromBillID").attr("title", OrderId); //订单ID
                $("#CustID").val(data.ord[0].CustName); //客户名称
                $("#CustID").attr("title", data.ord[0].CustID); //客户编号
                $("#UserSeller").val(data.ord[0].EmployeeName)//业务员名称
                $("#hiddSeller").val(data.ord[0].Seller)//业务员名称
                $("#DeptId").val(data.ord[0].DeptName); //部门名称
                $("#hiddDeptID").val(data.ord[0].SellDeptId); //部门编号
                $("#CustType").val(data.ord[0].TypeName); //客户类型
                $("#CustTel").val(data.ord[0].CustTel); //客户电话

                $("#CurrencyType").val(data.ord[0].CurrencyName); //币种
                $("#CurrencyType").attr("title", data.ord[0].CurrencyType); //币种
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该单据信息已不存在,请确认");
            }
        }
    });
    closeSellOrderdiv(); //关闭客户选择控件  
}

////选择回款计划后带出回款计划明细信息
function fnSelectSellSend(OrderId) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SelectSellSend.ashx",
        data: 'actionSend=orderList&OrderID=' + OrderId,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取回款计划数据失败,请确认"); },
        success: function(data) {
            //订单基本信息    
            if (data.ord != null) {
                $("#FromBillID").val(data.ord[0].SendNo); //订单编号
                $("#FromBillID").attr("title", OrderId); //订单ID
                $("#CustID").val(data.ord[0].CustName); //客户名称
                $("#CustID").attr("title", data.ord[0].CustID); //客户编号
                $("#UserSeller").val(data.ord[0].EmployeeName)//业务员名称
                $("#hiddSeller").val(data.ord[0].Seller)//业务员名称
                $("#DeptId").val(data.ord[0].DeptName); //部门名称
                $("#hiddDeptID").val(data.ord[0].SellDeptId); //部门编号
                $("#CustType").val(data.ord[0].TypeName); //客户类型
                $("#CustTel").val(data.ord[0].CustTel); //客户电话

                $("#CurrencyType").val(data.ord[0].CurrencyName); //币种
                $("#CurrencyType").attr("title", data.ord[0].CurrencyType); //币种
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该单据信息已不存在,请确认");
            }
        }
    });
    closeSellSenddiv(); //关闭客户选择控件  
}

//保存数据
function InsertSellOfferData() {
    if (!CheckInput()) {
        return;
    }
    var Title = $.trim($("#Title").val());         //主题    
    var CodeType = $("#SellGathering_ddlCodeRule").val(); //回款计划编号产生的规则
    var GatheringNo = $("#SellGathering_txtCode").val(); //回款计划编号                                  
    var CustID = $("#CustID").attr("title");         //客户ID（关联客户信息表）                   
    var FromType = $("#FromType").val();       //源单类型（0无来源，1发货通知单，2销售订单）
    if ($("#FromBillID").val() != '') {
        var FromBillID = $("#FromBillID").attr("title");     //源单ID   
    }
    else {
        var FromBillID = '';
    }
    var CurrencyType = $("#CurrencyType").attr("title");   //币种ID(对应货币代码表CD)                   
    var PlanGatherDate = $("#PlanGatherDate").val(); //计划回款日期                               
    var PlanPrice = $("#PlanPrice").val();      //计划回款金额                               
    var GatheringTime = $("#GatheringTime").val();  //期次                                       
    var FactPrice = $("#FactPrice").val();      //实际回款金额                               
    var FactGatherDate = $("#FactGatherDate").val(); //实际回款日期
    var Seller = $("#hiddSeller").val();         //业务员(对应员工表ID)                       
    var SellDeptId = $("#hiddDeptID").val();     //部门(对部门表ID)                           
    var LinkBillNo = $("#LinkBillNo").val();     //回款相关单据号                             
    var State = $("#State").val();          //状态(1已回款2未回款 3部分回款)             
    var Remark = $("#Remark").val();         //备注


    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellGatheringAdd.ashx",
        data: 'CodeType=' + escape(CodeType) + '&GatheringNo=' + escape(GatheringNo) + '&Title=' + escape(Title) + '&CustID=' + escape(CustID) +
        '&FromType=' + escape(FromType) + '&FromBillID=' + escape(FromBillID) + '&CurrencyType=' + escape(CurrencyType) +
        '&PlanGatherDate=' + escape(PlanGatherDate) + '&PlanPrice=' + escape(PlanPrice) + '&GatheringTime=' + escape(GatheringTime) +
        '&FactPrice=' + escape(FactPrice) + '&FactGatherDate=' + escape(FactGatherDate) + '&Seller=' + escape(Seller) +
        '&SellDeptId=' + escape(SellDeptId) + '&LinkBillNo=' + escape(LinkBillNo) + '&State=' + escape(State) +
        '&Remark=' + escape(Remark) + '&action=insert'+GetExtAttrValue(),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {

                $("#btn_save").css("display", "none");
                $("#btn_update").css("display", "inline");
                $("#SellGathering_txtCode").val(data.no);
                $("#SellGathering_txtCode").attr("disabled", "disabled");
                $("#SellGathering_ddlCodeRule").css("display", "none");
                $("#SellGathering_txtCode").css("width", "95%");
            }
            hidePopup();
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//保存数据
function UpdateSellOfferData() {
    if (!CheckInput()) {
        return;
    }
    var Title = $.trim($("#Title").val());         //主题    
    var CodeType = $("#SellGathering_ddlCodeRule").val(); //回款计划编号产生的规则
    var GatheringNo = $("#SellGathering_txtCode").val(); //回款计划编号                                  
    var CustID = $("#CustID").attr("title");         //客户ID（关联客户信息表）                   
    var FromType = $("#FromType").val();       //源单类型（0无来源，1发货通知单，2销售订单）
    if ($("#FromBillID").val() != '') {
        var FromBillID = $("#FromBillID").attr("title");     //源单ID   
    }
    else {
        var FromBillID = '';
    }
    var CurrencyType = $("#CurrencyType").attr("title");   //币种ID(对应货币代码表CD)                   
    var PlanGatherDate = $("#PlanGatherDate").val(); //计划回款日期                               
    var PlanPrice = $("#PlanPrice").val();      //计划回款金额                               
    var GatheringTime = $("#GatheringTime").val();  //期次                                       
    var FactPrice = $("#FactPrice").val();      //实际回款金额                               
    var FactGatherDate = $("#FactGatherDate").val(); //实际回款日期
    var Seller = $("#hiddSeller").val();         //业务员(对应员工表ID)                       
    var SellDeptId = $("#hiddDeptID").val();     //部门(对部门表ID)                           
    var LinkBillNo = $("#LinkBillNo").val();     //回款相关单据号                             
    var State = $("#State").val();          //状态(1已回款2未回款 3部分回款)             
    var Remark = $("#Remark").val();         //备注


    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellGatheringAdd.ashx",
        data: 'CodeType=' + escape(CodeType) + '&GatheringNo=' + escape(GatheringNo) + '&Title=' + escape(Title) + '&CustID=' + escape(CustID) +
        '&FromType=' + escape(FromType) + '&FromBillID=' + escape(FromBillID) + '&CurrencyType=' + escape(CurrencyType) +
        '&PlanGatherDate=' + escape(PlanGatherDate) + '&PlanPrice=' + escape(PlanPrice) + '&GatheringTime=' + escape(GatheringTime) +
        '&FactPrice=' + escape(FactPrice) + '&FactGatherDate=' + escape(FactGatherDate) + '&Seller=' + escape(Seller) +
        '&SellDeptId=' + escape(SellDeptId) + '&LinkBillNo=' + escape(LinkBillNo) + '&State=' + escape(State) +
        '&Remark=' + escape(Remark) + '&action=update'+GetExtAttrValue(),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {

                $("#btn_save").css("display", "none");
                $("#btn_update").css("display", "inline");
                $("#SellGathering_txtCode").val(data.no);
                $("#SellGathering_txtCode").attr("disabled", "disabled");
                $("#SellGathering_ddlCodeRule").css("display", "none");
                $("#SellGathering_txtCode").css("width", "95%");
            }
            hidePopup();
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var CodeType = $("#SellGathering_ddlCodeRule").val(); //回款计划编号产生的规则
    var SendNo = $.trim($("#SellGathering_txtCode").val()); //回款计划编号    
    //var Title = $.trim($("#Title").val());         //主题                                                              
    var FromType = $("#FromType").val();      //源单类型（0无源单，1销售订单，2销售回款计划）
    var Seller = $("#UserSeller").val();        //业务员(对应员工表ID)
    var SellDeptId = $("#DeptId").val();    //部门(对部门表ID)                                                                                                                                                                                                                                                                                                                  
    var FromBillID = $("#FromBillID").val(); //来源单据编号                                                
    var CurrencyType = $.trim($("#CurrencyType").val());      //币种                                                    
    var CustID = $("#CustID").val(); //客户
    var PlanPrice = $("#PlanPrice").val(); //计划回款金额
    var PlanGatherDate = $.trim($("#PlanGatherDate").val()); //计划回款时间
    var FactPrice = $.trim($("#FactPrice").val()); //实际回款金额
    var FactGatherDate = $("#FactGatherDate").val(); //实际回款时间                                                                                                                        
    var GatheringTime = $.trim($("#GatheringTime").val()); //期次

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    
    if (CodeType == "") {
        if (SendNo == "") {
            isFlag = false;
            fieldText = fieldText + "回款计划编号|";
            msgText = msgText + "请输入回款计划编号|";
        }
        else {
            if (isnumberorLetters(SendNo)) {
                isFlag = false;
                fieldText = fieldText + "回款计划编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
    }

    if (!IsZint(GatheringTime)) {
        isFlag = false;
        fieldText = fieldText + "期次|";
        msgText = msgText + "期次只能为正整数|";
    }
    if (FromType != "0") {
        if (FromBillID == "") {
            isFlag = false;
            fieldText = fieldText + "来源单据|";
            msgText = msgText + "请选择销售订单|";
        }
    }

//    if (Title == "") {
//        isFlag = false;
//        fieldText = fieldText + "主题|";
//        msgText = msgText + "请输入主题|";
//    }

    if (CustID == "") {
        isFlag = false;
        fieldText = fieldText + "客户名称|";
        msgText = msgText + "请选择客户|";
    }

    if (Seller == "") {
        isFlag = false;
        fieldText = fieldText + "业务员|";
        msgText = msgText + "请选择业务员|";
    }
    if (SellDeptId == "") {
        isFlag = false;
        fieldText = fieldText + "部门|";
        msgText = msgText + "请选择部门|";
    }
    if (CurrencyType == "") {
        isFlag = false;
        fieldText = fieldText + "币种|";
        msgText = msgText + "请选择币种|";
    }

    if (PlanGatherDate == "") {
        isFlag = false;
        fieldText = fieldText + "预计回款时间|";
        msgText = msgText + "请选择预计回款时间|";
    }
    if (FactPrice.length > 0) {
        if (!IsNumeric(FactPrice, 14, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "实际回款金额|";
            msgText = msgText + "实际回款金额输入有误，请输入有效的数值！|";
        }
    }
    if (PlanPrice == '') {
        isFlag = false;
        fieldText = fieldText + "计划回款金额|";
        msgText = msgText + "请输入计划回款金额|";
    }
    if (PlanPrice.length > 0) {
        if (!IsNumeric(PlanPrice, 14, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "计划回款金额|";
            msgText = msgText + "计划回款金额输入有误，请输入有效的数值！|";
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

//打印功能
function fnPrintOrder() {
    var OrderNo = $.trim($("#SellGathering_txtCode").val());
    if (OrderNo == '保存时自动生成' || OrderNo == '') {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存单据再打印！");
        return;
    }
    window.open('../../../Pages/PrinttingModel/SellManager/PrintSellGathering.aspx?no=' + OrderNo);
}

//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
//    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
//    var arrKey = strKey.split('|');
//    
//    if(typeof(data)!='undefined')
//    {
//       $.each(data,function(i,item){
//            for (var t = 0; t < arrKey.length; t++) {
//                //不为空的字段名才取值
//                if ($.trim(arrKey[t]) != '') {
//                    $("#" + $.trim(arrKey[t])).val(item[$.trim(arrKey[t])]);
//                }
//            }

//       });
//    }
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
