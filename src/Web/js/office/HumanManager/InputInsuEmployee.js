$(document).ready(function(){
  DoSearch();
});
function insertData()
{
var txtNum= document .getElementById ("txtNum").value.Trim();
var txtPStartDate= document .getElementById ("txtPStartDate").value.Trim();
if (txtNum =="" || txtNum ==null || txtNum =="undefine")
{
   if (txtPStartDate =="" || txtPStartDate ==null || txtPStartDate =="undefine")
    {
    return ;
    }
}
else
{
   if (!IsNumeric(txtNum ,12,2))
   {
//       document.getElementById("spanMsg").style.display = "block";
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("错误|", "请输入正确的缴税基数|");
	    
	      showMessage("错误|", "请输入正确的缴税基数|");
  // alert ("请输入正确的缴税基数");
  return;
   }
   
    //获取工资项总数
    insuCount = parseInt(document.getElementById("txtInsuSocialCount").value.Trim());
    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim());
    //参数定义
    //遍历所有员工，以便设置每个员工的工资信息
   if (txtNum =="" || txtNum ==null || txtNum =="undefine")
{

}
else
{
   
        for (var i = 1; i <= userCount; i++)
        {
            //员工ID
            
            //遍历所有工资项，获取员工每个工资项的值
            for (var j = 1; j <= insuCount; j++)
            {
                //缴费基数
              document.getElementById("txtInsuranceBase_" + i + "_" + j).value=txtNum ;
            }
            
        }
    }
   if (txtPStartDate =="" || txtPStartDate ==null || txtPStartDate =="undefine")
{

}
else
{
   
        for (var i = 1; i <= userCount; i++)
        {
            
            
            //遍历所有工资项，获取员工每个工资项的值
            for (var j = 1; j <= insuCount; j++)
            {
                //开始时间
             document.getElementById("txtStartDate_" + i + "_" + j).value=txtPStartDate ;
            }
            
        }
    }
    
    
    
    




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
        url: "../../../Handler/Office/HumanManager/InputInsuEmployee.ashx",
        data : postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
//            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.EditStatus == 1) 
            { 
           // alert (data .DataInfo);
            if (data.DataInfo!="" && data.DataInfo!=null )
            {
                //设置工资项信息
                $.each(data.DataInfo,function(i,item)
                {
                    //获取编辑模式
                    var editFlag = item.EditFlag;
                    //获取行编号
                    var rowNo = item.RowNo;
                    //获取工资项列编号
                    var insuNo = item.InsuColumnNo;
                    //新输入项，更改编辑模式
                    if ("0" == editFlag)
                    {
                        document.getElementById("txtEditFlag_" + rowNo + "_" + insuNo).value = "1";
                    }
                    //删除项，更改编辑模式
                    else
                    {
                        document.getElementById("txtEditFlag_" + rowNo + "_" + insuNo).value = "0";
                        document.getElementById("txtAddr_" + rowNo + "_" + insuNo).value = "";
                    }
                    
                    
                    
                });
                }
                //设置提示信息
              //  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                  showMessage("提示|","保存成功！|");
            }
            else 
            { 
                hidePopup();
                   showMessage("提示|","保存失败,请确认！|");
                //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
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
    insuCount = parseInt(document.getElementById("txtInsuSocialCount").value.Trim());
    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim());
//    alert (insuCount );
  if (!IsNumber( document.getElementById("txtUserCount").value.Trim() )|| insuCount ==0)
    {
           isErrorFlag = true;
       fieldText += "提示|";
        msgText += "请先设置保险项！|";
     
    }
    else
    {
    
    //遍历所有员工，校验每个员工的工资信息
    for (var i = 1; i <= userCount; i++)
    {
        //遍历所有工资项，校验工资项的值
        for (var j = 1; j <= insuCount; j++)
        {
            //缴费基数
            baseMoney = document.getElementById("txtInsuranceBase_" + i + "_" + j).value.Trim();
            //参保时间
            startDate = document.getElementById("txtStartDate_" + i + "_" + j).value.Trim();
            //获取员工名
            emplName = document.getElementById("tdEmployeeName_" + i).innerHTML;
            //获取保险名称
            insuName = document.getElementById("txtInsuranceName_" + j).value.Trim();
            //输入时校验输入的有效性
            if (baseMoney != null && baseMoney != "")
            {
                //输入不正确时
                if (!IsNumeric(baseMoney, 8, 2))
                {
                    //设置错误信息
                    isErrorFlag = true;
                    fieldText += emplName + " " + insuName + "|";
                    msgText += "请输入正确的缴费基数！|";
                }
                //参保时间未输入时
                if (startDate == null || startDate == "")
                {
                    //设置错误信息
                    isErrorFlag = true;
                    fieldText += emplName + " " + insuName + "|";
                    msgText += "请输入参保时间！|";
                }
            }
            else
            {
                if (startDate != null && startDate != "")
                {
                    //设置错误信息
                    isErrorFlag = true;
                    fieldText += emplName + " " + insuName + "|";
                    msgText += "请输入缴费基数！|";
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
         showMessage(fieldText,msgText);
//	    document.getElementById("spanMsg").style.display = "block";
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
    }

    return isErrorFlag;
}
function showMessage(fieldText,msgText)
{
document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").style.top = "240px";
	    document.getElementById("spanMsg").style.left = "450px";
	    document.getElementById("spanMsg").style.width = "290px";
	    document.getElementById("spanMsg").style.position = "absolute";
  document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
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
    //获取工资项总数
    insuCount = parseInt(document.getElementById("txtInsuSocialCount").value.Trim());
    //获取人员总数
    userCount = parseInt(document.getElementById("txtUserCount").value.Trim());
    //参数定义
    var strParams = "UserCount=" + userCount + "&InsuCount=" + insuCount;
    //遍历所有员工，以便设置每个员工的工资信息
    for (var i = 1; i <= userCount; i++)
    {
        //员工ID
        strParams += "&EmployeeID_" + i + "=" + document.getElementById("txtEmplID_" + i).value.Trim();
        //遍历所有工资项，获取员工每个工资项的值
        for (var j = 1; j <= insuCount; j++)
        {
            //缴费基数
            strParams += "&InsuBase_" + i + "_" + j + "=" + escape ( document.getElementById("txtInsuranceBase_" + i + "_" + j).value.Trim());
            //参保时间
            strParams += "&StartDate_" + i + "_" + j + "=" + escape ( document.getElementById("txtStartDate_" + i + "_" + j).value.Trim());
            //参保地
            strParams += "&Addr_" + i + "_" + j + "=" +escape ( document.getElementById("txtAddr_" + i + "_" + j).value.Trim());
            //编辑模式
            strParams += "&EditFlag_" + i + "_" + j + "=" + escape ( document.getElementById("txtEditFlag_" + i + "_" + j).value.Trim());
        }
        
    }
    //设置工资项ID
    for(var x = 1; x <= insuCount; x++)
    {
        //设置工资项ID
        strParams += "&InsuID_" + x + "=" + document.getElementById("txtInsuranceID_" + x).value.Trim();   
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
    showMessage(fieldText, msgText);
// document.getElementById("spanMsg").style.display = "block";
//	    document.getElementById("spanMsg").style.top = "240px";
//	    document.getElementById("spanMsg").style.left = "450px";
//	    document.getElementById("spanMsg").style.width = "290px";
//	    document.getElementById("spanMsg").style.position = "absolute";
//	    document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
      return;
    }


    /* 获取参数 */
    //员工编号
    postParam =  "Action=Search" + "&EmployeeNo=" + escape (  document.getElementById("txtEmployeeNo").value.Trim());
    //员工姓名
    postParam += "&EmployeeName=" +  escape ( document.getElementById("txtEmployeeName").value.Trim());
    //所在岗位
    postParam += "&QuarterID=" +  escape ( document.getElementById("ddlQuarter").value.Trim());
    //保险名称
    postParam += "&InsuranceName=" +  escape ( document.getElementById("txtInsuranceName").value.Trim());
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/InputInsuEmployee.ashx',
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
            document.getElementById("divInsuDetail").innerHTML = insuInfo;
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