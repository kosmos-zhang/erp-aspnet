<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataKeyList.ascx.cs" Inherits="UserControl_Decision_DataKeyList" %>
<select id="Select1" onchange="DataChange(this)">
    <asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
     <option value="<%#Eval("DataID")%>"><%#Eval("DataName")%></option>
    </ItemTemplate>
    </asp:Repeater>
</select>