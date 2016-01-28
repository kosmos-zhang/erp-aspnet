$(document).ready(function()
{
      alerDeptQuarter();
   
});
/*
* 添加招聘目标
*/
function AddGoal(){
    //获取表格
    table = document.getElementById("tblRectGoalDetailInfo");
    //获取行号
    var no = table.rows.length;
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='checkbox' id='tblRectGoalDetailInfo_chkSelect_" + no + "'>"
	    + "<input type='hidden' id='hidDeptID_" + no + "' value=''>";
	   // 招聘部门
	    objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '10' size='10' class='tdinput' id='DeptrtName_" + no + "'  onclick=\"alertdiv('DeptrtName_"+no+",hidDeptID_"+no+"');\" />";
	    
	    
	//岗位
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	      
                     
	objTD.innerHTML = "<input type=\"hidden\" id=\"DeptQuarter" + no + "Hidden\" /> <input id=\"DeptQuarter" + no + "\" type=\"text\"  reado     maxlength =\"30\" class=\"tdinput\"       onclick =\"treeveiwPopUp.show()\" readonly=\"readonly\" />     ";
	//需求数量
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '3' size='10' class='tdinput' size=\"3\" id='txtPersonCount_" + no + "' onkeydown='Numeric_OnKeyDown();'  onchange='GetRequireNum();'>";
	//性别
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<select class='tdinput' id='ddlSex_" + no 
	        + "'><option value='3' selected>不限</option><option value='1'>男</option><option value='2'>女</option></select>";
	//年龄
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '25' size='10' class='tdinput' id='txtAge_" + no + "' onkeydown='Numeric_OnKeyDown();' >";
	//工作年限
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
		objTD.innerHTML = "<select class='tdinput'id='txtWorkAge_" + no + "'>" + document.getElementById("divWorkNature").innerHTML + "</select>";
	//学历       
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<select class='tdinput'id='ddlCultureLevel_" + no + "'>" + document.getElementById("ddlCulture_ddlCodeType").innerHTML + "</select>";
	//专业
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength = '50' class='tdinput' id='ddlProfessional_" + no + "'>";
	objTD.innerHTML = "<select class='tdinput'id='ddlProfessional_" + no + "'>" + document.getElementById("ddlProfessional_ddlCodeType").innerHTML + "</select>";
	//工作要求
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '500' size='10' class='tdinput' id='txtRequisition_" + no + "'>";
	//计划完成时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
objTD.innerHTML = "<input type='text' maxlength = '10' size='10' readonly class='tdinput' id='txtCompleteDate_" + no + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCompleteDate_" + no + "')})\">";

}

/*
* 添加信息发布时间和渠道
*/
function AddPublish(){
    //获取表格
    table = document.getElementById("tblRectPublishDetailInfo");
    //获取行号
    var no = table.rows.length;
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='checkbox' id='tblRectPublishDetailInfo_chkSelect_" + no + "'>";
	//发布媒体和渠道
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '50' size='10' style='width:85%' class='tdinput' id='txtPublishPlace_" + no + "'>    <span  style=\"cursor:hand\" onclick=\"popTaskObj.ShowList('txtPublishPlace_"+no+"')\">选择</span>";
	//发布时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
objTD.innerHTML = "<input type='text' maxlength = '10' size='10' readonly class='tdinput' id='txtPublishDate_" + no + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCompleteDate_" + no + "')})\">";
	//有效时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '4' size='10' style='width:95%'  onblur='SetEndDate(\"" + no + "\");' class='tdinput' id='txtValid_" + no + "'>";
	//截止时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength = '10' size='10' style='width:95%' class='tdinput' readonly id='txtEndDate_" + no + "'>";
	//费用
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '12' size='10'  style='width:95%' onchange='Number_round(this,\"2\");'  class='tdinput' id='txtCost_" + no + "'>";
	//效果
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '25' size='10' style='width:95%'  class='tdinput' id='txtEffect_" + no + "'>";
	//发布状态
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<select class='tdinput' id='ddlStatus_" + no + "'><option value='0'>暂停</option><option value='1' selected>发布中</option><option value='2'>结束</option></select>";

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
    editFlag = document.getElementById("hidEditFlag").value.Trim();
    //新建时，编号选择手工输入时
    if ("INSERT" == editFlag)
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("codruleRectPlan_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            rectApplyNo = document.getElementById("codruleRectPlan_txtCode").value.Trim();
            //编号必须输入
            if (rectApplyNo == "")
            {
                isErrorFlag = true;
                fieldText += "计划编号|";
   		        msgText += "请输入计划编号|";
            }
            else
            {
                if (!CodeCheck(rectApplyNo))
                {
                    isErrorFlag = true;
                    fieldText += "计划编号|";
                    msgText += "计划编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
    
    //主题必须输入
    if (document.getElementById("txtTitle").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "请输入主题|";  
    }
    //开始时间
    startDate = document.getElementById("txtStartDate").value.Trim();
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
    //负责人
    if (document.getElementById("txtPrincipal").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "负责人|";
        msgText += "请输入负责人|";  
    }
     
     //结束时间
    EndDate = document.getElementById("txtEndDate").value.Trim();
    if (EndDate == "")
    {
        isErrorFlag = true;
        fieldText += "结束时间|";
        msgText += "请输入结束时间|";  
    }
    else if (!isDate(EndDate))
    {
        isErrorFlag = true;
        fieldText += "结束时间|";
        msgText += "请输入正确的结束时间|";  
    }
    
    var  PlanFee=document.getElementById("txtPlanFee").value.Trim();
    if (PlanFee=="")
    {
    }
    else
    {
        if(  !IsNumeric(PlanFee, 8, 2))
            {
                isErrorFlag = true;
                  fieldText += "预算金额|";
        msgText += "请输入正确的预算金额|";  
            }
    }
    var FeeNote=document.getElementById("txtFeeNote").value.Trim();
        if(strlen(FeeNote)> 200)
        {
          isErrorFlag = true;
          fieldText += " 预算说明|";
          msgText += " 预算金额最多只允许输入200个字符！|";  
        }
          var JoinNote=document.getElementById("txtJoinNote").value.Trim();
        if(strlen(JoinNote)> 1024)
        {
          isErrorFlag = true;
          fieldText += " 成员分工说明 |";
          msgText += " 成员分工说明 最多只允许输入1024个字符！|";  
        }
          var JoinMan=document.getElementById("txtJoinMan").value.Trim();
        if(strlen(JoinMan)> 1024)
        {
          isErrorFlag = true;
          fieldText += " 招聘小组成员  |";
          msgText += " 招聘小组成员 最多只允许输入1024个字符！|";  
        }
        
        
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}

/*
* 招聘目标校验
*/
function CheckGoalInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //获取表格
    table = document.getElementById("tblRectGoalDetailInfo");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var errorRow = 0;
    //开始时间
    var startDate = document.getElementById("txtStartDate").value.Trim();
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，校验输入
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            errorRow++;
                 //职务名称
            DeptrtName = document.getElementById("DeptrtName_" + i).value.Trim();            
            //判断是否输入
            if(DeptrtName == "")
            {
                isErrorFlag = true;
                fieldText += "招聘目标 行：" + errorRow + " 招聘部门|";
                msgText += "请输入招聘部门|";  
            }
            
	        //职务名称
            positionTitile = document.getElementById("DeptQuarter"+i+"Hidden").value.Trim();            
            //判断是否输入
            if(positionTitile == "")
            {
                isErrorFlag = true;
                fieldText += "招聘目标 行：" + errorRow + " 岗位名称|";
                msgText += "请选择岗位名称|";  
            }
	        //人员数量
            personCount = document.getElementById("txtPersonCount_" + i).value.Trim();            
            //判断是否输入
            if(personCount == "")
            {
                isErrorFlag = true;
                fieldText += "招聘目标 行：" + errorRow + " 需求数量|";
                msgText += "请输入需求数量|";  
            }
            else if (!IsNumber(personCount))
            {
                isErrorFlag = true;
                fieldText += "招聘目标 行：" + errorRow + " 需求数量|";
                msgText += "请输入正确的需求数量|";  
            }
            
//              //人员数量
//            txtAge = document.getElementById("txtAge_" + i).value;            
//            //判断是否输入
//            if(personCount == "")
//            {
//               
//            }
//            else if (!IsNumber(txtAge))
//            {
//                isErrorFlag = true;
//                fieldText += "招聘目标 行：" + errorRow + " 年龄|";
//                msgText += "请输入正确的年龄|";  
//            }
	        //计划完成时间
            completeDate = document.getElementById("txtCompleteDate_" + i).value.Trim();            
            //判断是否输入
            if(completeDate != "" && !isDate(completeDate))
            {
                isErrorFlag = true;
                fieldText += "招聘目标 行：" + errorRow + " 计划完成时间|";
                msgText += "请输入正确的计划完成时间|";  
            }
            else if (CompareDate(startDate, completeDate) == 1)
            {
                isErrorFlag = true;
                fieldText += "招聘目标 行：" + errorRow + " 计划完成时间|";
                msgText += "您输入计划完成时间早于开始时间|";  
            }
	    }
	}
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}

/*
* 信息发布校验
*/
function CheckPublishInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //获取表格
    table = document.getElementById("tblRectPublishDetailInfo");
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
	        //发布媒体和渠道
            publishPlace = document.getElementById("txtPublishPlace_" + i).value.Trim();            
            //判断是否输入
            if(publishPlace == "")
            {
                isErrorFlag = true;
                fieldText += "信息发布 行：" + errorRow + " 发布媒体和渠道|";
                msgText += "请输入发布媒体和渠道|";  
            }
	        //发布时间
            publishDate = document.getElementById("txtPublishDate_" + i).value.Trim();            
            //判断是否输入
            if (publishDate == "")
            {
                isErrorFlag = true;
                fieldText += "信息发布 行：" + errorRow + " 发布时间|";
                msgText += "请输入发布时间|";  
            }
            else if(!isDate(publishDate))
            {
                isErrorFlag = true;
                fieldText += "信息发布 行：" + errorRow + " 发布时间|";
                msgText += "请输入正确的发布时间|";  
            }
	        //有效时间(天)
            valid = document.getElementById("txtValid_" + i).value.Trim();            
            //判断是否输入
            if(valid != "" && !IsNumber(valid))
            {
                isErrorFlag = true;
                fieldText += "信息发布 行：" + errorRow + " 有效时间|";
                msgText += "请输入正确的有效时间|";  
            }
	        //费用
            cost = document.getElementById("txtCost_" + i).value.Trim();            
            //判断输入是否正确
            if(cost != "" && !IsNumeric(cost, 10, 2))
            {
                isErrorFlag = true;
                fieldText += "信息发布 行：" + errorRow + " 费用|";
                msgText += "请输入正确的费用|";  
            }
//            //效果
//            effect = document.getElementById("txtEffect_" + i).value;    
//            if (!IsNumber(effect))
//            {
//                isErrorFlag = true;
//                fieldText += "招聘目标 行：" + errorRow + " 效果|";
//                msgText += "请输入正确的效果|";  
//            }
	    }
	}
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}

/*
* 保存招聘活动信息
*/
function SaveRectPlanInfo()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    //招聘目标校验 有错误时，返回
    } else if(CheckGoalInfo())
    {
        return;
    //信息发布校验 有错误时，返回
    } else if(CheckPublishInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    //获取招聘目标参数
    postParams += GetGoalParams();
    //获取信息发布参数
    postParams += GetPublishParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/RectPlan_Edit.ashx",
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
                //显示招聘活动编号 招聘活动编号DIV可见              
                document.getElementById("divRectPalnNo").style.display = "block";
                //设置招聘活动编号
                document.getElementById("divRectPalnNo").value = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                //设置招聘活动编号
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
    //编辑标识
    editFlag = document.getElementById("hidEditFlag").value;
    var param = "EditFlag=" + editFlag;
    var no = "";
    //更新的时候
    if ("UPDATE" == editFlag)
    {
        //编号
        no = document.getElementById("divRectPalnNo").value.Trim();
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codruleRectPlan_ddlCodeRule").value.Trim();
        //手工输入的时候
        if ("" == codeRule)
        {
            //招聘活动编号
            no = document.getElementById("codruleRectPlan_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            param += "&CodeRuleID=" + document.getElementById("codruleRectPlan_ddlCodeRule").value.Trim();
        }
    }
    //编号
    param += "&RectPlanNo=" + no;
    //主题
    param += "&Title=" + escape(document.getElementById("txtTitle").value.Trim());
    //开始时间
    param += "&StartDate=" + document.getElementById("txtStartDate").value.Trim();
   //结束日期
    param += "&EndDate=" + document.getElementById("txtEndDate").value.Trim();
    //计划状态
    param += "&Status=" + document.getElementById("ddlStatus").value.Trim();  
      //负责人
     param += "&Principal=" + document.getElementById("txtPrincipal").value.Trim();
      //招聘预算（元）
     param += "&PlanFee=" +escape ( document.getElementById("txtPlanFee").value.Trim());
        //预算说明
     param += "&FeeNote=" + escape (document.getElementById("txtFeeNote").value.Trim());
       //招聘人数
     param += "&RequireNum=" + escape (document.getElementById("txtRequireNum").value.Trim());
        //招聘小组成员
     param += "&UserJoinMan=" + escape (document.getElementById("UserJoinMan").value.Trim());
             //成员分工说明
     param += "&JoinNote=" +escape ( document.getElementById("txtJoinNote").value.Trim());
     
    return param;
}

/*
* 招聘目标参数
*/
function GetGoalParams()
{
    //获取表格
    table = document.getElementById("tblRectGoalDetailInfo");
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
            //部门ID
            param += "&ApplyDept_" + count + "=" + escape (document.getElementById("hidDeptID_" + i).value.Trim());
	        //职务名称 
	        //DeptQuarter" + no + "Hidden\" /> <input id=\"DeptQuarter" + no + "\" t
	        //岗位ID
            param += "&DeptQuarter"+ count + "Hidden=" + escape(document.getElementById("DeptQuarter" + i+"Hidden").value.Trim());
        //   岗位名称
               param += "&DeptQuarter_"+ count + "=" + escape(document.getElementById("DeptQuarter" + i).value.Trim());
	        //人员数量
            param += "&PersonCount_" + count + "=" + escape (document.getElementById("txtPersonCount_" + i).value.Trim());
	        //性别
            param += "&Sex_" + count + "=" + escape (document.getElementById("ddlSex_" + i).value.Trim());
	        //年龄
            param += "&Age_" + count + "=" + escape ( document.getElementById("txtAge_" + i).value.Trim());
	        //学历
            param += "&CultureLevel_" + count + "=" +escape ( document.getElementById("ddlCultureLevel_" + i).value.Trim());
	        //专业
            param += "&Professional_" + count + "=" + escape (document.getElementById("ddlProfessional_" + i).value.Trim());
             param += "&WorkAge_" + count + "=" + escape (document.getElementById("txtWorkAge_" + i).value.Trim());
            
            
	        //要求
            param += "&Requisition_" + count + "=" + escape(document.getElementById("txtRequisition_" + i).value.Trim());
	        //计划完成时间
            param += "&CompleteDate_" + count + "=" + escape (document.getElementById("txtCompleteDate_" + i).value.Trim());
	    }
	}
	//table表记录数
    param += "&GoalCount=" + count;
    //返回参数信息
    return param;
}

/*
* 信息发布参数
*/
function GetPublishParams()
{  
    //获取表格
    table = document.getElementById("tblRectPublishDetailInfo");
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
	        //发布媒体和渠道
            param += "&PublishPlace_" + count + "=" + escape(document.getElementById("txtPublishPlace_" + i).value.Trim());
	        //发布时间
            param += "&PublishDate_" + count + "=" + document.getElementById("txtPublishDate_" + i).value.Trim();
	        //有效时间
            param += "&Valid_" + count + "=" + document.getElementById("txtValid_" + i).value.Trim();
	        //截止时间
            param += "&EndDate_" + count + "=" + document.getElementById("txtEndDate_" + i).value.Trim();
	        //费用
            param += "&Cost_" + count + "=" + document.getElementById("txtCost_" + i).value.Trim();
	        //效果
            param += "&Effect_" + count + "=" + escape(document.getElementById("txtEffect_" + i).value.Trim());
	        //发布状态
            param += "&Status_" + count + "=" + document.getElementById("ddlStatus_" + i).value.Trim();
	    }
	}
	//table表记录数
    param += "&PublishCount=" + count;
    //返回参数信息
    return param;
}

/*
* 返回招聘活动列表
*/
function DoBack()
{
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //获取模块功能ID
    moduleID = document.getElementById("hfModuleID").value;
    window.location.href = "RectPlan_Info.aspx?ModuleID=" + moduleID + searchCondition;
}
/*
* 从招聘计划添加目标
*/
function AddGoalFromApply()
{
    //设置div的位置
    var addGoalFromApply ="";
    // 整个div的大小和位子
    addGoalFromApply += "<div id='divAddGoalFromApply' style='background-color:white;position:absolute;left:200;top:200;'>";
    // 白色div中的信息
    addGoalFromApply += "<table cellpadding='0' cellspacing='1' border='0' class='border' align=left>";
    addGoalFromApply += "<tr><td>";
    addGoalFromApply += "<iframe src='AddGoalFromApply.aspx' scrolling=yes width='800' height='450' frameborder='0'></iframe>";
    addGoalFromApply += "</td></tr>";
    addGoalFromApply += "</table>";
    //--end
    addGoalFromApply += "</div>";
    document.body.insertAdjacentHTML("afterBegin", addGoalFromApply);
}

/*
* 设置从申请中添加的招聘目标
*/
function SetGoalFromApply(goalInfo)
{
    
    //隐藏DIV
    document.getElementById("divAddGoalFromApply").style.display = "none";
    //设置选择的招聘目标
    if (goalInfo != null && typeof(goalInfo) == "object")
    {
        len = goalInfo.length;
        for (i = 0; i < len; i++)
        {
            //获取招聘目标
            goal = goalInfo[i];
            //设置招聘目标到表格中
            AddGoalToTable(goal);
        }
    }
}

/*
* 添加申请中的目标到Table
*/
function AddGoalToTable(goal){
 
    //获取表格
    table = document.getElementById("tblRectGoalDetailInfo");
    //获取行号
    var no = table.rows.length;
   var sDepID= goal.PositionID;
    var sDepName= goal.JobName;
    var deptLength=sDepID .split(",").length;
    
    var sdDeptQuterName=sDepName.split(",");
        var sdDeptQuterID=sDepID.split(",");
    for (var i =0 ;i <deptLength ;i ++)
    {
    var newRow=no +i;
 
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='checkbox' id='tblRectGoalDetailInfo_chkSelect_" + newRow + "'>"
	    + "<input type='hidden' id='hidDeptID_" + newRow + "' value='" + goal.DeptID + "'>";
	       // 招聘部门
	    objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '10' class='tdinput' id='DeptrtName_" + newRow + "' value='" + goal.DeptName + "'  onclick=\"alertdiv('DeptrtName_"+newRow+",hidDeptID_"+newRow+"');\" />";
	//职务名称
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
//	objTD.innerHTML = "<input type='text' maxlength = '50' class='tdinput' id='txtPositionTitle_" + newRow + "' value='" + goal.JobName + "'>";
	
		objTD.innerHTML = "<input type=\"hidden\" id=\"DeptQuarter" + newRow + "Hidden\"  value='" + sdDeptQuterID[i]+ "' /> <input id=\"DeptQuarter" + newRow + "\" type=\"text\"  reado     maxlength =\"30\" class=\"tdinput\"       onclick =\"treeveiwPopUp.show()\" readonly=\"readonly\"  value='" + sdDeptQuterName[i]+ "'/>      ";
		
		
	//人员数量
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '3' class='tdinput' id='txtPersonCount_" + newRow + "' value='" + goal.RectCount + "'   onkeydown='Numeric_OnKeyDown();'  onchange='GetRequireNum();'>";
	//性别
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<select class='tdinput' id='ddlSex_" + newRow 
	        + "'><option value='3' selected>不限</option><option value='1'>男</option><option value='2'>女</option></select>";
	document.getElementById("ddlSex_" + newRow).value = goal.SexID;
	//年龄
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '25' class='tdinput' id='txtAge_" + newRow + "' value='" + goal.Age + "'   onkeydown='Numeric_OnKeyDown();' >";
	//工作年限
		objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
		objTD.innerHTML = "<select class='tdinput'id='txtWorkAge_" + newRow + "'>" + document.getElementById("divWorkNature").innerHTML + "</select>";
 
	if(goal.workAge=='null' ||goal.workAge=='')
	{
		document.getElementById("txtWorkAge_" + newRow).value =0;
		}
		else
		{
			document.getElementById("txtWorkAge_" + newRow).value =goal.workAge;
		}
		
	//学历
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<select class='tdinput'id='ddlCultureLevel_" + newRow + "'>" + document.getElementById("ddlCulture_ddlCodeType").innerHTML + "</select>";
	if (goal.CultureLevelID==0)
	{
	document.getElementById("ddlCultureLevel_" + newRow).value = '';
	}else
	{
	document.getElementById("ddlCultureLevel_" + newRow).value = goal.CultureLevelID;
	}
	//专业
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength = '50' class='tdinput' id='ddlProfessional_" + newRow + "'>";
	objTD.innerHTML = "<select class='tdinput'id='ddlProfessional_" + newRow + "'>" + document.getElementById("ddlProfessional_ddlCodeType").innerHTML + "</select>";
		if (goal.ProfessionalID==0)
	{
	document.getElementById("ddlProfessional_" + newRow).value ='';
	}
	else
	{
		document.getElementById("ddlProfessional_" + newRow).value = goal.ProfessionalID;
	}
	//要求
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '500' class='tdinput' id='txtRequisition_" + newRow + "' value='" + goal.WorkNeeds + "'>";
	//计划完成时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
objTD.innerHTML = "<input type='text' maxlength = '10' class='tdinput' value='" + goal.CompleteDate + "' id='txtCompleteDate_" + newRow + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCompleteDate_" + newRow + "')})\">";
   }

GetRequireNum();
}

/*
* 设置截止日期
*/
function SetEndDate(row)
{   
    //发布时间
    var publishDate = document.getElementById("txtPublishDate_" + row).value.Trim();
   
    //有效时间    
    var valid = document.getElementById("txtValid_" + row).value.Trim();
    //如果发布时间和有效时间有一个没有输入时，不进行自动计算
    if (publishDate == "" || valid == "") return;
    //如果输入了正确的日期和有效时间时，计算截止日期
    if (isDate(publishDate) && IsNumber(valid))
    {
        //转化为日期型的发布时间
        var publish = new Date(publishDate.replace(/-/g, "/"));
        //获取截止日期
        var endDate = new Date(publish.getFullYear(), publish.getMonth(), publish.getDate() + parseInt(valid));
        var month = (endDate.getMonth() + 1).toString();
        if (month.length == 1) month = "0" + month;
        var day = endDate.getDate().toString();
        if (day.length == 1) day = "0" + day;
        //设置到控件表示
        document.getElementById("txtEndDate_" + row).value = endDate.getFullYear() + "-" + month + "-" + day;
    }
}

function GetRequireNum()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //获取表格
    table = document.getElementById("tblRectGoalDetailInfo");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var errorRow = 0;
    //开始时间
 var sum=0;
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，校验输入
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            errorRow++;
    
            
	     
	        //人员数量
            personCount = document.getElementById("txtPersonCount_" + i).value.Trim();        
                
            //判断是否输入
            if(personCount == "")
            {
                 personCount =0;
            }
            else if (!IsZint(personCount))
            {
                isErrorFlag = true;
                fieldText += "人员需求 行：" + errorRow + " 需求人数|";
                msgText += " 需求数量输入有误，请输入有效的数值（整数）|";  
                break ;
            }
            else
            {
            sum =sum +parseInt (personCount ,10);
            }
   
	    }
	}
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }else
    {
    document .getElementById ("txtRequireNum").value=sum ;
    }

 
}