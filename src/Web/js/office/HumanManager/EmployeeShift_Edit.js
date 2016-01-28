//打印
function DoPrint()
{
  if(document.getElementById("hidIdentityID").value=="0")
  {
     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请保存后打印");
     return;
  }  
  window.open("../../PrinttingModel/HumanManager/EmployeeShiftPrint.aspx?Id=" + document.getElementById("hidIdentityID").value);
}

/*
* 申请变更
*/
function DoApplyChange()
{
    //获取选中值
    selectValue = document.getElementById("ddlApply").value;
    //未选中时
    if (selectValue == null || selectValue == "")
    {
        //人员ID
        document.getElementById("txtEmployeeID").value = "";
        //人员编号
        document.getElementById("txtEmployeeNo").value = "";
        //人员编号不可编辑
        document.getElementById("txtEmployeeNo").disabled = false;
        //员工姓名
        document.getElementById("txtEmployeeName").value = "";
        //原部门
        document.getElementById("txtNowDeptName").value = "";
        document.getElementById("txtNowDeptID").value = "";
        //原岗位
        document.getElementById("txtNowQuarterName").value = "";
        document.getElementById("txtNowQuarterID").value = "";
        //原岗位职等
        document.getElementById("txtNowQuarterAdminName").value = "";
        document.getElementById("txtNowQuarterAdminID").value = "";
        //调入部门
        document.getElementById("DeptNew").value = "";
        document.getElementById("txtNewDept").value = "";
        //调入部门可修改
        document.getElementById("DeptNew").disabled = false;
        //调入岗位
        document.getElementById("ddlNewQuarter").value = "";
        //调入岗位可修改
        document.getElementById("ddlNewQuarter").disabled = false;
        //调入岗位职等
        document.getElementById("ctNewQuaterAdmin_ddlCodeType").value = "";
        //调入岗位职等可修改
        document.getElementById("ctNewQuaterAdmin_ddlCodeType").disabled = false;
    }
    else
    {
        //查询人员信息
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/HumanManager/EmployeeShift_Edit.ashx?Action=Apply&ApplyNo=" + selectValue,
            dataType:'json',//返回json格式数据
            cache:false,
            success:function(msg) 
            {
                hidePopup();
                /* 设置人员信息 */
                $.each(msg.data, function(i,item){
                    //人员ID
                    document.getElementById("txtEmployeeID").value = item.EmployeeID;
                    //人员编号
                    document.getElementById("txtEmployeeNo").value = item.EmployeeNo;
                    //人员编号不可编辑
                    document.getElementById("txtEmployeeNo").disabled = true;
                    //员工姓名
                    document.getElementById("txtEmployeeName").value = item.EmployeeName;
                    //原部门
                    document.getElementById("txtNowDeptName").value = item.NowDeptName;
                    document.getElementById("txtNowDeptID").value = item.NowDeptID;
                    //原岗位
                    document.getElementById("txtNowQuarterName").value = item.NowQuarterName;
                    document.getElementById("txtNowQuarterID").value = item.NowQuarterID;
                    //原岗位职等
                    document.getElementById("txtNowQuarterAdminName").value = item.NowAdminLevelName;
                    document.getElementById("txtNowQuarterAdminID").value = item.NowAdminLevelID;
                    //调入部门
                    document.getElementById("DeptNew").value = item.NewDeptName;
                    document.getElementById("txtNewDept").value = item.NewDeptID;
                    //调入部门不可修改
                    document.getElementById("DeptNew").disabled = true;
                    //调入岗位
                    document.getElementById("ddlNewQuarter").value = item.NewQuarterID;
                    //调入岗位不可修改
                    document.getElementById("ddlNewQuarter").disabled = true;
                    //调入岗位职等
                    document.getElementById("ctNewQuaterAdmin_ddlCodeType").value = item.NewAdminLevelID;
                    //调入岗位职等不可修改
                    document.getElementById("ctNewQuaterAdmin_ddlCodeType").disabled = true;
                });
            } 
        }); 
    }
}


/*
* 确认
*/
function DoConfirm()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
//    if (CheckConfirmInput())
//    {
//        return;
//    }
    //获取信息参数
    //离职编号
    postParams = "NotifyNo=" + document.getElementById("divCodeNo").value;
    //对应申请单
    postParams += "&EmplApplyNo=" + document.getElementById("ddlApply").value;
    //调职人
    postParams += "&EmployeeID=" + document.getElementById("txtEmployeeID").value;
    //调至部门
    postParams += "&NewDept=" + document.getElementById("txtNewDept").value;
    //调至岗位
    postParams += "&NewQuarter=" + document.getElementById("ddlNewQuarter").value;
    //调至岗位职等
    postParams += "&NewQuaterAdmin=" + document.getElementById("ctNewQuaterAdmin_ddlCodeType").value;
    //确认人
    postParams += "&Confirmor=" + document.getElementById("txtConfirmor").value;
    //确认日期
    postParams += "&ConfirmDate=" + document.getElementById("txtConfirmDate").value;
    //执行更新
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/EmployeeShift_Edit.ashx?Action=Confirm&" + postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            {
                //设置提示信息
                document.getElementById("btnConfirm").src = "../../../Images/Button/UnClick_qr.jpg";
                document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";

                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！");
                $("#txtConfirmor").val(data.data.split(',')[1]);
                $("#UserConfirmor").val(data.data.split(',')[0]);
                $("#txtConfirmDate").val(data.info);

            }
            else 
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认失败,请确认！");
            } 
        } 
    }); 
}

/*
* 输入信息校验
*/
function CheckConfirmInput()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;

    //确认人
    confirmor = document.getElementById("txtConfirmor").value;    
    if (confirmor == "" || confirmor == null)
    {
        isErrorFlag = true;
        fieldText += "确认人|";
        msgText += "请输入确认人|";  
    }
    //确认日期
    confirmDate = document.getElementById("txtConfirmDate").value;    
    if (confirmDate == "" || confirmDate == null)
    {
        isErrorFlag = true;
        fieldText += "确认日期|";
        msgText += "请输入确认日期|";  
    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText, msgText);
    }
    return isErrorFlag;
}

/*
* 保存按钮
*/
function DoSave()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckInput())
    {
        return;
    }
    //获取人员基本信息参数
    postParams = "Action=Save&" + GetPostParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/EmployeeShift_Edit.ashx",
        data : postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            {
                //设置ID 
                document.getElementById("hidIdentityID").value = data.info;
                /* 设置编号的显示 */ 
                //显示招聘申请的编号 招聘申请编号DIV可见              
                document.getElementById("divCodeNo").style.display = "block";
                //设置招聘申请编号
                document.getElementById("divCodeNo").value = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                //确认按钮
                document.getElementById("btnConfirm").src = "../../../Images/Button/Bottom_btn_confirm.jpg";
                document.getElementById("btnConfirm").attachEvent("onclick", DoConfirm);
//                $("#txtConfirmor").val($("#txtCreator").val());
//                $("#UserConfirmor").val($("#UserCreator").val());
//                $("#txtConfirmDate").val($("#txtCreateDate").val());

                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请输入未使用的编号！");
            }
            else if(data.sta==0)
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
            else if(data.sta == 3)
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","此员工离职单还未确认，不能再次新建！");
            } 
        } 
    }); 
}

/*
* 输入信息校验
*/
function CheckInput()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
     //获取ID
    id = document.getElementById("hidIdentityID").value;
    //新建时，编号选择手工输入时
    if (id == null || id == "")
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("codeRule_ddlCodeRule").value;
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            codeNo = document.getElementById("codeRule_txtCode").value;
            //编号必须输入
            if (codeNo == "")
            {
                isErrorFlag = true;
                fieldText += "调职单编号|";
   		        msgText += "请输入调职单编号|";
            }
            else
            {
                if (!CodeCheck(codeNo))
                {
                    isErrorFlag = true;
                    fieldText += "调职单编号|";
                    msgText += "调职单编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
                if(strlen(codeNo)>50)
                {
                    isErrorFlag = true;
                    fieldText += "调职单编号|";
                    msgText += "调职单编号不能超过50个字符";
                }
            }
        }
    }
    
    //调职单主题
    title = document.getElementById("txtTitle").value;
    if (title == "" || title == null)
    {
        isErrorFlag = true;
        fieldText += "调职单主题|";
        msgText += "请输入调职单主题|";  
    }
    //员工编号
    employeeID = document.getElementById("txtEmployeeID").value;
    if (employeeID == "" || employeeID == null)
    {
        isErrorFlag = true;
        fieldText += "员工编号|";
        msgText += "请输入员工编号|";  
    }
    //事由
    reason = document.getElementById("txtReason").value;
    if (reason == "" || reason == null)
    {
        isErrorFlag = true;
        fieldText += "事由|";
        msgText += "请输入事由|";  
    }
    //调入部门
    newDept = document.getElementById("txtNewDept").value;
    if (newDept == "" || newDept == null)
    {
        isErrorFlag = true;
        fieldText += "调入部门|";
        msgText += "请输入调入部门|";  
    }
    //调出时间
    outDate = document.getElementById("txtOutDate").value;    
    if (outDate == "" || outDate == null)
    {
        isErrorFlag = true;
        fieldText += "调出时间|";
        msgText += "请输入调出时间|";  
    }
    //调入时间
    intDate = document.getElementById("txtIntDate").value;    
    if (intDate == "" || intDate == null)
    {
        isErrorFlag = true;
        fieldText += "调入时间|";
        msgText += "请输入调入时间|";  
    }
    if (CompareDate(outDate, intDate) == 1)
    {
        isErrorFlag = true;
        fieldText += "调入时间|";
        msgText += "您输入的调入时间早于调出时间|";  
    }
    //制单人
    creator = document.getElementById("txtCreator").value;    
    if (creator == "" || creator == null)
    {
        isErrorFlag = true;
        fieldText += "制单人|";
        msgText += "请输入制单人|";  
    }
    //制单日期
    createDate = document.getElementById("txtCreateDate").value;    
    if (createDate == "" || createDate == null)
    {
        isErrorFlag = true;
        fieldText += "制单日期|";
        msgText += "请输入制单日期|";  
    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText, msgText);
    }
    return isErrorFlag;
}

/*
* 获取提交的基本信息
*/
function GetPostParams()
{
    //获取ID       
    id = document.getElementById("hidIdentityID").value;
    var strParams = "ID=" + id;//ID
    var no = "";
    //更新的时候
    if (id != null && id != "")
    {
        //编号
        no = document.getElementById("divCodeNo").value;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codeRule_ddlCodeRule").value;
        //手工输入的时候
        if ("" == codeRule)
        {
            //招聘申请编号
            no = document.getElementById("codeRule_txtCode").value;
        }
        else
        {
            //编码规则ID
            strParams += "&CodeRuleID=" + document.getElementById("codeRule_ddlCodeRule").value;
        }
    }
    //编号
    strParams += "&NotifyNo=" + no;
    //调职单主题
    strParams += "&Title=" + escape(document.getElementById("txtTitle").value);
    //对应申请单
    strParams += "&EmplApplyNo=" + document.getElementById("ddlApply").value;
    //被调职人
    strParams += "&EmployeeID=" + document.getElementById("txtEmployeeID").value;
    //事由
    strParams += "&Reason=" + escape(document.getElementById("txtReason").value);
    //原部门
    strParams += "&NowDeptID=" + document.getElementById("txtNowDeptID").value;
    //原岗位
    strParams += "&NowQuarterID=" + document.getElementById("txtNowQuarterID").value;
    //原岗位职等
    strParams += "&NowAdminLevel=" + document.getElementById("txtNowQuarterAdminID").value;    
    //调入部门
    strParams += "&NewDeptID=" + document.getElementById("txtNewDept").value;
    //调入岗位
    strParams += "&NewQuarterID=" + document.getElementById("ddlNewQuarter").value;
    //调入岗位职等
    strParams += "&NewAdminLevel=" + document.getElementById("ctNewQuaterAdmin_ddlCodeType").value;    
    //调出时间
    strParams += "&OutDate=" + document.getElementById("txtOutDate").value;
    //调入时间
    strParams += "&IntDate=" + document.getElementById("txtIntDate").value;
    
    //制单人
    strParams += "&Creator=" + document.getElementById("txtCreator").value;
    //制单日期
    strParams += "&CreateDate=" + document.getElementById("txtCreateDate").value;
    //确认人
    strParams += "&Confirmor=" + document.getElementById("txtConfirmor").value;
    //确认日期
    strParams += "&ConfirmDate=" + document.getElementById("txtConfirmDate").value;
    //备注
    strParams += "&Remark=" + escape(document.getElementById("txtRemark").value); 
    
    return strParams;
}

/*
* 返回按钮
*/
function DoBack()
{
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    fromPage = document.getElementById("hidFromPage").value;
    //快速调职
    if (fromPage == "0")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidFastModuleID").value;
        window.location.href = "FastShift_Info.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //列表页面
    else if (fromPage == "1")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidListModuleID").value;
        window.location.href = "EmployeeShift_Info.aspx?ModuleID=" + moduleID + searchCondition;
    }
}

/*
* 选择人才储备
*/
function SelectEmployeeInfo()
{
    //设置div的位置
    var selectEmployee = "";
    // 整个div的大小和位子
    selectEmployee += "<div id='divSelectEmployee' style='background-color:white;z-index:11;position:absolute;left:100;top:100;'>";
    // 白色div中的信息
    selectEmployee += "<table cellpadding='0' cellspacing='1' border='0' class='border' align=left>";
    selectEmployee += "<tr><td>";
    selectEmployee += "<iframe src='SelectEmployeeInfo.aspx' scrolling=yes width='800' height='500' frameborder='0'></iframe>";
    selectEmployee += "</td></tr>";
    selectEmployee += "</table>";
    //--end
    selectEmployee += "</div>";
    document.body.insertAdjacentHTML("afterBegin", selectEmployee);
}

/*
* 设置选择的人员信息
*/
function SetEmployeeInfo(employee)
{
    //设置选择的人员信息
    if (employee != null && typeof(employee) == "object")
    {
        //人员ID
        document.getElementById("txtEmployeeID").value = employee.EmployeeID;
        //人员编号
        document.getElementById("txtEmployeeNo").value = employee.EmployeeNo;
        //员工姓名
        document.getElementById("txtEmployeeName").value = employee.EmployeeName;
        //原部门
        document.getElementById("txtNowDeptName").value = employee.DeptName;
        document.getElementById("txtNowDeptID").value = employee.DeptID;
        //原岗位
        document.getElementById("txtNowQuarterName").value = employee.QuarterName;
        document.getElementById("txtNowQuarterID").value = employee.QuarterID;
        //原岗位职等
        document.getElementById("txtNowQuarterAdminName").value = employee.AdminLevelName;
        document.getElementById("txtNowQuarterAdminID").value = employee.AdminLevelID;
    }
    //选择页面不可见
    document.getElementById("divSelectEmployee").style.display = "none";
}