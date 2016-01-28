<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleInfo_Edit.aspx.cs" Inherits="Pages_Office_SystemManager_RoleInfo_Edit" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色信息追加</title>

    <script src="../../../js/office/SystemManager/RoleInfoAdd.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <link href="../../../css/pagecss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .divboxbody.mydivleft
        {
            float: left;
            padding-left: 10px;
        }
        .divboxbody.mydivright
        {
            float: left;
            padding-right: 10px;
        }
    </style>
</head>
<body>
    <form name="formPackage" id="formPackage" runat="server">
    <div class="divbox">
        <input id="hf_id" type="hidden" value="0" />
        <asp:HiddenField ID="hfRoleID" runat="server" />
        <asp:HiddenField ID="action" Value="add" runat="server" />
        <asp:HiddenField ID="hidModuleID" runat="server" />
        <asp:HiddenField ID="hidSearchCondition" runat="server" />
        <span id="Forms" class="Spantype"></span>
        <uc1:Message ID="Message1" runat="server" />
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
                                新建角色信息
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">
                                <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btnSave" style="cursor: hand;
                                    float: left" border="0" onclick="InsertRoleData();" runat="server" visible="false" /><a
                                        onclick="DoBack();"><img src="../../../images/Button/Bottom_btn_back.jpg" border="0"
                                            style="float: left;" id="btnback" runat="server" /></a>
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
                                            基础信息<input type="hidden" id="strHiddenNum" name="strHiddenNum" value="0" />
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
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="Tb_01" style="display: block">
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" style="width:20%">
                                角色名称<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" colspan="3">
                                <input id="txtRoleID" name="txtRoleID" size="15" type="text" class="tdinput" style="width: 257px"
                                    specialworkcheck="角色名称" />
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
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick2')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Tb_03">
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width:20%">
                                备注
                            </td>
                            <td height="20" colspan="5" bgcolor="#FFFFFF">
                                <textarea name="txtRemark" id="txtRemark" class="tdinput" cols="50" rows="5"></textarea>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="2" bgcolor="#999999">
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="10">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="txtCompanyCD" runat="server" />
    </form>
</body>
</html>
