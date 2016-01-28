//修改密码
function ClearText()
{
    document.getElementById("txtNewPassword").value="";
    document.getElementById("txtRePassword").value="";
}
 function ShowOldPwd()
{
   var olepsd=document.getElementById("txtOldPassword").value;
   var fieldText = "";
   var msgText = "";
   var isFlag = true;
   var flag=true;
   var UserID=trim(document.getElementById("txt_User").value);
  var NewPassword=trim(document.getElementById("txtNewPassword").value);
   var RePassword=trim(document.getElementById("txtRePassword").value);
   var Commanycd=document.getElementById("hfcommanycd").value;
   var psd= document.getElementById("hf_psd").value;
   var hfuserid=document.getElementById("hfuserid").value;
   if(UserID.length<=0)
  {
        isFlag = false;
        fieldText = fieldText + "用户名|";
   		msgText = msgText +  "请选择用修改的用户|";   
  }
  if(NewPassword.length<=0)
  {
        isFlag = false;
        fieldText = fieldText + "新密码|";
   		msgText = msgText +  "新密码不能为空|";   
  }
  if(strlen(NewPassword)>16||strlen(NewPassword)<8)
    {
        isFlag = false;
        fieldText = fieldText +  "新密码|";
   		msgText = msgText + "新密码位数处于8-16位之间|";
    }
//    if(NewPassword!="")
//    if(psd==NewPassword)
//    {
//       isFlag = false;
//        fieldText = fieldText +  "新密码|";
//   		msgText = msgText + "新密码不能与原密码相同|";
//    }
    if(NewPassword!=RePassword)
    {
      isFlag = false;
        fieldText = fieldText +  "新密码|";
   		msgText = msgText + "新密码与确认新密码不相同|";
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return;
    }
    var oldpsd= document.getElementById("hf_psd").value;
    var  returnValue=window.showModalDialog("SurePsd.aspx?oldpsd="+oldpsd,"","dialogWidth=350px;dialogHeight=100px; center:yes;help:no;resizable:no;status:no;fullscreen;borderstyle:0;"); 
     if(typeof(returnValue)!="undefined")
     {
       var postParams = "UserID=" + UserID + "&NewPassword="+NewPassword+"&CommanyCD="+Commanycd+"&hfuserid="+hfuserid;
    $.ajax({ 
                  type: "POST",
                   url: "../../../Handler/Office/SystemManager/ChangePsd.ashx?"+postParams,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta==1) 
                    {
                        popMsgObj.ShowMsg("保存成功");
                      document.getElementById("hf_psd").value=data.data;
                      ClearText();
                    }
                    else
                    {
                    if(data.sta==3)
                    {
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","原密码不正确，请重新输入！");
                    }
                    else 
                        popMsgObj.ShowMsg(data.info);

                    }
                  } 
               });
     }

   

}