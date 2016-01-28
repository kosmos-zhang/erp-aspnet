$(document).ready(function() {
    GetFlowButton_DisplayControl();
    requestQuaobj = GetRequest();
    var action = requestQuaobj['myAction'];
    fnGetExtAttr(); //物品控件拓展属性
    if (action == 'edit' || action == 'fromlist') {
        $("#btnReturn").css("display", "");

    }
    if (ReportID > 0) {
        LoadReportInfo(ReportID);
    }
    else {
        //加载拓展属性(这里放在新建的时候)
        GetExtAttr("officedba.QualityCheckReport", null);
    }
});
function LoadReportInfo(ReportID) {
    var rowsCount = 0;
    ClearSignRow();
    $.ajax({
        type: "POST", //用POST方式传输ss
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/StorageManager/StorageReportInfo.ashx?ID=" + ReportID, //目标地址
        cache: false,
        beforeSend: function() { AddPop(); }, //发送数据之前        
        success: function(msg) {
            var rowsCount = 0;
            var countTotals = 0;
            //数据获取完毕，填充页面据显示
            //生产任务单信息
            if (typeof (msg.dataReport) != 'undefined') {
                GetFlowButton_DisplayControl();
                $.each(msg.dataReport, function(i, item) {
                    if (msg.IsFlow > 0 || msg.IsTransfer > 0) {
                        try {
                            document.getElementById('btnSave').src = '../../../images/Button/UnClick_bc.jpg';
                            $("#isupdate").val('1');
                            $("#isflow").val('1');
                        }
                        catch (e) { }
                    }
                    $("#lbInfoNo").val(item.ReportNo);
                    $("#ApplyID").val(item.FromReportNo); //源 单编号

                    $("#hiddenApplyID").val(item.ReportID);  //源单ID
                    $("#divTaskNo").css("display", "");
                    $("#divCodeRuleUC").css("display", "none");
                    $("#txtSubject").val(item.Title);
                    $("#hiddenReportId").val(item.ID);
                    $("#sltFromType").val(item.FromType);
                    $("#tbCustID").val(item.OtherCorpName); //往来单位名称
                    $("#hiddentbCustID").val(item.OtherCorpID); //往来单位ID
                    $("#tbCustBigID").val(item.BigTypeName);  //往来单位大类名称
                    $("#hiddentbCustBigID").val(item.CorpBigType); //大类ID 
                    $("#ddlCheckType").val(item.CheckType);
                    $("#ddlCheckMode").val(item.CheckMode);
                    ShowSample();
                    $("#UserChecker").val(item.ApplyUserIDName);   //报检人名称
                    $("#hiddentbChecker").val(item.ApplyUserIDName); //报检人ID
                    $("#DeptChecker").val(item.ApplyDeptIDName); //报检部门名称
                    $("#hiddentbCheckDept").val(item.ApplyDeptID); //报检部门ID
                    $("#UserCheck").val(item.EmployeeName); // 检验人名称
                    $("#hiddenUserID").val(item.Checker);  //检验人ID
                    $("#DeptChecking").val(item.DeptName); //检验人部门名称
                    $("#hiddenDept").val(item.CheckDeptId);  //检验人部门ID
                    $("#CheckContent").val(FormatAfterDotNumber(item.CheckContent, selPoint));
                    $("#CheckDate").val(item.CheckDate.substring(0, 10));
                    $("#tbProNo").val(item.ProdNo);
                    $("#tbProName").val(item.ProductName);
                    $("#hiddenProID").val(item.ProductID);
                    $("#FromDetailID").val(item.FromDetailID);
                    $("#tbProductCount").val(FormatAfterDotNumber(item.CheckNum, selPoint));
                    $("#mySpecification").val(item.Specification);

                    $("#SampleNum").val(FormatAfterDotNumber(item.SampleNum, selPoint));
                    $("#PassNum").val(FormatAfterDotNumber(item.PassNum, selPoint));
                    $("#PassPercent").val(FormatAfterDotNumber(item.PassPercent, selPoint));
                    $("#NotPassNum").val(FormatAfterDotNumber(item.NoPass, selPoint));
                    $("#CheckResult").val(FormatAfterDotNumber(item.CheckResult, selPoint));
                    $("#ddlisPass").val(item.isPass);
                    $("#ddlisRecheck").val(item.isRecheck);
                    $("#CheckStandard").val(item.CheckStandard);
                    $("#tbUnit").val(item.CodeName);
                    $("#hiddentbUnit").val(item.UnitID);

                    $("#UserPrincipal").val(item.PrincipalName);
                    $("#hiddentxPrincipal").val(item.Principal);
                    $("#Depttxt").val(item.TheDeptName);
                    $("#hiddentbDept").val(item.TheDeptID);

                    // 填充扩展属性
                    GetExtAttr("officedba.QualityCheckReport", item);
                    var testBillStatus = item.BillStatusID;
                    if (testBillStatus == '1') {
                        $("#txtBillStatus").val('1');
                        $("#ddlBillStatus").val('制单');
                    }
                    if (testBillStatus == '2') {
                        $("#txtBillStatus").val('2');
                        $("#ddlBillStatus").val('执行');

                    }
                    if (testBillStatus == '3') {
                        $("#txtBillStatus").val('3');
                        $("#ddlBillStatus").val('变更');
                    }
                    if (testBillStatus == '4') {
                        $("#txtBillStatus").val('4');
                        $("#ddlBillStatus").val('手动结单');
                    }
                    if (testBillStatus == '5') {
                        $("#txtBillStatus").val('5');
                        $("#ddlBillStatus").val('自动结单');
                    }
                    if (testBillStatus > 1) {
                        $("#divConfirmor").css("display", "");
                        $("#divConfirmorDate").css("display", "");
                    }
                    if (testBillStatus > 3) {
                        $("#divCloser").css("display", "");
                        $("#divCloserDate").css("display", "");
                    }
                    $("#tbCreater").val(item.CreatorName); //制单人名字
                    $("#txtCreator").val(item.Creator);
                    $("#txtCreateDate").val(item.CreateDate.substring(0, 10));
                    $("#txtConfirmor").val(item.ConfirmorName);
                    $("#txtConfirmDate").val(item.ConfirmDate.substring(0, 10));
                    if (item.txtCloser != '' && item.txtCloser != null && item.txtCloser != '0') {
                        $("#txtCloserReal").val(item.CloserName);
                    }
                    $("#txtCloseDate").val(item.CloseDate.substring(0, 10));
                    $("#txtModifiedUserID").val(item.ModifiedUserIDName);
                    $("#txtModifiedDate").val(item.ModifiedDate.substring(0, 10));
                    $("#txtRemark").val(item.Remark);

                    $("#hfPageAttachment").val(item.Attachment.replace(/,/g, "\\"));
                    var testurl = item.Attachment.replace(/,/g, "\\");
                    var testurl2 = testurl.lastIndexOf('\\');
                    testurl2 = testurl.substring(testurl2 + 1, testurl.lenght);
                    document.getElementById('attachname').innerHTML = testurl2;

                    if ($("#hfPageAttachment").val() != "") {
                        //下载删除显示
                        document.getElementById("divDealAttachment").style.display = "";
                        //上传不显示
                        document.getElementById("divUploadAttachment").style.display = "none";
                    }


                });
            }

            if (typeof (msg.dataDetail) != 'undefined') {
                $.each(msg.dataDetail, function(i, item) {
                    if (item.DetailID != null && item.DetailID != "") {
                        rowsCount++;
                        FillSignRow(i, item.CheckItem, item.CheckStandard, item.CheckValue, item.CheckResult, item.isPass, item.CheckNum, item.PassNum, item.NotPassNum, item.Checker, item.EmployeeName, item.CheckDeptID, item.DeptName, item.StandardValue, item.NormUpLimit, item.LowerLimit, item.Remark);
                    }
                });
            }

            $("#txtTRLastIndex").val(rowsCount + 1);

        },
        error: function() { alert('加载数据时发生请求异常'); },
        complete: function() { hidePopup(); }
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

function GetFloat(a) {
    if (a != null && a != '') {
        var testFlaot = a.toString().indexOf('.');
        var myValue = a;
        if (testFlaot != -1) {
            var testvalue = a.toString().split('.');
            var myvalue = testvalue[1];
            if (myvalue == '00' || myvalue == '0' || myvalue == '000' || myvalue == '0000') {
                myValue = testvalue[0];
            }
            if (myvalue.toString().length > 2 && myvalue != '000' && myvalue != '0000') {
                myValue = parseFloat(a.toString()).toFixed(2);
            }

        }
        return myValue;
    }
}
//当前页面更新时，清除行，只留标题
function ClearSignRow() {
    $("#txtTRLastIndex").val("1");
    //移除所有的tr和td
    var signFrame = findObj("dg_Log", document);
    var rowNum = signFrame.rows.length;
    if (rowNum > 1) {
        for (i = 1; i < rowNum; i++) {
            signFrame.deleteRow(i);
            rowNum = rowNum - 1;
            i = i - 1;
        }
    }
}



function Fun_Save_CheckReport() {
    if ($("#isflow").val() > 0) {
        return false;
    }
    if ($("#isupdate").val() > 0) {
        return false;
    }
    if (CheckInput()) {
        if (CheckDetailCount()) {

            var ReportNo = "";
            var isFlag = true;
            var fieldText = "";
            var msgText = "";
            var UrlParam = '';
            var myAction = 'add';
            var bmgz = '';
            var myMethod = $("#hiddenReportId").val();
            if (myMethod == '0')  // 新建
            {
                if ($("#checkNo_ddlCodeRule").val() == "")//手工输入
                {
                    if (strlen($("checkNo_txtCode")) > 50) {
                        popMsgObj.ShowMsg('单据编号仅限于50个字符内!');
                        return false;
                    }
                    ReportNo = $("#checkNo_txtCode").val();
                    bmgz = "sd";
                }
                else {
                    ReportNo = $("#checkNo_ddlCodeRule").val();
                    bmgz = "zd";
                }
            }
            else   //编辑
            {
                ReportNo = $("#lbInfoNo").val();
                myAction = 'edit';
            }

            var Title = $("#txtSubject").val();
            var FromType = $("#sltFromType").val();   //-------------------------源单类型  根据ID 查找需要更新的相应表
            var ApplyID = $("#hiddenApplyID").val(); //原单 的ID号    -----------需要更新的源单明细ID
            var ApplyIDName = $("#ApplyID").val(); //源单编号
            var CustID = $("#hiddentbCustID").val();  //往来单位ID  默认为0
            var CustBigID = $("#hiddentbCustBigID").val(); //往来单位大类  默认为0
            var Principal = $("#hiddentxPrincipal").val(); //生产负责人ID   默认0
            var Dept = $("#hiddentbDept").val(); //生产部门ID  默认0
            var CheckType = $("#ddlCheckType").val(); //质检类别
            var CheckMode = $("#ddlCheckMode").val(); //检验方式                        这个数据不插入？？？？
            var Checker = $("#hiddentbChecker").val();  //报检人员 默认0
            var CheckerDept = $("#hiddentbCheckDept").val();  //报检部门ID 默认0
            var theChecker = $("#hiddenUserID").val();  //检验人ID  
            var myReportBillStatus = $("#txtBillStatus").val(); //单据状态
            var theCustName = $("#tbCustID").val(); //往来单位名称
            var theCustID = $("#hiddentbCustID").val(); //往来单位ID
            var theBigCustID = $("#hiddentbCustBigID").val(); //往来单位大类ID

            var theDept = $("#hiddenDept").val(); //检验部门ID
            var CheckContent = $("#CheckContent").val();
            var CheckDate = $("#CheckDate").val();
            var ProNo = $("#tbProNo").val(); //物品编号
            var ProName = $("#tbProName").val();
            var UnitId = $("#hiddentbUnit").val();
            var ProductCount = $("#tbProductCount").val(); //报检数量
            var SampleNum = $("#SampleNum").val();
            var PassNum = $("#PassNum").val();
            var PassPercent = $("#PassPercent").val();
            var NotPassNum = $("#NotPassNum").val();
            var CheckResult = $("#CheckResult").val();
            var IsPass = $("#ddlisPass").val();
            var IsReCheck = $("#ddlisRecheck").val();
            var CheckStandard = $("#CheckStandard").val();
            var Creater = $("#txtCreator").val(); //制单人ID
            var CreatDate = $("#txtCreateDate").val();
            var ModifiedUserID = $("#hiddenModifiedUserID").val(); //最后更新人
            var ModifiedDate = $("#txtModifiedDate").val(); //最后更新时间
            var Remark = $("#txtRemark").val();
            var Attachment = $("#hfPageAttachment").val();
            var FromDetailID = $("#FromDetailID").val();
            var FromLineNo = $("#hiddenLineNo").val();
            var ProductID = $("#hiddenProID").val();

            //-----------------------处理需要
            if (parseFloat(ProductCount) < parseFloat(SampleNum)) {
                popMsgObj.ShowMsg('抽样数量不能大于报检数量!');
                return false;
            }
            if (parseFloat(ProductCount) < parseFloat(PassNum)) {
                popMsgObj.ShowMsg('合格数量不能大于报检数量!');
                return false;
            }

            //-----------------------------------------------------------明细Start
            var DetailSortNo = new Array();        //序号
            var DetailCheckItem = new Array();     //检验项目
            var DetailCheckStandard = new Array(); //检验指标
            var DetailCheckValue = new Array();    //检验值
            var DetailCheckResult = new Array();   //检验结论
            var DetailIsPass = new Array();
            var DetailCheckNum = new Array();
            var DetailPassNum = new Array();
            var DetailNoPassNum = new Array();
            var DetailChecker = new Array();
            var DetailDept = new Array();
            var DetailStandardValue = new Array();
            var DetailNormUpLimit = new Array();
            var DetailLowerLimit = new Array();
            var DetailRemark = new Array();

            var signFrame = findObj("dg_Log", document);
            var count = signFrame.rows.length; //有多少行
            var detailFlag = true;
            var detailFild = '';
            var detailMsg = '';
            var testIsPass = '1';
            for (var i = 1; i < count; i++) {
                if (signFrame.rows[i].style.display != 'none') {

                    var theCheckItem = document.getElementById('TD_CheckItem_' + (i)).value;     //检验项目
                    var theCheckStandard = document.getElementById('TD_CheckStandard_' + (i)).value; //检验指标
                    var theCheckValue = document.getElementById('TD_CheckValue_' + (i)).value;    //检验值
                    var theCheckResult = document.getElementById('TD_CheckResult' + (i)).value;   //检验结论
                    var theIsPass = document.getElementById('TD_isPass_' + (i)).value;
                    if (theIsPass == '0') {
                        testIsPass = '0';
                    }
                    var theCheckNum = document.getElementById('TD_CheckNum_' + (i)).value;
                    var thePassNum = document.getElementById('TD_PassNum_' + (i)).value;
                    var theNoPassNum = document.getElementById('TD_NotPassNum_' + (i)).value;
                    var theDetailChecker = document.getElementById('hiddenChecker_' + (i)).value;
                    var thedetailDept = document.getElementById('hiddenCheckDeptID' + (i)).value;
                    var theStandardValue = document.getElementById('TD_StandardValue_' + (i)).value;
                    var theNormUpLimit = document.getElementById('TD_NormUpLimit_' + (i)).value;
                    var theLowerLimit = document.getElementById('TD_LowerLimit_' + (i)).value;
                    var theRemark = document.getElementById('TD_Remark_' + (i)).value;

                    DetailSortNo.push(i);
                    DetailCheckNum.push(theCheckNum);
                    DetailChecker.push(theDetailChecker);
                    DetailCheckItem.push(theCheckItem);
                    DetailCheckResult.push(theCheckResult);
                    DetailCheckStandard.push(theCheckStandard);
                    DetailCheckValue.push(theCheckValue);
                    DetailDept.push(thedetailDept);
                    DetailIsPass.push(theIsPass);
                    DetailLowerLimit.push(theLowerLimit);
                    DetailNoPassNum.push(theNoPassNum);
                    DetailNormUpLimit.push(theNormUpLimit);
                    DetailPassNum.push(thePassNum);
                    DetailRemark.push(theRemark);
                    DetailStandardValue.push(theStandardValue);
                }
            }

            //------------------------------明细End
            if (testIsPass == '0') {
                IsPass = '0';
            }
            else {
                IsPass = '1';
            }

            UrlParam = "Title=" + escape(Title) +
        "&ApplyIDName=" + escape(ApplyIDName) +
        "&CustID=" + CustID +
        "&FromDetailID=" + FromDetailID +
        "&CustBigID=" + CustBigID +
        "&Principal=" + Principal +
        "&Dept=" + Dept +
        "&ApplyID=" + ApplyID +
        "&FromType=" + FromType +
        "&CheckType=" + CheckType +
        "&ProductCount=" + ProductCount +
        "&PassNum=" + PassNum +
        "&PassPercent=" + PassPercent +
        "&NotPassNum=" + NotPassNum +
        "&CheckMode=" + CheckMode +
        "&Checker=" + Checker +
        "&CheckerDept=" + CheckerDept +
        "&theChecker=" + escape(theChecker) +
        "&theDept=" + theDept +
        "&CheckContent=" + escape(CheckContent) +
        "&CheckDate=" + CheckDate +
        "&ProNo=" + escape(ProNo) +
        "&ProName=" + escape(ProName) +
        "&UnitId=" + UnitId +
        "&theCustName=" + escape(theCustName) +
        "&theCustID=" + theCustID +
        "&theBigCustID=" + theBigCustID +
        "&ModifiedDate=" + ModifiedDate +
        "&SampleNum=" + SampleNum +
        "&CheckStandard=" + CheckStandard +
        "&ModifiedUserID=" + ModifiedUserID +
        "&CheckResult=" + CheckResult +
        "&IsPass=" + IsPass +
        "&myReportBillStatus=" + myReportBillStatus +
        "&ProductID=" + ProductID +
        "&IsReCheck=" + IsReCheck +
        "&Creater=" + Creater +
        "&CreatDate=" + CreatDate +
        "&ModifiedUserID=" + ModifiedUserID +
        "&ModifiedDate=" + ModifiedDate +
        "&Remark=" + escape(Remark) +
        "&Attachment=" + escape(Attachment) +
        "&FromLineNo=" + FromLineNo +
        "&DetailSortNo=" + DetailSortNo +
        "&DetailCheckItem=" + DetailCheckItem +
        "&DetailCheckStandard=" + escape(DetailCheckStandard.toString()) +
        "&DetailCheckValue=" + DetailCheckValue.toString() +
        "&DetailCheckResult=" + escape(DetailCheckResult.toString()) +
        "&DetailIsPass=" + DetailIsPass.toString() +
        "&DetailCheckNum=" + DetailCheckNum.toString() +
        "&DetailPassNum=" + DetailPassNum.toString() +
        "&DetailNoPassNum=" + DetailNoPassNum.toString() +
        "&DetailChecker=" + DetailChecker.toString() +
        "&DetailDept=" + DetailDept.toString() +
        "&DetailStandardValue=" + escape(DetailStandardValue.toString()) +
        "&DetailNormUpLimit=" + escape(DetailNormUpLimit.toString()) +
        "&DetailLowerLimit=" + escape(DetailLowerLimit.toString()) +
        "&DetailRemark=" + escape(DetailRemark.toString()) +
        "&myAction=" + myAction +
        "&bmgz=" + bmgz +
        "&ReportNo=" + ReportNo +
        "&ID=" + myMethod +
        GetExtAttrValue();

            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/StorageManager/StorageCheckReportAdd.ashx",
                dataType: 'json', //返回json格式数据
                data: UrlParam,
                cache: false,
                beforeSend: function() {
                },
                error: function() {
                    popMsgObj.ShowMsg('保存质检报告时请求发生错误');
                },
                success: function(data) {
                    var reInfo = data.data.split('|');
                    if (reInfo.length > 1) {
                        popMsgObj.ShowMsg(data.info);
                        if (reInfo[0] == "CheckValue") {
                            return false;
                        }
                        $("#lbInfoNo").val(reInfo[1]);

                        $("#hiddenReportId").val(reInfo[0]);
                        $("#txtModifiedUserID").val(reInfo[2]);
                        $("#txtModifiedDate").val(reInfo[3]);
                        $("#divCodeRuleUC").css("display", "none");
                        $("#divTaskNo").css("display", "");
                        if (myMethod > 0 && myReportBillStatus == '2') {
                            $("#txtBillStatus").val('3');
                            $("#ddlBillStatus").val('变更');
                        }
                    }
                    else {
                        popMsgObj.ShowMsg(data.info);
                    }
                    GetFlowButton_DisplayControl();
                }
            });
        }
    }
}


function FillSignRow(i, CheckItem, CheckStandard, CheckValue, CheckResult, isPass, CheckNum, PassNum, NotPassNum, Checker, EmployeeName, CheckDeptID, DeptName, StandardValue, NormUpLimit, LowerLimit, Remark) {

    var rowID = parseInt(i) + 1;
    var signFrame = findObj("dg_Log", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = "Row_" + rowID;

    var colChoose = newTR.insertCell(0); //添加列:选择
    colChoose.className = "cell";
    colChoose.align = "center";
    colChoose.innerHTML = "<input name='Chk'  style='width:90%'  id='Chk_Option_" + rowID + "'  value=\"0\" type='checkbox' />";

    var colNum = newTR.insertCell(1); //添加列:序号
    colNum.className = "cell";
    colNum.innerHTML = "<input type=\"text\"  style='width:90%'  id=\"TD_Num_" + rowID + "\" value=\"" + rowID + "\"  class=\"tdinput\"  readonly />";


    var colProductNo = newTR.insertCell(2); //添加列:检验项目
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<select id='TD_CheckItem_" + rowID + "' name='TD_CheckItem_" + rowID + "' ></select>";
    var theID = 'TD_CheckItem_' + rowID;
    GetPublicCode(theID, CheckItem);

    var colProductName = newTR.insertCell(3); //添加列:检验指标
    colProductName.className = "cell";
    colProductName.innerHTML = "<input type=\"text\"  style='width:90%'  id=\"TD_CheckStandard_" + rowID + "\" value=\"" + CheckStandard + "\" class=\"tdinput\"    />";

    var colUnitID = newTR.insertCell(4); //添加列:检验值 
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"text\" onchange=\"Number_round(this," + selPoint + ");\"  style='width:90%'  id='TD_CheckValue_" + rowID + "' value=\"" + FormatAfterDotNumber(CheckValue, selPoint) + "\" class=\"tdinput\"   />";

    var colSpecification = newTR.insertCell(5); //添加列:检验结论
    colSpecification.className = "cell";
    colSpecification.innerHTML = "<input type=\"text\"  style='width:90%'  id='TD_CheckResult" + rowID + "' value=\"" + CheckResult + "\" class=\"tdinput\"   />";

    var colProductCount = newTR.insertCell(6); //添加列:是否合格
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<select id='TD_isPass_" + rowID + "'><option value='1'>合格</option><option value='0'>不合格</option></option></select>";
    document.getElementById('TD_isPass_' + rowID).value = isPass;
    var testmyCheckMode = $("#ddlCheckMode").val();

    var colBom = newTR.insertCell(7); //添加列:检验数量
    colBom.className = "cell";
    colBom.innerHTML = "<input type=\"text\" maxlength=\"10\" onchange=\"Number_round(this," + selPoint + ");\" style='width:90%'  id='TD_CheckNum_" + rowID + "' value=\"" + FormatAfterDotNumber(CheckNum, selPoint) + "\" class=\"tdinput\"  />";


    var colRouteID = newTR.insertCell(8); //添加列:合格数量
    colRouteID.className = "cell";
    colRouteID.innerHTML = "<input type=\"text\" maxlength=\"10\" onchange=\"Number_round(this," + selPoint + ");\" onblur=\"CheckDetailPassNum(" + rowID + ");\" style='width:90%'   class=\"tdinput\" id='TD_PassNum_" + rowID + "' value=\"" + FormatAfterDotNumber(PassNum, selPoint) + "\" />";


    var colStart = newTR.insertCell(9); //添加列:不合格数量
    colStart.className = "cell";
    colStart.innerHTML = "<input type=\"text\" maxlength=\"10\" onchange=\"Number_round(this," + selPoint + ");\" onblur=\"CheckDetailPassNum(" + rowID + ");\" readonly style='width:90%'  id='TD_NotPassNum_" + rowID + "' value=\"" + FormatAfterDotNumber(NotPassNum, selPoint) + "\"  class=\"tdinput\" />";

    var colFromType = newTR.insertCell(10); //添加列:检验人员
    colFromType.className = "cell";
    colFromType.innerHTML = "<input id=\"UserChecker_" + rowID + "\" value=\"" + EmployeeName + "\"  readOnly style='width:90%' onclick=\"alertdiv('UserChecker_" + rowID + ",hiddenChecker_" + rowID + "');\"  class=\"tdinput\"  name=\"UserChecker_" + rowID + "\"  /><input value=\"" + Checker + "\" id=\"hiddenChecker_" + rowID + "\" type=\"hidden\">";

    var colFromBillID = newTR.insertCell(11); //添加列:检验部门
    colFromBillID.className = "cell";
    colFromBillID.innerHTML = "<input type=\"text\" value=\"" + DeptName + "\" readOnly style='width:90%' onclick=\"alertdiv('DeptTD_CheckDeptID_" + rowID + ",hiddenCheckDeptID" + rowID + "');\"  id='DeptTD_CheckDeptID_" + rowID + "' value=\"\"  class=\"tdinput\" /><input type=\"hidden\" id='hiddenCheckDeptID" + rowID + "' value=\"" + CheckDeptID + "\" />";

    var colFromLineNo = newTR.insertCell(12); //添加列:标准值
    colFromLineNo.className = "cell";
    colFromLineNo.innerHTML = "<input type=\"text\"  style='width:90%'  id='TD_StandardValue_" + rowID + "' value=\"" + StandardValue + "\"  class=\"tdinput\" />";

    var colRemark = newTR.insertCell(13); //添加列:指标上限
    colRemark.className = "cell";
    colRemark.innerHTML = "<input type=\"text\"   style='width:90%'  id='TD_NormUpLimit_" + rowID + "' value=\"" + NormUpLimit + "\"  class=\"tdinput\" />";

    var colRemark = newTR.insertCell(14); //添加列:指标下限
    colRemark.className = "cell";
    colRemark.innerHTML = "<input type=\"text\"  style='width:90%'  id='TD_LowerLimit_" + rowID + "' value=\"" + LowerLimit + "\"  class=\"tdinput\" />";

    var colRemark = newTR.insertCell(15); //添加列:备注
    colRemark.className = "cell";
    colRemark.innerHTML = "<input type=\"text\"  style='width:80%'  id='TD_Remark_" + rowID + "' value=\"" + Remark + "\"  class=\"tdinput\"  />";

    $("#txtTRLastIndex").val((rowID + 1).toString()); //将行号推进下一行

}
function AddSignRow() {
    var FromType = $("#sltFromType").val();
    var txtTRLastIndex = findObj("txtTRLastIndex", document);
    var rowID = parseInt(txtTRLastIndex.value);
    var signFrame = findObj("dg_Log", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = "Row_" + rowID;

    var colChoose = newTR.insertCell(0); //添加列:选择
    colChoose.className = "cell";
    colChoose.align = "center";
    colChoose.innerHTML = "<input name='Chk'  style='width:90%'  id='Chk_Option_" + rowID + "'  value=\"0\" type='checkbox' />";

    var colNum = newTR.insertCell(1); //添加列:序号
    colNum.className = "cell";
    colNum.innerHTML = "<input type=\"text\"  style='width:90%'  id=\"TD_Num_" + rowID + "\" value=\"" + rowID + "\"  class=\"tdinput\"  readonly />";


    var colProductNo = newTR.insertCell(2); //添加列:检验项目
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<select id='TD_CheckItem_" + rowID + "' name='TD_CheckItem_" + rowID + "' ></select>";
    var theID = 'TD_CheckItem_' + rowID;
    GetPublicCode(theID, 0);

    var colProductName = newTR.insertCell(3); //添加列:检验指标
    colProductName.className = "cell";
    colProductName.innerHTML = "<input type=\"text\"  style='width:90%'  id=\"TD_CheckStandard_" + rowID + "\" value=\"\" class=\"tdinput\"    />";

    var colUnitID = newTR.insertCell(4); //添加列:检验值 
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"text\" onchange=\"Number_round(this," + selPoint + ");\" style='width:90%'  id='TD_CheckValue_" + rowID + "' value=\"\" class=\"tdinput\"   />";

    var colSpecification = newTR.insertCell(5); //添加列:检验结论
    colSpecification.className = "cell";
    colSpecification.innerHTML = "<input type=\"text\"  style='width:90%'  id='TD_CheckResult" + rowID + "' value=\"\" class=\"tdinput\"   />";

    var colProductCount = newTR.insertCell(6); //添加列:是否合格
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<select id='TD_isPass_" + rowID + "'><option value='1'>合格</option><option value='0'>不合格</option></option></select>";

    var testmyCheckMode = $("#ddlCheckMode").val();

    var colBom = newTR.insertCell(7); //添加列:检验数量
    colBom.className = "cell";
    colBom.innerHTML = "<input type=\"text\" maxlength=\"10\" onchange=\"Number_round(this," + selPoint + ");\" style='width:90%'  id='TD_CheckNum_" + rowID + "' value=\"\" class=\"tdinput\"  />";


    var colRouteID = newTR.insertCell(8); //添加列:合格数量
    colRouteID.className = "cell";
    colRouteID.innerHTML = "<input type=\"text\" maxlength=\"10\" onchange=\"Number_round(this," + selPoint + ");\" onblur=\"CheckDetailPassNum(" + rowID + ");\" style='width:90%'   class=\"tdinput\" id='TD_PassNum_" + rowID + "' />";


    var colStart = newTR.insertCell(9); //添加列:不合格数量
    colStart.className = "cell";
    colStart.innerHTML = "<input type=\"text\" onchange=\"Number_round(this," + selPoint + ");\" onblur=\"CheckDetailPassNum(" + rowID + ");\"  style='width:90%' readonly id='TD_NotPassNum_" + rowID + "'  class=\"tdinput\" />";


    var colFromType = newTR.insertCell(10); //添加列:检验人员
    colFromType.className = "cell";
    colFromType.innerHTML = "<input id=\"UserChecker_" + rowID + "\"  readOnly style='width:90%' onclick=\"alertdiv('UserChecker_" + rowID + ",hiddenChecker_" + rowID + "');\"  class=\"tdinput\"  name=\"UserChecker_" + rowID + "\"  /><input id=\"hiddenChecker_" + rowID + "\" type=\"hidden\">";


    var colFromBillID = newTR.insertCell(11); //添加列:检验部门
    colFromBillID.className = "cell";
    colFromBillID.innerHTML = "<input type=\"text\"  style='width:90%' readOnly onclick=\"alertdiv('DeptTD_CheckDeptID_" + rowID + ",hiddenCheckDeptID" + rowID + "');\"  id='DeptTD_CheckDeptID_" + rowID + "' value=\"\"  class=\"tdinput\" /><input type=\"hidden\" id='hiddenCheckDeptID" + rowID + "' value=\"\" />";

    var colFromLineNo = newTR.insertCell(12); //添加列:标准值
    colFromLineNo.className = "cell";
    colFromLineNo.innerHTML = "<input type=\"text\"   style='width:90%'  id='TD_StandardValue_" + rowID + "' value=\"\"  class=\"tdinput\" />";

    var colRemark = newTR.insertCell(13); //添加列:指标上限
    colRemark.className = "cell";
    colRemark.innerHTML = "<input type=\"text\"   style='width:90%'  id='TD_NormUpLimit_" + rowID + "' value=\"\"  class=\"tdinput\" />";

    var colRemark = newTR.insertCell(14); //添加列:指标下限
    colRemark.className = "cell";
    colRemark.innerHTML = "<input type=\"text\"  style='width:90%'  id='TD_LowerLimit_" + rowID + "' value=\"\"  class=\"tdinput\" />";

    var colRemark = newTR.insertCell(15); //添加列:备注
    colRemark.className = "cell";
    colRemark.innerHTML = "<input type=\"text\" style='width:80%'  id='TD_Remark_" + rowID + "' value=\"\"  class=\"tdinput\"  />";

    txtTRLastIndex.value = (rowID + 1).toString(); //将行号推进下一行

}
//获取公共分类
function GetPublicCode(a, b) {
    var Order = "";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/StorageManager/GetCodePublicType.ashx", //目标地址
        data: "&orderby=" + Order, //数据
        cache: false,
        beforeSend: function() { }, //发送数据之前
        success: function(msg) {
            var index = 1;
            //数据获取完毕，填充页面据显示
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "")
                    var CheckItemSelect = document.getElementById(a);
                CheckItemSelect.options.add(new Option(item.TypeName, item.ID));
                if (b != '0') {
                    if (item.ID == b) {
                        CheckItemSelect.value = b;
                    }
                }

            });
        },
        error: function() {

        },
        complete: function() { } //接收数据完毕
    });
}
//明细信息：删除行
function DeleteSignRow() {
    var signFrame = findObj("dg_Log", document);
    var ck = document.getElementsByName("Chk");
    var txtTRLastIndex = $("#txtTRLastIndex").val();
    var signFrame = findObj("dg_Log", document);
    for (var i = 0; i < txtTRLastIndex - 1; i++) {
        if (signFrame.rows[i + 1].style.display != "none") {
            var objRadio = 'Chk_Option_' + (i + 1);
            if (document.getElementById(objRadio.toString()).checked) {
                signFrame.rows[i + 1].style.display = 'none';
            }
        }
    }
}

function SelectSubAll() {
    var signFrame = findObj("dg_Log", document);
    var txtTRLastIndex = $("#txtTRLastIndex").val();
    var signFrame = findObj("dg_Log", document);
    if (document.getElementById("CheckAll").checked) {
        for (var i = 0; i < txtTRLastIndex - 1; i++) {
            if (signFrame.rows[i + 1].style.display != "none") {
                var objRadio = 'Chk_Option_' + (i + 1);
                document.getElementById(objRadio.toString()).checked = true;
            }
        }
    }
    else {
        for (var i = 0; i < txtTRLastIndex - 1; i++) {
            if (signFrame.rows[i + 1].style.display != "none") {
                var objRadio = 'Chk_Option_' + (i + 1);
                document.getElementById(objRadio.toString()).checked = false;
            }
        }
    }
}

function CheckDetailCount() {
    var testCheckMode = $("#ddlCheckMode").val();
    var ProductCount = $("#tbProductCount").val();   //报检数量
    var testSampleNum = $("#SampleNum").val();       //抽样数量
    var falg = true;
    var signFrame = findObj("dg_Log", document);
    var ck = document.getElementsByName("Chk");
    var txtTRLastIndex = $("#txtTRLastIndex").val();
    var signFrame = findObj("dg_Log", document);
    var total = 0;
    for (var i = 0; i < txtTRLastIndex - 1; i++) {
        if (signFrame.rows[i + 1].style.display != "none") {
            total++;
            var thetestMode = 'TD_CheckNum_' + (i + 1);
            var theCheckMode = document.getElementById(thetestMode.toString());
            if (strlen(document.getElementById('TD_CheckStandard_' + (i + 1)).value) > 200) {
                popMsgObj.ShowMsg('检验指标仅限于200个字符内！');
                falg = false;
                return false;
            }
            if (strlen(document.getElementById('TD_Remark_' + (i + 1)).value) > 1024) {
                popMsgObj.ShowMsg('备注仅限于1024个字符内！');
                falg = false;
                return false;
            }
            if (parseFloat(theCheckMode.value) > parseFloat(ProductCount)) {
                popMsgObj.ShowMsg("第" + (i + 1) + "行明细检验数量不能大于物品的报检数量！");
                falg = false;
                return false;
            }
            if (parseFloat($("#TD_PassNum_" + (i + 1)).val()) > parseFloat(theCheckMode.value)) {
                popMsgObj.ShowMsg("第" + (i + 1) + "行明细合格数量不能大于检验数量！");
                falg = false;
                return false;
            }
        }
    }
    if (total == 0) {
        popMsgObj.ShowMsg('请输入质检报告明细！');
        falg = false;
        return false;
    }

    return falg;
}
function CheckDetailPassNum(own) {
    var TestPass = document.getElementById('TD_PassNum_' + own);
    var TestNotPass = document.getElementById('TD_NotPassNum_' + own);
    var TestCheckCount = document.getElementById('TD_CheckNum_' + own);
    if (parseFloat(TestCheckCount.value) >= 0 && parseFloat(TestPass.value) >= 0) {
        TestNotPass.value = FormatAfterDotNumber(parseFloat(TestCheckCount.value) - parseFloat(TestPass.value), selPoint);
    }
    if (TestPass.value != '') {
        if (parseFloat(TestPass.value) > parseFloat(TestCheckCount)) {
            popMsgObj.ShowMsg('合格数量不能大于物品的检验数量！');
            return false;
        }
    }
    if (TestNotPass.value != '') {
        if (parseFloat(TestNotPass.value) > parseFloat(TestCheckCount)) {
            popMsgObj.ShowMsg('不合格数量不能大于物品的检验数量！');
            return false;
        }
    }
    if (TestNotPass.value != '' && TestPass.value != '') {
        if (parseFloat(TestPass.value) + parseFloat(TestNotPass) > parseFloat(TestCheckCount)) {
            popMsgObj.ShowMsg('合格数量加不合格数量不能大于物品的检验数量！');
            return false;
        }

    }
}
function CheckDetailBadNum(own) {

}

function changeCheckMode() {
    var myCheckMode = document.getElementById('ddlCheckMode');
    if (myCheckMode.value == '1')  //检验方式为 全检
    {
        $("#SampleNum").val(FormatAfterDotNumber(0, selPoint));
        document.getElementById('SampleNum').readOnly = true;
        var signFrame = findObj("dg_Log", document);
        var ck = document.getElementsByName("Chk");
        var txtTRLastIndex = $("#txtTRLastIndex").val();
        var signFrame = findObj("dg_Log", document);
        for (var i = 0; i < txtTRLastIndex - 1; i++) {
            if (signFrame.rows[i + 1].style.display != "none") {

                var mycheckNum = 'TD_CheckNum_' + (i + 1);
                document.getElementById(mycheckNum).value = FormatAfterDotNumber(0, selPoint);
            }
        }
    }
    if (myCheckMode.value == '2')   //抽检
    {
        if (parseFloat($("#tbProductCount").val()) > 0) {
            $("#SampleNum").val($("#tbProductCount").val());
        }
        $("#SampleNum").val(FormatAfterDotNumber(0, selPoint));

        document.getElementById('SampleNum').readOnly = false;

        var signFrame = findObj("dg_Log", document);
        var ck = document.getElementsByName("Chk");
        var txtTRLastIndex = $("#txtTRLastIndex").val();
        var signFrame = findObj("dg_Log", document);
        for (var i = 0; i < txtTRLastIndex - 1; i++) {
            if (signFrame.rows[i + 1].style.display != "none") {
                var mycheckNum = 'TD_CheckNum_' + (i + 1);
                document.getElementById(mycheckNum).value = FormatAfterDotNumber(0, selPoint);
            }
        }
    }
    ShowSample();
    GetPassPrcent();
}

// 判断是否显示必填
function ShowSample() {
    if ($("#ddlCheckMode").val() == "1") {
        $("#sSample").hide();
    }
    else {
        $("#sSample").show();
    }

}

//质检确认 
function Fun_ConfirmOperate() {
    if (!window.confirm('确认要进行确认操作吗?')) {
        return false;
    }
    var hiddeniqreportid = $("#hiddenReportId").val();
    var FromType = $("#sltFromType").val();
    var ApplyID = $("#hiddenApplyID").val();
    var ProductCount = $("#tbProductCount").val();
    var PassNum = $("#PassNum").val();
    var PassPercent = $("#PassPercent").val();
    var NotPassNum = $("#NotPassNum").val();
    var CheckType = $("#ddlCheckType").val();
    var FromReportNo = $("#ApplyID").val();
    var FromDetailID = $("#FromDetailID").val();
    var isRecheck = $("#ddlisRecheck").val();
    var UrlParam = "myAction=Confirm&isRecheck=" + isRecheck
                    + "&ID=" + hiddeniqreportid.toString()
                    + "&FromDetailID=" + FromDetailID
                    + "&ApplyID=" + ApplyID
                    + "&FromType=" + FromType
                    + "&CheckType=" + CheckType
                    + "&ProductCount=" + ProductCount
                    + "&PassNum=" + PassNum
                    + "&PassPercent=" + PassPercent
                    + "&NotPassNum=" + NotPassNum
                    + "&FromReportNo=" + FromReportNo;

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/StorageManager/StorageCheckReportAdd.ashx",
        dataType: 'json', //返回json格式数据
        data: UrlParam,
        cache: false,
        beforeSend: function() {
            //AddPop();
        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误');
        },
        success: function(data) {
            if (data.sta == 1) {
                var reInfo = data.data.split('|');
                popMsgObj.ShowMsg(data.info);
                if (reInfo[0] == 'CheckValue') {
                    return false;
                }

                document.getElementById('btnSave').src = '../../../images/Button/UnClick_bc.jpg';
                $("#isflow").val('1');
                $("#isupdate").val('1');



                $("#txtBillStatus").val('2');
                $("#ddlBillStatus").val('执行');
                $("#divConfirmor").css("display", "");
                $("#divConfirmorDate").css("display", "");
                $("#txtModifiedUserID").val(reInfo[0]);
                $("#txtModifiedDate").val(reInfo[1]);
                GetFlowButton_DisplayControl();
            }
            else {
                popMsgObj.ShowMsg(data.info);
            }
        }
    });
}
function Fun_UnConfirmOperate() {
    if (!window.confirm('确认要进行取消确认操作吗?')) {
        return false;
    }
    var hiddeniqreportid = $("#hiddenReportId").val();
    var FromType = $("#sltFromType").val();
    var ApplyID = $("#hiddenApplyID").val();
    var ProductCount = $("#tbProductCount").val();
    var PassNum = $("#PassNum").val();
    var PassPercent = $("#PassPercent").val();
    var NotPassNum = $("#NotPassNum").val();
    var CheckType = $("#ddlCheckType").val();
    var FromReportNo = $("#ApplyID").val();
    var FromDetailID = $("#FromDetailID").val();
    var isRecheck = $("#ddlisRecheck").val();
    var UrlParam = "myAction=UnConfirm&ID=" + hiddeniqreportid.toString() + "&isRecheck=" + isRecheck + "&FromDetailID=" + FromDetailID + "&ApplyID=" + ApplyID + "&FromType=" + FromType + "&CheckType=" + CheckType + "&ProductCount=" + ProductCount + "&PassNum=" + PassNum + "&PassPercent=" + PassPercent + "&NotPassNum=" + NotPassNum + "&FromReportNo=" + FromReportNo;

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/StorageManager/StorageCheckReportAdd.ashx",
        dataType: 'json', //返回json格式数据
        data: UrlParam,
        cache: false,
        beforeSend: function() {
            //AddPop();
        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误');
        },
        success: function(data) {
            if (data.sta == 1) {
                var reInfo = data.data.split('|');
                popMsgObj.ShowMsg(data.info);
                if (reInfo[0] != "no") {
                    $("#txtBillStatus").val('1');
                    $("#ddlBillStatus").val('制单');
                    $("#isupdate").val('0');
                    $("#isflow").val('0');
                    document.getElementById('btnSave').src = '../../../images/Button/Bottom_btn_save.jpg';
                    $("#divConfirmor").css("display", "none");
                    $("#divConfirmorDate").css("display", "none");
                    $("#txtModifiedUserID").val(reInfo[0]);
                    $("#txtModifiedDate").val(reInfo[1]);
                    GetFlowButton_DisplayControl();
                }
            }
            else {
                popMsgObj.ShowMsg(data.info);
            }
        }
    });
}
function Fun_CompleteOperate(isComplete)   //结单
{

    var myMethod = '0'; //0结单 1取消结单
    if (!isComplete) {
        myMethod = '1';
        if (!window.confirm('确认要进行取消结单操作吗?')) {
            return false;
        }
    }
    else {
        if (!window.confirm('确认要进行结单操作吗?')) {
            return false;
        }
    }
    var hiddeniqreportid = $("#hiddenReportId").val()
    var UrlParam = "myAction=Close&myMethod=" + myMethod + "&ID=" + hiddeniqreportid.toString() + "";

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/StorageManager/StorageCheckReportAdd.ashx",
        dataType: 'json', //返回json格式数据
        data: UrlParam,
        cache: false,
        beforeSend: function() {
            //AddPop();
        },

        error: function() {

            popMsgObj.ShowMsg('请求发生错误');

        },
        success: function(data) {
            if (data.sta == 1) {
                popMsgObj.ShowMsg(data.info);
                var reInfo = data.data.split('|');
                document.getElementById('btnSave').src = '../../../images/Button/UnClick_bc.jpg';
                $("#isflow").val('1');
                $("#isupdate").val('1');
                if (isComplete) {
                    $("#txtBillStatus").val('4');
                    $("#ddlBillStatus").val('手工结单');
                    $("#divCloser").css("display", "");
                    $("#divCloserDate").css("display", "");
                    $("#txtModifiedUserID").val(reInfo[0]);
                    $("#txtModifiedDate").val(reInfo[1]);

                }
                else {
                    $("#txtBillStatus").val('2');
                    $("#ddlBillStatus").val('执行');
                    $("#divCloser").css("display", "none");
                    $("#divCloserDate").css("display", "none");
                    $("#txtModifiedUserID").val(reInfo[0]);
                    $("#txtModifiedDate").val(reInfo[1]);
                }
                GetFlowButton_DisplayControl();

            }
            else {
                popMsgObj.ShowMsg(data.info);
            }
        }
    });
}


function CheckInput() {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    var txtProcessNo = '';

    var thetxtSubject = $("#txtSubject").val();
    var theCustInfo = $("#tbCustID").val();    //往来单位
    var theChecker = $("#UserChecker").val(); //报检人员
    var theCheckerDept = $("#DeptChecker").val(); //报检人员部门
    var theCheckUser = $("#UserCheck").val(); //检验人员
    var theDeptChecking = $("#DeptChecking").val(); //检验部门
    var theProNo = $("#tbProNo").val(); //物品编号
    var theProductCount = $("#tbProductCount").val();  //报检数量
    var theSampleNum = $("#SampleNum").val();          //抽样数量
    var thePassNum = $("#PassNum").val();  //合格数量 
    var theNotPassNum = $("#NotPassNum").val();  //不合格数量
    var CheckResult = $("#CheckResult").val(); //检验结论
    var CheckStandard = $("#CheckStandard").val(); //检验标准
    var txtRemark = $("#txtRemark").val(); //备注
    //先检验页面上的特殊字符
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    //获取编码规则下拉列表选中项
    codeRule = $("#checkNo_ddlCodeRule").val();
    if ($("#hiddenReportId").val() == '0') {
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "") {
            //获取输入的编号
            txtProcessNo = $("#checkNo_txtCode").val();
            //编号必须输入
            if (txtProcessNo == "") {
                isFlag = false;
                fieldText += "单据编号|";
                msgText += "请输入单据编号|";
            }
            else {
                if (isnumberorLetters(txtProcessNo)) {
                    isFlag = false;
                    fieldText += "单据编号|";
                    msgText += "单据编号只能包含字母或数字！|";
                }
            }
        }
    }
    if (strlen(thetxtSubject) > 0) {
        if (!CheckSpecialWord(thetxtSubject)) {
            isFlag = false;
            fieldText = fieldText + "主题|";
            msgText = msgText + "单据主题不能含有特殊字符 |";
        }
    }
    if (strlen(thetxtSubject) > 100) {
        isFlag = false;
        fieldText = fieldText + "主题|";
        msgText = msgText + "仅限于100个字符以内|";
    }
    if (strlen(theChecker) <= 0) {
        isFlag = false;
        fieldText = fieldText + "报检人员|";
        msgText = msgText + "请选择报检人员 |";
    }
    if (strlen(theCheckerDept) <= 0) {
        isFlag = false;
        fieldText = fieldText + "报检部门|";
        msgText = msgText + "报检部门不能为空 |";
    }

    if (strlen(theCheckUser) <= 0) {
        isFlag = false;
        fieldText = fieldText + "检验人员|";
        msgText = msgText + "请选择检验人员 |";
    }
    if (strlen(theDeptChecking) <= 0) {
        isFlag = false;
        fieldText = fieldText + "检验部门|";
        msgText = msgText + "请选择检验部门 |";
    }
    if (strlen(theProNo) <= 0) {
        isFlag = false;
        fieldText = fieldText + "物品|";
        msgText = msgText + "请选择一个源单后再选择物品 |";
    }

    if (strlen(theProductCount) <= 0) {
        isFlag = false;
        fieldText = fieldText + "报检数量|";
        msgText = msgText + "请输入报检数量 |";
    }
    if (strlen(theProductCount) > 0) {
        if (!IsNumeric(theProductCount, 10, selPoint)) {
            isFlag = false;
            fieldText = fieldText + "报检数量|";
            msgText = msgText + "报检数量格式不正确 |";
        }
    }

    if ($("#sSample").css("display") != "none") {
        if (strlen(theSampleNum) <= 0) {
            isFlag = false;
            fieldText = fieldText + "抽样数量|";
            msgText = msgText + "请输入抽样数量 |";
        }
    }

    if (strlen(theSampleNum) > 0) {
        if (!IsNumeric(theSampleNum, 10, selPoint)) {
            isFlag = false;
            fieldText = fieldText + "抽样数量|";
            msgText = msgText + "抽样数量格式不正确 |";
        }
    }
    if (strlen(CheckResult) > 1024)//
    {
        isFlag = false;
        fieldText = fieldText + "检验结论|";
        msgText = msgText + "仅限于1024个字符以内|";
    }
    if (strlen(CheckStandard) > 1024)//
    {
        isFlag = false;
        fieldText = fieldText + "检验标准|";
        msgText = msgText + "仅限于1024个字符以内|";
    }
    if (strlen(txtRemark) > 800)//
    {
        isFlag = false;
        fieldText = fieldText + "备注|";
        msgText = msgText + "仅限于800个字符以内|";
    }
    if (strlen(thePassNum) <= 0) {
        isFlag = false;
        fieldText = fieldText + "合格数量|";
        msgText = msgText + "请输入合格数量 |";
    }
    if (strlen(thePassNum) > 0) {
        if (!IsNumeric(thePassNum, 10, selPoint)) {
            isFlag = false;
            fieldText = fieldText + "合格数量|";
            msgText = msgText + "合格数量格式不正确|";
        }
    }

    //Start 验证明细列表输入
    var txtTRLastIndex = $("#txtTRLastIndex").val();
    var signFrame = findObj("dg_Log", document);
    for (var i = 0; i < txtTRLastIndex - 1; i++) {
        if (signFrame.rows[i + 1].style.display != "none") {
            var objCheckValue = 'TD_CheckValue_' + (i + 1);         //检验值numeric(14,4)
            var objIsPass = 'TD_isPass_' + (i + 1);          //是否合格  00默认
            var objCheckNum = 'TD_CheckNum_' + (i + 1);                //检验数量numeric(14,4)
            var objPassNum = 'TD_PassNum_' + (i + 1);  //合格数量
            var objUserChecker = 'UserChecker_' + (i + 1);                    //检验人员
            var objDept = 'DeptTD_CheckDeptID_' + (i + 1);       //检验部门
            var objStardtandValue = 'TD_StandardValue_' + (i + 1);   //标准值numeric(14,4)
            var objCheckResult = 'TD_CheckResult' + (i + 1);
            var myobjCheckValue = document.getElementById(objCheckValue.toString()).value;
            var myobjIsPass = document.getElementById(objIsPass.toString()).value;
            var myobjCheckNum = document.getElementById(objCheckNum.toString()).value;
            var myobjUserChecker = document.getElementById(objUserChecker.toString()).value;
            var myobjPassNum = document.getElementById(objPassNum.toString()).value;
            var myobjDept = document.getElementById(objDept.toString()).value;
            var myobjStardtandValue = document.getElementById(objStardtandValue.toString()).value;
            var myobjCheckResult = document.getElementById(objCheckResult.toString()).value;

            if (strlen(myobjCheckValue) > 0) {
                if (!CheckSpecialWord(myobjCheckValue)) {
                    isFlag = false;
                    fieldText = fieldText + "明细检验值" + (i + 1) + "|";
                    msgText = msgText + "明细检验值不能含有特殊字符|";
                }
            }
            if (strlen(myobjCheckNum) <= 0) {
                isFlag = false;
                fieldText = fieldText + "明细检验数量" + (i + 1) + "|";
                msgText = msgText + "请输入检验数量|";
            }
            if (strlen(myobjCheckNum) > 0) {
                if (!IsNumeric(myobjCheckNum, 10, selPoint)) {
                    isFlag = false;
                    fieldText = fieldText + "明细检验数量" + (i + 1) + "|";
                    msgText = msgText + "检验数量格式不正确|";
                }
            }
            if (strlen(myobjPassNum) <= 0) {
                isFlag = false;
                fieldText = fieldText + "明细合格数量" + (i + 1) + "|";
                msgText = msgText + "请输入合格数量|";
            }
            if (strlen(myobjPassNum) > 0) {
                if (!IsNumeric(myobjPassNum, 10, selPoint)) {
                    isFlag = false;
                    fieldText = fieldText + "明细合格数量" + (i + 1) + "|";
                    msgText = msgText + "合格数量格式不正确|";
                }
            }

            if (strlen(myobjStardtandValue) > 0) {
                if (!CheckSpecialWord(myobjStardtandValue)) {
                    isFlag = false;
                    fieldText = fieldText + "明细标准值" + (i + 1) + "|";
                    msgText = msgText + "明细标准值不能含有特殊字符|";
                }
            }

            if (myobjIsPass == '00') {
                isFlag = false;
                fieldText = fieldText + "是否合格" + (i + 1) + "|";
                msgText = msgText + "请选择是否合格|";
            }
            if (strlen(myobjUserChecker) <= 0) {
                isFlag = false;
                fieldText = fieldText + "明细检验人员" + (i + 1) + "|";
                msgText = msgText + "请输入检验人员|";
            }
            if (strlen(myobjDept) <= 0) {
                isFlag = false;
                fieldText = fieldText + "明细检验部门" + (i + 1) + "|";
                msgText = msgText + "请输入检验部门|";
            }
            if (strlen(myobjCheckResult) > 1024) {
                isFlag = false;
                fieldText = fieldText + "检验结论" + (i + 1) + "|";
                msgText = msgText + "最多只允许输入1024个字符|";
            }

        }
    }

    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    return isFlag;
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
        var FilePath = $("#hfPageAttachment").val();
        var FileName = document.getElementById("attachname").innerHTML;
        DeleteUploadFile(FilePath, FileName);
        // 删除成功
        $("#hfPageAttachment").val("");
        // 更新附件字段
        UpDateAttachment();
    }
    //下载附件
    else if ("download" == flag) {
        //获取附件路径
        attachUrl = $("#hfPageAttachment").val();
        window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + attachUrl, "_blank");
    }
}

/*
* 上传文件后回调处理
*/
function AfterUploadFile(url) {
    if (url != "") {
        //设置附件路径
        $("#hfPageAttachment").val(url);
        var testurl = url.lastIndexOf('\\');
        testurl = url.substring(testurl + 1, url.lenght);
        document.getElementById('attachname').innerHTML = testurl;
        //下载删除显示
        document.getElementById("divDealAttachment").style.display = "";
        //上传不显示
        document.getElementById("divUploadAttachment").style.display = "none";
        // 更新附件字段
        UpDateAttachment();
    }
}

// 更新附件字段
function UpDateAttachment() {
    var Action = "";
    var txtIdentityID = $("#hiddenReportId").val();                      /*自增长ID*/
    if (txtIdentityID != "" && txtIdentityID != "undefined" && txtIdentityID != null) {
        Action = "updateattachment";
    }
    else {
        return;
    }

    var UrlParam = '';
    var Attachment = $("#hfPageAttachment").val().replace(/\\/g, "\\\\"); //附件   

    UrlParam = "myAction=" + Action +
              "&Attachment=" + escape(Attachment) +
              "&ID=" + escape(txtIdentityID);
    $.ajax({
        type: "POST",
        dataType: 'json', //返回json格式数据
        data: UrlParam,
        url: "../../../Handler/Office/StorageManager/StorageCheckReportAdd.ashx",
        cache: false,
        beforeSend: function() { },
        error: function() { },
        success: function(data) { }
    });

}

function SetSaveButton_DisplayControl(flowStatus) {

    //流程状态：0：待提交   1：待审批   2：审批中   3：审批通过     4：审批不通过   5：撤销审批
    //制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
    //制单状态且审批状态“撤销审批”、“ 审批拒绝”状态的可以进行修改
    //变更和手工结单的不可以修改
    var PageBillID = $("#hiddenReportId").val();
    var FromType = $("#sltFromType").val();
    var PageBillStatus = $("#txtBillStatus").val();
    if (PageBillID > 0) {
        if (PageBillStatus == '2' || PageBillStatus == '3' || PageBillStatus == '4') {
            //单据状态：变更和手工结单状态
            document.getElementById('btnSave').src = '../../../images/Button/UnClick_bc.jpg';
            if (FromType == "0") {
                $("#btnGetGoods").css("display", "none");
            }
            $("#isflow").val('1');
            $("#isupdate").val('1');


        }
        else {
            if (PageBillStatus == 1 && (flowStatus == 1 || flowStatus == 2 || flowStatus == 3)) {
                //单据状态+审批状态：制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
                document.getElementById('btnSave').src = '../../../images/Button/UnClick_bc.jpg';
                if (FromType == "0") {
                    $("#btnGetGoods").css("display", "none");
                }
                $("#isflow").val('1');
                $("#isupdate").val('1');


            }
            else {
                document.getElementById('btnSave').src = '../../../images/Button/Bottom_btn_save.jpg';
                if (FromType == "0") {
                    $("#btnGetGoods").css("display", "");
                }
                $("#isflow").val('0');
                $("#isupdate").val('0');
            }
        }
    }
}
//--------------------------------------------------------------------------------------条码扫描需要Start 
function GetGoodsDataByBarCode(ID, ProdNo, ProductName,
                                                  StandardSell, UnitID, CodeName,
                                                  TaxRate, SellTax, Discount,
                                                  Specification, CodeTypeName, TypeID,
                                                  StandardBuy, TaxBuy, InTaxRate,
                                                  StandardCost) {
    Fun_FillParent_Content(ID, ProdNo, ProductName, ProdNo, UnitID, CodeName, UnitID, CodeName, ID, Specification, Specification, Specification);
    $("#divStorageProduct").css("display", "none");
}







//function Fun_FlowApply_Operate_Succeed(myValues)// 0:提交审批成功  1:审批成功
//{
//      if (myValues != 2 && myValues != 3)
//      {
//   
//           document.getElementById('btnSave').src='../../../images/Button/UnClick_bc.jpg';
//           $("#isflow").val()='1';
//           $("#isupdate").val()='1';
//      }
//      else
//      {
//       
//           document.getElementById('btnSave').src='../../../images/Button/Bottom_btn_save.jpg';
//           $("#isflow").val()='0';
//           $("#isupdate").val()='0'; 
//      }
//}


