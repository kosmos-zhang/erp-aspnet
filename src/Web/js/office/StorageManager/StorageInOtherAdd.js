eventObj.Table = 'dg_Log';

//---------Start 其他入库详细信息和明细列表 --------
var InNoID = document.getElementById('txtIndentityID').value;
var GlbDetailID = new Array();
var ReNumDetail = new Array(); //需要回写SellBackDetail的以入库数据
var ReBackNo = "";
var ReLineNo = new Array();
var ReProductID = new Array();
var ReStorageID = new Array();


$(document).ready(function() {
    fnGetExtAttr(); //物品控件拓展属性
    if (InNoID > 0) {

        LoadDetailInfo(InNoID);
        document.getElementById("t_Edit").style.display = "";
    }
    else {
        GetExtAttr('officedba.StorageInOther', null);
        document.getElementById("t_Add").style.display = "";

        try {
            document.getElementById('btn_AddRow').onclick = ShowProdInfo;
            $("#btn_save").show();
            $("#btnPageFlowConfrim").show();
            $("#btn_Unclic_Close").show();
            $("#btn_Unclick_CancelClose").show();
            document.getElementById('btn_AddRow').onclick = ShowProdInfo;
            document.getElementById('txtOtherCorp').onclick = alerTreediv;
        }
        catch (e) { }
    }
});
function LoadDetailInfo(InNoID) {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/StorageManager/StorageInOtherInfo.ashx?Act=&ID=" + InNoID, //目标地址
        cache: false,
        success: function(msg) {
            GetExtAttr('officedba.StorageInOther', msg.data[0]); //获取扩展属性并填充值
            //数据获取完毕，填充页面据显示
            document.getElementById('lbInNo').innerHTML = msg.data[0].InNo;
             document.getElementById("divBatchNo").style.display = "none";   
                
            document.getElementById('txtTitle').value = msg.data[0].Title;
            document.getElementById('HidProjectID').value = msg.data[0].ProjectID;
            document.getElementById('txtSelProject').value = msg.data[0].ProjectName;
            
            document.getElementById('ddlFromType').value = msg.data[0].FromType;
            $("#txtFromBillID").attr("title", msg.data[0].FromBillID); //
            $("#txtFromBillID").val(msg.data[0].BackNo);

            $("#sltCorpBigType").val(msg.data[0].CorpBigType); //往来单位类型
            $("#txtOtherCorp").val(msg.data[0].OtherCorpName); //往来单位名称
            $("#txtOtherCorpID").val(msg.data[0].OtherCorpID); //往来单位ID

            document.getElementById('DeptName').value = msg.data[0].DeptName; //入库部门
            //$("#txtDept").val(msg.data[0].DeptID);//入库部门ID 待修改
            $("#txtDeptID").val(msg.data[0].DeptID); //入库部门ID
            $("#UserTaker").val(msg.data[0].TakerName);
            $("#txtTakerID").val(msg.data[0].Taker);
            $("#UserChecker").val(msg.data[0].CheckerName);
            $("#txtCheckerID").val(msg.data[0].Checker);
            document.getElementById('UserExecutor').value = msg.data[0].ExecutorName;
            $("#txtExecutorID").val(msg.data[0].Executor); //入库人ID
            document.getElementById('txtEnterDate').value = msg.data[0].EnterDate;
            $("#ddlReasonType").val(msg.data[0].ReasonType);
            $("#txtSummary").val(msg.data[0].Summary);
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

            $.each(msg.data, function(i, item) {

                if (item.ProductID != null && item.ProductID != '')
                 {
                    if(msg.data[i].BatchNo != "")
                     {
                            document.getElementById("divBatchNo").style.display = "none";
                            document.getElementById('divBatchNoShow').innerHTML = msg.data[i].BatchNo;
                     }
                    FillSignRow(i, item.InNo, item.ProductID, item.ProductNo, item.ProductName,item.ColorName,  item.Specification, item.UnitID,item.UnitPrice,
                     item.B_TotalPrice, item.FromBillCount, item.InCount, item.NotInCount, item.BackNo,item.FromLineNo, item.DetaiRemark,
                      item.StorageID, item.FromType, item.InNumber,item.UsedUnitID,item.UsedUnitCount,item.UsedPrice,item.ExRate,item.IsBatchNo);
                }
            });
            ReBackNo = document.getElementById('txtFromBillID').value; //获取销售退货单的单据编号
            var signFrame = findObj("dg_Log", document);
            var count = signFrame.rows.length; //有多少行
            for (var i = 1; i < count; i++) {
                if (signFrame.rows[i].style.display != "none") {
                    var objInCount = 'InCount_SignItem_TD_Text_' + (i);
                    var objFromLineNo = 'FromLineNo_SignItem_TD_Text_' + (i);
                    var objProductNo = 'ProductNo_SignItem_TD_Text_' + (i);
                    var objStorageID = 'StorageID_SignItem_TD_Text_' + (i);
                    ReProductID.push($("#" + objProductNo).attr("title"));
                    ReStorageID.push(document.getElementById(objStorageID.toString()).value);
                    ReNumDetail.push(document.getElementById(objInCount.toString()).value);
                    ReLineNo.push(document.getElementById(objFromLineNo.toString()).value);
                }
            }

            if (document.getElementById('sltBillStatus').value == 1)//根据单据状态显示按钮
            {
                try {
                    $("#Confirm").show();
                    $("#btn_save").show();
                    $("#btn_Unclic_Close").show();
                    $("#btn_Unclick_CancelClose").show();
                    if (document.getElementById('ddlFromType').value == "0") {
                        document.getElementById('btn_AddRow').onclick = ShowProdInfo;
                        document.getElementById('txtOtherCorp').onclick = alerTreediv;
                    }
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
                    document.getElementById('txtOtherCorp').onclick = jinzhi;
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

//---------End 其他入库详细信息和明细列表 ----------

//---------Start 其他入库保存 ----------
function Fun_Save_StorageInOther() {
    CountNum();
    var bmgz = "";
    var pcgz = "";
    var BratchNo = "null";
    var Flag = true;
    var txtExecutor = $("#UserExecutor").val();

    var fieldText = "";
    var msgText = "";
    var isFlag = true;

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
    //var txtInNo=$("#txtInNo_ddlCodeRule").val(); 
    var txtTitle = $("#txtTitle").val();
    var ddlFromType = $("#ddlFromType").val();
    var txtFromBillID = $("#txtFromBillID").attr("title"); //
    var txtDept = $("#txtDeptID").val();
    var txtTaker = $("#txtTakerID").val();
    var txtChecker = $("#txtCheckerID").val();
    var txtExecutor = $("#txtExecutorID").val(); //入库人
    var txtEnterDate = $("#txtEnterDate").val(); //入库时间
    var txtSummary = $("#txtSummary").val();
    var ddlReasonType = $("#ddlReasonType").val();

    var sltCorpBigType = $("#sltCorpBigType").val(); //来往单位类型
    var txtOtherCorpID = $("#txtOtherCorpID").val(); //往来单位ID

    var txtTotalPrice = $("#txtTotalPrice").val();
    var txtTotalCount = $("#txtTotalCount").val();

    var txtRemark = $("#txtRemark").val();
     var CanUserID = $("#txtCanUserID").val();//可查看人ID
    var CanUserName = $("#txtCanUserName").val();//可查看人姓名

    var sltBillStatus = $("#sltBillStatus").val();
    var hiddenValue = $("#txtInNoHidden").val();
    var HidProjectID = $("#HidProjectID").val();//所属项目

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

    //    if (txtTitle.Trim().length == "") {
    //        isFlag = false;
    //        fieldText = fieldText + "主题|";
    //        msgText = msgText + "请输入主题|";
    //    }

    if (strlen(txtTitle) > 100) {
        isFlag = false;
        fieldText = fieldText + "主题|";
        msgText = msgText + "仅限于100个字符以内|";
    }

    if (txtExecutor == "") {
        isFlag = false;
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
    
    if (ddlFromType == "1") {
        if ($("#txtFromBillID").val() == "") {
            isFlag = false;
            fieldText = fieldText + "源单编号|";
            msgText = msgText + "请选择源单|";
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
    var ifProductNo = "0";
    var ifUnitPrice = "0";
    var ifRemark = "0";
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
            ProductCount = "InCount_SignItem_TD_Text_" + i;
        }

        var UnitPrice = "UnitPrice_SignItem_TD_Text_" + i;
        var Remark = "Remark_SignItem_TD_Text_" + i;
        if (document.getElementById(ProductCount).value == "" || parseFloat(document.getElementById(ProductCount).value) <= 0 || document.getElementById(ProductCount).value == "0.00") {
            ifProductCount = "1";
        }
        if (IsDisplayPrice!="none"&&(document.getElementById(UnitPrice).value == "" || parseFloat(document.getElementById(UnitPrice).value) <0) ) {
            ifUnitPrice = "1";
        }
        if(IsDisplayPrice!="none" && document.getElementById("hidZero").value == "False" && parseFloat(document.getElementById(UnitPrice).value) == 0)
        {
            ifUnitPrice = "1";
        }
        
        if (strlen($("#" + Remark).val()) > 200) {
            ifRemark = "1";
        }
    }

    if (ddlFromType == "0") {
        for (i = 1; i < signFrame.rows.length; i++) {
            var ProductNo = "ProductNo_SignItem_TD_Text_" + i;
            if ($("#" + ProductNo).val() == "" || $("#" + ProductNo).attr("title") == "undefined") {
                ifProductNo = "1";
            }
        }
    }
    if (ddlFromType == "1") {
        for (i = 1; i < signFrame.rows.length; i++) {
             var InCount;
            if(document.getElementById("hidMoreUnit").value == "true")
            {
                InCount = "OutCount_SignItem_TD_Text_" + i;
            }
            else
            {
                InCount = "InCount_SignItem_TD_Text_" + i;
            }
            
            var NotInCount = "NotInCount_SignItem_TD_Text_" + i;
            if (parseFloat(document.getElementById(InCount).value) > parseFloat(document.getElementById(NotInCount).value)) {
                ifbig = "1"; rownum = i;
                break;
            }
        }
    }

    if (ifProductNo == "1") {
        isFlag = false;
        fieldText = fieldText + "明细物品|";
        msgText = msgText + "请选择明细物品|";
    }

    if (ifUnitPrice == "1" && IsDisplayPrice != "none") {
        isFlag = false;
        fieldText = fieldText + "明细单价|";
        msgText = msgText + "请输入有效的数值（大于0）|";
    }

    if (ifProductCount == "1") {
        isFlag = false;
        fieldText = fieldText + "基本数量|";
        msgText = msgText + "请输入有效的数值（大于0）|";
    }
//     if (ifProductC == "1") {
//        isFlag = false;
//        fieldText = fieldText + "入库数量|";
//        msgText = msgText + "请输入有效的数值（大于0）|";
//    }
    if (ifbig == "1") {
        isFlag = false;
        fieldText = fieldText + "入库数量|";
        msgText = msgText + "第" + rownum + "行入库数量不能大于未入库数量|";
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

        //期初库存明细

        var DetailProductID = new Array();
        var DetailSortNo = new Array(); //序号
        var DetailUnitID = new Array();
        var DetailStorageID = new Array();
        var DetailFromLineNo = new Array();
        var DetailProductCount = new Array();
        var DetailUnitPrice = new Array();
        var DetailTotalPrice = new Array();
        var DetailRemark = new Array();
        var DetailRemark = new Array();
        var DetailUpdate = new Array(); //明细中要更新
        
        var DetailUnitZ = new Array();//单位z
        var DetailProductCountZ = new Array();//数量
        var DetailUsedPrice = new Array();//单价
        var DetailExRate = new Array();//单位换算率
         var DetailBatchNo = new Array();//批次

        var signFrame = findObj("dg_Log", document);
        var count = signFrame.rows.length; //有多少行
        for (var i = 1; i < count; i++) {
            if (signFrame.rows[i].style.display != "none") {
                var objProductNo = 'ProductNo_SignItem_TD_Text_' + (i);
                var objSortNo = 'SignItem_TD_Index_' + (i);
                var objUnitID = 'UnitID_SignItem_TD_Text_' + (i);
                var objStorageID = 'StorageID_SignItem_TD_Text_' + (i);
                var objProductCount = 'InCount_SignItem_TD_Text_' + (i);
                var objUnitPrice = 'JiBen_DanJia_' + (i);
                var objTotalPrice = 'TotalPrice_SignItem_TD_Text_' + (i);
                var objRemark = 'Remark_SignItem_TD_Text_' + (i);
                var objFromLineNo = 'FromLineNo_SignItem_TD_Text_' + (i);
                var objRadio = 'chk_Option_' + (i);
                
                
                
                var objUnitZ = 'DetailUnitID_SignItem_TD_Text_' + (i);//单位z
                var objProductCountZ = 'OutCount_SignItem_TD_Text_' + (i);//数量
                var objUsedPrice = 'UnitPrice_SignItem_TD_Text_' + (i);//单价
                var objBatchNo = 'BatchNo_SignItem_TD_Text_' + (i);

                DetailProductID.push($("#" + objProductNo).attr("title"));
                DetailSortNo.push(document.getElementById(objSortNo.toString()).innerHTML);
                DetailUnitID.push(document.getElementById(objUnitID.toString()).value);
                DetailStorageID.push(document.getElementById(objStorageID.toString()).value);
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value);
                DetailProductCount.push(document.getElementById(objProductCount.toString()).value);
                
                DetailTotalPrice.push(document.getElementById(objTotalPrice.toString()).value);
                DetailRemark.push(document.getElementById(objRemark.toString()).value);
                DetailBatchNo.push($("#" + objBatchNo).val());//用于存储批次是否启用的数组

                if(document.getElementById("hidMoreUnit").value == "true")
                {
                DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value);
                
                    DetailUnitZ.push(document.getElementById(objUnitZ.toString()).value.split('|')[0].toString());//单位
                    DetailProductCountZ.push(document.getElementById(objProductCountZ.toString()).value);//数量
                    var ReUsedPrice = document.getElementById(objUsedPrice.toString()).value;// * document.getElementById(objUnitZ.toString()).value.split('|')[1].toString();
                    DetailUsedPrice.push(ReUsedPrice);//实际单价
                    DetailExRate.push(document.getElementById(objUnitZ.toString()).value.split('|')[1].toString());//单位换算率
                }
                else
                {
                    var objUnitP = 'UnitPrice_SignItem_TD_Text_' + (i);  
                      
                    DetailUnitPrice.push(document.getElementById(objUnitP).value);               
                }
            }
        }

         var UrlParam = "&txtInNo="+escape(txtInNo)+
                        "&txtTitle="+escape(txtTitle)+
                        "&ddlFromType="+escape(ddlFromType)+
                        "&sltCorpBigType="+escape(sltCorpBigType)+
                        "&ddlReasonType="+escape(ddlReasonType)+
                        "&txtOtherCorpID="+escape(txtOtherCorpID)+
                        "&txtTaker="+escape(txtTaker)+
                        "&txtChecker="+escape(txtChecker)+
                        "&txtFromBillID="+escape(txtFromBillID)+
                        "&txtTotalPrice="+escape(txtTotalPrice)+
                        "&txtTotalCount="+escape(txtTotalCount)+
                        "&txtDept="+escape(txtDept)+
                        "&txtExecutor="+escape(txtExecutor)+
                        "&txtEnterDate="+escape(txtEnterDate)+
                        "&txtSummary="+escape(txtSummary)+
                        "&txtRemark="+escape(txtRemark)+
                        "&CanUserName="+escape(CanUserName)+
                        "&CanUserID="+escape(CanUserID)+
                        "&sltBillStatus="+escape(sltBillStatus)+
                        "&DetailProductID="+escape(DetailProductID.toString())+
                        "&DetailSortNo="+escape(DetailSortNo.toString())+
                        "&DetailUnitID="+escape(DetailUnitID.toString())+
                        "&DetailStorageID="+escape(DetailStorageID.toString())+
                        "&DetailFromLineNo="+escape(DetailFromLineNo.toString())+
                        "&DetailProductCount="+escape(DetailProductCount.toString())+
                        "&DetailUnitPrice="+escape(DetailUnitPrice.toString())+
                        "&DetailTotalPrice="+escape(DetailTotalPrice.toString())+
                        "&DetailRemark="+escape(DetailRemark.toString())+
                        "&Act=edit"+
                        "&bmgz="+bmgz+
                        "&pcgz="+pcgz+
                        "&IsMoreUnit="+document.getElementById("hidMoreUnit").value+
                        "&BratchNo="+escape(BratchNo.toString())+
                        "&DetailBatchNo="+escape(DetailBatchNo.toString())+
                        "&DetailUnitZ="+escape(DetailUnitZ.toString())+
                        "&DetailProductCountZ="+escape(DetailProductCountZ.toString())+
                        "&DetailUsedPrice="+escape(DetailUsedPrice.toString())+
                        "&DetailExRate="+escape(DetailExRate.toString())+
                        "&HidProjectID="+escape(HidProjectID)+
                        "&ID="+txtIndentityID.toString()+
                        "&ReNumDetail="+ReNumDetail.toString()+
                        "&ReBackNo="+ReBackNo.toString()+
                        "&ReLineNo="+ReLineNo.toString()+
                        "&ReProductID="+ReProductID.toString()+
                        "&ReStorageID="+ReStorageID.toString()+GetExtAttrValue();
                        

        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageInOtherAdd.ashx",
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
//                        if(document.getElementById("hidBatchNo").value == "true")//是否启用批次
//                        {
//                            document.getElementById("divBatchNo").style.display = "none";
//                            document.getElementById("divBatchNoShow").style.display = "block";
//                        }
                        
                        if (parseInt(txtIndentityID) <= 0) {
                            document.getElementById('lbInNo').innerHTML = reInfo[1];
                            document.getElementById('txtIndentityID').value = reInfo[0];
                            document.getElementById('txtModifiedUserID').value = reInfo[2];
                            document.getElementById('txtModifiedDate').value = reInfo[3];
                            if(reInfo[4] != "NULL")
                            {
                                document.getElementById("divBatchNoShow").innerHTML = reInfo[4];
                                document.getElementById("divBatchNoShow").style.display = "block";
                                document.getElementById("divBatchNo").style.display = "none";   
                            }
                            
                        }
                        else {
                            document.getElementById('txtModifiedUserID').value = reInfo[0];
                            document.getElementById('txtModifiedDate').value = reInfo[1];
                        }
                    }
                    try {
                        $("#btnPageFlowConfrim").hide();
                        $("#Confirm").show();
                    }
                    catch (e) { }
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


//---------End   生产完工入库保存 ----------

//计算明细中入库数量总合以及价格总合
function CountNum() {
    var List_TB = findObj("dg_Log", document);
    var rowlength = List_TB.rows.length;
    var countnum = 0;
    var countprice = 0;
    for (var i = 1; i < rowlength; i++) {
        if (List_TB.rows[i].style.display != "none") {
           
            if(document.getElementById("hidMoreUnit").value == "true")
            {
                countnum += parseFloat(document.getElementById('OutCount_SignItem_TD_Text_' + (i)).value, 10);
                 //这里是更换物品的时候需要每一行单选物品的时候需要从新计算当前行的金额
                $("#TotalPrice_SignItem_TD_Text_" + i).val(($("#OutCount_SignItem_TD_Text_" + i).val() * $("#UnitPrice_SignItem_TD_Text_" + i).val()).toFixed($("#hidSelPoint").val()));
            
            }
            else
            {
               countnum += parseFloat(document.getElementById('InCount_SignItem_TD_Text_' + (i)).value, 10);
                //这里是更换物品的时候需要每一行单选物品的时候需要从新计算当前行的金额
                $("#TotalPrice_SignItem_TD_Text_" + i).val(($("#InCount_SignItem_TD_Text_" + i).val() * $("#UnitPrice_SignItem_TD_Text_" + i).val()).toFixed($("#hidSelPoint").val()));
            
            }
                        
            countprice += parseFloat(document.getElementById('TotalPrice_SignItem_TD_Text_' + (i)).value, 10);            
        }
    }
    document.getElementById('txtTotalCount').value = countnum.toFixed($("#hidSelPoint").val());
    document.getElementById('txtTotalPrice').value = countprice.toFixed($("#hidSelPoint").val());

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
            document.getElementById("SignItem_TD_Index_" + j).id = "SignItem_TD_Index_" + no;
            document.getElementById("chk_Option_" + j).id = "chk_Option_" + no;
            document.getElementById("ProductNo_SignItem_TD_Text_" + j).id = "ProductNo_SignItem_TD_Text_" + no;
            document.getElementById("ProductName_SignItem_TD_Text_" + j).id = "ProductName_SignItem_TD_Text_" + no;
            document.getElementById("Specification_SignItem_TD_Text_" + j).id = "Specification_SignItem_TD_Text_" + no;
            document.getElementById("UnitID_SignItem_TD_Text_" + j).id = "UnitID_SignItem_TD_Text_" + no;
            document.getElementById("StorageID_SignItem_TD_Text_" + j).id = "StorageID_SignItem_TD_Text_" + no;
            document.getElementById("FromBillCount_SignItem_TD_Text_" + j).id = "FromBillCount_SignItem_TD_Text_" + no;
            document.getElementById("InCount_SignItem_TD_Text_" + j).id = "InCount_SignItem_TD_Text_" + no;
            document.getElementById("NotInCount_SignItem_TD_Text_" + j).id = "NotInCount_SignItem_TD_Text_" + no;
            document.getElementById("UnitPrice_SignItem_TD_Text_" + j).id = "UnitPrice_SignItem_TD_Text_" + no;
            document.getElementById("TotalPrice_SignItem_TD_Text_" + j).id = "TotalPrice_SignItem_TD_Text_" + no;
            document.getElementById("FromBillID_SignItem_TD_Text_" + j).id = "FromBillID_SignItem_TD_Text_" + no;
            document.getElementById("FromLineNo_SignItem_TD_Text_" + j).id = "FromLineNo_SignItem_TD_Text_" + no;
            document.getElementById("Remark_SignItem_TD_Text_" + j).id = "Remark_SignItem_TD_Text_" + no;
            document.getElementById("InNumber_SignItem_TD_Text_" + j).id = "InNumber_SignItem_TD_Text_" + no;

        }
    }
}

//通过销售退货单带出明细
function AddSignRow_uc(i, InNo, ProductID, ProductNo, ProductName,ColorName, Specification, UnitID, UnitPrice, TotalPrice, FromBillCount,
                 NotInCount, FromLineNo, Remark, InNumber, StorageID,DetailUnitID,IsBatchNo,UsedUnitID,UsedUnitCount) 
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
    newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"" + ProductNo + "\" title=\"" + ProductID + "\" class=\"tdinput\"  readonly=\"readonly\" style='width:90%'  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(3); //添加列:物品名称
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ProductName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ProductName_SignItem_TD_Text_" + rowID + "' value=\"" + ProductName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    var newFitDesctd = newTR.insertCell(4); //添加列:颜色
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ColorName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ColorName_SignItem_TD_Text_" + rowID + "' value=\"" + ColorName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容


    var newFitDesctd = newTR.insertCell(5); //添加列:物品规格
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Specification_' + rowID;
    newFitDesctd.innerHTML = "<input id='Specification_SignItem_TD_Text_" + rowID + "' value=\"" + Specification + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    var newFitNametd = newTR.insertCell(6); //添加列:单位
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_UnitID_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_" + rowID + "\" value=\"" + UnitID + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

var newDanWeitd = newTR.insertCell(7); //添加列:单位组
newDanWeitd.className = "cell";    
newDanWeitd.id = 'SignItem_TD_UnitZ_' + rowID;//divUnitZ
newDanWeitd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容

    var newFitNametd = newTR.insertCell(8); //添加列:仓库ID
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_StorageID_' + rowID;
    newFitNametd.innerHTML = "<select class='tdinput' id=\"StorageID_SignItem_TD_Text_" + rowID + "\"   style=' width:90%;'>" + document.getElementById("ddlStorageInfo").innerHTML + "</select>";
    document.getElementById("StorageID_SignItem_TD_Text_" + rowID).value = StorageID; //赋值当前的的仓库

    var newFitNametd = newTR.insertCell(9); //添加列:原单数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(FromBillCount) + "\"  class=\"tdinput\"  style='width:90%'  readonly=\"readonly\"/>"; //添加列内容

    var newFitNametd = newTR.insertCell(10); //添加列:已入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_InNumber_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InNumber_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(InNumber) + "\"  class=\"tdinput\"  style='width:90%'  readonly=\"readonly\"/>"; //添加列内容


    var newFitNametd = newTR.insertCell(11); //添加列:未入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"NotInCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(NotInCount) + "\"  class=\"tdinput\"  style='width:90%' readonly=\"readonly\"/>"; //添加列内容

    var newFitNametd = newTR.insertCell(12); //添加列:jiben数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(NotInCount) + "\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice(this.id);CountNum();\"/>"; //添加列内容

var newShuLiangtd = newTR.insertCell(13); //添加列:数量?
newShuLiangtd.className = "tdinput";
newShuLiangtd.id = 'SignItem_TD_ProductCount_' + rowID;
newShuLiangtd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_" + rowID + "\" value=\""+NumberSetPoint(UsedUnitCount)+"\"  class=\"tdinput\" style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_z(this.id);CountNum();\"/>"; //添加列内容

if(document.getElementById("hidMoreUnit").value == "true")
{
        newDanWeitd.style.display = "block";
       newShuLiangtd.style.display = "block";
       document.getElementById("InCount_SignItem_TD_Text_"+rowID).readOnly = true;
       document.getElementById("InCount_SignItem_TD_Text_"+rowID).onblur = function(){ return false;}
}
else
{
       newDanWeitd.style.display = "none";
       newShuLiangtd.style.display = "none";
        document.getElementById("InCount_SignItem_TD_Text_"+rowID).readOnly = false;
}

    var newFitDesctd = newTR.insertCell(14); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.style.display = IsDisplayPrice;
    newFitDesctd.id = 'SignItem_TD_UnitPrice_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' value=\"" + NumberSetPoint(UnitPrice) + "\" type='text' class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_a(this.id);CountNum();\" />"; //添加列内容

    var newFitNametd = newTR.insertCell(15); //添加列:物品总价（金额）
    newFitNametd.className = "cell";    
    var productcount = document.getElementById("InCount_SignItem_TD_Text_" + rowID).value; //计算总额
    var unitprice = document.getElementById("UnitPrice_SignItem_TD_Text_" + rowID).value;   
var sss = (productcount * unitprice).toFixed($("#hidSelPoint").val());   
    newFitNametd.style.display = IsDisplayPrice;
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_" + rowID + "\" value=\"" + sss + "\" class=\"tdinput\" readonly=\"readonly\" style='width:90%' />"; //添加列内容
    

    var newFitNametd = newTR.insertCell(16); //添加列:原单编号
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillID_SignItem_TD_Text_" + rowID + "\" value=\"" + InNo + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(17); //添加列:原单行号
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
    if(InNo != "")
    {
        GetUnitGroupSelectEx(ProductID,"StockUnit","DetailUnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,UsedUnitID,"NoShowUnit("+rowID+","+UnitPrice+")");    
    }
    else
    {
        GetUnitGroupSelectEx(ProductID,"StockUnit","DetailUnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,'');    
    }    
}
    var newFitDesctd = newTR.insertCell(18); //添加列:备注
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_" + rowID + "' value=\"" + Remark + "\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

    var newBatchNo = newTR.insertCell(19); //添加列:pici
    newBatchNo.className = "cell";
    newBatchNo.id = 'SignItem_TD_BatchNo_' + rowID;
    newBatchNo.innerHTML = "<input name='chk' id='BatchNo_SignItem_TD_Text_" + rowID + "' value=\"" + IsBatchNo + "\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容
    newBatchNo.style.display="none";
    
    var newJiBen_DanJia = newTR.insertCell(20); //添加列:基本单价
    newJiBen_DanJia.className = "cell";
    newJiBen_DanJia.id = 'TD_JiBen_DanJia_' + rowID;
    newJiBen_DanJia.innerHTML = "<input name='chk' id='JiBen_DanJia_" + rowID + "' value=\"" + UnitPrice + "\" type='text'  />"; //添加列内容
    newJiBen_DanJia.style.display="none";
    
}

function FillSignRow(i, InNo, ProductID, ProductNo, ProductName,ColorName, Specification, UnitID, UnitPrice, TotalPrice, FromBillCount, InCount, 
                    NotInCount, SBNo, FromLineNo, Remark, StorageID, ISNOFromType, InNumber,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,IsBatchNo) 
{ //读取最后一行的行号，存放在txtTRLastIndex文本框中 

    var signFrame = findObj("dg_Log", document);
    var rowID = signFrame.rows.length;
    var newTR = signFrame.insertRow(rowID); //添加行
    newTR.id = "SignItem_Row_" + rowID;

var SetFunNo = "SetEventFocus('ProductNo_SignItem_TD_Text_','InCount_SignItem_TD_Text_'," + rowID + ",false);";
if(document.getElementById("hidMoreUnit").value == "true")
{
    SetFunNo = "SetEventFocus('ProductNo_SignItem_TD_Text_','OutCount_SignItem_TD_Text_'," + rowID + ",false);";
}

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
    //var tdhtml0 = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"" + ProductNo + "\" title=\"" + ProductID + "\" class=\"tdinput\" onclick=\"popTechObj.ShowList(this.id);\"  readonly=\"readonly\" style='width:90%'  />";
    var tdhtml0 = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
                            "<tr><td><input id=\"ProductNo_SignItem_TD_Text_" + rowID + "\"  value=\"" + ProductNo + "\" title=\"" + ProductID + "\"  onblur=\"SetMatchProduct(" + rowID + ",this.value,'ProductNo_SignItem_TD_Text_" + rowID + "');\" onkeydown=\"" + SetFunNo + "\"  class=\"tdinput\" size=\"7\" type=\"text\"></td>" +
                            "<td align=\"right\"><img style=\"cursor: hand\" onclick=\"BeforeShowListSpecial('ProductNo_SignItem_TD_Text_" + rowID + "','');\" align=\"absMiddle\" src=\"../../../Images/search.gif\" height=\"21\"></td>" +
                         "</tr></table>";
    
    
    
    var tdhtml1 = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"" + ProductNo + "\" title=\"" + ProductID + "\" class=\"tdinput\"  readonly=\"readonly\" style='width:90%'  />";
    if (ISNOFromType == 0) {
        newFitNametd.innerHTML = tdhtml0; //添加列内容（无来源）
    }
    else {
        newFitNametd.innerHTML = tdhtml1; //添加列内容（采购退货）
    }

    var newFitDesctd = newTR.insertCell(3); //添加列:物品名称
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ProductName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ProductName_SignItem_TD_Text_" + rowID + "' value=\"" + ProductName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    var newFitDesctd = newTR.insertCell(4); //添加列:颜色
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ColorName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ColorName_SignItem_TD_Text_" + rowID + "' value=\"" + ColorName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容


    var newFitDesctd = newTR.insertCell(5); //添加列:物品规格
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Specification_' + rowID;
    newFitDesctd.innerHTML = "<input id='Specification_SignItem_TD_Text_" + rowID + "' value=\"" + Specification + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    var newFitNametd = newTR.insertCell(6); //添加列:单位
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_UnitID_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_" + rowID + "\" value=\"" + UnitID + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

var newDanWeitd = newTR.insertCell(7); //添加列:单位组
newDanWeitd.className = "cell";    
newDanWeitd.id = 'SignItem_TD_UnitZ_' + rowID;//divUnitZ
newDanWeitd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容

    var newFitNametd = newTR.insertCell(8); //添加列:仓库ID
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_StorageID_' + rowID;
    newFitNametd.innerHTML = "<select class='tdinput' id=\"StorageID_SignItem_TD_Text_" + rowID + "\"   style=' width:90%;'>" + document.getElementById("ddlStorageInfo").innerHTML + "</select>";
    document.getElementById("StorageID_SignItem_TD_Text_" + rowID).value = StorageID; //赋值当前的的仓库

    var newFitNametd = newTR.insertCell(9); //添加列:原单数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(FromBillCount) + "\"  readonly=\"readonly\" class=\"tdinput\"  style='width:90%'/>"; //添加列内容

    var newFitNametd = newTR.insertCell(10); //添加列:已入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_InNumber_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InNumber_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(InNumber) + "\"  class=\"tdinput\"  style='width:90%'  readonly=\"readonly\"/>"; //添加列内容


    var newFitNametd = newTR.insertCell(11); //添加列:未入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"NotInCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(NotInCount) + "\"  class=\"tdinput\"  style='width:90%' readonly=\"readonly\"/>"; //添加列内容

    var newFitNametd = newTR.insertCell(12); //添加列:入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(InCount) + "\"  class=\"tdinput\"  style='width:90%'  onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice(this.id);CountNum();\"    onkeydown=\"SetEventFocus('InCount_SignItem_TD_Text_','UnitPrice_SignItem_TD_Text_'," + rowID + ",false);\"    />"; //添加列内容

if(UsedUnitCount == "")
{
    UsedUnitCount = "0";
}
var newShuLiangtd = newTR.insertCell(13); //添加列:数量?
newShuLiangtd.className = "tdinput";
newShuLiangtd.id = 'SignItem_TD_ProductCount_' + rowID;
newShuLiangtd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_" + rowID + "\" value=\"" + NumberSetPoint(UsedUnitCount) + "\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_z(this.id);CountNum();\"    onkeydown=\"SetEventFocus('OutCount_SignItem_TD_Text_','UnitPrice_SignItem_TD_Text_'," + rowID + ",false);\"   />"; //添加列内容


if(document.getElementById("hidMoreUnit").value == "true")
{
       newDanWeitd.style.display = "block";
       newShuLiangtd.style.display = "block";
       document.getElementById("InCount_SignItem_TD_Text_"+rowID). readOnly = true;
       document.getElementById("InCount_SignItem_TD_Text_"+rowID).onblur = function(){ return false;}
}
else
{
       newDanWeitd.style.display = "none";
       newShuLiangtd.style.display = "none";
        document.getElementById("InCount_SignItem_TD_Text_"+rowID).readOnly = false;
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

    var newFitDesctd = newTR.insertCell(14); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.style.display = IsDisplayPrice;
    newFitDesctd.id = 'SignItem_TD_UnitPrice_' + rowID;
    //var tdhtml3 = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' value=\"" + NumberSetPoint(UsedPrice) + "\" type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />";
    var tdhtml4 = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' value=\"" + NumberSetPoint(danjia) + "\" type='text' class=\"tdinput\"  style='width:90%'  onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_a(this.id);CountNum();\"    onkeydown=\"SetEventFocus('UnitPrice_SignItem_TD_Text_','ProductNo_SignItem_TD_Text_'," + rowID + ",true);\"   />";
    newFitDesctd.innerHTML = tdhtml4; //添加列内容（无来源）

//    if (ISNOFromType == 0) {
//        newFitDesctd.innerHTML = tdhtml4; //添加列内容（无来源）
//    }
//    else {
//        newFitDesctd.innerHTML = tdhtml3; //添加列内容（采购退货）
//    }


    var newFitNametd = newTR.insertCell(15); //添加列:物品总价（金额）
    newFitNametd.className = "cell";
    newFitNametd.style.display = IsDisplayPrice;
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_" + rowID + "\" value=\""+NumberSetPoint(TotalPrice)+"\" class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />"; //添加列内容

//    var productcount = document.getElementById("InCount_SignItem_TD_Text_" + rowID).value; //计算总额
//    var unitprice = document.getElementById("UnitPrice_SignItem_TD_Text_" + rowID).value;
//    document.getElementById("TotalPrice_SignItem_TD_Text_" + rowID).value = (productcount * unitprice).toFixed($("#hidSelPoint").val());
    
//绑定单位组
if(document.getElementById("hidMoreUnit").value == "true")
{
    if(SBNo != "")
    {
        GetUnitGroupSelectEx(ProductID,"StockUnit","DetailUnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,UsedUnitID,"NoShowUnit("+rowID+")");
    }
    else
    {
        //GetUnitGroupSelectEx(ProductID,"StockUnit","DetailUnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,UsedUnitID);
        GetUnitGroupSelect(ProductID,"StockUnit","DetailUnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,UsedUnitID);
    }
}

    var newFitNametd = newTR.insertCell(16); //添加列:原单编号
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillID_SignItem_TD_Text_" + rowID + "\" value=\"" + SBNo + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(17); //添加列:原单行号
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='FromLineNo_SignItem_TD_Text_" + rowID + "' value=\"" + FromLineNo + "\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(18); //添加列:备注
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_" + rowID + "' value=\"" + Remark + "\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容

    var newBatchNo = newTR.insertCell(19); //添加列:pici
    newBatchNo.className = "cell";
    newBatchNo.id = 'SignItem_TD_BatchNo_' + rowID;
    newBatchNo.innerHTML = "<input name='chk' id='BatchNo_SignItem_TD_Text_" + rowID + "' value=\"" + IsBatchNo + "\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容
    newBatchNo.style.display="none";
    //txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
    
    var newJiBen_DanJia = newTR.insertCell(20); //添加列:基本单价
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


function AddRow(id, ProNo, ProdName,ColorName, UnitName, Specification, UnitPrice,UnitID,IsBatchNo)//增加一个空白行
{
//读取最后一行的行号，存放在txtTRLastIndex文本框中 
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
    newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"" + ProNo + "\" title=\"" + id + "\"  class=\"tdinput\" onclick=\"popTechObj.ShowList(this.id);\"  readonly=\"readonly\" style='width:90%'  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(3); //添加列:物品名称
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ProductName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ProductName_SignItem_TD_Text_" + rowID + "' value=\"" + ProdName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    var newFitDesctd = newTR.insertCell(4); //添加列:颜色
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ColorName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ColorName_SignItem_TD_Text_" + rowID + "' value=\"" + ColorName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容


    var newFitDesctd = newTR.insertCell(5); //添加列:物品规格
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Specification_' + rowID;
    newFitDesctd.innerHTML = "<input id='Specification_SignItem_TD_Text_" + rowID + "' value=\"" + Specification + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    var newFitNametd = newTR.insertCell(6); //添加列:基本单位
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_UnitID_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_" + rowID + "\" value=\"" + UnitName + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

var newDanWeitd = newTR.insertCell(7); //添加列:单位组
newDanWeitd.className = "cell";    
newDanWeitd.id = 'SignItem_TD_UnitZ_' + rowID;//divUnitZ
newDanWeitd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容

    var newFitNametd = newTR.insertCell(8); //添加列:仓库ID
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_StorageID_' + rowID;
    newFitNametd.innerHTML = "<select class='tdinput' id=\"StorageID_SignItem_TD_Text_" + rowID + "\"   style=' width:90%;'>" + document.getElementById("ddlStorageInfo").innerHTML + "</select>";

    var newFitNametd = newTR.insertCell(9); //添加列:原单数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillCount_SignItem_TD_Text_" + rowID + "\" value=\"\"  class=\"tdinput\" readonly=\"readonly\" style='width:90%'/>"; //添加列内容

    var newFitNametd = newTR.insertCell(10); //添加列:已入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_InNumber_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InNumber_SignItem_TD_Text_" + rowID + "\" value=\"\"  class=\"tdinput\"  style='width:90%'  readonly=\"readonly\"/>"; //添加列内容


    var newFitNametd = newTR.insertCell(11); //添加列:未入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"NotInCount_SignItem_TD_Text_" + rowID + "\" value=\"\"  class=\"tdinput\"  style='width:90%' readonly=\"readonly\"/>"; //添加列内容

    var newFitNametd = newTR.insertCell(12); //添加列:jiben数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InCount_SignItem_TD_Text_" + rowID + "\" value=\"0\"  class=\"tdinput\"  style='width:90%'  onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice(this.id);CountNum();\"/>"; //添加列内容

 var newShuLiangtd = newTR.insertCell(13); //添加列:数量?
newShuLiangtd.className = "tdinput";
newShuLiangtd.id = 'SignItem_TD_ProductCount_' + rowID;
newShuLiangtd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_" + rowID + "\" value=\"0\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_z(this.id);CountNum();\"/>"; //添加列内容

if(document.getElementById("hidMoreUnit").value == "true")
{
        newDanWeitd.style.display = "block";
       newShuLiangtd.style.display = "block";
       document.getElementById("InCount_SignItem_TD_Text_"+rowID).readOnly = true;
       document.getElementById("InCount_SignItem_TD_Text_"+rowID).onblur = function(){ return false;}
}
else
{
       newDanWeitd.style.display = "none";
       newShuLiangtd.style.display = "none";
        document.getElementById("InCount_SignItem_TD_Text_"+rowID).readOnly = false;
}
    var newFitDesctd = newTR.insertCell(14); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.style.display = IsDisplayPrice;
    newFitDesctd.id = 'SignItem_TD_UnitPrice_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' value=\"" + NumberSetPoint(UnitPrice) + "\" type='text' class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_a(this.id);CountNum();\"/>"; //添加列内容

    var newFitNametd = newTR.insertCell(15); //添加列:物品总价（金额）
    newFitNametd.className = "cell";
    newFitNametd.style.display = IsDisplayPrice;
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_" + rowID + "\" value=\"0\" class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />"; //添加列内容

    var newFitNametd = newTR.insertCell(16); //添加列:原单编号
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillID_SignItem_TD_Text_" + rowID + "\" value=\"\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(17); //添加列:原单行号
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='FromLineNo_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

//绑定单位组
if(document.getElementById("hidMoreUnit").value == "true")
{
    GetUnitGroupSelectEx(id,"StockUnit","DetailUnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,'');
}

    var newFitDesctd = newTR.insertCell(18); //添加列:备注
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容

    var newBatchNo = newTR.insertCell(19); //添加列:pici
    newBatchNo.className = "cell";
    newBatchNo.id = 'SignItem_TD_BatchNo_' + rowID;
    newBatchNo.innerHTML = "<input name='chk' id='BatchNo_SignItem_TD_Text_" + rowID + "' value=\"" + IsBatchNo + "\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容
    newBatchNo.style.display="none";
    //txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
    
    var newJiBen_DanJia = newTR.insertCell(20); //添加列:基本单价
    newJiBen_DanJia.className = "cell";
    newJiBen_DanJia.id = 'TD_JiBen_DanJia_' + rowID;
    newJiBen_DanJia.innerHTML = "<input name='chk' id='JiBen_DanJia_" + rowID + "' value=\"" + UnitPrice + "\" type='text'  />"; //添加列内容
    newJiBen_DanJia.style.display="none";
}

function AddRow_kb()//增加一个空白行
{
//id, ProNo, ProdName,ColorName, UnitName, Specification, UnitPrice,UnitID,IsBatchNo
//读取最后一行的行号，存放在txtTRLastIndex文本框中 
    var signFrame = findObj("dg_Log", document);
    var rowID = signFrame.rows.length;
    var newTR = signFrame.insertRow(rowID); //添加行
    newTR.id = "SignItem_Row_" + rowID;
    
var SetFunNo = "SetEventFocus('ProductNo_SignItem_TD_Text_','InCount_SignItem_TD_Text_'," + rowID + ",false);";
if(document.getElementById("hidMoreUnit").value == "true")
{
    SetFunNo = "SetEventFocus('ProductNo_SignItem_TD_Text_','OutCount_SignItem_TD_Text_'," + rowID + ",false);";
}

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
    //newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"\" title=\"\"  class=\"tdinput\" onclick=\"popTechObj.ShowList(this.id);\"  readonly=\"readonly\" style='width:90%'  />"; //添加列内容
    newFitNametd.innerHTML = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
                            "<tr><td><input id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" onblur=\"SetMatchProduct(" + rowID + ",this.value,'ProductNo_SignItem_TD_Text_" + rowID + "');\" onkeydown=\"" + SetFunNo + "\"  class=\"tdinput\" size=\"7\" type=\"text\"></td>" +
                            "<td align=\"right\"><img style=\"cursor: hand\"  onclick=\"BeforeShowListSpecial('ProductNo_SignItem_TD_Text_" + rowID + "','');\"  align=\"absMiddle\" src=\"../../../Images/search.gif\" height=\"21\"></td>" +
                         "</tr></table>";              //onclick=\"popTechObj.ShowListSpecial('ProductNo_SignItem_TD_Text_" + rowID + "','');\"
                         
    var newFitDesctd = newTR.insertCell(3); //添加列:物品名称
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ProductName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ProductName_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' disabled />"; ; //添加列内容

    var newFitDesctd = newTR.insertCell(4); //添加列:颜色
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ColorName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ColorName_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' disabled />"; ; //添加列内容


    var newFitDesctd = newTR.insertCell(5); //添加列:物品规格
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Specification_' + rowID;
    newFitDesctd.innerHTML = "<input id='Specification_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' disabled />"; ; //添加列内容

    var newFitNametd = newTR.insertCell(6); //添加列:基本单位
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_UnitID_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_" + rowID + "\" value=\"\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%' disabled  />"; //添加列内容

var newDanWeitd = newTR.insertCell(7); //添加列:单位组
newDanWeitd.className = "cell";    
newDanWeitd.id = 'SignItem_TD_UnitZ_' + rowID;//divUnitZ
newDanWeitd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容

    var newFitNametd = newTR.insertCell(8); //添加列:仓库ID
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_StorageID_' + rowID;
    newFitNametd.innerHTML = "<select class='tdinput' id=\"StorageID_SignItem_TD_Text_" + rowID + "\"   style=' width:90%;'>" + document.getElementById("ddlStorageInfo").innerHTML + "</select>";

    var newFitNametd = newTR.insertCell(9); //添加列:原单数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillCount_SignItem_TD_Text_" + rowID + "\" value=\"\"  class=\"tdinput\" readonly=\"readonly\" style='width:90%'/>"; //添加列内容

    var newFitNametd = newTR.insertCell(10); //添加列:已入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_InNumber_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InNumber_SignItem_TD_Text_" + rowID + "\" value=\"\"  class=\"tdinput\"  style='width:90%'  readonly=\"readonly\"/>"; //添加列内容


    var newFitNametd = newTR.insertCell(11); //添加列:未入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"NotInCount_SignItem_TD_Text_" + rowID + "\" value=\"\"  class=\"tdinput\"  style='width:90%' readonly=\"readonly\"/>"; //添加列内容

    var newFitNametd = newTR.insertCell(12); //添加列:jiben数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InCount_SignItem_TD_Text_" + rowID + "\" value=\"0\"  class=\"tdinput\"  style='width:90%'  onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice(this.id);CountNum();\"   onkeydown=\"SetEventFocus('InCount_SignItem_TD_Text_','UnitPrice_SignItem_TD_Text_'," + rowID + ",false);\"    />"; //添加列内容

 var newShuLiangtd = newTR.insertCell(13); //添加列:数量?
newShuLiangtd.className = "tdinput";
newShuLiangtd.id = 'SignItem_TD_ProductCount_' + rowID;
newShuLiangtd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_" + rowID + "\" value=\"0\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_z(this.id);CountNum();\"    onkeydown=\"SetEventFocus('OutCount_SignItem_TD_Text_','UnitPrice_SignItem_TD_Text_'," + rowID + ",false);\"    />"; //添加列内容

if(document.getElementById("hidMoreUnit").value == "true")
{
        newDanWeitd.style.display = "block";
       newShuLiangtd.style.display = "block";
       document.getElementById("InCount_SignItem_TD_Text_"+rowID).readOnly = true;
       document.getElementById("InCount_SignItem_TD_Text_"+rowID).onblur = function(){ return false;}
}
else
{
       newDanWeitd.style.display = "none";
       newShuLiangtd.style.display = "none";
        document.getElementById("InCount_SignItem_TD_Text_"+rowID).readOnly = false;
}
    var newFitDesctd = newTR.insertCell(14); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.style.display = IsDisplayPrice;
    newFitDesctd.id = 'SignItem_TD_UnitPrice_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_a(this.id);CountNum();\"    onkeydown=\"SetEventFocus('UnitPrice_SignItem_TD_Text_','ProductNo_SignItem_TD_Text_'," + rowID + ",true);\"   />"; //添加列内容

    var newFitNametd = newTR.insertCell(15); //添加列:物品总价（金额）
    newFitNametd.className = "cell";
    newFitNametd.style.display = IsDisplayPrice;
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_" + rowID + "\" value=\"0\" class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />"; //添加列内容

    var newFitNametd = newTR.insertCell(16); //添加列:原单编号
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillID_SignItem_TD_Text_" + rowID + "\" value=\"\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(17); //添加列:原单行号
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='FromLineNo_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

//绑定单位组
//if(document.getElementById("hidMoreUnit").value == "true")
//{
//    GetUnitGroupSelectEx(id,"StockUnit","DetailUnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,'');
//}

    var newFitDesctd = newTR.insertCell(18); //添加列:备注
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容

    var newBatchNo = newTR.insertCell(19); //添加列:pici
    newBatchNo.className = "cell";
    newBatchNo.id = 'SignItem_TD_BatchNo_' + rowID;
    newBatchNo.innerHTML = "<input name='chk' id='BatchNo_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容
    newBatchNo.style.display="none";
    //txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
    
    var newJiBen_DanJia = newTR.insertCell(20); //添加列:基本单价
    newJiBen_DanJia.className = "cell";
    newJiBen_DanJia.id = 'TD_JiBen_DanJia_' + rowID;
    newJiBen_DanJia.innerHTML = "<input name='chk' id='JiBen_DanJia_" + rowID + "' value=\"\" type='text'  />"; //添加列内容
    newJiBen_DanJia.style.display="none";
}

//选择单位时计算
 function ChangeUnit(ObjID/*下拉列表控件（单位）*/,rowid/*行号*/,UnitPrice)
 { 
    if($("#OutCount_SignItem_TD_Text_"+rowid).val() != "")
    {
        var EXRate = $("#"+ObjID).val().split('|')[1].toString(); /*比率*/       
        //var EXRate = ObjID.value.split('|')[1].toString(); /*换算率*/
        var ReBasePCount = $("#OutCount_SignItem_TD_Text_"+rowid).val() * EXRate;
        var DanJia = EXRate * UnitPrice;//单价 = 比率 * 基本单价
        
        $("#InCount_SignItem_TD_Text_"+ rowid).val(ReBasePCount.toFixed($("#hidSelPoint").val()));//基本数量= 数量*比率
        $("#UnitPrice_SignItem_TD_Text_"+rowid).val(DanJia.toFixed($("#hidSelPoint").val()));//单价
        
         $("#TotalPrice_SignItem_TD_Text_"+rowid).val(($("#OutCount_SignItem_TD_Text_"+rowid).val() * DanJia).toFixed($("#hidSelPoint").val()));
    }
    CountNum();
 }

function SelectStorage(id, value) {
    document.getElementById('StorageID_' + id).value = value;
}

function TotalPrice(id) {
    var idstr = id.substring(8, id.length);
    var count = id;
    var productcount = document.getElementById(count).value;
    var unitprice = document.getElementById('UnitPrice_' + idstr).value;
    document.getElementById('TotalPrice_' + idstr).value = (productcount * unitprice).toFixed($("#hidSelPoint").val());
}

//填写数量后，根据选中单位，更改基本数量
function TotalPrice_z(id)
{
     var idstr = id.substring(8, id.length);   
     var count = 'DetailUnitID' + idstr;
   
    try
    {
         var EXRate = $("#"+count).val().split('|')[1].toString(); /*比率*/   
         var ReCount = $("#"+id).val() * EXRate;
          $("#"+"InCount" + idstr).val(ReCount.toFixed($("#hidSelPoint").val()));
          
         var ReTotaoPrice = $("#OutCount" + idstr).val() * $("#UnitPrice" + idstr).val();      
         $("#TotalPrice" + idstr).val(ReTotaoPrice.toFixed($("#hidSelPoint").val()));
    }
    catch(e)
    {}
    
}

function TotalPrice_a(id) {
    var idstr = id.substring(10, id.length);
    var count = 'InCount_' + idstr;
    var productcount = document.getElementById(count).value;
    var unitprice = document.getElementById('UnitPrice_' + idstr).value;
    document.getElementById('TotalPrice_' + idstr).value = (productcount * unitprice).toFixed($("#hidSelPoint").val());
}


//tempOnclick = "" + item.ID + ",'" + item.ProdNo + "','" + item.ProductName + "','" + item.StandardSell + "',\
//      '" + item.UnitID + "','" + item.CodeName + "','" + item.TaxRate + "','" + item.SellTax + "','" + item.Discount + "',\
//      '" + item.Specification + "','" + item.CodeTypeName + "','" + item.TypeID + "','" + item.StandardCost + "','" + item.Source + "',\
//      '" + item.GroupUnitNo + "','" + item.SaleUnitID + "','" + item.SaleUnitName + "',\
//      '" + item.InUnitID + "','" + item.InUnitName + "', '" + item.StockUnitID + "','" + item.StockUnitName + "',\
//     '" + item.MakeUnitID + "','" + item.MakeUnitName + "', '" + item.IsBatchNo + "','"+item.ColorName+"'";

function Fun_FillParent_Content(id, ProNo, ProdName, a, 
        UnitID, UnitName, b,c, d, 
        Specification, a1,a2, UnitPrice, a4, 
        a5,a15,a6,
        a7,a8,a10,a11,
        a12,a13,IsBatchNo,ColorName) 
{

document.getElementById("hidShowP").value = "0";
if(UnitPrice == "")
{
    UnitPrice = "0";
}
    var idstr = popTechObj.InputObj.substring(10, popTechObj.InputObj.length);
    var Price = 'UnitPrice_' + idstr;
    var ProductNo = 'ProductNo_' + idstr;
    var ProductName = 'ProductName_' + idstr;
    var colorName = 'ColorName_' + idstr;//颜色
    var unitID = 'UnitID_' + idstr;
    var specification = 'Specification_' + idstr;
    var jibendj = 'JiBen_DanJia_' + idstr.substring(17, popTechObj.InputObj.length);
    var batchno = 'BatchNo_' + idstr;//是否启用批次
   
    document.getElementById(ProductNo).value = ProNo;
    $("#" + ProductNo).attr("title", id);
    document.getElementById(ProductName).value = ProdName;
    document.getElementById(colorName).value = ColorName;
    
    document.getElementById(Price).value = UnitPrice;
    document.getElementById(jibendj).value = UnitPrice;
    
    document.getElementById(unitID).value = UnitName;
    document.getElementById(batchno).value = IsBatchNo;
    
    if(document.getElementById("hidMoreUnit").value == "true")
    {

        //var Func = "ChangeUnit(DetailUnitID_"+idstr+","+idstr.substring(17, popTechObj.InputObj.length)+","+UnitPrice+")";
            var Func = "ChangeUnit(this.id,"+idstr.substring(17, popTechObj.InputObj.length)+","+UnitPrice+")";
        var Obj ="unitdiv" + idstr.substring(17, popTechObj.InputObj.length);
       
        GetUnitGroupSelect(id,'StockUnit','DetailUnitID_' + idstr,Func,Obj,'');
        //GetUnitGroupSelectEx(id,"StockUnit",'UnitID_' + idstr,Func,Obj,'','');
    }
   
    
    document.getElementById(specification).value = Specification;
    document.getElementById('divStorageProduct').style.display = 'none';

}


//验证唯一性
function checkonly() {
    var InNo = document.getElementById('txtInNo').value;
    var TableName = "officedba.StorageInOther";
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
        if (document.getElementById("ddlFromType").value == "1") {
            for (i = 1; i < signFrame.rows.length; i++) 
            {
                var InCount = "";
                if(document.getElementById("hidMoreUnit").value == "true")
                {
                    InCount = "OutCount_SignItem_TD_Text_" + i;
                }
                else
                {
                    InCount = "InCount_SignItem_TD_Text_" + i;
                }
                    
                var NotInCount = "NotInCount_SignItem_TD_Text_" + i;
                if (parseFloat(document.getElementById(InCount).value) > parseFloat(document.getElementById(NotInCount).value)) {
                    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "入库数量不能大于未入库数量！");
                    return false;
                }
            }
        }
        /*--------------------------以上判断是否大于源单未入库数量--------------------------------------*/
        var txtIndentityID = $("#txtIndentityID").val();
        var act = "Confirm";
        var TotalPrice = document.getElementById("txtTotalPrice").value;
        
        var UrlParam = "&Act=Confirm&TotalPrice="+escape(TotalPrice)+"&ID=" + txtIndentityID.toString() + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageInOtherAdd.ashx",
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
                        document.getElementById('btn_AddRow').onclick = function() { return false; };
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
            url: "../../../Handler/Office/StorageManager/StorageInOtherInfo.ashx?" + UrlParam,
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
            url: "../../../Handler/Office/StorageManager/StorageInOtherInfo.ashx?" + UrlParam,
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


function fnSelectSBInfo() {
    if ($("#ddlFromType").val() == 1) {
        popSellBackObj.ShowList('txtFromBillID');
    }
}

function FillBasicInfo(TaskNo, FromBillID, ProcessType, ProcessDeptID, ProcessDeptName, Processor, ProcessorName) {
    $("#txtFromBillID").val(TaskNo);
    $("#txtFromBillID").attr("title", FromBillID);
    $("#ddlProcessType").val(ProcessType); //加工类别
    $("#txtProcessDept").val(ProcessDeptName);
    $("#txtProcessDept").attr("title", ProcessDeptID); //加工部门
    $("#txtProcessor").val(ProcessorName); //负责人
    $("#txtProcessor").attr("title", Processor);
}


function DoChange() {
    document.getElementById('txtFromBillID').value = "";
    $("#txtFromBillID").attr("title", "");
    fnDelRow();
    var InType = document.getElementById('ddlFromType').value;
    if (InType != "0") {
        //popSellBackObj.ShowList('txtFromBillID');
        document.getElementById('btn_AddRow').onclick = function() { return false; };
        document.getElementById('txtOtherCorp').onclick = function() { return false; };
        $("#spanhide").show();
        //$("#btn_AddRow").hide();
    }
    else {
        document.getElementById('btn_AddRow').onclick = ShowProdInfo;
        document.getElementById('txtOtherCorp').onclick = alerTreediv;
        $("#spanhide").hide();
        //$("#btn_AddRow").show();
        //closeSellBackdiv();
        document.getElementById('txtFromBillID').value = "";
        $("#txtFromBillID").attr("title", "");
        fnDelRow();

        $("#txtOtherCorp").val("");
        $("#txtOtherCorpID").val("");
    }
}

/*-----------------明细中输入数据格式验证---------------------*/
function check(str) {
    fieldText = ""; var msgText = ""; var isFlag = true;
    if (document.getElementById(str).value.length > 0) {
        if (!IsNumeric(document.getElementById(str).value)) {
            isFlag = false;
            fieldText = fieldText + "|";
            msgText = msgText + "入库数量输入有误,请输入有效的数值（大于0）！";
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
    window.location.href = "StorageInOtherList.aspx?ModuleID=" + moduleID + searchCondition;
}


/*----------------------------禁止按钮事件-------------------------------------*/
function jinzhi()
{ return false; }
/*-----------------------------------------------------------------*/


/*-----------------------------------Start 往来客户----------------------------------------------------*/

function alerTreediv() {

    document.getElementById('zhezhaoTree').style.display = "";
    document.getElementById('divTree').style.display = "";
}
function hideTreediv() {
    document.getElementById('divTree').style.display = "none";
    document.getElementById('zhezhaoTree').style.display = "none";

}

function SelectedNodeChanged(Name, ID, type) {
    document.getElementById('txtOtherCorpID').value = ID;
    document.getElementById('txtOtherCorp').value = Name;
    document.getElementById('sltCorpBigType').value = type;
    document.getElementById('divTree').style.display = "none"
    document.getElementById('zhezhaoTree').style.display = "none";
}

function cleareCompany() {
    document.getElementById('divTree').style.display = "none";
    document.getElementById('zhezhaoTree').style.display = "none";
    document.getElementById('txtOtherCorpID').value = "";
    document.getElementById('txtOtherCorp').value = "";
    document.getElementById('sltCorpBigType').value = "";
}
/*-----------------------------------End 往来客户----------------------------------------------------*/



/*单据打印*/
function BillPrint() {
    var ID = $("#txtIndentityID").val();
    if (ID == "" || ID == "0") {
        popMsgObj.Show("打印|", "请保存单据再打印|");
        return;
    }
    window.open("../../../Pages/PrinttingModel/StorageManager/StorageInOtherPrint1.aspx?ID=" + ID);
}



/*-----------------------------------------------------条码扫描Start-----------------------------------------------------------------*/

var rerowID = "";
//判断是否有相同记录有返回true，没有返回false
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


// GetGoodsDataByBarCode(item.data[0].ID,item.data[0].ProdNo,item.data[0].ProductName,
//                                                  item.data[0].StandardSell,item.data[0].UnitID,item.data[0].CodeName,
//                                                  item.data[0].TaxRate,item.data[0].SellTax,item.data[0].Discount,
//                                                  item.data[0].Specification,item.data[0].CodeTypeName,item.data[0].TypeID,
//                                                  item.data[0].StandardBuy,item.data[0].TaxBuy,item.data[0].InTaxRate,
//                                                  item.data[0].StandardCost);
//条码扫描方法
function GetGoodsDataByBarCode(id, ProNo, ProdName,a, UnitID, UnitName,b, c, d,Specification, e, f,g, h, i,UnitPrice,ColorName) 
{
    if (!IsExist(ProNo))//如果重复记录，就不增加
    {
        AddRowByBarCode(id, ProNo, ProdName, UnitName, Specification, UnitPrice,UnitID,ColorName);
    }
    else
    {
        var productcount = "";
         if(document.getElementById("hidMoreUnit").value == "true")
         {
                document.getElementById("OutCount_SignItem_TD_Text_" + rerowID).value = (parseFloat(document.getElementById("OutCount_SignItem_TD_Text_" + rerowID).value) + 1).toFixed($("#hidSelPoint").val());
                productcount = document.getElementById("OutCount_SignItem_TD_Text_" + rerowID).value;
                
                 //基本数量
                var ssss = Q_EXRate * parseFloat(document.getElementById("OutCount_SignItem_TD_Text_" + rerowID).value);
                document.getElementById("InCount_SignItem_TD_Text_" + rerowID).value = ssss.toFixed($("#hidSelPoint").val());
            
         }
         else
         {
                document.getElementById("InCount_SignItem_TD_Text_" + rerowID).value = (parseFloat(document.getElementById("InCount_SignItem_TD_Text_" + rerowID).value) + 1).toFixed($("#hidSelPoint").val()); 
                productcount = document.getElementById("InCount_SignItem_TD_Text_" + rerowID).value;
         }
        
        var unitprice = document.getElementById("UnitPrice_SignItem_TD_Text_" + rerowID).value;
        document.getElementById("TotalPrice_SignItem_TD_Text_" + rerowID).value = (productcount * unitprice).toFixed($("#hidSelPoint").val());
        CountNum();
    }

}


//扫描增加行
function AddRowByBarCode(id, ProNo, ProdName, UnitName, Specification, UnitPrice,UnitID,ColorName) 
{ //读取最后一行的行号，存放在txtTRLastIndex文本框中
    var signFrame = findObj("dg_Log", document);
    var rowID = signFrame.rows.length;
    var newTR = signFrame.insertRow(rowID); //添加行
    newTR.id = "SignItem_Row_" + rowID;

var SetFunNo = "SetEventFocus('ProductNo_SignItem_TD_Text_','InCount_SignItem_TD_Text_'," + rowID + ",false);";
if(document.getElementById("hidMoreUnit").value == "true")
{
    SetFunNo = "SetEventFocus('ProductNo_SignItem_TD_Text_','OutCount_SignItem_TD_Text_'," + rowID + ",false);";
}

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
    //newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"" + ProNo + "\" title=\"" + id + "\" class=\"tdinput\" onclick=\"popTechObj.ShowList(this.id);\"  readonly=\"readonly\" style='width:90%'  />"; //添加列内容
    newFitNametd.innerHTML = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
                            "<tr><td><input id=\"ProductNo_SignItem_TD_Text_" + rowID + "\"  value=\"" + ProNo + "\" title=\"" + id + "\"  onblur=\"SetMatchProduct(" + rowID + ",this.value,'ProductNo_SignItem_TD_Text_" + rowID + "');\" onkeydown=\"" + SetFunNo + "\"  class=\"tdinput\" size=\"7\" type=\"text\"></td>" +
                            "<td align=\"right\"><img style=\"cursor: hand\"  onclick=\"BeforeShowListSpecial('ProductNo_SignItem_TD_Text_" + rowID + "','');\"  align=\"absMiddle\" src=\"../../../Images/search.gif\" height=\"21\"></td>" +
                         "</tr></table>"; 

    var newFitDesctd = newTR.insertCell(3); //添加列:物品名称
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ProductName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ProductName_SignItem_TD_Text_" + rowID + "' value=\"" + ProdName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

 var newFitDesctd = newTR.insertCell(4); //添加列:颜色
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ColorName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ColorName_SignItem_TD_Text_" + rowID + "' value=\"" + ColorName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    var newFitDesctd = newTR.insertCell(5); //添加列:物品规格
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Specification_' + rowID;
    newFitDesctd.innerHTML = "<input id='Specification_SignItem_TD_Text_" + rowID + "' value=\"" + Specification + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容

    var newFitNametd = newTR.insertCell(6); //添加列:单位
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_UnitID_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_" + rowID + "\" value=\"" + UnitName + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

var newDanWeitd = newTR.insertCell(7); //添加列:单位组
newDanWeitd.className = "cell";    
newDanWeitd.id = 'SignItem_TD_UnitZ_' + rowID;//divUnitZ
newDanWeitd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容

    var newFitNametd = newTR.insertCell(8); //添加列:仓库ID
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_StorageID_' + rowID;
    newFitNametd.innerHTML = "<select class='tdinput' id=\"StorageID_SignItem_TD_Text_" + rowID + "\"   style=' width:90%;'>" + document.getElementById("ddlStorageInfo").innerHTML + "</select>";

    var newFitNametd = newTR.insertCell(9); //添加列:原单数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillCount_SignItem_TD_Text_" + rowID + "\" value=\"\"  class=\"tdinput\" readonly=\"readonly\" style='width:90%'/>"; //添加列内容

    var newFitNametd = newTR.insertCell(10); //添加列:已入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_InNumber_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InNumber_SignItem_TD_Text_" + rowID + "\" value=\"\"  class=\"tdinput\"  style='width:90%'  readonly=\"readonly\"/>"; //添加列内容

    var newFitNametd = newTR.insertCell(11); //添加列:未入库数量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"NotInCount_SignItem_TD_Text_" + rowID + "\" value=\"\"  class=\"tdinput\"  style='width:90%' readonly=\"readonly\"/>"; //添加列内容

    var newFitNametd = newTR.insertCell(12); //添加列:入库数量
var jbsl = parseFloat(1).toFixed($("#hidSelPoint").val())
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"InCount_SignItem_TD_Text_" + rowID + "\" value=\"" + jbsl + "\"  class=\"tdinput\"  style='width:90%'  onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice(this.id);CountNum();\"/>"; //添加列内容

var newShuLiangtd = newTR.insertCell(13); //添加列:数量?
var shuliang = parseFloat(1).toFixed($("#hidSelPoint").val())
newShuLiangtd.className = "tdinput";
newShuLiangtd.id = 'SignItem_TD_ProductCount_' + rowID;
newShuLiangtd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_" + rowID + "\" value=\"" + shuliang + "\"  class=\"tdinput\" style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_z(this.id);CountNum();\"/>"; //添加列内容

if(document.getElementById("hidMoreUnit").value == "true")
{
        newDanWeitd.style.display = "block";
       newShuLiangtd.style.display = "block";
       document.getElementById("InCount_SignItem_TD_Text_"+rowID).readOnly = true;
       document.getElementById("InCount_SignItem_TD_Text_"+rowID).onblur = function(){ return false;}
}
else
{
       newDanWeitd.style.display = "none";
       newShuLiangtd.style.display = "none";
        document.getElementById("InCount_SignItem_TD_Text_"+rowID).readOnly = false;
}

    var newFitDesctd = newTR.insertCell(14); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.style.display = IsDisplayPrice;
    newFitDesctd.id = 'SignItem_TD_UnitPrice_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' value=\"" + NumberSetPoint(UnitPrice) + "\" type='text' class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_a(this.id);CountNum();\"/>"; //添加列内容

    var newFitNametd = newTR.insertCell(15); //添加列:物品总价（金额）
    newFitNametd.className = "cell";
    newFitNametd.style.display = IsDisplayPrice;
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_" + rowID + "\" value=\"\" class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />"; //添加列内容
    var productcount = document.getElementById("InCount_SignItem_TD_Text_" + rowID).value; //计算总额
    var unitprice = document.getElementById("UnitPrice_SignItem_TD_Text_" + rowID).value;
    document.getElementById("TotalPrice_SignItem_TD_Text_" + rowID).value = (productcount * unitprice).toFixed($("#hidSelPoint").val());

    var newFitNametd = newTR.insertCell(16); //添加列:原单编号
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillID_SignItem_TD_Text_" + rowID + "\" value=\"\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

    var newFitDesctd = newTR.insertCell(17); //添加列:原单行号
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='FromLineNo_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />"; //添加列内容

//绑定单位组
if(document.getElementById("hidMoreUnit").value == "true")
{
    if(UnitPrice == "")
    {
        UnitPrice = "0";
    }
    GetUnitGroupSelectEx(id,"StockUnit","DetailUnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,'',"NoShowUnit("+rowID+","+UnitPrice+")");
}

    var newFitDesctd = newTR.insertCell(18); //添加列:备注
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Remark_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_" + rowID + "' value=\"\" type='text' class=\"tdinput\"  style='width:90%'  />"; //添加列内容

    var newJiBen_DanJia = newTR.insertCell(19); //添加列:基本单价
    newJiBen_DanJia.className = "cell";
    newJiBen_DanJia.id = 'TD_JiBen_DanJia_' + rowID;
    newJiBen_DanJia.innerHTML = "<input name='chk' id='JiBen_DanJia_" + rowID + "' value=\"" + UnitPrice + "\" type='text'  />"; //添加列内容
    newJiBen_DanJia.style.display="none";
    
    CountNum();
}



//----弹出多选层--------add by hm 2009-11-23

function ShowProdInfo() 
{
    //然后调用添加空白行
    AddRow_kb();
    //popTechObj.ShowListCheckSpecial('SubStorageIn"+rowID+"', 'Check');
}




//多选明细方法---

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
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/SupplyChain/ProductCheck.ashx?str=' + str, //目标地址
        cache: false,
        data: '', //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $.each(msg.data, function(i, item) {//填充物品ID，物品编号，物品名称，规格，单位ID，单位名称，入库数量(默认为0)
                if (!IsExist(item.ProdNo)) {
                    //AddRow(id,ProNo,ProdName,UnitName,Specification,UnitPrice)
                    AddRow(item.ID, item.ProdNo, item.ProductName,item.ColorName, item.CodeName, item.Specification, item.StandardCost,item.UnitID,item.IsBatchNo);
                }
            });

        },

        complete: function() { } //接收数据完毕
    });
    closeProductdiv();
}


//判断是否有相同记录有返回true，没有返回false
var rerowID = "";
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
    //    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
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
    //}
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
//定义个全局变量记录比率
var Q_EXRate = 0;
function NoShowUnit(RowID,UnitPrice)
{
    $("#DetailUnitID_SignItem_TD_Text_"+RowID).attr("disabled",true);
    
    var EXRate = $("#DetailUnitID_SignItem_TD_Text_"+RowID).val().split('|')[1].toString(); /*比率*/
    Q_EXRate = EXRate;
    var num = parseFloat($("#OutCount_SignItem_TD_Text_"+RowID).val());
    var sss = parseFloat(num*EXRate).toFixed($("#hidSelPoint").val());
    
    if (EXRate != '')
    {
         //$("#UnitPrice_SignItem_TD_Text_"+RowID).val(NumberSetPoint(UnitPrice*EXRate));
         $("#InCount_SignItem_TD_Text_"+RowID).val(sss);
         var unitprice = parseFloat($("#UnitPrice_SignItem_TD_Text_"+RowID).val());
         var zprice = (unitprice * num).toFixed($("#hidSelPoint").val());
         $("#TotalPrice_SignItem_TD_Text_"+RowID).val(zprice);
         
    }
}


/*焦点2：鼠标失去焦点时，匹配数据库物品信息*/
function SetMatchProduct(rows, values,conname) 
{

ClearRows(rows);

popTechObj.InputObj = conname;

    var ProdNo = values;
    if (values != "") {
        //popTechObj.InputObj = rows;
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../../../Handler/Office/SupplyChain/BatchProductInfo.ashx?ProdNo=" + ProdNo, //目标地址
            cache: false,
            success: function(msg) {
                var rowsCount = 0;

                if (typeof (msg.dataBatch) != 'undefined') {
                    $.each(msg.dataBatch, function(i, item) 
                    {
                        rowsCount++;
                        //g-StandardSell去税售价; UnitID单位ID, UnitName单位名称, b销项税率,c含税售价, d销售折扣
                            //Specification规格型号,e物品种类名称, f物品种类, UnitPrice含税进价
                        Fun_FillParent_Content(item.ID,item.ProdNo, item.ProductName, item.StandardSell, 
                        item.UnitID,item.CodeName, item.TaxRate, item.SellTax, item.Discount,
                        item.Specification, item.CodeTypeName,item.TypeID, item.StandardCost,item.Source,
                        item.GroupUnitNo,item.SaleUnitID,item.SaleUnitName,
                        item.InUnitID,item.InUnitName,item.StockUnitID,item.StockUnitName,
                        item.MakeUnitID,item.MakeUnitName,item.IsBatchNo,item.ColorName) 
                    });
                }
                if (rowsCount == 0) {
                document.getElementById("hidShowP").value = "0";
                    popMsgObj.Show("物品编号" + rows + "|", "不存在此"+values+"物品，请重新输入或选择物品");
                    
                }

            },
            error: function() { popMsgObj.ShowMsg('匹配物品数据时发生请求异常!'); },
            complete: function() { }
        });
    }
    else 
    {
        if (document.getElementById("ProductNo_SignItem_TD_Text_"+rows).title != "") 
        { 
        document.getElementById("hidShowP").value = "1";
            popMsgObj.Show("物品编号" + rows + "|", "请重新输入或选择物品");
        }
    }
}

//回车键调用此方法
function AddSignRow()
{
    //然后调用添加空白行
    AddRow_kb();
}

function BeforeShowListSpecial(obj,val)
{
    if(document.getElementById("hidShowP").value == "0" || document.getElementById(obj).value == "")
    {
       popTechObj.ShowListSpecial(obj,'');
    }
    else
    {
        return;
    }
}

//清空明细
function ClearRows(rows)
{
    document.getElementById("UnitPrice_SignItem_TD_Text_" + rows).value = "";
    
    document.getElementById("ProductNo_SignItem_TD_Text_" + rows).title = "";
    document.getElementById("ProductName_SignItem_TD_Text_" + rows).value = "";
    document.getElementById("ColorName_SignItem_TD_Text_" + rows).value = "";
    document.getElementById("UnitID_SignItem_TD_Text_" + rows).value = "";
    document.getElementById("Specification_SignItem_TD_Text_" + rows).value = "";
    document.getElementById("JiBen_DanJia_" + rows).value = "";
    $("#unitdiv"+rows).html("");
}