
 var hidCodeBarObj = '0';
$(document).ready(function() {
fnGetExtAttr();
    funSetCodeBar();//是否显示条码控制
    var pakeageUC = document.getElementById("PackageUC_ddlCodeType");
    pakeageUC.style.display = "none";
    var ddlReasonType = document.getElementById("ddlReasonType");
    ddlReasonType.style.display = "none";
    $("#SellBackUC_ddlCodeRule").css("width", "100px");
    $("#PayTypeUC_ddlCodeType").css("width", "120px");
    $("#MoneyTypeUC_ddlCodeType").css("width", "120px");
    $("#CarryType_ddlCodeType").css("width", "120px");
    $("#SellBackUC_ddlCodeRule").css("width", "80px");
    $("#SellBackUC_txtCode").css("width", "80px");

    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['islist']; //是否从列表页面进入
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031801', 'ModuleID=2031802');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (flag != null) {
            $("#ibtnBack").css("display", "inline");
        }
    }
    GetExtAttr("officedba.SellBack",null);
    GetFlowButton_DisplayControl();
});

/*参数设置：是否显示条码控制*/
function funSetCodeBar()
{
    try
    {
        hidCodeBarObj = document.getElementById('hidBarCode').value;
        var objCodeBar = document.getElementById('GetGoods');
        hidCodeBarObj=='1'?objCodeBar.style.display='':objCodeBar.style.display='none';
    }catch(e){}
}

//获取查询参数
function GetRequestToHidden() {
    var url = location.search;
    return url;
}

//返回按钮
function fnBack() {
    var URLParams = document.getElementById("HiddenURLParams").value;
    window.location.href = 'SellBackList.aspx' + URLParams;
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

//选择退货单明细信息
function fnSelectOrderList() {
    if ($("#FromType").val() == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "源单类型为无来源时，不可选择！");
    }
    if ($("#FromType").val() == 1) {
        if ($("#CustID").val() == '') {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请选择发货单！");
        }
        else {
            var custID = $("#CustID").attr("title");
            var FromBillID = $("#FromBillID").attr("title"); //来源单据编号
            var CurrencyType = $("#CurrencyType").attr("title"); //币种ID
            var Rate = $("#Rate").val(); //币种ID
            popSendDetailObj.ShowList(custID, '', CurrencyType, FromBillID, Rate,'sellBack');
        }
    }

}

//选择发货单来源时
function fnFromTypeChange(obj) {
    if (obj.selectedIndex == 0) {
        //清除已填写数据
        fnDelRow();
        $("#FromBillID").val(''); //发货单编号
        $("#FromBillID").removeAttr("title"); //发货单ID
        $("#CustID").val(''); //客户名称
        $("#CustID").removeAttr("title"); //客户编号
        $("#CurrencyType").val(''); //币种
        $("#CurrencyType").removeAttr("title"); //币种 
        $("#Rate").val(FormatAfterDotNumber(0, 4)); //汇率           
        $("#UserSeller").val(''); //业务员名称
        $("#hiddSeller").val(''); //业务员编号
        $("#DeptId").val(''); //部门名称
        $("#hiddDeptID").val(''); //部门编号
        $("#CustTel").val(''); //客户电话
        $("#SendAddress").val('');
        $("#Discount").val(FormatAfterDotNumber(100, precisionLength));      //整单折扣（%）  
        $("#MoneyTypeUC_ddlCodeType").attr("selectedIndex", 0); //支付方式
        $("#CarryType_ddlCodeType").attr("selectedIndex", 0); //运送方式
        $("#PayTypeUC_ddlCodeType").attr("selectedIndex", 0); //结算方式
        $("#BusiType").attr("selectedIndex", 0); //业务类型

        $("#isAddTax").removeAttr("disabled");
        $("#NotAddTax").removeAttr("disabled");
        $("#isAddTax").attr("checked", "checked");
        $("#NotAddTax").removeAttr("checked", "checked");
    }
    if (obj.selectedIndex == 1) {
        fnDelRow();
       
        //清除已填写数据
        $("#FromBillID").val(''); //发货单编号
        $("#FromBillID").removeAttr("title"); //发货单ID
        $("#CustID").val(''); //客户名称
        $("#CustID").removeAttr("title"); //客户编号
        $("#CurrencyType").val(''); //币种
        $("#CurrencyType").removeAttr("title"); //币种
        $("#Rate").val(FormatAfterDotNumber(0, 4)); //汇率           
        $("#UserSeller").val(''); //业务员名称
        $("#hiddSeller").val(''); //业务员编号
        $("#DeptId").val(''); //部门名称
        $("#hiddDeptID").val(''); //部门编号
        $("#CustTel").val(''); //客户电话
        $("#SendAddress").val('');
        $("#Discount").val(FormatAfterDotNumber(100, precisionLength));      //整单折扣（%）  
        $("#MoneyTypeUC_ddlCodeType").attr("selectedIndex", 0); //支付方式
        $("#CarryType_ddlCodeType").attr("selectedIndex", 0); //运送方式
        $("#PayTypeUC_ddlCodeType").attr("selectedIndex", 0); //结算方式
        $("#BusiType").attr("selectedIndex", 0); //业务类型


       

        $("#isAddTax").attr("disabled", "disabled");
        $("#NotAddTax").attr("disabled", "disabled");
        $("#isAddTax").attr("checked", "checked");
        $("#NotAddTax").removeAttr("checked", "checked");
    }
}

//选择源单
function fnSelectOfferInfo() {
    if ($("#FromType").val() == 1) {
        if ($("#FromType").attr("disabled") != true) {
            popSellSendObj.ShowList('', 'protion');
        }
    }
}

//清除销售订单
function clearSellSend() {
    fnFromTypeChange(document.getElementById("FromType"));

    closeSellSenddiv();
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

//清除客户信息
function ClearSellModuCustdiv() {
    $("#CustID").val(''); //客户名称
    $("#CustID").attr("title", ''); //客户编号
    $("#CustTel").val(''); //客户类型
    $("#CurrencyType").val(''); //币种
    $("#CurrencyType").attr("title", ''); //币种
    $("#BusiType").attr("selectedIndex", 0); //业务类型
    $("#CarryType_ddlCodeType").attr("selectedIndex", 0); //运送方式
    $("#PayTypeUC_ddlCodeType").attr("selectedIndex", 0); //结算方式
    $("#Rate").val(FormatAfterDotNumber(0,4)); //汇率
    $("#MoneyTypeUC_ddlCodeType").attr("selectedIndex", 0); //支付方式
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
            $("#CustTel").val(data.Tel); //客户名称          
            $("#CurrencyType").val(data.CurrencyName); //币种
            $("#CurrencyType").attr("title", data.CurrencyType); //币种
            $("#BusiType").val(data.BusiType); //业务类型
            $("#CarryType_ddlCodeType").val(data.CarryType); //运送方式
            $("#PayTypeUC_ddlCodeType").val(data.PayType); //结算方式
            $("#Rate").val(data.ExchangeRate); //汇率
            $("#MoneyTypeUC_ddlCodeType").val(data.MoneyType); //支付方式
            try {
                fnGetPriceByRate();
            } catch (e) { }
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}


//选择币种
function fnSelectCurrency() {
    if ($("#FromType").val() == 0) {
        popSellCurrObj.ShowList('CurrencyType', 'Rate');
    }
}

////选择发货单后带出退货单明细信息
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
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取发货单数据失败,请确认"); },
        success: function(data) {
            //发货单基本信息    
            if (data.ord != null) {
                $("#FromBillID").val(data.ord[0].SendNo); //发货单编号
                $("#FromBillID").attr("title", OrderId); //发货单ID
                $("#CustID").val(data.ord[0].CustName); //客户名称
                $("#SendAddress").val(data.ord[0].SendAddr); //发货地址
                $("#CustID").attr("title", data.ord[0].CustID); //客户编号
                $("#CustTel").val(data.ord[0].CustTel); //客户电话
                $("#PayTypeUC_ddlCodeType").val(data.ord[0].PayType); //结算方式
                $("#CurrencyType").attr("title", data.ord[0].CurrencyType); //币种
                $("#CurrencyType").val(data.ord[0].CurrencyName);
                $("#Rate").val(data.ord[0].Rate); //汇率
                $("#UserSeller").val(data.ord[0].EmployeeName)//业务员名称
                $("#hiddSeller").val(data.ord[0].Seller)//业务员名称
                $("#MoneyTypeUC_ddlCodeType").val(data.ord[0].MoneyType); //支付方式
                $("#CarryType_ddlCodeType").val(data.ord[0].CarryType); //运送方式
                $("#OrderMethod_ddlCodeType").val(data.ord[0].OrderMethod); //订货方式
                $("#DeptId").val(data.ord[0].DeptName); //部门名称
                $("#BusiType").val(data.ord[0].BusiType); //业务类型
                $("#Discount").val(FormatAfterDotNumber( parseFloat( data.ord[0].Discount),precisionLength));      //整单折扣（%）             
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
            //退货单明细信息
            if (data.ordList != null) {
                fnSetDetailData(data.ordList);
            }
        }
    });
    closeSellSenddiv(); //关闭客户选择控件  
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

//添加退货原因选择项的值
function fnGetReason(id) {
    var Reason = document.getElementById("ddlReasonType");
    var obj = document.getElementById(id);
    var varItem = new Option('请选择', '');
    obj.options.add(varItem);
    for (var i = 0; i < Reason.length; i++) {
        var varItem = new Option(Reason.options[i].text, Reason.options[i].value);
        obj.options.add(varItem);
    }
}


//计算各种合计信息
function fnTotalInfo() {
    var TotalPrice = 0; //金额合计
    var Tax = 0; //税额合计
    var TotalFee = 0; //含税金额合计
    var Discount = $("#Discount").val(); //整单折扣
    if (Discount == '') {
        $("#Discount").val(FormatAfterDotNumber(100, precisionLength));
    }
    else {
        Number_round(document.getElementById("Discount"), precisionLength);
        if (!IsNumeric(Discount, 12, precisionLength)) {
            $("#Discount").val(FormatAfterDotNumber(100, precisionLength));
            Discount = FormatAfterDotNumber(100, precisionLength);
        }

    }
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
                    CalCulateNum("selectUnitID"+rowid ,"BackNumber"+rowid ,"BaseCountD"+rowid,'','', precisionLength );
                }
            }catch(e){}
            var pCountDetail = $("#BackNumber" + rowid).val(); //数量
            if ($.trim(pCountDetail) == '') {
                pCountDetail = 0;
            }
            else {
                Number_round(document.getElementById("BackNumber" + rowid), precisionLength);
                if (!IsNumeric($("#BackNumber" + rowid).val(), 14, precisionLength)) {
                    $("#BackNumber" + rowid).val('');
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

//多选添加行
function AddSignRows()
{
    if ($("#FromType").val() == 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "源单类型为销售发货单时，不可添加无来源明细信息！");
    }
    if ($("#FromType").val() == 0) {
        popTechObj.ShowListCheckSpecial('','check');
    }
}
//多选填充值
function GetValue()
{ 
   var ck = document.getElementsByName("CheckboxProdID");
        var strarr=''; 
        var str = "";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               strarr += ck[i].value+',';
            }
        }
         str = strarr.substring(0,strarr.length-1);
         if(str =="")
         {
            popMsgObj.ShowMsg('请先选择数据！');
            return;
         }
          $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/ProductCheck.ashx?str='+str,//目标地址
           cache:false,
               data:'',//数据
               beforeSend:function(){},//发送数据之前
         
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $.each(msg.data,function(i,item)
                    {
                    //填充物品ID，物品编号，物品名称,单位ID，单位名称，规格，
                        if(!IsExistCheck(item.ProdNo))//如果是重复的物品编号不允许添加了
                        {  
                             AddSignRow();
                             var txtTRLastIndex = findObj("txtTRLastIndex", document);
                             var rowIndex = parseInt(txtTRLastIndex.value);
                             //var rowIndex = $("#rowIndex").val(); //当前选择物品的行号
                            $("#ProductID" + rowIndex).val(item.ProdNo); //商品编号
                            $("#hiddProductID" + rowIndex).val(item.ID); //商品ID
                            $("#ProductID" + rowIndex).attr("title", item.ID); //商品ID
                            $("#ProductName" + rowIndex).val(item.ProductName); //商品名称
                            //$("#UnitID" + rowIndex).val(item.CodeName); //单位名称
                            //$("#UnitID" + rowIndex).attr("title", item.UnitID); //单位ID
                            $("#UnitPrice" + rowIndex).val(FormatAfterDotNumber(item.StandardSell, precisionLength)); //单价
                            $("#Specification" + rowIndex).val(item.Specification); //规格
                            $("#ColorID"+rowIndex).val(item.ColorName);//颜色
                            $("#TaxPrice" + rowIndex).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                            $("#hiddTaxPrice" + rowIndex).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                            $("#hiddBaseTaxPrice" + rowIndex).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                            $("#Discount" + rowIndex).val(FormatAfterDotNumber(item.Discount, precisionLength)); //折扣
                            $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                            $("#hiddTaxRate" + rowIndex).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                            $("#FromType" + rowIndex).attr("title", "0"); //单据来源
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
                            //多计量单位启用
                            if($("#txtIsMoreUnit").val() == "1")
                            {
                                $("#BaseUnitID" + rowIndex).val(item.CodeName); //单位名称
                                $("#BaseUnitID" + rowIndex).attr("title", item.UnitID); //单位ID
                            }
                            else
                            {
                               $("#UnitID" + rowIndex).val(item.CodeName); //单位名称
                               $("#UnitID" + rowIndex).attr("title", item.UnitID); //单位ID  
                            }
                            
                            $("#hiddUnitPrice" + rowIndex).val(FormatAfterDotNumber(item.StandardSell, precisionLength)); //单价
                            $("#hiddBaseUnitPrice"+rowIndex).val(FormatAfterDotNumber(item.StandardSell, precisionLength));//基本单价（对应基本价格）
                            if($("#txtIsMoreUnit").val() == "1")
                            {   
                                //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
                                //GetUnitGroupSelect(ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit(this)', "UnitID" + rowIndex, SaleUnitID);
                                GetUnitGroupSelectEx(item.ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit('+rowIndex+')', "UnitID" + rowIndex, item.SaleUnitID,'fnGetPriceByRate1('+rowIndex+')');
                            }
                            else
                            {
                                fnGetPriceByRate1(rowIndex);
                            }
                            //$("#hiddUnitPrice" + rowIndex).val(FormatAfterDotNumber(item.StandardSell, 2)); //单价
                            //fnGetPriceByRate1(rowIndex);
                        }
                   });
                     
                      },
             
               complete:function(){}//接收数据完毕
           });
      closeProductdiv();
} 
//判断物品在明细中添加是否重复
function IsExistCheck(prodNo)
{
    var sign=false ;
    var signFrame = findObj("dg_Log",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < signFrame.rows.length; i++) 
        {
          var prodNo1 = document.getElementById("ProductID" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none" )&&(prodNo1 == prodNo)) 
            {
                 sign= true;
                 break ;
            }
        }
    }
 
    return sign ;
}

//添加新行
function AddSignRow() { //读取最后一行的行号，存放在txtTRLastIndex文本框中 
//    if ($("#FromType").val() == 1) {
//        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "源单类型为销售发货单时，不可添加无来源明细信息！");
//    }
//    if ($("#FromType").val() == 0) {
       
        var txtTRLastIndex = findObj("txtTRLastIndex", document);
        var rowID = parseInt(txtTRLastIndex.value) + 1;
        var signFrame = findObj("dg_Log", document);
        var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
        newTR.id = rowID;
        var m=0;

        var newNameXH = newTR.insertCell(m); //添加列:序号
        newNameXH.className = "cell";
        newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  />";
        m++;
        
        var newFitNotd = newTR.insertCell(m); //添加列:物品编号
        newFitNotd.className = "cell";
        newFitNotd.innerHTML = "<input id='ProductID" + rowID + "' readonly='readonly' style=' width:90%;' type='text'  class='tdinput' /><input type=\"hidden\" value='' id='hiddProductID" + rowID + "'/>"; //添加列内容
        $("#ProductID" + rowID).bind("click", function() {
            $("#rowIndex").val(rowID);
            var v = 'ProductID' + rowID;
            popTechObj.ShowList(v);
        }); //绑定选择物品事件
        m++;
        
        var newFitNametd = newTR.insertCell(m); //添加列:物品名称
        newFitNametd.className = "cell";
        newFitNametd.innerHTML = "<input id='ProductName" + rowID + "'  disabled='disabled'   style=' width:90%; ' type='text'  class='tdinput' />"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:规格
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='Specification" + rowID + "'  style=' width:90%;' disabled='disabled'  type='text'  class='tdinput' />"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:颜色
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='ColorID" + rowID + "'  style=' width:90%;' disabled='disabled'  type='text'  class='tdinput' />"; //添加列内容
        m++;
        
        //计量单位开启
        if ($("#txtIsMoreUnit").val() == "1") 
        {
            var newFitDesctd = newTR.insertCell(m); //添加列:基本单位(从物品档案带出不给修改)
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='BaseUnitID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
            m++;
            
            var newFitDesctd = newTR.insertCell(m); //添加列:基本数量(从物品档案带出不给修改)
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='BaseCountD" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
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
            newFitDesctd.innerHTML = "<input id='UnitID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
            m++;
            
        }

        var newFitDesctd = newTR.insertCell(m); //添加列:发货单数量
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='ProductCount" + rowID + "'  disabled='disabled' type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:已退货数量
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='BackCount" + rowID + "'  disabled='disabled' type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:退货数量
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='BackNumber" + rowID + "'   onblur=\"fnTotalInfo()\"   type='text'   class='tdinput'  style=' width:90%;'/>"; //添加列内容
        m++;

        var newFitDesctd = newTR.insertCell(m); //添加列:包装要求
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<select  id='Package" + rowID + "'  style=' width:90%;'></select>"; //添加列内容
        fnGetPackage("Package" + rowID); //添加包装要求选择项的值
        m++;

        var newFitDesctd = newTR.insertCell(m); //添加列:单价
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='UnitPrice" + rowID + "'  onblur=\"fnTotalInfo()\"   type='text' class='tdinput' style=' width:94%;'/><input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:含税价
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TaxPrice" + rowID + "'  onblur=\"fnTotalInfo()\"   type='text' class='tdinput' style=' width:94%;'/> <input type=\"hidden\" value='0.00' id='hiddTaxPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseTaxPrice" + rowID + "'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:折扣(%)
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='Discount" + rowID + "'   onblur=\"fnTotalInfo()\"  value='100' type='text' class='tdinput' style=' width:94%;'/> "; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 税率(%)
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TaxRate" + rowID + "'  onblur=\"fnTotalInfo()\"  type='text' class='tdinput' style=' width:94%;'/> <input type=\"hidden\" value='0.00' id='hiddTaxRate" + rowID + "'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 含税金额
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TotalFee" + rowID + "'   disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 金额
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TotalPrice" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 税额
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TotalTax" + rowID + "'   disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 退货原因
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<select id='Reason" + rowID + "' style=' width:90%;'></select>"; //添加列内容
        fnGetReason("Reason" + rowID);
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 备注
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='Remark" + rowID + "'  maxlength='100'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 源单类型
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='FromType" + rowID + "' value='无来源'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 源单编号
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='FromBillID" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 源单行号
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='FromLineNo" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        m++;
        
        txtTRLastIndex.value = rowID; //将行号推进下一行
        //如果是增值税
        if ($("#isAddTax").attr("checked")) {
            $("#TaxRate" + rowID).removeAttr("readonly");
            $("#TaxRate" + rowID).val($("#hiddTaxRate" + rowID).val());
            $("#TaxPrice" + rowID).val($("#hiddTaxPrice" + rowID).val());
        }
        //如果不是增值税
        //如果是增值税
        if ($("#NotAddTax").attr("checked")) {
            $("#TaxRate" + rowID).attr("readonly", "readonly");
            $("#TaxRate" + rowID).val(FormatAfterDotNumber(0, precisionLength));

            $("#TaxPrice" + rowID).val($("#UnitPrice" + rowID).val());
        }
    //}
}

//选择物品
function Fun_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, CodeName, TaxRate, SellTax, Discount, Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost,GroupUnitNo,SaleUnitID,SaleUnitName,InUnitID,InUnitName,StockUnitID,StockUnitName,MakeUnitID,MakeUnitName,IsBatchNo,ColorName) {
    if(!IsExistCheck(ProdNo))//如果是重复的物品编号不允许添加了
    {
        var rowIndex = $("#rowIndex").val(); //当前选择物品的行号
        $("#ProductID" + rowIndex).val(ProdNo); //商品编号
        $("#hiddProductID" + rowIndex).val(ID); //商品ID
        $("#ProductID" + rowIndex).attr("title", ID); //商品ID
        $("#ProductName" + rowIndex).val(ProductName); //商品名称
        //$("#UnitID" + rowIndex).val(CodeName); //单位名称
        //$("#UnitID" + rowIndex).attr("title", UnitID); //单位ID
        //$("#UnitPrice" + rowIndex).val(FormatAfterDotNumber(StandardSell, precisionLength)); //单价
        $("#Specification" + rowIndex).val(Specification); //规格
        $("#ColorID" + rowIndex).val(ColorName); //规格
        $("#TaxPrice" + rowIndex).val(FormatAfterDotNumber(SellTax, precisionLength)); //含税价
        $("#hiddTaxPrice" + rowIndex).val(FormatAfterDotNumber(SellTax, precisionLength)); //含税价
        $("#hiddBaseTaxPrice" + rowIndex).val(FormatAfterDotNumber(SellTax, precisionLength)); //含税价
        $("#Discount" + rowIndex).val(FormatAfterDotNumber(Discount, precisionLength)); //折扣
        $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(TaxRate, precisionLength)); //税率
        $("#hiddTaxRate" + rowIndex).val(FormatAfterDotNumber(TaxRate, precisionLength)); //税率
        $("#FromType" + rowIndex).attr("title", "0"); //单据来源
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
        //$("#hiddUnitPrice" + rowIndex).val(FormatAfterDotNumber(StandardCost, 2)); //单价
        //fnGetPriceByRate1(rowIndex);  
        //多计量单位启用
        if($("#txtIsMoreUnit").val() == "1")
        {
            $("#BaseUnitID" + rowIndex).val(CodeName); //单位名称
            $("#BaseUnitID" + rowIndex).attr("title", UnitID); //单位ID
        }
        else
        {
           $("#UnitID" + rowIndex).val(CodeName); //单位名称
           $("#UnitID" + rowIndex).attr("title", UnitID); //单位ID  
        }
        $("#hiddUnitPrice" + rowIndex).val(FormatAfterDotNumber(StandardSell, precisionLength)); //单价
        $("#hiddBaseUnitPrice"+rowIndex).val(FormatAfterDotNumber(StandardSell, precisionLength));//基本单价（对应基本价格）
        if($("#txtIsMoreUnit").val() == "1")
        {   
            //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
            //GetUnitGroupSelect(ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit(this)', "UnitID" + rowIndex, SaleUnitID);
            GetUnitGroupSelectEx(ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit('+rowIndex+')', "UnitID" + rowIndex, SaleUnitID,'fnGetPriceByRate1('+rowIndex+')');
        }
        else
        {
            fnGetPriceByRate1(rowIndex);
        }
    }
    else
    {
        popMsgObj.ShowMsg('明细已存在！');
    }

}
//改变计量单位时改变基本数量和价格
function ChangeUnit(rowid)
{
    //根据单位和数量计算基本数量。参数（单位下列表ID，数量文本框ID，基本数量文本框ID，单价，基本单价）
    CalCulateNum("selectUnitID"+rowid ,"BackNumber"+rowid ,"BaseCountD"+rowid,"UnitPrice"+rowid,"hiddBaseUnitPrice"+rowid , precisionLength);
    CalCulateNum("selectUnitID"+rowid ,"BackNumber"+rowid ,"BaseCountD"+rowid,"TaxPrice"+rowid,"hiddBaseTaxPrice"+rowid , precisionLength);
    $("#hiddTaxPrice" + rowid).val($("#TaxPrice" + rowid).val());   //把带回的单价放到隐藏域中
    $("#hiddUnitPrice" + rowid).val($("#UnitPrice" + rowid).val());   //把带回的单价放到隐藏域中
    //fnTotalInfo();
    fnGetPriceByRate1(rowid);
}

//新选择物品根据汇率计算单价含税价
function fnGetPriceByRate1(rowid) {
    var Rate = $("#Rate").val();           //汇率
    if(Rate==''|| Rate=="0.0000")
    {
        Rate = FormatAfterDotNumber(1,4);
    }

    var UnitPrice = $("#hiddUnitPrice" + rowid).val();   //单价
    var TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
    //如果是增值税
    if ($("#isAddTax").attr("checked")) {
        TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
    }
    //如果不是增值税
    //如果是增值税
    if ($("#NotAddTax").attr("checked")) {
        TaxPrice = $("#hiddUnitPrice" + rowid).val();    //含税价
    }
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
            var TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
            //如果是增值税
            if ($("#isAddTax").attr("checked")) {
                TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
            }
            //如果不是增值税
            //如果是增值税
            if ($("#NotAddTax").attr("checked")) {
                TaxPrice = $("#hiddUnitPrice" + rowid).val();    //含税价
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
    $("#checkall").removeAttr("checked");
    fnTotalInfo();
}


//选择退货单明细
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
        newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox' /> ";
        m++;
        
        var newFitNotd = newTR.insertCell(m); //添加列:物品编号
        newFitNotd.className = "cell";
        newFitNotd.innerHTML = "<input id='ProductID" + rowID + "' readonly='readonly' style=' width:90%;' type='text'  class='tdinput' /><input type=\"hidden\" value='' id='hiddProductID" + rowID + "'/>"; //添加列内容
        $("#ProductID" + rowID).val(item.ProdNo);
        $("#hiddProductID" + rowID).val(item.ProductID);
        $("#ProductID" + rowID).attr("title", item.ProductID);
        m++;
        
        var newFitNametd = newTR.insertCell(m); //添加列:物品名称
        newFitNametd.className = "cell";
        newFitNametd.innerHTML = "<input id='ProductName" + rowID + "'   disabled='disabled'   style=' width:90%; ' type='text'  class='tdinput' />"; //添加列内容
        $("#ProductName" + rowID).val(item.ProductName);
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:规格
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='Specification" + rowID + "'  style=' width:90%;' disabled='disabled'  type='text'  class='tdinput' />"; //添加列内容
        $("#Specification" + rowID).val(item.Specification);
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:颜色
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='ColorID" + rowID + "'  style=' width:90%;' disabled='disabled'  type='text'  class='tdinput' />"; //添加列内容
        $("#ColorID" + rowID).val(item.ColorName);
        m++;
        
        //计量单位开启
        if ($("#txtIsMoreUnit").val() == "1") 
        {
            var newFitDesctd = newTR.insertCell(m); //添加列:基本单位(从物品档案带出不给修改)
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='BaseUnitID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
            m++;
            
            var newFitDesctd = newTR.insertCell(m); //添加列:基本数量(从物品档案带出不给修改)
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='BaseCountD" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
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
            newFitDesctd.innerHTML = "<input id='UnitID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
            m++;
        }
//        var newFitDesctd = newTR.insertCell(m); //添加列:单位
//        newFitDesctd.className = "cell";
//        newFitDesctd.innerHTML = "<input id='UnitID" + rowID + "' disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
//        $("#UnitID" + rowID).val(item.CodeName);
//        $("#UnitID" + rowID).attr("title", item.UnitID);
//        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:发货单数量
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='ProductCount" + rowID + "'  disabled='disabled'  type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
        $("#ProductCount" + rowID).val(FormatAfterDotNumber(item.ProductCount, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:已退货数量
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input  id='BackCount" + rowID + "'  disabled='disabled'  type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
        $("#BackCount" + rowID).val(FormatAfterDotNumber(item.BackCount, precisionLength));
        m++;
        
        //计量单位开启
        if ($("#txtIsMoreUnit").val() == "1") 
        {
            var newFitDesctd = newTR.insertCell(m); //添加列:退货数量
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='BackNumber" + rowID + "' onblur=\"fnTotalInfo()\"   type='text' value='0.00'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
            $("#BackNumber" + rowID).val(FormatAfterDotNumber((item.UsedUnitCount - item.BackCount), precisionLength));
            m++;
        }
        else
        {
            var newFitDesctd = newTR.insertCell(m); //添加列:退货数量
            newFitDesctd.className = "cell";
            newFitDesctd.innerHTML = "<input id='BackNumber" + rowID + "' onblur=\"fnTotalInfo()\"   type='text' value='0.00'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
            $("#BackNumber" + rowID).val(FormatAfterDotNumber((item.ProductCount - item.BackCount), precisionLength));
            m++;
        }
        
//        var newFitDesctd = newTR.insertCell(m); //添加列:退货数量
//        newFitDesctd.className = "cell";
//        newFitDesctd.innerHTML = "<input id='BackNumber" + rowID + "' onblur=\"fnTotalInfo()\"   type='text' value='0.00'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
//        $("#BackNumber" + rowID).val(FormatAfterDotNumber((item.ProductCount - item.BackCount), precisionLength));
//        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:包装要求
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<select  id='Package" + rowID + "'  disabled='disabled'   style=' width:90%;'></select>"; //添加列内容
        fnGetPackage("Package" + rowID); //添加包装要求选择项的值
        $("#Package" + rowID).val(item.Package);
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:单价
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='UnitPrice" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/><input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
        //$("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength));
        //$("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardCost, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:含税价
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TaxPrice" + rowID + "'  disabled='disabled'   type='text' class='tdinput' style=' width:94%;'/> <input type=\"hidden\" value='0.00' id='hiddTaxPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
        $("#TaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength));
        $("#hiddTaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength));
        $("#hiddBaseTaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:折扣(%)
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='Discount" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        $("#Discount" + rowID).val(FormatAfterDotNumber(item.Discount, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 税率(%)
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TaxRate" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/> <input type=\"hidden\" value='0.00' id='hiddTaxRate" + rowID + "'/>"; //添加列内容
        $("#TaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength));
        $("#hiddTaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 含税金额
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TotalFee" + rowID + "'    disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        $("#TotalFee" + rowID).val(FormatAfterDotNumber(item.TotalFee, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 金额
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TotalPrice" + rowID + "'   disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        $("#TotalPrice" + rowID).val(FormatAfterDotNumber(item.TotalPrice, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 税额
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='TotalTax" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        $("#TotalTax" + rowID).val(FormatAfterDotNumber(item.TotalTax, precisionLength));
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 退货原因
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<select id='Reason" + rowID + "' style=' width:90%;'></select>"; //添加列内容
        fnGetReason("Reason" + rowID);
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 备注
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='Remark" + rowID + "' maxlength='100' type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 源单类型
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='FromType" + rowID + "' value='销售发货单'  disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
        $("#FromType" + rowID).attr("title", "1");
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 源单编号
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='FromBillID" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
        $("#FromBillID" + rowID).attr("title", item.ID);
        $("#FromBillID" + rowID).val(item.SendNo);
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列: 源单行号
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='FromLineNo" + rowID + "' disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
        $("#FromLineNo" + rowID).val(item.SortNo);
        m++;
        
        //多计量单位启用
        if($("#txtIsMoreUnit").val() == "1")
        {
            $("#BaseUnitID" + rowID).val(item.CodeName); //单位名称
            $("#BaseUnitID" + rowID).attr("title", item.UnitID); //单位ID
            //$("#BaseCountD" + rowID).val(item.ProductCount); //基本数量
            $("#BackNumber" + rowID).val(FormatAfterDotNumber((item.UsedUnitCount-item.BackCount), precisionLength)); //数量
            $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UsedPrice, precisionLength)); //单价
            $("#hiddBaseUnitPrice"+rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength));//基本单价（对应基本价格）
        }
        else
        {
           $("#UnitID" + rowID).val(item.CodeName); //单位名称
           $("#UnitID" + rowID).attr("title", item.UnitID); //单位ID  
           $("#BackNumber" + rowID).val(FormatAfterDotNumber((item.ProductCount-item.BackCount), precisionLength)); //数量
           $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength)); //单价
        }
        $("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardSell, precisionLength)); //单价
        if($("#txtIsMoreUnit").val() == "1")
        {   
            //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
            //GetUnitGroupSelect(item.ProductID, 'SaleUnit', 'selectUnitID' + rowID, 'ChangeUnit('+rowID+')', "UnitID" + rowID, item.UsedUnitID);
            GetUnitGroupSelectEx(item.ProductID, 'SaleUnit', 'selectUnitID' + rowID, 'ChangeUnit('+rowID+')', "UnitID" + rowID, item.UsedUnitID,'fnTotalInfo(),fnDisabledOrNot('+rowID+');');
        }
        else
        {
            fnTotalInfo();
        }
        txtTRLastIndex.value = rowID; //将行号推进下一行
    });
    closeSendDetaildiv();
    //fnTotalInfo();
}

//源单为销售发货时单位不可修改
function fnDisabledOrNot(rowID)
{
    var FromType=$("#FromType").val();
    if(FromType=="1")
    {
       $("#selectUnitID"+rowID).attr("disabled", "disabled");
    }
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
        url: "../../../Handler/Office/SellManager/SellBackAdd.ashx",
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
                $("#SellBackUC_txtCode").val(data.no);
                $("#SellBackUC_txtCode").attr("disabled", "disabled");
                $("#SellBackUC_ddlCodeRule").css("display", "none");
                $("#SellBackUC_txtCode").css("width", "95%");
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

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellBackAdd.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=update',
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
                $("#SellBackUC_txtCode").val(data.no);
                $("#SellBackUC_txtCode").attr("disabled", "disabled");
                $("#SellBackUC_ddlCodeRule").css("display", "none");
                $("#SellBackUC_txtCode").css("width", "95%");
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
        //var CodeType = $("#SellBackUC_ddlCodeRule").val(); //发货单编号产生的规则
        //var BackNo = $("#SellBackUC_txtCode").val();        //退货单编号
        var ID=$("#hiddOrderID").val();
        
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/SellBackAdd.ashx",
            data: fnGetInfo() + '&ID='+escape(ID)+'&action=confirm',
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
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    if (hidCodeBarObj=='1') 
                    {
                        $("#GetGoods").css("display", "none");
                        $("#UnGetGoods").css("display", "inline");
                    }
                    
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

//获取报价单明细信息
function fnGetDetail(orderNo) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellBackLsit.ashx",
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

                    var newNameXH = newTR.insertCell(0); //添加列:序号
                    newNameXH.className = "cell";
                    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  />";

                    var newFitNotd = newTR.insertCell(1); //添加列:物品编号
                    newFitNotd.className = "cell";
                    newFitNotd.innerHTML = "<input id='ProductID" + rowID + "' readonly='readonly' style=' width:90%;' type='text'  class='tdinput' /><input type=\"hidden\" value='' id='hiddProductID" + rowID + "'/>"; //添加列内容
                    $("#ProductID" + rowID).bind("click", function() {
                        $("#rowIndex").val(rowID);
                        var v = 'ProductID' + rowID;
                        popTechObj.ShowList(v);
                    }); //绑定选择物品事件

                    var newFitNametd = newTR.insertCell(2); //添加列:物品名称
                    newFitNametd.className = "cell";
                    newFitNametd.innerHTML = "<input id='ProductName" + rowID + "'  disabled='disabled'   style=' width:90%; ' type='text'  class='tdinput' />"; //添加列内容

                    var newFitDesctd = newTR.insertCell(3); //添加列:规格
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input  id='Specification" + rowID + "'  style=' width:90%;' disabled='disabled'  type='text'  class='tdinput' />"; //添加列内容

                    var newFitDesctd = newTR.insertCell(4); //添加列:单位
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='UnitID" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(5); //添加列:发货单数量
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input  id='ProductCount" + rowID + "'  disabled='disabled' type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(6); //添加列:已退货数量
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input  id='BackCount" + rowID + "'  disabled='disabled' type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(7); //添加列:退货数量
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='BackNumber" + rowID + "' onblur=\"fnTotalInfo()\"   type='text'   class='tdinput'  style=' width:90%;'/>"; //添加列内容


                    var newFitDesctd = newTR.insertCell(8); //添加列:包装要求
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<select  id='Package" + rowID + "'  style=' width:90%;'></select>"; //添加列内容
                    fnGetPackage("Package" + rowID); //添加包装要求选择项的值


                    var newFitDesctd = newTR.insertCell(9); //添加列:单价
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='UnitPrice" + rowID + "' onblur=\"fnTotalInfo()\"   type='text' class='tdinput' style=' width:94%;'/><input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(10); //添加列:含税价
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='TaxPrice" + rowID + "' onblur=\"fnTotalInfo()\"   type='text' class='tdinput' style=' width:94%;'/> <input type=\"hidden\" value='0.00' id='hiddTaxPrice" + rowID + "'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(11); //添加列:折扣(%)
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='Discount" + rowID + "' onblur=\"fnTotalInfo()\"  value='100.00' type='text' class='tdinput' style=' width:94%;'/> "; //添加列内容

                    var newFitDesctd = newTR.insertCell(12); //添加列: 税率(%)
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='TaxRate" + rowID + "' onblur=\"fnTotalInfo()\"  type='text' class='tdinput' style=' width:94%;'/> <input type=\"hidden\" value='0.00' id='hiddTaxRate" + rowID + "'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(13); //添加列: 含税金额
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='TotalFee" + rowID + "'   disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(14); //添加列: 金额
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='TotalPrice" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(15); //添加列: 税额
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='TotalTax" + rowID + "'   disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(16); //添加列: 退货原因
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<select id='Reason" + rowID + "' style=' width:90%;'></select>"; //添加列内容
                    fnGetReason("Reason" + rowID);

                    var newFitDesctd = newTR.insertCell(17); //添加列: 备注
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='Remark" + rowID + "'  maxlength='100'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(18); //添加列: 源单类型
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='FromType" + rowID + "' value='无来源'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(19); //添加列: 源单编号
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='FromBillID" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容

                    var newFitDesctd = newTR.insertCell(20); //添加列: 源单行号
                    newFitDesctd.className = "cell";
                    newFitDesctd.innerHTML = "<input id='FromLineNo" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:94%;'/>"; //添加列内容

                    txtTRLastIndex.value = rowID; //将行号推进下一行


                    $("#ProductID" + rowID).val(item.ProdNo); //商品编号
                    $("#hiddProductID" + rowID).val(item.ProductID); //商品ID
                    $("#ProductID" + rowID).attr("title", item.ProductID); //商品ID
                    $("#ProductName" + rowID).val(item.ProductName); //商品名称
                    $("#UnitID" + rowID).val(item.CodeName); //单位名称
                    $("#UnitID" + rowID).attr("title", item.UnitID); //单位ID
                    $("#BackCount" + rowID).val(FormatAfterDotNumber(item.BackCount, precisionLength));
                    $("#ProductCount" + rowID).val(item.ProductCount); //发货日期
                    $("#FromType" + rowID).val(item.FromTypeText); //源单类型
                    $("#FromType" + rowID).attr("title", item.FromType); //单据来源
                    $("#FromBillID" + rowID).attr("title", item.FromBillID);
                    $("#FromBillID" + rowID).val(item.SendNo);
                   // $("#FromBillID" + rowID).val(item.FromBillID); //源单类型
                    $("#FromLineNo" + rowID).val(item.FromLineNo); //源单类型

                    $("#BackNumber" + rowID).val(FormatAfterDotNumber(item.BackNumber, precisionLength)); //退货数量
                    $("#Reason" + rowID).val(item.Reason);

                    $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength)); //单价
                    $("#Specification" + rowID).val(item.Specification); //规格
                    $("#TaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength)); //含税价
                    $("#hiddTaxPrice" + rowID).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                    $("#Discount" + rowID).val(FormatAfterDotNumber(item.Discount, precisionLength)); //折扣
                    $("#TaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                    $("#hiddTaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate1, precisionLength)); //税率
                    $("#TotalFee" + rowID).val(FormatAfterDotNumber(item.TotalFee, precisionLength));    //含税金额
                    $("#TotalPrice" + rowID).val(FormatAfterDotNumber(item.TotalPrice, precisionLength));  //金额
                    $("#TotalTax" + rowID).val(FormatAfterDotNumber(item.TotalTax, precisionLength));    //税额
                    $("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardCost, precisionLength));
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
                        $("#Package" + rowID).attr("disabled", "disabled");     //包装要求ID                    
                    }
                    txtTRLastIndex.value = rowID; //将行号推进下一行
                });
                fnTotalInfo();
            }
        }
    });
}

//结单按钮调用的函数名称，业务逻辑不同模块各自处理
//isFlag=true结单操作，isFlag=false取消结单
function Fun_CompleteOperate(isFlag) {
    var CodeType = $("#SellBackUC_ddlCodeRule").val(); //发货单编号产生的规则
    var BackNo = $("#SellBackUC_txtCode").val();        //退货单编号
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
        url: "../../../Handler/Office/SellManager/SellBackAdd.ashx",
        data: 'CodeType=' + escape(CodeType) + '&BackNo=' + escape(BackNo) + '&action=' + acction,
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
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    if (hidCodeBarObj=='1')
                    {
                        $("#GetGoods").css("display", "none");
                        $("#UnGetGoods").css("display", "inline");
                    }
                    
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
                    $("#FromType").attr("disabled", "disabled");
                    $("#btn_update").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    if (hidCodeBarObj=='1')
                    {
                        $("#GetGoods").css("display", "none");
                        $("#UnGetGoods").css("display", "inline");
                    }
                    
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
//取消确认
function Fun_UnConfirmOperate() {
    if (confirm("是否取消确认？")) {
        //var CodeType = $("#SellBackUC_ddlCodeRule").val(); //发货单编号产生的规则
        //var SendNo = $("#SellBackUC_txtCode").val();        //退货单编号
        var ID=$("#hiddOrderID").val();
        var acction = 'UnConfirm';

        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/SellBackAdd.ashx",
            data: fnGetInfo() + '&ID='+escape(ID)+'&action=' + acction,
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
                    $("#imgUnAdd").css("display", "none");
                    $("#imgAdd").css("display", "inline");
                    $("#imgUnDel").css("display", "none");
                    if (hidCodeBarObj=='1')
                    {
                        $("#GetGoods").css("display", "inline");
                        $("#UnGetGoods").css("display", "none");
                    }
                    
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
    var CodeType = $("#SellBackUC_ddlCodeRule").val(); //发货单编号产生的规则

    var BackNo = $("#SellBackUC_txtCode").val();        //退货单编号                                                                           
    var CustID = $("#CustID").attr("title");        //客户ID（关联客户信息表）                                                             
    var FromType = $("#FromType").val();      //来源单据类型（0无来源，1发货通知单）                                                 
    var FromBillID = $("#FromBillID").attr("title");    //来源单据ID（发货通知单）                                                             
    var CustTel = $("#CustTel").val();       //客户联系电话                                                                         
    var BackCargoTheme = $.trim($("#BackCargoTheme").val()); //主题                                                                                 
    var BusiType = $("#BusiType").val();      //业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨，6分期付款）                   
    var BackDate = $("#BackDate").val();      //退货日期
    var Seller = $("#hiddSeller").val();        //业务员(对应员工表ID)
    var SellDeptId = $("#hiddDeptID").val();      //部门(对部门表ID)
    var CarryType = $("#CarryType_ddlCodeType").val();     //运送方式ID                                                                           
    var SendAddress = $("#SendAddress").val();   //发货地址                                                                             
    var ReceiveAddress = $("#ReceiveAddress").val(); //收货地址                                                                             
    var PayType = $("#PayTypeUC_ddlCodeType").val();       //结算方式ID
    var MoneyType = $("#MoneyTypeUC_ddlCodeType").val();     //支付方式ID                                                                           
    var CurrencyType = $("#CurrencyType").attr("title");  //币种ID                                                                               
    var Rate = $("#Rate").val();          //汇率                                                                                                                                            
    var TotalPrice = $("#TotalPrice").val();    //金额合计                                                                             
    var Tax = $("#Tax").val();           //税额合计                                                                             
    var TotalFee = $("#TotalFee").val();      //含税金额合计                                                                         
    var Discount = $("#Discount").val();      //整单折扣（%）                                                                        
    var DiscountTotal = $("#DiscountTotal").val(); //折扣金额                                                                             
    var RealTotal = $("#RealTotal").val();     //折后含税金额合计                                                                     
    var CountTotal = $("#CountTotal").val();    //退货数量合计                                                                         
    var NotPayTotal = $("#NotPayTotal").val();   //抵应收账款                                                                           
    var BackTotal = $("#BackTotal").val();     //应退货款总额                                                                         
    var Remark = $("#Remark").val();        //备注                                                                                 
    var BillStatus = '1';    //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
    var ProjectID = $("#hiddProjectID").val();//所属项目ID                                 

    //如果是增值税
    if ($("#isAddTax").attr("checked")) {
        var isAddTax = 1; //是否增值税（0否,1是 ）    
    }
    //如果不是增值税
    if ($("#NotAddTax").attr("checked")) {
        var isAddTax = 0; //是否增值税（0否,1是 ）
    }
    strInfo = 'CodeType=' + escape(CodeType) + '&BackNo=' + escape(BackNo) + '&CustID=' + escape(CustID) + '&FromType=' + escape(FromType) + '&FromBillID=' + escape(FromBillID)
            + '&CustTel=' + escape(CustTel) + '&BackCargoTheme=' + escape(BackCargoTheme) + '&BusiType=' + escape(BusiType) + '&BackDate=' + escape(BackDate)
            + '&Seller=' + escape(Seller) + '&SellDeptId=' + escape(SellDeptId) + '&CarryType=' + escape(CarryType) + '&SendAddress=' + escape(SendAddress)
            + '&ReceiveAddress=' + escape(ReceiveAddress) + '&PayType=' + escape(PayType)
            + '&MoneyType=' + escape(MoneyType) + '&CurrencyType=' + escape(CurrencyType) + '&Rate=' + escape(Rate) + '&isAddTax=' + escape(isAddTax)
            + '&TotalPrice=' + escape(TotalPrice) + '&Tax=' + escape(Tax) + '&TotalFee=' + escape(TotalFee) + '&Discount=' + escape(Discount)
            + '&DiscountTotal=' + escape(DiscountTotal) + '&RealTotal=' + escape(RealTotal) + '&CountTotal=' + escape(CountTotal)
            + '&NotPayTotal=' + escape(NotPayTotal) + '&BackTotal=' + escape(BackTotal) + '&Remark=' + escape(Remark) + '&BillStatus=' + escape(BillStatus)
            + '&ProjectID=' + escape(ProjectID)+GetExtAttrValue();
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
            var ProductID = $("#ProductID" + rowid).attr("title"); //物品ID（对应物品表ID）          
            //var ProductCount = $("#ProductCount" + rowid).val();     //发货数量                        
            //var UnitID = $("#UnitID" + rowid).attr("title");   //单位ID（对应计量单位ID）        
            //var UnitPrice = $("#UnitPrice" + rowid).val();        //单价                            
            var TaxPrice = $("#TaxPrice" + rowid).val();         //含税价                          
            var Discount = $("#Discount" + rowid).val();         //折扣（%）                       
            var TaxRate = $("#TaxRate" + rowid).val();          //税率（%）                       
            var TotalFee = $("#TotalFee" + rowid).val();         //含税金额                        
            var TotalPrice = $("#TotalPrice" + rowid).val();       //金额                            
            var TotalTax = $("#TotalTax" + rowid).val();         //税额                            
            var Package = $("#Package" + rowid).val();          //包装要求ID                                            
            var Reason = $("#Reason" + rowid).val();           //退货原因ID                      
            var Remark = $("#Remark" + rowid).val();           //备注
            var FromType = $("#FromType" + rowid).attr("title");    //源单类型（0无源单，1销售发货单，2销售合同）
            var FromBillID = $("#FromBillID" + rowid).attr("title");  //源单ID                          
            var FromLineNo = $("#FromLineNo" + rowid).val();       //来源单据行号
            var ProductCount = $("#ProductCount" + rowid).val(); //发货数量 
            
            var UnitID = $("#UnitID" + rowid).attr("title");      //基本单位ID（对应计量单位ID）
            var BackNumber = $("#BackNumber" + rowid).val();       //退货数量（基本数量）  
            var UnitPrice = $("#UnitPrice" + rowid).val();   //单价（基本单价）
            var UsedUnitID=$("#UnitID" + rowid).attr("title");//单位
            var UsedUnitCount=$("#BackNumber" + rowid).val();//数量
            var UsedPrice=$("#UnitPrice" + rowid).val();//单价（对应于单位的单价，也就是明细中显示的实际单价）
            var ExRate=1;//换算比率
            
            if($("#txtIsMoreUnit").val() == "1")
            {
                UnitID=$("#BaseUnitID" + rowid).attr("title");
                BackNumber=$("#BaseCountD" + rowid).val();
                UnitPrice = $("#hiddBaseUnitPrice" + rowid).val(); 
                
                var UnitRate=document.getElementById("selectUnitID"+rowid).value;
                var arrUnitRate=UnitRate.split('|');
                UsedUnitID=arrUnitRate[0];
                ExRate=arrUnitRate[1];
                
            }

            SendOrderFit_Item[j] = [[SortNo], [ProductID], [ProductCount], [UnitID], [UnitPrice], [TaxPrice], [Discount], [TaxRate],
                                    [TotalFee], [TotalPrice], [TotalTax], [Package], [BackNumber], [Reason],
                                    [Remark], [FromType], [FromBillID], [FromLineNo],[UsedUnitID],[UsedUnitCount],[UsedPrice],[ExRate]];
            arrlength++;
        }
    }
    return SendOrderFit_Item;
}


//删除明细一行
function fnDelOneRow() {
    var dg_Log = findObj("dg_Log", document);
    var rowscount = dg_Log.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除
        if ($("#chk" + i).attr("checked")) {
            dg_Log.deleteRow(i);
        }
    }
    var txtTRLastIndex = findObj("txtTRLastIndex", document); //重置最后行号为
    txtTRLastIndex.value = dg_Log.rows.length - 1;

    $("#checkall").removeAttr("checked");
    fnTotalInfo();
}

function fnSelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}
//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var CodeType = $("#SellBackUC_ddlCodeRule").val(); //发货单编号产生的规则
    var BackNo = $.trim($("#SellBackUC_txtCode").val());        //退货单编号
    var CustID = $.trim($("#CustID").val());        //客户ID（关联客户信息表）                                                             
    var FromType = $("#FromType").val();      //来源单据类型（0无来源，1发货通知单）                                                 
    var FromBillID = $("#FromBillID").val();    //来源单据ID（发货通知单）                                                             
    //var BackCargoTheme = $.trim($("#BackCargoTheme").val()); //主题                                                                                 
    var BackDate = $.trim($("#BackDate").val());      //退货日期
    var Seller = $.trim($("#hiddSeller").val());        //业务员(对应员工表ID)
    var SellDeptId = $("#hiddDeptID").val();      //部门(对部门表ID)

    var Discount = $.trim($("#Discount").val());      //整单折扣（%）

    var CurrencyType = $.trim($("#CurrencyType").val());   //币种ID
    
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
        if (BackNo == "") {
            isFlag = false;
            fieldText = fieldText + "退货单编号|";
            msgText = msgText + "请输入退货单编号|";
        }
        else {
            if (isnumberorLetters(BackNo)) {
                isFlag = false;
                fieldText = fieldText + "退货单编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
    }
    if (FromType == "1") {
        if (FromBillID == "") {
            isFlag = false;
            fieldText = fieldText + "来源单据|";
            msgText = msgText + "请选择发货单|";
        }
    }

//    if (BackCargoTheme == "") {
//        isFlag = false;
//        fieldText = fieldText + "主题|";
//        msgText = msgText + "请输入退货单主题|";
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
    if (BackDate == "") {
        isFlag = false;
        fieldText = fieldText + "退货时间|";
        msgText = msgText + "请选择退货时间|";
    }

    if (Discount.length > 0) {
        if (!IsNumeric(Discount, 12, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "整单折扣|";
            msgText = msgText + "整单折扣格式不正确|";
        }
    }
    if (CurrencyType == "") {
        isFlag = false;
        fieldText = fieldText + "币种|";
        msgText = msgText + "请选择币种|";
    }


    //验证明细信息
    var signFrame = findObj("dg_Log", document);
    var iCount = 0; //明细中数据数目
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].id;
            iCount = iCount + 1;
            var ProductID = $("#ProductID" + rowid).attr("title"); //物品ID（对应物品表ID）          
            var ProductCount = $("#ProductCount" + rowid).val();     //发货数量                        

            var UnitPrice = $("#UnitPrice" + rowid).val();        //单价                            
            var TaxPrice = $("#TaxPrice" + rowid).val();         //含税价                          
            var Discount = $("#Discount" + rowid).val();         //折扣（%）                       
            var TaxRate = $("#TaxRate" + rowid).val();          //税率（%）                                                                             
            var BackNumber = $("#BackNumber" + rowid).val();       //退货数量
            var ProCount = $("#ProductCount" + rowid).val(); //      发货数量
            
            var TotalFee = $("#TotalFee" + rowid).val();         //含税金额                        
            var TotalPrice = $("#TotalPrice" + rowid).val();       //金额                            
            var TotalTax = $("#TotalTax" + rowid).val();         //税额

            if (!IsNumeric(TotalFee, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                msgText = msgText + "含税金额超出范围|";
            }

            if (!IsNumeric(TotalPrice, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                msgText = msgText + "金额超出范围|";
            }

            if (!IsNumeric(TotalTax, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                msgText = msgText + "税额超出范围|";
            }

            if ($("#FromType").val() != '0') {
                var BackCount = $("#BackCount" + rowid).val();//已退货数量

                if (parseFloat(BackNumber) > (parseFloat(ProCount) - parseFloat(BackCount))) {
                    isFlag = false;
                    fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                    msgText = msgText + "本次退货数量不能大于未退货数量|";
                }
            }
            
            if (TaxRate == "") {
                isFlag = false;
                fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                msgText = msgText + "请输入税率|";
            }
            else {
                if (!IsNumeric(TaxRate, 12, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                    msgText = msgText + "税率格式错误|";
                }
            }
            if (TaxPrice == "") {
                isFlag = false;
                fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                msgText = msgText + "请输入含税价|";
            }
            else {
                if (!IsNumeric(TaxPrice, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                    msgText = msgText + "含税价格式错误|";
                }
            }
            if (Discount == "") {
                isFlag = false;
                fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                msgText = msgText + "请输入折扣|";
            }
            else {
                if (!IsNumeric(Discount, 12, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                    msgText = msgText + "折扣格式错误|";
                }
            }


            if (UnitPrice == "") {
                isFlag = false;
                fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                msgText = msgText + "请输入单价|";
            }
            else {
                if (!IsNumeric(UnitPrice, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                    msgText = msgText + "单价格式错误|";
                }
            }
            if (BackNumber == "") {
                isFlag = false;
                fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                msgText = msgText + "请输入退货数量|";
            }
            else {
                if (!IsNumeric(BackNumber, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                    msgText = msgText + "退货数量只能为数字|";
                }
            }
            if (ProductID == "") {
                isFlag = false;
                fieldText = fieldText + "退货单明细（行号：" + i + "）|";
                msgText = msgText + "请选择物品|";
            }
        }
    }

    if (iCount == 0) {
        isFlag = false;
        fieldText = fieldText + "退货单明细|";
        msgText = msgText + "退货单明细不能为空！|";
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
        $("#imgAdd").css("display", "none");
        $("#imgUnAdd").css("display", "inline");
        if (hidCodeBarObj=='1')
        {
            $("#GetGoods").css("display", "none");
            $("#UnGetGoods").css("display", "inline");
        }
        
        $("#imgDel").css("display", "none");
        $("#btnFromBill").css("display", "none");
        $("#btnUnFromBill").css("display", "inline");
        $("#imgUnDel").css("display", "inline");
    }
    else {
        $("#btn_update").css("display", "inline");
        $("#imgUnSave").css("display", "none");
        $("#imgAdd").css("display", "inline");
        $("#imgUnAdd").css("display", "none");
        if (hidCodeBarObj=='1')
        {
            $("#GetGoods").css("display", "inline");
            $("#UnGetGoods").css("display", "none");
        }
        
        $("#imgDel").css("display", "inline");
        $("#imgUnDel").css("display", "none");
        $("#FromType").removeAttr("disabled");
        $("#btnFromBill").css("display", "inline");
        $("#btnUnFromBill").css("display", "none");
    }
}



//打印功能
function fnPrintOrder() {
    var OrderNo = $.trim($("#SellBackUC_txtCode").val());
    if (OrderNo == '保存时自动生成' || OrderNo == '') {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存单据再打印！");
        return;
    }
    window.open('../../../Pages/PrinttingModel/SellManager/PrintSellBack.aspx?no=' + OrderNo);
}

//---------------------------------------------------条码扫描Start------------------------------------------------------------------------------------
/*
 参数：商品ID，商品编号，商品名称，去税售价，单位ID，单位名称，销项税率（%），含税售价，销售折扣，
规格，类型名称，类型ID，含税进价，去税进价，进项税率(%)，标准成本
*/
//根据条码获取的商品信息填充数据
function GetGoodsDataByBarCode(ID, ProdNo, ProductName, StandardSell, UnitID, CodeName, TaxRate, SellTax, Discount, Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost,a,b,c,d,e,ColorName)
{
    if(!IsExist(ID))//如果重复记录，就不增加
    {
       AddSignRow();//插入行
       var txtTRLastIndex = findObj("txtTRLastIndex", document);
       var rowID = parseInt(txtTRLastIndex.value);
       $("#rowIndex").val(rowID);
       //填充数据
       Fun_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, CodeName, TaxRate, SellTax, Discount, Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost,a,b,c,d,e,'','','','','',ColorName);
       $("#BackNumber"+rowID).val(1);
       fnGetPriceByRate1(rowID);
    }
    else
    {
        document.getElementById("BackNumber"+rerowID).value=parseFloat(document.getElementById("BackNumber"+rerowID).value)+1;
        fnTotalInfo();
    }           
    
}

var rerowID="";
//判断是否有相同记录有返回true，没有返回false
function IsExist(ID)
{
     var signFrame = findObj("dg_Log", document);
     if((typeof(signFrame)=="undefined")|| signFrame==null)
     {
        return false;
     }
     for (i = 1; i < signFrame.rows.length; i++) {//判断商品是否在明细列表中
         if (signFrame.rows[i].style.display != "none") {
              var rowid = signFrame.rows[i].id;
              var ProductID = $("#ProductID" + rowid).attr("title"); //商品ID（对应商品表ID）
              if(ProductID==ID)
               {
                   rerowID=i;
                   return true;
               }
            }
        }
     return false;
 }
 function fnGetProInfoByBarCode() {
     if ($("#FromType").val() == 1) {
         showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "源单类型为销售发货单时，不可添加无来源明细信息！");
     }
     if ($("#FromType").val() == 0) {
         GetGoodsInfoByBarCode();
     }
 }
//---------------------------------------------------条码扫描END------------------------------------------------------------------------------------

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

////清除选择的所属项目
//function fnClearProject()
//{
//    $("#hiddProjectID").val('');//所属项目ID
//    $("#ProjectID").val('');//所属项目名称
//}