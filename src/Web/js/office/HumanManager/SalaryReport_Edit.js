
$(document).ready(function()
{
      requestobj = GetRequest();
      ID=requestobj['ID'];

      if(typeof(ID)=="undefined")
      {
            GetFlowButton_DisplayControl();
      }
       else
      {
                  //alert (document .getElementById ("txtBillStatus").value);
                      GetFlowButton_DisplayControl();
        }
});


 function DoDelete()
 {
    var reportNo=document.getElementById("txtReportNo").value.Trim();
// if (reportNo =="" || reportNo =null )
// {
//      showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
// }
        $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryReport_Edit.ashx?Action=DeleteInfo&ReportNo="+reportNo ,
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
            if(data.sta == "1") 
            {  
            self.location='SalaryReport_Edit.aspx?ModuleID=2011703'; 
            }
 
        } 
    }); 
 
 
 
 }
/*
* 生成报表
*/
function DoCreateReport()
{
 
  if(!fnCheck())
    return;
    //输入校验
    if (CheckCreateInput("1"))
    {
        return;
    }
    //获取输入参数
    postParam = GetCreateParams("1");
    //生成工资报表
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryReport_Edit.ashx?" + postParam,
        dataType:"string",//返回json格式数据
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
       
            var info = data.split("|");
           
            if(info[0] == "1") 
            { 
                /* 设置编号的显示 */ 
                //显示工资报表编号
                document.getElementById("divCodeNo").style.display = "block";
                //设置工资报表编号
                document.getElementById("divCodeNo").innerHTML = info[1];
                document.getElementById("txtReportNo").value = info[1];
                //隐藏编码规则生成控制
                document.getElementById("divCodeRule").style.display = "none";
                document.getElementById("divSalaryDetailInfo").innerHTML = info[2];
                document.getElementById("txtIdentityID").value = info[3];
                //
                document.getElementById("btnCreateReport").src = "../../../Images/Button/cw_uscbb.jpg";
                document.getElementById("btnCreateReport").disabled = true;
     
   document .getElementById ("ImgReBuild").src = "../../../Images/Button/btn_qxsc.jpg";
                 document.getElementById("ImgReBuild").attachEvent("onclick", DoDelete);
                 
                 
                 
                   document .getElementById ("txtReportStatus").value="已生成";
                    document .getElementById ("ddlYear").disabled=true ;
                     document .getElementById ("ddlMonth").disabled=true ;
                //设置提示信息
                   $("#hiddenBillStatus").val(1);
                GetFlowButton_DisplayControl();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","生成报表成功！");
                //getAll();
            }
            else if( info[0] == 2)
            {
          //  alert (1);
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请输入未使用的编号！");
            }
            else if(info[0] == "3")
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","对应月份的工资报表已经生成！");
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
        } 
    }); 
}
//function gets()
//{
//alert (document.getElementById("divSalaryDetailInfo").innerHTML );
//}
 function cTrim(sInputString,iType)   // Description: sInputString 为输入字符串，iType为类型，分别为 0 - 去除前后空格; 1 - 去前导空格; 2 - 去尾部空格
 {   
    var sTmpStr = ' ' ;
    var i = -1 ;
    if(iType == 0 || iType == 1) 
    {  
          while(sTmpStr == ' ') 
          {  
            ++i ; 
             sTmpStr = sInputString.substr(i,1) ;
           } 
         sInputString = sInputString.substring(i) ;
    } 
   if(iType == 0 || iType == 2)  
   {  
           sTmpStr = ' ' ;
            i = sInputString.length ; 
            while(sTmpStr == ' ') 
             {  
               --i;  
               sTmpStr = sInputString.substr(i,1); 
              }  
            sInputString = sInputString.substring(0,i+1);  
    }  
    return sInputString;  
     }
/*
* 输入内容正确性校验
*/
function CheckCreateInput(flag)
{

    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    /* 页面信息校验 */
    //获取ID
    ID = document.getElementById("hidIdentityID").value;
    //生成报表时
    if ("1" == flag)
    { 
        //新建时 
        if (ID == null || ID == "")
        {
            //编码规则ID
            codeID = document.getElementById("codeRule_ddlCodeRule").value.Trim();
            //手工输入时
            if (codeID == null || codeID == "")
            {
                //获取输入的编号
                codeNo = document.getElementById("codeRule_txtCode").value.Trim();
                //编号没有输入时
                if (codeNo == null || codeNo == "")
                {
                    //设置错误信息
                    isErrorFlag = true;
                    fieldText += "工资报表编号|";
                    msgText += "请输入工资报表编号|";  
                }
                //输入了字母数字以外的字符时
                else if (!CodeCheck(codeNo))
                {
                    //设置错误信息
                    isErrorFlag = true;
                    fieldText += "工资报表编号|";
                    msgText += "工资报表编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";  
                }
            }
        }
    }
    //工资报表主题
    input = document.getElementById("txtTitle").value.Trim();
    //主题没有输入时
    if (cTrim ( input,0) == null ||cTrim ( input,0) == "")
    {
        //设置错误信息
        isErrorFlag = true;
        fieldText += "工资报表主题|";
        msgText += "请输入工资报表主题|";  
    }
    //开始时间没有输入时
    startDate = document.getElementById("txtStartDate").value.Trim();
    if (startDate == null || startDate == "")
    {
        //设置错误信息
        isErrorFlag = true;
        fieldText += "开始时间|";
        msgText += "请输入开始时间|";  
    }
    //结束时间没有输入时
    endDate = document.getElementById("txtEndDate").value.Trim();
    if (endDate == null || endDate == "")
    {
        //设置错误信息
        isErrorFlag = true;
        fieldText += "结束时间|";
        msgText += "请输入结束时间|";  
    }
    //比较开始日期和结束日期大小
    if (startDate != null && startDate != "" && endDate != "" && endDate != null)
    {
        if (CompareDate(startDate, endDate) == 1)
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += "开始时间|";
            msgText += "您输入的开始时间大于结束时间|";  
        }
    }
    //新建时 
 
    if (ID == null || ID == "")
    {
        //编制人没有输入时
        input = document.getElementById("txtCreator").value.Trim();
        if (input == null || input == "")
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += "编制人|";
            msgText += "请输入编制人|";  
        }
        //编制日期没有输入时
        input = document.getElementById("txtCreateDate").value.Trim();
        if (input == null || input == "")
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += "编制日期|";
            msgText += "请输入编制日期|";  
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
function GetCreateParams(flag)
{ 
    var strParams = "";
    if ("1" == flag)
    {
        ID = document.getElementById("hidIdentityID").value;
        strParams = "Action=Create";
        //获取编码规则ID
        codeRule = document.getElementById("codeRule_ddlCodeRule").value.Trim();
        //手工输入的时候
        if ("" == codeRule)
        {
            //人员编号
            strParams += "&ReportNo=" + document.getElementById("codeRule_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            strParams += "&CodeRuleID=" + document.getElementById("codeRule_ddlCodeRule").value.Trim();
        }
        //编制人
        strParams += "&Creator=" + document.getElementById("txtCreator").value.Trim();
        //编制日期
        strParams += "&CreateDate=" + document.getElementById("txtCreateDate").value.Trim();
    }
    else
    {
        strParams = "Action=Save";
        //人员编号
        strParams += "&ReportNo=" + document.getElementById("divCodeNo").innerHTML;
    }
    
    /* 获取工资报表基本信息 */
    //工资报表主题
    strParams += "&Title=" + escape(document.getElementById("txtTitle").value.Trim());
    //所属月份
    strParams += "&BelongMonth=" + document.getElementById("ddlYear").value.Trim() 
                + document.getElementById("ddlMonth").value.Trim();
    //开始时间
    strParams += "&StartDate=" + document.getElementById("txtStartDate").value.Trim();
    //结束时间
    strParams += "&EndDate=" + document.getElementById("txtEndDate").value.Trim();
    
    //返回参数字符串
    return strParams;
}


function CalculateAddSalary(obj, row)
{

    //获取输入的值
    var inputValue = obj.value;
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
        else if (index > -1)
        {
            obj.value = inputValue.substring(0, index + 3);
        }
    //获取工资项总数
    salaryCount = parseInt(document.getElementById("txtSalaryItemCount").value.Trim());

        //获取工资项个数

    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim());
    //遍历所有员工，校验每个员工的工资信息
  var i=row;
    var subMoney=0;
        //社会保险
       var  inputInsuranceSalary = 0;
       var a=document.getElementById("txtInsurance_" + i).value.Trim() ;
        //输入时校验输入的有效性
        if (a!= null && a != "" && IsNumeric(a, 10, 2))
        {
            inputInsuranceSalary = document.getElementById("txtInsurance_" + i).value.Trim();
        }
        //个人所得税
        var   inputIncomeTaxSalary = 0;
        var b=document.getElementById("txtIncomeTax_" + i).value.Trim();
        //输入时校验输入的有效性
        if (b != null && b!= "" &&IsNumeric(b, 10, 2))
        {
             inputIncomeTaxSalary = document.getElementById("txtIncomeTax_" + i).value.Trim();
        }
        //其他应扣款
       var  inputOtherMinusMoneySalary = 0;
        //输入时校验输入的有效性
        var c=document.getElementById("txtOtherMinusMoney_" + i).value.Trim() ;
        if (c!= null && c != "" && IsNumeric(c, 10, 2))
        {
         inputOtherMinusMoneySalary = document.getElementById("txtOtherMinusMoney_" + i).value.Trim();
        }
         //输入时校验输入的有效性
  
        
        salaryRowID = "txtFixedMoney_" + i + "_";
            var totalSalaryMoney = 0;
        //遍历所有工资输入
        for (var a = 1; a<= salaryCount; a++)
        {
            //工资额 
            salaryMoney = document.getElementById(salaryRowID + a).value.Trim();
            //是否扣款
            payFlag = document.getElementById("txtSalaryFlag_" + a).value.Trim();
            if (salaryMoney != null && salaryMoney != "" && IsNumeric(salaryMoney, 10, 2))
            {
                //扣款时
                if ("1" == payFlag)
                {
                    //计算总金额
//                    totalSalaryMoney -= parseFloat(salaryMoney);
                    subMoney +=parseFloat(salaryMoney);
                }
                //加款
                else
                {
                    //计算总金额
                    totalSalaryMoney += parseFloat(salaryMoney);
                }
           }
        }
        
             //计件工资
        var workMoney = document.getElementById("txtWorkMoney_" + i).value.Trim();
        if (workMoney != null && workMoney != "" && IsNumeric(workMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(workMoney);
        }
        //计时工资
        var timeMoney = document.getElementById("txtTimeMoney_" + i).value.Trim();
        if (timeMoney != null && timeMoney != "" && IsNumeric(timeMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(timeMoney);
        }
        //产品单品提成工资
        var commissionMoney = document.getElementById("txtCommissionMoney_" + i).value.Trim();
        if (commissionMoney != null && commissionMoney != "" && IsNumeric(commissionMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(commissionMoney);
        }
           //公司提成工资
        var CompanyComMoney = document.getElementById("txtCompanyComMoney_" + i).value.Trim();
        if (CompanyComMoney != null && CompanyComMoney != "" && IsNumeric(CompanyComMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(CompanyComMoney);
        }
        //部门提成工资
        var DeptComMoney = document.getElementById("txtDeptComMoney_" + i).value.Trim();
        if (DeptComMoney != null && DeptComMoney != "" && IsNumeric(DeptComMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(DeptComMoney);
        }
         //个人业务提成
        var PersonComMoney = document.getElementById("txtPersonComMoney_" + i).value.Trim();
        if (PersonComMoney != null && PersonComMoney != "" && IsNumeric(PersonComMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(PersonComMoney);
        }
             //绩效提成
        var PerformanceMoney = document.getElementById("txtPerformanceMoney_" + i).value.Trim();
        if (PerformanceMoney != null && PerformanceMoney != "" && IsNumeric(PerformanceMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(PerformanceMoney);
        }
        
        
        
        //其他应付工资
        var otherPayMoney = document.getElementById("txtOtherPayMoney_" + i).value.Trim(); 
        if (otherPayMoney != null && otherPayMoney != "" && IsNumeric(otherPayMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(otherPayMoney);
        }
        
        
        
     //   totalSalaryMoney = parseInt(totalSalaryMoney * 100 + 0.5) / 100;
//        var swMoney=totalSalaryMoney-parseFloat (subMoney);
        
        document.getElementById("txtAllGetMoney_" + i).value=tofloat(totalSalaryMoney,2);
        
//              var allKillMoney= 0;
//        var d=document.getElementById("txtAllGetMoney_" + i).value;
//        if (d != null && d != "" && IsNumeric(d, 10, 2))
//        {
//         allKillMoney = parseFloat( document.getElementById("txtAllGetMoney_" + i).value);
//        }
        
        
        
        
        
        
        
        var minus=parseFloat( inputInsuranceSalary)+parseFloat(inputIncomeTaxSalary )+parseFloat(inputOtherMinusMoneySalary);
        var last=totalSalaryMoney-minus-subMoney  ;
        document.getElementById("txtAllKillMoney_" + i).value=tofloat(minus+subMoney,2);
         //   document.getElementById("txtFactSalaryMoney_" + i).value=last ;
                 document.getElementById("txtFactSalaryMoney_" + i).value=NumRound(last,2);
//           document.getElementById("txtFactSalaryMoney_" + i).value=tofloat (last,2);
        }
    
//    lastALL();
}


///*
//* 计算工资合计
//*/
//function CalculateAddSalary(obj, row)
//{ 
//    //获取输入的值
//    var inputValue = obj.value;
//    //判断输入的小数点
//    var arr = inputValue.split(".");
//    if (arr.length <= 2)
//    {
//        index = inputValue.indexOf(".");
//        //输入的第一位是小数点时，前面加0
//        if (index == 0)
//        {
//            obj.value = "0" + inputValue;
//        }
//        //最后一位是小数点时
//        else if (index == inputValue.length - 1)
//        {
//            obj.value = inputValue.replace(".", "");
//        }
//        else if (index > -1)
//        {
//            obj.value = inputValue.substring(0, index + 3);
//        }
//        //获取工资ID相同部门
//        salaryRowID = "txtFixedMoney_" + row + "_";
//        //获取工资项个数
//        salaryCount = parseInt(document.getElementById("txtSalaryItemCount").value);
//        //定义变量
//        var totalSalaryMoney = 0;
//        //遍历所有工资输入
//        for (var i = 1; i <= salaryCount; i++)
//        {
//            //工资额 
//            salaryMoney = document.getElementById(salaryRowID + i).value;
//            //是否扣款
//            payFlag = document.getElementById("txtSalaryFlag_" + i).value;
//            if (salaryMoney != null && salaryMoney != "" && IsNumeric(salaryMoney, 10, 2))
//            {
//                //扣款时
//                if ("1" == payFlag)
//                {
//                    //计算总金额
//                    totalSalaryMoney -= parseFloat(salaryMoney);
//                }
//                //加款
//                else
//                {
//                    //计算总金额
//                    totalSalaryMoney += parseFloat(salaryMoney);
//                }
//            }
//        }
//        //计件工资
//        var workMoney = document.getElementById("txtWorkMoney_" + row).value;
//        if (workMoney != null && workMoney != "" && IsNumeric(workMoney, 10, 2))
//        {
//            totalSalaryMoney += parseFloat(workMoney);
//        }
//        //计时工资
//        var timeMoney = document.getElementById("txtTimeMoney_" + row).value;
//        if (timeMoney != null && timeMoney != "" && IsNumeric(timeMoney, 10, 2))
//        {
//            totalSalaryMoney += parseFloat(timeMoney);
//        }
//        //提成工资
//        var commissionMoney = document.getElementById("txtCommissionMoney_" + row).value;
//        if (commissionMoney != null && commissionMoney != "" && IsNumeric(commissionMoney, 10, 2))
//        {
//            totalSalaryMoney += parseFloat(commissionMoney);
//        }
//        //其他应付工资
//        var otherPayMoney = document.getElementById("txtOtherPayMoney_" + row).value; 
//        if (otherPayMoney != null && otherPayMoney != "" && IsNumeric(otherPayMoney, 10, 2))
//        {
//            totalSalaryMoney += parseFloat(otherPayMoney);
//        }
//        totalSalaryMoney = parseInt(totalSalaryMoney * 100 + 0.5) / 100;
//              
//        //获取现在的工资合计
//        var allMoney = document.getElementById("txtAllGetMoney_" + row).value;
//        if (allMoney == "" || allMoney == null) allMoney = "0";
//        //相差额
//        var minusMoney =tofloat ( (totalSalaryMoney - allMoney),2);
//        document.getElementById("txtAllGetMoney_" + row).value = totalSalaryMoney;
//        
//        //获取目前应发工资
//        var factMoney = document.getElementById("txtFactSalaryMoney_" + row).value;
//        //计算新的应发工资
//        
//        
//	    /* 未考虑 NaN的情况 有时间再做 */
//	    document.getElementById("txtFactSalaryMoney_" + row).value = tofloat(( parseFloat(factMoney) -parseFloat( minusMoney) ),2);
//    }
//}

function lastALL()
{

   salaryCount = parseInt(document.getElementById("txtSalaryItemCount").value.Trim());
    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim());
    //遍历所有员工，校验每个员工的工资信息
    for (var row= 1; row <= userCount;row++)
    {
 //获取工资ID相同部门
        salaryRowID = "txtFixedMoney_" + row + "_";
        //获取工资项个数
        salaryCount = parseInt(document.getElementById("txtSalaryItemCount").value.Trim());
        //定义变量
        var totalSalaryMoney = 0;
        //遍历所有工资输入
        for (var i = 1; i <= salaryCount; i++)
        {
            //工资额 
            salaryMoney = document.getElementById(salaryRowID + i).value.Trim();
            //是否扣款
            payFlag = document.getElementById("txtSalaryFlag_" + i).value.Trim();
            if (salaryMoney != null && salaryMoney != "" && IsNumeric(salaryMoney, 10, 2))
            {
                //扣款时
                if ("1" == payFlag)
                {
                    //计算总金额
                    totalSalaryMoney -= parseFloat(salaryMoney);
                }
                //加款
                else
                {
                    //计算总金额
                    totalSalaryMoney += parseFloat(salaryMoney);
                }
           }
        }
        
        //计件工资
        var workMoney = document.getElementById("txtWorkMoney_" + row).value.Trim();
        if (workMoney != null && workMoney != "" && IsNumeric(workMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(workMoney);
        }
        //计时工资
        var timeMoney = document.getElementById("txtTimeMoney_" + row).value.Trim();
        if (timeMoney != null && timeMoney != "" && IsNumeric(timeMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(timeMoney);
        }
        //提成工资
        var commissionMoney = document.getElementById("txtCommissionMoney_" + row).value.Trim();
        if (commissionMoney != null && commissionMoney != "" && IsNumeric(commissionMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(commissionMoney);
        }
        //其他应付工资
        var otherPayMoney = document.getElementById("txtOtherPayMoney_" + row).value.Trim(); 
        if (otherPayMoney != null && otherPayMoney != "" && IsNumeric(otherPayMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(otherPayMoney);
        }
        totalSalaryMoney = parseInt(totalSalaryMoney * 100 + 0.5) / 100;
              
        //获取现在的工资合计
        var allMoney = document.getElementById("txtAllGetMoney_" + row).value.Trim();
        if (allMoney == "" || allMoney == null) allMoney = "0";
        //相差额
        var minusMoney =tofloat ( (totalSalaryMoney - allMoney),2);
        document.getElementById("txtAllGetMoney_" + row).value = totalSalaryMoney;
        
        //获取目前应发工资
        var factMoney = document.getElementById("txtFactSalaryMoney_" + row).value.Trim();
        //计算新的应发工资
        
        
	    /* 未考虑 NaN的情况 有时间再做 */
	    document.getElementById("txtFactSalaryMoney_" + row).value = tofloat(( parseFloat(factMoney) -parseFloat( minusMoney) ),2);
    }
}




function getAll()
{

    //获取工资项总数
    salaryCount = parseInt(document.getElementById("txtSalaryItemCount").value.Trim());

        //获取工资项个数

    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim());
    //遍历所有员工，校验每个员工的工资信息
    for (var i = 1; i <= userCount; i++)
    {
    var subMoney=0;
        //社会保险
       var  inputInsuranceSalary = 0;
       var a=document.getElementById("txtInsurance_" + i).value.Trim() ;
        //输入时校验输入的有效性
        if (a!= null && a != "" && IsNumeric(a, 10, 2))
        {
            inputInsuranceSalary = document.getElementById("txtInsurance_" + i).value.Trim();
        }
        //个人所得税
        var   inputIncomeTaxSalary = 0;
        var b=document.getElementById("txtIncomeTax_" + i).value.Trim();
        //输入时校验输入的有效性
        if (b != null && b!= "" &&IsNumeric(b, 10, 2))
        {
             inputIncomeTaxSalary = document.getElementById("txtIncomeTax_" + i).value.Trim();
        }
        //其他应扣款
       var  inputOtherMinusMoneySalary = 0;
        //输入时校验输入的有效性
        var c=document.getElementById("txtOtherMinusMoney_" + i).value.Trim() ;
        if (c!= null && c != "" && IsNumeric(c, 10, 2))
        {
         inputOtherMinusMoneySalary = document.getElementById("txtOtherMinusMoney_" + i).value.Trim();
        }
         //输入时校验输入的有效性
  
        
        salaryRowID = "txtFixedMoney_" + i + "_";
            var totalSalaryMoney = 0;
        //遍历所有工资输入
        for (var a = 1; a<= salaryCount; a++)
        {
            //工资额 
            salaryMoney = document.getElementById(salaryRowID + a).value.Trim();
            //是否扣款
            payFlag = document.getElementById("txtSalaryFlag_" + a).value.Trim();
            if (salaryMoney != null && salaryMoney != "" && IsNumeric(salaryMoney, 10, 2))
            {
                //扣款时
                if ("1" == payFlag)
                {
                    //计算总金额
//                    totalSalaryMoney -= parseFloat(salaryMoney);
                    subMoney +=parseFloat(salaryMoney);
                }
                //加款
                else
                {
                    //计算总金额
                    totalSalaryMoney += parseFloat(salaryMoney);
                }
           }
        }
        
             //计件工资
        var workMoney = document.getElementById("txtWorkMoney_" + i).value.Trim();
        if (workMoney != null && workMoney != "" && IsNumeric(workMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(workMoney);
        }
        //计时工资
        var timeMoney = document.getElementById("txtTimeMoney_" + i).value.Trim();
        if (timeMoney != null && timeMoney != "" && IsNumeric(timeMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(timeMoney);
        }
        //提成工资
        var commissionMoney = document.getElementById("txtCommissionMoney_" + i).value.Trim();
        if (commissionMoney != null && commissionMoney != "" && IsNumeric(commissionMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(commissionMoney);
        }
        //其他应付工资
        var otherPayMoney = document.getElementById("txtOtherPayMoney_" + i).value.Trim(); 
        if (otherPayMoney != null && otherPayMoney != "" && IsNumeric(otherPayMoney, 10, 2))
        {
            totalSalaryMoney += parseFloat(otherPayMoney);
        }
        
        
        
//        totalSalaryMoney = tofloat(totalSalaryMoney,2);                                                parseInt(totalSalaryMoney * 100 + 0.5) / 100;
         //var swMoney=totalSalaryMoney-parseFloat (subMoney);
        document.getElementById("txtAllGetMoney_" + i).value=tofloat(totalSalaryMoney,2);
        
//              var allKillMoney= 0;
//        var d=document.getElementById("txtAllGetMoney_" + i).value;
//        if (d != null && d != "" && IsNumeric(d, 10, 2))
//        {
//         allKillMoney = parseFloat( document.getElementById("txtAllGetMoney_" + i).value);
//        }
        
        
        
        
        
        
        
        var minus=parseFloat( inputInsuranceSalary)+parseFloat(inputIncomeTaxSalary )+parseFloat(inputOtherMinusMoneySalary);
        var last=totalSalaryMoney-minus-subMoney  ;
        document.getElementById("txtAllKillMoney_" + i).value=tofloat(minus+subMoney,2);
          document.getElementById("txtFactSalaryMoney_" + i).value=NumRound(last,2);
          
          // document.getElementById("txtFactSalaryMoney_" + i).value=NumRound(this,2) (last,2);
        
    }
//    lastALL();
}

function   tofloat(f,dec)   {     
  if(dec<0)   return   "Error:dec<0!";     
  result=parseInt(f)+(dec==0?"":".");     
  f-=parseInt(f);     
  if(f==0)     
  for(i=0;i<dec;i++)   result+='0';     
  else   {     
  for(i=0;i<dec;i++)   f*=10;     
  result+=parseInt(Math.round(f));     
  }     
  return   result;     
  }  
/*
* 扣除工资合计
*/
function CalculateMinusSalary(obj, row)
{ 
CalculateAddSalary(obj,row );
////debugger ;
//    //获取输入的值
//    var inputValue = obj.value;
//    //判断输入的小数点
//    var arr = inputValue.split(".");
//    if (arr.length <= 2)
//    {
//        index = inputValue.indexOf(".");
//        //输入的第一位是小数点时，前面加0
//        if (index == 0)
//        {
//            obj.value = "0" + inputValue;
//        }
//        //最后一位是小数点时
//        else if (index == inputValue.length - 1)
//        {
//            obj.value = inputValue.replace(".", "");
//        }
//        else if (index > -1)
//        {
//            obj.value = inputValue.substring(0, index + 3);
//        }
//        //定义变量
//        var totalSalaryMoney = 0;
//        //社会保险
//        var insurance = document.getElementById("txtInsurance_" + row).value;
//        if (insurance != null && insurance != "" && IsNumeric(insurance, 10, 2))
//        {
//            totalSalaryMoney += parseFloat(insurance);
//        }
//        //个人所得税
//        var incomeTax = document.getElementById("txtIncomeTax_" + row).value;
//        if (incomeTax != null && incomeTax != "" && IsNumeric(incomeTax, 10, 2))
//        {
//            totalSalaryMoney += parseFloat(incomeTax);
//        }
//        //其他应扣款
//        var otherMinusMoney = document.getElementById("txtOtherMinusMoney_" + row).value;
//        if (otherMinusMoney != null && otherMinusMoney != "" && IsNumeric(otherMinusMoney, 10, 2))
//        {
//            totalSalaryMoney += parseFloat(otherMinusMoney);
//        }
//        totalSalaryMoney = parseInt(totalSalaryMoney * 100 + 0.5) / 100;
//              
//        //获取现在的扣款合计
//        var allMoney = document.getElementById("txtAllKillMoney_" + row).value;
//        if (allMoney == "" || allMoney == null) allMoney = "0";
//        //相差额
//         
//        var minusMoney = tofloat (( totalSalaryMoney - allMoney),2);
//        document.getElementById("txtAllKillMoney_" + row).value = totalSalaryMoney;
//        
//        //获取目前应发工资
//        var factMoney = document.getElementById("txtFactSalaryMoney_" + row).value;
//        //计算新的应发工资        
//    
//	    /* 未考虑 NaN的情况 有时间再做 */
//	    document.getElementById("txtFactSalaryMoney_" + row).value = tofloat(( parseFloat(factMoney) -parseFloat( minusMoney) ),2);
//    }
}

function fnCheck()
{

    var fieldText = "";
    var msgText = "";
    var isFlag = true;


    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(!isFlag)
    {
    popMsgObj.Show(fieldText,msgText);
    } 
    return isFlag;   
}
/*
* 保存处理
*/
function DoSave()
{
  if(!fnCheck())
    return;
if (document .getElementById ("divCodeNo").innerHTML=="" || document .getElementById ("divCodeNo").innerHTML==null )
{
       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请生成报表！");
       return ;
}

    //校验基本信息
    if (CheckCreateInput("2"))
    {
        return;
    }
    //校验工资信息
    else if (CheckSalaryInput())
    {
        return;
    } 
    //获取基本信息参数
    var postParams = GetCreateParams("2");
    //获取工资信息参数
    postParams += GetSalaryParams();
    //生成工资报表
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryReport_Edit.ashx",
        dataType:"json",//返回json格式数据
        data: postParams,
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
                
                GetFlowButton_DisplayControl();
                document .getElementById ("txtReportStatus").value="待提交";
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
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
function CheckSalaryInput()
{ 
     //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;    
        
    //获取工资项总数
    salaryCount = parseInt(document.getElementById("txtSalaryItemCount").value.Trim());
    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim());
    //遍历所有员工，校验每个员工的工资信息
    for (var i = 1; i <= userCount; i++)
    {
        //获取员工名
        var emplName = document.getElementById("tdEmployeeName_" + i).innerHTML;
        //遍历所有工资项，校验工资项的值
        for (var j = 1; j <= salaryCount; j++)
        {
            //获取工资项的值
            var fixedMoney = document.getElementById("txtFixedMoney_" + i + "_" + j).value.Trim();
            //输入时校验输入的有效性
            if (fixedMoney != null && fixedMoney != "" && !IsNumeric(fixedMoney, 10, 2))
            {
                //获取工资项名称
                var salaryName = document.getElementById("txtSalaryName_" + j).value.Trim();
                //设置错误信息
                isErrorFlag = true;
                fieldText += emplName + " " + salaryName + "|";
                msgText += "请输入正确的工资值！|";
            }
        }
        //计件工资
        var inputSalary = document.getElementById("txtWorkMoney_" + i).value.Trim();
        //输入时校验输入的有效性
        if (inputSalary != null && inputSalary != "" && !IsNumeric(inputSalary, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 计件工资|";
            msgText += "请输入正确的计件工资！|";
        }
        //计时工资
        var inputSalary = document.getElementById("txtTimeMoney_" + i).value.Trim();
        //输入时校验输入的有效性
        if (inputSalary != null && inputSalary != "" && !IsNumeric(inputSalary, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 计时工资|";
            msgText += "请输入正确的计时工资！|";
        }
         //公司提成
        var  CompanyComMoney = document.getElementById("txtCompanyComMoney_" + i).value.Trim();
        //输入时校验输入的有效性
        if (CompanyComMoney != null && CompanyComMoney != "" && !IsNumeric(CompanyComMoney, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 公司提成|";
            msgText += "请输入正确的公司提成！|";
        }
        
            //部门提成
        var  DeptComMoney = document.getElementById("txtDeptComMoney_" + i).value.Trim();
        //输入时校验输入的有效性
        if (DeptComMoney != null && DeptComMoney != "" && !IsNumeric(DeptComMoney, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 部门提成|";
            msgText += "请输入正确的部门提成！|";
        }
              //个人业务提成
        var  PersonComMoney = document.getElementById("txtPersonComMoney_" + i).value.Trim();
        //输入时校验输入的有效性
        if (PersonComMoney != null && PersonComMoney != "" && !IsNumeric(PersonComMoney, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 个人业务提成|";
            msgText += "请输入正确的个人业务提成！|";
        }
        
        //产品单品提成工资
        inputSalary = document.getElementById("txtCommissionMoney_" + i).value.Trim();
        //输入时校验输入的有效性
        if (inputSalary != null && inputSalary != "" && !IsNumeric(inputSalary, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 产品单品提成|";
            msgText += "请输入正确的产品单品提成！|";
        }
         //绩效提成
        var  PerformanceMoney = document.getElementById("txtPerformanceMoney_" + i).value.Trim();
        //输入时校验输入的有效性
        if (PerformanceMoney != null && PerformanceMoney != "" && !IsNumeric(PerformanceMoney, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 绩效工资|";
            msgText += "请输入正确的绩效工资！|";
        }
        //其他应付工资
        inputSalary = document.getElementById("txtOtherPayMoney_" + i).value.Trim();
        //输入时校验输入的有效性
        if (inputSalary != null && inputSalary != "" && !IsNumeric(inputSalary, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 其他应付工资|";
            msgText += "请输入正确的其他应付工资！|";
        }
        //社会保险
        inputSalary = document.getElementById("txtInsurance_" + i).value.Trim();
        //输入时校验输入的有效性
        if (inputSalary != null && inputSalary != "" && !IsNumeric(inputSalary, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 社会保险|";
            msgText += "请输入正确的社会保险！|";
        }
        //个人所得税
        inputSalary = document.getElementById("txtIncomeTax_" + i).value.Trim();
        //输入时校验输入的有效性
        if (inputSalary != null && inputSalary != "" && !IsNumeric(inputSalary, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 个人所得税|";
            msgText += "请输入正确的个人所得税！|";
        }
        //其他应扣款
        inputSalary = document.getElementById("txtOtherMinusMoney_" + i).value.Trim();
        //输入时校验输入的有效性
        if (inputSalary != null && inputSalary != "" && !IsNumeric(inputSalary, 10, 2))
        {
            //设置错误信息
            isErrorFlag = true;
            fieldText += emplName + " 其他应扣款|";
            msgText += "请输入正确的其他应扣款！|";
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
* 输入信息校验
*/
function GetSalaryParams()
{        
    //获取工资项总数
    salaryCount = parseInt(document.getElementById("txtSalaryItemCount").value.Trim());
    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim()); 
    //参数定义
    var strParams = "&UserCount=" + userCount + "&SalaryCount=" + salaryCount;
    
    //遍历所有员工，校验每个员工的工资信息
    for (var i = 1; i <= userCount; i++)
    {
        //员工ID
        strParams += "&EmployeeID_" + i + "=" + document.getElementById("txtEmplID_" + i).value.Trim();
        //遍历所有工资项，校验工资项的值
        for (var j = 1; j <= salaryCount; j++)
        {
            //设置员工工资项工资
            strParams += "&FixedMoney_" + i + "_" + j + "=" + document.getElementById("txtFixedMoney_" + i + "_" + j).value.Trim();
        }
        //计件工资
        strParams += "&WorkMoney_" + i + "=" + document.getElementById("txtWorkMoney_" + i).value.Trim();
        //计时工资
        strParams += "&TimeMoney_" + i + "=" + document.getElementById("txtTimeMoney_" + i).value.Trim();
           //公司提成
        strParams += "&CompanyComMoney_" + i + "=" + document.getElementById("txtCompanyComMoney_" + i).value.Trim();
           //部门提成
        strParams += "&DeptComMoney_" + i + "=" + document.getElementById("txtDeptComMoney_" + i).value.Trim();
           //个人业务提成
        strParams += "&PersonComMoney_" + i + "=" + document.getElementById("txtPersonComMoney_" + i).value.Trim();
           //绩效工资
        strParams += "&PerformanceMoney_" + i + "=" + document.getElementById("txtPerformanceMoney_" + i).value.Trim();
        //产品单品提成
        strParams += "&CommissionMoney_" + i + "=" + document.getElementById("txtCommissionMoney_" + i).value.Trim();
        //其他应付工资
        strParams += "&OtherPayMoney_" + i + "=" + document.getElementById("txtOtherPayMoney_" + i).value.Trim();
        //应付工资合计
        strParams += "&AllGetMoney_" + i + "=" + document.getElementById("txtAllGetMoney_" + i).value.Trim();
        //社会保险
        strParams += "&Insurance_" + i + "=" + document.getElementById("txtInsurance_" + i).value.Trim();
        //个人所得税
        strParams += "&IncomeTax_" + i + "=" + document.getElementById("txtIncomeTax_" + i).value.Trim();
        //其他应扣款
        strParams += "&OtherMinusMoney_" + i + "=" + document.getElementById("txtOtherMinusMoney_" + i).value.Trim();
        //应扣款合计
        strParams += "&AllKillMoney_" + i + "=" + document.getElementById("txtAllKillMoney_" + i).value.Trim();
        //实发工资额
        strParams += "&FactSalaryMoney_" + i + "=" + document.getElementById("txtFactSalaryMoney_" + i).value.Trim();
        
    }
    
    //设置工资项ID
    for(var x = 1; x <= salaryCount; x++)
    {
        //设置工资项ID
        strParams += "&SalaryItemID_" + x + "=" + document.getElementById("txtSalaryID_" + x).value.Trim();   
    }
    
    //返回参数字符串
    return strParams;
}
/*
* 返回按钮
*/
function DoBack()
{
  var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['isLiebiao']; //是否从列表页面进入
        if (flag != null && typeof (flag) != "undefined") {
  searchCondition = document.getElementById("hidSearchCondition").value.Trim();
    //获取模块功能ID
    moduleID = document.getElementById("hidModuleID").value;
    window.location.href = "SalaryReport_Info.aspx?ModuleID=" + moduleID + searchCondition;
        }
        else {
            var intFromType = requestObj['intFromType']; //是否从列表页面进入           
            var ListModuleID = requestObj['ListModuleID']; //来源页MODULEID       
            if (intFromType == 2) {
                window.location.href = '../../../DeskTop.aspx';
            }
            if (intFromType == 3) {
                window.location.href = '../../Personal/WorkFlow/FlowMyApply.aspx?ModuleID=' + ListModuleID;
            }
            if (intFromType == 4) {
                window.location.href = '../../Personal/WorkFlow/FlowMyProcess.aspx?ModuleID=' + ListModuleID;
            }
            if (intFromType == 5) {
                window.location.href = '../../Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID=' + ListModuleID;
            }

        }
    }
//if(document.getElementById("hidIsliebiao").value == "")
//    {
//        window.location.href='../../../Desktop.aspx';
//    }
//    else
//    {

//    //获取查询条件
//    searchCondition = document.getElementById("hidSearchCondition").value;
//    //获取模块功能ID
//    moduleID = document.getElementById("hidModuleID").value;
//    window.location.href = "SalaryReport_Info.aspx?ModuleID=" + moduleID + searchCondition;
//    }
}


function Fun_UnConfirmOperate()
{
    var ID=$("#txtIdentityID").val();
  var status ='0';
       var postParams = "Action=UpdateMoveApplyCancelConfirmInfo&ID="+ID +"&Status=" + status + "&ReportNo=" + document.getElementById("divCodeNo").innerHTML;
    //审批处理
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryReport_Edit.ashx",
        dataType:"json",//返回json格式数据
        data: postParams,
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
           if(data.sta==1) 
                { 
//                document .getElementById ("btnSave").style.display="block";
 document.getElementById("btnSave").src = "../../../Images/Button/Bottom_btn_save.jpg";
                document.getElementById("btnSave").attachEvent("onclick", DoSave);
              
         document .getElementById ("ImgReBuild").src = "../../../Images/Button/btn_qxsc.jpg";
                 document.getElementById("ImgReBuild").attachEvent("onclick", DoDelete);
                     CancelConfirm(data.info);
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认成功！");
                }else
                {
                      hidePopup();
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认失败！");
                } 
        
        
        //显示保存按钮
//           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认成功！");
//           CancelConfirm(id)
        } 
    }); 




}

//取消确认后处理
function CancelConfirm(id)
{
    $("#hidIdentityID").val(id);
    $("#hiddenBillStatus").attr("value","1");
    GetFlowButton_DisplayControl();
}

function     Fun_ConfirmOperate()
 { 
 
    if(!confirm("是否确认?"))
      {
      return ;
      }
   var status ='3';
     var postParams = "Action=FlowOperate&Status=" + status + "&ReportNo=" + document.getElementById("divCodeNo").innerHTML;
    //审批处理
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryReport_Edit.ashx",
        dataType:"json",//返回json格式数据
        data: postParams,
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！");
           $("#txtBillStatus").val(2);
            GetFlowButton_DisplayControl();
        } 
    }); 
 
 
 }

/*
* 提交审批
*/
function Fun_FlowApply_Operate_Succeed(operateType)
{
    var status = "2";
      //提交审批成功后
    if (operateType == 0)
    {
   
        status = "2";
//         document .getElementById ("btnSave").style.display="none";
//           document .getElementById ("btnUnSave").style.display="Block";
            document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";
                document.getElementById("btnSave").attachEvent("onclick", gogo);
               document .getElementById ("ImgReBuild").src = "../../../Images/Button/UnClick_qxsc.jpg";
                 document.getElementById("ImgReBuild").attachEvent("onclick", gogo2);
              
    }
    //审批成功后
     else   if (operateType == 1)
    {
        status = "1";
//         document .getElementById ("btnSave").style.display="none";
//            document .getElementById ("btnUnSave").style.display="Block";
document .getElementById ("txtBillStatus").value="1";
               document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";
                document.getElementById("btnSave").attachEvent("onclick", gogo);
            document .getElementById ("ImgReBuild").src = "../../../Images/Button/UnClick_qxsc.jpg";
                 document.getElementById("ImgReBuild").attachEvent("onclick", gogo2);
    }//审批不通过 成功后
    else if (operateType == 2)
    {
        status = "1";
//        document .getElementById ("btnSave").style.display="none";
////           document .getElementById ("btnUnSave").style.display="Block";
              document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";
                document.getElementById("btnSave").attachEvent("onclick", gogo);
                  document .getElementById ("ImgReBuild").src = "../../../Images/Button/UnClick_qxsc.jpg";
                 document.getElementById("ImgReBuild").attachEvent("onclick", gogo2);
    }//撤消审批 成功后
    else if (operateType == 3)
    {
        status = "1";
//        document .getElementById ("btnSave").style.display="block";
       document.getElementById("btnSave").src = "../../../Images/Button/Bottom_btn_save.jpg";
                document.getElementById("btnSave").attachEvent("onclick", DoSave);
                
//           document .getElementById ("btnUnSave").style.display="Block";
       
             document .getElementById ("ImgReBuild").src = "../../../Images/Button/btn_qxsc.jpg";
                 document.getElementById("ImgReBuild").attachEvent("onclick", DoDelete);
        
    }
    var postParams = "Action=FlowOperate&Status=" + status + "&ReportNo=" + document.getElementById("divCodeNo").innerHTML;
    //审批处理
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryReport_Edit.ashx",
        dataType:"json",//返回json格式数据
        data: postParams,
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
        } 
    }); 
}

function gogo()
{

}
function gogo2()
{

}

//function yearChange()
//{
//alert (document .getElementById ("ddlYear").value);
//}