$(document).ready(function(){
    document.getElementById("hidEditFlag").value="INSERT";
    
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
function ddds()
{
       DoModify(document .getElementById ("hidElemID").value,document .getElementById ("hidEmployeId").value,document .getElementById ("hidTemplateNo").value);
  
       DoGetemployeeInfo(document .getElementById ("hidElemID").value,document .getElementById ("hidEmployeId").value,document .getElementById ("hidTemplateNo").value);
 
        var sign=document .getElementById ("hidSign").value;
        if (sign=="1" )
         {
           document .getElementById ("btnGather").style.display="block";
           }
        else
        {
          document .getElementById ("btnGather").style.display="None";
              document.getElementById("inpSignNote").disabled = "true"; 
          }
 }
 function DoModify(taskNo,EmployeId,templateNo)
{
        document.getElementById("hidEditFlag").value="UPDATE";
        document.getElementById("hidElemID").value = taskNo;
        document.getElementById("txtPerformTmNo").style.display="none"; 
        document.getElementById("inpTaskNo").style.display="block";
        document.getElementById("inpTaskNo").value = taskNo;//taskNo编号
        
                   
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceSummary.ashx?action=GetTaskInfoByTaskNO&tempalteNo="+templateNo +"&EmployeId="+EmployeId+"&taskNo="+taskNo ,
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
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
                          document.getElementById("inpRemark").value = item.Remark;
                  
                   
                        if (item.AdviceNote!=null && item.AdviceNote!="")
                          document.getElementById("inpAdviceNote").value = item.AdviceNote;  
                        if (item.Note!=null && item.Note!="")
                          document.getElementById("inpNote").value = item.Note;
                          document.getElementById ("selTaskFlag").innerHTML=item.TaskFlag;
                            document.getElementById ("dvTaslNum").innerHTML=item.TaskNum;
//                          objSelect= document.getElementById ("selTaskFlag");
//                          objItemValue=item.TaskFlag;
//                          for (var i = 0; i < objSelect.options.length; i++)
//                           {        
//                              if (objSelect.options[i].value == objItemValue)
//                              {        
//                                   document.getElementById ("selTaskFlag").value=objItemValue; 
//                                    break;        
//                               }
//                           }
                    
                          if (item .TaskStaus=="3")
                        {
                              document.getElementById("inpKillScore").disabled = "true";
                              document.getElementById("inpAddScore").disabled = "true";
                              document.getElementById("sleAdviceType").disabled = "true";
                              document.getElementById("sleLevelType").disabled = "true";
                              document.getElementById("inpSummaryNote").disabled = "true";
                              document.getElementById("inpSrs").disabled = "true";
                              document.getElementById("inpsan").disabled = "true"; 
                              document.getElementById("inpSumarryRemark").disabled = "true";
                              document.getElementById("inpRewardNote").disabled = "true"; 
                         }
                       if (item .Summaryer!=null && item .Summaryer!="")
                       {
                           document.getElementById ("inpSummaryer").innerHTML=item.Summaryer; 
                           }
                              if (item .Summaryer!=null && item .Summaryer!="")
                       {
                           document.getElementById ("inpSummaryDate").innerHTML=item.SummaryDate; 
                           }
                           
                           
                           
                       if (item .Creator!=null && item .Creator!="")
                          document.getElementById ("inpCreater").innerHTML=item .Creator;
                       if (item .CreateDate!=null && item .CreateDate!="")
                          document.getElementById ("inpCreateDate").innerHTML=item .CreateDate; 
                       if (item .StatusName!=null && item .StatusName!="")
                         document.getElementById ("inpTaskStaus").innerHTML=item .StatusName;  
                        if (item.TaskFlag=="临时考核")   
                        {
                           document.getElementById("dvTaslNum").style.display="none";
                         }
                        else
                        {
//                           var obj=document.getElementById("selTaskFlag");
//                           getChange(obj);
//                           objSelect1= document.getElementById ("selTaskNum");
//                           objItemValue1=item.TaskNum;
//                           for (var a = 0; a < objSelect1.options.length; a++)
//                           {        
//                              if (objSelect1.options[i].value == objItemValue1)
//                             {        
//                               document.getElementById ("selTaskNum").value=objItemValue1;     
//                               break;        
//                              }
//                            }
                        
                        } 
                       /// alert (msg .info);
                       if ( msg .info!=null )
                      {
//                       var info=msg.info;//考核人数_已完成人数_未完成人数_是否可汇总
//                      // alert (info );
//                       var infolist=  info.split("_");
                       //alert (infolist );
                      // alert (infolist [0]);
//                     document .getElementById ("inpEmployee").innerHTML=infolist [0];
//                        document .getElementById ("inpHasScore").innerHTML=infolist [1];
//                          document .getElementById ("inpNotScore").innerHTML=infolist [2];
//                       if (infolist [3]=="1")
//                            {
//                              if (item .TaskStaus=="1")
//                              {
//                              document .getElementById ("inpIsCheck").innerHTML="是";
//                              }else
//                              {
//                               document .getElementById ("inpIsCheck").innerHTML="已汇总";
//                              }
//                             
//                           }else
//                           {
//                            document .getElementById ("btnGather").style.display="None";
//                              document .getElementById ("inpIsCheck").innerHTML="否";
//                            }
                             
                       
                       } 
                        
                           
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
    document.getElementById("inpAdviceNote").disabled = "true";
    document.getElementById("inpNote").disabled = "true"; 
}

function DoGetemployeeInfo(taskNo,EmployeId,TemplateNo)
{
       document.getElementById("hidEmployeId").value =EmployeId;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceSummary.ashx?action=GetSurmarryInfoByTaskNOEmployeeID&TemplateNo="+TemplateNo+"&EmployeId="+EmployeId +"&taskNo="+taskNo ,
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
              popMsgObj.ShowMsg(' 请求发生错误！');
        }, 
          success:function(msg) 
        {
            //隐藏提示框  
            var employee=new Array ();
            var templateNo=new Array ();
            hidePopup();
            /* 设置考核类型信息 */
            $.each(msg.data, function(i,item)
            {
                //要素名称
                         objSelect= document.getElementById ("sleAdviceType");
                         objItemValue=item.AdviceType;
                          for (var i = 0; i < objSelect.options.length; i++) 
                          {        
                            if (objSelect.options[i].value == objItemValue)
                            {        
                              document.getElementById ("sleAdviceType").value=objItemValue; 
                              break;        
                             }
                          }
                         objSelect2= document.getElementById ("sleLevelType");
                         objItemValue2=item.LevelType;
                          for (var a = 0; a < objSelect2.options.length;a++) 
                          {        
                             if (objSelect2.options[a].value == objItemValue2)
                             {        
                              document.getElementById ("sleLevelType").value=objItemValue2; 
                              break;        
                              }
                           }
                        
                       if (item .SignNote!=null && item .SignNote!="")
                            document.getElementById ("inpSignNote").value=item .SignNote;
                          if (item .SummaryNote!=null && item .SummaryNote!="")
                            document.getElementById ("inpSummaryNote").value=item .SummaryNote;
                          if (item .AddScore!=null && item .AddScore!="")
                            document.getElementById ("inpAddScore").value=item .AddScore;
                          if (item .KillScore!=null && item .KillScore!="")
                            document.getElementById ("inpKillScore").value=item .KillScore;
                          if (item .SummaryAdviceNote!=null && item .SummaryAdviceNote!="")
                            document.getElementById ("inpsan").value=item .SummaryAdviceNote;
                          if (item .Remark!=null && item .Remark!="")
                            document.getElementById ("inpSumarryRemark").value=item .Remark;
                          if (item .TotalScore!=null && item .TotalScore!="")
                            document.getElementById ("inpTotalScore").innerHTML=item .TotalScore;
                          if (item .RewardNote!=null && item .RewardNote!="")
                            document.getElementById ("inpRewardNote").value=item .RewardNote;
                                 if (item .RealScore!=null && item .RealScore!="")
                            document.getElementById ("inpSrs").value=item .RealScore;
                            
                            
                            
                       if ( msg .info!=null )
                      {
                       var info=msg.info;//考核人数_已完成人数_未完成人数_是否可汇总
                       var infolist=  info.split("_");
                       }          
            });
        }
    }); 
   

}


//根据下拉框自动填充内容
function getChange(obj)
{
      var m=obj.options[obj.selectedIndex].value;
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


function CheckBaseInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    var KillScore=document.getElementById("inpKillScore").value;
    var AddScore=document.getElementById("inpAddScore").value;
    if (""==KillScore )
    {   
       isErrorFlag = true;
       fieldText += "累计扣分|";
       msgText += "累计扣分项不能为空|";
    }else
   { 
        if (!IsNumeric(KillScore))
         {
            isErrorFlag = true;
            fieldText += "累计扣分|";
            msgText += "累计扣分必须为数字|";
          }
    }
   if (""==AddScore )
     {    
        isErrorFlag = true;
        fieldText += "累计加分|";
        msgText += "累计加分项不能为空|";
     }else
      { 
         if (!IsNumeric(AddScore))
          {
            isErrorFlag = true;
            fieldText += "累计加分|";
            msgText += "累计加分必须为数字|";
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



	
function Caculate()
{
     if (CheckBaseInfo())
           { return;}
    var elemList = new Array();
    var tmList = $(".ElemScore");
    var tmListlen = tmList.length;
    var sum=0.00;
    for (var a = 0; a < tmListlen; a++)//取消所有的勾选项
    {
       var temp= tmList [a].title.split("_");
      sum=sum + parseFloat(tmList[a].value)*parseFloat(temp[1])/100;
    }
    document .getElementById ("inpAccount").value=sum;
}	
	
	
	
/*
* 基本信息校验
*/

function GetBaseInfoParams()
{
       var  TaskNo = document.getElementById("inpTaskNo").value;
       
       var EmployeeID=document.getElementById("hidEmployeId").value;
   
       var TemplateNo=document.getElementById("hidTemplateNo").value;
       var SignNote=document.getElementById("inpSignNote").value;
      
   var  param = "TaskNo=" + TaskNo;
         param += "&EmployeeID=" + EmployeeID;
         param += "&TemplateNo=" + TemplateNo;
         param += "&SignNote=" + SignNote;
    return param;
}
	function DoSave()
{
	  /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
       if(confirm("确认后不可修改，继续确认吗！"))
	      {
    if (CheckBaseInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceSummary.ashx?action=GatherSummaryCheckInfo&" + postParams,
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
                 document.getElementById("hidElemID").value = data.data;//taskNo编号
                 document.getElementById("txtPerformTmNo").style.display="none"; 
                 document.getElementById("inpTaskNo").style.display="block";
                  document.getElementById("inpTaskNo").value = data.data;//taskNo编号
                
                  if (data.info=="1")//1为确认状态
                  {
                    document.getElementById("btnGather").style.display="none"; 
                    popMsgObj.ShowMsg(' 确认成功！');
//                  self.location='PerformanceTaskCheck.aspx';                   
                  }else
                  {
                    popMsgObj.ShowMsg(' 保存成功！');
                  }
            }
            else  if(data.sta == 2) 
            {  popMsgObj.ShowMsg(' 该考核任务编号已存在！');
            }
            //保存失败
            else 
            { 
                hidePopup();
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





function DoBack()
{
 self.location="PerformanceTaskCheck.aspx?ModuleID=2011808"; 
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
          {     IsNumber = false;      }   
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
         {
         }
         else
         {
          alert ("输入数字需为"+minScore +"分 到"+maxScore+"分  范围内!");
          obj.value=minScore ;
          obj .focus();
         return ;
         }
  }
  


