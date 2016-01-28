/*
* 获取培训参与人员，并在页面显示
*/
function GetJoinUserInfo()
{
    //数据获取完毕，填充页面据显示
    var table = document.getElementById("tblRsseResultDetail");
    //获取已有的表格行数
    var tableLen = table.rows.length;
    //删除一览中已有的的数据
    for (var i = tableLen - 1; i > 0; i--)
    {
        //删除变更前的培训的参与人员信息
        table.deleteRow(i);
    }
    //获取当前选中的培训编号
    trainingNo = document.getElementById("ddlTraining").value;
    //未选择时，返回
    if (trainingNo == null || trainingNo == "")
    {
        return;
    }
    //设置请求参数
    var postParam = "TrainingNo=" +trainingNo;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/GetJoinUserInfo.ashx?' + postParam,//目标地址
        dataType:"json",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(msg)
        {
            //隐藏提示框
            hidePopup();
            //定义行变量
            var objTR;
            //记录数标识
            var index = 0;
            $.each(msg.data
                ,function(i,item)
                {
                    //当标识为0时，新建一行
                    if (index == 0)
                    {
                        //添加一新行
                        objTR = table.insertRow(-1);
	                    index = 1;
                    }
                    //当标识为1时，在原来行后面添加
                    else 
                    {
	                    index = 0;
                    }
                    //姓名
                    objTD = objTR.insertCell(-1);
                    objTD.className = "tdColInputCenter";
                    objTD.innerHTML = "<input type='hidden' class='tdinput' value='" + item.UserID + "' id='txtJoinUserID_" + i + "'>"
                                        + item.UserName;
                    //考核等分
                    objTD = objTR.insertCell(-1);
                    objTD.className = "tdColInput";
                    objTD.innerHTML = "<input type='text' maxlength = '6' onchange='Number_round(this,2);' width='90%' class='tdinput' id='txtAsseScore_" + i + "'>";
                    //考核等级
                    objTD = objTR.insertCell(-1);
                    objTD.className = "tdColInputCenter";
                    objTD.innerHTML = "<select class='tdinput'id='ddlAsseLevel_" + i + "'>" + document.getElementById("ddlAsseLevel").innerHTML + "</select>";
                }
            );
            //当标识为1时，在原来行后面添加列补全表格
            if (parseInt(index) == 1)
            {
                //姓名
                objTD = objTR.insertCell(-1);
	            objTD.className = "tdColInputCenter";
                objTD.innerHTML = "&nbsp;";
                //考核等分
                objTD = objTR.insertCell(-1);
	            objTD.className = "tdColInputCenter";
                objTD.innerHTML = "&nbsp;";
                //考核等级
                objTD = objTR.insertCell(-1);
	            objTD.className = "tdColInputCenter";
                objTD.innerHTML = "&nbsp;";
            }
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        }
    });
}

function DoPrint()
{     
     var Url = document.getElementById("divTrainingAsseNo").innerHTML;     
     if(Url == "")
     {
       showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存单据再打印！");
        return;
     }
    window.open("../../PrinttingModel/HumanManager/TrainingAssePrint.aspx?id=" + Url);
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
        codeRule = document.getElementById("codruleTrainingAsse_ddlCodeRule").value;
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            trainingNo = document.getElementById("codruleTrainingAsse_txtCode").value;
            //编号必须输入
            if (trainingNo == "")
            {
                isErrorFlag = true;
                fieldText += "考核编号|";
   		        msgText += "请输入考核编号|";
            }
            else
            {
                if (!CodeCheck(trainingNo))
                {
                    isErrorFlag = true;
                    fieldText += "考核编号|";
                    msgText += "考核编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
    
    //获取当前选中的培训编号
    trainingNo = document.getElementById("ddlTraining").value;
    if (trainingNo == "" || trainingNo == null)
    {
        isErrorFlag = true;
        fieldText += "培训编号|";
        msgText += "请选择培训编号|";  
    }
        
    //考核人必须输入
    if (document.getElementById("UserCheckPerson").value == "")
    {
        isErrorFlag = true;
        fieldText += "考核人|";
        msgText += "请输入考核人|";  
    }
    //考核方式必须输入
    if (document.getElementById("txtCheckWay").value == "")
    {
        isErrorFlag = true;
        fieldText += "考核方式|";
        msgText += "请输入考核方式|";  
    }

     var RetVal=CheckSpecialWords();
     var CheckWay = document.getElementById("txtCheckWay").value;
     if(CheckWay != "")
    {
        if(!CheckSpecialWord(CheckWay))
        {
            isErrorFlag = true;
            fieldText += "考核方式|";
            msgText += "考核方式不能含有特殊字符|";
        }
    }
    //考核时间
    checkDate = document.getElementById("txtCheckDate").value;
    if (checkDate == "")
    {
        isErrorFlag = true;
        fieldText += "考核时间|";
        msgText += "请输入考核时间|";  
    }
    else if (!isDate(checkDate))
    {
        isErrorFlag = true;
        fieldText += "考核时间|";
        msgText += "请输入正确的考核时间|";  
    }
    else
    {
        //获取当前日期
        today = document.getElementById("hidTodayDate").value;
        //比较考核时间和当前日期
        if (CompareDate(checkDate, today) == 1)
        {
            isErrorFlag = true;
            fieldText += "考核时间|";
            msgText += "您输入的考核时间晚于当前日期|";  
        }
    }
    //培训规划
    var TrainingPlan = document.getElementById("txtTrainingPlan").value;
    if(TrainingPlan != "" && strlen(TrainingPlan) > 1000)
    {
        isErrorFlag = true;
        fieldText += "培训规划|";
        msgText += "培训规划最多只允许输入1000个字符|";  
    }
    //领导意见
    var LeadViews = document.getElementById("txtLeadViews").value;
    if(LeadViews != "" && strlen(LeadViews) > 1000)
    {
        isErrorFlag = true;
        fieldText += "领导意见|";
        msgText += "领导意见最多只允许输入1000个字符|";  
    }
    //说明
    var Description = document.getElementById("txtDescription").value;
    if(Description != "" && strlen(Description) > 1000)
    {
        isErrorFlag = true;
        fieldText += "说明|";
        msgText += "说明最多只允许输入1000个字符|";  
    }
    //考核总评
    generalComment = document.getElementById("txtGeneralComment").value;
    if (generalComment != "" && strlen(generalComment) > 1000)
    {
        isErrorFlag = true;
        fieldText += "考核总评|";
        msgText += "考核总评最多只允许输入1000个字符|";  
    }
    //考核备注
    checkRemark = document.getElementById("txtCheckRemark").value;
    if (checkRemark != "" && strlen(checkRemark) > 500)
    {
        isErrorFlag = true;
        fieldText += "考核备注|";
        msgText += "考核备注最多只允许输入500个字符|";  
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
* 考核结果校验
*/
function CheckResultInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;    
    
    //获取表格
    table = document.getElementById("tblRsseResultDetail");
    //获取表格行数
    var tableCount = table.rows.length;
    /*
    * 因为参与者必须输入，所以该表格行数肯定大于等于2，因此未对 tableCount - 1 进行判断
    * 表格的最后一行可能只有一条记录，第二条是否是参与人员再作判断
    */
    var count = (tableCount - 1) * 2;
    //判断最后一行的第二条记录是否存在
    lastOne = document.getElementById("txtJoinUserID_" + (count - 1));
    if (lastOne == null || typeof(lastOne) == "undefined")
    {
        //不存在时，将记录总数减一
        count = parseInt(count) - 1;
    }
    var param = "";
    //遍历所有行，获取考核结果
	for (var i = 0 ; i < count ; i++)
	{
        //考核得分
        asseScore = document.getElementById("txtAsseScore_" + i).value; 
        //判断输入是否正确
        if(asseScore != null && asseScore!= "" &&!IsNumeric(asseScore, 3, 2))
        {
            isErrorFlag = true;
            fieldText += "考核结果 行：" + Math.round(((i + 1)/2)) + " 考核得分|";
            msgText += "请输入正确的考核得分|";  
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
* 保存培训考核信息
*/
function SaveTrainingRessInfo()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    //考核结果校验 有错误时，返回
    } else if(CheckResultInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    //获取考核结果参数
    postParams += GetResultParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/TrainingAsse_Edit.ashx?" + postParams,
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
                //设置编辑模式
                document.getElementById("hidEditFlag").value = data.info;
                /* 设置编号的显示 */ 
                //显示考核的编号 考核编号DIV可见              
                document.getElementById("divTrainingAsseNo").style.display = "block";
                //设置考核编号
                document.getElementById("divTrainingAsseNo").innerHTML = data.data;
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
* 基本信息参数
*/
function GetBaseInfoParams()
{
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value;
    //设置提交参数
    var param = "EditFlag=" + editFlag;
    var trainingNo = "";
    //新建时，编号选择手工输入时
    if ("INSERT" != editFlag)
    {
        //考核编号
        trainingNo = document.getElementById("divTrainingAsseNo").innerHTML;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codruleTrainingAsse_ddlCodeRule").value;
        //手工输入的时候
        if ("" == codeRule)
        {
            //招聘活动编号
            trainingNo = document.getElementById("codruleTrainingAsse_txtCode").value;
        }
        else
        {
            //编码规则ID
            param += "&CodeRuleID=" + codeRule;
        }
    }
    //考核编号
    param += "&TrainingAsseNo=" + trainingNo;
    
    //培训编号
    param += "&TrainingNo=" + document.getElementById("ddlTraining").value;
    //考核人
    param += "&CheckPerson=" + reescape(document.getElementById("UserCheckPerson").value);
    //填写人
    param += "&FillUserID=" + document.getElementById("hidFillUserID").value;
    //考核方式 
    //param += "&CheckWay=" + reescape(document.getElementById("ddlCheckWay_ddlCodeType").value);
    param += "&CheckWay=" + reescape(document.getElementById("txtCheckWay").value);
    //考核时间
    param += "&CheckDate=" + document.getElementById("txtCheckDate").value;
    //培训规划
    param += "&TrainingPlan=" + reescape(document.getElementById("txtTrainingPlan").value);
    //领导意见
    param += "&LeadViews=" + reescape(document.getElementById("txtLeadViews").value);
    //说明
    param += "&Description=" + reescape(document.getElementById("txtDescription").value);
    //考核总评
    param += "&GeneralComment=" + reescape(document.getElementById("txtGeneralComment").value);
    //考核备注 
    param += "&CheckRemark=" + reescape(document.getElementById("txtCheckRemark").value);
    
    return param;
}

/*
* 考核结果参数
*/
function GetResultParams()
{
    //获取表格
    table = document.getElementById("tblRsseResultDetail");
    //获取表格行数
    var tableCount = table.rows.length;
    /*
    * 因为参与者必须输入，所以该表格行数肯定大于等于2，因此未对 tableCount - 1 进行判断
    * 表格的最后一行可能只有一条记录，第二条是否是参与人员再作判断
    */
    var count = (tableCount - 1) * 2;
    //判断最后一行的第二条记录是否存在
    lastOne = document.getElementById("txtJoinUserID_" + (count - 1));
    if (lastOne == null || typeof(lastOne) == "undefined")
    {
        //不存在时，将记录总数减一
        count = parseInt(count) - 1;
    }
    var param = "";
    //遍历所有行，获取技能信息
	for (var i = 0 ; i < count ; i++)
	{
        //参与人ID
        param += "&JoinUserID_" + i + "=" + document.getElementById("txtJoinUserID_" + i).value;
        //考核得分 
        param += "&AsseScore_" + i + "=" + document.getElementById("txtAsseScore_" + i).value;
        //考核等级
        param += "&AsseLevel_" + i + "=" + document.getElementById("ddlAsseLevel_" + i).value;
	}
	//参与人员记录数
    param += "&ResultCount=" + count;
    //返回参数信息
    return param;
}

/*
* 返回培训考核列表
*/
function DoBack()
{
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    from = document.getElementById("hidFrom").value;
    //培训列表链接过来时
    if ("1" == from)
    {
        //获取模块功能ID
        var ModuleIDTraining = document.getElementById("hidModuleIDTraining").value;
        window.location.href = "Training_Info.aspx?ModuleID=" + ModuleIDTraining + searchCondition;
    }
    //培训考核列表链接过来时
    else
    {
        //获取模块功能ID
        var ModuleID = document.getElementById("hidModuleID").value;
        window.location.href = "TrainingAsse_Info.aspx?ModuleID=" + ModuleID + searchCondition;
    }
}