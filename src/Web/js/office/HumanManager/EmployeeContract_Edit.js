/*
* 返回按钮
*/
function DoBack()
{
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    fromPage = document.getElementById("hidFromPage").value;
    //待入职
    if (fromPage == "0")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidEnterModuleID").value;
        window.location.href = "WaitEnter.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //列表页面
    else if (fromPage == "1")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidListModuleID").value;
        window.location.href = "EmployeeContract_Info.aspx?ModuleID=" + moduleID + searchCondition;
    }
}
/*
* 附件处理
*/
function DealAttachment(flag)
{
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    //上传附件
    else if ("upload" == flag)
    {
        ShowUploadFile();
    }
    //清除附件
    else if ("clear" == flag)
    {
            //设置附件路径
            document.getElementById("hfPageAttachment").value = "";
            //下载删除不显示
            document.getElementById("divDealAttachment").style.display = "none";
            //上传显示 
            document.getElementById("divUploadAttachment").style.display = "block";
    }
    //下载附件
    else if ("download" == flag)
    {
            //获取附件路径
            attachUrl = document.getElementById("hfPageAttachment").value;
            //下载文件
            window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + attachUrl, "_blank");
    
    }
}

/*
* 上传文件后回调处理
*/
function AfterUploadFile(url,docName)
{
    if (url != "")
    {
        //设置附件路径
        document.getElementById("hfPageAttachment").value = url;
        //下载删除显示
        document.getElementById("divDealAttachment").style.display = "block";
        document.getElementById("spanAttachmentName").innerHTML = docName;
        //上传不显示
        document.getElementById("divUploadAttachment").style.display = "none";
    }
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
    postParams = GetPostParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/EmployeeContract_Edit.ashx",
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
                document.getElementById("divCodeNo").innerHTML = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请输入未使用的编号！");
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
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
                fieldText += "合同编号|";
   		        msgText += "请输入合同编号|";
            }
            else
            {
                if (!CodeCheck(codeNo))
                {
                    isErrorFlag = true;
                    fieldText += "合同编号|";
                    msgText += "合同编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
    
    var RetVal=CheckSpecialWords();    
    if(RetVal!="")
    {
       isErrorFlag = true;
        fieldText = fieldText + RetVal+"|";
	    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    
    //员工
    employeeID = document.getElementById("txtEmployeeID").value;
    if (employeeID == "" || employeeID == null)
    {
        isErrorFlag = true;
        fieldText += "员工|";
        msgText += "请输入员工|";  
    }
    var ctContractName = document.getElementById("ctContractName_ddlCodeType").value;
    if(ctContractName == "")
    {
        isErrorFlag = true;
        fieldText += "合同类别|";
        msgText += "请选择合同类别|";
    }
    //主题
    title = document.getElementById("txtTitle").value;
    if (title == "" || title == null)
    {
        isErrorFlag = true;
        fieldText += "合同名称|";
        msgText += "请输入合同名称|";  
    }
    //试用月数
    testMonth = document.getElementById("txtTestMonth").value;
    if (testMonth != "" && testMonth != null && !IsNumber(testMonth))
    {
        isErrorFlag = true;
        fieldText += "试用月数|";
        msgText += "请输入正确的试用月数|";  
    }
    //试用工资
    testWage = document.getElementById("txtTestWage").value;
    if (testWage != "" && testWage != null && !IsNumeric(testWage, 6, 2))
    {
        isErrorFlag = true;
        fieldText += "试用工资|";
        msgText += "请输入正确的试用工资|";  
    }
    //转正工资
    wage = document.getElementById("txtWage").value;
    if (wage != "" && !IsNumeric(wage, 6, 2))
    {
        isErrorFlag = true;
        fieldText += "转正工资|";
        msgText += "请输入正确的转正工资|";  
    }
    //签约时间
    signingDate = document.getElementById("txtSigningDate").value;    
    if (signingDate == "" || signingDate == null)
    {
        isErrorFlag = true;
        fieldText += "签约时间|";
        msgText += "请输入签约时间|";  
    }
    //生效时间
    startDate = document.getElementById("txtStartDate").value;    
    if (startDate == "" || startDate == null)
    {
        isErrorFlag = true;
        fieldText += "生效时间|";
        msgText += "请输入生效时间|";  
    }
    //签约时间 晚于生效时间时
    if (CompareDate(signingDate, startDate) == 1)
    {
        isErrorFlag = true;
        fieldText += "生效时间|";
        msgText += "您输入的生效时间早于签约时间|";  
    }
    //失效时间
    endDate = document.getElementById("txtEndDate").value;    
    if (endDate == "" || endDate == null)
    {
        isErrorFlag = true;
        fieldText += "失效时间|";
        msgText += "请输入失效时间|";  
    }
    //失效时间 早于 签约时间
    if (CompareDate(signingDate, endDate) == 1)
    {
        isErrorFlag = true;
        fieldText += "失效时间|";
        msgText += "您输入的失效时间早于签约时间|";  
    }
    //失效时间 早于生效时间
    if (CompareDate(startDate, endDate) == 1)
    {
        isErrorFlag = true;
        fieldText += "失效时间|";
        msgText += "您输入的失效时间早于生效时间|";  
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
        no = document.getElementById("divCodeNo").innerHTML;
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
    strParams += "&ContractNo=" + no;
    //员工
    strParams += "&EmployeeID=" + document.getElementById("txtEmployeeID").value;
    //合同名称
    strParams += "&ContractName=" + document.getElementById("ctContractName_ddlCodeType").value;
   // alert(document.getElementById("ctContractName_ddlCodeType").value);
    //主题
    strParams += "&Title=" + escape(document.getElementById("txtTitle").value);
    //alert(document.getElementById("txtTitle").value);
    //合同类型
    strParams += "&ContractType=" + document.getElementById("ddlContractType").value;
    //合同属性
    strParams += "&ContractProperty=" + document.getElementById("ddlContractProperty").value;
    //工种
    strParams += "&ContractKind=" ;
    //合同状态
    strParams += "&ContractStatus=" + document.getElementById("ddlContractStatus").value;
    //合同期限
    strParams += "&ContractPeriod=" + document.getElementById("ddlContractPeriod").value;
    //试用月数
    strParams += "&TrialMonthCount=" + document.getElementById("txtTestMonth").value;
    //试用工资
    strParams += "&TestWage=" + document.getElementById("txtTestWage").value;    
    //转正工资
    strParams += "&Wage=" + document.getElementById("txtWage").value;
    //签约时间
    strParams += "&SigningDate=" + document.getElementById("txtSigningDate").value;
    //生效时间
    strParams += "&StartDate=" + document.getElementById("txtStartDate").value;    
    //失效时间
    strParams += "&EndDate=" + document.getElementById("txtEndDate").value;
    //合同到期提醒人
    strParams += "&Reminder=" + document.getElementById("hidReminder").value;
    //提前时间
    strParams += "&AheadDay=" + document.getElementById("txtAheadDay").value;
    //转正标识
    strParams += "&Flag=" + document.getElementById("ddlFlag").value;
    //附件
    strParams += "&Attachment=" + escape(document.getElementById("hfAttachment").value);
    strParams += "&PageAttachment=" + escape(document.getElementById("hfPageAttachment").value);
    strParams += "&AttachmentName=" + escape(document.getElementById("spanAttachmentName").innerHTML);

    
    return strParams;
}

//打印功能
function fnPrintOrder() {
   
        var OrderNo = $.trim($("#codeRule_txtCode").val());
        if (OrderNo == '保存时自动生成' || OrderNo == '') {

            OrderNo = $.trim($("#divCodeNo").text());
            if (OrderNo == '保存时自动生成' || OrderNo == '') {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存您所填的信息！");
            }
            else {

                window.open('../../../Pages/PrinttingModel/HumanManager/EmployeeContractPrint.aspx?no=' + OrderNo);
            }
        }
        else {

            window.open('../../../Pages/PrinttingModel/HumanManager/EmployeeContractPrint.aspx?no=' + OrderNo);
        }

}