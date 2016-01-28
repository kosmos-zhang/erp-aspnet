<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRole_Add.aspx.cs" Inherits="Pages_Office_SystemManager_UserRole_Add" %>

<%@ Register Src="../../../UserControl/RoleDrpControl.ascx" TagName="RoleDrpControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色关联追加</title>
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

    <script type="text/javascript">
 function GetUserName()
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
   var httpRequstResult = httpRequst("../../../Handler/Office/SystemManager/GetUserName.ashx?userid="+Userid);
   document.getElementById("txtUserName").value=httpRequstResult.split('|')[0];
   document.getElementById("LblRoleID").innerHTML =httpRequstResult.split('|')[1];
 }

    </script>

    <style type="text/css">
        .divboxbody.mydivleft
        {
            float: left;
            padding-left: 10px;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <div id="popupContent">
    </div>
    <div>
        <span id="Forms" class="Spantype"></span>
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
                                新建用户角色关联
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">
                                <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btnSave" runat="server"
                                    visible="false" style="cursor: hand; float: left" border="0" onclick="FormSubmit();" /><a
                                        onclick="DoBack();"><img src="../../../images/Button/Bottom_btn_back.jpg" border="0"
                                            style="float: left;" id="btnback" runat="server" /></a>
                                <asp:HiddenField ID="hidModuleID" runat="server" />
                                <asp:HiddenField ID="hidSearchCondition" runat="server" />
                            </td>
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
                                <td align="right" bgcolor="#E6E6E6" style="width: auto">
                                    用户名<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input type="hidden" id="HiddenRoleId" name="HiddenRoleID" runat="server" />
                                    <asp:DropDownList ID="Drp_UserInfo" runat="server" onchange="GetUserName();">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="CloseDate">
                                <td height="20" align="right" bgcolor="#E6E6E6" style="width: 6%">
                                    角色信息：
                                </td>
                                <td height="20" bgcolor="#FFFFFF">
                                    <div class="mydivleft">
                                        <asp:Label ID="LblRoleID" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="10">
                                </td>
                            </tr>
                        </table>
                        <input id="txtUserName" type="hidden" />
                </td>
            </tr>
        </table>
    </div>
    <uc1:Message ID="Message1" runat="server" />
    </form>
</body>
</html>
