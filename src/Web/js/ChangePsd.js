var flagpsd=false;
//修改密码
function EditPwd()
{
  var olepsd=document.getElementById("txtOldPassword").value;
   var fieldText = "";
   var msgText = "";
   var isFlag = true;
   var flag=true;
   var UserID=trim(document.getElementById("txt_User").value);
   var Password=trim(document.getElementById("hf_psd").value);
  var OldPassword=trim(document.getElementById("txtOldPassword").value);
  var NewPassword=trim(document.getElementById("txtNewPassword").value);
   var RePassword=trim(document.getElementById("txtRePassword").value);
   var Commanycd=document.getElementById("hfcommanycd").value;
  if(OldPassword.length<=0)
  {
   fieldText="原密码不能为空！";  
  }
//  if(strlen(OldPassword)>16||strlen(OldPassword)<8)
//    {
//       fieldText=fieldText+"\n"+"原密码位数处于8-16位之间";
//    }
    if(OldPassword.length>0)
    if(flagpsd)
    {
   fieldText=fieldText+"\n"+"原密码不正确，请重新输入！";
    }
  if(NewPassword.length<=0)
  {
  fieldText=fieldText+"\n"+"新密码不能为空！";
  }
  if(NewPassword!="")
  if(strlen(NewPassword)>16||strlen(NewPassword)<8)
    {
     fieldText=fieldText+"\n"+"新密码位数处于8-16位之间！";
    }
    if(NewPassword!="")
    if(OldPassword==NewPassword||(Password==NewPassword))
    {
     fieldText=fieldText+"\n"+"新密码不能与原密码相同！";
    }
    if(NewPassword!="")
    if(NewPassword!=RePassword)
    {
    fieldText=fieldText+"\n"+"新密码与确认新密码不相同！";
    }
    if(fieldText!="")
    {
     alert(fieldText)
    return;
    }
    var postParams = "UserID=" + UserID + "&Password=" + Password + "&OldPassword=" + OldPassword+"&NewPassword="+NewPassword+"&CommanyCD="+Commanycd;
    $.ajax({ 
                  type: "POST",
                  url: "Handler/ChangePsd.ashx?" + postParams,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  error: function() 
                  {
                     alert("保存失败！");
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta==1) 
                    {
//                        popMsgObj.ShowMsg(data.info); 
                    alert("保存成功！");
                      document.getElementById("hf_psd").value=data.data;
                      ClearText();
                    }
                    else
                    {
                    if(data.sta==3)
                    {
                    flagpsd=true;
                    }
                    else 
                        alert("保存失败！");

                    }
                  } 
               });
}
function ClearText()
{

    document.getElementById("txtOldPassword").value="";
    document.getElementById("txtNewPassword").value="";
    document.getElementById("txtRePassword").value="";
     closeRotoscopingDiv(false,'divBackShadow');
     document.getElementById("ChangePsd").style.display = 'none';
}
function OnlyPsd()
{
 var Password=trim(document.getElementById("hf_psd").value);
  var OldPassword=trim(document.getElementById("txtOldPassword").value);
  var NewPassword=trim(document.getElementById("txtNewPassword").value);
   var RePassword=trim(document.getElementById("txtRePassword").value);
  var postParams = "flag=Only&Password=" + Password + "&OldPassword=" + OldPassword;
    $.ajax({ 
                  type: "POST",
                  url: "Handler/ChangePsd.ashx?" + postParams,
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
                   if(data.sta==3)
                   {
                    flagpsd=true;
                   }
                   else 
                   flagpsd=false;
                  } 
               });
}