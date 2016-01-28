/************************************************************************
* 作    者： 朱贤兆
* 创建日期： 2010.04.01
* 描    述： 借货返还单
* 修改日期： 2010.04.01
* 版    本： 0.1.0                                                                     
************************************************************************/



//读取借货单列表
function getStorageBorrowList() {
    popProductInfoObj.ShowList();
}

/*重写toFixed*/
Number.prototype.toFixed = function(d) {
    return FormatAfterDotNumber(this, parseInt($("#hidSelPoint").val()));
}

/*
*	填充借货单页面
*/
function selectedStorageBorrow(ID, BorrowNo, DeptIDText, BorrowerText, BorrowDate, OutDeptIDText, StorageID, StorageIDText, TotalCount, TotalPrice) {
    document.getElementById("txtBorrowNo").value = BorrowNo;
    document.getElementById("txtBorrowNo").title = ID;
    document.getElementById("txtBorrowDept").value = DeptIDText;
    document.getElementById("txtBorrower").value = BorrowerText;
    document.getElementById("txtBorrowDate").value = (BorrowDate == "1753-01-01" ? "" : BorrowDate);
    document.getElementById("txtOutDept").value = OutDeptIDText;
    document.getElementById("ddlStorage").value = StorageID;
    closeProductInfodiv();
    getStorageBorrowDetail(BorrowNo);
}

//计算明细价格
function _c(count, price) {
    return parseFloat(parseFloat(count) * parseFloat(price)).toFixed(2);
}

//计算明细返还的总价格与数量
function _cTotal() {
    var count = 0;
    var price = 0;
    var chk = document.getElementsByName("chkDetail");
      //  alert( chk.length);
    for (var i = 0; i < chk.length; i++) {
        //计算时排除隐藏的行
        if (document.getElementById("tr_list_" + chk[i].value).style.display != "none") {
            //计量单位开启
            if ($("#txtIsMoreUnit").val() == "1") {
                price += parseFloat(document.getElementById("txtUsedReturnCount_" + chk[i].value).value) * parseFloat(document.getElementById("txtUsedUnitPrice_" + chk[i].value).value);
                count += parseInt(document.getElementById("txtUsedReturnCount_" + chk[i].value).value);
            } else {
                price += parseFloat(document.getElementById("txtReturnCount_" + chk[i].value).value) * parseFloat(document.getElementById("hidUnitPrice_" + chk[i].value).value);
                count += parseInt(document.getElementById("txtReturnCount_" + chk[i].value).value);
            }

        }
    }
    document.getElementById("txtTotalCount").value = count.toFixed(2);
    document.getElementById("txtTotalPrice").value = price.toFixed(2);

}

//修改返还数量 验证和计算
function checkc(index) {
    var objPrice = document.getElementById("hidUnitPrice_" + index);
    var objCount = document.getElementById("txtReturnCount_" + index);
    if (IsNumOrFloat(objCount.value)) {
        popMsgObj.Show("借货单明细|", "实还数量应为数字|");
        objCount.value = "0";
        return;
    }
    else {
        if (parseFloat(document.getElementById("txtMustReturnCount_" + index).value) < parseFloat(objCount.value)) {
            popMsgObj.Show("借货单明细|", "实还数量不能大于应还数量|");
            return;
        }
    }

    //重新计算 单条明细归还数量和归还金额 及 总数量与总金额
    _cTotal();
    document.getElementById("txtReturnTotalPrice_" + index).value = parseFloat(_c(objCount.value, objPrice.value)).toFixed(2);

}


//读取借货单明细
function getStorageBorrowDetail(borrowno) {
    var para = "BorrowNo=" + borrowno;
    var url = "../../../Handler/Office/StorageManager/StorageBorrowDetailForReturn.ashx";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            var index = 1;
            $("#tblBorrowDetailList tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {

                    AddDetailRow(item, index);
                    index++;
                }

                //计算返还金额与数量
                _cTotal();
            });
        },
        error: function() { popMsgObj.Show("借货单明细|", "载入明细失败|"); }
    });
}

/****************************************************************
* 添加明细行
****************************************************************/
function AddDetailRow(data, rowcount) {
    var detailArr = new Array();
    detailArr.push({ type: "chk", id: "chkDetail", controlshow: "", event: "onclick='unSelAll()'", val: rowcount });
    detailArr.push({ type: "hid", id: "DetailID", controlshow: "", event: "", val: data.ID });
    detailArr.push({ type: "txt", id: "DetailSortNo", controlshow: "disabled", event: "", val: rowcount });
    detailArr.push({ type: "txt", id: "ProductNo", controlshow: "disabled", event: "", val: data.ProductNo }); 
    detailArr.push({ type: "txt", id: "ProcutName", controlshow: "disabled", event: "", val: data.ProductName });
    
    detailArr.push({ type: "txt", id: "BatchNo", controlshow: "disabled", event: "", val: data.BatchNo });
    detailArr.push({ type: "txt", id: "Spec", controlshow: "disabled", event: "", val: data.ProductSpec });
    detailArr.push({ type: "txt", id: "Unit", controlshow: "disabled", event: "", val: data.ProductUnit });
    detailArr.push({ type: "hid", id: "ProductID", controlshow: "disabled", event: "", val: data.ProductID });
    detailArr.push({ type: "hid", id: "UnitID", controlshow: "disabled", event: "", val: data.ProductUnitID });
    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        //基本数量
        detailArr.push({ type: "txt", id: "ReturnCount", controlshow: "", event: "", val: parseFloat(data.ProductCount).toFixed(2) });
        //单位
        detailArr.push({ type: "", id: "UsedUnit", controlshow: "", event: "", val: "" });
    }
    if ($("#txtIsMoreUnit").val() == "1" && data.ExRate!="" && parseFloat(data.ExRate)!=parseFloat("0")) {
    
         detailArr.push({ type: "txt", id: "MustReturnCount", controlshow: "disabled", event: "", val: parseFloat(parseFloat(data.ProductCount).toFixed(2)/parseFloat(data.ExRate).toFixed(2)).toFixed(2) });
        detailArr.push({ type: "txt", id: "RealReturnCount", controlshow: "disabled", event: "", val: parseFloat(parseFloat( data.RealReturnCount).toFixed(2)/parseFloat(data.ExRate).toFixed(2)).toFixed(2) });

    }
    else 
    {
        detailArr.push({ type: "txt", id: "MustReturnCount", controlshow: "disabled", event: "", val: parseFloat(data.ProductCount).toFixed(2) });
        detailArr.push({ type: "txt", id: "RealReturnCount", controlshow: "disabled", event: "", val: parseFloat(data.RealReturnCount).toFixed(2) });
    }

 
    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        detailArr.push({ type: "txt", id: "UsedReturnCount", controlshow: "", event: " onblur='cCheckReturnCount(" + rowcount + "," + getMustReturnCount(data) + ");'  onchange='Number_round(this,"+selPoint+")' ", val: parseFloat(data.UsedUnitCount).toFixed(2) });
    }
    else {
        detailArr.push({ type: "txt", id: "ReturnCount", controlshow: "", event: " onblur='cCheckReturnCount(" + rowcount + "," + getMustReturnCount(data) + ");'  onchange='Number_round(this,"+selPoint+")' ", val: getMustReturnCount(data) });
    }
    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        detailArr.push({ type: "hid", id: "UnitPrice", controlshow: "", event: "", val: parseFloat(data.UnitPrice).toFixed(2) });

        if (data.UsedPrice) {
            detailArr.push({ type: "txt", id: "UsedUnitPrice", controlshow: "", event: " onblur='cCheckPrice(" + rowcount + "," + data.UnitPrice + ");' onchange='Number_round(this,"+selPoint+")' ", val: parseFloat(data.UsedPrice).toFixed(2) });
        } else {
            detailArr.push({ type: "txt", id: "UsedUnitPrice", controlshow: "", event: " onblur='cCheckPrice(" + rowcount + "," + data.UnitPrice + ");'  onchange='Number_round(this,"+selPoint+")' ", val: parseFloat(data.UnitPrice).toFixed(2) });
        }
    }
    else {
        detailArr.push({ type: "txt", id: "UnitPrice", controlshow: "", event: " onblur='cCheckPrice(" + rowcount + "," + data.UnitPrice + ");' onchange='Number_round(this,"+selPoint+")' ", val: parseFloat(data.UnitPrice).toFixed(2) });
    }
    detailArr.push({ type: "txt", id: "ReturnTotalPrice", controlshow: "disabled", event: "", val: cDetailCost(data) });
    detailArr.push({ type: "txt", id: "Remark", controlshow: "", event: "", val: "" });
    detailArr.push({ type: "txt", id: "BorrowNo", controlshow: "disabled", event: "", val: data.BorrowNo });
    detailArr.push({ type: "txt", id: "SortNo", controlshow: "disabled", event: "", val: data.SortNo });

    var tab = { tabId: "tblBorrowDetailList", rowId: rowcount, tdDetail: detailArr };
    GBLAddAddDetailRow(tab);

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        //填充单位下拉列表
        GetUnitGroupSelectEx(data.ProductID, 'StockUnit', 'ddlUsedUnit_' + rowcount, 'ChangeUnit(this,' + rowcount + ')', "listUsedUnit_" + rowcount, data.UsedUnitID, "$('#ddlUsedUnit_" + rowcount + "').attr('disabled','disabled')");
    }
}



/*获取实还数量*/
function getMustReturnCount(item) {
    var ProductCount = parseFloat(item.ProductCount);
    var RealReturnCount = parseFloat(item.RealReturnCount);

    return parseFloat(ProductCount - RealReturnCount).toFixed(2);
}


/*计算明细价格*/
function cDetailCost(item) {
    var ProductCount = parseFloat(item.ProductCount);
    var RealReturnCount = parseFloat(item.RealReturnCount);

    var MustCount = parseFloat(ProductCount - RealReturnCount).toFixed(2);
    return parseFloat(parseFloat(item.UnitPrice).toFixed(2) * MustCount).toFixed(2);
}

/*验证实还数量并重新计算明细价格*/
function cCheckReturnCount(rowid, MustReturnCount) {
    var objValue=0;
    
       //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        
        objValue=document.getElementById("txtUsedReturnCount_" + rowid).value;
       
    }
    else {
    objValue=document.getElementById("ReturnCount" + rowid).value;
    }
    
    
      var realReturnCount =document.getElementById("txtRealReturnCount_" + rowid).value;
 

    /*验证返还数量*/
    if (parseFloat(objValue) > (parseFloat(MustReturnCount) + parseFloat(realReturnCount))) {
        popMsgObj.Show("借货返还单明细|", "实还数量不能大于剩余应还数量|");
        document.getElementById("txtUsedReturnCount_" + rowid).value = MustReturnCount.toFixed(2);
        return;
    }


    if (!IsNumOrFloat(objValue)) {
        popMsgObj.Show("借货返还单明细|", "实还数量必须是有效的数值|");
        document.getElementById("txtUsedReturnCount_" + rowid).value = MustReturnCount.toFixed(2);
        return;
    }
    document.getElementById("txtReturnTotalPrice_" + rowid).value = parseFloat(parseFloat(objValue) * parseFloat(document.getElementById("txtUsedUnitPrice_" + rowid).value)).toFixed(2);
    CalCulateNum('ddlUsedUnit_' + rowid, "txtUsedReturnCount_" + rowid, "txtReturnCount_" + rowid, "txtUsedUnitPrice_" + rowid, "hidUnitPrice_" + rowid, $("#hidSelPoint").val());
    _cTotal();

}

/*验证单价 并计算明细价格*/
function cCheckPrice(rowid, Price) {
    var objValue = document.getElementById("txtUsedUnitPrice_" + rowid).value;
    if (!IsNumOrFloat(objValue)) {
        popMsgObj.Show("借货返还单明细|", "返还单价必须是有效的数值|");
        document.getElementById("txtUsedUnitPrice_" + rowid).value = Price.toFixed(2);
        return;
    }
    document.getElementById("txtReturnTotalPrice_" + rowid).value = parseFloat(parseFloat(objValue) * parseFloat(document.getElementById("txtUsedReturnCount_" + rowid).value)).toFixed(2);
    _cTotal();
}




//获取选中行的index
function getCheckedList() {
    var IDList = new Array();
    var chk = document.getElementsByName("chkDetail");
    if (chk.length <= 0) {
        return "";
    }
    var Flag = false;
    for (var i = 0; i < chk.length; i++) {
        if (chk[i].checked) {
            Flag = true;
            IDList.push(chk[i].value);
        }
    }
    if (Flag)
        return IDList;
    else
        return "";
}

//删除明细   
function DelRow() {
    var IDList = getCheckedList();
    if (IDList == "") {
        popMsgObj.Show("删除明细|", "请选择需要删除的明细|");
        return;
    }
    if (!confirm("确认删除选中明细？")) {
        return;
    }
    for (var i = 0; i < IDList.length; i++) {
        document.getElementById("tr_list_" + IDList[i]).style.display = "none";
        document.getElementById("chkReturnDetail").checked = false;
        //重新排序
        ResortRow();
    }
    //删除行重新计算返还数量与价格
    _cTotal();
}

//行重新排序
function ResortRow() {
    var chk = document.getElementsByName("chkDetail");
    var index = 1;
    for (var i = 0; i < chk.length; i++) {
        if (document.getElementById("tr_list_" + chk[i].value).style.display != "none") {
            document.getElementById("txtDetailSortNo_" + chk[i].value).value = index;
            index++;
        }
    }

}

//验证表单
function ValidateInfo() {
    var objTitle = "";
    var objMsg = "";
    var Flag = true;

    //获取编码规则下拉列表选中项
    var codeRule = document.getElementById("txtRuleCodeNo_ddlCodeRule").value;
    var action = document.getElementById("action").value;
    //如果选中的是 手工输入时，校验编号是否输入
    if (codeRule == "" && action != "EDIT") {
        //获取输入的编号
        var txtPlanNo = document.getElementById("txtRuleCodeNo_txtCode").value;
        //编号必须输入
        if (txtPlanNo == "") {
            Flag = false;
            objTitle = objTitle + "单据编号|";
            objMsg = objMsg + "请输入单据编号|";
        }
        else {
            if (!CodeCheck(txtPlanNo)) {
                Flag = false;
                objTitle = objTitle + "单据编号|";
                objMsg = objMsg + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
    }

    if (document.getElementById("txtBorrowNo").value == "") {
        objTitle = objTitle + "借货单|";
        objMsg = objMsg + "请选择借货单|";
        Flag = false;
    }
    if (document.getElementById("txtReturnerID").value == "") {
        objTitle = objTitle + "返还人|";
        objMsg = objMsg + "请输入返还人|";
        Flag = false;
    }
    if (document.getElementById("txtReturnDeptID").value == "") {
        objTitle = objTitle + "返还部门|";
        objMsg = objMsg + "请输入返还部门|";
        Flag = false;
    }
    if (document.getElementById("ddlStorage").value == "-1") {
        objTitle = objTitle + "返还仓库|";
        objMsg = objMsg + "请输入返还仓库|";
        Flag = false;
    }
    if (document.getElementById("txtTransactor").value == "") {
        objTitle = objTitle + "入库人|";
        objMsg = objMsg + "请输入入库人|";
        Flag = false;
    }
    if (strlen(document.getElementById("tboxSummary").value) > 200) {
        objTitle = objTitle + "摘要|";
        objMsg = objMsg + "摘要字符数不能大于200|"
        Flag = false;
    }
    if (strlen(document.getElementById("tboxRemark").value) > 800) {
        objTitle = objTitle + "备注|";
        objMsg = objMsg + "备注字符数不能大于800";
        Flag = false;
    }
    /*验证明细*/
    var ck = document.getElementsByName("chkDetail");
    var IsHas = false;
    var IsReturn = false;
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_list_" + ck[i].value).style.display != "none") {
            IsHas = true;
        }
    }

    if (!IsHas) {
        objTitle = objTitle + "明细|";
        objMsg = objMsg + "借货返还单明细不能为空|";
        Flag = false;
    }
    if (!Flag) {
        popMsgObj.Show(objTitle, objMsg);
        return false;
    }
    else
        return true;
}



//保存数据 
function Save() {
    if (ValidateInfo()) {
        var action = document.getElementById("action");
        if (action.value == "ADD")
            Add();
        else if (action.value == "EDIT")
            Edit();
    }
}

//添加数据
function Add() {
    var url = "../../../Handler/Office/StorageManager/StorageReturnSave.ashx";
    var ReturnNo = "";
    var bmgz = "";
    if (document.getElementById("txtRuleCodeNo_ddlCodeRule").value == "") {
        ReturnNo = document.getElementById("txtRuleCodeNo_txtCode").value;
        if (ReturnNo == "") {
            popMsgObj.Show("编号|", "请输入单据编号|");
            return;
        }
        else
            bmgz = "sd";
    }
    else {
        bmgz = "zd";
        ReturnNo = document.getElementById("txtRuleCodeNo_ddlCodeRule").value;
    }

    //构造参数列表
    var para = "ReturnNo=" + ReturnNo +
                  "&ReturnTitle=" + escape(document.getElementById("txtReturnTitle").value) +
                  "&BorrowNo=" + document.getElementById("txtBorrowNo").value +
                  "&StorageID=" + document.getElementById("ddlStorage").value +
                  "&DeptID=" + document.getElementById("txtReturnDeptID").value +
                  "&ReturnerID=" + document.getElementById("txtReturnerID").value +
                  "&TotalPrice=" + document.getElementById("txtTotalPrice").value +
                  "&TotalCount=" + document.getElementById("txtTotalCount").value +
                  "&Summary=" + escape(document.getElementById("tboxSummary").value) +
                  "&Remark=" + escape(document.getElementById("tboxRemark").value) +
                  "&FromBillID=" + document.getElementById("txtBorrowNo").title +
                  "&Transactor=" + document.getElementById("txtTransactor").value +
                  "&action=" + document.getElementById("action").value + "&bmgz=" + bmgz + GetExtAttrValue();


    var SortNo = new Array();
    var ProductID = new Array();
    var ProductName = new Array();
    var UnitID = new Array();
    var ProductCount = new Array();
    var ReturnCount = new Array();
    var UnitPrice = new Array();
    var dTotalPrice = new Array();
    var FromLineNo = new Array();
    var RemarkList = new Array();
    var UsedUnitID = new Array();
    var UsedCount = new Array();
    var UsedPrice = new Array();
    var ExRate = new Array();
    var BatchNo = new Array();

    var ck = document.getElementsByName("chkDetail");
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_list_" + ck[i].value).style.display != "none") {
            SortNo.push(document.getElementById("txtDetailSortNo_" + ck[i].value).value);
            ProductID.push(document.getElementById("hidProductID_" + ck[i].value).value);
            ProductName.push(document.getElementById("txtProcutName_" + ck[i].value).value);
            UnitID.push(document.getElementById("hidUnitID_" + ck[i].value).value);
            ProductCount.push(document.getElementById("txtMustReturnCount_" + ck[i].value).value);
            ReturnCount.push(document.getElementById("txtReturnCount_" + ck[i].value).value);
            UnitPrice.push(document.getElementById("hidUnitPrice_" + ck[i].value).value);
            dTotalPrice.push(document.getElementById("txtReturnTotalPrice_" + ck[i].value).value);
            FromLineNo.push(document.getElementById("txtSortNo_" + ck[i].value).value);
            RemarkList.push(document.getElementById("txtRemark_" + ck[i].value).value);
            BatchNo.push(document.getElementById("txtBatchNo_" + ck[i].value).value);

            if ($("#txtIsMoreUnit").val() == "1") {
                UsedUnitID.push(document.getElementById("ddlUsedUnit_" + ck[i].value).value.split('|')[0]);
                UsedCount.push(document.getElementById("txtUsedReturnCount_" + ck[i].value).value);
                UsedPrice.push(document.getElementById("txtUsedUnitPrice_" + ck[i].value).value);
                ExRate.push(document.getElementById("ddlUsedUnit_" + ck[i].value).value.split('|')[1]);
            } else {
                UsedUnitID.push("0");
                UsedCount.push("0");
                UsedPrice.push("0");
                ExRate.push("0");
            }
        }
    }
    para = para +
           "&SortNo=" + SortNo.toString() +
           "&ProductID=" + ProductID.toString() +
           "&ProductName=" + escape(ProductName.toString()) +
           "&UnitID=" + UnitID.toString() +
           "&ProductCount=" + ProductCount.toString() +
           "&ReturnCount=" + ReturnCount.toString() +
           "&UnitPrice=" + UnitPrice.toString() +
           "&dTotalPrice=" + dTotalPrice.toString() +
           "&FromLineNo=" + FromLineNo.toString() +
           "&RemarkList=" + RemarkList.toString() +
           "&UsedUnitID=" + UsedUnitID.toString() +
           "&UsedCount=" + UsedCount.toString() +
           "&UsedPrice=" + UsedPrice.toString() +
           "&ExRate=" + ExRate.toString() +
           "&BatchNo=" + BatchNo.toString();

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            if (msg.split('|')[0] == "1") {
                popMsgObj.Show("保存数据|", "该编号已被使用，请输入未使用的编号！|");
            }
            else if (msg.split('|')[0] == "2") {
                // 更改动作参数
                document.getElementById("action").value = "EDIT";
                //分析返回的参数 为明细添加ID 以便执行更新操作
                document.getElementById("txtRerunNo").value = msg.split('|')[1].split('#')[1];
                document.getElementById("txtReturnID").value = msg.split('|')[1].split('#')[0];
                document.getElementById("div_InNo_uc").style.display = "none";
                document.getElementById("div_InNo_Lable").style.display = "block";
                document.getElementById("imgNew").style.display = "block";
                document.getElementById("imgNew").style.display = "none";
                popMsgObj.Show("保存数据|", "保存成功|");
                var CurrentUserID = document.getElementById("txtCurrentUserID").value;
                var CurrentDate = document.getElementById("txtCurrentDate").value;
                var CurrentUserName = document.getElementById("txtCurrentUserName").value;
                document.getElementById("tbxoModifiedUser").value = CurrentUserID;
                document.getElementById("tboxModifiedDate").value = CurrentDate;
                cSaveButton();
            }
            else if (msg.split('|')[0] == "3") {
                popMsgObj.Show("保存数据|", msg.split('|')[1] + "|");
            }
            else
                popMsgObj.Show("保存数据|", "保存失败|");


        },
        error: function() { popMsgObj.Show("保存|", "保存失败|"); }
    });
}


//更新数据
function Edit() {
    var url = "../../../Handler/Office/StorageManager/StorageReturnSave.ashx";
    var ReturnNo = document.getElementById("txtRerunNo").value;
    var bmgz = "";
    //构造参数列表
    var para = "ReturnNo=" + ReturnNo +
                  "&ReturnTitle=" + escape(document.getElementById("txtReturnTitle").value) +
                  "&BorrowNo=" + document.getElementById("txtBorrowNo").value +
                  "&StorageID=" + document.getElementById("ddlStorage").value +
                  "&DeptID=" + document.getElementById("txtReturnDeptID").value +
                  "&ReturnerID=" + document.getElementById("txtReturnerID").value +
                  "&TotalPrice=" + document.getElementById("txtTotalPrice").value +
                  "&TotalCount=" + document.getElementById("txtTotalCount").value +
                  "&Summary=" + escape(document.getElementById("tboxSummary").value) +
                  "&Remark=" + escape(document.getElementById("tboxRemark").value) +
                  "&FromBillID=" + document.getElementById("txtBorrowNo").title +
                  "&Transactor=" + document.getElementById("txtTransactor").value +
                  "&action=" + document.getElementById("action").value + "&ReturnID=" + document.getElementById("txtReturnID").value + GetExtAttrValue(); ;

    var SortNo = new Array();
    var ProductID = new Array();
    var ProductName = new Array();
    var UnitID = new Array();
    var ProductCount = new Array();
    var ReturnCount = new Array();
    var UnitPrice = new Array();
    var dTotalPrice = new Array();
    var FromLineNo = new Array();
    var RemarkList = new Array();
    var UsedUnitID = new Array();
    var UsedCount = new Array();
    var UsedPrice = new Array();
    var ExRate = new Array();
    var BatchNo = new Array();

    var ck = document.getElementsByName("chkDetail");
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_list_" + ck[i].value).style.display != "none") {
            SortNo.push(document.getElementById("txtDetailSortNo_" + ck[i].value).value);
            ProductID.push(document.getElementById("hidProductID_" + ck[i].value).value);
            ProductName.push(document.getElementById("txtProcutName_" + ck[i].value).value);
            UnitID.push(document.getElementById("hidUnitID_" + ck[i].value).value);
            ProductCount.push(document.getElementById("txtMustReturnCount_" + ck[i].value).value);
            ReturnCount.push(document.getElementById("txtReturnCount_" + ck[i].value).value);
            UnitPrice.push(document.getElementById("hidUnitPrice_" + ck[i].value).value);
            dTotalPrice.push(document.getElementById("txtReturnTotalPrice_" + ck[i].value).value);
            FromLineNo.push(document.getElementById("txtSortNo_" + ck[i].value).value);
            RemarkList.push(escape(document.getElementById("txtRemark_" + ck[i].value).value));
            BatchNo.push(document.getElementById("txtBatchNo_" + ck[i].value).value);
            if ($("#txtIsMoreUnit").val() == "1") {
                UsedUnitID.push(document.getElementById("ddlUsedUnit_" + ck[i].value).value.split('|')[0]);
                UsedCount.push(document.getElementById("txtUsedReturnCount_" + ck[i].value).value);
                UsedPrice.push(document.getElementById("txtUsedUnitPrice_" + ck[i].value).value);
                ExRate.push(document.getElementById("ddlUsedUnit_" + ck[i].value).value.split('|')[1]);
            } else {
                UsedUnitID.push("0");
                UsedCount.push("0");
                UsedPrice.push("0");
                ExRate.push("0");
            }
        }
    }
    para = para +
           "&SortNo=" + SortNo.toString() +
           "&ProductID=" + ProductID.toString() +
           "&ProductName=" + escape(ProductName.toString()) +
           "&UnitID=" + UnitID.toString() +
           "&ProductCount=" + ProductCount.toString() +
           "&ReturnCount=" + ReturnCount.toString() +
           "&UnitPrice=" + UnitPrice.toString() +
           "&dTotalPrice=" + dTotalPrice.toString() +
           "&FromLineNo=" + FromLineNo.toString() +
           "&RemarkList=" + RemarkList.toString() +
            "&UsedUnitID=" + UsedUnitID.toString() +
            "&UsedCount=" + UsedCount.toString() +
            "&UsedPrice=" + UsedPrice.toString() +
            "&ExRate=" + ExRate.toString() +
            "&BatchNo=" + BatchNo.toString();
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {

            if (msg.split('|')[0] == "1") {
                popMsgObj.Show("更新数据|", "保存成功|");
                cSaveButton();
                var CurrentUserID = document.getElementById("txtCurrentUserID").value;
                var CurrentDate = document.getElementById("txtCurrentDate").value;
                var CurrentUserName = document.getElementById("txtCurrentUserName").value;
                document.getElementById("tbxoModifiedUser").value = CurrentUserID;
                document.getElementById("tboxModifiedDate").value = CurrentDate;
            }
            else
                popMsgObj.Show("更新数据|", "保存失败|");


        },
        error: function() { popMsgObj.Show("更新|", "保存失败|"); }
    });

}
   var selPoint=0;
window.onload = function() {
    Init(); LoadInitInfo();
    cSaveButton();
   

  selPoint=  parseInt( document.getElementById("hidSelPoint").value);
}

//初始化信息
function Init() {
    var action = document.getElementById("action");
    if (action.value == "EDIT") {
        document.getElementById("div_InNo_uc").style.display = "none";
        document.getElementById("div_InNo_Lable").style.display = "block";
        document.getElementById("imgNew").style.display = "block";
        document.getElementById("divTitle").innerHTML = "借货返货单";
        document.getElementById("imgBack").style.display = "block";
        document.getElementById("imgNew").style.display = "none";
    }
    else if (action.value == "ADD") {
        GetExtAttr('officedba.StorageReturn', null);
        document.getElementById("imgNew").style.display = "none";
        document.getElementById("imgBack").style.display = "none";
    }

    if (document.getElementById("txtIsBack").value == "1") {
        document.getElementById("imgBack").style.display = "block";
    }

}

//载入信息
function LoadInitInfo() {
    var action = document.getElementById("action");
    if (action.value == "EDIT") {
        //读取基本信息
        getBaseInfo();

    }
}

//读取基本信息
function getBaseInfo() {
    var ReturnID = document.getElementById("txtReturnID").value;
    var url = "../../../Handler/Office/StorageManager/StorageReturnSave.ashx";
    var para = "ReturnID=" + ReturnID + "&action=GET";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {

            var data = msg.data[0];
            GetExtAttr('officedba.StorageReturn', data); //获取扩展属性并填充值 
            document.getElementById("txtReturnTitle").value = data.Title;
            document.getElementById("txtBorrowNo").value = data.BorrowNo;
            document.getElementById("txtBorrowDept").value = data.BorrowDeptName;
            document.getElementById("txtBorrower").value = data.BorrowerName;
            document.getElementById("txtBorrowDate").value = data.BorrowDate;
            document.getElementById("txtOutDept").value = data.OutDeptName;
            document.getElementById("UserReturner").value = data.ReturnPersonName;
            document.getElementById("txtReturnerID").value = data.ReturnPerson;
            document.getElementById("txtReturnDate").value = data.ReturnDate;
            document.getElementById("DeptReturnDept").value = data.DeptName;
            document.getElementById("txtReturnDeptID").value = data.DeptID;
            document.getElementById("ddlStorage").value = data.StorageID;
            document.getElementById("UserTransactor").value = data.TransactorName;
            document.getElementById("txtTransactor").value = data.Transactor;
            document.getElementById("tboxSummary").value = data.Summary;
            document.getElementById("txtTotalCount").value = parseFloat(data.TotalCount).toFixed(2);
            document.getElementById("txtTotalPrice").value = parseFloat(data.TotalPrice).toFixed(2);
            document.getElementById("tboxCreator").value = data.Creator;
            document.getElementById("tboxCreateDate").value = data.CreateDate;
            document.getElementById("ddlBillStatus").value = data.BillStatus;
            document.getElementById("tboxRemark").value = data.Remark;
            document.getElementById("tboxConfirmor").value = data.Confirmor;
            document.getElementById("tboxConfirmorDate").value = data.ConfirmDate;
            document.getElementById("tboxCloser").value = data.Closer;
            document.getElementById("tboxCloseDate").value = data.CloseDate;
            document.getElementById("tbxoModifiedUser").value = data.ModifiedUer;
            document.getElementById("tboxModifiedDate").value = data.ModifiedDate;
            document.getElementById("txtBorrowNo").title = data.FromBillID;
            //载入明细
            getDetailInfo();
        },
        error: function() { popMsgObj.Show("载入数据|", "载入数据失败|"); }
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
//读取借货返还单明细
function getDetailInfo() {
    var url = "../../../Handler/Office/StorageManager/StorageReturnDetailList.ashx";
    var para = "ReturnNo=" + document.getElementById("txtRerunNo").value;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            var index = 1;
            $("#tblBorrowDetailList tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    var html = "<td class=\"cell\"align=\"center\" >" + " <input onclick=\"unSelAll();\" type=\"checkbox\" id=\"chk_list_" + item.SortNo + "\" name=\"chkDetail\" value=\"" + item.SortNo + "\" /><input type=\"hidden\" id=\"txtDetailID_" + item.SortNo + "\" value=\"" + item.ID + "\" /></td>";
                    html += "<td  class=\"cell\"><input type=\"text\" readonly  id=\"txtDetailSortNo_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.SortNo + "\"  name=\"rownum\"/></td>";
                    html += "<td  class=\"cell\"><input type=\"text\"  disabled=\"true\"  class=\"tdinput tboxsize textAlign\" id=\"txtProductNo_" + item.SortNo + "\"  value=\"" + item.ProductNo + "\"/><input type='hidden' id='hidProductID_" + item.SortNo + "' value='" + item.ProductID + "'/></td>";
                    html += "<td  class=\"cell\"><input type=\"text\" disabled=\"true\" class=\"tdinput tboxsize textAlign\" id=\"txtProcutName_" + item.SortNo + "\"  value=\"" + item.ProductName + "\"/></td>"
                    //批次
                    html += "<td  class=\"cell\"><input type=\"text\" disabled=\"true\" class=\"tdinput tboxsize textAlign\" id=\"txtBatchNo_" + item.SortNo + "\"  value=\"" + item.BatchNo + "\"/></td>"

                    html += "<td  class=\"cell\"><input type=\"text\" disabled=\"true\" class=\"tdinput tboxsize textAlign\" id=\"txtSpec_" + item.SortNo + "\" value=\"" + item.ProductSpec + "\" /></td>";
                    html += "<td  class=\"cell\"><input type=\"text\" disabled=\"true\"  class=\"tdinput tboxsize textAlign\" id=\"txtUnit_" + item.SortNo + "\" value=\"" + item.UnitName + "\" title=\"" + item.UnitID + "\" /><input type='hidden' id='hidUnitID_" + item.SortNo + "' value='" + item.UnitID + "' ></td>";

                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        html += "<td  class=\"cell\"><input type=\"text\"  disabled=\"true\" class=\"tdinput tboxsize textAlign\" id=\"txtReturnCount_" + item.SortNo + "\" value=\"" + parseFloat(item.ReturnCount).toFixed(2) + "\"   /></td>";
                        html += "<td  class=\"cell\" id=\"listUsedUnit_" + item.SortNo + "\"></td>";
                    }

                    html += "<td  class=\"cell\"><input type=\"text\"  disabled=\"true\" class=\"tdinput tboxsize textAlign\" id=\"txtMustReturnCount_" + item.SortNo + "\" value=\"" + parseFloat(item.ProductCount).toFixed(2) + "\"   /></td>";
                    html += "<td  class=\"cell\"><input type=\"text\"  disabled=\"true\" class=\"tdinput tboxsize textAlign\" id=\"txtRealReturnCount_" + index + "\" value=\"" + parseFloat(item.RealReturnCount).toFixed(2) + "\"  /></td>";

                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        html += "<td  class=\"cell\"><input type=\"text\"   class=\"tdinput tboxsize textAlign\" id=\"txtUsedReturnCount_" + item.SortNo + "\" value=\"" + parseFloat(item.UsedUnitCount).toFixed(2) + "\" onblur=\"_cCheckCountReturnCount(" + item.SortNo + "," + item.ReturnCount + ");\" onchange=\"Number_round(this,"+selPoint+")\"/></td>";
                    }
                    else {
                        html += "<td  class=\"cell\"><input type=\"text\"   class=\"tdinput tboxsize textAlign\" id=\"txtReturnCount_" + item.SortNo + "\" value=\"" + getRealReturnCount(item) + "\" onblur=\"_cCheckCountReturnCount(" + item.SortNo + "," + item.ReturnCount + ");\" onchange=\"Number_round(this,"+selPoint+")\"/></td>";
                    }
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        html += "<td  class=\"cell\"><input type=\"hidden\"   class=\"tdinput tboxsize textAlign\" id=\"hidUnitPrice_" + item.SortNo + "\"  value=\"" + parseFloat(item.UnitPrice).toFixed(2) + "\"/>"; if (item.UsedPrice) {
                            html += "<input type=\"text\"   class=\"tdinput tboxsize textAlign\" id=\"txtUsedUnitPrice_" + item.SortNo + "\"  value=\"" + parseFloat(item.UsedPrice).toFixed(2) + "\"  onblur=\"_cCheckPrice(" + item.SortNo + "," + item.UnitPrice + ")\" onchange=\"Number_round(this,"+selPoint+")\"/></td>";
                        }
                        else {
                            html += "<input type=\"text\"   class=\"tdinput tboxsize textAlign\" id=\"txtUsedUnitPrice_" + item.SortNo + "\"  value=\"" + parseFloat(item.UnitPrice).toFixed(2) + "\"  onblur=\"_cCheckPrice(" + item.SortNo + "," + item.UnitPrice + ")\" onchange=\"Number_round(this,"+selPoint+")\"/></td>";
                        }

                    }
                    else {
                        html += "<td  class=\"cell\"><input type=\"text\"   class=\"tdinput tboxsize textAlign\" id=\"hidUnitPrice_" + item.SortNo + "\"  value=\"" + parseFloat(item.UnitPrice).toFixed(2) + "\"  onblur=\"_cCheckPrice(" + item.SortNo + "," + item.UnitPrice + ")\" onchange=\"Number_round(this,"+selPoint+")\"/></td>";
                    }

                    html += "<td  class=\"cell\"><input type=\"text\"  disabled=\"true\" class=\"tdinput tboxsize textAlign\" id=\"txtReturnTotalPrice_" + item.SortNo + "\" value=\"" + parseFloat(item.TotalPrice).toFixed(2) + "\" /></td>";
                    html += "<td  class=\"cell\"><input type=\"text\"  class=\"tdinput tboxsize textAlign\"   title=\"item.Remark\" id=\"txtRemark_" + item.SortNo + "\" value=\"" + item.Remark + "\" onblur=\"checkControlValue('txtRemark_" + item.SortNo + "','备注')\" maxlength=\"50\" /></td>";
                    html += "<td  class=\"cell\"><input type=\"text\"  disabled=\"true\"  class=\"tdinput tboxsize textAlign\" id=\"txtBorrowNo_" + item.SortNo + "\"  value=\"" + item.BorrowNo + "\"   /></td>";
                    html += "<td  class=\"cell\"><input type=\"text\"  disabled=\"true\" class=\"tdinput tboxsize textAlign\" id=\"txtSortNo_" + item.SortNo + "\" value=\"" + item.FromLineNo + "\" /></td>";

                    $("<tr id=\"tr_list_" + item.SortNo + "\" class=\"newrow\"></tr>").append(html).appendTo($("#tblBorrowDetailList tbody"));
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        if (item.ExRate != null && item.ExRate != "") {
                            //填充单位下拉列表
                            GetUnitGroupSelectEx(item.ProductID, 'StockUnit', 'ddlUsedUnit_' + item.SortNo, 'ChangeUnit(this,' + item.SortNo + ')', "listUsedUnit_" + item.SortNo, item.UsedUnitID, "$('#ddlUsedUnit_" + item.SortNo + "').attr('disabled','disabled')");
                        }
                        else {
                            GetUnitGroupSelectEx(item.ProductID, 'StockUnit', 'ddlUsedUnit_' + item.SortNo, 'ChangeUnit(this,' + item.SortNo + ')', "listUsedUnit_" + item.SortNo, item.UsedUnitID, "ReCalByMoreUnit('" + item.SortNo + "','" + item.UnitPrice + "')");
                        }
                    }
                    index++;
                }
                cSaveButton();
            });
        },
        error: function() { popMsgObj.Show("借货单明细|", "载入明细失败|"); }
    });

}

//对于开启多单位 以前的单据重新计算
function ReCalByMoreUnit(rowid, usedPrice) {

    //如果启用 多单位 则重新计算 
    var strRate = document.getElementById("ddlUsedUnit_" + rowid).value;
    var ExRate = strRate.split("|")[1];
    if (usedPrice == "")
        usedPrice = 0;
   
    $("#txtUsedUnitPrice_" + rowid).val(parseFloat(parseFloat(usedPrice) * parseFloat(ExRate)).toFixed(2));
    $("#txtUsedReturnCount_" + rowid).val("");


}



/****************************************************************
* 计量单位切换
****************************************************************/
function ChangeUnit(own, rowid) {

}

/*获取实还数量*/
function getRealReturnCount(item) {

    var ProductCount = parseFloat(item.ProductCount);
    var RealCount = parseFloat(item.RealReturnCount == "" ? "0" : item.RealReturnCount);
    var ReturnCount = parseFloat(item.ReturnCount).toFixed(2);
    if (ReturnCount != "" && ReturnCount != "0" && ReturnCount != "0.0000" && ReturnCount != "0.00")
        return parseFloat(ReturnCount).toFixed(2);
    else
        return parseFloat(ProductCount - RealCount).toFixed(2);
}

/*验证实还数量 并计算价格*/
function _cCheckCountReturnCount(rowid, MustReturnCount) {
    var objValue = document.getElementById("txtReturnCount_" + rowid).value;
    var realReturnCount = document.getElementById("txtRealReturnCount_" + rowid).value;
    /*验证返还数量*/
    if (parseFloat(objValue) > (parseFloat(MustReturnCount) + parseFloat(realReturnCount))) {
        popMsgObj.Show("借货返还单明细|", "实还数量不能大于剩余应还数量|");
        document.getElementById("txtReturnCount_" + rowid).value = MustReturnCount.toFixed(2);
        return;
    }

    if (!IsNumOrFloat(objValue)) {
        popMsgObj.Show("借货返还单明细|", "实还数量必须是有效的数值|");
        document.getElementById("txtReturnCount_" + rowid).value = MustReturnCount.toFixed(2);
        return;
    }

    document.getElementById("txtReturnTotalPrice_" + rowid).value = parseFloat(parseFloat(objValue) * parseFloat(document.getElementById("hidUnitPrice_" + rowid).value)).toFixed(2);
    CalCulateNum('ddlUsedUnit_' + rowid, "txtUsedReturnCount_" + rowid, "txtReturnCount_" + rowid, "txtUsedUnitPrice_" + rowid, "hidUnitPrice_" + rowid, $("#hidSelPoint").val());
    _cTotal();
}

/*验证单价 并计算明细价格*/
function _cCheckPrice(rowid, Price) {
    var objValue = document.getElementById("hidUnitPrice_" + rowid).value;
    if (!IsNumOrFloat(objValue)) {
        popMsgObj.Show("借货返还单明细|", "返还单价必须是有效的数值|");
        document.getElementById("hidUnitPrice_" + rowid).value = Price.toFixed(2);
        return;
    }
    document.getElementById("txtReturnTotalPrice_" + rowid).value = parseFloat(parseFloat(objValue) * parseFloat(document.getElementById("txtReturnCount_" + rowid).value)).toFixed(2);
    _cTotal();
}






function selall() {
    var chk = document.getElementsByName("chkDetail");
    var flag = document.getElementById("chkReturnDetail").checked;
    for (var i = 0; i < chk.length; i++) {
        chk[i].checked = flag;
    }
}
function unSelAll() {
    var flag = true;
    var chk = document.getElementsByName("chkDetail");
    for (var i = 0; i < chk.length; i++) {
        if (chk[i].checked == false) {
            flag = false;
            break;
        }
    }
    document.getElementById("chkReturnDetail").checked = flag;
}
function backtolsit() {
    window.history.back(-1);
}


/*************************************
*更新单据状态 确认时执行业务逻辑操作
*************************************/
function UpdateStatus(type, info) {
    var url = "../../../Handler/Office/StorageManager/StorageReturnSave.ashx";
    var para = "action=STA&type=" + type + "&ReturnNo=" + document.getElementById("txtRerunNo").value + "&ReturnID=" + document.getElementById("txtReturnID").value;
    $.ajax({
        type: "POST",
        dataType: "string",
        url: url,
        cache: false,
        data: para,
        beforeSend: function() { },
        success: function(msg) {
            if (msg == "1") {
                popMsgObj.Show(info + "|", info + "成功|");
                var CurrentUserID = document.getElementById("txtCurrentUserID").value;
                var CurrentDate = document.getElementById("txtCurrentDate").value;
                var CurrentUserName = document.getElementById("txtCurrentUserName").value;
                document.getElementById("tbxoModifiedUser").value = CurrentUserID;
                document.getElementById("tboxModifiedDate").value = CurrentDate;
                //修改控件状态值
                if (type == 1) {
                    document.getElementById("ddlBillStatus").value = 2;
                    document.getElementById("tboxConfirmor").value = CurrentUserName;
                    document.getElementById("tboxConfirmorDate").value = CurrentDate;
                    /*确认成功后 读取明细 以及时显示已返还数量*/
                    //getDetailInfo();
                }
                else if (type == 2) {
                    document.getElementById("ddlBillStatus").value = 4;
                    document.getElementById("tboxCloser").value = CurrentUserName;
                    document.getElementById("tboxCloseDate").value = CurrentDate;
                }
                else if (type == 3) {
                    document.getElementById("ddlBillStatus").value = 2;
                }
                //GetFlowButton_DisplayControl();
                cSaveButton();
            }
            else {
                popMsgObj.Show(info + "|", info + "失败|");
            }
        },
        error: function() { popMsgObj.Show(info + "|", info + "失败|"); }
    });


}

/****************************************
*根据当前订单状态 改变保存按钮
****************************************/
function cSaveButton() {

    var objConfirm = document.getElementById("imgConfirm");
    var objUnConfirm = document.getElementById("imgUnConfirm");

    var objClose = document.getElementById("imgClose");
    var objUnClose = document.getElementById("imgUnClose");

    var objReopen = document.getElementById("imgReopen");
    var objUnReopen = document.getElementById("imgUnReopen");

    try {
        if (document.getElementById("ddlBillStatus").value == "1") {
            if (document.getElementById("action").value == "ADD") {
                objConfirm.style.display = "none";
                objUnConfirm.style.display = "block";
            }
            else {
                objConfirm.style.display = "block";
                objUnConfirm.style.display = "none";
            }
            document.getElementById("imgSave").style.display = "block";
            document.getElementById("imgUnSave").style.display = "none";

            objUnClose.style.dispaly = "block";
            objClose.style.display = "none";

            objReopen.style.display = "none";
            objUnReopen.style.display = "block";

        }
        else if (document.getElementById("ddlBillStatus").value == "2") {
            document.getElementById("imgSave").style.display = "none";
            document.getElementById("imgUnSave").style.display = "block";

            objConfirm.style.display = "none";
            objUnConfirm.style.display = "block";

            objUnClose.style.display = "none";
            objClose.style.display = "block";

            objReopen.style.display = "none";
            objUnReopen.style.display = "block";
        }
        else if (document.getElementById("ddlBillStatus").value == "4") {
            document.getElementById("imgSave").style.display = "none";
            document.getElementById("imgUnSave").style.display = "block";

            objConfirm.style.display = "none";
            objUnConfirm.style.display = "block";

            objUnClose.style.display = "block";
            objClose.style.display = "none";

            objReopen.style.display = "block";
            objUnReopen.style.display = "none";
        }

    }
    catch (e)
  { }
}






/***************************************
* 确认操作
***************************************/
function Fun_ConfirmOperate() {
    var ck = document.getElementsByName("chkDetail");
    var IsReturn = false;
    for (var i = 0; i < ck.length; i++) {
        var ProductCount = parseFloat(document.getElementById("txtMustReturnCount_" + ck[i].value).value);
        var RealReturnCount = parseFloat(document.getElementById("txtRealReturnCount_" + ck[i].value).value);
        var ReturnCount = parseFloat(document.getElementById("txtReturnCount_" + ck[i].value).value);
        if (ProductCount < (RealReturnCount + ReturnCount))
            IsReturn = true;
    }

    if (IsReturn) {
        popMsgObj.Show("明细|", "借货单明细中的实还数量大于剩余返还数量|");
        return;
    }

    if (confirm("是否确认单据？"))
        UpdateStatus(1, "确认");
}

/*************************************
*取消结单 与 结单 操作 flag: true 结单 false 取消结单
*************************************/
function Fun_CompleteOperate(flag) {
    if (flag) {
        if (confirm("是否确认结单？"))
            UpdateStatus(2, "结单");
    }
    else {
        if (confirm("是否确认取消结单？"))
            UpdateStatus(3, "取消结单");
    }
}


/******************************************************
*验证控件中的值是否为浮点或者整数
*******************************************************/
function checkValueIsNumOrFloat(control, currentValue, msg, IsNum) {
    var obj = document.getElementById(control);
    if (!IsNumOrFloat(obj.value, IsNum)) {
        popMsgObj.Show("借货返还单明细|", msg + "|");
        obj.value = parseFloat(currentValue).toFixed(2);
        return;
    }
}
/**/
function checkFloat(control, currentValue, msg, IsNum, rowid) {
    var obj = document.getElementById(control);
    if (!IsNumOrFloat(obj.value, IsNum)) {
        popMsgObj.Show("借货返还单明细|", msg + "|");
        obj.value = parseFloat(currentValue).toFixed(2);
        return;
    }
    if (parseFloat(obj.value) > (parseFloat(document.getElementById("txtMustReturnCount_" + rowid).value) - parseFloat(document.getElementById("txtRealReturnCount_" + rowid).value))) {
        popMsgObj.Show("借货返还单明细|", "返还数量不能大于剩余返还数量|");
        obj.value = parseFloat(currentValue).toFixed(2);
        return;
    }
}

/*************************************************
*验证控件中是否包含特殊字符 如果有则弹出提示 清空
*************************************************/
function checkControlValue(control, msg) {
    msg += "中包含特殊字符，请重新输入";
    if (!CheckSpecialWord(document.getElementById(control).value)) {
        popMsgObj.Show("基本信息|", msg + "|");
        document.getElementById(control).value = "";
        return;
    }
}

/*打印*/
function PrintBill() {
    var ID = document.getElementById("txtReturnID").value;
    var No = document.getElementById("txtRerunNo").value;
    if (ID == "" || ID == "0" || No == "") {
        popMsgObj.Show("打印|", "请保存单据再打印|");
        return;
    }
    window.open("../../../Pages/PrinttingModel/StorageManager/StorageReturnPrint.aspx?ID=" + ID + "&No=" + No);
}


/*库存快照*/
function ShowSnapshot() {
    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';
    var signFrame = findObj("tblBorrowDetailList", document);
    for (var i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].id;
            if (document.getElementById('chk_list_' + i).checked) {
                detailRows++;
                intProductID = $("#TD_ProID_" + i).val();
                snapProductName = $("#TD_Pro_" + i).val();
                snapProductNo = $("#TD_ProNo_" + i).val();
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