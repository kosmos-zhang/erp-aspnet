$(document).ready(function(){
 requestobj = GetRequest();
       recordnoparam = requestobj['UserIDFlag'];
          if(typeof(recordnoparam)=="undefined")
          {
           GetUserName();
          }
    });    
function FormSubmit()
{
 var Userid = document.getElementById("Drp_UserInfo").value;
  if(Userid=="0")
   {
      popMsgObj.ShowMsg("请选择用户！");
      return;
   }
    if(Userid=="")
   {
      popMsgObj.ShowMsg("请先新建用户！");
      return;
   }
   var getForm = document.formPackage;
   var UserID =document.getElementById("Drp_UserInfo").value;
   //获取选项
   var boxes = document.getElementsByName("RoleID");   
   var CheckboxValue = "0";
   for(var i=0;i<boxes.length;i++) 
   { 
      if(boxes[i].checked)
      {
         CheckboxValue = CheckboxValue + "," + boxes[i].value;
      }     
   }
   if(CheckboxValue=="0")
   {
     popMsgObj.ShowMsg("请选择角色！");
      return;
   }
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/SystemManager/UserRole.ashx?RoleIDArray="+CheckboxValue+"&UserID="+UserID,
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

 function GetUserName()
 { 
   var Userid = document.getElementById("Drp_UserInfo").value;
  
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
        window.location.href = "UserRole_Query.aspx?ModuleID=" + ModuleID + searchCondition;
}