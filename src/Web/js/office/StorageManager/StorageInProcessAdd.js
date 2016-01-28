//---------Start 生产入库详细信息和明细列表 --------
var InNoID = document.getElementById('txtIndentityID').value;
var GlbDetailID = new Array();

$(document).ready(function() {

    if (InNoID > 0) {

        LoadDetailInfo(InNoID);
        document.getElementById("t_Edit").style.display = "";
    }
    else {
        GetExtAttr('officedba.StorageInProcess', null);
        document.getElementById("t_Add").style.display = "";
        try {
            $("#btn_save").show();
            $("#btnPageFlowConfrim").show();
            $("#btn_Unclic_Close").show();
            $("#btn_Unclick_CancelClose").show();
        }
        catch (e) { }
    }
});
function LoadDetailInfo(InNoID) {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/StorageManager/StorageInProcessInfo.ashx?Act=&ID=" + InNoID, //目标地址
        cache: false,
        success: function(msg) {
            //数据获取完毕，填充页面据显示
            document.getElementById('lbInNo').innerHTML = msg.data[0].InNo;
//            if(document.getElementById("hidBatchNo").value == true)
//            {
                 document.getElementById("divBatchNo").style.display = "none";            
                //document.getElementById('divBatchNoShow').innerHTML = msg.data[0].BatchNo;
            //}            
            
            document.getElementById('txtTitle').value = msg.data[0].Title;
            document.getElementById('ddlFromType').value = msg.data[0].FromType;
            document.getElementById('txtFromBillID').value = msg.data[0].TaskNo; //生产任务单（原单编号）
            $("#txtFromBillID").attr("title", msg.data[0].FromBillID); //

            $("#ddlProcessType").val(msg.data[0].ProcessType); //加工类别
            $("#txtProcessDept").val(msg.data[0].ProcessDeptName); //加工部门
            $("#txtProcessor").val(msg.data[0].ProcessorName); //负责人
            document.getElementById('DeptName').value = msg.data[0].InPutDeptName; //入库部门
            $("#txtDeptID").val(msg.data[0].InPutDept); //入库部门ID

            document.getElementById('UserExecutor').value = msg.data[0].ExecutorName;
            $("#txtExecutorID").val(msg.data[0].Executor); //入库人ID
            document.getElementById('txtEnterDate').value = msg.data[0].EnterDate;

            document.getElementById('txtSummary').value = msg.data[0].Summary;
            document.getElementById('txtTotalPrice').value = NumberSetPoint(msg.data[0].A_TotalPrice);//.slice(0, msg.data[0].A_TotalPrice.length - 2); //入库金额合计
            document.getElementById('txtTotalCount').value = NumberSetPoint(msg.data[0].CountTotal);
            document.getElementById('txtRemark').value = msg.data[0].Remark;

             document.getElementById('txtCanUserID').value = msg.data[0].CanViewUser;
            document.getElementById('txtCanUserName').value = msg.data[0].CanViewUserName;
            
            document.getElementById('txtCreator').value = msg.data[0].CreatorName;
            document.getElementById('txtCreateDate').value = msg.data[0].CreateDate;
            document.getElementById('sltBillStatus').value = msg.data[0].BillStatus;
            document.getElementById('txtConfirmor').value = msg.data[0].ConfirmorName;
            document.getElementById('txtConfirmDate').value = msg.data[0].ConfirmDate;
            document.getElementById('txtCloser').value = msg.data[0].CloserName;
            document.getElementById('txtCloseDate').value = msg.data[0].CloseDate;
            document.getElementById('txtModifiedUserID').value = msg.data[0].ModifiedUserName;
            document.getElementById('txtModifiedDate').value = msg.data[0].ModifiedDate;
            document.getElementById('txtIndentityID').value = msg.data[0].ID;

            GetExtAttr('officedba.StorageInProcess', msg.data[0]); //获取扩展属性并填充值

            $.each(msg.data, function(i, item) {
                if (item.ProductID != null && item.ProductID != '') {
                    if(msg.data[i].BatchNo != "")
                     {
                            document.getElementById("divBatchNo").style.display = "none";
                            document.getElementById('divBatchNoShow').innerHTML = msg.data[i].BatchNo;
                     }
                    FillSignRow(i, item.SortNo, item.InNo, item.ProductID, item.ProductNo, item.ProductName, item.Specification,item.UnitID,
                     item.UnitPrice, item.B_TotalPrice, item.ProductCount, item.DetaiRemark, item.TaskNo,item.FromLineNo, item.StorageID,
                      item.NotInCount, item.FromBillCount, item.InCount,item.UsedUnitID,item.UsedUnitCount,item.UsedPrice,item.ExRate,item.IsBatchNo);
                }

            });
            if (document.getElementById('sltBillStatus').value == 1)//根据单据状态显示按钮
            {
                try {
                    $("#btn_save").show();
                    $("#Confirm").show();
                    $("#btn_Unclic_Close").show();
                    $("#btn_Unclick_CancelClose").show();
                }
                catch (e) { }
            }
            else if (document.getElementById('sltBillStatus').value == 2) {
                try {
                    $("#btn_save").hide();
                    $("#Confirm").hide();
                    $("#btn_UnClick_bc").show();
                    $("#btnPageFlowConfrim").show();
                    $("btn_Unclick_CancelClose").show();
                    $("#btn_Close").show();
                    $("#btn_Unclic_Close").hide();
                    $("#btn_CancelClose").hide();
                    $("#btn_Unclick_CancelClose").show();
                }
                catch (e) { }
            }
            else {
                try {
                    $("#btn_save").hide();
                    $("#Confirm").hide();
                    $("#btn_UnClick_bc").show();
                    $("#btnPageFlowConfrim").show();
                    $("#btn_Close").hide();
                    $("#btn_Unclic_Close").show();
                    $("#btn_CancelClose").show();
                    $("#btn_Unclick_CancelClose").hide();
                }
                catch (e) { }
            }
        },
        error: function() { },
        complete: function() { }
    });
}

//---------End 生产完工详细信息和明细列表 ----------

//---------Start 生产完工入库保存 ----------
function Fun_Save_StorageInProcess() {
    CountNum();
    var bmgz = "";
    var pcgz = "";
    var Flag = true;
    var txtExecutor = $("#txtExecutor").val();
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var BratchNo = "null";

    var txtIndentityID = $("#txtIndentityID").val();
    if (txtIndentityID == "0")//新建
    {
        if ($("#txtInNo_ddlCodeRule").val() == "")//手工输入
        {
            txtInNo = $("#txtInNo_txtCode").val();
            bmgz = "sd";
            if (txtInNo == "") {
                isFlag = false;
                fieldText += "单据编号|";
                msgText += "请输入单据编号|";
            }
            else if (!CodeCheck($("#txtInNo_txtCode").val())) {
                isFlag = false;
                fieldText = fieldText + "单据编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
        else {

            txtInNo = $("#txtInNo_ddlCodeRule").val();
            bmgz = "zd";
        }
        
//        if($("#hidBatchNo").val() == "true")//启用批次
//        {
            
                if($("#BatchRuleControl1_ddlBatchRule").val() == "")//手工输入批次
                {
                    BratchNo = $("#BatchRuleControl1_txtBatch").val();
                    
                    if(BratchNo =="")
                    {
                        isFlag = false;
                        fieldText += "批次|";
                        msgText += "请输入批次|";
                    }
                    else if (!CodeCheck($("#BatchRuleControl1_txtBatch").val()))
                    {
                        isFlag = false;
                        fieldText = fieldText + "批次|";
                        msgText = msgText + "批次只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
                    }
                }
                else//自动生成批次
                {
                    BratchNo = $("#BatchRuleControl1_ddlBatchRule").val();
                    pcgz = "zd";
                }
//        }
//        else//未启用批次
//        {}
    }
    else {
        txtInNo = document.getElementById("lbInNo").innerHTML;
        BratchNo = document.getElementById("divBatchNoShow").innerHTML;
    }
    var txtTitle = $("#txtTitle").val();
    var ddlFromType = $("#ddlFromType").val();
    var txtFromBillID = $("#txtFromBillID").attr("title"); //
    var ddlProcessType = $("#ddlProcessType").val();
    var txtProcessDept = $("#txtProcessDept").attr("title");
    var txtProcessor = $("#txtProcessor").attr("title");
    var txtTotalPrice = $("#txtTotalPrice").val();
    var txtTotalCount = $("#txtTotalCount").val();
    var txtInPutDept = $("#txtDeptID").val(); //入库部门
    var txtExecutor = $("#txtExecutorID").val(); //入库人
    var txtEnterDate = $("#txtEnterDate").val(); //入库时间
    var txtSummary = $("#txtSummary").val();
    var txtRemark = $("#txtRemark").val();
    var sltBillStatus = $("#sltBillStatus").val();
    var hiddenValue = $("#txtInNoHidden").val();
    var CanUserID = $("#txtCanUserID").val();//可查看人ID
    var CanUserName = $("#txtCanUserName").val();//可查看人姓名

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

    if (strlen(txtInNo) > 50) {
        isFlag = false;
        fieldText = fieldText + "单据编号|";
        msgText = msgText + "仅限于50个字符以内|";
    }

    if (strlen(txtTitle) > 100) {
        isFlag = false;
        fieldText = fieldText + "主题|";
        msgText = msgText + "仅限于100个字符以内|";
    }

    if (document.getElementById("txtFromBillID").value == "") {
        isFlag = false;
        fieldText = fieldText + "生产任务单|";
        msgText = msgText + "请选择生产任务单|";
    }

    if (txtExecutor == "") {
        Flag = false;
        fieldText = fieldText + "入库人|";
        msgText = msgText + "请选择入库人|";
    }

    if (txtEnterDate == "") {
        isFlag = false;
        fieldText = fieldText + "入库时间|";
        msgText = msgText + "请选择入库时间|";
    }
    if(txtEnterDate.length>0)
    {
        if(StringToDate(txtEnterDate)>StringToDate(GetNow()))
        {
            isFlag = false;
            fieldText = fieldText + "入库时间|";
   		    msgText = msgText +  "入库时间需小于等于当前日期|";
        }
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

    if ($("#StorageID_SignItem_TD_Text_1").val() == null) {
        isFlag = false;
        fieldText = fieldText + "明细仓库|";
        msgText = msgText + "请首先创建仓库|";
    }

    //明细验证
    var signFrame = findObj("dg_Log", document);
    if (signFrame.rows.length <= 1) {
        isFlag = false;
        fieldText = fieldText + "明细信息|";
        msgText = msgText + "明细信息不能为空|";
    }
    var ifProductCount = "0";
    //var ifProductC = "0";
    var ifRemark = "0";
    var ifUnitprice="0";
    var ifbig = "0"; //是否大于应收数量
    var rownum = null; //第几行大于源单数量
    for (i = 1; i < signFrame.rows.length; i++) {
        var ProductCount;
        if(document.getElementById("hidMoreUnit").value == "true")
        {
            ProductCount = "OutCount_SignItem_TD_Text_" + i;
        }
        else
        {
            ProductCount = "ProductCount_SignItem_TD_Text_" + i;
        }

        var NotInCount = "NotInCount_SignItem_TD_Text_" + i;
        var Remark = "Remark_SignItem_TD_Text_" + i;
            var UnitPrice = "UnitPrice_SignItem_TD_Text_" + i;
        if (document.getElementById(ProductCount).value == "" || parseFloat(document.getElementById(ProductCount).value) <= 0 || document.getElementById(ProductCount).value == "0.00") {
            ifProductCount = "1";
        }
        
        if (IsDisplayPrice!="none"&&(document.getElementById(UnitPrice).value == "" || parseFloat(document.getElementById(UnitPrice).value) <0 )) {
            ifUnitprice = "1";
        }
        
        if(IsDisplayPrice!="none" && document.getElementById("hidZero").value == "False" && parseFloat(document.getElementById(UnitPrice).value) == 0)
        {
            ifUnitprice = "1";
        }
        
        if (strlen($("#" + Remark).val()) > 200) {
            ifRemark = "1";
        }
    }

    for (i = 1; i < signFrame.rows.length; i++) {
        var ProductCount;
        if(document.getElementById("hidMoreUnit").value == "true")
        {
            ProductCount = "OutCount_SignItem_TD_Text_" + i;
        }
        else
        {
            ProductCount = "ProductCount_SignItem_TD_Text_" + i;
        }        
        var NotInCount = "NotInCount_SignItem_TD_Text_" + i;
        
        if (parseFloat(document.getElementById(ProductCount).value) > parseFloat(document.getElementById(NotInCount).value)) {
            ifbig = "1"; rownum = i;
            break;
        }
    }

    if (ifUnitprice == "1") {
        isFlag = false;
        fieldText = fieldText + "入库单明细:单价|";
        msgText = msgText + "请输入有效的数值（大于0）|";
    }
    
    if (ifProductCount == "1") {
        isFlag = false;
        fieldText = fieldText + "数量|";
        msgText = msgText + "请输入有效的数值（大于0）|";
    }
 
    if (ifbig == "1") {
        isFlag = false;
        fieldText = fieldText + "入库数量|";
        msgText = msgText + "第" + rownum + "行入库数量不能大于源单未入库数量|";
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
            var StorageIDi = "StorageID_SignItem_TD_Text_" + i;
            var StorageIDj = "StorageID_SignItem_TD_Text_" + j;
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

        //期初库存明细

        var DetailProductID = new Array();
        var DetailSortNo = new Array(); //序号
        var DetailStorageID = new Array();
        var DetailFromLineNo = new Array();
        var DetailProductCount = new Array();
        var DetailUnitPrice = new Array();
        var DetailTotalPrice = new Array();
        var DetailRemark = new Array();
        
        var DetailUnitZ = new Array();//单位z
        var DetailProductCountZ = new Array();//数量
        var DetailUsedPrice = new Array();//单价
        var DetailExRate = new Array();//单位换算率
        var DetailBatchNo = new Array();//批次

        //var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
        var signFrame = findObj("dg_Log", document);
        var count = signFrame.rows.length; //有多少行
        for (var i = 1; i < count; i++) {
            if (signFrame.rows[i].style.display != "none") {
                //var objProductID = 'Hidden_SignItem_TD_Text_'+(i+1);
                var objProductNo = 'ProductNo_SignItem_TD_Text_' + (i);
                var objSortNo = 'SignItem_TD_Index_' + (i);
                var objStorageID = 'StorageID_SignItem_TD_Text_' + (i);
                var objProductCount = 'ProductCount_SignItem_TD_Text_' + (i);
                var objUnitPrice = 'JiBen_DanJia_' + (i);
                var objTotalPrice = 'TotalPrice_SignItem_TD_Text_' + (i);
                var objRemark = 'Remark_SignItem_TD_Text_' + (i);
                var objFromLineNo = 'FromLineNo_SignItem_TD_Text_' + (i);
                
                 var objUnitZ = 'DetailUnitID_SignItem_TD_Text_' + (i);//单位z
                var objProductCountZ = 'OutCount_SignItem_TD_Text_' + (i);//数量
                var objUsedPrice = 'UnitPrice_SignItem_TD_Text_' + (i);//单价
                var objBatchNo = 'BatchNo_SignItem_TD_Text_' + (i);

                DetailProductID.push($("#" + objProductNo).attr("title"));
                DetailSortNo.push(document.getElementById(objSortNo.toString()).innerHTML);
                DetailStorageID.push(document.getElementById(objStorageID.toString()).value);
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value);
                DetailProductCount.push(document.getElementById(objProductCount.toString()).value);
                DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value);
                DetailTotalPrice.push(document.getElementById(objTotalPrice.toString()).value);
                DetailRemark.push(document.getElementById(objRemark.toString()).value);
                DetailBatchNo.push($("#" + objBatchNo).val());//用于存储批次是否启用的数组
                
                if(document.getElementById("hidMoreUnit").value == "true")//是否启用单据组
                {
                     DetailUnitZ.push(document.getElementById(objUnitZ.toString()).value.split('|')[0].toString());//单位
                    DetailProductCountZ.push(document.getElementById(objProductCountZ.toString()).value);//数量
                    var ReUsedPrice = document.getElementById(objUsedPrice.toString()).value;// * document.getElementById(objUnitZ.toString()).value.split('|')[1].toString();
                    DetailUsedPrice.push(ReUsedPrice);//实际单价
                    DetailExRate.push(document.getElementById(objUnitZ.toString()).value.split('|')[1].toString());//单位换算率
                }
               
            }
        }

        var UrlParam ="&txtInNo="+escape(txtInNo)+"&txtTitle="+escape(txtTitle)+"&ddlFromType="+escape(ddlFromType)+"&txtFromBillID="+escape(txtFromBillID);
                 UrlParam+="&txtTotalPrice="+escape(txtTotalPrice);
                           UrlParam+="&txtTotalCount="+escape(txtTotalCount);
                           UrlParam+="&txtInPutDept="+escape(txtInPutDept);
                           UrlParam+="&txtExecutor="+escape(txtExecutor);
                           UrlParam+="&txtEnterDate="+escape(txtEnterDate);
                           UrlParam+="&txtSummary="+escape(txtSummary);
                           UrlParam+="&txtRemark="+escape(txtRemark);
                           UrlParam+="&CanUserName="+escape(CanUserName);
                           UrlParam+="&CanUserID="+escape(CanUserID);
                           UrlParam+="&sltBillStatus="+escape(sltBillStatus);
                           UrlParam+="&DetailProductID="+escape(DetailProductID.toString());
                           UrlParam+="&DetailSortNo="+escape(DetailSortNo.toString());
                           UrlParam+="&DetailStorageID="+escape(DetailStorageID.toString());
                           UrlParam+="&DetailFromLineNo="+escape(DetailFromLineNo.toString());
                           UrlParam+="&DetailProductCount="+escape(DetailProductCount.toString());
                           UrlParam+="&DetailUnitPrice="+escape(DetailUnitPrice.toString());
                           UrlParam+="&DetailTotalPrice="+escape(DetailTotalPrice.toString());
                           UrlParam+="&DetailRemark="+escape(DetailRemark.toString());
                           UrlParam+="&Act=edit";
                           UrlParam+="&bmgz="+bmgz;                           
                           UrlParam+="&BratchNo="+escape(BratchNo.toString());
                           UrlParam+="&pcgz="+escape(pcgz.toString());
                           UrlParam+="&IsMoreUnit="+escape(document.getElementById("hidMoreUnit").value);
                           UrlParam+="&DetailBatchNo="+escape(DetailBatchNo.toString());
                           UrlParam+="&DetailUnitZ="+escape(DetailUnitZ.toString());
                           UrlParam+="&DetailProductCountZ="+escape(DetailProductCountZ.toString());
                           UrlParam+="&DetailUsedPrice="+escape(DetailUsedPrice.toString());
                           UrlParam+="&DetailExRate="+escape(DetailExRate.toString());
                           UrlParam+="&ID="+txtIndentityID.toString()+GetExtAttrValue();
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageInProcessAdd.ashx?",
            dataType: 'json', //返回json格式数据
            cache: false,
            data:UrlParam,
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
                        document.getElementById('div_InNo_uc').style.display = "none";
                        document.getElementById('div_InNo_Lable').style.display = "";
                        
                        if (parseInt(txtIndentityID) <= 0) {
                            document.getElementById('lbInNo').innerHTML = reInfo[1];
                            document.getElementById('txtIndentityID').value = reInfo[0];
                            if(reInfo[4] != "NULL")
                            {
                                document.getElementById("divBatchNoShow").innerHTML = reInfo[4];
                                document.getElementById("divBatchNoShow").style.display = "block";
                                document.getElementById("divBatchNo").style.display = "none";
                            }
                        }
                    }
                    try {
                        $("#btnPageFlowConfrim").hide();
                        $("#Confirm").show();
                    }
                    catch (e) { }
                }
                popMsgObj.ShowMsg(data.info);
                document.getElementById("t_Edit").style.display = "";
                document.getElementById("t_Add").style.display = "none";
            }
        });
    }
}


//---------End   生产完工入库保存 ----------


//全选
function SelectAll() {
    var signFrame = findObj("dg_Log", document);
    var txtTRLastIndex = signFrame.rows.length;
    var signFrame = findObj("dg_Log", document);
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


//计算明细中入库数量总合以及价格总合
function CountNum() {
    var List_TB = findObj("dg_Log", document);
    var rowlength = List_TB.rows.length;
    var countnum = 0;
    var countprice = 0;
    for (var i = 1; i < rowlength; i++) {
        if (List_TB.rows[i].style.display != "none") 
        {
            if(document.getElementById("hidMoreUnit").value == "true")
            {
                 countnum += parseFloat(document.getElementById('OutCount_SignItem_TD_Text_' + (i)).value, 10);
            }
            else
            {
                countnum += parseFloat(document.getElementById('ProductCount_SignItem_TD_Text_' + (i)).value);
            }
            
            countprice += parseFloat(document.getElementById('TotalPrice_SignItem_TD_Text_' + (i)).value);
        }
    }
    document.getElementById('txtTotalCount').value = countnum.toFixed($("#hidSelPoint").val());
    document.getElementById('txtTotalPrice').value = countprice.toFixed($("#hidSelPoint").val());

}

function TotalPrice(id) {
    var idstr = id.substring(13, id.length);
    var count = 'ProductCount_' + idstr;
    var productcount = document.getElementById(count).value;
    var unitprice = document.getElementById('UnitPrice_' + idstr).value;
    document.getElementById('TotalPrice_' + idstr).value = (productcount * unitprice).toFixed($("#hidSelPoint").val());
}

//删除行,才是最后版本 
function DeleteSignRow() {
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
            document.getElementById("chk_Option_" + j).id = "chk_Option_" + no;
            document.getElementById("SignItem_TD_Index_" + j).id = "SignItem_TD_Index_" + no;
            document.getElementById("ProductNo_SignItem_TD_Text_" + j).id = "ProductNo_SignItem_TD_Text_" + no;
            document.getElementById("ProductName_SignItem_TD_Text_" + j).id = "ProductName_SignItem_TD_Text_" + no;
            document.getElementById("Specification_SignItem_TD_Text_" + j).id = "Specification_SignItem_TD_Text_" + no;
            document.getElementById("UnitID_SignItem_TD_Text_" + j).id = "UnitID_SignItem_TD_Text_" + no;
            document.getElementById("NotInCount_SignItem_TD_Text_" + j).id = "NotInCount_SignItem_TD_Text_" + no;
            document.getElementById("ProductCount_SignItem_TD_Text_" + j).id = "ProductCount_SignItem_TD_Text_" + no;
            document.getElementById("UnitPrice_SignItem_TD_Text_" + j).id = "UnitPrice_SignItem_TD_Text_" + no;
            document.getElementById("TotalPrice_SignItem_TD_Text_" + j).id = "TotalPrice_SignItem_TD_Text_" + no;
            document.getElementById("Remark_SignItem_TD_Text_" + j).id = "Remark_SignItem_TD_Text_" + no;
            document.getElementById("StorageID_SignItem_TD_Text_" + j).id = "StorageID_SignItem_TD_Text_" + no;
            document.getElementById("FromBillID_SignItem_TD_Text_" + j).id = "FromBillID_SignItem_TD_Text_" + no;
            document.getElementById("FromLineNo_SignItem_TD_Text_" + j).id = "FromLineNo_SignItem_TD_Text_" + no;

            document.getElementById("FromBillCount_SignItem_TD_Text_" + j).id = "FromBillCount_SignItem_TD_Text_" + no;
            document.getElementById("InCount_SignItem_TD_Text_" + j).id = "InCount_SignItem_TD_Text_" + no;

        }
    }
}

//通过生产任务单带出明细
function AddSignRow(i, FromBillNo, ProductID, ProductNo, ProductName, Specification, UnitID, UnitPrice, ProductCount, FromLineNo,
                 FromBillCount, InCount, StorageID,IsBatchNo,UsedUnitID) 
{ //读取最后一行的行号，存放在txtTRLastIndex文本框中 
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
    newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"" + ProductNo + "\" title=\"" + ProductID + "\" class=\"tdinput\"  readonly=\"readonly\" style='width:90%''  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(3); //添加列:物品名称
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ProductName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ProductName_SignItem_TD_Text_" + rowID + "' value=\"" + ProductName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%'' />"; ; //添加列内容

    var newFitDesctd = newTR.insertCell(4); //添加列:物品规格
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Specification_' + rowID;
    newFitDesctd.innerHTML = "<input id='Specification_SignItem_TD_Text_" + rowID + "' value=\"" + Specification + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    var newFitNametd = newTR.insertCell(5); //添加列:基本单位
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_UnitID_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_" + rowID + "\" value=\"" + UnitID + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容

var newDanWeitd = newTR.insertCell(6); //添加列:单位组
newDanWeitd.className = "cell";    
newDanWeitd.id = 'SignItem_TD_UnitZ_' + rowID;//divUnitZ
newDanWeitd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容

    var newFitNametd = newTR.insertCell(7); //添加列:仓库ID
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_StorageID_' + rowID;
    newFitNametd.innerHTML = "<select class='tdinput' id=\"StorageID_SignItem_TD_Text_" + rowID + "\"   style=' width:90%;'>" + document.getElementById("ddlStorageInfo").innerHTML + "</select>";
    document.getElementById("StorageID_SignItem_TD_Text_" + rowID).value = StorageID; //赋值当前的的仓库


    var newFitNametd = newTR.insertCell(8); //添加列:源单数量
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_FromBillCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(FromBillCount) + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容

    var newFitNametd = newTR.insertCell(9); //添加列:已入库数量
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_InCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(InCount) + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容


    var newFitNametd = newTR.insertCell(10); //添加列:未入库数量
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_NotInCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"NotInCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(ProductCount) + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容


    var newFitNametd = newTR.insertCell(11); //添加列:基本数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(ProductCount) + "\"  class=\"tdinput\"  style='width:90%'  onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice(this.id);CountNum();\"/>"; //添加列内容

 var newShuLiangtd = newTR.insertCell(12); //添加列:数量?
newShuLiangtd.className = "tdinput";
newShuLiangtd.id = 'SignItem_TD_ProductCount_' + rowID;
newShuLiangtd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_" + rowID + "\" value=\"0\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_z(this.id);CountNum();\"/>"; //添加列内容

if(document.getElementById("hidMoreUnit").value == "true")
{
        newDanWeitd.style.display = "block";
       newShuLiangtd.style.display = "block";
       document.getElementById("ProductCount_SignItem_TD_Text_"+rowID). readOnly = true;
       document.getElementById("ProductCount_SignItem_TD_Text_"+rowID).onblur = function(){ return false;}
}
else
{
       newDanWeitd.style.display = "none";
       newShuLiangtd.style.display = "none";
        document.getElementById("ProductCount_SignItem_TD_Text_"+rowID).readOnly = false;
}    

    var newFitDesctd = newTR.insertCell(13); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.style.display = IsDisplayPrice;
    newFitDesctd.id = 'SignItem_TD_UnitPrice_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' value=\"" + NumberSetPoint(UnitPrice) + "\" type='text' class=\"tdinput\"   style='width:90%'  onblur=\"check2(this.id);Number_roundBegin(this,2);TotalPrice2(this.id);CountNum();\" />"; //添加列内容

    var newFitNametd = newTR.insertCell(14); //添加列:物品总价（金额）
    newFitNametd.className = "cell";
    newFitNametd.style.display = IsDisplayPrice;
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_" + rowID + "\" value=\"\" class=\"tdinput\"  readonly=\"readonly\"  style='width:90%'' />"; //添加列内容

    var productcount = document.getElementById("ProductCount_SignItem_TD_Text_" + rowID).value; //计算总额
    var unitprice = document.getElementById("UnitPrice_SignItem_TD_Text_" + rowID).value;    
    
    document.getElementById("TotalPrice_SignItem_TD_Text_" + rowID).value = (productcount * unitprice).toFixed($("#hidSelPoint").val());

    var newFitNametd = newTR.insertCell(15); //添加列:原单编号
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillID_SignItem_TD_Text_" + rowID + "\" value=\"" + FromBillNo + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(16); //添加列:原单行号
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='FromLineNo_SignItem_TD_Text_" + rowID + "' value=\"" + FromLineNo + "\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

//绑定单位组
if(document.getElementById("hidMoreUnit").value == "true")
{
    if(UnitPrice == "")
    {
        UnitPrice = "0";
    }
    GetUnitGroupSelectEx(ProductID,"StockUnit","DetailUnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,UsedUnitID,"NoShowUnit("+rowID+","+UnitPrice+")");
}
    var newFitDesctd = newTR.insertCell(17); //添加列:备注
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\"  style='width:90%''  />"; //添加列内容
    //txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
    
    var newBatchNo = newTR.insertCell(18); //添加列:pici
    newBatchNo.className = "cell";
    newBatchNo.id = 'SignItem_TD_BatchNo_' + rowID;
    newBatchNo.innerHTML = "<input name='chk' id='BatchNo_SignItem_TD_Text_" + rowID + "' value=\"" + IsBatchNo + "\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容
    newBatchNo.style.display="none";
    
    var newJiBen_DanJia = newTR.insertCell(19); //添加列:基本单价
    newJiBen_DanJia.className = "cell";
    newJiBen_DanJia.id = 'TD_JiBen_DanJia_' + rowID;
    newJiBen_DanJia.innerHTML = "<input name='chk' id='JiBen_DanJia_" + rowID + "' value=\"" + UnitPrice + "\" type='text'  />"; //添加列内容
    newJiBen_DanJia.style.display="none";
}

//选择单位时计算
 function ChangeUnit(ObjID/*下拉列表控件（单位）*/,rowid/*行号*/,UnitPrice)
 { 
    if($("#OutCount_SignItem_TD_Text_"+rowid).val() != "")
    {
        var EXRate = $("#"+ObjID).val().split('|')[1].toString(); /*比率*/
        var ReBasePCount = $("#OutCount_SignItem_TD_Text_"+rowid).val() * EXRate;
        var DanJia = EXRate * UnitPrice;//单价 = 比率 * 基本单价
        
        $("#ProductCount_SignItem_TD_Text_"+ rowid).val(ReBasePCount.toFixed($("#hidSelPoint").val()));//基本数量= 数量*比率
        $("#UnitPrice_SignItem_TD_Text_"+rowid).val(DanJia.toFixed($("#hidSelPoint").val()));//单价
    }
    
 }


function FillSignRow(i, SortNo, InNo, ProductID, ProductNo, ProductName, Specification,UnitID, UnitPrice, TotalPrice, ProductCount, Remark,
                    FromBillNo,FromLineNo, StorageID, NotInCount, FromBillCount, InCount,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,IsBatchNo)
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
    newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"" + ProductNo + "\" title=\"" + ProductID + "\" class=\"tdinput\"  readonly=\"readonly\" style='width:90%''  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(3); //添加列:物品名称
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ProductName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ProductName_SignItem_TD_Text_" + rowID + "' value=\"" + ProductName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%'' />"; ; //添加列内容

    var newFitDesctd = newTR.insertCell(4); //添加列:物品规格
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Specification_' + rowID;
    newFitDesctd.innerHTML = "<input id='Specification_SignItem_TD_Text_" + rowID + "' value=\"" + Specification + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    var newFitNametd = newTR.insertCell(5); //添加列:单位
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_UnitID_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_" + rowID + "\" value=\"" + UnitID + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容

var newDanWeitd = newTR.insertCell(6); //添加列:单位组
newDanWeitd.className = "cell";    
newDanWeitd.id = 'SignItem_TD_UnitZ_' + rowID;//divUnitZ
newDanWeitd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容

    var newFitNametd = newTR.insertCell(7); //添加列:仓库ID
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_StorageID_' + rowID;
    newFitNametd.innerHTML = "<select class='tdinput' id=\"StorageID_SignItem_TD_Text_" + rowID + "\"   style=' width:90%;'>" + document.getElementById("ddlStorageInfo").innerHTML + "</select>";
    document.getElementById("StorageID_SignItem_TD_Text_" + rowID).value = StorageID; //赋值当前的的仓库

    var newFitNametd = newTR.insertCell(8); //添加列:源单数量
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_FromBillCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(FromBillCount) + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容

    var newFitNametd = newTR.insertCell(9); //添加列:已入库数量
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_InCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(InCount) + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容


    var newFitNametd = newTR.insertCell(10); //添加列:未入库数量
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_NotInCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"NotInCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(NotInCount) + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容

    var newFitNametd = newTR.insertCell(11); //添加列:基本数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(ProductCount) + "\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice(this.id);CountNum();\"/>"; //添加列内容

if(UsedUnitCount == "")
{
    UsedUnitCount = "0";
}
var newShuLiangtd = newTR.insertCell(12); //添加列:数量?
newShuLiangtd.className = "tdinput";
newShuLiangtd.id = 'SignItem_TD_ProductCount_' + rowID;
newShuLiangtd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(UsedUnitCount) + "\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_z(this.id);CountNum();\"/>"; //添加列内容

if(document.getElementById("hidMoreUnit").value == "true")
{
        newDanWeitd.style.display = "block";
       newShuLiangtd.style.display = "block";
       document.getElementById("ProductCount_SignItem_TD_Text_"+rowID). readOnly = true;
       document.getElementById("ProductCount_SignItem_TD_Text_"+rowID).onblur = function(){ return false;}
}
else
{
       newDanWeitd.style.display = "none";
       newShuLiangtd.style.display = "none";
        document.getElementById("ProductCount_SignItem_TD_Text_"+rowID).readOnly = false;
}

var danjia;
if(document.getElementById("hidMoreUnit").value == "true")
{
    danjia = UsedPrice;
}
else
{
    danjia = UnitPrice;
}  
    var newFitDesctd = newTR.insertCell(13); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.style.display = IsDisplayPrice;
    newFitDesctd.id = 'SignItem_TD_UnitPrice_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' value=\"" + NumberSetPoint(danjia) + "\" type='text' class=\"tdinput\"  style='width:90%'    onblur=\"check2(this.id);Number_roundBegin(this,2);TotalPrice2(this.id);CountNum();\" />"; //添加列内容

    var newFitNametd = newTR.insertCell(14); //添加列:物品总价（金额）
    newFitNametd.className = "cell";
    newFitNametd.style.display = IsDisplayPrice;
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(TotalPrice) + "\" class=\"tdinput\"  style='width:90%''  readonly=\"readonly\" />"; //添加列内容

//    var productcount = document.getElementById("ProductCount_SignItem_TD_Text_" + rowID).value; //计算总额
//    var unitprice = document.getElementById("UnitPrice_SignItem_TD_Text_" + rowID).value;
//    document.getElementById("TotalPrice_SignItem_TD_Text_" + rowID).value = (productcount * unitprice).toFixed($("#hidSelPoint").val());

    var newFitNametd = newTR.insertCell(15); //添加列:原单编号
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillID_SignItem_TD_Text_" + rowID + "\" value=\"" + FromBillNo + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(16); //添加列:原单行号
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='FromLineNo_SignItem_TD_Text_" + rowID + "' value=\"" + FromLineNo + "\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

//绑定单位组
if(document.getElementById("hidMoreUnit").value == "true")
{
    GetUnitGroupSelectEx(ProductID,"StockUnit","DetailUnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,UsedUnitID,"NoShowUnit_b("+rowID+")");
}

    var newFitDesctd = newTR.insertCell(17); //添加列:备注
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_" + rowID + "' value=\"" + Remark + "\" type='text' class=\"tdinput\"  style='width:90%''  />"; //添加列内容
    //txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行

    var newBatchNo = newTR.insertCell(18); //添加列:pici
    newBatchNo.className = "cell";
    newBatchNo.id = 'SignItem_TD_BatchNo_' + rowID;
    newBatchNo.innerHTML = "<input name='chk' id='BatchNo_SignItem_TD_Text_" + rowID + "' value=\"" + IsBatchNo + "\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容
    newBatchNo.style.display="none";
    
    var newJiBen_DanJia = newTR.insertCell(19); //添加列:基本单价
    newJiBen_DanJia.className = "cell";
    newJiBen_DanJia.id = 'TD_JiBen_DanJia_' + rowID;
    newJiBen_DanJia.innerHTML = "<input name='chk' id='JiBen_DanJia_" + rowID + "' value=\"" + UnitPrice + "\" type='text'  />"; //添加列内容
    newJiBen_DanJia.style.display="none";
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

function TotalPrice(id) {
 
    var idstr = id.substring(13, id.length);
    var count = 'ProductCount_' + idstr;
    var productcount = document.getElementById(count).value;
    var unitprice = document.getElementById('UnitPrice_' + idstr).value;
    document.getElementById('TotalPrice_' + idstr).value = (productcount * unitprice).toFixed($("#hidSelPoint").val());
}

//填写数量后，根据选中单位，更改基本数量
function TotalPrice_z(id)
{
     var idstr = id.substring(8, id.length);   
     var count = 'DetailUnitID' + idstr;
     var EXRate = $("#"+count).val().split('|')[1].toString(); /*比率*/     
    
     var ReCount = $("#"+id).val() * EXRate;
      $("#"+"ProductCount" + idstr).val(ReCount.toFixed($("#hidSelPoint").val()));
      
     var ReTotaoPrice = $("#OutCount" + idstr).val() * $("#UnitPrice" + idstr).val();     
     $("#TotalPrice" + idstr).val(ReTotaoPrice.toFixed($("#hidSelPoint").val()));
}

function TotalPrice2(id) 
{
    var idstr = id.substring(10, id.length);
    var count;
    if(document.getElementById("hidMoreUnit").value == "true")
    {
        count = 'OutCount_' + idstr;
    }
    else
    {
        count = 'ProductCount_' + idstr;
    }
    
    var productcount = document.getElementById(count).value;
    var unitprice = document.getElementById('UnitPrice_' + idstr).value;
    document.getElementById('TotalPrice_' + idstr).value = (productcount * unitprice).toFixed($("#hidSelPoint").val());
}




//验证唯一性
function checkonly() {
    var InNo = document.getElementById('txtInNo').value;
    var TableName = "officedba.StorageInProcess";
    var ColumName = "InNo";
    $.ajax({
        type: "POST",
        url: "../../../Handler/CheckOnlyOne.ashx?strcode='" + InNo + "'&colname=" + ColumName + "&tablename=" + TableName + "",
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        complete: function() { hidePopup(); },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.sta != 1) {
                document.getElementById('txtInNoHidden').value = data.sta;
            }
            else {
                document.getElementById('txtInNoHidden').value = "";
            }
        }
    });
}

function ConfirmBill() {

    if (confirm('确认之后不可修改，你确定要确认吗？')) {

        var signFrame = findObj("dg_Log", document);
        for (i = 1; i < signFrame.rows.length; i++) 
        {
            var ProductCount = "";
            if(document.getElementById("hidMoreUnit").value == "true")
            {
                ProductCount = "OutCount_SignItem_TD_Text_" + i;
            }
            else
            {
                ProductCount = "ProductCount_SignItem_TD_Text_" + i;
            }
                        
            var NotInCount = "NotInCount_SignItem_TD_Text_" + i;
            
            if (parseFloat(document.getElementById(ProductCount).value) > parseFloat(document.getElementById(NotInCount).value)) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "入库数量不能大于未入库数量！");
                return false;
            }
        }
        /*--------------------------以上判断是否大于源单未入库数量--------------------------------------*/
        var txtIndentityID = $("#txtIndentityID").val();
        var act = "Confirm";
        var UrlParam = "&Act=Confirm\
                            &ID=" + txtIndentityID.toString() + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageInProcessAdd.ashx?" + UrlParam,
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
                    document.getElementById('sltBillStatus').value = 2;
                    try {
                        $("#btn_save").hide();
                        $("#Confirm").hide();
                        $("#btn_UnClick_bc").show();
                        $("#btnPageFlowConfrim").show();
                        $("#btn_Close").show();
                        $("#btn_Unclic_Close").hide();
                        $("#btn_CancelClose").hide();
                        $("#btn_Unclick_CancelClose").show();
                    }
                    catch (e) { }
                }
                fnDelRow();
                LoadDetailInfo($("#txtIndentityID").val());
                popMsgObj.ShowMsg(data.info);
            }
        });
    }
    else {
        return false;
    }
}

//结单

function CloseBill() {
    if (confirm('确认要进行结单操作吗？')) {
        var txtIndentityID = $("#txtIndentityID").val();
        var UrlParam = "&Act=Close\
                            &ID=" + txtIndentityID.toString() + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageInProcessInfo.ashx?" + UrlParam,
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
                        document.getElementById('txtCloser').value = reInfo[0];
                        document.getElementById('txtCloseDate').value = reInfo[1];
                    }
                    document.getElementById('sltBillStatus').value = 4;
                    try {
                        $("#btn_save").hide();
                        $("#Confirm").hide();
                        $("#btn_UnClick_bc").show();
                        $("#btnPageFlowConfrim").show();
                        $("#btn_Close").hide();
                        $("#btn_Unclic_Close").show();
                        $("#btn_CancelClose").show();
                        $("#btn_Unclick_CancelClose").hide();
                    }
                    catch (e) { }
                }
                popMsgObj.ShowMsg(data.info);
            }
        });
    }
    else {
        return false;
    }
}


//取消结单

function CancelCloseBill() {

    if (confirm('确认要进行取消结单操作吗？')) {
        var txtIndentityID = $("#txtIndentityID").val();
        var UrlParam = "&Act=CancelClose\
                            &ID=" + txtIndentityID.toString() + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageInProcessInfo.ashx?" + UrlParam,
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
                        document.getElementById('txtCloser').value = "";
                        document.getElementById('txtCloseDate').value = "";
                    }
                    document.getElementById('sltBillStatus').value = 2;
                    try {
                        $("#btn_save").hide();
                        $("#Confirm").hide();
                        $("#btn_UnClick_bc").show();
                        $("#btnPageFlowConfrim").show();
                        $("#btn_Close").show();
                        $("#btn_Unclic_Close").hide();
                        $("#btn_CancelClose").hide();
                        $("#btn_Unclick_CancelClose").show();
                    }
                    catch (e) { }
                }
                popMsgObj.ShowMsg(data.info);
            }
        });
    }
    else {
        return false;
    }
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
}


function fnSelectMTInfo() {
    popManufactureTaskObj.ShowList('txtFromBillID');

}

function FillBasicInfo(TaskNo, FromBillID, ProcessType, ProcessDeptName, ProcessorName) {
    $("#txtFromBillID").val(TaskNo);
    $("#txtFromBillID").attr("title", FromBillID);
    $("#ddlProcessType").val(ProcessType); //加工类别
    $("#txtProcessDept").val(ProcessDeptName);
    $("#txtProcessor").val(ProcessorName); //负责人
}

/*-----------------明细中输入数据格式验证---------------------*/
function check(str) {
    fieldText = ""; var msgText = ""; var isFlag = true;
    if (document.getElementById(str).value.length > 0) {
        if (!IsNumeric(document.getElementById(str).value)) {
            isFlag = false;
            fieldText = fieldText + "入库数据|";
            msgText = msgText + "入库数据输入有误，请输入有效的数值（大于0）！";
            document.getElementById(str).value = "0";
            document.getElementById(str).focus();
        }
    }
    if (!isFlag) {
        popMsgObj.ShowMsg(msgText);
    }

}

function check2(str) {
    fieldText = ""; var msgText = ""; var isFlag = true;
    if (document.getElementById(str).value.length > 0) {
        if (!IsNumeric(document.getElementById(str).value)) {
            isFlag = false;
            fieldText = fieldText + "  入库单明细|";
            msgText = msgText + "单价数据输入有误，请输入有效的数值（非负）！";
            document.getElementById(str).value = "0";
            document.getElementById(str).focus();
        }
    }
    if (!isFlag) {
        popMsgObj.ShowMsg(msgText);
    }

}

/*
* 返回按钮
*/
function DoBack() {
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    moduleID = document.getElementById("hidModuleID").value;
    window.location.href = "StorageInProcessList.aspx?ModuleID=" + moduleID + searchCondition;
}

/*单据打印*/
function BillPrint() {
    var ID = $("#txtIndentityID").val();
    if (ID == "" || ID == "0") {
        popMsgObj.Show("打印|", "请保存单据再打印|");
        return;
    }
    window.open("../../../Pages/PrinttingModel/StorageManager/StorageInProcessPrint1.aspx?ID=" + ID);
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
        }
        else {
            popMsgObj.ShowMsg('请选择单个物品查看库存快照');
            return false;
        }
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

function Number_roundBegin(obj,num)
{
    var SetPoint = $("#hidSelPoint").val();
    Number_round(obj,SetPoint);
}

function NumberSetPoint(num)
{
    var SetPoint = parseFloat(num).toFixed($("#hidSelPoint").val());
    return SetPoint;
}

//使单位组不可读
function NoShowUnit(RowID,UnitPrice)
{
    $("#DetailUnitID_SignItem_TD_Text_"+RowID).attr("disabled",true);
    var EXRate = $("#DetailUnitID_SignItem_TD_Text_"+RowID).val().split('|')[1].toString(); /*比率*/
    if (EXRate != '')
    {
         $("#UnitPrice_SignItem_TD_Text_"+RowID).val(NumberSetPoint(UnitPrice*EXRate));
    }
}

//使单位组不可读
function NoShowUnit_b(RowID,UnitPrice)
{
    $("#DetailUnitID_SignItem_TD_Text_"+RowID).attr("disabled",true);
    
}
