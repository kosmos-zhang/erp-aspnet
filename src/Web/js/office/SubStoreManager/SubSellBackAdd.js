var page = "";
var Isliebiao;

$(document).ready(function() {
    fnGetExtAttr();
    SetDefaultValue();
    requestQuaobj = GetRequest();
    document.getElementById("hidBillStatusName").value = requestQuaobj["BillStatusName"];
    document.getElementById('hidBusiStatusName').value = requestQuaobj["BusiStatusName"];
    requestobj = GetRequest(location.search);
    if (requestobj["Page"] != null) {
        $("#btn_back").css("display", "inline");
        $("#hidSearchCondition").val(location.search.substr(1))
        if (requestobj["ID"]) {
            DealPage(requestobj["ID"]);
        }
    }
    else {
        // 填充扩展属性
        GetExtAttr("officedba.SubSellBack", null);
    }
});

// 设置初始值
function SetDefaultValue() {
    document.getElementById('txtInDate').disabled = true;
    document.getElementById('ddlInDateHour').disabled = true;
    document.getElementById('ddlInDateMin').disabled = true;
    document.getElementById('UserInUserID').disabled = true;
    document.getElementById('txtSttlDate').disabled = true;
    document.getElementById('ddlSttlDateHour').disabled = true;
    document.getElementById('ddlSttlDateMin').disabled = true;
    document.getElementById('UserSttlUserID').disabled = true;
    var temp = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
    $("#txtCountTotal").val(temp);
    $("#txtTotalMoney").val(temp);
    $("#txtTotalTaxHo").val(temp);
    $("#txtTotalFeeHo").val(temp);
    $("#txtDiscount").val(FormatAfterDotNumber(100, parseInt($("#hidSelPoint").val())));
    $("#txtDiscountTotal").val(temp);
    $("#txtRealTotal").val(temp);
    $("#txtWairPayTotal").val(temp);
    $("#txtSettleTotal").val(temp);
    $("#txtPayedTotal").val(temp);
    $("#txtWairPayTotalOverage").val(temp);
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

//处理加载页面
function DealPage(recordnoparam) {
    if (recordnoparam != 0) {
        document.getElementById("txtAction").value = "2";
        GetSubSellBack(recordnoparam);

        //页面加载完对页面进行设置
        var IsBillStatusName = document.getElementById("hidBillStatusName").value;
        if (IsBillStatusName != "制单") {
            //设置所有文本控件只读
            $(":text").each(function() {
                this.disabled = true;
            });

            document.getElementById('drpFromType').disabled = true;
            document.getElementById('ddlSendMode').disabled = true;
            document.getElementById('ddlOutDateHour').disabled = true;
            document.getElementById('ddlOutDateMin').disabled = true;
            document.getElementById('chkisAddTax').disabled = true;
            document.getElementById('ddlBackDateHour').disabled = true;
            document.getElementById('ddlBackDateMin').disabled = true;
            document.getElementById('drpCurrencyType').disabled = true;
            document.getElementById('txtBackReason').disabled = true;
            document.getElementById('txtRemark').disabled = true;
            document.getElementById('divUploadResume').disabled = true;
            document.getElementById('divDeleteResume').disabled = true;
            try {
                //按扭控制
                try {
                    document.getElementById('btnGetGoods').style.display = 'none';
                    document.getElementById('unbtnGetGoods').style.display = '';
                }
                catch (e) { }
                document.getElementById("imgSave").style.display = "none";
                document.getElementById("imgUnSave").style.display = "inline";
                document.getElementById("imgAdd").style.display = "none";
                document.getElementById("imgUnAdd").style.display = "inline";
                document.getElementById("imgDel").style.display = "none";
                document.getElementById("imgUnDel").style.display = "inline";
                document.getElementById("Get_Potential").style.display = "none";
                document.getElementById("Get_UPotential").style.display = "inline";
                document.getElementById("btn_confirm").style.display = "none";
                document.getElementById("btn_ruku").style.display = "none";
                document.getElementById("btn_jiesuan").style.display = "none";
                document.getElementById("btn_Unconfirm").style.display = "inline";
            }
            catch (e)
                   { }


            var IsBusiStatusName = document.getElementById("hidBusiStatusName").value;

            if (IsBusiStatusName == "入库") {
                try {


                    //再次对按扭控制
                    try {
                        document.getElementById('btnGetGoods').style.display = 'none';
                        document.getElementById('unbtnGetGoods').style.display = '';
                    }
                    catch (e) { }
                    document.getElementById("imgSave").style.display = "none";
                    document.getElementById("imgUnSave").style.display = "none";
                    document.getElementById("imgAdd").style.display = "none";
                    document.getElementById("imgUnAdd").style.display = "none";
                    document.getElementById("imgDel").style.display = "none";
                    document.getElementById("imgUnDel").style.display = "none";
                    document.getElementById("Get_Potential").style.display = "none";
                    document.getElementById("Get_UPotential").style.display = "none";
                    document.getElementById("btn_confirm").style.display = "none";
                    document.getElementById("btn_Qxconfirm").style.display = "inline";
                    document.getElementById("btn_UnQxconfirm").style.display = "none";
                    document.getElementById("btn_ruku").style.display = "inline";
                    document.getElementById("btn_jiesuan").style.display = "none";
                    document.getElementById("btn_Unconfirm").style.display = "none";

                }
                catch (e)
                       { }

                document.getElementById("divTitle").innerText = "销售退货管理--入库";
                document.getElementById("divInDate").style.display = "inline";
                document.getElementById("divInUserID").style.display = "inline";
                //默认的入库人和入库时间显示
                document.getElementById("UserInUserID").value = document.getElementById("UserName").value;
                document.getElementById("HidInUserID").value = document.getElementById("UserID").value;
                var InDate = document.getElementById("SystemTime2").value;
                var InDate1 = InDate.split(' ')[0];
                var InDatehour = InDate.split(' ')[1].split(':')[0];
                var InDatemin = InDate.split(' ')[1].split(':')[1];
                document.getElementById("txtInDate").value = InDate1;
                document.getElementById("ddlInDateHour").value = InDatehour;
                document.getElementById("ddlInDateMin").value = InDatemin;
                //设置部分控件可用
                document.getElementById('txtInDate').disabled = false;
                document.getElementById('ddlInDateHour').disabled = false;
                document.getElementById('ddlInDateMin').disabled = false;
                document.getElementById('UserInUserID').disabled = false;
                document.getElementById('txtWairPayTotal').disabled = false;
                document.getElementById('txtSettleTotal').disabled = false;
            }
            else if (IsBusiStatusName == "结算") {
                try {


                    //按扭控制
                    try {
                        document.getElementById('btnGetGoods').style.display = 'none';
                        document.getElementById('unbtnGetGoods').style.display = '';
                    }
                    catch (e) { }
                    document.getElementById("imgSave").style.display = "none";
                    document.getElementById("imgUnSave").style.display = "none";
                    document.getElementById("imgAdd").style.display = "none";
                    document.getElementById("imgUnAdd").style.display = "none";
                    document.getElementById("imgDel").style.display = "none";
                    document.getElementById("imgUnDel").style.display = "none";
                    document.getElementById("Get_Potential").style.display = "none";
                    document.getElementById("Get_UPotential").style.display = "none";
                    document.getElementById("btn_confirm").style.display = "none";
                    document.getElementById("btn_ruku").style.display = "none";
                    document.getElementById("btn_jiesuan").style.display = "inline";
                    document.getElementById("btn_Unconfirm").style.display = "none";

                }
                catch (e)
                       { }

                document.getElementById("divTitle").innerText = "销售退货管理--结算";
                document.getElementById("divSttlDate").style.display = "inline";
                document.getElementById("divSttlUserID").style.display = "inline";
                document.getElementById("divInDate").style.display = "inline";
                document.getElementById("divInUserID").style.display = "inline";
                //默认的结算人和结算时间显示
                document.getElementById("UserSttlUserID").value = document.getElementById("UserName").value;
                document.getElementById("HidSttlUserID").value = document.getElementById("UserID").value;
                var SttlDate = document.getElementById("SystemTime2").value;
                var SttlDate1 = SttlDate.split(' ')[0];
                var SttlDatehour = SttlDate.split(' ')[1].split(':')[0];
                var SttlDatemin = SttlDate.split(' ')[1].split(':')[1];
                document.getElementById("txtSttlDate").value = SttlDate1;
                document.getElementById("ddlSttlDateHour").value = SttlDatehour;
                document.getElementById("ddlSttlDateMin").value = SttlDatemin;
                //设置部分控件可用
                document.getElementById('txtWairPayTotal').disabled = false;
                document.getElementById('txtSettleTotal').disabled = false;
            }
            else if (IsBusiStatusName == "完成") {
                document.getElementById("divTitle").innerText = "销售退货管理--完成";
                $(":text").each(function() {
                    this.disabled = true;
                });
                try {
                    //按扭控制
                    try {
                        document.getElementById('btnGetGoods').style.display = 'none';
                        document.getElementById('unbtnGetGoods').style.display = '';
                    }
                    catch (e) { }
                    document.getElementById("imgSave").style.display = "none";
                    document.getElementById("imgUnSave").style.display = "none";
                    document.getElementById("imgAdd").style.display = "none";
                    document.getElementById("imgUnAdd").style.display = "none";
                    document.getElementById("imgDel").style.display = "none";
                    document.getElementById("imgUnDel").style.display = "none";
                    document.getElementById("Get_Potential").style.display = "none";
                    document.getElementById("Get_UPotential").style.display = "none";
                    document.getElementById("btn_confirm").style.display = "none";
                    document.getElementById("btn_ruku").style.display = "none";
                    document.getElementById("btn_jiesuan").style.display = "none";
                    document.getElementById("btn_Unconfirm").style.display = "none";
                }
                catch (e)
                        { }
            }
        }
        else {
            try {

                //按扭控制                    
                try {
                    document.getElementById('btnGetGoods').style.display = '';
                    document.getElementById('unbtnGetGoods').style.display = 'none';
                }
                catch (e) { }
                document.getElementById("imgSave").style.display = "inline";
                document.getElementById("imgUnSave").style.display = "none";
                document.getElementById("imgAdd").style.display = "inline";
                document.getElementById("imgUnAdd").style.display = "none";
                document.getElementById("imgDel").style.display = "inline";
                document.getElementById("imgUnDel").style.display = "none";
                document.getElementById("btn_confirm").style.display = "inline";
                document.getElementById("btn_Unconfirm").style.display = "none";
            }
            catch (e)
                    { }
        }

    }
    //    }
}

//返回
function Back() {
    var URLParams = document.getElementById("hidSearchCondition").value;
    URLParams = URLParams.replace("ModuleID=2121203", "ModuleID=2121204");

    window.location.href = 'SubSellBackList.aspx?' + URLParams;
}

function GetSubSellBack(ID) {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/SubStoreManager/SubSellBackEdit.ashx", //目标地址
        data: "ID=" + ID + "",
        cache: false,
        beforeSend: function() { AddPop(); }, //发送数据之前           
        success: function(msg) {
            //数据获取完毕，填充页面据显示
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    //基本信息
                    $("#CodingRuleControl1_txtCode").attr("value", item.BackNo); //编号
                    $("#divSubSellBackNo").attr("value", item.BackNo); //合同编号
                    $("#txtTitle").attr("value", item.Title); //主题
                    $("#txtDeptName").attr("value", item.DeptName); //分店名称
                    $("#HidDeptID").attr("value", item.DeptID); //分店ID
                    $("#HidDeptID").attr("value", item.DeptID); //部门id
                    $("#drpFromType").attr("value", item.FromType); //源单类型
                    $("#txtOrderID").attr("value", item.OrderNo); //对应订单编号
                    $("#HidOrderID").attr("value", item.OrderID); //对应订单ID
                    $("#ddlSendMode").attr("value", item.SendMode); //发货模式
                    if (item.OutDate != "") {//发货时间
                        var OutDate = item.OutDate;
                        var OutDate1 = OutDate.split(' ')[0];
                        var OutDateHour = OutDate.split(' ')[1].split(':')[0];
                        var OutDateMin = OutDate.split(' ')[1].split(':')[1];
                        document.getElementById("txtOutDate").value = OutDate1;
                        document.getElementById("ddlOutDateHour").value = OutDateHour;
                        document.getElementById("ddlOutDateMin").value = OutDateMin;
                    }
                    $("#UserOutUserID").attr("value", item.OutUserIDName); //发货人名称
                    $("#HidOutUserID").attr("value", item.OutUserID); //发货人ID
                    if (item.isAddTax == 1) {
                        document.getElementById("chkisAddTax").checked = true;
                        $("#labAddTax").html("是增值税");
                    }
                    else {
                        document.getElementById("chkisAddTax").checked = false;
                        $("#labAddTax").html("非增值税");
                    }
                    $("#txtCustName").attr("value", item.CustName); //客户名称
                    $("#txtCustTel").attr("value", item.CustTel); //联系电话
                    $("#txtCustMobile").attr("value", item.CustMobile); //客户手机号
                    $("#drpCurrencyType").attr("title", item.CurrencyTypeName); //币种下拉框显示
                    $("#txtRate").attr("value", item.Rate); //汇率
                    for (var i = 0; i < document.getElementById("drpCurrencyType").options.length; ++i) {
                        if (document.getElementById("drpCurrencyType").options[i].value.split('_')[0] == item.CurrencyType) {
                            $("#drpCurrencyType").attr("selectedIndex", i);
                            break;
                        }
                    }
                    $("#drpBusiStatus").attr("value", item.BusiStatus); //业务状态
                    if (item.BusiStatus == "2") {
                        document.getElementById("divTitle").innerText = "销售退货管理--入库";
                    }
                    if (item.BusiStatus == "3") {
                        document.getElementById("divTitle").innerText = "销售退货管理--结算";
                    }
                    if (item.BusiStatus == "4") {
                        document.getElementById("divTitle").innerText = "销售退货管理--完成";
                    }
                    if (item.BackDate != "") {//退货时间
                        var BackDate = item.BackDate;
                        var BackDate1 = BackDate.split(' ')[0];
                        var BackDateHour = BackDate.split(' ')[1].split(':')[0];
                        var BackDateMin = BackDate.split(' ')[1].split(':')[1];
                        document.getElementById("txtBackDate").value = BackDate1;
                        document.getElementById("ddlBackDateHour").value = BackDateHour;
                        document.getElementById("ddlBackDateMin").value = BackDateMin;
                    }
                    $("#UserSeller").attr("value", item.SellerName); //退货处理人名称
                    $("#HidSeller").attr("value", item.Seller); //退货处理人id
                    if (item.InDate != "") {//入库时间
                        var InDate = item.InDate;
                        var InDate1 = InDate.split(' ')[0];
                        var InDateHour = InDate.split(' ')[1].split(':')[0];
                        var InDateMin = InDate.split(' ')[1].split(':')[1];
                        document.getElementById("txtInDate").value = InDate1;
                        document.getElementById("ddlInDateHour").value = InDateHour;
                        document.getElementById("ddlInDateMin").value = InDateMin;
                    }
                    if (item.InUserIDName != "") {
                        $("#UserInUserID").attr("value", item.InUserIDName); //入库人名称
                        $("#HidInUserID").attr("value", item.InUserID); //入库人ID 
                    }
                    if (item.SttlDate != "") {//结算时间
                        var SttlDate = item.SttlDate;
                        var SttlDate1 = SttlDate.split(' ')[0];
                        var SttlDateHour = SttlDate.split(' ')[1].split(':')[0];
                        var SttlDateMin = SttlDate.split(' ')[1].split(':')[1];
                        document.getElementById("txtSttlDate").value = SttlDate1;
                        document.getElementById("ddlSttlDateHour").value = SttlDateHour;
                        document.getElementById("ddlSttlDateMin").value = SttlDateMin;
                    }
                    if (item.SttlUserIDName != "") {
                        $("#UserSttlUserID").attr("value", item.SttlUserIDName); //结算人名称
                        $("#HidSttlUserID").attr("value", item.SttlUserID); //结算人ID
                    }
                    $("#txtCustAddr").attr("value", item.CustAddr); //客户地址
                    $("#txtBackReason").attr("value", item.BackReason); //退货理由描述

                    //合计信息
                    $("#txtCountTotal").attr("value", FormatAfterDotNumber(item.CountTotal, parseInt($("#hidSelPoint").val()))); //退货数量合计
                    $("#txtTotalMoney").attr("value", FormatAfterDotNumber(item.TotalPrice, parseInt($("#hidSelPoint").val()))); //金额合计
                    $("#txtTotalTaxHo").attr("value", FormatAfterDotNumber(item.Tax, parseInt($("#hidSelPoint").val()))); //税额合计
                    $("#txtTotalFeeHo").attr("value", FormatAfterDotNumber(item.TotalFee, parseInt($("#hidSelPoint").val()))); //含税金额合计
                    $("#txtDiscount").attr("value", FormatAfterDotNumber(item.Discount, parseInt($("#hidSelPoint").val()))); //整单折扣
                    $("#txtDiscountTotal").attr("value", FormatAfterDotNumber(item.DiscountTotal, parseInt($("#hidSelPoint").val()))); //折扣金额
                    $("#txtRealTotal").attr("value", FormatAfterDotNumber(item.RealTotal, parseInt($("#hidSelPoint").val()))); //折后含税金额
                    $("#txtWairPayTotal").attr("value", FormatAfterDotNumber(item.WairPayTotal, parseInt($("#hidSelPoint").val()))); //应退款金额 
                    $("#txtPayedTotal").attr("value", FormatAfterDotNumber(item.PayedTotal, parseInt($("#hidSelPoint").val()))); //已退款金额
                    $("#txtWairPayTotalOverage").attr("value", FormatAfterDotNumber(item.WairPayTotalOverage, parseInt($("#hidSelPoint").val()))); //应退货款金额

                    //备注信息
                    $("#txtCreator").attr("value", item.Creator); //制单人id
                    $("#txtCreatorReal").attr("value", item.CreatorName); //制单人名称
                    $("#txtCreateDate").attr("value", item.CreateDate); //制单时间
                    $("#ddlBillStatus").attr("value", item.BillStatus); //单据状态
                    $("#txtConfirmor").attr("value", item.Confirmor); //确认人id
                    $("#txtConfirmorReal").attr("value", item.ConfirmorName); //确认人名称
                    $("#txtConfirmDate").attr("value", item.ConfirmDate); //确认时间
                    $("#txtCloser").attr("value", item.Closer); //结单人id
                    $("#txtCloserReal").attr("value", item.CloserName); //结单人名称
                    $("#txtCloseDate").attr("value", item.CloseDate); //结单日期
                    $("#txtModifiedUserID").attr("value", item.ModifiedUserID); //最后更新人id
                    $("#txtModifiedUserIDReal").attr("value", item.ModifiedUserID); //最后更新人id显示
                    $("#txtModifiedDate").attr("value", item.ModifiedDate); //最后更新时间
                    $("#hfPageAttachment").attr("value", item.Attachment); //附件
                    $("#txtRemark").attr("value", item.Remark); //备注
                    if (item.Attachment != "") {
                        //下载删除显示
                        document.getElementById("divDealResume").style.display = "block";
                        //上传不显示
                        document.getElementById("divUploadResume").style.display = "none"
                    }

                    document.getElementById("divSubSellBackNo").innerHTML = item.BackNo;
                    document.getElementById("divSubSellBackNo").style.display = "block";
                    document.getElementById("divInputNo").style.display = "none";
                    // 填充扩展属性
                    GetExtAttr("officedba.SubSellBack", item);
                }

            });
            $.each(msg.data2, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    var index = AddSignRow();
                    $("#txtProductID" + index).attr("value", item.ProductID);
                    $("#txtProductNo" + index).attr("value", item.ProductNo);
                    $("#txtProductName" + index).attr("value", item.ProductName);
                    $("#txtstandard" + index).attr("value", item.standard);
                    $("#txtUnitID" + index).attr("value", item.UnitID);
                    $("#txtUnitName" + index).attr("value", item.UnitName);
                    var btshuliang = item.ProductCount;
                    if (btshuliang == 0.00) {
                        $("#txtProductCount" + index).attr("value", "");
                    }
                    else {
                        $("#txtProductCount" + index).attr("value", parseFloat(item.ProductCount).toFixed(2));
                    }
                    $("#txtYBackCount" + index).attr("value", FormatAfterDotNumber(item.YBackCount, parseInt($("#hidSelPoint").val())));
                    $("#txtBackCount" + index).attr("value", FormatAfterDotNumber(item.BackCount, parseInt($("#hidSelPoint").val())));
                    $("#txtUnitPrice" + index).attr("value", FormatAfterDotNumber(item.UnitPrice, parseInt($("#hidSelPoint").val())));
                    $("#txtTaxPrice" + index).attr("value", FormatAfterDotNumber(item.TaxPrice, parseInt($("#hidSelPoint").val())));
                    $("#hiddTaxPrice" + index).attr("value", FormatAfterDotNumber(item.TaxPrice, parseInt($("#hidSelPoint").val())));
                    $("#txtDiscount" + index).attr("value", FormatAfterDotNumber(item.Discount, parseInt($("#hidSelPoint").val())));
                    $("#txtTaxRate" + index).attr("value", FormatAfterDotNumber(item.TaxRate, parseInt($("#hidSelPoint").val())));
                    $("#hiddTaxRate" + index).attr("value", FormatAfterDotNumber(item.TaxRate, parseInt($("#hidSelPoint").val())));
                    $("#txtTotalPrice" + index).attr("value", FormatAfterDotNumber(item.TotalPrice, parseInt($("#hidSelPoint").val())));
                    $("#txtTotalFee" + index).attr("value", FormatAfterDotNumber(item.TotalFee, parseInt($("#hidSelPoint").val())));
                    $("#txtTotalTax" + index).attr("value", FormatAfterDotNumber(item.TotalTax, parseInt($("#hidSelPoint").val())));
                    $("#txtStorageID" + index).attr("value", item.StorageID);
                    $("#txtFromBillID" + index).attr("value", item.FromBillID);
                    $("#txtFromBillNo" + index).attr("value", item.FromBillNo);
                    $("#txtFromLineNo" + index).attr("value", item.FromLineNo);
                    $("#txtRemark" + index).attr("value", item.Remark);
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        $("#txtUsedPrice" + index).attr("value", FormatAfterDotNumber(item.UsedPrice, parseInt($("#hidSelPoint").val())));
                        $("#txtUsedUnitCount" + index).attr("value", FormatAfterDotNumber(item.UsedUnitCount, parseInt($("#hidSelPoint").val())));
                    }
                    GetUnitGroupSelect(item.ProductID, 'SaleUnit', 'txtUsedUnit' + index, 'ChangeUnit(this,' + index + ')', "td_UsedUnitID_" + index, item.UsedUnitID);
                    SetSubBatchSelect(index, item.ProductID, item.IsBatchNo, item.BatchNo)

                }
            });
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
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





//新建门店销售退货单
function InsertSubSellBack() {

    if (!CheckBaseInfo()) {
        return false;
    }

    var action = null;

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var backNo = "";
    var CodeType = $("#CodingRuleControl1_ddlCodeRule").val();

    //基本信息

    if (CodeType == "") {
        backNo = $("#CodingRuleControl1_txtCode").val(); //单据编号
    }
    var title = document.getElementById('txtTitle').value; //单据主题
    var deptID = $("#HidDeptID").val(); //销售分店

    var orderID = $("#HidOrderID").val(); //对应销售订单编号id
    if (orderID == "") {
        orderID = 0;
    }
    var sendMode = $("#ddlSendMode").val(); //发货模式
    var fromType = $("#drpFromType").val(); //源单类型
    var outDate = $("#txtOutDate").val() + " " + $("#ddlOutDateHour").val() + ":" + $("#ddlOutDateMin").val(); //发货时间
    var outUserID = $("#HidOutUserID").val(); //发货人ID

    var isAddtax = ""; //是否增值税
    if ($("#chkisAddTax").attr("checked")) {
        isAddtax = 1;
    }
    else {
        isAddtax = 0;
    }
    var custName = $("#txtCustName").val(); //客户名称
    var custTel = $("#txtCustTel").val(); //客户联系电话
    var custMobile = $("#txtCustMobile").val(); //客户手机号 
    var currencyType = $("#CurrencyTypeID").val(); //币种取隐藏域的值
    var rate = $("#txtRate").val(); //汇率
    var busiStatus = $("#drpBusiStatus").val(); //业务状态
    var backDate = $("#txtBackDate").val() + " " + $("#ddlBackDateHour").val() + ":" + $("#ddlBackDateMin").val(); //退货时间
    var seller = $("#HidSeller").val(); //退货处理人
    var inDate = $("#txtInDate").val() + " " + $("#ddlInDateHour").val() + ":" + $("#ddlInDateMin").val(); //入库时间
    var inUserID = $("#HidInUserID").val(); //入库人
    var sttlDate = $("#txtSttlDate").val() + " " + $("ddlSttlDateHour").val() + ":" + $("#ddlSttlDateMin").val(); //结算时间
    var sttlUserID = $("#HidSttlUserID").val(); //结算人
    var custAddr = $("#txtCustAddr").val(); //客户地址
    var backReason = $("#txtBackReason").val(); //退货理由描述


    //合计信息
    var countTotal = $("#txtCountTotal").val(); //退货数量合计
    var totalMoney = $("#txtTotalMoney").val(); //金额合计
    var totalTax = $("#txtTotalTaxHo").val(); //税额合计
    var totalfee = document.getElementById('txtTotalFeeHo').value; //含税金额合计
    var discount = document.getElementById('txtDiscount').value; //整单折扣(%）合计
    var discounttotal = document.getElementById('txtDiscountTotal').value; //折扣金额合计
    var realtotal = document.getElementById('txtRealTotal').value; //折后含税金额合计
    if (document.getElementById("txtSettleTotal").value == "") {
        document.getElementById("txtSettleTotal").value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
    }
    var payedTotal = parseFloat(document.getElementById('txtSettleTotal').value) + parseFloat(document.getElementById('txtPayedTotal').value); //已退款金额 +已付款金额(第一次默认为0)
    var wairPayTotal = document.getElementById('txtWairPayTotal').value.Trim(); //应退款金额
    if (wairPayTotal == "") {
        wairPayTotal = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
    }

    //备注信息
    var creator = $("#txtCreator").val(); //制单人
    var createDate = $("#txtCreateDate").val(); //制单时间
    var billStatus = $("#ddlBillStatus").val(); //单据状态
    var confirmor = $("#txtConfirmor").val(); //确认人
    var confirmDate = $("#txtConfirmDate").val(); //确认时间
    var closer = $("#txtCloser").val(); //结单人
    var closeDate = $("#txtCloseDate").val(); //结单日期
    var modifiedUserID = $("#txtModifiedUserID").val(); //最后更新人
    var attachment = document.getElementById("hfPageAttachment").value.replace(/\\/g, "\\\\"); //附件
    var remark = $("#txtRemark").val(); //备注
    if (document.getElementById("txtAction").value == "1") {
        action = "Add";
    }
    else {
        action = "Update";
    }




    var DetailID = new Array();
    var Detailchk = new Array();
    var DetailProductID = new Array();
    var DetailProductNo = new Array();
    var DetailProductName = new Array();
    var DetailUnitID = new Array();
    var DetailProductCount = new Array();
    var DetailBackCount = new Array();
    var DetailUnitPrice = new Array();
    var DetailTaxPrice = new Array();
    var DetailDiscount = new Array();
    var DetailTaxRate = new Array();
    var DetailTotalPrice = new Array();
    var DetailTotalFee = new Array();
    var DetailTotalTax = new Array();
    var DetailStorageID = new Array();
    var DetailFromBillID = new Array();
    var DetailFromBillNo = new Array();
    var DetailFromLineNo = new Array();
    var DetailRemark = new Array();
    var DetailUsedUnitID = new Array();
    var DetailUsedUnitCount = new Array();
    var DetailUsedPrice = new Array();
    var DetailExRate = new Array();
    var DetailBatchNo = new Array();

    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var length = 0;
    var signFrame = findObj("dg_Log", document);
    for (var i = 0; i < txtTRLastIndex - 1; i++) {

        if (signFrame.rows[i + 1].style.display != "none") {

            var objID = 'ID' + (i + 1);
            var objchk = 'chk' + (i + 1);
            var objProductID = 'txtProductID' + (i + 1);
            var objProductNo = 'txtProductNo' + (i + 1);
            var objProductName = 'txtProductName' + (i + 1);
            var objUnitID = 'txtUnitID' + (i + 1);
            var objProductCount = 'txtProductCount' + (i + 1);
            var objBackCount = 'txtBackCount' + (i + 1);
            var objUnitPrice = 'txtUnitPrice' + (i + 1);
            var objTaxPrice = 'txtTaxPrice' + (i + 1);
            var objDiscount = 'txtDiscount' + (i + 1);
            var objTaxRate = 'txtTaxRate' + (i + 1);
            var objTotalPrice = 'txtTotalPrice' + (i + 1);
            var objTotalFee = 'txtTotalFee' + (i + 1);
            var objTotalTax = 'txtTotalTax' + (i + 1);
            var objStorageID = 'txtStorageID' + (i + 1);
            var objFromBillID = 'txtFromBillID' + (i + 1);
            var objFromBillNo = 'txtFromBillNo' + (i + 1);
            var objFromLineNo = 'txtFromLineNo' + (i + 1);
            var objRemark = 'txtRemark' + (i + 1);
            var objUsedUnitID = 'txtUsedUnit' + (i + 1);
            var objUsedUnitCount = 'txtUsedUnitCount' + (i + 1);
            var objUsedPrice = 'txtUsedPrice' + (i + 1);

            Detailchk.push(document.getElementById(objchk).value);
            DetailProductID.push(document.getElementById(objProductID.toString()).value);
            DetailProductNo.push(document.getElementById(objProductNo.toString()).value);
            DetailProductName.push(document.getElementById(objProductName.toString()).value);
            DetailUnitID.push(document.getElementById(objUnitID.toString()).value);
            DetailProductCount.push(document.getElementById(objProductCount.toString()).value);
            DetailBackCount.push(document.getElementById(objBackCount.toString()).value);
            DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value);
            DetailTaxPrice.push(document.getElementById(objTaxPrice.toString()).value);
            DetailDiscount.push(document.getElementById(objDiscount.toString()).value);
            DetailTaxRate.push(document.getElementById(objTaxRate.toString()).value);
            DetailTotalPrice.push(document.getElementById(objTotalPrice.toString()).value);
            DetailTotalFee.push(document.getElementById(objTotalFee.toString()).value);
            DetailTotalTax.push(document.getElementById(objTotalTax.toString()).value);
            DetailStorageID.push(document.getElementById(objStorageID.toString()).value);
            DetailFromBillID.push(document.getElementById(objFromBillID.toString()).value);
            DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value);
            DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value);
            DetailRemark.push(document.getElementById(objRemark.toString()).value);
            DetailBatchNo.push(document.getElementById('selBatch' + (i + 1)).value);
            //计量单位开启
            if ($("#txtIsMoreUnit").val() == "1") {
                DetailUsedUnitID.push(document.getElementById(objUsedUnitID.toString()).value.split('|')[0]);
                DetailUsedUnitCount.push(document.getElementById(objUsedUnitCount.toString()).value);
                DetailUsedPrice.push(document.getElementById(objUsedPrice.toString()).value);
                DetailExRate.push(document.getElementById(objUsedUnitID.toString()).value.split('|')[1]);
            }
            else {
                DetailUsedUnitID.push('0');
                DetailUsedUnitCount.push('0');
                DetailUsedPrice.push('0');
                DetailExRate.push('0');
            }

            length++;
        }
    }
    if (backNo.length <= 0) {
        isFlag = false;
        fieldText = fieldText + "单据编号|";
        msgText = msgText + "单据编号不允许为空|";
    }


    var no = document.getElementById("divSubSellBackNo").innerHTML;
    var txtIndentityID = $("#txtIndentityID").val();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SubStoreManager/SubSellBackAdd.ashx",
        dataType: 'json', //返回json格式数据
        data: "backNo=" + escape(backNo) + "&title=" + escape(title) + "&fromType=" + escape(fromType) + "&deptID=" + escape(deptID) + "&orderID=" + escape(orderID)
                        + "&sendMode=" + escape(sendMode) + "&outDate=" + escape(outDate) + "&outUserID=" + escape(outUserID) + "&isAddtax=" + escape(isAddtax) + "&custName=" + escape(custName)
                        + "&custTel=" + escape(custTel) + "&custMobile=" + escape(custMobile) + "&currencyType=" + escape(currencyType) + "&rate=" + escape(rate) + "&busiStatus=" + escape(busiStatus)
                        + "&backDate=" + escape(backDate) + "&seller=" + escape(seller) + "&custAddr=" + escape(custAddr) + "&backReason=" + escape(backReason) + "&countTotal=" + escape(countTotal)
                        + "&totalMoney=" + escape(totalMoney) + "&totalTax=" + escape(totalTax) + "&totalfee=" + escape(totalfee) + "&discount=" + escape(discount) + "&discounttotal=" + escape(discounttotal)
                        + "&realtotal=" + escape(realtotal) + "&payedTotal=" + escape(payedTotal) + "&wairPayTotal=" + escape(wairPayTotal) + "&creator=" + escape(creator) + "&createDate=" + (createDate)
                        + "&billStatus=" + escape(billStatus) + "&confirmor=" + escape(confirmor) + "&confirmDate=" + escape(confirmDate) + "&closer=" + escape(closer) + "&closeDate=" + escape(closeDate)
                        + "&modifiedUserID=" + escape(modifiedUserID) + "&attachment=" + escape(attachment) + "&remark=" + escape(remark) + "&DetailProductID=" + escape(DetailProductID) + "&DetailProductNo=" + escape(DetailProductNo)
                        + "&DetailProductName=" + escape(DetailProductName) + "&DetailUnitID=" + escape(DetailUnitID) + "&DetailProductCount=" + escape(DetailProductCount) + "&DetailBackCount=" + escape(DetailBackCount) + "&DetailUnitPrice=" + escape(DetailUnitPrice) + "&DetailTaxPrice=" + escape(DetailTaxPrice)
                        + "&DetailDiscount=" + escape(DetailDiscount) + "&DetailTaxRate=" + escape(DetailTaxRate) + "&DetailTotalPrice=" + escape(DetailTotalPrice) + "&DetailTotalFee=" + escape(DetailTotalFee) + "&DetailTotalTax=" + escape(DetailTotalTax)
                        + "&DetailUsedUnitID=" + DetailUsedUnitID + "&DetailUsedUnitCount=" + DetailUsedUnitCount + "&DetailUsedPrice=" + DetailUsedPrice + "&DetailExRate=" + DetailExRate
                        + "&DetailStorageID=" + escape(DetailStorageID) + "&DetailRemark=" + escape(DetailRemark) + "&action=" + escape(action) + "&CodeType=" + escape(CodeType) + "&length=" + escape(length) + "&cno=" + escape(no)
                        + "&inDate=" + escape(inDate) + "&inUserID=" + escape(inUserID) + "&sttlDate=" + escape(sttlDate) + "&sttlUserID=" + escape(sttlUserID)
                        + "&DetailBatchNo=" + escape(DetailBatchNo) + "&DetailFromBillID=" + escape(DetailFromBillID) + "&DetailFromBillNo=" + escape(DetailFromBillNo) + "&DetailFromLineNo=" + escape(DetailFromLineNo) + "&ID=" + escape(txtIndentityID) + "" + GetExtAttrValue(), //数据

        cache: false,
        beforeSend: function() {
            //AddPop();
        },
        error: function() {


        },
        success: function(data) {
            if (data.sta > 0) {
                $("#CodingRuleControl1_txtCode").val(data.data);
                $("#CodingRuleControl1_txtCode").attr("readonly", "readonly");
                $("#CodingRuleControl1_ddlCodeRule").attr("disabled", "false");

                if (action == "Add") {
                    popMsgObj.ShowMsg(data.info);
                    document.getElementById('txtIndentityID').value = data.sta;

                    document.getElementById("txtAction").value = "2";
                    if (CodeType != "") {
                        isnew = "edit";
                        document.getElementById("divSubSellBackNo").innerHTML = data.data;
                        document.getElementById("divSubSellBackNo").style.display = "block";
                        document.getElementById("divInputNo").style.display = "none";

                        //设置源单类型不可改
                        $("#drpFromType").attr("disabled", "disabled");

                        var no1 = parseFloat(document.getElementById("txtPayedTotal").value.Trim()); //已退货款
                        var no2 = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
                        document.getElementById("txtSettleTotal").value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
                        document.getElementById("txtPayedTotal").value = FormatAfterDotNumber(no1 + no2, parseInt($("#hidSelPoint").val()));

                        document.getElementById("btn_confirm").style.display = "inline";
                        document.getElementById("btn_Unconfirm").style.display = "none";
                    }
                    else {
                        document.getElementById("divSubSellBackNo").innerHTML = data.data;
                        document.getElementById("divSubSellBackNo").style.display = "block";
                        document.getElementById("divInputNo").style.display = "none";

                        //设置源单类型不可改
                        $("#drpFromType").attr("disabled", "disabled");

                        var no1 = parseFloat(document.getElementById("txtPayedTotal").value.Trim()); //已退货款
                        var no2 = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
                        document.getElementById("txtSettleTotal").value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
                        document.getElementById("txtPayedTotal").value = FormatAfterDotNumber(no1 + no2, parseInt($("#hidSelPoint").val()));

                        document.getElementById("btn_confirm").style.display = "inline";
                        document.getElementById("btn_Unconfirm").style.display = "none";

                    }

                }
                else {
                    popMsgObj.ShowMsg(data.info);
                    document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedUserIDReal").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;

                    var no1 = parseFloat(document.getElementById("txtPayedTotal").value.Trim()); //已退货款
                    var no2 = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
                    document.getElementById("txtSettleTotal").value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
                    document.getElementById("txtPayedTotal").value = FormatAfterDotNumber(no1 + no2, parseInt($("#hidSelPoint").val()));

                    document.getElementById("btn_confirm").style.display = "inline";
                    document.getElementById("btn_Unconfirm").style.display = "none";
                }

            }
            else {
                hidePopup();
                popMsgObj.ShowMsg(data.info);
            }
        }
    });

    return true;

}

function Fun_Clear_Input() {
    action = "Add";
    window.location = 'PurchaseContract_Add.aspx';
}


function Isquanxuan() {
    var signFrame = findObj("dg_Log", document);
    var quanxuan = true;
    for (var i = 1; i < signFrame.rows.length; ++i) {
        if (document.getElementById("chk" + i).checked == false) {
            quanxuan = false;
        }
    }
    if (quanxuan) {
        document.getElementById("checkall").checked = true;
    }
    else {
        document.getElementById("checkall").checked = false;
    }
}


///添加行
function AddSignRow() {
    //读取最后一行的行号，存放在txtTRLastIndex文本框中 
    var Flag1 = document.getElementById("ddlSendMode").value;
    var Flag2 = document.getElementById("drpFromType").value;

    var hidIsliebiao = document.getElementById("hidIsliebiao").value //为1表示从列表页面过来
    var BillStatusName = document.getElementById("hidBillStatusName").value; //单据状态
    var IsliebiaoSendMode = document.getElementById("hidIsliebiaoSendMode").value; //发货模式
    var IsliebiaoFromType = document.getElementById("hidIsliebiaoFromType").value; //源单类型

    var txtTRLastIndex = findObj("txtTRLastIndex", document);

    var rowID = parseInt(txtTRLastIndex.value);
    var signFrame = findObj("dg_Log", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = "ID" + rowID;
    var i = 0;

    var newNameXH = newTR.insertCell(i++); //添加列:选择
    newNameXH.className = "tdColInputCenter";
    newNameXH.innerHTML = "<input name='chk' id='chk" + rowID + "' size='10' value=" + rowID + " type='checkbox'   onclick=\"Isquanxuan();\"  />";

    var newNameTD = newTR.insertCell(i++); //添加列:序号
    newNameTD.className = "cell";
    newNameTD.id = "newNameTD" + rowID;
    newNameTD.innerHTML = GenerateNo(rowID);


    var newProductNo = newTR.insertCell(i++); //添加列:物品ID
    newProductNo.style.display = "none";
    newProductNo.innerHTML = "<input name='txtProductID" + rowID + "'  id='txtProductID" + rowID + "' type='text' class='tdinput' style='width:90%'  />"; //添加列内容

    if (hidIsliebiao == "1") {
        if (BillStatusName == "制单") {//判断从列表页面传来的源单类型
            if (IsliebiaoFromType == "0") {//无来源
                var newProductNo = newTR.insertCell(i++); //添加列:物品编号popProductInfoSubUC.ShowList
                newProductNo.className = "cell";
                newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' readonly   onclick=\"GetProduct(" + rowID + ")\"  type='text' class='tdinput' style='width:90%'  readonly />"; //添加列内容
            }
            else {//销售订单
                var newProductNo = newTR.insertCell(i++); //添加列:物品编号
                newProductNo.className = "cell";
                newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' type='text' class='tdinput' style='width:90%'  readonly disabled/>"; //添加列内容
            }
        }
        else if (BillStatusName == "") {
            var newProductNo = newTR.insertCell(i++); //添加列:物品编号
            newProductNo.className = "cell";
            if (Flag2 == "1") {
                newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' type='text' class='tdinput' style='width:90%'  readonly disabled/>"; //添加列内容
            }
            else if (Flag2 != "1") {
                newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' readonly  onclick=\"GetProduct(" + rowID + ")\"  type='text' class='tdinput' style='width:90%'  readonly />"; //添加列内容
            }
        }
        else {
            var newProductNo = newTR.insertCell(i++); //添加列:物品编号
            newProductNo.className = "cell";
            newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' type='text' class='tdinput' style='width:90%'  readonly disabled/>"; //添加列内容
        }
    }
    else {
        var newProductNo = newTR.insertCell(i++); //添加列:物品编号
        newProductNo.className = "cell";
        if (Flag2 == "1") {
            newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' type='text' class='tdinput' style='width:90%'  readonly disabled/>"; //添加列内容
        }
        else if (Flag2 != "1") {
            newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' readonly onclick=\"GetProduct(" + rowID + ")\"  type='text' class='tdinput' style='width:90%'  readonly />"; //添加列内容
        }
    }

    var newProductName = newTR.insertCell(i++); //添加列:物品名称
    newProductName.className = "cell";
    newProductName.innerHTML = "<input name='txtProductName" + rowID + "' id='txtProductName" + rowID + "' type='text'  class='tdinput' style='width:90%'  readonly/>"; //添加列内容

    var newselBatch = newTR.insertCell(i++); //添加列:批次
    newselBatch.className = "cell";
    newselBatch.innerHTML = "<select id='selBatch" + rowID + "' class='tdinput' ></select>";

    var newProductName = newTR.insertCell(i++); //添加列:规格(从物品表中带来显示，不往明细表中存)
    newProductName.className = "cell";
    newProductName.innerHTML = "<input name='txtstandard" + rowID + "' id='txtstandard" + rowID + "' type='text'  class='tdinput' style='width:90%'  readonly/>"; //添加列内容

    var newUnitID = newTR.insertCell(i++); //添加列:单位ID
    newUnitID.style.display = "none";
    newUnitID.innerHTML = "<input name='txtUnitID" + rowID + "' id='txtUnitID" + rowID + "'style='width:10%' type='text'  class='tdinput' readonly />"; //添加列内容

    var newUnitID = newTR.insertCell(i++); //添加列:单位
    newUnitID.className = "cell";
    newUnitID.innerHTML = "<input name='txtUnitName" + rowID + "' id='txtUnitName" + rowID + "' type='text' class='tdinput' style='width:90%' onclick=\"popUnitObj.ShowList(" + rowID + ");\"  readonly disabled/>"; //添加列内容

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        var newBackCount = newTR.insertCell(i++); //添加列:退货数量
        newBackCount.className = "cell";
        newBackCount.innerHTML = "<input name='txtBackCount" + rowID + "'  id='txtBackCount" + rowID + "'  class='tdinput' style='width:90%'  readonly  type='text'/>"; //添加列内容

        var newUnitName = newTR.insertCell(i++); //添加列:单位
        newUnitName.className = "cell";
        newUnitName.id = "td_UsedUnitID_" + rowID;
        newUnitName.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;";

    }

    var newProductCount = newTR.insertCell(i++); //添加列:发货数量
    newProductCount.className = "cell";
    newProductCount.innerHTML = "<input name='txtProductCount" + rowID + "'  id='txtProductCount" + rowID + "'class='tdinput' style='width:90%'  readonly type='text'/>"; //添加列内容

    var newYBackCount = newTR.insertCell(i++); //添加列:已退货数量

    newYBackCount.style.display = "none";
    newYBackCount.innerHTML = "<input name='txtYBackCount" + rowID + "'  id='txtYBackCount" + rowID + "'class='tdinput' style='width:90%'  readonly type='text'/>"; //添加列内容

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        if (hidIsliebiao != "1" || BillStatusName == "制单" || BillStatusName == "") {
            //判断从列表页面传来的源单类型
            var newBackCount = newTR.insertCell(i++); //添加列:退货数量
            newBackCount.className = "cell";
            newBackCount.innerHTML = "<input name='txtUsedUnitCount" + rowID + "'  id='txtUsedUnitCount" + rowID + "'   onchange=\"Number_round(this,2)\"   onblur=\"ChangeUnit(this,'" + rowID + "');\" class='tdinput' style='width:90%'  type='text'/>"; //添加列内容 
        }
        else {
            var newBackCount = newTR.insertCell(i++); //添加列:退货数量
            newBackCount.className = "cell";
            newBackCount.innerHTML = "<input name='txtUsedUnitCount" + rowID + "'  id='txtUsedUnitCount" + rowID + "'  class='tdinput' style='width:90%'  readonly  type='text'/>"; //添加列内容 
        }

    }
    else {
        if (hidIsliebiao != "1" || BillStatusName == "制单" || BillStatusName == "") {   //判断从列表页面传来的源单类型
            var newBackCount = newTR.insertCell(i++); //添加列:退货数量
            newBackCount.className = "cell";
            newBackCount.innerHTML = "<input name='txtBackCount" + rowID + "'  id='txtBackCount" + rowID + "'   onchange=\"Number_round(this," + $("#hidSelPoint").val() + ")\"   onblur=\"fnTotalInfo();\" class='tdinput' style='width:90%'  type='text'/>"; //添加列内容 
        }
        else {
            var newBackCount = newTR.insertCell(i++); //添加列:退货数量
            newBackCount.className = "cell";
            newBackCount.innerHTML = "<input name='txtBackCount" + rowID + "'  id='txtBackCount" + rowID + "'  class='tdinput' style='width:90%'  readonly  type='text'/>"; //添加列内容 
        }
    }
    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        var newUnitPrice = newTR.insertCell(i++); //添加列:单价
        newUnitPrice.className = "cell";
        newUnitPrice.innerHTML = "<input name='txtUnitPrice" + rowID + "' id='txtUnitPrice" + rowID + "' type='hidden' /><input type=\"hidden\"  id='hiddUnitPrice" + rowID + "'/><input name='txtUsedPrice" + rowID + "' id='txtUsedPrice" + rowID + "' type='text'   class='tdinput' style='width:90%'    readonly/>"; //添加列内容
    }
    else {
        var newUnitPrice = newTR.insertCell(i++); //添加列:单价
        newUnitPrice.className = "cell";
        newUnitPrice.innerHTML = "<input name='txtUnitPrice" + rowID + "' id='txtUnitPrice" + rowID + "' type='text'   class='tdinput' style='width:90%'    readonly/><input type=\"hidden\"  id='hiddUnitPrice" + rowID + "'/>"; //添加列内容
    }

    var newUnitPrice = newTR.insertCell(i++); //添加列:含税价
    newUnitPrice.className = "cell";
    newUnitPrice.innerHTML = "<input name='txtTaxPrice" + rowID + "' id='txtTaxPrice" + rowID + "' type='text'   class='tdinput' style='width:90%'    readonly/> <input type=\"hidden\"  id='hiddTaxPrice" + rowID + "'/>"; //添加列内容


    var newUnitPrice = newTR.insertCell(i++); //添加列:折扣
    newUnitPrice.className = "cell";
    newUnitPrice.innerHTML = "<input name='txtDiscount" + rowID + "' id='txtDiscount" + rowID + "' type='text'   class='tdinput' style='width:90%'    readonly/>"; //添加列内容

    var newUnitPrice = newTR.insertCell(i++); //添加列:税率
    newUnitPrice.className = "cell";
    newUnitPrice.innerHTML = "<input name='txtTaxRate" + rowID + "' id='txtTaxRate" + rowID + "' type='text'   class='tdinput' style='width:90%'    readonly/> <input type=\"hidden\"  id='hiddTaxRate" + rowID + "'/>"; //添加列内容

    var newTotalPrice = newTR.insertCell(i++); //添加列:金额
    newTotalPrice.className = "cell";
    newTotalPrice.innerHTML = "<input name='txtTotalPrice" + rowID + "' id='txtTotalPrice" + rowID + "' type='text'  class='tdinput' style='width:90%'   readonly/>"; //添加列内容
    $("#txtTotalPrice" + rowID).onfocus = "TotalPrice1();"

    var newUnitPrice = newTR.insertCell(i++); //添加列:含税金额
    newUnitPrice.className = "cell";
    newUnitPrice.innerHTML = "<input name='txtTotalFee" + rowID + "' id='txtTotalFee" + rowID + "' type='text'   class='tdinput' style='width:90%'    readonly/>"; //添加列内容

    var newUnitPrice = newTR.insertCell(i++); //添加列:税额
    newUnitPrice.className = "cell";
    newUnitPrice.innerHTML = "<input name='txtTotalTax" + rowID + "' id='txtTotalTax" + rowID + "' type='text'   class='tdinput' style='width:90%'    readonly/>"; //添加列内容     

    if (hidIsliebiao == "1") {
        if (BillStatusName == "制单") {//判断从列表页面传来的源单类型
            if (IsliebiaoSendMode == "1") {//分店发货
                var newApplyReason = newTR.insertCell(i++); //仓库下拉选择
                newApplyReason.className = "cell";
                newApplyReason.innerHTML = "<select  class='tdinput' style='width:90%' class='tdinput' id='txtStorageID" + rowID + "'   readonly>" + document.getElementById("drpStorageID").innerHTML + "</select>";
            }
            else {//总部发货
                var newApplyReason = newTR.insertCell(i++); //仓库下拉选择
                newApplyReason.className = "cell";
                newApplyReason.innerHTML = "<select  class='tdinput' style='width:90%' class='tdinput' id='txtStorageID" + rowID + "'>" + document.getElementById("drpStorageID").innerHTML + "</select>";
            }
        }
        else if (IsliebiaoSendMode == "") {
            var newApplyReason = newTR.insertCell(i++); //仓库下拉选择
            newApplyReason.className = "cell";
            if (Flag1 == "2") {
                newApplyReason.innerHTML = "<select  class='tdinput' style='width:90%' class='tdinput' id='txtStorageID" + rowID + "'>" + document.getElementById("drpStorageID").innerHTML + "</select>";
            }
            else if (Flag1 != "2") {
                newApplyReason.innerHTML = "<select  class='tdinput' style='width:90%' class='tdinput' id='txtStorageID" + rowID + "'   readonly>" + document.getElementById("drpStorageID").innerHTML + "</select>";
            }
        }
        else {
            var newApplyReason = newTR.insertCell(i++); //仓库下拉选择
            newApplyReason.className = "cell";
            newApplyReason.innerHTML = "<select  class='tdinput' style='width:90%' class='tdinput' id='txtStorageID" + rowID + "'   readonly>" + document.getElementById("drpStorageID").innerHTML + "</select>";
        }
    }
    else {
        var newApplyReason = newTR.insertCell(i++); //仓库下拉选择
        newApplyReason.className = "cell";
        if (Flag1 == "2") {
            newApplyReason.innerHTML = "<select  class='tdinput' style='width:90%' class='tdinput' id='txtStorageID" + rowID + "'>" + document.getElementById("drpStorageID").innerHTML + "</select>";
        }
        else if (Flag1 != "2") {
            newApplyReason.innerHTML = "<select  class='tdinput' style='width:90%' class='tdinput' id='txtStorageID" + rowID + "'   readonly>" + document.getElementById("drpStorageID").innerHTML + "</select>";
        }
    }

    var newFromBillID = newTR.insertCell(i++); //添加列:源单ID
    newFromBillID.style.display = "none";
    newFromBillID.innerHTML = "<input name='txtFromBillID" + rowID + "' id='txtFromBillID" + rowID + "' type='text' class='tdinput' style='width:90%'   readonly />"; //添加列内容

    var newFromBillID = newTR.insertCell(i++); //添加列:源单编号
    newFromBillID.className = "cell";
    newFromBillID.innerHTML = "<input name='txtFromBillNo" + rowID + "' id='txtFromBillNo" + rowID + "' type='text'  class='tdinput' style='width:90%'  readonly   disabled/>"; //添加列内容

    var newFromLineNo = newTR.insertCell(i++); //添加列:源单序号
    newFromLineNo.className = "cell";
    newFromLineNo.innerHTML = "<input name='txtFromLineNo" + rowID + "' id='txtFromLineNo" + rowID + "' type='text'  class='tdinput' style='width:90%'  readonly   disabled/>"; //添加列内容

    var newRemark = newTR.insertCell(i++); //添加列:备注
    newRemark.className = "cell";
    newRemark.innerHTML = "<input name='txtRemark" + rowID + "' id='txtRemark" + rowID + "' type='text' class='tdinput' style='width:90%' />"; //添加列内容

    txtTRLastIndex.value = (rowID + 1).toString(); //将行号推进下一行
    return rowID;
}



function GetProduct(rowID) {
    if ($("#ddlSendMode").val() == "1") {//分店发货
        popProductInfoSubUC.ShowList(rowID)
    }
    else {
        popTechObj.ShowList(rowID);
    }
}



function GenerateNo(Edge) {//生成序号
    DtlSCnt = findObj("txtTRLastIndex", document); //明细来源新增行号
    var signFrame = findObj("dg_Log", document);
    var SortNo = 1; //起始行号
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (var i = 1; i < Edge; ++i) {
            if (signFrame.rows[i].style.display != "none") {
                document.getElementById("newNameTD" + i).value = SortNo++;
            }
        }
        return SortNo;
    }
    return 0; //明细表不存在时返回0
}


function DeleteSignRowSubSellBack() {//删除明细行，需要将序号重新生成
    var signFrame = findObj("dg_Log", document);
    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        var rowID = i + 1;
        if (ck[i].checked) {
            signFrame.rows[rowID].style.display = "none";
        }
        document.getElementById("newNameTD" + rowID).innerHTML = GenerateNo(rowID);
    }
    var Flag = document.getElementById("drpFromType").value;

    //判断是否有明细了，若没有了，则将供应商设为可选
    var totalSort = 0; //明细行数
    for (var i = 0; i < ck.length; i++) {
        var rowID = i + 1;
        if (signFrame.rows[rowID].style.display != "none") {
            totalSort++;
            break;
        }
    }
    if (totalSort != 0) {//明细行数大于0
    }
    else {   //无明细，供应商可选
        document.getElementById("checkall").checked = false;
        document.getElementById('drpCurrencyType').disabled = false;
        document.getElementById('txtOrderID').disabled = false;
    }

    fnTotalInfo();
}


//计算各种合计信息
function fnTotalInfo() {
    //金额格式判断
    if (document.getElementById("txtWairPayTotal").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtWairPayTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            alert("【应退货款】格式不正确！");
            return;
        }
    }

    if (document.getElementById("txtSettleTotal").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtSettleTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            alert("【结算货款】格式不正确！");
            return;
        }
    }

    if (document.getElementById("txtPayedTotal").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtPayedTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            alert("【已退货款】格式不正确！");
            return;
        }
    }

    if (document.getElementById("txtWairPayTotalOverage").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtWairPayTotalOverage").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            alert("【应退货款余额】格式不正确！");
            return;
        }
    }


    var CountTotal = 0; //数量合计
    var TotalPrice = 0; //金额合计
    var Tax = 0; //税额合计
    var TotalFee = 0; //含税金额合计
    var Discount = $("#txtDiscount").val(); //整单折扣
    var DiscountTotal = 0; //折扣金额
    var RealTotal = 0; //折后含税金额
    //先判断已退款金额、整单折扣是否符合格式再往下计算
    if (document.getElementById("txtDiscount").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtDiscount").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            alert("【整单折扣】格式不正确！");
            return;
        }
    }

    var Zhekoumingxi = 0; //明细中折扣合计

    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = i;
            var pProductCount = $("#txtProductCount" + rowid).val(); //发货数量

            var pYBackCount = $("#txtYBackCount" + rowid).val(); //已退货数量
            var pCountDetail = $("#txtBackCount" + rowid).val(); //退货数量
            if ($("#txtIsMoreUnit").val() == "1") {
                pCountDetail = $("#txtUsedUnitCount" + rowid).val();
            }
            if (pCountDetail == "") {
                pCountDetail = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
            }
            if (IsNumberOrNumeric(pCountDetail, 9, parseInt($("#hidSelPoint").val())) == false) {
                alert("第" + i + "行的【退货数量】格式不正确！");
                return;
            }
            if (pProductCount != "") {
                if ((parseFloat(pProductCount) - parseFloat(pYBackCount)) < parseFloat(pCountDetail)) {
                    var xxxxx = parseFloat(pProductCount) - parseFloat(pYBackCount);
                    alert("第" + i + "行的【退货数量】不能大于当前可用退货数量(" + xxxxx + ")！");
                    return;
                }
            }
            var UnitPriceDetail = $("#txtUnitPrice" + rowid).val(); //单价
            if ($("#txtIsMoreUnit").val() == "1") {
                UnitPriceDetail = $("#txtUsedPrice" + rowid).val();
            }
            //判断是否是增值税，不是增值税含税价始终等于单价
            if (!document.getElementById('chkisAddTax').checked) {
                $("#txtTaxPrice" + rowid).val(UnitPriceDetail);
            }
            else {
                $("#txtTaxPrice" + rowid).val(parseFloat(UnitPriceDetail) * (1 + parseFloat($("#txtTaxRate" + rowid).val())));
            }

            var TaxPriceDetail = $("#txtTaxPrice" + rowid).val(); //含税价
            var DiscountDetail = $("#txtDiscount" + rowid).val(); //折扣
            var TaxRateDetail = $("#txtTaxRate" + rowid).val(); //税率

            var TotalPriceDetail = FormatAfterDotNumber((UnitPriceDetail * pCountDetail * DiscountDetail / 100), parseInt($("#hidSelPoint").val())); //金额=数量*单价*折扣
            var TotalFeeDetail = FormatAfterDotNumber((TaxPriceDetail * pCountDetail * DiscountDetail / 100), parseInt($("#hidSelPoint").val())); //含税金额=数量*含税单价*折扣
            var TotalTaxDetail = FormatAfterDotNumber(TotalFeeDetail - TotalPriceDetail, parseInt($("#hidSelPoint").val())); //税额=金额 *税率


            $("#txtTotalPrice" + rowid).val(FormatAfterDotNumber(TotalPriceDetail, parseInt($("#hidSelPoint").val()))); //金额
            $("#txtTotalTax" + rowid).val(FormatAfterDotNumber(TotalTaxDetail, parseInt($("#hidSelPoint").val()))); //税额
            $("#txtTotalFee" + rowid).val(FormatAfterDotNumber(TotalFeeDetail, parseInt($("#hidSelPoint").val()))); //含税金额
            TotalPrice += parseFloat(TotalPriceDetail); //金额
            Tax += parseFloat(TotalTaxDetail); //税额
            TotalFee += parseFloat(TotalFeeDetail); //含税金额
            CountTotal += parseFloat(pCountDetail); //数量合计
            Zhekoumingxi += parseFloat(TaxPriceDetail * pCountDetail * (100 - DiscountDetail) / 100); //明细折扣金额=含税价*数量*（1-折扣）
        }
    }
    $("#txtTotalMoney").val(FormatAfterDotNumber(TotalPrice, parseInt($("#hidSelPoint").val()))); //金额合计
    $("#txtTotalTaxHo").val(FormatAfterDotNumber(Tax, parseInt($("#hidSelPoint").val()))); //税额合计
    $("#txtTotalFeeHo").val(FormatAfterDotNumber(TotalFee, parseInt($("#hidSelPoint").val()))); //含税金额合计
    $("#txtCountTotal").val(FormatAfterDotNumber(CountTotal, parseInt($("#hidSelPoint").val()))); //数量合计
    $("#txtDiscountTotal").val(FormatAfterDotNumber(((TotalFee * (100 - Discount) / 100) + Zhekoumingxi), parseInt($("#hidSelPoint").val()))); //折扣金额
    $("#txtRealTotal").val(FormatAfterDotNumber((TotalFee * Discount / 100), parseInt($("#hidSelPoint").val()))); //折后含税金额
    $("#txtWairPayTotal").val(FormatAfterDotNumber((TotalFee * Discount / 100), parseInt($("#hidSelPoint").val()))); //应退款金额 默认等于折后含税金额可以进行修改


    var no1 = parseFloat(document.getElementById("txtPayedTotal").value.Trim()); //已退货款
    var no2 = parseFloat(document.getElementById("txtWairPayTotal").value.Trim()); //应退货款
    var no4 = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
    var no5 = parseFloat(document.getElementById("txtWairPayTotalOverage").value.Trim()); //应退货款余额

    document.getElementById("txtWairPayTotal").value = FormatAfterDotNumber(no2, parseInt($("#hidSelPoint").val()));
    document.getElementById("txtSettleTotal").value = FormatAfterDotNumber(no4, parseInt($("#hidSelPoint").val()));
    document.getElementById("txtWairPayTotalOverage").value = FormatAfterDotNumber(no2 - no4 - no1, parseInt($("#hidSelPoint").val()));

}


//门店相关业务需要判断的金额计算
function fnTotalInfo1() {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    //金额格式判断
    if (document.getElementById("txtWairPayTotal").value.Trim() != "") {
        if (IsNumberOrNumeric(document.getElementById("txtWairPayTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            isFlag = false;
            fieldText += "应退货款|";
            msgText += "应退货款格式不正确！|";
        }
    }
    else {
        document.getElementById("txtWairPayTotal").value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
    }


    if (document.getElementById("txtSettleTotal").value.Trim() != "") {
        if (IsNumberOrNumeric(document.getElementById("txtSettleTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            isFlag = false;
            fieldText += "结算货款|";
            msgText += "结算货款格式不正确！|";
        }
    }
    else {
        document.getElementById("txtSettleTotal").value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
    }

    if (document.getElementById("txtPayedTotal").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtPayedTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            isFlag = false;
            fieldText += "已退货款|";
            msgText += "已退货款格式不正确！|";
        }
    }

    var no1 = parseFloat(document.getElementById("txtPayedTotal").value.Trim()); //已退货款
    var no2 = parseFloat(document.getElementById("txtWairPayTotal").value.Trim()); //应退货款
    var no3 = parseFloat(document.getElementById("txtRealTotal").value.Trim()); //折后含税金额
    var no4 = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
    var no5 = parseFloat(document.getElementById("txtWairPayTotalOverage").value.Trim()); //应退货款余额

    if (no1 > no3) {
        isFlag = false;
        fieldText += "已退货款|";
        msgText += "已退货款不能大于折后含税金额|";
    }
    if (no2 > no3) {
        isFlag = false;
        fieldText += "应退货款|";
        msgText += "应退货款不能大于折后含税金额|";
    }
    if (no4 > no3) {
        isFlag = false;
        fieldText += "结算货款|";
        msgText += "结算货款不能大于折后含税金额|";
    }
    if (no5 > no3) {
        isFlag = false;
        fieldText += "应退货款余额|";
        msgText += "应退货款余额不能大于折后含税金额|";
    }
    if (no1 > no2) {
        isFlag = false;
        fieldText += "已退货款|";
        msgText += "已退货款不能大于应退货款|";
    }
    if (no4 > no2) {
        isFlag = false;
        fieldText += "结算货款|";
        msgText += "结算货款不能大于应退货款|";
    }
    if (no5 > no2) {
        isFlag = false;
        fieldText += "应退货款余额|";
        msgText += "应退货款余额不能大于应退货款|";
    }
    if (no4 > parseFloat(no2 - no1)) {
        isFlag = false;
        fieldText += "结算货款|";
        msgText += "结算货款不能大于应退货款余额|";
    }


    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
    }
    else {
        document.getElementById("txtWairPayTotal").value = FormatAfterDotNumber(no2, parseInt($("#hidSelPoint").val()));
        document.getElementById("txtSettleTotal").value = FormatAfterDotNumber(no4, parseInt($("#hidSelPoint").val()));
        document.getElementById("txtWairPayTotalOverage").value = FormatAfterDotNumber(no2 - no4 - no1, parseInt($("#hidSelPoint").val()));
    }


}


function fnChangeAddTax() {//改变是否为增值税

    var isAddTax = $("#chkisAddTax").attr("checked");
    //新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();
    if (isAddTax == true) {
        $("#labAddTax").html("是增值税");
    }
    else {
        $("#labAddTax").html("非增值税");
    }

    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {//是增值税则
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxRate" + rowIndex).value, parseInt($("#hidSelPoint").val())); //税率等于隐藏域税率

            }
            else {
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("txtUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //含税价等于单价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())); //税率等于0

            }
        }
    }
    fnTotalInfo();
}



//获取明细信息
function getDtlSValue() {
    arrlength = 0;
    var signFrame = document.getElementById("dg_Log");
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (i = 1; i < signFrame.rows.length; i++) {
            if (signFrame.rows[i].style.display != "none") {
                alert("i=" + i);
                var temp = new Array();
                signFrame.rows[i].cells[0].innerHTML = i.toString();
                rowid = parseInt(signFrame.rows[i].cells[0].innerText);
                temp.push(document.getElementById("chk" + rowid).value); //0
                temp.push(document.getElementById("txtProductNo" + rowid).value); //
                temp.push(document.getElementById("txtProductName" + rowid).value); //2
                temp.push(document.getElementById("txtProductCount" + rowid).value); //3
                temp.push(document.getElementById("txtUnitID" + rowid).value); //4
                temp.push(document.getElementById("txtRequireDate" + rowid).value); //5
                temp.push(document.getElementById("txtUnitPrice" + rowid).value); //6
                temp.push(document.getElementById("txtTotalPrice" + rowid).value); //7
                temp.push(document.getElementById("txtTotalPrice" + rowid).value); //8
                temp.push(document.getElementById("txtApplyReason" + rowid).value); //9需求日期
                temp.push(document.getElementById("txtRemark" + rowid).value); //10
                temp.push(document.getElementById("txtFromBillID" + rowid).value); //11
                temp.push(document.getElementById("txtFromLineNo" + rowid).value); //12
                DtlS_Item[i - 1] = temp;
                arrlength++;
            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "已缓存配件信息，如需保存请提交！");
        }
    }
}



//附件处理
function DealResume(flag) {
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "") {
        return;
    }
    //上传附件
    else if ("upload" == flag) {
        ShowUploadFile();
    }
    //清除附件
    else if ("clear" == flag) {
        //设置附件路径
        document.getElementById("hfPageAttachment").value = "";
        //下载删除不显示
        document.getElementById("divDealResume").style.display = "none";
        //上传显示 
        document.getElementById("divUploadResume").style.display = "block";
    }
    //下载附件
    else if ("download" == flag) {
        //获取简历路径
        resumeUrl = document.getElementById("hfPageAttachment").value;
        window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + resumeUrl, "_blank");
    }
}

function AfterUploadFile(url) {
    if (url != "") {
        //下载删除显示
        document.getElementById("divDealResume").style.display = "block";
        //上传不显示
        document.getElementById("divUploadResume").style.display = "none";


        //设置简历路径
        document.getElementById("hfPageAttachment").value = url;
    }
}

function ChangeCurreny() {//选择币种带出汇率
    var IDExchangeRate = document.getElementById("drpCurrencyType").value;
    document.getElementById("CurrencyTypeID").value = IDExchangeRate.split('_')[0];
    document.getElementById("txtRate").value = IDExchangeRate.split('_')[1];

    var isAddTax = $("#chkisAddTax").attr("checked");
    //新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();

    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {//是增值税则
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxRate" + rowIndex).value, parseInt($("#hidSelPoint").val())); //税率等于隐藏域税率
                $("#labAddTax").html("是增值税");
            }
            else {
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("txtUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //含税价等于单价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())); //税率等于0
                $("#labAddTax").html("非增值税");
            }
        }
    }

    fnTotalInfo();
}

function DeleteAll() {
    var Flag = document.getElementById("drpFromType").value;


    if (Flag == 0) {
        DeleteSignRow100();
        fnTotalInfo();
        document.getElementById("divIsbishuxiang").style.display = "none";
        try {
            document.getElementById('unbtnGetGoods').style.display = 'none';
            document.getElementById('btnGetGoods').style.display = '';
        }
        catch (e) { }
        document.getElementById("divyuandan").style.display = "inline";
        document.getElementById("imgAdd").style.display = "inline";
        document.getElementById("imgUnAdd").style.display = "none";
        document.getElementById("txtOrderID").style.display = "none";
        document.getElementById("Get_Potential").style.display = "none";
        document.getElementById("Get_UPotential").style.display = "inline";
        document.getElementById('drpCurrencyType').disabled = false;
        document.getElementById('txtOrderID').disabled = false;
        document.getElementById('ddlSendMode').disabled = false;

    }
    else {
        DeleteSignRow100();
        fnTotalInfo();
        document.getElementById("divIsbishuxiang").style.display = "inline";
        try {
            document.getElementById('unbtnGetGoods').style.display = '';
            document.getElementById('btnGetGoods').style.display = 'none';
        }
        catch (e) { }
        document.getElementById("divyuandan").style.display = "inline";
        document.getElementById("imgAdd").style.display = "none";
        document.getElementById("imgUnAdd").style.display = "inline";
        document.getElementById("txtOrderID").style.display = "inline";
        document.getElementById("Get_Potential").style.display = "inline";
        document.getElementById("Get_UPotential").style.display = "none";
        document.getElementById('drpCurrencyType').disabled = false;
        document.getElementById('txtOrderID').disabled = false;
        document.getElementById('ddlSendMode').disabled = false;
    }

}


function DeleteSignRow100() {
    var signFrame = document.getElementById("dg_Log");
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (var i = signFrame.rows.length - 1; i > 0; --i) {
            signFrame.deleteRow(i);
        }
    }
    findObj("txtTRLastIndex", document).value = 1;
}


//全选
function SelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}


function clearSubSellOrderdiv() {
    DeleteSignRow100();
    fnTotalInfo();
    document.getElementById("txtOrderID").value = "";
    document.getElementById("HidOrderID").value = "";
    document.getElementById("ddlSendMode").value = 1;
    document.getElementById("txtOutDate").value = "";
    document.getElementById("ddlOutDateHour").value = "00";
    document.getElementById("ddlOutDateMin").value = "00";
    document.getElementById("UserOutUserID").value = "";
    document.getElementById("HidOutUserID").value = "";
    document.getElementById("txtCustName").value = "";
    document.getElementById("txtCustTel").value = "";
    document.getElementById("txtCustMobile").value = "";
    document.getElementById("txtCustAddr").value = "";
    document.getElementById('drpCurrencyType').disabled = false;
    closeSubSellOrderdiv();
}



///验证电话号码
//只允许输入0-9和‘-’号   
// 任何一个字符不符合返回true                       
function isvalidtel(inputs) //校验电话号码    //add by taochun
{
    var i, temp;
    var isvalidtel = false;
    inputstr = trim(inputs);
    if (inputstr.length == null || inputstr.length == 0) return true;
    for (i = 0; i < inputstr.length; i++) {
        temp = inputstr.substring(i, i + 1);
        if (!(temp >= "0" && temp <= "9" || temp == "-")) {
            isvalidtel = true;
            break;
        }
    }
    return isvalidtel;
}



function checkMobile(s) {
    var regu = /^[1]([3][0-9]{1}|59|58)[0-9]{8}$/;
    var re = new RegExp(regu);
    if (re.test(s)) {
        return true;
    } else {
        return false;
    }
}

//验证
/*
* 基本信息校验
*/
function CheckBaseInfo() {

    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    //先检验页面上的特殊字符
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }


    //新建时，编号选择手工输入时
    if (document.getElementById("txtAction").value == "1") {
        //        获取编码规则下拉列表选中项
        codeRule = document.getElementById("CodingRuleControl1_ddlCodeRule").value;
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "") {
            //获取输入的编号
            employeeNo = document.getElementById("CodingRuleControl1_txtCode").value;
            //编号必须输入
            if (employeeNo == "") {
                isFlag = false;
                fieldText += "单据编号|";
                msgText += "请输入单据编号|";
            }
            else {
                if (!CodeCheck($.trim($("#CodingRuleControl1_txtCode").val()))) {
                    isFlag = false;
                    fieldText = fieldText + "单据编号|";
                    msgText = msgText + "单据编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
                }
                else if (strlen($.trim($("#CodingRuleControl1_txtCode").val())) > 50) {
                    isErrorFlag = true;
                    fieldText += "单据编号|";
                    msgText += "单据编号长度不大于50|";
                }

            }
        }
    }

    if (document.getElementById("txtDeptName").value.Trim() == "") {
        isFlag = false;
        fieldText += "销售分店|";
        msgText += "销售分店不能为空|";
    }

    //判断手机号和电话号码
    var CustTel = document.getElementById("txtCustTel").value; //客户联系电话
    var CustMobile = document.getElementById("txtCustMobile").value; //客户手机号
    if (CustTel != "") {
        if (isvalidtel(CustTel)) {
            isFlag = false;
            fieldText += "客户联系电话|";
            msgText += "客户联系电话格式不对|";
        }
    }
    if (CustMobile != "") {
        if (!IsNumberOrNumeric(CustMobile)) {
            isFlag = false;
            fieldText = fieldText + "客户手机号|";
            msgText = msgText + "客户手机号格式不正确|";
        }
    }

    //验证是否为数字
    if (document.getElementById("txtRate").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtRate").value, 12, 4) == false) {
            isFlag = false;
            fieldText += "汇率|";
            msgText += "请输入正确的汇率|";
        }
    }

    if (document.getElementById("txtDiscount").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtDiscount").value, 12, parseInt($("#hidSelPoint").val()) * 2) == false) {
            isFlag = false;
            fieldText += "整单折扣|";
            msgText += "请输入正确的整单折扣|";
        }
    }
    if (document.getElementById("txtTotalMoney").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtTotalMoney").value, 20, parseInt($("#hidSelPoint").val()) * 2) == false) {
            isFlag = false;
            fieldText += "金额合计|";
            msgText += "请输入正确的金额合计|";
        }
    }
    if (document.getElementById("txtTotalTaxHo").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtTotalTaxHo").value, 20, parseInt($("#hidSelPoint").val()) * 2) == false) {
            isFlag = false;
            fieldText += "税额合计|";
            msgText += "请输入正确的税额合计|";
        }
    }
    if (document.getElementById("txtTotalFeeHo").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtTotalFeeHo").value, 20, parseInt($("#hidSelPoint").val()) * 2) == false) {
            isFlag = false;
            fieldText += "含税金额合计|";
            msgText += "请输入正确的含税金额合计|";
        }
    }
    if (document.getElementById("txtDiscountTotal").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtDiscountTotal").value, 20, parseInt($("#hidSelPoint").val()) * 2) == false) {
            isFlag = false;
            fieldText += "折扣金额合计|";
            msgText += "请输入正确的折扣金额合计|";
        }
    }
    if (document.getElementById("txtRealTotal").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtRealTotal").value, 20, parseInt($("#hidSelPoint").val()) * 2) == false) {
            isFlag = false;
            fieldText += "折后含税金额|";
            msgText += "请输入正确的折后含税金额|";
        }
    }

    if (strlen(document.getElementById("txtTitle").value.Trim()) > 100) {
        isFlag = false;
        fieldText += "单据主题|";
        msgText += "单据主题仅限于100个字符以内|";
    }

    if (document.getElementById("txtBackDate").value == "") {
        isFlag = false;
        fieldText += "退货时间|";
        msgText += "请输入退货时间|";
    }
    if (document.getElementById("UserSeller").value == "") {
        isFlag = false;
        fieldText += "退货处理人|";
        msgText += "请输入退货处理人|";
    }

    //金额格式判断
    if (document.getElementById("txtWairPayTotal").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtWairPayTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            isFlag = false;
            fieldText += "应退货款|";
            msgText += "应退货款格式不正确！|";
        }
    }

    if (document.getElementById("txtSettleTotal").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtSettleTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            isFlag = false;
            fieldText += "结算货款|";
            msgText += "结算货款格式不正确！|";
        }
    }

    if (document.getElementById("txtPayedTotal").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtPayedTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            isFlag = false;
            fieldText += "已退货款|";
            msgText += "已退货款格式不正确！|";
        }
    }

    if (document.getElementById("txtWairPayTotalOverage").value != "") {
        if (IsNumberOrNumeric(document.getElementById("txtWairPayTotalOverage").value, 12, parseInt($("#hidSelPoint").val())) == false) {
            isFlag = false;
            fieldText += "应退货款余额|";
            msgText += "应退货款余额格式不正确！|";
        }
    }

    var no1 = parseFloat(document.getElementById("txtPayedTotal").value.Trim()); //已退货款
    var no2 = parseFloat(document.getElementById("txtWairPayTotal").value.Trim()); //应退货款
    var no3 = parseFloat(document.getElementById("txtRealTotal").value.Trim()); //折后含税金额
    var no4 = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
    var no5 = parseFloat(document.getElementById("txtWairPayTotalOverage").value.Trim()); //应退货款余额

    if (no1 > no3) {
        isFlag = false;
        fieldText += "已退货款|";
        msgText += "已退货款不能大于折后含税金额|";
    }
    if (no2 > no3) {
        isFlag = false;
        fieldText += "应退货款|";
        msgText += "应退货款不能大于折后含税金额|";
    }
    if (no4 > no3) {
        isFlag = false;
        fieldText += "结算货款|";
        msgText += "结算货款不能大于折后含税金额|";
    }
    if (no5 > no3) {
        isFlag = false;
        fieldText += "应退货款余额|";
        msgText += "应退货款余额不能大于折后含税金额|";
    }
    if (no1 > no2) {
        isFlag = false;
        fieldText += "已退货款|";
        msgText += "已退货款不能大于应退货款|";
    }
    if (no4 > no2) {
        isFlag = false;
        fieldText += "结算货款|";
        msgText += "结算货款不能大于应退货款|";
    }
    if (no5 > no2) {
        isFlag = false;
        fieldText += "应退货款余额|";
        msgText += "应退货款余额不能大于应退货款|";
    }

    //限制字数
    var BackReason = document.getElementById("txtBackReason").value; //退货理由描述
    var Remark = document.getElementById("txtRemark").value; //备注

    if (strlen(BackReason) > 1024) {
        isFlag = false;
        fieldText += "退货理由描述|";
        msgText += "退货理由描述仅限于1024个字符以内|";
    }
    if (strlen(Remark) > 200) {
        isFlag = false;
        fieldText += "备注|";
        msgText += "备注仅限于200个字符以内|";
    }

    //明细来源的校验
    var signFrame = document.getElementById("dg_Log");
    if ((typeof (signFrame) != "undefined") || signFrame != null) {
        RealCount = 0;
        for (var i = 1; i < signFrame.rows.length; ++i) {
            var SendMode = document.getElementById("ddlSendMode").value;
            if (signFrame.rows[i].style.display != "none") {

                RealCount++;
                var BackCount = "txtBackCount" + i;
                if ($("#txtIsMoreUnit").val() == "1") {
                    BackCount = "txtUsedUnitCount" + i;
                }
                var no = document.getElementById(BackCount).value;
                if (IsNumeric(no, 12, parseInt($("#hidSelPoint").val())) == false) {
                    isFlag = false;
                    fieldText += "退货数量|";
                    msgText += "请输入正确的退货数量|";
                }
                var UnitPrice = "txtUnitPrice" + i;
                var no1 = document.getElementById(UnitPrice).value;
                if (IsNumeric(no1, 12, parseInt($("#hidSelPoint").val())) == false) {
                    isFlag = false;
                    fieldText += "单价|";
                    msgText += "请输入正确的单价";
                }

                var YBackCount = "txtYBackCount" + i;
                var no2 = document.getElementById(YBackCount).value;
                var pCountDetail = "txtProductCount" + i;
                if ($("#txtIsMoreUnit").val() == "1") {
                    pCountDetail = "txtUsedUnitCount" + i;
                }

                var no3 = document.getElementById(pCountDetail).value;
                if (no3 != "") {
                    if ((no3 - no2) < no) {
                        var xxxxx = no3 - no2;
                        isFlag = false;
                        fieldText += "退货数量|";
                        msgText += "退货数量不能大于当前可用退货数量(" + xxxxx + ")|";
                    }
                }

                //校验发货模式为总部发货时仓库必须选择
                if (SendMode == 2) {
                    var StorageID = document.getElementById("txtStorageID" + i).value;
                    if (StorageID == 0) {
                        isFlag = false;
                        fieldText += "仓库|";
                        msgText += "仓库不能为空|";
                    }
                }
            }
        }
        if (RealCount == 0) {
            isFlag = false;
            fieldText += "销售退货单明细|";
            msgText += "销售退货单明细不能为空|";
        }
    }
    else {
        isFlag = false;
        fieldText += "销售退货单明细|";
        msgText += "销售退货单明细不能为空|";
    }


    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    return isFlag;
}


//更改发货模式时
function SendModeSelect() {
    var SendMode = $("#ddlSendMode").val();
    var signFrame = findObj("dg_Log", document);
    if (SendMode == "1") {//分店发货
        document.getElementById("storagedetail").style.display = "none";
        for (var i = 1; i < signFrame.rows.length; ++i) {
            $("#txtStorageID" + i).val("");
            $("#txtStorageID" + i).attr("disabled", true);
        }
    }
    else if (SendMode == "2") {//总部发货
        document.getElementById("storagedetail").style.display = "inline";
        for (var i = 1; i < signFrame.rows.length; ++i) {
            $("#txtStorageID" + i).attr("disabled", false);
        }
    }
}


function popSubSellOrderBBBB() {
    var CurrencyTypeID = document.getElementById("CurrencyTypeID").value;
    var Rate = "0";
    popSubSellOrder.ShowList('', CurrencyTypeID, Rate)
}



function SubSellBackSelect() {
    var Flag = document.getElementById("drpFromType").value;

    if (Flag == 0) {//无来源
        document.getElementById("divIsbishuxiang").style.display = "none";
    }
    else if (Flag == 1) {//采购订单 
        var CurrencyTypeID = document.getElementById("CurrencyTypeID").value;
        var Rate = document.getElementById("txtRate").value.Trim();

        var OrderNo = document.getElementById("txtOrderID").value; //直接取界面上的值，根据销售订单的编号过滤数据。
        if (OrderNo == "") {
            alert("请先选择【对应分店销售订单】！");
            return;
        }
        popSubSellOrderUC.ShowList('', OrderNo, CurrencyTypeID, Rate, $("#txtIsMoreUnit").val() == "1");

    }
}


/********************
* 取窗口滚动条高度 
******************/
function getScrollTop() {
    var scrollTop = 0;
    if (document.documentElement && document.documentElement.scrollTop) {
        scrollTop = document.documentElement.scrollTop;
    }
    else if (document.body) {
        scrollTop = document.body.scrollTop;
    }
    return scrollTop;
}



function FillProvider(providerid, providerno, providername, taketype, taketypename, carrytype, carrytypename, paytype, paytypename)//选择供应商带出部分信息
{
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    document.getElementById("txtHiddenProviderID1").value = providername; //当选择源单类型时，用于显示供应商并且不允许再修改
    document.getElementById("drpTakeType").value = taketype;
    document.getElementById("drpCarryType").value = carrytype;
    document.getElementById("drpPayType").value = paytype;

    closeProviderdiv();
}


//选择分店销售订单带出相关信息
function FillFromSubSellOrder(id, orderno, sendmode, outdate, outuserid, outusername, custname, custtel, custmobile, currencytype, currencytypename, rate, custaddr) {
    if (document.getElementById("txtOrderID").value == "") {
        document.getElementById("HidOrderID").value = id;
        document.getElementById("txtOrderID").value = orderno;
        document.getElementById("ddlSendMode").value = sendmode;
        document.getElementById('ddlSendMode').disabled = true;
        if (outdate != "") {
            var outdate1 = outdate.split(' ')[0];
            var outdatehour = outdate.split(' ')[1].split(':')[0];
            var outdatemin = outdate.split(' ')[1].split(':')[1];
            document.getElementById("txtOutDate").value = outdate1;
            document.getElementById("ddlOutDateHour").value = outdatehour;
            document.getElementById("ddlOutDateMin").value = outdatemin;
        }
        document.getElementById("HidOutUserID").value = outuserid;
        document.getElementById("UserOutUserID").value = outusername;
        document.getElementById("txtCustName").value = custname;
        document.getElementById("txtCustTel").value = custtel;
        document.getElementById("txtCustMobile").value = custmobile;
        document.getElementById("txtCustAddr").value = custaddr;

        closeSubSellOrderdiv();
    }
    else {
        if (document.getElementById("txtOrderID").value == orderno) {
            closeSubSellOrderdiv();
        }
        else {
            DeleteSignRow100();
            fnTotalInfo();
            document.getElementById("HidOrderID").value = id;
            document.getElementById("txtOrderID").value = orderno;
            document.getElementById("ddlSendMode").value = sendmode;
            document.getElementById('ddlSendMode').disabled = true;
            if (outdate != "") {
                var outdate1 = outdate.split(' ')[0];
                var outdatehour = outdate.split(' ')[1].split(':')[0];
                var outdatemin = outdate.split(' ')[1].split(':')[1];
                document.getElementById("txtOutDate").value = outdate1;
                document.getElementById("ddlOutDateHour").value = outdatehour;
                document.getElementById("ddlOutDateMin").value = outdatemin;
            }
            document.getElementById("HidOutUserID").value = outuserid;
            document.getElementById("UserOutUserID").value = outusername;
            document.getElementById("txtCustName").value = custname;
            document.getElementById("txtCustTel").value = custtel;
            document.getElementById("txtCustMobile").value = custmobile;
            document.getElementById("txtCustAddr").value = custaddr;

            closeSubSellOrderdiv();
        }
    }

    for (var i = 0; i < document.getElementById("drpCurrencyType").options.length; ++i) {
        if (document.getElementById("drpCurrencyType").options[i].value.split('_')[0] == currencytype) {
            $("#drpCurrencyType").attr("selectedIndex", i);
            break;
        }
    }
    document.getElementById("CurrencyTypeID").value = currencytype;
    document.getElementById("txtRate").value = rate;

    document.getElementById('drpCurrencyType').disabled = true;



}


//选择分店销售订单明细来源
function FillFromSubSellOrderUC(deptid, deptname, orderno, fromLineno, frombillno, frombillid, productid, productno, productname, standard,
                       unitid, unitname, unitprice, taxprice, discount, taxrate, storageid, storagename, productcount, remark
                       , ybackcount, usedunitid, usedunitcount, usedprice, batchNo) {
    if (!ExistFromBill(productid, frombillid, fromLineno)) {
        var Index = findObj("txtTRLastIndex", document).value;
        AddSignRow();
        var ProductID = 'txtProductID' + Index;
        var ProductNo = 'txtProductNo' + Index;
        var ProductName = 'txtProductName' + Index;
        var Standard = 'txtstandard' + Index;
        var UnitID = 'txtUnitID' + Index;
        var UnitName = 'txtUnitName' + Index;
        var ProductCount = 'txtProductCount' + Index;
        var UnitPrice = 'txtUnitPrice' + Index;
        if ($("#txtIsMoreUnit").val() == "1") {
            UnitPrice = 'txtUsedPrice' + Index;
        }
        var TaxPrice = 'txtTaxPrice' + Index;
        var Discount = 'txtDiscount' + Index;
        var TaxRate = 'txtTaxRate' + Index;
        var Remark = 'txtRemark' + Index;
        var FromBillID = 'txtFromBillID' + Index;
        var FromBillNo = 'txtFromBillNo' + Index;
        var FromLineNo = 'txtFromLineNo' + Index;
        var StorageID = 'txtStorageID' + Index;
        var HiddTaxPrice = 'hiddTaxPrice' + Index;
        var HiddTaxRate = 'hiddTaxRate' + Index;
        var HiddUnitPrice = 'hiddUnitPrice' + Index;
        var YBackCount = 'txtYBackCount' + Index;

        var UsedUnitCount = 'txtUsedUnitCount' + Index;
        var UsedPrice = 'txtUsedPrice' + Index;

        document.getElementById(ProductID).value = productid;
        document.getElementById(ProductNo).value = productno;
        document.getElementById(ProductName).value = productname;
        document.getElementById(Standard).value = standard;
        document.getElementById(UnitID).value = unitid;
        document.getElementById(UnitName).value = unitname;
        document.getElementById(UnitPrice).value = FormatAfterDotNumber(unitprice, parseInt($("#hidSelPoint").val()));
        document.getElementById(TaxPrice).value = FormatAfterDotNumber(taxprice, parseInt($("#hidSelPoint").val()));
        document.getElementById(Discount).value = FormatAfterDotNumber(discount, parseInt($("#hidSelPoint").val()));
        document.getElementById(TaxRate).value = FormatAfterDotNumber(taxrate, parseInt($("#hidSelPoint").val()));
        document.getElementById(Remark).value = remark;
        document.getElementById(FromBillID).value = frombillid;
        document.getElementById(FromBillNo).value = frombillno;
        document.getElementById(FromLineNo).value = fromLineno;
        document.getElementById(StorageID).value = StorageID;
        document.getElementById(HiddTaxPrice).value = FormatAfterDotNumber(taxprice, parseInt($("#hidSelPoint").val()));
        document.getElementById(HiddTaxRate).value = FormatAfterDotNumber(taxrate, parseInt($("#hidSelPoint").val()));
        document.getElementById(YBackCount).value = FormatAfterDotNumber(ybackcount, parseInt($("#hidSelPoint").val()));
        document.getElementById(HiddUnitPrice).value = FormatAfterDotNumber(unitprice, parseInt($("#hidSelPoint").val()));
        document.getElementById("txtBackCount" + Index).value = FormatAfterDotNumber(productcount, parseInt($("#hidSelPoint").val()));
        $("#selBatch" + Index).html("<option value='" + batchNo + "'>" + batchNo + "</option>");
        $("#selBatch" + Index).attr("disabled", "true");
        //计量单位开启
        if ($("#txtIsMoreUnit").val() == "1") {
            document.getElementById(ProductCount).value = FormatAfterDotNumber(usedunitcount, parseInt($("#hidSelPoint").val()));
            document.getElementById(UsedUnitCount).value = FormatAfterDotNumber(usedunitcount, parseInt($("#hidSelPoint").val()));
            document.getElementById(UsedPrice).value = FormatAfterDotNumber(usedprice, parseInt($("#hidSelPoint").val()));
            GetUnitGroupSelectEx(productid, 'SaleUnit', 'txtUsedUnit' + Index, 'ChangeUnit(this,' + Index + ')', "td_UsedUnitID_" + Index, usedunitid, "$('#txtUsedUnit" + Index + "').attr('disabled','disabled')");
        }
        else {
            document.getElementById(ProductCount).value = FormatAfterDotNumber(productcount, parseInt($("#hidSelPoint").val()));
        }
    }

    closeSubSellOrderUCdiv();

    var isAddTax = $("#chkisAddTax").attr("checked");
    //新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();

    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {//是增值税则
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxRate" + rowIndex).value, parseInt($("#hidSelPoint").val())); //税率等于隐藏域税率
                $("#labAddTax").html("是增值税");
            }
            else {
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //含税价等于单价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())); //税率等于0
                $("#labAddTax").html("非增值税");
            }
        }
    }

    document.getElementById('txtOrderID').disabled = true;
    fnTotalInfo();
}


// 设置总店批次
function SetBatchSelect(rowID, ProductID, isBatchNo, BatchNo) {
    // 批次
    GetBatchList(ProductID, "txtStorageID" + rowID, "selBatch" + rowID, isBatchNo, BatchNo);
    $("#txtStorageID" + rowID).change(function() {
        GetBatchList(ProductID, "txtStorageID" + rowID, "selBatch" + rowID, isBatchNo, BatchNo);
    });
}

// 设置分店批次
function SetSubBatchSelect(rowID, ProductID, isBatchNo, BatchNo) {
    // 批次
    GetSubBatchList(ProductID, "selBatch" + rowID, isBatchNo, BatchNo, $("#HidDeptID").val(), $("#hidCompanyCD").val());
}



//判断是否有相同记录有返回true，没有返回false
function ExistFromBill(productid, frombillid, fromLineno) {
    var signFrame = document.getElementById("dg_Log");

    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 1; i < signFrame.rows.length; ++i) {
        var productid1 = document.getElementById("txtProductID" + i).value;
        var frombillid1 = document.getElementById("txtFromBillID" + i).value;
        var fromlineno1 = document.getElementById("txtFromLineNo" + i).value;

        if ((signFrame.rows[i].style.display != "none") && (productid1 == productid) && (frombillid1 == frombillid) && (fromlineno1 == fromLineno)) {
            return true;
        }
    }
    return false;
}


function FillFromArrive(productid, productno, productname, standard, unitid, unitname, productcount, unitprice, taxprice, discount,
                       taxrate, totalprice, totalfee, totaltax, remark, frombillid, frombillno, fromLineno) {
    if (!ExistFromBill(productid, frombillid, fromLineno)) {
        var Index = findObj("txtTRLastIndex", document).value;
        AddSignRow();
        var ProductID = 'txtProductID' + Index;
        var ProductNo = 'txtProductNo' + Index;
        var ProductName = 'txtProductName' + Index;
        var Standard = 'txtstandard' + Index;
        var UnitID = 'txtUnitID' + Index;
        var UnitName = 'txtUnitName' + Index;
        var ProductCount = 'txtProductCount' + Index;
        var UnitPrice = 'txtUnitPrice' + Index;
        var TaxPrice = 'txtTaxPrice' + Index;
        var Discount = 'txtDiscount' + Index;
        var TaxRate = 'txtTaxRate' + Index;
        var Remark = 'txtRemark' + Index;
        var FromBillID = 'txtFromBillID' + Index;
        var FromBillNo = 'txtFromBillNo' + Index;
        var FromLineNo = 'txtFromLineNo' + Index;
        var HiddTaxPrice = 'hiddTaxPrice' + Index;
        var HiddTaxRate = 'hiddTaxRate' + Index;


        document.getElementById(ProductID).value = productid;
        document.getElementById(ProductNo).value = productno;
        document.getElementById(ProductName).value = productname;
        document.getElementById(Standard).value = standard;
        document.getElementById(UnitID).value = unitid;
        document.getElementById(UnitName).value = unitname;
        document.getElementById(ProductCount).value = FormatAfterDotNumber(productcount, parseInt($("#hidSelPoint").val()));
        document.getElementById(UnitPrice).value = FormatAfterDotNumber(unitprice, parseInt($("#hidSelPoint").val()));
        document.getElementById(TaxPrice).value = FormatAfterDotNumber(taxprice, parseInt($("#hidSelPoint").val()));
        document.getElementById(Discount).value = discount;
        document.getElementById(TaxRate).value = FormatAfterDotNumber(taxrate, parseInt($("#hidSelPoint").val()));
        document.getElementById(Remark).value = remark;
        document.getElementById(FromBillID).value = frombillid;
        document.getElementById(FromBillNo).value = frombillno;
        document.getElementById(FromLineNo).value = fromLineno;
        document.getElementById(HiddTaxPrice).value = FormatAfterDotNumber(taxprice, parseInt($("#hidSelPoint").val()));
        document.getElementById(HiddTaxRate).value = FormatAfterDotNumber(taxrate, parseInt($("#hidSelPoint").val()));


    }
    document.getElementById("txtProviderID").style.display = "none";
    document.getElementById("txtHiddenProviderID1").style.display = "block";
    closeMaterialdiv();

    var isAddTax = $("#chkisAddTax").attr("checked");
    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {//是增值税则
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value, parseInt($("#hidSelPoint").val())); //含税价等于隐藏域含税价
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxRate" + rowIndex).value, parseInt($("#hidSelPoint").val())); //税率等于隐藏域税率
                $("#labAddTax").html("是增值税");
            }
            else {
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("txtUnitPrice" + rowIndex).value, parseInt($("#hidSelPoint").val())); //含税价等于单价
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())); //税率等于0
                $("#labAddTax").html("非增值税");
            }
        }
    }
}


function popProductInfoSubUCFill(id, no, productname, standard, unitid, unit, taxprice, price, taxrate, discount, IsBatchNo, BatchNo, ProductCount) {
    var index = popProductInfoSubUC.objRowID;
    var ProductID = 'txtProductID' + index;
    var ProductNo = 'txtProductNo' + index;
    var ProductName = 'txtProductName' + index;
    var UnitID = 'txtUnitID' + index;
    var Unit = 'txtUnitName' + index;
    var Price = 'txtUnitPrice' + index;
    if ($("#txtIsMoreUnit").val() == "1") {
        Price = 'txtUsedPrice' + index;
    }
    var Standard = 'txtstandard' + index;
    var TaxPrice = 'txtTaxPrice' + index;
    var Discount = 'txtDiscount' + index;
    var TaxRate = 'txtTaxRate' + index;
    var HiddTaxPrice = 'hiddTaxPrice' + index;
    var HiddTaxRate = 'hiddTaxRate' + index;
    var HiddUnitPrice = 'hiddUnitPrice' + index;

    document.getElementById(TaxRate).value = taxrate;
    document.getElementById(Discount).value = discount;
    document.getElementById(TaxPrice).value = taxprice;
    document.getElementById(Standard).value = standard;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(Unit).value = unit;
    document.getElementById(ProductID).value = id;
    document.getElementById(ProductNo).value = no;
    document.getElementById(ProductName).value = productname;
    document.getElementById(Price).value = price;
    document.getElementById(HiddTaxPrice).value = taxprice;
    document.getElementById(HiddTaxRate).value = taxrate;
    document.getElementById(HiddUnitPrice).value = FormatAfterDotNumber(price, parseInt($("#hidSelPoint").val()));

    var isAddTax = $("#chkisAddTax").attr("checked");
    //新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();

    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {//是增值税则
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value, parseInt($("#hidSelPoint").val())); //含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxRate" + rowIndex).value, parseInt($("#hidSelPoint").val())); //税率等于隐藏域税率
                $("#labAddTax").html("是增值税");
            }
            else {
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value, parseInt($("#hidSelPoint").val())); //含税价等于单价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())); //税率等于0
                $("#labAddTax").html("非增值税");
            }
        }
    }
    GetUnitGroupSelect(id, 'SaleUnit', 'txtUsedUnit' + index, 'ChangeUnit(this,' + index + ')', "td_UsedUnitID_" + index, "");
    SetSubBatchSelect(index, id, IsBatchNo, BatchNo)
    closeProductInfoSubUCdiv();
}

/***************************************************
*切换单位
***************************************************/
function ChangeUnit(own, rowid) {
    CalCulateNum('txtUsedUnit' + rowid, "txtUsedUnitCount" + rowid, "txtBackCount" + rowid, "txtUsedPrice" + rowid, "txtUnitPrice" + rowid, $("#hidSelPoint").val());
    fnTotalInfo();
}


function Fun_FillParent_Content(id, no, productname, price, unitid, unit, taxrate, taxprice, discount, standard, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, GroupUnitNo,
                    SaleUnitID, SaleUnitName, InUnitID, InUnitName, StockUnitID, StockUnitName, MakeUnitID, MakeUnitName, IsBatchNo) {//陶春物品控件,门店这块暂时不用


    var index = popTechObj.InputObj;
    var ProductID = 'txtProductID' + index;
    var ProductNo = 'txtProductNo' + index;
    var ProductName = 'txtProductName' + index;
    var UnitID = 'txtUnitID' + index;
    var Unit = 'txtUnitName' + index;
    var Price = 'txtUnitPrice' + index;
    if ($("#txtIsMoreUnit").val() == "1") {
        Price = 'txtUsedPrice' + index;
    }
    
    var Standard = 'txtstandard' + index;
    var TaxPrice = 'txtTaxPrice' + index;
    var Discount = 'txtDiscount' + index;
    var TaxRate = 'txtTaxRate' + index;
    var HiddTaxPrice = 'hiddTaxPrice' + index;
    var HiddTaxRate = 'hiddTaxRate' + index;
    var HiddUnitPrice = 'hiddUnitPrice' + index;

    document.getElementById(TaxRate).value = FormatAfterDotNumber(taxrate/100, parseInt($("#hidSelPoint").val()));
    document.getElementById(Discount).value = FormatAfterDotNumber(discount, parseInt($("#hidSelPoint").val()));
    document.getElementById(TaxPrice).value = FormatAfterDotNumber(taxprice, parseInt($("#hidSelPoint").val()));
    document.getElementById(Standard).value = standard;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(Unit).value = unit;
    document.getElementById(ProductID).value = id;
    document.getElementById(ProductNo).value = no;
    document.getElementById(ProductName).value = productname;
    document.getElementById(Price).value = FormatAfterDotNumber(price, parseInt($("#hidSelPoint").val()));
    document.getElementById(HiddTaxPrice).value = FormatAfterDotNumber(taxprice, parseInt($("#hidSelPoint").val()));
    document.getElementById(HiddTaxRate).value = FormatAfterDotNumber(taxrate / 100, parseInt($("#hidSelPoint").val()));
    document.getElementById(HiddUnitPrice).value = FormatAfterDotNumber(price, parseInt($("#hidSelPoint").val()));
    var isAddTax = $("#chkisAddTax").attr("checked");
    //新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();

    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {//是增值税则
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = document.getElementById("hiddTaxRate" + rowIndex).value; //税率等于隐藏域税率
                $("#labAddTax").html("是增值税");
            }
            else {
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //含税价等于单价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())); //税率等于0
                $("#labAddTax").html("非增值税");
            }
        }

        GetUnitGroupSelect(id, 'SaleUnit', 'txtUsedUnit' + rowIndex, 'ChangeUnit(this,' + rowIndex + ')', "td_UsedUnitID_" + rowIndex, "");
        SetSubBatchSelect(rowIndex, id, IsBatchNo, "")
    }

}


function FillUnit(unitid, unitname) //回填单位
{
    var i = popUnitObj.InputObj;
    var UnitID = "txtUnitID" + i;
    var UnitName = "txtUnitName" + i;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(UnitName).value = unitname;
}



//确认
function Fun_ConfirmOperate() {

    if (!InsertSubSellBack()) {
        return;
    }

    var ActionArrive = document.getElementById("txtAction").value

    if (ActionArrive == "1") {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先保存再确认！");
        return;
    }
    var nox = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
    if (nox > 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先保存再确认！");
        return;
    }
    else {


        var c = window.confirm("确认执行确认操作吗？")
        if (c == true) {
            document.getElementById("ddlBillStatus").value = "2"; //改变单据状态显示
            document.getElementById("drpBusiStatus").value = "2"; //改变业务状态显示

            glb_BillID = document.getElementById('txtIndentityID').value;
            var deptID = document.getElementById("HidDeptID").value;
            document.getElementById("txtConfirmorReal").value = document.getElementById("UserName").value;
            document.getElementById("txtConfirmor").value = document.getElementById("UserID").value;
            document.getElementById("txtConfirmDate").value = document.getElementById("SystemTime").value;
            action = "Confirm";

            var DetailBackCount = new Array();
            var DetailFromBillNo = new Array();
            var DetailFromLineNo = new Array();
            var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
            var length = 0;
            var signFrame = findObj("dg_Log", document);
            for (var i = 0; i < txtTRLastIndex - 1; i++) {

                if (signFrame.rows[i + 1].style.display != "none") {
                    var objBackCount = 'txtBackCount' + (i + 1);
                    var objFromBillNo = 'txtFromBillNo' + (i + 1);
                    var objFromLineNo = 'txtFromLineNo' + (i + 1);


                    DetailBackCount.push(document.getElementById(objBackCount.toString()).value);
                    DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value);
                    DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value);
                    length++;
                }
            }

            var confirmor = document.getElementById("txtConfirmor").value;
            var BackNo = document.getElementById("divSubSellBackNo").innerHTML;
            var strParams = "action=" + action + "\
                            &confirmor=" + confirmor + "\
                            &deptID=" + deptID + "\
                            &ID=" + glb_BillID + "\
                            &BackNo=" + BackNo + "\
                            &DetailBackCount=" + DetailBackCount + "\
                            &DetailFromBillNo=" + DetailFromBillNo + "\
                            &DetailFromLineNo=" + DetailFromLineNo + "\
                            &length=" + length + "";
            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/SubStoreManager/SubSellBackAdd.ashx?" + strParams,

                dataType: 'json', //返回json格式数据
                cache: false,
                beforeSend: function() {
                },
                error: function() {
                    //popMsgObj.ShowMsg('sdf');
                },
                success: function(data) {
                    if (data.sta > 0) {
                        popMsgObj.ShowMsg('确认成功');
                        document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                        document.getElementById("txtModifiedUserIDReal").value = document.getElementById("txtModifiedUserID2").value;
                        document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;


                        //先设置所有文本控件只读
                        $(":text").each(function() {
                            this.disabled = true;
                        });
                        document.getElementById('drpFromType').disabled = true;
                        document.getElementById('ddlSendMode').disabled = true;
                        document.getElementById('ddlOutDateHour').disabled = true;
                        document.getElementById('ddlOutDateMin').disabled = true;
                        document.getElementById('chkisAddTax').disabled = true;
                        document.getElementById('ddlBackDateHour').disabled = true;
                        document.getElementById('ddlBackDateMin').disabled = true;
                        document.getElementById('drpCurrencyType').disabled = true;
                        document.getElementById('txtBackReason').disabled = true;
                        document.getElementById('txtRemark').disabled = true;
                        document.getElementById('divUploadResume').disabled = true;
                        document.getElementById('divDeleteResume').disabled = true;

                        var SendMode = $("#ddlSendMode").val();
                        var signFrame = findObj("dg_Log", document);
                        if (SendMode == "1") {//分店发货
                            document.getElementById("storagedetail").style.display = "none";
                            for (var i = 1; i < signFrame.rows.length; ++i) {
                                $("#txtStorageID" + i).val("");
                                $("#txtStorageID" + i).attr("disabled", true);
                                $("txtProductNo" + i).attr("disabled", true);
                                $("txtBackCount" + i).attr("disabled", true);
                            }
                        }
                        else if (SendMode == "2") {//总部发货
                            document.getElementById("storagedetail").style.display = "inline";
                            for (var i = 1; i < signFrame.rows.length; ++i) {
                                $("#txtStorageID" + i).attr("disabled", true);
                                $("txtProductNo" + i).attr("disabled", true);
                                $("txtBackCount" + i).attr("disabled", true);
                            }
                        }
                        try {
                            try {
                                document.getElementById('btnGetGoods').style.display = 'none';
                                document.getElementById('unbtnGetGoods').style.display = '';
                            }
                            catch (e) { }
                            document.getElementById("imgSave").style.display = "none";
                            document.getElementById("imgUnSave").style.display = "none";
                            document.getElementById("imgAdd").style.display = "none";
                            document.getElementById("imgUnAdd").style.display = "none";
                            document.getElementById("imgDel").style.display = "none";
                            document.getElementById("imgUnDel").style.display = "none";
                            document.getElementById("Get_Potential").style.display = "none";
                            document.getElementById("Get_UPotential").style.display = "none";
                            document.getElementById("btn_confirm").style.display = "none";
                            document.getElementById("btn_Qxconfirm").style.display = "inline";
                            document.getElementById("btn_UnQxconfirm").style.display = "none";
                            document.getElementById("btn_ruku").style.display = "inline";
                            document.getElementById("btn_jiesuan").style.display = "none";
                            document.getElementById("btn_Unconfirm").style.display = "none";

                        }
                        catch (e)
                                { }

                        document.getElementById("divTitle").innerText = "销售退货管理--入库";
                        document.getElementById("divInDate").style.display = "inline";
                        document.getElementById("divInUserID").style.display = "inline";
                        //默认的入库人和入库时间显示
                        document.getElementById("UserInUserID").value = document.getElementById("UserName").value;
                        document.getElementById("HidInUserID").value = document.getElementById("UserID").value;
                        var InDate = document.getElementById("SystemTime2").value;
                        var InDate1 = InDate.split(' ')[0];
                        var InDatehour = InDate.split(' ')[1].split(':')[0];
                        var InDatemin = InDate.split(' ')[1].split(':')[1];
                        document.getElementById("txtInDate").value = InDate1;
                        document.getElementById("ddlInDateHour").value = InDatehour;
                        document.getElementById("ddlInDateMin").value = InDatemin;
                        //设置部分控件可用
                        document.getElementById('txtInDate').disabled = false;
                        document.getElementById('ddlInDateHour').disabled = false;
                        document.getElementById('ddlInDateMin').disabled = false;
                        document.getElementById('UserInUserID').disabled = false;
                        document.getElementById('txtWairPayTotal').disabled = false;
                        document.getElementById('txtSettleTotal').disabled = false;
                    }
                }
            });

        }

    }

}




//取消确认
function Fun_QxConfirmOperate() {

    var ActionArrive = document.getElementById("drpBusiStatus").value

    if (ActionArrive != "2") {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "只有确认后的单据才可以取消确认！");
        return;
    }


    var c = window.confirm("确认执行取消确认操作吗？")
    if (c == true) {
        document.getElementById("ddlBillStatus").value = "1"; //改变单据状态显示
        document.getElementById("drpBusiStatus").value = "1"; //改变业务状态显示

        glb_BillID = document.getElementById('txtIndentityID').value;
        var deptID = document.getElementById("HidDeptID").value;
        document.getElementById("txtConfirmorReal").value = "";
        document.getElementById("txtConfirmor").value = "";
        document.getElementById("txtConfirmDate").value = "";
        action = "QxConfirm";

        var DetailBackCount = new Array();
        var DetailFromBillNo = new Array();
        var DetailFromLineNo = new Array();
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
        var length = 0;
        var signFrame = findObj("dg_Log", document);
        for (var i = 0; i < txtTRLastIndex - 1; i++) {

            if (signFrame.rows[i + 1].style.display != "none") {
                var objBackCount = 'txtBackCount' + (i + 1);
                var objFromBillNo = 'txtFromBillNo' + (i + 1);
                var objFromLineNo = 'txtFromLineNo' + (i + 1);


                DetailBackCount.push(document.getElementById(objBackCount.toString()).value);
                DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value);
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value);
                length++;
            }
        }


        var BackNo = document.getElementById("divSubSellBackNo").innerHTML;
        var confirmor = document.getElementById("txtConfirmor").value;
        var confirmdate = document.getElementById("txtConfirmDate").value;
        var strParams = "action=" + action + "\
                        &deptID=" + deptID + "\
                        &ID=" + glb_BillID + "\
                        &BackNo=" + BackNo + "\
                        &confirmor=" + confirmor + "\
                        &confirmdate=" + confirmdate + "\
                        &DetailBackCount=" + DetailBackCount + "\
                        &DetailFromBillNo=" + DetailFromBillNo + "\
                        &DetailFromLineNo=" + DetailFromLineNo + "\
                        &length=" + length + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SubStoreManager/SubSellBackAdd.ashx?" + strParams,

            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
                //popMsgObj.ShowMsg('sdf');
            },
            success: function(data) {
                if (data.sta > 0) {
                    popMsgObj.ShowMsg('取消确认成功');
                    document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedUserIDReal").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;

                    //先设置所有文本控件不可用
                    $(":text").each(function() {
                        this.disabled = true;
                    });

                    //取消对入库人和入库时间的控制
                    document.getElementById("UserInUserID").value = "";
                    document.getElementById("HidInUserID").value = "";
                    document.getElementById("txtInDate").value = "";
                    document.getElementById("ddlInDateHour").value = 00;
                    document.getElementById("ddlInDateMin").value = 00;

                    document.getElementById('txtInDate').disabled = true;
                    document.getElementById('ddlInDateHour').disabled = true;
                    document.getElementById('ddlInDateMin').disabled = true;
                    document.getElementById('UserInUserID').disabled = true;
                    document.getElementById('txtSttlDate').disabled = true;
                    document.getElementById('ddlSttlDateHour').disabled = true;
                    document.getElementById('ddlSttlDateMin').disabled = true;
                    document.getElementById('UserSttlUserID').disabled = true;



                    //设置部分控件可用
                    document.getElementById('txtTitle').disabled = false;
                    document.getElementById('drpFromType').disabled = false;
                    document.getElementById('txtOrderID').disabled = false;
                    document.getElementById('ddlSendMode').disabled = false;
                    document.getElementById('txtOutDate').disabled = false;
                    document.getElementById('ddlOutDateHour').disabled = false;
                    document.getElementById('ddlOutDateMin').disabled = false;
                    document.getElementById('UserOutUserID').disabled = false;
                    document.getElementById('chkisAddTax').disabled = false;
                    document.getElementById('txtCustName').disabled = false;
                    document.getElementById('txtCustTel').disabled = false;
                    document.getElementById('txtCustMobile').disabled = false;
                    document.getElementById('drpCurrencyType').disabled = false;
                    document.getElementById('txtBackDate').disabled = false;
                    document.getElementById('ddlBackDateHour').disabled = false;
                    document.getElementById('ddlBackDateMin').disabled = false;
                    document.getElementById('UserSeller').disabled = false;
                    document.getElementById('txtCustAddr').disabled = false;
                    document.getElementById('txtBackReason').disabled = false;
                    document.getElementById('txtDiscount').disabled = false;
                    document.getElementById('txtWairPayTotal').disabled = false;
                    document.getElementById('txtSettleTotal').disabled = false;
                    document.getElementById('txtRemark').disabled = false;
                    document.getElementById('divUploadResume').disabled = false;
                    document.getElementById('divUploadResume').disabled = false;
                    document.getElementById('divDeleteResume').disabled = false;
                    var FromType = document.getElementById("drpFromType").value;
                    if (FromType == 0) {
                        document.getElementById("divIsbishuxiang").style.display = "none";
                        document.getElementById("divyuandan").style.display = "inline";
                        document.getElementById("imgAdd").style.display = "inline";
                        try {
                            document.getElementById("imgUnAdd").style.display = "none";
                            document.getElementById("txtOrderID").style.display = "none";
                            document.getElementById("Get_Potential").style.display = "none";
                            document.getElementById("Get_UPotential").style.display = "inline";
                        }
                        catch (e)
                                 { }
                    }
                    else {
                        document.getElementById("divIsbishuxiang").style.display = "inline";
                        document.getElementById("divyuandan").style.display = "inline";
                        try {
                            document.getElementById("imgAdd").style.display = "none";
                            document.getElementById("imgUnAdd").style.display = "inline";
                            document.getElementById("txtOrderID").style.display = "inline";
                            document.getElementById("Get_Potential").style.display = "inline";
                            document.getElementById("Get_UPotential").style.display = "none";
                        }
                        catch (e)
                                 { }
                    }



                    var SendMode = $("#ddlSendMode").val();
                    var signFrame = findObj("dg_Log", document);
                    if (SendMode == "1") {//分店发货
                        document.getElementById("storagedetail").style.display = "none";
                        for (var i = 1; i < signFrame.rows.length; ++i) {
                            $("#txtStorageID" + i).val("");
                            $("#txtStorageID" + i).attr("disabled", true);
                            $("#txtBackCount" + i).attr("disabled", false);
                        }
                    }
                    else if (SendMode == "2") {//总部发货
                        document.getElementById("storagedetail").style.display = "inline";
                        for (var i = 1; i < signFrame.rows.length; ++i) {
                            $("#txtStorageID" + i).attr("disabled", false);
                            $("#txtBackCount" + i).attr("disabled", false);
                        }
                    }

                    document.getElementById("divTitle").innerText = "新建销售退货单";
                    document.getElementById("divInDate").style.display = "none";
                    document.getElementById("divInUserID").style.display = "none";
                    try {
                        try {
                            document.getElementById('btnGetGoods').style.display = '';
                            document.getElementById('unbtnGetGoods').style.display = 'none';
                        }
                        catch (e) { }
                        document.getElementById("imgSave").style.display = "inline";
                        document.getElementById("imgUnSave").style.display = "none";
                        document.getElementById("imgDel").style.display = "inline";
                        document.getElementById("imgUnDel").style.display = "none";
                        document.getElementById("Get_Potential").style.display = "inline";
                        document.getElementById("Get_UPotential").style.display = "none";
                        document.getElementById("btn_confirm").style.display = "inline";
                        document.getElementById("btn_Qxconfirm").style.display = "none";
                        document.getElementById("btn_UnQxconfirm").style.display = "inline";
                        document.getElementById("btn_ruku").style.display = "none";
                        document.getElementById("btn_jiesuan").style.display = "none";
                        document.getElementById("btn_Unconfirm").style.display = "none";

                    }
                    catch (e)
                            { }



                }
            }
        });

    }

}





//确认按扭入库用
function Fun_RukuOperate() {
    var c = window.confirm("确认执行入库操作吗？")
    if (c == true) {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;

        //校验入库时间和入库人不能为空
        if (document.getElementById("txtInDate").value == "") {
            isFlag = false;
            fieldText += "入库时间|";
            msgText += "请输入入库时间|";
        }
        if (document.getElementById("UserInUserID").value == "") {
            isFlag = false;
            fieldText += "入库人|";
            msgText += "请输入入库人|";
        }

        //金额格式判断
        if (document.getElementById("txtWairPayTotal").value != "") {
            if (IsNumberOrNumeric(document.getElementById("txtWairPayTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
                isFlag = false;
                fieldText += "应退货款|";
                msgText += "应退货款格式不正确！|";
            }
        }

        if (document.getElementById("txtSettleTotal").value != "") {
            if (IsNumberOrNumeric(document.getElementById("txtSettleTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
                isFlag = false;
                fieldText += "结算货款|";
                msgText += "结算货款格式不正确！|";
            }
        }

        if (document.getElementById("txtPayedTotal").value != "") {
            if (IsNumberOrNumeric(document.getElementById("txtPayedTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
                isFlag = false;
                fieldText += "已退货款|";
                msgText += "已退货款格式不正确！|";
            }
        }

        if (document.getElementById("txtWairPayTotalOverage").value != "") {
            if (IsNumberOrNumeric(document.getElementById("txtWairPayTotalOverage").value, 12, parseInt($("#hidSelPoint").val())) == false) {
                isFlag = false;
                fieldText += "应退货款余额|";
                msgText += "应退货款余额格式不正确！|";
            }
        }


        var no1 = parseFloat(document.getElementById("txtPayedTotal").value.Trim()); //已退货款
        var no2 = parseFloat(document.getElementById("txtWairPayTotal").value.Trim()); //应退货款
        var no3 = parseFloat(document.getElementById("txtRealTotal").value.Trim()); //折后含税金额
        var no4 = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
        var no5 = parseFloat(document.getElementById("txtWairPayTotalOverage").value.Trim()); //应退货款余额

        if (no1 > no3) {
            isFlag = false;
            fieldText += "已退货款|";
            msgText += "已退货款不能大于折后含税金额|";
        }
        if (no2 > no3) {
            isFlag = false;
            fieldText += "应退货款|";
            msgText += "应退货款不能大于折后含税金额|";
        }
        if (no4 > no3) {
            isFlag = false;
            fieldText += "结算货款|";
            msgText += "结算货款不能大于折后含税金额|";
        }
        if (no5 > no3) {
            isFlag = false;
            fieldText += "应退货款余额|";
            msgText += "应退货款余额不能大于折后含税金额|";
        }
        if (no1 > no2) {
            isFlag = false;
            fieldText += "已退货款|";
            msgText += "已退货款不能大于应退货款|";
        }
        if (no4 > no2) {
            isFlag = false;
            fieldText += "结算货款|";
            msgText += "结算货款不能大于应退货款|";
        }
        if (no5 > no2) {
            isFlag = false;
            fieldText += "应退货款余额|";
            msgText += "应退货款余额不能大于应退货款|";
        }

        if (!isFlag) {
            popMsgObj.Show(fieldText, msgText);
            return;
        }


        if (no5 > 0) {
            document.getElementById("ddlBillStatus").value = "2"; //改变单据状态显示
            document.getElementById("drpBusiStatus").value = "3"; //改变业务状态显示
        }
        else {
            document.getElementById("ddlBillStatus").value = "4"; //改变单据状态显示
            document.getElementById("drpBusiStatus").value = "4"; //改变业务状态显示
        }


        glb_BillID = document.getElementById('txtIndentityID').value;
        var deptID = document.getElementById("HidDeptID").value;

        var inDate = $("#txtInDate").val() + " " + $("#ddlInDateHour").val() + ":" + $("#ddlInDateMin").val(); //入库时间
        var inUserID = $("#HidInUserID").val(); //入库人
        action = "Ruku";
        var sendMode = document.getElementById("ddlSendMode").value;
        var payedTotal = document.getElementById('txtPayedTotal').value; //已退款金额
        var wairPayTotal = document.getElementById('txtWairPayTotal').value; //应退款金额
        var closer = document.getElementById("UserID").value;


        var DetailBackCount = new Array();
        var DetailStorageID = new Array();
        var DetailProductID = new Array();
        var DetailUnitPrice = new Array();
        var DetailBatchNo = new Array();
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
        var length = 0;
        var signFrame = findObj("dg_Log", document);
        for (var i = 0; i < txtTRLastIndex - 1; i++) {

            if (signFrame.rows[i + 1].style.display != "none") {
                var objBackCount = 'txtBackCount' + (i + 1);
                var objStorageID = 'txtStorageID' + (i + 1);
                var objProductID = 'txtProductID' + (i + 1);
                var objUnitPrice = 'txtUnitPrice' + (i + 1);

                DetailBackCount.push(document.getElementById(objBackCount.toString()).value);
                DetailStorageID.push(document.getElementById(objStorageID.toString()).value);
                DetailProductID.push(document.getElementById(objProductID.toString()).value);
                DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value);
                DetailBatchNo.push(document.getElementById('selBatch' + (i + 1)).value);
                length++;
            }
        }

        var busiStatus = document.getElementById("drpBusiStatus").value;
        var billStatus = document.getElementById("ddlBillStatus").value;
        var BackNo = document.getElementById("divSubSellBackNo").innerHTML;
        var strParams = "action=" + action + "\
                        &inDate=" + inDate + "\
                        &inUserID=" + inUserID + "\
                        &busiStatus=" + busiStatus + "\
                        &billStatus=" + billStatus + "\
                        &sendMode=" + sendMode + "\
                        &payedTotal=" + payedTotal + "\
                        &wairPayTotal=" + wairPayTotal + "\
                        &deptID=" + deptID + "\
                        &ID=" + glb_BillID + "\
                        &BackNo=" + BackNo + "\
                        &closer=" + closer + "\
                        &DetailBackCount=" + DetailBackCount + "\
                        &DetailStorageID=" + DetailStorageID + "\
                        &DetailProductID=" + DetailProductID + "\
                        &DetailUnitPrice=" + DetailUnitPrice + "\
                        &DetailBatchNo=" + DetailBatchNo + "\
                        &length=" + length + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SubStoreManager/SubSellBackAdd.ashx?" + strParams,

            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
                //popMsgObj.ShowMsg('sdf');
            },
            success: function(data) {
                if (data.sta > 0) {
                    popMsgObj.ShowMsg('入库成功');
                    document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedUserIDReal").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;
                    try {
                        try {
                            document.getElementById('btnGetGoods').style.display = 'none';
                            document.getElementById('unbtnGetGoods').style.display = '';
                        }
                        catch (e) { }
                        document.getElementById("imgSave").style.display = "none";
                        document.getElementById("imgUnSave").style.display = "none";
                        document.getElementById("imgAdd").style.display = "none";
                        document.getElementById("imgUnAdd").style.display = "none";
                        document.getElementById("imgDel").style.display = "none";
                        document.getElementById("imgUnDel").style.display = "none";
                        document.getElementById("Get_Potential").style.display = "none";
                        document.getElementById("Get_UPotential").style.display = "none";
                        document.getElementById("btn_confirm").style.display = "none";
                        document.getElementById("btn_Qxconfirm").style.display = "none";
                        document.getElementById("btn_UnQxconfirm").style.display = "none";
                        document.getElementById("btn_ruku").style.display = "none";
                    }
                    catch (e)
                            { }

                    document.getElementById("divInDate").style.display = "inline";
                    document.getElementById("divInUserID").style.display = "inline";


                    var no6 = parseFloat(document.getElementById("txtPayedTotal").value.Trim()); //已退货款
                    var no7 = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
                    var no8 = parseFloat(document.getElementById("txtWairPayTotalOverage").value.Trim()); //应退货款余额
                    document.getElementById("txtSettleTotal").value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
                    document.getElementById("txtPayedTotal").value = FormatAfterDotNumber(no6 + no7, parseInt($("#hidSelPoint").val()));

                    if (no8 > 0) {
                        document.getElementById("divTitle").innerText = "销售退货管理--结算";
                        document.getElementById("btn_jiesuan").style.display = "inline";
                        document.getElementById("btn_Unconfirm").style.display = "none";

                        //先设置所有文本控件只读
                        $(":text").each(function() {
                            this.disabled = true;
                        });
                        document.getElementById('drpFromType').disabled = true;
                        document.getElementById('ddlSendMode').disabled = true;
                        document.getElementById('ddlOutDateHour').disabled = true;
                        document.getElementById('ddlOutDateMin').disabled = true;
                        document.getElementById('chkisAddTax').disabled = true;
                        document.getElementById('ddlBackDateHour').disabled = true;
                        document.getElementById('ddlBackDateMin').disabled = true;
                        document.getElementById('ddlInDateHour').disabled = true;
                        document.getElementById('ddlInDateMin').disabled = true;
                        document.getElementById('drpCurrencyType').disabled = true;
                        document.getElementById('txtBackReason').disabled = true;
                        document.getElementById('txtRemark').disabled = true;
                        document.getElementById('divUploadResume').disabled = true;
                        document.getElementById('divDeleteResume').disabled = true;


                        //默认的结算人和结算时间显示，并且不允许修改
                        document.getElementById("UserSttlUserID").value = document.getElementById("UserName").value;
                        document.getElementById("HidSttlUserID").value = document.getElementById("UserID").value;
                        var SttlDate = document.getElementById("SystemTime2").value;
                        var SttlDate1 = SttlDate.split(' ')[0];
                        var SttlDatehour = SttlDate.split(' ')[1].split(':')[0];
                        var SttlDatemin = SttlDate.split(' ')[1].split(':')[1];
                        document.getElementById("txtSttlDate").value = SttlDate1;
                        document.getElementById("ddlSttlDateHour").value = SttlDatehour;
                        document.getElementById("ddlSttlDateMin").value = SttlDatemin;
                        //设置部分控件可用
                        document.getElementById('txtWairPayTotal').disabled = false;
                        document.getElementById('txtSettleTotal').disabled = false;
                    }
                    else {
                        document.getElementById("divTitle").innerText = "销售退货管理--完成";
                        document.getElementById("btn_jiesuan").style.display = "none";
                        document.getElementById("btn_Unconfirm").style.display = "inline";

                        document.getElementById("txtCloserReal").value = document.getElementById("UserName").value;
                        document.getElementById("txtCloser").value = document.getElementById("UserID").value;
                        document.getElementById("txtCloseDate").value = document.getElementById("SystemTime").value;

                        //设置所有文本控件只读
                        $(":text").each(function() {
                            this.disabled = true;
                        });
                        document.getElementById('drpFromType').disabled = true;
                        document.getElementById('ddlSendMode').disabled = true;
                        document.getElementById('ddlOutDateHour').disabled = true;
                        document.getElementById('ddlOutDateMin').disabled = true;
                        document.getElementById('chkisAddTax').disabled = true;
                        document.getElementById('ddlBackDateHour').disabled = true;
                        document.getElementById('ddlBackDateMin').disabled = true;
                        document.getElementById('ddlInDateHour').disabled = true;
                        document.getElementById('ddlInDateMin').disabled = true;
                        document.getElementById('drpCurrencyType').disabled = true;
                        document.getElementById('txtBackReason').disabled = true;
                        document.getElementById('txtRemark').disabled = true;
                        document.getElementById('divUploadResume').disabled = true;
                        document.getElementById('divDeleteResume').disabled = true;
                    }

                }
            }
        });

    }
}



//确认按扭结算用
function Fun_JiesuanOperate() {
    var c = window.confirm("确认执行结算操作吗？")
    if (c == true) {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;

        //校验结算时间和结算人不能为空
        if (document.getElementById("txtSttlDate").value == "") {
            isFlag = false;
            fieldText += "结算时间|";
            msgText += "请输入结算时间|";
        }
        if (document.getElementById("UserSttlUserID").value == "") {
            isFlag = false;
            fieldText += "结算人|";
            msgText += "请输入结算人|";
        }

        //金额格式判断
        if (document.getElementById("txtWairPayTotal").value != "") {
            if (IsNumberOrNumeric(document.getElementById("txtWairPayTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
                isFlag = false;
                fieldText += "应退货款|";
                msgText += "应退货款格式不正确！|";
            }
        }

        if (document.getElementById("txtSettleTotal").value != "") {
            if (IsNumberOrNumeric(document.getElementById("txtSettleTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
                isFlag = false;
                fieldText += "结算货款|";
                msgText += "结算货款格式不正确！|";
            }
        }

        if (document.getElementById("txtPayedTotal").value != "") {
            if (IsNumberOrNumeric(document.getElementById("txtPayedTotal").value, 12, parseInt($("#hidSelPoint").val())) == false) {
                isFlag = false;
                fieldText += "已退货款|";
                msgText += "已退货款格式不正确！|";
            }
        }

        if (document.getElementById("txtWairPayTotalOverage").value != "") {
            if (IsNumberOrNumeric(document.getElementById("txtWairPayTotalOverage").value, 12, parseInt($("#hidSelPoint").val())) == false) {
                isFlag = false;
                fieldText += "应退货款余额|";
                msgText += "应退货款余额格式不正确！|";
            }
        }


        var no1 = parseFloat(document.getElementById("txtPayedTotal").value.Trim()); //已退货款
        var no2 = parseFloat(document.getElementById("txtWairPayTotal").value.Trim()); //应退货款
        var no3 = parseFloat(document.getElementById("txtRealTotal").value.Trim()); //折后含税金额
        var no4 = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
        var no5 = parseFloat(document.getElementById("txtWairPayTotalOverage").value.Trim()); //应退货款余额

        if (no1 > no3) {
            isFlag = false;
            fieldText += "已退货款|";
            msgText += "已退货款不能大于折后含税金额|";
        }
        if (no2 > no3) {
            isFlag = false;
            fieldText += "应退货款|";
            msgText += "应退货款不能大于折后含税金额|";
        }
        if (no4 > no3) {
            isFlag = false;
            fieldText += "结算货款|";
            msgText += "结算货款不能大于折后含税金额|";
        }
        if (no5 > no3) {
            isFlag = false;
            fieldText += "应退货款余额|";
            msgText += "应退货款余额不能大于折后含税金额|";
        }
        if (no1 > no2) {
            isFlag = false;
            fieldText += "已退货款|";
            msgText += "已退货款不能大于应退货款|";
        }
        if (no4 > no2) {
            isFlag = false;
            fieldText += "结算货款|";
            msgText += "结算货款不能大于应退货款|";
        }
        if (no5 > no2) {
            isFlag = false;
            fieldText += "应退货款余额|";
            msgText += "应退货款余额不能大于应退货款|";
        }

        if (!isFlag) {
            popMsgObj.Show(fieldText, msgText);
            return;
        }



        if (no5 > 0) {
            document.getElementById("ddlBillStatus").value = "2"; //改变单据状态显示
            document.getElementById("drpBusiStatus").value = "3"; //改变业务状态显示
        }
        else {
            document.getElementById("ddlBillStatus").value = "5"; //改变单据状态显示
            document.getElementById("drpBusiStatus").value = "4"; //改变业务状态显示
        }


        glb_BillID = document.getElementById('txtIndentityID').value;
        var sttlDate = $("#txtSttlDate").val() + " " + $("#ddlSttlDateHour").val() + ":" + $("#ddlSttlDateMin").val(); //结算时间
        var sttlUserID = $("#HidSttlUserID").val(); //结算人
        action = "Jiesuan";

        var payedTotal = FormatAfterDotNumber(parseFloat(document.getElementById("txtPayedTotal").value.Trim()) + parseFloat(document.getElementById("txtSettleTotal").value.Trim()), parseInt($("#hidSelPoint").val())); //已退款金额+应退
        var wairPayTotal = document.getElementById('txtWairPayTotal').value; //应退款金额
        var closer = document.getElementById("UserID").value;
        var busiStatus = document.getElementById("drpBusiStatus").value;
        var billStatus = document.getElementById("ddlBillStatus").value;
        var BackNo = document.getElementById("divSubSellBackNo").innerHTML;
        var strParams = "action=" + action + "\
                        &sttlDate=" + sttlDate + "\
                        &sttlUserID=" + sttlUserID + "\
                        &busiStatus=" + busiStatus + "\
                        &billStatus=" + billStatus + "\
                        &payedTotal=" + payedTotal + "\
                        &wairPayTotal=" + wairPayTotal + "\
                        &closer=" + closer + "\
                        &ID=" + glb_BillID + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SubStoreManager/SubSellBackAdd.ashx?" + strParams,

            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
                //popMsgObj.ShowMsg('sdf');
            },
            success: function(data) {
                if (data.sta > 0) {
                    popMsgObj.ShowMsg('结算成功');
                    document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedUserIDReal").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;
                    try {
                        try {
                            document.getElementById('btnGetGoods').style.display = 'none';
                            document.getElementById('unbtnGetGoods').style.display = '';
                        }
                        catch (e) { }
                        document.getElementById("imgSave").style.display = "none";
                        document.getElementById("imgUnSave").style.display = "none";
                        document.getElementById("imgAdd").style.display = "none";
                        document.getElementById("imgUnAdd").style.display = "none";
                        document.getElementById("imgDel").style.display = "none";
                        document.getElementById("imgUnDel").style.display = "none";
                        document.getElementById("Get_Potential").style.display = "none";
                        document.getElementById("Get_UPotential").style.display = "none";
                        document.getElementById("btn_confirm").style.display = "none";
                        document.getElementById("btn_ruku").style.display = "none";
                    }
                    catch (e)
                            { }

                    document.getElementById("divInDate").style.display = "inline";
                    document.getElementById("divInUserID").style.display = "inline";
                    document.getElementById("divSttlDate").style.display = "inline";
                    document.getElementById("divSttlUserID").style.display = "inline";

                    var no6 = parseFloat(document.getElementById("txtPayedTotal").value.Trim()); //已退货款
                    var no7 = parseFloat(document.getElementById("txtSettleTotal").value.Trim()); //结算货款
                    var no8 = parseFloat(document.getElementById("txtWairPayTotalOverage").value.Trim()); //应退货款余额
                    document.getElementById("txtSettleTotal").value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val()));
                    document.getElementById("txtPayedTotal").value = FormatAfterDotNumber(no6 + no7, parseInt($("#hidSelPoint").val()));

                    if (no8 > 0) {
                        document.getElementById("divTitle").innerText = "销售退货管理--结算";
                        try {
                            document.getElementById("btn_jiesuan").style.display = "inline";
                            document.getElementById("btn_Unconfirm").style.display = "none";
                        }
                        catch (e)
                                { }

                        //先设置所有文本控件只读
                        $(":text").each(function() {
                            this.disabled = true;
                        });
                        document.getElementById('drpFromType').disabled = true;
                        document.getElementById('ddlSendMode').disabled = true;
                        document.getElementById('ddlOutDateHour').disabled = true;
                        document.getElementById('ddlOutDateMin').disabled = true;
                        document.getElementById('chkisAddTax').disabled = true;
                        document.getElementById('ddlBackDateHour').disabled = true;
                        document.getElementById('ddlBackDateMin').disabled = true;
                        document.getElementById('ddlInDateHour').disabled = true;
                        document.getElementById('ddlInDateMin').disabled = true;
                        document.getElementById('drpCurrencyType').disabled = true;
                        document.getElementById('txtBackReason').disabled = true;
                        document.getElementById('txtRemark').disabled = true;
                        document.getElementById('divUploadResume').disabled = true;
                        document.getElementById('divDeleteResume').disabled = true;


                        //默认的结算人和结算时间显示，并且不允许修改
                        document.getElementById("UserSttlUserID").value = document.getElementById("UserName").value;
                        document.getElementById("HidSttlUserID").value = document.getElementById("UserID").value;
                        var SttlDate = document.getElementById("SystemTime2").value;
                        var SttlDate1 = SttlDate.split(' ')[0];
                        var SttlDatehour = SttlDate.split(' ')[1].split(':')[0];
                        var SttlDatemin = SttlDate.split(' ')[1].split(':')[1];
                        document.getElementById("txtSttlDate").value = SttlDate1;
                        document.getElementById("ddlSttlDateHour").value = SttlDatehour;
                        document.getElementById("ddlSttlDateMin").value = SttlDatemin;
                        //设置部分控件可用
                        document.getElementById('txtWairPayTotal').disabled = false;
                        document.getElementById('txtSettleTotal').disabled = false;
                    }
                    else {
                        document.getElementById("divTitle").innerText = "销售退货管理--完成";
                        try {
                            document.getElementById("btn_jiesuan").style.display = "none";
                            document.getElementById("btn_Unconfirm").style.display = "inline";
                        }
                        catch (e)
                                { }

                        document.getElementById("txtCloserReal").value = document.getElementById("UserName").value;
                        document.getElementById("txtCloser").value = document.getElementById("UserID").value;
                        document.getElementById("txtCloseDate").value = document.getElementById("SystemTime").value;

                        //设置所有文本控件只读
                        $(":text").each(function() {
                            this.disabled = true;
                        });
                        document.getElementById('drpFromType').disabled = true;
                        document.getElementById('ddlSendMode').disabled = true;
                        document.getElementById('ddlOutDateHour').disabled = true;
                        document.getElementById('ddlOutDateMin').disabled = true;
                        document.getElementById('chkisAddTax').disabled = true;
                        document.getElementById('ddlBackDateHour').disabled = true;
                        document.getElementById('ddlBackDateMin').disabled = true;
                        document.getElementById('drpCurrencyType').disabled = true;
                        document.getElementById('txtBackReason').disabled = true;
                        document.getElementById('txtRemark').disabled = true;
                        document.getElementById('divUploadResume').disabled = true;
                        document.getElementById('divDeleteResume').disabled = true;
                    }

                }
            }
        });

    }
}




//结单或取消结单按钮操作
function Fun_CompleteOperate(isComplete) {

    glb_BillID = document.getElementById('txtIndentityID').value;
    document.getElementById("txtCloserReal").value = document.getElementById("UserName").value;
    document.getElementById("txtCloser").value = document.getElementById("UserID").value;
    document.getElementById("txtCloseDate").value = document.getElementById("SystemTime").value;
    var closer = document.getElementById("txtCloser").value;
    var rejectNo = document.getElementById("divSubSellBackNo").innerHTML;

    if (isComplete) {
        action = "Close";
    }
    else {
        action = "CancelClose";
    }
    //结单操作

    var strParams = "action=" + action + "\
                                 &closer=" + closer + "\
                                 &rejectNo=" + rejectNo + "\
                                 &ID=" + glb_BillID + "";
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseRejectAdd.ashx?" + strParams,

        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
        },
        success: function(data) {
            if (data.sta > 0) {
                if (data.sta == 1) {
                    document.getElementById("ddlBillStatus").value = "4";
                    document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedUserIDReal").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;
                    fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                    fnFlowStatus($("#FlowStatus").val());
                }
                else {
                    document.getElementById("ddlBillStatus").value = "2";
                    document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedUserIDReal").value = document.getElementById("txtModifiedUserID2").value;
                    document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;
                    fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                    fnFlowStatus($("#FlowStatus").val());
                }
                popMsgObj.ShowMsg(data.info);
                GetFlowButton_DisplayControl(); //审批处理
            }
        }
    });
}



function Fun_FlowApply_Operate_Succeed(operateType) {
    try {
        if (operateType == "0") {//提交审批成功后,不可改
            $("#imgUnSave").css("display", "inline"); //保存灰
            $("#imgSave").css("display", "none"); //保存
            try {
                document.getElementById('btnGetGoods').style.display = 'none';
                document.getElementById('unbtnGetGoods').style.display = '';
            }
            catch (e) { }
            $("#imgAdd").css("display", "none"); //明细添加
            $("#imgUnAdd").css("display", "inline"); //明细添加灰
            $("#imgDel").css("display", "none"); //明细删除
            $("#imgUnDel").css("display", "inline"); //明细删除灰
            $("#Get_Potential").css("display", "none"); //源单总览
            $("#Get_UPotential").css("display", "inline"); //源单总览灰
        }
        else if (operateType == "1") {//审批成功后，不可改
            $("#imgUnSave").css("display", "inline");
            $("#imgSave").css("display", "none");
            try {
                document.getElementById('btnGetGoods').style.display = 'none';
                document.getElementById('unbtnGetGoods').style.display = '';
            }
            catch (e) { }

            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#Get_Potential").css("display", "none");
            $("#Get_UPotential").css("display", "inline");
        }
        else if (operateType == "2") {//审批不通过
            $("#imgUnSave").css("display", "none");
            $("#imgSave").css("display", "inline");
            try {
                document.getElementById('btnGetGoods').style.display = '';
                document.getElementById('unbtnGetGoods').style.display = 'none';
            }
            catch (e) { }

            $("#imgAdd").css("display", "inline");
            $("#imgUnAdd").css("display", "none");
            $("#imgDel").css("display", "inline");
            $("#imgUnDel").css("display", "none");
            $("#Get_Potential").css("display", "inline");
            $("#Get_UPotential").css("display", "none");
        }
    }
    catch (e)
    { }
}

//根据单据状态决定页面按钮操作
function fnStatus(BillStatus, Isyinyong) {
    try {
        switch (BillStatus) { //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
            case '1': //制单
                break;
            case '2': //执行
                if (Isyinyong == 'True') {//被引用不可编辑
                    $("#imgSave").css("display", "none");
                    try {
                        document.getElementById('btnGetGoods').style.display = 'none';
                        document.getElementById('unbtnGetGoods').style.display = '';
                    }
                    catch (e) { }
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#Get_Potential").css("display", "none");
                    $("#Get_UPotential").css("display", "inline");
                }
                else {
                    try {
                        document.getElementById('btnGetGoods').style.display = '';
                        document.getElementById('unbtnGetGoods').style.display = 'none';
                    }
                    catch (e) { }
                    $("#imgSave").css("display", "inline");
                    $("#imgUnSave").css("display", "none");
                    $("#imgAdd").css("display", "inline");
                    $("#imgUnAdd").css("display", "none");
                    $("#imgDel").css("display", "inline");
                    $("#imgUnDel").css("display", "none");
                    $("#Get_Potential").css("display", "inline");
                    $("#Get_UPotential").css("display", "none");
                }

                break;
            case '3': //变更
                $("#FromType").attr("disabled", "disabled");
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
                try {
                    document.getElementById('btnGetGoods').style.display = '';
                    document.getElementById('unbtnGetGoods').style.display = 'none';
                }
                catch (e) { }
                break;
            case '4': //手工结单
                try {
                    document.getElementById('btnGetGoods').style.display = 'none';
                    document.getElementById('unbtnGetGoods').style.display = '';
                }
                catch (e) { }
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                break;

            case '5':
                try {
                    document.getElementById('btnGetGoods').style.display = 'none';
                    document.getElementById('unbtnGetGoods').style.display = '';
                }
                catch (e) { }
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                break;
        }
    }
    catch (e)
    { }
}

function fnFlowStatus(FlowStatus) {
    try {
        switch (FlowStatus) {
            case "": //未提交审批         
                break;
            case "待审批": //当前单据正在待审批
                try {
                    document.getElementById('btnGetGoods').style.display = 'none';
                    document.getElementById('unbtnGetGoods').style.display = '';
                }
                catch (e) { }
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                break;
            case "审批中": //当前单据正在审批中
                try {
                    document.getElementById('btnGetGoods').style.display = 'none';
                    document.getElementById('unbtnGetGoods').style.display = '';
                }
                catch (e) { }
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                break;
            case "审批通过": //当前单据已经通过审核
                //制单状态的审批通过单据,不可修改
                if ($("#ddlBillStatus").val() == "1") {
                    try {
                        document.getElementById('btnGetGoods').style.display = 'none';
                        document.getElementById('unbtnGetGoods').style.display = '';
                    }
                    catch (e) { }
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#Get_Potential").css("display", "none");
                    $("#Get_UPotential").css("display", "inline");
                }

                break;
            case "审批不通过": //当前单据审批未通过
                break;
        }
    }
    catch (e)
   { }
}



function clearProviderdiv() {
    document.getElementById("txtProviderID").value = "";
    document.getElementById("txtHidProviderID").value = "";
}

function PrintSellBack() {
    var BillID = $("#txtIndentityID").val();
    if (BillID == "" || parseInt(BillID) < 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先保存！");
        return;
    }
    window.open("../../../Pages/PrinttingModel/SubStoreManager/SubSellBackPrint.aspx?ID=" + BillID);
}







function Numb_roundPur(numberStr, fractionDigits) {
    var num = numberStr.toString().split('.');
    if (num[1] != "" && num[1] != null && parseInt(num[1]) == 0) {
        return numberStr;
    }
    else {
        if (numberStr != parseInt(numberStr)) {
            with (Math) {
                return round(numberStr * pow(10, fractionDigits)) / pow(10, fractionDigits);
            }
        }
        else {
            return numberStr + ".00";
        }
    }
}

//-------------------------------------------------------------------------------------------------条码扫描需要
// 门店条码添加功能
function SubGetGoodsDataByBarCode(ProductID, ProductNo, ProductName,
                                                  UnitID, UnitName, Specification,
                                                  SubPriceTax, SubPrice, SubTax,
                                                  Discount, IsBatchNo) {
    AddSignRowSearch(ProductID, ProductNo, ProductName, Specification, UnitID, UnitName, SubPriceTax, SubPrice, SubTax, Discount, IsBatchNo);
}

function GetGoodsDataByBarCode(ProductID, ProductNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount,
                    Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, IsBatchNo, BatchNo
                    , ProductCount, CurrentStore, Source, ColorName) {
    AddSignRowSearch(ProductID, ProductNo, ProductName, Specification, UnitID, UnitName, SellTax, StandardSell, TaxRate / 100, Discount, IsBatchNo);
}

// 条码添加数据
function AddSignRowSearch(ProductID, ProductNo, ProductName, Specification, UnitID, UnitName, SubPriceTax, SubPrice, SubTax, Discount, IsBatchNo) {

    var txtTRLastIndex = findObj("txtTRLastIndex", document);
    var signFrame = findObj("dg_Log", document);
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    else {
        for (var i = 0; i < txtTRLastIndex.value - 1; i++) {
            if (signFrame.rows[i + 1].style.display != "none") {
                var checkPro = document.getElementById('txtProductID' + (i + 1));
                if (checkPro.value == ProductID) {
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        if ($("#txtUsedUnitCount" + (i + 1)).val() == "") {
                            $("#txtUsedUnitCount" + (i + 1)).val(FormatAfterDotNumber(1, $("#hidSelPoint").val()));
                        }
                        $("#txtUsedUnitCount" + (i + 1)).val(FormatAfterDotNumber(parseFloat($("#txtUsedUnitCount" + (i + 1)).val()) + 1, $("#hidSelPoint").val()));
                    }
                    else {
                        if ($("#txtBackCount" + (i + 1)).val() == "") {
                            $("#txtBackCount" + (i + 1)).val(FormatAfterDotNumber(1, $("#hidSelPoint").val()));
                        }
                        $("#txtBackCount" + (i + 1)).val(FormatAfterDotNumber(parseFloat($("#txtBackCount" + (i + 1)).val()) + 1, $("#hidSelPoint").val()));
                    }
                    ChangeUnit(this, (i + 1));
                    return;
                }
            }
        }
    }
    var rowID = AddSignRow();
    $("#txtProductID" + rowID).val(ProductID); // 物品ID
    $("#txtProductNo" + rowID).val(ProductNo); // 物品编号
    $("#txtProductName" + rowID).val(ProductName); // 物品名称
    $("#txtstandard" + rowID).val(Specification); // 规格
    $("#txtUnitID" + rowID).val(UnitID); // 单位ID
    $("#txtUnitName" + rowID).val(UnitName); // 单位ID
    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        $("#txtUsedUnitCount" + rowID).val(FormatAfterDotNumber(1, parseInt($("#hidSelPoint").val()))); // 单位ID
    }
    else {
        $("#txtBackCount" + rowID).val(FormatAfterDotNumber(1, parseInt($("#hidSelPoint").val()))); // 单位ID
    }
    $("#txtUnitPrice" + rowID).val(FormatAfterDotNumber(SubPrice, parseInt($("#hidSelPoint").val()))); // 单价
    $("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(SubPrice, parseInt($("#hidSelPoint").val()))); // 单价
    $("#txtTaxPrice" + rowID).val(FormatAfterDotNumber(SubPriceTax, parseInt($("#hidSelPoint").val()))); // 含税价
    $("#hiddTaxPrice" + rowID).val(FormatAfterDotNumber(SubPriceTax, parseInt($("#hidSelPoint").val()))); // 含税价
    $("#txtDiscount" + rowID).val(Discount); // 折扣
    $("#txtTaxRate" + rowID).val(FormatAfterDotNumber(SubTax, parseInt($("#hidSelPoint").val()))); // 含税价
    $("#hiddTaxRate" + rowID).val(FormatAfterDotNumber(SubTax, parseInt($("#hidSelPoint").val()))); // 含税价
    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        GetUnitGroupSelectEx(ProductID, 'SaleUnit', 'txtUsedUnit' + rowID, 'ChangeUnit(this,' + rowID + ')', "td_UsedUnitID_" + rowID, '', 'ChangeUnit(this,' + rowID + ')');
    }
    SetSubBatchSelect(rowID, ProductID, IsBatchNo, '')


    var isAddTax = $("#chkisAddTax").attr("checked");
    //新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();

    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {//是增值税则
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxRate" + rowIndex).value, parseInt($("#hidSelPoint").val())); //税率等于隐藏域税率
                $("#labAddTax").html("是增值税");
            }
            else {
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //含税价等于单价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value / Rate, parseInt($("#hidSelPoint").val())); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, parseInt($("#hidSelPoint").val())); //税率等于0
                $("#labAddTax").html("非增值税");
            }
        }
    }
}


function GetGoodsInfo() {
    if ($("#ddlSendMode").val() == "1") {
        SubGetGoodsInfoByBarCode();
    }
    else {
        GetGoodsInfoByBarCode();
    }
}
