var jsondata1;
var jsondata2;
var jsondata3;
var jsondata4;
var jsondata5;
var jsondata6;
 var returnResult =0;
 var Today = new Date();
var tY = Today.getFullYear();
var tM = Today.getMonth();
var tD = Today.getDate();
 var dateTimeClock;
 var sessionSection="";
$(document).ready(function(){
      var url = document.location.href.toLowerCase(); 
      if(url.indexOf("/(s(") != -1)
      {
        var sidx = url.indexOf("/(s(")+1;
        var eidx = url.indexOf("))")+2;
        //alert(sidx+":"+eidx);
        url = document.location.href; 
        
        sessionSection = url.substring(sidx,eidx);        
        sessionSection += "/";
      }
      
    }); 

 function SearchDeskTaskList(){
           $.ajax({
            type: "POST",//用POST方式传输
            dataType:"json",//数据格式:JSON
            url:  'Handler/Personal/Task/TaskList.ashx?TaskType=1', //目标地址
            cache:false,//指令
           beforeSend:function()
           {
           },//发送数据之前 
           success: function(msg)
           { 
            
                    jsondata1 = eval(msg.data);
                     returnResult++;
                      isDeskAllRetrun();
           },
           error:function(res) {
           
           }
          });        
  }
 //***************************************************************************************************
  function SearchDeskMemoList(){
           var action="loaddesktop";
        
           var fields="a.[ID] ,a.[TItle] ,a.Status,a.[Content] ,a.[CanViewUserName] ,a.[Attachment]";
            fields += " ,a.[Memoer] ,a.[MemoDate] ,a.[Creator] ,a.[CreateDate],b.EmployeeName";
      
           fields =  encodeURI(fields);
                       
          var prams = "Fields="+fields;
          prams += "&OrderExp="+encodeURI(" a.[MemoDate] ");
           
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  'Handler/Personal/Memo/PersonalMemo.ashx',//目标地址
           cache:false,
           data: "action="+action+"&pageIndex=1&pageSize=5&"+prams,//数据
           beforeSend:function(){},//发送数据之前
           
           success: function(msg)
           {
                
                     jsondata5 =msg.data.list;
                     returnResult++;
                      isDeskAllRetrun();
           },
            error:function(res) {
          }
          });        
  }
  
   function SearchDeskAgendaList(){
 
           $.ajax({
            type: "POST",//用POST方式传输
            dataType:"json",//数据格式:JSON
            url:  'Handler/Personal/Agenda/AgendaList.ashx?isDeskTop=true', //目标地址
            cache:false,//指令
           beforeSend:function()
           {
           },//发送数据之前 
           success: function(msg)
           { 
            
                    jsondata6 = eval(msg) ;
                     returnResult++;
                      isDeskAllRetrun();
           },
           error:function(res) {
           
           }
          });        
  }
 //****************************************************************************************************************
 function SearchDeskFlow(){
 
  $.ajax({
            type: "POST",//用POST方式传输
            dataType:"json",//数据格式:JSON
            url:  'Handler/DeskTop.ashx', //目标地址
            cache:false,//指令
           beforeSend:function()
           {
           },//发送数据之前 
           success: function(msg)
           { 
            
                      jsondata2 = msg.data.list;
                      returnResult++;
                      isDeskAllRetrun();
                      
           },
           error:function(res) {
           
           }
          });        
 }
 
 
 function getBillTypeItem(i,j)
{
    for(var mem in  billTypes)
    {
        if(billTypes[mem].v == j && billTypes[mem].p == i)
        {            
            return billTypes[mem];
            break;
        }
    }
    
    return null;
}

 function SearchDeskUnreadMessage(){
 
  $.ajax({
            type: "POST",//用POST方式传输
            dataType:"json",//数据格式:JSON
              url:  'Handler/Personal/MessageBox/InputBox.ashx',//目标地址
            data:"action=desktoploaddata",
            cache:false,//指令
           beforeSend:function()
           {
           },//发送数据之前 
           success: function(msg)
           {    
            
                       jsondata4 = msg.data.list;
                       returnResult++;
                       isDeskAllRetrun();
           },
           error:function(res) {
           
           }
          });        
 }
 
 
 function getBillTypeItem(i,j)
{
    for(var mem in  billTypes)
    {
        if(billTypes[mem].v == j && billTypes[mem].p == i)
        {            
            return billTypes[mem];
            break;
        }
    }
    
    return null;
}

       function viewMsg(id)
        {
              var action  ="GetItem";              
              $.ajax({ 
                    type: "POST",
                    url: "Handler/Personal/MessageBox/InputBox.ashx?action=" + action,
                    dataType:'string',
                    data: 'id='+id,
                    cache:false,
                    success:function(data) 
                    {                          
                        var result = null;
                        eval("result = "+data);
                        
                        if(result.result)                    
                        {
                              //showInfo(result.data);  
                               dispData(result.data);              
                        }else{                  
                              showInfo(result.data);               
                        }                   
                    },
                    error:function(data)
                    {
                         showInfo(data.responseText);
                    }
                });
                
        }
        
                function dispData(data)
        {
            var ttID = document.getElementById("ttID");
            var ttTitle = document.getElementById("ttTitle");
            var ttSendUser = document.getElementById("ttSendUser");
            var ttSendDate = document.getElementById("ttSendDate");
            var ttContent = document.getElementById("ttContent");
            ttTitle.innerHTML = data.ID;
            ttSendUser.innerHTML = data.SendUserID;
            ttTitle.innerHTML = data.Title;
            ttSendDate.innerHTML = data.CreateDate;
            ttContent.innerHTML = data.Content;
                      
            
            document.getElementById("editPanel").style.left = "200px";
            document.getElementById("editPanel").style.top ="200px";
            document.getElementById("editPanel").style.display = "";
            
           SearchDeskUnreadMessage();
        }
        function hideMsg()
        {
            document.getElementById("editPanel").style.display = "none";
        }
        
function showdailyattendance()
{
    if($("#HiddenEmployeeAttendanceSetID").val().split(',')[1]=="1")
    {
        showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","您不需要签到签退！");
        return;
    }
     if($("#HiddenEmployeeAttendanceSetID").val()=="休息")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","您今天休息，不需要签到签退！");
        return;
    }
    if($("#HiddenEmployeeAttendanceSetID").val()=="节假日")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","今天节假日，您不需要签到签退！");
        return;
    }
    if(document.getElementById("ddlworkshift").selectedIndex==-1)
    {
        showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","您还没有进行人员考勤设置！");
    }
    else
    {
        GetUserInfo();
      //  Getdate();
        document.getElementById("DailyAttendanceSpan").style.display= "block";
    }
}
//获取当前日期和时间
function Getdate()
{
   var today=new Date();
    var year=today.getYear();
    var month=today.getMonth()+1;
    var day=today.getDate();
    var hours = today.getHours();
    var minutes = today.getMinutes();
    var seconds = today.getSeconds();
    var timeValue= hours;//((hours >12) ? hours -12 :hours);
    timeValue += ((minutes < 10) ? ":0" : ":") + minutes+"";
    //timeValue += (hours >= 12) ? "PM" : "AM";
    timeValue+=((seconds < 10) ? ":0" : ":") + seconds+"";
    var timetext=year+"-"+month+"-"+day+" "+timeValue
    //document.write("<span onclick=\"document.getElementById('time').value='"+timetext+"'\">"+timetext+"</span>");
    document.getElementById("Today").value=year+"-"+month+"-"+day;//获取当前日期
//    document.getElementById("liveclock").innerText = timetext; //div的html是now这个字符串
    document.getElementById("liveclock").value = timeValue; //div的html是now这个字符串
    setTimeout("Getdate()",1000); //设置每秒，调用showtime方法
    
 }
 var c="";
function GetUserInfo()
{
           var action="getuserinfo";
           $.ajax({ 
              type: "POST",
              url: "Handler/Office/AdminManager/DailyAttendance.ashx",
              data:'action='+escape(action),
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                  //AddPop();
              }, 
           // complete :function(){hidePopup();},
            error: function() { showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","请求发生错误！");}, 
            success:function(data) 
            { 
                document.getElementById("Today").value=data.data.split(',')[1];//获取当前日期
                document.getElementById("liveclock").value = data.data.split(',')[2].replace(data.data.split(',')[1]+" ",""); //div的html是now这个字符串
                if(data.sta!="0")//签退
                {
                    $("#EmployNo").val(data.info);
                    $("#EmployName").val(data.data.split(',')[0]);
                }
                else//签到
                {
                    $("#EmployNo").val(data.info);
                    $("#EmployName").val(data.data.split(',')[0]);
                }
            }//,
            //complete:function(){if(ifuserexist=="1")hidePopup();}//接收数据完毕
           });
           c=setTimeout("GetUserInfo()",1000); //设置每秒，调用showtime方法
}
function CloseDailyAttendanceSpan()
{
    document.getElementById('DailyAttendanceSpan').style.display='none';
    clearTimeout(c);
}
//插入员工签到信息
function InsertEmploySignInData()
{
   var currdate = document.getElementById("Today").value;//当前日期
   var currdatetime = document.getElementById("liveclock").value;//签到时间
   var workshifttimeid=$("#ddlworkshift").val();//对应班段
   var EmpSetID=$("#HiddenEmployeeAttendanceSetID").val().split(',')[0];//人员考勤设置ID
   var remark=$("#Remark").val();
   var action="insertdailyattendance";
   $.ajax({ 
              type: "POST",
              url: "Handler/Office/AdminManager/DailyAttendance.ashx",
              data:'currdate='+escape(currdate)+'\
                    &currdatetime='+escape(currdatetime)+'\
                    &remark='+escape(remark)+'\
                    &workshifttimeid='+escape(workshifttimeid)+'\
                    &EmpSetID='+escape(EmpSetID)+'\
                    &action='+escape(action),
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                  AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() {showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","请求发生错误！");}, 
            success:function(data) 
            { 
                if(data.sta==2) 
                { 
                     showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","考勤失败，可能原因：此班段今天已经签到！");
                     return;
                } 
                if(data.sta==1) 
                { 
                     ifshow="1";
                     showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","考勤成功！");
                     document.getElementById("DailyAttendanceSpan").style.display= "none"
                } 
                else if(data.sta==0)
                { 
                  hidePopup();
                  showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","考勤失败,联系管理员！");
                } 
            } 
           });
}
//更新员工签退信息
function UpdateEmploySignOutData()
{
   var currdate = document.getElementById("Today").value;//当前日期
   var currdatetime = document.getElementById("liveclock").value;//签退时间
   var workshifttimeid=$("#ddlworkshift").val();//对应班段
   var EmpSetID=$("#HiddenEmployeeAttendanceSetID").val().split(',')[0];//人员考勤设置ID
   var remark=$("#Remark").val();
   var action="updatedailyattendance";
   $.ajax({ 
              type: "POST",
              url: "Handler/Office/AdminManager/DailyAttendance.ashx",
              data:'currdate='+escape(currdate)+'\
                    &currdatetime='+escape(currdatetime)+'\
                    &remark='+escape(remark)+'\
                    &workshifttimeid='+escape(workshifttimeid)+'\
                    &EmpSetID='+escape(EmpSetID)+'\
                    &action='+escape(action),
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                  AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() {showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","请求发生错误！");}, 
            success:function(data) 
            { 
                if(data.sta==3) 
                { 
                     showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","考勤失败，可能原因：已经做过签退！");
                     return;
                }
                if(data.sta==4) 
                { 
                     showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","考勤失败，可能原因：还没有签到！");
                     return;
                }
                if(data.sta==1) 
                { 
                     ifshow="1";
                     showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","考勤成功！");
                     document.getElementById("DailyAttendanceSpan").style.display= "none"
                } 
                else if(data.sta==0)
                { 
                  hidePopup();
                  showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","考勤失败,联系管理员！");
                } 
            } 
           });
}



function SearchDeskTopNotice(){
 
  $.ajax({
            type: "POST",//用POST方式传输
            dataType:"json",//数据格式:JSON
            url:  'Handler/Personal/MessageBox/Notice.ashx',//目标地址
            data:"action=desktopdataload",
            cache:false,//指令
           beforeSend:function()
           {
           },//发送数据之前 
           success: function(msg)
           { 
            
                      jsondata3 = msg.data.list;      
                       returnResult++;
                      isDeskAllRetrun();
           },
           error:function(res) {
              
           }
          });        
 }
 
 function showNoticePanel(title,content){
    document.getElementById ("spNewsTitle").value = title;
    document.getElementById("spNewsContent").value = content.replace(/<br><br>/g,"\r");
    document.getElementById('desktopnotice').style.display='';
 }
 
 
 function getStatusName(i)
{
    if( i == "1")
        return "待我审批";
    if( i == "2")
        return "审批中";
   if( i == "3")
        return "审批通过";
    if( i == "4")
        return "审批不通过";
            if( i == "5")
        return "撤销审批";
  return "未知";
}

function  InitPage(){   
    SearchDeskFlow();
    SearchDeskTaskList();
    SearchDeskTopNotice();
    SearchDeskUnreadMessage();
    SearchDeskMemoList();
    SearchDeskAgendaList();
    DateInit();
    GetServerTime();
 }

function isDeskAllRetrun(){

    if(  returnResult == 6 )
    {
       ShowDeskSearchDeskFlow();
       ShowDeskSearchTaskList();
       ShowDeskSearchDeskTopNotice();
       ShowDeskSearchUnreadMessage();
       ShowDeskSearchAgenda();
       ShowDeskSearchDeskMemo();
    }else
       return;
}

//********************************************************************************************************
 function ShowDeskSearchAgenda(){
      var   listtable =  document.getElementById("tbList5");
                       
                       var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       } 
                       listtable.style.width = '100%'; 
                       listtable.cellSpacing=1; 
                 try{
                    var  th = listtable.insertRow(-1);
                           th.align = "center";
                           th.style.background="#999999";  
                           th.height = 20; 
                           th.insertCell(-1).innerHTML = "日期";
                           th.insertCell(-1).innerHTML = "内容";                         
                                                  
                    listcount =jsondata6.length;
                    for(var listindex = 0 ;listindex<jsondata6.length;listindex++){
                           if (listindex>2 ){
                                 var tr =  listtable.insertRow(-1);
                                  tr.height = "20";
                              tr.align = "right";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                               if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                 var td = tr.insertCell(-1);
                                 td.colSpan=8;
                                 td.innerHTML = "<a href='Pages/Personal/Agenda/AgendaList.aspx?ModuleID=10312' >更多...</a> " 
                                return;
                              }
   
                             var tr =  listtable.insertRow(-1);
                              tr.height = "20";
                              tr.align = "center";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                              if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                           tr.insertCell(-1).innerHTML = "<a href='pages/personal/Agenda/AgendaEdit.aspx?ID="+jsondata6[listindex].ID+"'>"+jsondata6[listindex].StartDate.split(' ')[0]+"</a>";
                                           tr.insertCell(-1).innerHTML = jsondata6[listindex].Content ;                           
                    }
                    
                   
                  }catch(e){  } 
   }
   
   
   
   function ShowDeskSearchDeskMemo(){
      var   listtable =  document.getElementById("tbList4");
                       
                       var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       } 
                       listtable.style.width = '100%'; 
                       listtable.cellSpacing=1; 
                 try{
                    var  th = listtable.insertRow(-1);
                           th.align = "center";
                           th.style.background="#999999";  
                           th.height = 20; 
                           th.insertCell(-1).innerHTML = "备忘日期";
                           th.insertCell(-1).innerHTML = "备忘内容";  
                           th.insertCell(-1).innerHTML = "处理状态";                       
                                                  
                    listcount =jsondata5.length;
                    for(var listindex = 0 ;listindex<jsondata5.length;listindex++){
                           if (listindex>2 ){
                                 var tr =  listtable.insertRow(-1);
                                  tr.height = "20";
                              tr.align = "right";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                               if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                 var td = tr.insertCell(-1);
                                 td.colSpan=8;
                                 td.innerHTML = "<a href='Pages/Personal/Memo/MemoList.aspx?ModuleID=10312' >更多...</a> " 
                                return;
                              }
   
                             var tr =  listtable.insertRow(-1);
                              tr.height = "20";
                              tr.align = "center";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                              if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                              var status="";
                                if(jsondata5[listindex].Status=="1")status="已处理";else if(jsondata5[listindex].Status=="0") status="未处理"; else status=""; 
                                           tr.insertCell(-1).innerHTML ="<a href='pages/personal/Memo/MemoEdit.aspx?ID="+jsondata5[listindex].ID+ "'>"+ jsondata5[listindex].MemoDate.split(' ')[0]+"</a>" ;
                                           // tr.insertCell(-1).innerHTML =  jsondata5[listindex].Content;
                                           tr.insertCell(-1).innerHTML =  jsondata5[listindex].Content.length>10?(jsondata5[listindex].Content.substr(0,10)+"..."):jsondata5[listindex].Content  ;                           
                                           tr.insertCell(-1).innerHTML =  status;
                    }
                    
                   
                  }catch(e){  } 
   }
   
//********************************************************************************************************

   function ShowDeskSearchDeskFlow(){
      var   listtable =  document.getElementById("tbList2");
                       
                       var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       } 
                       listtable.style.width = '100%'; 
                       listtable.cellSpacing=1; 
                 try{
                    var  th = listtable.insertRow(-1);
                           th.align = "center";
                           th.style.background="#999999";  
                           th.height = 20;
                           th.insertCell(-1).innerHTML = "单据编号";
                           th.insertCell(-1).innerHTML = "单据类型";
                           th.insertCell(-1).innerHTML = "流程名称";
                           th.insertCell(-1).innerHTML = "流程状态";
                           th.insertCell(-1).innerHTML = "流程提交人";
                           th.insertCell(-1).innerHTML = "当前处理步骤序号";
                           th.insertCell(-1).innerHTML = "当前处理步骤描述";
                           th.insertCell(-1).innerHTML = "当前处理人";
                         
                                                  
                    listcount =jsondata2.length;
                    for(var listindex = 0 ;listindex<jsondata2.length;listindex++){
                           if (listindex>2 ){
                                 var tr =  listtable.insertRow(-1);
                                  tr.height = "20";
                              tr.align = "right";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                               if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                 var td = tr.insertCell(-1);
                                 td.colSpan=8;
                                 td.innerHTML = "<a href='Pages/Personal/WorkFlow/FlowWaitProcess.aspx' >更多...</a> " 
                                return;
                              }
                       var billtypeitem = getBillTypeItem(jsondata2[listindex].BillTypeFlag,jsondata2[listindex].BillTypeCode);
                        
                        if(billtypeitem == null)
                        {
                            billtypeitem = {};
                        }
                             var tr =  listtable.insertRow(-1);
                              tr.height = "20";
                              tr.align = "center";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                              if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                           tr.insertCell(-1).innerHTML = "<a href='/"+sessionSection+billtypeitem.u+jsondata2[listindex].BillID+"&intFromType=2'  title='点击查看"+ jsondata2[listindex].BillNo+"的详细信息'  >"+ jsondata2[listindex].BillNo +"</a>";
                 
                                           tr.insertCell(-1).innerHTML =  billtypeitem.t;
                                           tr.insertCell(-1).innerHTML = jsondata2[listindex].FlowName ;                           
                                           tr.insertCell(-1).innerHTML = getStatusName(jsondata2[listindex].FlowStatus); 
                                           tr.insertCell(-1).innerHTML = jsondata2[listindex].ApplyUserName; 
                                           tr.insertCell(-1).innerHTML = jsondata2[listindex].StepNo; 
                                           tr.insertCell(-1).innerHTML = jsondata2[listindex].StepName;   
                                           tr.insertCell(-1).innerHTML = jsondata2[listindex].ModifiedUserID;  
                    
                    }
                    
                   
                  }catch(e){  } 
   }
   function   ShowDeskSearchTaskList(){
    var   listtable =  document.getElementById("tbList1");
                       var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       } 
                       listtable.style.width = '100%';   
                       listtable.cellSpacing=1;
                 try{
                    var  th = listtable.insertRow(-1);
                           th.align = "center";
                           th.style.background="#999999";  
                           th.height = 20;
                           th.insertCell(-1).innerHTML = "编号";
                           th.insertCell(-1).innerHTML = "主题";
                           th.insertCell(-1).innerHTML = "负责人";
                           th.insertCell(-1).innerHTML = "完成期限";
                           th.insertCell(-1).innerHTML = "创建时间";
                   
                    listcount =jsondata1.length;
                    
                    for(var listindex = 0 ;listindex<jsondata1.length;listindex++){
                            if (listindex>2 ){
                                 var tr =  listtable.insertRow(-1);
                                  tr.height = "20";
                              tr.align = "right";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                               if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                 var td = tr.insertCell(-1);
                                 td.colSpan=5;
                                 td.innerHTML = "<a href='Pages/Personal/Task/TaskList.aspx'  >更多...</a> " 
                                return;
                              }
                             var tr =  listtable.insertRow(-1);
                              tr.height = "20";
                              tr.align = "center";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                              if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                           tr.insertCell(-1).innerHTML = "<a href='pages/personal/task/TaskEdit.aspx?ID="+jsondata1[listindex].ID+"'>"+ jsondata1[listindex].TaskNo +"</a>";
                                           tr.insertCell(-1).innerHTML =  jsondata1[listindex].Title;
                                           tr.insertCell(-1).innerHTML = jsondata1[listindex].PrincipalName ;                           
                                           tr.insertCell(-1).innerHTML = jsondata1[listindex].CompleteDate; 
                                            tr.insertCell(-1).innerHTML = jsondata1[listindex].CreateDate;   
                    
                    }
                
                  }catch(e){   }
   }
   function ShowDeskSearchDeskTopNotice(){

     var   listtable =  document.getElementById("tbdestnoticeList");
                       
                       var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       } 
                       listtable.style.width = '100%';   
                       listtable.height =20;
                 try{
                    var  th = listtable.insertRow(-1);
                           th.align = "center";
                           th.height = 20;
                           th.style.background="#999999";  
                           th.insertCell(-1).innerHTML = "公告主题";
                           th.insertCell(-1).innerHTML = "发布时间";                    
                    
                                                  
                    listcount =jsondata3.length;
                    for(var listindex = 0 ;listindex<jsondata3.length;listindex++){
                          if (listindex>2 ){
                                 var tr =  listtable.insertRow(-1);
                                  tr.height = "20";
                              tr.align = "right";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                               if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                 var td = tr.insertCell(-1);
                                 td.colSpan=2;
                                 td.innerHTML = "<a href='Pages/Personal/MessageBox/DeskTopPublicNotice.aspx' >更多...</a> " 
                                return;
                              }
                       var billtypeitem = getBillTypeItem(jsondata3[listindex].BillTypeFlag,jsondata3[listindex].BillTypeCode);
                        
                        if(billtypeitem == null)
                        {
                            billtypeitem = {};
                        }
                             var tr =  listtable.insertRow(-1);
                              tr.height = "20";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                              if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                         
                                           tr.insertCell(-1).innerHTML = "<a href=\"#\" onclick=\"showNoticePanel('"+ jsondata3[listindex].NewsTitle.replace(/\'/g,"\\\'")+"','"+jsondata3[listindex].NewsContent.replace(/\'/g,"\\\'")+"')\" title=\"点击查看详细\">"+ jsondata3[listindex].NewsTitle+"</a>"                         
                                           tr.insertCell(-1).innerHTML = jsondata3[listindex].CreateDate; 
  
                    }
                    
                
                  }catch(e){  } 
   }
   function ShowDeskSearchUnreadMessage(){
  var   listtable =  document.getElementById("tbList3");
                       
                       var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       } 
                       listtable.style.width = '100%';
                       listtable.cellSpacing=1;   
                 try{
                    var  th = listtable.insertRow(-1);
                           th.align = "center";
                           th.height = 20;
                           th.style.background="#999999";   
                           th.insertCell(-1).innerHTML = "信息主题";
                           th.insertCell(-1).innerHTML = "发布时间";
                           th.insertCell(-1).innerHTML = "发布人";                        
                   
                                                  
                    listcount =jsondata4.length;
                    for(var listindex = 0 ;listindex<jsondata4.length;listindex++){
                             if (listindex>2 ){
                                 var tr =  listtable.insertRow(-1);
                                  tr.height = "20";
                              tr.align = "right";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                               if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                 var td = tr.insertCell(-1);
                                 td.colSpan=3;
                                 td.innerHTML = "<a href='Pages/Personal/MessageBox/UnReadedInfo.aspx' >更多...</a> " 
                                return;
                              }
                       var billtypeitem = getBillTypeItem(jsondata4[listindex].BillTypeFlag,jsondata4[listindex].BillTypeCode);
                        
                        if(billtypeitem == null)
                        {
                            billtypeitem = {};
                        }
                             var tr =  listtable.insertRow(-1);
                              tr.height = "20";
                              tr.align = "center";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                              if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                         
                                           tr.insertCell(-1).innerHTML = "<a href=\"#\" onclick=\"viewMsg("+ jsondata4[listindex].ID+")\" title=\"点击查看详细\">"+ jsondata4[listindex].Title+"</a>"                         
                                           tr.insertCell(-1).innerHTML = jsondata4[listindex].CreateDate; 
                                           tr.insertCell(-1).innerHTML = jsondata4[listindex].SendUserName;           
                    }
                     
                
                  }catch(e){  } 
   }
   
  function DateInit(){
   document.getElementById("lblshowyear").innerHTML ="公元"+ new Date().getYear()+"年" ;
    document.getElementById("lblmonth").innerHTML = new Date().getMonth()+1 +"月";
    document.getElementById("spanday").innerHTML = new Date().getDate();
    document.getElementById("lblweek").innerHTML =returnWeek(new Date().getDay());
  }
  
  function returnWeek(day){
       switch (day){
         case 1:return "星期一";
         case 2:return "星期二";
         case 3:return "星期三";
         case 4:return "星期四";
         case 5:return "星期五";
         case 6:return "星期六";
         case 0:return "星期日";
       }
  }
  
 
function GetServerTime(){
   $.ajax({ 
              type: "POST",
              url: "Handler/DeskTop.ashx?GetServerTime=true",
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
              }, 
           // complete :function(){hidePopup();},
            error: function() { showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","请求发生错误！");}, 
            success:function(msg) 
            { 
                 
                 var datearray = msg.data.split(' ')[0].split('-');
                 var timearray = msg.data.split(' ')[1].split(':');
                  dateTimeClock =  new Date(datearray[0],datearray[1],datearray[2],timearray[0],timearray[1],timearray[2]) ;
                  startTimer();
            },
            complete:function(){}//接收数据完毕
           });
}

var timer;
function startTimer(){
  timer= setInterval("RefreshTime();",1000);  //1秒执行一次
}

function stopTimer(){
  clearInterval(timer);
}

function RefreshTime(){
  dateTimeClock =  dateAdd(dateTimeClock,1000);
  document.getElementById("divTime").innerHTML = date2str(dateTimeClock,"HH:mm:ss");
}

function dateAdd(date,val)
{    
    var b=Date.parse(date)+val ;
    b   = new   Date(b)   ;
    return b;
}

function date2str(obj,fmt)
{
    var y = obj.getYear();
    var M = obj.getMonth()+1;
    var d = obj.getDate();
    
    var h = obj.getHours();
    var m = obj.getMinutes();
    var s = obj.getSeconds();
    
    //var f = obj.getMilliseconds();
    
    if(M<10)
        M="0"+M;
    if(d<10)
        d="0"+d;
    if(h<10)
        h="0"+h;
    if(m<10)
        m="0"+m;
    if(s<10)
        s="0"+s;
         
    //fmt:yyyyMMddHHmmss fff
    
    fmt = fmt.replace("yyyy",y);
    fmt = fmt.replace("MM",M);
    fmt = fmt.replace("dd",d);
    fmt = fmt.replace("HH",h);
    fmt = fmt.replace("mm",m);
    fmt = fmt.replace("ss",s);
    
    return fmt;
   
}