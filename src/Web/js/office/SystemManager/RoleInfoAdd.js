var isnew="Add";
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var txtRoleID = trim(document.getElementById("txtRoleID").value);//角色名称
    var txtCompanyCD = document.getElementById("txtCompanyCD").value;//所属公司编码
    var txtRemark = document.getElementById("txtRemark").value;//角色备注
     var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(txtRoleID==0)
    {
        isFlag = false;
        fieldText = fieldText + "角色名称|";
   		msgText = msgText +  "请输入角色名称|";
   		
    }
    if(strlen(txtRoleID)>50)
    {
        isFlag = false;
        fieldText = fieldText + "角色名称|";
   		msgText = msgText + "角色名称仅限于50个字符以内|";
    }      
//     if(!txtRoleID.match(/^[\u4e00-\u9fa5a-zA-Z]+$/))
//    { 
//       isFlag = false;
//        fieldText = fieldText + "角色名称|";
//   		msgText = msgText + "角色名称必须是中文、字母的组合|";
//    }
     if(strlen(txtRemark)>=0)
    {
       if(strlen(txtRemark)>50)
       {
        isFlag = false;
        fieldText = fieldText + "备注|";
   		msgText = msgText + "备注仅限于50个字符以内|";
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

function EnableInput(flag)
{
    $(":text").each(function(){ 
    this.disabled=flag; 
    }); 
 document.getElementById('txtRemark').disabled=flag;
}
function InsertRoleData() 
{ 
        if(!CheckInput())
        {
            return;
        }
//    var strHiddenNum = document.getElementById('strHiddenNum').value;
//    if(strHiddenNum=="3")
//     {
//      popMsgObj.ShowMsg("角色名称已经存在，请重新输入！");
//      return;
//     }
    var RoleName = $("#txtRoleID").val();                   //角色名称
    var remark = $("#txtRemark").val();                //备注
    var CompanyCD=$("#txtCompanyCD").val();
    var Actionflag=$("#actionedit").val();
    var roleid=$("#hfRoleID").val();
    var id=document.getElementById("hf_id").value;
    if(typeof(Actionflag)=="undefined")
    {
      Actionflag=$("#actionedit").val();
    }
    if(typeof(roleid)=="undefined")
    {
      roleid=$("#hfRoleID").val();
    }
     var tablename="RoleInfo";
      var columname="RoleName";
        var UrlParam = "RoleName="+escape(RoleName)+"\
                        &CompanyCD="+CompanyCD+"\
                        &action="+isnew+"\
                        &actionedit="+Actionflag+"\
                        &roleid="+roleid+"\
                        &tablename="+tablename+"\
                        &columname="+columname+"\
                        &id="+id+"\
                        &Remark="+escape(remark)+"";
        $.ajax({ 
                  type: "POST",
                   url: "../../../Handler/Office/SystemManager/RoleInfo.ashx?"+(UrlParam),
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
                    if(data.data=="1") 
                    {
                        popMsgObj.ShowMsg(data.info);
                        document.getElementById("hf_id").value=data.sta;
                        document.getElementById("txtRoleID").disabled=true;
                        isnew="Edit";
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
   document.getElementById('txtRoleID').value="";
   document.getElementById('txtRemark').value="";
   isnew="Add";
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
        window.location.href = "RoleInfo_Query.aspx?ModuleID=" + ModuleID + searchCondition;
}