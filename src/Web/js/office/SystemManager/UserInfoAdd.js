function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var hiddenUserID = document.getElementById("hiddenUserID").value;//隐藏用户ID是否存在
    var txtUserID = trim(document.getElementById("txtUserID").value);//用户编号
    var txtUserName = trim(document.getElementById("txtUserName").value);//用户姓名
    var txtPassword = trim(document.getElementById("txtPassword").value);//密码
    var txtRePassword = trim(document.getElementById("txtRePassword").value);//重复密码
    var txtCloseDate = document.getElementById("txtCloseDate").value;//失效日期
    var txtOpenDate = document.getElementById("txtOpenDate").value;//生效日期
    var txtRemark = document.getElementById("txtRemark").value;//生效日期
     var UsedStatus = document.getElementById("UsedStatus").value;//启用状态
     var EmName=document.getElementById("EmployeeID").value;
     var puserid=document.getElementById("hfuserid").value;
     if(puserid=="3")
     {
      return;
     }
    if(txtUserID=="")
    {
        isFlag = false;
        fieldText = fieldText + "用户名|";
   		msgText = msgText +  "请输入用户名|";
   		
    }
    if(txtUserID!="")
    {
       if(txtUserID.length>10)
    {
        isFlag = false;
        fieldText = fieldText + "用户名|";
   		msgText = msgText + "用户名长度必须在8-10位之间|";
    }
    if(txtUserID.length<8)
    {
      isFlag = false;
        fieldText = fieldText + "用户名|";
   		msgText = msgText + "用户名长度必须在8-10位之间|";
    }
  if(!txtUserID.match(/^[a-zA-Z]+[\d]+([a-zA-Z0-9])*$|^[\d]+[a-zA-Z]+([a-zA-Z0-9])*$/))
   {
   isFlag = false;
    fieldText = fieldText + "用户名|";
	msgText = msgText + "用户名必须是字母、数字的组合|";
     }
    }
   
    if(EmName=="")
    {
     isFlag = false;
    fieldText = fieldText + "员工姓名|";
	msgText = msgText + "请输入员工姓名|";
    }
  var RetVal=CheckSpecialWords();

    if(RetVal!="")
    {
    isFlag = false;
    fieldText = fieldText + RetVal+"|";
    msgText = msgText +RetVal+"不能含有特殊字符|";
    }
   
    if(txtPassword=="")
    {
        isFlag = false;
        fieldText = fieldText + "密码|";
   		msgText = msgText + "请输入密码|";
    }
    else 
    { 
    if(strlen(txtPassword)>16||strlen(txtPassword)<8)
    {
        isFlag = false;
        fieldText = fieldText +  "密码|";
   		msgText = msgText + "密码位数处于8-16位之间|";
   		
    }
    if(!txtPassword.match(/^[0-9a-zA-Z]+$/))
    { 
       isFlag = false;
        fieldText = fieldText + "密码|";
   		msgText = msgText + "密码必须是字母、数字的组合|";
    }
     
    }
   
    if(txtRePassword=="")
    {
     isFlag = false;
        fieldText = fieldText + "重复密码|";
   		msgText = msgText + "请再次输入密码|";
    }
    else
    {
     if(txtPassword!=txtRePassword)
    {
       isFlag = false;
        fieldText = fieldText + "密码错误|";
   		msgText = msgText + "请确认两次输入的密码是否一致|";
    }
    }
    
    
    if(strlen(txtOpenDate)<=0)
    {
        isFlag = false;
         fieldText = fieldText +  "生效日期|";
   		 msgText = msgText + "请选择生效日期|";   
    }
     if(strlen(txtRemark)>50)
    {
        isFlag = false;
        fieldText = fieldText + "备注|";
   		msgText = msgText +  "备注仅限于50字符以内|";
    }
    if(strlen(txtOpenDate)>0)
    {
        if(!isDate(txtOpenDate))
        {
            isFlag = false;
             fieldText = fieldText +  "生效日期|";
   		     msgText = msgText + "生效日期格式不正确|";   
        }
    }
    if(strlen(txtCloseDate)<=0)
    {
        isFlag = false;
         fieldText = fieldText +  "失效日期|";
   		 msgText = msgText + "请选择失效日期|";   
    }
   if(strlen(txtCloseDate)>0)
    {
        if(!isDate(txtCloseDate))
        {
            isFlag = false;
             fieldText = fieldText +  "失效日期|";
   		     msgText = msgText + "失效日期格式不正确|";    
        }
    }
     if(strlen(txtCloseDate)>0&&strlen(txtOpenDate)>0)
    {
   var  tmpBeginTime = new Date(txtOpenDate.replace(/-/g,"\/"));
   var  tmpEndTime = new Date(txtCloseDate.replace(/-/g,"\/"));
    if ( tmpBeginTime > tmpEndTime )
    {
          isFlag = false;
             fieldText = fieldText +  "日期不匹配|";
   		     msgText = msgText + "失效日期不能小于生效日期|";    
    }
    }
    
       if(!isFlag)
    {
        //注：两个方法显示弹出提示信息层,方法一是对字段用红色处理，方法二是所有的提示信息传入未处理颜色
        
        //方法一
        popMsgObj.Show(fieldText,msgText);
        
        //方法二
        //popMsgObj.ShowMsg('所有的错误信息字符串');
    }

    return isFlag;
}
function CheckUserNum()
{  
    var userid=document.getElementById('txtUserID').value;
    if(userid.length!=0)
    {
      var tablename="UserInfo";
      var columname="UserID";
    $.ajax({ 
              type: "POST",
              url: "../../../Handler/Office/SystemManager/UserCount.ashx?strcode="+userid+"&colname="+columname+"&tablename="+tablename,
              //data:'strcode='+$("#txtEquipCode").val()+'&tablename='+tablename,
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                 // AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() 
            {
                //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
            }, 
          success:function(data) 
            { 
                if(data.sta!=1) 
                { 
                    popMsgObj.ShowMsg(data.data);
                    document.getElementById("hfuserid").value=data .sta;
                }
                else
                {
                  document.getElementById("hfuserid").value=data .sta;
                 }            
            } 
           });
    }
  
}
function EnableInput(flag)
{
    $(":text").each(function(){ 
    this.disabled=flag; 
    }); 
    document.getElementById('txtPassword').disabled=flag;
    document.getElementById('txtRePassword').disabled=flag;
    document.getElementById('txtRemark').disabled=flag;
}
function InsertUserData() 
{ 
        if(!CheckInput())
        {
            return;
        }
    var txtUserID = $("#txtUserID").val();
    var txtUserName = $("#txtUserName").val();
    var EmployeeID = $("#EmployeeID").val();
    var txtPassword = $("#txtPassword").val();
    var txtRePassword = $("#txtRePassword").val();
    var txtOpenDate = $("#txtOpenDate").val();
    var txtCloseDate = $("#txtCloseDate").val();
    var txtRemark = $("#txtRemark").val();
    var UsedStatus = $("#UsedStatus").val();
    var chkLockFlag = document.getElementById('chkLockFlag').checked ? "1" : "0"; ///选中是1为锁定，不选中0为正常
    var chkIsHardValidate = document.getElementById('chkIsHardValidate').checked ? "1" : "0";                    
        var UrlParam = "UserId="+txtUserID+
                        "&UserName="+txtUserName+
                        "&LockFlag="+chkLockFlag+
                        "&EmployeeID="+EmployeeID+
                        "&UsedStatus="+UsedStatus+
                        "&Password="+txtPassword+
                        "&txtOpenDate="+txtOpenDate+
                        "&txtCloseDate=" + txtCloseDate +
                        "&IsHardValidate="+chkIsHardValidate+
                        "&Remark="+txtRemark;
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/SystemManager/UserInfo.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  //complete :function(){ //hidePopup();},
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta==1) 
                    {
                        popMsgObj.ShowMsg(data.info);
                        document.getElementById('txtUserID').disabled=true;
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
  

} 
function clearDiv()
{
//    document.getElementById('EmployeeID').value="0";
//    document.getElementById('txtUserID').value="";
//    document.getElementById('txtUserName').value="";
//    document.getElementById('txtPassword').value="";
//    document.getElementById('txtRePassword').value="";
//    document.getElementById('chkLockFlag').checked=false;
//    document.getElementById('txtOpenDate').value="";
//    document.getElementById('txtCloseDate').value="";
//    document.getElementById('UsedStatus').value="1";
//    EnableInput(false)
}

/*
* 返回按钮
*/
function DoBack()
{
    //获取查询条件
       var searchCondition = document.getElementById("hidSearchCondition").value;
        //获取模块功能ID
         var ModuleID = document.getElementById("hidModuleID").value;
        window.location.href = "UserInfo_Query.aspx?ModuleID=" + ModuleID + searchCondition;
    }


    window.onload = function() {
        var flag = document.getElementById("IsCompanyOpen").value;
        if (flag == "1") {
            document.getElementById("chkIsHardValidate").style.display = "";
            document.getElementById("spanUsbTitle").style.display = "";
        }
        else {
            document.getElementById("chkIsHardValidate").style.display = "none";
            document.getElementById("spanUsbTitle").style.display = "none";
        }
    }