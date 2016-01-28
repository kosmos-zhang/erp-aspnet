$(document).ready(function(){
  DoSearch();
});



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
        url: "../../../Handler/Office/HumanManager/InputPersonTrueIncomeTax.ashx?"+postParams ,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
////            AddPop();
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
               // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                 showMessage("提示|","保存成功！|");
            }
            else 
            { 
                hidePopup();
                 showMessage("提示|","保存失败,请确认！|");
               // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
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
   table = document.getElementById("tblInsuDetail");
    //获取行号
     var no = table.rows.length;
     
  
     for (var i =1;i<no-1;i++)
     {
           var  SalaryCount=document .getElementById ("txtSalaryCount_"+i ).value.Trim();
//           var TaxPercent=  document .getElementById ("txtTaxPercent_"+Row ).value;
//           var TaxCount=  document .getElementById ("txtTaxCount_"+Row ).value;
           var StartDate=  document .getElementById ("txtStartDate_"+i ).value.Trim();
               var TaxCountHid=  document .getElementById ("txtTaxCount_"+i ).value.Trim();
              if (SalaryCount =="" || SalaryCount ==null || SalaryCount =="undefine")
              {
                     if (StartDate !="")
                  {
                     isErrorFlag = true;
                    fieldText += "第"+(i)+ "行缴税基数项|";
                    msgText += "请输入缴税基数！|";
                    break ;
                  }
              }else
              {
              if ( parseFloat (TaxCountHid,2)=="0")
              {
                  
               }
               else
               {
               
                  if (StartDate =="" || StartDate ==null || StartDate =="undefine")
                  {
                     isErrorFlag = true;
                  fieldText += "第"+(i)+ "行开始缴税日期项|";
                    msgText += "请输入开始缴税日期！|";
                    break ;
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

       table = document.getElementById("tblInsuDetail");
    //获取行号
     var no = table.rows.length;
     var getInfo=new Array ();
     
     for (var i =1;i<no-1;i++)
     {
     
          var  EmplID=document .getElementById ("txtEmplID_"+i ).value.Trim();
           var  SalaryCount=document .getElementById ("txtSalaryCount_"+i ).value.Trim();
           var TaxPercent=  document .getElementById ("txtTaxPercent_"+i ).value.Trim();
           var TaxCount=  document .getElementById ("txtTaxCount_"+i ).value.Trim();
           var StartDate=  document .getElementById ("txtStartDate_"+i ).value.Trim();
           getInfo .push ( EmplID,SalaryCount,TaxPercent,TaxCount,StartDate);
   }
   var strParams="TaxInfo="+getInfo ;
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
    
//     popMsgObj.Show(fieldText,msgText);
      showMessage(fieldText,msgText);
     
// document.getElementById("spanMsg").style.display = "block";
//	    document.getElementById("spanMsg").style.top = "240px";
//	    document.getElementById("spanMsg").style.left = "450px";
//	    document.getElementById("spanMsg").style.width = "290px";
//	    document.getElementById("spanMsg").style.position = "absolute";
//  document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
      return;
    }
    /* 获取参数 */
    //员工编号
    postParam =  "Action=Search" + "&EmployeeNo=" +escape( document.getElementById("txtEmployeeNo").value.Trim());
    //员工姓名
    postParam += "&EmployeeName=" + escape(document.getElementById("txtEmployeeName").value.Trim());
    //所在岗位
    postParam += "&QuarterID=" +escape( document.getElementById("ddlQuarter").value.Trim());
    //保险名称

    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/InputPersonTrueIncomeTax.ashx',
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

function insertData()
{
//debugger ;
table = document.getElementById("tblInsuDetail");
    //获取行号
var no = table.rows.length;
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
	    
//	     document.getElementById("spanMsg").style.display = "block";
//	    document.getElementById("spanMsg").style.top = "240px";
//	    document.getElementById("spanMsg").style.left = "450px";
//	    document.getElementById("spanMsg").style.width = "290px";
//	    document.getElementById("spanMsg").style.position = "absolute";
//  document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("错误|", "请输入正确的缴税基数|");
  // alert ("请输入正确的缴税基数");
  return;
   }
}


    if (txtPStartDate =="" || txtPStartDate ==null || txtPStartDate =="undefine")
    {
 
    }
    else
    {
        for (var i=1;i<no-1;i++)
        {
        document .getElementById ("txtStartDate_"+i ).value=txtPStartDate;
        }
    }
    
if (txtNum =="" || txtNum ==null || txtNum =="undefine")
{

}
else
{
for (var i=1;i<no-1;i++)
{
document .getElementById ("txtSalaryCount_"+i ).value=txtNum;
Caculate(txtNum, i);
}
}

}
function Caculate(num ,Row)
{
//debugger ;
var taxInfo=document.getElementById ("hidTaxInfo").value.Trim();
var allTaxInfo=taxInfo.split("@");
var flage=false ;
        for (var i=0;i<allTaxInfo.length;i++)
        {
           var oneTaxInfo=allTaxInfo [i].split(",");
           if ( parseFloat (  num )>=  parseFloat (  oneTaxInfo [0]) && parseFloat (  num )<= parseFloat (  oneTaxInfo [1]))
           {
           flage =true ;
       
           document .getElementById ("txtTaxPercent_"+Row ).value=oneTaxInfo[2];
             document .getElementById ("txtTaxCount_"+Row ).value=oneTaxInfo[3];
             break ;
           }
        }
        if (!flage )
        {
         document .getElementById ("txtStartDate_"+Row ).value="";
        document .getElementById ("txtSalaryCount_"+Row ).value="";
            document .getElementById ("txtTaxPercent_"+Row ).value="";
             document .getElementById ("txtTaxCount_"+Row ).value="";
        }

}
function CalculateTotalSalary(obj ,Row)
{
 
var num=obj.value;
if (num =="" || num ==null || num =="undefine")
{
     document .getElementById ("txtStartDate_"+Row ).value="";
        document .getElementById ("txtSalaryCount_"+Row ).value="";
            document .getElementById ("txtTaxPercent_"+Row ).value="";
             document .getElementById ("txtTaxCount_"+Row ).value="";
return ;
}
if (!IsNumeric(num,12,2))
{
    showMessage("错误|", "请输入正确的缴税基数|");
//        document.getElementById("spanMsg").style.display = "block";
//        
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("错误|", "请输入正确的缴税基数|");
	    
	    
	    
	    return ;
}

var taxInfo=document.getElementById ("hidTaxInfo").value.Trim();
var allTaxInfo=taxInfo.split("@");
var flage=false ;
        for (var i=0;i<allTaxInfo.length;i++)
        {
           var oneTaxInfo=allTaxInfo [i].split(",");
           if ( parseFloat (  num )>=  parseFloat (  oneTaxInfo [0]) && parseFloat (  num )<= parseFloat (  oneTaxInfo [1]))
           {
           flage =true ;
       
           document .getElementById ("txtTaxPercent_"+Row ).value=oneTaxInfo[2];
             document .getElementById ("txtTaxCount_"+Row ).value=oneTaxInfo[3];
             break ;
           }
        }
        if (!flage )
        {
         document .getElementById ("txtStartDate_"+Row ).value="";
        document .getElementById ("txtSalaryCount_"+Row ).value="";
            document .getElementById ("txtTaxPercent_"+Row ).value="";
             document .getElementById ("txtTaxCount_"+Row ).value="";
        }

}

// onblur =\"CalculateTotalSalary('" + (i + 1).ToString() + "')\"

//function CalculateTotalSalary( row)
//{
//document .getElementById ("")



//}