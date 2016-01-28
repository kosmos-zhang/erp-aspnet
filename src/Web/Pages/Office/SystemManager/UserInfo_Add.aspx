<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfo_Add.aspx.cs" Inherits="Pages_Office_SystemManager_UserInfo_Add" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <title>用户管理追加</title>&nbsp;<link href="../../../css/validatorTidyMode.css" rel="stylesheet"
        type="text/css" />

    <script src="../../../js/office/SystemManager/UserInfoAdd.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

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
        .style1
        {
            width: 273px;
        }
        #txtUserID
        {
            width: 224px;
        }
        #txtOpenDate
        {
            width: 223px;
        }
        #txtCloseDate
        {
            width: 218px;
        }
        .style2
        {
            width: 249px;
        }
        .style3
        {
            border-width: 0pt;
            background-color: #ffffff;
            height: 21px;
            margin-left: 2px;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <input  type="hidden" id="IsCompanyOpen" runat="server"/>
    <div id="popupContent">
    </div>
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
                            <span lang="zh-cn">新建</span>用户信息
                            <asp:HiddenField ID="hidModuleID" runat="server" />
                            <asp:HiddenField ID="hidSearchCondition" runat="server" />
                            <asp:HiddenField ID="txtUserName" runat="server" />
                            <input id="hfuserid" type="hidden" />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btnSave" style="cursor: hand;
                                float: left" border="0" onclick="InsertUserData();" runat="server" visible="false" />
                            <a onclick="DoBack();">
                                <img src="../../../images/Button/Bottom_btn_back.jpg" border="0" style="float: left;"
                                    id="btnback" runat="server" /></a>
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
                            <td align="right" bgcolor="#E6E6E6">
                                用户名<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" class="style1">
                                <input id="txtUserID" specialworkcheck="用户名" name="txtUserID" size="15" type="text"
                                    class="tdinput" onblur="CheckUserNum();" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                密码<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="223px" CssClass="tdinput"></asp:TextBox>
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                &nbsp;重复密码<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF">
                                &nbsp;<font color="red"></font><asp:TextBox ID="txtRePassword" runat="server" TextMode="Password"
                                    Width="216px" CssClass="tdinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="CloseDate">
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                员工姓名<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <font color="red">
                                    <select id="EmployeeID" runat="server" name="SetPro1" width="139px">
                                        <option></option>
                                    </select></font>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                生效日期<span class="redbold">*</span>&nbsp;
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                &nbsp;<input id="txtOpenDate" class="tdinput" name="txtbuydate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOpenDate')})"
                                    size="15" type="text" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                失效日期<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="txtCloseDate" class="tdinput" name="txtbuydate0" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCloseDate')})"
                                    size="15" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                是否锁定
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" >
                                <input id="chkLockFlag" type="checkbox" />
                            </td>
                             <td height="20" align="right" bgcolor="#E6E6E6">
                             <span  id="spanUsbTitle" style=" display:none"> 是否启用加密狗验证</span> 
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                           <input type="checkbox"  id="chkIsHardValidate" checked="true" runat="server" style=" display:none"/>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                              
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                            
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
                        id="Tb_03">
                        <tr>
                            <td height="20" align="center" bgcolor="#E6E6E6">
                                备注
                            </td>
                            <td height="20" colspan="5" bgcolor="#FFFFFF">
                                <textarea name="txtEquipRemark" id="txtRemark" class="tdinput" cols="50" rows="5"></textarea>
                                <input id="UsedStatus" type="hidden" value="1" />
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
    </div>
    </form>
    <p>
        <input id="hidden_companycd" type="hidden" runat="server" />
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
