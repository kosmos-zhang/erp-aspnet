//---------Start 库存报损详细信息和明细列表 --------

var LossNoID = document.getElementById('txtIndentityID').value;
$(document).ready(function() {
    fnGetExtAttr(); //物品控件拓展属性
    if (LossNoID > 0) {
        LoadDetailInfo(LossNoID);
        document.getElementById("t_Edit").style.display = "";
    }
    else {
        GetExtAttr('officedba.StorageLoss', null);
        document.getElementById("t_Add").style.display = "";
        try {
            $("#btn_save").show();
        }
        catch (e) { }
    }
    GetFlowButton_DisplayControl();
    if($("#HiddenMoreUnit").val()=="False")
        {
            $("#baseuint").hide();
            $("#basecount").hide();       
        }
        else
        {
            $("#baseuint").show();
            $("#basecount").show();
        }
    //    alert(document.getElementById('btnPageFlowConfrimUn'))
    //    $("#btnPageFlowConfrimUn").hide();
    //    document.getElementById('btnPageFlowConfrimUn').style.display='none';
});
function LoadDetailInfo(LossNoID) {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/StorageManager/StorageLossInfo.ashx?Act=&ID=" + LossNoID, //目标地址
        cache: false,
        success: function(msg) {

            //数据获取完毕，填充页面据显示
            GetExtAttr('officedba.StorageLoss', msg.data[0]);  //获取扩展属性并填充值 
            document.getElementById('lbLossNo').innerHTML = msg.data[0].LossNo;
            document.getElementById('txtLossNo_txtCode').value = msg.data[0].LossNo;
            document.getElementById('txtTitle').value = msg.data[0].Title;

            document.getElementById('UserExecutor').value = msg.data[0].ExecutorName;
            $("#txtExecutorID").val(msg.data[0].Executor); //经办人人ID
            $("#ddlReason").val(msg.data[0].ReasonType); //原因
            $("#ddlStorage").val(msg.data[0].StorageID);
            $("#txtDeptID").val(msg.data[0].DeptID);
            $("#DeptName").val(msg.data[0].DeptName);
            $("#txtSummary").val(msg.data[0].Summary);
            document.getElementById('hfPageAttachment').value = msg.data[0].Attachment;
            document.getElementById('txtLossDate').value = msg.data[0].LossDate;
            document.getElementById('txtTotalPrice').value = parseFloat(msg.data[0].A_TotalPrice).toFixed($("#HiddenPoint").val()); //报损金额合计
            document.getElementById('txtTotalCount').value = parseFloat(msg.data[0].CountTotal).toFixed($("#HiddenPoint").val());
            document.getElementById('txtRemark').value = msg.data[0].Remark;
            document.getElementById('txtCreator').value = msg.data[0].CreatorName;
            $("#txtCreator").attr("title", msg.data[0].Creator);
            document.getElementById('txtCreateDate').value = msg.data[0].CreateDate;
            document.getElementById('sltBillStatus').value = msg.data[0].BillStatus;
            document.getElementById('txtConfirmor').value = msg.data[0].ConfirmorName;
            document.getElementById('txtConfirmDate').value = msg.data[0].ConfirmDate;
            document.getElementById('txtCloser').value = msg.data[0].CloserName;
            $("#txtCloser").attr("title", msg.data[0].Closer);
            document.getElementById('txtCloseDate').value = msg.data[0].CloseDate;
            document.getElementById('txtModifiedUserID').value = msg.data[0].ModifiedUserName;
            document.getElementById('txtModifiedDate').value = msg.data[0].ModifiedDate;
            document.getElementById('txtIndentityID').value = msg.data[0].ID;

            $.each(msg.data, function(i, item) {
               // if (item.ProductID != null && item.ProductID != '') {
                    FillSignRow(i, item.ProductID, item.ProductNo
                            , item.ProductName, item.Specification, item.UnitID,item.HiddenUnitID, item.UnitPrice
                            , item.ProductCount, item.DetaiRemark,item.BatchNo,item.UsedUnitID,item.UsedUnitCount,item.UsedPrice,item.ExRate,item.IsBatchNo);
               // }

            });
            if (document.getElementById('sltBillStatus').value == 1)//根据单据状态显示按钮
            {
                if (msg.data[0].FlowStatus == 0 || msg.data[0].FlowStatus == 5) {
                    try {
                        $("#btn_save").show();
                        $("#btn_UnClick_bc").hide();
                    }
                    catch (e) { }
                }
                else {
                    try {
                        $("#btn_save").hide();
                        $("#btn_UnClick_bc").show();
                    }
                    catch (e) { }
                }

            }
            else {
                try {
                    $("#btn_save").hide();
                    $("#btn_UnClick_bc").show();
                }
                catch (e) { }
            }
            if (document.getElementById('hfPageAttachment').value != "") {
                $("#divDealAttachment").show();
                $("#divUploadAttachment").hide();
                var url = document.getElementById("hfPageAttachment").value;
                var testurl = url.lastIndexOf('\\');
                testurl = url.substring(testurl + 1, url.lenght);
                document.getElementById('attachname').innerHTML = testurl;


            }
            $("#hiddBillStatus").val(document.getElementById('sltBillStatus').value); //给单据隐藏域赋值
        },
        error: function() { },
        complete: function() { }
    });
}

//---------End 库存报损详细信息和明细列表 ----------

//---------Start 库存报损保存 ----------
function Fun_Save_StorageLoss() {
    CountNum();
    var bmgz = "";
    var Flag = true;

    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    var txtIndentityID = $("#txtIndentityID").val();
    if (txtIndentityID == "0")//新建
    {
        if ($("#txtLossNo_ddlCodeRule").val() == "")//手工输入
        {
            txtLossNo = $("#txtLossNo_txtCode").val();
            bmgz = "sd";
            if (txtLossNo == "") {
                isFlag = false;
                fieldText += "单据编号|";
                msgText += "请输入单据编号|";
            }
            else if (!CodeCheck($("#txtLossNo_txtCode").val())) {
                isFlag = false;
                fieldText = fieldText + "单据编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
        else {

            txtLossNo = $("#txtLossNo_ddlCodeRule").val();
            bmgz = "zd";
        }
    }
    else {
        txtLossNo = document.getElementById("lbLossNo").innerHTML;
    }
    var txtTitle = $("#txtTitle").val();
    var txtExecutor = $("#txtExecutorID").val(); //经办人
    var txtDept = $("#txtDeptID").val(); //部门ID
    var ddlStorage = $("#ddlStorage").val();
    var ddlReason = $("#ddlReason").val();
    var txtLossDate = $("#txtLossDate").val();
    var txtSummary = $("#txtSummary").val();
    var hfPageAttachment = document.getElementById("hfPageAttachment").value.replace(/\\/g, "\\\\");
    var txtTotalPrice = $("#txtTotalPrice").val();
    var txtTotalCount = $("#txtTotalCount").val();
    var txtRemark = $("#txtRemark").val();
    var sltBillStatus = $("#sltBillStatus").val();
    var hiddenValue = $("#txtLossNoHidden").val();

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (hiddenValue == '0') {
        isFlag = false;
        fieldText = fieldText + "单据编号|";
        msgText = msgText + "单据编号不允许重复|";
    }
    if (strlen(txtLossNo) > 50) {
        isFlag = false;
        fieldText = fieldText + "单据编号|";
        msgText = msgText + "仅限于50个字符以内|";
    }

    //    if(txtTitle.Trim().length == "")
    //    {
    //        isFlag = false;
    //        fieldText = fieldText + "主题|";
    //   		msgText = msgText +  "请输入主题|";
    //    }
    if (strlen(txtTitle) > 100) {
        isFlag = false;
        fieldText = fieldText + "主题|";
        msgText = msgText + "仅限于100个字符以内|";
    }

    if (document.getElementById('UserExecutor').value == "") {
        isFlag = false;
        fieldText = fieldText + "经办人|";
        msgText = msgText + "请选择经办人|";
    }
    if (txtLossDate == "") {
        isFlag = false;
        fieldText = fieldText + "报损时间|";
        msgText = msgText + "请选择报损时间|";
    }

    if (strlen(txtSummary) > 200) {
        isFlag = false;
        fieldText = fieldText + "摘要|";
        msgText = msgText + "仅限于200个字符以内|";
    }

    if (strlen(txtRemark) > 800) {
        isFlag = false;
        fieldText = fieldText + "备注|";
        msgText = msgText + "仅限于800个字符以内|";
    }

    //明细验证
    var signFrame = findObj("dg_Log", document);
    if (signFrame.rows.length <= 1) {
        isFlag = false;
        fieldText = fieldText + "明细信息|";
        msgText = msgText + "明细信息不能为空|";
    }
    var ifProductNo = "0";
    var ifProductCount = "0";
    var ifRemark = "0";
    var ifbig = "0"; //是否大于应收数量
    for (i = 1; i < signFrame.rows.length; i++) {
        var ProductCount = "ProductCount_SignItem_TD_Text_" + i;
        var Remark = "Remark_SignItem_TD_Text_" + i;
        if (document.getElementById(ProductCount).value == "" || parseFloat(document.getElementById(ProductCount).value) <= 0 || document.getElementById(ProductCount).value == "0.00") {
            ifProductCount = "1";
        }
        if (strlen($("#" + Remark).val()) > 200) {
            ifRemark = "1";
        }
    }
    if (ifProductCount == "1") {
        isFlag = false;
        fieldText = fieldText + "报损数量|";
        msgText = msgText + "请输入有效的数值（大于0）|";
    }

    for (i = 1; i < signFrame.rows.length; i++) {
        var ProductNo = "ProductNo_SignItem_TD_Text_" + i;
        if (document.getElementById(ProductNo).value == "" || $("#ProductNo").attr("title") == "undefined") {
            ifProductNo = "1";
        }
    }

    if (ifProductNo == "1") {
        isFlag = false;
        fieldText = fieldText + "明细物品|";
        msgText = msgText + "明细物品不允许为空|";
    }

    if (ifRemark == "1") {
        isFlag = false;
        fieldText = fieldText + "明细备注|";
        msgText = msgText + "仅限于200个字符以内|";
    }

    var List_TB = findObj("dg_Log", document);
    var rowlength = List_TB.rows.length;
    for (var i = 1; i < rowlength - 1; i++) {
        for (var j = i + 1; j < rowlength; j++) {
            var ProductNoi = "ProductNo_SignItem_TD_Text_" + i;
            var ProductNoj = "ProductNo_SignItem_TD_Text_" + j;
            var StorageIDi = " StorageID_SignItem_TD_Text_" + i;
            var StorageIDj = " StorageID_SignItem_TD_Text_" + j;
            if ($("#" + ProductNoi).val() == $("#" + ProductNoj).val() && $("#" + StorageIDi).val() == $("#" + StorageIDj).val()) {
                isFlag = false;
                fieldText = fieldText + "明细信息|";
                msgText = msgText + "明细中不允许存在重复记录|";
                break;
            }
        }
    }

    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }

    else {

        //明细数组

        var DetailProductID = new Array();
        var DetailSortNo = new Array(); //序号
        var DetailProductCount = new Array();//实际数量
        var DetailUnitID = new Array();//实际单位(改造前就没加)
        var DetailUnitPrice = new Array();//实际单价
        var DetailTotalPrice = new Array();
        var DetailRemark = new Array();
        
        
        var DetailBaseUnitID = new Array();//基本单位
        var DetailBaseCount = new Array();//基本数量
        var DetailBasePrice = new Array();//基本单价
        var DetailExtRate = new Array();//比率
        
        var DetailBatchNo = new Array();//批次
        /*---------明细数组-----------*/

        var signFrame = findObj("dg_Log", document);
        var count = signFrame.rows.length; //有多少行
        for (var i = 1; i < count; i++) {
            if (signFrame.rows[i].style.display != "none") {
                var objProductNo = 'ProductNo_SignItem_TD_Text_' + (i);
                var objSortNo = 'SignItem_TD_Index_' + (i);
                var objProductCount = 'ProductCount_SignItem_TD_Text_' + (i);
                var objUnitPrice = 'UnitPrice_SignItem_TD_Text_' + (i);
                var objTotalPrice = 'TotalPrice_SignItem_TD_Text_' + (i);
                var objRemark = 'Remark_SignItem_TD_Text_' + (i);
                var objRadio = 'chk_Option_' + (i);
                
                var objUnitID='UnitID_SignItem_TD_Text_'+(i);//实际单位                
                var objBaseUnitID='BaseUnit_SignItem_TD_Text_'+(i);//基本单位
                var objBaseCount='BaseCount_SignItem_TD_Text_'+(i);//基本数量
                var objBasePrice='baseprice_td'+(i);//基本单价
                var objExtRate='UnitID_SignItem_TD_Text_'+(i);//比率
                
                var objBatchNo='BatchNo_SignItem_TD_Text_'+(i);//批次

                DetailProductID.push($("#" + objProductNo).attr("title"));
                DetailSortNo.push(document.getElementById(objSortNo.toString()).innerHTML);
                //DetailProductCount.push(document.getElementById(objProductCount.toString()).value);
                //DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value);
                DetailTotalPrice.push(document.getElementById(objTotalPrice.toString()).value);
                DetailRemark.push(document.getElementById(objRemark.toString()).value);
                
                
                if($("#HiddenMoreUnit").val()=="False")//未启用时
                {
                    DetailBaseUnitID.push(document.getElementById(objUnitID.toString()).title);//单位                
                    DetailBaseCount.push(document.getElementById(objProductCount.toString()).value);//数量
                    DetailBasePrice.push(document.getElementById(objUnitPrice.toString()).value);//单价
                    
                    
                }     
                else
                {
                    DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value);//实际单价
                    DetailBaseUnitID.push(document.getElementById(objBaseUnitID.toString()).title);//基本单位                
                    DetailBaseCount.push(document.getElementById(objBaseCount.toString()).value);//基本数量
                    DetailUnitID.push(document.getElementById(objUnitID.toString()).value.split('|')[0].toString());//实际单位ID  
                    DetailProductCount.push(document.getElementById(objProductCount.toString()).value);//实际数量 
                    DetailBasePrice.push(document.getElementById(objBasePrice.toString()).value);//基本单价
                    DetailExtRate.push(document.getElementById(objExtRate.toString()).value.split('|')[1].toString());//比率
                } 
                DetailBatchNo.push(document.getElementById(objBatchNo.toString()).value);//批次


            }
        }

        var UrlParam = "&txtLossNo=" + escape(txtLossNo)
                        + "&txtTitle=" + escape(txtTitle)
                        + "&txtExecutor=" + escape(txtExecutor)
                        + "&txtDept=" + escape(txtDept)
                        + "&ddlStorage=" + escape(ddlStorage)
                        + "&ddlReason=" + escape(ddlReason)
                        + "&txtLossDate=" + escape(txtLossDate)
                        + "&txtTotalPrice=" + escape(txtTotalPrice)
                        + "&txtTotalCount=" + escape(txtTotalCount)
                        + "&txtSummary=" + escape(txtSummary)
                        + "&hfPageAttachment=" + escape(hfPageAttachment)
                        + "&txtRemark=" + escape(txtRemark)
                        + "&sltBillStatus=" + escape(sltBillStatus)
                        + "&DetailProductID=" + escape(DetailProductID.toString())
                        + "&DetailSortNo=" + escape(DetailSortNo.toString())
                        + "&DetailProductCount=" + escape(DetailProductCount.toString())
                        + "&DetailUnitPrice=" + escape(DetailUnitPrice.toString())
                        + "&DetailTotalPrice=" + escape(DetailTotalPrice.toString())
                        + "&DetailRemark=" + escape(DetailRemark.toString())
                        + "&DetailUnitID=" + escape(DetailUnitID.toString())//
                        + "&DetailBaseUnitID=" + escape(DetailBaseUnitID.toString())//
                        + "&DetailBaseCount=" + escape(DetailBaseCount.toString())//
                        + "&DetailBasePrice=" + escape(DetailBasePrice.toString())//
                        + "&DetailExtRate=" + escape(DetailExtRate.toString())//
                        + "&DetailBatchNo=" + escape(DetailBatchNo.toString())//
                        + "&Act=edit"
                        + "&bmgz=" + bmgz
                        + "&ID=" + txtIndentityID.toString() + GetExtAttrValue();

        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageLossAdd.ashx?" + UrlParam,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            complete: function() { hidePopup(); },
            error: function() {
                popMsgObj.ShowMsg('请求发生错误');

            },
            success: function(data) //保存成功以后
            {
                if (data.sta == 1) {
                    var reInfo = data.data.split('|');
                    if (reInfo.length > 1) {

                        document.getElementById('div_LossNo_uc').style.display = "none";
                        document.getElementById('div_LossNo_Lable').style.display = "";
                        if (parseInt(txtIndentityID) <= 0) {
                            document.getElementById('lbLossNo').innerHTML = reInfo[1];
                            document.getElementById('txtLossNo_txtCode').value = reInfo[1];
                            document.getElementById('txtIndentityID').value = reInfo[0];
                            document.getElementById('txtModifiedUserID').value = reInfo[2];
                            document.getElementById('txtModifiedDate').value = reInfo[3];
                        }
                        else {
                            document.getElementById('txtModifiedUserID').value = reInfo[0];
                            document.getElementById('txtModifiedDate').value = reInfo[1];
                        }
                        GetFlowButton_DisplayControl();
                    }
                    popMsgObj.ShowMsg(data.info);
                }
                else {
                    popMsgObj.ShowMsg(data.info);
                }

                document.getElementById("t_Edit").style.display = "";
                document.getElementById("t_Add").style.display = "none";
            }
        });
    }
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

//---------End   销售报损保存 ----------




//计算明细中报损数量总合以及价格总合
function CountNum() {

    
    var List_TB = findObj("dg_Log", document);
    var rowlength = List_TB.rows.length;
    var countnum = 0;
    var countprice = 0;
    for (var i = 1; i < rowlength; i++) {
        if (List_TB.rows[i].style.display != "none") {

            countnum += parseFloat(document.getElementById('ProductCount_SignItem_TD_Text_' + (i)).value);
            countprice += parseFloat(document.getElementById('TotalPrice_SignItem_TD_Text_' + (i)).value);
             if($("#HiddenMoreUnit").val()=="True")//启用时(重新计算)
             {
             try{
                var EXRate = $("#UnitID_SignItem_TD_Text_"+i).val().split('|')[1].toString(); /*比率*/
                var ProductCount = $("#ProductCount_SignItem_TD_Text_"+i).val();/*报损数量*/
                
                if (ProductCount != '')
                {
                    var tempcount=parseFloat(ProductCount*EXRate).toFixed($("#HiddenPoint").val());
                    //var tempprice=parseFloat(EXRate*UnitPrice).toFixed($("#HiddenPoint").val());
                    $("#BaseCount_SignItem_TD_Text_"+i).val(tempcount);/*基本数量根据报损数量和比率算出*/
                    //$("#UnitPrice_SignItem_TD_Text_"+RowID).val(tempprice);/*单价根据基本单价和比率算出*/
                    //$("#TotalPrice_SignItem_TD_Text_"+RowID).val(parseFloat(ProductCount*tempprice).toFixed($("#HiddenPoint").val()));/*金额*/
                }}
                catch(Error){}
            }
        }
    }
    document.getElementById('txtTotalCount').value = (parseFloat(countnum)).toFixed($("#HiddenPoint").val());
    document.getElementById('txtTotalPrice').value = (parseFloat(countprice)).toFixed($("#HiddenPoint").val());

}

//全选
function SelectAll() {
    var signFrame = findObj("dg_Log", document);
    var txtTRLastIndex = signFrame.rows.length;
    if (document.getElementById("checkall").checked) {
        for (var i = 1; i < txtTRLastIndex; i++) {
            if (signFrame.rows[i].style.display != "none") {
                var objRadio = 'chk_Option_' + (i);
                document.getElementById(objRadio.toString()).checked = true;
            }
        }
    }
    else {
        for (var i = 1; i < txtTRLastIndex; i++) {
            if (signFrame.rows[i].style.display != "none") {
                var objRadio = 'chk_Option_' + (i);
                document.getElementById(objRadio.toString()).checked = false;
            }
        }
    }
}


//删除行,才是最后版本 
function DeleteDetailRow() {
    //获取表格
    table = document.getElementById("dg_Log");
    var count = table.rows.length;
    for (var i = count - 1; i > 0; i--) {
        var select = document.getElementById("chk_Option_" + i);
        if (select.checked) {
            DeleteRow(table, i);
        }
    }
}

function DeleteRow(table, row) {
    //获取表格
    var count = table.rows.length - 1;
    table.deleteRow(row);
    if (row < count)//
    {
        for (var j = parseInt(row) + 1; j <= count; j++) {
            var no = j - 1;
            document.getElementById("SignItem_TD_Index_" + j).innerHTML = parseInt(document.getElementById("SignItem_TD_Index_" + j).innerHTML, 10) - 1;
            document.getElementById("SignItem_TD_Index_" + j).id = "SignItem_TD_Index_" + no;
            document.getElementById("chk_Option_" + j).id = "chk_Option_" + no;
            document.getElementById("ProductNo_SignItem_TD_Text_" + j).id = "ProductNo_SignItem_TD_Text_" + no;
            document.getElementById("ProductName_SignItem_TD_Text_" + j).id = "ProductName_SignItem_TD_Text_" + no;
            document.getElementById("Specification_SignItem_TD_Text_" + j).id = "Specification_SignItem_TD_Text_" + no;
            document.getElementById("UnitID_SignItem_TD_Text_" + j).id = "UnitID_SignItem_TD_Text_" + no;
            document.getElementById("ProductCount_SignItem_TD_Text_" + j).id = "ProductCount_SignItem_TD_Text_" + no;
            document.getElementById("UnitPrice_SignItem_TD_Text_" + j).id = "UnitPrice_SignItem_TD_Text_" + no;
            document.getElementById("TotalPrice_SignItem_TD_Text_" + j).id = "TotalPrice_SignItem_TD_Text_" + no;
            document.getElementById("Remark_SignItem_TD_Text_" + j).id = "Remark_SignItem_TD_Text_" + no;
        }
    }
}


//添加行
function AddRow()//增加一个空白行
{
    var signFrame = findObj("dg_Log", document);
    var rowID = signFrame.rows.length;
    var newTR = signFrame.insertRow(rowID); //添加行
    newTR.id = "SignItem_Row_" + rowID;

    var newNameXH = newTR.insertCell(0); //添加列:选择
    newNameXH.className = "cell";
    newNameXH.id = 'SignItem_TD_Check_' + rowID;
    newNameXH.innerHTML = "<input name='chk1' id='chk_Option_" + rowID + "' value=\"\" type='checkbox' onclick=\"IfSelectAll('chk1','checkall');\"/>";

    var newNameTD = newTR.insertCell(1); //添加列:序号
    newNameTD.className = "cell";
    newNameTD.id = 'SignItem_TD_Index_' + rowID;
    newNameTD.innerHTML = newTR.rowIndex.toString();

    var newFitNametd = newTR.insertCell(2); //添加列:物品编号
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_ProductNo_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"\"  class=\"tdinput\"  readonly=\"readonly\" onclick=\"popProductInfoObj.ShowList(this.id,'"+rowID+"');\" style='width:90%'  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(3); //添加列:物品名称
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ProductName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ProductName_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    /*增加批次列：2010.4.12*/
    var newBatchNotd=newTR.insertCell(4);//添加列:批次
    newBatchNotd.className="cell";
    newBatchNotd.id = 'SignItem_TD_BatchNo_'+rowID;
    newBatchNotd.innerHTML ="<select style=' width:90%;' id='BatchNo_SignItem_TD_Text_"+rowID+"' />";//添加列内容

    var newFitDesctd = newTR.insertCell(5); //添加列:物品规格
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Specification_' + rowID;
    newFitDesctd.innerHTML = "<input id='Specification_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容


    /*增加基本单位、基本数量列：2010.4.12*/
    var newBaseUnit=newTR.insertCell(6);//添加列:基本单位
    newBaseUnit.className="cell";
    newBaseUnit.id = 'SignItem_TD_BaseUnit_'+rowID;
    newBaseUnit.innerHTML ="<input id='BaseUnit_SignItem_TD_Text_"+rowID+"' type='text'  class=\"tdinput\"  style='width:90%' readonly />";;//添加列内容
    
    var newBaseCount=newTR.insertCell(7);//添加列：基本数量
    newBaseCount.className="cell";
    newBaseCount.id = 'SignItem_TD_BaseCount_'+rowID;
    newBaseCount.innerHTML ="<input id='BaseCount_SignItem_TD_Text_"+rowID+"' value='0.00' type='text'  class=\"tdinput\"  readonly style='width:90%' />";;//添加列内容
        



    if($("#HiddenMoreUnit").val()=="False")
    {
        newBaseUnit.style.display="none"
        newBaseCount.style.display="none"        
        var newFitNametd = newTR.insertCell(8); //添加列:单位
        newFitNametd.className = "cell";
        newFitNametd.id = 'SignItem_TD_UnitID_' + rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_" + rowID + "\" value=\"\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

    }
    else
    {
        $("#baseuint").show();
        $("#basecount").show();
        var newFitNametd=newTR.insertCell(8);//添加列:单位
        newFitNametd.className="cell";
        newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
        newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
    }



    var point=$("#HiddenPoint").val();
    if($("#HiddenMoreUnit").val()=="False")
    {
        var newFitNametd = newTR.insertCell(9); //添加列:报损数量
        newFitNametd.className = "tdinput";
        newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductCount_SignItem_TD_Text_" + rowID + "\" value='0.00'  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_round(this,'"+point+"');TotalPrice(this.id);CountNum();\"/>"; //添加列内容

    }
    else
    {
        var newFitNametd=newTR.insertCell(9);//添加列:报损数量(根据基本数量计算)
        newFitNametd.className="tdinput";
        newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductCount_SignItem_TD_Text_"+rowID+"\" value='0.00' class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_round(this,'"+point+"');TotalPrice_MoreUnit(this.id,"+rowID+");CountNum();\"/>";//添加列内容
    }
    if($("#HiddenMoreUnit").val()=="False")
    {
        var newFitDesctd = newTR.insertCell(10); //添加列:单价
        newFitDesctd.className = "cell";
        newFitDesctd.id = 'SignItem_TD_UnitPrice_' + rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />"; //添加列内容

    }
    else
    {
        var newFitDesctd=newTR.insertCell(10);//添加列:单价(根据基本单价计算,隐藏基本单价，比率)
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_UnitPrice_'+rowID;
        newFitDesctd.innerHTML = "<input type='hidden' id='baseprice_td"+rowID+"'><input type='hidden' id='BaseExRate"+rowID+"'><input name='chk' id='UnitPrice_SignItem_TD_Text_"+rowID+"'  type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />";//添加列内容
    }
    var newFitNametd = newTR.insertCell(11); //添加列:物品总价（金额）
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_" + rowID + "\" value=\"0\" class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />"; //添加列内容

    var newFitDesctd = newTR.insertCell(12); //添加列:备注
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容

}
//点击物品控件赋值
function Fun_FillParent_Content(id,ProNo,ProdName,UnitPrice,UnitID,HiddenUnitID,Specification,rowID,IsBatchNo,BatchNo) 
{
    //var idstr = popProductInfoObj.InputObj.substring(10, popProductInfoObj.InputObj.length);
//    var hiddenObj = 'Hidden_' + idstr;
//    var Price = 'UnitPrice_' + idstr;
//    var ProductNo = 'ProductNo_' + idstr;
//    var ProductName = 'ProductName_' + idstr;
//    var unitID = 'UnitID_' + idstr;
//    var specification = 'Specification_' + idstr;
    if(UnitPrice!="")UnitPrice=parseFloat(UnitPrice).toFixed($("#HiddenPoint").val());
    
    var hiddenObj = 'Hidden_SignItem_TD_Text_' + rowID;
    var Price = 'UnitPrice_SignItem_TD_Text_' + rowID;
    var ProductNo = 'ProductNo_SignItem_TD_Text_' + rowID;
    var ProductName = 'ProductName_SignItem_TD_Text_' + rowID;
    var unitID = 'UnitID_SignItem_TD_Text_' + rowID;
    var specification = 'Specification_SignItem_TD_Text_' + rowID;
    //alert(ProNo);
   // document.getElementById(ProductNo).value = ProNo;
    $("#" + ProductNo).val(ProNo);
    $("#" + ProductNo).attr("title", id);
    document.getElementById(ProductName).value = ProdName;
    document.getElementById(Price).value = UnitPrice;
    //document.getElementById(unitID).value = UnitID;
    document.getElementById(specification).value = Specification;
    
    //绑定批次
    var ListControlID="BatchNo_SignItem_TD_Text_"+rowID;
    var StorageControlID="ddlStorage";
    if(BatchNo!="undefined")
        GetBatchList(id,StorageControlID,ListControlID,IsBatchNo,BatchNo);
    else
        GetBatchList(id,StorageControlID,ListControlID,IsBatchNo);
    //多计量单位
    if($("#HiddenMoreUnit").val()=="True")
    {
       var BasePriceControl="baseprice_td"+rowID;
       var BaseUnitControl="BaseUnit_SignItem_TD_Text_"+rowID;
       $("#"+BaseUnitControl).val(UnitID);
       $("#" + BaseUnitControl).attr("title", HiddenUnitID);
       $("#"+BasePriceControl).val(UnitPrice);
       GetUnitGroupSelectEx(id,"StockUnit", "UnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,'',"FillContent("+rowID+","+UnitPrice+")");
    }//
    else
    {
        $("#"+unitID).val(UnitID);
        $("#" + unitID).attr("title", HiddenUnitID);
    }
    closeProductInfodiv();
}
//暂时不用
 function LoadUnitContent(rowid,usedunit)
 {
//   var exrate=$("#BaseExRate"+rowid).val();
//   var usedunitvalue=usedunit+"|"+exrate;
//   $("#UnitID_SignItem_TD_Text_"+rowid).val(usedunitvalue); /*比率*/
 }
 
//本行小计，，数量变动时根据比率算出基本数量
function TotalPrice_MoreUnit(id,rowid)
{
try{
    var idstr=id.substring(13,id.length);
    var count=id;
    var productcount=document.getElementById(count).value;
    var EXRate = $("#UnitID_SignItem_TD_Text_"+rowid).val().split('|')[1].toString(); /*比率*/
    var AcCount = $("#ProductCount_SignItem_TD_Text_"+rowid).val(); /*报损数量*/
    var unitprice=document.getElementById('UnitPrice_'+idstr).value;
    document.getElementById('TotalPrice_'+idstr).value=(productcount*unitprice).toFixed($("#HiddenPoint").val());
    if(EXRate!="0")
        document.getElementById('BaseCount_SignItem_TD_Text_'+rowid).value=(AcCount*EXRate).toFixed($("#HiddenPoint").val());
  }catch(Error){}
}
function TotalPrice(id) {
    var idstr = id.substring(13, id.length);
    var count = 'ProductCount_' + idstr;
    var productcount = document.getElementById(count).value;
    var unitprice = document.getElementById('UnitPrice_' + idstr).value;
    document.getElementById('TotalPrice_' + idstr).value = (productcount * unitprice).toFixed($("#HiddenPoint").val());
}
//选择单位时计算
 function ChangeUnit(ObjID/*下拉列表控件（单位）*/,RowID/*行号*/,UnitPrice/*基本单价*/)
 {
    var EXRate = $("#UnitID_SignItem_TD_Text_"+RowID).val().split('|')[1].toString(); /*比率*/
    var ProductCount = $("#ProductCount_SignItem_TD_Text_"+RowID).val();/*报损数量*/
    
    if (EXRate != '')
    {
        var tempcount=parseFloat(ProductCount*EXRate).toFixed($("#HiddenPoint").val());
        var tempprice=parseFloat(UnitPrice/EXRate).toFixed($("#HiddenPoint").val());
        $("#BaseCount_SignItem_TD_Text_"+RowID).val(tempcount);/*基本数量根据报损数量和比率算出*/
        $("#UnitPrice_SignItem_TD_Text_"+RowID).val(tempprice);/*单价根据基本单价和比率算出*/
        $("#TotalPrice_SignItem_TD_Text_"+RowID).val(parseFloat(ProductCount*tempprice).toFixed($("#HiddenPoint").val()));/*金额*/
    }
     CountNum();
 }
 ////////点击物品后填充单价根据基本单价和比率
 function FillContent(RowID,UnitPrice)
 {
     var EXRate = $("#UnitID_SignItem_TD_Text_"+RowID).val().split('|')[1].toString(); /*比率*/
     if (EXRate != '')
     {
        var tempprice=parseFloat(UnitPrice/EXRate).toFixed($("#HiddenPoint").val());
        $("#UnitPrice_SignItem_TD_Text_"+RowID).val(tempprice);/*单价根据基本单价和比率算出*/
     }
     CountNum();
 }
//列表进入查看页面填充明细
function FillSignRow(i, ProductID, ProductNo
        , ProductName, Specification, UnitID,HiddenUnitID, UnitPrice
        , ProductCount, Remark
        ,BatchNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,IsBatchNo) 
        { //读取最后一行的行号，存放在txtTRLastIndex文本框中 
       // alert(HiddenUnitID);alert(BatchNo);alert(UsedUnitID);alert(UsedUnitCount);alert(UsedPrice);alert(ExRate);alert(IsBatchNo);
        if(UsedPrice=="")UsedPrice="0.00";
        if(UsedUnitID=="")UsedUnitID="0.00";
        if(UsedUnitCount=="")UsedUnitCount="0.00";
        
        if(ProductCount=="")ProductCount="0.00";
        if(UnitPrice=="")UnitPrice="0.00";
        
        if(ExRate=="")ExRate="0.00";
        if(UnitPrice!="")UnitPrice=parseFloat(UnitPrice).toFixed($("#HiddenPoint").val());
        if(ProductCount!="")ProductCount=parseFloat(ProductCount).toFixed($("#HiddenPoint").val());
        if(UsedUnitCount!="")UsedUnitCount=parseFloat(UsedUnitCount).toFixed($("#HiddenPoint").val());
        if(UsedPrice!="")UsedPrice=parseFloat(UsedPrice).toFixed($("#HiddenPoint").val());
    var signFrame = findObj("dg_Log", document);
    var rowID = signFrame.rows.length;
    var newTR = signFrame.insertRow(rowID); //添加行
    newTR.id = "SignItem_Row_" + rowID;

    var newNameXH = newTR.insertCell(0); //添加列:选择
    newNameXH.className = "cell";
    newNameXH.id = 'SignItem_TD_Check_' + rowID;
    newNameXH.innerHTML = "<input name='chk1' id='chk_Option_" + rowID + "' value=\"\" type='checkbox' onclick=\"IfSelectAll('chk1','checkall');\"/>";

    var newNameTD = newTR.insertCell(1); //添加列:序号
    newNameTD.className = "cell";
    newNameTD.id = 'SignItem_TD_Index_' + rowID;
    newNameTD.innerHTML = newTR.rowIndex.toString();

    var newFitNametd = newTR.insertCell(2); //添加列:物品编号
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_ProductNo_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"" + ProductNo + "\" title=\"" + ProductID + "\" class=\"tdinput\" onclick=\"popProductInfoObj.ShowList('SignItem_TD_Text_" + rowID + "');\"  readonly=\"readonly\" style='width:90%'  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(3); //添加列:物品名称
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ProductName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ProductName_SignItem_TD_Text_" + rowID + "' value=\"" + ProductName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    //增加批次列：2010.04.12
    var newBatchNotd=newTR.insertCell(4);//添加列:批次
    newBatchNotd.className="cell";
    newBatchNotd.id = 'SignItem_TD_BatchNo_'+rowID;
    newBatchNotd.innerHTML ="<select style=' width:90%;' id='BatchNo_SignItem_TD_Text_"+rowID+"' />";//添加列内容
    /*绑定批次*/
    var ListControlID="BatchNo_SignItem_TD_Text_"+rowID;
    var StorageControlID="ddlStorage";
    GetBatchList(ProductID,StorageControlID,ListControlID,IsBatchNo,BatchNo);


    var newFitDesctd = newTR.insertCell(5); //添加列:物品规格
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Specification_' + rowID;
    newFitDesctd.innerHTML = "<input id='Specification_SignItem_TD_Text_" + rowID + "' value=\"" + Specification + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    //增加基本单位、基本数量列：2010.04.12
    var newBaseUnit=newTR.insertCell(6);//添加列:基本单位
    newBaseUnit.className="cell";
    newBaseUnit.id = 'SignItem_TD_BaseUnit_'+rowID;
    newBaseUnit.innerHTML ="<input id='BaseUnit_SignItem_TD_Text_"+rowID+"' value=\""+UnitID+"\" title=\""+HiddenUnitID+"\" type='text' class=\"tdinput\"  style='width:90%' />";;//添加列内容
    
    var newBaseCount=newTR.insertCell(7);//添加列：基本数量
    newBaseCount.className="cell";
    newBaseCount.id = 'SignItem_TD_BaseCount_'+rowID;
    newBaseCount.innerHTML ="<input id='BaseCount_SignItem_TD_Text_"+rowID+"' value=\""+ProductCount+"\" type='text' class=\"tdinput\"  style='width:90%' />";;//添加列内容
   
   if($("#HiddenMoreUnit").val()=="False")
    {
        newBaseUnit.style.display="none"
        newBaseCount.style.display="none"   
        var newFitNametd = newTR.insertCell(8); //添加列:单位
        newFitNametd.className = "cell";
        newFitNametd.id = 'SignItem_TD_UnitID_' + rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_" + rowID + "\" value=\"" + UnitID + "\" title=\""+HiddenUnitID+"\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容


    }
    else
    {
        $("#baseuint").show();
        $("#basecount").show();
        var newFitNametd=newTR.insertCell(8);//添加列:单位
        newFitNametd.className="cell";
        newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
        newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
    }
    var point=$("#HiddenPoint").val();
    if($("#HiddenMoreUnit").val()=="False")
        {
            var newFitNametd = newTR.insertCell(9); //添加列:报损数量
            newFitNametd.className = "tdinput";
            newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
            newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductCount_SignItem_TD_Text_" + rowID + "\" value=\"" + ProductCount + "\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_round(this,'"+point+"');TotalPrice(this.id);CountNum();\"/>"; //添加列内容

        }
        else
        {
            var newFitNametd=newTR.insertCell(9);//添加列:报损数量
            newFitNametd.className="tdinput";
            newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
            newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductCount_SignItem_TD_Text_"+rowID+"\" value=\""+UsedUnitCount+"\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_round(this,'"+point+"');TotalPrice_MoreUnit(this.id,"+rowID+");CountNum();\"/>";//添加列内容

        }
        if($("#HiddenMoreUnit").val()=="False")
        {
            var newFitDesctd = newTR.insertCell(10); //添加列:单价
            newFitDesctd.className = "cell";
            newFitDesctd.id = 'SignItem_TD_UnitPrice_' + rowID;
            newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' value=\"" + UnitPrice + "\" type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />"; //添加列内容

        }
        else
        {
            UsedPrice=(parseFloat(UsedPrice)).toFixed($("#HiddenPoint").val());
            var newFitDesctd=newTR.insertCell(10);//添加列:出库单价(根据基本单价计算,隐藏基本单价，比率)
            newFitDesctd.className="cell";
            newFitDesctd.id = 'SignItem_TD_UnitPrice_'+rowID;
            newFitDesctd.innerHTML = "<input type='hidden' value=\""+UnitPrice+"\" id='baseprice_td"+rowID+"'><input type='hidden' id='BaseExRate"+rowID+"' value=\""+ExRate+"\"><input name='chk' id='UnitPrice_SignItem_TD_Text_"+rowID+"' value=\""+UsedPrice+"\"  type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />";//添加列内容
        }
        if($("#HiddenMoreUnit").val()=="True")//绑定单位组
            GetUnitGroupSelectEx(ProductID,"StockUnit", "UnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,UsedUnitID,"LoadUnitContent("+rowID+","+UsedUnitID+")");//

    var newFitNametd = newTR.insertCell(11); //添加列:物品总价（金额）
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_" + rowID + "\" value=\"\" class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />"; //添加列内容

    var productcount = document.getElementById("ProductCount_SignItem_TD_Text_" + rowID).value; //计算总额
    var unitprice = document.getElementById("UnitPrice_SignItem_TD_Text_" + rowID).value;
    document.getElementById("TotalPrice_SignItem_TD_Text_" + rowID).value = (productcount * unitprice).toFixed($("#HiddenPoint").val());

    var newFitDesctd = newTR.insertCell(12); //添加列:备注
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_" + rowID + "' value=\"" + Remark + "\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容

}



//---------Start  明细处理 ----------
function findObj(theObj, theDoc) {
    var p, i, foundObj;
    if (!theDoc) theDoc = document;
    if ((p = theObj.indexOf("?")) > 0 && parent.frames.length) {
        theDoc = parent.frames[theObj.substring(p + 1)].document;
        theObj = theObj.substring(0, p);
    }

    if (!(foundObj = theDoc[theObj]) && theDoc.all) foundObj = theDoc.all[theObj];
    for (i = 0; !foundObj && i < theDoc.forms.length; i++)
        foundObj = theDoc.forms[i][theObj]; for (i = 0; !foundObj && theDoc.layers && i < theDoc.layers.length; i++)
        foundObj = findObj(theObj, theDoc.layers[i].document);
    if (!foundObj && document.getElementById) foundObj = document.getElementById(theObj);
    return foundObj;
}


function SelectStorage(id, value) {
    document.getElementById('StorageID_' + id).value = value;
}





///提交审批后的返回状态
function Fun_FlowApply_Operate_Succeed(operateType) {
    if (operateType == "0")//提交审批完成的判断
    {
        $("#btn_save").hide();
        $("#btn_UnClick_bc").show();
    }
    else if (operateType == "1")//审批成功时的判断
    {
        $("#btn_save").hide();
        $("#btn_UnClick_bc").show();
    }
    else if (operateType == "3")//撤销审批
    {
        $("#btn_save").show();
        $("#btn_UnClick_bc").hide();
    }
}
/*----------------------------------Start 确认-------------------------------------------*/
function Fun_ConfirmOperate() {
    if (confirm('确认之后不可修改，你确定要确认吗？')) {
        var StorageID = $("#ddlStorage").val();
        var txtIndentityID = $("#txtIndentityID").val();
        var act = "Confirm";
        var UrlParam = "&Act=Confirm\
                            &ID=" + txtIndentityID.toString() + "&StorageID=" + StorageID;
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageLossAdd.ashx?" + UrlParam,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            complete: function() { hidePopup(); },
            error: function() {
                popMsgObj.ShowMsg('请求发生错误');

            },
            success: function(data) {
                if (data.sta == 1) {
                    var reInfo = data.data.split('|');
                    if (reInfo.length > 1) {
                        document.getElementById('txtModifiedUserID').value = reInfo[0];
                        document.getElementById('txtModifiedDate').value = reInfo[1];
                        document.getElementById('txtConfirmor').value = reInfo[0];
                        document.getElementById('txtConfirmDate').value = reInfo[1];
                    }
                    popMsgObj.ShowMsg(data.info);
                    document.getElementById('sltBillStatus').value = 2;
                    try {
                        $("#btn_save").hide();
                        $("#btn_UnClick_bc").show();
                    }
                    catch (e) { }
                    $("#hiddBillStatus").val('2');
                    GetFlowButton_DisplayControl();
                }
            }
        });
    }
    else {
        return false;
    }
}



/*----------------------------------Start 取消确认-------------------------------------------*/
function Fun_UnConfirmOperate() {
    if (confirm('确认要进行取消确认操作吗')) {
        var StorageID = $("#ddlStorage").val();
        var txtIndentityID = $("#txtIndentityID").val();
        var act = "Confirm";
        var UrlParam = "&Act=UnConfirm\
                            &ID=" + txtIndentityID.toString() + "&StorageID=" + StorageID;
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageLossAdd.ashx?" + UrlParam,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            complete: function() { hidePopup(); },
            error: function() {
                popMsgObj.ShowMsg('请求发生错误');

            },
            success: function(data) {
                if (data.sta == 1) {
                    var reInfo = data.data.split('|');
                    if (reInfo.length > 1) {
                        document.getElementById('txtModifiedUserID').value = reInfo[0];
                        document.getElementById('txtModifiedDate').value = reInfo[1];
                        document.getElementById('txtConfirmor').value = reInfo[0];
                        document.getElementById('txtConfirmDate').value = reInfo[1];
                    }
                    popMsgObj.ShowMsg(data.info);
                    document.getElementById('sltBillStatus').value = 1;
                    try {
                        $("#btn_save").show();
                        $("#btn_UnClick_bc").hide();
                    }
                    catch (e) { }
                    $("#hiddBillStatus").val('1');
                    GetFlowButton_DisplayControl();
                }
            }
        });
    }
    else {
        return false;
    }
}



/*-------------------------结单和取消结单---Start-------------------------------*/

//结单或取消结单按钮操作
function Fun_CompleteOperate(isComplete) {
    var Act = "Close";
    var msgInfo = isComplete ? "确认要进行结单操作吗？" : "确认要进行取消结单操作吗？";
    if (isComplete) {
        Act = "Close";
    }
    else {
        Act = "CancelClose";
    }
    //结单操作

    var Action = 'Edit';
    var txtIndentityID = $("#txtIndentityID").val();
    var UrlParam = "&Act=" + Act +
                        "&ID=" + txtIndentityID.toString();

    if (window.confirm(msgInfo)) {
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageLossInfo.ashx?" + UrlParam,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
                popMsgObj.ShowMsg('结单或取消结单时请求发生错误');
            },
            success: function(data) {
                if (data.sta == 1) {
                    var reInfo = data.data.split('|');
                    if (reInfo.length > 1) {
                        document.getElementById('txtModifiedUserID').value = reInfo[0];
                        document.getElementById('txtModifiedDate').value = reInfo[1];
                        document.getElementById('txtCloser').value = reInfo[0];
                        document.getElementById('txtCloseDate').value = reInfo[1];
                    }
                    if (Act == "Close") {
                        document.getElementById('sltBillStatus').value = 4;
                        $("#hiddBillStatus").val('4');
                    }
                    else if (Act == "CancelClose") {
                        document.getElementById('sltBillStatus').value = 2;
                        $("#hiddBillStatus").val('2');
                    }
                    GetFlowButton_DisplayControl();
                }
                popMsgObj.ShowMsg(data.info);
            }
        });
    }
}

/*-------------------------结单和取消结单--End--------------------------------*/

//删除明细中所有数据
function fnDelRow() {
    var dg_Log = findObj("dg_Log", document);
    var rowscount = dg_Log.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除
        dg_Log.deleteRow(i);
    }
    var txtTRLastIndex = findObj("txtTRLastIndex", document); //重置最后行号为
    txtTRLastIndex.value = "0";
}


/*-----------------明细中输入数据格式验证---------------------*/
function check(str) {
    fieldText = ""; var msgText = ""; var isFlag = true;
    if (document.getElementById(str).value.length > 0) {
        if (!IsNumeric(document.getElementById(str).value)) {
            isFlag = false;
            fieldText = fieldText + "";
            msgText = msgText + "报损数量输入有误,请输入有效的数值（大于0）！";
            document.getElementById(str).value = "0";
            document.getElementById(str).focus();
        }
    }
    if (!isFlag) {
        popMsgObj.ShowMsg(msgText);
    }

}

/*
* 附件处理
*/
function DealAttachment(flag) {
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
        //            //设置附件路径
        //            document.getElementById("hfPageAttachment").value = "";
        //            //下载删除不显示
        //            document.getElementById("divDealAttachment").style.display = "none";
        //            //上传显示
        //            document.getElementById("divUploadAttachment").style.display = "";
        var FilePath = document.getElementById("hfPageAttachment").value;
        var FileName = document.getElementById("attachname").innerHTML;
        DeleteUploadFile(FilePath, FileName);
    }
    //下载附件
    else if ("download" == flag) {
        //获取附件路径
        attachUrl = document.getElementById("hfPageAttachment").value;
        //下载文件
        window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + attachUrl, "_blank");

    }
}

/*
* 上传文件后回调处理
*/
function AfterUploadFile(url) {
    if (url != "") {
        //设置附件路径
        document.getElementById("hfPageAttachment").value = url;
        //下载删除显示
        document.getElementById("divDealAttachment").style.display = "";
        //上传不显示
        document.getElementById("divUploadAttachment").style.display = "none";
        var lastIndex = url.lastIndexOf('\\');
        var testurl = "";
        if (document.all) {
            testurl = url.substring(lastIndex + 1, url.length);
        }
        else {
            testurl = url.substring(lastIndex + 1, url.length + 1);
        }
//        alert(parseInt(url.length) + 1)
//        alert(url.substring(lastIndex + 1, 29))
//        alert(url.substring(lastIndex + 1, url.length + 1));
        document.getElementById('attachname').innerHTML = testurl;

    }
}

/*
* 返回按钮
*/
function DoBack() {
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    ModuleID = document.getElementById("hidModuleID").value;
    intFromType = document.getElementById('intFromType').value;
    if (intFromType == 1) {
        window.location.href = "StorageLossList.aspx?ModuleID=" + ModuleID + searchCondition;
    }
    if (intFromType == 2) {
        window.location.href = '../../../DeskTop.aspx';
    }
    if (intFromType == 3) {
        window.location.href = '../../Personal/WorkFlow/FlowMyApply.aspx?ModuleID=' + ModuleID;
    }
    if (intFromType == 4) {
        window.location.href = '../../Personal/WorkFlow/FlowMyProcess.aspx?ModuleID=' + ModuleID;
    }
    if (intFromType == 5) {
        window.location.href = '../../Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID=' + ModuleID;
    }

}

/*单据打印*/
function BillPrint() {
    var ID = $("#txtIndentityID").val();
    if (ID == "" || ID == "0") {
        popMsgObj.Show("打印|", "请保存单据再打印|");
        return;
    }
    // if(confirm("请确认您的单据已经保存？"))
    window.open("../../../Pages/PrinttingModel/StorageManager/StorageLossPrint.aspx?ID=" + ID);
}


function DoChange() {
    fnDelRow();
    CloseBarCodeDiv();
}


/*-----------------------------------------------------条码扫描Start-----------------------------------------------------------------*/

var rerowID = "";
//判断是否有相同记录有返回true，没有返回false
function IsExist(prodNo) {
    var signFrame = document.getElementById("dg_Log");
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 1; i < signFrame.rows.length; ++i) {
        var prodNo1 = document.getElementById("ProductNo_SignItem_TD_Text_" + i).value;

        if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
            rerowID = i;
            return true;
        }
    }
    return false;
}
//条码扫描方法
function GetGoodsDataByBarCode(id, ProNo, ProdName,
                                a, UnitID, UnitName,
                                b, c, d,
                                Specification, e, f,
                                g, h, i,
                                UnitPrice,IsBatchNo) {
    if (!IsExist(ProNo))//如果重复记录，就不增加
    {
        
        AddRow();
        var signFrame = findObj("dg_Log", document);
        var rowID = signFrame.rows.length;
        Fun_FillParent_Content(id,ProNo,ProdName,UnitPrice,UnitName,UnitID,Specification,rowID-1,IsBatchNo);
    }
    else {
        document.getElementById("ProductCount_SignItem_TD_Text_" + rerowID).value = parseFloat(document.getElementById("ProductCount_SignItem_TD_Text_" + rerowID).value) + 1;
        TotalPrice("ProductCount_SignItem_TD_Text_" + rerowID);
        CountNum();
    }

}
function Showtmsm() {
    var StoID = $("#ddlStorage").val();
    GetGoodsInfoByBarCode(StoID);
}
//库存快照
function ShowSnapshot() {

    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';

    var signFrame = findObj("dg_Log", document);
    var txtTRLastIndex = signFrame.rows.length;


    if (txtTRLastIndex > 1) {
        for (var i = 1; i < txtTRLastIndex; i++) {
            if (document.getElementById('chk_Option_' + i).checked) {
                detailRows++;
                intProductID = document.getElementById('ProductNo_SignItem_TD_Text_' + (i)).title;
                snapProductName = document.getElementById('ProductName_SignItem_TD_Text_' + (i)).value;
                snapProductNo = document.getElementById('ProductNo_SignItem_TD_Text_' + (i)).value;
            }
        }

        if (detailRows == 1) {
            if (intProductID <= 0 || strlen(cTrim(intProductID, 0)) <= 0) {
                popMsgObj.ShowMsg('选中的明细行中没有添加物品');
                return false;
            }

            ShowStorageSnapshot(intProductID, snapProductName, snapProductNo);
        } else {
            popMsgObj.ShowMsg('请选择单个物品查看库存快照');
            return false;
        }
    }
    else if (txtTRLastIndex == 1) {
        popMsgObj.ShowMsg('请选择单个物品查看库存快照');
        return false;
    }
}
