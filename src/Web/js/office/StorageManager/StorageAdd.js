
  $(document).ready(function(){
            init(); 
        });
        
      var treeview1=null; 
         function init(){             
               $.ajax({ 
                type: "POST",
                url: "../../../Handler/Common/UserDept.ashx?action=loaduserlistwithdepartment",
                dataType:'string',
                data: '',
                cache:false,
                success:function(data) 
                {                          
                    var result = null;
                    eval("result = "+data);
                    
                    if(result.result)                    
                    {                      
                          /// <param name="containerID">TreeView的容器元素的ID</param>
                        /// <param name="nodes">节点数组</param>     
                                 
                        /// <param name="selMode">选择模式0:多选;1:单选</param>
                        /// <param name="selNodeType">可选节点类型:0不限制</param>        
                       /// <param name="expandLevel">默认展开层级</param>
                       /// <param name="mode">弹出(1) OR 平板 显示方式(0)</param>     
                       // <param name="valNodeType">取值节点类型</param>                    
                       /// <param name="selDuplicate">取值是否允许重复</param>
                         treeview1 = new TreeView("treeDiv1",result.data,0,0,4,1,2,false);
                   
                    }else{                  
                           alert(result.data);               
                    }                   
                },
                error:function(data)
                {
                     alert(data.responseText);
                }
            });
       }     










function Fun_Save_Storage()
{
 
    var userListName1=$("#txtUserList").val();
    if (userListName1=="")
    {
 document .getElementById ("txtUserListHidden").value="";
    }
    
    
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var bmgz="";
    var txtStorageNo="";
    var txtIndentityID =$("#txtIndentityID").val();
        if(txtIndentityID=="0")//新建
        {
            if($("#txtStorageNo_ddlCodeRule").val()=="")//手工输入
            {
                txtStorageNo=$("#txtStorageNo_txtCode").val();
                bmgz="sg";
                if (txtStorageNo == "")
                {
                    isFlag = false;
                    fieldText += "仓库编号|";
   		            msgText += "请输入仓库编号|";
                }
                else if(!CodeCheck($("#txtStorageNo_txtCode").val()))
                {
                    isFlag = false;
                    fieldText = fieldText + "仓库编号|";
   		            msgText = msgText +  "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
                }
            }
            else
            {
                txtStorageNo=$("#txtStorageNo_ddlCodeRule").val();
                bmgz="zd";
            }
        }
         else//编辑
         {
            txtStorageNo=document.getElementById("lbStorageNo").innerHTML;
         }    
    
    var txtStorageName = $("#txtStorageName").val();
    var sltType = $("#sltType").val();
    var sltUsedStatus = $("#sltUsedStatus").val();
    var txtRemark = $("#txtRemark").val();
    var Action = 'Add';
    var hiddenValue = $("#txtStorageNoHidden").val();
    var storageAdmin=document .getElementById ("txtExecutorID").value.Trim();
      
    
    if(hiddenValue == '0')
    {
        isFlag=false;
        fieldText=fieldText + "仓库编号|";
        msgText = msgText +  "仓库编号不允许重复|";
    }
    if(strlen(txtStorageNo)>50)
    {
        isFlag = false;
        fieldText = fieldText + "仓库编号|";
   		msgText = msgText +  "仅限于50个字符以内|";
    }
    if(strlen(txtStorageName.Trim())<=0)
    {
        isFlag = false;
        fieldText = fieldText + "仓库名称|";
   		msgText = msgText +  "请输入仓库名称|";
    }
    
    if(strlen(txtStorageName.Trim())>100)
    {
        isFlag = false;
        fieldText = fieldText + "仓库名称|";
   		msgText = msgText +  "仓库名称仅限于100个字符以内|";
    }

    if(strlen(txtRemark.Trim())>100)
    {
        isFlag = false;
        fieldText = fieldText + "仓库说明|";
   		msgText = msgText +  "说明仅限于100字符以内|";
    }
    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }

    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    else
    {  
    var userListId=$("#txtUserListHidden").val();
    var userListName=$("#txtUserList").val();


     var   UrlParam = "Action="+escape(Action)+"\
                        &CanViewUser="+escape(userListId )+"\
                        &storageAdmin="+escape(storageAdmin)+"\
                        &StorageNo="+escape(txtStorageNo)+"\
                        &StorageName="+escape(txtStorageName)+"\
                        &StorageType="+escape(sltType)+"\
                        &UsedStatus="+escape(sltUsedStatus)+"\
                        &Remark="+escape(txtRemark)+"\
                        &bmgz="+bmgz+"\
                        &ID="+txtIndentityID+"";

     
    
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageAdd.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     AddPop();
                  }, 
                  complete :function(){ hidePopup();},
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta==1) 
                    {
                        var reInfo=data.data.split('|');
                        if(reInfo.length > 1)
                        { 
                            document.getElementById('div_StorageNo_uc').style.display="none";
                            document.getElementById('div_StorageNo_Lable').style.display="block";
                            if(parseInt(txtIndentityID)<=0)
                            {   
                                document.getElementById('lbStorageNo').innerHTML = reInfo[1];
                                document.getElementById('txtIndentityID').value= reInfo[0];
                            }
                        }    
                    }
                  popMsgObj.ShowMsg(data.info);
                  document.getElementById("t_Edit").style.display="block";
                  document.getElementById("t_Add").style.display="none";
                  
                  } 
               });
    }
    
}

//验证唯一性
function checkonly(StorageNo)
{
    var InNo=StorageNo;
    var TableName="officedba.StorageInfo";
    var ColumName="StorageNo";
    $.ajax({ 
              type: "POST",
              url: "../../../Handler/CheckOnlyOne.ashx?strcode='"+InNo+"'&colname="+ColumName+"&tablename="+TableName+"",
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                 // AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() 
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
            }, 
            success:function(data) 
            { 
                if(data.sta!=1) 
                { 
                    document.getElementById('txtStorageNoHidden').value = data.sta;
                }
                else
                {
                    document.getElementById('txtStorageNoHidden').value = "";
                } 
            } 
           });
}

/*
* 返回按钮
*/
function DoBack()
{
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    moduleID = document.getElementById("hidModuleID").value;
    window.location.href = "StorageInfo.aspx?ModuleID=" + moduleID + searchCondition;
}