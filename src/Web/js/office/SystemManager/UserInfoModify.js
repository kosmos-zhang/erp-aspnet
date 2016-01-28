function CheckInput()
{

 var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var txtUserName = document.getElementById("txtUserName").value;//用户姓名
    var txtCloseDate = document.getElementById("txtCloseDate").value;//失效日期
    var txtOpenDate = document.getElementById("txtOpenDate").value;//生效日期
    var txtRemark = document.getElementById("txtRemark").value;//备注
    var UsedStatus = document.getElementById("UsedStatus").value;//启用状态
    var UserId=document.getElementById("hiddenUserID").value;     
      var EmName=document.getElementById("EmployeeID").value;
     if(EmName=="0")
    {
     isFlag = false;
        fieldText = fieldText + "员工姓名|";
   		msgText = msgText + "请输入员工姓名|";
    }
//    if(strlen(txtOpenDate)<=0)
//    {
//        isFlag = false;
//         fieldText = fieldText +  "生效日期|";
//   		 msgText = msgText + "请选择生效日期|";   
//    }
     if(strlen(txtRemark)>100)
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
//    if(strlen(txtCloseDate)<=0)
//    {
//        isFlag = false;
//         fieldText = fieldText +  "失效日期|";
//   		 msgText = msgText + "请选择失效日期|";   
//    }
 
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
    var chkLockFlag = document.getElementById('chkLockFlag').checked ? "1" : "0";                            ///选中是1为锁定，不选中0为正常
    var chkIsHardValidate = document.getElementById('chkIsHardValidate').checked ? "1" : "0";     
        var UrlParam = "UserId="+txtUserID+
                        "&UserName="+txtUserName+
                        "&LockFlag="+chkLockFlag+
                        "&EmployeeID="+EmployeeID+
                        "&UsedStatus="+UsedStatus+
                        "&txtOpenDate="+txtOpenDate+
                        "&txtCloseDate="+txtCloseDate+
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
    document.getElementById('EmployeeID').value="0";
    document.getElementById('txtUserID').value="";
    document.getElementById('txtUserName').value="";
    document.getElementById('txtPassword').value="";
    document.getElementById('txtRePassword').value="";
    document.getElementById('chkLockFlag').checked=false;
    document.getElementById('txtOpenDate').value="";
    document.getElementById('txtCloseDate').value="";
    document.getElementById('UsedStatus').value="1";
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