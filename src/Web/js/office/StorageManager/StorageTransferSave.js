$(document).ready(function() {
    fnGetExtAttr(); //物品控件拓展属性
    funSetCodeBar();
});

/*重写toFixed*/
Number.prototype.toFixed = function(d) {
    return FormatAfterDotNumber(this, parseInt($("#hidSelPoint").val()));
}

  var selPoint=0;


window.onload = function() { Init();
  selPoint=  parseInt( document.getElementById("hidSelPoint").value); }

/*参数设置：是否显示条码控制*/
function funSetCodeBar() {
    try {
        var hidCodeBar = document.getElementById('hidCodeBar').value;
        var objCodeBar = document.getElementById('btnGetGoods');
        hidCodeBar == '1' ? objCodeBar.style.display = '' : objCodeBar.style.display = 'none';
    } catch (e) { }

}

//初始化页面信息
function Init() {
    var action = document.getElementById("txtAction").value;
    var divTitle = document.getElementById("divTitle");
    if (action == "ADD") {
        GetExtAttr('officedba.StorageTransfer', null);
        document.getElementById("imgNew").style.display = "none";
        document.getElementById("imgBack").style.display = "none";
        GetFlowButton_DisplayControl();
    }
    else if (action == "EDIT") {
        divTitle.innerHTML = "库存调拨单";
        document.getElementById("imgBack").style.display = "block";
        document.getElementById("div_InNo_uc").style.display = "none";
        document.getElementById("div_InNo_Lable").style.display = "block";
        getBaseInfo();
    }
    if (document.getElementById("txtIsBack").value == "1") {
        document.getElementById("imgBack").style.display = "block";
    }
}

//验证信息完整性
function ValidateInfo() {
    var msgtitle = "";
    var msgtext = "";
    var flag = true;

    //获取编码规则下拉列表选中项
    var codeRule = document.getElementById("txtRuleCodeNo_ddlCodeRule").value;
    var action = document.getElementById("txtAction").value;
    //如果选中的是 手工输入时，校验编号是否输入
    if (codeRule == "" && action != "EDIT") {
        //获取输入的编号
        var txtPlanNo = document.getElementById("txtRuleCodeNo_txtCode").value;
        //编号必须输入
        if (txtPlanNo == "") {
            flag = false;
            msgtitle = msgtitle + "单据编号|";
            msgtext = msgtext + "请输入单据编号|";
        }
        else {
            if (!CodeCheck(txtPlanNo)) {
                flag = false;
                msgtitle = msgtitle + "单据编号|";
                msgtext = msgtext + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
    }

    if (document.getElementById("txtApplyUserID").value == "") {
        msgtitle = msgtitle + "调拨申请人|";
        msgtext = msgtext + "请输入调拨申请人|";
        flag = false;
    }
    if (document.getElementById("txtApplyDeptID").value == "") {
        msgtitle = msgtitle + "要货部门|";
        msgtext = msgtext + "请输入要货部门|";
        flag = false;
    }
    if (document.getElementById("ddlInStorageID").value == "-1") {
        msgtitle = msgtitle + "调入仓库|";
        msgtext = msgtext + "请输入调入仓库|";
        flag = false;
    }
    if (document.getElementById("txtRequireInDate").value == "") {
        msgtitle = msgtitle + "要求到货日期|";
        msgtext = msgtext + "请输入要求到货日期|";
        flag = false;
    }
    if (document.getElementById("txtOutDeptID").value == "") {
        msgtitle = msgtitle + "调货部门|";
        msgtext = msgtext + "请输入调货部门|";
        flag = false;
    }
    if (document.getElementById("ddlOutStorageID").value == "-1") {
        msgtitle = msgtitle + "调出仓库|";
        msgtext = msgtext + "请输入调出仓库|";
        flag = false;
    }
    if (strlen(document.getElementById("tboxSummary").value) > 200) {
        msgtitle = msgtitle + "摘要|";
        msgtext = msgtext + "摘要字符数不能大于200|"
        flag = false;
    }
    if (strlen(document.getElementById("tboxRemark").value) > 800) {
        msgtitle = msgtitle + "备注|";
        msgtext = msgtext + "备注字符数不能大于800|";
        flag = false;
    }
    if (document.getElementById("ddlInStorageID").value == document.getElementById("ddlOutStorageID").value) {
        msgtitle = msgtitle + "调入仓库|";
        msgtext = msgtext + "调入仓库与调出仓库不能相同|";
        flag = false;
    }
    if (!IsNumOrFloat(document.getElementById("txtTransferFeeSum").value, false) && document.getElementById("txtTransferFeeSum").value != "" && document.getElementById("txtTransferFeeSum").value != "0" && document.getElementById("txtTransferFeeSum").value != "0.00" && parseFloat(document.getElementById("txtTransferFeeSum").value)!=parseFloat("0")) {
        msgtitle = msgtitle + "调拨费用|";
        msgtext = msgtext + "调拨费用必须为数值|";
        flag = false;
    }
    if (document.getElementById("txtTitle").value != "") {
        if (!CheckSpecialWord(document.getElementById("txtTitle").value)) {
            msgtitle = msgtitle + "主题|";
            msgtext = msgtext + "主题中包含特殊字符|";
            flag = false;
        }
    }
    if (document.getElementById("tboxSummary").value != "") {
        if (!CheckSpecialWord(document.getElementById("tboxSummary").value)) {
            msgtitle = msgtitle + "摘要|";
            msgtext = msgtext + "摘要中包含特殊字符|";
            flag = false;
        }
    }

    if (document.getElementById("tboxRemark").value != "") {
        if (!CheckSpecialWord(document.getElementById("tboxRemark").value)) {
            msgtitle = msgtitle + "备注|";
            msgtext = msgtext + "备注中包含特殊字符|";
            flag = false;
        }
    }
    /*验证明细*/
    var ck = document.getElementsByName("chk");
    if (ck.length <= 0) {
        msgtitle = msgtitle + "明细|";
        msgtext = msgtext + "调拨单明细不能为空|";
        flag = false;
    }
    else {
        var isHas = true;
        var FlagIndex = 0;
        for (var i = 0; i < ck.length; i++) {
            if (document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {
                FlagIndex++;
                if (document.getElementById("txtTransferCount_" + ck[i].value).value == "") {
                    isHas = false;
                }
            }
        }
        if (FlagIndex == 0) {
            msgtitle = msgtitle + "明细|";
            msgtext = msgtext + "调拨单明细不能为空|";
            flag = false;
        }
        if (!isHas) {
            msgtitle = msgtitle + "明细|";
            msgtext = msgtext + "至少有一行明细为未填写完整|";
            flag = false;
        }
    }


    if (!flag) {
        popMsgObj.Show(msgtitle, msgtext);
        return false;
    }
    else
        return true;
}

//保存数据 添加与更新
function Save() {
    var action = document.getElementById("txtAction").value;
    if (!ValidateInfo())
        return;
    if (action == "ADD") {
        Add();
    }
    else {
        Edit();
    }
}

//添加
function Add() {
    var TransferNo = "";
    var bmgz = "";
    if (document.getElementById("txtRuleCodeNo_ddlCodeRule").value == "") {
        TransferNo = document.getElementById("txtRuleCodeNo_txtCode").value;
        if (TransferNo == "") {
            popMsgObj.Show("编号|", "请输入单据编号|");
            return;
        }
        else
            bmgz = "sd";
    }
    else {
        bmgz = "zd";
        TransferNo = document.getElementById("txtRuleCodeNo_ddlCodeRule").value;
    }

    var para = "TransferNo=" + TransferNo +
                        "&Title=" + escape(document.getElementById("txtTitle").value) +
                        "&ApplyUserID=" + document.getElementById("txtApplyUserID").value +
                        "&ApplyDeptID=" + document.getElementById("txtApplyDeptID").value +
                        "&InStorageID=" + document.getElementById("ddlInStorageID").value +
                        "&RequireInDate=" + document.getElementById("txtRequireInDate").value +
                        "&ReasonType=" + document.getElementById("ddlReasonType").value +
                        "&OutDeptID=" + document.getElementById("txtOutDeptID").value +
                        "&OutStorageID=" + document.getElementById("ddlOutStorageID").value +
                        "&BusiStatus=" + document.getElementById("ddlBusiStatus").value +
                        "&Summary=" + escape(document.getElementById("tboxSummary").value) +
                        "&TotalCount=" + document.getElementById("txtTotalCount").value +
                        "&TotalPrice=" + document.getElementById("txtTotalPrice").value +
                        "&Remark=" + escape(document.getElementById("tboxRemark").value) +
                        "&action=ADD&bmgz=" + bmgz +
                        "&TransferFeeSum=" + document.getElementById("txtTransferFeeSum").value + GetExtAttrValue();

    var SortNo = new Array();
    var ProductID = new Array();
    var ProductName = new Array();
    var ProductSpec = new Array();
    var ProductUnit = new Array();
    var TransferPrice = new Array();
    var TransferCount = new Array();
    var TransferTotalPrice = new Array();
    var RemarkList = new Array();
    var UsedUnitID = new Array();
    var UsedCount = new Array();
    var UsedPrice = new Array();
    var ExRate = new Array();
    var BatchNo = new Array();

    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {
            SortNo.push(document.getElementById("txtSortNo_" + ck[i].value).value);
            ProductID.push(document.getElementById("txtProductNo_" + ck[i].value).title);
            ProductName.push(escape(document.getElementById("txtProductName_" + ck[i].value).value));
            ProductSpec.push(escape(document.getElementById("txtProductSpec_" + ck[i].value).value));
            ProductUnit.push(document.getElementById("txtProductUnit_" + ck[i].value).title);
            TransferPrice.push(document.getElementById("txtTransferPrice_" + ck[i].value).value);
            TransferCount.push(document.getElementById("txtTransferCount_" + ck[i].value).value);
            TransferTotalPrice.push(document.getElementById("txtTransferTotalPrice_" + ck[i].value).value);
            BatchNo.push(escape(document.getElementById("BatchNo_SignItem_TD_Text_" + ck[i].value).value));
            RemarkList.push("");
            if ($("#txtIsMoreUnit").val() == "1") {
                UsedUnitID.push(document.getElementById("txtUsedUnit_" + ck[i].value).value.split('|')[0]);
                UsedCount.push(document.getElementById("txtUsedCount_" + ck[i].value).value);
                UsedPrice.push(document.getElementById("txtUsedPrice_" + ck[i].value).value);
                ExRate.push(document.getElementById("txtUsedUnit_" + ck[i].value).value.split('|')[1]);
            }
            else {
                UsedUnitID.push("0");
                UsedCount.push("0");
                UsedPrice.push("0");
                ExRate.push("0");
            }
        }
    }
    para = para + "&SortNo=" + SortNo.toString() +
                "&ProductID=" + ProductID.toString() +
                "&ProductName=" + ProductName.toString() +
                "&ProductSpec=" + ProductSpec.toString() +
                "&ProductUnit=" + ProductUnit.toString() +
                "&TransferPrice=" + TransferPrice.toString() +
                "&TransferCount=" + TransferCount.toString() +
                "&TransferTotalPrice=" + TransferTotalPrice.toString() +
                "&RemarkList=" + RemarkList.toString() +
                "&UsedUnitID=" + UsedUnitID.toString() +
                "&UsedCount=" + UsedCount.toString() +
                "&UsedPrice=" + UsedPrice.toString() +
                "&ExRate=" + ExRate.toString() +
                "&BatchNo=" + BatchNo.toString();

    var url = "../../../Handler/Office/StorageManager/StorageTransferSave.ashx";

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            var Flag = msg.split('|');
            if (Flag[0] == "1") {
                popMsgObj.Show("保存信息|", "保存成功|");
                document.getElementById("div_InNo_uc").style.display = "none";
                document.getElementById("div_InNo_Lable").style.display = "block";
                document.getElementById("txtNo").value = Flag[1].split('#')[1];
                document.getElementById("txtTransferID").value = Flag[1].split('#')[0];
                document.getElementById("txtAction").value = "EDIT";
                GetFlowButton_DisplayControl();
                GetCurrentInfo(3);
                GetFlowButton_DisplayControl();
            }
            else if (Flag[0] == "2")
                popMsgObj.Show("保存信息|", Flag[1] + "|");
            else if (Flag[0] == "3") {
                popMsgObj.Show("保存信息|", Flag[1] + "|");
            }
            else
                popMsgObj.Show("保存信息|", "保存失败|");

        },
        error: function() { popMsgObj.Show("保存|", "保存失败|"); }
    });
}

//更新
function Edit() {
    var TransferNo = document.getElementById("txtNo").value;
    var para = "TransferNo=" + TransferNo +
                    "&Title=" + escape(document.getElementById("txtTitle").value) +
                    "&ApplyUserID=" + document.getElementById("txtApplyUserID").value +
                    "&ApplyDeptID=" + document.getElementById("txtApplyDeptID").value +
                    "&InStorageID=" + document.getElementById("ddlInStorageID").value +
                    "&RequireInDate=" + document.getElementById("txtRequireInDate").value +
                    "&ReasonType=" + document.getElementById("ddlReasonType").value +
                    "&OutDeptID=" + document.getElementById("txtOutDeptID").value +
                    "&OutStorageID=" + document.getElementById("ddlOutStorageID").value +
                    "&BusiStatus=" + document.getElementById("ddlBusiStatus").value +
                    "&Summary=" + escape(document.getElementById("tboxSummary").value) +
                    "&TotalCount=" + document.getElementById("txtTotalCount").value +
                    "&TotalPrice=" + document.getElementById("txtTotalPrice").value +
                    "&Remark=" + escape(document.getElementById("tboxRemark").value) +
                    "&action=EDIT" +
                    "&TransferFeeSum=" + document.getElementById("txtTransferFeeSum").value +
                    "&id=" + document.getElementById("txtTransferID").value + GetExtAttrValue();

    var SortNo = new Array();
    var ProductID = new Array();
    var ProductName = new Array();
    var ProductSpec = new Array();
    var ProductUnit = new Array();
    var TransferPrice = new Array();
    var TransferCount = new Array();
    var TransferTotalPrice = new Array();
    var RemarkList = new Array();
    var UsedUnitID = new Array();
    var UsedCount = new Array();
    var UsedPrice = new Array();
    var ExRate = new Array();
    var BatchNo = new Array();

    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {
            SortNo.push(document.getElementById("txtSortNo_" + ck[i].value).value);
            ProductID.push(document.getElementById("txtProductNo_" + ck[i].value).title);
            ProductName.push(escape(document.getElementById("txtProductName_" + ck[i].value).value));
            ProductSpec.push(escape(document.getElementById("txtProductSpec_" + ck[i].value).value));
            ProductUnit.push(document.getElementById("txtProductUnit_" + ck[i].value).title);
            TransferPrice.push(document.getElementById("txtTransferPrice_" + ck[i].value).value);
            TransferCount.push(document.getElementById("txtTransferCount_" + ck[i].value).value);
            TransferTotalPrice.push(document.getElementById("txtTransferTotalPrice_" + ck[i].value).value);
            BatchNo.push(escape(document.getElementById("BatchNo_SignItem_TD_Text_" + ck[i].value).value));
            RemarkList.push("");
            if ($("#txtIsMoreUnit").val() == "1") {
                UsedUnitID.push(document.getElementById("txtUsedUnit_" + ck[i].value).value.split('|')[0]);
                UsedCount.push(document.getElementById("txtUsedCount_" + ck[i].value).value);
                UsedPrice.push(document.getElementById("txtUsedPrice_" + ck[i].value).value);
                ExRate.push(document.getElementById("txtUsedUnit_" + ck[i].value).value.split('|')[1]);
            }
            else {
                UsedUnitID.push("0");
                UsedCount.push("0");
                UsedPrice.push("0");
                ExRate.push("0");
            }
        }
    }
    para = para + "&SortNo=" + SortNo.toString() +
            "&ProductID=" + ProductID.toString() +
            "&ProductName=" + ProductName.toString() +
            "&ProductSpec=" + ProductSpec.toString() +
            "&ProductUnit=" + ProductUnit.toString() +
            "&TransferPrice=" + TransferPrice.toString() +
            "&TransferCount=" + TransferCount.toString() +
            "&TransferTotalPrice=" + TransferTotalPrice.toString() +
            "&RemarkList=" + RemarkList.toString() +
            "&UsedUnitID=" + UsedUnitID.toString() +
            "&UsedCount=" + UsedCount.toString() +
            "&UsedPrice=" + UsedPrice.toString() +
            "&ExRate=" + ExRate.toString() +
            "&BatchNo=" + BatchNo.toString();

    var url = "../../../Handler/Office/StorageManager/StorageTransferSave.ashx";

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            var Flag = msg.split('|');
            if (Flag[0] == "1") {
                popMsgObj.Show("保存信息|", "保存成功|");
                GetFlowButton_DisplayControl();
                GetCurrentInfo(3);
                GetFlowButton_DisplayControl();
            }
            else if (Flag[0] == "2")
                popMsgObj.Show("保存信息|", Flag[1] + "|");
            else
                popMsgObj.Show("保存信息|", "保存失败|");

        },
        error: function() { popMsgObj.Show("保存|", "保存失败|"); }
    });
}

//添加行
function AddRow() {
    var tbl = document.getElementById("tblTransfer");
    var LastRowID = document.getElementById("txtLastRowID");
    var LastSortNo = document.getElementById("txtLastSortNo");
    var RowID = parseInt(LastRowID.value);
    //添加行
    var row = tbl.insertRow(-1);
    row.id = "tr_Row_" + RowID;
    row.className = "newrow";

    var i = 0;

    //添加checkbox列
    var cellCheck = row.insertCell(i);
    cellCheck.className = "cell";
    cellCheck.align = "center";
    cellCheck.innerHTML = "<input type=\"checkbox\" id=\"chk_list_" + RowID + "\" name=\"chk\" value=\"" + RowID + "\" onclick=\"subSelect();\" />";
    i++;

    //序号
    var cellNo = row.insertCell(i);
    cellNo.className = "cell";
    cellNo.innerHTML = "<input type=\"text\" id=\"txtSortNo_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseInt(LastSortNo.value) + "\"  name=\"rownum\" disabled=\"true\"/>";
    i++;

    //物品编号
    var cellProductNo = row.insertCell(i);
    cellProductNo.className = "cell";
    cellProductNo.innerHTML = "<input type=\"text\" id=\"txtProductNo_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  readonly onclick=\"getProdcutList(" + RowID + ");\"  />";
    i++;

    //物品名称
    var cellProductName = row.insertCell(i);
    cellProductName.className = "cell";
    cellProductName.innerHTML = "<input type=\"text\" id=\"txtProductName_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  disabled=\"true\"/>";
    i++;


    //批次
    var cellProductName = row.insertCell(i);
    cellProductName.className = "cell";
    cellProductName.innerHTML = "<select style=' width:90%;' id='BatchNo_SignItem_TD_Text_" + RowID + "' />";
    i++;

    //规格
    var cellProdcutSpec = row.insertCell(i);
    cellProdcutSpec.className = "cell";
    cellProdcutSpec.innerHTML = "<input type=\"text\" id=\"txtProductSpec_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  disabled=\"true\" />";
    i++;

    //计量单位开启时（基本单位）、反（单位）
    var cellProductUnit = row.insertCell(i);
    cellProductUnit.className = "cell";
    cellProductUnit.innerHTML = "<input type=\"text\" id=\"txtProductUnit_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\" />";
    i++;

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        //调拨数量
        var cellTransferCount = row.insertCell(i);
        cellTransferCount.className = "cell";
        cellTransferCount.innerHTML = "<input type=\"text\" id=\"txtTransferCount_" + RowID + "\"  disabled=\"true\"  class=\"tdinput tboxsize textAlign\" value=\"\" />";
        i++;

        //单位
        var cellTransferCount = row.insertCell(i);
        cellTransferCount.className = "cell";
        cellTransferCount.id = "tboxUsedUnit_listtd_" + RowID;
        cellTransferCount.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        i++;
    }

    //调拨单价
    var cellTransferPrice = row.insertCell(i);
    cellTransferPrice.className = "cell";

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        cellTransferPrice.innerHTML = "<input type=\"hidden\" id=\"txtTransferPrice_" + RowID + "\" /><input type=\"text\" id=\"txtUsedPrice_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\" />";
    } else {
        cellTransferPrice.innerHTML = "<input type=\"text\" id=\"txtTransferPrice_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\" />";
    }
    i++;

    //调拨数量
    var cellTransferCount = row.insertCell(i);
    cellTransferCount.className = "cell";
    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        cellTransferCount.innerHTML = "<input type=\"text\" id=\"txtUsedCount_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  onblur=\"_cDetail(" + RowID + ")\"  onchange=\"Number_round(this,"+selPoint+")\"/>";
    } else {
        cellTransferCount.innerHTML = "<input type=\"text\" id=\"txtTransferCount_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  onblur=\"_cDetail(" + RowID + ")\"  onchange=\"Number_round(this,"+selPoint+")\"/>";
    }
    i++;

    //调拨金额
    var cellTransferTotalPrice = row.insertCell(i);
    cellTransferTotalPrice.className = "cell";
    cellTransferTotalPrice.innerHTML = "<input type=\"text\" id=\"txtTransferTotalPrice_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\"/>";
    i++;

    /*已出库数量*/
    var cellTransferTotalPrice = row.insertCell(i);
    cellTransferTotalPrice.className = "cell";
    cellTransferTotalPrice.innerHTML = "";
    i++;

    /*已入库数量*/
    var cellTransferTotalPrice = row.insertCell(i);
    cellTransferTotalPrice.className = "cell";
    cellTransferTotalPrice.innerHTML = "";
    i++;

    //移动行号与序号
    LastRowID.value = parseInt(LastRowID.value) + 1;
    LastSortNo.value = parseInt(LastSortNo.value) + 1;

}

//全选
function selall() {
    var ck = document.getElementsByName("chk");
    var Flag = document.getElementById("chkDetail").checked;
    for (var i = 0; i < ck.length; i++) {
        ck[i].checked = Flag;
    }
}
//子项单击
function subSelect() {
    var Flag = true;
    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        if (!ck[i].checked) {
            Flag = false;
        }
    }
    document.getElementById("chkDetail").checked = Flag;
}

//载入产品信息
function getProdcutList(rowid) {
    var OutDeptID = document.getElementById("txtOutDeptID").value;
    var OutStorageID = document.getElementById("ddlOutStorageID").value;
    if (OutStorageID == "-1") {
        popMsgObj.Show("基本信息|", "请选择调货仓库|");
        return;
    }
    var Para = "OutStorageID=" + OutStorageID + "&OutDeptID=" + OutDeptID + "&action=GETPUT";
    document.getElementById("txtProductRowID").value = rowid;
    popProductInfoObj.ShowList(Para, rowid);

}

//填充明细
function fnSelectProductInfo(ID, ProductNo, ProductName, ProductSpec, ProductCount, ProdcutUnitName, ProductUnitID, TransferPrice, rowid,BatchNo) {
    rowid = document.getElementById("txtProductRowID").value;
    document.getElementById("txtProductNo_" + rowid).value = ProductNo;
    document.getElementById("txtProductNo_" + rowid).title = ID;
    document.getElementById("txtProductName_" + rowid).value = ProductName;
    document.getElementById("txtProductName_" + rowid).title = ProductCount;
    document.getElementById("txtProductSpec_" + rowid).value = ProductSpec;
    document.getElementById("txtProductUnit_" + rowid).value = ProdcutUnitName;
    document.getElementById("txtProductUnit_" + rowid).title = ProductUnitID;
    document.getElementById("txtTransferPrice_" + rowid).value = parseFloat(TransferPrice).toFixed(2);
    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        $("#txtUsedPrice_" + rowid).val(TransferPrice);
        GetUnitGroupSelect(ID, 'StockUnit', 'txtUsedUnit_' + rowid, 'ChangeUnit(this,' + rowid + ')', "tboxUsedUnit_listtd_" + rowid, "");
    }

    GetBatchList(ID, "ddlOutStorageID", "BatchNo_SignItem_TD_Text_" + rowid, document.getElementById("hidIsBatchNo").value == "1" ? true : false,BatchNo);

    closeProductInfodiv();
}

/***************************************************
*切换单位
***************************************************/
function ChangeUnit(own, rowid) {
    CalCulateNum('txtUsedUnit_' + rowid, "txtUsedCount_" + rowid, "txtTransferCount_" + rowid, "txtUsedPrice_" + rowid, "txtTransferPrice_" + rowid, $("#hidSelPoint").val());
    _cDetail(rowid);
}

/***************************************************
*验证调拨数量与现存数量的关系
***************************************************/
function checkTCount(rowid) {
    return true;
}


//合计 包括明细
function _cDetail(rowid) {
    var objCount = 0;
    var objPrice = 0;

    if ($("#txtIsMoreUnit").val() == "1") {
        objPrice = document.getElementById("txtUsedPrice_" + rowid);
        objCount = document.getElementById("txtUsedCount_" + rowid);
    }
    else {
        objPrice = document.getElementById("txtTransferPrice_" + rowid);
        objCount = document.getElementById("txtTransferCount_" + rowid);
    }
    if (!IsNumOrFloat(objCount.value, false)) {
        popMsgObj.Show("调拨明细|", "调拨数量必须为数字|");
        objCount.value = "";
        return;
    } 
    if ($("#txtIsMoreUnit").val() == "1") {
        CalCulateNum('txtUsedUnit_' + rowid, "txtUsedCount_" + rowid, "txtTransferCount_" + rowid, "txtUsedPrice_" + rowid, "txtTransferPrice_" + rowid, $("#hidSelPoint").val());
    }

    if (!checkTCount(rowid))
        return;

    document.getElementById("txtTransferTotalPrice_" + rowid).value = parseFloat(parseFloat(objCount.value) * parseFloat(objPrice.value)).toFixed(2);
    _cTotal();
}

//合计
function _cTotal() {
    var ck = document.getElementsByName("chk");
    var TotalCount = 0;
    var TotalPrice = 0;
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {
            var dTotalCount = document.getElementById("txtTransferCount_" + ck[i].value).value == "" ? "0" : document.getElementById("txtTransferCount_" + ck[i].value).value;
            var dTotalPrice = document.getElementById("txtTransferTotalPrice_" + ck[i].value).value == "" ? "0" : document.getElementById("txtTransferTotalPrice_" + ck[i].value).value;
            if ($("#txtIsMoreUnit").val() == "1") {
                var dTotalCount = document.getElementById("txtUsedCount_" + ck[i].value).value == "" ? "0" : document.getElementById("txtUsedCount_" + ck[i].value).value;
            }
            TotalCount = TotalCount + parseFloat(dTotalCount);
            TotalPrice = parseFloat(TotalPrice) + parseFloat(dTotalPrice);
        }
    }
    document.getElementById("txtTotalCount").value = parseFloat(TotalCount).toFixed(2);
    document.getElementById("txtTotalPrice").value = parseFloat(TotalPrice).toFixed(2);
}

//格式化价格
function formatNumber(num, exponent) {
    return num.toFixed(exponent);
}

//获取选中行的ROWID
function getCheckedList() {
    var ck = document.getElementsByName("chk");
    var IDList = new Array();
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked & document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {
            IDList.push(ck[i].value);
        }
    }
    return IDList;
}

//移除行
function RemoveRow() {
    var IDList = getCheckedList();
    if (IDList.length <= 0) {
        popMsgObj.Show("调拨明细|", "请先选中需要操作的行|");
        return;
    }
    var LastSortNo = document.getElementById("txtLastSortNo");
    if (confirm("确认删除选中" + IDList.length + "行的调拨明细嘛？")) {
        for (var i = 0; i < IDList.length; i++) {
            document.getElementById("tr_Row_" + IDList[i]).style.display = "none";
            Resort(IDList[i]);
            document.getElementById("chkDetail").checked = false;
        }
    }
    _cTotal();
}

//序号重新排序
function Resort(id) {
    var ck = document.getElementsByName("chk");
    var index = 1;
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {
            document.getElementById("txtSortNo_" + ck[i].value).value = index;
            index++;
        }
    }
    document.getElementById("txtLastSortNo").value = index;
}

//验证数据是否为浮点型
function isFloat(s) {
    if (IsNumber(s) != null) return true;
    var re = /^[+|-]{0,1}\d*\.\d+$/;
    return re.test(s);
}

//修改单据状态和业务状态 type [1 确认] [2 结单] [3 取消结单]
function UpdateStatus(type) {
    var para = "action=STA&type=" + type + "&ID=" + document.getElementById("txtTransferID").value + "&TransferNo=" + document.getElementById("txtNo").value;
    var url = "../../../Handler/Office/StorageManager/StorageTransferSave.ashx";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            if (msg.split('|')[0] == "1") {
                if (type == 1) {
                    popMsgObj.Show("单据操作|", "确认成功|");
                    document.getElementById("ddlBillStatus").value = "2";
                    document.getElementById("ddlBusiStatus").value = "2";
                    GetCurrentInfo(1);
                }
                else if (type == 2) {
                    popMsgObj.Show("单据操作|", "结单成功|");
                    document.getElementById("ddlBillStatus").value = "4";
                    GetCurrentInfo(2);
                }
                else if (type == 3) {
                    popMsgObj.Show("单据操作|", "取消结单成功|");
                    document.getElementById("ddlBillStatus").value = "2";
                    GetCurrentInfo(3);
                }
                else if (type == 4) {
                    popMsgObj.Show("单据操作|", "取消确认成功|");
                    document.getElementById("ddlBillStatus").value = "1";
                    document.getElementById("ddlBusiStatus").value = "1";
                    document.getElementById("tboxConfirmor").value = "";
                    document.getElementById("tboxConfirmorDate").value = "";
                }
                GetFlowButton_DisplayControl();
                //changeSaveButton();
            }
            else {
                if (type == 1)
                    popMsgObj.Show("单据操作|", "确认失败|");
                else if (type == 2)
                    popMsgObj.Show("单据操作|", "结单失败|");
                else if (type == 3)
                    popMsgObj.Show("单据操作|", "取消结单失败|");
            }


        },
        error: function() { popMsgObj.Show("|", "保存新建调拨单失败|"); }
    });

}

//单据确认
function Fun_ConfirmOperate() {
    if (confirm("是否确认单据？"))
        UpdateStatus(1);
}

function Fun_CompleteOperate(flag) {
    if (flag) {
        if (confirm("是否确认结单？"))
            UpdateStatus(2);
    }
    else {
        if (confirm("是否确认取消结单？"))
            UpdateStatus(3);
    }
}




function getBaseInfo() {
    var url = "../../../Handler/Office/StorageManager/StorageTransferSave.ashx";
    var para = "action=GET&TransferID=" + document.getElementById("txtTransferID").value;

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            var data = msg.data[0];
            GetExtAttr('officedba.StorageTransfer', data); //获取扩展属性并填充值 
            document.getElementById("txtNo").value = data.TransferNo;
            document.getElementById("txtTitle").value = data.Title;
            document.getElementById("UserApplyUser").value = data.ApplyUserIDName;
            document.getElementById("txtApplyUserID").value = data.ApplyUserID;
            document.getElementById("DeptApplyDept").value = data.ApplyDeptIDName;
            document.getElementById("txtApplyDeptID").value = data.ApplyDeptID;
            document.getElementById("ddlInStorageID").value = data.InStorageID;
            document.getElementById("txtRequireInDate").value = data.RequireInDate;
            document.getElementById("ddlReasonType").value = data.ReasonType;
            document.getElementById("DeptOutDeptID").value = data.OutDeptIDName;
            document.getElementById("txtOutDeptID").value = data.OutDeptID;
            document.getElementById("ddlOutStorageID").value = data.OutStorageID;
            document.getElementById("ddlBusiStatus").value = data.BusiStatus;
            document.getElementById("tboxSummary").value = data.Summary;
            document.getElementById("txtTotalCount").value = parseFloat(data.TransferCount).toFixed(2);
            document.getElementById("txtTotalPrice").value = parseFloat(data.TransferPrice).toFixed(2);
            document.getElementById("txtTransferFeeSum").value = parseFloat(data.TransferFeeSum == "" ? "0" : data.TransferFeeSum).toFixed(2);
            document.getElementById("tboxCreator").value = data.CreatorName;
            document.getElementById("tboxCreateDate").value = data.CreateDate;
            document.getElementById("ddlBillStatus").value = data.BillStatus;
            document.getElementById("tboxRemark").value = data.Remark;
            if (document.getElementById("ddlBillStatus").value != "1") {
                document.getElementById("tboxConfirmor").value = data.ConfirmorName;
                document.getElementById("tboxConfirmorDate").value = data.ConfirmDate;
            }
            else {
                document.getElementById("tboxConfirmor").value = "";
                document.getElementById("tboxConfirmorDate").value = "";
            }
            document.getElementById("tboxCloser").value = data.CloserName;
            document.getElementById("tboxCloseDate").value = data.CloseDate;
            document.getElementById("tbxoModifiedUser").value = data.ModifiedUserID;
            document.getElementById("tboxModifiedDate").value = data.ModifiedDate;
            getDetailInfo();

        },
        error: function() { popMsgObj.Show("保存|", "读取调拨单失败|"); }
    });
}

//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
    try {
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
    catch (Error) { }
}

//读取明细信息
function getDetailInfo() {
    var url = "../../../Handler/Office/StorageManager/StorageTransferSave.ashx";
    var para = "action=GETDTL&TransferNo=" + document.getElementById("txtNo").value;

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            GetFlowButton_DisplayControl();
            var index = 1;
            $("#tblTransfer tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item != null && item != "") {
                    var html = "<td class=\"cell\" align=\"center\"><input type=\"checkbox\" id=\"chk_list_" + item.SortNo + "\" name=\"chk\" value=\"" + item.SortNo + "\" onclick=\"subSelect();\" /></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtSortNo_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.SortNo + "\" disabled=\"true\"  name=\"rownum\"/></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductNo_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProdNo + "\"  onclick=\"getProdcutList(" + item.SortNo + ");\"  title=\"" + item.ProductID + "\" readonly /></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductName_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProductName + "\" disabled=\"true\" /></td>";
                    html += "<td  class=\"cell\" ><select style=' width:90%;' id='BatchNo_SignItem_TD_Text_" + item.SortNo + "' /></td>";

                    html += "<td class=\"cell\" ><input type=\"text\" id=\"txtProductSpec_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.Specification + "\"   disabled=\"true\"/></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductUnit_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.UnitName + "\" title=\"" + item.UnitID + "\"    disabled=\"true\" /></td>";
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" +parseFloat(  item.TranCount==""?"0":item.TranCount).toFixed(2) + "\"  disabled=\"true\" /></td>";
                        html += "<td  class=\"cell\" id=\"tboxUsedUnit_listtd_" + item.SortNo + "\" ></td>";
                    }

                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        html += "<td  class=\"cell\" ><input type=\"hidden\" id=\"txtTransferPrice_" + item.SortNo + "\" value=\"" + parseFloat(item.TranPrice).toFixed(2) + "\" />";
                        html += "<input type=\"text\" id=\"txtUsedPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.UsedPrice).toFixed(2) + "\"   disabled=\"true\" /></td>";
                    } else {
                        html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranPrice).toFixed(2) + "\"    disabled=\"true\" /></td>";
                    }
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        html += "<td class=\"cell\" ><input type=\"text\" id=\"txtUsedCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.UsedUnitCount).toFixed(2) + "\"  onblur=\"_cDetail(" + item.SortNo + ")\"  onchange=\"Number_round(this,"+selPoint+")\"/></td>";
                    } else {
                        html += "<td class=\"cell\" ><input type=\"text\" id=\"txtTransferCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranCount).toFixed(2) + "\"  onblur=\"_cDetail(" + item.SortNo + ")\"  onchange=\"Number_round(this,"+selPoint+")\"/></td>";
                    }
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferTotalPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranPriceTotal).toFixed(2) + "\"    disabled=\"true\"/></td>";
                    html += "<td class=\"cell\" ><input type=\"text\" id=\"txtOutCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.OutCount == "" ? "0" : item.OutCount).toFixed(2) + "\"   disabled=\"true\" /></td>";
                    html += "<td class=\"cell\" ><input type=\"text\" id=\"txtInCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.InCount == "" ? "0" : item.InCount).toFixed(2) + "\"  disabled=\"true\" /></td>";

                    $("<tr class='newrow' id=\"tr_Row_" + item.SortNo + "\"></tr>").append(html).appendTo($("#tblTransfer tbody")); index++;


                    //填充批次
                    GetBatchList(item.ProductID, "ddlOutStorageID", "BatchNo_SignItem_TD_Text_" + item.SortNo, document.getElementById("hidIsBatchNo").value == "1" ? true : false, item.BatchNo);
                    if ($("#txtIsMoreUnit").val() == "1") {
                        if (item.ExRate != null && item.ExRate != "")
                            GetUnitGroupSelect(item.ProductID, 'StockUnit', 'txtUsedUnit_' + item.SortNo, 'ChangeUnit(this,' + item.SortNo + ')', "tboxUsedUnit_listtd_" + item.SortNo, item.UsedUnitID);
                        else
                            GetUnitGroupSelectEx(item.ProductID, 'StockUnit', 'txtUsedUnit_' + item.SortNo, 'ChangeUnit(this,' + item.SortNo + ')', "tboxUsedUnit_listtd_" + item.SortNo, item.UsedUnitID, "ReCalByMoreUnit('" + item.SortNo + "','" + item.TranPrice + "')");
                    }
                   // document.getElementById("BatchNo_SignItem_TD_Text_" + item.SortNo).value =;





                }
            });
            document.getElementById("txtLastRowID").value = index;
            document.getElementById("txtLastSortNo").value = index;
        },
        error: function() { popMsgObj.Show("保存|", "读取调拨单失败|"); }
    });
}

function selectBatchNo(sortNo, BatchNo) { 
    
    
}



//对于开启多单位 以前的单据重新计算
function ReCalByMoreUnit(rowid, usedPrice) {

    //如果启用 多单位 则重新计算
    var strRate = document.getElementById("txtUsedUnit_" + rowid).value;
    var ExRate = strRate.split("|")[1];
    if (usedPrice == "")
        usedPrice = 0;

    $("#txtUsedPrice_" + rowid).val(parseFloat(parseFloat(usedPrice) * parseFloat(ExRate)).toFixed(2));
    $("#txtUsedCount_" + rowid).val("");



}



function backtolsit() {
    window.history.back(-1);
}


/*************************************************************
*即时更新 最后更新人 结单人 确认人 及时间
*type 1:确认操作 2：结单操作  其他为修改
*************************************************************/
function GetCurrentInfo(type) {
    var currentUsetID = document.getElementById("txtCurrentUserID").value;
    var currentDate = document.getElementById("txtCurrentDate").value;
    var currentUserName = document.getElementById("txtCurrentUserName").value;

    var objModifiedUser = document.getElementById("tbxoModifiedUser");
    var objModifiedDate = document.getElementById("tboxModifiedDate");
    if (type == 1) {
        document.getElementById("tboxConfirmor").value = currentUserName;
        document.getElementById("tboxConfirmorDate").value = currentDate;
    }
    else if (type == 2) {
        document.getElementById("tboxCloser").value = currentUserName;
        document.getElementById("tboxCloseDate").value = currentDate;
    }

    objModifiedUser.value = currentUsetID;
    objModifiedDate.value = currentDate;
}

/***********************************************************
*根据单据状态 和审批状态 操作 保存按钮 
***********************************************************/
function changeSaveButton() {
    var status = document.getElementById("ddlBillStatus").value;
    var objSvae = document.getElementById("imgSave");
    var objUnSave = document.getElementById("imgUnClickSave");
    if (status != "1") {
        objSvae.style.display = "none";
        objUnSave.style.display = "block";
    }
    else {
        if (document.getElementById("txtFlowStatus").value != "0") {
            objSvae.style.display = "none";
            objUnSave.style.display = "block";
        }
    }
}


/*根据单据状态和审批状态 是否启用保存按钮*/
function SetSaveButton_DisplayControl(flowStatus) {
    //流程状态：0：待提交   1：待审批   2：审批中   3：审批通过     4：审批不通过   5：撤销审批
    //制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
    //制单状态且审批状态“撤销审批”、“ 审批拒绝”状态的可以进行修改
    //变更和手工结单的不可以修改
    var PageBillID = document.getElementById('txtTransferID').value;
    var PageBillStatus = document.getElementById('ddlBillStatus').value;
    var objSave = document.getElementById("imgSave");
    var objUnSave = document.getElementById("imgUnClickSave");
    if (PageBillID != "") {

        if (PageBillStatus == '2' || PageBillStatus == '3' || PageBillStatus == '4') {
            objSave.style.display = "none";
            objUnSave.style.display = "block";
        }
        else {
            if (PageBillStatus == 1 && (flowStatus == 1 || flowStatus == 2 || flowStatus == 3)) {
                //单据状态+审批状态：制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
                objSave.style.display = "none";
                objUnSave.style.display = "block";
            }
            else {
                objSave.style.display = "block";
                objUnSave.style.display = "none";
            }
        }
    }
}

/*取消确认*/
function Fun_UnConfirmOperate() {

    var BusiStatus = document.getElementById("ddlBusiStatus").value;

    if (BusiStatus == "3") {
        popMsgObj.Show("单据操作|", "该调拨单已经执行出库操作，不能取消确认|");
        return;
    }
    if (BusiStatus == "4") {
        popMsgObj.Show("单据操作|", "该调拨单已经执行入库操作，不能取消确认|");
        return;
    }


    if (confirm("是否执行取消确认操作？")) {
        UpdateStatus(4);
    }

}

/*更换调出仓库时，清空明细*/
function clearDetail() {
    $("#tblTransfer tbody").find("tr.newrow").remove();
    CloseBarCodeDiv();

    /*初始化行号与序号*/
    document.getElementById("txtLastRowID").value = "0";
    document.getElementById("txtLastSortNo").value = "1";
}

/*打印*/
function BillPrint() {
    var ID = document.getElementById("txtTransferID").value;
    var No = document.getElementById("txtNo").value;
    if (ID == "" || ID == "0" || No == "") {
        popMsgObj.Show("打印|", "请保存单据再打印|");
        return;
    }
    //    if(confirm("请确认您的单据已经保存？"))
    window.open("../../../Pages/PrinttingModel/StorageManager/StorageTransferPrint.aspx?ID=" + ID + "&No=" + No);
}



/*-----------------------------------------------------条码扫描Start-----------------------------------------------------------------*/

var rerowID = "";
//判断是否有相同记录有返回true，没有返回false
function IsExist(prodNo) {
    var signFrame = document.getElementById("tblTransfer");
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }


    var length = $("#txtLastRowID").val();
    for (var i = 0; i <= length; i++) {

        var prodNo1 = document.getElementById("txtProductNo_" + i);

        if (prodNo1 != null && (signFrame.rows[i].style.display != "none") && (prodNo1.value == prodNo)) {
            rerowID = i;

            return true;
        }
    }
    return false;
}

//条码扫描方法
function GetGoodsDataByBarCode(ID, ProductNo, ProductName,
                                a, ProductUnitID, ProdcutUnitName,
                                b, c, d,
                                ProductSpec, e, f,
                                g, h, i,
                                TransferPrice) {
    if (!IsExist(ProductNo))//如果重复记录，就不增加
    {
        AddRowByBarCode(ID, ProductNo, ProductName, ProductSpec, ProdcutUnitName, ProductUnitID, TransferPrice);

    }
    else {

        //开启批次
        if ($("#txtIsMoreUnit").val() == "1") {
            var num = parseFloat($("#txtUsedCount_" + rerowID).val()).toFixed(2);
            $("#txtUsedCount_" + rerowID).val(++num);
        }
        else
            document.getElementById("txtTransferCount_" + rerowID).value = parseFloat(document.getElementById("txtTransferCount_" + rerowID).value) + 1;

        _cDetail(rerowID);
    }

}

//扫描增加行
function AddRowByBarCode(ID, ProductNo, ProductName, ProductSpec, ProdcutUnitName, ProductUnitID, TransferPrice) {

    var tbl = document.getElementById("tblTransfer");
    var LastRowID = document.getElementById("txtLastRowID");
    var LastSortNo = document.getElementById("txtLastSortNo");
    var rowid = parseInt(LastRowID.value);

    //添加行
    AddRow();
    //赋值
    //  rowid = document.getElementById("txtProductRowID").value;
    document.getElementById("txtProductNo_" + rowid).value = ProductNo;
    document.getElementById("txtProductNo_" + rowid).title = ID;
    document.getElementById("txtProductName_" + rowid).value = ProductName;
    document.getElementById("txtProductName_" + rowid).title = "";
    document.getElementById("txtProductSpec_" + rowid).value = ProductSpec;
    document.getElementById("txtProductUnit_" + rowid).value = ProdcutUnitName;
    document.getElementById("txtProductUnit_" + rowid).title = ProductUnitID;
    document.getElementById("txtTransferPrice_" + rowid).value = parseFloat(TransferPrice).toFixed(2);

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        $("#txtUsedPrice_" + rowid).val(TransferPrice);
        //  $("#txtUsedCount_" + rowid).val(1);
        GetUnitGroupSelectEx(ID, 'StockUnit', 'txtUsedUnit_' + rowid, 'ChangeUnit(this,' + rowid + ')', "tboxUsedUnit_listtd_" + rowid, ' ', "calProductCount('" + rowid + "')");
        // GetUnitGroupSelectEx(data.ProductID, 'StockUnit', 'tboxUsedUnit_list_' + rowid, 'ChangeUnit(this,' + rowid + ')', "tboxUsedUnit_listtd_" + rowid, data.UsedUnitID, "ReCalByMoreUnit('" + rowid + "','" + data.StandardCost + "')");
    }
    else {
        $("#txtTransferCount_" + rowid).val(1);
        _cDetail(rowid);
    }

    GetBatchList(ID, "ddlOutStorageID", "BatchNo_SignItem_TD_Text_" + rowid, document.getElementById("hidIsBatchNo").value == "1" ? true : false);


    //移动行号与序号
    LastRowID.value = parseInt(LastRowID.value) + 1;
    LastSortNo.value = parseInt(LastSortNo.value) + 1;

}
function calProductCount(rowid) {
    $("#txtUsedCount_" + rowid).val(1);
    //计算
    _cDetail(rowid);
}


function Showtmsm() {
    var StoID = $("#ddlOutStorageID").val();
    GetGoodsInfoByBarCode(StoID);
}

/*库存快照*/
function ShowSnapshot() {
    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';
    var signFrame = findObj("tblTransfer", document);
    for (var i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].id;
            if (document.getElementById('chk_list_' + i).checked) {
                detailRows++;
                intProductID = $("#txtProductNo_" + i).attr("title");
                snapProductName = $("#txtProductName_" + i).val();
                snapProductNo = $("#txtProductNo_" + i).val();
            }
        }

    }

    if (detailRows == 1) {
        try {
            if (intProductID <= 0 || strlen(cTrim(intProductID, 0)) <= 0) {
                popMsgObj.ShowMsg('选中的明细行中没有添加物品');
                return false;
            }
        }
        catch (ex) {
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

