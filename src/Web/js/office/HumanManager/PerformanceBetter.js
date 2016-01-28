$(document).ready(function(){
  //  DoGetTemplateInfo();
  //  DoGetEmployeeInfo();
   // document.getElementById("hidEditFlag").value="INSERT";
});
   String.prototype.length2 = function() {
    var cArr = this.match(/[^\x00-\xff]/ig);
    return this.length + (cArr == null ? 0 : cArr.length);
}

  function textcontrol(taId,maxSize) 
  {   
                // 默认 最大字符限制数   
                var defaultMaxSize = 250;   
                var ta = document.getElementById(taId);   
                // 检验 textarea 是否存在   
               if(!ta) {   
                          return;   
                }   
                // 检验 最大字符限制数 是否合法   
                if(!maxSize) {   
                   maxSize = defaultMaxSize;   
               } else {   
                    maxSize = parseInt(maxSize);   
                    if(!maxSize || maxSize < 1) {   
                        maxSize = defaultMaxSize;   
                   }   
               }   
               　　 if (ta.value.length2() > maxSize) {   
                   ta.value=ta.value.substring(0,maxSize);   
                   alert("超过最大字符限制："+maxSize);   
               }    
           } 





 
 
function AddPublish(){
    //获取表格
    table = document.getElementById("tbDetail");
    //获取行号
    var no = table.rows.length;
   no=no-2;
    //添加新行
	var objTR = table.insertRow(-1);
	objTR.style.display="block";
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='checkbox' id='tbDetail_chkSelect_" + no + "'>";
	//员工
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input  id='UsertxtEmployeeID1" + no + "' size='8'  maxlength='50'  type='text'   class='tdinput' onclick=alertdiv('UsertxtEmployeeID1"+no+",txtEmployeeID_"+no+"')  readonly />"+"<input  id='txtEmployeeID_"+no+"'     type='hidden'  />";
	//待改进计划  
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '200' class='tdinput' id='txtContent_" + no + "'>";
	//完成目标  
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '200' class='tdinput' id='txtCompleteAim_" + no + "'>";
	//完成期限
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
		objTD.innerHTML = "<input type='text' maxlength = '10'  size='8'  class='tdinput' readonly='readonly' id='txtCompleteDate_" + no + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCompleteDate_" + no + "')})\">";
	//核查人
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input  id='UsertxtChecker1" + no + "'   maxlength='50'  type='text'   class='tdinput' onclick=alertdiv('UsertxtChecker1"+no+",txtChecker_"+no+"') />"+"<input  id='txtChecker_"+no+"'     type='hidden'  />";
	
		
		
		
		//核查结果
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '50' class='tdinput' id='txtCheckResult_" + no + "'>";
	//核查时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength = '10'  size='8' readonly='readonly' class='tdinput' id='txtCompleteDate_" + no + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCompleteDate_" + no + "')})\">";
		//备注
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '50' class='tdinput' id='txtRemark_" + no + "'>";
	
	//alert (table.innerHTML);
}


/*
* 删除表格行
*/
function DeleteRows(tableName){
    //获取表格
    table = document.getElementById(tableName);
//   alert (table .innerHTML);
    //获取表格行数
	var count = table.rows.length;
//	alert (count );
	
	if (count < 3) return;
	//遍历表格中的数据，判断是否选中
	for (var row = count ; row > 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById(tableName + "_chkSelect_" + row);
	    //如果控件为选中状态，删除该行，并对之后的行改变控件名称
	  if (chkControl )
	  {
	    if (chkControl.checked)
	    {
	//    alert (row);
	       //删除行，实际是隐藏该行
	       table.rows[row+2].style.display = "none";
	    }
	    }
	}
}
//function DoBack()
//{
// self.location='PerformanceBetter_Edit.aspx?ModuleID=2011808'; 
//}
function DoBack()
{
  
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    window.location.href = "PerformanceBetter_Edit.aspx?ModuleID=" + ModuleID + searchCondition;
   
    
    
    
    
    
}

function SaveRectPlanInfo()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    //招聘目标校验 有错误时，返回
    }
     else if(CheckPublishInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    //获取信息发布参数
    postParams += GetPublishParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceBetter.ashx?" + postParams,
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
                document.getElementById("divRectPalnNo").innerHTML = data.data;
                //编码规则DIV不可见
                document.getElementById("txtPerformTmNo").style.display = "none";
                //设置招聘活动编号
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
    editFlag = document.getElementById("hidEditFlag").value;
    //新建时，编号选择手工输入时
    if ("INSERT" == editFlag)
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("AimNum_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            rectApplyNo = document.getElementById("AimNum_txtCode").value.Trim();
            //编号必须输入
            if (rectApplyNo == "")
            {
                isErrorFlag = true;
                fieldText += "改进计划编号|";
   		        msgText += "请输入改进计划编号|";
            }
            else
            {
                if ( isnumberorLetters( document.getElementById("AimNum_txtCode").value.Trim()))
           {
            isErrorFlag = true;
            fieldText += "改进计划编号|";
             msgText += "改进计划编号只能包含字母或数字！|";
           }
            var num=document.getElementById("AimNum_txtCode").value.Trim();
           if (checkstr(num,50))
           {
             isErrorFlag = true;
            fieldText += "改进计划编号|";
             msgText += "改进计划编号长度过长！|";
           }
           }
        }
    }
    
    //主题必须输入
   var title= cTrim(document.getElementById("txtTitle").value.Trim(),0);
   //alert (title);
    if (""== title)
    {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "请输入改进计划主题|";  
    }
    else
    {
     if( !CheckSpecialWord(document .getElementById ("txtTitle").value.Trim()))
    {
            isFlag = false;
            fieldText ="改进计划主题|";
   		    msgText = "改进计划主题不能含有特殊字符|";
   		    
    }
    }
    
      var txtWorkNeeds = document.getElementById("txtPlanRemark").value.Trim();
  if(strlen(txtWorkNeeds)> 1024)
    {
        isErrorFlag = true;
        fieldText = fieldText + "备注|";
   		msgText = msgText + "备注最多只允许输入1024个字符|";
    }

  //  alert (isErrorFlag);
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}
	//判断字符串是否超过指定的digit长度
	function checkstr(str,digit)
	{ 
	
	     //定义checkstr函数实现对用户名长度的限制
	        var n=0;         //定义变量n，初始值为0
	        for(i=0;i<str.length;i++){     //应用for循环语句，获取表单提交用户名字符串的长度
	        var leg=str.charCodeAt(i);     //获取字符的ASCII码值
	        if(leg>255)
	        {       //判断如果长度大于255 
	          n+=2;       //则表示是汉字为两个字节
	        }
	        else 
	        {
	         n+=1;       //否则表示是英文字符，为一个字节
	        }
	        }
	        
	       // alert (n);
	        
	        if (n>digit)
	        {        //判断用户名的总长度如果超过指定长度，则返回true
	        return true;
	        }
	        else 
	        {return false;       //如果用户名的总长度不超过指定长度，则返回false
	        }  
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
    var isHave=false ;
    //获取表格
    table = document.getElementById("tbDetail");
    //获取表格行数
    var count = table.rows.length;
   // alert (count );
   //alert (table .innerHTML);
  // alert (count );
    if (count==3)
    {
   //   alert ("jinru");
       isErrorFlag = true;
       fieldText += "员工|";
       msgText += "请添加员工|";  
    }
    //定义变量
    var errorRow = 0;
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，校验输入
	   //alert (table.rows[i].style.display);
	   
	    if (table.rows[i].style.display == "block")
	    {
	    var k=i-2;
	    //i=i-2;
	  // alert (i);
	       //alert ("1");
	    //alert (table .innerHTML);
            //增长技能数
            errorRow++;
            var listCount=errorRow ;
	        //发布媒体和渠道
	       // alert (i);
	        if (document.getElementById("txtEmployeeID_" + k))
	        {
              var   publishPlace = document.getElementById("txtEmployeeID_" + k).value.Trim();       
             // alert ("publishPlace");     
                //判断是否输入
              //  alert (publishPlace );
             //   alert (i);
                 isHave =true ;
                if( "" ==publishPlace)
                {
               // alert (listCount );
                    isErrorFlag = true;
                    fieldText += "信息发布 行：" + listCount+ " 员工|";
                    msgText += "请选择员工|";  
                }
            }
	        //完成期限
	            if (document.getElementById("txtCompleteDate_" + k))
	        {
               var      publishDate = document.getElementById("txtCompleteDate_" + k).value.Trim();            
                    //判断是否输入
                    if (publishDate == "")
                    {
//                        isErrorFlag = true;
//                        fieldText += "信息发布 行：" + errorRow + " 完成期限|";
//                        msgText += "请选择完成期限|";  
                    }
                    else  if(!isDate(publishDate))
                    {
                        isErrorFlag = true;
                        fieldText += "信息发布 行：" + errorRow + " 完成期限|";
                        msgText += "请输入正确的完成期限|";  
                     }
             }
             
//             if (document.getElementById("txtChecker_" + i))
//	        {
//                   var      Checker = document.getElementById("txtChecker_" + i).value;            
//                    //判断是否输入
//                    if(Checker == "")
//                    {
//                        isErrorFlag = true;
//                        fieldText += "信息发布 行：" + errorRow + " 核查人|";
//                        msgText += "请选择核查人|";  
//                    }
//              }
            //核查时间
              if (document.getElementById("txtCheckDate_" + k))
	        {
                var   CheckDate = document.getElementById("txtCheckDate_" + k).value.Trim();            
                //判断是否输入
                if (CheckDate == "")
                {
//                    isErrorFlag = true;
//                    fieldText += "信息发布 行：" + errorRow + " 核查时间|";
//                    msgText += "请选择核查时间|";  
                }
                else   if(!isDate(CheckDate))
                {
                    isErrorFlag = true;
                    fieldText += "信息发布 行：" + errorRow + " 核查时间|";
                    msgText += "请输入正确的核查时间|";  
                }
           }
         
	    }
	}
	if (isErrorFlag==false )
	{
        if (!isHave )
        { 
          isErrorFlag = true;
          fieldText += "员工项|";
          msgText += "请添加员工|";  
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
function GetBaseInfoParams()
{
    //编辑标识
    editFlag = document.getElementById("hidEditFlag").value;
    var param = "EditFlag=" + editFlag;
    //更新的时候
    if ("UPDATE" == editFlag)
    {
        //人员编号
        param += "&RectPlanNo=" + document.getElementById("divRectPalnNo").innerHTML;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("AimNum_ddlCodeRule").value.Trim();
        //手工输入的时候
        if ("" == codeRule)
        {
            //招聘活动编号
            param += "&RectPlanNo=" + document.getElementById("AimNum_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            param += "&CodeRuleID=" + document.getElementById("AimNum_ddlCodeRule").value.Trim();
        }
    }
    //主题
    param += "&Title=" +escape (  document.getElementById("txtTitle").value.Trim());
    param += "&PlanRemark=" +escape ( document.getElementById("txtPlanRemark").value.Trim());
    
    return param;
}
function GetPublishParams()
{  
    //获取表格
    table = document.getElementById("tbDetail");
    //获取表格行数
    var tableCount = table.rows.length;
    //定义变量
    var count = 0;
    var param = "";
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < tableCount ; i++)
	{
	    //不是删除行时，添加信息
	    if (table.rows[i].style.display =="block")
	    {
	       var k=i-2;
            //增长技能数
            count++;
	        //员工编号
	        if (document.getElementById("txtEmployeeID_" + k))
            param += "&EmployeeID_" + count + "=" + document.getElementById("txtEmployeeID_" + k).value.Trim();
	        //待改进计划
	         if (document.getElementById("txtContent_" + k))
            param += "&Content_" + count + "=" + escape ( document.getElementById("txtContent_" + k).value.Trim());
	        //完成目标
	          if (document.getElementById("txtCompleteAim_" + k))
            param += "&CompleteAim_" + count + "=" + document.getElementById("txtCompleteAim_" + k).value.Trim();
	        //完成时间
	          if (document.getElementById("txtCompleteDate_" + k))
            param += "&CompleteDate_" + count + "=" + document.getElementById("txtCompleteDate_" + k).value.Trim();
	        //核查人
	          if (document.getElementById("txtChecker_" + k))
            param += "&Checker_" + count + "=" + document.getElementById("txtChecker_" + k).value.Trim();
	        //核查结果
	          if (document.getElementById("txtCheckResult_" + k))
            param += "&CheckResult_" + count + "=" +escape ( document.getElementById("txtCheckResult_" + k).value.Trim());
	        //核查时间
	          if (document.getElementById("txtCheckDate_" + k))
            param += "&CheckDate_" + count + "=" + escape ( document.getElementById("txtCheckDate_" + k).value.Trim());
             //备注
               if (document.getElementById("txtRemark_" + k))
            param += "&Remark_" + count + "=" + escape ( document.getElementById("txtRemark_" + k).value.Trim());
            
	    }
	}
	//table表记录数
    param += "&PublishCount=" + count;
    //返回参数信息
    return param;
}
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