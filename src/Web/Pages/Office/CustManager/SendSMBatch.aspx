<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendSMBatch.aspx.cs" Inherits="Pages_Office_CustManager_SendSMBatch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>群发手机短信</title>
<link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
   <script src="../../../js/common/TreeView.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function msgbox(msg)
        {
             showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msg);
        }
    
    
        function fetchTreeviewValueCallback(val)
        {
           
           if(val.val + "" == "")
           {
                document.getElementById("chkAll").checked = false;
           }else{
                if( treeview1.checkAllSelected() )
                {
                    document.getElementById("chkAll").checked = true;
                }          
           };
                     
                   //  alert(val.txt+":"+val.val);
                     
           if(val.val + "" == "")
           {
            document.getElementById("hiddenNumList").value = "";
            document.getElementById("numCnt").innerHTML = "0";
             return;
           }
            document.getElementById("hiddenNumList").value = val.val;
            document.getElementById("numCnt").innerHTML = val.val.split(',').length;
        }
    
        function switchAllChks(obj)
        {
            if(obj.checked)
            {
                treeview1.selectAll();
                
                var selVal = treeview1.getValue();
                
                document.getElementById("numCnt").innerHTML = selVal.val.split(',').length;
                 document.getElementById("hiddenNumList").value = selVal.val;
            }else{
                treeview1.unSelectAll();
                document.getElementById("numCnt").innerHTML = 0;
                document.getElementById("hiddenNumList").value = "";
            };
            
        }
    
        var treeview1=null;       
         function init(){             
               $.ajax({ 
                type: "POST",
                url: "../../../Handler/Common/UserDept.ashx?action=GetLinkManEx",
                dataType:'string',
                data: '',
                cache:false,
                success:function(data) 
                {                       
                //alert(data);   
                    var result = null;
                    eval("result = "+data);
                    
                    if(result.result)                    
                    {                      
                       
                        //var nodes=[{text:"全部",value:"0",nodeType:-1,subNodes:result.data}];
                    
/// <summary>
/// TreeView
/// </summary>
/// <param name="containerID">TreeView的容器元素的ID</param>
/// <param name="nodes">节点数组</param>
/// <param name="selMode">选择模式0:多选;1:单选</param>
/// <param name="selNodeType">可选节点类型:0不限制</param>
/// <param name="expandLevel">默认展开层级</param>      
/// <param name="mode">弹出 OR 平板 显示方式</param>      
/// <param name="valNodeType">取值节点类型</param>     
/// <param name="selDuplicate">取值是否允许重复（1:启用；0:禁用）</param>
/// <param name="enableLinkage">是否启用联动效果(1:启用；0:禁用,默认：1)</param>
                         treeview1 = new TreeView("treeDiv1",result.data,0,0,0,0,2,false);              
                         treeview1.callback = fetchTreeviewValueCallback;
                                  
                    }else{                  
                           msgbox(result.data);               
                    }                   
                },
                error:function(data)
                {
                     msgbox(data.responseText);
                }
            });
       }
          
       $(document).ready(function(){
            init();
        });
        
        
        function freshLength(obj)
        {
            document.getElementById("txtCnt").innerHTML = obj.value.length;
        }
        
        function checkInput()
        {           
            var txtContent = document.getElementById("txtContent").value;
            var cnt = txtContent.length ;
            
            
//            var rg = /[\u4e00-\u9fa5]/g;
//            var resultM = txtContent.match(rg);
//            
//            if(resultM)
//            {                 
//                 //alert( resultM.length);
//                 cnt = txtContent.length + resultM.length; 
//            }
           
            if(cnt < 1)
            {
                 msgbox("请输入要发送的短信内容");
                return false;
            }
            
            if(cnt > 120)
            {
                msgbox("内容过长，最多允许120个字");
                return false;
            }
            
            
            if(document.getElementById("hiddenNumList").value == "")
            {
                 msgbox("请选择短信接收人");
                return false;
            }
            
        }
    </script>
        
    <style type="text/css">
        .style1
        {
            width: 295px;
        }
        .style2
        {
            width: 177px;
        }
        .style3
        {
            width: 172px;
        }
    </style>
        
</head>
<body>
<br/>

<form id="EquipAddForm" runat="server" >
<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
  <tr>
    <td valign="top">
        <input type="hidden" id="hiddenEquipCode" value="" />
        <img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
    <td align="center" valign="top"></td>
  </tr>
  <tr>
    <td height="30" colspan="2" valign="top" class="Title">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td height="30" align="center" class="Title">
                            群发手机短信 </td>
        </tr>
      </table>
      
      </td></tr>
  <tr>
    <td colspan="2" valign="top" width="100%" >
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="6"></td>
      </tr>
    </table>
        
   
      
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="20" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
              <tr>
                <td class="style3"> </td>
                <td >
                   
                    <asp:ImageButton ID="ImageButton1" 
                        ImageUrl="../../../Images/Button/Main_btn_send.jpg" runat="server" 
                        onclick="ImageButton1_Click" />
                   
                  </td>
                <td align="left">
                     &nbsp;</td>
              </tr>
          </table></td>
        </tr>
      </table>
            
      <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01" >
     
        <tr>
            <td bgcolor="#E6E6E6" align="right" class="style2">&nbsp;</td><td bgcolor="#FFFFFF" 
                class="style1"  >
                        短信接收人 <input type="checkbox" id="chkAll" onclick="switchAllChks(this)" />全选</td><td bgcolor="#FFFFFF">
                    
                        短信内容</td>
        </tr>   
        <tr>
            <td bgcolor="#E6E6E6" align="right" valign="top" class="style2">&nbsp;</td><td bgcolor="#FFFFFF" 
                class="style1" >            
                
                <div id="treeDiv1" style="width:240px;margin:0;height:360px;overflow:auto;border:solid 1px #999999;"></div>

                    
                 </td>
                 <td valign="top" bgcolor="#FFFFFF" >
                        <asp:HiddenField ID="hiddenNumList" runat="server" />
                        <asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server" Rows="16" Columns="60" 
                            Height="313px" Width="351px"></asp:TextBox>
                            <br />
                            <br />
                            
                     <asp:CheckBox ID="cbAddtionalInfo" Text="是否将接收人的 姓名+先生/女士 包括到短信开头 " runat="server" />                           
                       
                 </td>
        </tr>       
        
         <tr>
            <td bgcolor="#E6E6E6" align="right" class="style2">&nbsp;</td><td align="left" bgcolor="#FFFFFF" 
                class="style1"  >
                        已选择联系人数量：<span id="numCnt">0</span>;&nbsp;&nbsp;还可以发送 
                <asp:Label ID="smCnt" runat="server" Text="0"></asp:Label> 条</td><td bgcolor="#FFFFFF"  align="left">              
                        已输入短信字数：<span id="txtCnt">0</span>, 最多120个字</td>
        </tr>
        
      </table>
      
      
      </td>
  </tr>
  
  <tr>
        <td height="28" bgcolor="#FFFFFF">&nbsp;</td>
   </tr>
  
      
      </table>
     
    </form>
   

</body>

</html>
