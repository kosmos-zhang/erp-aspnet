/*
* 设置考试结果
*/
function SetTestResult()
{
    //获取表格
    var table = document.getElementById("tblTestResultDetail");
    //获取已有的表格行数
    var tableLen = table.rows.length;
    //删除一览中已有的的数据
    for (var i = tableLen - 1; i > 0; i--)
    {
        //删除行
        table.deleteRow(i);
    }
    //获取参加人员ID
    joinUserID = document.getElementById("txtJoinUserID").value;
    document.getElementById("txtJoinUserID4Modify").value = joinUserID;
    
    //参与人员未设置时，返回
    if (joinUserID == "")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择参与人员！");
        return;
    }
    IDList = joinUserID.split(",");
    nameList = document.getElementById("UserJoinName").value.split(",");
    //定义行变量
    var objTR;
    //记录数标识
    var index = 0;
    for (var i = 0; i < IDList.length; i++)
    {
   // alert(i);
        //当标识为0时，新建一行
        if (index == 0)
        {
            //添加一新行
            objTR = table.insertRow(-1);
            index = 1;
        }
        //当标识为1时，在原来行后面添加
        else if(index == 1)
        {
            index = 2;
        }
        else
        {
            index = 0;
        }
		var userID = IDList[i];
        //姓名
        objTD = objTR.insertCell(-1);
        objTD.className = "tdColInputCenter";
        objTD.innerHTML = "<input type='hidden' class='tdinput' value='" + userID + "' id='txtUser_" + userID + "'>"
                            + nameList[i];
        //考试得分
        objTD = objTR.insertCell(-1);
        objTD.className = "tdColInput";
        objTD.innerHTML = "<input type='text' maxlength = '6' style='width:90%;' class='tdinput' id='txtUser_" + userID + "_Score'>";
    }            
    //当标识小于2时，在原来行后面添加列补全表格
    if (parseInt(index) > 0)
    {
        //最后一行相差的参与人员个数
        var col = 3 - parseInt(index);
        //添加对应的列
        for (var j = 0; j < col; j++)
        {
            //姓名
            objTD = objTR.insertCell(-1);
            objTD.className = "tdColInputCenter";
            objTD.innerHTML = "&nbsp;";
            //考核等分
            objTD = objTR.insertCell(-1);
            objTD.className = "tdColInputCenter";
            objTD.innerHTML = "&nbsp;";
        }
    }
}

/*打印*/
function DoPrint()
{
try
 {
      if(document.getElementById("divCodeNo").value=="")
      {
         showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请保存后打印");
         return;
      }  
      window.open("../../PrinttingModel/HumanManager/EmployeeTestPrint.aspx?Id=" + document.getElementById("divCodeNo").value);
  }
  catch(e){}
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
function AfterUploadFile(url, docName)
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
* 基本信息校验
*/
function CheckBaseInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value;
    //新建时，编号选择手工输入时
    if ("INSERT" == editFlag)
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
                fieldText += "考试编号|";
   		        msgText += "请输入考试编号|";
            }
            else
            {
                if (!CodeCheck(codeNo))
                {
                    isErrorFlag = true;
                    fieldText += "考试编号|";
                    msgText += "考试编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
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
	       // popMsgObj.Show(fieldText,msgText);
	        //return;
       }
    //主题必须输入
    if (document.getElementById("txtTitle").value == "")
    {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "请输入主题|";  
    }
    //考试负责人必须输入
    if (document.getElementById("txtTeacher").value == "")
    {
        isErrorFlag = true;
        fieldText += "考试负责人|";
        msgText += "请输入考试负责人|";  
    }
    //开始时间
    startDate = document.getElementById("txtStartDate").value;
    if (startDate == "")
    {
        isErrorFlag = true;
        fieldText += "开始时间|";
        msgText += "请输入开始时间|";  
    }
    else if (!isDate(startDate))
    {
        isErrorFlag = true;
        fieldText += "开始时间|";
        msgText += "请输入正确的开始时间|";  
    }
    //结束时间
    endDate = document.getElementById("txtEndDate").value;
    if (endDate == "")
    {
        isErrorFlag = true;
        fieldText += "结束时间|";
        msgText += "请输入结束时间|";  
    }
    else if (!isDate(endDate))
    {
        isErrorFlag = true;
        fieldText += "结束时间|";
        msgText += "请输入正确的结束时间|";  
    }
    //比较开始时间和结束时间
    if (startDate != "" && endDate != "")
    {
        if (CompareDate(startDate, endDate) == 1)
        {
            isErrorFlag = true;
            fieldText += "开始时间|";
            msgText += "您输入的开始时间晚于结束时间|";  
        }
    }
    //考试地点
    if (document.getElementById("txtAddress").value == "")
    {
        isErrorFlag = true;
        fieldText += "考试地点|";
        msgText += "请输入考试地点|";  
    }
    //缺考人数
    var absenceCount = document.getElementById("txtAbsenceCount").value;
    if (absenceCount != "" && !IsNumber(absenceCount))
    {
        isErrorFlag = true;
        fieldText += "缺考人数|";
        msgText += "请输入正确的缺考人数|";  
    }
    //参与人员
    joinUserID = document.getElementById("txtJoinUserID").value;
    if (joinUserID == "" || joinUserID == null)
    {
        isErrorFlag = true;
        fieldText += "参与人员|";
        msgText += "请输入参与人员|";  
    }
    else
    {
        //获取录入成绩前的参与人员
        modify = document.getElementById("txtJoinUserID4Modify").value;
        if (joinUserID != modify)
        {
            isErrorFlag = true;
            fieldText += "参与人员|";
            msgText += "参与人员已修改，但未录入对应成绩|";
        }
    }
    //考试内容摘要
    var Content = document.getElementById("txtContent").value;
    //考试内容摘要输入时，校验长度
    if (Content != null && Content != "")
    {
        if (strlen(Content) > 1024)
        {
            isErrorFlag = true;
            fieldText += "考试内容摘要|";
            msgText += "考试内容摘要最多只允许输入512个字符|";  
        }
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
* 考试结果校验
*/
function CheckResultInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;    
    
    //获取参加人员ID
    joinUserID = document.getElementById("txtJoinUserID").value;
    //参与人员未设置时，返回
    if (joinUserID == "")
    {
        return;
    }
    //获取参与人员ID
    IDList = joinUserID.split(",");
    //获取参与人员姓名
    nameList = document.getElementById("UserJoinName").value.split(",");
    //遍历所有参与人员，校验分数正确性
    for (var i = 0; i < IDList.length; i++)
    { 
		var userID = IDList[i];
		if(userID=="")
		    break;
        //考试得分
        score = document.getElementById("txtUser_" + userID + "_Score").value; 
        //判断输入是否正确
        if(score != "" && !IsNumberOrNumeric(score, 3, 2))
        {
            isErrorFlag = true;
            fieldText += "考试结果：" + nameList[i] + " 考试得分|";
            msgText += "请输入正确的考试得分|";  
        }
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
* 保存考试记录信息
*/
function DoSave()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    //考试结果校验 有错误时，返回
    } else if(CheckResultInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    //获取进度安排参数
    postParams += GetResultParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/EmployeeTest_Edit.ashx",
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
            //隐藏提示框
            hidePopup();
            //保存成功
            if(data.sta == 1) 
            { 
                //设置编辑模式
                $("#divCodeNo").attr("disabled",true);
                
                document.getElementById("hidEditFlag").value = data.info;
                /* 设置编号的显示 */ 
                //显示考核的编号 考核编号DIV可见              
                document.getElementById("divCodeNo").style.display = "block";
                //设置考核编号
                document.getElementById("divCodeNo").value = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            //编号已存在
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","您输入的编号已经存在！");
            }
            //保存失败
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            }
        } 
    });  
}

/*
* 基本信息参数
*/
function GetBaseInfoParams()
{
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value;
    //新建时，编号选择手工输入时
    var param = "EditFlag=" + editFlag;
    var no = "";
    //更新的时候
    if ("INSERT" != editFlag)
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
            //招聘活动编号
            no = document.getElementById("codeRule_txtCode").value;
        }
        else
        {
            //编码规则ID
            param += "&CodeRuleID=" + codeRule;
        }
    }
    //编号
    param += "&TestNo=" + no;
    //主题
    param += "&Title=" + escape(document.getElementById("txtTitle").value);
    //考试负责人
    param += "&Teacher=" + document.getElementById("txtTeacher").value;
    //开始时间
    param += "&StartDate=" + document.getElementById("txtStartDate").value;
    //结束时间
    param += "&EndDate=" + document.getElementById("txtEndDate").value;
    //考试地点
    param += "&Address=" + escape(document.getElementById("txtAddress").value);
    //考试结果
    param += "&TestResult=" + escape(document.getElementById("txtTestResult").value);
    //考试状态
    param += "&Status=" + document.getElementById("ddlStatus").value;
    //附件
    param += "&Attachment=" + escape(document.getElementById("hfAttachment").value);
    param += "&PageAttachment=" + escape(document.getElementById("hfPageAttachment").value);
    param += "&AttachmentName=" + escape(document.getElementById("spanAttachmentName").innerHTML);
    //缺考人数
    param += "&AbsenceCount=" + document.getElementById("txtAbsenceCount").value;
    //参与人员
    param += "&JoinUserID=" + document.getElementById("txtJoinUserID").value;
    //考试内容摘要
    param += "&Content=" + escape(document.getElementById("txtContent").value);
    //备注
    param += "&Remark=" + escape(document.getElementById("txtRemark").value);
    
    return param;
}

/*
* 考试结果参数
*/
function GetResultParams()
{   
    var param = "";
    //获取参加人员ID
    joinUserID = document.getElementById("txtJoinUserID").value;
    //参与人员未设置时，返回
    if (joinUserID == "")
    {
        return;
    }
    //获取参与人员ID
    IDList = joinUserID.split(",");
    //遍历所有参与人员，校验分数正确性
    for (var i = 0; i < IDList.length; i++)
    { 
		var userID = IDList[i];
		if(userID=="")
		    break;
        //考试得分
        param += "&User" + userID + "Score" + "=" + document.getElementById("txtUser_" + userID + "_Score").value;
    }
    //返回参数信息
    return param;
}

/*
* 返回考试记录列表
*/
function DoBack()
{
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    window.location.href = "EmployeeTest_Info.aspx?ModuleID=" + ModuleID + searchCondition;
}