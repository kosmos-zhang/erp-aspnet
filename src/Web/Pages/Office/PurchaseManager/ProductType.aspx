<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductType.aspx.cs" Inherits="Handler_Office_PurchaseManager_ProductType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>物料类型</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TreeView ID="ProductTypeTree" runat="server"  
            onselectednodechanged="ProductTypeTree_SelectedNodeChanged">
        </asp:TreeView>
    </div>
    </form>
</body>
</html>

    
