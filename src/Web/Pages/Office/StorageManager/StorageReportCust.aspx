<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageReportCust.aspx.cs" Inherits="Pages_Office_StorageManager_StorageReportCust" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 
    <title>往来客户选择</title>
    <base target="_self"/>
     <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
     
   //获取选择节点的编码和名称
   var oldcontrol="";
  function SelectedNodeChanged(nodeIndo,control)
  { 
    if(oldcontrol!="" && oldcontrol!=null)
    {
       document.getElementById(oldcontrol).style.background="";
    }
    oldcontrol=control;
    document.getElementById('txtNodeCode').value=nodeIndo;
    if(control!="" && control!=null)
    {
     document.getElementById(control).style.background="#66CCFF";
    }
  }
  
  //清除信息
  function ClearInfo()
  {
     window.returnValue="clear";
     window.close();
  }

   //获取节点值          
  function GetNode()
  {

     var div_id=document.getElementById("dv_tree");
     var objs=div_id.getElementsByTagName('input');
     var select="";
     //遍历所有控件
     for(var i = 0; i < objs.length; i++)
      {
          //判断是否是选中的radiobutton
          if(objs[i].getAttribute("type") == "radio" && objs[i].checked)
           {
            //获取列的值
             var values = objs[i].value;
             select += values.toString();
           }
       }
              if(select=="")
                {
                    alert("请选择往来单位！");
                    return;
                }
                select=select.substring(0,select.length);
                window.returnValue=select;
                window.close();
        
  }
     </script>
</head>
<body style="background-color:#FFFFFF"> 
    <form id="form1" runat="server">

    <div id="dv_tree" style="height:350px; width:300px;overflow-x:hidden;overflow-y:auto;">
        <asp:TreeView ID="CustTree" runat="server" ShowLines="True">
   
        </asp:TreeView>
           </div>
   
<p>
 <img src="../../../Images/Button/Bottom_btn_confirm.jpg" runat="server"  id="BtnGetValue" onclick="GetNode();"  />&nbsp;<img src="../../../Images/Button/Bottom_btn_cancel.jpg" runat="server"   id="btnCancel" onclick="window.close();" />

 &nbsp;  <img src="../../../images/Button/Bottom_btn_del.jpg" alt="清空"  id="btnclear"  onclick="ClearInfo();"    runat="server"  style='cursor:pointer;' width="51" height="25" />
    <input id="txtNodeCode"  type="hidden" />
    
    
    </p>
    <uc1:Message ID="Message1" runat="server" />
    </form>
</body>
</html>

