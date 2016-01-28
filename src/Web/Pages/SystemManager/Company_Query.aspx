<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Company_Query.aspx.cs" Inherits="Pages_SystemManager_Company_Query" 

 EnableViewState="true" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../css/default.css" rel="stylesheet" type="text/css" />

    <script src="../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../js/SystemManager/CompanyQuery.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"  onsubmit="return submitFlag;">
    <input type="hidden" name="hidDelete" />
 <div class="divboxtitle"><span>企业检索</span><div class="clearbox"></div></div>
   企业编码 <asp:TextBox ID="txtCompanyCD" runat="server"></asp:TextBox>
    企业中文名称<asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
      
     
       <asp:ImageButton ID="btnSearch" runat="server" Text="查询"  
        ImageUrl="~/Images/Button/Button_Check.jpg" OnClientClick="DoSearch();" onclick="btnSearch_Click"/>
     
     <div class="divbox">
	<div class="divboxtitle"><span>企业列表</span><div class="clearbox"></div></div>
    <div class="divboxbody">
    	<div class="divboxbodyhack">
        <%--    <uc1:PageControl ID="PageList" runat="server"  Visible="false" />--%>
        </div>
    </div>
        <div class="divboxbodyhack"> 
          <img src="../../Images/Button/Button_Add.jpg" onclick="DoAdd();" border="0"/>
          &nbsp;&nbsp;
          <img src="../../Images/Button/Button_Edit.jpg" onclick="DoModify();" />
          &nbsp;&nbsp;
 
          <asp:ImageButton ID="btnDelete" runat="server"    OnClientClick="DoDelete();" 
                ImageUrl="~/Images/Button/Button_Delete.jpg" onclick="btnDelete_Click" />
                  &nbsp;&nbsp;
                  
    <asp:ImageButton ID="btnlicense" runat="server"   ImageUrl="~/Images/Button/Button_Authorization .jpg" OnClientClick="DoLicense();" />  
               
        </div>
	<div class="divboxbodyhack">
        <asp:Label ID="lblMessage" runat="server"  ForeColor="Red" ></asp:Label>
    </div>
   </div>
    </form>
</body>
</html>
