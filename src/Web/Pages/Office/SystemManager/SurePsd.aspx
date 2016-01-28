<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurePsd.aspx.cs" Inherits="Pages_Office_SystemManager_SurePsd" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>请输入您的密码</title>
<base target="_self"></base>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
</head>
<body  style="background-color:#f1f1f1">
    <form id="form1" runat="server"><uc1:Message 
            ID="Message1" runat="server" />
             <div id="div_Add"    
                 >
    <table width="100%">
    <tr><td> 
        <asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="../../../Images/Button/Bottom_btn_save.jpg" 
            onclick="ImageButton1_Click" />
            <img alt="返回"  src="../../../Images/Button/Bottom_btn_back.jpg" onclick="closedfrm();"  /></td></tr>
            <tr><td><asp:Label ID="Label1" runat="server" Text="请输入密码："></asp:Label>
                <asp:TextBox ID="txt_oldpsd" runat="server"   Width="232px" 
                    TextMode="Password"></asp:TextBox></td></tr>
    </table>

</div>
    </form>
</body>
</html>
<script type="text/javascript">   
function closedfrm()
{
 this.window.opener = null;   
window.close(); 
}
  
</script> 
