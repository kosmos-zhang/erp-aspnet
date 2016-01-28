<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Company_Query.aspx.cs" Inherits="Pages_Office_SystemManager_Company_Query" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户管理</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/office/SystemManager/CompanyInfoModify.js" type="text/javascript"></script>
    <script type="text/javascript">
    </script>
</head>
<body>
    <form id="frmMain" runat="server" onsubmit="return submitFlag;">
<div class="divbox">
	<div class="divboxtitle"><span>企业列表：</span><div class="clearbox"></div></div>
	<div class="divbox">
	<div class="divboxtitle"> <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="DoModify();"  ImageUrl="~/Images/Button/Button_Edit.jpg" />
        &nbsp;
        <asp:ImageButton ID="ImageButton2" runat="server"  OnClientClick="DoDelete();" ImageUrl="~/Images/Button/Button_Delete.jpg" /><div class="clearbox"></div></div>
   <div id="BtnArea">
           <span style="font-size: 14px; font-weight: bold">企业信息管理</span> </div>
    <div class="divboxbody">
    	<div class="divboxbodyhack">
        
        </div>
    </div>
	<div class="divboxbodyhack">
        <asp:Label ID="lblMessage" runat="server" CssClass="redbold"></asp:Label>
        <asp:HiddenField ID="hidDelete" runat="server" />
    </div>
</div>
</form>
</body>
</html>
