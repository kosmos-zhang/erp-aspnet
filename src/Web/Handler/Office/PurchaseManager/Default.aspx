<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Handler_Office_PurchaseManager_Default" %>

<%@ Register src="../../../UserControl/ProductInfoControl.ascx" tagname="ProductInfoControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
        #txtMaterialNo
        {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td class="tdColTitle" width="10%">物料编码</td>
                <td class="tdColInput" width="23%">
                    <uc1:ProductInfoControl ID="ProductInfoControl1" runat="server" />
                    <input name="txtMaterialNo" id="txtMaterialNo" maxlength="25" type="text" onclick="popTechObj.ShowList('txtMaterialNo');" class="tdinput" size = "19" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
