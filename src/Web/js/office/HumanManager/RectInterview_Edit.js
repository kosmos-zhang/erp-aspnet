
$(document).ready(function(){
if (document.getElementById ("hidIsSearch").value=="1")
{
 InitQuterInfo()
//InitTemplateInfo();
}
});

/*
* 获取岗位列表
*/
function InitQuterInfo()
{ //获取当前选中的应聘岗位
    quarter = document.getElementById("ddlRectPlan").value.Trim();
    if (quarter =="" || quarter ==null || quarter =="undefine")
    {
     quarter ="Special";
      
    }
    //设置请求参数
    var postParam = "Action=InitQuter&QuarterID=" + quarter;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/RectInterview_Edit.ashx',
        data : postParam,//目标地址
        dataType:"string",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
        },//发送数据之前
        success: function(msg)
        {
            //隐藏提示框
            hidePopup();
            //获取模板信息
            document.getElementById("divQuarter").innerHTML = "<select class='tdinput'id='ddlQuarter' onchange='InitTemplateInfo();'>" + msg + "</select>";
            //设置要素
            InitTemplateInfo();
          //  InitInterviewElem();
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        }
    });
}





/*
* 添加简历
*/
function AddEmployee()
{
    var ModuleID = document.getElementById("hidEmployeeModuleID").value;
    window.location.href = "EmployeeInfo_Add.aspx?FromPage=4&ModuleID=" + ModuleID;
}
/*
* 选择人才储备
*/
function DoSelectReserve()
{
    
    //设置div的位置
    var addGoalFromApply ="";
    // 整个div的大小和位子
    addGoalFromApply += "<div id='divSelectReserve' style='width:800;height:580;background-color:white;z-index:11;position:absolute;left:100;top:100;overflow-y:scroll;'>";
    // 白色div中的信息
    addGoalFromApply += "<table cellpadding='0' cellspacing='1' border='0' align=left>";
    addGoalFromApply += "<tr><td>";
    addGoalFromApply += "<iframe src='SelectReserveInfo.aspx' scrolling=no width='780' height='560' frameborder='0'></iframe>";
    addGoalFromApply += "</td></tr>";
    addGoalFromApply += "</table>";
    //--end
    addGoalFromApply += "</div>";
    document.body.insertAdjacentHTML("afterBegin", addGoalFromApply);
}

/*
* 设置选择的人才储备信息
*/
function SetReserveInfo(userID, userName)
{
    document.getElementById("divSelectReserve").style.display = "none";
    if (userID == null || userID == "") return;
    document.getElementById("hidStaffName").value = userID;
    document.getElementById("txtStaffName").value = userName;
}

/*
* 附件处理
*/
function DealAttachment(flag)
{
    //flag未设置时，返回不处理
    if (typeof(flag) == "undefined" || flag == "" || flag == null)
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
        document.getElementById("spanAttachmentName").innerHTML = docName;
        //下载删除显示
        document.getElementById("divDealAttachment").style.display = "block";
        //上传不显示
        document.getElementById("divUploadAttachment").style.display = "none";
    }
}

/*
* 获取面试模板
*/
function InitTemplateInfo()
{
//    planNo = document.getElementById("ddlRectPlan").value;
//    if (planNo =="" || planNo ==null || planNo =="undefine")
//    {
//     return ;
//    }
    
 //获取当前选中的应聘岗位
    quarter = document.getElementById("ddlQuarter").value.Trim();
    if (quarter =="" || quarter ==null || quarter =="undefine")
    {
    
         document.getElementById("divTemplate").innerHTML = "<select class='tdinput'id='ddlTemplate' onchange='InitInterviewElem();'>" + "" + "</select>";
         table = document.getElementById("tblElemScoreDetail");
            //获取已有的表格行数
            tableLen = table.rows.length;
            //删除一览中已有的的数据
            for (var i = tableLen - 1; i > 0; i--)
            {
                //删除变更前的培训的参与人员信息
                table.deleteRow(i);
            }
     return ;
    }
    //设置请求参数
    var postParam = "Action=InitTemplate&QuarterID=" + quarter;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/RectInterview_Edit.ashx',
        data : postParam,//目标地址
        dataType:"string",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
        },//发送数据之前
        success: function(msg)
        {
            //隐藏提示框
            hidePopup();
            //获取模板信息
            document.getElementById("divTemplate").innerHTML = "<select class='tdinput'id='ddlTemplate' onchange='InitInterviewElem();'>" + msg + "</select>";
            //设置要素
            InitInterviewElem();
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        }
    });
}

/*
* 获取面试模板要素，并在页面显示
*/
function InitInterviewElem()
{
    //获取当前选中的应聘岗位
    template = document.getElementById("ddlTemplate").value.Trim();
    //设置请求参数
    var postParam = "Action=InitElem&TemplateNo=" + template;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/RectInterview_Edit.ashx',
        data : postParam,//目标地址
        dataType:"json",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
        },//发送数据之前
        success: function(msg)
        {
            //隐藏提示框
            hidePopup();
            //数据获取完毕，填充页面据显示
            table = document.getElementById("tblElemScoreDetail");
            //获取已有的表格行数
            tableLen = table.rows.length;
            //删除一览中已有的的数据
            for (var i = tableLen - 1; i > 0; i--)
            {
                //删除变更前的培训的参与人员信息
                table.deleteRow(i);
            }
            $.each(msg.data
                ,function(i,item)
                {
                    //定义行变量
                    objTR = table.insertRow(-1);
                    //要素名称
                    objTD = objTR.insertCell(-1);
                    objTD.className = "tdColInput";
                    objTD.innerHTML = "<input type='hidden' class='tdinput' value='" + item.ElemID + "' id='txtElemID_" + (i + 1) + "'>"
                                        + item.ElemName;
                    //满分
                    objTD = objTR.insertCell(-1);
                    objTD.className = "tdColInputCenter";
                    objTD.id = "tdMaxScore_" + (i + 1);
                    objTD.innerHTML = TrimRightZero(item.MaxScore);
                    //权重
                    objTD = objTR.insertCell(-1);
                    objTD.className = "tdColInputCenter";
                    objTD.id = "tdRate_" + (i + 1);
                    objTD.innerHTML = TrimRightZero(item.Rate);
                    //面试得分
                    objTD = objTR.insertCell(-1);
                    objTD.className = "tdColInput";
                    objTD.innerHTML = "<input type='text' maxlength = '6' style= 'width:95%;' onchange='Number_round(this,\"2\");'   onblur='CalculateTestScore(this);' class='tdinput' id='txtRealScore_" + (i + 1) + "'>";
                    //备注
                    objTD = objTR.insertCell(-1);
                    objTD.className = "tdColInput";
                    objTD.innerHTML = "<input type='text' maxlength = '250' style= 'width:95%;' class='tdinput' id='txtScoreRemark_" + (i + 1) + "'>";
                }
            );
            
            //定义行变量
            objTR = table.insertRow(-1);
            //面试总成绩
            objTD = objTR.insertCell(-1);
            objTD.colSpan = 5;
            objTD.align = "right";
            objTD.className = "tdColInput";
            objTD.innerHTML = "<table><tr><td>面试总成绩：</td><td><div id='divTestScore' style='width:100px;text-align:right;'>0.00</div></td></tr></table>";
            
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        }
    });
}

/*
* 数字后面的零
* strNumeric   需要处理的数字
*/
function TrimRightZero(strNumeric)
{
    //参数为空时，返回
    if (strNumeric == null || strNumeric == "") return strNumeric;
    //判断是否含有小数点
    tempNumber = strNumeric.split(".");
    if (tempNumber.length == 1) return strNumeric;
    //获取
    strFloat = tempNumber[1];
    for(var i = strFloat.length; i >= 0; i--)
    {
        strTemp = strFloat.substring(strFloat.length - 1, strFloat.length);
        if (strTemp == "0") strFloat = strFloat.substring(0, strFloat.length - 1);
        else break;
    }
    if (strFloat == "" || strFloat.length == 0) return tempNumber[0];
    else return tempNumber[0] + "." + strFloat;
}

/*
* 计算面试总成绩
*/
function CalculateTestScore(obj)
{ 
    //获取输入的值
    inputValue = obj.value;
    //判断输入的小数点
    var arr = inputValue.split(".");
    if (arr.length <= 2)
    {
        index = inputValue.indexOf(".");
        //输入的第一位是小数点时，前面加0
        if (index == 0)
        {
            obj.value = "0" + inputValue;
        }
        //最后一位是小数点时
        else if (index == inputValue.length - 1)
        {
            obj.value = inputValue.replace(".", "");
        }
        //获取表格
        table = document.getElementById("tblElemScoreDetail");
        //获取表格行数
        var count = table.rows.length;
        //定义变量
        var testScore = 0;
        //遍历所有行，获取技能信息
	    for (var i = 1 ; i < count - 1; i++)
	    {
            //面试得分
            elemScore = document.getElementById("txtRealScore_" + i).value.Trim();
            //权重
            rate = document.getElementById("tdRate_" + i).innerHTML;
            //面试得分 和 权重都输入时，自动计算       
            if (elemScore != null && elemScore != "" && rate != null && rate != "")
            {
                //计算面试总成绩
                testScore += (parseFloat(elemScore) * parseFloat(rate))/100;
            }
            
	    }
	    //设置面试总成绩
	    /* 未考虑 NaN的情况 有时间再做 */
	    document.getElementById("divTestScore").innerHTML = parseInt(testScore * 100 + 0.5) / 100;
    }
}

/*
* 保存按钮
*/
function DoSave()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    }
    //要素得分校验 有错误时，返回
    else if(CheckElemScoreInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetPostParams();
    //获取要素得分参数信息
    postParams += GetElemScoreParams();
    //获取
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/RectInterview_Edit.ashx",
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
                document.getElementById("hidInterviewID").value = data.info;
                document.getElementById("hidBillNo").value = data.data;
               
                /* 设置编号的显示 */ 
                //显示编号            
                document.getElementById("divCodeNo").style.display = "block";
                //设置编号
                document.getElementById("divCodeNo").innerHTML = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","您输入的编号已经存在！");
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
    interviewID = document.getElementById("hidInterviewID").value.Trim();
    //新建时，编号选择手工输入时
    if (interviewID == "" || interviewID == null)
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("codeRule_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            codeNo = document.getElementById("codeRule_txtCode").value.Trim();
            //编号必须输入
            if (codeNo == "")
            {
                isErrorFlag = true;
                fieldText += "记录编号|";
   		        msgText += "请输入记录编号|";
            }
            else
            {
                if (!CodeCheck(codeNo))
                {
                    isErrorFlag = true;
                    fieldText += "记录编号|";
                    msgText += "记录编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
    
    //姓名必须输入
    staffName = document.getElementById("hidStaffName").value.Trim();
    if (staffName == "" || staffName == null)
    {
        isErrorFlag = true;
        fieldText += "姓名|";
        msgText += "请输入姓名|";  
    }
    //应聘岗位必须输入
    quarter = document.getElementById("ddlQuarter").value.Trim();
    if (quarter == "" || quarter == null)
    {
        isErrorFlag = true;
        fieldText += "应聘岗位|";
        msgText += "请输入应聘岗位|";  
    }
    //面试日期必须输入
    interviewDate = document.getElementById("txtInterviewDate").value.Trim();
    if (interviewDate == "" || interviewDate == null)
    {
        isErrorFlag = true;
        fieldText += "初试日期|";
        msgText += "请输入初试日期|";  
    }

    
    
    //初试地点 
    InterviewPlace = document.getElementById("txtInterviewPlace").value.Trim();
    if (InterviewPlace != "" && InterviewPlace != null && strlen(InterviewPlace) > 100)
    {
        isErrorFlag = true;
        fieldText += "初试地点|";
        msgText += "初试地点最多只允许输入100个字符！|";  
    }
        //可提供的待遇
    OurSalary = document.getElementById("txtOurSalary").value.Trim();
    if (OurSalary != "" && OurSalary != null && strlen(OurSalary) > 600)
    {
        isErrorFlag = true;
        fieldText += "可提供的待遇|";
        msgText += "可提供的待遇最多只允许输入600个字符！|";  
    }
            //确认工资
    FinalSalary = document.getElementById("txtFinalSalary").value.Trim();
    if (FinalSalary != "" && FinalSalary != null && strlen(FinalSalary) > 600)
    {
        isErrorFlag = true;
        fieldText += "确认工资|";
        msgText += "确认工资最多只允许输入600个字符！|";  
    }
              //其他方面
    OtherNote = document.getElementById("txtOtherNote").value.Trim();
    if (OtherNote != "" && OtherNote != null && strlen(OtherNote) > 600)
    {
        isErrorFlag = true;
        fieldText += "其他方面|";
        msgText += "其他方面最多只允许输入600个字符！|";  
    }
        //初试人员意见 
    InterviewNote = document.getElementById("txtInterviewNote").value.Trim();
    if (InterviewNote != "" && InterviewNote != null && strlen(InterviewNote) > 600)
    {
        isErrorFlag = true;
        fieldText += "初试人员意见|";
        msgText += "试人员最多只允许输入600个字符！|";  
    }
        InterviewNote = document.getElementById("txtInterviewNote").value.Trim();
    if (InterviewNote != "" && InterviewNote != null && strlen(InterviewNote) > 600)
    {
        isErrorFlag = true;
        fieldText += "初试人员意见|";
        msgText += "初试人员意见最多只允许输入600个字符！|";  
    }
        CheckPlace = document.getElementById("txtCheckPlace").value.Trim();
    if (CheckPlace != "" && CheckPlace != null && strlen(CheckPlace) > 100)
    {
        isErrorFlag = true;
        fieldText += "复试地点|";
        msgText += "复试地点最多只允许输入100个字符！|";  
    }
     ManNote = document.getElementById("txtManNote").value.Trim();
    if (ManNote != "" && ManNote != null && strlen(ManNote) > 600)
    {
        isErrorFlag = true;
        fieldText += "综合素质|";
        msgText += "综合素质最多只允许输入600个字符！|";  
    }
      KnowNote = document.getElementById("txtKnowNote").value.Trim();
    if (KnowNote != "" && KnowNote != null && strlen(KnowNote) > 600)
    {
        isErrorFlag = true;
        fieldText += "专业知识|";
        msgText += "专业知识最多只允许输入600个字符！|";  
    }
     WorkNote = document.getElementById("txtWorkNote").value.Trim();
    if (WorkNote != "" && WorkNote != null && strlen(WorkNote) > 600)
    {
        isErrorFlag = true;
        fieldText += "工作经验|";
        msgText += "工作经验最多只允许输入600个字符！|";  
    }
        SalaryNote = document.getElementById("txtSalaryNote").value.Trim();
    if (SalaryNote != "" && SalaryNote != null && strlen(SalaryNote) > 600)
    {
        isErrorFlag = true;
        fieldText += "要求待遇|";
        msgText += "要求待遇最多只允许输入600个字符！|";  
    }   
       Remark = document.getElementById("txtRemark").value.Trim();
    if (Remark != "" && Remark != null && strlen(Remark) > 500)
    {
        isErrorFlag = true;
        fieldText += "备注|";
        msgText += " 备注最多只允许输入500个字符！|";  
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
* 要素得分校验
*/
function CheckElemScoreInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;    
    
    //获取表格
    table = document.getElementById("tblElemScoreDetail");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count - 1; i++)
	{
	
         sRemark=document.getElementById("txtScoreRemark_" + i).value.Trim();
                 if (sRemark != "" && sRemark != null && strlen(sRemark) > 500)
          {
                 isErrorFlag = true;
                 fieldText += "评测记录 行：" + i + " 备注|";
                  msgText += "评测备注最多只允许输入500个字符！|";  
           }       
        //面试得分
        elemScore = document.getElementById("txtRealScore_" + i).value.Trim();            
        //判断是否输入
        if(elemScore == "" || elemScore == null)
        {
//            isErrorFlag = true;
//            fieldText += "评测记录 行：" + i + " 面试得分|";
//            msgText += "请输入面试得分|";  
        }
        //判断是否为数字
        else if (!IsNumeric(elemScore, 3, 2))
        {
            isErrorFlag = true;
            fieldText += "评测记录 行：" + i + " 面试得分|";
            msgText += "请输入正确的面试得分|";  
        }
        else
        {
           //满分
           maxScore = document.getElementById("tdMaxScore_" + i).innerHTML;
           if(parseFloat(maxScore) < parseFloat(elemScore))
           {
                isErrorFlag = true;
                fieldText += "评测记录 行：" + i + " 面试得分|";
                msgText += "您输入的面试得分超过了满分|";  
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
* 获取提交的基本信息
*/
function GetPostParams()
{
    //获取面试记录ID  
    interviewID = document.getElementById("hidInterviewID").value.Trim();
    //设置请求参数
    var strParams = "Action=SaveInfo&InterviewID=" + interviewID;
    var no = "";
    //更新的时候
    if (interviewID !=null && interviewID != "")
    {
        //编号
        no = document.getElementById("divCodeNo").innerHTML;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codeRule_ddlCodeRule").value.Trim();
        //手工输入的时候
        if ("" == codeRule)
        {
            //编号
            no = document.getElementById("codeRule_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            strParams += "&CodeRuleID=" + document.getElementById("codeRule_ddlCodeRule").value.Trim();
        }
    }
    //编号
    strParams += "&InterviewNo=" + no;
    //招聘活动
    strParams += "&PlanID=" + escape(document.getElementById("ddlRectPlan").value.Trim());
    //面试者
    strParams += "&StaffName=" + escape(document.getElementById("hidStaffName").value.Trim());
    //招聘方式
        strParams += "&RectType=" + escape(document.getElementById("ddlRectType").value.Trim());
    //应聘岗位
    strParams += "&QuarterID=" + escape(document.getElementById("ddlQuarter").value.Trim());
    //面试模板
    strParams += "&TemplateNo=" + escape(document.getElementById("ddlTemplate").value.Trim());
    //初试日期
    strParams += "&InterviewDate=" + escape(document.getElementById("txtInterviewDate").value.Trim());
    //初试面试方式
    strParams += "&InterviewType=" + escape(document.getElementById("ddlInterviewType_ddlCodeType").value.Trim());
    //初试面试地点
    strParams += "&InterviewPlace=" + escape(document.getElementById("txtInterviewPlace").value.Trim());
    //初试人员意见
    strParams += "&InterviewNote=" + escape(document.getElementById("txtInterviewNote").value.Trim());
    
       //初试结果
    strParams += "&InterviewResult=" + escape(document.getElementById("ddlInterviewResult").value.Trim());

    
        //复试日期
    strParams += "&CheckDate=" + escape(document.getElementById("txtCheckDate").value.Trim());
    //复试方式
    strParams += "&CheckType=" + escape(document.getElementById("ddlCheckType_ddlCodeType").value.Trim());
    //复试地点
    strParams += "&CheckPlace=" + escape(document.getElementById("txtCheckPlace").value.Trim());
    //复试人员意见
    strParams += "&CheckNote=" + escape(document.getElementById("txtCheckNote").value.Trim());
       //复试结果
    strParams += "&FinalResult=" + escape(document.getElementById("ddlFinalResult").value.Trim());
    
    //综合素质
        strParams += "&ManNote=" +escape( document.getElementById("txtManNote").value.Trim());
    //专业知识
    strParams += "&KnowNote=" + escape(document.getElementById("txtKnowNote").value.Trim());
    //工作经验
    strParams += "&WorkNote=" + escape(document.getElementById("txtWorkNote").value.Trim());
    //要求待遇
    strParams += "&SalaryNote=" + escape(document.getElementById("txtSalaryNote").value.Trim());


    //可提供的待遇
    strParams += "&OurSalary=" + escape(document.getElementById("txtOurSalary").value.Trim());
    //确认工资
    strParams += "&FinalSalary=" + escape(document.getElementById("txtFinalSalary").value.Trim());
    //其他方面
    strParams += "&OtherNote=" + escape(document.getElementById("txtOtherNote").value.Trim());
    
    
//最终结果
if (document.getElementById("divTestScore"))
{
    strParams += "&TestScore=" + escape(document.getElementById("divTestScore").innerHTML);
    }
    
    //附件
    strParams += "&Attachment=" + escape(document.getElementById("hfAttachment").value.Trim());
    strParams += "&PageAttachment=" + escape(document.getElementById("hfPageAttachment").value.Trim());
    strParams += "&AttachmentName=" + escape(document.getElementById("spanAttachmentName").innerHTML);    
    //复试备注
    strParams += "&Remark=" + escape(document.getElementById("txtRemark").value.Trim());
    return strParams;
}

/*
* 要素得分校验
*/
function GetElemScoreParams()
{    
    //获取表格
    table = document.getElementById("tblElemScoreDetail");
    //获取表格行数
    var count = table.rows.length;
    var param = "";
    //要素得分数据存在时
    if (count > 1)
    {
        //设置要素得分 
        param += "&ScoreCount=" + (count - 2);
        //遍历所有行，获取要素得分信息
	    for (var i = 1 ; i < count - 1; i++)
	    {
	        //要素ID
            param += "&ElemScoreID_" + i + "=" + escape ( document.getElementById("txtElemID_" + i).value.Trim());
            //面试得分
            param += "&RealScore_" + i + "=" +escape (  document.getElementById("txtRealScore_" + i).value.Trim());
            //备注
            param += "&ScoreRemark_" + i + "=" + escape ( document.getElementById("txtScoreRemark_" + i).value.Trim());
	    }
    }

    return param;
}

/*
* 返回按钮 
*/
function DoBack()
{

 if(document.getElementById("hidIsliebiao").value.Trim() == "")
    {
        window.location.href='../../../Desktop.aspx';
    }
    else
    {

    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    window.location.href = "RectInterview_Info.aspx?ModuleID=" + ModuleID + searchCondition;
    }
}

/*
* 打印按钮 
*/
function DoPrint()
{
    alert("该功能还未完成");
}