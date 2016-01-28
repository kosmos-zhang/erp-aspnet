<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Pages_Common_test" %>







<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script language="javascript" type="text/javascript" >
    
    function aa()
    {
     alert(document.getElementById("<%=TextBox1.ClientID %>").value);
    }
    </script>
    <style type="text/css">
        .style2
        {
            width: 117px;
        }
        .style3
        {
            width: 208px;
            background-color: #6699FF;
        }
        .style4
        {
            width: 92px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
 
 
 
 
 
 <div id="divLogin" style="border: solid 10px #898989; background: #fff; padding: 10px;
    width: 780px; z-index: 1001; position: absolute; display: none; top: 50%; left: 50%;
    margin: -200px 0 0 -400px; overflow: scroll">
</div>

    <table style="width:100%;">
        <tr>
            <td class="style2">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr  >
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <asp:ImageButton ID="ImageButton1" runat="server" Height="26px"  
                    ImageUrl="~/Images/Button/Bottom_btn_save.jpg" Width="62px"   />
            </td>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</form>


   
    <p>
        <input id="Button1" type="button" value="button"  /></p>


   
</body>
</html>
