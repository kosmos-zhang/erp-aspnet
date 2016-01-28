$(document).ready(function(){
    //DoGetTemplateInfo();
   // DoGetEmployeeInfo();
    
    document.getElementById("hidEditFlag").value="INSERT";
    
});
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
	
function Caculate()
{
 if (CheckBaseInfo())
    {
        return;
    }
   var elemList = new Array();
   var tmList = $(".ElemScore");
   var tmListlen = tmList.length;
    var sum=0.00;
    for (var a = 0; a < tmListlen; a++)//取消所有的勾选项
    {
        
        
       var temp= tmList [a].title.split("_");
      // alert (temp[1]);
        
      sum=sum + parseFloat(tmList[a].value)*parseFloat(temp[1])/100;
     
     
        

    }
    document .getElementById ("inpAccount").value=sum;
  
    
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
    var tmList = $(".ElemScore");
    var tmListlen = tmList.length;
    for (var a = 0; a < tmListlen; a++)//取消所有的勾选项
    {
        //alert (tmList[a].value); 
        // alert (employeeID[i]);
        if (tmList[a].value.Trim() == "") {

            isErrorFlag = true;
            fieldText += "评分项目|";
            msgText += "您没有评分|";
        } else {
        if (!IsNumeric(tmList[a].value.Trim())) {
            isErrorFlag = true;
            fieldText += "评分项目|";
            msgText += "评分项必须为数字|";
        }
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
function GetBaseInfoParams()
{
   var  TaskNo = document.getElementById("inpTaskNo").value.Trim();
   var  param = "TaskNo=" + TaskNo;
 
    return param;
}
	function DoSave()
	{
	  /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
       if(confirm("汇总后不可修改，继续汇总吗！"))
	      {
    if (CheckBaseInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceSummary.ashx?action=GatherInfo&" + postParams,
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
               
                  if (data.info=="1")//1为确认状态
                  {
                  document .getElementById ("btnGather").style.display="none";
                     popMsgObj.ShowMsg(' 确认成功！');
                     
                   //self.location='PerformanceSummary.aspx';                   
                  }else
                  {
                   popMsgObj.ShowMsg(' 保存成功！');
                  }
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
    search += "TaskNo=" + escape ( document.getElementById("txtSearchTask").value.Trim());
    //启用状态
    search += "&Title=" + escape ( document.getElementById("inptSearchTitle").value.Trim());
     search += "&TaskFlag=" + document.getElementById("selSearchTaskFlag").value.Trim();
       search += "&TaskNum=" + document.getElementById("selSearchTaskNum").value.Trim();
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
{
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
        url: "../../../Handler/Office/HumanManager/PerformanceSummary.ashx?action=GetTaskInfoByTaskNO&taskNo="+taskNo ,
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
                        if (item.Remark!=null && item.Remark!="")
                        {
                          document.getElementById("inpRemark").value = item.Remark;
                         }
                          document.getElementById("hidEmployeId").value = item.EmployeeID;
//                        if (item.Status =="1")//已确认的打分记录
//                        {
//                         document.getElementById("btnSave").style.display="none"; 
//                         document.getElementById("btnCheck").style.display="none"; 
//                         document.getElementById("inpAdviceNote").disabled = "true";
//                         document.getElementById("inpNote").disabled = "true"; 
//                         }
//                        if (item.AdviceNote!=null && item.AdviceNote!="")
//                        {
//                         document.getElementById("inpAdviceNote").value = item.AdviceNote;  
//                        }
//                        if (item.Note!=null && item.Note!="")
//                        {
//                         document.getElementById("inpNote").value = item.Note;
//                        }
document.getElementById ("selTaskFlag").innerHTML=item.TaskFlag;

//                         objSelect= document.getElementById ("selTaskFlag");
//                         objItemValue=item.TaskFlag;
//                          for (var i = 0; i < objSelect.options.length; i++) {        
//                           if (objSelect.options[i].value == objItemValue)
//                         {        
//                            document.getElementById ("selTaskFlag").value=objItemValue; 
//                           
//                            break;        
//                         }
//                        }
                       //  alert (document.getElementById ("selTaskFlag").selectedIndex);  
                       
                       if (item .TaskStaus=="1")
                       {
                         document .getElementById ("btnGather").style.display="block";
                       }else
                       {
                        document .getElementById ("btnGather").style.display="None";
                       }
                         
                         document.getElementById ("inpTaskStaus").innerHTML=item .StatusName;  
                        if (item.TaskFlag=="临时考核")   
                        {
                           document.getElementById("dvTaslNum").style.display="none";
//                           document.getElementById("selTaskNum").options.length=0;
                        }
                        else
                        {document.getElementById ("dvTaslNum").innerHTML=item.TaskNum;
//                         var obj=document.getElementById("selTaskFlag");
//                           getChange(obj);
//                           objSelect1= document.getElementById ("selTaskNum");
//                           objItemValue1=item.TaskNum;
//                           for (var a = 0; a < objSelect1.options.length; a++)
//                           {        
//                              if (objSelect1.options[i].value == objItemValue1)
//                             {        
//                              document.getElementById ("selTaskNum").value=objItemValue1;     
//                               break;        
//                              }
//                            }
                        
                        } 
                       /// alert (msg .info);
                       if ( msg .info!=null )
                      {
                       var info=msg.info;//考核人数_已完成人数_未完成人数_是否可汇总
                      // alert (info );
                       var infolist=  info.split("_");
                       //alert (infolist );
                      // alert (infolist [0]);
                     document .getElementById ("inpEmployee").innerHTML=infolist [0];
                        document .getElementById ("inpHasScore").innerHTML=infolist [1];
                          document .getElementById ("inpNotScore").innerHTML=infolist [2];
                       if (infolist [3]=="1")
                            {
                           // alert (item .TaskStaus);
                              if (item .TaskStaus=="4")
                              {
                              document .getElementById ("inpIsCheck").innerHTML="是";
                                 document .getElementById ("btnGather").style.display="Block";
                              }else
                              {
                               document .getElementById ("inpIsCheck").innerHTML="已汇总";
                              }
                             
                           }else
                           {
                            document .getElementById ("btnGather").style.display="None";
                              document .getElementById ("inpIsCheck").innerHTML="否";
                            }
                             
                       
                       } 
                         // alert (msg.sta );
//                        employee.push (item.EmployeeID);
//                        templateNo.push (item.TemplateNo);
//                        changeSelect(employee);  AsseStandard
//                        changeTemplateSelect(templateNo);

//                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.ParentElemName +"|"+ item.ElemName + "</td>"
//                     
//                        + "<td height='22' align='center'>" + item.ScoreRules + "</td>"
//                         + "<td height='22' align='center'>" + item.StandardScore + "</td>"
//                          + "<td height='22' align='center'><input id='234'  class='ElemScore' type='text' value='"+item .Score +"' title='" + item.ElemID+"_"+item .ElemRate +"' onblur=textcontrol(this," + item.MinScore + "," + item.MaxScore + ") /> </td>"
//                            + "<td height='22' align='center'>" + item.ElemRate +"</td>"
//                          + "<td height='22' align='center'>" + item.AsseStandard +"</td>"
//                           + "<td height='22' align='center'>" + item.AsseFrom +"</td>"
//                           + "<td height='22' align='center'>" + item.Remark +"</td>").appendTo($("#tblDetailInfo tbody")
                           
                          //启用状态
//                    );
                        
                           
            });
        }
    }); 
    //显示修改页面
    document.getElementById("divEditCheckItem").style.display = "block";
    document.getElementById("inptTitle").disabled = "true";
//    document.getElementById("selTaskFlag").disabled = "true";
//    document.getElementById("selTaskNum").disabled = "true";
    document.getElementById("txtStartDate").disabled = "true";
    document.getElementById("txtEndDate").disabled = "true";
    document.getElementById("inpRemark").disabled = "true";
    document.getElementById("inpTaskNo").disabled = "true"; 
  
    
    
    
    
  
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
 self.location='PerformanceSummary.aspx?ModuleID=2011805'; 
}


   function InitialPage()
    {DoModify(document .getElementById ("hidElemID").value.Trim());
    
    }
    function IsNumeric(sText)   
 {   //判斷是否為數值   
   var ValidChars = "0123456789.";   
    var IsNumber=true;   
     var Char;   
      
     for (i = 0; i < sText.length && IsNumber == true; i++)    
       {    
       Char = sText.charAt(i);    
        if (ValidChars.indexOf(Char) == -1)    
          {   
           IsNumber = false;   
          }   
        }   
     return IsNumber;      
   }  
   
   
   
   
  function textcontrol(objId,minScore,maxScore)
  {
         //var obj= document .getElementById (objId );
       var obj=objId;
         if (!IsNumeric(obj.value))
         {
         alert ("必须输入数字!");
         obj.value=minScore ;
         obj .focus();
         return ;
         }
         if (obj.value>minScore && obj .value<=maxScore )
         {}
         else
         {
         alert ("输入数字需为"+minScore +"分 到"+maxScore+"分  范围内!");
         obj.value=minScore ;
          obj .focus();
         return ;
         }
         
  
  
  }
