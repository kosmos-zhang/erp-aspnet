
$(document).ready(function() {
    fnGetExtAttr();
    var pakeageUC = document.getElementById("PackageUC_ddlCodeType");
    pakeageUC.style.display = "none";
    $("#FeeType_ddlCodeType").css("display", "none");

    $("#sellTypeUC_ddlCodeType").css("width", "120px");
    $("#PayTypeUC_ddlCodeType").css("width", "120px");
    $("#MoneyTypeUC_ddlCodeType").css("width", "120px");

    $("#CarryType_ddlCodeType").css("width", "120px");
    $("#TakeType_ddlCodeType").css("width", "120px");
    $("#TransPayType_ddlCodeType").css("width", "120px");

    $("#SendOrderUC_ddlCodeRule").css("width", "80px");
    $("#SendOrderUC_txtCode").css("width", "80px");

    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['islist']; //是否从列表页面进入
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031601', 'ModuleID=2031602');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (flag != null) {
            $("#ibtnBack").css("display", "inline");

        }
    }
    GetExtAttr("officedba.SellSend",null);
    GetFlowButton_DisplayControl();
});

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


//获取查询参数
function GetRequestToHidden() {
    var url = location.search;
    return url;
}

//返回按钮
function fnBack() {
    var URLParams = document.getElementById("HiddenURLParams").value;
    window.location.href = 'SellSendList.aspx' + URLParams;
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
//选择发货单明细信息
function fnSelectOrderList() {
   
        if ($("#CustID").val() == '') {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请选择订单！");
        }
        else {
            var custID = $("#CustID").attr("title");
            var FromBillID = $("#FromBillID").attr("title"); //来源单据编号
            var CurrencyType = $("#CurrencyType").attr("title"); //币种ID
            var Rate = $("#Rate").val(); //币种ID
            popOrderDetailObj.ShowList(custID, CurrencyType, Rate, FromBillID);
        }
  

}

//选择发货单来源时
function fnFromTypeChange(obj) {
    
   
        fnDelRow();

        //清除已填写数据
        $("#FromBillID").val(''); //发货单编号
        $("#FromBillID").removeAttr("title"); //发货单ID
        $("#CustID").val(''); //客户名称
        $("#CustID").removeAttr("title"); //客户编号
        $("#BusiType").attr("selectedIndex", 0); //业务类型
        $("#PayTypeUC_ddlCodeType").attr("selectedIndex", 0); //结算方式
        $("#sellTypeUC_ddlCodeType").attr("selectedIndex", 0); //销售类别
        $("#CurrencyType").val(''); //币种
        $("#CurrencyType").removeAttr("title"); //币种
        $("#Rate").val(FormatAfterDotNumber(0, 4)); //汇率           
        $("#UserSeller").val(''); //业务员名称
        $("#hiddSeller").val(''); //业务员编号
        $("#DeptId").val(''); //部门名称
        $("#hiddDeptID").val(''); //部门编号
        $("#MoneyTypeUC_ddlCodeType").attr("selectedIndex", 0); //支付方式
        $("#CarryType_ddlCodeType").attr("selectedIndex", 0); //运送方式
        $("#TakeType_ddlCodeType").attr("selectedIndex", 0); //交货方式

        $("#SellDeptId").val('');

        $("#Discount").val(FormatAfterDotNumber(100, precisionLength));


        $("#isAddTax").attr("disabled", "false");
        $("#NotAddTax").attr("disabled", "false");
        $("#isAddTax").attr("checked", "checked");
        $("#NotAddTax").removeAttr("checked", "checked");
   
}


//清除销售订单
function clearSellOrder() {
    fnFromTypeChange(document.getElementById("FromType"));

    closeSellOrderdiv();
}


//选择源单
function fnSelectOfferInfo() {
    if ($("#FromType").val() == 1) {
        if ($("#FromType").attr("disabled") != true) {
            popSellOrderObj.ShowList('protion');
        }
    }
}

//选择业务员
function fnSelectSeller() {

    alertdiv('UserSeller,hiddSeller');

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
            $("#BusiType").val(data.BusiType); //业务类型
            $("#TakeType_ddlCodeType").val(data.TakeType); //交货方式
            $("#CarryType_ddlCodeType").val(data.CarryType); //运送方式
            $("#PayTypeUC_ddlCodeType").val(data.PayType); //结算方式
            $("#Rate").val(data.ExchangeRate); //汇率
            $("#MoneyTypeUC_ddlCodeType").val(data.MoneyType); //支付方式
            $("#CurrencyType").val(data.CurrencyName); //币种
            $("#CurrencyType").attr("title", data.CurrencyType); //币种
            try {
                fnGetPriceByRate();
            } catch (e) { }
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}

//清除客户信息
function ClearSellModuCustdiv() {
    fnFromTypeChange(document.getElementById("FromType"));
    closeSellModuCustdiv(); //关闭客户选择控件
}


//选择币种
function fnSelectCurrency() {
    if ($("#FromType").val() == 0) {
        popSellCurrObj.ShowList('CurrencyType', 'Rate');
    }
}

////选择订单后带出发货单明细信息
function fnSelectSellOrder(OrderId) {

    fnDelRow();
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
            //发货单基本信息    
            if (data.ord != null) {
                $("#FromBillID").val(data.ord[0].OrderNo); //发货单编号
                $("#FromBillID").attr("title", OrderId); //发货单ID
                $("#CustID").val(data.ord[0].CustName); //客户名称
                $("#CustID").attr("title", data.ord[0].CustID); //客户编号
                $("#PayTypeUC_ddlCodeType").val(data.ord[0].PayType); //结算方式
                $("#CurrencyType").attr("title", data.ord[0].CurrencyType); //币种
                $("#CurrencyType").val(data.ord[0].CurrencyName);
                $("#Rate").val(data.ord[0].Rate); //汇率
                $("#UserSeller").val(data.ord[0].EmployeeName)//业务员名称
                $("#hiddSeller").val(data.ord[0].Seller)//业务员名称
                $("#sellTypeUC_ddlCodeType").val(data.ord[0].SellType); //销售类别
                $("#BusiType").val(data.ord[0].BusiType); //业务类型
                $("#Discount").val(FormatAfterDotNumber(parseFloat(data.ord[0].Discount), precisionLength));      //整单折扣（%）
                $("#MoneyTypeUC_ddlCodeType").val(data.ord[0].MoneyType); //支付方式
                $("#TakeType_ddlCodeType").val(data.ord[0].TakeType); //交货方式
                $("#CarryType_ddlCodeType").val(data.ord[0].CarryType); //运送方式

                $("#DeptId").val(data.ord[0].DeptName); //部门名称
                $("#hiddDeptID").val(data.ord[0].SellDeptId); //部门编号
                if (data.ord[0].isAddTax == '0') {
                    $("#NotAddTax").attr("checked", "checked");
                    $("#isAddTax").removeAttr("checked", "checked");
                }
                else {
                    $("#isAddTax").attr("checked", "checked");
                    $("#NotAddTax").removeAttr("checked", "checked");
                }
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该单据信息已不存在,请确认");
            }
            //源订单明细不为空时填充发货单明细信息
            if (data.ordList != null) {
                fnSetDetailData(data.ordList);
            }
        }
    });
    closeSellOrderdiv(); //关闭客户选择控件  
}

//添加包装要求选择项的值
function fnGetPackage(id) {
    var pakeageUC = document.getElementById("PackageUC_ddlCodeType");
    var obj = document.getElementById(id);
    for (var i = 0; i < pakeageUC.length; i++) {
        var varItem = new Option(pakeageUC.options[i].text, pakeageUC.options[i].value);
        obj.options.add(varItem);
    }
}
//验证整单折扣是否正确
function fnCheckPer(obj) {
    if (obj.value.length > 0) {
        Number_round(obj, precisionLength);
        if (!IsNumeric(obj.value, 12, precisionLength)) {
            obj.value = FormatAfterDotNumber(100, precisionLength);
            //showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "代销提成率输入错误,请确认！");
        }
        else {
            if (100 < parseFloat(obj.value)) {
                obj.value = FormatAfterDotNumber(100, precisionLength);
            }
            if (parseFloat(obj.value) < 0) {
                obj.value = FormatAfterDotNumber(100, precisionLength);
            }
        }
    }
    else {
        obj.value = FormatAfterDotNumber(100, precisionLength);
        // showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "代销提成率不能为空！");
    }
    fnTotalInfo();
}

//计算各种合计信息
function fnTotalInfo() {
    var TotalPrice = 0; //金额合计
    var Tax = 0; //税额合计
    var TotalFee = 0; //含税金额合计
    var Discount = $("#Discount").val(); //整单折扣
    var DiscountTotal = 0; //折扣金额
    var DiscountTotalDetail = 0; //折扣金额
    var RealTotal = 0; //折后含税金额
    var CountTotal = 0; //发货数量合计
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
                    CalCulateNum("selectUnitID"+rowid ,"pCount"+rowid ,"BaseCountD"+rowid,'','', precisionLength );
                }
            }catch(e){}
            
            var pCountDetail = $("#pCount" + rowid).val(); //数量
            if ($.trim(pCountDetail) == '') {
                pCountDetail = 0;
            }
            else {
                Number_round(document.getElementById("pCount" + rowid), precisionLength);
                if (!IsNumeric($("#pCount" + rowid).val(), 14, precisionLength)) {
                    $("#pCount" + rowid).val('');
                    pCountDetail = 0;
                }

            }
            var UnitPriceDetail = $("#UnitPrice" + rowid).val(); //单价
            if ($.trim(UnitPriceDetail) == '') {
                UnitPriceDetail = 0;
            }
            else {
                Number_round(document.getElementById("UnitPrice" + rowid), precisionLength);
                if (!IsNumeric($("#UnitPrice" + rowid).val(), 14, precisionLength)) {
                    $("#UnitPrice" + rowid).val('');
                    UnitPriceDetail = 0;
                }
            }

            var TaxPriceDetail = $("#TaxPrice" + rowid).val(); //含税价
            if ($.trim(TaxPriceDetail) == '') {
                TaxPriceDetail = 0;
            }
            else {
                Number_round(document.getElementById("TaxPrice" + rowid), precisionLength);
                if (!IsNumeric($("#TaxPrice" + rowid).val(), 14, precisionLength)) {
                    $("#TaxPrice" + rowid).val('');
                    TaxPriceDetail = 0;
                }
            }
            var DiscountDetail = $("#Discount" + rowid).val(); //折扣
            if ($.trim(DiscountDetail) == '') {
                DiscountDetail = FormatAfterDotNumber(100, precisionLength);
            }
            else {
                Number_round(document.getElementById("Discount" + rowid), precisionLength);
                if (!IsNumeric($("#Discount" + rowid).val(), 12, precisionLength)) {
                    $("#Discount" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    DiscountDetail = FormatAfterDotNumber(100, precisionLength);
                }
                else {
                    if (100 < parseFloat(DiscountDetail)) {
                        $("#Discount" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    }
                    if (parseFloat(DiscountDetail) < 0) {
                        $("#Discount" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    }
                }
            }
            var TaxRateDetail = $("#TaxRate" + rowid).val(); //税率
            if ($.trim(TaxRateDetail) == '') {
                TaxRateDetail = FormatAfterDotNumber(100, precisionLength);
            }
            else {
                Number_round(document.getElementById("TaxRate" + rowid), precisionLength);
                if (!IsNumeric($("#TaxRate" + rowid).val(), 12, precisionLength)) {
                    $("#TaxRate" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    TaxRateDetail = FormatAfterDotNumber(100, precisionLength);
                }
                else {
                    if (100 < parseFloat(TaxRateDetail)) {
                        $("#TaxRate" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    }
                    if (parseFloat(TaxRateDetail) < 0) {
                        $("#TaxRate" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    }
                }
            }

            var TotalFeeDetail = FormatAfterDotNumber((TaxPriceDetail * pCountDetail * DiscountDetail / 100), precisionLength); //含税金额
            var TotalPriceDetail = FormatAfterDotNumber((UnitPriceDetail * pCountDetail * DiscountDetail / 100), precisionLength); //金额
            var TotalTaxDetail = FormatAfterDotNumber((TotalPriceDetail * TaxRateDetail / 100), precisionLength); //税额
            $("#TotalFee" + rowid).val(FormatAfterDotNumber(TotalFeeDetail, precisionLength)); //含税金额
            $("#TotalPrice" + rowid).val(FormatAfterDotNumber(TotalPriceDetail, precisionLength)); //金额
            $("#TotalTax" + rowid).val(FormatAfterDotNumber(TotalTaxDetail, precisionLength)); //税额
            TotalPrice += parseFloat(TotalPriceDetail);
            Tax += parseFloat(TotalTaxDetail);
            TotalFee += parseFloat(TotalFeeDetail);
            CountTotal += parseFloat(pCountDetail);
            DiscountTotalDetail += ((100 - parseFloat(DiscountDetail)) / 100) * parseFloat(TaxPriceDetail) * parseFloat(pCountDetail);
        }
    }
    $("#TotalPrice").val(FormatAfterDotNumber(TotalPrice, precisionLength));
    $("#Tax").val(FormatAfterDotNumber(Tax, precisionLength));
    $("#TotalFee").val(FormatAfterDotNumber(TotalFee, precisionLength));
    $("#CountTotal").val(FormatAfterDotNumber(CountTotal, precisionLength));
    $("#DiscountTotal").val(FormatAfterDotNumber(((TotalFee * (100 - Discount) / 100) + parseFloat(DiscountTotalDetail)), precisionLength));
    $("#RealTotal").val(FormatAfterDotNumber((TotalFee * parseFloat(Discount) / 100), precisionLength));
}



//新选择物品根据汇率计算单价含税价
function fnGetPriceByRate1(rowid) {
    var Rate = $("#Rate").val();           //汇率
    if(Rate==''|| Rate=="0.0000")
    {
        Rate = FormatAfterDotNumber(1,4);
    }


    var UnitPrice = $("#hiddUnitPrice" + rowid).val();   //单价
    //如果是增值税
    if ($("#isAddTax").attr("checked")) {
        var TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
    }
    //如果不是增值税
    //如果是增值税
    if ($("#NotAddTax").attr("checked")) {
        var TaxPrice = $("#hiddUnitPrice" + rowid).val();    //含税价
    }
    var TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
    UnitPrice = parseFloat(UnitPrice) / parseFloat(Rate);
    TaxPrice = parseFloat(TaxPrice) / parseFloat(Rate);
    $("#UnitPrice" + rowid).val(FormatAfterDotNumber(UnitPrice, precisionLength));   //单价
    $("#TaxPrice" + rowid).val(FormatAfterDotNumber(TaxPrice, precisionLength));    //含税价                                      

    fnTotalInfo();
}

//改变币种时重新获取单价含税价
function fnGetPriceByRate() {
    var Rate = $("#Rate").val();           //汇率
    if(Rate==''|| Rate=="0.0000")
    {
        Rate = FormatAfterDotNumber(1,4);
    }

    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;
            var UnitPrice = $("#hiddUnitPrice" + rowid).val();   //单价
            //如果是增值税
            if ($("#isAddTax").attr("checked")) {
                var TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
            }
            //如果不是增值税
            //如果是增值税
            if ($("#NotAddTax").attr("checked")) {
                var TaxPrice = $("#hiddUnitPrice" + rowid).val();    //含税价
            }
            UnitPrice = parseFloat(UnitPrice) / parseFloat(Rate);
            TaxPrice = parseFloat(TaxPrice) / parseFloat(Rate);

            $("#UnitPrice" + rowid).val(FormatAfterDotNumber(UnitPrice, precisionLength));   //单价
            $("#TaxPrice" + rowid).val(FormatAfterDotNumber(TaxPrice, precisionLength));    //含税价                                      
        }
    }
    fnTotalInfo();
}

//选择是否是增值税时就行的操作
function fnAddTax() {
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = signFrame.rows[i].id;
            //如果是增值税
            if ($("#isAddTax").attr("checked")) {
                $("#TaxRate" + rowIndex).removeAttr("readonly");
                $("#TaxRate" + rowIndex).val($("#hiddTaxRate" + rowIndex).val());

                $("#TaxPrice" + rowIndex).val($("#hiddTaxPrice" + rowIndex).val());
            }
            //如果不是增值税
            //如果是增值税
            if ($("#NotAddTax").attr("checked")) {
                $("#TaxRate" + rowIndex).attr("readonly", "readonly");
                $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(0, precisionLength));

                $("#TaxPrice" + rowIndex).val($("#UnitPrice" + rowIndex).val());
            }
        }
    }
    fnTotalInfo();
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
    fnTotalInfo();
}


//选择发货单明细(填充)
function fnSetDetailData(data) {
    $.each(data, function(i, item) {

        //读取最后一行的行号，存放在txtTRLastIndex文本框中 
        var txtTRLastIndex = findObj("txtTRLastIndex", document);
        var rowID = parseInt(txtTRLastIndex.value) + 1;
        var signFrame = findObj("dg_Log", document);
        var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
        newTR.id = rowID;
        var m=0;

        var newNameXH = newTR.insertCell(m); //添加列:序号
        newNameXH.className = "cell";
        newNameXH.innerHTML = "<input id='chk" + rowID + "'  onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  />";
        m++;
        
        var newFitNotd = newTR.insertCell(m); //添加列:物品编号
        newFitNotd.className = "cell";
        newFitNotd.innerHTML = "<input id='ProductID" + rowID + "' readonly='readonly' style=' width:80%;' type='text'  class='tdinput' /><input type=\"hidden\" value='' id='hiddProductID" + rowID + "'/>"; //添加列内容
        $("#ProductID" + rowID).val(item.ProdNo);
        $("#hiddProductID" + rowID).val(item.ProductID);
        $("#ProductID" + rowID).attr("title", item.ProductID);
        m++;
        
        var newFitNametd = newTR.insertCell(m); //添加列:物品名称
        newFitNametd.className = "cell";
        newFitNametd.innerHTML = "<input id='ProductName" + rowID + "'   disabled='disabled'    style=' width:80%; ' type='text'  class='tdinput' />"; //添加列内容
        $("#ProductName" + rowID).val(item.ProductName);
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:规格
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='Specification" + rowID + "'   disabled='disabled'  style=' width:80%;' type='text'  class='tdinput' />"; //添加列内容
        $("#Specification" + rowID).val(item.Specification);
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:颜色
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='ColorID" + rowID + "'   disabled='disabled'  style=' width:80%;' type='text'  class='tdinput' />"; //添加列内容
        $("#ColorID" + rowID).val(item.ColorName);
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
            $("#BaseCountD" + rowID).val(FormatAfterDotNumber(item.pCount, precisionLength));
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
        
        var newFitDesctd = newTR.insertCell(m); //添加列:订单数量
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='OrderCount" + rowID + "'   disabled='disabled'   type='text'  class='tdinput'  style=' width:80%;'/>"; //添加列内容
        //$("#OrderCount" + rowID).val(FormatAfterDotNumber(item.ProductCount, precisionLength));
        if($("#txtIsMoreUnit").val() == "1")
        {
            $("#OrderCount" + rowID).val(FormatAfterDotNumber(item.UsedUnitCount, precisionLength));
        }
        else
        {
            $("#OrderCount" + rowID).val(FormatAfterDotNumber(item.ProductCount, precisionLength));
        }
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:已执行数量
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='transactCount" + rowID + "'   disabled='disabled'  type='text' value='0.00'  class='tdinput'  style=' width:80%;'/>"; //添加列内容
        $("#transactCount" + rowID).val(FormatAfterDotNumber(item.transactCount, precisionLength));
        m++;
        
        if ($("#txtIsMoreUnit").val() == "1") 
        {
            var newFitDesctd = newTR.insertCell(m); //添加列:本次发货数量
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='pCount" + rowID + "' onblur=\"fnTotalInfo()\"   type='text'  class='tdinput'  style=' width:80%;'/>"; //添加列内容
            $("#pCount" + rowID).val(FormatAfterDotNumber(item.UsedUnitCount, precisionLength));
            m++;
        }
        else
        {
            var newFitDesctd = newTR.insertCell(m); //添加列:基本数量(从物品档案带出不给修改)
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='pCount" + rowID + "' onblur=\"fnTotalInfo()\"  type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
            $("#pCount" + rowID).val(FormatAfterDotNumber(item.pCount, precisionLength));
            m++;
        }
        
        var newFitDesctd = newTR.insertCell(m); //添加列:发货日期
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='SendDate" + rowID + "' readonly='readonly' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('SendDate" + rowID + "')})\"  type='text' class='tdinput'  style=' width:80%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:包装要求
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<select id='Package" + rowID + "'  style=' width:80%;'></select>"; //添加列内容
        fnGetPackage("Package" + rowID); //添加包装要求选择项的值
        $("#Package" + rowID).val(item.Package);
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:单价
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='UnitPrice" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/><input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
        $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UsedPrice, precisionLength));
        $("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardCost, precisionLength));
        $("#hiddBaseUnitPrice"+rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength));//基本单价（对应基本价格）
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:含税价
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TaxPrice" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/> <input type=\"hidden\" value='0.00' id='hiddTaxPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseTaxPrice" + rowID + "'/>"; //添加列内容
        $("#TaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength));
        $("#hiddTaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength));
        $("#hiddBaseTaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:折扣(%)
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='Discount" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
        $("#Discount" + rowID).val(FormatAfterDotNumber(item.Discount, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 税率(%)
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TaxRate" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/> <input type=\"hidden\" value='0.00' id='hiddTaxRate" + rowID + "'/>"; //添加列内容
        $("#TaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength));
        $("#hiddTaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 含税金额
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TotalFee" + rowID + "'  disabled='disabled'   readonly='readonly' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
        $("#TotalFee" + rowID).val(FormatAfterDotNumber(item.TotalFee, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 金额
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TotalPrice" + rowID + "'   disabled='disabled'  readonly='readonly' type='text' class='tdinput' style=' width:80%;'/> "; //添加列内容
        $("#TotalPrice" + rowID).val(FormatAfterDotNumber(item.TotalPrice, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 税额
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TotalTax" + rowID + "'  disabled='disabled'   readonly='readonly' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
        $("#TotalTax" + rowID).val(FormatAfterDotNumber(item.TotalTax, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 备注
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='Remark" + rowID + "' type='text' class='tdinput' maxlength='100'  style=' width:80%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 源单类型
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='FromType" + rowID + "'   disabled='disabled'  value='销售订单'  type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
        $("#FromType" + rowID).attr("title", "1");
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 源单编号
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='FromBillID" + rowID + "'   disabled='disabled'  type='text' class='tdinput' style=' width:95%;'/>"; //添加列内容
        $("#FromBillID" + rowID).attr("title", item.OrderID);
        $("#FromBillID" + rowID).val(item.OrderNo);
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 源单行号
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='FromLineNo" + rowID + "'   disabled='disabled'  type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
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
    });
    closeOrderDetaildiv();
    //fnTotalInfo();
}

//单位改变时重新计算
function ChangeUnit(rowid)
{
    //根据单位和数量计算基本数量。参数（单位下列表ID，数量文本框ID，基本数量文本框ID，单价，基本单价）
    CalCulateNum("selectUnitID"+rowid ,"pCount"+rowid ,"BaseCountD"+rowid,"UnitPrice"+rowid,"hiddBaseUnitPrice"+rowid , precisionLength);
    CalCulateNum("selectUnitID"+rowid ,"pCount"+rowid ,"BaseCountD"+rowid,"TaxPrice"+rowid,"hiddBaseTaxPrice"+rowid , precisionLength);
    $("#hiddTaxPrice" + rowid).val($("#TaxPrice" + rowid).val());   //把带回的单价放到隐藏域中
    $("#hiddUnitPrice" + rowid).val($("#UnitPrice" + rowid).val());   //把带回的单价放到隐藏域中
    fnTotalInfo();
}

//源单类型为销售订单时单位不给修改
function fnDisabledOrNot(rowID)
{
    //document .getElementById("#UnitID"+rowID).disabled=true;
    $("#selectUnitID"+rowID).attr("disabled", "disabled");
}
//当数量超出库存数时允许保存调用的方法
function fnMsgConfim() {
    if (!CheckInput()) {
        return;
    }
    try {
        document.getElementById('mydiv').style.display = 'none';
        closeRotoscopingDiv(false, 'divMsgShadow');
    } catch (e) { }
    var BusType = $("#hiddBusType").val(); //当前操作的业务类型

    switch (BusType) {
        case 'insert': //添加
            InsertSellOfferData();
            break;
        case 'update': //修改
            UpdateSellOfferData();
            break;
        case 'confirm': //确认

            fnConfirmOperate();

            break;
    }

}
//验证当前发货的商品数码是否超出库存数目
function fnCheckProCount(BusType) {
    if (!CheckInput()) {
        return;
    }
    var strfitinfo = getDropValue().join("|");
    $("#hiddBusType").val(BusType); //当前操作的业务类型
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellSendAdd.ashx",
        data: 'CodeType=&SendNo=&strfitinfo=' + escape(strfitinfo) + '&action=checkProCount',
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.data.length != 0) {
                popMsgObj1.Show(data.data, data.Msg);
            }
            else {
                var BusType = $("#hiddBusType").val(); //当前操作的业务类型

                switch (BusType) {
                    case 'insert': //添加
                        InsertSellOfferData();
                        break;
                    case 'update': //修改
                        UpdateSellOfferData();
                        break;
                    case 'confirm': //确认
                        if (confirm("是否确认单据？")) {
                            fnConfirmOperate();
                        }
                        break;
                }
            }
        }
    });
}

//保存数据
function InsertSellOfferData() {


    var strfitinfo = getDropValue().join("|");
    var strInfo = fnGetInfo();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellSendAdd.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=insert'+GetExtAttrValue(),
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
                $("#SendOrderUC_txtCode").val(data.no);
                $("#SendOrderUC_txtCode").attr("disabled", "disabled");

                $("#SendOrderUC_ddlCodeRule").css("display", "none");
                $("#SendOrderUC_txtCode").css("width", "95%");

                
                $("#imgBilling").css("display", "none");
                $("#imgUnBilling").css("display", "inline");
                GetFlowButton_DisplayControl();

            }
            else {
                hidePopup();

            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}



//保存修改后的信息
function UpdateSellOfferData() {

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var strfitinfo = getDropValue().join("|");
    var strInfo = fnGetInfo();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellSendAdd.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=update'+GetExtAttrValue(),
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
                $("#SendOrderUC_txtCode").val(data.no);
                $("#SendOrderUC_txtCode").attr("disabled", "disabled");

                $("#SendOrderUC_ddlCodeRule").css("display", "none");
                $("#SendOrderUC_txtCode").css("width", "95%");
                
                $("#imgBilling").css("display", "none");
                $("#imgUnBilling").css("display", "inline");
                GetFlowButton_DisplayControl();
            }
            else {
                hidePopup();

            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//确认按钮的调用的函数名称，业务逻辑不同模块各自处理
function Fun_ConfirmOperate() {
    fnCheckProCount('confirm');

}

//确认按钮的调用的函数名称，业务逻辑不同模块各自处理
function fnConfirmOperate() {

    //var CodeType = $("#SendOrderUC_ddlCodeRule").val(); //发货单编号产生的规则
    //var SendNo = $("#SendOrderUC_txtCode").val();        //退货单编号
    var ID=$("#hiddOrderID").val();//发货单ID
    
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellSendAdd.ashx",
        data: fnGetInfo() + '&ID=' + escape(ID) + '&action=confirm',
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
                $("#FromType").attr("disabled", "disabled");

                $("#BillStatus").html("执行");
                $("#hiddBillStatus").val('2');
                $("#imgUnSave").css("display", "inline");
                $("#btn_update").css("display", "none");
                $("#imgBilling").css("display", "inline");
                $("#imgUnBilling").css("display", "none");
              
                $("#imgDel").css("display", "none");
                $("#btnUnFromBill").css("display", "inline");
                $("#btnFromBill").css("display", "none");
                if ($("#FromType").val() == 1) {
                    fnGetDetail(data.no);
                }
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

//获取报价单明细信息
function fnGetDetail(orderNo) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellSendList.ashx",
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


                    var txtTRLastIndex = findObj("txtTRLastIndex", document);
                    var rowID = parseInt(txtTRLastIndex.value) + 1;
                    var signFrame = findObj("dg_Log", document);
                    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
                    newTR.id = rowID;
                    var m=0;

                    var newNameXH = newTR.insertCell(m); //添加列:序号
                    newNameXH.className = "cell";
                    newNameXH.innerHTML = "<input id='chk" + rowID + "'  onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  />";
                    m++;
                    
                    var newFitNotd = newTR.insertCell(m); //添加列:物品编号
                    newFitNotd.className = "cell";
                    newFitNotd.innerHTML = "<input id='ProductID" + rowID + "' readonly='readonly' style=' width:80%;' type='text'  class='tdinput' /><input type=\"hidden\" value='' id='hiddProductID" + rowID + "'/>"; //添加列内容
                    m++;

                    var newFitNametd = newTR.insertCell(m); //添加列:物品名称
                    newFitNametd.className = "cell";
                    newFitNametd.innerHTML = "<input id='ProductName" + rowID + "'   disabled='disabled'   style=' width:80%; ' type='text'  class='tdinput' />"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:规格
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input  id='Specification" + rowID + "'   disabled='disabled'  style=' width:80%;' type='text'  class='tdinput' />"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:颜色
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input  id='ColorID" + rowID + "'   disabled='disabled'  style=' width:80%;' type='text'  class='tdinput' />"; //添加列内容
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
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:发货单数量
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input  id='OrderCount" + rowID + "'   disabled='disabled'  type='text'  class='tdinput'  style=' width:80%;'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:已执行数量
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='transactCount" + rowID + "'   disabled='disabled'   type='text'   class='tdinput'  style=' width:80%;'/>"; //添加列内容
                    m++;
                    
                    if ($("#txtIsMoreUnit").val() == "1") 
                    {
                        var newFitDesctd = newTR.insertCell(m); //添加列:本次发货数量
                        newFitDesctd.className = "cell";
                        newFitDesctd.innerHTML = "<input id='pCount" + rowID + "' onblur=\"fnTotalInfo()\"   type='text'  class='tdinput'  style=' width:80%;'/>"; //添加列内容
                        $("#pCount" + rowID).val(FormatAfterDotNumber(item.UsedUnitCount, precisionLength));
                        m++;
                    }
                    else
                    {
                        var newFitDesctd = newTR.insertCell(m); //添加列:基本数量(从物品档案带出不给修改)
                        newFitDesctd.className = "cell";
                        newFitDesctd.innerHTML = "<input id='pCount" + rowID + "' onblur=\"fnTotalInfo()\"  type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
                        $("#pCount" + rowID).val(FormatAfterDotNumber(item.ProductCount, precisionLength));
                        m++;
                    }
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:发货日期
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='SendDate" + rowID + "' readonly='readonly' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('SendDate" + rowID + "')})\"  type='text' class='tdinput'  style=' width:80%;'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:包装要求
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<select class='tdinput'   id='Package" + rowID + "'   style=' width:80%;'></select>"; //添加列内容
                    fnGetPackage("Package" + rowID); //添加包装要求选择项的值
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:单价
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='UnitPrice" + rowID + "' onblur=\"fnTotalInfo()\"    type='text' class='tdinput' style=' width:80%;'/><input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
                    m++;
                    $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UsedPrice, precisionLength));
                    $("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardCost, precisionLength));
                    $("#hiddBaseUnitPrice"+rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength));//基本单价（对应基本价格）
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:含税价
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='TaxPrice" + rowID + "' onblur=\"fnTotalInfo()\"    type='text' class='tdinput' style=' width:80%;'/> <input type=\"hidden\"  id='hiddTaxPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseTaxPrice" + rowID + "'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列:折扣(%)
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='Discount" + rowID + "' onblur=\"fnTotalInfo()\"   value='100' type='text' class='tdinput' style=' width:80%;'/> "; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 税率(%)
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='TaxRate" + rowID + "' onblur=\"fnTotalInfo()\"  type='text' class='tdinput' style=' width:80%;'/> <input type=\"hidden\"  id='hiddTaxRate" + rowID + "'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 含税金额
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='TotalFee" + rowID + "'   disabled='disabled'   type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 金额
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='TotalPrice" + rowID + "'  disabled='disabled'    type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 税额
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='TotalTax" + rowID + "'  disabled='disabled'    type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 备注
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='Remark" + rowID + "' type='text' class='tdinput' maxlength='100' style=' width:80%;'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 源单类型
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='FromType" + rowID + "'   disabled='disabled'  value='无来源' readonly='readonly' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 源单编号
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='FromBillID" + rowID + "'   disabled='disabled' type='text' class='tdinput' style=' width:95%;'/>"; //添加列内容
                    m++;
                    
                    var newFitDesctd = newTR.insertCell(m); //添加列: 源单行号
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='FromLineNo" + rowID + "'   disabled='disabled'   type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
                    m++;

                    $("#ProductID" + rowID).val(item.ProdNo); //商品编号
                    $("#hiddProductID" + rowID).val(item.ProductID); //商品ID
                    $("#ProductID" + rowID).attr("title", item.ProductID); //商品ID
                    $("#ProductName" + rowID).val(item.ProductName); //商品名称
                    //$("#UnitID" + rowID).val(item.CodeName); //单位名称
                    //$("#UnitID" + rowID).attr("title", item.UnitID); //单位ID

                    $("#SendDate" + rowID).val(item.SendDate); //发货日期
                    $("#FromType" + rowID).val(item.FromTypeText); //源单类型
                    $("#FromType" + rowID).attr("title", item.FromType); //单据来源
                    $("#FromBillID" + rowID).val(item.OrderNo); //源单类型
                    $("#FromBillID" + rowID).attr("title", item.FromBillID); //源单类型
                    $("#FromLineNo" + rowID).val(item.FromLineNo); //源单类型
                    //多计量单位
                    if ($("#txtIsMoreUnit").val() == "1") {
                        $("#OrderCount" + rowID).val(FormatAfterDotNumber(item.UsedOrderCount, precisionLength)); //订单数量(订单数量)
                    }
                    else
                    {
                        $("#OrderCount" + rowID).val(FormatAfterDotNumber(item.OrderCount, precisionLength)); //订单数量(订单数量)
                    }
                    if (item.transactCount != "") {
                        $("#transactCount" + rowID).val(FormatAfterDotNumber(item.transactCount, precisionLength)); //订单数量
                    }
                    //$("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardCost, precisionLength));
                    //$("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength)); //单价
                    $("#Specification" + rowID).val(item.Specification); //规格
                    $("#ColorID"+rowID).val(item.ColorName);//颜色
                    $("#TaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength)); //含税价
                    $("#hiddTaxPrice" + rowID).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                    $("#hiddBaseTaxPrice" + rowID).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                    $("#Discount" + rowID).val(FormatAfterDotNumber(item.Discount, precisionLength)); //折扣
                    $("#TaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                    $("#hiddTaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate1, precisionLength)); //税率
                    $("#TotalFee" + rowID).val(FormatAfterDotNumber(item.TotalFee, precisionLength));    //含税金额
                    $("#TotalPrice" + rowID).val(FormatAfterDotNumber(item.TotalPrice, precisionLength));  //金额
                    $("#TotalTax" + rowID).val(FormatAfterDotNumber(item.TotalTax, precisionLength));    //税额
                    //$("#pCount" + rowID).val(item.ProductCount);    //数量
                    $("#Package" + rowID).val(item.Package);     //包装要求ID
                    $("#Remark" + rowID).val(item.Remark);      //备注
                    //如果是增值税
                    if ($("#isAddTax").attr("checked")) {
                        $("#TaxRate" + rowID).removeAttr("readonly");

                    }
                    //如果不是增值税
                    //如果是增值税
                    if ($("#NotAddTax").attr("checked")) {
                        $("#TaxRate" + rowID).attr("readonly", "readonly");
                        $("#TaxRate" + rowID).val(FormatAfterDotNumber(0, precisionLength));

                    }
                    if (item.FromType == '1') {
                        $("#UnitPrice" + rowID).attr("disabled", "disabled"); //单价
                        $("#TaxPrice" + rowID).attr("disabled", "disabled"); //含税价
                        $("#Discount" + rowID).attr("disabled", "disabled"); //折扣
                        $("#TaxRate" + rowID).attr("disabled", "disabled"); //税率
                        //$("#Package" + rowID).attr("disabled", "disabled");     //包装要求ID                      
                    }
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
            }
        }
    });
}

//结单按钮调用的函数名称，业务逻辑不同模块各自处理
//isFlag=true结单操作，isFlag=false取消结单
function Fun_CompleteOperate(isFlag) {
    var CodeType = $("#SendOrderUC_ddlCodeRule").val(); //发货单编号产生的规则
    var SendNo = $("#SendOrderUC_txtCode").val();        //退货单编号
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
        url: "../../../Handler/Office/SellManager/SellSendAdd.ashx",
        data: 'CodeType=' + escape(CodeType) + '&SendNo=' + escape(SendNo) + '&action=' + acction,
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
                    $("#FromType").attr("disabled", "disabled");
                    $("#btn_update").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    
                  
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#hiddBillStatus").val('4');
                    $("#btnFromBill").css("display", "none");
                    $("#btnUnFromBill").css("display", "inline");
                    
                    $("#imgBilling").css("display", "none");
                    $("#imgUnBilling").css("display", "inline");

                }
                else {
                    $("#BillStatus").html("执行");
                    $("#Closer").val('');
                    $("#CloseDate").val('');
                    $("#FromType").attr("disabled", "disabled");
                    $("#btn_update").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    
                    
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#hiddBillStatus").val('2');
                    $("#btnFromBill").css("display", "none");
                    $("#btnUnFromBill").css("display", "inline");

                    $("#imgBilling").css("display", "inline");
                    $("#imgUnBilling").css("display", "none");
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

//取消确认
function Fun_UnConfirmOperate() {
    if (confirm("是否取消确认？")) {
        //var CodeType = $("#SendOrderUC_ddlCodeRule").val(); //发货单编号产生的规则
        //var SendNo = $("#SendOrderUC_txtCode").val();        //退货单编号
        var ID=$("#hiddOrderID").val();//发货单ID
        var acction = 'UnConfirm';

        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/SellSendAdd.ashx",
            data: fnGetInfo()+'&ID=' + escape(ID) + '&action=' + acction,
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
                    $("#FromType").removeAttr("disabled");
                    if ($("#FromType").val() == 1) {
                        fnGetDetail(data.no);
                    }
                    
                    $("#imgBilling").css("display", "none");
                    $("#imgUnBilling").css("display", "inline");
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
}


//获取主表信息
function fnGetInfo() {
    var strInfo = '';
    var CodeType = $("#SendOrderUC_ddlCodeRule").val(); //发货单编号产生的规则
    var SendNo = $("#SendOrderUC_txtCode").val(); //发货单编号    
    var Title = $.trim($("#Title").val());         //主题
    var FromType = $("#FromType").val();      //源单类型（0无源单，1销售发货单，2销售合同）
    var FromBillID = $("#FromBillID").attr("title"); //来源单据编号

    var SellType = $("#sellTypeUC_ddlCodeType").val();      //销售类别ID（对应分类代码表ID）                                    
    var BusiType = $("#BusiType").val();      //业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨，6分期付款  ）
    var PayType = $("#PayTypeUC_ddlCodeType").val();       //结算方式ID（对应分类代码表ID）
    var Seller = $("#hiddSeller").val();        //业务员(对应员工表ID)                                              
    var SellDeptId = $("#hiddDeptID").val();    //部门(对部门表ID)                                                  
    var MoneyType = $("#MoneyTypeUC_ddlCodeType").val();     //支付方式ID                                                        
    var TakeType = $("#TakeType_ddlCodeType").val();      //交货方式ID                                                        
    var CarryType = $("#CarryType_ddlCodeType").val();     //运送方式ID    

    var SendAddr = $("#SendAddr").val();      //发货地址
    var Sender = $("#hiddSender").val();        //发货人ID（对应员工表ID）                                          
    var ReceiveAddr = $("#ReceiveAddr").val();   //收货地址                                                          
    var Receiver = $("#Receiver").val();      //收货人姓名                                                        
    var Tel = $("#Tel").val();           //收货人电话                                                        
    var Modile = $("#Modile").val();        //收货人移动电话                                                    
    var Post = $("#Post").val();          //收货人邮编                                                        
    var IntendSendDate = $("#SendDate").val(); //预计发货时间                                                      
    var Transporter = $("#Transporter").attr("title");   //运输商                                                            
    var TransportFee = $("#TransportFee").val();  //运费合计                                                          
    var TransPayType = $("#TransPayType_ddlCodeType").val();  //运费结算方式ID
    var CurrencyType = $("#CurrencyType").attr("title"); //币种ID                                                             
    var Rate = $("#Rate").val();          //汇率                                                              
    var TotalPrice = $("#TotalPrice").val();    //金额合计                                                          
    var Tax = $("#Tax").val();           //税额合计                                                          
    var TotalFee = $("#TotalFee").val();      //含税金额合计                                                      
    var Discount = $("#Discount").val();      //整单折扣（%）                                                     
    var DiscountTotal = $("#DiscountTotal").val(); //折扣金额                                                          
    var RealTotal = $("#RealTotal").val();     //折后含税金额    
    var CustID = $("#CustID").attr("title"); //客户
    var CountTotal = $("#CountTotal").val(); //数量合计
    var PayRemark = $("#PayRemark").val();    //付款说明        
    var DeliverRemark = $("#DeliverRemark").val(); //交付说明        
    var PackTransit = $("#PackTransit").val();  //包装运输说明    
    var Remark = $("#Remark").val();       //备注  
    var ProjectID = $("#hiddProjectID").val();//所属项目ID   
    var CanViewUser = $("#CanViewUser").val();//可查看此订单人员       

    //如果是增值税
    if ($("#isAddTax").attr("checked")) {
        var isAddTax = 1; //是否增值税（0否,1是 ）    
    }
    //如果不是增值税
    if ($("#NotAddTax").attr("checked")) {
        var isAddTax = 0; //是否增值税（0否,1是 ）   
    }

    var CountTotal = $("#CountTotal").val();    //发货数量合计

    strInfo = 'CodeType=' + escape(CodeType) + '&SendNo=' + escape(SendNo) + '&Title=' + escape(Title) + '&CustID=' + escape(CustID) +
        '&FromType=' + escape(FromType) + '&FromBillID=' + escape(FromBillID) + '&SellType=' + escape(SellType) + '&BusiType=' + escape(BusiType) +
        '&PayType=' + escape(PayType) + '&Seller=' + escape(Seller) + '&SellDeptId=' + escape(SellDeptId) +
        '&MoneyType=' + escape(MoneyType) + '&TakeType=' + escape(TakeType) + '&CarryType=' + escape(CarryType) +
        '&SendAddr=' + escape(SendAddr) + '&Sender=' + escape(Sender) +
        '&ReceiveAddr=' + escape(ReceiveAddr) + '&Receiver=' + escape(Receiver) + '&Tel=' + escape(Tel) + '&CountTotal=' + escape(CountTotal) +
        '&Modile=' + escape(Modile) + '&Post=' + escape(Post) + '&IntendSendDate=' + escape(IntendSendDate) +
        '&Transporter=' + escape(Transporter) + '&TransportFee=' + escape(TransportFee) + '&isAddTax=' + escape(isAddTax) +
        '&TransPayType=' + escape(TransPayType) + '&CurrencyType=' + escape(CurrencyType) + '&Rate=' + escape(Rate) +
        '&TotalPrice=' + escape(TotalPrice) + '&Tax=' + escape(Tax) + '&TotalFee=' + escape(TotalFee) + '&PayRemark=' + escape(PayRemark) +
        '&DeliverRemark=' + escape(DeliverRemark) + '&PackTransit=' + escape(PackTransit) + '&Remark=' + escape(Remark) +
        '&Discount=' + escape(Discount) + '&DiscountTotal=' + escape(DiscountTotal) + '&RealTotal=' + escape(RealTotal)+
        '&ProjectID=' + escape(ProjectID)+'&CanViewUser=' + escape(CanViewUser);

    return strInfo;

}
////获取明细数据
function getDropValue() {
    var SendOrderFit_Item = new Array();
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;

            var SortNo = j;      //序号（行号）                             
            var ProductID = $("#ProductID" + rowid).attr("title");   //物品ID（对应物品表ID）                   
            //var ProductCount = $("#pCount" + rowid).val(); //数量                                     
            //var UnitID = $("#UnitID" + rowid).attr("title");      //单位ID（对应计量单位ID）                 
            //var UnitPrice = $("#UnitPrice" + rowid).val();   //单价                                     
            var TaxPrice = $("#TaxPrice" + rowid).val();    //含税价                                   
            var Discount = $("#Discount" + rowid).val();    //折扣（%）                                
            var TaxRate = $("#TaxRate" + rowid).val();     //税率（%）                                
            var TotalFee = $("#TotalFee" + rowid).val();    //含税金额                                 
            var TotalPrice = $("#TotalPrice" + rowid).val();  //金额                                     
            var TotalTax = $("#TotalTax" + rowid).val();    //税额                                     
            var SendDate = $("#SendDate" + rowid).val();    //发货日期                                 
            var Package = $("#Package" + rowid).val();     //包装要求ID                               
            var Remark = $("#Remark" + rowid).val();      //备注                                     
            var FromType = $("#FromType" + rowid).attr("title");    //源单类型（0无源单，1销售发货单，2销售合同）
            var FromBillID = $("#FromBillID" + rowid).attr("title");  //源单ID                                   
            var FromLineNo = $("#FromLineNo" + rowid).val();  //来源单据行号   
            
            var UnitID = $("#UnitID" + rowid).attr("title");      //基本单位ID（对应计量单位ID）
            var ProductCount = $("#pCount" + rowid).val(); //基本数量 
            var UnitPrice = $("#UnitPrice" + rowid).val();   //单价（基本单价）
            var UsedUnitID=$("#UnitID" + rowid).attr("title");//单位
            var UsedUnitCount=$("#pCount" + rowid).val();//数量
            var UsedPrice=$("#UnitPrice" + rowid).val();//单价（对应于单位的单价，也就是明细中显示的实际单价）
            var ExRate=1;//换算比率
            
            if($("#txtIsMoreUnit").val() == "1")
            {
                UnitID=$("#BaseUnitID" + rowid).attr("title");
                ProductCount=$("#BaseCountD" + rowid).val();
                UnitPrice = $("#hiddBaseUnitPrice" + rowid).val(); 
                
                var UnitRate=document.getElementById("selectUnitID"+rowid).value;
                var arrUnitRate=UnitRate.split('|');
                UsedUnitID=arrUnitRate[0];
                ExRate=arrUnitRate[1];
                //UsedUnitID=$("#UnitID" + rowid).attr("title");
                //ExRate=$("#hiddExRate"+rowid).val();
                
            }                          

            SendOrderFit_Item[j] = [[SortNo], [ProductID], [ProductCount], [UnitID], [UnitPrice], [TaxPrice], [Discount], [TaxRate], [TotalFee], [TotalPrice], [TotalTax], [SendDate], [Package], [Remark], [FromType], [FromBillID], [FromLineNo],[UsedUnitID],[UsedUnitCount],[UsedPrice],[ExRate]];
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
    var CodeType = $("#SendOrderUC_ddlCodeRule").val(); //发货单编号产生的规则
    var SendNo = $.trim($("#SendOrderUC_txtCode").val()); //发货单编号    
    //var Title = $.trim($("#Title").val());         //主题                                                              
    var FromType = $("#FromType").val();      //源单类型（0无源单，1销售发货单，2销售合同）                                                             
    var Seller = $("#UserSeller").val();        //业务员(对应员工表ID)                                              
    var SellDeptId = $("#hiddDeptID").val();    //部门(对部门表ID)                                                                                                                                                               
    //var Transporter = $.trim($("#Transporter").val());   //运输商                                                            
    var TransportFee = $.trim($("#TransportFee").val());  //运费合计                                                          
    var FromBillID = $("#FromBillID").val(); //来源单据编号                                                
    var Discount = $("#Discount").val();      //整单折扣（%）
    var CustID = $("#CustID").val();
    var CurrencyType = $("#CurrencyType").val();   //币种ID

    var TotalPrice = $("#TotalPrice").val();    //金额合计                                                                             
    var Tax = $("#Tax").val();           //税额合计                                                                             
    var TotalFee = $("#TotalFee").val();      //含税金额合计

    if (!IsNumeric(TotalPrice, 22, precisionLength)) {
        isFlag = false;
        fieldText = fieldText + "金额合计|";
        msgText = msgText + "金额合计超出范围|";
    }

    if (!IsNumeric(Tax, 22, precisionLength)) {
        isFlag = false;
        fieldText = fieldText + "税额合计|";
        msgText = msgText + "税额合计超出范围|";
    }

    if (!IsNumeric(TotalFee, 22, precisionLength)) {
        isFlag = false;
        fieldText = fieldText + "含税金额合计|";
        msgText = msgText + "含税金额合计超出范围|";
    }


    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (CodeType == "") {
        if (SendNo == "") {
            isFlag = false;
            fieldText = fieldText + "发货单编号|";
            msgText = msgText + "请输入发货单编号|";
        }
        else {
            if (!CodeCheck(SendNo)) {
                isFlag = false;
                fieldText = fieldText + "发货单编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
    }
    if (FromType == "1") {
        if (FromBillID == "") {
            isFlag = false;
            fieldText = fieldText + "来源单据|";
            msgText = msgText + "请选择销售订单|";
        }
    }

//    if (Title == "") {
//        isFlag = false;
//        fieldText = fieldText + "主题|";
//        msgText = msgText + "请输入发货单主题|";
//    }

    if (CustID == "") {
        isFlag = false;
        fieldText = fieldText + "客户名称|";
        msgText = msgText + "请选择客户|";
    }
//    if (Transporter == "") {
//        isFlag = false;
//        fieldText = fieldText + "运输商|";
//        msgText = msgText + "请选择运输商|";
//    }
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

    if (Discount.length > 0) {
        if (!IsNumeric(Discount, 12, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "整单折扣|";
            msgText = msgText + "整单折扣输入有误，请输入有效的数值！|";
        }
    }

    if (TransportFee.length > 0) {
        if (!IsNumeric(TransportFee, 14, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "运费合计|";
            msgText = msgText + "运费合计输入有误，请输入有效的数值！|";
        }
    }

    //验证明细信息
    var signFrame = findObj("dg_Log", document);
    var iCount = 0; //明细中数据数目
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].id;
            iCount = iCount + 1;
            var ProductID = $("#ProductID" + rowid).val;   //物品ID（对应物品表ID）                   
            var ProductCount = $("#pCount" + rowid).val(); //数量                                              
            var UnitPrice = $("#UnitPrice" + rowid).val();   //单价                                     
            var TaxPrice = $("#TaxPrice" + rowid).val();    //含税价                                   
            var Discount = $("#Discount" + rowid).val();    //折扣（%）                                
            var TaxRate = $("#TaxRate" + rowid).val();     //税率（%）
            var SendDate = $("#SendDate" + rowid).val();    //发货日期

            var TotalFee = $("#TotalFee" + rowid).val();         //含税金额                        
            var TotalPrice = $("#TotalPrice" + rowid).val();       //金额                            
            var TotalTax = $("#TotalTax" + rowid).val();         //税额

            if (!IsNumeric(TotalFee, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                msgText = msgText + "含税金额超出范围|";
            }

            if (!IsNumeric(TotalPrice, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                msgText = msgText + "金额超出范围|";
            }

            if (!IsNumeric(TotalTax, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                msgText = msgText + "税额超出范围|";
            }

            var txtIsOverOrder=$("#txtIsOverOrder").val();//是否启用超订单发货
            if(txtIsOverOrder=="0")
            {
                if ($("#FromType").val() != '0') {
                    var OrderCount = $("#OrderCount" + rowid).val();
                    var transactCount = $("#transactCount" + rowid).val();
                    if (parseFloat(ProductCount) > (parseFloat(OrderCount) - parseFloat(transactCount))) {
                        isFlag = false;
                        fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                        msgText = msgText + "本次发货数量不能大于未执行数量|";
                    }
                }
            }

            if (SendDate == "") {
                isFlag = false;
                fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                msgText = msgText + "请选择发货日期|";
            }
            if (TaxRate == "") {
                isFlag = false;
                fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                msgText = msgText + "请输入税率|";
            }
            else {
                if (!IsNumeric(TaxRate, 12, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                    msgText = msgText + "税率输入有误，请输入有效的数值！|";
                }
            }
            if (TaxPrice == "") {
                isFlag = false;
                fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                msgText = msgText + "请输入含税价|";
            }
            else {
                if (!IsNumeric(TaxPrice, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                    msgText = msgText + "含税价格输入有误，请输入有效的数值！|";
                }
            }
            if (Discount == "") {
                isFlag = false;
                fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                msgText = msgText + "请输入折扣|";
            }
            else {
                if (!IsNumeric(Discount, 12, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                    msgText = msgText + "折扣输入有误，请输入有效的数值！|";
                }
            }

            if (UnitPrice == "") {
                isFlag = false;
                fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                msgText = msgText + "请输入单价|";
            }
            else {
                if (!IsNumeric(UnitPrice, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                    msgText = msgText + "单价输入有误，请输入有效的数值！|";
                }
            }
            if (ProductCount == "") {
                isFlag = false;
                fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                msgText = msgText + "请输入数量|";
            }
            else {
                if (!IsNumeric(ProductCount, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                    msgText = msgText + "数量输入有误，请输入有效的数值！|";
                }
            }
            if (ProductID == "") {
                isFlag = false;
                fieldText = fieldText + "发货单明细（行号：" + i + "）|";
                msgText = msgText + "请选择物品|";
            }
        }
    }
    if (iCount == 0) {
        isFlag = false;
        fieldText = fieldText + "发货单明细|";
        msgText = msgText + "发货单明细不能为空！|";
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
        $("#FromType").attr("disabled", "true");
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
        $("#FromType").removeAttr("disabled");
        $("#btnFromBill").css("display", "inline");
        $("#btnUnFromBill").css("display", "none");
    }
}



//打印功能
function fnPrintOrder() {
    var OrderNo = $.trim($("#SendOrderUC_txtCode").val());
    if (OrderNo == '保存时自动生成' || OrderNo == '') {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存单据再打印！");
        return;
    }
    window.open('../../../Pages/PrinttingModel/SellManager/PrintSellSend.aspx?no=' + OrderNo);

}

/*库存快照*/
function ShowSnapshot()
{
       var intProductID = 0;
       var detailRows = 0;
       var snapProductName = '';
       var snapProductNo = '';
       var signFrame = findObj("dg_Log",document); 
      for(var i=1;i<signFrame.rows.length;i++)
       {
            if(signFrame.rows[i].style.display!="none")
            {
                var rowid = signFrame.rows[i].id;
                if(document.getElementById('chk'+rowid).checked)
                {
                    detailRows ++;
                    intProductID = $("#hiddProductID" + rowid).val();
                    snapProductName= $("#ProductName"+rowid).val();
                    snapProductNo = $("#ProductID"+rowid).val();
                }
            }
            
       }
       
       if(detailRows==1)
       {
           if(intProductID<=0 || strlen(cTrim(intProductID,0))<=0)
           {
              popMsgObj.ShowMsg('选中的明细行中没有添加物品');
              return false;  
           }
            ShowStorageSnapshot(intProductID,snapProductName,snapProductNo);
           
       }
       else
       {
          popMsgObj.ShowMsg('请选择单个物品查看库存快照');
          return false;
       }   
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

////清除选择的所属项目
//function fnClearProject()
//{
//    $("#hiddProjectID").val('');//所属项目ID
//    $("#ProjectID").val('');//所属项目名称
//}

//开票
function fnToBilling()
{
    var orderID=$("#hiddOrderID").val();//单据ID
    
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellSendList.ashx",
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
                //fnInitPage(data.data[0]); //给页面赋值
                var ParamLoc="";    
                ParamLoc=data.data[0].ID+"|"+data.data[0].SendNo+"|"+data.data[0].CustID+"|"+data.data[0].CustName+"|"+data.data[0].CurrencyType+"|"+data.data[0].CurrencyName+"|"+data.data[0].Rate+"|"+data.data[0].RealTotal+"|7";

                var billingmoduleid=$("#hiddBillingAddModuleid").val();
                var moduleid=$("#hiddModuleID").val();
                window.location.href="../FinanceManager/Billing_Add.aspx?aid="+escape(data.data[0].ID)+"&ParamLoc="+ParamLoc+"&ModuleID="+billingmoduleid;
            }
        }
    });
 
}