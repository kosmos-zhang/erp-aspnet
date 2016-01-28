<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePsd.aspx.cs" Inherits="Pages_Office_SystemManager_ChangePsd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script src="../../../js/office/SystemManager/ChangePsd.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <title>无标题页</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #TypeName
        {
            width: 252px;
        }
        #TypeName0
        {
            width: 252px;
        }
        #TypeName1
        {
            width: 252px;
        }
        #txt_User
        {
            width: 230px;
        }
        #txtNewPassword
        {
            width: 230px;
        }
        #txtRePassword
        {
            width: 230px;
        }
    </style>

    <script type="text/javascript">
    function SelectedNodeChanged(userid)
    {
     document.getElementById("txt_User").value=userid;
     document.getElementById("txtNewPassword").value="";
     document.getElementById("txtRePassword").value="";
    }
    </script>

</head>
<body>
    <form id="SubscribeKeyWord" runat="server">
    <input type="hidden" id="keyid" />
    <uc1:Message ID="Message1" runat="server" />
    <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
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
                            强制修改密码
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="left" class="Title">
                            <img alt="" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="ShowOldPwd();"
                                id="btnSave" runat="server" visible="false" /><img alt="取消" src="../../../Images/Button/Bottom_btn_cancel.jpg"
                                    onclick="ClearText();" /><asp:HiddenField ID="hf_psd" runat="server" />
                            <asp:HiddenField ID="hfcommanycd" runat="server" />
                            <asp:HiddenField ID="hfuserid" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top" width="100%">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td bgcolor="#F4F0ED" class="Blue" align="left" width="600">
                            <table border="0" cellpadding="3" height="300" style="width: 138%">
                                <tr>
                                    <td valign="top" width="280" style="padding-left: 20px;">
                                        <div id="treeContainer" style="width: 336px; height: 500px; overflow: auto; border: solid 1px #999999;">
                                            <asp:TreeView ID="Tree_BillTpye" runat="server" ShowLines="True">
                                            </asp:TreeView>
                                        </div>
                                    </td>
                                    <td valign="top">
                                        <table cellpadding="2" width="500px">
                                            <tr>
                                                <td align="right">
                                                    用户名：<input type="hidden" id="txtID" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txt_User" name="TypeName" disabled="disabled" runat="server" /><font
                                                        color="red">*</font>
                                                </td>
                                            </tr>
                                            <%--<tr>
                            <td>大类标识：</td>
                            <td>
                                <select id="slFlag">
                                    <option value="-1">请选择</option>
                                    <option value="1">文献</option>
                                    <option value="2">范本</option>
                                </select><font color=red>*</font>
                            </td>
                        </tr>      --%>
                                            <tr>
                                                <td align="right">
                                                    新密码：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                    <font color="red">*</font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    确认新密码：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRePassword" runat="server" TextMode="Password"></asp:TextBox>
                                                    <font color="red">*</font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
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
    <div id="div_Add" style="border: #898989; background: #fff; padding: 10px; width: 400px;
        z-index: 1001; position: absolute; top: 53%; left: 68%; margin: -200px 0 0 -400px;
        display: none">
        <%--<iframe id="aaaa" style="position: absolute; z-index: -1; width:400px; height:10px;" frameborder="0">  </iframe>--%>
        <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
            style="margin-left: 30px">
            <tr>
                <td height="28" bgcolor="#FFFFFF">
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="InsertCodeReasonFee();" />
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
            <tr>
                <td align="right">
                    请输入原密码：
                </td>
                <td>
                    <asp:TextBox ID="txtOldPassword" Width="200px" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
