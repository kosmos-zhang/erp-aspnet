function getMoney()
{
alert (document .getElementById ("divSalaryList").innerHTML);
}

/*
* 添加部门提成设置
*/
function DoAdd(){

    var DeptID=$("#HidDeptID").val();
    var DeptName=$("#HidDeptName").val();
    var CustName= $("#CustName").val();
    var CustID=$("#HidtxtCus").val();
    var Flag=$("#Hdflag").val();
    if(Flag=="0")
    {
     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先执行检索！");
        return;
    }
    //获取表格
    table = document.getElementById("tblSalary");
    //获取行号
    var no = table.rows.length;
    if (no>1)
    {
        var ssd=no-1;
        var getTemp=document .getElementById ("txtMaxMoney_"+ssd).value.Trim();
        if (getTemp =="" || getTemp ==null  || getTemp =="undifine")
        {
            document .getElementById ("txtMaxMoney_"+ssd).focus();
        return ;
        }
        else
        {
            var getMaxMoney=document .getElementById ("txtTaxPercent_"+ssd).value.Trim();
              if (getMaxMoney =="" || getMaxMoney ==null  || getMaxMoney =="undifine")
              {
//                document .getElementById ("txtTaxPercent_"+ssd).focus();
//                return ;
             }
        }
    }
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='hidden' id='txtSalaryID_" + no + "' value='' />"
	                + "<input type='hidden' id='txtEditFlag_" + no + "' value='0' />"
	                + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + no + "'>";
	                
	                
  	//公司
	objTD=objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' id='txtDept_" + no + "' value='"+DeptName+"' class='tdinput' readonly='readonly' />"
	                + "<input type='hidden' id='HidtxtDept_" + no + "' value='"+DeptID+"' />";
	//上限
	if (no==1)
	{
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='12'  readonly ='readonly' onchange='Number_round(this,0);'  onkeydown='Numeric_OnKeyDown();'  class='tdinput' id='txtMiniMoney_" + no + "' value='0' />";
	}
	else
	{
	var row=no-1;
	document .getElementById ("chkSelect_"+row).style.display="none";
    document .getElementById ("txtMaxMoney_"+row).readOnly="true";
    document .getElementById ("txtMiniMoney_"+row).readOnly="true";
	document .getElementById ("txtTaxPercent_"+row).readOnly="true";
	document .getElementById ("txtOldCustomerTax_"+row).readOnly="true";
	document .getElementById ("txtNewCustomerTax_"+row).readOnly="true";
	document .getElementById ("ddlISCustomerRoyaltySet_"+row).disabled="disabled";
	var preMoney=document .getElementById ("txtMaxMoney_"+row).value.Trim();
	
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='12'  readonly ='readonly'   onkeydown='Numeric_OnKeyDown();' onchange='Number_round(this,0);'   class='tdinput' id='txtMiniMoney_" + no + "' value="+preMoney+"  />";
	}
	
	//下限
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='12'  onkeydown='Numeric_OnKeyDown();' onchange='Number_round(this,2);'   class='tdinput' id='txtMaxMoney_" + no + "' />";
	
	//税率
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='3'  disabled onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,2);'  onblur='CalculateTotalSalary(this, \"" + no+ "\");'  class='tdinput' id='txtTaxPercent_" + no + "' />";
	
	
	//是否区分客户提成
    objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<select id='ddlISCustomerRoyaltySet_" + no + "' disabled><option value='1'>是</option><option value='0'>否</option></select>";
	
	//客户名称
	objTD=objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' id='txtCustName_" + no + "'  value='"+CustName+"' class='tdinput' readonly='readonly' />"
	                + "<input type='hidden' id='HidtxtCusID_" + no + "' value='"+CustID+"' />";
	                
	 //新客户提成            
	 objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='3' style='width:98%;' onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,2);' onblur='CalculateTotalSalary(this, \"" + no+ "\");'    class='tdinput' id='txtNewCustomerTax_" + no + "' />";
	
	//老客户提成
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='3' style='width:98%;' onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,2);' onblur='CalculateTotalSalary(this, \"" + no+ "\");'    class='tdinput' id='txtOldCustomerTax_" + no + "' />";
	if(CustName=="默认")
	{
            document.getElementById("txtCustName_"+no).disabled=true;
            document.getElementById("txtNewCustomerTax_"+no).disabled=true;
            document.getElementById("txtOldCustomerTax_"+no).disabled=true;
            document.getElementById("txtNewCustomerTax_"+no).value="";
            document.getElementById("txtOldCustomerTax_"+no).value="";
            document.getElementById("txtTaxPercent_"+no).disabled=false;
            document.getElementById("ddlISCustomerRoyaltySet_"+no).value="0";
      
	}
	else
	{
	        document.getElementById("txtCustName_"+no).disabled=false;
            document.getElementById("txtNewCustomerTax_"+no).disabled=false;
            document.getElementById("txtOldCustomerTax_"+no).disabled=false;
            document.getElementById("txtTaxPercent_"+no).disabled=true;
            document.getElementById("ddlISCustomerRoyaltySet_"+no).value="1";
	}
	
}

//function Allchange(row)
//{
//    if (row==1)
//    {
//    CalculateTotalSalary(1);
//    }
//    else
//    {
//    var newRow=row+1;
//    if ()
//    
//    
//    }
//}
/*
* 设置全选CheckBox
*/
function SetCheckAll(obj){
    //获取全选择控件
    checkAll = document.getElementById("chkAll");
    //如果checkbox未选中
    if (!obj.checked)
    {
        checkAll.checked = false;
        return;
    }
    else
    {
        isSelectAll = true;        
        //获取表格
        table = document.getElementById("tblSalary");
        //获取表格行数
	    var count = table.rows.length;
	    if (count < 2) return;
	    //遍历表格中的数据，判断是否选中
	    for (var row = count - 1; row > 0; row--)
	    { 
	        //行未被删除时
	        if (table.rows[row].style.display != "none")
	        {
	            //获取选择框控件
	            chkControl = document.getElementById("chkSelect_" + row);
	            //如果未选中，则返回
	            if (!chkControl.checked)
	            {
	                isSelectAll = false;
	                break;
	            }
	        }
	    }
	    //列表中全部选中时，选择checkbox选中
	    if (isSelectAll)
	    {
	        checkAll.checked = true;
	    }
	    //列表中有一个未选中时，选择checkbox未选中
	    else
	    {
	        checkAll.checked = false;
	    }
    }
}

/*
* 全选择表格行
*/
function SelectAll(){
    //获取表格
    table = document.getElementById("tblSalary");
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	var isSelectAll = document.getElementById("chkAll").checked;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	    //行未被删除时
	    if (table.rows[row].style.display != "none")
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
* 删除表格行
*/
function DoDelete(){

    //获取表格
    table = document.getElementById("tblSalary");
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
	      // table.rows[row].style.display = "none";
	            table.deleteRow(row);
	            var ss=row-1;
	            if (ss>0)
	            {
	            document .getElementById ("txtMaxMoney_"+ss).readOnly=false;
	            if (ss!=1)
	            {
			}
	          document .getElementById ("chkSelect_"+ss).style.display="Block";
	          }
	          		
	    }
	}
}

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

        var minMoney=document.getElementById("txtMiniMoney_"+row ).value.Trim();
        var maxMoney=document.getElementById("txtMaxMoney_"+row ).value.Trim(); 
        var personTaxPercent=document.getElementById("txtTaxPercent_"+row ).value.Trim(); 
//          var personMinusMoney=document.getElementById("txtPersonMinusMoney_"+row ).value.Trim(); 
          var isErrorFlag=false ;
             var fieldText = "";
    //出错提示信息
    var msgText = "";
//          if (!IsNumeric(minMoney ,12,2))
//          {
//            fieldText +=   "第"+row+"行" +"税率项" + "|";
//                msgText += "请输入税率！|";
//                isErrorFlag=true ;
//          }
               if ((personTaxPercent ==null || personTaxPercent =="")&&(maxMoney ==null || maxMoney =="")  )
            {
            }
            else
            {
                  if (!isErrorFlag )
                  {
                      if (parseFloat (maxMoney )<parseFloat (minMoney ) )
                      {
                          fieldText +=   "第"+row+"行" +"业绩下限项" + "|";
                            msgText += "业绩下限项应大于业绩上限项！|";
                            isErrorFlag=true ;
                      }
                  }
          }
           
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText, msgText);
        
//	    document.getElementById("spanMsg").style.display = "block";
//	    document.getElementById("spanMsg").style.top = "240px";
//	    document.getElementById("spanMsg").style.left = "450px";
//	    document.getElementById("spanMsg").style.width = "290px";
//	    document.getElementById("spanMsg").style.position = "absolute";
//	    document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
	    return ;
    }
          
          
        if (row==1)
        {
//        document.getElementById("txtTaxPercent_"+row ).value=0;
        }
        else
        {
        
//         var info=new Array ();
//         var sum=0;
//         var leftMoney=0;
//            if (row>1)
//            {
//              
//                    for (var i=1;i<row;i++)
//                    {
//                     
//                        var PreMinMoney=document.getElementById("txtMiniMoney_"+i ).value.Trim();
//                        var PreMaxMoney=document.getElementById("txtMaxMoney_"+i ).value.Trim(); 
//                        var PrePersonTaxPercent=document.getElementById("txtTaxPercent_"+i ).value.Trim(); 
////                        var PrePersonMinusMoney=document.getElementById("txtPersonMinusMoney_"+i ).value.Trim(); 
////                          
//                          if (leftMoney ==0)
//                          {
//                                   leftMoney= minMoney -PreMaxMoney ;
//                                   if (leftMoney >0)
//                                   {
//                                    sum=sum+PreMaxMoney *(personTaxPercent-PrePersonTaxPercent)/100;
//                                   }
//                                   else  if (leftMoney ==0)
//                                   {
//                                     sum=sum+PreMaxMoney *(personTaxPercent-PrePersonTaxPercent)/100;
//                                   }
//                                    else  if (leftMoney <0)
//                                    {
//                                    sum=0;
//                                    }
//                                   
//                          }else
//                          {
//                              
//                                   if (leftMoney -PreMaxMoney >0)
//                                   {
//                                    sum=sum+PreMaxMoney *(personTaxPercent-PrePersonTaxPercent)/100;
//                                   }
//                                   else  if (leftMoney -PreMaxMoney ==0)
//                                   {
//                                   sum=sum+leftMoney *(personTaxPercent-PrePersonTaxPercent)/100;
//                                   }
//                                     else  if (leftMoney -PreMaxMoney <0)
//                                   {
//                                         sum=sum+leftMoney *(personTaxPercent-PrePersonTaxPercent)/100;
//                                   }
//                           leftMoney= leftMoney -PreMaxMoney ;
//                          
//                          }
//                   
//                    
//                    }
//               
//            } 
//          sum=tofloat (sum,2);
//            document.getElementById("txtPersonMinusMoney_"+row ).value=sum;
        }
         
        
//        
//        //获取工资ID相同部门
//        salaryRowID = "txtSalaryMoney_" + row + "_";
//        //获取工资项个数
//        salaryCount = parseInt(document.getElementById("txtSalaryCount").value);
//        //定义变量
//        var totalSalaryMoney = 0;
//        //遍历所有工资输入
//        for (var i = 1; i <= salaryCount; i++)
//        {
//            //工资额 
//            salaryMoney = document.getElementById(salaryRowID + i).value;
//            //是否扣款
//            payFlag = document.getElementById("txtPayFlag_" + row + "_" + i).value;
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
	    //设置面试总成绩
	    /* 未考虑 NaN的情况 有时间再做 */
//	    document.getElementById("txtTotalSalary_" + row).value = parseInt(totalSalaryMoney * 100 + 0.5) / 100;
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
//     $("#HidDeptID").val(EmpID);
//                $("#HidDeptName").val(DeptName);
//                $("#divSalaryList").html(data);
//                $("#CustName").val(CustName);
//                $("#HidtxtCus").val(CustID);
                 var EmpID=$("#HidDeptID").val();
                 var CustID= $("#HidtxtCus").val();
                 postParams = "?Action=Save&EmpID="+EmpID+"&CustID="+CustID+"&message=" + GetPostParams();
//    postParams = "?Action=Save&message=" + GetPostParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryPersonalRoyaltySet_Edit.ashx"+postParams ,
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


function   GetPostParams()
{
var tempInfo=new Array ();
table = document.getElementById("tblSalary");
    //获取行号
var no = table.rows.length;
for (var i=1;i<no;i++)
{
       var minMoney=document.getElementById("txtMiniMoney_"+i ).value.Trim();
       var maxMoney=document.getElementById("txtMaxMoney_"+i ).value.Trim(); 
       var personTaxPercent=document.getElementById("txtTaxPercent_"+i ).value.Trim(); 
       var DeptID=$("#HidDeptID").val(); 
       var CustID=$("#HidtxtCusID_"+i).val();
       var ISCustomerRoyaltySet=$("#ddlISCustomerRoyaltySet_"+i).val();
       var NewCustomerTax=$("#txtNewCustomerTax_"+i).val();
       var OldCustomerTax=$("#txtOldCustomerTax_"+i).val();
       tempInfo .push (i,minMoney ,maxMoney,personTaxPercent ,DeptID,CustID,ISCustomerRoyaltySet,NewCustomerTax,OldCustomerTax );
}
return tempInfo ;
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
   
table = document.getElementById("tblSalary");
    //获取行号
var no = table.rows.length;

for (var i=1;i<no;i++)
{
      var minMoney=document.getElementById("txtMiniMoney_"+i ).value.Trim();
         if (minMoney =="" || minMoney ==null  || minMoney =="undifine")
         {
                isErrorFlag = true;
                fieldText +=   "第"+(no-1)+"行" +"业绩上限项" + "|";
                msgText += "请输入业绩上限项！|";
                  break ;
         }
        var maxMoney=document.getElementById("txtMaxMoney_"+i ).value.Trim(); 
            if (maxMoney =="" || maxMoney ==null  || maxMoney =="undifine")
             {
                isErrorFlag = true;
                fieldText +=   "第"+(no-1)+"行" +"业绩下限项" + "|";
                msgText += "请输入业绩下限项！|";
                  break ;
              }
        var personTaxPercent=document.getElementById("txtTaxPercent_"+i ).value.Trim(); 
        var OldCustomerTax=document.getElementById("txtOldCustomerTax_"+i ).value.Trim(); 
        var NewCustomerTax=document.getElementById("txtNewCustomerTax_"+i ).value.Trim(); 
            if ((personTaxPercent =="" || personTaxPercent ==null  || personTaxPercent =="undifine")&&(OldCustomerTax =="" || OldCustomerTax ==null  || OldCustomerTax =="undifine")&&(NewCustomerTax =="" || NewCustomerTax ==null  || NewCustomerTax =="undifine"))
            {
                isErrorFlag = true;
                fieldText +=   "第"+(no-1)+"行" +"提成率项,新客户提成率项,老客户提成率项" + "|";
                msgText += "不能同时为空！|";
                break ;
             }
           var maxMoney=document.getElementById("txtMaxMoney_"+i ).value.Trim(); 
           var personTaxPercent=document.getElementById("txtTaxPercent_"+i ).value.Trim();  
          if (maxMoney ==null || maxMoney =="")
           {  
          }
          else
          {
             if (!IsNumeric(maxMoney  ,10,2))
                  {
                    isErrorFlag = true;
                  fieldText +=   "第"+(no-1) +"行" +"业绩下限项" + "|";
                        msgText += "请输入正确的业绩下限！|";
                        break;
                  }
          }
            if (personTaxPercent ==null || personTaxPercent =="")
           {
                 
            } 
            else
            {
                if (!IsNumeric(personTaxPercent  ,6,2))
                  {
                  isErrorFlag = true;
                  fieldText +=   "第"+row+"行" +"提成率项" + "|";
                        msgText += "请输入正确的提成率！|";
                         break;
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

function alertdiv(ControlID)
{
      var Array = ControlID.split(",");
     if(Array[0].indexOf("User") >= 0) 
         {
          
                if(Array.length==2)
                {
                    var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
                    {
                        var splitInfo=returnValue.split("|");
                        
     
                         window.parent.window.frames[0].document.getElementById(Array[1]).value=splitInfo[0].toString();
                         window.parent.window.frames[0].document.getElementById(Array[0]).value=splitInfo[1].toString();
                       // window.parent.document.getElementById(Array[1]).value =
                        //window.parent.document.getElementById(Array[0]).value =splitInfo[1].toString();
                    }
                   else  if(returnValue=="ClearInfo")
                   {
                         window.parent.window.frames[0].document.getElementById(Array[1]).value="";
                         window.parent.window.frames[0].document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[1]).value=""; 
                   } 
                }
          else
          {
             var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=2";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
             {
                 var ID="";
                 var Name="";
                 var getinfo = returnValue.split(",");
                  for(var i=0; i < getinfo.length; i++)
                  {
                      var c = getinfo[i].toString();
                      ID+=c.substring(0,c.indexOf("|"))+",";
                      Name+=c.substring(c.indexOf("|")+1,c.length)+",";
                  }
              ID = ID.substring(0, ID.length-1);
              Name=Name.substring(0,Name.length-1);
             //window.parent.document.getElementById(Array[1]).value =ID; 
             if(window.parent.window.frames[0].document.getElementById(Array[0]).value!="")
             {
                var Oldvalue=window.parent.window.frames[0].document.getElementById(Array[0]).value;
                var Newvalue=Name;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {                  
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames[0].document.getElementById(Array[0]).value+=  window.parent.window.frames[0].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
                else
                {
                     
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                      window.parent.window.frames[0].document.getElementById(Array[0]).value+=  window.parent.window.frames[0].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 window.parent.window.frames[0].document.getElementById(Array[0]).value =Name;   
             }
             if(window.parent.window.frames[0].document.getElementById(Array[1]).value!="")
             {
                var Oldvalue=window.parent.window.frames[0].document.getElementById(Array[1]).value;
                var Newvalue=ID;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames[0].document.getElementById(Array[1]).value+=  window.parent.window.frames[0].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
                else
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                      window.parent.window.frames[0].document.getElementById(Array[1]).value+=  window.parent.window.frames[0].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 window.parent.window.frames[0].document.getElementById(Array[1]).value =ID;   
             }
           }
           else if(returnValue=="ClearInfo")
           {
               window.parent.window.frames[0].document.getElementById(Array[0]).value="";
               window.parent.window.frames[0].document.getElementById(Array[1]).value=""; 
           } 
         }        
       }
}

//查询当前部门的信息
function DoSearch()
{
    //获取值value
    var EmpID= $("#txtExecutorID").val();
     var DeptName= $("#UserExecutor").val();
     var CustID= $("#HidtxtCus").val();
    var CustName=$("#CustID").val();
    var postParam = "Action=GetSub&EmpID=" + escape(EmpID)+"&CustID="+CustID;
    $.ajax(
        { 
            type: "POST",
            url: "../../../Handler/Office/HumanManager/SalaryPersonalRoyaltySet.ashx",
            data:postParam,
            dataType:'html',//返回html格式数据
            cache:false,
            beforeSend:function()
            { 
                AddPop();
            },
            error: function() 
            {
              ///  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg('请求发生错误！');
            }, 
            success:function(data) 
            { 
               $("#HidDeptID").val(EmpID);
                $("#HidDeptName").val(DeptName);
                $("#divSalaryList").html(data);
                $("#CustName").val(CustName);
                $("#HidtxtCus").val(CustID);
                $("#Hdflag").val("1");
                if(EmpID=="")$("#HidDeptName").val("默认");
                if(CustID=="")$("#CustName").val("默认");
            },
            complete:function(){hidePopup();}
        });
}

//选择客户
function fnSelectCustInfo(j) {
        popSellCustObj.ShowList('protion',j);
}
//选择客户后，为页面填充数据
function fnSelectCust(custID,k) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SellModuleSelectCustUC.ashx",
        data: 'actionSellCust=info&id=' + custID,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取客户数据失败,请确认"); },
        success: function(data) {
//            $("#txtCustNo_1").val(data.CustNO); //客户名称
//            document.getElementById("txtCustNo_"+k).value=data.CustNO;
            document.getElementById("CustID").value=data.CustName;
            document.getElementById("HidtxtCus").value=custID;
            try {
                fnGetPriceByRate();
            } catch (e) { }
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}

//function LoadCust(k)
//{
// 
//}