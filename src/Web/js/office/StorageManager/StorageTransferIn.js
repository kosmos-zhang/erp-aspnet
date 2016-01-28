window.onload = function() { Init(); }

  var selPoint=0;


//初始化页面信息
function Init() {
    var action = document.getElementById("txtAction").value;
    var divTitle = document.getElementById("divTitle");

    divTitle.innerHTML = "调拨入库";
    document.getElementById("imgBack").style.display = "block";
    document.getElementById("div_InNo_uc").style.display = "none";
    document.getElementById("div_InNo_Lable").style.display = "block";
    getBaseInfo();

  selPoint=  parseInt( document.getElementById("hidSelPoint").value); 
}

function InitControl() {
    if (document.getElementById("ddlBusiStatus").value != "3") {
        document.getElementById("imgConfirm").style.display = "none";
        document.getElementById("imgUnConfirm").style.display = "block";
    }

}

/*重写toFixed*/
Number.prototype.toFixed = function(d) {
    return FormatAfterDotNumber(this, parseInt($("#hidSelPoint").val()));
}


//保存数据 添加与更新
function Save() {
    if (document.getElementById("txtInUserID").value == "") {
        popMsgObj.Show("基本信息|", "请选择入库人|");
        return;
    }
    if (document.getElementById("txtInDate").value == "") {
        popMsgObj.Show("基本信息|", "请选择入库日期|");
        return;
    }
    if (document.getElementById("txtOutDate").value > document.getElementById("txtInDate").value) {
        popMsgObj.Show("基本信息|", "入库日期必须在出库日期之后|");
        return;
    }
    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("txtOutCount_" + ck[i].value).value == "") {
            popMsgObj.Show("调拨单明细|", "调拨单明细行号" + ck[i].value + "：入库数量不能为空|");
            return;
        }
    }
    Edit();

}


//更新
function Edit() {
    if (!confirm("是否确认入库？"))
        return;
    var para = "TransferID=" + document.getElementById("txtTransferID").value +
                          "&TransferNo=" + document.getElementById("txtNo").value +
                          "&OutDate=" + document.getElementById("txtOutDate").value +
                          "&OutUserID=" + document.getElementById("txtOutUserID").value +
                          "&action=IN" +
                          "&OutCountTotal=" + document.getElementById("txtOutCount").value +
                          "&OutFeeSum=" + document.getElementById("txtOutFeeSum").value +
                        "&Title=" + escape(document.getElementById("txtTitle").value) +
                        "&ApplyUserID=" + document.getElementById("txtApplyUserID").value +
                        "&ApplyDeptID=" + document.getElementById("txtApplyDeptID").value +
                        "&InStorageID=" + document.getElementById("ddlInStorageID").value +
                        "&RequireInDate=" + document.getElementById("txtRequireInDate").value +
                        "&ReasonType=" + document.getElementById("ddlReasonType").value +
                        "&OutDeptID=" + document.getElementById("txtOutDeptID").value +
                        "&OutStorageID=" + document.getElementById("ddlOutStorageID").value +
                        "&BusiStatus=" + document.getElementById("ddlBusiStatus").value +
                        "&Summary=" + document.getElementById("tboxSummary").value +
                        "&TotalCount=" + document.getElementById("txtTotalCount").value +
                        "&TotalPrice=" + document.getElementById("txtTotalPrice").value +
                         "&TransferFeeSum=" + document.getElementById("txtTransferFeeSum").value +
                         "&InUserID=" + document.getElementById("txtInUserID").value +
                         "&InDate=" + document.getElementById("txtInDate").value +
                         "&InCountTotal=" + document.getElementById("txtInCount").value +
                         "&InFeeSum=" + document.getElementById("txtInFeeSum").value;

    var SortNo = new Array();
    var ProductID = new Array();
    var ProductName = new Array();
    var ProductSpec = new Array();
    var ProductUnit = new Array();
    var TransferPrice = new Array();
    var TransferCount = new Array();
    var TransferTotalPrice = new Array();
    var OutCount = new Array();
    var OutPriceTotal = new Array();
    var InCount = new Array();
    var InPriceTotal = new Array();
    var RemarkList = new Array();

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
            //RemarkList.push(escape(document.getElementById("txtTransferRemark_"+ck[i].value).value));
            RemarkList.push("");
            OutPriceTotal.push(document.getElementById("txtOutFeeSum_" + ck[i].value).value);
            //alert(document.getElementById("txtOutFeeSum_"+ck[i].value).value);
            OutCount.push(document.getElementById("txtOutCount_" + ck[i].value).value);
            if ($("#txtIsMoreUnit").val() == "1") {
                var exRate = parseFloat(document.getElementById("txtUsedUnit_" + ck[i].value).value.split('|')[1]);
                InCount.push(exRate * parseFloat(document.getElementById("txtInCount_" + ck[i].value).value));
            }
            else {
                InCount.push(document.getElementById("txtInCount_" + ck[i].value).value);
            }
            //alert(document.getElementById("txtInCount_"+ck[i].value).value);
            InPriceTotal.push(document.getElementById("txtInFeeSum_" + ck[i].value).value);
        }
    }
    para = para +
                "&SortNo=" + SortNo.toString() +
                "&ProductID=" + ProductID.toString() +
                "&ProductName=" + ProductName.toString() +
                "&ProductSpec=" + ProductSpec.toString() +
                "&ProductUnit=" + ProductUnit.toString() +
                "&TransferPrice=" + TransferPrice.toString() +
                "&TransferCount=" + TransferCount.toString() +
                "&TransferTotalPrice=" + TransferTotalPrice.toString() +
                "&RemarkList=" + RemarkList.toString() +
                "&OutPriceTotal=" + OutPriceTotal.toString() +
                "&OutCount=" + OutCount.toString() +
                "&InCount=" + InCount.toString() +
                "&InPriceTotal=" + InPriceTotal.toString();
    var url = "../../../Handler/Office/StorageManager/StorageTransferOutIn.ashx";
    // alert(para);
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
                popMsgObj.Show("确认入库|", "确认入库成功|");
                document.getElementById("ddlBusiStatus").value = "4";

                InitControl();
            }
            else
                popMsgObj.Show("确认入库|", "确认调拨入库失败|");


        },
        error: function() { popMsgObj.Show("确认入库|", "确认调拨入库失败|"); }
    });
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
    if (OutDeptID == "") {
        popMsgObj.Show("基本信息|", "请选择调货部门|");
        return;
    }
    if (OutStorageID == "-1") {
        popMsgObj.Show("基本信息|", "请选择调货仓库|");
        return;
    }
    var Para = "OutStorageID=" + OutStorageID + "&OutDeptID=" + OutDeptID + "&action=GETPUT";
    popProductInfoObj.ShowList(Para, rowid);
}

//填充明细
function fnSelectProductInfo(ID, ProductNo, ProductName, ProductSpec, ProductCount, ProdcutUnitName, ProductUnitID, TransferPrice, rowid) {
    document.getElementById("txtProductNo_" + rowid).value = ProductNo;
    document.getElementById("txtProductNo_" + rowid).title = ID;
    document.getElementById("txtProductName_" + rowid).value = ProductName;
    document.getElementById("txtProductSpec_" + rowid).value = ProductSpec;
    document.getElementById("txtProductUnit_" + rowid).value = ProdcutUnitName;
    document.getElementById("txtProductUnit_" + rowid).title = ProductUnitID;
    document.getElementById("txtTransferPrice_" + rowid).value = parseFloat(TransferPrice).toFixed(2);
    closeProductInfodiv();
}

//合计 包括明细
function _cDetail(rowid) {
    var objCount = document.getElementById("txtTransferCount_" + rowid);
    var objPrice = document.getElementById("txtTransferPrice_" + rowid);
    if (IsNumber(objCount.value) == null) {
        popMsgObj.Show("调拨明细|", "调拨数量必须为数字|");
        objCount.value = "";
        return;
    }
    document.getElementById("txtTransferTotalPrice_" + rowid).value = formatNumber(parseFloat(objCount.value) * parseFloat(objPrice.value), 2);
    _cTotal();
}

function _cTotal() {
    var ck = document.getElementsByName("chk");
    var TotalCount = 0;
    var TotalPrice = 0;
    for (var i = 0; i < ck.length; i++) {
        TotalCount = TotalCount + parseInt(document.getElementById("txtTransferCount_" + ck[i].value).value);
        TotalPrice = parseFloat(TotalPrice) + parseFloat(document.getElementById("txtTransferTotalPrice_" + ck[i].value).value);
    }
    document.getElementById("txtTotalCount").value = formatNumber(TotalCount, 2);
    document.getElementById("txtTotalPrice").value = formatNumber(TotalPrice, 2);
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
        }
    }
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
            document.getElementById("txtTransferFeeSum").value = parseFloat(data.TransferFeeSum).toFixed(2);
            document.getElementById("tboxCreator").value = data.CreatorName;
            document.getElementById("tboxCreateDate").value = data.CreateDate;
            document.getElementById("ddlBillStatus").value = data.BillStatus;
            document.getElementById("tboxRemark").value = data.Remark;
            document.getElementById("tboxConfirmor").value = data.ConfirmorName;
            document.getElementById("tboxConfirmorDate").value = data.ConfirmDate;
            document.getElementById("tboxCloser").value = data.CloserName;
            document.getElementById("tboxCloseDate").value = data.CloseDate;
            document.getElementById("tbxoModifiedUser").value = data.ModifiedUserID;
            document.getElementById("tboxModifiedDate").value = data.ModifiedDate;
            document.getElementById("txtOutFeeSum").value = parseFloat(data.TransferPrice).toFixed(2);
            document.getElementById("txtOutCount").value = parseFloat(data.TransferCount).toFixed(2);
            document.getElementById("UsertxtOutUserID").value = data.OutUserIDName;
            document.getElementById("txtOutDate").value = data.OutDate;
            if (document.getElementById("ddlBusiStatus").value == "3") {
                document.getElementById("txtInCount").value = parseFloat(data.OutCount).toFixed(2);
                document.getElementById("txtInFeeSum").value = parseFloat(data.OutFeeSum).toFixed(2);
            }
            else {
                document.getElementById("txtInCount").value = parseFloat(data.InCount).toFixed(2);
                document.getElementById("txtInFeeSum").value = parseFloat(data.InFeeSum).toFixed(2);
            }
            InitControl();
            getDetailInfo();

        },
        error: function() { popMsgObj.Show("保存|", "保存新建调拨单失败|"); }
    });
}


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
            var index = 1;
            $("#tblTransfer tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item != null && item != "") {

                    //                        $("<tr class='newrow' id=\"tr_Row_"+item.SortNo+"\"></tr>").append(
                    //                            "<td class=\"cell\" align=\"center\"><input type=\"hidden\" id=\"txtMinusIs\" value=\""+item.MinusIs+"\"/><input type=\"hidden\" id=\"txtUseCount\" value=\""+item.UseCount+"\"/><input type=\"checkbox\" id=\"chk_list_"+item.SortNo+"\" name=\"chk\" value=\""+item.SortNo+"\" onclick=\"subSelect();\" /></td>"+
                    //                            "<td  class=\"cell\" ><input type=\"text\" id=\"txtSortNo_"+item.SortNo+"\" class=\"tdinput tboxsize textAlign\" value=\""+item.SortNo+"\"  name=\"rownum\" disabled=\"true\"/></td>"+
                    //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductNo_"+item.SortNo+"\" disabled=\"true\" class=\"tdinput tboxsize textAlign\" value=\""+item.ProdNo+"\"    title=\""+item.ProductID+"\"/></td>"+
                    //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductName_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProductName + "\" disabled=\"true\" /></td>" +
                    //                                //批次
                    //                             "<td  class=\"cell\" ><select style=' width:90%;' id='BatchNo_SignItem_TD_Text_" + item.SortNo + "' /></td>" +
                    //                             "<td class=\"cell\" ><input type=\"text\" id=\"txtProductSpec_"+item.SortNo+"\" class=\"tdinput tboxsize textAlign\" value=\""+item.Specification+"\"  disabled=\"true\" /></td>"+
                    //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductUnit_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.UnitName + "\" title=\"" + item.UnitID + "\"   disabled=\"true\" /></td>" +
                    //                        //基本数量
                    //                               "<td  class=\"cell\" ><input type=\"text\" id=\"txtTranCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranCount).toFixed(2) + "\"   disabled=\"true\" /></td>" +
                    //                        //单位
                    //                               "<td  class=\"cell\" ><input type=\"text\" id=\"txtUsedUnitName_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.UsedUnitName + "\"   disabled=\"true\" /> <input type=\"hidden\" id=\"txtUsedUnitID_" + item.SortNo + "\"  value=\"" + item.UesdUnitID + "\" /> </td>" +
                    //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferPrice_"+item.SortNo+"\" class=\"tdinput tboxsize textAlign\" value=\""+parseFloat( item.TranPrice).toFixed(2)+"\"   disabled=\"true\" /></td>"+
                    //                             "<td class=\"cell\" ><input type=\"text\" id=\"txtTransferCount_"+item.SortNo+"\" class=\"tdinput tboxsize textAlign\" value=\""+parseFloat(item.TranCount).toFixed(2)+"\"   disabled=\"true\" /></td>"+
                    //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferTotalPrice_"+item.SortNo+"\" class=\"tdinput tboxsize textAlign\" value=\""+parseFloat(item.TranPriceTotal).toFixed(2)+"\"   disabled=\"true\"/></td>"+
                    //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtOutCount_"+item.SortNo+"\" class=\"tdinput tboxsize textAlign\" value=\""+ parseFloat(item.OutCount).toFixed(2)+"\"  disabled=\"true\" /></td>"+
                    //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtOutFeeSum_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.OutPriceTotal).toFixed(2) + "\"   disabled=\"true\"/></td>" +


                    var html = "<td class=\"cell\" align=\"center\"><input type=\"checkbox\" id=\"chk_list_" + item.SortNo + "\" name=\"chk\" value=\"" + item.SortNo + "\" onclick=\"subSelect();\" /></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtSortNo_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.SortNo + "\" disabled=\"true\"  name=\"rownum\"/></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductNo_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProdNo + "\"  onclick=\"getProdcutList(" + item.SortNo + ");\"  title=\"" + item.ProductID + "\"  disabled=\"true\" /></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductName_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProductName + "\" disabled=\"true\" /></td>";
                    html += "<td  class=\"cell\" ><select style=' width:90%;' id='BatchNo_SignItem_TD_Text_" + item.SortNo + "'   disabled=\"true\" /></td>";

                    html += "<td class=\"cell\" ><input type=\"text\" id=\"txtProductSpec_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.Specification + "\"   disabled=\"true\"/></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductUnit_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.UnitName + "\" title=\"" + item.UnitID + "\"    disabled=\"true\" /></td>";
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.TranCount + "\"  disabled=\"true\" /></td>";
                        html += "<td  class=\"cell\" id=\"tboxUsedUnit_listtd_" + item.SortNo + "\" ></td>";
                    }

                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        html += "<td  class=\"cell\" ><input type=\"hidden\" id=\"txtTransferPrice_" + item.SortNo + "\" value=\"" + parseFloat(item.TranPrice).toFixed(2) + "\" />";
                        html += "<input type=\"text\" id=\"txtUsedPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.UsedPrice == "" ? item.TranPrice : item.UsedPrice).toFixed(2) + "\"   disabled=\"true\" /></td>";
                    } else {
                        html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranPrice).toFixed(2) + "\"    disabled=\"true\" /></td>";
                    }
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        html += "<td class=\"cell\" ><input type=\"text\" id=\"txtUsedCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + (item.UsedUnitCount == "" ? parseFloat("0").toFixed(2) : parseFloat(item.UsedUnitCount).toFixed(2)) + "\"  onblur=\"_cDetail(" + item.SortNo + ")\"  onchange=\"Number_round(this,"+selPoint+")\"   disabled=\"true\"/></td>";
                    } else {
                        html += "<td class=\"cell\" ><input type=\"text\" id=\"txtTransferCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranCount).toFixed(2) + "\"  onblur=\"_cDetail(" + item.SortNo + ")\"  onchange=\"Number_round(this,"+selPoint+")\"/></td>";
                    }
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferTotalPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranPriceTotal).toFixed(2) + "\"    disabled=\"true\"/></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtOutCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + (parseFloat(item.ExRate) == parseFloat("0") ? item.OutCount : (parseFloat(parseFloat(item.OutCount) / parseFloat(item.ExRate)).toFixed(2))) + "\"      disabled=\"true\"/></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtOutFeeSum_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.OutPriceTotal).toFixed(2) + "\"   disabled=\"true\"/></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtInCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + getDefaultInCount(item.InCount, item.OutCount, item.ExRate) + "\"    onblur=\"cInPrice(" + getDefaultInCount(item.InCount, item.OutCount, item.ExRate) + "," + item.SortNo + ");\" onchange=\"Number_round(this,"+selPoint+")\"/></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtInFeeSum_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + getDefaultInPrice(item.InPriceTotal, item.OutPriceTotal) + "\"   disabled=\"true\"/></td>";


                    $("<tr class='newrow' id=\"tr_Row_" + item.SortNo + "\"></tr>").append(html).appendTo($("#tblTransfer tbody")); index++;

                    //"<td class=\"cell\" ><input type=\"text\" id=\"txtTransferRemark_"+item.SortNo+"\" class=\"tdinput tboxsize textAlign\" value=\""+item.Remark+"\"  /></td>"
                    //填充批次
                    GetBatchList(item.ProductID, "ddlOutStorageID", "BatchNo_SignItem_TD_Text_" + item.SortNo, document.getElementById("hidIsBatchNo").value == "1" ? true : false, item.BatchNo);
                    // index++;
                    if ($("#txtIsMoreUnit").val() == "1") {
                        GetUnitGroupSelect(item.ProductID, 'StockUnit', 'txtUsedUnit_' + item.SortNo, 'ChangeUnit(this,' + item.SortNo + ')', "tboxUsedUnit_listtd_" + item.SortNo, item.UsedUnitID);
                        //                        else
                        //                            GetUnitGroupSelectEx(item.ProductID, 'StockUnit', 'txtUsedUnit_' + item.SortNo, 'ChangeUnit(this,' + item.SortNo + ')', "tboxUsedUnit_listtd_" + item.SortNo, item.UsedUnitID, "ReCalByMoreUnit('" + item.SortNo + "','" + item.TranPrice + "')");
                    }

                }
            });
            document.getElementById("txtLastRowID").value = index;
            document.getElementById("txtLastSortNo").value = index;

        },
        error: function() { popMsgObj.Show("保存|", "保存数据失败|"); }
    });
}


function GetCountByExRate(count, exrate) {
    if ($("#txtIsMoreUnit").val() == "1" && exrate!="") {
        return parseFloat(parseFloat(count) / parseFloat(exrate)).toFixed(2);
    }
    else
        return count;
}




/*设置入库数量默认值*/
function getDefaultInCount(InCount,OutCount,ExRate) {
    if (InCount == "" || InCount == "0.0000" || InCount == "0" || InCount == "0.00" )
          return GetCountByExRate(OutCount, ExRate);
    else
        return parseFloat(InCount).toFixed(2);
      
}
/*设置默入出库金额*/
function getDefaultInPrice(InPriceTotal, OutPriceTotal) {
    if (InPriceTotal == "" || InPriceTotal == "0.0000" || InPriceTotal == "0")
        return parseFloat(OutPriceTotal).toFixed(2);
    else
        return parseFloat(InPriceTotal).toFixed(2);
}

//计算入库金额
function cInPrice(Incount, rowno) {
    if (document.getElementById("txtInCount_" + rowno).value == "") {
        popMsgObj.Show("调拨单明细|", "行号" + rowno + "：入库数量不能为空|");
    }

    if (IsNumOrFloat(document.getElementById("txtInCount_" + rowno).value, false)) {
        if (parseFloat(document.getElementById("txtOutCount_" + rowno).value) >= parseFloat(document.getElementById("txtInCount_" + rowno).value))
            document.getElementById("txtInFeeSum_" + rowno).value = (parseFloat(document.getElementById("txtInCount_" + rowno).value) * parseFloat(document.getElementById("txtTransferPrice_" + rowno).value)).toFixed(2);
        else {
            popMsgObj.Show("调拨单明细|", "行号" + rowno + "：入库数量必须小于出库数量|");
            document.getElementById("txtInCount_" + rowno).value = document.getElementById("txtOutCount_" + rowno).value;
            return;
        }
    }
    else {
        popMsgObj.Show("调拨单明细|", "行号" + rowno + "：入库数量必须大于0或者输入半角状态的数字|");
        document.getElementById("txtInCount_" + rowno).value = parseFloat(Incount).toFixed(2);
        return
    }


    cInTotal();

}

//合计入库数量及金额
function cInTotal() {
    var ck = document.getElementsByName("chk");
    var inTotalCount = 0;
    var inTotalPrice = 0;
    for (var i = 0; i < ck.length; i++) {
        inTotalCount = inTotalCount + parseFloat(document.getElementById("txtInCount_" + ck[i].value).value);
        inTotalPrice = inTotalPrice + parseFloat(document.getElementById("txtInFeeSum_" + ck[i].value).value);
    }
    document.getElementById("txtInCount").value = inTotalCount.toFixed(2);
    document.getElementById("txtInFeeSum").value = inTotalPrice.toFixed(2);
}

function backtolsit() {
    window.history.back(-1);
}

//计算出库金额
function cOutPrice(OutCount, rowNo) {
    if (IsNumber(OutCount) == null) {
        popMsgObj.Show("出库数量|", "出库数量必须为数字|");
        return;
    }

    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].value == rowNo) {
            document.getElementById("txtOutFeeSum_" + rowNo).value = (parseFloat(OutCount) * parseFloat(document.getElementById("txtTransferPrice_" + rowNo).value)).toFixed(2);
        }
    }
    cOutTotal();

}
function cOutTotal() {
    var ck = document.getElementsByName("chk");
    var TotalCount = 0;
    var TotalPrice = 0;
    for (var i = 0; i < ck.length; i++) {
        TotalCount = TotalCount + parseInt(document.getElementById("txtOutCount_" + ck[i].value).value);
        TotalPrice = parseFloat(TotalPrice) + parseFloat(document.getElementById("txtOutFeeSum_" + ck[i].value).value);
    }
    document.getElementById("txtOutFeeSum").value = TotalPrice.toFixed(2);
    document.getElementById("txtOutCount").value = TotalCount.toFixed(2);
}