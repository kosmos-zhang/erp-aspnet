<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<%@ Register src="UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/default.css" rel="stylesheet" type="text/css" />
    <title>修改密码</title>

    <script src="js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="js/ChangePsd.js" type="text/javascript"></script>
    <script src="js/common/Check.js" type="text/javascript"></script>
    <script src="js/common/Common.js" type="text/javascript"></script>
    <style type="text/css">
.tdinput
{
	border-width:0pt;
	background-color:#ffffff;
	height:21px;
	margin-left:2px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server"><span id="Forms" class="Spantype"></span>
    <div style="border: solid 2px #898989; background: #fff;  padding: 10px; width: 400px; z-index: -1; position: absolute;top: 53%; left: 68%; margin: -200px 0 0 -400px; display:block">
               <table width="99%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#999999" style=" margin-left:6px">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        修改密码<input id="hfcommanycd" type="hidden"  runat="server"/></td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
               <table width="99%" border="0" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" style="display: block ; margin-left:6px">
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" colspan="2">
                     <table style="width:100%"  align="center" border="0" >
                        <tr>
                             <td height="28" bgcolor="#FFFFFF" align="left">
                          <img alt="" src="Images/Button/Bottom_btn_save.jpg" 
                            onclick="EditPwd();"/>
                                 <img alt="取消" src="Images/Button/Bottom_btn_cancel.jpg" 
                                     style="width: 51px; height: 25px"  onclick="ClearText();"/><input id="hf_psd" type="hidden" runat="server"/></td>
                      </tr>
                   </table>
                        </td>
                        </tr>
                    <tr>
                        <td align="right" bgcolor="#E6E6E6">
                            用户名：</td>
                        <td bgcolor="#FFFFFF">
                            <asp:TextBox ID="txt_User" runat="server" CssClass="tdinput" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            原密码<span class="redbold">*</span>：</td>
                        <td height="20" bgcolor="#FFFFFF" >
                            <asp:TextBox ID="txtOldPassword" runat="server" CssClass="tdinput" 
                                TextMode="Password"></asp:TextBox>
                        </td>
                        </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            新密码<span class="redbold">*</span>：</td>
                        <td height="20" bgcolor="#FFFFFF" >
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="tdinput" 
                                TextMode="Password"></asp:TextBox>
                        </td>
                        </tr>
                    <tr id="CloseDate">
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            确认新密码<span class="redbold">*</span>：</td>
                        <td height="20" bgcolor="#FFFFFF" >
                            <asp:TextBox ID="txtRePassword" runat="server" CssClass="tdinput" 
                                TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                
    </div>
    <uc1:Message ID="Message1" runat="server" />
    </form>
</body>
</html>
