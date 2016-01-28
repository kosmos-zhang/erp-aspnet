//var precisionLength=4;
$(document).ready(function() {
    $("#PayTypeUC_ddlCodeType").css("width", "120px");
    $("#SellChannelSttlUC_ddlCodeRule").css("width", "80px");
    
    $("#SellChannelSttlUC_txtCode").css("width", "80px");
    $("#MoneyTypeUC_ddlCodeType").css("width", "120px");
    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['islist']; //是否从列表页面进入
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031901', 'ModuleID=2031902');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (flag != null) {
            $("#ibtnBack").css("display", "inline");

        }
    }
    GetExtAttr("officedba.SellChannelSttl",null);
    GetFlowButton_DisplayControl();
});

//选择单据明细信息
function fnSelectOrderList() {
    if ($("#CustID").val() == '') {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请选择单据！");
    }
    else {
        var custID = $("#CustID").attr("title");
        var FromBillID = $("#FromBillID").attr("title"); //来源单据编号
        var CurrencyType = $("#CurrencyType").attr("title"); //币种ID
        var Rate = $("#Rate").val(); //币种ID
        popSendDetailObj.ShowList(custID, '2', CurrencyType, FromBillID, Rate);
    }
}

//清除销售订单
function clearSellSend() {
    fnDelRow();
    $("#FromBillID").val(''); //单据编号
    $("#FromBillID").attr("title", ''); //单据ID
    $("#CustID").val(''); //客户名称
    $("#CustID").attr("title", ''); //客户编号
    $("#CustTel").val(''); //客户电话
    $("#PayTypeUC_ddlCodeType").val(''); //结算方式
    $("#MoneyTypeUC_ddlCodeType").val(''); //结算方式
    $("#CurrencyType").attr("title", ''); //币种
    $("#CurrencyType").val('');
    $("#Rate").val(FormatAfterDotNumber(0,4)); //汇率
    $("#UserSeller").val('')//业务员名称
    $("#hiddSeller").val('')//业务员名称

    $("#DeptID").val(''); //部门名称
    $("#hiddDeptID").val(''); //部门编号
   
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
    window.location.href = 'SellChannelSttlList.aspx' + URLParams;
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


//去除全选按钮
function fnUnSelect(obj) {

    if (!obj.checked) {
        $("#checkall").removeAttr("checked");
        return;
    }

    else {
        //验证明细信息
        var signFrame = findObj("dg_Log", document);
        var iCount = 0; //明细中总数据数目
        var checkCount = 0; //明细中选择的数据数目
        for (i = 1; i < signFrame.rows.length; i++) {
            if (signFrame.rows[i].style.display != "none") {
                iCount = iCount + 1;
                var rowid = signFrame.rows[i].id;
                if ($("#chk" + rowid).attr("checked")) {
                    checkCount = checkCount + 1;
                }
            }
        }
        if (checkCount == iCount) {

            $("#checkall").attr("checked", "checked");
        }

    }
}

////选择单据后带出退货单明细信息
function fnSelectSellSend(OrderId) {

    fnDelRow();
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SelectSellSend.ashx",
        data: 'actionSend=orderList&OrderID=' + OrderId,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取单据数据失败,请确认"); },
        success: function(data) {
            //单据基本信息    
            if (data.ord != null) {
                $("#FromBillID").val(data.ord[0].SendNo); //单据编号
                $("#FromBillID").attr("title", OrderId); //单据ID
                $("#CustID").val(data.ord[0].CustName); //客户名称
                $("#CustID").attr("title", data.ord[0].CustID); //客户编号
                $("#CustTel").val(data.ord[0].CustTel); //客户电话
                $("#PayTypeUC_ddlCodeType").val(data.ord[0].PayType); //结算方式
                $("#MoneyTypeUC_ddlCodeType").val(data.ord[0].MoneyType); //支付方式
                $("#CurrencyType").attr("title", data.ord[0].CurrencyType); //币种
                $("#CurrencyType").val(data.ord[0].CurrencyName);
                $("#Rate").val(data.ord[0].Rate); //汇率
                $("#UserSeller").val(data.ord[0].EmployeeName)//业务员名称
                $("#hiddSeller").val(data.ord[0].Seller)//业务员名称
              //  $("#Discount").val(FormatAfterDotNumber(parseFloat(data.ord[0].Discount), 2));      //整单折扣（%） 
                $("#DeptId").val(data.ord[0].DeptName); //部门名称
                $("#hiddDeptID").val(data.ord[0].SellDeptId); //部门编号
                
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该单据信息已不存在,请确认");
            }
            //退货单明细信息
            if (data.ordList != null) {
                fnSetDetailData(data.ordList);
            }
        }
    });
    closeSellSenddiv(); //关闭客户选择控件  
}

//验证提成率是否正确
function fnCheckPer(obj) {
    if (obj.value.length > 0) {
        Number_round(obj, precisionLength);
        if (!IsNumeric(obj.value, 12, precisionLength)) {
            obj.value = FormatAfterDotNumber(100,precisionLength);
            //showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "代销提成率输入错误,请确认！");
        }
    }
    else {
        obj.value = FormatAfterDotNumber(100,precisionLength);
        // showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "代销提成率不能为空！");
    }
    fnTotalInfo();
}
//判断输入数量或手续费是否正确
function fnCheck(obj, text) {
    if (obj.value.length > 0) {
        Number_round(obj, precisionLength);
        if (!IsNumeric(obj.value, 14, precisionLength)) {
            obj.value = '';
            if (text == '手续费合计') {
                //showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", text + "输入错误,请确认！");
            }
        }
    }

    fnTotalInfo();
}
//计算各种合计信息
function fnTotalInfo() {
    var CountTotal = 0; //结算数量合计
    var TotalFee = 0; //结算代销金额合计
    var PushMoneyPercent = $("#PushMoneyPercent").val(); //代销提成率
    var PushMoney = 0; //代销提成额
    var HandFeeTotal = $("#HandFeeTotal").val(); //手续费合计
    if ($.trim(HandFeeTotal) == '') {
        HandFeeTotal = 0;
    }
    var SttlTotal = 0; //应结算金额
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;
            try{
                if($("#txtIsMoreUnit").val() == "1")
                {
                    //根据单位和数量计算基本数量。参数（单位下列表ID，数量文本框ID，基本数量文本框ID，单价ID，基本单价ID）
                    //此处“单价ID”和“基本单价ID”均传空，此处仅作基本数量的计算，单价不作计算，单价只在“单位”改变时重新计算。
                    CalCulateNum("selectUnitID"+rowid ,"SttlNumber"+rowid ,"BaseCountD"+rowid,'','', precisionLength );
                }
            }catch(e){}
            
            var SttlNumber = $("#SttlNumber" + rowid).val(); //本次结算数量
            if ($.trim(SttlNumber) == '') {
                SttlNumber = 0;
            }
            
            var UnitPrice = $("#UnitPrice" + rowid).val(); //单价
            var totalPrice = FormatAfterDotNumber(parseFloat(SttlNumber) * parseFloat(UnitPrice), precisionLength); //本次结算代销金额
            $("#totalPrice" + rowid).val(totalPrice); //本次结算代销金额

            TotalFee += parseFloat(totalPrice);
            CountTotal += parseFloat(SttlNumber);
        }
    }
    $("#CountTotal").val(FormatAfterDotNumber(CountTotal, precisionLength)); //结算数量合计
    $("#TotalFee").val(FormatAfterDotNumber(TotalFee, precisionLength)); //结算代销金额合计
    $("#PushMoney").val(FormatAfterDotNumber((parseFloat(TotalFee) * parseFloat(PushMoneyPercent)) / 100, precisionLength)); //代销提成额
    $("#SttlTotal").val(FormatAfterDotNumber((parseFloat($("#PushMoney").val()) + parseFloat(HandFeeTotal)), precisionLength)); //应结算金额

}



//删除明细一行
function fnDelOneRow() {
    var dg_Log = findObj("dg_Log", document);
    var rowscount = dg_Log.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除

        if ($("#chk" + i).attr("checked")) {
            dg_Log.rows[i].style.display = "none";
        }
    }
    $("#checkall").removeAttr("checked");
    fnTotalInfo();
}

function fnSelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

//删除明细中所有数据
function fnDelRow() {
    var dg_Log = findObj("dg_Log", document);
    var rowscount = dg_Log.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除
        dg_Log.deleteRow(i);
    }
    var txtTRLastIndex = findObj("txtTRLastIndex", document); //重置最后行号为
    txtTRLastIndex.value = "0";
    $("#checkall").removeAttr("checked");
    fnTotalInfo();
}


//选择退货单明细
function fnSetDetailData(data) {
    $.each(data, function(i, item) {
        var sendProductCount=0;
        //计量单位开启
        if ($("#txtIsMoreUnit").val() == "1") 
        {
           sendProductCount=item.UsedUnitCount;
        }
        else
        {
            sendProductCount=item.ProductCount;
        }
        if ((parseFloat(sendProductCount) - parseFloat(item.SttlCount)) > 0) {
            //读取最后一行的行号，存放在txtTRLastIndex文本框中 
            var txtTRLastIndex = findObj("txtTRLastIndex", document);
            var rowID = parseInt(txtTRLastIndex.value) + 1;
            var signFrame = findObj("dg_Log", document);
            var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
            newTR.id = rowID;
            var m=0;

            var newNameXH = newTR.insertCell(m); //添加列:序号
            newNameXH.className = "cell";
            newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox' /> ";
            m++;
            
            var newFitNotd = newTR.insertCell(m); //添加列:物品编号
            newFitNotd.className = "cell";
            newFitNotd.innerHTML = "<input id='ProductID" + rowID + "' disabled='disabled' style=' width:90%;' type='text'  class='tdinput' />"; //添加列内容
            $("#ProductID" + rowID).val(item.ProdNo);
            $("#ProductID" + rowID).attr("title", item.ProductID);
            m++;
           
            var newFitNametd = newTR.insertCell(m); //添加列:物品名称
            newFitNametd.className = "cell";
            newFitNametd.innerHTML = "<input id='ProductName" + rowID + "'  disabled='disabled'  style=' width:90%; ' type='text'  class='tdinput' />"; //添加列内容
            $("#ProductName" + rowID).val(item.ProductName);
            m++;
           
            var newFitDesctd = newTR.insertCell(m); //添加列:规格
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input  id='Specification" + rowID + "'  style=' width:90%;' disabled='disabled' type='text'  class='tdinput' />"; //添加列内容
            $("#Specification" + rowID).val(item.Specification);
            m++;
            
            //计量单位开启
            if ($("#txtIsMoreUnit").val() == "1") 
            {
                var newFitDesctd = newTR.insertCell(m); //添加列:基本单位(从物品档案带出不给修改)
                newFitDesctd.className = "cell";
                newFitDesctd.innerHTML = "<input id='BaseUnitID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
                $("#BaseUnitID" + rowID).val(item.CodeName);
                $("#BaseUnitID" + rowID).attr("title", item.UnitID);
                m++;
                
                var newFitDesctd = newTR.insertCell(m); //添加列:基本数量(从物品档案带出不给修改)
                newFitDesctd.className = "cell";
                newFitDesctd.innerHTML = "<input id='BaseCountD" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
                $("#BaseCountD" + rowID).val(FormatAfterDotNumber(item.ProductCount, precisionLength));
                m++;

                var newFitDesctd = newTR.insertCell(m); //添加列:单位
                newFitDesctd.className = "cell";
                newFitDesctd.id="UnitID"+rowID;
                newFitDesctd.innerHTML = ""; //添加列内容
                m++;
            }
            else
            {
                var newFitDesctd = newTR.insertCell(m); //添加列:单位
                newFitDesctd.className = "cell";
                newFitDesctd.innerHTML = "<input id='UnitID" + rowID + "'   disabled='disabled'   type='text' class='tdinput' style=' width:80%;'/><input type=\"hidden\" value='1' id='hiddExRate" + rowID + "'/>"; //添加列内容
                $("#UnitID" + rowID).val(item.CodeName);
                $("#UnitID" + rowID).attr("title", item.UnitID);
                m++;
            }

            var newFitDesctd = newTR.insertCell(m); //添加列:代销数量
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input  id='ProductCount" + rowID + "' disabled='disabled' type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
            m++;
           
            var newFitDesctd = newTR.insertCell(m); //添加列: 已结算数量
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='SttlCount" + rowID + "' disabled='disabled'   type='text'   class='tdinput'  style=' width:90%;'/>"; //添加列内容
            m++;
           
            var newFitDesctd = newTR.insertCell(m); //添加列:未结算数量
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='noCount" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
            m++;
           
            var newFitDesctd = newTR.insertCell(m); //添加列: 本次结算数量
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='SttlNumber" + rowID + "' onblur=\"fnCheck(this,'本次结算数量')\"   maxlength='8' type='text' class='tdinput' style=' width:90%;'/> "; //添加列内容
            m++;
           
            //多计量单位
            if($("#txtIsMoreUnit").val()=="1")
            {
                $("#ProductCount" + rowID).val(FormatAfterDotNumber(item.UsedUnitCount, precisionLength));//代销数量
                $("#SttlCount" + rowID).val(FormatAfterDotNumber(item.SttlCount, precisionLength));//已结算数量
                $("#noCount" + rowID).val(FormatAfterDotNumber((parseFloat(item.UsedUnitCount) - parseFloat(item.SttlCount)), precisionLength));//未结算数量
                $("#SttlNumber" + rowID).val(FormatAfterDotNumber((parseFloat(item.UsedUnitCount) - parseFloat(item.SttlCount)), precisionLength));//本次结算数量
            }
            else
            {
                $("#ProductCount" + rowID).val(FormatAfterDotNumber(item.ProductCount, precisionLength));//代销数量
                $("#SttlCount" + rowID).val(FormatAfterDotNumber(item.SttlCount, precisionLength));//已结算数量
                $("#noCount" + rowID).val(FormatAfterDotNumber((parseFloat(item.ProductCount) - parseFloat(item.SttlCount)), precisionLength));//未结算数量
                $("#SttlNumber" + rowID).val(FormatAfterDotNumber((parseFloat(item.ProductCount) - parseFloat(item.SttlCount)), precisionLength));//本次结算数量
            }
            
            var newFitDesctd = newTR.insertCell(m); //代销单价
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='UnitPrice" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
            $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength));
            m++;
           
            var newFitDesctd = newTR.insertCell(m); //添加列:本次结算代销金额
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='totalPrice" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/> "; //添加列内容
            $("#totalPrice" + rowID).val(FormatAfterDotNumber((parseFloat($("#SttlNumber" + rowID).val()) * parseFloat(item.TaxPrice)), precisionLength));
            m++;
           
            var newFitDesctd = newTR.insertCell(m); //添加列: 备注
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='Remark" + rowID + "' maxlength='100' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
            m++;
           
            var newFitDesctd = newTR.insertCell(m); //添加列: 源单类型
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='FromType" + rowID + "' value='销售单据' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
            $("#FromType" + rowID).attr("title", "1");
            m++;
           
            var newFitDesctd = newTR.insertCell(m); //添加列: 源单编号
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='FromBillID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
            $("#FromBillID" + rowID).attr("title", item.ID);
            $("#FromBillID" + rowID).val(item.SendNo);
            m++;
           
            var newFitDesctd = newTR.insertCell(m); //添加列: 源单行号
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='FromLineNo" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
            $("#FromLineNo" + rowID).val(item.SortNo);
            m++;
            
            //多计量单位
           if($("#txtIsMoreUnit").val() == "1")
            {   
                //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
                //GetUnitGroupSelect(ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit(this)', "UnitID" + rowIndex, SaleUnitID);
                GetUnitGroupSelectEx(item.ProductID, 'SaleUnit', 'selectUnitID' + rowID, 'ChangeUnit('+rowID+')', "UnitID" + rowID, item.UsedUnitID,'fnTotalInfo(),fnDisabledOrNot('+rowID+')');
            }
            else
            {
                fnTotalInfo();
            }
            txtTRLastIndex.value = rowID; //将行号推进下一行
        }
    });
    
    closeSendDetaildiv();
    //fnTotalInfo();
}
//单位改变时重新计算
function ChangeUnit(rowid)
{
    //根据单位和数量计算基本数量。参数（单位下列表ID，数量文本框ID，基本数量文本框ID，单价，基本单价）
    CalCulateNum("selectUnitID"+rowid ,"SttlNumber"+rowid ,"BaseCountD"+rowid,"UnitPrice"+rowid,"hiddBaseUnitPrice"+rowid , precisionLength);
    $("#hiddUnitPrice" + rowid).val($("#UnitPrice" + rowid).val());   //把带回的单价放到隐藏域中
    fnTotalInfo();
}

//源单类型为发货通知单时单位不给修改
function fnDisabledOrNot(rowID)
{
    $("#selectUnitID"+rowID).attr("disabled", "disabled");
}

//保存数据
function InsertSellOfferData() {
    if (!CheckInput()) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var strfitinfo = getDropValue().join("|");
    var strInfo = fnGetInfo();

    var str = 'insert';
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellChannelSttlAdd.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=' + escape(str),
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
                $("#hiddBillStatus").val('1');
                $("#btn_save").css("display", "none");
                $("#btn_update").css("display", "inline");
                $("#SellChannelSttlUC_txtCode").val(data.no);
                $("#SellChannelSttlUC_txtCode").attr("disabled", "disabled");
                $("#SellChannelSttlUC_ddlCodeRule").css("display", "none");
                $("#SellChannelSttlUC_txtCode").css("width", "95%");

                GetFlowButton_DisplayControl();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存成功！");
                GetFlowButton_DisplayControl();
            }
            hidePopup();
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}


//保存修改后的信息
function UpdateSellOfferData() {
    if (!CheckInput()) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var strfitinfo = getDropValue().join("|");
    var strInfo = fnGetInfo();
    var str = 'update';
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellChannelSttlAdd.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=' + escape(str),
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
                $("#hiddBillStatus").val('1');
                $("#btn_save").css("display", "none");
                $("#btn_update").css("display", "inline");
                $("#SellChannelSttlUC_txtCode").val(data.no);
                $("#SellChannelSttlUC_txtCode").attr("disabled", "disabled");
                $("#SellChannelSttlUC_ddlCodeRule").css("display", "none");
                $("#SellChannelSttlUC_txtCode").css("width", "95%");

                GetFlowButton_DisplayControl();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存成功！");
                GetFlowButton_DisplayControl();
            }
            hidePopup();
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//确认按钮的调用的函数名称，业务逻辑不同模块各自处理
function Fun_ConfirmOperate() {
    if (confirm("是否确认单据？")) {
        //var CodeType = $("#SellChannelSttlUC_ddlCodeRule").val(); //单据编号产生的规则
        //var SttlNo = $("#SellChannelSttlUC_txtCode").val();        //退货单编号
        var ID=$("#hiddOrderID").val();
        
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/SellChannelSttlAdd.ashx",
            data: fnGetInfo()+'&ID='+escape(ID) + '&action=confirm',
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
                    $("#Confirmor").val($("#Creator").val());
                    $("#ConfirmDate").val($("#CreateDate").val());

                    $("#BillStatus").html("执行");
                    $("#hiddBillStatus").val('2');
                    $("#imgUnSave").css("display", "inline");
                    $("#btn_update").css("display", "none");

                    $("#imgUnDel").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#btnUnFromBill").css("display", "inline");
                    $("#btnFromBill").css("display", "none");
                    fnGetDetail(data.no);
                    GetFlowButton_DisplayControl();

                }
                else {
                    hidePopup();
                }
                hidePopup();
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

//获取报价单明细信息
function fnGetDetail(orderNo) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellChannelSttlList.ashx",
        data: "action=detail&orderNo=" + escape(orderNo),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            fnDelRow();
            if (data.data != null) {
                $.each(data.data, function(i, item) {
                    //读取最后一行的行号，存放在txtTRLastIndex文本框中 
                    var txtTRLastIndex = findObj("txtTRLastIndex", document);
                    var rowID = parseInt(txtTRLastIndex.value) + 1;
                    var signFrame = findObj("dg_Log", document);
                    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
                    newTR.id = rowID;
                    var m=0;

                    var newNameXH = newTR.insertCell(m); //添加列:序号
                    newNameXH.className = "cell";
                    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox' /> ";
                    m++;
                    
                    var newFitNotd = newTR.insertCell(m); //添加列:物品编号
                    newFitNotd.className = "cell";
                    newFitNotd.innerHTML = "<input id='ProductID" + rowID + "' disabled='disabled' style=' width:90%;' type='text'  class='tdinput' />"; //添加列内容
                    $("#ProductID" + rowID).val(item.ProdNo);
                    $("#ProductID" + rowID).attr("title", item.ProductID);
                    m++;
                    
                    var newFitNametd = newTR.insertCell(m); //添加列:物品名称
                    newFitNametd.className = "cell";
                    newFitNametd.innerHTML = "<input id='ProductName" + rowID + "'  disabled='disabled'  style=' width:90%; ' type='text'  class='tdinput' />"; //添加列内容
                    $("#ProductName" + rowID).val(item.ProductName);
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:规格
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input  id='Specification" + rowID + "'  style=' width:90%;'disabled='disabled' type='text'  class='tdinput' />"; //添加列内容
                    $("#Specification" + rowID).val(item.Specification);
                    m++;
                    
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") 
                    {
                        var newFitDesctd = newTR.insertCell(m); //添加列:基本单位(从物品档案带出不给修改)
                        newFitDesctd.className = "cell";
                        newFitDesctd.innerHTML = "<input id='BaseUnitID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
                        $("#BaseUnitID" + rowID).val(item.CodeName);
                        $("#BaseUnitID" + rowID).attr("title", item.UnitID);
                        m++;
                        
                        var newFitDesctd = newTR.insertCell(m); //添加列:基本数量(从物品档案带出不给修改)
                        newFitDesctd.className = "cell";
                        newFitDesctd.innerHTML = "<input id='BaseCountD" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
                        $("#BaseCountD" + rowID).val(FormatAfterDotNumber(item.ProductCount, precisionLength));
                        m++;

                        var newFitDesctd = newTR.insertCell(m); //添加列:单位
                        newFitDesctd.className = "cell";
                        newFitDesctd.id="UnitID"+rowID;
                        newFitDesctd.innerHTML = ""; //添加列内容
                        m++;
                    }
                    else
                    {
                        var newFitDesctd = newTR.insertCell(m); //添加列:单位
                        newFitDesctd.className = "cell";
                        newFitDesctd.innerHTML = "<input id='UnitID" + rowID + "'   disabled='disabled'   type='text' class='tdinput' style=' width:80%;'/><input type=\"hidden\" value='1' id='hiddExRate" + rowID + "'/>"; //添加列内容
                        $("#UnitID" + rowID).val(item.CodeName);
                        $("#UnitID" + rowID).attr("title", item.UnitID);
                        m++;
                    }
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:代销数量
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input  id='ProductCount" + rowID + "' disabled='disabled' type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 已结算数量
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='SttlCount" + rowID + "' disabled='disabled'   type='text'   class='tdinput'  style=' width:90%;'/>"; //添加列内容
                    m++;

                    var newFitDesctd = newTR.insertCell(m); //添加列:未结算数量
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='noCount" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 本次结算数量
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='SttlNumber" + rowID + "' onblur=\"fnCheck(this,'本次结算数量')\"   maxlength='8' type='text' class='tdinput' style=' width:90%;'/> "; //添加列内容
                    m++;
                    
                    //多计量单位
                    if($("#txtIsMoreUnit").val()=="1")
                    {
                        $("#ProductCount" + rowID).val(FormatAfterDotNumber(item.UsedUnitCount, precisionLength));//代销数量
                        $("#SttlCount" + rowID).val(FormatAfterDotNumber(item.SttlCount, precisionLength));//已结算数量
                        $("#noCount" + rowID).val(FormatAfterDotNumber((parseFloat(item.UsedUnitCount) - parseFloat(item.SttlCount)), precisionLength));//未结算数量
                        $("#SttlNumber" + rowID).val(FormatAfterDotNumber((parseFloat(item.UsedUnitCount) - parseFloat(item.SttlCount)), precisionLength));//本次结算数量
                    }
                    else
                    {
                        $("#ProductCount" + rowID).val(FormatAfterDotNumber(item.ProductCount, precisionLength));//代销数量
                        $("#SttlCount" + rowID).val(FormatAfterDotNumber(item.SttlCount, precisionLength));//已结算数量
                        $("#noCount" + rowID).val(FormatAfterDotNumber((parseFloat(item.ProductCount) - parseFloat(item.SttlCount)), precisionLength));//未结算数量
                        $("#SttlNumber" + rowID).val(FormatAfterDotNumber((parseFloat(item.ProductCount) - parseFloat(item.SttlCount)), precisionLength));//本次结算数量
                    }
                    var newFitDesctd = newTR.insertCell(m); //代销单价
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='UnitPrice" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
                    $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength));
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:本次结算代销金额
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='totalPrice" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/> "; //添加列内容
                    $("#totalPrice" + rowID).val(FormatAfterDotNumber(item.totalPrice, precisionLength));
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 备注
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='Remark" + rowID + "' maxlength='100' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
                    $("#Remark" + rowID).val(item.Remark);
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 源单类型
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='FromType" + rowID + "' value='发货单' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
                    $("#FromType" + rowID).attr("title", "1");
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 源单编号
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='FromBillID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
                    $("#FromBillID" + rowID).attr("title", item.FromBillID);
                    $("#FromBillID" + rowID).val(item.SendNo);
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 源单行号
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='FromLineNo" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
                    $("#FromLineNo" + rowID).val(item.FromLineNo);
                    m++;
                   
                   //多计量单位
                   if($("#txtIsMoreUnit").val() == "1")
                    {   
                        //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
                        //GetUnitGroupSelect(ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit(this)', "UnitID" + rowIndex, SaleUnitID);
                        GetUnitGroupSelectEx(item.ProductID, 'SaleUnit', 'selectUnitID' + rowID, 'ChangeUnit('+rowID+')', "UnitID" + rowID, item.UsedUnitID,'fnTotalInfo(),fnDisabledOrNot('+rowID+')');
                    }
                    else
                    {
                        fnTotalInfo();
                    }
                    txtTRLastIndex.value = rowID; //将行号推进下一行
                });
                //fnTotalInfo();
            }
        }
    });
}

//结单按钮调用的函数名称，业务逻辑不同模块各自处理
//isFlag=true结单操作，isFlag=false取消结单
function Fun_CompleteOperate(isFlag) {
    if (confirm("是否取消确认？")) {
        var CodeType = $("#SellChannelSttlUC_ddlCodeRule").val(); //单据编号产生的规则
        var SttlNo = $("#SellChannelSttlUC_txtCode").val();        //退货单编号
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
            url: "../../../Handler/Office/SellManager/SellChannelSttlAdd.ashx",
            data: 'CodeType=' + escape(CodeType) + '&SttlNo=' + escape(SttlNo) + '&action=' + acction,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            //complete :function(){hidePopup();},
            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
            success: function(data) {
                if (data.sta == 1) {
                    if (isFlag) {
                        $("#BillStatus").html("手工结单");
                        $("#Closer").val($("#Creator").val());
                        $("#CloseDate").val($("#CreateDate").val());
                        $("#btn_update").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#hiddBillStatus").val('4');
                        $("#btnFromBill").css("display", "none");
                        $("#btnUnFromBill").css("display", "inline");
                    }
                    else {
                        $("#BillStatus").html("执行");
                        $("#Closer").val('');
                        $("#CloseDate").val('');
                        $("#btn_update").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#hiddBillStatus").val('2');
                        $("#btnFromBill").css("display", "none");
                        $("#btnUnFromBill").css("display", "inline");

                    }
                    $("#hiddOrderID").val(data.id);

                    GetFlowButton_DisplayControl();
                }
                else {
                    hidePopup();

                }
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
            }
        });
    }
}
//取消确认
function Fun_UnConfirmOperate() {
    //var CodeType = $("#SellChannelSttlUC_ddlCodeRule").val(); //发货单编号产生的规则
    //var SendNo = $("#SellChannelSttlUC_txtCode").val();        //退货单编号
    var acction = 'UnConfirm';
    var ID=$("#hiddOrderID").val();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellChannelSttlAdd.ashx",
        data: fnGetInfo()+'&ID='+escape(ID) + '&action=' + acction,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {

                $("#BillStatus").html("制单");
                $("#Closer").val('');
                $("#CloseDate").val('');
                $("#imgUnSave").css("display", "none");
                $("#btn_update").css("display", "inline");
                $("#imgUnDel").css("display", "none");
                $("#imgDel").css("display", "inline");
                $("#Confirmor").val('');
                $("#ConfirmDate").val('');

                $("#hiddBillStatus").val('1');
                $("#btnFromBill").css("display", "inline");
                $("#btnUnFromBill").css("display", "none");

                $("#hiddOrderID").val(data.id);
                fnGetDetail(data.no);
                GetFlowButton_DisplayControl();
            }
            else {
                hidePopup();

            }
            hidePopup();
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });

}
//获取主表信息
function fnGetInfo() {
    var strInfo = '';
    var CodeType = $("#SellChannelSttlUC_ddlCodeRule").val(); //单据编号产生的规则
    var SttlNo = $("#SellChannelSttlUC_txtCode").val();        //退货单编号

    var CustID = $("#CustID").attr("title");        //客户ID（关联客户信息表）
    var Seller = $("#hiddSeller").val();        //业务员(对应员工表ID)
    var SellDeptId = $("#hiddDeptID").val();      //部门(对部门表ID)                                                           
    var FromType = '1';      //来源单据类型（0无来源，1发货通知单）                                                 
    var FromBillID = $("#FromBillID").attr("title");    //来源单据ID（发货通知单）                                                             
    var CustTel = $("#CustTel").val();       //客户联系电话
    var PayType = $("#PayTypeUC_ddlCodeType").val();       //结算方式ID
    var MoneyType = $("#MoneyTypeUC_ddlCodeType").val();       //结算方式ID
    var CurrencyType = $("#CurrencyType").attr("title");  //币种ID                                                                               
    var Rate = $("#Rate").val();          //汇率
    var BillStatus = '1';    //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
    var Remark = $("#Remark").val();        //备注
    var Title = $.trim($("#Title").val());//主题
    var SttlDate = $("#SttlDate").val(); //结算日期
    var CountTotal = $("#CountTotal").val();      //本次结算代销数量合计   
    var TotalFee = $("#TotalFee").val();        //本次结算代销金额合计   
    var PushMoneyPercent = $("#PushMoneyPercent").val(); //代销提成率             
    var PushMoney = $("#PushMoney").val();       //代销提成额             
    var HandFeeTotal = $("#HandFeeTotal").val();    //代销手续费             
    var SttlTotal = $("#SttlTotal").val();       //应结金额合计



    strInfo = 'CodeType=' + escape(CodeType) + '&SttlNo=' + escape(SttlNo) + '&CustID=' + escape(CustID)
             + '&Seller=' + escape(Seller) + '&SellDeptId=' + escape(SellDeptId) + '&FromType=' + escape(FromType)
             + '&FromBillID=' + escape(FromBillID) + '&CustTel=' + escape(CustTel) + '&PayType=' + escape(PayType)
             + '&CurrencyType=' + escape(CurrencyType) + '&Rate=' + escape(Rate) + '&BillStatus=' + escape(BillStatus)
             + '&Remark=' + escape(Remark) + '&Title=' + escape(Title) + '&SttlDate=' + escape(SttlDate) + '&MoneyType=' + escape(MoneyType)
             + '&CountTotal=' + escape(CountTotal) + '&TotalFee=' + escape(TotalFee) + '&PushMoneyPercent=' + escape(PushMoneyPercent)
             + '&PushMoney=' + escape(PushMoney) + '&HandFeeTotal=' + escape(HandFeeTotal) + '&SttlTotal=' + escape(SttlTotal)+GetExtAttrValue();

    return strInfo;
}

////获取明细数据
function getDropValue() {
    var SendOrderFit_Item = new Array();
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (var i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;

            var SortNo = j;      //序号（行号）
            var ProductID = $("#ProductID" + rowid).attr("title"); //物品ID（对应物品表ID）
            //var UnitID = $("#UnitID" + rowid).attr("title");
            var ProductCount = $("#ProductCount" + rowid).val();
            //var SttlNumber = $("#SttlNumber" + rowid).val();
            var UnitPrice = $("#UnitPrice" + rowid).val();
            var totalPrice = $("#totalPrice" + rowid).val();
            var Remark = $("#Remark" + rowid).val();
            var FromType = '1';
            var FromBillID = $("#FromBillID" + rowid).attr("title");  //源单ID   
            var FromLineNo = $("#FromLineNo" + rowid).val();
            
            var UnitID = $("#UnitID" + rowid).attr("title");      //基本单位ID（对应计量单位ID）
            var SttlNumber = $("#SttlNumber" + rowid).val(); //基本数量 
            var UnitPrice = $("#UnitPrice" + rowid).val();   //单价（基本单价）
            var UsedUnitID=$("#UnitID" + rowid).attr("title");//单位
            var UsedUnitCount=$("#SttlNumber" + rowid).val();//数量
            var UsedPrice=$("#UnitPrice" + rowid).val();//单价（对应于单位的单价，也就是明细中显示的实际单价）
            var ExRate=1;//换算比率
            
            if($("#txtIsMoreUnit").val() == "1")
            {
                UnitID=$("#BaseUnitID" + rowid).attr("title");
                SttlNumber=$("#BaseCountD" + rowid).val();
                UnitPrice = $("#hiddBaseUnitPrice" + rowid).val(); 
                
//                UsedUnitID=$("#UnitID" + rowid).attr("title");
//                ExRate=$("#hiddExRate"+rowid).val();
                var UnitRate=document.getElementById("selectUnitID"+rowid).value;
                var arrUnitRate=UnitRate.split('|');
                UsedUnitID=arrUnitRate[0];
                ExRate=arrUnitRate[1];
                
            }

            SendOrderFit_Item[j] = [[SortNo], [ProductID], [UnitID], [ProductCount], [SttlNumber], [UnitPrice], [totalPrice],
                                    [Remark], [FromType], [FromBillID], [FromLineNo],[UsedUnitID],[UsedUnitCount],[UsedPrice],[ExRate]];
            arrlength++;
        }
    }
    return SendOrderFit_Item;
}


//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var CodeType = $.trim($("#SellChannelSttlUC_ddlCodeRule").val()); //单据编号产生的规则
    var SttlNo = $.trim($("#SellChannelSttlUC_txtCode").val());        //退货单编号

    var HandFeeTotal = $.trim($("#HandFeeTotal").val());    //代销手续费 
    var FromBillID = $.trim($("#FromBillID").val());    //来源单据ID（发货通知单）
   // var Title = $.trim($("#Title").val()); //主题

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }


    if (CodeType == "") {
        if (SttlNo == "") {
            isFlag = false;
            fieldText = fieldText + "委托代销单编号|";
            msgText = msgText + "请输入委托代销单编号|";
        }
        else {
            if (isnumberorLetters(SttlNo)) {
                isFlag = false;
                fieldText = fieldText + "委托代销单编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
    }

//    if (Title.length == 0) {
//        isFlag = false;
//        fieldText = fieldText + "主题|";
//        msgText = msgText + "请输入主题|"
//    }
    if (FromBillID == "") {
        isFlag = false;
        fieldText = fieldText + "源单编号|";
        msgText = msgText + "请选择源单|";
    }
    

    //验证明细信息
    var signFrame = findObj("dg_Log", document);
    var iCount = 0; //明细中数据数目
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].id;
            iCount = iCount + 1;
            var noCount = $("#noCount" + rowid).val();
            var SttlNumber = $("#SttlNumber" + rowid).val();


            if (SttlNumber == "") {
                isFlag = false;
                fieldText = fieldText + "委托代销单明细（行号：" + i + "）|";
                msgText = msgText + "请输入本次结算数量|";
            }
            else {
                if (!IsNumeric(SttlNumber, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "委托代销单明细（行号：" + i + "）|";
                    msgText = msgText + "本次结算数量只能为数字|";
                }
                else {
                    if (parseFloat(SttlNumber) > parseFloat(noCount)) {
                        isFlag = false;
                        fieldText = fieldText + "退货委托代销单明细单明细（行号：" + i + "）|";
                        msgText = msgText + "本次结算数量数量不能大于未结算数量|";
                    }
                }

            }
        }
    }

    if (iCount == 0) {
        isFlag = false;
        fieldText = fieldText + "委托代销单明细|";
        msgText = msgText + "委托代销单明细不能为空！|";
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


//提交审批成功和审批成功后调用一个函数0:提交审批成功  1:审批成功
function Fun_FlowApply_Operate_Succeed(values) {
    if (values != 2 && values != 3) {

        $("#btn_update").css("display", "none");
        $("#imgUnSave").css("display", "inline");

        $("#imgDel").css("display", "none");
        $("#btnFromBill").css("display", "none");
        $("#btnUnFromBill").css("display", "inline");
        $("#imgUnDel").css("display", "inline");
    }
    else {
        $("#btn_update").css("display", "inline");
        $("#imgUnSave").css("display", "none");

        $("#imgDel").css("display", "inline");
        $("#imgUnDel").css("display", "none");

        $("#btnFromBill").css("display", "inline");
        $("#btnUnFromBill").css("display", "none");
    }
}

//打印功能
function fnPrintOrder() {
    var OrderNo = $.trim($("#SellChannelSttlUC_txtCode").val());
    if (OrderNo == '保存时自动生成' || OrderNo == '') {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存单据再打印！");
        return;
    }
    window.open('../../../Pages/PrinttingModel/SellManager/PrintSellChannelSttl.aspx?no=' + OrderNo);
}

//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
//    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
//    
//    var arrKey = strKey.split('|');
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
