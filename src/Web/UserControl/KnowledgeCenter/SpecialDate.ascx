<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecialDate.ascx.cs" Inherits="UserControl_KnowledgeCenter_SpecialDate" %>

<asp:DropDownList ID="DDLSpecialDate" runat="server">
    <asp:ListItem Text="--请选择--" Value="-1"></asp:ListItem>
    <asp:ListItem Text="7天内" Value="7"></asp:ListItem>
    <asp:ListItem Text="1个月内" Value="30"></asp:ListItem>
    <asp:ListItem Text="3个月内" Value="90"></asp:ListItem>
    
</asp:DropDownList>
