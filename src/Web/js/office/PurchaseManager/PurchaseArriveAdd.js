//采购到货通知

var DtlS_Item = new Array();
var Dtl_Item2 = new Array();
var DtlCount = 0;

var page = "";
var arriveNo;
var Title;
var TypeID;
var Purchaser;
var PurchaserName;
var FromType;
var ProviderID;
var ProviderName;
var BillStatus;
var UsedStatus;
var Isliebiao;
var _id = 0;

$(document).ready(function() {
    requestobj = GetRequest(location.search);
    $("#hidSearchCondition").val(location.search.substr(1));
    var intMasterArriveID = $("#txtIndentityID").val();
    var intFromType = requestobj["intFromType"];
    if (intFromType != null) {
        $("#btn_back").show(); // 桌面返回修改
    }
    if (intMasterArriveID != null) {
        DealPage(intMasterArriveID);
    }
    GetFlowButton_DisplayControl();
});


// 获取url中"?"符后的字串
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

// 处理加载页面
function DealPage(recordnoparam) {
    if (recordnoparam != 0) {
        $("#txtAction").val("2");
        document.getElementById("divTitle").innerText = "采购到货单";
        GetArriveInfo(recordnoparam);
    }
    else {
        GetExtAttr('officedba.PurchaseArrive', null);
        $("#txtAction").val("1");
        document.getElementById("divTitle").innerText = "新建采购到货单";
    }
}

// 返回
function Back() {
    var URLParams = $("#hidSearchCondition").val();
    var requestobj2 = GetRequest(location.search);
    var intFromType = requestobj2["intFromType"];
    switch (intFromType) {
        case "1":
            URLParams = URLParams.replace("ModuleID=2041901", "ModuleID=2041902");
            window.location.href = 'PurchaseArriveInfo.aspx?' + URLParams;
            break;
        case "2":
            window.location.href = '../../../Desktop.aspx';
            break;
        case "3":
            window.location.href = '../../Personal/WorkFlow/FlowMyApply.aspx?ModuleID=' + ListModuleID;
            break;
        case "4":
            window.location.href = '../../Personal/WorkFlow/FlowMyProcess.aspx?ModuleID=' + ListModuleID;
            break;
        case "5":
            window.location.href = '../../Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID=' + ListModuleID;
            break;
    }
}

// 修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
    var strKey = $.trim($("#hiddKey").val()); // 扩展属性字段值

    var arrKey = strKey.split('|');
    if (typeof (data) != 'undefined') {
        $.each(data, function(i, item) {
            for (var t = 0; t < arrKey.length; t++) {
                // 不为空的字段名才取值
                if ($.trim(arrKey[t]) != '') {
                    $("#" + $.trim(arrKey[t])).val(item[$.trim(arrKey[t])]);

                }
            }

        });
    }
}
// --------------------扩展属性操作----------------------------------------------------------------------------//
function GetArriveInfo(ID) {
    var arriveNo = $("#txtIsliebiaoNo").val();
    $.ajax({
        type: "POST", // 用POST方式传输
        dataType: "json", // 数据格式:JSON
        url: "../../../Handler/Office/PurchaseManager/PurchaseArriveEdit.ashx", // 目标地址
        data: "ID=" + ID + "",
        cache: false,
        beforeSend: function() { AddPop(); }, // 发送数据之前
        success: function(msg) {
            // 数据获取完毕，填充页面据显示
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    // 基本信息
                    _id = item.ID;
                    $("#CodingRuleControl1_txtCode").val(item.ArriveNo); // 合同编号
                    $("#divPurchaseArriveNo").val(item.ArriveNo); // 合同编号
                    $("#txtTitle").val(item.Title); // 主题
                    $("#drpFromType").val(item.FromType); // 源单类型id
                    $("#drpTypeID").val(item.TypeID); // 采购类别
                    $("#HidDeptID").val(item.DeptID); // 部门id
                    $("#DeptDeptID").val(item.DeptName); // 部门名称
                    $("#txtProviderID").val(item.ProviderName); // 供应商名称
                    $("#txtHidProviderID").val(item.ProviderID); // 供应商ID
                    $("#drpTakeType").val(item.TakeType); // 交货方式
                    $("#drpCarryType").val(item.CarryType); // 运送方式
                    $("#drpPayType").val(item.PayType); // 结算方式
                    $("#txtRate").val(FormatAfterDotNumber(item.Rate, 4)); // 汇率
                    $("#hidRate").val(item.Rate); // 汇率
                    $("#txtHidOurDept").val(item.DeptID); // 部门id
                    if (item.isAddTax == 1) {
                        document.getElementById("chkisAddTax").checked = true;
                        $("#AddTax").html("是增值税");
                    }
                    else {
                        document.getElementById("chkisAddTax").checked = false;
                        $("#AddTax").html("非增值税");
                    }

                    for (var i = 0; i < document.getElementById("drpCurrencyType").options.length; ++i) {
                        if (document.getElementById("drpCurrencyType").options[i].value.split('_')[0] == item.CurrencyType) {
                            $("#drpCurrencyType").attr("selectedIndex", i);
                            break;
                        }
                    }
                    $("#CurrencyTypeID").val(item.CurrencyType); // 币种ID
                    $("#HidCheckUserID").val(item.CheckUserID); // 点收人ID
                    $("#UserCheckUserID").val(item.CheckUserName); // 点收人名称
                    $("#txtCheckDate").attr("title", item.CheckDate); // 点收日期
                    $("#txtAddress").val(item.SignAddr); // 签约地点
                    $("#drpMoneyType").val(item.MoneyType); // 支付方式
                    $("#HidPurchaser").val(item.Purchaser); // 采购员ID
                    $("#UserPurchaser").val(item.PurchaserName); // 采购员名称
                    $("#txtArriveDate").val(item.ArriveDate); // 到货日期
                    $("#txtSendAddress").val(item.SendAddress); // 发货地址
                    $("#txtReceiveOverAddress").val(item.ReceiveOverAddress); // 发货地址
                    $("#hidProjectID").val(item.ProjectID); // 项目ID
                    $("#txtProject").val(item.ProjectName); // 项目名称
                    // 合计信息
                    $("#txtCountTotal").val(FormatAfterDotNumber(item.CountTotal, selPoint)); // 采购到货数量合计
                    $("#txtTotalMoney").val(FormatAfterDotNumber(item.TotalMoney, selPoint)); // 金额合计
                    $("#txtTotalTaxHo").val(FormatAfterDotNumber(item.TotalTax, selPoint)); // 税额合计
                    $("#txtTotalFeeHo").val(FormatAfterDotNumber(item.TotalFee, selPoint)); // 含税金额合计
                    $("#txtDiscount").val(FormatAfterDotNumber(item.Discount, selPoint)); // 整单折扣
                    $("#txtDiscountTotal").val(FormatAfterDotNumber(item.DiscountTotal, selPoint)); // 折扣金额
                    $("#txtRealTotal").val(FormatAfterDotNumber(item.RealTotal, selPoint)); // 折后含税金额
                    $("#txtOtherTotal").val(FormatAfterDotNumber(item.OtherTotal, selPoint)); // 其它费用支出合计
                    // 备注信息
                    $("#txtCreator").val(item.Creator); // 制单人id
                    $("#txtCreatorReal").val(item.CreatorName); // 制单人名称
                    $("#txtCreateDate").val(item.CreateDate); // 制单时间
                    $("#ddlBillStatus").val(item.BillStatus); // 单据状态
                    $("#txtConfirmor").val(item.Confirmor); // 确认人id
                    $("#txtConfirmorReal").val(item.ConfirmorName); // 确认人名称
                    $("#txtConfirmDate").val(item.ConfirmDate); // 确认时间
                    $("#txtCloser").val(item.Closer); // 结单人id
                    $("#txtCloserReal").val(item.CloserName); // 结单人名称
                    $("#txtCloseDate").val(item.CloseDate); // 结单日期
                    $("#txtModifiedUserID").val(item.ModifiedUserID); // 最后更新人id
                    $("#txtModifiedUserIDReal").val(item.ModifiedUserID); // 最后更新人id显示
                    $("#txtModifiedDate").val(item.ModifiedDate); // 最后更新时间
                    $("#hfPageAttachment").val(item.Attachment); // 附件
                    $("#txtRemark").val(item.Remark); // 备注

                    $("#txtCanUserName").val(item.UserName); //附件
                    $("#txtCanUserID").val(item.CanUserID); //     备注
                    if (item.Attachment != "") {
                        // 下载删除显示
                        $("#divDealResume").css("display", "block");
                        // 上传不显示
                        document.getElementById("divUploadResume").style.display = "none"
                    }
                    $("#divPurchaseArriveNo").html(item.ArriveNo);
                    $("#divPurchaseArriveNo").css("display", "block");
                    $("#divInputNo").css("display", "none");

                    $("#FlowStatus").val(item.UsedStatus);

                    $("#Isyinyong").val(item.IsCite);
                    fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                    var TableName = "officedba.PurchaseArrive";
                    GetExtAttr(TableName, msg.data);

                    if (item.BillStatus == "2" && item.isOpenbill == "0") {
                        $("#imgBilling").css("display", "inline");
                        $("#imgUnBilling").css("display", "none");
                    }
                    else {
                        $("#imgBilling").css("display", "none");
                        $("#imgUnBilling").css("display", "inline");
                    }
                }

            });
            if (typeof (msg.data2) == "undefined") { }
            else {
                $.each(msg.data2, function(i, item) {
                    var FromTypeName = $("#hidFromTypeName").val().Trim();
                    var Rate = $("#hidRate").val().Trim();
                    if (FromTypeName == "采购订单") {
                        document.getElementById('txtProviderID').disabled = true;
                        document.getElementById('drpCurrencyType').disabled = true;
                    }
                    var index = AddSignRow();
                    $("#txtProductID" + index).val(item.ProductID);
                    $("#txtProductNo" + index).val(item.ProductNo);
                    $("#txtProductName" + index).val(item.ProductName);
                    $("#txtstandard" + index).val(item.Specification);
                    $("#txtColorName" + index).val(item.ColorName);
                    $("#hidExistProduct_" + index).val('1'); // 判断产品存在
                    if (isMoreUnit) {// 启用多计量单位
                        $("#UsedUnitID" + index).val(item.UnitID);
                        $("#UsedUnitName" + index).val(item.UnitName);
                        $("#UsedUnitCount" + index).val(FormatAfterDotNumber(item.ProductCount, selPoint));
                        $("#txtProductCount" + index).val(FormatAfterDotNumber(item.UsedUnitCount, selPoint));
                        GetUnitGroupSelect(item.ProductID, 'InUnit', 'txtUnitID' + index, 'ChangeUnit(this,' + index + ',1)', 'tdUnitName' + index, item.UsedUnitID);
                        $("#txtProductCount" + index).blur(function() { ChangeUnit(this, index); });

                        $("#txtUnitPrice" + index).val(FormatAfterDotNumber(item.UsedPrice, selPoint));
                        $("#hiddUnitPrice" + index).val(FormatAfterDotNumber(item.UnitPrice * Rate, selPoint));
                    }
                    else {
                        $("#txtUnitID" + index).val(item.UnitID);
                        $("#txtUnitName" + index).val(item.UnitName);
                        $("#txtProductCount" + index).val(FormatAfterDotNumber(item.ProductCount, selPoint));

                        $("#txtUnitPrice" + index).val(FormatAfterDotNumber(item.UnitPrice, selPoint));
                        $("#hiddUnitPrice" + index).val(FormatAfterDotNumber(item.UnitPrice * Rate, selPoint));
                    }

                    var btshuliang = item.ProductOrder;
                    if (parseFloat(btshuliang) == 0) {
                        $("#txtProductOrder" + index).val("");
                    }
                    else {
                        $("#txtProductOrder" + index).val(FormatAfterDotNumber(item.ProductOrder, selPoint));
                    }
                    $("#txtArrivedCount" + index).val(FormatAfterDotNumber(item.ArrivedCount, selPoint));
                    $("#txtTaxPrice" + index).val(FormatAfterDotNumber(item.TaxPrice, selPoint));
                    $("#hiddTaxPrice" + index).val(FormatAfterDotNumber(item.TaxPrice * Rate, selPoint));
                    $("#txtTaxRate" + index).val(FormatAfterDotNumber(item.TaxRate, selPoint));
                    $("#hiddTaxRate" + index).val(FormatAfterDotNumber(item.HidTaxRate, selPoint));
                    $("#txtTotalPrice" + index).val(FormatAfterDotNumber(item.TotalPrice, selPoint));
                    $("#txtTotalFee" + index).val(FormatAfterDotNumber(item.TotalFee, selPoint));
                    $("#txtTotalTax" + index).val(FormatAfterDotNumber(item.TotalTax, selPoint));
                    $("#txtRemark" + index).val(item.Remark);
                    $("#txtFromBillID" + index).val(item.FromBillID);
                    $("#txtFromBillNo" + index).val(item.FromBillNo);

                    if (item.FromLineNo == "0") {
                        $("#txtFromLineNo" + index).val('');
                    }
                    else {
                        $("#txtFromLineNo" + index).val(item.FromLineNo);
                    }
                    $("#txtApplyCheckCount" + index).val(FormatAfterDotNumber(item.ApplyCheckCount, selPoint));
                    $("#txtCheckedCount" + index).val(FormatAfterDotNumber(item.CheckedCount, selPoint));
                    $("#txtPassCount" + index).val(FormatAfterDotNumber(item.PassCount, selPoint));
                    $("#txtNotPassCount" + index).val(FormatAfterDotNumber(item.NotPassCount, selPoint));
                    $("#txtInCount" + index).val(FormatAfterDotNumber(item.InCount, selPoint));
                    $("#txtBackCount" + index).val(FormatAfterDotNumber(item.BackCount, selPoint)); //    
                    if (msg.IsCite[0].IsCite == "True") {// 如果被引用，保存按钮隐藏
                        $("#Isyinyong").val("True");
                        $("#imgSave").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#Get_Potential").css("display", "none");
                        $("#Get_UPotential").css("display", "inline");
                        $("#btnGetGoods").css("display", "none"); // 条码扫描按钮
                    }
                    GetFlowButton_DisplayControl();
                });
            }
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        complete: function() { hidePopup(); } // 接收数据完毕
    });
}


function kong()
{ }


// 如果启用多计量单位重新计算基本数量等
function ReCacl() {
    if (!isMoreUnit) {
        return;
    }

    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
    var signFrame = findObj("dg_Log", document);
    var rowID = 0;
    for (var i = 0; i < txtTRLastIndex - 1; i++) {

        if (signFrame.rows[i + 1].style.display != "none") {
            rowID = i + 1;
            ChangeUnit(this, rowID);
        }
    }
    fnTotalInfo();
}

// 切换单位
function ChangeUnit(own, rowID, calPrice, source) {
    if ($("#txtProductID" + rowID).val() == "") {
        return;
    }

    var price = 0;
    var rate = 0;
    var unitPrice = $("#hiddUnitPrice" + rowID).val();
    var unitIDValue = $("#txtUnitID" + rowID).val()
    if (isMoreUnit && unitIDValue != null && unitIDValue.indexOf('|') > -1) {
        CalCulateNum('txtUnitID' + rowID, 'txtProductCount' + rowID, 'UsedUnitCount' + rowID, '', '', selPoint);
        if (unitPrice != '' && (calPrice == "1") && parseFloat(unitPrice) > 0) {
            $("#txtUnitPrice" + rowID).val(FormatAfterDotNumber(parseFloat(unitPrice) * parseFloat(unitIDValue.split('|')[1]), selPoint));
        }
    }
    if (source == "1") {// 操作含税价时计算单价
        price = parseFloat($("#txtTaxPrice" + rowID).val() == "" ? "0" : $("#txtTaxPrice" + rowID).val());
        rate = 1 + parseFloat($("#txtTaxRate" + rowID).val()) / 100;
        $("#txtUnitPrice" + rowID).val(FormatAfterDotNumber(price / rate, selPoint));
    }
    else {
        price = parseFloat($("#txtUnitPrice" + rowID).val() == "" ? "0" : $("#txtUnitPrice" + rowID).val());
        rate = 1 + parseFloat($("#txtTaxRate" + rowID).val()) / 100;
        $("#txtTaxPrice" + rowID).val(FormatAfterDotNumber(price * rate, selPoint));
    }
    fnTotalInfo();
}

// 新增到货通知单
function InsertPurchaseArrive() {
    // 重新计算
    ReCacl();

    if (!CheckBaseInfo()) return;

    var action = null;

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var arriveNo = "";
    var CodeType = $("#CodingRuleControl1_ddlCodeRule").val();

    // 基本信息

    if (CodeType == "") {
        arriveNo = $("#CodingRuleControl1_txtCode").val();
    }
    var title = document.getElementById('txtTitle').value.Trim(); // 单据主题
    var fromType = $("#drpFromType").val(); // 源单类型id
    var typeID = $("#drpTypeID").val(); // 采购类别id
    var deptid = $("#HidDeptID").val(); // 部门ID
    var providerID = $("#txtHidProviderID").val(); // 供应商id
    var purchase = $("#HidPurchaser").val(); // 采购员id
    var takeType = $("#drpTakeType").val(); // 交货方式id
    var carryType = $("#drpCarryType").val(); // 运送方式id
    var payeType = $("#drpPayType").val(); // 结算方式
    var rate = $("#txtRate").val(); // 汇率
    var currencyType = $("#CurrencyTypeID").val(); // 币种取隐藏域的值
    var isAddtax = ""; // 是否增值税
    if (document.getElementById("chkisAddTax").checked) {
        isAddtax = 1;
    }
    else {
        isAddtax = 0;
    }
    var checkUserID = $("#HidCheckUserID").val(); // 点收人ID
    var checkDate = $("#txtCheckDate").val(); // 点收日期
    var arriveDate = $("#txtArriveDate").val(); // 到货日期
    var moneyType = $("#drpMoneyType").val(); // 支付方式
    var sendAddress = $("#txtSendAddress").val(); // 发货地址
    var receiveOverAddress = $("#txtReceiveOverAddress").val(); // 收货地址
    var txtProjectID = $("#hidProjectID").val(); // 项目ID

    // 合计信息
    var countTotal = $("#txtCountTotal").val(); // 到货数量合计
    var totalMoney = $("#txtTotalMoney").val(); // 金额合计
    var totalTax = $("#txtTotalTaxHo").val(); // 税额合计
    var totalfee = document.getElementById('txtTotalFeeHo').value.Trim(); // 含税金额合计
    var discount = document.getElementById('txtDiscount').value.Trim(); // 整单折扣(%）合计
    var discounttotal = document.getElementById('txtDiscountTotal').value.Trim(); // 折扣金额合计
    var realtotal = document.getElementById('txtRealTotal').value.Trim(); // 折后含税金额合计
    var otherTotal = document.getElementById('txtOtherTotal').value.Trim(); // 其他费用支出合计

    // 备注信息
    var creator = $("#txtCreator").val(); // 制单人
    var createDate = $("#txtCreateDate").val(); // 制单时间
    var fflag2 = "";

    if ($("#ddlBillStatus").val().Trim() == "2") {
        $("#ddlBillStatus").val("1");
        fflag2 = 1;
    }
    else {
        fflag2 = 2;
    }
    var billStatus = $("#ddlBillStatus").val(); // 单据状态
    var confirmor = $("#txtConfirmor").val(); // 确认人
    var confirmDate = $("#txtConfirmDate").val(); // 确认时间
    var closer = $("#txtCloser").val(); // 结单人
    var closeDate = $("#txtCloseDate").val(); // 结单日期
    var modifiedUserID = $("#txtModifiedUserID").val(); // 最后更新人
    var modifiedDate = $("#txtModifiedDate").val(); // 最后更新日期
    var attachment = $("#hfPageAttachment").val().replace(/\\/g, "\\\\"); // 附件

    var remark = $("#txtremark").val(); // 备注
    if ($("#txtAction").val().Trim() == "1") {
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
    var DetailRequireDate = new Array();
    var DetailUnitPrice = new Array();
    var DetailTaxPrice = new Array();
    var DetailDiscount = new Array();
    var DetailTaxRate = new Array();
    var DetailTotalPrice = new Array();
    var DetailTotalFee = new Array();
    var DetailTotalTax = new Array();
    var DetailRemark = new Array();
    var DetailFromBillID = new Array();
    var DetailFromBillNo = new Array();
    var DetailFromLineNo = new Array();
    var DetailUsedUnitID = new Array();
    var DetailUsedUnitCount = new Array();
    var DetailUsedPrice = new Array();



    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
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
            var objRequireDate = 'txtRequireDate' + (i + 1);
            var objUnitPrice = 'txtUnitPrice' + (i + 1);
            var objTaxPrice = 'txtTaxPrice' + (i + 1);
            var objDiscount = 'txtDiscount' + (i + 1);
            var objTaxRate = 'txtTaxRate' + (i + 1);
            var objTotalPrice = 'txtTotalPrice' + (i + 1);
            var objTotalFee = 'txtTotalFee' + (i + 1);
            var objTotalTax = 'txtTotalTax' + (i + 1);
            var objRemark = 'txtRemark' + (i + 1);
            var objFromBillID = 'txtFromBillID' + (i + 1);
            var objFromBillNo = 'txtFromBillNo' + (i + 1);
            var objFromLineNo = 'txtFromLineNo' + (i + 1);

            Detailchk.push(document.getElementById(objchk).value.Trim());
            DetailProductID.push(document.getElementById(objProductID.toString()).value.Trim());
            DetailProductNo.push(document.getElementById(objProductNo.toString()).value.Trim());
            DetailProductName.push(document.getElementById(objProductName.toString()).value.Trim());
            if (isMoreUnit) {// 启用多计量单位
                DetailUnitID.push($("#UsedUnitID" + (i + 1)).val());
                DetailProductCount.push($("#UsedUnitCount" + (i + 1)).val());
                DetailUsedUnitID.push($("#" + objUnitID).val());
                DetailUsedUnitCount.push($("#" + objProductCount).val());
                DetailUsedPrice.push(document.getElementById(objUnitPrice.toString()).value.Trim());
                DetailUnitPrice.push($("#hiddUnitPrice" + (i + 1)).val());

            }
            else {
                DetailUnitID.push(document.getElementById(objUnitID.toString()).value.Trim());
                DetailProductCount.push(document.getElementById(objProductCount.toString()).value.Trim());
                DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value.Trim());
            }

            DetailRequireDate.push(document.getElementById(objRequireDate.toString()).value.Trim());
            DetailTaxPrice.push(document.getElementById(objTaxPrice.toString()).value.Trim());
            DetailTaxRate.push(document.getElementById(objTaxRate.toString()).value.Trim());
            DetailTotalPrice.push(document.getElementById(objTotalPrice.toString()).value.Trim());
            DetailTotalFee.push(document.getElementById(objTotalFee.toString()).value.Trim());
            DetailTotalTax.push(document.getElementById(objTotalTax.toString()).value.Trim());
            DetailRemark.push(document.getElementById(objRemark.toString()).value.Trim());
            DetailFromBillID.push(document.getElementById(objFromBillID.toString()).value.Trim());
            DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value.Trim());
            DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value.Trim());
            length++;

        }
    }
    if (arriveNo.length <= 0) {
        isFlag = false;
        fieldText = fieldText + "到货通知单编号|";
        msgText = msgText + "到货通知单编号不允许为空|";
    }


    var no = $("#divPurchaseArriveNo").html();
    var txtIndentityID = $("#txtIndentityID").val();
    var CanUserID = escape($("#txtCanUserID").val().Trim());

    var CanUserName = escape($("#txtCanUserName").val().Trim());


    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseArriveAdd.ashx",
        dataType: 'json', // 返回json格式数据
        data: "arriveNo=" + escape(arriveNo) + "&CanUserID=" + CanUserID + "&CanUserName=" + CanUserName + "&title=" + escape(title)
                        + "&fromType=" + escape(fromType)
                        + "&providerID=" + escape(providerID)
                        + "&typeID=" + escape(typeID)
                        + "&deptid=" + escape(deptid)
                        + "&purchase=" + escape(purchase)
                        + "&takeType=" + escape(takeType)
                        + "&carryType=" + escape(carryType)
                        + "&payeType=" + escape(payeType)
                        + "&rate=" + escape(rate)
                        + "&currencyType=" + escape(currencyType)
                        + "&isAddtax=" + escape(isAddtax)
                        + "&projectID=" + escape(txtProjectID)
                        + "&checkUserID=" + escape(checkUserID)
                        + "&checkDate=" + escape(checkDate)
                        + "&moneyType=" + escape(moneyType)
                        + "&sendAddress=" + escape(sendAddress)
                        + "&receiveOverAddress=" + escape(receiveOverAddress)
                        + "&countTotal=" + escape(countTotal)
                        + "&totalMoney=" + escape(totalMoney)
                        + "&totalTax=" + escape(totalTax)
                        + "&totalfee=" + escape(totalfee)
                        + "&discount=" + escape(discount)
                        + "&discounttotal=" + escape(discounttotal)
                        + "&realtotal=" + escape(realtotal)
                        + "&otherTotal=" + escape(otherTotal)
                        + "&creator=" + escape(creator)
                        + "&createDate=" + escape(createDate)
                        + "&billStatus=" + escape(billStatus)
                        + "&arriveDate=" + (arriveDate)
                        + "&confirmDate=" + escape(confirmDate)
                        + "&closer=" + escape(closer)
                        + "&closeDate=" + escape(closeDate)
                        + "&modifiedUserID=" + escape(modifiedUserID)
                        + "&attachment=" + escape(attachment)
                        + "&remark=" + escape(remark)
                        + "&DetailProductID=" + escape(DetailProductID)
                        + "&DetailProductNo=" + escape(DetailProductNo)
                        + "&DetailProductName=" + escape(DetailProductName)
                        + "&DetailUnitID=" + escape(DetailUnitID)
                        + "&DetailProductCount=" + escape(DetailProductCount)
                        + "&DetailRequireDate=" + escape(DetailRequireDate)
                        + "&DetailUnitPrice=" + escape(DetailUnitPrice)
                        + "&DetailUsedPrice=" + escape(DetailUsedPrice)
                        + "&DetailTaxPrice=" + escape(DetailTaxPrice)
                        + "&DetailDiscount=" + escape(DetailDiscount)
                        + "&DetailTaxRate=" + escape(DetailTaxRate)
                        + "&DetailTotalPrice=" + escape(DetailTotalPrice)
                        + "&DetailTotalFee=" + escape(DetailTotalFee)
                        + "&DetailTotalTax=" + escape(DetailTotalTax)
                        + "&DetailRemark=" + escape(DetailRemark)
                        + "&DetailFromBillID=" + escape(DetailFromBillID)
                        + "&DetailFromBillNo=" + escape(DetailFromBillNo)
                        + "&DetailFromLineNo=" + escape(DetailFromLineNo)
                        + "&DetailUsedUnitID=" + escape(DetailUsedUnitID)
                        + "&DetailUsedUnitCount=" + escape(DetailUsedUnitCount)
                        + "&action=" + escape(action)
                        + "&CodeType=" + escape(CodeType)
                        + "&length=" + escape(length)
                        + "&cno=" + escape(no)
                        + "&fflag2=" + escape(fflag2)
                        + "&ID=" + escape(txtIndentityID)
                        + GetExtAttrValue() + "", // 数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
        },
        success: function(data) {

            if (parseInt(data.sta) > 0) {
                $("#CodingRuleControl1_txtCode").val(data.data);
                $("#CodingRuleControl1_txtCode").attr("readonly", "readonly");
                $("#CodingRuleControl1_ddlCodeRule").attr("disabled", "false");

                if (action == "Add") {
                    popMsgObj.ShowMsg(data.info);
                    document.getElementById('txtIndentityID').value = data.sta;
                    _id = data.sta;
                    $("#txtAction").val("2");
                    if (CodeType != "") {
                        isnew = "edit";
                        $("#divPurchaseArriveNo").html(data.data);
                        $("#divPurchaseArriveNo").css("display", "block");
                        $("#divInputNo").css("display", "none");

                        // 设置源单类型不可改
                        $("#ddlFromType").attr("disabled", "disabled");
                        fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                        fnFlowStatus($("#FlowStatus").val());
                        GetFlowButton_DisplayControl(); // 审批处理
                    }
                    else {
                        $("#divPurchaseArriveNo").html(data.data);
                        $("#divPurchaseArriveNo").css("display", "block");
                        $("#divInputNo").css("display", "none");

                        // 设置源单类型不可改
                        $("#ddlFromType").attr("disabled", "disabled");
                        fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                        fnFlowStatus($("#FlowStatus").val());
                        GetFlowButton_DisplayControl(); // 审批处理

                    }

                }
                else {
                    popMsgObj.ShowMsg(data.info);
                    $("#txtModifiedUserID").val($("#txtModifiedUserID2").val().Trim());
                    $("#txtModifiedUserIDReal").val($("#txtModifiedUserID2").val().Trim());
                    $("#txtModifiedDate").val($("#txtModifiedDate2").val().Trim());
                    fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                    fnFlowStatus($("#FlowStatus").val());
                    GetFlowButton_DisplayControl(); // 审批处理
                }

            }
            else {
                hidePopup();
                popMsgObj.ShowMsg(data.info);
            }
        }
    });



}

function Fun_Clear_Input() {
    action = "Add";
    window.location = 'PurchaseContract_Add.aspx';
}

// 全选
function SelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
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

function ShowProdInfo() {
    popTechObj.ShowListCheckSpecial('SubStorageIn"+rowID+"', 'Check');
}


// 多选明细方法
function GetValue() {

    var ck = document.getElementsByName("CheckboxProdID");
    var strarr = '';
    var str = "";
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            strarr += ck[i].value + ',';
        }
    }
    str = strarr.substring(0, strarr.length - 1);
    if (str == "") {
        popMsgObj.ShowMsg('请先选择数据！');
        return;
    }
    $.ajax({
        type: "POST", // 用POST方式传输
        dataType: "json", // 数据格式:JSON
        url: '../../../Handler/Office/SupplyChain/ProductCheck.ashx?str=' + str, // 目标地址
        cache: false,
        data: '', // 数据
        beforeSend: function() { }, // 发送数据之前

        success: function(msg) {
            // 数据获取完毕，填充页面据显示
            // 数据列表
            $.each(msg.data, function(i, item) {
                // 填充物品ID，物品编号，物品名称,单位ID，单位名称，规格，
                if (!IsExistCheck(item.ProdNo)) {

                    var Index = AddSignRow();
                    $("#txtProductID" + Index).val(item.ID);
                    $("#txtProductNo" + Index).val(item.ProdNo);
                    $("#txtProductName" + Index).val(item.ProductName);
                    $("#txtstandard" + Index).val(item.Specification);
                    $("#txtColorName" + Index).val(item.ColorName);
                    $("#txtUnitPrice" + Index).val(FormatAfterDotNumber(item.TaxBuy, selPoint)); // 去税进价
                    $("#txtTaxPrice" + Index).val(FormatAfterDotNumber(item.StandardBuy, selPoint)); // 含税进价
                    // txtTotalFee
                    $("#txtTaxRate" + Index).val(FormatAfterDotNumber(item.InTaxRate, selPoint)); // 进项税率
                    $("#hiddUnitPrice" + Index).val(FormatAfterDotNumber(item.TaxBuy, selPoint));
                    $("#hiddTaxPrice" + Index).val(FormatAfterDotNumber(item.StandardBuy, selPoint));
                    $("#hiddTaxRate" + Index).val(FormatAfterDotNumber(item.InTaxRate, selPoint));
                    $("#hidExistProduct_" + Index).val('1'); // 判断产品存在
                    if (isMoreUnit) {// 启用多计量单位
                        $("#UsedUnitID" + Index).val(item.UnitID);
                        $("#UsedUnitName" + Index).val(item.CodeName);
                        GetUnitGroupSelectEx(item.ID, 'InUnit', 'txtUnitID' + Index, 'ChangeUnit(this,' + Index + ',1)', 'tdUnitName' + Index, '', 'ChangeUnit(this,' + Index + ',1)');
                        $("#txtProductCount" + Index).blur(function() { ChangeUnit(this, Index); });
                    }
                    else {
                        $("#txtUnitID" + Index).val(item.UnitID);
                        $("#txtUnitName" + Index).val(item.CodeName);
                    }
                }
            });

        },

        complete: function() { } // 接收数据完毕
    });
    closeProductdiv();
}

function IsExistCheck(prodNo) {
    var sign = false;
    var signFrame = findObj("dg_Log", document);
    var DetailLength = 0; // 明细长度
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (i = 1; i < signFrame.rows.length; i++) {
            var prodNo1 = document.getElementById("txtProductNo" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
                sign = true;
                break;
            }
        }
    }

    return sign;
}


// /添加行
function AddSignRow() {
    // 读取最后一行的行号，存放在txtTRLastIndex文本框中

    var txtTRLastIndex = findObj("txtTRLastIndex", document);

    var rowID = parseInt(txtTRLastIndex.value);

    var signFrame = findObj("dg_Log", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); // 添加行
    newTR.id = "ID" + rowID;
    var i = 0;

    var SetFunNo = "SetEventFocus('txtProductNo','txtProductCount'," + rowID + ",false);";

    var newNameXH = newTR.insertCell(i++); // 添加列:选择
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input name='chk' id='chk" + rowID + "' size='10' value=" + rowID + " type='checkbox'    onclick=\"Isquanxuan();\"  class='tdinput' style='width:90%' />";

    var newNameTD = newTR.insertCell(i++); // 添加列:序号
    newNameTD.className = "cell";
    newNameTD.id = "newNameTD" + rowID;
    newNameTD.innerHTML = GenerateNo(rowID);

    var newProductNo = newTR.insertCell(i++); // 添加列:物品ID
    newProductNo.style.display = "none";
    newProductNo.innerHTML = "<input name='txtProductID" + rowID + "'  id='txtProductID" + rowID + "' type='text' style='width:100%' class='tdinput' />"; // 添加列内容

    var newProductNo = newTR.insertCell(i++); // 添加列:物品编号
    newProductNo.className = "cell";
    newProductNo.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'>"
                            + "<tr>"
                            + "<td><input id='txtProductNo" + rowID + "' onblur='SetMatchProduct(" + rowID + ",this.value);' onkeydown=" + SetFunNo + "  class='tdinput' size='7' type='text'></td>"
                            + "<td align='right'><img style='cursor: hand' onclick=OpenSelectProduct('" + rowID + "'); align='absMiddle' src='../../../Images/search.gif' height='21'><input type='hidden' id='hidExistProduct_" + rowID + "' value='0'></td>"
                            + "</tr></table>"; // 添加列内容

    var newProductName = newTR.insertCell(i++); // 添加列:物品名称
    newProductName.className = "cell";
    newProductName.innerHTML = "<input name='txtProductName" + rowID + "' id='txtProductName" + rowID + "' type='text'  style='width:95%;border:0px'  readonly  class='tdinput'  />"; // 添加列内容

    var newProductName = newTR.insertCell(i++); // 添加列:规格(从物品表中带来显示，不往明细表中存)
    newProductName.className = "cell";
    newProductName.innerHTML = "<input name='txtstandard" + rowID + "' id='txtstandard" + rowID + "' type='text'  style='width:95%;border:0px'  readonly  class='tdinput'  />"; // 添加列内容

    var newColorName = newTR.insertCell(i++); // 添加列:颜色(从物品表中带来显示，不往明细表中存)
    newColorName.className = "cell";
    newColorName.innerHTML = "<input name='txtColorName" + rowID + "' id='txtColorName" + rowID + "' type='text'  style='width:95%;border:0px'  readonly  class='tdinput'  />"; // 添加列内容

    if (isMoreUnit) {// 启用多计量单位
        // 基本单位
        var UsedUnitName = newTR.insertCell(i++);
        UsedUnitName.className = "cell";
        UsedUnitName.innerHTML = "<input name='UsedUnitName" + rowID + "' id='UsedUnitName" + rowID + "' type='text' style='width:95%;border:0px' readonly  class='tdinput'  /><input id='UsedUnitID" + rowID + "' type='hidden'  >";

        // 基本数量
        var UsedUnitCount = newTR.insertCell(i++);
        UsedUnitCount.className = "cell";
        UsedUnitCount.innerHTML = "<input name='UsedUnitCount" + rowID + "' id='UsedUnitCount" + rowID + "' type='text' style='width:95%;border:0px' onchange='Number_round(this," + selPoint + ");' readOnly  class='tdinput'  />";

        // 单位
        var tdUnitName = newTR.insertCell(i++);
        tdUnitName.className = "cell";
        tdUnitName.id = "tdUnitName" + rowID;

    }
    else {
        var newUnitID = newTR.insertCell(i++); // 添加列:单位ID
        newUnitID.style.display = "none";
        newUnitID.innerHTML = "<input name='txtUnitID" + rowID + "' id='txtUnitID" + rowID + "'style='width:10%;border:0px' type='text'  class='tdinput' readonly />"; // 添加列内容

        var newUnitID = newTR.insertCell(i++); // 添加列:单位
        newUnitID.className = "cell";
        newUnitID.innerHTML = "<input name='txtUnitName" + rowID + "' id='txtUnitName" + rowID + "' type='text' style='width:95%;border:0px' readonly  class='tdinput' />"; // 添加列内容
    }

    var newProductCount = newTR.insertCell(i++); // 添加列:采购数量(从采购定单表中带来显示，不往明细表中存)
    newProductCount.className = "cell";
    newProductCount.innerHTML = "<input name='txtProductOrder" + rowID + "'  id='txtProductOrder" + rowID + "'style='width:95%;border:0px'  readonly   type='text' class='tdinput' />"; // 添加列内容

    var newArrivedCount = newTR.insertCell(i++); // 添加列:已到货数量(从采购定单表中带来显示，不往明细表中存)
    newArrivedCount.style.display = "none";
    newArrivedCount.innerHTML = "<input name='txtArrivedCount" + rowID + "'  id='txtArrivedCount" + rowID + "'style='width:95%;border:0px'  readonly   type='text' class='tdinput' />"; // 添加列内容

    var newProductCount = newTR.insertCell(i++); // 添加列:到货数量
    newProductCount.className = "cell";
    newProductCount.innerHTML = "<input name='txtProductCount" + rowID + "'  id='txtProductCount" + rowID + "'  onchange='Number_round(this," + selPoint + ");' onblur=ChangeUnit(this," + rowID + "); onkeydown=SetEventFocus('txtProductCount','txtProductNo'," + rowID + ",true);  style='width:95%;border:0px'  type='text' class='tdinput'/>"; // 添加列内容

    var newRequireDate = newTR.insertCell(i++); // 添加列:交货日期
    newRequireDate.style.display = "none";
    newRequireDate.className = "cell";
    newRequireDate.innerHTML = "<input name='txtRequireDate" + rowID + "' id='txtRequireDate" + rowID + "' type='text' style='width:95%;border:0px' readonly class='tdinput' />"; // 添加列内容
    $("#txtRequireDate" + rowID).click(function() { WdatePicker({ dateFmt: 'yyyy-MM-dd', el: $dp.$("#txtRequireDate" + rowID) }) });

    if ($("#drpFromType").val() == "0") {
        var newUnitPrice = newTR.insertCell(i++); // 添加列:单价
        newUnitPrice.className = "cell";
        newUnitPrice.style.display = IsDisplayPrice;
        newUnitPrice.innerHTML = "<input name='txtUnitPrice" + rowID + "' id='txtUnitPrice" + rowID + "' type='text'  onchange='Number_round(this," + selPoint + ");'   onblur=\"ChangeUnit(this," + rowID + ");\"   style='width:95%;border:0px' class='tdinput'/><input type=\"hidden\"  id='hiddUnitPrice" + rowID + "'/>";

        var newUnitPrice = newTR.insertCell(i++); // 添加列:含税价
        newUnitPrice.className = "cell";
        newUnitPrice.style.display = IsDisplayPrice;
        newUnitPrice.innerHTML = "<input name='txtTaxPrice" + rowID + "' id='txtTaxPrice" + rowID + "' type='text'   onchange='Number_round(this," + selPoint + ");'   onblur=\"ChangeUnit(this," + rowID + ",0,1);\"  style='width:95%;border:0px' class='tdinput'/> <input type=\"hidden\"  id='hiddTaxPrice" + rowID + "'/>"; // 添加列内容
    }
    else {
        var newUnitPrice = newTR.insertCell(i++); // 添加列:单价
        newUnitPrice.className = "cell";
        newUnitPrice.style.display = IsDisplayPrice;
        newUnitPrice.innerHTML = "<input name='txtUnitPrice" + rowID + "' id='txtUnitPrice" + rowID + "' type='text'   style='width:95%;border:0px'  readonly  class='tdinput'/><input type=\"hidden\"  id='hiddUnitPrice" + rowID + "'/>";

        var newUnitPrice = newTR.insertCell(i++); // 添加列:含税价
        newUnitPrice.className = "cell";
        newUnitPrice.style.display = IsDisplayPrice;
        newUnitPrice.innerHTML = "<input name='txtTaxPrice" + rowID + "' id='txtTaxPrice" + rowID + "' type='text'   style='width:95%;border:0px'    readonly  class='tdinput'/> <input type=\"hidden\"  id='hiddTaxPrice" + rowID + "'/>"; // 添加列内容
    }


    var newTaxPricHide = newTR.insertCell(i++); // 添加列:含税价隐藏
    newTaxPricHide.style.display = "none";
    newTaxPricHide.innerHTML = "<input id='OrderTaxPriceHide" + rowID + "' type='text'  style='width:98%'   />";

    var newUnitPrice = newTR.insertCell(i++); // 添加列:税率
    newUnitPrice.className = "cell";
    newUnitPrice.style.display = IsDisplayPrice;
    newUnitPrice.innerHTML = "<input name='txtTaxRate" + rowID + "' id='txtTaxRate" + rowID + "' type='text'   style='width:95%;border:0px'    readonly  class='tdinput'/>  <input type=\"hidden\"  id='hiddTaxRate" + rowID + "'/>"; // 添加列内容

    var newTotalPrice = newTR.insertCell(i++); // 添加列:金额
    newTotalPrice.className = "cell";
    newTotalPrice.style.display = IsDisplayPrice;
    newTotalPrice.innerHTML = "<input name='txtTotalPrice" + rowID + "' id='txtTotalPrice" + rowID + "' type='text'  style='width:95%;border:0px'   readonly  class='tdinput'/>"; // 添加列内容
    $("#txtTotalPrice" + rowID).onfocus = "TotalPrice1();"

    var newUnitPrice = newTR.insertCell(i++); // 添加列:含税金额
    newUnitPrice.className = "cell";
    newUnitPrice.style.display = IsDisplayPrice;
    newUnitPrice.innerHTML = "<input name='txtTotalFee" + rowID + "' id='txtTotalFee" + rowID + "' type='text'   style='width:95%;border:0px'    readonly  class='tdinput'/>"; // 添加列内容

    var newUnitPrice = newTR.insertCell(i++); // 添加列:税额
    newUnitPrice.className = "cell";
    newUnitPrice.style.display = IsDisplayPrice;
    newUnitPrice.innerHTML = "<input name='txtTotalTax" + rowID + "' id='txtTotalTax" + rowID + "' type='text'   style='width:95%;border:0px'    readonly  class='tdinput'/>"; // 添加列内容

    var newRemark = newTR.insertCell(i++); // 添加列:备注
    newRemark.className = "cell";
    newRemark.innerHTML = "<input name='txtRemark" + rowID + "' id='txtRemark" + rowID + "' type='text' style='width:95%;border:0px'  class='tdinput'/>"; // 添加列内容

    var newFromBillID = newTR.insertCell(i++); // 添加列:源单ID
    newFromBillID.style.display = "none";
    newFromBillID.innerHTML = "<input name='txtFromBillID" + rowID + "' id='txtFromBillID" + rowID + "' type='text' style='width:95%;border:0px'   readonly class='tdinput'/>"; // 添加列内容

    var newFromBillID = newTR.insertCell(i++); // 添加列:源单编号
    newFromBillID.className = "cell";
    newFromBillID.innerHTML = "<input name='txtFromBillNo" + rowID + "' id='txtFromBillNo" + rowID + "' type='text'  style='width:95%;border:0px'  readonly   class='tdinput'/>"; // 添加列内容

    var newFromLineNo = newTR.insertCell(i++); // 添加列:源单序号
    newFromLineNo.className = "cell";
    newFromLineNo.innerHTML = "<input name='txtFromLineNo" + rowID + "' id='txtFromLineNo" + rowID + "' type='text' style='width:95%;border:0px'  readonly   class='tdinput'/>"; // 添加列内容

    var newFromLineNo = newTR.insertCell(i++); // 添加列:已报质检数量
    newFromLineNo.className = "cell";
    newFromLineNo.innerHTML = "<input name='txtApplyCheckCount" + rowID + "' id='txtApplyCheckCount" + rowID + "' type='text' style='width:95%;border:0px'  readonly   class='tdinput'/>"; // 添加列内容

    var newFromLineNo = newTR.insertCell(i++); // 添加列:实检数量
    newFromLineNo.className = "cell";
    newFromLineNo.innerHTML = "<input name='txtCheckedCount" + rowID + "' id='txtCheckedCount" + rowID + "' type='text' style='width:95%;border:0px'  readonly   class='tdinput'/>"; // 添加列内容

    var newFromLineNo = newTR.insertCell(i++); // 添加列:合格数量
    newFromLineNo.className = "cell";
    newFromLineNo.innerHTML = "<input name='txtPassCount" + rowID + "' id='txtPassCount" + rowID + "' type='text' style='width:95%;border:0px'  readonly   class='tdinput'/>"; // 添加列内容

    var newFromLineNo = newTR.insertCell(i++); // 添加列:不合格数量
    newFromLineNo.className = "cell";
    newFromLineNo.innerHTML = "<input name='txtNotPassCount" + rowID + "' id='txtNotPassCount" + rowID + "' type='text' style='width:95%;border:0px'  readonly   class='tdinput'/>"; // 添加列内容

    var newFromLineNo = newTR.insertCell(i++); // 添加列:已入库数量
    newFromLineNo.className = "cell";
    newFromLineNo.innerHTML = "<input name='txtInCount" + rowID + "' id='txtInCount" + rowID + "' type='text' style='width:95%;border:0px'  readonly   class='tdinput'/>"; // 添加列内容

    var newFromLineNo = newTR.insertCell(i++); // 添加列:拒收数量
    newFromLineNo.style.display = "none";
    newFromLineNo.className = "cell";
    newFromLineNo.innerHTML = "<input name='txtRejectCount" + rowID + "' id='txtRejectCount" + rowID + "' type='text' style='width:95%;border:0px'  readonly   class='tdinput'/>"; // 添加列内容

    var newFromLineNo = newTR.insertCell(i++); // 添加列:退货数量
    newFromLineNo.className = "cell";
    newFromLineNo.innerHTML = "<input name='txtBackCount" + rowID + "' id='txtBackCount" + rowID + "' type='text' style='width:95%;border:0px'  readonly  class='tdinput' />"; // 添加列内容

    txtTRLastIndex.value = (rowID + 1).toString(); // 将行号推进下一行
    return rowID;
}

// 打开物品选择界面
function OpenSelectProduct(iRow) {
    if ($("#hidExistProduct_" + iRow).val() == '1'
        || $("#txtProductNo" + iRow).val() == '') {
        popTechObj.ShowList(iRow);
    }
}

/*焦点2：鼠标失去焦点时，匹配数据库物品信息*/
function SetMatchProduct(rows, values) {
    var ProdNo = values;
    if (values != "") {
        popTechObj.InputObj = rows;
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../../../Handler/Office/SupplyChain/BatchProductInfo.ashx?ProdNo=" + ProdNo, //目标地址
            cache: false,
            success: function(msg) {
                var rowsCount = 0;
                if (typeof (msg.dataBatch) != 'undefined') {
                    $.each(msg.dataBatch, function(i, item) {
                        rowsCount++;
                        $("#txtProductID" + rows).val(item.ID);
                        $("#txtProductNo" + rows).val(item.ProdNo);
                        $("#txtProductName" + rows).val(item.ProductName);
                        $("#txtstandard" + rows).val(item.Specification);
                        $("#txtColorName" + rows).val(item.ColorName);
                        $("#txtUnitPrice" + rows).val(FormatAfterDotNumber(item.TaxBuy, selPoint)); // 去税进价
                        $("#txtTaxPrice" + rows).val(FormatAfterDotNumber(item.StandardBuy, selPoint)); // 含税进价
                        // txtTotalFee
                        $("#txtTaxRate" + rows).val(FormatAfterDotNumber(item.InTaxRate, selPoint)); // 进项税率
                        $("#hiddUnitPrice" + rows).val(FormatAfterDotNumber(item.TaxBuy, selPoint));
                        $("#hiddTaxPrice" + rows).val(FormatAfterDotNumber(item.StandardBuy, selPoint));
                        $("#hiddTaxRate" + rows).val(FormatAfterDotNumber(item.InTaxRate, selPoint));
                        if (isMoreUnit) {// 启用多计量单位
                            $("#UsedUnitID" + rows).val(item.UnitID);
                            $("#UsedUnitName" + rows).val(item.CodeName);
                            GetUnitGroupSelectEx(item.ID, 'InUnit', 'txtUnitID' + rows, 'ChangeUnit(this,' + rows + ',1)', 'tdUnitName' + rows, '', 'ChangeUnit(this,' + rows + ',1)');
                            $("#txtProductCount" + rows).blur(function() { ChangeUnit(this, rows); });
                        }
                        else {
                            $("#txtUnitID" + rows).val(item.UnitID);
                            $("#txtUnitName" + rows).val(item.CodeName);
                        }
                        $("#hidExistProduct_" + rows).val('1');
                    });
                }
                if (rowsCount == 0) {
                    popMsgObj.Show("物品编号" + rows + "|", "不存在此" + values + "物品，请重新输入或选择物品");
                    ClearOneProductInfo(rows);
                }

            },
            error: function() {
                popMsgObj.ShowMsg('匹配物品数据时发生请求异常!');
                ClearOneProductInfo(rows);
            },
            complete: function() { }
        });
    }
    else {
        if (document.getElementById('txtProductID' + rows).value != "") {
            popMsgObj.Show("物品编号" + rows + "|", "请重新输入或选择物品");
            ClearOneProductInfo(rows);
        }
    }
}


/*手工输入物品编号不存在时，清除行信息(之前输入一个存在的物品，之后再输入一个不存在的物品)*/
function ClearOneProductInfo(rows) {
    $("#txtProductID" + rows).val("");
    $("#txtProductNo" + rows).val("");
    $("#txtProductName" + rows).val("");
    $("#txtstandard" + rows).val("");
    $("#txtColorName" + rows).val("");
    $("#txtUnitPrice" + rows).val(""); // 去税进价
    $("#txtTaxPrice" + rows).val(""); // 含税进价
    $("#hidExistProduct_" + rows).val('0');
    // txtTotalFee
    $("#txtTaxRate" + rows).val(""); // 进项税率
    $("#hiddUnitPrice" + rows).val("");
    $("#hiddTaxPrice" + rows).val("");
    $("#hiddTaxRate" + rows).val("");
    $("#txtProductCount" + rows).val("");
    if (isMoreUnit) {// 启用多计量单位
        $("#UsedUnitID" + rows).val("");
        $("#UsedUnitName" + rows).val("");
        $("#UsedUnitCount" + rows).val("");
        $("#txtUnitID" + rows).find("option").remove();
    }
    else {
        $("#txtUnitID" + rows).val("");
        $("#txtUnitName" + rows).val("");
    }
}


function GenerateNo(Edge) {// 生成序号
    DtlSCnt = findObj("txtTRLastIndex", document); // 明细来源新增行号
    var signFrame = findObj("dg_Log", document);
    var SortNo = 1; // 起始行号
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (var i = 1; i < Edge; ++i) {
            if (signFrame.rows[i].style.display != "none") {
                document.getElementById("newNameTD" + i).value = SortNo++;
            }
        }
        return SortNo;
    }
    return 0; // 明细表不存在时返回0
}


function DeleteSignRowArrive() {// 删除明细行，需要将序号重新生成
    var signFrame = findObj("dg_Log", document);
    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        var rowID = i + 1;
        if (ck[i].checked) {
            signFrame.rows[rowID].style.display = "none";
        }
        document.getElementById("newNameTD" + rowID).innerHTML = GenerateNo(rowID);
    }
    var Flag = $("#drpFromType").val().Trim();

    // 判断是否有明细了，若没有了，则将供应商设为可选
    var totalSort = 0; // 明细行数
    for (var i = 0; i < ck.length; i++) {
        var rowID = i + 1;
        if (signFrame.rows[rowID].style.display != "none") {
            totalSort++;
            break;
        }
    }
    if (totalSort != 0) {// 明细行数大于0
    }
    else {// 无明细，供应商可选
        // $("#txtProviderName").removeAttr("disabled");
        $("#txtProviderID").css("display", "block");
        $("#txtHiddenProviderID1").css("display", "none");

        document.getElementById("checkall").checked = false;
        document.getElementById('txtProviderID').disabled = false;
        document.getElementById('drpCurrencyType').disabled = false;
    }

    fnTotalInfo();
}

// 计算各种合计信息
function fnTotalInfo() {
    var CountTotal = 0; // 数量合计
    var TotalPrice = 0; // 金额合计
    var Tax = 0; // 税额合计
    var TotalFee = 0; // 含税金额合计
    var Discount = $("#txtDiscount").val(); // 整单折扣
    var DiscountTotal = 0; // 折扣金额
    var RealTotal = 0; // 折后含税金额
    var Zhekoumingxi = 0; // 明细中折扣合计

    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = i;
            var pProductCount = $("#txtProductOrder" + rowid).val(); // 采购数量
            var pYBackCount = $("#txtArrivedCount" + rowid).val(); // 已到货数量
            var pCountDetail = $("#txtProductCount" + rowid).val(); // 到货数量
            if (pCountDetail == "") {
                pCountDetail = FormatAfterDotNumber(0, selPoint);
            }
            if (IsNumberOrNumeric(pCountDetail, 9, selPoint) == false) {
                alert("【到货数量】格式不正确！");
                return;
            }
            document.getElementById("txtProductCount" + rowid).value = FormatAfterDotNumber(pCountDetail, selPoint);
            if (pProductCount != "" && !isOverOrder) {
                if ((parseFloat(pProductCount) - parseFloat(pYBackCount)) < parseFloat(pCountDetail)) {
                    var xxxxx = FormatAfterDotNumber((parseFloat(pProductCount) - parseFloat(pYBackCount)), selPoint);
                    alert("第" + i + "行的【到货数量】不能大于当前可用到货数量(" + xxxxx + ")！");
                    return;
                }
            }
            var UnitPriceDetail = $("#txtUnitPrice" + rowid).val(); // 单价
            // 判断是否是增值税，不是增值税含税价始终等于单价
            if (!document.getElementById('chkisAddTax').checked) {
                $("#txtTaxPrice" + rowid).val($("#txtUnitPrice" + rowid).val());
            }
            var TaxPriceDetail = $("#txtTaxPrice" + rowid).val(); // 含税价
            var TaxRateDetail = $("#txtTaxRate" + rowid).val(); // 税率
            var TotalPriceDetail = FormatAfterDotNumber((UnitPriceDetail * pCountDetail), selPoint); // 金额=数量*单价*折扣
            var TotalTaxDetail = FormatAfterDotNumber((TotalPriceDetail * TaxRateDetail / 100), selPoint); // 税额=金额
            // *税率
            var TotalFeeDetail = FormatAfterDotNumber((TaxPriceDetail * pCountDetail), selPoint); // 含税金额=数量*含税单价*折扣
            $("#txtTotalPrice" + rowid).val(FormatAfterDotNumber(TotalPriceDetail, selPoint)); // 金额
            $("#txtTotalTax" + rowid).val(FormatAfterDotNumber(TotalTaxDetail, selPoint)); // 税额
            $("#txtTotalFee" + rowid).val(FormatAfterDotNumber(TotalFeeDetail, selPoint)); // 含税金额
            TotalPrice += parseFloat(TotalPriceDetail); // 金额
            Tax += parseFloat(TotalTaxDetail); // 税额
            TotalFee += parseFloat(TotalFeeDetail); // 含税金额
            CountTotal += parseFloat(pCountDetail); // 数量合计
            Zhekoumingxi += 0; // 明细折扣金额=含税价*数量*（1-折扣）
        }
    }
    $("#txtTotalMoney").val(FormatAfterDotNumber(TotalPrice, selPoint)); // 金额合计
    $("#txtTotalTaxHo").val(FormatAfterDotNumber(Tax, selPoint)); // 税额合计
    $("#txtTotalFeeHo").val(FormatAfterDotNumber(TotalFee, selPoint)); // 含税金额合计
    $("#txtCountTotal").val(FormatAfterDotNumber(CountTotal, selPoint)); // 数量合计
    $("#txtDiscountTotal").val(FormatAfterDotNumber((TotalFee * (100 - Discount) / 100), selPoint)); // 折扣金额
    $("#txtRealTotal").val(FormatAfterDotNumber((TotalFee * Discount / 100), selPoint)); // 折后含税金额
    $("#txtDiscountTotal").val(FormatAfterDotNumber(((TotalFee * (100 - Discount) / 100) + Zhekoumingxi), selPoint)); // 折扣金额
    $("#txtRealTotal").val(FormatAfterDotNumber((TotalFee * Discount / 100), selPoint)); // 折后含税金额
}

function fnChangeAddTax() {// 改变是否为增值税
    var isAddTax = document.getElementById("chkisAddTax").checked;
    // 新加币种的汇率问题
    var Rate = $("#txtRate").val().Trim();

    if (isAddTax == true) {
        $("#AddTax").html("是增值税");
    }
    else {
        $("#AddTax").html("非增值税");
    }
    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {// 是增值税则
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value.Trim() / Rate, selPoint); // 含税价等于隐藏域含税价
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = document.getElementById("hiddTaxRate" + rowIndex).value.Trim(); // 税率等于隐藏域税率

            }
            else {
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("txtUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 含税价等于单价
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, selPoint); // 税率等于0

            }
        }
    }
    fnTotalInfo();
}



// 获取明细信息
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
                temp.push(document.getElementById("chk" + rowid).value.Trim()); // 0
                temp.push(document.getElementById("txtProductNo" + rowid).value.Trim()); //
                temp.push(document.getElementById("txtProductName" + rowid).value.Trim()); // 2
                temp.push(document.getElementById("txtProductCount" + rowid).value.Trim()); // 3
                temp.push(document.getElementById("txtUnitID" + rowid).value.Trim()); // 4
                temp.push(document.getElementById("txtRequireDate" + rowid).value.Trim()); // 5
                temp.push(document.getElementById("txtUnitPrice" + rowid).value.Trim()); // 6
                temp.push(document.getElementById("txtTotalPrice" + rowid).value.Trim()); // 7
                temp.push(document.getElementById("txtTotalPrice" + rowid).value.Trim()); // 8
                temp.push(document.getElementById("txtApplyReason" + rowid).value.Trim()); // 9需求日期
                temp.push(document.getElementById("txtRemark" + rowid).value.Trim()); // 10
                temp.push(document.getElementById("txtFromBillID" + rowid).value.Trim()); // 11
                temp.push(document.getElementById("txtFromLineNo" + rowid).value.Trim()); // 12
                DtlS_Item[i - 1] = temp;
                arrlength++;
            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "已缓存配件信息，如需保存请提交！");
        }
    }
}



// 附件处理
function DealResume(flag) {
    // flag未设置时，返回不处理
    if (flag == "undefined" || flag == "") {
        return;
    }
    // 上传附件
    else if ("upload" == flag) {
        ShowUploadFile();
    }
    // 清除附件
    else if ("clear" == flag) {
        // 设置附件路径
        $("#hfPageAttachment").val("");
        // 下载删除不显示
        $("#divDealResume").css("display", "none");
        // 上传显示
        $("#divUploadResume").css("display", "block");
    }
    // 下载附件
    else if ("download" == flag) {
        // 获取简历路径
        resumeUrl = $("#hfPageAttachment").val().Trim();
        window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + resumeUrl, "_blank");
    }
}

function AfterUploadFile(url) {
    if (url != "") {
        // alert(url);
        // //设置简历路径
        // $("#hfPageResume").val(url);
        // 下载删除显示
        $("#divDealResume").css("display", "block");
        // 上传不显示
        $("#divUploadResume").css("display", "none");


        // 设置简历路径
        $("#hfPageAttachment").val(url);
    }
}

function ChangeCurreny() {// 选择币种带出汇率
    var IDExchangeRate = $("#drpCurrencyType").val().Trim();
    $("#CurrencyTypeID").val(IDExchangeRate.split('_')[0]);
    $("#txtRate").val(IDExchangeRate.split('_')[1]);

    var isAddTax = document.getElementById("chkisAddTax").checked;
    // 新加币种的汇率问题
    var Rate = $("#txtRate").val().Trim();

    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {// 是增值税则
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value.Trim() / Rate, selPoint); // 含税价等于隐藏域含税价
                // 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = document.getElementById("hiddTaxRate" + rowIndex).value.Trim(); // 税率等于隐藏域税率
                $("#AddTax").html("是增值税");
            }
            else {
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("txtUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 含税价等于单价
                // 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, selPoint); // 税率等于0
                $("#AddTax").html("非增值税");
            }
        }
    }

    fnTotalInfo();
}

function DeleteAll() {
    var Flag = $("#drpFromType").val().Trim();

    if (Flag == 0) {
        DeleteSignRow100();
        fnTotalInfo();
        $("#txtProviderID").css("display", "block");
        $("#txtHiddenProviderID1").css("display", "none");
        document.getElementById('txtProviderID').disabled = false;
        document.getElementById('drpCurrencyType').disabled = false;
    }
    else {
        DeleteSignRow100();
        fnTotalInfo();
        document.getElementById('txtProviderID').disabled = false;
        document.getElementById('drpCurrencyType').disabled = false;
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







// 验证
/*
* 基本信息校验
*/
function CheckBaseInfo() {

    var fieldText = "";
    var msgText = "";
    var isFlag = true;


    // 先检验页面上的特殊字符
    // var RetVal=CheckSpecialWords();
    // if(RetVal!="")
    // {
    // isFlag = false;
    // fieldText = fieldText + RetVal+"|";
    // msgText = msgText +RetVal+ "不能含有特殊字符|";

    // }

    var ssw = CheckSpecialWords();
    if (ssw != "") {
        isFlag = false;
        var tmpKeys = ssw.split(',');
        for (var i = 0; i < tmpKeys.length; i++) {
            isFlag = false;
            fieldText = fieldText + tmpKeys[i].toString() + "|";
            msgText = msgText + "不能含有特殊字符|";
        }
    }


    // 新建时，编号选择手工输入时
    if ($("#txtAction").val().Trim() == "1") {
        // 获取编码规则下拉列表选中项
        codeRule = $("#CodingRuleControl1_ddlCodeRule").val().Trim();
        // 如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "") {
            // 获取输入的编号
            employeeNo = $("#CodingRuleControl1_txtCode").val().Trim();
            // 编号必须输入
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

    // 整单折扣验证是否为数字
    if ($("#txtDiscount").val().Trim() != "") {
        if (IsNumberOrNumeric($("#txtDiscount").val().Trim(), 8, selPoint) == false) {
            isFlag = false;
            fieldText += "整单折扣|";
            msgText += "请输入正确的整单折扣|";
        }
    }
    if ($("#txtTotalMoney").val().Trim() != "") {
        if (IsNumberOrNumeric($("#txtTotalMoney").val().Trim(), 20, selPoint) == false) {
            isFlag = false;
            fieldText += "金额合计|";
            msgText += "请输入正确的金额合计|";
        }
    }
    if ($("#txtTotalTaxHo").val().Trim() != "") {
        if (IsNumberOrNumeric($("#txtTotalTaxHo").val().Trim(), 20, selPoint) == false) {
            isFlag = false;
            fieldText += "税额合计|";
            msgText += "请输入正确的税额合计|";
        }
    }
    if ($("#txtTotalFeeHo").val().Trim() != "") {
        if (IsNumberOrNumeric($("#txtTotalFeeHo").val().Trim(), 20, selPoint) == false) {
            isFlag = false;
            fieldText += "含税金额合计|";
            msgText += "请输入正确的含税金额合计|";
        }
    }
    if ($("#txtDiscountTotal").val().Trim() != "") {
        if (IsNumberOrNumeric($("#txtDiscountTotal").val().Trim(), 20, selPoint) == false) {
            isFlag = false;
            fieldText += "折扣金额合计|";
            msgText += "请输入正确的折扣金额合计|";
        }
    }
    if ($("#txtRealTotal").val().Trim() != "") {
        if (IsNumberOrNumeric($("#txtRealTotal").val().Trim(), 20, selPoint) == false) {
            isFlag = false;
            fieldText += "折后含税金额|";
            msgText += "请输入正确的折后含税金额|";
        }
    }
    if ($("#txtOtherTotal").val().Trim() != "") {
        if (IsNumberOrNumeric($("#txtOtherTotal").val().Trim(), 12, selPoint) == false) {
            isFlag = false;
            fieldText += "其它费用支出合计|";
            msgText += "请输入正确的其它费用支出合计|";
        }
    }

    if ($("#txtRate").val().Trim() != "") {
        if (IsNumberOrNumeric($("#txtRate").val().Trim(), 12, 4) == false) {
            isFlag = false;
            fieldText += "汇率|";
            msgText += "请输入正确的汇率|";
        }
    }
    if ($("#txtTitle").val().Trim() == "") {
        // isFlag = false;
        // fieldText += "单据主题|";
        // msgText += "请输入单据主题|";
    }
    else {
        if (strlen($("#txtTitle").val().Trim()) > 100) {
            isFlag = false;
            fieldText += "单据主题|";
            msgText += "单据主题仅限于100个字符以内|";
        }
    }
    if ($("#txtProviderID").val().Trim() == "") {
        isFlag = false;
        fieldText += "供应商|";
        msgText += "请输入供应商|";
    }
    //    if($("#UserPurchaser").val().Trim() == "")
    //    {
    //        isFlag = false;
    //        fieldText += "采购员|";
    //        msgText += "请输入采购员|";
    //    }
    if ($("#txtArriveDate").val().Trim() == "") {
        isFlag = false;
        fieldText += "到货时间|";
        msgText += "请输入到货时间|";
    }

    // 限制字数
    var SendAddress = $("#txtSendAddress").val().Trim(); // 发货地址
    var ReceiveOverAddress = $("#txtReceiveOverAddress").val().Trim(); // 收货地址
    var Remark = $("#txtremark").val().Trim(); // 备注
    if (strlen(SendAddress) > 200) {
        isFlag = false;
        fieldText += "发货地址|";
        msgText += "发货地址仅限于200个字符以内|";
    }
    if (strlen(ReceiveOverAddress) > 200) {
        isFlag = false;
        fieldText += "收货地址|";
        msgText += "收货地址仅限于200个字符以内|";
    }
    if (strlen(Remark) > 200) {
        isFlag = false;
        fieldText += "备注|";
        msgText += "备注仅限于200个字符以内|";
    }

    // 明细来源的校验txtTRLastIndex
    var signFrame = document.getElementById("dg_Log");
    if ((typeof (signFrame) != "undefined") || signFrame != null) {
        // isFlag = false;
        // fieldText +="明细来源|";
        // msgText +="明细来源不能为空|";
        // }
        // else
        // {
        RealCount = 0;
        for (var i = 1; i < signFrame.rows.length; ++i) {
            if (signFrame.rows[i].style.display != "none") {
                RealCount++;
                var ProductCount = "txtProductCount" + i;
                var no = document.getElementById(ProductCount).value.Trim();
                var remarks = "txtRemark" + i;
                var Remarks = document.getElementById(remarks).value.Trim();
                var ProductNo1 = "txtProductNo" + i;

                var ProductNo2 = document.getElementById(ProductNo1).value.Trim();
                if (IsNumeric(no, 12, selPoint) == false) {
                    isFlag = false;
                    fieldText += "到货数量|";
                    msgText += "请输入正确的物品编号为" + ProductNo2 + "的到货数量|";

                }
                else {

                    if (no > 0)
                    { }
                    else {
                        isFlag = false;
                        fieldText += "到货数量|";
                        msgText += " 物品编号为" + ProductNo2 + "的到货数量需大于零|";


                    }
                }
                var UnitPrice = "txtUnitPrice" + i;
                var no1 = document.getElementById(UnitPrice).value.Trim();
                if (IsNumeric(no1, 12, selPoint) == false) {
                    isFlag = false;
                    fieldText += "单价|";
                    msgText += "请输入正确的物品编号为" + ProductNo2 + "的单价|";

                }
                else {
                    if (no1 > 0)
                    { }
                    else {
                        //                        isFlag = false;
                        //                        fieldText += "单价|";
                        //                        msgText += " 物品编号为" + ProductNo2 + "的单价需大于零|";
                    }
                }
                var TotalPrice = "txtTotalPrice" + i;
                var no2 = document.getElementById(TotalPrice).value.Trim();
                if (IsNumeric(no2, 12, selPoint) == false) {
                    isFlag = false;
                    fieldText += "金额小计|";
                    msgText += "请输入正确的物品编号为" + ProductNo2 + "的金额小计数量|";

                }
                if (strlen(Remarks) > 200) {
                    isFlag = false;
                    fieldText += "备注|";
                    msgText += "物品编号为" + ProductNo2 + "的备注仅限于200个字符以内|";

                }
            }
        }
        if (RealCount == 0) {
            isFlag = false;
            fieldText += "采购到货明细|";
            msgText += "采购到货明细不能为空|";
        }
    }
    else {
        isFlag = false;
        fieldText += "采购到货明细|";
        msgText += "采购到货明细不能为空|";
    }


    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    return isFlag;
}



// 删除行
function DeleteSignRow22() {
    var signFrame = findObj("dg_Log", document);
    var ck = document.getElementsByName("chk");
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
    var signFrame = findObj("dg_Log", document);
    var index = 0;
    for (var i = 0; i < txtTRLastIndex - 1; i++) {
        if (signFrame.rows[i + 1].style.display != "none") {
            var objRadio = 'chk' + (i + 1);
            if (document.getElementById(objRadio.toString()).checked) {
                signFrame.rows[i + 1].style.display = 'none';
                var rowID1 = i + 1;

                // 对表单金额合计计算
                var smalltotal = document.getElementById('txtTotalPrice' + rowID1).value.Trim();
                var test = $("#txtTotalMoney").val().Trim();
                var total = test;
                var count = 0
                if (smalltotal != "") {
                    count = parseInt(total) - parseInt(smalltotal);
                    $("#txtTotalMoney").val(count.toString());
                }

                // 对表单采购数量合计计算
                var productcount = document.getElementById("txtProductCount" + rowID1).value.Trim();
                var test1 = $("#txtCountTotal").val().Trim();
                var total1 = test1;
                var count1 = 0;
                if (productcount != "") {
                    count1 = parseInt(total1) - productcount;
                    $("#txtCountTotal").val(count1.toString());
                }

                // 对表单税额合计计算
                var smalltotaltax = document.getElementById("txtTotalTax" + rowID1).value.Trim();
                var test2 = $("#txtTotalTaxHo").val().Trim();
                var total2 = test2;
                var count2 = 0;
                if (smalltotaltax != "") {
                    count2 = parseInt(total2) - parseInt(smalltotaltax);
                    $("#txtTotalTaxHo").val(count2.toString());
                }

                // 对表单含税金额合计计算
                var smalltotalfee = document.getElementById("txtTotalFee" + rowID1).value.Trim();
                var test3 = $("#txtTotalFeeHo").val().Trim();
                var total3 = test3;
                var count3 = 0;
                if (smalltotalfee != "") {
                    count3 = parseInt(total3) - parseInt(smalltotalfee);
                    $("#txtTotalFeeHo").val(count3.toString());
                    if (count3 == 0) {
                        $("#txtDiscountTotal").val("");
                        $("#txtRealTotal").val("");
                    }
                }
            }
        }
    }
}



function PurchaseOrderSelect() {
    var Flag = $("#drpFromType").val().Trim();

    if (Flag == 0) {// 无来源
        $("#txtProviderID").css("display", "none");
        $("#txtHiddenProviderID1").css("display", "block");
    }
    else if (Flag == 1) {// 采购订单
        var ProviderID = $("#txtHidProviderID").val().Trim();
        var CurrencyTypeID = $("#CurrencyTypeID").val().Trim();
        var Rate = $("#txtRate").val().Trim();
        var signFrame = findObj("dg_Log", document);
        var ck = document.getElementsByName("chk");
        var totalSort = 0; // 明细行数
        for (var i = 0; i < ck.length; i++) {
            var rowID = i + 1;
            if (signFrame.rows[rowID].style.display != "none") {
                totalSort++;
                break;
            }
        }
        if (totalSort == 0) {
            Rate = 0;
        }
        if (ProviderID == "") {
            ProviderID = 0;
        }
        popOrder.ShowList('', ProviderID, CurrencyTypeID, Rate, isMoreUnit);
    }
}


function FillProvider(providerid, providerno, providername, taketype, taketypename, carrytype, carrytypename, paytype, paytypename)// 选择供应商带出部分信息
{
    $("#txtProviderID").val(providername);
    $("#txtHidProviderID").val(providerid);
    $("#txtHiddenProviderID1").val(providername); // 当选择源单类型时，用于显示供应商并且不允许再修改

    if (taketype == "") {
        taketype = "0";
    }
    if (carrytype == "") {
        carrytype = "0";
    }
    if (paytype == "") {
        paytype = "0";
    }
    $("#drpTakeType").val(taketype);
    $("#drpCarryType").val(carrytype);
    $("#drpPayType").val(paytype);

    closeProviderdiv();
}

// 判断是否有相同记录有返回true，没有返回false
function ExistFromBill(frombillid, fromlineno) {
    var signFrame = document.getElementById("dg_Log");

    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 1; i < signFrame.rows.length; ++i) {
        var FromBillID = document.getElementById("txtFromBillID" + i).value.Trim();
        var FromLineNo = document.getElementById("txtFromLineNo" + i).value.Trim();

        if ((signFrame.rows[i].style.display != "none") && (frombillid == FromBillID) && (fromlineno == FromLineNo)) {
            return true;
        }
    }
    return false;
}


// /遍历选择的是否都是同一个供应商
function CheckIsOneProvider() {
    var ck = document.getElementsByName("ChkBoxPPurArrive");
    var getCheck = new Array();
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            getCheck.push($("#PPurArriveProviderID" + i).html());
        }
    }
    if (getCheck.length > 1) {

        for (var a = 0; a < getCheck.length; a++) {
            if (getCheck[a] == getCheck[0]) {
                continue;
            }
            else {
                return false;
                break;
            }
        }
    }
    return true;
}

function GetValuePPurArrive() {
    if (!CheckIsOneProvider()) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "选择的明细只能来自一个供应商！");
        return;
    }

    var ck = document.getElementsByName("ChkBoxPPurArrive");
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            if (isMoreUnit) {
                FillFromOrder($("#PPurArriveProductID" + i).html(), $("#PPurArriveProductNo" + i).html(), $("#PPurArriveProductName" + i).html(), $("#PPurArrivestandard" + i).html(), $("#PPurArriveUsedUnitID" + i).html(),
                $("#PPurArriveUsedUnitName" + i).html(), $("#PPurArriveUsedUnitCount" + i).html(), $("#PPurArriveRequireDate" + i).html(), $("#PPurArriveUnitPrice" + i).html(), $("#PPurArriveTaxPrice" + i).html(),
                $("#PPurArriveDiscount" + i).html(), $("#PPurArriveTaxRate" + i).html(), $("#PPurArriveTotalPrice" + i).html(), $("#PPurArriveTotalFee" + i).html(), $("#PPurArriveTotalTax" + i).html(),
                $("#PPurArriveRemark" + i).html(), $("#PPurArriveFromBillID" + i).html(), $("#PPurArriveFromBillNo" + i).html(), $("#PPurArriveFromLineNo" + i).html(), $("#PPurArriveArrivedCount" + i).html(),
                $("#PPurArriveProviderID" + i).html(), $("#PPurArriveProviderName" + i).html(), $("#PPurArriveCurrencyType" + i).html(), $("#PPurArriveCurrencyTypeName" + i).html(), $("#PPurArriveRate" + i).html()
                    , $("#PPurArrivePurchaser" + i).html(), $("#PPurArrivePurchaserName" + i).html(), $("#PPurArriveUnitID" + i).html(), $("#PPurArriveColorName" + i).html());
            }
            else {
                FillFromOrder($("#PPurArriveProductID" + i).html(), $("#PPurArriveProductNo" + i).html(), $("#PPurArriveProductName" + i).html(), $("#PPurArrivestandard" + i).html(), $("#PPurArriveUnitID" + i).html(),
                $("#PPurArriveUnitName" + i).html(), $("#PPurArriveProductOrder" + i).html(), $("#PPurArriveRequireDate" + i).html(), $("#PPurArriveUnitPrice" + i).html(), $("#PPurArriveTaxPrice" + i).html(),
                $("#PPurArriveDiscount" + i).html(), $("#PPurArriveTaxRate" + i).html(), $("#PPurArriveTotalPrice" + i).html(), $("#PPurArriveTotalFee" + i).html(), $("#PPurArriveTotalTax" + i).html(),
                $("#PPurArriveRemark" + i).html(), $("#PPurArriveFromBillID" + i).html(), $("#PPurArriveFromBillNo" + i).html(), $("#PPurArriveFromLineNo" + i).html(), $("#PPurArriveArrivedCount" + i).html(),
                $("#PPurArriveProviderID" + i).html(), $("#PPurArriveProviderName" + i).html(), $("#PPurArriveCurrencyType" + i).html(), $("#PPurArriveCurrencyTypeName" + i).html(), $("#PPurArriveRate" + i).html()
                , $("#PPurArrivePurchaser" + i).html(), $("#PPurArrivePurchaserName" + i).html(), '', $("#PPurArriveColorName" + i).html());
            }
        }
    }
}




function FillFromOrder(productid, productno, productname, standard, unitid, unitname, productorder, requiredate, unitprice, taxprice, discount,
                       taxrate, totalprice, totalfee, totaltax, remark, frombillid, frombillno, fromLineno, arrivedcount, providerid, providername, currencytype, currencytypename, rate, Purchaser, PurchaserName, usedUnitID, ColorName) {

    // 添加采购员
    $("#HidPurchaser").val(Purchaser)
    $("#UserPurchaser").val(PurchaserName)

    // 带出供应商及币种等信息
    $("#txtHidProviderID").val(providerid);
    $("#txtProviderID").val(providername);
    $("#txtHiddenProviderID1").val(providername);
    for (var i = 0; i < document.getElementById("drpCurrencyType").options.length; ++i) {
        if (document.getElementById("drpCurrencyType").options[i].value.split('_')[0] == currencytype) {
            $("#drpCurrencyType").attr("selectedIndex", i);
            break;
        }
    }
    $("#CurrencyTypeID").val(currencytype);
    $("#txtRate").val(rate);
    document.getElementById('txtProviderID').disabled = true;
    document.getElementById('drpCurrencyType').disabled = true;


    if (!ExistFromBill(frombillid, fromLineno)) {
        var Index = findObj("txtTRLastIndex", document).value.Trim();
        AddSignRow();
        var ProductID = 'txtProductID' + Index;
        var ProductNo = 'txtProductNo' + Index;
        var ProductName = 'txtProductName' + Index;
        var Standard = 'txtstandard' + Index;
        var txtColorName = 'txtColorName' + Index;
        var UnitID = 'txtUnitID' + Index;
        var UnitName = 'txtUnitName' + Index;
        var ProductOrder = 'txtProductOrder' + Index;
        var ProductCount = 'txtProductCount' + Index;
        var RequireDate = 'txtRequireDate' + Index;
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
        var ArrivedCount = 'txtArrivedCount' + Index;
        var HiddUnitPrice = 'hiddUnitPrice' + Index;

        document.getElementById(ProductID).value = productid;
        document.getElementById(ProductNo).value = productno;
        document.getElementById(ProductName).value = productname;
        document.getElementById(Standard).value = standard;
        document.getElementById(txtColorName).value = ColorName; // 颜色
        $("#hidExistProduct_" + Index).val('1'); // 判断产品存在

        if (isMoreUnit) {// 启用多计量单位
            document.getElementById('UsedUnitName' + Index).value = unitname;
            document.getElementById('UsedUnitID' + Index).value = unitid;
            GetUnitGroupSelectEx(productid, 'InUnit', 'txtUnitID' + Index, 'ChangeUnit(this,' + Index + ',1)', 'tdUnitName' + Index, usedUnitID, 'SetDisabled(' + Index + ')');
            $("#txtProductCount" + Index).blur(function() { ChangeUnit(this, Index); });
        }
        else {
            document.getElementById("txtUnitID" + Index).value = unitid;
            document.getElementById("txtUnitName" + Index).value = unitname;
        }
        document.getElementById(ProductOrder).value = FormatAfterDotNumber(productorder, selPoint);
        document.getElementById(ArrivedCount).value = FormatAfterDotNumber(arrivedcount, selPoint);

        document.getElementById(ProductCount).value = FormatAfterDotNumber(0, selPoint); // 默认为0，好计算金额
        document.getElementById(RequireDate).value = requiredate;
        document.getElementById(UnitPrice).value = FormatAfterDotNumber(unitprice, selPoint);
        document.getElementById(TaxPrice).value = FormatAfterDotNumber(taxprice, selPoint);
        document.getElementById(TaxRate).value = FormatAfterDotNumber(taxrate, selPoint);
        document.getElementById(Remark).value = remark;
        document.getElementById(FromBillID).value = frombillid;
        document.getElementById(FromBillNo).value = frombillno;
        document.getElementById(FromLineNo).value = fromLineno;
        document.getElementById(HiddTaxPrice).value = FormatAfterDotNumber(taxprice, selPoint);
        document.getElementById(HiddTaxRate).value = FormatAfterDotNumber(taxrate, selPoint);
        document.getElementById(HiddUnitPrice).value = FormatAfterDotNumber(unitprice, selPoint);

    }
    closeOrderdiv();


    var isAddTax = document.getElementById("chkisAddTax").checked;
    // 新加币种的汇率问题
    var Rate = $("#txtRate").val().Trim();

    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {// 是增值税则
                //                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value.Trim() / Rate, selPoint); // 含税价等于隐藏域含税价
                // 再除以汇率(币种要求修改)
                //                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 单价则改为隐藏域里的单价除以汇率
                //                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxRate" + rowIndex).value.Trim(), selPoint); // 税率等于隐藏域税率
                $("#AddTax").html("是增值税");
            }
            else {
                //                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 含税价等于单价
                //                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 单价则改为隐藏域里的单价除以汇率
                //                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, selPoint); // 税率等于0
                $("#AddTax").html("非增值税");
            }
        }
    }
}


// 设置单位不可以修改
function SetDisabled(iRow) {
    $("#txtUnitID" + iRow).attr("disabled", "true");
}


function Fun_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, CodeName, TaxRate, SellTax,
            Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, GroupUnitNo,
            SaleUnitID, SaleUnitName, InUnitID, InUnitName, StockUnitID, StockUnitName, MakeUnitID, MakeUnitName, IsBatchNo, ColorName) {
    var Index = parseInt(popTechObj.InputObj);
    $("#txtProductID" + Index).val(ID);
    $("#txtProductNo" + Index).val(ProdNo);
    $("#txtProductName" + Index).val(ProductName);
    $("#txtstandard" + Index).val(Specification);
    $("#txtColorName" + Index).val(ColorName);
    $("#hidExistProduct_" + Index).val('1'); // 判断产品存在
    $("#txtUnitPrice" + Index).val(FormatAfterDotNumber(TaxBuy, selPoint)); // 去税进价
    $("#txtTaxPrice" + Index).val(FormatAfterDotNumber(StandardBuy, selPoint)); // 含税进价
    $("#txtTaxRate" + Index).val(FormatAfterDotNumber(InTaxRate, selPoint)); // 进项税率
    $("#hiddUnitPrice" + Index).val(FormatAfterDotNumber(TaxBuy, selPoint));
    $("#hiddTaxPrice" + Index).val(FormatAfterDotNumber(StandardBuy, selPoint));
    $("#hiddTaxRate" + Index).val(FormatAfterDotNumber(InTaxRate, selPoint));
    if (isMoreUnit) {// 启用多计量单位
        $("#UsedUnitID" + Index).val(UnitID);
        $("#UsedUnitName" + Index).val(CodeName);
        GetUnitGroupSelectEx(ID, 'InUnit', 'txtUnitID' + Index, 'ChangeUnit(this,' + Index + ',1)', 'tdUnitName' + Index, '', 'ChangeUnit(this,' + Index + ',1)');
        $("#txtProductCount" + Index).blur(function() { ChangeUnit(this, Index); });
    }
    else {
        $("#txtUnitID" + Index).val(UnitID);
        $("#txtUnitName" + Index).val(CodeName);
    }

    //ClearProductInfo();
}

//function Fun_FillParent_Content(id, no, productname, dddf, unitid, unit, df, sdfge, discount, standard, fg, fgf, taxprice, price, taxrate, ColorName) {
//    var temp = popTechObj.InputObj;
//    var index = parseInt(temp.split('o')[2]);
//    var ProductNo = 'txtProductNo' + index;
//    var ProductName = 'txtProductName' + index;
//    var UnitID = 'txtUnitID' + index;
//    var Unit = 'txtUnitName' + index;
//    var Price = 'txtUnitPrice' + index;
//    var Standard = 'txtstandard' + index;
//    var txtColorName = 'txtColorName' + index;
//    var ProductID = 'txtProductID' + index;
//    var TaxPrice = 'txtTaxPrice' + index;
//    var Discount = 'txtDiscount' + index;
//    var TaxRate = 'txtTaxRate' + index;
//    var HiddTaxPrice = 'hiddTaxPrice' + index;
//    var HiddTaxRate = 'hiddTaxRate' + index;
//    var HiddUnitPrice = 'hiddUnitPrice' + index;

//    document.getElementById(TaxRate).value = taxrate;
//    document.getElementById(TaxPrice).value = taxprice;
//    document.getElementById(Standard).value = standard;
//    document.getElementById(txtColorName).value = ColorName; // 颜色
//    if (isMoreUnit) {// 启用多计量单位
//        document.getElementById('UsedUnitName' + index).value = unit;
//        document.getElementById('UsedUnitID' + index).value = unitid;
//        GetUnitGroupSelectEx(id, 'InUnit', 'txtUnitID' + index, 'ChangeUnit(this,' + index + ',1)', 'tdUnitName' + index, '', 'ChangeUnit(this,' + index + ',1)');
//        $("#txtProductCount" + index).attr("onblur", 'ChangeUnit(this,' + index + ')');
//    }
//    else {
//        document.getElementById("txtUnitID" + index).value = unitid;
//        document.getElementById("txtUnitName" + index).value = unit;
//    }
//    document.getElementById(ProductID).value = id;
//    document.getElementById(ProductNo).value = no;
//    document.getElementById(ProductName).value = productname;
//    document.getElementById(Price).value = price;
//    document.getElementById(HiddTaxPrice).value = taxprice;
//    document.getElementById(HiddTaxRate).value = taxrate;
//    document.getElementById(HiddUnitPrice).value = FormatAfterDotNumber(price, selPoint);

//    var isAddTax = document.getElementById("chkisAddTax").checked;
//    // 新加币种的汇率问题
//    var Rate = $("#txtRate").val().Trim();
//    var signFrame = findObj("dg_Log", document);
//    for (i = 1; i < signFrame.rows.length; i++) {
//        if (signFrame.rows[i].style.display != "none") {
//            var rowIndex = i;
//            if (isAddTax == true) {// 是增值税则
//                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value.Trim() / Rate, selPoint); // 含税价等于隐藏域含税价
//                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 单价则改为隐藏域里的单价除以汇率
//                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxRate" + rowIndex).value.Trim(), selPoint); // 税率等于隐藏域税率
//                $("#AddTax").html("是增值税");
//            }
//            else {
//                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 含税价等于单价
//                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 单价则改为隐藏域里的单价除以汇率
//                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, selPoint); // 税率等于0
//                $("#AddTax").html("非增值税");
//            }
//        }
//    }
//}


function FillUnit(unitid, unitname) // 回填单位
{
    var i = popUnitObj.InputObj;
    var UnitID = "txtUnitID" + i;
    var UnitName = "txtUnitName" + i;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(UnitName).value = unitname;
}




// 确认
function Fun_ConfirmOperate() {
    // alert ("1");
    var c = window.confirm("确认执行确认操作吗？")
    if (c == true) {
        var ActionArrive = $("#txtAction").val()

        if (ActionArrive == "1") {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先保存再确认！");
            return;
        }

        glb_BillID = document.getElementById('txtIndentityID').value;
        $("#txtConfirmorReal").val($("#UserName").val().Trim());
        $("#txtConfirmor").val($("#UserID").val().Trim());
        $("#txtConfirmDate").val($("#SystemTime").val().Trim());
        action = "Confirm";

        var DetailProductCount = new Array();
        var DetailFromBillNo = new Array();
        var DetailFromLineNo = new Array();
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
        var length = 0;
        var signFrame = findObj("dg_Log", document);
        for (var i = 0; i < txtTRLastIndex - 1; i++) {

            if (signFrame.rows[i + 1].style.display != "none") {
                var objProductCount = 'txtProductCount' + (i + 1);
                var objFromBillNo = 'txtFromBillNo' + (i + 1);
                var objFromLineNo = 'txtFromLineNo' + (i + 1);


                DetailProductCount.push(document.getElementById(objProductCount.toString()).value.Trim());
                DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value.Trim());
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value.Trim());
                length++;
            }
        }

        var confirmor = $("#txtConfirmor").val().Trim();
        var arriveNo = $("#divPurchaseArriveNo").html();
        var strParams = "action=" + action
            + "&confirmor=" + escape(confirmor)
            + "&arriveNo=" + escape(arriveNo)
            + "&ID=" + escape(glb_BillID)
            + "&DetailProductCount=" + escape(DetailProductCount)
            + "&DetailFromBillNo=" + escape(DetailFromBillNo)
            + "&DetailFromLineNo=" + escape(DetailFromLineNo)
            + "&length=" + escape(length) + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseArriveAdd.ashx",
            dataType: 'json', // 返回json格式数据
            data: strParams,
            cache: false,
            beforeSend: function() {
            },
            error: function() {
                // popMsgObj.ShowMsg('sdf');
            },
            success: function(data) {
                if (data.sta > 0) {
                    $("#ddlBillStatus").val("2");
                    popMsgObj.ShowMsg('确认成功');
                    $("#txtModifiedUserID").val($("#txtModifiedUserID2").val().Trim());
                    $("#txtModifiedUserIDReal").val($("#txtModifiedUserID2").val().Trim());
                    $("#txtModifiedDate").val($("#txtModifiedDate2").val().Trim());
                    fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                    fnFlowStatus($("#FlowStatus").val());
                    GetFlowButton_DisplayControl(); // 审批处理
                    $("#imgBilling").css("display", "inline");
                    $("#imgUnBilling").css("display", "none");
                }
                else {
                    popMsgObj.ShowMsg(data.data);
                }
            }
        });
    }
}



// 取消确认
function Fun_UnConfirmOperate() {
    var c = window.confirm("确认执行取消确认操作吗？")
    if (c == true) {
        glb_BillID = document.getElementById('txtIndentityID').value;
        $("#txtConfirmorReal").val("");
        $("#txtConfirmor").val("");
        $("#txtConfirmDate").val("");
        action = "CancelConfirm";

        var DetailProductCount = new Array();
        var DetailFromBillNo = new Array();
        var DetailFromLineNo = new Array();
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
        var length = 0;
        var signFrame = findObj("dg_Log", document);
        for (var i = 0; i < txtTRLastIndex - 1; i++) {

            if (signFrame.rows[i + 1].style.display != "none") {
                var objProductCount = 'txtProductCount' + (i + 1);
                var objFromBillNo = 'txtFromBillNo' + (i + 1);
                var objFromLineNo = 'txtFromLineNo' + (i + 1);


                DetailProductCount.push(document.getElementById(objProductCount.toString()).value.Trim());
                DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value.Trim());
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value.Trim());
                length++;
            }
        }

        var confirmor = $("#txtConfirmor").val().Trim();
        var confirmdate = $("#txtConfirmDate").val().Trim();
        var arriveNo = $("#divPurchaseArriveNo").html();
        var strParams = "action=" + action
                    + "&confirmor=" + confirmor
                    + "&confirmdate=" + confirmdate
                    + "&arriveNo=" + arriveNo
                    + "&ID=" + glb_BillID
                    + "&DetailProductCount=" + DetailProductCount
                    + "&DetailFromBillNo=" + DetailFromBillNo
                    + "&DetailFromLineNo=" + DetailFromLineNo
                    + "&length=" + length + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseArriveAdd.ashx",
            data: strParams,
            dataType: 'json', // 返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
                // popMsgObj.ShowMsg('sdf');
            },
            success: function(data) {
                if (data.sta > 0) {
                    $("#ddlBillStatus").val("1");
                    popMsgObj.ShowMsg('取消确认成功');

                    $("#txtModifiedUserID").val($("#txtModifiedUserID2").val().Trim());
                    $("#txtModifiedUserIDReal").val($("#txtModifiedUserID2").val().Trim());
                    $("#txtModifiedDate").val($("#txtModifiedDate2").val().Trim());
                    fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                    fnFlowStatus($("#FlowStatus").val());
                    GetFlowButton_DisplayControl(); // 审批处理
                    $("#imgBilling").css("display", "none");
                    $("#imgUnBilling").css("display", "inline");
                }
                else {
                    popMsgObj.ShowMsg('该单据已被其它单据调用了，不允许取消确认！');
                }
            }
        });
    }
}





// 结单或取消结单按钮操作
function Fun_CompleteOperate(isComplete) {
    if (isComplete) {
        var c = window.confirm("确认执行结单操作吗？")
        if (c == true) {
            glb_BillID = document.getElementById('txtIndentityID').value;
            $("#txtCloserReal").val($("#UserName").val().Trim());
            $("#txtCloser").val($("#UserID").val().Trim());
            $("#txtCloseDate").val($("#SystemTime").val().Trim());
            var closer = $("#txtCloser").val().Trim();
            var arriveNo = $("#divPurchaseArriveNo").html();

            if (isComplete) {
                action = "Close";
            }
            else {
                action = "CancelClose";
            }
            // 结单操作

            var strParams = "action=" + action + "&closer=" + closer + "&arriveNo=" + arriveNo + "&ID=" + glb_BillID + "";
            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/PurchaseManager/PurchaseArriveAdd.ashx?" + strParams,

                dataType: 'json', // 返回json格式数据
                cache: false,
                beforeSend: function() {
                },
                error: function() {
                    // popMsgObj.ShowMsg('sdf');
                },
                success: function(data) {
                    if (data.sta > 0) {
                        if (data.sta == 1) {
                            $("#ddlBillStatus").val("4");
                            $("#txtModifiedUserID").val($("#txtModifiedUserID2").val().Trim());
                            $("#txtModifiedUserIDReal").val($("#txtModifiedUserID2").val().Trim());
                            $("#txtModifiedDate").val($("#txtModifiedDate2").val().Trim());
                            fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val());
                        }
                        else {
                            $("#ddlBillStatus").val("2");
                            $("#txtModifiedUserID").val($("#txtModifiedUserID2").val().Trim());
                            $("#txtModifiedUserIDReal").val($("#txtModifiedUserID2").val().Trim());
                            $("#txtModifiedDate").val($("#txtModifiedDate2").val().Trim());
                            fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val());
                        }
                        // popMsgObj.ShowMsg('确认成功');
                        popMsgObj.ShowMsg(data.info);
                        GetFlowButton_DisplayControl(); // 审批处理
                        $("#imgBilling").css("display", "none");
                        $("#imgUnBilling").css("display", "inline");

                    }
                }
            });
        }
    }
    else {
        var c = window.confirm("确认执行取消结单操作吗？")
        if (c == true) {
            glb_BillID = $("#txtIndentityID").val().Trim();
            $("#txtCloserReal").val($("#UserName").val().Trim());
            $("#txtCloser").val($("#UserID").val().Trim());
            $("#txtCloseDate").val($("#SystemTime").val().Trim());
            var closer = $("#txtCloser").val().Trim();
            var arriveNo = $("#divPurchaseArriveNo").html();

            if (isComplete) {
                action = "Close";
            }
            else {
                action = "CancelClose";
            }
            // 结单操作

            var strParams = "action=" + action + "\
                                     &closer=" + closer + "\
                                     &arriveNo=" + arriveNo + "\
                                     &ID=" + glb_BillID + "";
            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/PurchaseManager/PurchaseArriveAdd.ashx?" + strParams,

                dataType: 'json', // 返回json格式数据
                cache: false,
                beforeSend: function() {
                },
                error: function() {
                    // popMsgObj.ShowMsg('sdf');
                },
                success: function(data) {
                    if (data.sta > 0) {
                        if (data.sta == 1) {
                            $("#ddlBillStatus").val("4");
                            $("#txtModifiedUserID").val($("#txtModifiedUserID2").val().Trim());
                            $("#txtModifiedUserIDReal").val($("#txtModifiedUserID2").val().Trim());
                            $("#txtModifiedDate").val($("#txtModifiedDate2").val().Trim());
                            fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val());
                        }
                        else {
                            $("#ddlBillStatus").val("2");
                            $("#txtModifiedUserID").val($("#txtModifiedUserID2").val().Trim());
                            $("#txtModifiedUserIDReal").val($("#txtModifiedUserID2").val().Trim());
                            $("#txtModifiedDate").val($("#txtModifiedDate2").val().Trim());
                            fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val());
                        }
                        // popMsgObj.ShowMsg('确认成功');
                        popMsgObj.ShowMsg(data.info);
                        GetFlowButton_DisplayControl(); // 审批处理
                        $("#imgBilling").css("display", "inline");
                        $("#imgUnBilling").css("display", "none");
                    }
                }
            });
        }
    }

}


function Fun_FlowApply_Operate_Succeed(operateType) {
    try {
        if (operateType == "0") {// 提交审批成功后,不可改
            $("#imgUnSave").css("display", "inline"); // 保存灰
            $("#imgSave").css("display", "none"); // 保存

            $("#imgAdd").css("display", "none"); // 明细添加
            $("#imgUnAdd").css("display", "inline"); // 明细添加灰
            $("#imgDel").css("display", "none"); // 明细删除
            $("#imgUnDel").css("display", "inline"); // 明细删除灰
            $("#Get_Potential").css("display", "none"); // 源单总览
            $("#Get_UPotential").css("display", "inline"); // 源单总览灰
            $("#btnGetGoods").css("display", "none"); // 条码扫描按钮

        }
        else if (operateType == "1") {// 审批成功后，不可改
            $("#imgUnSave").css("display", "inline");
            $("#imgSave").css("display", "none");


            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#Get_Potential").css("display", "none");
            $("#Get_UPotential").css("display", "inline");
            $("#btnGetGoods").css("display", "none"); // 条码扫描按钮

        }
        else if (operateType == "2") {// 审批不通过
            $("#imgUnSave").css("display", "none");
            $("#imgSave").css("display", "inline");


            $("#imgAdd").css("display", "inline");
            $("#imgUnAdd").css("display", "none");
            $("#imgDel").css("display", "inline");
            $("#imgUnDel").css("display", "none");
            $("#Get_Potential").css("display", "inline");
            $("#Get_UPotential").css("display", "none");
            $("#btnGetGoods").css("display", "inline"); // 条码扫描按钮

        }
        else if (operateType == "3") {
            $("#CodingRuleControl1_txtCode").val(objFlow.BillID);

        }
    }
    catch (e)
    { }
}

// 根据单据状态决定页面按钮操作
function fnStatus(BillStatus, Isyinyong) {
    try {
        switch (BillStatus) { // 单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
            case '1': // 制单
                // $("#HiddenAction").val('Add');
                // fnFlowStatus($("#FlowStatus").val());
                break;
            case '2': // 执行
                if (Isyinyong == 'True') {// 被引用不可编辑
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#Get_Potential").css("display", "none");
                    $("#Get_UPotential").css("display", "inline");
                    $("#btnGetGoods").css("display", "none"); // 条码扫描按钮
                }
                else {
                    // $("#HiddenAction").val('Update');
                    $("#imgSave").css("display", "inline");
                    $("#imgUnSave").css("display", "none");
                    $("#imgAdd").css("display", "inline");
                    $("#imgUnAdd").css("display", "none");
                    $("#imgDel").css("display", "inline");
                    $("#imgUnDel").css("display", "none");
                    $("#Get_Potential").css("display", "inline");
                    $("#Get_UPotential").css("display", "none");
                    $("#btnGetGoods").css("display", "inline"); // 条码扫描按钮
                }

                break;
            case '3': // 变更
                // $("#HiddenAction").val('Update');
                $("#FromType").attr("disabled", "disabled");
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
                break;
            case '4': // 手工结单
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                $("#btnGetGoods").css("display", "none"); // 条码扫描按钮
                break;

            case '5':
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
            case "": // 未提交审批
                break;
            case "待审批": // 当前单据正在待审批
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                $("#btnGetGoods").css("display", "none"); // 条码扫描按钮

                break;
            case "审批中": // 当前单据正在审批中
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                $("#btnGetGoods").css("display", "none"); // 条码扫描按钮

                break;
            case "审批通过": // 当前单据已经通过审核
                // 制单状态的审批通过单据,不可修改
                if ($("#ddlBillStatus").val() == "1") {
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#Get_Potential").css("display", "none");
                    $("#Get_UPotential").css("display", "inline");
                    $("#btnGetGoods").css("display", "none"); // 条码扫描按钮

                }

                break;
            case "撤消审批": // 当前单据进行取消确认了.
                // 取消确认后，为制单,可以修改
                if ($("#ddlBillStatus").val() == "1") {
                    $("#imgSave").css("display", "inline");
                    $("#imgUnSave").css("display", "none");
                    $("#imgAdd").css("display", "inline");
                    $("#imgUnAdd").css("display", "none");
                    $("#imgDel").css("display", "inline");
                    $("#imgUnDel").css("display", "none");
                    $("#Get_Potential").css("display", "inline");
                    $("#Get_UPotential").css("display", "none");
                    $("#btnGetGoods").css("display", "inline"); // 条码扫描按钮

                }

                break;
            case "审批不通过": // 当前单据审批未通过

                break;
        }
    }
    catch (e)
    { }
}



function SetSaveButton_DisplayControl(flowStatus) {
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
    var length = 0;
    var signFrame = findObj("dg_Log", document);

    try {
        switch (parseInt(flowStatus)) {
            case 5: // 当前单据进行取消确认了.
                // 取消确认后，为制单,可以修改
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
                $("#imgAdd").css("display", "inline");
                $("#imgUnAdd").css("display", "none");
                $("#imgDel").css("display", "inline");
                $("#imgUnDel").css("display", "none");
                $("#Get_Potential").css("display", "inline");
                $("#Get_UPotential").css("display", "none");
                $("#btnGetGoods").css("display", "inline"); // 条码扫描按钮
                for (var i = 0; i < txtTRLastIndex - 1; i++) {

                    if (signFrame.rows[i + 1].style.display != "none") {
                        var objProductCount = 'txtProductCount' + (i + 1);
                        document.getElementById(objProductCount.toString()).disabled = false;
                        length++;
                    }
                }

                break;
            case 0: // 未提交审批
                break;
            case 1: // 当前单据正在待审批
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                $("#btnGetGoods").css("display", "none"); // 条码扫描按钮
                for (var i = 0; i < txtTRLastIndex - 1; i++) {

                    if (signFrame.rows[i + 1].style.display != "none") {
                        var objProductCount = 'txtProductCount' + (i + 1);
                        document.getElementById(objProductCount.toString()).disabled = true;
                        length++;
                    }
                }

                break;
            case 2: // 当前单据正在审批中
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                $("#btnGetGoods").css("display", "none"); // 条码扫描按钮
                for (var i = 0; i < txtTRLastIndex - 1; i++) {

                    if (signFrame.rows[i + 1].style.display != "none") {
                        var objProductCount = 'txtProductCount' + (i + 1);
                        document.getElementById(objProductCount.toString()).disabled = true;
                        length++;
                    }
                }

                break;
            case 3: // 当前单据已经通过审核
                // 制单状态的审批通过单据,不可修改
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                $("#btnGetGoods").css("display", "none"); // 条码扫描按钮
                for (var i = 0; i < txtTRLastIndex - 1; i++) {

                    if (signFrame.rows[i + 1].style.display != "none") {
                        var objProductCount = 'txtProductCount' + (i + 1);
                        document.getElementById(objProductCount.toString()).disabled = true;
                        length++;
                    }
                }


                break;

            case 4: // 当前单据审批未通过
                break;
        }
    }
    catch (e)
    { }
}



function clearProviderdiv() {
    $("#txtProviderID").val("");
    $("#txtHidProviderID").val("");
}


/* 单据打印 */
function PurchaseArrivePrint() {
    var ID = $("#txtIndentityID").val();
    if (parseInt(ID) == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先保存！");
        return;
    }

    // if(confirm("请确认您的单据已经保存？"))
    window.open("../../../Pages/PrinttingModel/PurchaseManager/PurchaseArrivePrint.aspx?ID=" + $("#txtIndentityID").val());
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


// 供应商控件委琐遮照
/* 弹出 */
function PopPurProviderInfo() {
    openRotoscopingDiv(false, 'divPBackShadow', 'PBackShadowIframe');
    popProviderObj.ShowList()
}
/* 关闭 */
function closeProviderdiv() {
    closeRotoscopingDiv(false, 'divPBackShadow');
    $("#divProviderInfohhh").css("display", "none");
}

// ---------------------------------------------------条码扫描Start------------------------------------------------------------------------------------
/*
* 参数：商品ID，商品编号，商品名称，去税售价，单位ID，单位名称，销项税率（%），含税售价，销售折扣，规格，类型名称，类型ID，含税进价，去税进价，进项税率(%)，标准成本
*/
// 根据条码获取的商品信息填充数据
function GetGoodsDataByBarCode(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax
                                , Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, IsBatchNo, BatchNo
                                , ProductCount, CurrentStore, Source, ColorName) {
    if (!IsExist(ID))// 如果重复记录，就不增加
    {
        var rowID = AddSignRow(); // 插入行
        // 填充数据
        // 物品ID
        // 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
        BarCode_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, rowID, ColorName);
    }
    else {
        document.getElementById("txtProductCount" + rerowID).value = parseFloat(document.getElementById("txtProductCount" + rerowID).value) + 1;
        ChangeUnit(this, rerowID); // 更改数量后重新计算
    }

}

var rerowID = "";
// 判断是否有相同记录有返回true，没有返回false
function IsExist(ID) {
    var signFrame = findObj("dg_Log", document);
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (i = 1; i < signFrame.rows.length; i++) {// 判断商品是否在明细列表中
        if (signFrame.rows[i].style.display != "none") {
            var ProductID = $("#txtProductID" + i).val(); // 商品ID（对应商品表ID）
            if (ProductID == ID) {
                rerowID = i;
                return true;
            }
        }
    }
    return false;
}
// 物品ID 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
function BarCode_FillParent_Content(id, no, productname, dddf, unitid, unit, df, sdfge, discount, standard, fg, fgf, taxprice, price, taxrate, index, ColorName) {
    var ProductNo = 'txtProductNo' + index;
    var ProductName = 'txtProductName' + index;
    var UnitID = 'txtUnitID' + index;
    var Unit = 'txtUnitName' + index;
    var Price = 'txtUnitPrice' + index;
    var Standard = 'txtstandard' + index;
    var txtColorName = 'txtColorName' + index;
    var ProductID = 'txtProductID' + index;
    var TaxPrice = 'txtTaxPrice' + index;
    var Discount = 'txtDiscount' + index;
    var TaxRate = 'txtTaxRate' + index;
    var HiddTaxPrice = 'hiddTaxPrice' + index;
    var HiddTaxRate = 'hiddTaxRate' + index;
    var HiddUnitPrice = 'hiddUnitPrice' + index;

    document.getElementById(TaxRate).value = FormatAfterDotNumber(taxrate, selPoint);
    document.getElementById(TaxPrice).value = FormatAfterDotNumber(taxprice, selPoint);
    document.getElementById(Standard).value = FormatAfterDotNumber(standard, selPoint);
    document.getElementById(txtColorName).value = ColorName; // 颜色
    $("#hidExistProduct_" + index).val('1'); // 判断产品存在
    if (isMoreUnit) {// 启用多计量单位
        document.getElementById('UsedUnitName' + index).value = unit;
        document.getElementById('UsedUnitID' + index).value = unitid;
        GetUnitGroupSelectEx(id, 'InUnit', 'txtUnitID' + index, 'ChangeUnit(this,' + index + ',1)', 'tdUnitName' + index, '', 'ChangeUnit(this,' + index + ',1)');
        $("#txtProductCount" + index).blur(function() { ChangeUnit(this, index); });
    }
    else {
        document.getElementById("txtUnitID" + index).value = unitid;
        document.getElementById("txtUnitName" + index).value = unit;
    }
    document.getElementById(ProductID).value = id;
    document.getElementById(ProductNo).value = no;
    document.getElementById(ProductName).value = productname;
    document.getElementById(Price).value = FormatAfterDotNumber(price, selPoint);
    document.getElementById(HiddTaxPrice).value = FormatAfterDotNumber(taxprice, selPoint);
    document.getElementById(HiddTaxRate).value = FormatAfterDotNumber(taxrate, selPoint);
    document.getElementById(HiddUnitPrice).value = FormatAfterDotNumber(price, selPoint);
    document.getElementById("txtProductCount" + index).value = FormatAfterDotNumber(1, selPoint);

    var isAddTax = document.getElementById("chkisAddTax").checked;
    // 新加币种的汇率问题
    var Rate = $("#txtRate").val().Trim();
    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = i;
            if (isAddTax == true) {// 是增值税则
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddTaxPrice" + rowIndex).value.Trim() / Rate, selPoint); // 含税价等于隐藏域含税价
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = document.getElementById("hiddTaxRate" + rowIndex).value.Trim(); // 税率等于隐藏域税率
                $("#AddTax").html("是增值税");
            }
            else {
                document.getElementById("txtTaxPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 含税价等于单价
                document.getElementById("txtUnitPrice" + rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate, selPoint); // 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = FormatAfterDotNumber(0, selPoint); // 税率等于0
                $("#AddTax").html("非增值税");
            }
        }
    }
    fnTotalInfo();
}
// ---------------------------------------------------条码扫描END------------------------------------------------------------------------------------
function ShowSnapshot() {
    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';
    var ck = document.getElementsByName("chk");
    var signFrame = findObj("dg_Log", document);
    for (var i = 0; i < ck.length; i++) {

        var rowID = i + 1;
        if (signFrame.rows[rowID].style.display != "none") {
            if (ck[i].checked) {
                detailRows++;
                intProductID = document.getElementById('txtProductID' + rowID).value;
                snapProductName = document.getElementById('txtProductNo' + rowID).value;
                snapProductNo = document.getElementById('txtProductName' + rowID).value;
            }
        }

    }

    if (detailRows == 1) {
        if (intProductID <= 0 || strlen(cTrim(intProductID, 0)) <= 0) {
            popMsgObj.ShowMsg('选中的明细行中没有添加物品');
            return false;
        }
        ShowStorageSnapshot(intProductID, snapProductName, snapProductNo);

    }
    else {
        popMsgObj.ShowMsg('请选择单个物品查看库存快照');
        return false;
    }
}

//开票
function fnToBilling() {
    var ParamLoc = "";
    ParamLoc = _id + "|"
            + escape($("#divPurchaseArriveNo").html()) + "|"
            + escape($("#txtHidProviderID").val()) + "|"
            + escape($("#txtProviderID").val()) + "|"
            + escape($("#drpCurrencyType").val().split('_')[0]) + "|"
            + escape($("#drpCurrencyType").find("option:selected").html()) + "|"
            + escape($("#txtRate").val()) + "|"
            + escape($("#txtRealTotal").val()) + "|6";
    var billingmoduleid = $("#hiddBillingAddModuleid").val();
    var moduleid = $("#hiddModuleID").val();
    var para = $("#hidSearchCondition").val();
    if (para.indexOf("ArriveNo") > 0) {
        //  把已经存在的ParamLoc消除
        para = para.substr(para.indexOf("ArriveNo"));
    }
    if (para.indexOf("intMasterArriveID") < 0) {
        // 如果intMasterArriveID不存在就需要添加，因为在开票返回到当前界面需要
        para += "&intMasterArriveID=" + _id;
    }
    window.location.href = "../FinanceManager/Billing_Add.aspx?ParamLoc=" + ParamLoc + "&ModuleID=" + billingmoduleid + "&" + para.replace("ModuleID", "ModuleIDSource");
}