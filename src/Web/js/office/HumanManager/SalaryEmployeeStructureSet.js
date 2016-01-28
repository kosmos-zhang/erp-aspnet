


/*
* Ajax获取人员信息
*/
function GetUserInfo(EmployeeID, Name) {
    //切换人员的时候，清空提成率，并隐藏Span
    //$("#SpanCom").hide();
    //$("#SpanDep").hide();
    document.getElementById("SpanCom").style.display = "none";
    document.getElementById("SpanDep").style.display = "none";
    
    $("#txtComRate").val("");
    $("#txtDepRate").val("");

    $.ajax(
        {
            type: "POST",
            url: "../../../Handler/Office/HumanManager/SalaryEmployeeStructureSet.ashx",
            data: "Action=GetInfo&EmployeeID=" + EmployeeID,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            error: function() {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
            },
            success: function(msg) {
                $("#ckAll").attr("checked", false);
                $("#hidUserID").val(EmployeeID);
                $("#lbName").html(Name);

                if (msg.data.length > 0) {
                    //直接给CheckBox的checked属性赋0，或1，注意这里要转换为int类型的0，1
                    $("#chCom").attr("checked", parseInt(msg.data[0].IsCompanyRoyaltySet));
                    if (msg.data[0].IsCompanyRoyaltySet == "1") {
                        $("#txtComRate").val(msg.data[0].CompanyRatePercent);
                        document.getElementById("SpanCom").style.display = "";
                    }
                    $("#ckDep").attr("checked", parseInt(msg.data[0].IsDeptRoyaltySet));
                    if (msg.data[0].IsDeptRoyaltySet == "1") {
                        $("#txtDepRate").val(msg.data[0].DeptRatePercent);
                        document.getElementById("SpanDep").style.display = "";
                    }
                    $("#ckPro").attr("checked", parseInt(msg.data[0].IsProductRoyaltySet));
                    $("#ckFix").attr("checked", parseInt(msg.data[0].IsFixSalarySet));
                    $("#ckPiece").attr("checked", parseInt(msg.data[0].IsPieceWorkSet));
                    $("#ckInsur").attr("checked", parseInt(msg.data[0].IsInsurenceSet));
                    $("#ckPerIn").attr("checked", parseInt(msg.data[0].IsPerIncomeTaxSet));
                    $("#ckQute").attr("checked", parseInt(msg.data[0].IsQuteerSet));
                    $("#ckTime").attr("checked", parseInt(msg.data[0].IsTimeWorkSet));
                    $("#ckPerRoy").attr("checked", parseInt(msg.data[0].IsPersonalRoyaltySet));
                    $("#ckPerforman").attr("checked", parseInt(msg.data[0].IsPerformanceSet));

                }
                else {
                    $("input:checkbox", $("#tbchk")).each(function() {
                        this.checked = false;
                    }
                       );
                }
            },
            complete: function() { hidePopup(); }
        });
}


/*
* Ajax获取人员信息
*/
function DoSave() {
    //获取隐藏域里面的UserID，这是点击节点异步读取数据成功后赋值的
    var EmployeeID = $("#hidUserID").val();
    if (EmployeeID == "" || EmployeeID == null || EmployeeID == "undifine") {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先选择人员！");
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    if ($("#chCom").attr("checked") == true) {
        if ($("#txtComRate").val() == "" || parseFloat($("#txtComRate").val()) < 0 || parseFloat($("#txtComRate").val()) > 100) {
            isFlag = false;
            fieldText = fieldText + "公司提成率|";
            msgText = msgText + "请输入有效的数值（0~100）|";
        }
    }
    if ($("#ckDep").attr("checked") == true) {
        if ($("#txtDepRate").val() == "" || parseFloat($("#txtDepRate").val()) < 0 || parseFloat($("#txtDepRate").val()) > 100) {
            isFlag = false;
            fieldText = fieldText + "部门提成率|";
            msgText = msgText + "请输入有效的数值（0~100）|";
        }
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
    }

    var chCom = "0";
    var ckDep = "0";
    var ckPro = "0";
    var ckFix = "0";
    var ckPiece = "0";
    var ckInsur = "0";
    var ckPerIn = "0";
    var ckQute = "0";
    var ckTime = "0";
    var ckPerRoy = "0";
    var ckPerforman = "0";
    var txtComRate = "";
    var txtDepRate = "";

    if ($("#chCom").attr("checked") == true) {
        chCom = "1";
        txtComRate = $("#txtComRate").val();
    }
    if ($("#ckDep").attr("checked") == true) {
        ckDep = "1";
        txtDepRate = $("#txtDepRate").val();
    }
    if ($("#ckPro").attr("checked") == true)
        ckPro = "1";
    if ($("#ckFix").attr("checked") == true)
        ckFix = "1";
    if ($("#ckPiece").attr("checked") == true)
        ckPiece = "1";
    if ($("#ckInsur").attr("checked") == true)
        ckInsur = "1";
    if ($("#ckPerIn").attr("checked") == true)
        ckPerIn = "1";
    if ($("#ckQute").attr("checked") == true)
        ckQute = "1";
    if ($("#ckTime").attr("checked") == true)
        ckTime = "1";
    if ($("#ckPerRoy").attr("checked") == true)
        ckPerRoy = "1";
    if ($("#ckPerforman").attr("checked") == true)
        ckPerforman = "1";

    var tempInfo = new Array();
    tempInfo.push(chCom, ckDep, ckPro, ckFix, ckPiece, ckInsur, ckPerIn, ckQute, ckTime, ckPerRoy, ckPerforman, txtComRate, txtDepRate);

    $.ajax(
        {
            type: "POST",
            url: "../../../Handler/Office/HumanManager/SalaryEmployeeStructureSet.ashx",
            data: "Action=Save&EmployeeID=" + EmployeeID + "&message=" + tempInfo,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            error: function() {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
            },
            success: function(msg) {
                hidePopup();
                if (msg.sta == 1) {
                    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存成功！");
                }
                else {
                    hidePopup();
                    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存失败,请确认！");
                }
            },
            complete: function() { }
        });

}

//全选
function SelectAll() {
    if ($("#ckAll").attr("checked") == true) {
        $("input:checkbox", $("#tbchk")).each(function() {
            this.checked = true;
        }
        );
        //显示提成率文本框
        document.getElementById("SpanCom").style.display = "";
        document.getElementById("SpanDep").style.display = "";
        //$("#SpanCom").show();
        //$("#SpanDep").show();
    }
    else {
        $("input:checkbox", $("#tbchk")).each(function() {
            this.checked = false;
        }
        );
        //隐藏提成率文本框
        //$("#SpanCom").hide();
        //$("#SpanDep").hide();
        document.getElementById("SpanCom").style.display = "none";
        document.getElementById("SpanDep").style.display = "none";
    }
}

//显示Span
function ShowSpan(chid, spanid) {
    if ($("#" + chid).attr("checked") == true) {
        //document.getElementById(spanid).style.display="";
        $("#" + spanid).show();
    }
    else {
        //document.getElementById(spanid).style.display="none";
        $("#" + spanid).hide();
    }
}