$(document).ready(function(){
    //DoGetTemplateInfo();
    //DoGetEmployeeInfo();
  // SearchPerTypeInfo();
  //  document.getElementById("hidEditFlag").value="INSERT";
 //   if (document .getElementById ("btnSearch"))
 //   {
   //DoSearchInfo();
 //   }
  //   var selctrlaimyear  =document.getElementById("selSearchTaskYear");
          
//    selctrlaimyear.length = 0;
//     document.getElementById("selSearchTaskYear").options.add(new Option("--请选择--",0));  
//    var startyear =   new Date().getYear()-10 ;
//    for( var i = 0;i<=30;i++){        
//    selctrlaimyear.options.add(new Option( (startyear+i),startyear+i )); 
//   }
//      selctrlaimyear.value =0;   
});

function GetLevelType(obj)
{
document .getElementById ("HidLevelTypeName").value=obj.options[obj.selectedIndex].text;
document .getElementById ("HidLevelTypeID").value=obj.selectedIndex;
}
function GetTaskNum(obj)
{
document .getElementById ("HidTaskNumName").value=obj.options[obj.selectedIndex].text;
document .getElementById ("HidTaskNumID").value=obj.selectedIndex;
}

function GetTaskYear(obj)
{
document .getElementById ("HidTaskYearName").value=obj.options[obj.selectedIndex].text;
document .getElementById ("HidTaskYearID").value=obj.selectedIndex;
}

function GetTaskFlag(obj)
{
document .getElementById ("HidTaskFlagName").value=obj.options[obj.selectedIndex].text;
document .getElementById ("HidTaskFlagID").value=obj.selectedIndex;
}
function GetPerType(obj)
{
document .getElementById ("HidPerTypeName").value=obj.options[obj.selectedIndex].text;
document .getElementById ("HidPerTypeID").value=obj.selectedIndex;
}
function getTaskNo()
{
document .getElementById ("hidTestNo").value=document .getElementById ("txtTaskTitle").title;

}



function getChangeAll()
{
              var m=document .getElementById ("ddlTaskFlag").value;
 if (m==1)//月考核
{
document.getElementById("ddlTaskYear").style.display="block";
document.getElementById("dvSearchTaslNum").style.display="block";
document.getElementById("ddlTaskNum").options.length=0;
document.getElementById("ddlTaskNum").options.add(new Option("--请选择--",0));  
document.getElementById("ddlTaskNum").options.add(new Option("1月",1));  
document.getElementById("ddlTaskNum").options.add(new Option("2月",2));  
document.getElementById("ddlTaskNum").options.add(new Option("3月",3));  
document.getElementById("ddlTaskNum").options.add(new Option("4月",4));  
document.getElementById("ddlTaskNum").options.add(new Option("5月",5));  
document.getElementById("ddlTaskNum").options.add(new Option("6月",6));  
document.getElementById("ddlTaskNum").options.add(new Option("7月",7));  
document.getElementById("ddlTaskNum").options.add(new Option("8月",8));  
document.getElementById("ddlTaskNum").options.add(new Option("9月",9));
document.getElementById("ddlTaskNum").options.add(new Option("10月",10));  
document.getElementById("ddlTaskNum").options.add(new Option("11月",11));  
document.getElementById("ddlTaskNum").options.add(new Option("12月",12));    
}
else if (m==2)//季考核
{
document.getElementById("ddlTaskYear").style.display="block";
document.getElementById("dvSearchTaslNum").style.display="block";
document.getElementById("ddlTaskNum").options.length=0;
document.getElementById("ddlTaskNum").options.add(new Option("--请选择--",0));  
document.getElementById("ddlTaskNum").options.add(new Option("第一季度",1));  
document.getElementById("ddlTaskNum").options.add(new Option("第二季度",2));  
document.getElementById("ddlTaskNum").options.add(new Option("第三季度",3));  
document.getElementById("ddlTaskNum").options.add(new Option("第四季度",4));     

}
else if (m==3)//半年考核
{

document.getElementById("ddlTaskYear").style.display="block";
document.getElementById("dvSearchTaslNum").style.display="block";
document.getElementById("ddlTaskNum").options.length=0;
document.getElementById("ddlTaskNum").options.add(new Option("--请选择--",0));  
document.getElementById("ddlTaskNum").options.add(new Option("上半年",1));  
document.getElementById("ddlTaskNum").options.add(new Option("下半年",2));  

}
else if (m==4)//年考核
{
document.getElementById("ddlTaskYear").style.display="block";
document.getElementById("dvSearchTaslNum").style.display="none";
document.getElementById("ddlTaskNum").options.length=0;



}
else if (m==5)//临时考核
{
document.getElementById("ddlTaskYear").style.display="none";
document.getElementById("dvSearchTaslNum").style.display="none";
document.getElementById("ddlTaskNum").options.length=0;
}
}

    function PrintRecibal()
       {
         var searchStr = document.getElementById("hidSearchCondition").value;
         window.open("PrintPerformanceDetails.aspx?type=1"+searchStr);
       }
function getSearchChange(obj)
{
var m=obj.options[obj.selectedIndex].value;
//var dateNow=new Date();
//var year=dateNow.getFullYear();
if (m==1)//月考核
{
document.getElementById("selSearchTaskYear").style.display="block";
document.getElementById("dvSearchTaslNum").style.display="block";
document.getElementById("selSearchTaskNum").options.length=0;
document.getElementById("selSearchTaskNum").options.add(new Option("--请选择--",0));  
document.getElementById("selSearchTaskNum").options.add(new Option("1月",1));  
document.getElementById("selSearchTaskNum").options.add(new Option("2月",2));  
document.getElementById("selSearchTaskNum").options.add(new Option("3月",3));  
document.getElementById("selSearchTaskNum").options.add(new Option("4月",4));  
document.getElementById("selSearchTaskNum").options.add(new Option("5月",5));  
document.getElementById("selSearchTaskNum").options.add(new Option("6月",6));  
document.getElementById("selSearchTaskNum").options.add(new Option("7月",7));  
document.getElementById("selSearchTaskNum").options.add(new Option("8月",8));  
document.getElementById("selSearchTaskNum").options.add(new Option("9月",9));
document.getElementById("selSearchTaskNum").options.add(new Option("10月",10));  
document.getElementById("selSearchTaskNum").options.add(new Option("11月",11));  
document.getElementById("selSearchTaskNum").options.add(new Option("12月",12));    
}
else if (m==2)//季考核
{
document.getElementById("selSearchTaskYear").style.display="block";
document.getElementById("dvSearchTaslNum").style.display="block";
document.getElementById("selSearchTaskNum").options.length=0;
document.getElementById("selSearchTaskNum").options.add(new Option("--请选择--",0));  
document.getElementById("selSearchTaskNum").options.add(new Option("第一季度",1));  
document.getElementById("selSearchTaskNum").options.add(new Option("第二季度",2));  
document.getElementById("selSearchTaskNum").options.add(new Option("第三季度",3));  
document.getElementById("selSearchTaskNum").options.add(new Option("第四季度",4));     

}
else if (m==3)//半年考核
{

document.getElementById("selSearchTaskYear").style.display="block";
document.getElementById("dvSearchTaslNum").style.display="block";
document.getElementById("selSearchTaskNum").options.length=0;
document.getElementById("selSearchTaskNum").options.add(new Option("--请选择--",0));  
document.getElementById("selSearchTaskNum").options.add(new Option("上半年",1));  
document.getElementById("selSearchTaskNum").options.add(new Option("下半年",2));  

}
else if (m==4)//年考核
{
document.getElementById("dvSearchTaslNum").style.display="none";
document.getElementById("selSearchTaskNum").options.length=0;



}
else if (m==5)//临时考核
{
document.getElementById("selSearchTaskYear").style.display="none";
document.getElementById("dvSearchTaslNum").style.display="none";
document.getElementById("selSearchTaskNum").options.length=0;
}

}

//根据下拉框自动填充内容
function getChange(obj)
{
var m=obj.options[obj.selectedIndex].value;

//var dateNow=new Date();
//var year=dateNow.getFullYear();
if (m==1)//月考核
{
document.getElementById("selTaskNum").options.length=0;
document.getElementById("selTaskNum").options.add(new Option("1月",1));  
document.getElementById("selTaskNum").options.add(new Option("2月",2));  
document.getElementById("selTaskNum").options.add(new Option("3月",3));  
document.getElementById("selTaskNum").options.add(new Option("4月",4));  
document.getElementById("selTaskNum").options.add(new Option("5月",5));  
document.getElementById("selTaskNum").options.add(new Option("6月",6));  
document.getElementById("selTaskNum").options.add(new Option("7月",7));  
document.getElementById("selTaskNum").options.add(new Option("8月",8));  
document.getElementById("selTaskNum").options.add(new Option("9月",9));
document.getElementById("selTaskNum").options.add(new Option("10月",10));  
document.getElementById("selTaskNum").options.add(new Option("11月",11));  
document.getElementById("selTaskNum").options.add(new Option("12月",12));    
}
else if (m==2)//季考核
{
document.getElementById("selTaskNum").options.length=0;
document.getElementById("selTaskNum").options.add(new Option("第一季度",1));  
document.getElementById("selTaskNum").options.add(new Option("第二季度",2));  
document.getElementById("selTaskNum").options.add(new Option("第三季度",3));  
document.getElementById("selTaskNum").options.add(new Option("第四季度",4));     

}
else if (m==3)//半年考核
{


document.getElementById("selTaskNum").options.length=0;
document.getElementById("selTaskNum").options.add(new Option("上半年",1));  
document.getElementById("selTaskNum").options.add(new Option("下半年",2));  

}
else if (m==4)//年考核
{
document.getElementById("dvTaslNum").style.diplay="none";
document.getElementById("selTaskNum").options.length=0;



}
else if (m==5)//临时考核
{
document.getElementById("dvTaslNum").style.diplay="none";
document.getElementById("selTaskNum").options.length=0;
}

}
//获取模板列表
function DoGetTemplateInfo()
{
    //编辑模式
    //document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
   /// document.getElementById("hidElemID").value = elemID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTask.ashx?action=GetTemplateInfo",
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
              popMsgObj.ShowMsg(' 请求发生错误！');
        }, 
        success:function(msg) 
        {
            //隐藏提示框  
            hidePopup();
            /* 设置考核类型信息 */
          //  $.each(msg.data, function(i,item){
                //要素名称
           //     document.getElementById("txtEditElemName").value = item.TypeName;
                //启用状态
           //     document.getElementById("sltEditUsedStatus").value = item.UsedStatus;
           // });
            
            
          //  $("#tbTemplateEdit tbody").find("tr.newro").remove();
//              var tab=document .getElementById("tbTemplateEdit");
//        var sbody=tab.tBodies [0];
//       
//        var rowCount=sbody .rows.length-1;
//        //alert (rowCount);
//        if (rowCount >0)
//        {
//          for (var col=1;col <rowCount+1 ;col++)
//          {
//          //  alert (col );
//            sbody.deleteRow(1);
//          }
//        }
            
//            document.getElementById ("divShowEdit").style.display="block";
//             document.getElementById("selPerformanceType").options.length=0;
//              document.getElementById("selPerformanceType").options.add(new Option("请选择","请选择"))
//                document.getElementById("selSearchPerformanceType").options.length=0;
//              document.getElementById("selSearchPerformanceType").options.add(new Option("请选择","请选择"))
           
           
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.TemplateNo != null && item.TemplateNo != "")
                       $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"
                        + "<input id='chkSelect' class='TemplateNo'  name='chkSelect'onclick =DoGetEmployeeInfoByTemplateNo("+item.TemplateNo +") title='"+item.TemplateNo +"'   value='" + item.TemplateNo + "'  type='checkbox'/>" + "</td>" //选择框
                        + "<td height='22' align='center'><a href='#' onclick =DoGetEmployeeInfoByTemplateNo("+item.TemplateNo +")  >" + item.Title + "</a></td>").appendTo($("#tbTemplate tbody")
                        );
                        
                  
            });
        }
    });
    //显示修改页面
   // document.getElementById("divEditCheckItem").style.display = "block"; 
}
//获取员工列表
function DoGetEmployeeInfo()
{
    //编辑模式
    //document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
   /// document.getElementById("hidElemID").value = elemID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTask.ashx?action=GetEmployeeInf",
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
           
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
              popMsgObj.ShowMsg(' 请求发生错误！');
        }, 
        success:function(msg) 
        {
             
            //隐藏提示框  
            hidePopup();
            /* 设置考核类型信息 */
          //  $.each(msg.data, function(i,item){
                //要素名称
           //     document.getElementById("txtEditElemName").value = item.TypeName;
                //启用状态
           //     document.getElementById("sltEditUsedStatus").value = item.UsedStatus;
           // });
            
            
          //  $("#tbTemplateEdit tbody").find("tr.newro").remove();
//              var tab=document .getElementById("tbTemplateEdit");
//        var sbody=tab.tBodies [0];
//       
//        var rowCount=sbody .rows.length-1;
//        //alert (rowCount);
//        if (rowCount >0)
//        {
//          for (var col=1;col <rowCount+1 ;col++)
//          {
//          //  alert (col );
//            sbody.deleteRow(1);
//          }
//        }
            
//            document.getElementById ("divShowEdit").style.display="block";
//             document.getElementById("selPerformanceType").options.length=0;
//              document.getElementById("selPerformanceType").options.add(new Option("请选择","请选择"))
//                document.getElementById("selSearchPerformanceType").options.length=0;
//              document.getElementById("selSearchPerformanceType").options.add(new Option("请选择","请选择"))
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.EmployeeID != null && item.EmployeeID != "")
                       $("<tr ></tr>").append("<td height='22' align='center'>"
                        + "<input id='chkSelect' class='EmployeeID'  name='chkSelect' value='" + item.EmployeeID + "'  type='checkbox' title='"+item.EmployeeID +"'/>" + "</td>" //选择框
                        + "<td height='22' align='center'>" + item.EmployeeName + "</td>").appendTo($("#tbEmployee tbody")
                        );
                        
                  
            });
        }
    });
    //显示修改页面
   // document.getElementById("divEditCheckItem").style.display = "block"; 
  
}
//根据员工列表选中相应模块的人员
function changeSelect(employeeID)
{ // alert (employeeID); 
 var tmList=$(".EmployeeID");
 var tmListlen=tmList.length;
 for (var a=0;a<tmListlen ;a++)//取消所有的勾选项
 {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
    if ( tmList[a] .checked==true)
    {
       tmList[a] .checked=false ;
      }
  
  }
          


   if (employeeID !=null  && employeeID.length!=0)
   {
       var empListLen=employeeID .length;
       for (var i=0;i<empListLen ;i++  )
       {
      
          for (var a=0;a<tmListlen ;a++)
          {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
            if ( tmList[a].value==employeeID[i])
           {
              tmList[a] .checked=true;
            }
  
          }
       }
   
   }


}


//获得员工列表（根据模板编号）
function DoGetEmployeeInfoByTemplateNo(templateNo)
{
    //编辑模式
    //document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
   /// document.getElementById("hidElemID").value = elemID;
   var employeeID=new Array ();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTask.ashx?action=GetEmployeeInfoByTemplateNo&templateNo="+templateNo ,
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
          ///  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
              popMsgObj.ShowMsg(' 请求发生错误！');
        }, 
        success:function(msg) 
        {
            //隐藏提示框  
            hidePopup();
            /* 设置考核类型信息 */
          //  $.each(msg.data, function(i,item){
                //要素名称
           //     document.getElementById("txtEditElemName").value = item.TypeName;
                //启用状态
           //     document.getElementById("sltEditUsedStatus").value = item.UsedStatus;
           // });
            
            
          //  $("#tbTemplateEdit tbody").find("tr.newro").remove();
//              var tab=document .getElementById("tbTemplateEdit");
//        var sbody=tab.tBodies [0];
//       
//        var rowCount=sbody .rows.length-1;
//        //alert (rowCount);
//        if (rowCount >0)
//        {
//          for (var col=1;col <rowCount+1 ;col++)
//          {
//          //  alert (col );
//            sbody.deleteRow(1);
//          }
//        }
            
//            document.getElementById ("divShowEdit").style.display="block";
//             document.getElementById("selPerformanceType").options.length=0;
//              document.getElementById("selPerformanceType").options.add(new Option("请选择","请选择"))
//                document.getElementById("selSearchPerformanceType").options.length=0;
//              document.getElementById("selSearchPerformanceType").options.add(new Option("请选择","请选择"))
           
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.EmployeeID != null && item.EmployeeID != "")
                     employeeID.push (item.EmployeeID)
                        
                       // alert (employeeID); 
                          changeSelect(employeeID);
                        
                  
            });
        }
    });
    //显示修改页面
   // document.getElementById("divEditCheckItem").style.display = "block"; 
   
  
}

function Show()
{

AlertMsg();
document.getElementById ("divEditCheckItem").style.display="block" ;
ClearInput();
}
function ClearInput()
{

//document .getElementById ("trSearch").style.display="none";   

document.getElementById ("inptTitle").value="";
document.getElementById ("txtStartDate").value="";
document.getElementById ("txtEndDate").value="";
 var tmList=$(".EmployeeID");
 var tmListlen=tmList.length;
 var tmListEmp=$(".TemplateNo");
 var  tmListEmplen=tmListEmp.length;
 for (var a=0;a<tmListlen ;a++)//取消所有的勾选项
 {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
    if ( tmList[a] .checked==true)
    {
       tmList[a] .checked=false ;
      }
  
  }
   for (var b=0;b<tmListEmplen ;b++)//取消所有的勾选项
 {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
    if ( tmListEmp[b] .checked==true)
    {
       tmListEmp[b] .checked=false ;
      }
  
  }

}

	function AlertMsg(){

	   /**第一步：创建DIV遮罩层。*/
		var sWidth,sHeight;
		sWidth = window.screen.availWidth;
		//屏幕可用工作区高度： window.screen.availHeight;
		//屏幕可用工作区宽度： window.screen.availWidth;
		//网页正文全文宽：     document.body.scrollWidth;
		//网页正文全文高：     document.body.scrollHeight;
		if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
			sHeight = window.screen.availHeight;  
		}else{//当高度大于一屏
			sHeight = document.body.scrollHeight;   
		}
		//创建遮罩背景
		var maskObj = document.createElement("div");
		maskObj.setAttribute('id','BigDiv');
		maskObj.style.position = "absolute";
		maskObj.style.top = "0";
		maskObj.style.left = "0";
		maskObj.style.background = "#777";
		maskObj.style.filter = "Alpha(opacity=30);";
		maskObj.style.opacity = "0.3";
		maskObj.style.width = sWidth + "px";
		maskObj.style.height = sHeight + "px";
		maskObj.style.zIndex = "900";
		document.body.appendChild(maskObj);
		
	}

		function CloseDiv(){
		var Bigdiv = document.getElementById("BigDiv");
		//var Mydiv = document.getElementById("div_Add");
		if (Bigdiv)
		{
		document.body.removeChild(Bigdiv); 
		}
//         Bigdiv.style.display = "none";
	//	Mydiv.style.display="none";
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
        
          aim_codeRule = document.getElementById("AimNum_ddlCodeRule").value.Trim();
        //  alert (aim_codeRule);
       // alert (aim_codeRule);
        //手工输入的时候
        if ("" == aim_codeRule)
        {
     
            //人员编号
           if (""== document.getElementById("AimNum_txtCode").value.Trim())
           {
            isErrorFlag = true;
        fieldText += "考核任务编号|";
        msgText += "请输入考核任务编号|";
           }
          
           if ( isnumberorLetters( document.getElementById("AimNum_txtCode").value.Trim()))
           {
            isErrorFlag = true;
            fieldText += "考核任务编号|";
             msgText += "考核任务编号只能包含字母或数字！|";
           }
            var num=document.getElementById("AimNum_txtCode").value.Trim();
           if (checkstr(num,50))
           {
             isErrorFlag = true;
            fieldText += "考核任务编号|";
             msgText += "考核任务编号长度过长！|";
           }
           
           
        }
        else
        {
            if (isnumberorLetters( document.getElementById("AimNum_ddlCodeRule").value.Trim()))
          {
            isErrorFlag = true;
            fieldText += "考核任务编号|";
             msgText += "考核任务编号只能包含字母或数字！|";
          }
          
        
           
           
          
        }
        
    //考核任务主题必须输入
    if (document.getElementById("inptTitle").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "考核任务主题|";
        msgText += "请输入考核任务主题|";  
    }
    
     var startdate=null ;
     var endDate=null ;
    //考核期间开始时间必须输入
    if (document.getElementById("txtStartDate").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "考核期间开始时间|";
        msgText += "请录入开始时间|";  
    }
    else
    {
   var  temp1=document.getElementById("txtStartDate").value.Trim();
    var startLen=temp1.length;
   
     if (startLen==16)
     {
      startdate=temp1 .substring(0,9);
      startdate = startdate .replace("-","");
       startdate = startdate .replace("-","");
      
       //alert (startdate );
      }else
      {
       startdate=temp1 .substring(0,10);
        startdate = startdate .replace("-","");
         startdate = startdate .replace("-","");
     // alert (startdate );
      
      }
     // var startResult=startdate.
     
    }
   // ss=document.getElementById("txtStartDate").value;
   // alert(ss );
   // alert (ss.length);
    //考核期间结束时间必须输入
    if (document.getElementById("txtEndDate").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "考核期间结束时间|";
        msgText += "请录入结束时间|";  
    }else
    {
    
    
     var  temp2=document.getElementById("txtEndDate").value.Trim();
    var startLen=temp2.length;
   
     if (startLen==16)
     {
      endDate=temp2 .substring(0,9);
      endDate = endDate .replace("-","");
       endDate = endDate .replace("-","");
      
      // alert (endDate );
      }else
      {
       endDate=temp2 .substring(0,10);
        endDate = endDate .replace("-","");
         endDate = endDate .replace("-","");
    //  alert (endDate );
      }
    }
    
    if (startdate!=null && endDate!=null  )
    {
    if (startdate >endDate )
    {
       isErrorFlag = true;
        fieldText += "考核开始日期|";
        msgText += "开始日期不准晚于考核结束日期|"; 
    
    }
    }
    
    
     var tmList=$(".EmployeeID");
 var tmListlen=tmList.length;
 var tmListEmp=$(".TemplateNo");
 var  tmListEmplen=tmListEmp.length;
 var isEmploee=false ;
 var isTemplate=false ;
 for (var a=0;a<tmListlen ;a++)//取消所有的勾选项
 {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
    if ( tmList[a] .checked==true)
    {
      isEmploee=true ;
      break ;
      }
  
  }
   for (var b=0;b<tmListEmplen ;b++)//取消所有的勾选项
 {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
    if ( tmListEmp[b] .checked==true)
    {
         isTemplate=true ;
         break ;
      }
  
  }
  if (!isEmploee)
  {
         isErrorFlag = true;
        fieldText += "被考核人|";
        msgText += "请选择被考核人|";  
  }
   if (!isTemplate)
  {
         isErrorFlag = true;
        fieldText += "考核模板|";
        msgText += "请选择考核模板|";  
  }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText, msgText);
    }

    return isErrorFlag;
}
function GetBaseInfoParams()
{
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value.Trim();
    //新建时，编号选择手工输入时
    var param = "EditFlag=" + editFlag;
    //类型ID
    param += "&Title=" + escape ( document.getElementById("inptTitle").value.Trim());
    //考核类型名称
    param += "&StartDate=" + document.getElementById("txtStartDate").value.Trim();
    //启用状态
    param += "&EndDate=" + document.getElementById("txtEndDate").value.Trim();
    
    var taskFlag=document.getElementById("selTaskFlag").value.Trim();
    var taskNum=document.getElementById("selTaskNum").value.Trim();
      param += "&TaskFlag=" +taskFlag ;
        param += "&TaskNum=" +taskNum ;
        param += "&Remark=" +escape ( document.getElementById("inpRemark").value.Trim());
        
         if (editFlag !="UPDATE")
         {
        
           aim_codeRule = document.getElementById("AimNum_ddlCodeRule").value.Trim();
       // alert (aim_codeRule);
        //手工输入的时候
            if ("" == aim_codeRule)
          {
            //人员编号
             param += "&TaskNo=" + document.getElementById("AimNum_txtCode").value.Trim();
           }
          else
         {
            //编码规则ID
             param += "&TaskNo=" + document.getElementById("AimNum_ddlCodeRule").value.Trim();
          }
        }else
        {
         param += "&TaskNo=" + document.getElementById("hidElemID").value.Trim();
        }
    
         var tmList=$(".EmployeeID");
         var tmListlen=tmList.length;
  var tmListEmp=$(".TemplateNo");
 var  tmListEmplen=tmListEmp.length;
 var employeeIDList=new Array ();
 var TemplateNoList=new Array ();
 for (var a=0;a<tmListlen ;a++)//取消所有的勾选项
 {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
    if ( tmList[a] .checked==true)
    {
      employeeIDList.push (tmList[a] .value);
      }
  
  }
   for (var b=0;b<tmListEmplen ;b++)//取消所有的勾选项
 {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
    if ( tmListEmp[b] .checked==true)
    {
         TemplateNoList.push (tmListEmp[b] .value);
      }
  
  }
   param += "&employeeIDList=" +employeeIDList;
   param += "&TemplateNoList=" +TemplateNoList;
    // alert ( document.getElementById("AimNum_txtCode").value);
     //  alert ( document.getElementById("AimNum_ddlCodeRule").value);
    //alert (document.getElementById("hidParentElemNo").value);
    
    
    return param;
}
	function DoSave()
	{
	  /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTask.ashx?action=EditInfo&" + postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
           /// showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
              popMsgObj.ShowMsg(' 请求发生错误！');
        }, 
        success:function(data) 
        {
            //隐藏提示框
            hidePopup();
            //保存成功
            if(data.sta == 1) 
            { 
                //设置编辑模式
                document.getElementById("hidEditFlag").value = data.info;//保存编辑标示
                //设置ID 
                document.getElementById("hidElemID").value = data.data;//taskNo编号
                 document.getElementById("txtPerformTmNo").style.display="none"; 
                 document.getElementById("inpTaskNo").style.display="block";
                   document.getElementById("inpTaskNo").value = data.data;//taskNo编号
                //设置提示信息
                ///showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                   popMsgObj.ShowMsg(' 保存成功！');
            }
            else  if(data.sta == 2) 
            { 
              
                  popMsgObj.ShowMsg(' 该考核任务编号已存在！');
            }
            //保存失败
            else 
            { 
                hidePopup();
                ///showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                  popMsgObj.ShowMsg(' 保存失败,请确认！');
            }
        } 
    });  
	
	
	
	
	
	
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
    
    
    function DoSearchInfo(currPage)
{ 
    var search = "";
    //要素名称
        search += "&DeptID=" + document.getElementById("txtDeptID").value.Trim();
       // alert (document.getElementById("txtDeptID").value);
    //启用状态
    search += "&PerTypeID=" + document.getElementById("SelPerType").value.Trim();
     search += "&TaskFlag=" + document.getElementById("selSearchTaskFlag").value.Trim();
       search += "&TaskNum=" + document.getElementById("selSearchTaskNum").value.Trim();
          search += "&LevelType=" + document.getElementById("SelLevelType").value.Trim();
         search += "&TaskDate=" + document.getElementById("selSearchTaskYear").value.Trim();
             search += "&TaskNo=" + document.getElementById("txtTaskTitle").title;
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    
    TurnToPage(1);
  
}
/* 分页相关变量定义 */  

var pageCount = 10;//每页显示记录数
var totalRecord = 0;//总记录数
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;//当前页
var orderBy = "";//排序字段


/*
* 翻页处理
*/
function TurnToPage(pageIndex)
{
    //设置当前页
    currentPageIndex = pageIndex;
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //设置动作种类
    var action="SearchDetailsInfoByLT";
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&action=" + action + "&OrderBy=" + orderBy + "&" + searchCondition;
  
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/PerformanceQuery.ashx?' + postParam,//目标地址
        dataType:"json",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(msg)
        {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#tblDetailInfo tbody").find("tr.newrow").remove();
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.TaskNo != null && item.TaskNo != "")
                    $("<tr class='newrow'></tr>").append(
                    "<td height='22' align='center'>" + item.TaskNo + "</td>" 
                    +"<td height='22' align='center'>" + item.Remark + "</td>" ).appendTo($("#tblDetailInfo tbody")
                          //启用状态
                    );
            });
            //页码
            ShowPageBar(
                "divPageClickInfo",//[containerId]提供装载页码栏的容器标签的客户端ID
                "<%= Request.Url.AbsolutePath %>",//[url]
                {
                    style:pagerStyle,mark:"DetailListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上一页",
                    nextWord:"下一页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"
                }
            );
            totalRecord = msg.totalCount;
            $("#txtShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex,$("#pagecount"));
            $("#txtToPage").val(pageIndex);
        
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete:function(){
            hidePopup();
            $("#divPageClickInfo").show();
            SetTableRowColor("tblDetailInfo","#E7E7E7","#FFFFFF","#cfc","cfc");
        }  
    });
}
/*
* 设置数据明细表的行颜色
*/
function SetTableRowColor(elem,colora,colorb, colorc, colord){
    //获取DIV中 行数据
    var tableTr = document.getElementById(elem).getElementsByTagName("tr");
    for(var i = 0; i < tableTr.length; i++)
    {
        //设置行颜色
        tableTr[i].style.backgroundColor = (tableTr[i].sectionRowIndex%2 == 0) ? colora:colorb;
        //设置鼠标落在行上时的颜色
        tableTr[i].onmouseover = function()
        {
            if(this.x != "1") this.style.backgroundColor = colorc;
        }
        //设置鼠标离开行时的颜色
        tableTr[i].onmouseout = function()
        {
            if(this.x != "1") this.style.backgroundColor = (this.sectionRowIndex%2 == 0) ? colora:colorb;
        }
    }
}

/*
* 排序处理
*/
function OrderBy(orderColum,orderTip)
{
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderBy = orderColum+"_"+ordering;
    TurnToPage(1);
}
function ChangePageCountIndex(newPageCount, newPageIndex)
{
    //判断是否是数字
    if (!IsNumber(newPageCount))
    {
        popMsgObj.ShowMsg('请输入正确的显示条数！');
        return;
    }
    if (!IsNumber(newPageIndex))
    {
        popMsgObj.ShowMsg('请输入正确的转到页数！');
        return;
    }
    //判断重置的页数是否超过最大页数
    if(newPageCount <=0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1)/newPageCount) + 1)
    {
        popMsgObj.ShowMsg('转至页数超出查询范围！');
    }
    else
    {
        //设置每页显示记录数
        this.pageCount = parseInt(newPageCount);
        //显示页面数据
        TurnToPage(parseInt(newPageIndex));
    }
}



function DoModify(taskNo)
{AlertMsg();
        document.getElementById("hidEditFlag").value="UPDATE";
        document.getElementById("hidElemID").value = taskNo;
      document.getElementById("txtPerformTmNo").style.display="none"; 
                 document.getElementById("inpTaskNo").style.display="block";
                   document.getElementById("inpTaskNo").value = taskNo;//taskNo编号
    //编辑模式
    //document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
   /// document.getElementById("hidElemID").value = elemID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTask.ashx?action=GetTaskInfoByTaskNO&taskNo="+taskNo ,
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
          //  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
              popMsgObj.ShowMsg(' 请求发生错误！');
        }, 
          success:function(msg) 
        {
            //隐藏提示框  
            var employee=new Array ();
            var templateNo=new Array ();
            hidePopup();
            /* 设置考核类型信息 */
            $.each(msg.data, function(i,item){
                //要素名称
         
                    
                         document.getElementById ("inptTitle").value=item.Title;
                          document.getElementById ("txtStartDate").value=item.StartDate.substring(0,10);
                          document.getElementById ("txtEndDate").value=item.EndDate.substring(0,10);
                      document.getElementById ("inpRemark").value=item.Remark;
                         objSelect= document.getElementById ("selTaskFlag");
                         objItemValue=item.TaskFlag;
                          for (var i = 0; i < objSelect.options.length; i++) {        
                           if (objSelect.options[i].value == objItemValue)
                         {        
                            document.getElementById ("selTaskFlag").value=objItemValue; 
                           
                            break;        
                         }
                        }
                       //  alert (document.getElementById ("selTaskFlag").selectedIndex);    
                        if (objItemValue =="4"|| objItemValue=="5")   
                        {
                           document.getElementById("dvTaslNum").style.diplay="none";
                           document.getElementById("selTaskNum").options.length=0;
                        }
                        else
                        {
                         var obj=document.getElementById("selTaskFlag");
                           getChange(obj);
                           objSelect1= document.getElementById ("selTaskNum");
                           objItemValue1=item.TaskNum;
                           for (var a = 0; a < objSelect1.options.length; a++)
                           {        
                              if (objSelect1.options[i].value == objItemValue1)
                             {        
                              document.getElementById ("selTaskNum").value=objItemValue1;     
                               break;        
                              }
                            }
                        
                        } 
                        employee.push (item.EmployeeID);
                        templateNo.push (item.TemplateNo);
                        changeSelect(employee);
                        changeTemplateSelect(templateNo);
                       
                        
                           
            });
        }
    });
    //显示修改页面
    document.getElementById("divEditCheckItem").style.display = "block"; 
  
}
function changeTemplateSelect(employeeID)
{ // alert (employeeID); 
 var tmList=$(".TemplateNo");
 var tmListlen=tmList.length;
 for (var a=0;a<tmListlen ;a++)//取消所有的勾选项
 {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
    if ( tmList[a] .checked==true)
    {
       tmList[a] .checked=false ;
      }
  
  }
          


   if (employeeID !=null  && employeeID.length!=0)
   {
       var empListLen=employeeID .length;
       for (var i=0;i<empListLen ;i++  )
       {
      
          for (var a=0;a<tmListlen ;a++)
          {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
            if ( tmList[a].value==employeeID[i])
           {
              tmList[a] .checked=true;
            }
  
          }
       }
   
   }


}

function DoBack()
{

CloseDiv();
document .getElementById ("divEditCheckItem").style.display="none";
document .getElementById ("inpTaskNo").value="";
document .getElementById ("inptTitle").value="";
document .getElementById ("inpRemark").value="";


document .getElementById ("txtEndDate").value="";
document .getElementById ("txtStartDate").value="";
 var tmList=$(".TemplateNo");
 var tmListlen=tmList.length;
 for (var a=0;a<tmListlen ;a++)//取消所有的勾选项
 {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
    if ( tmList[a] .checked==true)
    {
       tmList[a] .checked=false ;
      }
  
  }
  var tmList1=$(".EmployeeID");
 var tmListlen1=tmList1.length;
 for (var c=0;c<tmListlen1 ;c++)//取消所有的勾选项
 {
          //alert (tmList[a].value); 
          // alert (employeeID[i]);
    if ( tmList1[c] .checked==true)
    {
       tmList1[c] .checked=false ;
      }
  
  }


}
function AlertMsg(){

	   /**第一步：创建DIV遮罩层。*/
		var sWidth,sHeight;
		sWidth = window.screen.availWidth;
		//屏幕可用工作区高度： window.screen.availHeight;
		//屏幕可用工作区宽度： window.screen.availWidth;
		//网页正文全文宽：     document.body.scrollWidth;
		//网页正文全文高：     document.body.scrollHeight;
		if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
			sHeight = window.screen.availHeight;  
		}else{//当高度大于一屏
			sHeight = document.body.scrollHeight;   
		}
		//创建遮罩背景
		var maskObj = document.createElement("div");
		maskObj.setAttribute('id','BigDiv');
		maskObj.style.position = "absolute";
		maskObj.style.top = "0";
		maskObj.style.left = "0";
		maskObj.style.background = "#777";
		maskObj.style.filter = "Alpha(opacity=30);";
		maskObj.style.opacity = "0.3";
		maskObj.style.width = sWidth + "px";
		maskObj.style.height = sHeight + "px";
		maskObj.style.zIndex = "200";
		document.body.appendChild(maskObj);
		
	}

		function CloseDiv(){
		var Bigdiv = document.getElementById("BigDiv");
		//var Mydiv = document.getElementById("div_Add");
		if (Bigdiv)
		{
		document.body.removeChild(Bigdiv);
		} 
//         Bigdiv.style.display = "none";
		///Mydiv.style.display="none";
	}
	
	
	
	 function DoDelete()
{
    //获取选择框
    var chkList = document.getElementsByName("chkSelect");
    var chkValue = "";    
    for( var i = 0; i < chkList.length; i++ )
    {
        //判断选择框是否是选中的
        if ( chkList[i].checked )
        {
           chkValue += "'" + chkList[i].value + "',";
        }
    }
    var deleteNos = chkValue.substring(0, chkValue.length - 1);
    selectLength = chkValue.split("',");
    if(chkValue == "" || chkValue == null || selectLength.length < 1)
    {
       // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
           popMsgObj.ShowMsg('  请至少选择一项删除！');
        return;
    }
    else
    {
        var postParam = "action=DeleteInfo&DeleteNO=" + escape(deleteNos);
        //删除
        $.ajax(
        { 
            type: "POST",
            url: "../../../Handler/Office/HumanManager/PerformanceTask.ashx?" + postParam,
            dataType:'json',//返回json格式数据
            cache:false,
            beforeSend:function()
            { 

            },
            error: function() 
            {
              ///  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg('请求发生错误！');
            }, 
            success:function(data) 
            { 
                if(data.sta == 1) 
                { 
                    // 
                    // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                      popMsgObj.ShowMsg('删除成功！');
                    TurnToPage(1);
                    //
                  
                } 
                else if(data.sta == 2) 
                {
                    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                     popMsgObj.ShowMsg('要素已经被使用 ,请确认！');
                }
                else
                {
                   // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                     popMsgObj.ShowMsg('删除失败 ,请确认！');
                } 
            } 
        });
    }
}


function SearchPerTypeInfo()
{
 document.getElementById("SelPerType").options.length=0;
 document.getElementById("SelPerType").options.add(new Option('--请选择--',0)); 
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceQuery.ashx?Action=SearchPerTypeInfo" ,
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","");
             popMsgObj.ShowMsg('请求发生错误！');
        }, 
        success:function(msg) 
        {
            //隐藏提示框  
            hidePopup();
            /* 设置考核类型信息 */  
       
            $.each(msg.data, function(i,item){
                if(item.ID != null && item.ID != "")  
                  
             document.getElementById("SelPerType").options.add(new Option(item.TypeName,item .ID));  
            });
        }
    });

}