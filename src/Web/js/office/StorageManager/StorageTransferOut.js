
window.onload = function() { Init(); }

//初始化页面信息
function Init() {
    var action = document.getElementById("txtAction").value;
    var divTitle = document.getElementById("divTitle");

    divTitle.innerHTML = "调拨出库";
    document.getElementById("imgBack").style.display = "block";
    document.getElementById("div_InNo_uc").style.display = "none";
    document.getElementById("div_InNo_Lable").style.display = "block";
    getBaseInfo();


}

function InitControl() {
    if (document.getElementById("ddlBusiStatus").value != "2") {
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
    if (document.getElementById("txtOutUserID").value == "") {
        popMsgObj.Show("基本信息|", "请选择出库人|");
        return;
    }
    if (document.getElementById("txtOOutDate").value == "") {
        popMsgObj.Show("基本信息|", "请选择出库日期|");
        return;
    }
    if (document.getElementById("txtOOutDate").value > document.getElementById("txtRequireInDate").value) {
        popMsgObj.Show("基本信息|", "出库日期应该在要求到货日期之前|");
        return;
    }
    Edit();

}


//更新
function Edit() {

    var ProductID = new Array();
    var StorageID = new Array();
    var BatchNo = new Array();
    var chk = document.getElementsByName("chk");
    for (var i = 0; i < chk.length; i++) {
        ProductID.push($("#txtProductNo_" + chk[i].value).attr("title"));
        StorageID.push($("#ddlOutStorageID").val());
        BatchNo.push($("#BatchNo_SignItem_TD_Text_" + chk[i].value).val());
    }


    var url = "../../../Handler/Office/StorageManager/StroageBorrowSave.ashx";
    var para = "action=checkcount" +
                     "&ProductID=" + ProductID.toString() +
                     "&StorageID=" + StorageID.toString() +
                     "&BatchNo=" + BatchNo.toString();
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //数据格式:JSON
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            var data = msg.split('^');
            var textMsg = "";
            var titleMsg = "";
            for (var i = 0; i < chk.length; i++) {
                var index = chk[i].value;
                var info = data[i].split('|');
                var nowCount = parseFloat(info[1]);
                var Pid = info[0];
                var minuIs = (info[2] == "0" ? false : true);
                var detailCount = parseFloat($("#txtTransferCount_" + index).val());
                if ($("#txtIsMoreUnit").val() == "1") {
                    var exRateTmp = $("#txtUsedUnit_" + index).val();
                    var exRate = exRateTmp.split('|')[1];
                    nowCount = parseFloat(nowCount / exRate);
                    detailCount = parseFloat($("#txtUsedCount_" + index).val());
                }

                if (nowCount < detailCount) {
                    var pName = $("#txtProductName_" + index).val();
                    if (minuIs) {

                        if (!confirm("明细物品（" + pName + "）的当前库存数量（" + parseFloat(nowCount).toFixed(2) + "）小于调拨数量（" + parseFloat(detailCount).toFixed(2) + "）,如果继续将导致负库存，是否继续？ ")) {
                            continue;
                        }
                    }
                    else {
                        titleMsg += "确认单据|";
                        textMsg += "明细物品（" + pName + "）当前库存数量（" + parseFloat(nowCount).toFixed(2) + "）小于调拨数量（" + parseFloat(detailCount).toFixed(2) + "）同时该物品仓库设置不允许出现负库存,请修改后重新提交";
                    }
                }

            }
            if (textMsg != "") {
                popMsgObj.Show(titleMsg, textMsg);
                return;
            }
            else {
                if (!confirm("是否确认出库？"))
                    return;
                var para = "TransferID=" + document.getElementById("txtTransferID").value +
                          "&TransferNo=" + document.getElementById("txtNo").value +
                          "&OutDate=" + document.getElementById("txtOOutDate").value +
                          "&OutUserID=" + document.getElementById("txtOutUserID").value +
                          "&action=SAVE" +
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
                         "&TransferFeeSum=" + document.getElementById("txtTransferFeeSum").value;

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
                var RemarkList = new Array();
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
                        //RemarkList.push(escape(document.getElementById("txtTransferRemark_"+ck[i].value).value));
                        RemarkList.push("");
                        OutPriceTotal.push(document.getElementById("txtOutFeeSum_" + ck[i].value).value);
                        //   OutCount.push(document.getElementById("txtOutCount_" + ck[i].value).value);


                        if ($("#txtIsMoreUnit").val() == "1") {
                            var exRate = parseFloat(document.getElementById("txtUsedUnit_" + ck[i].value).value.split('|')[1]);
                            OutCount.push(exRate * parseFloat(document.getElementById("txtOutCount_" + ck[i].value).value));
                        }
                        else {
                            OutCount.push(document.getElementById("txtOutCount_" + ck[i].value).value);
                        }
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
                "&OutCount=" + OutCount.toString();
                var url = "../../../Handler/Office/StorageManager/StorageTransferOutIn.ashx";

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
                            popMsgObj.Show("确认出库|", "确认出库成功|");
                            document.getElementById("ddlBusiStatus").value = "3";
                            InitControl();
                        }
                        else
                            popMsgObj.Show("确认出库|", "确认出库失败|");


                    },
                    error: function() { popMsgObj.Show("确认出库|", "确认出库失败|"); }
                });


            }


        },
        error: function() { popMsgObj.Show("确认单据|", "确认单据失败|"); }
    });











    //          var ck=document.getElementsByName("chk");
    //          for(var i=0;i<ck.length;i++)
    //          {
    //                var objOutCount=document.getElementById("txtOutCount_"+ck[i].value).value;
    //                var objUseCount=document.getElementById("txtUseCount_"+ck[i].value).value;
    //                var Flag=document.getElementById("txtMinusIs_"+ck[i].value).value;
    //                
    ////              alert("FLAG:"+Flag+"\r\n objOutCount:"+objOutCount+"\r\n"+" objUseCount:"+objUseCount);
    ////            return;
    //                
    //                if(Flag=="0")
    //                {
    //                    if(parseFloat(objOutCount)>parseInt( objUseCount))
    //                    {
    //                        popMsgObj.Show("调拨单明细|","当前库存小于（"+parseFloat( objUseCount).toFixed(2)+"）调拨数量同时该物品仓库设置不允许出现负库存,请修改后重新提交|");
    //                        return;
    //                    }
    //                }
    //                else if(Flag=="1")
    //                {
    //                    if(parseFloat(objOutCount)>parseFloat( objUseCount))
    //                    {
    //                        if(!confirm("明细行号"+ck[i].value+" ：当前库存可用数量("+parseFloat(objUseCount).toFixed(2)+")小于调拨数量，如果继续将导致负库存，是否继续？"))
    //                        {
    //                            return;
    //                        }
    //                    }
    //                }
    //                
    //          }

    //return;


}

//添加行
function AddRow() {
    var tbl = document.getElementById("tblTransfer");
    var LastRowID = document.getElementById("txtLastRowID");
    var LastSortNo = document.getElementById("txtLastSortNo");
    var RowID = parseInt(LastRowID.value);
    //添加行
    var row = tbl.insertRow();
    row.id = "tr_Row_" + RowID;

    //添加checkbox列
    var cellCheck = row.insertCell(0);
    cellCheck.className = "cell";
    cellCheck.align = "center";
    cellCheck.innerHTML = "<input type=\"checkbox\" id=\"chk_list_" + RowID + "\" name=\"chk\" value=\"" + RowID + "\" onclick=\"subSelect();\" />";

    //序号
    var cellNo = row.insertCell(1);
    cellNo.className = "cell";
    cellNo.innerHTML = "<input type=\"text\" id=\"txtSortNo_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseInt(LastSortNo.value) + "\"  name=\"rownum\" disabled=\"true\"/>";

    //物品编号
    var cellProductNo = row.insertCell(2);
    cellProductNo.className = "cell";
    cellProductNo.innerHTML = "<input type=\"text\" id=\"txtProductNo_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  onclick=\"getProdcutList(" + RowID + ");\" disabled=\"true\"  />";

    //物品名称
    var cellProductName = row.insertCell(3);
    cellProductName.className = "cell";
    cellProductName.innerHTML = "<input type=\"text\" id=\"txtProductName_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\" disabled=\"true\" />";

    //批次
    var cellProductName = row.insertCell(i);
    cellProductName.className = "cell";
    cellProductName.innerHTML = "<select style=' width:90%;' id='BatchNo_SignItem_TD_Text_" + RowID + "' disabled=\"true\"  />";
    i++;


    //规格
    var cellProdcutSpec = row.insertCell(4);
    cellProdcutSpec.className = "cell";
    cellProdcutSpec.innerHTML = "<input type=\"text\" id=\"txtProductSpec_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  disabled=\"true\" />";

    //单位
    var cellProductUnit = row.insertCell(5);
    cellProductUnit.className = "cell";
    cellProductUnit.innerHTML = "<input type=\"text\" id=\"txtProductUnit_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\" />";

    //调拨单价
    var cellTransferPrice = row.insertCell(6);
    cellTransferPrice.className = "cell";
    cellTransferPrice.innerHTML = "<input type=\"text\" id=\"txtTransferPrice_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\" />";

    //调拨数量
    var cellTransferCount = row.insertCell(7);
    cellTransferCount.className = "cell";
    cellTransferCount.innerHTML = "<input type=\"text\" id=\"txtTransferCount_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  onblur=\"_cDetail(" + RowID + ")\" />";

    //调拨金额
    var cellTransferTotalPrice = row.insertCell(8);
    cellTransferTotalPrice.className = "cell";
    cellTransferTotalPrice.innerHTML = "<input type=\"text\" id=\"txtTransferTotalPrice_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\"/>";

    //       //备注
    //       var cellTransferRemark=row.insertCell(9);
    //       cellTransferRemark.className="cell";
    //       cellTransferRemark.innerHTML="<input type=\"text\" id=\"txtTransferRemark_"+RowID+"\" class=\"tdinput tboxsize textAlign\" value=\"\"  />";

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
            if (document.getElementById("ddlBusiStatus").value == "2") {
                document.getElementById("txtOutFeeSum").value = parseFloat(data.TransferPrice).toFixed(2);
                document.getElementById("txtOutCount").value = parseFloat(data.TransferCount).toFixed(2);
            }
            else {
                document.getElementById("txtOutFeeSum").value = parseFloat(data.OutFeeSum).toFixed(2);
                document.getElementById("txtOutCount").value = parseFloat(data.OutCount).toFixed(2);
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
                //                  if (item != null && item != "") {
                //                          // alert(item.OutCount);
                //                          $("<tr class='newrow' id=\"tr_Row_" + item.SortNo + "\"></tr>").append(
                //                            "<td class=\"cell\" align=\"center\"><input type=\"hidden\" id=\"txtMinusIs_" + item.SortNo + "\" value=\"" + item.MinusIs + "\"/><input type=\"hidden\" id=\"txtUseCount_" + item.SortNo + "\" value=\"" + item.UseCount + "\"/><input type=\"checkbox\" id=\"chk_list_" + item.SortNo + "\" name=\"chk\" value=\"" + item.SortNo + "\" onclick=\"subSelect();\" /></td>" +
                //                            "<td  class=\"cell\" ><input type=\"text\" id=\"txtSortNo_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.SortNo + "\"  name=\"rownum\" disabled=\"true\"/></td>" +
                //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductNo_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProdNo + "\"    title=\"" + item.ProductID + "\" disabled=\"true\"/></td>" +
                //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductName_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProductName + "\" disabled=\"true\" /></td>" +
                //                          //批次
                //                             "<td  class=\"cell\" ><select style=' width:90%;' id='BatchNo_SignItem_TD_Text_" + item.SortNo + "'  /></td>" +
                //                             "<td class=\"cell\" ><input type=\"text\" id=\"txtProductSpec_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.Specification + "\"  disabled=\"true\" /></td>" +
                //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductUnit_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.UnitName + "\" title=\"" + item.UnitID + "\"   disabled=\"true\" /></td>" +
                //                                //基本数量
                //                               "<td  class=\"cell\" ><input type=\"text\" id=\"txtTranCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranCount).toFixed(2) + "\"   disabled=\"true\" /></td>" +
                //                          //单位
                //                               "<td  class=\"cell\" ><input type=\"text\" id=\"txtUsedUnitName_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.UsedUnitName + "\"   disabled=\"true\" /> <input type=\"hidden\" id=\"txtUsedUnitID_" + item.SortNo + "\"  value=\"" + item.UesdUnitID + "\" /> </td>" +
                //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranPrice).toFixed(2) + "\"   disabled=\"true\" /></td>" +
                //                             "<td class=\"cell\" ><input type=\"text\" id=\"txtTransferCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranCount).toFixed(2) + "\"   disabled=\"true\" /></td>" +
                //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferTotalPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranPriceTotal).toFixed(2) + "\"   disabled=\"true\"/></td>" +
                //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtOutCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + getDefaultOutCount(item) + "\"   onblur=\"cOutPrice(" + getDefaultOutCount(item) + "," + item.SortNo + ")\" onchange=\"Number_round(this,2)\"/></td>" +
                //                             "<td  class=\"cell\" ><input type=\"text\" id=\"txtOutFeeSum_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + getDefaultOutPrice(item) + "\"   disabled=\"true\"/></td>").appendTo($("#tblTransfer tbody"));

                //                          // "<td class=\"cell\" ><input type=\"text\" id=\"txtTransferRemark_"+item.SortNo+"\" class=\"tdinput tboxsize textAlign\" value=\""+item.Remark+"\"  /></td>"

                //                          //填充批次
                //                          GetBatchList(item.ProductID, "ddlOutStorageID", "BatchNo_SignItem_TD_Text_" + item.SortNo, document.getElementById("hidIsBatchNo").value == "1" ? true : false);
                //                        
                //                          index++;
                //                      } 
                //                  });
                //                  document.getElementById("txtLastRowID").value = index;
                //                  document.getElementById("txtLastSortNo").value = index;
                //                  
                //                  




                if (item != null && item != "") {
                    var html = "<td class=\"cell\" align=\"center\"><input type=\"checkbox\" id=\"chk_list_" + item.SortNo + "\" name=\"chk\" value=\"" + item.SortNo + "\" onclick=\"subSelect();\" /></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtSortNo_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.SortNo + "\" disabled=\"true\"  name=\"rownum\"/></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductNo_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProdNo + "\"  onclick=\"getProdcutList(" + item.SortNo + ");\"  title=\"" + item.ProductID + "\"  disabled=\"true\" /></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtProductName_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProductName + "\" disabled=\"true\" /></td>";
                    html += "<td  class=\"cell\" ><select style=' width:90%;' id='BatchNo_SignItem_TD_Text_" + item.SortNo + "' /></td>";

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
                        html += "<input type=\"text\" id=\"txtUsedPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" +parseFloat(item.UsedPrice == "" ? item.TranPrice : item.UsedPrice ).toFixed(2) + "\"   disabled=\"true\" /></td>";
                    } else {
                        html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranPrice).toFixed(2) + "\"    disabled=\"true\" /></td>";
                    }
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        html += "<td class=\"cell\" ><input type=\"text\" id=\"txtUsedCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + (item.UsedUnitCount == "" ? parseFloat( "0").toFixed(2) : parseFloat(item.UsedUnitCount).toFixed(2)) + "\"  onblur=\"_cDetail(" + item.SortNo + ")\"  onchange=\"Number_round(this,2)\"   disabled=\"true\"/></td>";
                    } else {
                        html += "<td class=\"cell\" ><input type=\"text\" id=\"txtTransferCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranCount).toFixed(2) + "\"  onblur=\"_cDetail(" + item.SortNo + ")\"  onchange=\"Number_round(this,2)\"   disabled=\"true\"/></td>";
                    }
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtTransferTotalPrice_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.TranPriceTotal).toFixed(2) + "\"    disabled=\"true\"/></td>";

                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtOutCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + getDefaultOutCount(item.OutCount, item.UsedUnitCount, item.TranCount) + "\"   onblur=\"cOutPrice(" + getDefaultOutCount(item.OutCount, item.UsedUnitCount, item.TranCount) + "," + item.SortNo + ")\" onchange=\"Number_round(this,2)\"/></td>";
                    html += "<td  class=\"cell\" ><input type=\"text\" id=\"txtOutFeeSum_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + getDefaultOutPrice(item.OutPriceTotal, item.TranPriceTotal) + "\"   disabled=\"true\"/></td>";


                    $("<tr class='newrow' id=\"tr_Row_" + item.SortNo + "\"></tr>").append(html).appendTo($("#tblTransfer tbody")); index++;


                    //填充批次
                    GetBatchList(item.ProductID, "ddlOutStorageID", "BatchNo_SignItem_TD_Text_" + item.SortNo, document.getElementById("hidIsBatchNo").value == "1" ? true : false, item.BatchNo);

                    if ($("#txtIsMoreUnit").val() == "1") {
                        //  if (item.ExRate != null && item.ExRate != "")
                        GetUnitGroupSelect(item.ProductID, 'StockUnit', 'txtUsedUnit_' + item.SortNo, 'ChangeUnit(this,' + item.SortNo + ')', "tboxUsedUnit_listtd_" + item.SortNo, item.UsedUnitID);
                        //                        else
                        //                            GetUnitGroupSelectEx(item.ProductID, 'StockUnit', 'txtUsedUnit_' + item.SortNo, 'ChangeUnit(this,' + item.SortNo + ')', "tboxUsedUnit_listtd_" + item.SortNo, item.UsedUnitID, "ReCalByMoreUnit('" + item.SortNo + "','" + item.TranPrice + "')");
                    }
                    // document.getElementById("BatchNo_SignItem_TD_Text_" + item.SortNo).value = item.BatchNo;
                }
            });
            document.getElementById("txtLastRowID").value = index;
            document.getElementById("txtLastSortNo").value = index;


        },
        error: function() { popMsgObj.Show("保存|", "读取数据失败|"); }
    });
}

/*设置出库数量默认值*/
function getDefaultOutCount(OutCount, UsedUnitCount, TranCount) {
    if (OutCount == "" || OutCount == "0.0000" || OutCount == "0" || OutCount == "0.00") {
        if ($("#txtIsMoreUnit").val() == "1") {
            return parseFloat(UsedUnitCount==""?TranCount:UsedUnitCount).toFixed(2);
        }
        else {
            return parseFloat(TranCount).toFixed(2);
        }

    }
    else
        return parseFloat(OutCount).toFixed(2);
}
/*设置默认出库金额*/
function getDefaultOutPrice(OutPriceTotal, TranPriceTotal) {
    if (OutPriceTotal == "" || OutPriceTotal == "0.0000" || OutPriceTotal == "0")
        return parseFloat(TranPriceTotal).toFixed(2);
    else
        return parseFloat(OutPriceTotal).toFixed(2);
}

function backtolsit() {
    window.history.back(-1);
}

//计算出库金额
function cOutPrice(OutCount, rowNo) {

    if (!IsNumOrFloat(document.getElementById("txtOutCount_" + rowNo).value, false)) {
        popMsgObj.Show("调拨单明细|", "出库数量必须为数值|");
        document.getElementById("txtOutCount_" + rowNo).value = parseFloat(OutCount).toFixed(2);
        return;
    }

    if (parseFloat(document.getElementById("txtOutCount_" + rowNo).value) > parseFloat(document.getElementById("txtTransferCount_" + rowNo).value)) {
        popMsgObj.Show("调拨单明细|", "出库数量必须小于调拨数量|");
        document.getElementById("txtOutCount_" + rowNo).value = parseFloat(OutCount).toFixed(2);
        return;
    }

    /*计算调拨价格*/
    //  document.getElementById("txtOutFeeSum_"+rowNo).value=parseFloat(parseFloat( document.getElementById("txtOutCount_"+rowNo).value)*parseFloat(document.getElementById("txtTransferPrice_"+rowNo).value)).toFixed(2);

    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].value == rowNo) {
            document.getElementById("txtOutFeeSum_" + rowNo).value = (parseFloat(document.getElementById("txtOutCount_" + rowNo).value) * parseFloat(document.getElementById("txtTransferPrice_" + rowNo).value)).toFixed(2);
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