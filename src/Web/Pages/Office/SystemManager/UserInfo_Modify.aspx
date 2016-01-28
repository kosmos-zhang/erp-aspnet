<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfo_Modify.aspx.cs" Inherits="Pages_Office_SystemManager_UserInfo_Modify" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <title>用户管理追加</title>&nbsp;<link href="../../../css/validatorTidyMode.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../js/office/SystemManager/UserInfoModify.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
   <script src="../../../js/common/Common.js" type="text/javascript"></script>
   <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>  

  <style type="text/css">                                                             
    #EmployeeID
    {
        width: 124px;
    }
    #Checkbox1
    {
        width: 20px;
    }
    #EmployeeID0
    {
        width: 124px;
    }
    </style></head>
 <body>
    <form id="frmMain" runat="server" >
        <input  type="hidden" id="IsCompanyOpen" runat="server"/>
    <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
    <div id="popupContent">
        <uc1:Message ID="Message1" runat="server" />
    </div>
    <span id="Forms" class="Spantype"></span>
      <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenUserID" runat="server" />
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
                            修改用户信息
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btnSave" style="cursor: hand; float:left"
                               border="0" onclick="InsertUserData();"  runat="server"  visible="false" />
                                             <a onclick="DoBack();">
                                            <img src="../../../images/Button/Bottom_btn_back.jpg" border="0"  style=" float:left;" id="btnback" runat="server"/><asp:HiddenField ID="hidSearchCondition" runat="server" />
                         
                            <asp:HiddenField ID="hidModuleID" runat="server" />
                            <asp:HiddenField ID="txtUserName" runat="server" />
                            </a></td>
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
       <div id="toachun">
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" style="display: block">
                    <tr>
                        <td align="right" bgcolor="#E6E6E6">
                            用户名<span class="redbold">*</span>
                        </td>
                        <td bgcolor="#FFFFFF">
                            <input id="txtUserID" class="tdinput" name="txtUserID" size="15" type="text" 
                                runat="server" disabled="disabled"  />
                        </td>
                        <td align="right" bgcolor="#E6E6E6">
                            员工姓名<span class="redbold">*</span></td>
                        <td bgcolor="#FFFFFF">
                            <font color="red">
                                <select id="EmployeeID" runat="server" name="SetPro1" width="139px">
                                    <option></option>
                                </select></font></td>
                        <td align="right" bgcolor="#E6E6E6">
                            锁定
                        </td>
                        <td bgcolor="#FFFFFF">
                            &nbsp;<font color="red"><input id="UsedStatus" type="hidden" value="1" runat="server"/></font><input id="chkLockFlag" type="checkbox" runat="server" /></td>
                    </tr>
                    <tr id="CloseDate">
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            生效日期<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF" >
                            <input id="txtOpenDate" class="tdinput" name="txtbuydate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOpenDate')})"
                                size="15" type="text" /></td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            失效日期<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF" class="tdinput" >
                            <input id="txtCloseDate" class="tdinput" name="txtbuydate0" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCloseDate')})"
                                size="15" type="text" /> </td>
                                 <td height="20" align="right" bgcolor="#E6E6E6">
                             <span  id="spanUsbTitle" style=" display:none"> 是否启用加密狗</span> 
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                           &nbsp;<input type="checkbox"  id="chkIsHardValidate" checked="true" runat="server" style=" display:none"/>
                            </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        备注信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick2'>
                                            <img src="../../../images/Main/Open.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick2')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    id="Tb_03" style="display: none">
                    <tr>
                        <td height="20" align="center" bgcolor="#E6E6E6">
                            备注
                        </td>
                        <td height="20" colspan="5" bgcolor="#FFFFFF">
                            <textarea name="txtEquipRemark" id="txtRemark" class="tdinput" cols="50" rows="5" runat="server"></textarea>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="2" bgcolor="#999999">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
    <p>
        <input id="Hidden1" type="hidden" />
    </p>
    </body>
</html>
