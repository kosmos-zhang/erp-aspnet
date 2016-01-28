<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthEnd.aspx.cs" Inherits="Pages_Office_StorageManager_MonthEnd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>库存月结</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table align="center">
        <tr>
            <td>
                <asp:Button ID="btn_MonthEnd" runat="server" Text="月结" 
                    onclick="btn_MonthEnd_Click" /></td>
        </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
