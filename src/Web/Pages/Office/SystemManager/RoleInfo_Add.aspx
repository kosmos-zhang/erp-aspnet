<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleInfo_Add.aspx.cs" Inherits="Pages_Office_SystemManager_RoleInfo_Add" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
        <title>修改角色信息</title>
    <link href="../../../css/validatorTidyMode.css" rel="stylesheet"
        type="text/css" />
     <script src="../../../js/office/SystemManager/RoleInfoAdd.js" type="text/javascript"></script>                                                                                                                                        
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>   

    <script src="../../../js/common/Check.js" type="text/javascript"></script>
        <style type="text/css">
            .style1
            {
                height: 16px;
            }
            .style2
            {
                border-width: 0pt;
                background-color: #ffffff;
                height: 21px;
                margin-left: 2px;
                width: 257px;
            }
        </style>
        </head>
    <body>
        <form name="formPackage" id="formPackage"  runat="server">
<div class="divbox">
	<asp:HiddenField ID="hfRoleID" runat="server"/>
    <uc1:Message ID="Message1" runat="server" />
    <asp:HiddenField ID="actionedit" Value="edit" runat="server"/>
    <span id="Forms" class="Spantype"></span>
                                             <a onclick="DoBack();">
                                            <asp:HiddenField ID="hidSearchCondition" runat="server" />
                         
                            <asp:HiddenField ID="hidModuleID" runat="server" />
                            </a>
       <input id="strHiddenNum" type="hidden" />
       <input id="txtCompanyCD" type="hidden" runat="server" />
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
                            修改角色信息<input id="hf_id" type="hidden" />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btnSave" style="cursor: hand; float:left"
                                 border="0" onclick="InsertRoleData();"  runat="server" visible="false"/>
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
                        <td align="right" bgcolor="#E6E6E6" width="20%">
                            角色名称<span class="redbold">*</span>
                        </td>
                        <td bgcolor="#FFFFFF">
                            <input id="txtRoleID" class="style2" name="txtRoleID" size="15" type="text" 
                                runat="server" disabled="disabled" specialworkcheck="角色名称" />
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
                                    <td >
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
                    id="Tb_03" >
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" width="20%">
                            备注
                        </td>
                        <td height="20" colspan="5" bgcolor="#FFFFFF">
                            <textarea name="txtRemark" id="txtRemark" class="tdinput" cols="50" rows="5" runat="server"></textarea>
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
</form>
</body>
</html>
