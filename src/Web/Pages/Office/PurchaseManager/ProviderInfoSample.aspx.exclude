<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderInfoSample.aspx.cs" Inherits="Pages_Office_PurchaseManager_ProviderInfo" %>


<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>


<%@ Register src="../../../UserControl/ProviderInfo.ascx" tagname="ProviderInfo" tagprefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
        <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
        <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
        <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
        <script src="../../../js/office/PurchaseManager/ProviderInfoSample.js" type="text/javascript"></script>
        <script src="../../../js/common/page.js" type="text/javascript"></script>
        <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
        <script src="../../../js/common/Check.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Message ID="Message1" runat="server" />
    </div>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">
                                <img src="../../../images/Button/Show_add.jpg" width="34" height="24" style="cursor: hand" onclick="AddSignRow();" />
                                <img src="../../../images/Button/Show_del.jpg" width="34" height="24" style="cursor: hand" onclick="DeleteSignRow();" />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" id="dg_Log" style="behavior: url(../../../css/draggrid.htc)" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" align="center" bgcolor="#E6E6E6">序号<uc2:ProviderInfo ID="ProviderInfo1" runat="server" /></td>
                            <td align="center" bgcolor="#E6E6E6" class="Blue">选择<input type="checkbox" name="checkall" id="checkall" onclick="SelectAll()" /></td>
                            <td align="center" bgcolor="#E6E6E6" class="Blue">供应商编号</td>
                            <td align="center" bgcolor="#E6E6E6" class="Blue">供应商名称</td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr><td height="2" bgcolor="#999999"></td></tr>
                    </table>
 <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
    </form>
</body>
</html>
