$(document).ready(function(){
 
    requestobj = GetRequest();
    recordnoparam = requestobj['type'];
    if(recordnoparam!=null)
    {
        ChangePanel(recordnoparam);
    }
    else
    {
        DoSearch();
    }
  
});

/*
* 改变显示页面
*/
function ChangePanel(flag)
{
 
    //计件工资
    if (flag == "1")
    {
        closeRoyaltydiv();
closeTimediv();
closeTemplatediv();
        //设置标题 
        document.getElementById("divSalaryTitle").innerHTML = "计件项目";
               document.getElementById("itemName").innerHTML = "计件工资";
        
        document.getElementById("divNewSalaryTitle").innerHTML = "计件项目<span class='redbold'>*</span>";
        document.getElementById("divAmountTitle").innerHTML = "数量<span class='redbold'>*</span>";
        //计件工资可操作
        document.getElementById("divPiece").style.display = "block";
        document.getElementById("divNewPiece").style.display = "block";
        //计时工资隐藏
        document.getElementById("divTime").style.display = "none";
        document.getElementById("divNewTime").style.display = "none";
        //提成工资隐藏
        document.getElementById("divCommission").style.display = "none";
        document.getElementById("divNewCommission").style.display = "none";
          document.getElementById("tdMessage").innerHTML = "单价";
    }
    //计时工资
    else if (flag == "2")
    {
       closeRoyaltydiv();
closeTimediv();
closeTemplatediv();
        //设置标题
        document.getElementById("divSalaryTitle").innerHTML = "计时项目";
                document.getElementById("itemName").innerHTML = "计件工资";
        document.getElementById("divNewSalaryTitle").innerHTML = "计时项目<span class='redbold'>*</span>";
        document.getElementById("divAmountTitle").innerHTML = "时长<span class='redbold'>*</span>";
        //计件工资隐藏
        document.getElementById("divPiece").style.display = "none";
        document.getElementById("divNewPiece").style.display = "none";
        //计时工资可操作
        document.getElementById("divTime").style.display = "block";
        document.getElementById("divNewTime").style.display = "block";
        //提成工资隐藏
        document.getElementById("divCommission").style.display = "none";
        document.getElementById("divNewCommission").style.display = "none";
         document.getElementById("tdMessage").innerHTML = "单价";
         
    }
    //提成工资
    else if (flag == "3")
    {
        //设置标题
            closeRoyaltydiv();
closeTimediv();
closeTemplatediv();
        document.getElementById("divSalaryTitle").innerHTML = "提成项目";
                document.getElementById("itemName").innerHTML = "产品单品提成";
        document.getElementById("divNewSalaryTitle").innerHTML = "提成项目<span class='redbold'>*</span>";
        document.getElementById("divAmountTitle").innerHTML = "业务量<span class='redbold'>*</span>";
        //计件工资隐藏
        document.getElementById("divPiece").style.display = "none";
        document.getElementById("divNewPiece").style.display = "none";
        //计时工资隐藏
        document.getElementById("divTime").style.display = "none";
        document.getElementById("divNewTime").style.display = "none";
        //提成工资可操作
        document.getElementById("divCommission").style.display = "block";
        document.getElementById("divNewCommission").style.display = "block";
         document.getElementById("tdMessage").innerHTML = "提成率(%)";
        
    }
    //设置操作的工资项
    document.getElementById("txtOprateSalary").value = flag;
    //清空列表信息
    document.getElementById("divFloatSalaryDetail").innerHTML = "";
    DoSearch();
}

/*
* 查询操作
*/
function DoSearch()
{

var isFlag=true ;
 var fieldText="";
    var msgText="";
   var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
   		 
   		    
    }
         if(!isFlag)
    {
 document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").style.top = "240px";
	    document.getElementById("spanMsg").style.left = "450px";
	    document.getElementById("spanMsg").style.width = "290px";
	    document.getElementById("spanMsg").style.position = "absolute";
	    document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
      return;
    }
    
    /* 获取参数 */
    //操作工资项
    postParam = "Action=Search";
    //操作工资项
    postParam += "&Flag=" + escape ( document.getElementById("txtOprateSalary").value.Trim());
    //员工编号
    postParam += "&EmployeeNo=" + escape ( document.getElementById("txtEmployeeNo").value.Trim());
    //员工姓名
    postParam += "&EmployeeName=" + escape ( document.getElementById("txtEmployeeName").value.Trim());
    //所在岗位
    postParam += "&QuarterID=" + escape ( document.getElementById("ddlQuarter").value.Trim());
    //计件项目
    postParam += "&PieceworkNo=" + escape ( document.getElementById("txtSearchPiece").title);
    //计时项目
    postParam += "&TimeNo=" +escape (  document.getElementById("txtSearchTime").title);
    //提成项目
    postParam += "&CommissionNo=" + escape ( document.getElementById("txtSearchRoyalty").title);
    //开始日期
    postParam += "&StartDate=" + escape ( document.getElementById("txtStartDate").value.Trim());
    //结束日期
    postParam += "&EndDate=" + escape ( document.getElementById("txtEndDate").value.Trim());
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/InputFloatSalary.ashx',
        data : postParam,//目标地址
        dataType:"string",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(insuInfo)
        {
            //设置工资项内容
            document.getElementById("divFloatSalaryDetail").innerHTML = insuInfo;
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete:function(){
            hidePopup();
        }
    });
}



function getChage( obj)
{

if (obj.checked==false )
{
 document .getElementById ("chkCheckAll").checked=false ;
}
}
/*
* 计算工资合计
*/
function CalculateTotalSalary(obj, i, j, x)
{
    //获取输入的值
    inputValue = obj.value;
    if (inputValue == null || inputValue == "") return;
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
        //获取单价
        unitPrice = parseFloat(document.getElementById("tdUnitPrice_" + i + "_" + j).innerHTML);
        //获取金额
        timeMoney = parseFloat(document.getElementById("tdAmountMoney_" + i + "_" + j + "_" + x).innerHTML);
        //获取小计
        timeTotalMoney = parseFloat(document.getElementById("tdAmountTotalMoney_" + i + "_" + j).innerHTML);
        //获取合计
        totalMoney = parseFloat(document.getElementById("tdTotalMoney_" + i).innerHTML);
        
        //计算新的金额
        tempMoney = 0;
        //提成
        if ("3" == document.getElementById("txtOprateSalary").value)
        {
            tempMoney = unitPrice * obj.value/100;
        }
        else
        {
            tempMoney = unitPrice * obj.value;
        }
        //计算差额
        backMoney = timeMoney- tempMoney;
        
        //设置金额
        document.getElementById("tdAmountMoney_" + i + "_" + j + "_" + x).innerHTML =tofloat( tempMoney,2);
        //设置小计
        document.getElementById("tdAmountTotalMoney_" + i + "_" + j).innerHTML = tofloat(timeTotalMoney - backMoney,2);
        //设置合计
        document.getElementById("tdTotalMoney_" + i).innerHTML = tofloat(totalMoney - backMoney,2);
    }
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
* 保存操作
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
        url: "../../../Handler/Office/HumanManager/InputFloatSalary.ashx",
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
                //设置提示信息      
                  DoSearch();
               // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "保存成功!|");
          
            }
            else 
            { 
                hidePopup();
                   document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "保存失败,请确认!|");
             //   showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
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
        
    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value);
  //  alert (userCount);
    if (!IsNumber( document.getElementById("txtUserCount").value.Trim() ))
    {
           isErrorFlag = true;
       fieldText += "提示|";
        msgText += "请新建项！|";
     
    }
    else
    {
    //获取当前操作的浮动工资项
    flag = document.getElementById("txtOprateSalary").value.Trim();
    //遍历所有员工，校验每个员工的工资信息
    for (var i = 1; i <= userCount; i++)
    {
        //获取人员的项目总数 
        itemCount = parseInt(document.getElementById("txtItemCount_" + i).value.Trim());
        //遍历所有工资项，校验工资项的具体值
        for (var j = 1; j <= itemCount; j++)
        {
            //获取人员的项目总数 
            salaryCount = parseInt(document.getElementById("txtSalaryCount_" + i + "_" + j).value.Trim());
            for (var x = 1; x <= salaryCount; x++)
            {
                //获取工资项的值
                salaryAmount = document.getElementById("txtAmount_" + i + "_" + j + "_" + x).value.Trim(); 
                //输入时校验输入的有效性
                if (salaryAmount == null || salaryAmount == "")
                {
                    //设置错误信息
                    isErrorFlag = true;
                    if ("1" == flag)
                    {
                        fieldText += "数量|";
                        msgText += "请输入数量！|";
                    }
                    else if ("2" == flag)
                    {
                        fieldText += "时长|";
                        msgText += "请输入时长！|";
                    }
                    else if ("3" == flag)
                    {
                        fieldText += "业务量|";
                        msgText += "请输入业务量！|";
                    }
                }
                else
                {
                    if ("1" == flag)
                    {
                  
                       if (!IsNumeric(salaryAmount, 8, 2))
                        {
                            //设置错误信息
                            isErrorFlag = true;
                            fieldText += "数量|";
                            msgText += "请输入正确的数量！|";
                        }
                    }
                    else if ("2" == flag)
                    {
                        if (!IsNumeric(salaryAmount, 2, 2))
                        {
                            //设置错误信息
                            isErrorFlag = true;
                            fieldText += "时长|";
                            msgText += "请输入正确的时长！|";   
                        }
                    }
                    else if ("3" == flag)
                    {
                        if (!IsNumeric(salaryAmount, 10, 2))
                        {
                            //设置错误信息
                            isErrorFlag = true;
                            fieldText += "业务量|";
                            msgText += "请输入正确的业务量！|";
                        }
                    }
                }
            }
            
        }
        
    }
    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        //popMsgObj.Show(fieldText, msgText);
        
	    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
    }

    return isErrorFlag;
}

///////创建层（）
function CreateErrorMsgDiv(fieldText, msgText)
{
    errorMsg = "";
    if(fieldText != null && fieldText != "" && msgText != null && msgText != "")
    {
        var fieldArray = fieldText.split("|");
        var alertArray = msgText.split("|");
        for(var i = 0; i < fieldArray.length - 1; i++)
        {
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

/*
* 获取提交的基本信息
*/
function GetPostParams()
{
    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim());
    //获取当前操作的浮动工资项
    flag = document.getElementById("txtOprateSalary").value.Trim();
    //参数定义
    var strParams = "Action=Update&UserCount=" + userCount + "&Flag=" + flag;
    //遍历所有员工，校验每个员工的工资信息
    for (var i = 1; i <= userCount; i++)
    {
        //获取员工ID
        strParams += "&EmplID_" + i + "=" + document.getElementById("txtEmplID_" + i).value.Trim();
        //获取人员的项目总数 
        itemCount = parseInt(document.getElementById("txtItemCount_" + i).value.Trim());
        //设置人员项目的总数
        strParams += "&EmplItemCount_" + i + "=" + itemCount;
        //遍历所有工资项，校验工资项的具体值
        for (var j = 1; j <= itemCount; j++)
        {
            //设置员工工资项工资
            strParams += "&SalaryNo_" + i + "_" + j + "=" + document.getElementById("txtSalaryNo_" + i + "_" + j).value.Trim();
            //获取人员的项目总数 
            salaryCount = parseInt(document.getElementById("txtSalaryCount_" + i + "_" + j).value.Trim());
            //设置项目的记录总数
            strParams += "&ItemSalaryCount_" + i + "_" + j + "=" + salaryCount;
            for (var x = 1; x <= salaryCount; x++)
            {
                //设置工资项的值
                strParams += "&SalaryAmount_" + i + "_" + j + "_" + x + "=" + document.getElementById("txtAmount_" + i + "_" + j + "_" + x).value.Trim();
                //设置金额
                strParams += "&AmountMoney_" + i + "_" + j + "_" + x + "=" + document.getElementById("tdAmountMoney_" + i + "_" + j + "_" + x).innerHTML;                
                //设置日期
                strParams += "&SalaryDate_" + i + "_" + j + "_" + x + "=" + document.getElementById("tdSalaryDate_" + i + "_" + j + "_" + x).innerHTML;
            }
            
        }
        
    }
    
    //返回参数字符串
    return strParams;
}

/*
* 返回处理
*/
function DoBack()
{
    //隐藏页面
    document.getElementById("divNewInputPage").style.display = "none";
    
   flag = document.getElementById("txtOprateSalary").value.Trim();
 
    if ("3" == flag)
    {
      document.getElementById("txtRoyalty").value="";
      document.getElementById("txtNewUnitPrice").value="0";
    }
    
}

/*
* 新建处理
*/
function DoNew()
{
    //显示页面
    document.getElementById("divNewInputPage").style.display = "block";
    //设置编辑模式
    document.getElementById("hidEditFlag").value = "0";
    //人员编号
    document.getElementById("txtNewEmployeeID").value = "";
    document.getElementById("txtNewEmployeeNo").value = "";
    //姓名
    document.getElementById("txtNewEmployeeName").value = "";
    //部门
    document.getElementById("txtNewDept").value = "";
    //岗位
    document.getElementById("txtNewQuarter").value = "";
    //岗位职等
    document.getElementById("txtNewQuarterLevel").value = "";
    //日期
    document.getElementById("txtInputDate").value = "";
    //项目
     document .getElementById ("txtPiece").value='';
        document .getElementById ("txtPiece").title='';
            document .getElementById ("txtTime").value='';
        document .getElementById ("txtTime").title='';
            document .getElementById ("txtRoyalty").value='';
        document .getElementById ("txtRoyalty").title='';
    //单价
    document.getElementById("txtNewUnitPrice").value = "0";
    //数量
    document.getElementById("txtNewAmount").value = "";
    //金额
    document.getElementById("txtNewSalaryTotal").value = "";
    
}

/*
* 选择人员
*/
function SelectEmployeeInfo()
{
    //设置div的位置
    var selectEmployee = "";
    // 整个div的大小和位子
    selectEmployee += "<div id='divSelectEmployee' style='background-color:white;z-index:11;position:absolute;width:850px '  class=\"checktable\" >";
    // 白色div中的信息
    selectEmployee += "<table cellpadding='0' cellspacing='1' border='0' class='border' align=left>";
    selectEmployee += "<tr><td>";
    selectEmployee += "<iframe src='SelectEmployeeInfo.aspx' scrolling=no width='850' height='550' frameborder='0'></iframe>";
    selectEmployee += "</td></tr>";
    selectEmployee += "</table>";
    //--end
    selectEmployee += "</div>";
    insertHtml("afterBegin", document.body, selectEmployee);
    CenterToDocument('divSelectEmployee',false );
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
        document.getElementById("txtNewEmployeeID").value = employee.EmployeeID;
        //人员编号
        document.getElementById("txtNewEmployeeNo").value = employee.EmployeeNo;
        //员工姓名
        document.getElementById("txtNewEmployeeName").value = employee.EmployeeName;
        //部门
        document.getElementById("txtNewDept").value = employee.DeptName;
        //岗位
        document.getElementById("txtNewQuarter").value = employee.QuarterName;
        //岗位职等
        document.getElementById("txtNewQuarterLevel").value = employee.AdminLevelName;
    }
    //选择页面不可见
    document.getElementById("divSelectEmployee").style.display = "none";
}

/*
* 保存新建的工资内容
*/
function DoNewSave()
{
    /* 页面校验 */
     
     //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //员工编号
    emplID = document.getElementById("txtNewEmployeeID").value.Trim();
    if (emplID == "" || emplID == null)
    {
        isErrorFlag = true;
        fieldText += "员工编号|";
        msgText += "请输入员工编号|";
    }
    //日期
    inputDate = document.getElementById("txtInputDate").value.Trim();
    if (inputDate == "" || inputDate == null)
    {
        isErrorFlag = true;
        fieldText += "日期|";
        msgText += "请输入日期|";
    } 
     SalaryTotal=document.getElementById("txtNewSalaryTotal").value;
    if(SalaryTotal=="0.00")
    {
      isErrorFlag = true;
       fieldText += "金额|";
        msgText += "金额必须大于0|";
    }
    //标识
    flag = document.getElementById("txtOprateSalary").value.Trim();
    //数量
    amount = document.getElementById("txtNewAmount").value.Trim();
    
    var piecework = "";
    var timeno = "";
    var commission = "";
    //计件
    if ("1" == flag)
    {
        //计件项目
        piecework = document.getElementById("txtPiece").value.Trim();
        
        if (piecework == "" || piecework == null)
        {
            isErrorFlag = true;
            fieldText += "计件项目|";
            msgText += "请选择计件项目|";
        }
           piecework = document.getElementById("txtPiece").title;
        if (amount == null || amount == "")
        {
            isErrorFlag = true;
            fieldText += "数量|";
            msgText += "请输入数量|";
        }
          else if (!IsNumeric(amount, 8, 2))
        {
            isErrorFlag = true;
            fieldText += "数量|";
            msgText += "请输入正确的数量|";
        }
    }
    //计时
    else if ("2" == flag)
    {
        //计时项目
        timeno = document.getElementById("txtTime").value.Trim();
        if (timeno == "" || timeno == null)
        {
            isErrorFlag = true;
            fieldText += "计时项目|";
            msgText += "请选择计时项目|";
        }
           timeno = document.getElementById("txtTime").title;
        if (amount == null || amount == "")
        {
            isErrorFlag = true;
            fieldText += "时长|";
            msgText += "请输入时长|";
        }
        else if (!IsNumeric(amount, 12, 2))
        {
            isErrorFlag = true;
            fieldText += "时长|";
            msgText += "请输入正确的时长|";
        }
    }
    //提成
    else if ("3" == flag)
    {
        //提成项目
        commission = document.getElementById("txtRoyalty").value.Trim();
        if (commission == "" || commission == null)
        {
            isErrorFlag = true;
            fieldText += "提成项目|";
            msgText += "请选择提成项目|";
        }
             commission = document.getElementById("txtRoyalty").title;
        if (amount == null || amount == "")
        {
            isErrorFlag = true;
            fieldText += "业务量|";
            msgText += "请输入业务量|";
        }
        else if (!IsNumeric(amount, 12, 2))
        {
            isErrorFlag = true;
            fieldText += "业务量|";
            msgText += "请输入正确的业务量|";
        }
    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
      //  popMsgObj.Show(fieldText,msgText);
        	    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
        
        return;
    }
//    alert (piecework);
    params = "Action=New&Flag=" +escape ( flag )+ "&EmplID=" + escape ( emplID )+ "&InputDate=" +escape (  inputDate )+ "&PieceNo=" +escape (  piecework) + "&TimeNo=" + escape ( timeno)
            + "&CommissionNo=" + commission + "&Amount=" + amount + "&TotalMoney=" + escape(document.getElementById("txtNewSalaryTotal").value.Trim())
            + "&EditFlag=" + escape(document.getElementById("hidEditFlag").value.Trim());
    
    //
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/InputFloatSalary.ashx",
        data : params,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
                 	    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "请求发生错误！|");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            { 
                //设置编辑模式
                document.getElementById("hidEditFlag").value = "1";
                //设置提示信息
             
              
                 //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");  
                 document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "保存成功！|");
                 DoSearch();  
            }
            else 
            { 
                hidePopup();
               // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
                     document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "保存失败,请确认！|");
            } 
        } 
    });    
}

/*
* 工资项目变更时
*/
function ChangeItemNo()
{
    //
    flag = document.getElementById("txtOprateSalary").value.Trim();
    //
    itemNo = "";
    if ("1" == flag)
    {
        itemNo = document.getElementById("txtPiece").title;
    }
    else if ("2" == flag)
    {
        itemNo = document.getElementById("txtTime").title;
    }
    else if ("3" == flag)
    {
        itemNo = document.getElementById("txtRoyalty").title;
        if(itemNo=="默认")itemNo='?#Flag?#';//区别标志
        if(document.getElementById("txtRoyalty").value=="")
        {
         document.getElementById("txtNewUnitPrice").value="0";
         return;
        }
        else
        {
          $.ajax({
        type: "POST",//用POST方式传输
        url:  "../../../Handler/Office/HumanManager/InputFloatSalary.ashx",//目标地址
        data : "Action=Price&Flag=" + flag + "&ItemNo=" + itemNo,
        dataType:"string",//数据格式:string
        cache:false,
        success: function(price)
        {
            //设置单价
            document.getElementById("txtNewUnitPrice").value = price;
            //计算金额
            CaculateMoney();
        }
    });
    return;
        }
    }
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  "../../../Handler/Office/HumanManager/InputFloatSalary.ashx",//目标地址
        data : "Action=Price&Flag=" + flag + "&ItemNo=" + itemNo,
        dataType:"string",//数据格式:string
        cache:false,
        success: function(price)
        {
            //设置单价
            document.getElementById("txtNewUnitPrice").value = price;
            //计算金额
            CaculateMoney();
        }
    });
}

/*
* 计算金额
*/
function CaculateMoney()
{
    //获取单价
    unitPrice = document.getElementById("txtNewUnitPrice").value.Trim();
    //获取数量
    
    var obj=document.getElementById("txtNewAmount");
      inputValue = obj.value;
    if (inputValue == null || inputValue == "") return;
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
        }
    
    amount = document.getElementById("txtNewAmount").value.Trim();
    
    
    
    
    
    
    
    
    
    if (unitPrice == null || unitPrice == "" || amount == null || amount == "") return;
    if (IsNumeric(unitPrice, 10, 2) && IsNumeric(unitPrice, 12, 2))
    {
        //计算金额
        var totalMoney = parseFloat(unitPrice) * parseFloat(amount);
        
        //提成
        if ("3" == document.getElementById("txtOprateSalary").value.Trim())
        {
            totalMoney = totalMoney/100;
        }
      
        //设置金额 t
        document.getElementById("txtNewSalaryTotal").value = tofloat ( ((totalMoney * 100 ) / 100),2)  ;
        
        
    }
}



function DoDelete()
{
    //获取选择框
      if(confirm("删除后不可恢复，确认删除吗！"))
      {

 var chkValue="";
        table = document.getElementById("tblInsuDetail");
            var flag=   document.getElementById("txtOprateSalary").value.Trim() ;
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	
	  chkControl = document.getElementById("chkSelect_" + row);
    if (chkControl )
    {
	    //如果控件为选中状态，删除该行，并对之后的行改变控件名称
	    if (chkControl.checked)
	    {
	    	var empid="";
	        if (flag =="1"||flag =="2"||flag =="3")
	        {
	           empid=document .getElementById ("txtEmplID_"+row ).value.Trim();
	         }
	    	if (flag =="1"||flag =="2"||flag =="3")
	        {
	         chkValue=chkValue+empid+",";
	         }
           
	    }
	  }
	}
    
    
    
    var deleteNos = chkValue.substring(0, chkValue.length - 1);
    selectLength = chkValue.split("',");
    if(chkValue == "" || chkValue == null || selectLength.length < 1)
    {
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项删除！");
               document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "请至少选择一项删除！|");
        return;
    }
    else
    {
        var postParam = "Action=DeleteInfo&flag="+escape (flag )+"&DeleteNO=" + escape(deleteNos);
        //删除
        $.ajax(
        { 
            type: "POST",
            url: "../../../Handler/Office/HumanManager/InputFloatSalary.ashx?" + postParam,
            dataType:'json',//返回json格式数据
            cache:false,
            beforeSend:function()
            { 

            },
            error: function() 
            {
              ///  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                // popMsgObj.ShowMsg('请求发生错误！');
                            document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "请求发生错误！|");
            }, 
            success:function(data) 
            { 
                if(data.sta == 1) 
                { 
                    // 
                    // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                         document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "删除成功！|");
               //       popMsgObj.ShowMsg('删除成功！');
                      DoSearch();
                    //
                  
                } 
                else if(data.sta == 2) 
                {
                    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                     popMsgObj.ShowMsg('类型已被使用 ,请确认！');
                }
                else
                {
                    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "删除失败,请确认！|");
                   // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                   //  popMsgObj.ShowMsg('删除失败 ,请确认！');
                } 
            } 
        });
    }
    }
}