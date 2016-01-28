<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="css/default.css" rel="stylesheet" type="text/css" />

    <script src="js/JQuery/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
         var Cookie = {
             set: function(name, value, min) {
                 var exp = new Date();
                 exp.setTime(exp.getTime() + min * 60 * 1000);
                 document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/";


             },
             get: function(name) {

                 var exp = "(^|[\\s]+)" + name + "=([^;]*)(;|$)";
                 var arr = document.cookie.match(new RegExp(exp));

                 if (arr != null)
                     return unescape(arr[2]);
                 return null;
             },

             del: function(name) {
                 var exp = new Date();
                 exp.setTime(exp.getTime() - 10000);
                 var cval = this.get(name);
                 if (cval != null)
                     document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/";
             }
         }
            
            
            
            //ff && ie Event start here
function SearchEvent()
{    
    if(document.all)
        return event;

    func=SearchEvent.caller;
    while(func!=null)
    {
        var arg0=func.arguments[0];             
        if(arg0)
        {
            if(arg0.constructor==MouseEvent) // 如果就是event 对象
                return arg0;
            if(arg0.constructor==KeyboardEvent) // 如果就是event 对象
                return arg0;
            if(arg0.constructor==Event) // 如果就是event 对象
                return arg0;
        }
        func=func.caller;
    }
    return null;
}
function GetEventCode(evt)
{
    if(typeof evt == "undefined" || evt == null)
    { 
        evt = SearchEvent();        
        if(typeof evt == "undefined" || evt == null)
        { 
            return null;
        }
    }
    
    return evt.keyCode || evt.charCode;
}

            function checkKey()
            {
                var code ;
                if(document.all)
                {
                    code = event.keyCode;
                }else{
                    code = GetEventCode();
                }
            
                if(code >= 97 && code <= 123)
                {
                    if(document.all)
                    {
                        event.keyCode = code-32;
                    }else{
                        evt.charCode =  code-32;
                    
                    }
                }
                
            }
    </script>

    <script src="js/login.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 68px;
        }
    </style>
</head>
<body>
    <form id="form1"  runat="server">

    
    <div id="divDefault" runat="server">
        </div>
        <div runat="server"  id="divVerConert">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" runat="server" id="tblDefault">
            <tr>
                <td background="Images/<%=CustomPath %>/bg_top.jpg"  style="height:78px">
                    <%--<img src="Images/<%=CustomPath %>/bg_top.jpg" width="29" height="78" />--%>
                </td>
            </tr>
        </table>
        <table width="795" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="135" background="Images/<%=CustomPath %>/bar_bg.jpg">
                                <img src="Images/<%=CustomPath %>/logo.jpg" width="129" height="45" />
                            </td>
                            <td align="right" background="Images/<%=CustomPath %>/bar_bg.jpg" class="white">
                                欢迎您使用<%=CustomSysName %>
                            </td>
                            <td width="23" align="right" background="Images/<%=CustomPath %>/bar_bg.jpg">
                                <img src="Images/<%=CustomPath %>/bar_right.jpg" width="23" height="45" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <table width="100%" height="365" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top" background="Images/<%=CustomPath %>/bg_main.jpg">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td height="34">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="390" height="220">
                                            <img src="Images/<%=CustomPath %>/main.jpg" />
                                        </td>
                                        <td>
                                            <table width="280" border="0" align="center" cellpadding="0" cellspacing="1">
                                                <tr>
                                                    <td>
                                                        <img src="Images/<%=CustomPath %>/login.jpg" width="118" height="27" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="2">
                                                            <tr>
                                                                <td width="21%" height="30" align="right">
                                                                    用户名：
                                                                </td>
                                                                <td colspan="2">
                                                                    <input name="txtUserID" id="txtUserID" type="text" size="25" style="width: 160px;"
                                                                        maxlength="10" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    密&nbsp;&nbsp;&nbsp;码：
                                                                </td>
                                                                <td colspan="2">
                                                                    <input type="password" id="txtPassword" name="txtPassword" style="width: 160px;"
                                                                        size="25" maxlength="16" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="30" align="right" width="21%">
                                                                    验证码：
                                                                </td>
                                                                <td width="16%">
                                                                    <input name="txtCheckCode" onkeypress="checkKey()" id="txtCheckCode" type="text"
                                                                        size="6" maxlength="4" />
                                                                </td>
                                                                <td>
                                                                    <img src="CheckCode.aspx" name="imgCheckCode" align="absmiddle" id="imgCheckCode" />
                                                                    <a onclick="ReloadCheckCode();" href="#">看不清?再来一张</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="21%" height="30" align="right">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <input name="chkUsername" id="chkUsername" type="checkbox" value="checkbox" />
                                                                    记住我的用户名<br />
                                                                    <input name="chkPassword" id="chkPassword" type="checkbox" value="checkbox" />
                                                                    记住我的密码
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="30" align="right">
                                                                    &nbsp;
                                                                </td>
                                                                <td height="&nbsp;</td">
                                                                    <td height="60" valign="top">
                                                                        <br />
                                                                        <img src="Images/<%=CustomPath %>/main_login.jpg" name="btnLogin" align="absmiddle" id="btnLogin"
                                                                            style="cursor: pointer" onclick="LoginSubmit();" /><br />
                                                                    </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table runat="server" id="panelNotice" align="right" width="80%" style="margin-right: 20px;">
                                    <tr>
                                        <td valign="top">
                                            <div style="height: 70px; overflow: auto; padding: 2px;">
                                                <asp:Label ID="lblSysNoticeContent" runat="server" Text=""></asp:Label></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1">
                        <tr>
                            <td width="300" height="40" valign="top" class="white">
                                
                            </td>
                            <td align="right" valign="top" class="white">
                            </td>
                        </tr>
                        <tr>
                            <td height="1" colspan="2" bgcolor="#CCCCCC" class="white">
                            </td>
                        </tr>
                        <tr>
                            <td height="30" class="white">
                            </td>
                            <td width="450" align="right" class="white">
                                推荐使用IE 7浏览器，建议分辨率为1280 X 800或以上。
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</div>
    <object id="USBSNReaderOCX1" classid="clsid:F8CE5E43-1135-11d4-A324-0040F6D487D9"
        width="0" height="0" style="display: none;">
    </object>
    </form>
</body>
</html>
