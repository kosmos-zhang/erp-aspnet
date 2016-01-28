$(document).ready(function(){
  DoSearch();
});
/*
* 计算工资合计
*/
function CalculateTotalSalary(obj, row)
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
        else if (index > -1)
        {
            obj.value = inputValue.substring(0, index + 3);
        }
        //获取工资ID相同部门
        salaryRowID = "txtSalaryMoney_" + row + "_";
        //获取工资项个数
        salaryCount = parseInt(document.getElementById("txtSalaryCount").value.Trim());
        //定义变量
        var totalSalaryMoney = 0;
        //遍历所有工资输入
        for (var i = 1; i <= salaryCount; i++)
        {
            //工资额 
            salaryMoney = document.getElementById(salaryRowID + i).value.Trim();
            //是否扣款
            payFlag = document.getElementById("txtPayFlag_" + row + "_" + i).value.Trim();
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
	    //设置面试总成绩
	    /* 未考虑 NaN的情况 有时间再做 */
	    document.getElementById("txtTotalSalary_" + row).value = parseInt(totalSalaryMoney * 100 + 0.5) / 100;
    }
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
    postParams = "Action=Save&" + GetPostParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/InputSalaryFixed.ashx",
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
            if(data.EditStatus == 1) 
            { 
                //设置工资项信息
                $.each(data.DataInfo,function(i,item)
                {
                    //获取编辑模式
                    var editFlag = item.EditFlag;
                    //获取行编号
                    var rowNo = item.RowNo;
                    //获取工资项列编号
                    var salaryNo = item.SalaryColumnNo;
                    //新输入项，更改编辑模式
                    if ("0" == editFlag)
                    {
                        document.getElementById("txtEditFlag_" + rowNo + "_" + salaryNo).value = "1";
                    }
                    //删除项，更改编辑模式
                    else
                    {
                        document.getElementById("txtEditFlag_" + rowNo + "_" + salaryNo).value = "0";
                    }
                });
                //设置提示信息
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
function CheckInput()
{ 
     //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;    
        
    //获取工资项总数
    salaryCount = parseInt(document.getElementById("txtSalaryCount").value.Trim());
    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim());
    //遍历所有员工，校验每个员工的工资信息
    for (var i = 1; i <= userCount; i++)
    {
        //遍历所有工资项，校验工资项的值
        for (var j = 1; j <= salaryCount; j++)
        {
            //获取工资项的值
            salaryMoney = document.getElementById("txtSalaryMoney_" + i + "_" + j).value.Trim();
            //输入时校验输入的有效性
            if (salaryMoney != null && salaryMoney != "" && !IsNumeric(salaryMoney, 10, 2))
            {
                //获取员工名
                emplName = document.getElementById("tdEmployeeName_" + i).innerHTML;
                //获取工资项名称
                salaryName = document.getElementById("txtSalaryName_" + j).value.Trim();
                //设置错误信息
                isErrorFlag = true;
                fieldText += emplName + " " + salaryName + "|";
                msgText += "请输入正确的工资值！|";
            }
        }
        
    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        //popMsgObj.Show(fieldText, msgText);
        
	    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").style.top = "240px";
	    document.getElementById("spanMsg").style.left = "450px";
	    document.getElementById("spanMsg").style.width = "290px";
	    document.getElementById("spanMsg").style.position = "absolute";
	    document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
    }

    return isErrorFlag;
}

/*
* 获取提交的基本信息
*/
function GetPostParams()
{    
    //获取工资项总数
    salaryCount = parseInt(document.getElementById("txtSalaryCount").value.Trim());
    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim());
    //参数定义
    var strParams = "UserCount=" + userCount + "&SalaryCount=" + salaryCount;
 
    //遍历所有员工，以便设置每个员工的工资信息
    for (var i = 1; i <= userCount; i++)
    {
        //员工ID
        strParams += "&EmployeeID_" + i + "=" + document.getElementById("txtEmplID_" + i).value.Trim();
        //遍历所有工资项，获取员工每个工资项的值
        for (var j = 1; j <= salaryCount; j++)
        {
            //设置员工工资项工资
            var salar=document.getElementById("txtSalaryMoney_" + i + "_" + j).value.Trim();
            if (salar !="")
            {
            strParams += "&SalaryMoney_" + i + "_" + j + "=" +salar  ;
            }
            else
            {
              strParams += "&SalaryMoney_" + i + "_" + j + "=" +"0"  ;
            }
            //编辑模式
            strParams += "&EditFlag_" + i + "_" + j + "=" + document.getElementById("txtEditFlag_" + i + "_" + j).value.Trim();
        }
        
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
* 检索操作
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
    //员工编号
    postParam = "Action=Search&EmployeeNo=" + document.getElementById("txtEmployeeNo").value.Trim();
    //员工工号
    postParam += "&EmployeeNum=" + document.getElementById("txtEmployeeNum").value.Trim();
    //员工姓名
    postParam += "&EmployeeName=" + escape ( document.getElementById("txtEmployeeName").value.Trim());
    //所在岗位
    postParam += "&QuarterID=" + document.getElementById("ddlQuarter").value.Trim();
    //岗位职等
    postParam += "&AdminLevelID=" + document.getElementById("ctAdminLevel_ddlCodeType").value.Trim();
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/InputSalaryFixed.ashx',
        data : postParam,//目标地址
        dataType:"string",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(salaryInfo)
        {
            //设置工资项内容
            document.getElementById("divSalaryDetail").innerHTML = salaryInfo;
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