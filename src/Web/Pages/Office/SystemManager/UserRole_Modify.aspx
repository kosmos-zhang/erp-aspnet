<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRole_Modify.aspx.cs" Inherits="Pages_Office_SystemManager_UserRole_Modify" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色权限修改</title>
 <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>   
    <script src="../../../js/common/Ajax.js" type="text/javascript"></script>

    <script src="../../../js/office/SystemManager/UserRoleAdd.js" type="text/javascript"></script>
<style type="text/css">
body {
	background-color: #FFFFFF;
}
.divboxbody.mydivleft{float:left;padding-left:10px;}
.divboxbody.mydivright{float:left;padding-right:10px;}
</style>
<script type="text/javascript">
function FormSubmit()
{
   var getForm = document.formPackage;
   var action = getForm.action.value;
   var UserID = getForm.hfUserID.value;
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
   var httpRequstResult = httpRequst("../../../Handler/Office/SystemManager/UserRole.ashx?action="+action+"&RoleIDArray="+CheckboxValue+"&UserID="+UserID);
   //alert(httpRequstResult);
   if(httpRequstResult=="True"){
     popMsgObj.ShowMsg("保存成功！");
   }else{
       popMsgObj.ShowMsg("保存失败！");
   }   
}

</script>
</head>
<body>
<form id="formPackage" runat="server">

<div class="divbox">
	<div class="divboxtitle"><span>用户角色关联</span></div>
    <asp:HiddenField ID="action" Value="add" runat="server"/>
    <asp:HiddenField ID="hfUserID" Value="" runat="server"/>
                                             <a onclick="DoBack();">
                                            <asp:HiddenField ID="hidSearchCondition" runat="server" />
                         
                            <asp:HiddenField ID="hidModuleID" runat="server" />
                            </a>
</div>
      <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenUserID" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            <uc1:Message ID="Message1" runat="server" />
                            修改角色关联信息
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btnSave" runat="server" visible="false" style="cursor: hand; float:left"
                                 border="0" onclick="FormSubmit();"/>
                                             <a onclick="DoBack();">
                                            <img src="../../../images/Button/Bottom_btn_back.jpg" border="0"  style=" float:left;" id="btnback" runat="server"/></a></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        基础信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" style="display: block">
                    <tr>
                        <td align="right" bgcolor="#E6E6E6" style="width:auto">
                            用户ID<span class="redbold">*</span>
                        </td>
                        <td bgcolor="#FFFFFF" colspan="3">
                            <input type="hidden" id="HiddenRoleId" name="HiddenRoleID" runat="server"  />
          <asp:Label ID="lbUserID" runat="server" onkeydown="return false;"></asp:Label>
                        </td>
                    </tr>
                    <tr id="CloseDate">
                        <td height="20" align="right" bgcolor="#E6E6E6" style=" width:6%">
                            角色信息：</td>
                        <td height="20" bgcolor="#FFFFFF" colspan=3 >
                        <div class="mydivleft">    	  		  
          <asp:Label ID="LblRoleID" runat="server"></asp:Label>
                         </div>
                         </td>
                   
                    </tr>
                       <asp:Label ID="lbUserName" runat="server"></asp:Label>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
