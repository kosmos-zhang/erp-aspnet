<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Pages_Office_SystemManager_test" %>

<%@ Register src="../../../UserControl/SearchProductUC.ascx" tagname="SearchProductUC" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />

        <uc1:SearchProductUC ID="SearchProductUC1" runat="server" />

    </div>
   <asp:DataList id="DataList1" runat="server"
           BorderColor="black"
           CellPadding="3"
           Font-Names="Verdana"
           Font-Size="8pt" onitemcommand="DataList1_ItemCommand" 
        onitemcreated="DataList1_ItemCreated" 
        onselectedindexchanged="DataList1_SelectedIndexChanged">

         <HeaderStyle BackColor="#aaaadd">
         </HeaderStyle>

         <AlternatingItemStyle BackColor="Gainsboro">
         </AlternatingItemStyle>

         <HeaderTemplate >
 
        <asp:LinkButton ID="LinkButton1"  Text="a" runat="server">LinkButton</asp:LinkButton>

         </HeaderTemplate>

         <ItemTemplate>

            <%# DataBinder.Eval(Container.DataItem, "Test") %>

         </ItemTemplate>

      </asp:DataList>

    <input id="Button2" type="button" value="button" onclick="popTechObj.ShowList()" /><asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px">
    </asp:DetailsView>

    </form>
</body>
</html>
