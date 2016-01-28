<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RoleDrpControl.ascx.cs" Inherits="UserControl_RoleDrpControl" %>

<asp:DropDownList ID="Drp_RoleInfo" runat="server" Height="25px" Width="120px" onchange="GetRoleId();">
</asp:DropDownList>
<script type="text/javascript">
var ControlRI = new Object();
ControlRI.ddRoleInfoID = "";
function GetRoleId()
{
 var objDownList = '<%=Drp_RoleInfo.ClientID %>';

 ControlRI.ddRoleInfoID =document.getElementById(objDownList).value;
 try{
    document.getElementById('HiddenRoleId').value =  ControlRI.ddRoleInfoID;
 }
 catch(e){}
}
</script>