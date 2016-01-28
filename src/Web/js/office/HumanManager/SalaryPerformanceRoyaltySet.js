
/*
* 添加社会保险
*/
function DoAdd() {
    var EmployeeID = $("#HidEmployeeID").val()
    if (EmployeeID == "") {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先执行检索！");
        return;
    }
    //获取表格
    table = document.getElementById("tblSalary");
    //获取行号
    var no = table.rows.length;
    if (no > 1) {
        var ssd = no - 1;
        var getTemp = document.getElementById("txtMaxScore_" + ssd).value.Trim();
        if (getTemp == "" || getTemp == null || getTemp == "undifine") {
            document.getElementById("txtMaxScore_" + ssd).focus();
            return;
        }
        else {
            var getMaxMoney = document.getElementById("txtConfficent_" + ssd).value.Trim();
            if (getMaxMoney == "" || getMaxMoney == null || getMaxMoney == "undifine") {
                document.getElementById("txtConfficent_" + ssd).focus();
                return;
            }


        }
    }

    //添加新行
    var objTR = table.insertRow(-1);
    /* 添加行的单元格 */
    //选择框
    var objTD = objTR.insertCell(-1);
    objTD.className = "tdColInputCenter";
    objTD.innerHTML = "<input type='hidden' id='txtSalaryID_" + no + "' value='' />"
	                + "<input type='hidden' id='txtEditFlag_" + no + "' value='0' />"
	                + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + no + "'>";
    //上限
    if (no == 1) {
        objTD = objTR.insertCell(-1);
        objTD.className = "tdColInputCenter";
        objTD.innerHTML = "<input type='text' maxlength='12' style='width:98%;' readonly ='readonly' onchange='Number_round(this,0);'  onkeydown='Numeric_OnKeyDown();'  class='tdinput' id='txtMiniScore_" + no + "' value='0' />";
    }
    else {
        var row = no - 1;
        document.getElementById("chkSelect_" + row).style.display = "none";
        document.getElementById("txtMaxScore_" + row).readOnly = "true";
        document.getElementById("txtMiniScore_" + row).readOnly = "true";
        document.getElementById("txtConfficent_" + row).readOnly = "true";
        var preMoney = document.getElementById("txtMaxScore_" + row).value.Trim();
        objTD = objTR.insertCell(-1);
        objTD.className = "tdColInputCenter";
        objTD.innerHTML = "<input type='text' maxlength='12' style='width:98%;' readonly ='readonly'   onkeydown='Numeric_OnKeyDown();' onchange='Number_round(this,0);'   class='tdinput' id='txtMiniScore_" + no + "' value=" + preMoney + " />";
    }
    //下限
    objTD = objTR.insertCell(-1);
    objTD.className = "tdColInputCenter";
    objTD.innerHTML = "<input type='text' maxlength='12' style='width:98%;' onkeydown='Numeric_OnKeyDown();' onchange='Number_round(this,0);' onblur='CalculateTotalSalary(this, \"" + no + "\");'   class='tdinput' id='txtMaxScore_" + no + "' />";
    //提成率
    objTD = objTR.insertCell(-1);
    objTD.className = "tdColInputCenter";
    objTD.innerHTML = "<input type='text' maxlength='6' style='width:98%;' onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,2);'  onblur='CalculateTotalSalary(this, \"" + no + "\");'  class='tdinput' id='txtConfficent_" + no + "' />";

}


/*
* 设置全选CheckBox
*/
function SetCheckAll(obj) {
    //获取全选择控件
    checkAll = document.getElementById("chkAll");
    //如果checkbox未选中
    if (!obj.checked) {
        checkAll.checked = false;
        return;
    }
    else {
        isSelectAll = true;
        //获取表格
        table = document.getElementById("tblSalary");
        //获取表格行数
        var count = table.rows.length;
        if (count < 2) return;
        //遍历表格中的数据，判断是否选中
        for (var row = count - 1; row > 0; row--) {
            //行未被删除时
            if (table.rows[row].style.display != "none") {
                //获取选择框控件
                chkControl = document.getElementById("chkSelect_" + row);
                //如果未选中，则返回
                if (!chkControl.checked) {
                    isSelectAll = false;
                    break;
                }
            }
        }
        //列表中全部选中时，选择checkbox选中
        if (isSelectAll) {
            checkAll.checked = true;
        }
        //列表中有一个未选中时，选择checkbox未选中
        else {
            checkAll.checked = false;
        }
    }
}

/*
* 全选择表格行
*/
function SelectAll() {
    //获取表格
    table = document.getElementById("tblSalary");
    //获取表格行数
    var count = table.rows.length;
    if (count < 2) return;
    var isSelectAll = document.getElementById("chkAll").checked;
    //遍历表格中的数据，判断是否选中
    for (var row = count - 1; row > 0; row--) {
        //行未被删除时
        if (table.rows[row].style.display != "none") {
            //获取选择框控件
            chkControl = document.getElementById("chkSelect_" + row);
            //全选择
            if (isSelectAll) {
                chkControl.checked = true;
            }
            else {
                chkControl.checked = false;
            }
        }
    }
}


function tofloat(f, dec) {
    if (dec < 0) return "Error:dec<0!";
    result = parseInt(f) + (dec == 0 ? "" : ".");
    f -= parseInt(f);
    if (f == 0)
        for (i = 0; i < dec; i++) result += '0';
    else {
        for (i = 0; i < dec; i++) f *= 10;
        result += parseInt(Math.round(f));
    }
    return result;
}
/*
* 删除表格行
*/
function DoDelete() {


    //获取表格
    table = document.getElementById("tblSalary");
    //获取表格行数
    var count = table.rows.length;
    if (count < 2) return;
    //遍历表格中的数据，判断是否选中
    for (var row = count - 1; row > 0; row--) {
        //获取选择框控件
        chkControl = document.getElementById("chkSelect_" + row);
        //如果控件为选中状态，删除该行，并对之后的行改变控件名称
        if (chkControl.checked) {
            //删除行，实际是隐藏该行
            // table.rows[row].style.display = "none";
            table.deleteRow(row);
            var ss = row - 1;
            if (ss > 0) {
                document.getElementById("txtMaxScore_" + ss).readOnly = false;
                if (ss != 1) {
                    //	document .getElementById ("txtMiniScore_"+ss).readOnly=false;
                }
                document.getElementById("txtConfficent_" + ss).readOnly = false;
                document.getElementById("chkSelect_" + ss).style.display = "Block";
            }

        }
    }
}

function CalculateTotalSalary(obj, row) {
    //获取输入的值
    inputValue = obj.value;
    //判断输入的小数点
    var arr = inputValue.split(".");
    if (arr.length <= 2) {
        index = inputValue.indexOf(".");
        //输入的第一位是小数点时，前面加0
        if (index == 0) {
            obj.value = "0" + inputValue;
        }
        //最后一位是小数点时
        else if (index == inputValue.length - 1) {
            obj.value = inputValue.replace(".", "");
        }
        else if (index > -1) {
            obj.value = inputValue.substring(0, index + 3);
        }


        var minMoney = document.getElementById("txtMiniScore_" + row).value.Trim();
        var maxMoney = document.getElementById("txtMaxScore_" + row).value.Trim();
        var personTaxPercent = document.getElementById("txtConfficent_" + row).value.Trim();
        var isErrorFlag = false;
        var fieldText = "";
        //出错提示信息
        var msgText = "";
        if (maxMoney == null || maxMoney == "") {
        }
        else {
            if (!IsNumeric(maxMoney, 10, 2)) {
                fieldText += "第" + row + "行" + "总评分数下限项" + "|";
                msgText += "请输入正确的总评分数下限！|";
                isErrorFlag = true;
            }
        }
        if (personTaxPercent == null || personTaxPercent == "") {

        }
        else {
            if (!IsNumeric(personTaxPercent, 6, 2)) {
                fieldText += "第" + row + "行" + "绩效系数" + "|";
                msgText += "请输入正确的绩效系数！|";
                isErrorFlag = true;
            }
        }
        if ((personTaxPercent == null || personTaxPercent == "") && (maxMoney == null || maxMoney == "")) {
        }
        else {
            if (!isErrorFlag) {
                if (parseFloat(maxMoney) < parseFloat(minMoney)) {
                    fieldText += "第" + row + "行" + "总评分数下限项" + "|";
                    msgText += "应总评分数下限项应大于总评分数上限项！|";
                    isErrorFlag = true;
                }
            }
        }

        //如果有错误，显示错误信息
        if (isErrorFlag) {
            //显示错误信息
            //popMsgObj.Show(fieldText, msgText);

            document.getElementById("spanMsg").style.display = "block";
            document.getElementById("spanMsg").style.top = "240px";
            document.getElementById("spanMsg").style.left = "450px";
            document.getElementById("spanMsg").style.width = "290px";
            document.getElementById("spanMsg").style.position = "absolute";
            document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
            document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
            return;
        }


        if (row == 1) {
            document.getElementById("txtMiniScore_" + row).value = 0;
        }
        else {

            var info = new Array();
            var sum = 0;
            var leftMoney = 0;
            if (row > 1) {

                for (var i = 1; i < row; i++) {

                    var PreMinMoney = document.getElementById("txtMiniScore_" + i).value.Trim();
                    var PreMaxMoney = document.getElementById("txtMaxScore_" + i).value.Trim();
                    var PrePersonTaxPercent = document.getElementById("txtConfficent_" + i).value.Trim();

                    if (leftMoney == 0) {
                        leftMoney = minMoney - PreMaxMoney;
                        if (leftMoney > 0) {
                            sum = sum + PreMaxMoney * (personTaxPercent - PrePersonTaxPercent) / 100;
                        }
                        else if (leftMoney == 0) {
                            sum = sum + PreMaxMoney * (personTaxPercent - PrePersonTaxPercent) / 100;
                        }
                        else if (leftMoney < 0) {
                            sum = 0;
                        }

                    } else {

                        if (leftMoney - PreMaxMoney > 0) {
                            sum = sum + PreMaxMoney * (personTaxPercent - PrePersonTaxPercent) / 100;
                        }
                        else if (leftMoney - PreMaxMoney == 0) {
                            sum = sum + leftMoney * (personTaxPercent - PrePersonTaxPercent) / 100;
                        }
                        else if (leftMoney - PreMaxMoney < 0) {
                            sum = sum + leftMoney * (personTaxPercent - PrePersonTaxPercent) / 100;
                        }
                        leftMoney = leftMoney - PreMaxMoney;

                    }


                }

            }
        }

    }
}

/*
* 保存操作
*/
function DoSave() {
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回

    if (CheckInput()) {
        return;
    }
    var EmployeeID = $("#HidEmployeeID").val();
    var Taskflag = $("#sltTaskflag").val();
    //获取人员基本信息参数
    postParams = "Action=Save&EmployeeID=" + escape(EmployeeID) + "&Taskflag=" + escape(Taskflag) + "&message=" + GetPostParams();
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryPerformanceRoyaltySet.ashx",
        data: postParams,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.sta == 1) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存成功！");
            }
            else {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存失败,请确认！");
            }
        }
    });
}


function GetPostParams() {

    var tempInfo = new Array();
    table = document.getElementById("tblSalary");
    //获取行号
    var no = table.rows.length;
    for (var i = 1; i < no; i++) {
        var minMoney = document.getElementById("txtMiniScore_" + i).value.Trim();
        var maxMoney = document.getElementById("txtMaxScore_" + i).value.Trim();
        var personTaxPercent = document.getElementById("txtConfficent_" + i).value.Trim();
        tempInfo.push(i, minMoney, maxMoney, personTaxPercent);
    }
    return tempInfo;


}

/*
* 输入信息校验
*/
function CheckInput() {
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;

    var EmployeeID = $("#txtEmployeeID").val();
    var Taskflag = $("#sltTaskflag").val();
    var hidTaskflag = $("#hidTaskflag").val();

    //    if(EmployeeID==""||EmployeeID==null||EmployeeID=="undifine")
    //    {
    //        isErrorFlag = true;
    //        fieldText += "人员|";
    //        msgText += "请选择人员！|";
    //    }
    if (hidTaskflag == "" || hidTaskflag == null || hidTaskflag == "undifine") {
        isErrorFlag = true;
        fieldText += "检索|";
        msgText += "请先执行检索！|";
    }
    else {
        if (Taskflag != hidTaskflag) {
            isErrorFlag = true;
            fieldText += "数据和考核类型不一致|";
            msgText += "请先执行检索！|";
        }
    }
    table = document.getElementById("tblSalary");


    //获取行号
    var no = table.rows.length;
    for (var i = 1; i < no; i++) {
        var minMoney = document.getElementById("txtMiniScore_" + i).value.Trim();
        if (minMoney == "" || minMoney == null || minMoney == "undifine") {
            isErrorFlag = true;
            fieldText += "第" + no + "行" + "总评分数上限项" + "|";
            msgText += "请输入总评分数上限项！|";
            break;
        }
        var maxMoney = document.getElementById("txtMaxScore_" + i).value.Trim();
        if (maxMoney == "" || maxMoney == null || maxMoney == "undifine") {
            isErrorFlag = true;
            fieldText += "第" + no + "行" + "总评分数下限项" + "|";
            msgText += "请输入总评分数下限项！|";
            break;
        }
        var personTaxPercent = document.getElementById("txtConfficent_" + i).value.Trim();
        if (personTaxPercent == "" || personTaxPercent == null || personTaxPercent == "undifine") {
            isErrorFlag = true;
            fieldText += "第" + no + "行" + "绩效系数项" + "|";
            msgText += "请输入绩效系数！|";
            break;
        }
        if (parseFloat(maxMoney) < parseFloat(minMoney)) {
            fieldText += "第" + no + "行" + "总评分数下限项" + "|";
            msgText += "应总评分数下限项应大于业绩上限项！|";
            isErrorFlag = true;
            break
        }

        //验证数字Numeric(12,2),这是前面10为整数，后面2个小数的形式(这里只判断下限)
        var obj = document.getElementById("txtMaxScore_" + i);
        inputValue = obj.value.Trim();
        //判断输入的小数点
        var arr = inputValue.split(".");
        if (arr.length <= 2) {
            index = inputValue.indexOf(".");
            //输入的第一位是小数点时，前面加0
            if (index == 0) {
                obj.value = "0" + inputValue;
            }
            //最后一位是小数点时
            else if (index == inputValue.length - 1) {
                obj.value = inputValue.replace(".", "");
            }
            else if (index > -1) {
                obj.value = inputValue.substring(0, index + 3);
            }

            if (!IsNumeric(maxMoney, 10, 2)) {
                fieldText += "第" + (no - 1) + "行" + "总评分数下限项" + "|";
                msgText += "请输入正确的总评分数下限！|";
                isErrorFlag = true;
                break;
            }
        }


    }

    //如果有错误，显示错误信息
    if (isErrorFlag) {
        //显示错误信息
        //popMsgObj.Show(fieldText, msgText);

        document.getElementById("spanMsg").style.display = "block";
        document.getElementById("spanMsg").style.top = "240px";
        document.getElementById("spanMsg").style.left = "450px";
        document.getElementById("spanMsg").style.width = "290px";
        document.getElementById("spanMsg").style.position = "absolute";
        document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
        document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
    }

    return isErrorFlag;
}
///////创建层（）
function CreateErrorMsgDiv(fieldText, msgText) {
    errorMsg = "";
    if (fieldText != null && fieldText != "" && msgText != null && msgText != "") {
        var fieldArray = fieldText.split("|");
        var alertArray = msgText.split("|");
        for (var i = 0; i < fieldArray.length - 1; i++) {
            errorMsg += "<strong>[</strong><font color=\"red\">" + fieldArray[i].toString()
                        + "</font><strong>]</strong>：" + alertArray[i].toString() + "<br />";
        }
    }
    table = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>"
            + "<tr><td align='center' height='1'>&nbsp;</td></tr>"
            + "<tr><td align='center'>" + errorMsg + "</td></tr>"
            + "<tr><td align='right'>"
            + "<img src='../../../Images/Button/closelabel.gif' onclick=\"document.getElementById('spanMsg').style.display='none';\" />"
            + "&nbsp;&nbsp;</td></tr></table>";

    return table;
}

//拼接隐藏域ID
function GetHidID(ID) {
    return "Hid" + ID;
}

//查询当前员工信息
function DoSearch() {
    //获取值人员
    var EmployeeID = $("#txtEmployeeID").val();
    //考核类型
    var Taskflag = $("#sltTaskflag").val();

    //验证
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    if (EmployeeID == "") {
        EmployeeID = "0"; //如果没有选择人员就去查公司的默认设置
        //        isFlag = false;
        //        fieldText += "人员|";
        //        msgText += "请选择人员|";
    }

    if (!isFlag) {
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        document.getElementById("spanMsg").style.display = "block";
        document.getElementById("spanMsg").style.top = "240px";
        document.getElementById("spanMsg").style.left = "450px";
        document.getElementById("spanMsg").style.width = "290px";
        document.getElementById("spanMsg").style.position = "absolute";
        document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
        document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
        return;
    }
    //验证结束

    var postParam = "Action=GetInfo&EmployeeID=" + escape(EmployeeID) + "&Taskflag=" + escape(Taskflag);
    $.ajax(
        {
            type: "POST",
            url: "../../../Handler/Office/HumanManager/SalaryPerformanceRoyaltySet.ashx",
            data: postParam,
            dataType: 'html', //返回html格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            error: function() {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
                //popMsgObj.ShowMsg('请求发生错误！');
            },
            success: function(data) {
                //给隐藏域赋值，以便判断是否执行过检索的数据跟更换的人员或下拉是否一致
                $("#HidEmployeeID").val(EmployeeID);
                $("#hidTaskflag").val(Taskflag);

                //先清空容器
                //$("#divSalaryList tbody").find("tr.newrow").remove();
                var dg_Log = findObj("tblSalary", document);
                var rowscount = dg_Log.rows.length;
                for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除
                    dg_Log.deleteRow(i);
                }

                //激活下拉框
                //document.getElementById("sltTaskflag").disabled=false;
                if (data.length > 0) {
                    var datalist = data.split('*');
                    $(datalist[0].toString()).appendTo($("#divSalaryList tbody"));
                    $("#sltTaskflag").val(datalist[1]);
                }

                //$("#divSalaryList").html(data);
            },
            complete: function() { hidePopup(); }
        });
}


/*--------------------------弹出人员选择层，把公用拖出来，跟frame框架名有冲突---------------------*/
function alertdiv1(ControlID) {
    var Array = ControlID.split(",");
    if (Array[0].indexOf("Dept") >= 0) {
        if (Array.length == 2) {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=1";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var splitInfo = returnValue.split("|");
                window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = splitInfo[0].toString();
                window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = splitInfo[1].toString();
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "";
                window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "";
            }
        }
        else {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=2";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var ID = "";
                var Name = "";
                var getinfo = returnValue.split(",");
                for (var i = 0; i < getinfo.length; i++) {
                    var c = getinfo[i].toString();
                    ID += c.substring(0, c.indexOf("|")) + ",";
                    Name += c.substring(c.indexOf("|") + 1, c.length) + ",";
                }
                ID = ID.substring(0, ID.length - 1);
                Name = Name.substring(0, Name.length - 1);
                //  window.parent.document.getElementById(Array[1]).value =ID;
                if (window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value != "") {
                    var Oldvalue = window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value;
                    var Newvalue = Name;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {

                            window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                    else {
                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                    // window.parent.document.getElementById(Array[0]).value+=window.parent.document.getElementById(Array[0]).value =","+Name;
                }
                else {
                    window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = Name;
                }
                if (window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value != "") {
                    // window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =","+ID;
                    var Oldvalue = window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value;
                    var Newvalue = ID;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                    else {
                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                }
                else {
                    window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = ID;
                }
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "";
                window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "";
            }
        }
    }
    if (Array[0].indexOf("User") >= 0) {

        if (Array.length == 2) {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var splitInfo = returnValue.split("|");


                window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = splitInfo[0].toString();
                window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = splitInfo[1].toString();
                // window.parent.document.getElementById(Array[1]).value =
                //window.parent.document.getElementById(Array[0]).value =splitInfo[1].toString();
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "";
                window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "";
                //window.parent.document.getElementById(Array[0]).value="";
                //window.parent.document.getElementById(Array[1]).value=""; 
            }
        }
        else {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=2";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var ID = "";
                var Name = "";
                var getinfo = returnValue.split(",");
                for (var i = 0; i < getinfo.length; i++) {
                    var c = getinfo[i].toString();
                    ID += c.substring(0, c.indexOf("|")) + ",";
                    Name += c.substring(c.indexOf("|") + 1, c.length) + ",";
                }
                ID = ID.substring(0, ID.length - 1);
                Name = Name.substring(0, Name.length - 1);
                //window.parent.document.getElementById(Array[1]).value =ID; 
                if (window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value != "") {
                    var Oldvalue = window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value;
                    var Newvalue = Name;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                    else {

                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                }
                else {
                    window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = Name;
                }
                if (window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value != "") {
                    var Oldvalue = window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value;
                    var Newvalue = ID;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                    else {
                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                }
                else {
                    window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = ID;
                }
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "";
                window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "";
            }
        }
    }

}