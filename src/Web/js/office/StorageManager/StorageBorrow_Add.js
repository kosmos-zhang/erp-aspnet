/************************************************************************
* 作    者： 朱贤兆
* 创建日期： 2010.03.31
* 描    述： 借货申请单
* 修改日期： 2010.03.31
* 版    本： 0.1.0                                                                     
************************************************************************/


/****************************************************************
* 页面初始化
****************************************************************/

  var selPoint=0;
$(document).ready(function() {
    funSetCodeBar();
  selPoint=  parseInt( document.getElementById("hidSelPoint").value);
    var objaction = document.getElementById("action");
    if (objaction.value == "EDIT") {
        getStorageBorrow();
        document.getElementById("divTitle").innerHTML = "借货申请单";
        document.getElementById("imgBack").style.display = "block";
    }
    else
    { GetExtAttr('officedba.StorageBorrow', null); ActionFlag = "addflag" }
    if (document.getElementById("txtIsBack").value != "") {
        document.getElementById("imgBack").style.display = "block";
    }
    GetFlowButton_DisplayControl();
});

var ActionFlag = "";

/*重写toFixed*/
Number.prototype.toFixed = function(d) {
    return FormatAfterDotNumber(this, parseInt($("#hidSelPoint").val()));
}

/****************************************************************
* 参数设置：是否显示条码控制
****************************************************************/
function funSetCodeBar() {
    try {
        var hidCodeBar = document.getElementById('hidCodeBar').value;
        var objCodeBar = document.getElementById('btnGetGoods');
        hidCodeBar == '1' ? objCodeBar.style.display = '' : objCodeBar.style.display = 'none';
    } catch (e) { }
}

/****************************************************************
* 验证数据
****************************************************************/
function checkForm() {
    var msgtitle = "";
    var msgtext = "";
    var flag = true;

    //获取编码规则下拉列表选中项
    var codeRule = document.getElementById("txtRuleCodeNo_ddlCodeRule").value;
    var action = document.getElementById("action").value;
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

    if (document.getElementById("tboxTitle").value == "") {
        //msgtitle = msgtitle + "主题|"; msgtext = msgtext + "请输入主题|"; flag = false;
    }
    else {
        if (!CheckSpecialWord(document.getElementById("tboxTitle").value)) {
            msgtitle = msgtext + "主题|";
            msgtext = msgtext + "借货单主题不能包含特殊字符|";
        }
    }
    if (document.getElementById("txtBorrower").value == "")
    { msgtitle = msgtitle + "借货人|"; msgtext = msgtext + "请输入借货人|"; flag = false; }
    if (document.getElementById("txtBorrowDeptID").value == "")
    { msgtitle = msgtitle + "借货部门|"; msgtext = msgtext + "请输入借货部门|"; flag = false; }
    if (document.getElementById("ddlDepot").value == "")
    { msgtitle = msgtitle + "借出仓库|"; msgtext = msgtext + "请输入借出仓库|"; flag = false; }
    if (document.getElementById("txtTransactor").value == "")
    { msgtitle = msgtitle + "出库人|"; msgtext = msgtext + "请输入出库人|"; flag = false; }
    if (document.getElementById("tboxOutDate").value == "") {
        msgtitle = msgtitle + "出库日期|";
        msgtext = msgtext + "请输入出库日期|";
        flag = false;
    }
    if (strlen(document.getElementById("tboxSummary").value) > 200) {
        msgtitle = msgtitle + "摘要|";
        msgtext = msgtext + "摘要字符数不能大于200|"
        flag = false;
    }
    if (strlen(document.getElementById("tboxRemark").value) > 800) {
        msgtitle = msgtitle + "备注|";
        msgtext = msgtext + "备注字符数不能大于800";
        flag = false;
    }

    if (document.getElementById("tboxBorrowTime").value != "") {
        if (document.getElementById("tboxBorrowTime").value > document.getElementById("tboxOutDate").value) {
            msgtitle = msgtitle + "借货日期|";
            msgtext = msgtext + "借货日期不能大于出库日期|";
            flag = false;
        }
    }


    /*验证明细*/
    var chk = document.getElementsByName("chk");
    var index = 0;
    for (var i = 0; i < chk.length; i++) {
        var returnCount = document.getElementById("tboxReturnCount_list_" + chk[i].value).value;
        var borrowCount = document.getElementById("tboxQuantity_list_" + chk[i].value).value;
        if (document.getElementById("tr_list_" + chk[i].value).style.display != "none")
            index++;

        if (document.getElementById("tr_list_" + chk[i].value).style.display != "none" && returnCount != "" && parseFloat(returnCount) > parseFloat(borrowCount)) {
            msgtitle = msgtitle + "借货单明细|";
            msgtext = msgtext + "预计归还数量不能大于借货数量|";
            flag = false;
            break;
        }
        if (document.getElementById("tr_list_" + chk[i].value).style.display != "none" && document.getElementById("tboxReturnDate_list_" + chk[i].value).value == "") {
            msgtitle = msgtitle + "借货单明细|";
            msgtext = msgtext + "预计归还时间不能为空|";
            flag = false;
            break;
        }
        if (document.getElementById("tboxOutDate").value != "" && document.getElementById("tr_list_" + chk[i].value).style.display != "none") {
            if (document.getElementById("tboxReturnDate_list_" + chk[i].value).value < document.getElementById("tboxOutDate").value) {
                msgtitle = msgtitle + "借货单明细|";
                msgtext = msgtext + "预计归还日期不能小于出库日期|";
                flag = false;
                break;
            }
        }


    }
    if (index == 0) {
        msgtitle = msgtitle + "借货单明细|";
        msgtext = msgtext + "借货申请单明细不能为空|";
        flag = false;
    }

    if (flag)
        return true;
    else {
        popMsgObj.Show(msgtitle, msgtext);
        return false;
    }
}


/****************************************************************
*添加行
****************************************************************/
function addProductList() {
    var tbl = document.getElementById("tblProductlist");
    var lastrowid = document.getElementById("rowlastid");
    var productno = document.getElementById("lastrownum");
    var rowid = parseInt(lastrowid.value);
    //添加行
    var row = tbl.insertRow(-1);
    row.id = "tr_list_" + rowid;
    row.className = "newrow";

    //可修改成循环
    //添加checkbox列
    var i = 0;
    var cellCheck = row.insertCell(i);
    cellCheck.className = "cell";
    cellCheck.align = "center";
    cellCheck.innerHTML = "<input type=\"checkbox\" id=\"chk_list_" + rowid + "\" name=\"chk\" value=\"" + rowid + "\"  onclick=\"subSelect();\" /><input type=\"hidden\" id=\"tboxMinusIs" + rowid + "\"  value=\"\"/><input type=\"hidden\" id=\"tboxProductID_" + rowid + "\" value=\"\" />";
    i++;
    //序号
    var cellNo = row.insertCell(i);
    cellNo.className = "cell";
    cellNo.innerHTML = "<input type=\"text\" id=\"tbox_list_" + rowid + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseInt(productno.value) + "\"  name=\"rownum\" disabled=\"true\"/>";
    i++;

    //物品编号
    var cellProductNo = row.insertCell(i);
    cellProductNo.className = "cell";
    cellProductNo.innerHTML = "<input type=\"text\" onclick=\"getProductInfo(" + rowid + ");\" class=\"tdinput tboxsize textAlign\"  id=\"tboxProductNo_list_" + rowid + "\" />";
    i++;

    //物品名称
    var cellProductName = row.insertCell(i);
    cellProductName.className = "cell";
    cellProductName.innerHTML = "<input type=\"text\"   onclick=\"getProductInfo(" + rowid + ");\" class=\"tdinput tboxsize textAlign\" id=\"tboxProcutName_list_" + rowid + "\" disabled=\"true\"  />";
    i++;

    //批次
    var cellBatchNo = row.insertCell(i++);
    cellBatchNo.className = "cell";
    cellBatchNo.innerHTML = "<select style=' width:90%;' id='BatchNo_SignItem_TD_Text_" + rowid + "' />";



    //规格
    var cellStandard = row.insertCell(i);
    cellStandard.className = "cell";
    cellStandard.innerHTML = "<input type=\"text\" readonly class=\"tdinput tboxsize textAlign\" id=\"tboxStandard_list_" + rowid + "\" disabled=\"true\" />";
    i++;

    //计量单位开启时（基本单位）、反（单位）
    var cellBasicUnit = row.insertCell(i);
    cellBasicUnit.className = "cell";
    cellBasicUnit.innerHTML = "<input type=\"text\" readonly  class=\"tdinput tboxsize textAlign\" id=\"tboxUnit_list_" + rowid + "\" /><input type=\"hidden\" id=\"tboxUnitID_" + rowid + "\" disabled=\"true\"  />";
    i++;

    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        //基本数量
        var cellBasicQuantity = row.insertCell(i);
        cellBasicQuantity.className = "cell";
        cellBasicQuantity.innerHTML = "<input type=\"text\" readonly  class=\"tdinput tboxsize textAlign\" id=\"tboxQuantity_list_" + rowid + "\" />";
        i++;

        //单位
        var cellUnit = row.insertCell(i);
        cellUnit.className = "cell";
        cellUnit.id = "tboxUsedUnit_listtd_" + rowid;
        cellUnit.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        i++;
    }


    //可用库存量
    var cellStorageQuantity = row.insertCell(i);
    cellStorageQuantity.className = "cell";
    cellStorageQuantity.innerHTML = "<input type=\"text\"  readonly class=\"tdinput tboxsize textAlign\" id=\"tboxStorageQuantity_list_" + rowid + "\" disabled=\"true\" />";
    i++;

    //借出单价
    var cellPrice = row.insertCell(i);
    cellPrice.className = "cell";
    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        cellPrice.innerHTML = "<input type=\"hidden\"  id=\"tboxPrice_list_" + rowid + "\" /><input type=\"text\" readonly  class=\"tdinput tboxsize textAlign\" id=\"tboxUsedPrice_list_" + rowid + "\" disabled=\"true\" />";
    }
    else {
        cellPrice.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxPrice_list_" + rowid + "\"  readonly />";
    }
    i++;
 
 


    //借出数量
    var cellQuantity = row.insertCell(i);
    cellQuantity.className = "cell";
    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {
        cellQuantity.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxUsedQuantity_list_" + rowid + "\" onblur=\"GetUsedTotalPrice(" + rowid + ");\"  onchange=\"Number_round(this,"+selPoint+")\"/>";
    }
    else {
        cellQuantity.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxQuantity_list_" + rowid + "\" onblur=\"getTotalPrice(" + rowid + ");\"  onchange=\"Number_round(this,"+selPoint+")\"/>";
    }
    i++;


    // 借出金额
    var cellTotalPrice = row.insertCell(i);
    cellTotalPrice.className = "cell";
    cellTotalPrice.align = "center";
    cellTotalPrice.innerHTML = "<input type=\"text\"  readonly class=\"tdinput tboxsize textAlign\" id=\"tboxTotalPrice_list_" + rowid + "\" />";
    i++;

    //预计返还时间
    var cellRerurnDate = row.insertCell(i);
    cellRerurnDate.className = "cell";
    cellRerurnDate.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxReturnDate_list_" + rowid + "\" onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('tboxReturnDate_list_" + rowid + "')})\" readOnly  />";
    i++;

    // 预计返还数量
    var cellReturnCount = row.insertCell(i);
    cellReturnCount.className = "cell";
    cellReturnCount.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxReturnCount_list_" + rowid + "\"    onblur=\"checkReturnNum('" + rowid + "');\" onchange=\"Number_round(this,"+selPoint+")\"/>";
    i++;

    // 已返还数量
    var cellReturnCount = row.insertCell(i);
    cellReturnCount.className = "cell";
    cellReturnCount.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxRealReturnCount_list_" + rowid + "\"   disabled=\"true\"  />";
    i++;

    // 备注
    var cellReturnCount = row.insertCell(i);
    cellReturnCount.className = "cell";
    cellReturnCount.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxRemark_list_" + rowid + "\"  maxlength=\"50\" />";

    //移动行号
    lastrowid.value = parseInt(rowid + 1);
    //移动序号
    productno.value = parseInt(productno.value) + 1;

}


/****************************************************************
* 验证预计返还数量 预计返还数量必须小于借货数量
****************************************************************/
function checkReturnNum(rowid) {
    var ReturnCount = document.getElementById("tboxReturnCount_list_" + rowid);
    var Count = document.getElementById("tboxQuantity_list_" + rowid);
    if (ReturnCount.value == "")
        return;
    if (IsNumOrFloat(ReturnCount.value, false)) {
        if (!IsNumOrFloat(Count.value))
            return;
    }
    else {
        popMsgObj.Show("借货单明细|", "预计返还数量必须为数字|");
        ReturnCount.value = "";
        ReturnCount.focus();
    }
}


/****************************************************************
* 验证预计返还日期 返还日期必须小于借货日期
****************************************************************/
function checkReturnDate(rowid) {
    var objReturnDate = document.getElementById("tboxReturnDate_list_" + rowid);
    var objborrowDate = document.getElementById("tboxBorrowTime");
    if (objborrowDate.vale != "" && objReturnDate.value != "") {
        if (objborrowDate.value > objReturnDate.value) {
            popMsgObj.Show("借货单明细|", "借货日期大于预计返还日期|");
            objReturnDate.value = "";
            objReturnDate.focus();
            return false;
        }
    }
}

/****************************************************************
* 弹出物品选择框
****************************************************************/
function getProductInfo(rowid) {

    if (document.getElementById("ddlDepot").value != "") {
        openRotoscopingDiv(false, "divPageMask", "PageMaskIframe");
        var para = "BorrowDeptID=-1&StorageID=" + document.getElementById("ddlDepot").value;
        document.getElementById("txtProductRowID").value = rowid;
        popProductInfoObj.ShowList(rowid, para);


    }
    else
        popMsgObj.Show("借货单明细|", "请输入借出仓库");

}

/****************************************************************
* 删除行
****************************************************************/
function DeleteRow() {
    var ck = document.getElementsByName("chk");
    var tbl = document.getElementById("tblProductlist");
    if (!confirm("是否确认删除选中的明细？"))
        return;
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            //alert(ck[i].value);
            document.getElementById("tr_list_" + ck[i].value).style.display = "none";
            //重新排序
            SortProcut(ck[i].value);
        }
    }

    //删除行后重新进行合计
    getTotal();
    if (!document.getElementById("checkall").checked)
        return;
    var flag = true;
    for (var i = -0; i < ck.length; i++) {
        if (document.getElementById("tr_list_" + ck[i].value).style.display != "none") {
            flag = false;
            break;
        }
    }
    if (flag) {
        document.getElementById("checkall").checked = false;
    }

}

/****************************************************************
* 根据输入的借货数量计算借出价格
****************************************************************/
function getTotalPrice(rowid) {
    var objprice = document.getElementById("tboxPrice_list_" + rowid);
    var objcount = document.getElementById("tboxQuantity_list_" + rowid);
    var flag = true;
    if (objprice.value == "")
        flag = false;
    if (!IsNumOrFloat(objcount.value, false) || objcount.value == "") {
        flag = false;
    }

    if (flag) {
        $("#tboxTotalPrice_list_" + rowid).val(parseFloat(parseFloat(objprice.value) * parseFloat(objcount.value)).toFixed(2));
        checkReturnNum(rowid);
        getTotal();
    }
}

/****************************************************************
* 计量单位开启时 根据输入的借货数量计算借出价格
****************************************************************/
function GetUsedTotalPrice(rowid) {
    var objprice = document.getElementById("tboxUsedPrice_list_" + rowid);
    var objcount = document.getElementById("tboxUsedQuantity_list_" + rowid);
    var flag = true;
    if (objprice.value == "")
        flag = false;
    if (!IsNumOrFloat(objcount.value, false) || objcount.value == "")
        flag = false;
    if (flag) {
        //计量单位开启
        //   $("#tboxTotalPrice_list_" + rowid).val(parseFloat(parseFloat(objprice.value) * parseFloat(objcount.value)).toFixed(2));
        CalCulateNum('tboxUsedUnit_list_' + rowid, "tboxUsedQuantity_list_" + rowid, "tboxQuantity_list_" + rowid, "tboxUsedPrice_list_" + rowid, "tboxPrice_list_" + rowid, $("#hidSelPoint").val());
        checkReturnNum(rowid);
        $("#tboxTotalPrice_list_" + rowid).val(parseFloat(parseFloat(objprice.value) * parseFloat(objcount.value)).toFixed(2));
        getTotal();

    }
    else {
        CalCulateNum('tboxUsedUnit_list_' + rowid, "tboxUsedQuantity_list_" + rowid, "tboxQuantity_list_" + rowid, "tboxUsedPrice_list_" + rowid, "tboxPrice_list_" + rowid, $("#hidSelPoint").val());
    }


}


/****************************************************************
* 合计数量与价格
****************************************************************/
function getTotal() {
    var ck = document.getElementsByName("chk");
    var tbl = document.getElementById("tblProductlist");
    var totalCount = 0;
    var totaltPrice = 0;
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_list_" + ck[i].value).style.display != "none" && document.getElementById("tboxQuantity_list_" + ck[i].value).value != "" && document.getElementById("tboxTotalPrice_list_" + ck[i].value).value != "") {
            if ($("#txtIsMoreUnit").val() == "1") {
                totalCount = parseFloat(totalCount) + parseFloat(document.getElementById("tboxUsedQuantity_list_" + ck[i].value).value);
            }
            else {
                totalCount = parseFloat(totalCount) + parseFloat(document.getElementById("tboxQuantity_list_" + ck[i].value).value);
            }

            totaltPrice = parseFloat(totaltPrice) + parseFloat(document.getElementById("tboxTotalPrice_list_" + ck[i].value).value);
        }
    }
    document.getElementById("tboxTotalCount").value = totalCount.toFixed(2);
    document.getElementById("tboxTotalPrice").value = totaltPrice.toFixed(2);

}

/****************************************************************
* 验证是否添加明细
****************************************************************/
function checkProductList() {
    var ck = document.getElementsByName("chk");
    var flag = false;
    var IsFill = false;
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_list_" + ck[i].value).style.display != "none") {
            flag = true;
            if (document.getElementById("tboxQuantity_list_" + ck[i].value).value != "" && document.getElementById("tboxTotalPrice_list_" + ck[i].value).value != "") {
                IsFill = true;
            }
        }
    }

    if (!flag) {
        popMsgObj.Show("借货单明细|", "借货单明细不能为空|");
        return false;
    }
    else {
        if (!IsFill) {
            popMsgObj.Show("借货单明细|", "借货单明细填写不完整|");
            return false;
        }
        else
            return true;
    }
}

/****************************************************************
* 验证明细中预计归还日期是否
****************************************************************/
function checkReturnDate() {
    var ck = document.getElementsByName("chk");
    if (document.getElementById("tboxBorrowTime").value == "")
        return true;
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tboxReturnDate_list_" + ck[i].value).value < document.getElementById("tboxBorrowTime").value) {
            popMsgObj.Show("借货单明细|", "行号" + (parseInt(ck[i].value) + 1) + "：预计归还日期必须大于借货日期|");
            return false;
        }
        else
            return true;
    }
}

/****************************************************************
* 提交数据
****************************************************************/
function SubmitData() {

    var action = document.getElementById("action");
    if (checkForm()) //表单验证
    {
        //验证是否添加明细
        if (checkProductList() && checkReturnDate()) {
            if (action.value == "ADD")
                SaveData(); //保存数据
            else if (action.value == "EDIT")
                Edit();

        }

    }
}

/****************************************************************
* 保存数据
****************************************************************/
function SaveData() {
    var BorrowNo = "";
    var bmgz = "";
    if (document.getElementById("txtRuleCodeNo_ddlCodeRule").value == "") {
        BorrowNo = document.getElementById("txtRuleCodeNo_txtCode").value;
        if (BorrowNo == "") {
            popMsgObj.Show("编号|", "请输入单据编号|");
            return;
        }
        else {
            if (cTrim(BorrowNo, 0) == "") {
                popMsgObj.Show("编号|", "请输入单据编号|");
                return;
            }
            else {
                if (isnumberorLetters(BorrowNo)) {
                    popMsgObj.Show("编号|", "借货单编号只能为字母或数字组合|");
                    return;
                }
            }
            bmgz = "sd";
        }
    }
    else {
        bmgz = "zd";
        BorrowNo = document.getElementById("txtRuleCodeNo_ddlCodeRule").value;
    }

    //获取动作
    var actionobj = document.getElementById("action");

    var Para = ""; //基本信息参数
    //明细参数
    var No = new Array();
    var ProdID = new Array();
    var ProductName = new Array();
    var UnitID = new Array();
    var Price = new Array();
    var Count = new Array();
    var TPrice = new Array();
    var ReturnDate = new Array();
    var RetunCount = new Array();
    var RemarkList = new Array();
    var UsedUnitID = new Array();
    var UsedCount = new Array();
    var UsedPrice = new Array();
    var ExRate = new Array();
    var BatchNo = new Array();


    var url = "../../../handler/Office/StorageManager/StroageBorrowSave.ashx"; //异步页面

    //构造基本信息参数
    Para = "BorrowNo=" + BorrowNo + "&bmgz=" + bmgz +
     "&Title=" + escape(document.getElementById("tboxTitle").value) +
     "&BorrowerID=" + document.getElementById("txtBorrower").value +
     "&DeptID=" + document.getElementById("txtBorrowDeptID").value +
     "&ResaonID=" + document.getElementById("ddlBorrowReason").value +
     "&BorrowDate=" + document.getElementById("tboxBorrowTime").value +
     "&DepotID=" + document.getElementById("ddlDepot").value +
     "&TransactorID=" + document.getElementById("txtTransactor").value +
     "&Summary=" + escape(document.getElementById("tboxSummary").value) +
     "&ConfirmorID=" + document.getElementById("tboxConfirmor").title +
     "&ConfirmDate=" + document.getElementById("tboxConfirmorDate").value +
     "&CloserID=" + document.getElementById("tboxConfirmor").title +
     "&CloseDate=" + document.getElementById("tboxCloseDate").value +
     "&TotalPrice=" + document.getElementById("tboxTotalPrice").value +
     "&TotalCount=" + document.getElementById("tboxTotalCount").value +
     "&BorrowDeptID=" + document.getElementById("txtOutDept").value +
     "&Remark=" + escape(document.getElementById("tboxRemark").value) +
     "&OutDate=" + document.getElementById("tboxOutDate").value + GetExtAttrValue();


    var ck = document.getElementsByName("chk");
    for (var i = ck.length - 1; i >= 0; i--) {

        if (document.getElementById("tr_list_" + ck[i].value).style.display != "none" && document.getElementById("tboxQuantity_list_" + ck[i].value).value != "" && document.getElementById("tboxTotalPrice_list_" + ck[i].value).value != "") {
            No.push(document.getElementById("tbox_list_" + ck[i].value).value);
            ProdID.push(document.getElementById("tboxProductID_" + ck[i].value).value);
            ProductName.push(document.getElementById("tboxProcutName_list_" + ck[i].value).value);
            Price.push(document.getElementById("tboxPrice_list_" + ck[i].value).value);
            Count.push(document.getElementById("tboxQuantity_list_" + ck[i].value).value);
            TPrice.push(document.getElementById("tboxTotalPrice_list_" + ck[i].value).value);
            ReturnDate.push(document.getElementById("tboxReturnDate_list_" + ck[i].value).value);
            RetunCount.push(document.getElementById("tboxReturnCount_list_" + ck[i].value).value);
            RemarkList.push(escape(document.getElementById("tboxRemark_list_" + ck[i].value).value));
            UnitID.push(escape(document.getElementById("tboxUnitID_" + ck[i].value).value));
            BatchNo.push(document.getElementById("BatchNo_SignItem_TD_Text_" + ck[i].value).value);
            if ($("#txtIsMoreUnit").val() == "1") {
                UsedUnitID.push(document.getElementById("tboxUsedUnit_list_" + ck[i].value).value.split('|')[0]);
                UsedCount.push(document.getElementById("tboxUsedQuantity_list_" + ck[i].value).value);
                UsedPrice.push(document.getElementById("tboxUsedPrice_list_" + ck[i].value).value);
                ExRate.push(document.getElementById("tboxUsedUnit_list_" + ck[i].value).value.split('|')[1]);
            } else {
                UsedUnitID.push("0");
                UsedCount.push("0");
                UsedPrice.push("0");
                ExRate.push("0");
            }
        }
    }

    //构造明细参数
    Para = Para + "&No=" + No.toString() +
    "&ProdID=" + ProdID.toString() +
    "&ProductName=" + ProductName.toString() +
    "&UnitID=" + UnitID.toString() +
    "&Price=" + Price.toString() +
    "&Count=" + Count.toString() +
    "&Cost=" + TPrice.toString() +
    "&ReturnDate=" + ReturnDate.toString() +
    "&ReturnCount=" + RetunCount.toString() +
    "&RemarkList=" + RemarkList.toString() +
    "&UsedUnitID=" + UsedUnitID.toString() +
    "&UsedCount=" + UsedCount.toString() +
    "&UsedPrice=" + UsedPrice.toString() +
    "&ExRate=" + ExRate.toString() +
    "&BatchNo=" + BatchNo.toString();

    //设置动作参数
    Para = Para + "&Action=" + actionobj.value;
    //url=encodeURI(url);
    // alert(url);
    //提交数据
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //数据格式:JSON
        url: url, //目标地址
        cache: false,
        data: Para, //数据
        beforeSend: function() { openRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); },
        success: function(msg) {
            var msglist = msg.split('^');
            //分析返回参数  flag[0:成功 1:失败] data[成功时返回数据主键ID,错误时返回提示]
            if (msglist[0] == 0) {
                //记录主键 提供更新操作
                document.getElementById("borrowid").value = msglist[1].split('|')[0];
                document.getElementById("div_InNo_uc").style.display = "none";
                document.getElementById("div_InNo_Lable").style.display = "block";
                document.getElementById("txtNo").value = msglist[1].split('|')[1];
                //修改动作参数
                actionobj.value = "EDIT";
                popMsgObj.Show("保存数据|", "保存成功|");
                GetFlowButton_DisplayControl();
                clearDetailTable();
                getStorageDetailList(document.getElementById("txtNo").value);


            }
            else if (msglist[0] == "-1") {
                popMsgObj.Show("保存信息|", "该编号已被使用，请输入未使用的编号！|");
                return;
            }
            else if (msglist[0] == "-2") {
                popMsgObj.Show("保存信息|", "该单据编号规则自动生成的序号已达到上限，请检查编号规则设置|");
                return;
            }
            else
                popMsgObj.Show("保存数据|", msglist[1] + "|");
        },
        error: function() { popMsgObj.Show("保存数据|", "数据保存失败|"); },
        complete: function() { closeRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); }
    });
}

/****************************************************************
* 删除行 重新排列序号
****************************************************************/
function SortProcut(rowid) {
    var ck = document.getElementsByName("chk");
    var index = 1;
    for (var i = 0; i < ck.length; i++) {
        var objno = document.getElementById("tbox_list_" + ck[i].value);
        if (document.getElementById("tr_list_" + ck[i].value).style.display != "none") {
            objno.value = index;
            index++;
        }
    }

    //重新标识行号
    document.getElementById("lastrownum").value = index;
}

/****************************************************************
* 清除行
****************************************************************/
function clearDetailTable() {
    CloseBarCodeDiv();
    $("#tblProductlist tbody").find("tr.newrow").remove();
    document.getElementById("rowlastid").value = 0;
    document.getElementById("lastrownum").value = 1;
}

/****************************************************************
* 读取明细列表
****************************************************************/
function getStorageDetailList(borrowno) {
    ActionFlag = "editflag";
    var para = "BorrowNo=" + borrowno;
    var url = "../../../handler/Office/StorageManager/StorageBorrowDetailList.ashx";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { openRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); },
        success: function(msg) {
            var index = 0;
            clearDetailTable();
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    addProductList(); //添加行
                    SetRowValue(item, index)//行赋值
                    index++;
                }
            });
            //移动行号
            document.getElementById("rowlastid").value = index;
            //移动序号
            document.getElementById("lastrownum").value = index;

        },
        error: function() { popMsgObj.Show("借货单明细|", "载入明细失败|"); },
        complete: function() { closeRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); }
    });


}


//对于开启多单位 以前的单据重新计算
function ReCalByMoreUnit(rowid, usedPrice) {

    //如果启用 多单位 则重新计算 借出单价 借货数量 预计返还数量 已返还数量
    var strRate = document.getElementById("tboxUsedUnit_list_" + rowid).value;
    var ExRate = strRate.split("|")[1];
    if (usedPrice == "")
        usedPrice = 0;

    $("#tboxUsedPrice_list_" + rowid).val(parseFloat(parseFloat(usedPrice) * parseFloat(ExRate)).toFixed(2));




}



/****************************************************************
* 给行中控件赋值
****************************************************************/
function SetRowValue(data, rowid) {
    $("#tboxProductID_" + rowid).val(data.ProductID);
    $("#tboxProductNo_list_" + rowid).val(data.ProdNo);
    $("#tboxProcutName_list_" + rowid).val(data.ProductName);
    $("#tboxStandard_list_" + rowid).val(data.Specification);
    $("#tboxUnit_list_" + rowid).val(data.CodeName);
    $("#tboxUnitID_" + rowid).val(data.UnitID);
    $("#tboxQuantity_list_" + rowid).val(parseFloat(data.ProductCount == "" ? "0" : data.ProductCount).toFixed(2));
    $("#tboxStorageQuantity_list_" + rowid).val(parseFloat(data.UseCount == "" ? "0" : data.UseCount).toFixed(2));
    $("#tboxPrice_list_" + rowid).val(parseFloat(data.StandardCost == "" ? "0" : data.StandardCost).toFixed(2));
    $("#tboxTotalPrice_list_" + rowid).val(parseFloat(data.BorrowPrice == "" ? "0" : data.BorrowPrice).toFixed(2));
    $("#tboxReturnDate_list_" + rowid).val(data.ReturnDate);
    $("#tboxReturnCount_list_" + rowid).val(parseFloat(data.ReturnCount == "" ? "0" : data.ReturnCount).toFixed(2));
    $("#tboxRealReturnCount_list_" + rowid).val(parseFloat(data.RealReturnCount == "" ? "0" : data.RealReturnCount).toFixed(2));
    $("#tboxRemark_list_" + rowid).val(data.Remark);

    //批次相关
    GetBatchList(data.ProductID, "ddlDepot", "BatchNo_SignItem_TD_Text_" + rowid, document.getElementById("hidIsBatchNo").value == "1" ? true : false, data.BatchNo);

    $("#BatchNo_SignItem_TD_Text_" + rowid).val(data.BatchNo);


    //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") {

        if (data.ExRate != null && data.ExRate != "") {
            $("#tboxReturnCount_list_" + rowid).val(parseFloat(data.ReturnCount).toFixed(2));
            $("#tboxUsedPrice_list_" + rowid).val(parseFloat(data.UsedPrice).toFixed(2));
            $("#tboxUsedQuantity_list_" + rowid).val(parseFloat(data.UsedUnitCount).toFixed(2));
            GetUnitGroupSelect(data.ProductID, 'StockUnit', 'tboxUsedUnit_list_' + rowid, 'ChangeUnit(this,' + rowid + ')', "tboxUsedUnit_listtd_" + rowid, data.UsedUnitID);

        }
        else {
            GetUnitGroupSelectEx(data.ProductID, 'StockUnit', 'tboxUsedUnit_list_' + rowid, 'ChangeUnit(this,' + rowid + ')', "tboxUsedUnit_listtd_" + rowid, data.UsedUnitID, "ReCalByMoreUnit('" + rowid + "','" + data.StandardCost + "')");

        }

    }





}

/****************************************************************
* 第二次点击保存时 为更新动作
****************************************************************/
function Edit() {
    if (document.getElementById("ddlBillStatus").value != 1) {
        popMsgObj.Show("保存数据|", "单据已经处于非制单状态不可修改|");
        return;
    }
    //获取动作
    var actionobj = document.getElementById("action");

    var Para = ""; //基本信息参数
    //明细参数
    var No = new Array();
    var ProdID = new Array();
    var ProductName = new Array();
    var UnitID = new Array();
    var Price = new Array();
    var Count = new Array();
    var TPrice = new Array();
    var ReturnDate = new Array();
    var RetunCount = new Array();
    var RemarkList = new Array();
    var UsedUnitID = new Array();
    var UsedCount = new Array();
    var UsedPrice = new Array();
    var ExRate = new Array();
    var BatchNo = new Array();


    var url = "../../../handler/Office/StorageManager/StroageBorrowSave.ashx"; //异步页面

    //构造基本信息参数
    Para = "BorrowNo=" + document.getElementById("txtNo").value +
                 "&Title=" + escape(document.getElementById("tboxTitle").value) +
                 "&BorrowerID=" + document.getElementById("txtBorrower").value +
                 "&DeptID=" + document.getElementById("txtBorrowDeptID").value +
                 "&ResaonID=" + document.getElementById("ddlBorrowReason").value +
                 "&BorrowDate=" + document.getElementById("tboxBorrowTime").value +
                 "&DepotID=" + document.getElementById("ddlDepot").value +
                 "&TransactorID=" + document.getElementById("txtTransactor").value +
                 "&Summary=" + escape(document.getElementById("tboxSummary").value) +
                 "&ConfirmorID=" + document.getElementById("tboxConfirmor").title +
                 "&ConfirmDate=" + document.getElementById("tboxConfirmorDate").value +
                 "&CloserID=" + document.getElementById("tboxConfirmor").title +
                 "&CloseDate=" + document.getElementById("tboxCloseDate").value +
                 "&TotalPrice=" + document.getElementById("tboxTotalPrice").value +
                 "&TotalCount=" + document.getElementById("tboxTotalCount").value +
                 "&BorrowDeptID=" + document.getElementById("txtOutDept").value +
                 "&Remark=" + escape(document.getElementById("tboxRemark").value) +
                 "&OutDate=" + document.getElementById("tboxOutDate").value + GetExtAttrValue();

    var ck = document.getElementsByName("chk");
    for (var i = ck.length - 1; i >= 0; i--) {
        if (document.getElementById("tr_list_" + ck[i].value).style.display != "none" && document.getElementById("tboxQuantity_list_" + ck[i].value).value != "" && document.getElementById("tboxTotalPrice_list_" + ck[i].value).value != "") {
            No.push(document.getElementById("tbox_list_" + ck[i].value).value);
            ProdID.push(document.getElementById("tboxProductID_" + ck[i].value).value);
            ProductName.push(document.getElementById("tboxProcutName_list_" + ck[i].value).value);
            Price.push(document.getElementById("tboxPrice_list_" + ck[i].value).value);
            Count.push(document.getElementById("tboxQuantity_list_" + ck[i].value).value);
            TPrice.push(document.getElementById("tboxTotalPrice_list_" + ck[i].value).value);
            ReturnDate.push(document.getElementById("tboxReturnDate_list_" + ck[i].value).value);
            RetunCount.push(document.getElementById("tboxReturnCount_list_" + ck[i].value).value);
            RemarkList.push(escape(document.getElementById("tboxRemark_list_" + ck[i].value).value));
            UnitID.push(escape(document.getElementById("tboxUnitID_" + ck[i].value).value));
            BatchNo.push(escape(document.getElementById("BatchNo_SignItem_TD_Text_" + ck[i].value).value));
            if ($("#txtIsMoreUnit").val() == "1") {
                UsedUnitID.push(document.getElementById("tboxUsedUnit_list_" + ck[i].value).value.split('|')[0]);
                UsedCount.push(document.getElementById("tboxUsedQuantity_list_" + ck[i].value).value);
                UsedPrice.push(document.getElementById("tboxUsedPrice_list_" + ck[i].value).value);
                ExRate.push(document.getElementById("tboxUsedUnit_list_" + ck[i].value).value.split('|')[1]);
            } else {
                UsedUnitID.push("0");
                UsedCount.push("0");
                UsedPrice.push("0");
                ExRate.push("0");
            }
        }
    }
    //构造明细参数
    Para = Para + "&No=" + No.toString() +
    "&ProdID=" + ProdID.toString() +
    "&ProductName=" + ProductName.toString() +
    "&UnitID=" + UnitID.toString() +
    "&Price=" + Price.toString() +
    "&Count=" + Count.toString() +
    "&Cost=" + TPrice.toString() +
    "&ReturnDate=" + ReturnDate.toString() +
    "&ReturnCount=" + RetunCount.toString() +
    "&RemarkList=" + RemarkList.toString() +
    "&UsedUnitID=" + UsedUnitID.toString() +
    "&UsedCount=" + UsedCount.toString() +
    "&UsedPrice=" + UsedPrice.toString() +
    "&ExRate=" + ExRate.toString() +
    "&BatchNo=" + BatchNo.toString();

    //设置动作参数
    Para = Para + "&Action=" + actionobj.value;
    if (actionobj.value == "EDIT")
        Para = Para + "&BorrowID=" + document.getElementById("borrowid").value;

    //url=encodeURI(url);
    // alert(url);
    //提交数据     
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //数据格式:JSON
        url: url, //目标地址
        cache: false,
        data: Para, //数据
        beforeSend: function() { openRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); },
        success: function(msg) {
            var msglist = msg.split('^');
            //分析返回参数  flag[0:成功 1:失败] data[成功时返回数据主键ID,错误时返回提示]
            if (msglist[0] == 0) {
                //clearDetailTable();
                popMsgObj.Show("保存数据|", "保存成功|");
                GetFlowButton_DisplayControl();
                //getStorageDetailList(document.getElementById("txtNo").value);
            }
            else
                popMsgObj.Show("保存数据|", "保存失败|");
        },
        error: function() { popMsgObj.Show("保存数据|", "保存失败"); },
        complete: function() { closeRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); }
    });
}

/****************************************************************
* 修改页面初始化扩展属性值
****************************************************************/
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

/****************************************************************
* 提交单据状态
****************************************************************/
function SubmitBill(id, info) {
    var objaction = document.getElementById("action");
    if (objaction.value == "ADD") {
        popMsgObj.Show("保存数据|", "请先保存单据|");
        return;
    }
    var url = "../../../handler/Office/StorageManager/StroageBorrowBillStatus.ashx"; //异步页面
    var Para = "status=" + id + "&BorrowID=" + document.getElementById("borrowid").value + "&BorrowNo=" + document.getElementById("txtNo").value + "&DeptID=" + document.getElementById("txtOutDept").value + "&StorageID=" + document.getElementById("ddlDepot").value;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //
        url: url, //目标地址
        cache: false,
        data: Para, //数据
        beforeSend: function() { openRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); },
        success: function(msg) {
            var data = msg.split('|');
            if (data[0] == 1) {

                popMsgObj.Show("单据操作|", info + "成功|");
                GetFlowButton_DisplayControl();

                var CurrentUser = document.getElementById("txtCurrentUserName").value;
                var CurrentDate = document.getElementById("txtCurrentDate").value;
                if (info == "确认单据") {
                    document.getElementById("tboxConfirmorDate").value = CurrentDate;
                    document.getElementById("tboxConfirmor").value = CurrentUser;
                    document.getElementById("ddlBillStatus").value = 2;
                }
                else if (info == "结单") {
                    document.getElementById("tboxCloseDate").value = CurrentDate;
                    document.getElementById("tboxCloser").value = CurrentUser;
                }
                else if (info == "取消确认") {
                    document.getElementById("ddlBillStatus").value = "1";
                }

                GetFlowButton_DisplayControl();
            }
            else if (data[0] == -1)
                popMsgObj.Show("单据操作|", info + "失败|");
            else
                popMsgObj.Show("单据操作|", info + "失败|");
        },
        error: function() { popMsgObj.Show("单据操作|", "单据操作失败|"); },
        complete: function() { closeRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); }
    });
}


/****************************************************************
* 读取所有的基本信息
****************************************************************/
function getStorageBorrow() {
    var action = document.getElementById("action");
    if (action.value == "EDIT") {


        document.getElementById("div_InNo_Lable").style.display = "block";
        document.getElementById("div_InNo_uc").style.display = "none";
        var url = "../../../Handler/Office/StorageManager/StorageBorrowDetails.ashx";
        var para = "action=EDIT&ID=" + document.getElementById("borrowid").value;
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: url, //目标地址
            cache: false,
            data: para, //数据
            beforeSend: function() { },
            success: function(msg) {
                GetExtAttr('officedba.StorageBorrow', msg.data[0]); //获取扩展属性并填充值 
                document.getElementById("txtNo").value = msg.data[0].BorrowNo;
                document.getElementById("tboxTitle").value = msg.data[0].Title;
                document.getElementById("txtBorrower").value = msg.data[0].Borrower;
                document.getElementById("UserBorrower").value = msg.data[0].BorrowerText;
                document.getElementById("txtBorrowDeptID").value = msg.data[0].DeptID;
                document.getElementById("DeptBorrowDeptName").value = msg.data[0].DeptName;

                document.getElementById("ddlBorrowReason").value = msg.data[0].ReasonType;
                if (msg.data[0].BorrowDate != "1753-01-01")
                    document.getElementById("tboxBorrowTime").value = msg.data[0].BorrowDate;
                document.getElementById("ddlDepot").value = msg.data[0].StorageID;
                document.getElementById("txtTransactor").value = msg.data[0].Transactor;
                document.getElementById("UserTransactor").value = msg.data[0].TransactorText;
                document.getElementById("tboxSummary").value = msg.data[0].Summary;
                document.getElementById("tboxConfirmor").title = msg.data[0].Confirmor;
                document.getElementById("tboxConfirmor").value = msg.data[0].ConfirmorText;
                document.getElementById("tboxConfirmorDate").value = msg.data[0].ConfirmDate;
                document.getElementById("tboxCloser").title = msg.data[0].Closer;
                document.getElementById("tboxCloser").value = msg.data[0].CloserText;
                document.getElementById("tboxCloseDate").value = msg.data[0].CloseDate;
                document.getElementById("tboxTotalPrice").value = parseFloat(msg.data[0].TotalPrice).toFixed(2);
                document.getElementById("tboxTotalCount").value = parseFloat(msg.data[0].CountTotal).toFixed(2);
                document.getElementById("txtOutDept").value = msg.data[0].OutDeptID;
                document.getElementById("DeptOutDept").value = msg.data[0].OutDeptName;
                document.getElementById("tboxRemark").value = msg.data[0].Remark;
                document.getElementById("tbxoModifiedUser").value = msg.data[0].ModifiedUserID;
                document.getElementById("tboxModifiedDate").value = msg.data[0].ModifiedDate;
                document.getElementById("ddlBillStatus").value = msg.data[0].BillStatus;
                document.getElementById("tboxCreateDate").value = msg.data[0].CreateDate;
                document.getElementById("tboxOutDate").value = msg.data[0].OutDate;
                document.getElementById("tboxCreator").value = msg.data[0].CreatorText;

                //读取明细
                getStorageDetailList(document.getElementById("txtNo").value);


                GetFlowButton_DisplayControl();
                //  alert(1);
            },
            error: function() { popMsgObj.Show("查看数据|", "载入数据出错|"); }
        });
    }
}
/****************************************************************
* 单据确认操作
****************************************************************/
function Fun_ConfirmOperate() {

    var chk = document.getElementsByName("chk");
    var flag = true;
    var ProductID = new Array();
    var StorageID = new Array();
    var BatchNo = new Array();

    for (var i = 0; i < chk.length; i++) {
        ProductID.push($("#tboxProductID_" + chk[i].value).val());
        StorageID.push($("#ddlDepot").val());
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
                var detailCount = parseFloat($("#tboxQuantity_list_" + index).val());
                if ($("#txtIsMoreUnit").val() == "1") {
                    var exRateTmp = $("#tboxUsedUnit_list_" + index).val();
                    var exRate = exRateTmp.split('|')[1];
                    nowCount = parseFloat(nowCount / exRate);
                    detailCount = parseFloat($("#tboxUsedQuantity_list_" + index).val());
                }
                if (nowCount < detailCount) {
                    var pName = $("#tboxProcutName_list_" + index).val();
                    if (minuIs) {
                        
                        if (!confirm("明细物品（" + pName + "）的当前库存数量（" +parseFloat( nowCount).toFixed(2) + "）小于借货数量（" +parseFloat( detailCount).toFixed(2) + "）,如果继续将导致负库存，是否继续？ ")) {
                            continue;
                        }
                    }
                    else {
                        titleMsg += "确认单据|";
                        textMsg += "明细物品（" + pName + "）当前库存数量（" + parseFloat(nowCount).toFixed(2) + "）小于借货数量（" + parseFloat(detailCount).toFixed(2) + "）同时该物品仓库设置不允许出现负库存,请修改后重新提交";
                    }
                }

            }
            if (textMsg != "") {
                popMsgObj.Show(titleMsg, textMsg);
                return;
            }


        },
        error: function() { popMsgObj.Show("确认单据|", "确认单据失败|"); }
    });


        if (confirm("是否执行确认单据？"))
           SubmitBill(2, "确认单据");
}

/****************************************************************
* 结单与却小结单操作
****************************************************************/
function Fun_CompleteOperate(flag) {
    //1制单，2执行，3变更，4手工结单，5自动结单
    if (flag) {
        document.getElementById("ddlBillStatus").value = 4;
        if (confirm("是否执行结单？"))
            SubmitBill(4, "结单");
    }
    else {
        document.getElementById("ddlBillStatus").value = 2;
        if (confirm("是否执行取消结单？"))
            SubmitBill(2, "取消结单");
    }
}

/****************************************************************
* 返回列表页面
****************************************************************/
function backtolsit() {
    window.location.href = "StorageBorrowList.aspx?ModuleID=2111402";
}


/*********************
*返回列表页面
*********************/
function backtolsit() {
    window.history.back(-1);
}

/****************************************************************
* 更换出货仓库 清除行
****************************************************************/
function clearRow() {
    clearDetailTable();
}

/****************************************************************
* 取消确认
****************************************************************/
function Fun_UnConfirmOperate() {
    /*验证单据是否被引用*/
    var para = "tableName=StorageReturn" +
                   "&colName=FromBillID" +
                   "&value=" + document.getElementById("borrowid").value;
    var url = "../../../Handler/Common/CheckBillQuote.ashx";


    $.ajax(
     {
         type: "POST",
         dataType: "json",
         url: url,
         cache: false,
         data: para,
         beforeSend: function() { openRotoscopingDiv(false, "divPageMask", "PageMaskIframe"); },
         success: function(msg) {
             if (msg.result) {
                 popMsgObj.Show("单据操作|", "该单据已经被其他单据引用，不能撤销确认！|");
                 return;
             }
             if (confirm("是否确认执行取消确认操作？")) {
                 SubmitBill(1, "取消确认");
             }
         },
         error: function() { closeRotoscopingDiv(false, "divPageMask", "PageMaskIframe"); },
         complete: function() { closeRotoscopingDiv(false, "divPageMask", "PageMaskIframe"); }
     }
        );



}

/****************************************************************
* 读取选中明细的ID
****************************************************************/
function getDetaiID() {
    var ck = document.getElementsByName("chk");
    var IDList = new Array();
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked)
            IDList.push(ck[i].value);
    }

    return IDList;
}

/****************************************************************
* 删除明细
****************************************************************/

function delDetail() {
    var IDList = getDetaiID();
    if (IDList == null || IDList.length <= 0) {
        popMsgObj.Show("删除明细|", "请选择需要删除的的明细|");
        return;
    }
    if (!confirm("确认删除选中的明细，删除后明细将不可恢复，是否继续？"))
        return;

    var url = "../../../Handler/Office/StorageManager/StorageBorrowList.ashx";
    var para = "IDList=" + IDList.toString() +
                   "&action=DELDETAIL";

    $.ajax(
     {
         type: "POST",
         dataType: "json",
         url: url,
         cache: false,
         data: para,
         beforeSend: function() { openRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); },
         success: function(msg) {
             if (msg.result) {
                 DeleteRow();
                 Edit();
                 popMsgObj.Show("删除明细|", "删除明细成功|");
             }
             else {
                 popMsgObj.Show("删除明细|", "删除明细失败|");
                 closeRotoscopingDiv(true, "divPageMask", "PageMaskIframe");
             }
         },
         error: function() { popMsgObj.Show("删除明细|", "删除明细失败|"); closeRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); },
         complete: function() { closeRotoscopingDiv(true, "divPageMask", "PageMaskIframe"); }
     }
    );
}

/****************************************************************
* 根据单据状态和审批状态 是否启用保存按钮
****************************************************************/
function SetSaveButton_DisplayControl(flowStatus) {


    //流程状态：0：待提交   1：待审批   2：审批中   3：审批通过     4：审批不通过   5：撤销审批
    //制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
    //制单状态且审批状态“撤销审批”、“ 审批拒绝”状态的可以进行修改
    //变更和手工结单的不可以修改
    var PageBillID = document.getElementById('borrowid').value;
    var PageBillStatus = document.getElementById('ddlBillStatus').value;
    var objSave = document.getElementById("imgSave");
    var objUnSave = document.getElementById("imgUnSave");

    //alert("flowStatus:"+flowStatus+"|BillStatus:"+PageBillStatus);
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

/****************************************************************
* 全选
****************************************************************/
function selall() {
    var ck = document.getElementsByName("chk");
    var Flag = document.getElementById("checkall").checked;
    for (var i = 0; i < ck.length; i++) {
        ck[i].checked = Flag;
    }
}

/****************************************************************
* 子项单击
****************************************************************/
function subSelect() {
    var Flag = true;
    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        if (!ck[i].checked) {
            Flag = false;
        }
    }
    document.getElementById("checkall").checked = Flag;
}


/****************************************************************
* 单据打印
****************************************************************/
function BillPrint() {
    var ID = document.getElementById("borrowid").value;
    var No = document.getElementById("txtNo").value;
    if (ID == "" || ID == "0" || No == "") {
        popMsgObj.Show("打印|", "请保存单据再打印|");
        return;
    }
    window.open("../../../Pages/PrinttingModel/StorageManager/StorageBorrowPrint.aspx?ID=" + ID + "&No=" + No);
}


/*-----------------------------------------------------条码扫描Start-----------------------------------------------------------------*/

var rerowID = "";

/****************************************************************
* 判断是否有相同记录有返回true，没有返回false
****************************************************************/
function IsExist(prodNo) {
    var signFrame = document.getElementById("tblProductlist");
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 0; i < signFrame.rows.length - 1; ++i) {
        var prodNo1 = document.getElementById("tboxProductNo_list_" + i).value;

        if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
            rerowID = i;
            return true;
        }
    }
    return false;
}

/****************************************************************
* 条码扫描方法
****************************************************************/
function GetGoodsDataByBarCode(ID, ProdNo, ProductName, a, UnitID, CodeName, b, c, d, Specification, e, f, g, h, i, StandardCost, ProductCount) {
    if (!IsExist(ProdNo))//如果重复记录，就不增加
    {
        AddRowByBarCode(ID, ProdNo, ProductName, Specification, ProductCount, CodeName, UnitID, StandardCost);

    }
    else {
        document.getElementById("tboxQuantity_list_" + rerowID).value = parseFloat(document.getElementById("tboxQuantity_list_" + rerowID).value) + 1;
        getTotalPrice(rerowID);
    }

}

/****************************************************************
* 扫描增加行
****************************************************************/
function AddRowByBarCode(ID, ProdNo, ProductName, Specification, ProductCount, CodeName, UnitID, StandardCost) {

    var tbl = document.getElementById("tblProductlist");
    var lastrowid = document.getElementById("rowlastid");
    var productno = document.getElementById("lastrownum");
    var rowid = parseInt(lastrowid.value);
    //添加行
    var row = tbl.insertRow(-1);
    row.id = "tr_list_" + rowid;
    row.className = "newrow";
    //可修改成循环

    //添加checkbox列
    var cellCheck = row.insertCell(0);
    cellCheck.className = "cell";
    cellCheck.align = "center";
    cellCheck.innerHTML = "<input type=\"checkbox\" id=\"chk_list_" + rowid + "\" name=\"chk\" value=\"" + rowid + "\"  onclick=\"subSelect();\" /><input type=\"hidden\" id=\"tboxMinusIs" + rowid + "\"  value=\"" + item.MinusIs + "\"/><input type=\"hidden\" id=\"tboxProductID_" + rowid + "\" value=\"" + ID + "\" />";

    //序号
    var cellNo = row.insertCell(1);
    cellNo.className = "cell";
    cellNo.innerHTML = "<input type=\"text\" id=\"tbox_list_" + rowid + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseInt(productno.value) + "\"  name=\"rownum\" disabled=\"true\"/>";

    //物品编号
    var cellProductNo = row.insertCell(2);
    cellProductNo.className = "cell";
    cellProductNo.innerHTML = "<input type=\"text\" onclick=\"getProductInfo(" + rowid + ");\" value=\"" + ProdNo + "\" class=\"tdinput tboxsize textAlign\"  id=\"tboxProductNo_list_" + rowid + "\" />";

    //物品名称
    var cellProductName = row.insertCell(3);
    cellProductName.className = "cell";
    cellProductName.innerHTML = "<input type=\"text\"   onclick=\"getProductInfo(" + rowid + ");\" value=\"" + ProductName + "\" class=\"tdinput tboxsize textAlign\" id=\"tboxProcutName_list_" + rowid + "\" disabled=\"true\"  />";

    //规格
    var cellStandard = row.insertCell(4);
    cellStandard.className = "cell";
    cellStandard.innerHTML = "<input type=\"text\" readonly class=\"tdinput tboxsize textAlign\" id=\"tboxStandard_list_" + rowid + "\" value=\"" + Specification + "\" disabled=\"true\" />";

    //单位
    var cellUnit = row.insertCell(5);
    cellUnit.className = "cell";
    cellUnit.innerHTML = "<input type=\"text\" readonly  class=\"tdinput tboxsize textAlign\" id=\"tboxUnit_list_" + rowid + "\" value=\"" + CodeName + "\" /><input type=\"hidden\" id=\"tboxUnitID_" + rowid + "\" disabled=\"true\" value=\"" + UnitID + "\" />";

    //可用库存量
    var cellStorageQuantity = row.insertCell(6);
    cellStorageQuantity.className = "cell";
    cellStorageQuantity.innerHTML = "<input type=\"text\"  readonly class=\"tdinput tboxsize textAlign\" id=\"tboxStorageQuantity_list_" + rowid + "\" disabled=\"true\" value=\"" + ProductCount + "\" />";

    //借出单价
    var cellPrice = row.insertCell(7);
    cellPrice.className = "cell";
    cellPrice.innerHTML = "<input type=\"text\" readonly  class=\"tdinput tboxsize textAlign\" id=\"tboxPrice_list_" + rowid + "\" disabled=\"true\" value=\"" + StandardCost + "\" />";

    //借出数量
    var cellQuantity = row.insertCell(8);
    cellQuantity.className = "cell";
    cellQuantity.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxQuantity_list_" + rowid + "\" onblur=\"getTotalPrice(" + rowid + ");\"  onchange=\"Number_round(this,"+selPoint+")\" value=\"1\" />";

    // 借出金额
    var cellTotalPrice = row.insertCell(9);
    cellTotalPrice.className = "cell";
    cellTotalPrice.align = "center";
    cellTotalPrice.innerHTML = "<input type=\"text\"  readonly class=\"tdinput tboxsize textAlign\" id=\"tboxTotalPrice_list_" + rowid + "\" />";

    //预计返还时间
    var cellRerurnDate = row.insertCell(10);
    cellRerurnDate.className = "cell";
    cellRerurnDate.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxReturnDate_list_" + rowid + "\" onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('tboxReturnDate_list_" + rowid + "')})\"  />";

    // 预计返还数量
    var cellReturnCount = row.insertCell(11);
    cellReturnCount.className = "cell";
    cellReturnCount.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxReturnCount_list_" + rowid + "\"   onblur=\"checkReturnNum('" + rowid + "');\" onchange=\"Number_round(this,"+selPoint+")\"/>";

    // 已返还数量
    var cellReturnCount = row.insertCell(12);
    cellReturnCount.className = "cell";
    cellReturnCount.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxRealReturnCount_list_" + rowid + "\"   disabled=\"true\"  />";

    // 备注
    var cellReturnCount = row.insertCell(13);
    cellReturnCount.className = "cell";
    cellReturnCount.innerHTML = "<input type=\"text\"  class=\"tdinput tboxsize textAlign\" id=\"tboxRemark_list_" + rowid + "\"  maxlength=\"50\" />";


    getTotalPrice(rowid);
    //移动行号
    lastrowid.value = parseInt(rowid + 1);
    //移动序号
    productno.value = parseInt(productno.value) + 1;
}

/****************************************************************
* 弹出选择层
****************************************************************/
function Showtmsm() {
    var StoID = $("#ddlDepot").val();
    GetGoodsInfoByBarCode(StoID);
}

/****************************************************************
* 库存快照
****************************************************************/
function ShowSnapshot() {
    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';


    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_list_" + ck[i].value).style.display != "none") {
            if (document.getElementById("chk_list_" + ck[i].value).checked) {
                detailRows++;
                intProductID = document.getElementById('tboxProductID_' + ck[i].value).value;
                snapProductName = document.getElementById('tboxProductNo_list_' + ck[i].value).value;
                snapProductNo = document.getElementById('tboxProcutName_list_' + ck[i].value).value;
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

/****************************************************************
* 明细单位切换带动的计算
****************************************************************/
function ChangeUnit(own, rowid) {
    GetUsedTotalPrice(rowid);
}

