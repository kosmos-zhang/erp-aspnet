/*
* 添加进度安排
*/
function AddScheduleInfo()
{
    //获取表格
    table = document.getElementById("tblScheduleDetailInfo");
    //获取行号
    var no = table.rows.length;
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='checkbox' id='chkSelect_" + no + "'>";
	//进度时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.width = "12%";                                                                                    
	objTD.innerHTML = "<input type='text' ReadOnly  maxlength = '10' class='tdinput' id='txtScheduleDate_" + no + "' onchange=\"chenkdate('txtScheduleDate_"+no+"\')\" onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtScheduleDate_" + no + "')})\">";
	//内容摘要
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.width = "40%";
	objTD.innerHTML = "<input type='text' style='width:95%' maxlength = '100' class='tdinput' id='txtAbstract_" + no + "'>";
	//备注
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.width = "40%";
	objTD.innerHTML = "<input type='text' style='width:95%' maxlength = '100' class='tdinput' id='txtRemark_" + no + "'>";
}

//进度安排的“时间”不能在“开始时间”和“结束时间”之外
function chenkdate(txtName)
{
    var dateBegin = document.getElementById("txtStartDate").value;
    var dateM = document.getElementById(txtName).value;
    var dateEnd = document.getElementById("txtEndDate").value;
    
    if (CompareDate(dateBegin, dateM) == 1 || CompareDate(dateM, dateEnd) == 1)
    {
        document.getElementById(txtName).value = "";       
        var fieldText = "时间|";
        var msgText = "进度安排中时间只能在开始时间和结束时间之间|";
        popMsgObj.Show(fieldText, msgText);
    }
}

/*
* 删除表格行
*/
function DeleteScheduleInfo(){
    //获取表格
    table = document.getElementById("tblScheduleDetailInfo");
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById("chkSelect_" + row);
	    //如果控件为选中状态，删除该行，并对之后的行改变控件名称
	    if (chkControl.checked)
	    {
	       //删除行，实际是隐藏该行
	       table.rows[row].style.display = "none";
	    }
	}
}

/*
* 全选择表格行
*/
function SelectAll(){
    //获取表格
    table = document.getElementById("tblScheduleDetailInfo");
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	var isSelectAll = document.getElementById("chkAll").checked;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById("chkSelect_" + row);
	    //全选择
	    if (isSelectAll)
	    {
	        chkControl.checked = true;
	    }
	    else
	    {
	        chkControl.checked = false;
	    }
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
//            //设置附件路径
//            document.getElementById("hfPageAttachment").value = "";
//            //下载删除不显示
//            document.getElementById("divDealAttachment").style.display = "none";
//            //上传显示 
//            document.getElementById("divUploadAttachment").style.display = "block";
            var FilePath=document.getElementById("hfPageAttachment").value;
            var FileName=document.getElementById("spanAttachmentName").innerHTML;
            DeleteUploadFile(FilePath,FileName);
             //设置附件路径
            document.getElementById("hfPageAttachment").value = "";
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
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value;
    //新建时，编号选择手工输入时
    if ("INSERT" == editFlag)
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("codruleTraining_ddlCodeRule").value;
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            trainingNo = document.getElementById("codruleTraining_txtCode").value;
            //编号必须输入
            if (trainingNo == "")
            {
                isErrorFlag = true;
                fieldText += "培训编号|";
   		        msgText += "请输入培训编号|";
            }
            else
            {
                if (!CodeCheck(trainingNo))
                {
                    isErrorFlag = true;
                    fieldText += "培训编号|";
                    msgText += "培训编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
    
    //培训名称必须输入
    if (document.getElementById("txtTrainingName").value == "")
    {
        isErrorFlag = true;
        fieldText += "培训名称|";
        msgText += "请输入培训名称|";  
    }
    //发起时间
    applyDate = document.getElementById("txtApplyDate").value;
    if (applyDate == "")
    {
        isErrorFlag = true;
        fieldText += "发起时间|";
        msgText += "请输入发起时间|";  
    }
    else if (!isDate(applyDate))
    {
        isErrorFlag = true;
        fieldText += "发起时间|";
        msgText += "请输入正确的发起时间|";  
    }
    //发起人
    if (document.getElementById("txtCreateID").value == "")
    {
        isErrorFlag = true;
        fieldText += "发起人|";
        msgText += "请输入发起人|";
    }
    //项目编号 
    projectNo = document.getElementById("txtProjectNo").value;
    if (projectNo != "")
    {   
       var lst = /^[0-9a-zA-Z_]+$/;
       if (!lst.test(projectNo))
       {   
            isErrorFlag = true;
            fieldText += "项目编号|";
            msgText += "项目编号只能包含字母或数字或下划线|";
       }
    }
    //预算费用 
    planCost = document.getElementById("txtPlanCost").value;
    if (planCost != "" && !IsNumeric(planCost, 10, 2))
    {
        isErrorFlag = true;
        fieldText += "预算费用|";
        msgText += "提示预算费用的整数位数不能大于10且小数位数不能大于2|";
    }
    //培训天数
    trainingCount = document.getElementById("txtTrainingCount").value;
    if (trainingCount != "" && (!IsNumber(trainingCount) || parseInt(trainingCount) < 1))
    {
        isErrorFlag = true;
        fieldText += "培训天数|";
        msgText += "请输入正确的培训天数|";
    }
    //开始时间
    startDate = document.getElementById("txtStartDate").value;
    //结束时间
    endDate = document.getElementById("txtEndDate").value;
    if (CompareDate(startDate, endDate) == 1)
    {
        isErrorFlag = true;
        fieldText += "开始时间|";
        msgText += "您输入开始时间晚于结束时间|";   
    }   
    if(trainingCount != "" && CompareDate(startDate, endDate) != 1)
    {       
        if((IsNumber(trainingCount) || parseInt(trainingCount) >= 1))
        {
            var Days = getDateDiff(startDate,endDate)
            if(Days < trainingCount)
            {
                 isErrorFlag = true;
                 fieldText += "培训天数|";
                 msgText += "培训天数不能大于结束时间减去开始时间|";
            }
        }
    }
    
    //参与人员
    joinUser = document.getElementById("txtJoinUser").value;
    if (joinUser == "")
    {
        isErrorFlag = true;
        fieldText += "参与人员|";
        msgText += "请输入参与人员|";   
    }
     
    var TrainingTeacher = document.getElementById("txtTrainingTeacher").value;
    if(TrainingTeacher != ""  && strlen(TrainingTeacher) > 50)
    {
        isErrorFlag = true;
        fieldText += "培训老师|";
        msgText += "培训老师最多只允许输入50个字符|";
    }   
    
    var TrainingPlace = document.getElementById("txtTrainingPlace").value;
    if(TrainingPlace != ""  && strlen(TrainingPlace) > 100)
    {
        isErrorFlag = true;
        fieldText += "培训地点|";
        msgText += "培训地点最多只允许输入100个字符|";
    }   

    var CheckPerson = document.getElementById("txtCheckPerson").value;
    if(CheckPerson != ""  && strlen(CheckPerson) > 30)
    {
        isErrorFlag = true;
        fieldText += "考核人|";
        msgText += "考核人最多只允许输入30个字符|";
    }
    
    var Goal = document.getElementById("txtGoal").value;
    if(Goal != "" && strlen(Goal) > 200 )
    {
        isErrorFlag = true;
        fieldText += "培训目的|";
        msgText += "培训目的最多只允许输入200个字符|";
    }
    
    var TrainingRemark = document.getElementById("txtTrainingRemark").value;
    if(TrainingRemark != "" && strlen(TrainingRemark) > 200 )
    {
        isErrorFlag = true;
        fieldText += "培训备注|";
        msgText += "培训备注最多只允许输入200个字符|";
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
* 进度安排校验
*/
function CheckScheduleInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //获取表格
    table = document.getElementById("tblScheduleDetailInfo");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var errorRow = 0;
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，校验输入
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            errorRow++;
	        //进度时间
            scheduleDate = document.getElementById("txtScheduleDate_" + i).value;            
            //判断是否输入
            if (scheduleDate == "")
            {
                isErrorFlag = true;
                fieldText += "进度安排 行：" + errorRow + " 进度时间|";
                msgText += "请输入进度时间|";  
            }
            //判断输入是否正确
            else if(!isDate(scheduleDate))
            {
                isErrorFlag = true;
                fieldText += "进度安排 行：" + errorRow + " 进度时间|";
                msgText += "请输入正确的进度时间|";  
            }
	        //内容摘要
            txtAbstract = document.getElementById("txtAbstract_" + i).value;            
            //判断是否输入
            if(txtAbstract == "")
            {
                isErrorFlag = true;
                fieldText += "进度安排 行：" + errorRow + " 内容摘要|";
                msgText += "请输入内容摘要|";  
            }
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
* 保存培训信息
*/
function SaveTrainingInfo()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    //进度安排校验 有错误时，返回
    } else if(CheckScheduleInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    //获取进度安排参数
    postParams += GetScheduleParams();

    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/Training_Edit.ashx",
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
                document.getElementById("hidEditFlag").value = data.info;
                /* 设置编号的显示 */ 
                //显示考核的编号 考核编号DIV可见              
                document.getElementById("divTrainingNo").style.display = "block";
                //设置考核编号
                document.getElementById("divTrainingNo").innerHTML = data.data;
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
        no = document.getElementById("divTrainingNo").innerHTML;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codruleTraining_ddlCodeRule").value;
        //手工输入的时候
        if ("" == codeRule)
        {
            //招聘活动编号
            no = document.getElementById("codruleTraining_txtCode").value;
        }
        else
        {
            //编码规则ID
            param += "&CodeRuleID=" + codeRule;
        }
    }
    //招聘活动编号
    param += "&TrainingNo=" + no;
    //培训名称
    param += "&TrainingName=" + reescape(document.getElementById("txtTrainingName").value);
    //发起时间
    param += "&ApplyDate=" + document.getElementById("txtApplyDate").value;
    //发起人
    param += "&CreateID=" + document.getElementById("txtCreateID").value;
    //项目编号
    param += "&ProjectNo=" + reescape(document.getElementById("txtProjectNo").value);
    //项目名称
    param += "&ProjectName=" + reescape(document.getElementById("txtProjectName").value);
    //培训机构
    param += "&TrainingOrgan=" + reescape(document.getElementById("txtTrainingOrgan").value);
    //预算费用
    param += "&PlanCost=" + reescape(document.getElementById("txtPlanCost").value);
    //培训天数
    param += "&TrainingCount=" + reescape(document.getElementById("txtTrainingCount").value);
    //培训地点 
    param += "&TrainingPlace=" + reescape(document.getElementById("txtTrainingPlace").value);
    //培训方式
    param += "&TrainingWay=" + reescape(document.getElementById("ddlTrainingWay_ddlCodeType").value);
    //培训老师
    param += "&TrainingTeacher=" + reescape(document.getElementById("txtTrainingTeacher").value);
    //开始时间
    param += "&StartDate=" + document.getElementById("txtStartDate").value;
    //结束时间
    param += "&EndDate=" + document.getElementById("txtEndDate").value;
    //考核人
    param += "&CheckPerson=" + reescape(document.getElementById("txtCheckPerson").value);
    //附件
    param += "&Attachment=" + reescape(document.getElementById("hfAttachment").value);
    param += "&PageAttachment=" + reescape(document.getElementById("hfPageAttachment").value);
    param += "&AttachmentName=" + reescape(document.getElementById("spanAttachmentName").innerHTML);
    //目的
    param += "&Goal=" + reescape(document.getElementById("txtGoal").value);
    //参与人员
    param += "&JoinUser=" + document.getElementById("txtJoinUser").value;
    //培训备注
    param += "&TrainingRemark=" + reescape(document.getElementById("txtTrainingRemark").value);
    
    return param;
}

/*
* 招聘目标参数
*/
function GetScheduleParams()
{

    //获取表格
    table = document.getElementById("tblScheduleDetailInfo");
    //获取表格行数
    var tableCount = table.rows.length;
    //定义变量
    var count = 0;
    var param = "";
    //遍历所有行，获取技能信息   
	for (var i = 1 ; i < tableCount ; i++)
	{
	    //不是删除行时，添加信息
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            count++;
            //进度时间
            param += "&ScheduleDate_" + count + "=" + document.getElementById("txtScheduleDate_" + i).value;
	        //内容摘要 
            param += "&ScheduleAbstract_" + count + "=" + reescape(document.getElementById("txtAbstract_" + i).value);
	        //备注
            param += "&Remark_" + count + "=" + reescape(document.getElementById("txtRemark_" + i).value);
	    }
	}
	//table表记录数
    param += "&ScheduleCount=" + count;
    //返回参数信息
    return param;
}

/*
* 返回培训列表
*/
function DoBack()
{
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    window.location.href = "Training_Info.aspx?ModuleID=" + ModuleID + searchCondition;
}

//打印功能
function fnPrintOrder() {
    //if (confirm('只能打印保存后的信息，请确认页面信息是否已保存！')) {
        var OrderNo = $.trim($("#codruleTraining_txtCode").val());
        if (OrderNo == '保存时自动生成' || OrderNo == '') {

            OrderNo = $.trim($("#divTrainingNo").text());
            if (OrderNo == '保存时自动生成' || OrderNo == '') {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存您所填的信息！");
            }
            else {

                window.open('../../../Pages/PrinttingModel/HumanManager/EmployeeTrainingPrint.aspx?no=' + OrderNo);
            }
        }
        else {

            window.open('../../../Pages/PrinttingModel/HumanManager/EmployeeTrainingPrint.aspx?no=' + OrderNo);
        }

    //}
}