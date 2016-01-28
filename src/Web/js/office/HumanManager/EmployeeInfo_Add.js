/*
* 添加工作履历
*/
function AddWorkHistory(){
    //获取表格
    table = document.getElementById("tblWorkHistory");
    //获取行号
    var no = table.rows.length;
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='checkbox' id='tblWorkHistory_chkSelect_" + no + "'>";
	//开始时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '10' readonly class='tdinput' id='txtWorkStart_" + no + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtWorkStart_" + no + "')})\">";
	//结束时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '10' readonly class='tdinput' id='txtWorkEnd_" + no + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtWorkEnd_" + no + "')})\">";
	//工作单位
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '50' class='tdinput' id='txtWorkCompany_" + no + "'>";
	//所在部门
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '50' class='tdinput' id='txtWorkDept_" + no + "'>";
	//工作内容
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '50' class='tdinput' id='txtWorkContent_" + no + "'>";
	//离职原因
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '50' class='tdinput' id='txtLeaveReason_" + no + "'>";

}

function DoPrint()
{
     var Url = document.getElementById("divEmployeeNo").innerHTML;
     var ID = document.getElementById("hidEmployeeID").value;
     if(Url == "")
     {
        popMsgObj.Show("打印|","请保存单据再打印|");
        return;
     }
    window.open("../../PrinttingModel/HumanManager/EmployeeInfoPrint.aspx?No=" + Url +"&ID="+ID);
}

/*
* 删除表格行
*/
function DeleteRows(tableName){
    //获取表格
    table = document.getElementById(tableName);
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById(tableName + "_chkSelect_" + row);
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
function SelectAll(tableName){
    //获取表格
    table = document.getElementById(tableName);
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	var isSelectAll = document.getElementById(tableName + "_chkAll").checked;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById(tableName + "_chkSelect_" + row);
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
* 添加学习履历
*/
function AddStudyHistory(){
    //获取表格
    table = document.getElementById("tblStudyHistory");
    //获取行号
    var no = table.rows.length;
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='checkbox' id='tblStudyHistory_chkSelect_" + no + "'>";
	//开始时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '10' readonly class='tdinput' id='txtStudyStart_" + no + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStudyStart_" + no + "')})\">";
	//结束时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '10' readonly class='tdinput' id='txtStudyEnd_" + no + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStudyEnd_" + no + "')})\">";
	//学校名称
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.width = "25%";
	objTD.innerHTML = "<input style='width:99%' type='text' maxlength = '50' class='tdinput' id='txtSchoolName_" + no + "'>";
	//专业
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<select class='tdinput'id='ddlProfessional_" + no + "'>" + document.getElementById("ddlProfessional_ddlCodeType").innerHTML + "</select>";
	//学历
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<select class='tdinput'id='ddlCultureLevel_" + no + "'>" + document.getElementById("ddlCulture_ddlCodeType").innerHTML + "</select>";
}

/*
* 添加技能及证照
*/
function AddSkill(){
    //获取表格
    table = document.getElementById("tblSkill");
    //获取行号
    var no = table.rows.length;
    
    
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.width="5%";	
	objTD.innerHTML = "<input  type='checkbox' id='tblSkill_chkSelect_" + no + "'>";
	//技能名称
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.width="15%";
	objTD.innerHTML = "<input style='width:95%' type='text' maxlength = '50' size='10' class='tdinput' id='txtSkillName_" + no + "'>";
	//证件名称
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.width="15%";
	objTD.innerHTML = "<input style='width:95%' type='text' maxlength = '50' size='10' class='tdinput' id='txtCertificateName_" + no + "'>";
	//证件编号
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.width="12%";
	objTD.innerHTML = "<input style='width:95%' type='text' maxlength = '25' size='10' class='tdinput' id='txtCertificateNo_" + no + "'>";
	//证件等级
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.width="12%";
	objTD.innerHTML = "<input style='width:95%' type='text' maxlength = '25' size='10' class='tdinput' id='txtCertificateLevel_" + no + "'>";
	//发证单位
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.width="15%";
	objTD.innerHTML = "<input style='width:95%' type='text' maxlength = '50' size='10' class='tdinput' id='txtIssueCompany_" + no + "'>";
	//发证时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '10' size='8' readonly class='tdinput' id='txtIssueDate_" + no + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtIssueDate_" + no + "')})\">";
	//有效期
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.width="12%";
	objTD.innerHTML = "<input style='width:95%' type='text' maxlength = '25'size='8' class='tdinput' id='txtValidity_" + no + "'>";

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
        codeRule = document.getElementById("codruleEmployNo_ddlCodeRule").value;
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            employeeNo = document.getElementById("codruleEmployNo_txtCode").value;
            //编号必须输入
            if (employeeNo == "")
            {
                isErrorFlag = true;
                fieldText += "编号|";
   		        msgText += "请输入编号|";
            } 
            else
            {
                if (!CodeCheck(employeeNo))
                {
                    isErrorFlag = true;
                    fieldText += "编号|";
                    msgText += "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
    //姓名
    if (document.getElementById("txtEmployeeName").value == "")
    {
        isErrorFlag = true;
        fieldText += "姓名|";
        msgText += "请输入姓名|";
    }
    var RetVal=CheckSpecialWords();    
    if(RetVal!="")
    {
       isErrorFlag = true;
        fieldText = fieldText + RetVal+"|";
	    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    
    //英文名
    nameEn = document.getElementById("txtNameEn").value.replace(/ /g,"");   
    if (nameEn != "" && !IsLetterStr(nameEn))
    {    
         isErrorFlag = true;
        fieldText += "英文名|";
        msgText += "请输入正确的英文名|";
    }
    //获取分类
    flag = document.getElementById("ddlFlag").value;
//    //人才储备时
//    if ("2" == flag)
//    {
//        if (document.getElementById("txtPositionTitle").value == "")
//        {
//            isErrorFlag = true;
//            fieldText += "应聘职务|";
//            msgText += "请输入应聘职务|";
//        }
//    }
    //在职人员和离职人员时
//    else
//    {
//        //工号必须输入
//        if (document.getElementById("txtEmployeeNum").value == "")
//        {
//            isErrorFlag = true;
//            fieldText += "工号|";
//            msgText += "请输入工号|";
//        }
//    }
    //工号
    emplNum = document.getElementById("txtEmployeeNum").value;
    if (emplNum != "" && isnumberorLetters(emplNum))
    {
        isErrorFlag = true;
        fieldText += "工号|";
        msgText += "工号只能包含字母或数字！|";
    }
//    //身份证不为空时，判断身份证是否合法
//    if (document.getElementById("txtCardID").value != "")
//    {
//        cardID = document.getElementById("txtCardID").value;
//        if (cardID.length != 15 && cardID.length != 18)
//        {
//            isErrorFlag = true;
//            fieldText += "身份证|";
//            msgText += "请输入正确的身份证|";
//        }
//    }
    
    //社保卡号不为空时，判断身份证是否合法
    SafeguardCard = document.getElementById("txtSafeguardCard").value;
     if (SafeguardCard != "" && isnumberorLetters(SafeguardCard))
    {
        isErrorFlag = true;
        fieldText += "社保卡号|";
        msgText += "社保卡号只能包含字母或数字！|";
    }
    
    //出身日期不为空时，判断出身日期是否正确
    var inputBirth = document.getElementById("txtBirth").value;
    if (inputBirth != "")
    {
        //获取当前系统日期
        systeDate  = document.getElementById("hidSysteDate").value;
        if (CompareDate(inputBirth, systeDate) == 1)
        {
            isErrorFlag = true;
            fieldText += "出生日期|";
            msgText += "您输入的出生日期晚于当前系统日期|";  
        }
    }
    
//    //联系电话不为空时，判断联系电话是否正确
//    if (document.getElementById("txtTelephone").value != "")
//    {
//        if(!IsNumber(document.getElementById("txtTelephone").value))
//        {
//            isErrorFlag = true;
//            fieldText += "联系电话|";
//            msgText += "请输入正确的联系电话|";  
//        }
//    }
    
    //手机号码不为空时，判断手机号码是否正确
    if (document.getElementById("txtMobile").value != "")
    {
        if(!IsNumber(document.getElementById("txtMobile").value))
        {
            isErrorFlag = true;
            fieldText += "手机号码|";
            msgText += "请输入正确的手机号码|";  
        }
    }
    
    //电子邮件不为空时，判断电子邮件是否正确
    if (document.getElementById("txtEMail").value != "")
    {
        if(!IsEmail(document.getElementById("txtEMail").value))
        {
            isErrorFlag = true;
            fieldText += "电子邮件|";
            msgText += "请输入正确的电子邮件|";  
        }
    }
    
    //身高不为空时，判断身高是否正确
    var userHeight = document.getElementById("txtHeight").value;
    if ( userHeight != "" && userHeight != null)
    {
        if(!IsNumber(userHeight))
        {
            isErrorFlag = true;
            fieldText += "身高|";
            msgText += "身高只允许输入整数|";  
        }
    }
    
    //体重不为空时，判断体重是否正确
    var userWeight = document.getElementById("txtWeight").value;
    if (userWeight != "" && userWeight != null)
    {
        if(!IsNumeric(userWeight, 6, 2))
        {
            isErrorFlag = true;
            fieldText += "体重|";
            msgText += "请输入正确的体重|";  
        }
        if(parseInt(userWeight).toString().length > 4)
        {
            isErrorFlag = true;
            fieldText = fieldText +  "体重|";
   		    msgText = msgText + "体重整数部分过长|";
        }
    }
    
    //视力不为空时，判断视力是否正确
    var userSight = document.getElementById("txtSight").value;
    if (userSight != "" && userSight != null)
    {
        if(!IsNumeric(userSight, 4, 2))
        {
            isErrorFlag = true;
            fieldText += "视力|";
            msgText += "请输入正确的视力|";  
        }
        if(parseInt(userSight).toString().length > 2)
        {
            isErrorFlag = true;
            fieldText = fieldText +  "视力|";
   		    msgText = msgText + "视力整数部分过长|";
        }
    }
    
    //参加工作时间不为空时，判断参加工作时间是否正确
    if (document.getElementById("txtWorkTime").value != "")
    {
        if(!isDate(document.getElementById("txtWorkTime").value))
        {
            isErrorFlag = true;
            fieldText += "参加工作时间|";
            msgText += "请输入正确的参加工作时间|";  
        }
    }
    
    //总工龄不为空时，判断总工龄是否正确
//    if (document.getElementById("txtTotalSeniority").value != "")
//    {
//        if(!IsNumeric(document.getElementById("txtTotalSeniority").value))
//        {
//            isErrorFlag = true;
//            fieldText += "总工龄|";
//            msgText += "请输入正确的总工龄|";  
//        }
//    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}

/*
* 工作履历校验
*/
function CheckWorkHistoryInfo()
{  
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false; 
    
    //获取表格
    table = document.getElementById("tblWorkHistory");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var workCount = 0;
    var workParam = "";
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，校验输入
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            workCount++;
	        //开始时间
            workStart = document.getElementById("txtWorkStart_" + i).value;            
            //判断是否输入
            if(workStart == "")
            {
                isErrorFlag = true;
                fieldText += "工作经历 行：" + workCount + " 开始时间|";
                msgText += "请输入开始时间|";  
            }
            //判断是否是正确的日期
            if(workStart != "" && !isDate(workStart))
            {
                isErrorFlag = true;
                fieldText += "工作经历 行：" + workCount + " 开始时间|";
                msgText += "请输入正确的开始时间|";  
            }
	        //结束时间
            workEnd = document.getElementById("txtWorkEnd_" + i).value;           
            //判断是否输入
            if(workEnd == "")
            {
                isErrorFlag = true;
                fieldText += "工作经历 行：" + workCount + " 结束时间|";
                msgText += "请输入结束时间|";  
            }
            //判断是否是正确的日期
            if(workEnd != "" && !isDate(workEnd))
            {
                isErrorFlag = true;
                fieldText += "工作经历 行：" + workCount + " 结束时间|";
                msgText += "请输入正确的结束时间|";  
            }
            //比较开始时间和结束时间
            if (CompareDate(workStart, workEnd)  == 1)
            {
                isErrorFlag = true;
                fieldText += "工作经历 行：" + workCount + " 开始结束时间|";
                msgText += "您输入的结束时间早于开始时间|";  
            }
            
	        //工作单位
            workCompany = document.getElementById("txtWorkCompany_" + i).value;
            //工作单位没有输入时
            if (workCompany == "")
            {
                isErrorFlag = true;
                fieldText += "工作经历 行：" + workCount + " 工作单位|";
                msgText += "请输入工作单位|";  
            }
	    }
	}
	//如果有错显示错误信息
	if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}

/*
* 学习履历校验
*/
function CheckStudyHistoryInfo()
{  
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false; 
     
    //获取表格
    table = document.getElementById("tblStudyHistory");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var studyCount = 0;
    var studyParam = "";
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，校验输入
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            studyCount++;
	        //开始时间
            studyStart = document.getElementById("txtStudyStart_" + i).value;         
            //判断是否输入
            if(studyStart == "")
            {
                isErrorFlag = true;
                fieldText += "教育经历 行：" + studyCount + " 开始时间|";
                msgText += "请输入开始时间|";  
            }
            //判断是否是正确的日期
            if(studyStart != "" &&　!isDate(studyStart))
            {
                isErrorFlag = true;
                fieldText += "教育经历 行：" + studyCount + " 开始时间|";
                msgText += "请输入正确的开始时间|";  
            }
	        //结束时间
            studyEnd = document.getElementById("txtStudyEnd_" + i).value;         
            //判断是否输入
            if(studyEnd == "")
            {
                isErrorFlag = true;
                fieldText += "教育经历 行：" + studyCount + " 结束时间|";
                msgText += "请输入结束时间|";  
            }
            //判断是否是正确的日期
            if(studyEnd != "" &&　!isDate(studyEnd))
            {
                isErrorFlag = true;
                fieldText += "教育经历 行：" + studyCount + " 结束时间|";
                msgText += "请输入正确的结束时间|";  
            }
            
            //比较开始时间和结束时间
            if (CompareDate(studyStart, studyEnd) == 1)
            {
                isErrorFlag = true;
                fieldText += "教育经历 行：" + studyCount + " 开始结束时间|";
                msgText += "您输入的结束时间早于开始时间|";  
            }
	        //学校名称
            schoolName = document.getElementById("txtSchoolName_" + i).value;            
            //学校名称没有输入时
            if (schoolName == "")
            {
                isErrorFlag = true;
                fieldText += "教育经历 行：" + studyCount + " 学校名称|";
                msgText += "请输入学校名称|";  
            }
	    }
	}
	
	//如果有错显示错误信息
	if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}

/*
* 技能信息校验
*/
function CheckSkillInfo()
{  
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false; 
    
    //获取表格
    table = document.getElementById("tblSkill");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var skillCount = 0;
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，校验输入
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            skillCount++;
	        //技能名称
            skillName = document.getElementById("txtSkillName_" + i).value;
            //技能名称没有输入时
            if (skillName == "")
            {
                isErrorFlag = true;
                fieldText += "技能及证照信息 行：" + skillCount + " 技能名称|";
                msgText += "请输入技能名称|";  
            }
	        //证件名称
            certifyName = document.getElementById("txtCertificateName_" + i).value;
            //证件名称没有输入时
            if (certifyName == "")
            {
                isErrorFlag = true;
                fieldText += "技能及证照信息 行：" + skillCount + " 证件名称|";
                msgText += "请输入证件名称|";  
            }
	        //证件编号
            certifyNo = document.getElementById("txtCertificateNo_" + i).value;
            //证件编号没有输入时
            if (certifyNo == "")
            {
                isErrorFlag = true;
                fieldText += "技能及证照信息 行：" + skillCount + " 证件编号|";
                msgText += "请输入证件编号|";  
            }
	        //证件等级
            certifyLevel = document.getElementById("txtCertificateLevel_" + i).value;
            //证件等级没有输入时
            if (certifyLevel == "")
            {
                isErrorFlag = true;
                fieldText += "技能及证照信息 行：" + skillCount + " 证件等级|";
                msgText += "请输入证件等级|";  
            }
	        //发证单位
            issueCompany = document.getElementById("txtIssueCompany_" + i).value;
            //发证单位没有输入时
            if (issueCompany == "")
            {
                isErrorFlag = true;
                fieldText += "技能及证照信息 行：" + skillCount + " 发证单位|";
                msgText += "请输入发证单位|";  
            }
	        //发证时间
            issueDate = document.getElementById("txtIssueDate_" + i).value;           
            //判断是否输入
            if(issueDate == "")
            {
                isErrorFlag = true;
                fieldText += "技能及证照信息 行：" + skillCount + " 发证时间|";
                msgText += "请输入发证时间|";  
            }
            //判断是否是正确的日期
            if(issueDate != "" && !isDate(issueDate))
            {
                isErrorFlag = true;
                fieldText += "技能及证照信息 行：" + skillCount + " 发证日期|";
                msgText += "请输入正确的发证日期|";  
            }
	    }
	}
	//如果有错显示错误信息
	if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}

/*
* 保存人员信息
*/
function SaveEmployeeInfo()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    //工作经历校验 有错误时，返回
    } else if(CheckWorkHistoryInfo())
    {
        return;
    //学习经历校验 有错误时，返回
    } else if(CheckStudyHistoryInfo())
    {
        return;
    //技能信息校验 有错误时，返回
    } else if(CheckSkillInfo())
    {
        return;
    }
    //获取人员基本信息参数
    postParams = GetBaseInfoParams();
    //获取工作履历信息参数
    postParams += GetWorkInfoParams();
    //获取学习履历参数
    postParams += GetStudyInfoParams();
    //获取技能信息参数
    postParams += GetSkillInfoParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/EmployeeInfo_Add.ashx",
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
                //设置编辑模式
                document.getElementById("hidEditFlag").value = data.info;
                /* 设置编号的显示 */ 
                //显示人员的编号 人员编号DIV可见              
                document.getElementById("divEmployeeNo").style.display = "block";
                //设置人员编号
                document.getElementById("divEmployeeNo").innerHTML = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                //设置人员编号
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                //相片地址
                document.getElementById("hfPhotoUrl").value = document.getElementById("hfPagePhotoUrl").value
                //简历
                document.getElementById("hfResume").value = document.getElementById("hfPageResume").value;
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
* 获取人员信息基本资料
*/
function GetBaseInfoParams()
{
    editFlag = document.getElementById("hidEditFlag").value;
    var strParams = "&EditFlag=" + editFlag;//编辑标识
    var no = "";
    //更新的时候
    if ("UPDATE" == editFlag)
    {
        //人员编号
        no = document.getElementById("divEmployeeNo").innerHTML;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codruleEmployNo_ddlCodeRule").value;
        //手工输入的时候
        if ("" == codeRule)
        {
            //人员编号
            no = document.getElementById("codruleEmployNo_txtCode").value;
        }
        else
        {
            //编码规则ID
            strParams += "&CodeRuleID=" + document.getElementById("codruleEmployNo_ddlCodeRule").value;
        }
    }
    //人员编号
    strParams += "&EmployeeNo=" + no;
    
    /* 人员相片 */  
    strParams += "&PagePhotoURL=" + reescape(document.getElementById("hfPagePhotoUrl").value);//页面相片路径
    strParams += "&PhotoURL=" + reescape(document.getElementById("hfPhotoUrl").value);//数据库中保存的相片路径
    
    strParams += "&EmployeeName=" + reescape(document.getElementById("txtEmployeeName").value);//姓名
    strParams += "&EmployeeNum=" + reescape(document.getElementById("txtEmployeeNum").value);//工号
    strParams += "&PYShort=" + reescape(document.getElementById("txtPYShort").value);//拼音缩写
    strParams += "&UsedName=" + reescape(document.getElementById("txtUsedName").value);//曾用名
    strParams += "&NameEn=" + reescape(document.getElementById("txtNameEn").value);//英文名
    //人员分类
    flag = document.getElementById("ddlFlag").value;
    strParams += "&Flag=" + flag;
    strParams += "&CardID=" + document.getElementById("txtCardID").value;//身份证
    strParams += "&SafeguardCard=" + document.getElementById("txtSafeguardCard").value;//社保卡号
    
//    //人才储备时
//    if ("2" == flag)
//    {
        strParams += "&PositionTitle=" + reescape(document.getElementById("txtPositionTitle").value);//应聘职务
//    }
//    //在职人员 离职人员
//    else
//    {
        strParams += "&Position=" + document.getElementById("ddlPosition_ddlCodeType").value;//职称
        strParams += "&Quarter=" + document.getElementById("ddlQuarter_ddlCodeType").value;//所在岗位
        strParams += "&DeptID=" + document.getElementById("hdDeptID").value;//所在部门
        strParams += "&AdminLevelID=" + document.getElementById("ddlAdminLevelID_ddlCodeType").value;//岗位职等
        strParams += "&EnterDate=" + document.getElementById("txtEnterDate").value;//入职时间
//    }
    
    strParams += "&Sex=" + document.getElementById("ddlSex").value;//性别
    strParams += "&Birth=" + document.getElementById("txtBirth").value;//出身日期
    strParams += "&Marriage=" + document.getElementById("ddlMarriage_ddlCodeType").value;//婚姻状况
    strParams += "&Origin=" + reescape(document.getElementById("txtOrigin").value);//籍贯
    strParams += "&Telephone=" + reescape(document.getElementById("txtTelephone").value);//联系电话
    strParams += "&Mobile=" + reescape(document.getElementById("txtMobile").value);//手机号码
    strParams += "&EMail=" + reescape(document.getElementById("txtEMail").value);//电子邮件
    strParams += "&OtherContact=" + reescape(document.getElementById("txtOtherContact").value);//其他联系方式
    strParams += "&HomeAddress=" + reescape(document.getElementById("txtHomeAddress").value);//家庭住址
    strParams += "&Health=" + document.getElementById("ddlHealth").value;//健康状况
    strParams += "&Culture=" + document.getElementById("ddlCulture_ddlCodeType").value;//学历
    strParams += "&School=" + reescape(document.getElementById("txtSchool").value);//毕业院校
    strParams += "&Professional=" + document.getElementById("ddlProfessional_ddlCodeType").value;//专业  
    
    strParams += "&Landscape=" + document.getElementById("ddlLandscape_ddlCodeType").value;//政治面貌
    strParams += "&Religion=" + document.getElementById("ddlReligion_ddlCodeType").value;//宗教信仰
    strParams += "&National=" + document.getElementById("ddlNational_ddlCodeType").value;//民族
    strParams += "&Account=" + reescape(document.getElementById("txtAccount").value);//户口
    strParams += "&AccountNature=" + document.getElementById("ddlAccountNature").value;//户口性质
    strParams += "&Country=" + document.getElementById("ddlCountry_ddlCodeType").value;//国籍
    strParams += "&Height=" + document.getElementById("txtHeight").value;//身高
    strParams += "&Weight=" + document.getElementById("txtWeight").value;//体重
    strParams += "&Sight=" + document.getElementById("txtSight").value;//视力
    strParams += "&Degree=" + reescape(document.getElementById("txtDegree").value);//最高学位
    strParams += "&DocuType=" + reescape(document.getElementById("txtDocuType").value);//证件类型
    
    strParams += "&Features=" + reescape(document.getElementById("txtFeatures").value);//特长
    strParams += "&ComputerLevel=" + reescape(document.getElementById("txtComputerLevel").value);//计算机水平
    strParams += "&WorkTime=" + document.getElementById("txtWorkTime").value;//参加工作时间
    strParams += "&TotalSeniority=0";// + document.getElementById("txtTotalSeniority").value;//总工龄
    strParams += "&Language1=" + document.getElementById("ddlLanguage1_ddlCodeType").value;//外语语种1
    strParams += "&Language2=" + document.getElementById("ddlLanguage2_ddlCodeType").value;//外语语种2
    strParams += "&Language3=" + document.getElementById("ddlLanguage3_ddlCodeType").value;//外语语种3
    strParams += "&LanguageLevel1=" + document.getElementById("ddlLanguageLevel1").value;//外语水平1
    strParams += "&LanguageLevel2=" + document.getElementById("ddlLanguageLevel2").value;//外语水平2
    strParams += "&LanguageLevel3=" + document.getElementById("ddlLanguageLevel3").value;//外语水平3
    /* 人员简历 */  
    strParams += "&PageResumeURL=" + reescape(document.getElementById("hfPageResume").value);//页面简历路径
    strParams += "&ResumeURL=" + reescape(document.getElementById("hfResume").value);//数据库中保存的简历路径
    strParams += "&ProfessionalDes=" + reescape(document.getElementById("txtProfessionalDes").value);//专业描述 
    
    return strParams;   
}

/*
* 获取人员工作履历
*/
function GetWorkInfoParams()
{   
    //获取表格
    table = document.getElementById("tblWorkHistory");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var workCount = 0;
    var workParam = "";
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，添加信息
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            workCount++;
	        //开始时间
            workParam += "&WorkStart_" + workCount + "=" + document.getElementById("txtWorkStart_" + i).value;
	        //结束时间
            workParam += "&WorkEnd_" + workCount + "=" + document.getElementById("txtWorkEnd_" + i).value;
	        //工作单位
            workParam += "&WorkCompany_" + workCount + "=" + reescape(document.getElementById("txtWorkCompany_" + i).value);
	        //所在部门
            workParam += "&WorkDept_" + workCount + "=" + reescape(document.getElementById("txtWorkDept_" + i).value);
	        //工作内容
            workParam += "&WorkContent_" + workCount + "=" + reescape(document.getElementById("txtWorkContent_" + i).value);
	        //离职原因
            workParam += "&LeaveReason_" + workCount + "=" + reescape(document.getElementById("txtLeaveReason_" + i).value);
	    }
	}
	//工作履历记录数
    workParam += "&WorkCount=" + workCount;
    //返回参数信息
    return workParam;
}

/*
* 获取人员学习履历
*/
function GetStudyInfoParams()
{   
    //获取表格
    table = document.getElementById("tblStudyHistory");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var studyCount = 0;
    var studyParam = "";
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，添加信息
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            studyCount++;
	        //开始时间
            studyParam += "&StudyStart_" + studyCount + "=" + document.getElementById("txtStudyStart_" + i).value;
	        //结束时间
            studyParam += "&StudyEnd_" + studyCount + "=" + document.getElementById("txtStudyEnd_" + i).value;
	        //学校名称
            studyParam += "&SchoolName_" + studyCount + "=" + reescape(document.getElementById("txtSchoolName_" + i).value);
	        //专业
            studyParam += "&Professional_" + studyCount + "=" + document.getElementById("ddlProfessional_" + i).value;
	        //学历
            studyParam += "&CultureLevel_" + studyCount + "=" + document.getElementById("ddlCultureLevel_" + i).value;
	    }
	}
	//学习履历记录数
    studyParam += "&StudyCount=" + studyCount;
    //返回参数信息
    return studyParam;
}

/*
* 获取人员技能信息
*/
function GetSkillInfoParams()
{    
    //获取表格
    table = document.getElementById("tblSkill");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var skillCount = 0;
    var skillParam = "";
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，添加信息
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            skillCount++;
	        //技能名称
            skillParam += "&SkillName_" + skillCount + "=" + reescape(document.getElementById("txtSkillName_" + i).value);
	        //证件名称
            skillParam += "&CertificateName_" + skillCount + "=" + reescape(document.getElementById("txtCertificateName_" + i).value);
	        //证件编号
            skillParam += "&CertificateNo_" + skillCount + "=" + reescape(document.getElementById("txtCertificateNo_" + i).value);
	        //证件等级
            skillParam += "&CertificateLevel_" + skillCount + "=" + reescape(document.getElementById("txtCertificateLevel_" + i).value);
	        //发证单位
            skillParam += "&IssueCompany_" + skillCount + "=" + reescape(document.getElementById("txtIssueCompany_" + i).value);
	        //发证时间
            skillParam += "&IssueDate_" + skillCount + "=" + document.getElementById("txtIssueDate_" + i).value;
	        //有效期
            skillParam += "&Validity_" + skillCount + "=" + reescape(document.getElementById("txtValidity_" + i).value);
	    }
	}
	//技能记录数
    skillParam += "&SkillCount=" + skillCount;
    //返回参数信息
    return skillParam;
}

/*
* 人员分类改变时，改变对应的输入项目
*/
function ChangeFlag()
{
    //获取分类
    flag = document.getElementById("ddlFlag").value;
    //人才储备时
    if ("2" == flag)
    {
        //列名修改为应聘职务
        document.getElementById("divJobTitle").innerHTML = "应聘职务<span class='redbold'>*</span>";
        document.getElementById("divNum").innerHTML = "工号";
        //隐藏所在岗位
        document.getElementById("divQuarter").innerHTML = "";
        //所在岗位不显示
        document.getElementById("divQuarterValue").style.display = "none";
        //职称不显示
        document.getElementById("divPosition").style.display = "none";
        //部门不显示
        document.getElementById("divDeptName").style.display = "none";
        //隐岗位职等
        document.getElementById("divAdminLevelID").style.display = "none";
        //应聘职务显示
        document.getElementById("divPositionTitle").style.display = "block";
        //隐入职时间
        document.getElementById("divEnterDate").style.display = "none";  
        
        
    }
    //在职人员和离职人员时
    else
    {
        //列名修改为职称
        document.getElementById("divJobTitle").innerHTML = "职称<span class='redbold'>*</span>";
        //显示所在岗位
        document.getElementById("divQuarter").innerHTML = "岗位<span class='redbold'>*</span>";
        document.getElementById("divQuarter").style.display = "block";
        document.getElementById("divNum").innerHTML = "工号<span class='redbold'>*</span>";
        //职称显示
        document.getElementById("divPosition").style.display = "block";
        //所在岗位显示
        document.getElementById("divQuarterValue").style.display = "block";
        //应聘职务不显示
        document.getElementById("divPositionTitle").style.display = "none";
        
        //部门显示
        document.getElementById("divDeptName").style.display = "block";
        //显示岗位职等
        document.getElementById("divAdminLevelID").style.display = "block";
        //显示入职时间        
        document.getElementById("divEnterDate").style.display = "block";  
    }
}

/*
* 相片处理
*/
function DealEmployeePhoto(flag)
{
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    //上传相片
    else if ("upload" == flag)
    {
        document.getElementById("uploadKind").value = "PHOTO";
        ShowUploadPhoto();
    }
    //清除相片
    else if ("clear" == flag)
    {
        document.getElementById("imgPhoto").src = "../../../Images/Pic/Pic_Nopic.jpg";  
        document.getElementById("hfPagePhotoUrl").value = "";
    }
}

/*
* 文档处理
*/
function DealResume(flag)
{
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    //上传简历
    else if ("upload" == flag)
    {
        document.getElementById("uploadKind").value = "RESUME";
        ShowUploadFile();
    }
    //清除简历
    else if ("clear" == flag)
    {
            //设置简历路径
            document.getElementById("hfPageResume").value = "";
            //下载删除不显示
            document.getElementById("divDealResume").style.display = "none";
            //上传显示 
            document.getElementById("divUploadResume").style.display = "block";
    }
    //下载简历
    else if ("download" == flag)
    {
            //获取简历路径
            resumeUrl = document.getElementById("hfPageResume").value;
            //下载文件
            window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + resumeUrl, "_blank");
    }
}

/*
* 上传文件后回调处理
*/
function AfterUploadFile(url,docName)
{
    kind = document.getElementById("uploadKind").value;
    if ("PHOTO" == kind)
    {
        if (url != "")
        {
        //debugger;
            document.getElementById("imgPhoto").src = "../../../Images/Photo/" + url;
            document.getElementById("hfPagePhotoUrl").value = url;
        }
    }
    else
    {
        if (url != "")
        {
            //设置简历路径
            document.getElementById("hfPageResume").value = url;
            //下载删除显示
            document.getElementById("divDealResume").style.display = "block";
            document.getElementById("spanAttachmentName").innerHTML = docName;
            //上传不显示
            document.getElementById("divUploadResume").style.display = "none";
        }
    }
}

/*
* 获取拼音缩写
*/
function GetPYShort()
{
    employeeName = document.getElementById("txtEmployeeName").value;
    if (employeeName == "" || !isChinese(employeeName))
    {
        return;
    }
    else
    {
    postParams = "Text=" + employeeName;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Common/PYShort.ashx?" + postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        error: function()
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            
            {
                document.getElementById("txtPYShort").value = data.info;
            }
        } 
    });
    }
}

/*
* 返回按钮
*/
function DoBack()
{
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //迁移页面
    fromPage = document.getElementById("hidFromPage").value;
    //在职人员列表
    if (fromPage == "1")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidWorkModuleID").value;
        window.location.href = "EmployeeWork_Info.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //离职人员列表
    else if (fromPage == "2")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidLeaveModuleID").value;
        window.location.href = "EmployeeLeave_Info.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //人才储备列表
    else if (fromPage == "3")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidReserveModuleID").value;
        window.location.href = "EmployeeReserve_Info.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //新建面试
    else if (fromPage == "4")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidInterviewModuleID").value;
        window.location.href = "RectInterview_Edit.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //待入职
    else if (fromPage == "5")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidWaitModuleID").value;
        window.location.href = "WaitEnter.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //人力资源初始化向导
    else if (fromPage == "6") {
        //获取模块功能ID
        moduleID = document.getElementById("hidInitHumanModuleID").value;
        window.location.href = "InitGuid.aspx?ModuleID=" + moduleID;
    }
    //系统管理初始化
    else if (fromPage == "7") {
        //获取模块功能ID
        moduleID = document.getElementById("hidInitSysModuleID").value;
        window.location.href = "../SystemManager/InitGuid.aspx?ModuleID=" + moduleID;
    }
    //人力资源回收
    else if (fromPage == "8") {
         //获取模块功能ID
        moduleID = "2011211";
        window.location.href = "EmployeeCallback.aspx?ModuleID=" + moduleID + searchCondition;
    }
}

function Continue()
{
//    var SearCondition = document.getElementById("hidSearchCondition").value;
//    var fromPage = document.getElementById("hidFromPage").value;
//    window.location.href = "EmployeeInfo_Add.aspx?ModuleID=2011201&type=Continue" + SearCondition +"&FromPage="+fromPage;
    document.getElementById("txtEmployeeName").value = "";
    document.getElementById("txtPYShort").value = "";

    document.getElementById("hidEditFlag").value = "INSERT";

    document.getElementById("divEmployeeNo").style.display = "none";
    document.getElementById("divEmployeeNo").innerHTML = "";
    document.getElementById("divCodeRule").style.display = "block";

    var NoType = document.getElementById("codruleEmployNo_ddlCodeRule").value;

    if(NoType == "")
    {
        document.getElementById("codruleEmployNo_txtCode").value = "";
    }
    else
    {
        document.getElementById("codruleEmployNo_txtCode").value = "保存时自动生成";
    }

    //document.getElementById("codruleEmployNo_txtCode").value = "";

}
 
//function TotalSeniority()
//{
//    var oldDate = document.getElementById("txtWorkTime").value;
//    var newDate = document.getElementById("hidSysteDate").value;
//    
//    var dif = getDateDiff(oldDate,newDate);
//    document.getElementById("txtTotalSeniority").value = parseInt(dif/365) + 1;     
//}
